using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ResourceControl.Entity;
using ResourceControl.BLL;
using System.Data;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.ResourceControl;
using Telerik.Web.UI;

namespace ResourceControl.PL.Forms
{
    public partial class ProfessorReview : System.Web.UI.Page
    {
        int userID;//professor id has to come from session
        RequestHandler rq = new RequestHandler();
        List<RequestFR> RequestList = null;
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            if (RequestList == null)
            {
                RequestList = rq.GetRequestListByIssuerID(userID);
            }
            if (!IsPostBack)
            {
                BindGrid(1);
            }
        }

        private void BindGrid(int stat)
        {

            if (RequestList != null)
            {
                if (stat != 5)
                {
                    dt = rq.GetRequestByUserIDandType(userID, Convert.ToInt32(drpRequestStatus.SelectedValue));

                    #region Convert DataTable To List

                    List<RequestFR> query = (from DataRow row in dt.Rows
                        select new RequestFR()
                        {
                            ID = Convert.ToInt32(row["ID"]),
                            Subject = row["subject"].ToString(),
                            Note = row["note"].ToString(),
                            Answernote = row["answernote"].ToString(),
                            Answer_time = row["answer_time"].ToString(),
                            Issue_time = row["issue_time"].ToString(),
                            Status = Convert.ToInt32(row["status"]),
                            IssuerID = Convert.ToInt32(row["issuerID"]),
                            Send_time = row["send_time"].ToString(),
                            CatID = Convert.ToInt32(row["catID"]),
                            ReplierID = Convert.ToInt32(row["replierID"]),
                            SenderID = Convert.ToInt32(row["senderID"]),
                            IsDeleted = Convert.ToBoolean(row["isDeleted"]),
                            IssuerName = row["issuerName"].ToString(),
                            Location = row["location"].ToString(),
                            Capacity = Convert.ToInt32(row["capacity"]),
                            CourseName = row["courseName"].ToString(),
                            CourseDID = Convert.ToInt32(row["courseID"]),
                            DaneshID = Convert.ToInt32(row["daneshID"]),
                        }).ToList();

                    #endregion

                    grdProfessorReview.DataSource = query.Distinct(new ComprareForId());
                }
                else
                {
                    grdProfessorReview.DataSource = RequestList.Distinct(new ComprareForId());
                }
            }
            if (drpRequestStatus.SelectedValue == 2.ToString())
            {
                grdProfessorReview.Columns[7].Visible = false;
            }
            grdProfessorReview.DataBind();
        }

        public string GetImage(int value)
        {
            if (value == 3)
            {

                return ResolveUrl("~/ResourceControl/Images/deny.png");
            }
            else if (value == 2)
            {
                return ResolveUrl("~/ResourceControl/Images/Approved-icon.png");
            }
            else if (value == 1)
            {
                return ResolveUrl("~/ResourceControl/Images/send-file-32.png");
            }
            else if (value == 4)
            {
                return ResolveUrl("~/ResourceControl/Images/informed.png");
            }
            else
            {
                return ResolveUrl("~/ResourceControl/Images/waiting.jpg");
            }
        }

