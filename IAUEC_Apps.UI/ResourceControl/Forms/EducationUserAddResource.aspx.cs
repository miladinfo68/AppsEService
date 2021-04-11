using ResourceControl.Entity;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Linq;
using System.Web.UI;


namespace IAUEC_Apps.ResourceControl.Froms
{
    public partial class EducationUserAddResource : System.Web.UI.Page
    {
        List<RequestFR> approvedreqlist = null;
        RequestHandler requestBussiness = new RequestHandler();
        int reqid;
        int userID;

        protected void Page_Load(object sender, EventArgs e)
        {
            //reqid = Convert.ToInt32(Session["reqID"]);
            reqid = Convert.ToInt32(Request.QueryString["reqid"].ToString());
            string address = "EducationUserReview.aspx?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr();
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
                Response.Redirect("EducationUserReview.aspx?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr());
            }
            
        }

        private void BindData()
        {
            RequestFR req = new RequestFR();
            req = requestBussiness.GetRequestDetails(reqid);
            lblRequestID.Text = req.ID.ToString();
            Category ct = new CategoryHandler().GetCategoryDetails(req.CatID);
            lblCategory.Text = ct.Name;
            lblCourseName.Text = req.CourseName ;
            lblLocation.Text = req.Location;
            lblProfessor.Text = req.IssuerName;
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
                grdApprovedRequestList.DataSource = requestBussiness.GetRequestListBySessionDate_resID_status(pcal1.Text, resID, 2);
                grdApprovedRequestList.DataBind();
                GridView grdPendingRequestList = (GridView)e.Row.FindControl("grdRequestPendingList");
                grdPendingRequestList.DataSource = requestBussiness.GetRequestListBySessionDate_resID_status(pcal1.Text, resID, 1);
                grdPendingRequestList.DataBind();
            }
        }

        protected void grdResourceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int resID = Convert.ToInt32(e.CommandArgument);
                RequestFR req = (RequestFR)ViewState["req"];
                //approvedreqlist = requestBussiness.GetRequestListBySessionDate_resID_status(req.Sessiondate, resID, 2);
                //if (BeforeToday(req.Sessiondate))
                //{
                //    RadWindowManager1.RadAlert(" تاریخ درخواست قبل از امروز می باشد ، شما فقط می توانید درخواست را ویرایش یا رد نمایید", 330, 220, "هشدار", "");
                //}
                //else
                //{
                //    if (approvedreqlist != null)
                //    {
                //        if (TimeConflicted(req))
                //        {
                //            RadWindowManager1.RadAlert(" تداخل در ساعات درخواست جاری و درخواست های از قبل تایید شده برای این کلاس ، لطفا کلاس دیگری را برای این درخواست پیشنهاد دهید و یا ساعت و تاریخ این درخواست را با هماهنگی استاد تغییر دهید.", 330, 220, "هشدار", "");
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
            ResourceHandler Rc = new ResourceHandler();
            string resName = Rc.GetResourceDetails(resID).Name;
            userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            req.SenderID = userID;//this is education user ID that has to come from session !!!
            req.Status = 1;//1 means sent status
            req.Send_time = DateTime.Now.ToPeString();
            
            try
            {
                requestBussiness.UpdateRequest(req);
                string st = resName+" برای درخواست شماره "+req.ID+" پیشنهاد و برا ی تایید به کاربر اداری ارسال شد. ";
                RadWindowManager1.RadAlert(st, 350, 200, "پیام سیستم", "redirectToLast");
                //Response.Redirect("EducationUserReview.aspx?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr());
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
            req.ReplierID = userID;//education user ID
            req.Answernote = txtAnswerNote.Text;
            req.Answer_time = DateTime.Now.ToPeString();
            req.Status = 3;//3 means request has been denied .
            
            requestBussiness.UpdateRequest(req);
            Response.Redirect("EducationUserReview.aspx?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr());
        }         

        protected void btnEditRequest_Click(object sender, EventArgs e)
        {
            Session.Add("reqID", reqid);
            Response.Redirect("UserEditRequest.aspx?id="+generaterandomstr() + "@A" + "0" + "-" + generaterandomstr());
        }

        [System.Web.Services.WebMethod]
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

        protected void btnSubmitMsg_Click()
        {
            Response.Redirect("EducationUserReview.aspx?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr());
        }

        protected void grdResourceList_Sorting(object sender, GridViewSortEventArgs e)
        {
        
        }
    }
}