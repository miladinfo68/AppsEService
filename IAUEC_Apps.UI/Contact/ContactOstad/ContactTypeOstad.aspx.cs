using System;
using System.Data;
using IAUEC_Apps.Business.Conatct;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Contact.ContactOstad
{
    public partial class ConatctTypeOstad : System.Web.UI.Page
    {
        protected static string userID_Ostad;
        protected static string idGrp = "1";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                userID_Ostad = "20056";// Session[sessionNames.userID_Karbar].ToString();
                RfrhDtLstContact();
                RfrhDtLstMessage(true);
            }
        }


        public void RfrhDtLstMessage(bool flagGroup)
        {

            DtLstMesages.DataSource = null;
            DtLstMesages.DataBind();
            DataTable dt;
            if (flagGroup == false && TxtIdOnChat.Text.Trim() != "")
            {
                dt = MessagePersonalBuisnes.GetMessagePersonal(userID_Ostad, TxtIdOnChat.Text);
            }
            else
            {
                dt = null;
             //   dt = MesageGroupBuisnes.GetMessageGroup(userID_Ostad);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                DtLstMesages.DataSource = dt;
                DtLstMesages.DataBind();
            }
        }

        public void RfrhDtLstContact()
        {
            if (userID_Ostad != null && userID_Ostad != "")
                DtlstContact.DataSource = ContactBuisnes.GetConatctStudentForType(userID_Ostad,1);
            DtlstContact.DataBind();

        }
        [System.Web.Services.WebMethod]
        public static void DeleteUnread()
        {
            //if (userID_Ostad != "")
             //   MsgUnReadOstadBuisnes.DeleteUnReadMsgOstad(userID_Ostad);

        }

        [System.Web.Services.WebMethod]
        public static void InsertMessage(string msg, string idOnChat, int idMsgReplayed)
        {
            bool flagReplayed = false;
            if (idMsgReplayed != 0)
            {
                flagReplayed = true;
            }
            if (msg != "" && idOnChat != "")
            {
                if (idOnChat != idGrp)
                {
                    MessagePersonalBuisnes.EnterMessagePersonal(userID_Ostad, idOnChat, msg, flagReplayed, idMsgReplayed);
                }
                else
                {// MesageGroupBuisnes.EnterMessageGroup(userID_Ostad, msg, flagReplayed, idMsgReplayed);
                }
                
            }

        }



        protected void DtlstContact_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            if (e.CommandName == "IdName")
            {
                TxtIdOnChat.Text = Convert.ToString(e.CommandArgument);
                if (TxtIdOnChat.Text != "" && TxtIdOnChat.Text != "0")
                    RfrhDtLstMessage(false);
                else
                    RfrhDtLstMessage(true);
            }
        }



        protected void LnkNameConatct_Click(object sender, EventArgs e)
        {
            LinkButton lBtn = sender as LinkButton;
            string id = ((LinkButton)sender).CommandArgument.ToString();
            LblNameOnChat.Text = lBtn.Text;
        }

        protected void BtnGrp_Click(object sender, EventArgs e)
        {
            LblGrp.Text = "گروه";
            RfrhDtLstMessage(true);
        }
    }
}
