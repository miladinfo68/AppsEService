using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Drawing.Charts;
using IAUEC_Apps.Business.Common;

namespace ResourceControl.PL.Forms
{
    public partial class ResearchUser : System.Web.UI.Page
    {
        int userID = 2003;//professor id has to come from session
        RequestHandler rq = new RequestHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["StausLinke"] != null)
                {
                    drpRequestStatus.ClearSelection();
                    if (Convert.ToInt32(Session["StausLinke"].ToString()) > 4)
                    {
                        Session["StausLinke"] = 0;
                    }

                    drpRequestStatus.Items.FindByValue(Session["StausLinke"] as string).Selected = true;
                    BindGrid(Convert.ToInt32(Session["StausLinke"]), Convert.ToInt32(Session["StausLinke"]));
                }
                else
                    BindGrid(0, 1);
            }
        }

        private void BindGrid(int stat1, int stat2)
        {
            var reqlist = new List<RequestFR>();
            var isMasouleDorehKootahModat = UtilityFunction.IsMasouleDorehKootahModat(Convert.ToInt32(Session["RoleID"]));
            if (isMasouleDorehKootahModat == 13) //رئییس کوتاه مدت
                reqlist = rq.GetRequestListByDaneshID(5);
            else
            {
                if (rq.GetRequestListByDaneshID(5) != null)
                {
                    reqlist = rq.GetRequestListByDaneshID(5);

                    reqlist = reqlist.Where(c => c.IssuerID == Convert.ToInt32(Session[sessionNames.userID_Karbar].ToString())).ToList();
                }
            }



            if (reqlist != null)
            {
                if (stat1 != 4)
                {
                    var rl = from r in reqlist
                             where r.Status == stat1 || r.Status == stat2
                             select r;
                    grdProfessorReview.DataSource = rl;
                }
                else
                {
                    grdProfessorReview.DataSource = reqlist;
                }
            }
            grdProfessorReview.DataBind();
        }
        public string GetImage(int value)
        {
            if (value == 5)
            {
                return ResolveUrl("~/ResourceControl/Images/Messaging-Question-icon.png");
            }
            if (value == 4)
            {
                return ResolveUrl("~/ResourceControl/Images/informed.png");
            }
            if (value == 3)
            {
                return ResolveUrl("~/ResourceControl/Images/deny.png");
            }
            if (value == 2)
            {
                return ResolveUrl("~/ResourceControl/Images/Approved-icon.png");
            }
            if (value == 1)
            {
                return ResolveUrl("~/ResourceControl/Images/send-file-32.png");
            }
            if (value == 0)
            {
                return ResolveUrl("~/ResourceControl/Images/waiting.jpg");
            }
            else
            {
                return ResolveUrl("~/ResourceControl/Images/red_trans_question.png");
            }
        }

        private void ResolveImageUrl(GridViewRowEventArgs e)
        {
            Image img = (Image)e.Row.FindControl("imgStatus");
            RequestFR dataRow = (RequestFR)e.Row.DataItem;
            switch (dataRow.Status)
            {
                case 0:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "در حال بررسی آموزش";
                    break;
                case 1:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "منتظر تایید اداری";
                    break;
                case 2:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "تایید اداری";
                    break;
                case 3:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "رد شده";
                    break;
                case 4:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "اطلاع رسانی شده";
                    break;
                case 5:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "از دست رفته";
                    break;
                default:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "نامشخص";
                    break;
            }
        }

        protected void txtAddReques_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResearchUserAddRequest.aspx");
        }

        protected void drpRequestStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int status = Convert.ToInt32(drpRequestStatus.SelectedValue);
            if (status == 1)
            {
                BindGrid(0, 1);
            }
            else if (status == 2)
            {
                BindGrid(2, 2);
            }
            else if (status == 3)
            {
                BindGrid(3, 3);
            }
            else if (status == 4)
            {
                BindGrid(4, 4);
            }
        }

        protected void grdProfessorReview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int status = Convert.ToInt32(drpRequestStatus.SelectedValue);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ResolveImageUrl(e);
            }
        }

        protected void grdProfessorReview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProfessorReview.PageIndex = e.NewPageIndex;

            int status = Convert.ToInt32(drpRequestStatus.SelectedValue);
            if (status == 1)
            {
                BindGrid(0, 1);
            }
            else if (status == 2)
            {
                BindGrid(2, 2);
            }
            else if (status == 3)
            {
                BindGrid(3, 3);
            }
            else if (status == 4)
            {
                BindGrid(4, 4);
            }
        }
    }
}