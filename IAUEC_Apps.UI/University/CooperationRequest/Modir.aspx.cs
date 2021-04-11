using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Faculty;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hire;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class Modir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string roleId = Session["RoleId"].ToString();
                //if (roleId == "1")
                //{

                //}
                //else
                //{
                //    Response.Redirect("../../CommonUI/CommonCmsIntro.aspx");
                //}
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCodeMeli.Text == string.Empty && txtName.Text == string.Empty)
            {
                string message = "حداقل باید یکی از فیلدها را پر کنید";
                RadWindowManager1.RadAlert(message, 0, 100, " پیام سیستم", "");
            }
            else
            {
                FacultyReportsBusiness FRB = null;
                try
                {
                    string ssn = txtCodeMeli.Text;
                    string name = txtName.Text;

                    FRB = new FacultyReportsBusiness();
                    DataTable user = FRB.GetInfoPeoByCodeMeliAndFamily(ssn, name);
                    if (user.Rows.Count > 0)
                    {
                        grdProfessorStatus.DataSource = user;
                        grdProfessorStatus.DataBind();
                        if(Session["RoleID"].ToString()=="11" || Session["RoleID"].ToString() == "12")
                        {
                            grdProfessorStatus.Columns[10].Visible = false;
                        }
                    }
                    else
                        RadWindowManager1.RadAlert("اطلاعات یافت نشد", 300, 100, "پیام سیستم", "");
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    FRB = null;
                }
            }
        }

        protected void grdProfessorStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                string status = dr["status"].ToString();

                DropDownList drpUserStatus = (DropDownList)e.Row.FindControl("drpUserStatus");
                drpUserStatus.Items.FindByValue(status).Selected = true;
            }
        }

        protected void grdProfessorStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //System.Threading.Thread.Sleep(5000);
            if (e.CommandName == "send")
            {
                GridViewRow curruntRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                DropDownList drpStatus = (DropDownList)curruntRow.FindControl("drpUserStatus") as DropDownList;
                string status = drpStatus.SelectedValue;
                string UserName = e.CommandArgument.ToString();
                FacultyReportsBusiness FRB = new FacultyReportsBusiness();
                try
                {

                    DataTable user = FRB.GetInfoPeoByCodeMeli(UserName);

                        string lastStatus = drpStatus.Items.FindByValue(user.Rows[0]["status"].ToString()).Text;
                        string newStatus = drpStatus.Items.FindByValue(status).Text;
                    if (canChangeStatus(Convert.ToInt32(user.Rows[0]["status"]), Convert.ToInt32(status)))
                    {

                        FRB.UpdateInfoPeopleStatus(UserName, status);

                        CommonBusiness cb = new CommonBusiness();
                        cb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), (int)DTO.eventEnum.تغییر_وضعیت_استاد, " تغییر وضعیت از " + lastStatus + " به " + newStatus, int.Parse(user.Rows[0]["ID"].ToString()));
                        RadWindowManager1.RadAlert("تغییر وضعیت استاد انجام شد.", 300, 100, "پیام سیستم", "");
                    }
                    else
                    {
                        RadWindowManager1.RadAlert(" تغییر وضعیت از " + lastStatus + " به " + newStatus+" امکان پذیر نمی باشد.", 300, 100, "پیام سیستم", "");
                    }
                    if (user.Rows.Count > 0)
                    {
                        grdProfessorStatus.DataSource = user;
                        grdProfessorStatus.DataBind();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    FRB = null;
                }
            }
            else if (e.CommandName == "Details")
            {
                Session["page"] = 4;
                Response.Redirect("ShowDetailInfo.aspx?ID=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "History")
            {
               // TeacherName.InnerText ="کد استاد: "+ e.CommandArgument.ToString();
                CommonBusiness cmb = new CommonBusiness();

                lst_history.DataSource = cmb.getUserAndStudentLogByAppId(13, int.Parse(e.CommandArgument.ToString()));
                lst_history.DataBind();


                //rgHistoryGrid.DataSource = cmb.getUserAndStudentLogByAppId(13, int.Parse(e.CommandArgument.ToString()), 5);
                //rgHistoryGrid.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }


        private bool canChangeStatus(int oldStatus, int newStatus)
        {
            switch (oldStatus)
            {
                case 0:
                case 8:
                    return false;
                case 1:
                case 2:
                case 3:
                    return newStatus == 0;
                case 4:
                case 5:
                case 6:
                case 7:
                    return newStatus == 1 || newStatus==0;
            }
            return false;
        }

        protected void rgHistoryGrid_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            Telerik.Web.UI.GridDataItem dataItem = (Telerik.Web.UI.GridDataItem)e.DetailTableView.ParentItem;
            if (e.DetailTableView.Name == "historyDetail")
            {
                string reqID = dataItem.GetDataKeyValue("reqId").ToString();
                Business.university.Request.ProfessorRequestBusiness PRB = new Business.university.Request.ProfessorRequestBusiness();
                e.DetailTableView.DataSource = PRB.getCustomizedProfessorEditedFields(Convert.ToInt32(reqID));
            }
        }

        protected void rgHistoryGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void rgHistoryGrid_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //if (rgHistoryGrid.MasterTableView.Items.Count > 0)
                //{
                //    rgHistoryGrid.MasterTableView.Items[0].Expanded = true;
                //    rgHistoryGrid.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
                //}
            }
        }

        protected void lst_history_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

    }
}