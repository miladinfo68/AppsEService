using IAUEC_Apps.Business.Common;
using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.ResourceControl.Admin
{
    public partial class main : System.Web.UI.Page
    {


        int daneshId = 0;
        int SectionId = 0;

        public main()
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
            if (!IsPostBack)
            {
                Session["StausLinke"] = null;
                LoginBusiness LoginB = new LoginBusiness();
                //HiddenField hiddenField = null;
                //HiddenField hiddenField2 = null;
                string userId = Session[sessionNames.userID_Karbar].ToString();
                DataTable userRoles = LoginB.Get_UserRoles(userId);
                int UserRole = int.Parse(userRoles.Rows[0]["RoleId"].ToString());
                int IssuerId = Convert.ToInt32(userId);
                Session.Add("IssuerId", IssuerId);

                //switch (UserRole)
                //{
                //    case "15":
                //    case "16":
                //    case "17":
                //    case "26":
                //    case "27":
                //    case "28":

                //        hiddenField = (HiddenField)Page.Master.FindControl("HiddenField1");
                //        break;
                //    case "37":
                //    case "38":
                //    case "39":
                //    case "40":
                //    case "50":
                //        hiddenField = (HiddenField)Page.Master.FindControl("HiddenField2");
                //        break;
                //    case "1":
                //    case "7":

                //        hiddenField = (HiddenField)Page.Master.FindControl("HiddenField1");
                //        hiddenField2 = (HiddenField)Page.Master.FindControl("HiddenField2");
                //        break;
                //    default:
                //        string scrp = "alert('شما مجاز به استفاده از این سیستم نمی باشید.');";
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", scrp, true);
                //        break;
                //}
                if (Session["DaneshId"] != null)
                {
                    daneshId = Convert.ToInt32(Session["DaneshId"]);
                }
                if (Session["SectionId"] != null)
                {
                    SectionId = Convert.ToInt32(Session["SectionId"]);
                }

                //waitForSend.HRef = "../" + hiddenField.Value.ToString() + "&status=0";
                //sent.HRef = "../" + hiddenField.Value.ToString() + "&status=1";
                //approved.HRef = "../" + hiddenField.Value.ToString() + "&status=2";
                //denied.HRef = "../" + hiddenField.Value.ToString() + "&status=3";
                //Informed.HRef = "../" + hiddenField.Value.ToString() + "&status=4";
                //lost.HRef = "../" + hiddenField.Value.ToString() + "&status=5";
                //if (!(/*(UserRole>40 && UserRole<49)||*/(UserRole > 55 && UserRole < 62) || (UserRole == 63) || (UserRole == 65)))
                if(IsReserveRole(UserRole))
                    LoadPanels(userId, UserRole.ToString(), daneshId, IssuerId, SectionId);

            }

        }





        private void LoadPanels(string userId, string UserRole, int daneshId, int IssuerId, int SectionId)
        {
            RequestHandler ReqHandler = new RequestHandler();
            mainViewModel ReqsCount = null;
            mainViewModel ReqsCount1 = null;

            if (UtilityFunction.IsMasouleDorehKootahModat(Convert.ToInt32(UserRole)) > 0)
            {
                if (!(daneshId == 0 || daneshId == 5))
                {
                    dvWaitingForSend.Visible = true;
                    dvSent.Visible = true;
                    dvApproved.Visible = true;
                    dvDenied.Visible = true;
                    ReqsCount = ReqHandler.GetRequestCountByStatusAndDaneshId(daneshId);
                    ReqsCount1 = ReqHandler.GetRequestCountByStatusAndDaneshIdForDefence(daneshId);
                }
                if (daneshId == 5)
                {
                    dvWaitingForSend.Visible = true;
                    dvSent.Visible = true;
                    dvApproved.Visible = true;
                    dvDenied.Visible = true;
                    dvWaitingForSend.Visible = false;
                    ReqsCount = ReqHandler.GetRequestCountByStatusAndIssuerId(IssuerId);
                    ReqsCount1 = ReqHandler.GetRequestCountByStatusAndIssuerIdForDefence(IssuerId);

                }
                if (daneshId == 0)
                {


                    dvWaitingForSend.Visible = true;
                    dvSent.Visible = true;
                    dvApproved.Visible = true;
                    dvDenied.Visible = true;
                    dvInformed.Visible = true;
                    dvLost.Visible = true;
                    ReqsCount = ReqHandler.GetRequestCountByStatusForAdmin();
                    ReqsCount1 = ReqHandler.GetRequestCountByStatusForAdminForDefence();

                    //  ReqsCount = ReqHandler.GetRequestCountByStatus();
                }

            }
            if (UserRole == "26" || UserRole == "27" || UserRole == "28" || UserRole == "68"
               || UserRole == "15" || UserRole == "16" || UserRole == "17" || UserRole == "67"
               || UserRole == "66" || UserRole == "53" || UserRole == "52" || UserRole == "51")//amoozesh
            {
                dvWaitingForSend.Visible = true;
                dvSent.Visible = true;
                dvApproved.Visible = true;
                dvDenied.Visible = true;
                dvLost.Visible = false;
                dvDenied2.Visible = true;
                dvDenied3.Visible = true;



                dvWaitingForSend1.Visible = true;
                dvSent1.Visible = true;
                dvApproved1.Visible = true;
                //dvDenied1.Visible = true;
                //dvLost1.Visible = false;
                //dvDenied21.Visible = true;
                //dvDenied31.Visible = true;

                //var rq = new RequestHandler();
                //var lostCount = rq.GetRequestListBystatusAnddaneshID(5, daneshId).Where(c => c.Status == 0).ToList().Count;
                ReqsCount = ReqHandler.GetRequestCountByStatusAndDaneshId(daneshId);
                ReqsCount1 = ReqHandler.GetDefenceRequestCountByLocationForEducation(daneshId);

                //ReqsCount.LostCount = lostCount;
            }
            if (UserRole == "38" || UserRole == "37")//edari molasadra
            {
                dvSent.Visible = true;
                dvApproved.Visible = true;
                dvDenied.Visible = true;
                dvLost.Visible = true;

                //ReqsCount = ReqHandler.GetRequestCountByLocation(1);
                ReqsCount = ReqHandler.GetRequestCountByLocationForEdari(1);
                ReqsCount1 = ReqHandler.GetRequestCountByLocationForEdariForDefence(1);
            }

            if (UtilityFunction.IsMasouleDaftarUser(Convert.ToInt32(UserRole)))//Masoul daftar -bonyan -omoumi 
            {
                // dvWaitingForSend.Visible = true;
                dvSent.Visible = true;
                dvApproved.Visible = true;
                dvDenied.Visible = true;
                //ReqsCount = ReqHandler.GetRequestCountByStatusAndDaneshId(daneshId);
                ReqsCount = ReqHandler.GetRequestCountByStatusAndIssuerId(IssuerId);
                ReqsCount1 = ReqHandler.GetRequestCountByStatusAndIssuerIdForDefence(IssuerId);

            }
            //if (UserRole=="62" || UserRole == "32" || UserRole=="21")// M_daneshjoyi - moAven AMouzeshi
            //{
            //    // dvWaitingForSend.Visible = true;
            //    dvSent.Visible = true;
            //    dvApproved.Visible = true;
            //    dvDenied.Visible = true;
            //    //ReqsCount = ReqHandler.GetRequestCountByStatusAndDaneshId(daneshId);
            //    ReqsCount = ReqHandler.GetRequestCountByStatusAndIssuerId(IssuerId);

            //}


            if (UserRole == "39" || UserRole == "40")//edari pasdaran
            {
                dvSent.Visible = true;
                dvApproved.Visible = true;
                dvDenied.Visible = true;
                dvSent1.Visible = true;
                dvApproved1.Visible = true;

                //ReqsCount = ReqHandler.GetRequestCountByLocation(2);

                ReqsCount = ReqHandler.GetRequestCountByLocationForEdari(2);
                ReqsCount1 = ReqHandler.GetDefenceRequestCountByLocationForEdari(2);


            }
            if (UserRole == "50")//moAven edari mali
            {
                dvSent.Visible = true;
                dvApproved.Visible = true;
                dvDenied.Visible = true;
                dvLost.Visible = true;
                ReqsCount = ReqHandler.GetRequestCountByStatus();
                ReqsCount1 = ReqHandler.GetRequestCountByStatusForDefence();
            }
            if (UserRole == "1")//admin
            {
                dvWaitingForSend.Visible = true;
                dvSent.Visible = true;
                dvApproved.Visible = true;
                dvDenied.Visible = true;
                dvInformed.Visible = true;
                dvLost.Visible = true;
                //ReqsCount = ReqHandler.GetRequestCountByStatus();
                ReqsCount = ReqHandler.GetRequestCountByStatusForAdmin();
                ReqsCount1 = ReqHandler.GetRequestCountByStatusForAdminForDefence();
            }
            //UserRole = "7";
            if (UserRole == "7")//fani
            {
                dvWaitingForSend1.Visible = true;
                //dvSent1.Visible = true;
                dvApproved1.Visible = true;

                ReqsCount1 = ReqHandler.GetDefenceRequestCountByLocationForTechnical();
            }
            lblWaitingForSend.Text = ReqsCount?.WaitingForSendCount.ToString();
            lblSent.Text = ReqsCount?.SentCount.ToString();
            lblApproved1.Text = ReqsCount?.ApprovedCount.ToString();
            lblDenid1.Text = ReqsCount?.DeniedCount.ToString();
            lblInformed.Text = ReqsCount?.InformedCount.ToString();
            lblLost.Text = ReqsCount?.LostCount.ToString();
            lblWaitingForSend1.Text = ReqsCount1?.WaitingForSendCount.ToString();
            //lblSent1.Text = ReqsCount1?.SentCount.ToString();
            if (UserRole != "7")
                lblApproved11.Text = ReqsCount1?.ApprovedCount.ToString();
            else
                lblApproved11.Text = ReqsCount1?.SentCount.ToString();

            lblDenid11.Text = ReqsCount1?.DeniedCount.ToString();
            lblInformed1.Text = ReqsCount1?.InformedCount.ToString();
            lblLost1.Text = ReqsCount1?.LostCount.ToString();
            RequestHandler rq = new RequestHandler();
            var countDeniedByEdari = 0;
            var countDeniedByAmouzesh = 0;
            var countDeniedByEdari1 = 0;
            var countDeniedByAmouzesh1 = 0;

            if (rq.GetRequestListBystatusAnddaneshID(5, daneshId) != null)
            {
                countDeniedByEdari = rq.GetRequestListBystatusAnddaneshID(5, daneshId).Where(c => c.Status == 0).ToList().Count;
                countDeniedByAmouzesh = rq.GetRequestListBystatusAnddaneshID(5, daneshId).Where(c => c.Status == 1).ToList().Count;
            }

            if (rq.GetRequestListBystatusAnddaneshIDForDefence(5, daneshId) != null)
            {
                countDeniedByEdari1 = rq.GetRequestListBystatusAnddaneshIDForDefence(5, daneshId).Where(c => c.Status == 0).ToList().Count;
                countDeniedByAmouzesh1 = rq.GetRequestListBystatusAnddaneshIDForDefence(5, daneshId).Where(c => c.Status == 1).ToList().Count;
            }
            
            lblDenied3.Text = countDeniedByAmouzesh.ToString();
            lblDenied2.Text = countDeniedByEdari.ToString();
            lblDenied31.Text = countDeniedByAmouzesh1.ToString();
            lblDenied21.Text = countDeniedByEdari1.ToString();


            if (UserRole == 62.ToString() 
                || UserRole == 32.ToString() 
                || UserRole == 64.ToString() ||
                UserRole == 50.ToString())
            {
                dvWaitingForSend.Visible = false;
                dvSent.Visible = false;
                dvApproved.Visible = false;
                dvDenied.Visible = false;
                dvInformed.Visible = false;
                dvLost.Visible = false;


            }
        }



        protected void FillLinkAndStatusForClick(int status)
        {
            Session["StausLinke"] = status.ToString();
            var link = "~/ResourceControl/" + Session["linkeeee"] as string;
            Response.Redirect(link);
        }
        protected void FillLinkAndStatusForClick1(int status)
        {
            Session["StausLinke"] = status.ToString();
            var link = "~/ResourceControl/" + Session["DefenceLink1"] as string;
            //  link=link.Replace("EducationStudentReview", "technicalstudentReview");
            Response.Redirect(link);
        }

        //***************************
        protected void FillLinkAndStatusForClickOffice(int status)
        {
            Session["StausLinke"] = status.ToString();
            var link = "~/ResourceControl/" + Session["linke2"] as string;
            Response.Redirect(link);
        }

        protected void FillLinkAndStatusForClickOffice1(int status)
        {
            Session["StausLinke"] = status.ToString();
            var link = "~/ResourceControl/" + Session["DefenceLink2"] as string;
            Response.Redirect(link);
        }

        //*************************
        protected void btnWaitingForSend_Click(object sender, EventArgs e)
        {

            FillLinkAndStatusForClick(0);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session[sessionNames.userID_Karbar].ToString()) == 1)
                FillLinkAndStatusForClickOffice(1);
            else
                FillLinkAndStatusForClick(1);


        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            FillLinkAndStatusForClick(2);
        }

        protected void btnInformed_Click(object sender, EventArgs e)
        {
            FillLinkAndStatusForClick(4);
        }

        protected void btnDenied_Click(object sender, EventArgs e)
        {
            FillLinkAndStatusForClick(3);
        }

        protected void btnLost_Click(object sender, EventArgs e)
        {
            FillLinkAndStatusForClick(5);
        }

        protected void btnDenied2_Click(object sender, EventArgs e)
        {
            FillLinkAndStatusForClick(5);
        }

        protected void btnDenied3_Click(object sender, EventArgs e)
        {
            FillLinkAndStatusForClick(7);
        }

        protected void btnWaitingForSend1_OnClick(object sender, EventArgs e)
        {
            FillLinkAndStatusForClick1(1);
        }

        protected void btnSend1_OnClick(object sender, EventArgs e)
        {
            var userRole = Session[sessionNames.roleID].ToString();

            if (userRole == "26" || userRole == "27" || userRole == "28" || userRole == "68"
                || userRole == "15" || userRole == "16" || userRole == "17" || userRole == "67"
                || userRole == "66" || userRole == "53" || userRole == "52" || userRole == "51") //amoozesh
            {
                FillLinkAndStatusForClick1(2);
            }
            if (userRole == "39" || userRole == "40")
                FillLinkAndStatusForClick1(1);

            if (userRole == "7")
                FillLinkAndStatusForClick1(2);
        }

        protected void btnApproved1_OnClick(object sender, EventArgs e)
        {
            var userRole = Session[sessionNames.roleID].ToString();

            if (userRole == "26" || userRole == "27" || userRole == "28" || userRole == "68"
            || userRole == "15" || userRole == "16" || userRole == "17" || userRole == "67"
            || userRole == "66" || userRole == "53" || userRole == "52" || userRole == "51") //amoozesh
            {
                FillLinkAndStatusForClick1(3);
            }
            if (userRole == "39" || userRole == "40")
                FillLinkAndStatusForClick1(2);

            if (userRole == "7")
                FillLinkAndStatusForClick1(2);
        }

        protected void btnInformed1_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnDenied1_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnDenied21_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnDenied31_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnLost1_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public static bool IsReserveRole(int roleId)
        {
            return (roleId == 7 || roleId == 50 || roleId == 39 || roleId == 40
                    || roleId == 38 || roleId == 37 || roleId == 26 || roleId == 27
                    || roleId == 28 || roleId == 68 || roleId == 15 || roleId == 16
                    || roleId == 17 || roleId == 67 || roleId == 66 || roleId == 53
                    || roleId == 52 || roleId == 51 || (roleId > 40 && roleId < 49)
                    || roleId == 13 || roleId == 14 || roleId == 73 || roleId == 55
                    || roleId == 57 || roleId == 62 || roleId == 64 || roleId == 58
                    || roleId == 59 || roleId == 60 || roleId == 61 || roleId == 63
                    || roleId == 65 || roleId == 74 || roleId == 75 || roleId == 1
                    || roleId==32);
        }
    }

}