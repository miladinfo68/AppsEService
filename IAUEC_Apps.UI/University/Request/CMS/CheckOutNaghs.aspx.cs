using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutNaghs : System.Web.UI.Page
    {
        CheckOutNaghsBusiness oNaghsBus = new CheckOutNaghsBusiness();
        CheckOutRequestBusiness business = new CheckOutRequestBusiness();
        LoginBusiness lngB = new LoginBusiness();
        DataTable userdt = new DataTable();
        string userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userID = Session[sessionNames.userID_Karbar].ToString();
                userdt = lngB.Get_UserRoles(userID);
                if (userdt.Rows.Count > 1 || (Convert.ToInt32(userdt.Rows[0][1]) == 1))
                {
                    ToMultipleRoleMode(userdt);
                }
                else
                {
                    ToSingleRoleMode(userdt);
                }
            }
        }

        private void ToSingleRoleMode(DataTable userdt)
        {
            int currentstatus = business.GetStatusOfRole(Convert.ToInt32(userdt.Rows[0][1]));
            BindData(currentstatus);
        }

        private void ToMultipleRoleMode(DataTable dtu)
        {
            drpUserRoles.Enabled = true;
            drpUserRoles.Visible = true;
            grdNaghsList.Visible = true;
            if (Convert.ToInt32(dtu.Rows[0][1]) == 1)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            else
            {
                foreach (DataRow row in userdt.Rows)
                {
                    CheckOutStatusEnum.CheckOutAllStatusEnum status = (CheckOutStatusEnum.CheckOutAllStatusEnum)business.GetStatusOfRole(Convert.ToInt32(row[1]));
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (!String.IsNullOrWhiteSpace(li.Text) && li.Value != "22")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            drpUserRoles.Items.Insert(0, "انتخاب کنید");
            if (drpUserRoles.SelectedIndex != 0)
            {
                BindData((Convert.ToInt32(drpUserRoles.SelectedValue)));
            }
        }

        private void BindData(int nextstatus)
        {
            ViewState.Add("status", nextstatus);
            grdNaghsList.DataSource = oNaghsBus.GetAllNaghsByStatusId(nextstatus);
            grdNaghsList.EmptyDataText = "هیچ درخواست تسویه ای پیدا نشد.";
            grdNaghsList.DataBind();
        }

        protected void drpUserRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpUserRoles.SelectedIndex != 0)
            {
                grdNaghsList.Visible = true;
                BindData((Convert.ToInt32(drpUserRoles.SelectedValue)));
            }
            else
            {
                grdNaghsList.Visible = false;
            }


        }

        protected void grdNaghsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int naghsId = Convert.ToInt32(e.CommandArgument);
            int currentstatus = Convert.ToInt32(ViewState["status"]);
            if (e.CommandName == "message")
            {
                hdfNaghsId.Value = naghsId.ToString();
                string scrp5 = "function f(){$find(\"" + rdwMessage.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp5, true);
            }
            if (e.CommandName == "resolve")
            {
                bool flag = oNaghsBus.ResolveNaghsById(naghsId);
                if (flag)
                {
                    RadWindowManager1.RadAlert("نقص پرونده بر طرف شد.", 300, 100, "پیام", "");
                }
                else
                {
                    RadWindowManager1.RadAlert("خطا در هنگام برطرف سازی نقص لطفا مجددا تلاش نمایید.", 300, 100, "پیام", "");
                }
                BindData(currentstatus);
            }
        }

        protected void grdNaghsList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                int reqLogId = Convert.ToInt32(rowView["RequestLogId"]);
                int erae_Be = Convert.ToInt32(rowView["Erae_Be"]);
                //bool isResolved = Convert.ToBoolean(rowView["IsResolved"]);


                TableCell cellreqLodId = (TableCell)e.Row.Cells[3];
                cellreqLodId.Text = business.GetPersianStatus(reqLogId);
                TableCell cellerae_Be = (TableCell)e.Row.Cells[4];
                cellerae_Be.Text = business.GetPersianStatus(erae_Be);
                //TableCell cellisResolved = (TableCell)e.Row.Cells[7];
                //if (isResolved)
                //{
                //    cellisResolved.Text = "رفع نقص";
                //    cellisResolved.CssClass = "text-success";
                //}
                //else
                //{
                //    cellisResolved.Text = "عدم رفع نقص";
                //    cellisResolved.CssClass = "text-danger";
                //}
            }
        }

        protected void btnSubmitResoleDesc_Click(object sender, EventArgs e)
        {
            int naghsId = Convert.ToInt32(hdfNaghsId.Value);
            int currentstatus = Convert.ToInt32(ViewState["status"]);
            bool flag = false;
            try
            {
                flag = oNaghsBus.AddResolveMessage(naghsId, txtResolveDescription.Text);
            }
            catch (Exception)
            {

                throw;
            }
            if (flag)
            {
                RadWindowManager1.RadAlert("پیام شما ثبت شد.", 300, 100, "پیام", "");
            }
            else
            {
                RadWindowManager1.RadAlert("خطا در هنگام ثبت پیام لطفا مجددا تلاش نمایید.", 300, 100, "خطا", "");
            }
            BindData(currentstatus);
        }
    }
}