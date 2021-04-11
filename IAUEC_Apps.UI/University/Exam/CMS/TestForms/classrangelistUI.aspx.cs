using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Exam.CMS.TestForms
{
    public partial class classrangelistUI : System.Web.UI.Page
    {
        /// <summary>
        ///ایجاد می گردد ExamBusiness یک شئ از کلاس
        /// </summary>
        ExamBusiness ExamBusiness = new ExamBusiness();
        /// <summary>
        /// رنج شروع بازه صندلی در این متغیر قرار می گیرد
        /// </summary>
        public int Startrangeid;
        /// <summary>
        /// رنج پایان بازه صندلی در این متغیر قرار می گیرد
        /// </summary>
        public int Endrangeid;
        /// <summary>
        /// آخرین رنج پایانی شماره صندلی که اختصاص پیدا کرده در این متغیر قرار می گیرد
        /// </summary>
        public int MaxEndRange;
        /// <summary>
        /// ظرفیت پرشده کلاس در این متغیر قرار می گیرد
        /// </summary>
        public int Porshode;
        /// <summary>
        /// این متغیر در محاسبه ظرفیت به کار می رود
        /// </summary>
        public static int Start;
        /// <summary>
        /// نام شهر در این متغیر قرار می گیرد
        /// </summary>
        public static string city;
        /// <summary>
        /// کد درس در این متغیر قرار می گیرد
        /// </summary>
        public string coursecode;
        /// <summary>
        /// ظرفیت هر کلاس در این متغیر قرار می گیرد
        /// </summary>
        public static string zarfiat;

        CommonBusiness cmnb = new CommonBusiness();

        /// <summary>
        ///با لود شدن صفحه، ظرفیت هر کلاس تعیین می گردد و دراپ دان مح و بازه کلاس از داده پر می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            coursecode = Request.QueryString["Coursecode"];
            zarfiat = Request.QueryString["Zarfiat"];
            city = Request.QueryString["c"];
            DataTable Porshode = new DataTable();
            Porshode = (ExamBusiness.Get_ZarfiateBaghimande(coursecode, city));
            lbl_Porshode.Text = Porshode.Rows[0][0].ToString();
            lbl_Zarfiat.Text = zarfiat;
           
            if (!IsPostBack)
            { 
                bindgrid();
                BindExamPlace(city);
            }
        }

        protected void BindExamPlace(string cityId)
        {
            var cityName = ExamBusiness.GetAllExamPlaceAddress().AsEnumerable()
                .Where(w => w.Field<decimal>("ID") == decimal.Parse(cityId))
                .Select(s => s.Field<string>("Name_City")).FirstOrDefault();
            DataTable dt = new DataTable();
            dt = ExamBusiness.Get_ExamPlaceName(cityName);
            ddl_ClassNumber.DataSource = dt;
            ddl_ClassNumber.DataTextField = "ExamPlace";
            ddl_ClassNumber.DataValueField = "ExamPlaceID";
            ddl_ClassNumber.DataBind();
            ddl_ClassNumber.Items.Insert(0, "انتخاب نمایید");
            ddl_ClassNumber.SelectedIndex = 0;
        }

        /// <summary>
        /// با فشردن کلید ثبت، بازه شروع و پایان برای هر کلاس تخصیص می یابد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ddl_az.SelectedValue.ToString() != "انتخاب نمایید" && ddl_ClassNumber.SelectedValue.ToString() != "انتخاب نمایید" && ddl_ta.SelectedValue.ToString() != "انتخاب نمایید")
            {
                DataTable dt = new DataTable();

                int porshode1 = ExamBusiness.Insert_ExamClassSaved(coursecode, int.Parse(ddl_ClassNumber.SelectedValue), int.Parse(ddl_az.SelectedValue), int.Parse(ddl_ta.SelectedValue), city);

                if (porshode1 == int.Parse(zarfiat))
                {

                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 40, "تخصیص بازه", int.Parse(coursecode));
                   
                    rwm.RadAlert("تخصیص کلاس با موفقیت انجام شد", null, 100, "پیام", "");
                   // btn_takhsis.Visible = true;
                }

                else
                {

                    rwm.RadAlert("لطفا تخصیص کلاس را تکمیل نمایید" + "از ظرفیت" + (int.Parse(zarfiat) - porshode1) + "نفر باقی مانده است", null, 100, "پیام", "");
                }
                bindgrid();
                lbl_Porshode.Text = porshode1.ToString();
            }
            else
            {
                rwm.RadAlert("لطفا کلیه گزینه ها انتخاب گردد", null, 100, "خطا", "");

            }

        }

        /// <summary>
        /// با انتخاب شماره کلاس از دراپ دان، دراپ دان بازه شروع و پایان آن کلاس از داده پر می شوند
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ClassNumber.SelectedIndex == 0)
            {
                if (ddl_az.Items.Count>0)
                {
                    ddl_az.SelectedIndex = 0;
                }
                if (ddl_ta.Items.Count>0)
                {
                     ddl_ta.SelectedIndex = 0;
                }
               
                rwm.RadAlert("لطفا یک گزینه انتخاب گردد", null, 100, "هشدار", "");

            }
            else
            {
                Startrangeid = ExamBusiness.Get_StartRange(int.Parse(ddl_ClassNumber.SelectedValue),coursecode);

                Endrangeid = ExamBusiness.Get_EndRenge(int.Parse(ddl_ClassNumber.SelectedValue));


                if (ddl_az.Items.Count > 0)
                {
                    ddl_az.Items.Clear();

                }

                ddl_az.Items.Insert(0, "انتخاب نمایید");
                DataTable dt4 = new DataTable();
                dt4 = ExamBusiness.Get_SaatDateExam_perID(coursecode);

                DataTable dts = new DataTable();
                dts = ExamBusiness.Get_MaxEndRange(dt4.Rows[0]["saatexam"].ToString(), dt4.Rows[0]["dateexam"].ToString(), int.Parse(ddl_ClassNumber.SelectedValue));
                string maxrange = dts.Rows[0]["endrange"].ToString();

                if (maxrange == "")
                {

                    Start = Startrangeid;

                }
                else
                {
                    Start = (int.Parse(maxrange) + 1);
                }


                for (int i = Start; i <= Endrangeid; i++)
                {
                    ListItem item = new ListItem(i.ToString(), i.ToString());
                    ddl_az.Items.Add(i.ToString());

                }
            }
        }

        /// <summary>
        /// با انتخاب رنج شروع بازه، بر حسب ظرفیت کلاس و دانشجویان، رنج های بازه ای که می توان انتخاب نمود در دراپ دان پر می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_az.SelectedIndex == 0 || ddl_ClassNumber.SelectedIndex==0)
            {
                if (ddl_ta.Items.Count>0)
                {
                     ddl_ta.SelectedIndex = 0;
                }
               
                rwm.RadAlert("لطفا یک گزینه انتخاب گردد", null, 100, "هشدار", "");

            }
            else
            {

           
                Endrangeid = ExamBusiness.Get_EndRenge(int.Parse(ddl_ClassNumber.SelectedValue));
                DataTable porshode = new DataTable();
                porshode = ExamBusiness.Get_ZarfiateBaghimande(coursecode, Request.QueryString["c"]);
                if (ddl_ta.Items.Count > 0)
                {
                    ddl_ta.Items.Clear();

                }
                ddl_ta.Items.Insert(0, "انتخاب نمایید");
                if (porshode.Rows[0]["p"].ToString() != "")
                {
                    if ((Endrangeid - (Start - 1)) < ((int.Parse(zarfiat) - int.Parse(porshode.Rows[0]["p"].ToString()))))
                    {
                        for (int i = (int.Parse(ddl_az.SelectedValue)); i <= Endrangeid; i++)
                        {
                            ListItem item = new ListItem(i.ToString(), i.ToString());
                            ddl_ta.Items.Add(item);
                        }

                    }
                    else
                    {
                        for (int i = (int.Parse(ddl_az.SelectedValue)); i <= (int.Parse(ddl_az.SelectedValue) + (((int.Parse(zarfiat) - int.Parse(porshode.Rows[0]["p"].ToString())) - 1))); i++)
                        {
                            ListItem item = new ListItem(i.ToString(), i.ToString());
                            ddl_ta.Items.Add(item);
                        }
                    }
                }
                else
                {
                    if ((Endrangeid - (Start - 1)) < (int.Parse(zarfiat)))
                    {
                        for (int i = (int.Parse(ddl_az.SelectedValue)); i <= Endrangeid; i++)
                        {
                            ListItem item = new ListItem(i.ToString(), i.ToString());
                            ddl_ta.Items.Add(item);
                        }

                    }
                    else
                    {
                        for (int i = (int.Parse(ddl_az.SelectedValue)); i <= (int.Parse(ddl_az.SelectedValue) + (int.Parse(zarfiat) - 1)); i++)
                       // for (int i = (int.Parse(ddl_az.SelectedValue)); i <= (int.Parse(ddl_az.SelectedValue) + (((int.Parse(zarfiat) - 0) - 1))); i++)
                        {
                            ListItem item = new ListItem(i.ToString(), i.ToString());
                            ddl_ta.Items.Add(item);
                        }
                    }
                }
            }
        }
        public string generaterandomstr(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        protected void grd_Class_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int commandArgs = int.Parse(e.CommandArgument.ToString());
                ExamBusiness.DeleteExamClassSavedById(commandArgs);
                bindgrid();
            }
        }
        private void bindgrid()
        {
            var cityName = ExamBusiness.GetAllExamPlaceAddress().AsEnumerable()
                .Where(w => w.Field<decimal>("ID") == decimal.Parse(Request.QueryString["c"]))
                .Select(s => s.Field<string>("Name_City")).FirstOrDefault();
            DataTable dtClass_saved = ExamBusiness.Get_ExamClassSavedDetail(coursecode, cityName);
            if (dtClass_saved.Rows.Count > 0)
            {

                grd_Class.Visible = true;
                grd_Class.DataSource = dtClass_saved;
                grd_Class.DataBind();
            }
            else
            {
                grd_Class.Visible = false;
            }
            lbl_Porshode.Text = ExamBusiness.Get_ZarfiateBaghimande(coursecode, Request.QueryString["c"]).Rows[0][0].ToString();
            ddl_ClassNumber.SelectedIndex = 0;
            ddl_az.Items.Clear();
            ddl_ta.Items.Clear();
        }
      
    }
}