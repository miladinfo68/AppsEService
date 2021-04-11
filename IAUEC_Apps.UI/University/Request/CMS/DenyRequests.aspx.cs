using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using System.Data;
using System.Text;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class DenyRequests : System.Web.UI.Page
    {
        CheckOutRequestBusiness business = new CheckOutRequestBusiness();
        DataTable dt = new DataTable();

        string stcode;
        string stName;
        int reqID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string mId = Request.QueryString["id"].ToString();
                //string[] id = mId.ToString().Split(new char[] { '@' });
                //string menuId = "";
                //for (int i = 0; i < id[1].Length; i++)
                //{
                //    string s = id[1].Substring(i + 1, 1);
                //    if (s != "-")
                //        menuId += s;
                //    else
                //        break;
                //    Session["MenuId"] = menuId;
                //}
                //AccessControl1.MenuId = Session["MenuId"].ToString();
                //AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            stcode = txtStcode.Text;
            ViewState.Add("stCode", stcode);
            BindGrid();
        }

        protected void grdResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            DropDownList drpReqStatus = (DropDownList)currentRow.FindControl("drpReqStatus") as DropDownList;
            reqID = Convert.ToInt32(currentRow.Cells[1].Text);
            stcode = currentRow.Cells[2].Text;
            stName = currentRow.Cells[3].Text;

            ViewState.Add("stcode", stcode);
            ViewState.Add("stName", stName);
            ViewState.Add("reqID", reqID);

            if (e.CommandName == "History")
            {
                CommonBusiness cmb = new CommonBusiness();

                lst_history.DataSource = cmb.GetUserLogByModifyId(int.Parse(e.CommandArgument.ToString()), 12);
                lst_history.DataBind();

                info1.InnerText = "نام دانشجو:" + ViewState["stName"].ToString();
                info2.InnerText = "شماره درخواست:" + ViewState["reqID"].ToString();

                stcode = ViewState["stCode"].ToString();
                stName = ViewState["stName"].ToString();
                reqID = Convert.ToInt32(ViewState["reqID"]);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            if (e.CommandName == "Deny")
            {
                string message;
                CheckOutRequestBusiness CheckBusiness = new CheckOutRequestBusiness();
                dt = (DataTable)ViewState["dt"];
                GridViewRow row = grdResult.Rows[0];
                int eraeBe = 5;
                int currentStatus = 5;
                int reqType = Convert.ToInt32(dt.Rows[0]["RequestTypeID"]);
                TextBox txtDenyReason = (TextBox)grdResult.Rows[0].Cells[6].FindControl("txtDenyReason");
                message = business.DenyCheckOutRequestByCurrentStatus(Session[sessionNames.userID_Karbar].ToString(), currentStatus, eraeBe, reqID, reqType);
                business.SendMessage(Session[sessionNames.userID_Karbar].ToString(), reqID, txtDenyReason.Text);
                CheckBusiness.DeleteCheckOutFromFraghat(reqID);
                txtDenyReason.Text = "";

                RadWindowManager1.RadAlert(message, 0, 100, " پیام سیستم", "");
                BindGrid();
            }
        }

        private void BindGrid()
        {
            dt = business.GetStudentInfo(stcode);
            ViewState.Add("dt", dt);
            grdResult.DataSource = dt;
            grdResult.DataBind();
        }

    }
}