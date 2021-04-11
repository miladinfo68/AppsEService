using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResourceControl.PL.Forms
{
    public partial class ResearchAddResource : System.Web.UI.Page
    {
        List<RequestFR> approvedreqlist = null;
        int reqid;
        int userID = 3;
        protected void Page_Load(object sender, EventArgs e)
        {
            reqid = Convert.ToInt32(Session["reqID"]);
            if (reqid != 0)
            {
                if (!IsPostBack)
                {
                    BindData();
                }
            }
            else
            {
                Response.Redirect("EducationUserReview.aspx");
            }

        }

        private void BindData()
        {
            RequestFR req = new RequestFR();
            //reqid = Convert.ToInt32(Session["reqID"]);
            RequestHandler rq = new RequestHandler();
            req = rq.GetRequestDetails(reqid);
            lblRequestID.Text = req.ID.ToString();
            Category ct = new CategoryHandler().GetCategoryDetails(req.CatID);
            lblCategory.Text = ct.Name;
           // lblCourseName.Text = req.CourseName;
            lblLocation.Text = req.Location;
            lblDescription.Text = req.Note;
            OptionHandler opt1 = new OptionHandler();
            List<Option> optlist1 = opt1.GetOptionListByReqID(req.ID);
            foreach (var item in optlist1)
            {
                chblOptions.Items.Add(item.Name);
                chblOptions.Items.FindByText(item.Name).Selected = true;
            }
            //lblSessionStart.Text = req.Sessionstart_time.ToString();
            //lblSessionEnd.Text = req.Sessionend_time.ToString();
            //pcal1.Text = req.Sessiondate;
            ResourceHandler rs = new ResourceHandler();
            List<Resource> reslist = rs.GetResourceListByReqID(req.ID);
            grdResourceList.DataSource = reslist;
            grdResourceList.DataBind();
            ViewState.Add("req", req);
        }

        protected void grdResourceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int resID = Convert.ToInt32(grdResourceList.DataKeys[e.Row.RowIndex].Value);
                BulletedList bltoptions = (BulletedList)e.Row.FindControl("bltResOptions");
                OptionHandler opt = new OptionHandler();
                List<Option> optlist = opt.GetOptionListByResID(resID);
                bltoptions.DataSource = optlist;
                bltoptions.DataTextField = "name";
                bltoptions.DataBind();

                GridView grdApprovedRequestList = (GridView)e.Row.FindControl("grdApprovedRequestList");
                RequestHandler rq = new RequestHandler();
                grdApprovedRequestList.DataSource = rq.GetRequestListBySessionDate_resID_status(pcal1.Text, resID, 2);
                grdApprovedRequestList.DataBind();

                GridView grdPendingRequestList = (GridView)e.Row.FindControl("grdRequestPendingList");
                RequestHandler rq1 = new RequestHandler();
                grdPendingRequestList.DataSource = rq1.GetRequestListBySessionDate_resID_status(pcal1.Text, resID, 1);
                grdPendingRequestList.DataBind();
            }
        }

        protected void grdResourceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int resID = Convert.ToInt32(e.CommandArgument);
                RequestFR req = (RequestFR)ViewState["req"];
                RequestHandler rq = new RequestHandler();
                //approvedreqlist = rq.GetRequestListBySessionDate_resID_status(req.Sessiondate, resID, 2);
                //if (BeforeToday(req.Sessiondate))
                //{
                //    string scrp = "alert('تاریخ درخواست قبل از امروز می باشد ، شما فقط می توانید درخواست را ویرایش یا رد نمایید');";
                //    ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, scrp, true);
                //}
                //else
                //{
                //    if (approvedreqlist != null)
                //    {
                //        if (TimeConflicted(req))
                //        {
                //            string scrp1 = "alert('تداخل در ساعات درخواست فعلی و درخواست های از قبل تایید شده !');";
                //            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, scrp1, true);
                //        }
                //        else
                //        {
                //            SendReq(resID, req);
                //        }
                //    }
                //    else
                //    {
                //        SendReq(resID, req);
                //    }
                //}
            }
        }

        private bool BeforeToday(string reqd)
        {
            int today = Convert.ToInt32(DateTime.Now.ToPeString().Replace("/", ""));
            int reqdate = Convert.ToInt32(reqd.Replace("/", ""));
            return today > reqdate;
        }

        private bool TimeConflicted(RequestFR req)
        {
            bool start = false;
            bool end = false;
            //start = approvedreqlist.Any(i => i.Sessionstart_time <= req.Sessionstart_time && i.Sessionend_time > req.Sessionstart_time);
            //end = approvedreqlist.Any(i => i.Sessionstart_time < req.Sessionend_time && i.Sessionend_time >= req.Sessionend_time);
            return start || end;
        }

        private void SendReq(int resID, RequestFR req)
        {
            //req.ResourceID = resID;
            req.SenderID = userID;//this is education user ID that has to come from session !!!
            req.Status = 1;//1 means sent status
            req.Send_time = DateTime.Now.ToPeString();
            RequestHandler rq = new RequestHandler();
            rq.UpdateRequest(req);
            Response.Redirect("EducationUserReview.aspx");
        }

        protected void btnDenyRequest_Click(object sender, EventArgs e)
        {
            RequestFR req = (RequestFR)ViewState["req"];
            req.ReplierID = 2;//education user ID
            req.Answernote = txtAnswerNote.Text;
            req.Answer_time = DateTime.Now.ToPeString();
            req.Status = 3;//3 means request has been denied .
            RequestHandler rq = new RequestHandler();
            rq.UpdateRequest(req);
            Response.Redirect("EducationUserReview.aspx");
        }

        protected void btnEditRequest_Click(object sender, EventArgs e)
        {
            Session.Add("reqID", reqid);
            Response.Redirect("UserAddEditRequest.aspx");
        }
    }
}