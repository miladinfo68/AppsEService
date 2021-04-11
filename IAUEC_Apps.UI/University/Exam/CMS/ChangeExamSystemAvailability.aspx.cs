using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ChangeExamSystemAvailability : System.Web.UI.Page
    {
        CommonBusiness CBusiness = new CommonBusiness();
        ExamBusiness Ebusiness = new ExamBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddl_Status.Items.Add(new ListItem("انتخاب نمایید", "0"));
                ddl_Status.Items.Add(new ListItem("آپلود سوالات باز شود", "1"));
                ddl_Status.Items.Add(new ListItem("آپلود سوالات بسته شود", "2"));
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
           
            if (txt_CodeOs.Text == "")
            {
                rwm.RadAlert("لطفا کد استاد را وارد نمایید", null, 100, "خطا", "");
            }
            else
            {
                DataTable dt=new DataTable();
                dt= Ebusiness.GetOstadPermision(int.Parse(txt_CodeOs.Text));
                if(dt.Rows.Count==0)
                {
                    rwm.RadAlert("استادی با این مشخصات موجود نمی باشد", null, 100, "خطا", "");
                }
                
                Ebusiness.UpdateOstadPermission(int.Parse(txt_CodeOs.Text));
                CBusiness.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 66, "باز و بسته کردن آپلود سوالات برای یک استاد", int.Parse(txt_CodeOs.Text));
                rwm.RadAlert("امکان آپلود سوالات برای این استاد فعال شد", null, 100, "پیام", "");
            }
        }

        protected void btn_Sabt_Click(object sender, EventArgs e)
        {
            if (ddl_Status.SelectedValue.ToString() == "0")
            {
                rwm.RadAlert("لطفا وضعیت را انتخاب نمایید", null, 100, "پیام", "");
            }
            else
            {
                bool status=false;
                if (ddl_Status.SelectedValue.ToString() == "1")
                    status = true;
                if (ddl_Status.SelectedValue.ToString() == "2")
                    status = false;
                CBusiness.UpdateSystemAvailability(8, status);
                CBusiness.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 66, "باز و بسته کردن آپلود سوالات");

                rwm.RadAlert("با موفقیت انجام شد", null, 100, "پیام", "");
 
            }
        }
    }
}