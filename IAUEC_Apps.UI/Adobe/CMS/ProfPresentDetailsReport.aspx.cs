using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.DTO.AdobeClasses;
using System.Data;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class ProfPresentDetailsReport : System.Web.UI.Page
    {
        ProfessorPresentDetailsBusiness pb = new ProfessorPresentDetailsBusiness();
        StuPresentBusiness StuPresentBusiness = new StuPresentBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string code = HttpUtility.ParseQueryString(this.ClientQueryString).Get("code");
                string LOGIN = HttpUtility.ParseQueryString(this.ClientQueryString).Get("LOGIN");
                string Leniency = HttpUtility.ParseQueryString(this.ClientQueryString).Get("Leniency");
                string tterm = HttpUtility.ParseQueryString(this.ClientQueryString).Get("tterm");

                // نیمسال
                List<string> TermList = StuPresentBusiness.GetActiveTerm();
                foreach (var item in TermList)
                ddl_Nimsal.Items.Add(item.ToString());
                ddl_Nimsal.SelectedValue = tterm;

                txt_ProfCode2.Text = LOGIN;
                txt_ClassCode1.Text = code;
                //if (Leniency==null)                
                //    Leniency = "20";

                if (code == null)
                    code = "";

                //long f1;
                //if (long.TryParse(Leniency.ToString(), out f1) == true)
                //    txt_Erfaq.Text = Leniency;
                //else
                //    txt_Erfaq.Text = "20";
  
                if (code != "" && LOGIN != "" && code != "0")
                    ResultProfDetails(LOGIN, code, "", "");
                string mId = Request.QueryString["id"].ToString();
                string[] id = mId.ToString().Split(new char[] { '@' });
                string menuId = "";
                for (int i = 0; i < id[1].Length; i++)
                {
                    string s = id[1].Substring(i + 1, 1);
                    if (s != "-")
                        menuId += s;
                    else
                        break;
                }
                Session[sessionNames.menuID] = menuId;
                AccessControl1.MenuId = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();        
            }
           

        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            ResultProfDetails(txt_ProfCode2.Text, txt_ClassCode1.Text, date_input_1.Value, date_input_2.Value);            
        }



        public void ResultProfDetails(string ProfCode, string Class, string BeginDate, string EndDate)
        {
        
            int ClassCode = 0;
            //float f;
            //if (float.TryParse(Class, out f) == true)
                ClassCode =int.Parse(txt_ClassCode1.Text);

            DataTable sortedDT = new DataTable();
            sortedDT = pb.GetSortedDataTableByProfCodeClassCode(ProfCode, ClassCode, BeginDate, EndDate, ddl_Nimsal.SelectedValue.ToString());
                        
            //ارفاق
            long f1;
            long TimeErfaq = 0;
            if (long.TryParse(txt_Erfaq.Text, out f1) == true)
                TimeErfaq = long.Parse(txt_Erfaq.Text);
                                
            List<DemoProfPresentDTO> DemoooList = new List<DemoProfPresentDTO>();
            for (int i = 0; i < sortedDT.Rows.Count; i++)
            {
                if (!(i > 0
                    && sortedDT.Rows[i]["code"].ToString() == sortedDT.Rows[i - 1]["code"].ToString()
                    && sortedDT.Rows[i]["PersianDate"].ToString() == sortedDT.Rows[i - 1]["PersianDate"].ToString()
                    && sortedDT.Rows[i]["FirstLogin"].ToString() == sortedDT.Rows[i - 1]["FirstLogin"].ToString()
                    && sortedDT.Rows[i]["LastLogOut"].ToString() == sortedDT.Rows[i - 1]["LastLogOut"].ToString()
                    ))
                {
                    DemoProfPresentDTO Demooo1 = new DemoProfPresentDTO();
                    Demooo1.PersianDate = sortedDT.Rows[i]["PersianDate"].ToString();
                    Demooo1.TimeStart = sortedDT.Rows[i]["TimeStart"].ToString() + "-" + sortedDT.Rows[i]["TimeEND"].ToString();
                    Demooo1.TimeEND = sortedDT.Rows[i]["TimeEND"].ToString();
                    Demooo1.FirstLogin = sortedDT.Rows[i]["FirstLogin"].ToString();
                    Demooo1.LastLogOut = sortedDT.Rows[i]["LastLogOut"].ToString();
                    Demooo1.SumOfTime = sortedDT.Rows[i]["SumOfTime"].ToString();
                    Demooo1.TimeClass = sortedDT.Rows[i]["TimeClass"].ToString();
                    Demooo1.Late = "1";
                    Demooo1.Soon = "1";
                    Demooo1.Status = "1";
                    DemoooList.Add(Demooo1);

                    if (sortedDT.Rows[i]["TeacherName"].ToString() != "")
                        lbl_TeacherName.Text = "نام استاد: " + sortedDT.Rows[i]["TeacherName"].ToString();
                    if (sortedDT.Rows[i]["LessonName"].ToString() != "")
                        lbl_LessonName.Text = "نام کلاس: " + sortedDT.Rows[i]["LessonName"].ToString();
                }
            }

            int countHazer = 0;
            int countQayeb = 0;
            long CountLate = 0;
            long CountSoon = 0;
            long x10 = 0;
            long y10 = 0;
            long z10 = 0;

            // اصلاح تاخیر و تعجیل
            for (int i = 0; i < DemoooList.Count; i++)
            {
                DateTime a2 = DateTime.Parse(DemoooList.ElementAt(i).TimeEND);
                DateTime b2 = DateTime.Parse(DemoooList.ElementAt(i).LastLogOut);

                //تاخیر
                if (a2 < b2)
                {
                    DemoooList.ElementAt(i).Late = (b2 - a2).Minutes.ToString();
                    DemoooList.ElementAt(i).Soon = "0";
                    CountLate = CountLate + (b2 - a2).Minutes;
                }
                //تعجیل
                else
                {
                    DemoooList.ElementAt(i).Late = "0";
                    DemoooList.ElementAt(i).Soon = (a2 - b2).Minutes.ToString();
                    CountSoon = CountSoon + (a2 - b2).Minutes;
                }
                
                x10 = int.Parse(DemoooList.ElementAt(i).SumOfTime);
                y10 = int.Parse(DemoooList.ElementAt(i).TimeClass);

                //افزودن ارفاق
                z10 = (x10 + TimeErfaq * int.Parse(sortedDT.Rows[i]["v_nazari"].ToString())) - y10;
                if (z10 > -1)
                {
                    DemoooList.ElementAt(i).Status = "حضور";
                    countHazer++;
                }
                else
                {
                    DemoooList.ElementAt(i).Status = "غیبت";
                    countQayeb++;
                }
            }

            countQayeb = countQayeb + (16 - (countQayeb + countHazer));
            
            //تبدیل لیست به جدول
            DataTable DT4 = pb.ConvertToDataTable(DemoooList);
            grd_ConvertToDataTable.DataSource = DemoooList;//DT4;
            grd_ConvertToDataTable.DataBind();

            if (DT4.Rows.Count > 0)
            {
                lbl_TimeErfagh.Text = "زمان ارفاق: " + TimeErfaq.ToString() + " دقیقه";
                lbl_CountHazer.Text = "جمع حضور: " + countHazer + " جلسه حاضر";
                lbl_CountQayeb.Text = "جمع غایب: " + countQayeb + " جلسه غیبت";
                lbl_CountLate.Text = "جمع تاخیر: " + CountLate + " دقیقه";
                lbl_CountSoon.Text = "جمع تعجیل: " + CountSoon + " دقیقه";           
            }
            
        }


        protected void ConvertExcel_Click(object sender, ImageClickEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            if (grd_ConvertToDataTable.MasterTableView.Items.Count > 0)
            {
                grd_ConvertToDataTable.ExportSettings.FileName = string.Format("ProfessorTime_{0}_{1}", "Teacher", DateTime.Now);
                grd_ConvertToDataTable.ExportSettings.IgnorePaging = true;
                grd_ConvertToDataTable.ExportSettings.OpenInNewWindow = true;
                grd_ConvertToDataTable.ExportSettings.ExportOnlyData = true;
                grd_ConvertToDataTable.MasterTableView.UseAllDataFields = true;
                grd_ConvertToDataTable.MasterTableView.ExportToExcel();
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 19, "");

            }
        }



    }
}
    
