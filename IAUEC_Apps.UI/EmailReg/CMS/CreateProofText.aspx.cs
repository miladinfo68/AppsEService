using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class CreateProofText : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
            AccessControl1.MenuId = menuId;
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                CommonBusiness cmnb = new CommonBusiness();

                string Prooftext = txt_ProofText.Text.ToString();
                ProofTextBusiness proofB = new ProofTextBusiness();
                proofB.CreateProofTextByProofText(Prooftext);
                txt_ProofText.Text = "";
                RadWindowManager1.RadAlert("پیام بدرستی ثبت گردید", 200, 200, "پیام", "CallBackConfirm");

                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 30, "");
            }
            catch (Exception)
            {
                RadWindowManager1.RadAlert("پیام بدرستی ثبت نگردید", 200, 200, "پیام", "CallBackConfirm");
            }
        }
    }
}