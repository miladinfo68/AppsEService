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

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ClassSeatSpecifyUI : System.Web.UI.Page
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
        ///ایجاد نموده ایم ExamBusiness یک شئ از کلاس 
        /// </summary>
        ExamBusiness ExamBusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            AccessControl1.MenuId = menuId;
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt = ExamBusiness.GetDidWithoutSeat();
                ddl_ClassCode.DataSource = dt;
                ddl_ClassCode.DataTextField = "did";
                ddl_ClassCode.DataBind();
                BindCityList();

            }
        }

        protected void BindCityList()
        {
            //var cityName = ExamBusiness.GetAllExamPlaceAddress().AsEnumerable()
            //    .Where(w => w.Field<bool>("IsActive"))
            //    .Select(s => new { Name = s.Field<string>("Name_City"), Id = s.Field<decimal>("ID") });
            //ddl_Shahr.DataSource = cityName;
            //ddl_Shahr.DataTextField = "Name";
            //ddl_Shahr.DataValueField = "Id";
            //ddl_Shahr.DataBind();
            var cityName = ExamBusiness.Get_ExamNameCity(Convert.ToInt32(Session[sessionNames.userID_Karbar]));
            ddl_Shahr.DataSource = cityName;
            ddl_Shahr.DataTextField = "Name_City";
            ddl_Shahr.DataValueField = "ID";
            ddl_Shahr.DataBind();
            ddl_Shahr.Items.Insert(0, new ListItem { Text = "انتخاب نمایید", Value = "-1" });
            if (cityName.Rows.Count == 1)
            {
                ddl_Shahr.SelectedIndex = 1;
                ddl_Shahr.Enabled = false;
            }
        }

        /// <summary>
        /// با فشردن کلید لیست کلاسها، کاربر به صفحه کد کلاسها هدایت می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("classlistUI.aspx?id=" + generaterandomstr(11) + "@A" + Session[sessionNames.menuID].ToString() + "-" + generaterandomstr(2));
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
        /// با فشردن کلید تخصیص صندلی، به صورت رندوم شماره صندلی به دانشجویان تخصیص می یابد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddl_ClassCode.SelectedValue.ToString() != "")
            {
                DataTable dtk = new DataTable();

                dtk = ExamBusiness.Check_Noduplicate_did(ddl_ClassCode.SelectedValue);

                if (dtk.Rows.Count > 0)
                {

                    rwm.RadAlert("برای این کد درس تخصیص صندلی انجام گرفته است", null, 100, "پیام", "");
                }
                else
                {
                    if (ddl_Shahr.SelectedItem.Text != "انتخاب کنید")
                    {
                        try
                        {
                            DataTable dt4 = new DataTable();
                            dt4 = ExamBusiness.Get_ExistClass(ddl_ClassCode.SelectedValue);

                            if (dt4.Rows.Count > 0)
                            {
                                DataTable dt1 = new DataTable();
                                dt1 = ExamBusiness.Get_did_detail(ddl_ClassCode.SelectedValue);
                                saatexam = dt1.Rows[0]["saatexam"].ToString();
                                dateexam = dt1.Rows[0]["dateexam"].ToString();

                                DataTable dt = new DataTable();
                                dt = ExamBusiness.Get_Stcode(int.Parse(ddl_ClassCode.SelectedValue.ToString()), saatexam, dateexam);

                                int tedad_daneshju = ExamBusiness.Get_tedad_daneshju(int.Parse(ddl_ClassCode.SelectedValue.ToString()), saatexam, dateexam);


                                int tedad_class = ExamBusiness.Get_tedad_class(int.Parse(ddl_ClassCode.SelectedValue.ToString()));


                                DataTable dt2 = new DataTable();
                                dt2 = ExamBusiness.Get_ExamClassSavedDetail(ddl_ClassCode.SelectedValue);


                                bool t = true;
                                if (tedad_class != 0)
                                {
                                    for (int i = 0; i < tedad_daneshju; i++)
                                    {
                                        t = true;
                                        Random random = new Random();
                                        halghe:
                                        while (t)
                                        {
                                            int randomNumber1 = random.Next(0, (tedad_class));
                                            classorder = (int)dt2.Rows[randomNumber1]["IDExamClass"];
                                            seatnumber = random.Next((int)dt2.Rows[randomNumber1]["StartRange"], ((int)dt2.Rows[randomNumber1]["EndRange"]) + 1);

                                            DataTable dtn = new DataTable();
                                            dtn = ExamBusiness.Get_No_DuplicateSeatNumber(saatexam, dateexam, seatnumber, int.Parse(ddl_ClassCode.SelectedValue.ToString()));
                                            if (dtn.Rows.Count > 0)
                                            {
                                                t = true;
                                                goto halghe;
                                            }
                                            else
                                            {
                                                t = false;
                                            }
                                            DataTable dt3 = new DataTable();
                                            dt3 = ExamBusiness.Get_ExamPlaceName(int.Parse(ddl_ClassCode.SelectedValue.ToString()));
                                            ExamPlace = dt3.Rows[randomNumber1]["ExamPlace"].ToString();
                                        }

                                        ExamBusiness.Insert_ExamSeat(dt.Rows[i]["stcode"].ToString(), int.Parse(ddl_ClassCode.SelectedValue.ToString()), seatnumber, ddl_Shahr.SelectedItem.Text);
                                    }

                                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 39, "تخصیص صندلی", int.Parse(ddl_ClassCode.SelectedValue.ToString()));

                                    rwm.RadAlert("تخصیص صندلی با موفقیت انجام شد", null, 100, "پیام", "");


                                }


                            }
                            else
                            {
                                rwm.RadAlert("برای این درس کلاس تعیین نشده است", null, 100, "پیام", "");
                            }



                        }
                        catch
                        {
                            rwm.RadAlert("خطا در انجام عملیات", null, 100, "پیام", "");
                        }
                    }

                    else
                    {

                        rwm.RadAlert("شهر را انتخاب نمایید", null, 100, "پیام", "");

                    }
                }
            }
            else
            {
                rwm.RadAlert("کد کلاس را انتخاب نمایید", null, 100, "پیام", "");
            }
        }


    }
}