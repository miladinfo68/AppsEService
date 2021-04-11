using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.CooperationRequest;
using System.Data;
using IAUEC_Apps.DTO.University.Request;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class ShowNewHokmToPortalUser : System.Web.UI.Page
    {
        const string substringScanFile = "ScanMadarek";
        CooperationRequestBusiness CRB = new CooperationRequestBusiness();
        CommonBusiness CB = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setGridSource();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                setGridSource();
            }
        }

        protected void grdHokm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "showHokm")
            {
                int hokmId = Convert.ToInt32(e.CommandArgument);
                dvNewHokm.Visible = true;
                setHokmInfoSource(hokmId);
                dvSeen.Visible = true;
                string scrp = "function f(){var win = $find(\"" + rwShowLastHokm.ClientID + "\"); win.show(); if (!win.isClosed()) {win.center();} Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }
            else if(e.CommandName == "OldHokm")
            {
                setOldHokmSource(Convert.ToInt32(e.CommandArgument));
                string scrp = "function f(){var win = $find(\"" + rwOldHokmList.ClientID + "\"); win.show(); if (!win.isClosed()) {win.center();} Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);

            }
        }



        private void setHokmInfoSource(int hokmId)
        {
            ProfessorHokmDTO oNewHokm = CRB.getHokmInfoByHokmId(hokmId);
            if (oNewHokm.HokmId == 0)
            {
                lblHokmId.InnerText = "کد حکم : " + hokmId.ToString();
                dvHokmBody.Visible = false;
                return;
            }
            dvHokmBody.Visible = true;
            hdnReqId.Value = hokmId.ToString();
            lblHokmId.InnerText = "کد حکم : " + hokmId.ToString();

            DataTable dtUniName = CB.GetNameUni_fcoding();
            for (int i = 0; i <= dtUniName.Rows.Count - 1; i++)
            {
                dtUniName.Rows[i][0] = dtUniName.Rows[i][0].ToString().Replace("ي", "ی");
            }
            drpPastUni.DataSource = dtUniName;
            drpPastUni.DataTextField = "namecoding";
            drpPastUni.DataValueField = "ID";
            drpPastUni.DataBind();
            drpPastUni.SelectedValue = oNewHokm.Uni_Khedmat.ToString();
            txtCodeOstad.Text = oNewHokm.Code_Ostad.ToString();
            txtHokmNumber.Text = oNewHokm.Number_Hokm.ToString();
            txtDateEjraHokm.Text = oNewHokm.Date_RunHokm;
            txtDateSodoorHokm.Text = oNewHokm.Date_Hokm;
            txtMablaghHokm.Text = oNewHokm.MablaghHokm.ToString();
            txtPaye.Text = oNewHokm.Payeh.ToString();
            imgNewHokm.Visible = false;
            string hokmUrl = oNewHokm.HokmUrl.ToString();
            if (hokmUrl.Length > 0)
            {
                int subIndex = hokmUrl.IndexOf(substringScanFile) + substringScanFile.Length + 1;
                imgNewHokm.HRef = "OpenScanImage.aspx?o=" + oNewHokm.Code_Ostad.ToString() + "&f=" + hokmUrl.Substring(subIndex);
                imgNewHokm.Visible = true;
            }
            drpHireType.SelectedValue = oNewHokm.Type_Estekhdam.ToString();
            if (drpMartabe.Items.FindByValue(oNewHokm.Martabeh.ToString()) != null)
                drpMartabe.SelectedValue = oNewHokm.Martabeh.ToString();
            else
                drpMartabe.SelectedValue = "-1";
            rdblHireType.SelectedValue = oNewHokm.Nahveh_Hamk.ToString();
            chkBoundHour.Checked = oNewHokm.BoundHour;

        }

        protected void btnSeen_Click(object sender, EventArgs e)
        {
            if (cbxSeen.Checked)
            {
                CRB.updateHokmSeenStatus(Convert.ToInt32(hdnReqId.Value), true);
            }
            setGridSource();
        }
        
        private void setGridSource()
        {
            
                grdHokm.DataSource = null;
                grdHokm.DataBind();
                DataTable dtSource = CRB.getAllHokmToShowToPortal(txtFamily.Text.Replace("ي", "ی"), fromDate.Text, toDate.Text, txtNationalCode.Text, Convert.ToInt32(drpDegree.SelectedValue));
                if (dtSource.Rows.Count > 0)
                {
                    grdHokm.DataSource = dtSource;
                    grdHokm.DataBind();
                    dvNewHokm.Visible = false;
                }
            
        }

        private void setOldHokmSource( int infoID)
        {
            Business.university.Request.ProfessorRequestBusiness pr = new Business.university.Request.ProfessorRequestBusiness();
            var hokm = pr.GetLastHokmInfoByInfoPeopleID_Datatable(infoID);
            grdOldHokm.DataSource = hokm;
            grdOldHokm.DataBind();
        }

        protected void grdHokm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            grdHokm.PageIndex = e.NewPageIndex;
            setGridSource();
        }

        protected void grdOldHokm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "showOldHokm")
            {
                int hokmId = Convert.ToInt32(e.CommandArgument);
                dvNewHokm.Visible = true;
                setHokmInfoSource(hokmId);
                dvSeen.Visible = false;
                string scrp = "function f(){var win = $find(\"" + rwShowLastHokm.ClientID + "\"); win.show(); if (!win.isClosed()) {win.center();} Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }
        }

        protected void grdHokm_DataBinding(object sender, EventArgs e)
        {
            Business.university.Request.ProfessorRequestBusiness pr = new Business.university.Request.ProfessorRequestBusiness();

        }
    }
}