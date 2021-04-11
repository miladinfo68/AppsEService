using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ResourceControl.PL.Forms
{
    public partial class OfficeUserReplyRequest : System.Web.UI.Page
    {
        RequestHandler RequestBussiness = new RequestHandler();
        List<RequestFR> approvedreqlist = null;
        int userID;//this user ID has to come from session.
        int reqid;
        protected void Page_Load(object sender, EventArgs e)
        {
            //reqid = Convert.ToInt32(Session["reqID"]);
            reqid = Convert.ToInt32(Request.QueryString["reqid"]);
            string address = "OfficeUserReview.aspx?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr();
            string scrp = "function redirectToLast(){ window.location= '" + address + "' ; }";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", scrp, true);
            if (reqid != 0)
            {
                if (!IsPostBack)
                {
                    BindData();
                }
            }
            else
            {
                Response.Redirect("OfficeUserReview.aspx?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr());
            }
        }

        private void BindData()
        {
            RequestFR req = new RequestFR();
            reqid = Convert.ToInt32(Request.QueryString["reqid"]);
            req = RequestBussiness.GetRequestDetails(reqid);
            lblRequestID.Text = req.ID.ToString();
            Category ct = new CategoryHandler().GetCategoryDetails(req.CatID);
            lblCategory.Text = ct.Name;
            lblIssuer.Text = req.IssuerName;
            lblCapacity.Text = req.Capacity.ToString();
            lblDescription.Text = req.Note;
            OptionHandler opt1 = new OptionHandler();
            List<Option> optlist1 = opt1.GetOptionListByReqID(req.ID);
            foreach (var item in optlist1)
            {
                chblOptions.Items.Add(item.Name);
                chblOptions.Items.FindByText(item.Name).Selected = true;
            }
            //lblSessionStart.Text = req.Sessionstart_time.Value.ToString(@"hh\:mm");
            //lblSessionEnd.Text = req.Sessionend_time.Value.ToString(@"hh\:mm");
            //pcal1.Text = req.Sessiondate;
            ResourceHandler rs = new ResourceHandler();
            List<Resource> reslist = rs.GetResourceListByReqID(req.ID);
            grdResourceList.DataSource = reslist;
            ViewState.Add("req", req);
            grdResourceList.DataBind();
        }

        protected void grdResourceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RequestFR req = (RequestFR)ViewState["req"];
                int resID = Convert.ToInt32(grdResourceList.DataKeys[e.Row.RowIndex].Value);
                BulletedList bltoptions = (BulletedList)e.Row.FindControl("bltResOptions");
                OptionHandler opt = new OptionHandler();
                List<Option> optlist = opt.GetOptionListByResID(resID);
                bltoptions.DataSource = optlist;
                bltoptions.DataTextField = "name";
                bltoptions.DataBind();
                GridView grdApprovedRequestList = (GridView)e.Row.FindControl("grdApprovedRequestList");
                approvedreqlist = RequestBussiness.GetRequestListBySessionDate_resID_status(pcal1.Text, resID, 2);
                grdApprovedRequestList.DataSource = approvedreqlist;
                grdApprovedRequestList.DataBind();
                GridView grdPendingRequestList = (GridView)e.Row.FindControl("grdRequestPendingList");
                List<RequestFR> perl = RequestBussiness.GetRequestListBySessionDate_resID_status(pcal1.Text, resID, 1);
                IEnumerable<RequestFR> perl1 = null;
                //if (perl != null)
                //{
                //    perl1 = perl.Where(item => (item.Sessionstart_time >= req.Sessionstart_time)
                //                            && (item.Sessionstart_time < req.Sessionend_time));
                //}
                grdPendingRequestList.DataSource = perl1;
                grdPendingRequestList.DataBind();
                ViewState.Add("req", req);
            }
        }

        protected void grdPendingRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RequestFR req = (RequestFR)ViewState["req"];
                if (Convert.ToInt32(e.Row.Cells[0].Text) == req.ID)
                {
                    e.Row.Attributes.Add("class", "bg-danger");
                }
            }
        }

        protected void grdResourceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "approve")
            {
                int resID = Convert.ToInt32(e.CommandArgument);
                RequestFR req = (RequestFR)ViewState["req"];

                //approvedreqlist = RequestBussiness.GetRequestListBySessionDate_resID_status(req.Sessiondate, resID, 2);
                //if (BeforeToday(req.Sessiondate))
                //{
                //    RadWindowManager1.RadAlert(" تاریخ درخواست قبل از امروز می باشد ، شما فقط می توانید این درخواست را رد نمایید", 330, 220, "هشدار", "");
                //}
                //else
                //{
                //    if (approvedreqlist != null)
                //    {
                //        if (TimeConflicted(req))
                //        {
                //            RadWindowManager1.RadAlert(" تداخل در ساعات درخواست جاری و درخواست های از قبل تایید شده برای این کلاس ، شما فقط می توانید این درخواست را رد نمایید.", 330, 220, "هشدار", "");
                //        }
                //        else
                //        {
                //            ApproveRequest(resID, req);
                //        }
                //    }
                //    else
                //    {
                //        ApproveRequest(resID, req);
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

        //private bool TimeConflicted(RequestFR req)
        //{
        //    //bool start = false;
        //    //bool end = false;
        //    //start = approvedreqlist.Any(i => i.Sessionstart_time <= req.Sessionstart_time && i.Sessionend_time > req.Sessionstart_time);
        //    //end = approvedreqlist.Any(i => i.Sessionstart_time < req.Sessionend_time && i.Sessionend_time >= req.Sessionend_time);
        //    //return start || end;
        //}

        private void ApproveRequest(int resID, RequestFR req)
        {
            //req.ResourceID = resID;
            ResourceHandler Rc = new ResourceHandler();
            string resName = Rc.GetResourceDetails(resID).Name;
            userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            req.ReplierID = userID;//this is office user ID that has to come from session !!!
            req.Status = 2;//2 means approved
            ResourceHandler resH = new ResourceHandler();
            Resource resource = resH.GetResourceDetails(resID);
            req.Answernote = resource.Name + "برای شما تایید شده";
            req.Answer_time = DateTime.Now.ToPeString();

            try
            {
                RequestBussiness.UpdateRequest(req);                
                string st = resName + " برای درخواست شماره " + req.ID + "تایید شد. ";
                //lblSuccess.Text = st;
                //RadWindow2.VisibleOnPageLoad = true;
                RadWindowManager1.RadAlert(st, 300, 200, "پیام سیستم", "redirectToLast");
            }
            catch (Exception)
            {
                RadWindowManager1.RadAlert(" متاسفانه خطایی در سیستم رخ داده است ، لطفا با مدیر سیستم تماس بگیرید.", 330, 220, "هشدار", "");
            }
        }

        protected void btnDenyRequest_Click(object sender, EventArgs e)
        {
            RequestFR req = (RequestFR)ViewState["req"];
            userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            req.ReplierID = userID;//office user ID.
            req.Answernote = txtAnswerNote.Text;
            req.Answer_time = DateTime.Now.ToPeString();
            req.Status = 0;//0 means request has been returned to education user.

            RequestBussiness.UpdateRequest(req);
            string st =  "  درخواست شماره " + req.ID + " رد شد و برای پیگیری به فرستنده آن برگردانده شد. ";
            //lblSuccess.Text = st;
            //RadWindow2.VisibleOnPageLoad = true;
            RadWindowManager1.RadAlert(st, 300, 200, "پیام سیستم", "redirectToLast");
        }

        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        protected void btnSubmitMsg_Click(object sender, EventArgs e)
        {
            Response.Redirect("OfficeUserReview.aspx?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr());
        }
    }
}