using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using IAUEC_Apps.Business.university.Exam;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;
using System.Drawing;
using IAUEC_Apps.Business.university.Education;

namespace IAUEC_Apps.UI.University.Exam.CMS
{

    public partial class classlistUI : System.Web.UI.Page
    {
        /// <summary>
        /// ساعت امتحان در این متغیر ذخیره می شود
        /// </summary>
        public string saatexam;
        /// <summary>
        /// تاریخ امتحان در این متغیر ذخیره می شود
        /// </summary>
        public string dateexam;
        /// <summary>
        /// این متغیر برای تشخیص شماره صندلی تکراری استفاده می گردد
        /// </summary>
        public int classorder;
        /// <summary>
        /// شماره صندلی در این متغیر ذخیره می شود
        /// </summary>
        public int seatnumber;
        /// <summary>
        /// محل امتحان در این متغیر ذخیره می گردد
        /// </summary>
        public string ExamPlace;
        /// <summary>
        /// کد درس در این متغیر ذخیره می گردد
        /// </summary>
        public string coursecode;
        /// <summary>
        /// ساعت امتحانات دروس ترم جاری در این جدول ذخیره می گردد
        /// </summary>
        DataTable dtsaat = new DataTable();
        /// <summary>
        /// تاریخ امتحانات دروس ترم جاری در این جدول ذخیره می گردد
        /// </summary>
        DataTable dtdate = new DataTable();
        /// <summary>
        /// ظرفیت هر درس در این جدول ذخیره می گردد
        /// </summary>
        DataTable dtzarfiat = new DataTable();

        //List<int> didList = new List<int>();
        DataTable baghimande = new DataTable();

