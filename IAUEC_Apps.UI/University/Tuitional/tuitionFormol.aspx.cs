using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IAUEC_Apps.UI.University.Tuitional
{
    public partial class tuitionFormol : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
            //accessControl();
            if (!IsPostBack)
            {
                fillComponents();
            }
            accessControl();
        }
        private void accessControl()
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
            AccessControl.MenuId = menuId;
            Session[sessionNames.menuID] = menuId;
            AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
        }
        private void fillComponents()
        {
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();

            string currentTerm = CB.getCurrentTerm();
            txtCurrentTerm.Text = currentTerm;
            txtLastTerm.Text = CB.getLastTerm(currentTerm);
            txtEntryYear.Text = "13" + currentTerm.Substring(0, 2);
            txtByEntryYear.Text = (Convert.ToInt32(txtEntryYear.Text) - 1).ToString();
        }

        protected void btnIncrease_CurrentEntry_Click(object sender, EventArgs e)
        {
            modalConfirmText.Text = string.Format("آیا مایل هستید فرمول شهریه برای دانشجویان مقطع {0} ورودی {1} بر اساس کد فرمولی که در ترم {2} برای دانشجویان ورودی {3} ثبت شده، درج شود؟"
                ,ddlLevel.SelectedItem.Text, txtEntryYear.Text, txtLastTerm.Text.Replace("-2", "-1"), txtByEntryYear.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showConfirmModal();", true);

            hdnTypeInsert.Value = "CurrentEntry";

        }

        protected void btnIncrease_OtherEntries_Click(object sender, EventArgs e)
        {
            modalConfirmText.Text = string.Format("آیا مایل هستید افزایش شهریه متغیر برای دانشجویان {0}  بر اساس کد فرمولی که در ترم {1} ثبت شده، درج شود؟"
               , ddlLevel_OtherEntries.SelectedItem.Text, txtLastTerm.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showConfirmModal();", true);
            hdnTypeInsert.Value = "OtherEntries";
        }

        private void showMessage(string msg)
        {
            modalMsgText.Text = msg;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showMessage();", true);
        }

        protected void btnConfirmToInsert_Click(object sender, EventArgs e)
        {
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            bool r;
            switch (hdnTypeInsert.Value)
            {
                case "CurrentEntry":
                     r = CB.insertTuitionFormol(ddlLevel.SelectedItem.Value, txtEntryYear.Text.Substring(2, 2), txtCurrentTerm.Text, txtByEntryYear.Text.Substring(2, 2), txtLastTerm.Text.Replace("-2", "-1"), Convert.ToInt32(txtFixTuition.Text), Convert.ToInt32(txtVarTuition_CurrentEntry.Text), Convert.ToInt32(txtInsurance.Text), Convert.ToInt64(txtService.Text));
                    if (r)
                        showMessage(string.Format("کد فرمول برای مقطع {0} ورودی سال {1} بر اساس ورودی {2} درج شد", ddlLevel.SelectedItem.Text, txtEntryYear.Text, txtByEntryYear.Text));
                    else
                        showMessage("کد فرمول درج نشد");
                    break;
                case "OtherEntries":
                     r = CB.insertTuitionFormol_LastEntries(txtCurrentTerm.Text, txtLastTerm.Text, Convert.ToInt32(txtVarTuition_OtherEntries.Text), Convert.ToInt32(ddlLevel_OtherEntries.SelectedItem.Value));
                    if (r)
                        showMessage(string.Format("کد فرمول برای {0} برای نیمسال {1} بر اساس نیمسال {2} درج شد", ddlLevel_OtherEntries.SelectedItem.Text, txtCurrentTerm.Text, txtLastTerm.Text));
                    else
                        showMessage("کد فرمول درج نشد");
                    break;
            }



            hdnTypeInsert.Value = "";
        }
    }
}