using IAUEC_Apps.Business.Common;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class EducationManualDateDefence : System.Web.UI.Page
    {
        private RequestHandler _requestHandler = new RequestHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                Session[sessionNames.menuID] = menuId;
                AccessControl.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
            }

            }

            protected void btnSave_Click(object sender, EventArgs e)
        {
            var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            if (!string.IsNullOrEmpty(txtstcode.Text) && !string.IsNullOrEmpty(txtDate.Text))
            {
                DataTable dtRequestID = _requestHandler.GetRequestIdAcceptedByStcode(txtstcode.Text);
                if (dtRequestID != null && dtRequestID.Rows.Count > 0)
                {


                    Session["PrintDefenceSession"] = dtRequestID.Rows[0]["RequestId"].ToString()
                        + "-" + txtstcode.Text + "-0-" + txtDate.Text+"-"+ChkFinal.Checked;
                    if (!string.IsNullOrEmpty(txtstcode.Text) && ChkFinal.Checked )        {
                            var scoreAccept = _requestHandler.GetScoreForDefence(int.Parse(dtRequestID.Rows[0]["RequestId"].ToString()));
                            if ((scoreAccept.FlagAcceptScoreDavin == false || scoreAccept.FlagAcceptScoreDavOut == false || scoreAccept.FlagAcceptScoreMosh1 == false
                                || scoreAccept.FlagAcceptScoreRah1 == false))
                            {
                                lblTitle.Text = "پیام سیستم";
                                lblAlert.Text = "همه اساتید نمره واردشده را تایید نکرده اند و یا نمره ای وارد نشده است";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#ModalAlert').modal();", true);
                                upModalAlert.Update();
                                return;
                            }
                        }

                    var avg = _requestHandler.GetTotalAverageByStudentCode(txtstcode.Text);

                    if (avg <= 14)
                    {
                        string scrp = "function f(){$find(\"" + rwAverageIsLessThan14.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
                        return;
                        
                    }
                    var comman = new CommonBusiness();
                    comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 245,
                        string.Format("{0}", ("چاپ و مشاهده صورتجلسه دفاع کیفی باتاریخ صورتجلسه دستی" + Session["PrintDefenceSession"].ToString()))
                        , Convert.ToInt32(dtRequestID.Rows[0]["RequestId"].ToString()));

                    string scrp2 = "function f(){$find(\"" + rdwPrint.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp2, true);
                }
                else
                    RadWindowManager1.RadAlert("چنین دانشجویی دفاع تایید شده ندارد", 500, 100, "خطا", "");


            }


        }

        protected void btnShowRadWindowSooratJalaseh_Click(object sender, EventArgs e)
        {
            var comman = new CommonBusiness();
            var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            var stcode = Session["PrintDefenceSession"].ToString().Split('-')[1];
            var reqid = Session["PrintDefenceSession"].ToString().Split('-')[0];
            comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 245,
     string.Format("{0}", ("چاپ و مشاهده صورتجلسه دفاع کیفی باتاریخ صورتجلسه دستی" + Session["PrintDefenceSession"].ToString()))
     , Convert.ToInt32(reqid));
            string scrp2 = "function f(){ closeRadWindowAverageIsLessThan14(); $find(\"" + rdwPrint.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp2, true);
        }

        protected void btnCancleShowRadWindowSooratJalaseh_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "closeRadWindowAverageIsLessThan14();", true);

        }


    }
}