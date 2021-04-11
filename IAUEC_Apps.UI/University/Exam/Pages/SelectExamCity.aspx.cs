using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.Pages
{
    public partial class SelectExamCity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ExamBusiness ExamBusiness = new ExamBusiness();
            DataTable dtcity = ExamBusiness.Get_Student_CityName(Session[sessionNames.userID_StudentOstad].ToString());
            if (!IsPostBack)
            {

                DataTable dtExamCity = new DataTable();
                dtExamCity = ExamBusiness.Get_St_ExamPlace();
                ddl_examCity.DataTextField = "Name_City";
                ddl_examCity.DataValueField = "ID";
                ddl_examCity.DataSource = dtExamCity;
                ddl_examCity.DataBind();
                ddl_examCity.Items.Insert(0, new ListItem("انتخاب کنید"));


                if (dtcity.Rows.Count > 0)
                {
                    ddl_examCity.SelectedValue = dtcity.Rows[0][1].ToString();
                    if (dtcity.Rows[0]["ID_EXAM_PLACE"].ToString() == "39")
                    {
                        ddl_examCity.SelectedItem.Text = "برون مرزی";
                        ddl_examCity.Enabled = false;
                        btn_Save.Enabled = false;
                    }
                    lbl_cityName.Text = "دانشجوی گرامی شما محل امتحان خود را شهر " + dtcity.Rows[0]["Name_City"].ToString() + " انتخاب نموده اید";
                    lbl_cityName.Visible = true;
                }

            }

            CommonBusiness cmnb = new CommonBusiness();
            DataTable dt = cmnb.GetSystemAvailability(8, 1, 0);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["IsSysOpen"].ToString() != "1")
                {
                    rwm.RadAlert("دانشجوی گرامی مطابق زمانبندی مراجعه نمایید", null, 100, "پیام", "");
                    ddl_examCity.Enabled = false;
                    btn_Save.Enabled = false;
                }

            }

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {

            try
            {
                //----------------
                ExamBusiness ExamBusiness = new ExamBusiness();
                CommonBusiness cmnb = new CommonBusiness();
                if (ddl_examCity.SelectedValue == "41")//tehran qarb
                {
                    var stdCountInWestTehranZone = ExamBusiness.GetCountOfStudentInTehranWestZone(int.Parse(ddl_examCity.SelectedValue));
                    if (stdCountInWestTehranZone > 2000)
                    {
                        rwm.RadAlert(".دانشجوی گرامی ظرفیت حوزه انتخابی تکمیل می باشد لطفا حوزه دیگری را انتخاب نمایید ", null, 100, "پیام", "");
                        return;
                    }
                }

                ExamBusiness.Update_Webmelli_Student_ExamPlace(Session[sessionNames.userID_StudentOstad].ToString(), int.Parse(ddl_examCity.SelectedValue));
                cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), 8, 27, "");
                rwm.RadAlert("ثبت با موفقیت انجام شد", null, 100, "پیام", "");
                try
                {
                    DataTable dtst = new DataTable();
                    dtst = ExamBusiness.Get_Student_CityName(Session[sessionNames.userID_StudentOstad].ToString());

                    if (dtst.Rows.Count > 0)
                    {
                        lbl_cityName.Text = "دانشجوی گرامی شما محل امتحان خود را شهر " + dtst.Rows[0]["Name_City"].ToString() + " انتخاب نموده اید";
                        lbl_cityName.Visible = true;
                    }
                    //else
                    //{
                    //    rwm.RadAlert("محلی انتخاب نشده است", null, 100, "پیام", "");
                    //}

                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch
            {
                rwm.RadAlert("خطا در ثبت", null, 100, "پیام", "");
            }
        }
    }
}