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
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;

namespace IAUEC_Apps.UI.University.Exam.CMS.TestForms
{
    public partial class AssignSeat : System.Web.UI.Page
    {
        /// <summary>
        ///ایجاد نموده ایم ExamBusiness یک شئ از کلاس
        /// </summary>
        ExamBusiness ExamBusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        EducationReportBusiness ebusiness = new EducationReportBusiness();
        /// <summary>
        ///از طریق این متد انجام می گیرد Error ارسال پیغام
        /// </summary>
        /// <param name="errorDesc">The error desc.</param>
        public void message(string errorDesc)
        {
            string script = "alert('" + errorDesc + "');";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "UserSecurity", script, true);
        }
        /// <summary>
        /// با لود شدن صفحه دراپ دان ساعت و روز امتحان از داده پر می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDropdownLists();
            }
        }

        protected void BindDropdownLists()
        {
            DataTable dtExamDate = new DataTable();
            dtExamDate = ExamBusiness.Get_Exam_dateexam();
            ddl_Day2.DataSource = dtExamDate;
            ddl_Day2.DataTextField = "dateexam";
            ddl_Day2.DataValueField = "dateexam";
            ddl_Day2.DataBind();
            ddl_Day2.Items.Insert(0, new ListItem("انتخاب کنید"));

            DataTable dtExamTime = new DataTable();
            dtExamTime = ExamBusiness.Get_Exam_saatexam();
            ddl_Time2.DataSource = dtExamTime;
            ddl_Time2.DataTextField = "saatexam";
            ddl_Time2.DataValueField = "saatexam";
            ddl_Time2.DataBind();
            ddl_Time2.Items.Insert(0, new ListItem("انتخاب کنید"));


            ddl_ExamDate.DataSource = dtExamDate;
            ddl_ExamDate.DataTextField = "dateexam";
            ddl_ExamDate.DataValueField = "dateexam";
            ddl_ExamDate.DataBind();
            ddl_ExamDate.Items.Insert(0, new ListItem("انتخاب کنید"));


            ddl_ExamTime.DataSource = dtExamTime;
            ddl_ExamTime.DataTextField = "saatexam";
            ddl_ExamTime.DataValueField = "saatexam";
            ddl_ExamTime.DataBind();
            ddl_ExamTime.Items.Insert(0, new ListItem("انتخاب کنید"));

            //var cityName = ExamBusiness.GetAllExamPlaceAddress().AsEnumerable()
            //    .Where(w => w.Field<bool>("IsActive"))
            //    .Select(s => new { Name = s.Field<string>("Name_City"), Id = s.Field<decimal>("ID") });
            var cityName = ExamBusiness.Get_ExamNameCity(Convert.ToInt32(Session[sessionNames.userID_Karbar]));

            ddlCity.DataSource = cityName;
            ddlCity.DataTextField = "Name_City";
            ddlCity.DataValueField = "ID";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("انتخاب کنید"));

            ddlCityDelete.DataSource = cityName;
            ddlCityDelete.DataTextField = "Name_City";
            ddlCityDelete.DataValueField = "ID";
            ddlCityDelete.DataBind();
            ddlCityDelete.Items.Insert(0, new ListItem("انتخاب کنید"));

            if (cityName.Rows.Count == 1)
            {
                ddlCity.SelectedIndex = 1;
                ddlCityDelete.SelectedIndex = 1;

                ddlCity.Enabled = false;
                ddlCityDelete.Enabled = false;
            }
        }

        /// <summary>
        /// با فشردن کلید تخصیص صندلی، به صورت رندوم شماره صندلی تخصیص می یابد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_AssignSeat_Click(object sender, EventArgs e)
        {
            if (ddl_ExamDate.SelectedIndex != 0 && ddl_ExamTime.SelectedIndex != 0 && ddlCity.SelectedIndex != 0)
            {
                string Examdate = ddl_ExamDate.SelectedValue.ToString();
                string ExamTime = ddl_ExamTime.SelectedValue.ToString();

                try
                {
                    var list = ExamBusiness.ListAllStudentsAndDID(Examdate, ExamTime, Convert.ToInt32(ddlCity.SelectedItem.Value)).AsEnumerable();
                    var didList = list.Select(s => s.Field<string>("did")).Distinct();
                    int filledSeats = ExamBusiness.GetFilledSeats(ddlCity.SelectedItem.Text, Examdate, ExamTime);
                    foreach(var did in didList)
                    {
                        var studentList = list.Where(w => w.Field<string>("did") == did).Select(s=> s.Field<string>("STCODE"));
                        var ClassCapacity = studentList.Count();
                        Random RandomSeat = new System.Random();
                        
                        int Stu_Seat = 0;
                        int seat_SRange = int.Parse(ExamBusiness.GetMinStartRange().Rows[0]["StartRange"].ToString()) + filledSeats;
                        int seat_ERange = seat_SRange + (ClassCapacity);
                        foreach (string student in studentList)
                        {
                            if (!ExamBusiness.CheckSeatIsAssigned(did, student, ddlCity.SelectedItem.Text))
                            {
                                do Stu_Seat = RandomSeat.Next(seat_SRange, seat_ERange);
                                while (ExamBusiness.CheckSeatNumberByTerm(Stu_Seat, Examdate, ExamTime, ddlCity.SelectedItem.Text));

                                DataTable dtexamplace = ExamBusiness.GetExamPlaceBySeatAndCity(Stu_Seat, int.Parse(ddlCity.SelectedItem.Value));
                                ExamBusiness.AssignSeatNumberToStudent(Stu_Seat, student, did, ddlCity.SelectedItem.Text, dtexamplace.Rows[0]["ExamPlace"].ToString());
                                filledSeats ++;
                            }
                        }
                    }

                    //DataTable dt_did = ExamBusiness.GetAllClassInDate(Examdate, ExamTime);
                    //DataTable dtsrange = new DataTable();
                    //dtsrange = ExamBusiness.GetMinStartRange();
                    //int seat_SRange = int.Parse(dtsrange.Rows[0]["StartRange"].ToString());
                    //int seat_ERange;


                    //for (int i = 0; i < dt_did.Rows.Count; i++)
                    //{
                    //    int did = int.Parse(dt_did.Rows[i]["did"].ToString());
                    //    int Stu_Seat = 0;
                    //    DataTable dt_Student = ExamBusiness.GetAllStudentByClassInDate(did, ddlCity.SelectedItem.Value);
                    //    int ClassCapacity = dt_Student.Rows.Count;
                    //    seat_ERange = seat_SRange + (ClassCapacity);
                    //    Random RandomSeat = new System.Random();

                    //    foreach (DataRow item in dt_Student.Rows)
                    //    {
                    //        do
                    //        {
                    //            Stu_Seat = RandomSeat.Next(seat_SRange, seat_ERange);

                    //        } while (ExamBusiness.CheckSeatNumberByTerm(Stu_Seat, Examdate, ExamTime, ddlCity.SelectedItem.Text));

                    //        string stcode = item["stcode"].ToString();
                    //        DataTable dtexamplace = ExamBusiness.GetExamPlaceBySeatAndCity(Stu_Seat);
                    //        ExamBusiness.AssignSeatNumberToStudent(Stu_Seat, stcode, did, ddlCity.SelectedItem.Text, dtexamplace.Rows[0]["ExamPlace"].ToString());

                    //    }

                    //    seat_SRange = seat_ERange;
                    //}


                    //cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 48, "تخصیص اتومات صندلی" + Examdate + "-" + ExamTime);
                    rwm.RadAlert("صندلی های این سانس تخصیص داده شد", null, 100, "پیام", "");
                }

                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                rwm.RadAlert("سانس باید انتخاب گردد", null, 100, "خطا", "");
            }

        }



        protected void btn_Delete_Click(object sender, EventArgs e)
        {

            if (ddl_Time2.SelectedIndex != 0 && ddl_Day2.SelectedIndex != 0 && ddlCityDelete.SelectedIndex != 0)
            {
                ExamBusiness.DeleteFromExamSeat(ddl_Time2.SelectedValue.ToString(), ddl_Day2.SelectedValue.ToString(), ddlCityDelete.SelectedItem.Text);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 120, "حذف تخصیص اتومات صندلی " + ddl_Day2.SelectedValue.ToString() + "-" + ddl_Time2.SelectedValue.ToString());

                rwm.RadAlert("با موفقیت حذف گردید", null, 100, "پیام", "");
            }
            else
            {
                rwm.RadAlert("سانس باید انتخاب گردد", null, 100, "خطا", "");
            }
        }

      
        

    }
}