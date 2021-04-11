using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class AcceptAgreement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setddlUserTypeSource();
            }
        }

        protected void grdAgreement_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            int hrID = 0;
            try
            {
                hrID = Convert.ToInt32(e.CommandArgument.ToString());
            }
            catch
            {
                return;
            }

            switch (e.CommandName)
            {
                case "ShowAgreement":
                    showAgreementFile(hrID,e.Item.Cells[3].Text,Convert.ToInt32(ddlUserType.SelectedItem.Value), (ddlAgreementType.SelectedItem.Value.ToString() == "2" ? "s" : "v"));
                    break;
                case "History":
                    showHistory(hrID);
                    break;
            }
        }

        protected void grdAgreement_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            setGridSource(getAgreementStatusToSearch());

        }

        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            setGridSource(getAgreementStatusToSearch());
            grdAgreement.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlAgreementType.SelectedIndex < 0 || (ddlAgreementType.SelectedValue != "1" && ddlAgreementType.SelectedValue != "2"))
            {
                return;
            }
            setGridSource(getAgreementStatusToSearch());
            grdAgreement.DataBind();
        }

        private void setddlUserTypeSource()
        {
            ListItem l9 = new ListItem(DTO.RoleEnums.مدیر_کل_امور_پژوهشی.ToString().Replace("_", " "), ((int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی).ToString());
            ListItem l77 = new ListItem(DTO.RoleEnums.سرپرست_واحد.ToString().Replace("_", " "), ((int)DTO.RoleEnums.سرپرست_واحد).ToString());
            ddlUserType.Items.Clear();
            switch (Session[sessionNames.roleID].ToString())
            {
                case "1":
                    ddlUserType.Items.Add(l9);
                    ddlUserType.Items.Add(l77);
                    break;
                case "9":
                    ddlUserType.Items.Add(l9);

                    break;
                case "77":
                    ddlUserType.Items.Add(l77);

                    break;
            }
        }

        private int getAgreementStatusToSearch()
        {
            int status = -1;

            switch (Convert.ToInt32(ddlAgreementType.SelectedItem.Value))
            {
                case 1://تایید شده
                    switch (Convert.ToInt32(ddlUserType.SelectedItem.Value))
                    {
                        case 77:
                            status = 2;
                            break;
                        case 9:
                            status = 1;
                            break;

                    }
                    break;
                case 2://تایید نشده
                    switch (Convert.ToInt32(ddlUserType.SelectedItem.Value))
                    {
                        case 77:
                            status = 1;
                            break;
                        case 9:
                            status = 0;
                            break;

                    }
                    break;
            }
            return status;
        }

        private void setGridSource(int status)
        {
            Business.university.CooperationRequest.CooperationRequestBusiness fbsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
            DataTable dt = fbsn.getAgreementByStatus(status);
            grdAgreement.DataSource = dt;
        }

        private void showAgreementFile(int hrID,string codeOstad,int userRole,string showType)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openWin('pc=" + codeOstad + "&hc=" + hrID + "&uc=" + userRole + "+&vs=" + showType + "');", true);

        }
        private void showHistory(int hrID)
        {
            Business.Common.CommonBusiness cmb = new Business.Common.CommonBusiness();
            lst_history.DataSource = cmb.getAgreementHistory(hrID);
            lst_history.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
}