        /// <summary>
        ///ایجاد می نماید ExamBusiness یک شئ از کلاس
        /// </summary>
        ExamBusiness examBusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        EducationReportBusiness ebusiness = new EducationReportBusiness();
        /// <summary>
        /// با لود شدن صفحه دراپ دان ساعت و تاریخ امتحانات از داده پر می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDateList();
                BindCityList();

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
            }
            AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
        }

        protected void BindDateList()
        {
            DataTable dtdate = new DataTable();
            dtdate = examBusiness.Get_Exam_dateexam();
            ddl_day.DataSource = dtdate;
            ddl_day.DataTextField = "dateexam";
            ddl_day.DataValueField = "dateexam";
            ddl_day.DataBind();
            ddl_day.Items.Insert(0, new ListItem("انتخاب کنید"));
        }
        protected void BindCityList()
        {
            //var cityName = examBusiness.GetAllExamPlaceAddress().AsEnumerable()
            //    .Where(w => w.Field<bool>("IsActive"))
            //    .Select(s => new { Name = s.Field<string>("Name_City"), Id = s.Field<decimal>("ID") });

            var cityName = examBusiness.Get_ExamNameCity(Convert.ToInt32(Session[sessionNames.userID_Karbar]));
            ddl_shahr.DataSource = cityName;
            ddl_shahr.DataTextField = "Name_City";
            ddl_shahr.DataValueField = "ID";
            ddl_shahr.DataBind();
            ddl_shahr.Items.Insert(0, new ListItem { Text = "انتخاب نمایید", Value = "-1" });
            if (cityName.Rows.Count == 1)
            {
                ddl_shahr.SelectedIndex = 1;
                ddl_shahr.Enabled = false;
            }
        }

        /// <summary>
        /// با فشردن کلید ثبت، اطلاعات کلاس ها نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddl_day.SelectedIndex != 0)
            {
                 if(ddl_shahr.SelectedValue !="-1")
                    LoadGridData();
                 else
                    rwm.RadAlert(" شهر را انتخاب نمایید", null, 100, "پیام", "");
            }
            else
            {
                rwm.RadAlert(" روز امتحان را انتخاب نمایید", null, 100, "پیام", "");
            }

        }

        public void LoadGridData()
        {
            DataTable dtm = new DataTable();
            dtm = examBusiness.zarfiat_per_cityes(ddl_saat.SelectedValue, ddl_day.SelectedValue, int.Parse(ddl_shahr.SelectedValue));
            dtzarfiat = examBusiness.Get_zarfiat_per_city(ddl_saat.SelectedValue, ddl_day.SelectedValue, int.Parse(ddl_shahr.SelectedValue));
            if (dtzarfiat.Rows.Count > 0)
            {
                if (dtm.Rows.Count > 0)
                {
                    var dids_in_city = dtm.AsEnumerable().Select(s => s.Field<string>("coursecode").ToString() ).ToList();
                    var cityId = int.Parse(ddl_shahr.SelectedValue??"-1");    
                    baghimande = examBusiness.CheckIsClassSetRange(dids_in_city, cityId);
                    hdnSearchedCity.Value = ddl_shahr.SelectedValue;
                    grd_Class.Visible = true;
                    grd_Class.DataSource = dtm;
                    grd_Class.DataBind();
                }
                else
                {
                    rwm.RadAlert("تخصیص داده شده است", null, 100, "پیام", "");
                }
            }
            else
            {
                grd_Class.Visible = false;
                rwm.RadAlert(" در این سانس کلاسی موجود نیست", null, 100, "پیام", "");
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

        /// <summary>
        /// با فشردن کلید تخصیص صندلی، کاربر به صفحه تخصیص صندلی هدایت می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClassSeatSpecifyUI.aspx?id=" + generaterandomstr(11) + "@A" + Session[sessionNames.menuID].ToString() + "-" + generaterandomstr(2), false);

        }

        protected void grd_Class_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "bookinglist")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                coursecode = commandArgs[0];  
                var did = commandArgs[0]?.ToString() ?? "-1";
                var Zarfiat = int.Parse(commandArgs[1]?.ToString() ?? "0");
                var cityId= int.Parse(commandArgs[2]?.ToString() ?? "0");
                var classCode = commandArgs[3]?.ToString() ?? "0";

                var dt = examBusiness.CheckIsClassSetRange(did , Zarfiat , cityId); 
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        rwm.RadAlert("برای این کد مشخصه، بازه کلاس تخصیص یافته است", null, 100, "پیام", "");
                    }

                    else
                    {
                        RadWindowManager windowManager = new RadWindowManager();
                        RadWindow widnow1 = new RadWindow();
                        //widnow1.NavigateUrl = "../CMS/classrangelistUI.aspx?Zarfiat=" + Zarfiat + "&Coursecode=" + coursecode + "&c=" + hdnSearchedCity.Value;
                        widnow1.NavigateUrl = "../CMS/classrangelistUI.aspx?Zarfiat=" + Zarfiat + "&Coursecode=" + coursecode + "&c=" + cityId.ToString()+"&classCode="+ classCode;
                        widnow1.ID = "RadWindow1";
                        windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(800);
                        windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                        widnow1.VisibleOnPageLoad = true;
                        windowManager.OnClientClose = "ReloadGrid";
                        windowManager.Windows.Add(widnow1);
                        ContentPlaceHolder mpContentPlaceHolder;
                        mpContentPlaceHolder =
                         (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                        mpContentPlaceHolder.Controls.Add(widnow1);
                        //Response.Redirect("classrangelistUI.aspx?Zarfiat=" + Zarfiat + "&Coursecode=" + coursecode + "&c=" + ddl_shahr.SelectedValue + "&id=" + generaterandomstr(11) + "@A" + Session["menuId"].ToString() + "-" + generaterandomstr(2), false);

                    }
                }
            }
            if (e.CommandName == "SeatSpecify")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                coursecode = commandArgs[0];
                string Zarfiat = commandArgs[1];               
                var classCode= commandArgs[3];
                var cityName = examBusiness.GetAllExamPlaceAddress().AsEnumerable()
                .Where(w => w.Field<bool>("IsActive") && w.Field<int>("ID") == int.Parse(ddl_shahr.SelectedValue))
                .Select(s => s.Field<string>("Name_City")).FirstOrDefault();
                DataTable dtk = new DataTable();
                dtk = examBusiness.Check_Noduplicate_did(coursecode, cityName);

                if (dtk.Rows.Count > 0)
                {

                    rwm.RadAlert("برای این کد درس تخصیص صندلی انجام گرفته است", null, 100, "پیام", "");
                }
                else
                {

                    //DataTable dt1 = new DataTable();
                    //dt1 = examBusiness.Get_did_detail(int.Parse(coursecode));
                    //saatexam = dt1.Rows[0]["saatexam"].ToString();
                    //dateexam = dt1.Rows[0]["dateexam"].ToString();

                    DataTable dt = new DataTable();
                    dt = examBusiness.GetStudentByDidAndExamPlace(coursecode, int.Parse(ddl_shahr.SelectedValue));

                    //int tedad_daneshju = examBusiness.Get_tedad_daneshju(int.Parse(coursecode), saatexam, dateexam);


                    // int tedad_class = examBusiness.Get_tedad_class(int.Parse(coursecode));


                    DataTable ExamClass = new DataTable();
                    ExamClass = examBusiness.Get_ExamClassSavedDetail(coursecode, cityName);


                    bool t = true;
                    //if (tedad_class != 0)
                    if (ExamClass.Rows.Count > 0)
                    {
                        List<int> classsaved = new List<int>();
                        for (int j = 0; j < ExamClass.Rows.Count; j++)
                        {
                            for (int z = int.Parse(ExamClass.Rows[j]["StartRange"].ToString()); z <= int.Parse(ExamClass.Rows[j]["EndRange"].ToString()); z++)
                            {
                                classsaved.Add(z);
                            }

                        }
                        for (int i = 0; i < int.Parse(Zarfiat); i++)
                        {

                            Random random = new Random();


                            int randomNumber1 = random.Next(0, (classsaved.Count));
                            //    if (classsaved.Count>=1)
                            //{
                            examBusiness.Insert_ExamSeat(dt.Rows[i]["stcode"].ToString(), int.Parse(coursecode), classsaved[randomNumber1], cityName);

                            classsaved.RemoveAt(randomNumber1);
                            // }

                            //do
                            //{
                            //     int randomNumber1 = random.Next(0, (classsaved.Count));
                            //     classorder = (int)ExamClass.Rows[randomNumber1]["IDExamClass"];
                            //     seatnumber = random.Next((int)ExamClass.Rows[randomNumber1]["StartRange"], ((int)ExamClass.Rows[randomNumber1]["EndRange"]) + 1);

                            //} while (examBusiness.Get_No_DuplicateSeatNumber(ddl_saat.SelectedValue, ddl_day.SelectedValue, seatnumber, int.Parse(coursecode)).Rows.Count>0);
                            //examBusiness.Insert_ExamSeat(dt.Rows[i]["stcode"].ToString(), int.Parse(coursecode), seatnumber, ddl_shahr.SelectedItem.Text);

                        }

                        cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 39, "تخصیص صندلی", int.Parse(classCode));
                        rwm.RadAlert("تخصیص صندلی با موفقیت انجام شد", null, 100, "پیام", "");
                    }
                }
            }
        }

        protected void grd_Class_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem itemAmount = (GridDataItem)e.Item;
                Button btn_Taeid = e.Item.FindControl("btn_Taeid") as Button;
                Button btn_Chair = e.Item.FindControl("btn_Chair") as Button;
                string coursecode = itemAmount.GetDataKeyValue("coursecode").ToString();
                //DataTable dtclass = new DataTable();
                //dtclass = examBusiness.CheckIsClassSetRange(int.Parse(coursecode), hdnSearchedCity.Value);
                //if (dtclass.Rows.Count > 0)
                if (baghimande.Rows.Count > 0)
                {
                    //if (dtclass.Rows[0][0].ToString() == "0")
                    int? remainCapcity = baghimande.AsEnumerable()
                        .Where(w => w.Field<string>("did") == coursecode)
                        .Select(s =>s.Field<int?>("baghimande") ).FirstOrDefault();
                    if (remainCapcity > 0 )
                    {
                        btn_Taeid.Enabled = true;
                        btn_Chair.Enabled = false;
                    }
                    else
                    {
                        //btn_Taeid.Enabled = false;
                        btn_Taeid.BackColor = Color.Green;
                        //btn_Chair.Enabled = true;
                    }

                    //if (remainCapcity <= 0)
                    //{
                    //    btn_Taeid.Enabled = false;
                    //    btn_Taeid.BackColor = Color.Green;
                    //    btn_Chair.Enabled = true;
                    //}
                    //else
                    //{
                    //    btn_Taeid.Enabled = true;
                    //    btn_Chair.Enabled = false;
                    //}

                }
                DataTable dtk = new DataTable();
                dtk = examBusiness.Check_Noduplicate_did(coursecode, hdnSearchedCity.Value);

                if (dtk.Rows.Count > 0)
                {
                    btn_Chair.Enabled = false;
                    btn_Chair.BackColor = Color.Red;
                }

            }

        }

        protected void ddl_day_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtsaat = new DataTable();
            dtsaat = examBusiness.GetSaatExamByDateExam(ddl_day.SelectedValue);
            ddl_saat.DataSource = dtsaat;
            ddl_saat.DataTextField = "saatexam";
            ddl_saat.DataValueField = "saatexam";
            ddl_saat.DataBind();
        }


    }
}