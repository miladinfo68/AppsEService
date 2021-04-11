using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.ResourceControlClasses;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI
{
    public partial class TeacherAcceptScore : System.Web.UI.Page
    {
        private RequestHandler _requestHandler = new RequestHandler();
        string user;
        ScoreDefence resScore;
        private CommonBusiness commonBusiness = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            resScore = new ScoreDefence();

        }



        protected void modalOpenScore_Click(object sender, EventArgs e)
        {
            btnAccept.Visible = false;
            btnRejectOrDisp.Text = "بستن";

            lblModalTitle.Text = "تاییدیه نمره دفاع توسط اساتید";
            Button btn = (Button)sender;
            user = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            var lblReqid = data.FindControl("lblRequestId") as Label;
            var lblStCode = data.FindControl("lblstudentcode") as Label;
            hdnReqId.Value = lblReqid.Text;
            hdnStcode.Value = lblStCode.Text;
            resScore = _requestHandler.GetScoreForDefence(int.Parse(lblReqid.Text));
            if (resScore.Score == null || resScore.Score < 0 || resScore.Score > 20)
            {
                lblTitle.Text = "پیام سیستم";
                lblAlert.Text = "نمره ای جهت تایید درج نشده است";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#ModalAlert').modal();", true);
                upModalAlert.Update();
                return;

            }
            CheckOutRequestBusiness _reqBusiness = new CheckOutRequestBusiness();
            var entryYear = _reqBusiness.GetSaleVoroodByStCode(lblStCode.Text);
            if (Convert.ToInt32(entryYear) < 95)
            {
                lblScore.Text = resScore.Score.ToString();
            }
            else
                lblScore.Text = UtilityFunction.ConvertScoreToDegree((resScore.Score == null ? -1 : resScore.Score.Value));

            var resDefOstads = _requestHandler.GetDefenceInformation(lblStCode.Text);

            if (resDefOstads.FirstConsultantId != "" && resDefOstads.FirstConsultantId != null)
            {
                PanelMosh1.Visible = true;
                chkMosh1.Checked = resScore.FlagAcceptScoreMosh1.Value;
                if (user == resDefOstads.FirstConsultantId && !resScore.FlagAcceptScoreMosh1.Value)
                {
                    chkMosh1.Enabled = true;
                    btnAccept.Visible = true;
                    btnRejectOrDisp.Text = "انصراف";
                    
                }
                else
                {
                    chkMosh1.Enabled = false;
                }
            }

            else
            {
                PanelMosh1.Visible = false;
                chkMosh1.Checked = true;

            }



            if (resDefOstads.SecondConsultantId != "" && resDefOstads.SecondConsultantId != null)
            {
                PanelMosh2.Visible = true;
                chkMosh2.Checked = resScore.FlagAcceptScoreMosh2.Value;
                if (user == resDefOstads.SecondConsultantId && !resScore.FlagAcceptScoreMosh2.Value)
                {
                    chkMosh2.Enabled = true;
                    btnAccept.Visible = true;
                    btnRejectOrDisp.Text = "انصراف";
                    
                }
                else
                {
                    chkMosh2.Enabled = false;
                }

            }
            else
            {
                PanelMosh2.Visible = false;
                chkMosh2.Checked = true;
            }

            if (resDefOstads.FirstGuideId != "" && resDefOstads.FirstGuideId != null)
            {
                PanelRah1.Visible = true;
                chkRah1.Checked = resScore.FlagAcceptScoreRah1.Value;
                if (user == resDefOstads.FirstGuideId && !resScore.FlagAcceptScoreRah1.Value)
                {
                    chkRah1.Enabled = true;
                    btnAccept.Visible = true;
                    btnRejectOrDisp.Text = "انصراف";
         
                }
                else
                {
                    chkRah1.Enabled = false;
                }
            }
            else
            {
                PanelRah1.Visible = false;
                chkRah1.Checked = true;
            }
            if (resDefOstads.SecondGuideId != "" && resDefOstads.SecondGuideId != null)
            {
                PanelRah2.Visible = true;
                chkRah2.Checked = resScore.FlagAcceptScoreRah2.Value;
                if (user == resDefOstads.SecondGuideId && !resScore.FlagAcceptScoreRah2.Value)
                {
                    chkRah2.Enabled = true;
                    btnAccept.Visible = true;
                    btnRejectOrDisp.Text = "انصراف";
                    
                }
                else
                {
                    chkRah2.Enabled = false;
                }
            }
            else
            {
                PanelRah2.Visible = false;
                chkRah2.Checked = true;
            }
            if (resDefOstads.FirstRefereeId != "" && resDefOstads.FirstRefereeId != null)
            {
                PanelDav1.Visible = true;
                chkDav1.Checked = resScore.FlagAcceptScoreDavin.Value;
                if (user == resDefOstads.FirstRefereeId && !resScore.FlagAcceptScoreDavin.Value)
                {
                    chkDav1.Enabled = true;
                    btnAccept.Visible = true;
                    btnRejectOrDisp.Text = "انصراف";
                    
                }
                else
                {
                    chkDav1.Enabled = false;
                }
            }
            else
            {
                PanelDav1.Visible = false;
                chkDav1.Checked = true;
            }
            if (resDefOstads.SecondRefereeId != "" && resDefOstads.SecondRefereeId != null)
            {
                PanelDav2.Visible = true;
                chkDav2.Checked = resScore.FlagAcceptScoreDavOut.Value;
                if (user == resDefOstads.SecondRefereeId && !resScore.FlagAcceptScoreDavOut.Value)
                {
                    chkDav2.Enabled = true;
                    btnAccept.Visible = true;
                    btnRejectOrDisp.Text = "انصراف";
                   
                }
                else
                {
                    chkDav2.Enabled = false;
                }
            }
            else
            {
                PanelDav2.Visible = false;
                chkDav2.Checked = true;
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#ModalAcceptScore').modal();", true);
            //upModalAccept.Update();
        }

        protected void grdDsiplayDefence_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RfrhgrdDefenceMeetings();
        }
        public void RfrhgrdDefenceMeetings()
        {
          
            user = Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dt = new DataTable();
            dt = _requestHandler.GetMeetingDefencesforScore("200" + user);

            if (dt != null && dt.Rows.Count > 0)
            {
                AddFlagDisplayOrAcceptScore(("200"+ user), dt);
                grdDsiplayDefence.DataSource = dt;
            }
            else
                grdDsiplayDefence.DataSource = string.Empty;

        }
        public DataTable AddFlagDisplayOrAcceptScore(string user ,DataTable dt)
        {
            dt.Columns.Add("flagDisplay", typeof(System.Int32));



            foreach (DataRow row in dt.Rows)
            {
                row["flagDisplay"] = 1;
        
                var resDefOstads = _requestHandler.GetDefenceInformation(row["studentcode"].ToString());
                resScore = _requestHandler.GetScoreForDefence(int.Parse(row["RequestId"].ToString()));
                if (resDefOstads.FirstConsultantId != "" && resDefOstads.FirstConsultantId != null)
                {
                    if (user == resDefOstads.FirstConsultantId && !resScore.FlagAcceptScoreMosh1.Value)
                    {
                        row["flagDisplay"] = 0;
                    }
                }
                if (resDefOstads.SecondConsultantId != "" && resDefOstads.SecondConsultantId != null)
                {
                    if (user == resDefOstads.SecondConsultantId && !resScore.FlagAcceptScoreMosh2.Value)
                    {
                        row["flagDisplay"] = 0;
                    }
                }

                if (resDefOstads.FirstGuideId != "" && resDefOstads.FirstGuideId != null)
                {

                    if (user == resDefOstads.FirstGuideId && !resScore.FlagAcceptScoreRah1.Value)
                    {
                        row["flagDisplay"] = 0;
                    }
                }
                if (resDefOstads.SecondGuideId != "" && resDefOstads.SecondGuideId != null)
                {
                    if (user == resDefOstads.SecondGuideId && !resScore.FlagAcceptScoreRah2.Value)
                    {
                        row["flagDisplay"] = 0;
                    }
                }
                if (resDefOstads.FirstRefereeId != "" && resDefOstads.FirstRefereeId != null)
                {
                    if (user == resDefOstads.FirstRefereeId && !resScore.FlagAcceptScoreDavin.Value)
                    {
                        row["flagDisplay"] = 0;
                    }
                }
                if (resDefOstads.SecondRefereeId != "" && resDefOstads.SecondRefereeId != null)
                {
                    if (user == resDefOstads.SecondRefereeId && !resScore.FlagAcceptScoreDavOut.Value)
                    {
                        row["flagDisplay"] = 0;
                    }
                }
            }
            return dt;
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "پیام سیستم";
            resScore = _requestHandler.GetScoreForDefence(int.Parse(hdnReqId.Value));
            var resDefOstads = _requestHandler.GetDefenceInformation(hdnStcode.Value);
            user = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            string DscAccespt="";
            bool flagOneAccept = true;
            if (user == resDefOstads.FirstConsultantId)
            {
                resScore.FlagAcceptScoreMosh1 = chkMosh1.Checked;
                DscAccespt = " تایید نمره دفاع توسط استاد مشاور جناب آقای /سرکارخانم ";
                if (chkMosh1.Enabled == false|| chkMosh1.Checked==false)
                    flagOneAccept = false;

            }
            else if (user == resDefOstads.SecondConsultantId)
            {
                resScore.FlagAcceptScoreMosh2 = chkMosh2.Checked;
                DscAccespt = " تایید نمره دفاع توسط استاد مشاور جناب آقای /سرکارخانم ";
                if (chkMosh2.Enabled == false || chkMosh2.Checked == false)
                    flagOneAccept = false;
            }
            else if (user == resDefOstads.FirstGuideId)
            {
                resScore.FlagAcceptScoreRah1 = chkRah1.Checked;
                DscAccespt = " تایید نمره دفاع توسط استاد راهنما  جناب آقای /سرکارخانم ";
                if (chkRah1.Enabled == false || chkRah1.Checked == false)
                    flagOneAccept = false;

            }
            else if (user == resDefOstads.SecondGuideId)
            {
                resScore.FlagAcceptScoreRah2 = chkRah2.Checked;
                DscAccespt = " تایید نمره دفاع توسط استاد راهنما  جناب آقای /سرکارخانم ";
                if (chkRah2.Enabled == false || chkRah2.Checked == false)
                    flagOneAccept = false;
            }
            else if (user == resDefOstads.FirstRefereeId)
            {
                resScore.FlagAcceptScoreDavin = chkDav1.Checked;
                DscAccespt = " تایید نمره دفاع توسط استاد داور  جناب آقای /سرکارخانم ";
                if (chkDav1.Enabled == false || chkDav1.Checked == false)
                    flagOneAccept = false;
            }
            else if (user == resDefOstads.SecondRefereeId)
            {
                resScore.FlagAcceptScoreDavOut = chkDav2.Checked;
                DscAccespt = " تایید نمره دفاع توسط استاد داور  جناب آقای /سرکارخانم ";
                if (chkDav2.Enabled == false || chkDav2.Checked == false)
                    flagOneAccept = false;
            }
            else
            {
                lblAlert.Text = "سیستم دچار اختلال شده است ,لطفا بعدا امتحان کنید";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#ModalAlert').modal();", true);
                upModalAlert.Update();
                return;
            }
            if (resDefOstads.FirstConsultantId == null)
            {
                resScore.FlagAcceptScoreMosh1 = true;
            }
            if (resDefOstads.SecondConsultantId == null)
            {
                resScore.FlagAcceptScoreMosh2 = true;
            }
            if (resDefOstads.FirstGuideId == null)
            {
                resScore.FlagAcceptScoreRah1 = true;
            }
            if (resDefOstads.SecondGuideId == null)
            {
                resScore.FlagAcceptScoreRah2 = true;
            }
            if (resDefOstads.FirstRefereeId == null)
            {
                resScore.FlagAcceptScoreDavin = true;
            }
            if (resDefOstads.SecondRefereeId == null)
            {
                resScore.FlagAcceptScoreDavOut = true;
            }



            if (_requestHandler.UpdateScoreForDefence(resScore))
            {
                var oscode = Session[sessionNames.userID_StudentOstad].ToString();
                commonBusiness = new CommonBusiness();
                lblAlert.Text = "عملیات تایید نمره دفاع با موفقیت انجام شد";
                
                if (flagOneAccept)
                {
                    DscAccespt += " "+Session[sessionNames.userName_StudentOstad].ToString()+" ";
                    commonBusiness.InsertIntoStudentLog(oscode, DateTime.Now.ToString("HH:mm")
                            , 11, 56, DscAccespt, int.Parse(hdnReqId.Value));
                   
                    grdDsiplayDefence.DataSource = null;
                    grdDsiplayDefence.Rebind();
                    //grdDsiplayDefence.Rebind();
                }
            }
            else
            {
                lblAlert.Text = "سیستم دچار اختلال شده است ,لطفا بعدا امتحان کنید";
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#ModalAcceptScore').modal('hide');", true);
            //upModalAccept.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#ModalAlert').modal();", true);
            upModalAlert.Update();
        }

        protected void btnRejectOrDisp_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#ModalAcceptScore').modal('hide');", true);
           // upModalAccept.Update();
        }
    }
}