        protected void txtAddReques_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfessorAddRequest.aspx");
        }

        protected void drpRequestStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int status = Convert.ToInt32(drpRequestStatus.SelectedValue);
            if (status == 1)
            {
                BindGrid(1);
            }
            else if (status == 2)
            {
                BindGrid(2);
            }
            else if (status == 3)
            {
                BindGrid(3);
            }
            else if (status == 4)
            {
                BindGrid(4);
            }
            else if (status == 0)
            {
                BindGrid(0);
            }
            else
            {
                BindGrid(5);
            }
        }

        protected void grdProfessorReview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int status = Convert.ToInt32(drpRequestStatus.SelectedValue);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //this method replace category id by category name
                Label lb = new Label();
                int rowindex = e.Row.RowIndex;
                List<RequestFR> reqlist = rq.GetRequestListByIssuerID(userID);
                CategoryHandler cth = new CategoryHandler();
                Category cat = null;
                if (reqlist.Count > rowindex)
                    cat = cth.GetCategoryDetails(reqlist[rowindex].CatID);
                if (cat != null)
                {
                    lb.Text = cat.Name;
                    e.Row.Cells[3].Controls.Add(lb);
                }

                int stat = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "status"));
                Image img = (Image)e.Row.FindControl("imgStatus");
                Button btnCancelRequest = (Button)e.Row.FindControl("btnCancelRequest");

                if (status != 0)
                {

                    var dataControlField = (DataControlField)grdProfessorReview.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "عملیات");
                    if (dataControlField != null && status != 5)
                        dataControlField.Visible = false;
                }
                else
                {
                    var dataControlField = (DataControlField)grdProfessorReview.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "عملیات");
                    if (dataControlField != null)
                        dataControlField.Visible = true;
                }
                if (stat == 3)
                {
                    img.Attributes.Add("title", "رد شده");
                }
                if (stat == 2)
                {
                    img.Attributes.Add("title", "تایید شده");
                }
                else if (stat == 1)
                {
                    img.Attributes.Add("title", "فرستاده شده");
                }
                else
                {
                    img.Attributes.Add("title", "در حال بررسی");
                }
                if (status == 1)
                {
                    var dataControlField =
                        (DataControlField)grdProfessorReview.Columns.Cast<DataControlField>()
                            .SingleOrDefault(fld => fld.HeaderText == "زمان پاسخ");
                    if (dataControlField != null)
                        dataControlField.Visible = false;
                }
                if (status == 5)
                {
                    var dataControlField = (DataControlField)grdProfessorReview.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "عملیات");

                    if (dataControlField != null) dataControlField.Visible = true;


                    foreach (GridViewRow row in grdProfessorReview.Rows)
                    {
                        var requesthandler = new RequestHandler();
                        var req = Convert.ToInt32(row.Cells[1].Text);
                        var detailOfRequest = requesthandler.GetRequestDetails(req);
                        if (detailOfRequest.Status != 0)
                        {
                            row.Cells[9].FindControl("btnCancelRequest").Visible = false;
                        }
                    }
                }
                //if (drpRequestStatus.SelectedValue=="2" || stat == 3)
                //{
                //    ((DataControlField)grdProfessorReview.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "عملیات").SingleOrDefault()).Visible = false;
                //}
                //else
                //{
                //    ((DataControlField)grdProfessorReview.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "عملیات").SingleOrDefault()).Visible = true;
                //}
            }
        }

        protected void grdProfessorReview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cancel1")
            {
                string confirmValue = Request.Form["confirm_value"];

                if (confirmValue == "بله")
                {
                    RequestHandler reqH = new RequestHandler();

                    var userId = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
                    var comman = new CommonBusiness();
                    comman.InsertIntoStudentLog(userId.ToString(), "", 11, 29, "حذف درخواست کلاس توسط استاد");

                    reqH.DeleteRequest(Convert.ToInt32(e.CommandArgument));
                    BindGrid(drpRequestStatus.SelectedIndex + 1);
                }
                else
                {
                    BindGrid(drpRequestStatus.SelectedIndex + 1);
                }
            }
            if (e.CommandName == "showTime")
            {
                List<RequestDateTime> timeList = new List<RequestDateTime>();
                int reqID = Convert.ToInt32(e.CommandArgument);

                GridViewRow curruntRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                lblDarsName.Text = curruntRow.Cells[2].Text;
                lblRequestId.Text = curruntRow.Cells[1].Text;
                imgStatus2.ImageUrl = ((Image)curruntRow.Cells[2].FindControl("imgStatus")).ImageUrl;

                RequestFR request = RequestList.Where(i => i.ID == reqID)
                                               .First();

                timeList = RequestList.Where(i => i.ID == reqID)
                                      .SelectMany(j => j.DateTimeRange)
                                      .OrderBy(x => x.Date)
                                      .ToList()
                                      ;

                grdDateTime.DataSource = timeList;
                grdDateTime.DataBind();

                Page.ClientScript.RegisterStartupScript(GetType(), "key", "Sys.Application.add_load(showWindow);", true);
            }
            if (e.CommandName == "History")
            {

                CommonBusiness cmb = new CommonBusiness();

                lst_history.DataSource = cmb.GetUserLogByModifyId(int.Parse(e.CommandArgument.ToString()), 11);
                lst_history.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

        }
    }
}