using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Wallet;
using IAUEC_Apps.DTO.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Wallet.MasterPages
{
    public partial class WalletPages : System.Web.UI.MasterPage
    {
        WalletBusiness _walletBusiness = new WalletBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null && string.IsNullOrEmpty(Request.Params["RefId"]))
                Response.Redirect("~/CommonUI/login.aspx", false);
            else if (!string.IsNullOrEmpty(Request.Params["RefId"]))
            {
                var pay = _walletBusiness.GetPaymentByRequestKey(Request.Params["RefId"]);
                if (pay != null)
                {
                    Session[sessionNames.userID_StudentOstad] = pay.stcode;
                    LoginBusiness lgnB = new LoginBusiness();
                    LoginDTO stInfo = lgnB.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                    stName.InnerText = stInfo.Name + " " + stInfo.LastName;
                }
                else
                    Response.Redirect("~/CommonUI/login.aspx", false);
            }
            else if (!IsPostBack)
            {
                LoginBusiness lgnB = new LoginBusiness();
                LoginDTO stInfo = lgnB.Get_StInfo(Session[sessionNames.userID_StudentOstad].ToString());
                stName.InnerText = stInfo.Name + " " + stInfo.LastName;
            }
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session[sessionNames.userID_StudentOstad] = null;
            Session["LogStatus"] = "0-0";
            Response.Redirect("~/CommonUI/login.aspx");
        }

        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/CommonUI/IntroPage.aspx");
        }
    }
}