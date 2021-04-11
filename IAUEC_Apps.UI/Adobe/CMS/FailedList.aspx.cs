using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Adobe;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class FailedList : System.Web.UI.Page
    {
        AssetBusiness assetB = new AssetBusiness();
        CommonBusiness cmb = new CommonBusiness();
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
            Session[sessionNames.menuID] = menuId;
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            if (!IsPostBack)
            {
                CommonBusiness cmb = new CommonBusiness();
                cmb_Term.DataSource = cmb.getActiveTerm_AdobeConnection();
                cmb_Term.DataTextField = "Term";
                cmb_Term.DataValueField = "Term";

                cmb_Term.DataBind();
            }
        }
        protected void headerChkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in grd_FailedList.MasterTableView.Items)
            {
                (dataItem.FindControl("chk") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
            }
        }
        protected void Btn_returnToList_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem grd in grd_FailedList.Items)
            {
                TableCell fd = (TableCell)grd["FileDate"];
                TableCell classcode = (TableCell)grd["class_code"];
                TableCell term = (TableCell)grd["term"];
                CheckBox check = (CheckBox)grd.FindControl("chk");
                if (check.Checked)
                {
                    assetB.set_archive(classcode.Text,fd.Text,term.Text, false);
                    cmb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 20, "");
                }

            }
            grd_FailedList.Dispose();
            grd_FailedList.DataSource = assetB.GetAssetListByTerm(cmb_Term.SelectedValue, true);
            grd_FailedList.DataBind();
        }

        protected void cmb_Term_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grd_FailedList.DataSource = assetB.GetAssetListByTerm(cmb_Term.SelectedValue, true);
            grd_FailedList.DataBind();
        }

        protected void grd_FailedList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grd_FailedList.DataSource = assetB.GetAssetListByTerm(cmb_Term.SelectedValue, true);
        }
    }
}