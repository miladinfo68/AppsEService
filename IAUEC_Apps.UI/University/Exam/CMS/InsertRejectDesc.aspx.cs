using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System.Data;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class InsertRejectDesc : System.Web.UI.Page
    {

        CommonBusiness CB = new CommonBusiness();
        ExamBusiness examBusiness = new ExamBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            div_Main.Visible = false;
            Confirmpnl.Visible = true;
        }

        protected void conf_Click(object sender, EventArgs e)
        {
            if (txt_Reject.Text != "")
            {
                string did =Request.QueryString["did"].ToString();
                int qID = int.Parse(Request.QueryString["qID"].ToString());
                int q2CityId = int.Parse(Request.QueryString["q2CityId"].ToString());
                int q2Status = int.Parse(Request.QueryString["q2Status"].ToString());
                int q1Status = int.Parse(Request.QueryString["q1Status"].ToString());

                int? cityIDQ2 = null;
                var zipFilePath = $"{did}.zip";
                var zipFilePathDestination = $"{did}_{new Random().Next(1000, 999999999)}.zip";

                if (q1Status == 3 && q2Status != -1)
                {
                    cityIDQ2 = q2CityId;
                    if (q2CityId == -1)
                    {
                        zipFilePath = $"{did}_canceled_1.zip";
                        zipFilePathDestination = $"{did}_canceled_1_{new Random().Next(1000, 999999999)}.zip";
                    }
                    if (q2CityId > 0)
                    {
                        zipFilePath = $"{did}_canceled_2.zip";
                        zipFilePathDestination = $"{did}_canceled_1_{new Random().Next(1000, 999999999)}.zip";
                    }
                }

                //var dtDetail = examBusiness.Get_ExamdetailbyDid(did, null, cityIDQ2);
                //==========================
                if (q2Status == 9)
                {
                    examBusiness.UpdateExamQuestionsCancled(qID, null, null, 11, q2CityId, txt_Reject.Text);
                }
                else
                {
                    examBusiness.UpdateQueizStatus(4, did, txt_Reject.Text, false);
                }
                //==========================               
                var dtpath = examBusiness.ShowQueizPaperByDid(did, cityIDQ2);
                string sourceFilePath = Server.MapPath("~/QueizPapers/" + dtpath.Rows[0]["tterm"].ToString() + "/" + dtpath.Rows[0]["code_ostad"].ToString() + "/pdffiles/" + dtpath.Rows[0]["coursecode"].ToString() );
                string destinationFilePath = Server.MapPath("~/QueizPapers/" + dtpath.Rows[0]["tterm"].ToString() + "/" + dtpath.Rows[0]["code_ostad"].ToString() + "/pdffiles/MoveFiles/" + dtpath.Rows[0]["coursecode"].ToString() );
                if (!Directory.Exists(destinationFilePath))
                    Directory.CreateDirectory(destinationFilePath);

                System.IO.File.Move($"{sourceFilePath}/{zipFilePath}",$"{destinationFilePath}/{zipFilePathDestination}" );


                examBusiness.TemplateDownloaded(did, false);
                var reasonReject = " رد سوالات امتحان ==>" + txt_Reject.Text;
                CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 56, reasonReject, int.Parse(dtpath.Rows[0]["QuestionId"].ToString()));
                // ramezanian - ارسال اس ام اس
                DataTable dtResault = examBusiness.GetMobileProfByDid(did);
                DataTable dtMessage = CB.GetTextMessage(0, 8, 4, 4);
                string oldText = dtMessage.Rows[0]["Text"].ToString().Trim();
                int index = oldText.IndexOf("شما");
                var newText = oldText.Substring(0, index + 3) + "با کد مشخصه " + did.ToString() + " " + oldText.Substring(index + 3);
                //string Text = (newText + "\r\n" + "http://service.iauec.ac.ir" + "\r\n" + "مراجعه نمایید");  
                var Text = $"{newText} ، http://service.iauec.ac.ir ، مراجعه نمایید ";
                //var Text = string.Format("{0}  {1} \r\n {2}", newText, "http://service.iauec.ac.ir", "مراجعه نمایید");
                //
                string IdAppMessage = dtMessage.Rows[0]["ID"].ToString();
                try
                {
                    if (!string.IsNullOrWhiteSpace(dtResault.Rows[0]["mobile"].ToString()))
                    {
                        string sendingSMSResult = "";

                        string smsStatusText; bool sentSMS;
                        sendingSMSResult = CB.sendSMS(dtResault.Rows[0]["mobile"].ToString(), Text, out sentSMS, out smsStatusText);
                        if (!sentSMS)
                        {
                            rwm.RadAlert(smsStatusText, null, 100, "خطا", "");
                            return;
                        }
                        int status = CB.getAsanakStatusID(sendingSMSResult);
                        CB.LogStatusMessage(dtResault.Rows[0]["code_ostad"].ToString(), sendingSMSResult, dtResault.Rows[0]["mobile"].ToString(), status, smsStatusText, int.Parse(IdAppMessage));

                    }
                    Confirmpnl.Visible = false;
                    div_Main.Visible = true;
                    txt_Reject.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "conf", "CloseModal();", true);
                }
                catch (Exception x)
                {
                    //throw;
                    rwm.RadAlert("User Name Is Invalid ! Close Current Window", null, 100, "خطا", "");//("علت رد، باید وارد گردد", null, 100, "خطا", "");
                    return;
                }
                rwm.RadAlert("ثبت انجام شد", null, 100, "پیغام", "");//("علت رد، باید وارد گردد", null, 100, "خطا", "");
                ScriptManager.RegisterStartupScript(this, GetType(), "conf", "CloseModal();", true);
                return;

            }
            else
            {
                rwm.RadAlert("علت رد، باید وارد گردد", null, 100, "خطا", "");
            }

        }

        protected void notConf_Click(object sender, EventArgs e)
        {
            Confirmpnl.Visible = false;
            div_Main.Visible = true;
            txt_Reject.Text = "";
        }

        protected void ddlPreDefinedReasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPreDefinedReasons.SelectedItem.Value == "-1")
                txt_Reject.Text = "";
            else
                txt_Reject.Text = ddlPreDefinedReasons.SelectedItem.Text;
        }
    }
}