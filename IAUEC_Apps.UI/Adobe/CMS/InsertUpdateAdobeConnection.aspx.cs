using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class InsertUpdateAdobeConnection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setTermSource();

            }
        }

        private void setTermSource()
        {
            ddlTerm.Visible = true;
            txtTerm.Visible = false;
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            ddlTerm.DataSource = CB.GetAllAdobeConnectionTerms();
            ddlTerm.DataTextField = "term";
            ddlTerm.DataValueField = "term";
            ddlTerm.DataBind();
            if (ddlTerm.Items.Count > 0)
            {
                setComponentValue(ddlTerm.SelectedItem.Value);
            }
            else
            {
                ddlTerm.Visible = false;
                txtTerm.Visible = true;
            }

        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            setComponentValue(ddlTerm.SelectedItem.Value);
        }

        private void setComponentValue(string term)
        {
            DAO.Adobe.SettingDAO stDAO = new DAO.Adobe.SettingDAO();
            Business.Adobe.SettingBusiness st = new Business.Adobe.SettingBusiness();
            var setting = st.GetSettingByTermC(term);
            txtConName.Text = setting.ConName;
            txtConString.Text =stDAO.Decrypt(setting.DBConName,true);
            txtDomainName.Text = setting.DomainName;
            txthName.Text = setting.hName;
           
            txthPass.Text = setting.hpass;
            txtvName.Text = setting.vName;
            txtvPass.Text = setting.vpass;

            txtAdminPassword.Text = setting.aPass!=""?stDAO.Decrypt(setting.aPass,true): setting.aPass;
        }

        protected void btnUpdateInsert_Click(object sender, EventArgs e)
        {
            Business.Adobe.AdobeBusiness ab = new Business.Adobe.AdobeBusiness();
            string term = "";
            if (ddlTerm.Visible)
                term = ddlTerm.SelectedItem.Value;
            else
                term = txtTerm.TextWithLiterals;
            ab.AddOrUpdateConnectionString(term, txtConString.Text, txtConName.Text, txtDomainName.Text, txthPass.Text, txtvPass.Text, txthName.Text, txtvName.Text,txtAdminPassword.Text.Trim());
            setTermSource();
            if (ddlTerm.Items.Contains(new ListItem(term, term)))
            {
                ddlTerm.SelectedValue = term;
                setComponentValue(term);
            }
        }

        protected void btnNewConnection_Click(object sender, EventArgs e)
        {
            txtConName.Text = "";
            txtConString.Text = "";
            txtDomainName.Text = "";
            txthName.Text = "";
            txthPass.Text = "";
            txtvName.Text = "";
            txtvPass.Text = "";
            txtvPass.TextMode = TextBoxMode.SingleLine;
            txthPass.TextMode = TextBoxMode.SingleLine;
            ddlTerm.Visible = false;
            txtTerm.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlTerm.Visible = true;
            txtTerm.Visible = false;
            setComponentValue(ddlTerm.SelectedItem.Value);
        }
    }
}