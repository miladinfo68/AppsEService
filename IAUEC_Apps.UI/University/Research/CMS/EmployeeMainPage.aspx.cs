using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Research.CMS
{
    public partial class EmployeeMainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int rolId = Convert.ToInt32(Session[sessionNames.roleID]);
            if (rolId != (int)DTO.RoleEnums.مدیر_ارشد && rolId != (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی)
            {
                Response.Redirect("~/commonui/CommonCmsIntro.aspx");
            }
            setPermitionDatasource();
        }

        protected void btnLoginPortal_Click(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar].ToString() == "1")
            {
                string usercode = "1032";
                e_Status.Value = "1";
                form1.Action = "http://thesis.iauec.ac.ir/Index.aspx";
                userCode.Value = usercode;
                e_Code.Value = "iauec_unit503";
                ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);
            }
        }
        private void setPermitionDatasource()
        {
            Business.university.Research.ResearchBusiness b = new Business.university.Research.ResearchBusiness();
            grdPermit.DataSource = b.PortalEntryPermition();
            grdPermit.DataBind();
        }

        protected void btnPermit_Click(object sender, EventArgs e)
        {
            if (txtStcode.Text != "" && Business.Common.CommonBusiness.IsNumeric(txtStcode.Text))
            {
                Business.university.Research.ResearchBusiness b = new Business.university.Research.ResearchBusiness();
                b.permitStudentToEnterPortal(txtStcode.Text);
            }
            else
            {

            }
            setPermitionDatasource();

        }

        protected void btnDontPermit_Click(object sender, EventArgs e)
        {
            if (txtStcode.Text != "" && Business.Common.CommonBusiness.IsNumeric(txtStcode.Text))
            {
                Business.university.Research.ResearchBusiness b = new Business.university.Research.ResearchBusiness();
                b.dontPermitStudentToEnterPortal(txtStcode.Text);
            }
            else
            {

            }
            setPermitionDatasource();

        }
    }
}