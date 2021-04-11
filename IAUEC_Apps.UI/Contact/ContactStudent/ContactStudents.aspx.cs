using System;
using IAUEC_Apps.Business.Conatct;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.UI.Contact.Functions;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using System.Text;

namespace IAUEC_Apps.UI.Contact.ContactStudent
{
    public partial class ContactStudents : System.Web.UI.Page
    {
        protected static string userID_Student;
        protected static string idGrp = "1";
        protected static bool flagGrp = true;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                userID_Student = Session[sessionNames.userID_StudentOstad].ToString();
                LblIdGrp.Text = userID_Student;
                LblIdUser.Text = userID_Student;
                RfrhDtLstContact();
                if (Request.QueryString["Flag_Grp"] != null)
                {
                    if (bool.Parse(Request.QueryString["Flag_Grp"].ToString()) == true
                        && Request.QueryString["IdGrpOrPerson"] != null)
                    {
                        TxtIdOnChat.Text = "";
                        LblIdGrp.Text = Request.QueryString["IdGrpOrPerson"];
                        flagGrp = true;
                    }
                    else if (bool.Parse(Request.QueryString["Flag_Grp"].ToString()) == false &&
                        Request.QueryString["IdGrpOrPerson"] != null)
                    {
                        flagGrp = false;
                        LblIdGrp.Text = "";
                        TxtIdOnChat.Text = Request.QueryString["IdGrpOrPerson"].ToString();

                    }
                }
                RfrhDtLstMessage(flagGrp);
            }
        }

        //[DllImport("winmm.dll",CharSet = CharSet.Ansi)]
        //public static extern long mciSendString(string command, StringBuilder retString, int returnPath, IntPtr callBack);

        //public ContactStudents()
        //{
        //    mciSendString("open new Type waveaudio alias recsound ", null, 0, IntPtr.Zero);
        //    //mciSendString("set capture samplespersec 11025", null, 0, IntPtr.Zero);//11025,22050,44100
        //    //mciSendString("set capture channels 1", null, 0, IntPtr.Zero);
        //}
        //[System.Web.Services.WebMethod]
        //public static void RecordVoice()//(string userID, string idOnChat, string idGrp, Object msg, int idMsgReplayed)
        //{
        //    mciSendString("record recsound", null, 0, IntPtr.Zero);

        //}
        //[System.Web.Services.WebMethod]
        //public static string SaveVoice(string userID, string idOnChat, string idGrp, string msg, int idMsgReplayed)
        //{
        //    const int flagVoice = 2;
        //    var voice = Functions.MessageJs.InsertMessage(userID, idOnChat, idGrp, msg, idMsgReplayed, flagVoice, ".wav");
        //    JObject jsonVoice = JObject.Parse(voice.Substring(1, voice.Length - 2));
        //    var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Contact/SoundRecorder");
        //    string command = "save recsound \"" + path + "\\" + jsonVoice["ChatID_P"] + ".wav \"";
        //    mciSendString("pause recsound", null, 0, IntPtr.Zero);
        //    mciSendString(command, null, 0, IntPtr.Zero);
        //    mciSendString("close recsound", null, 0, IntPtr.Zero);

        //    return voice;
        //}
        public void RfrhDtLstMessage(bool flagGroup)
        {

            DtLstMesages.DataSource = null;
            DtLstMesages.DataBind();
            DataTable dt;
            if (flagGroup == false && TxtIdOnChat.Text.Trim() != "")
            {
                LblNameOnChat.Text = "گفتگو شخصی";
                dt = MessagePersonalBuisnes.GetMessagePersonal(userID_Student, TxtIdOnChat.Text);
            }
            else
            {
                LblNameOnChat.Text = "گفتگو دفاع";
                dt = MesageGroupBuisnes.GetMessageGroup(userID_Student, userID_Student);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = Functions.AddDefualt.GetDefualt(dt);
                DtLstMesages.DataSource = dt;
                DtLstMesages.DataBind();
                if (dt.Rows[dt.Rows.Count - 1]["ChatID"] != null)
                    LblLastIdChat.Text = dt.Rows[dt.Rows.Count - 1]["ChatID"].ToString();
            }
            

        }

        public void RfrhDtLstContact()
        {
            if (userID_Student != null && userID_Student != "")
            {
                DataTable dt = ContactBuisnes.GetConatctOstads(userID_Student);
                dt = Functions.AddDefualt.GetDefualt(dt);
                DtlstContact.DataSource = dt;
                DtlstContact.DataBind();
            }

        }


        protected void DtlstContact_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            if (e.CommandName == "IdName")
            {
                LblCurCount.Text = "0";
                LblSearchCount.Text = "0";
                LblSearchText.Text = "";
                TxtIdOnChat.Text = Convert.ToString(e.CommandArgument);

                if (TxtIdOnChat.Text != "" && TxtIdOnChat.Text != "0")
                {
                    RfrhDtLstMessage(false);
                    LblIdGrp.Text = "";
                    MessageJs.DeleteUnreadStudent(LblIdUser.Text, "false", TxtIdOnChat.Text);
                }

                else
                {
                    RfrhDtLstMessage(true);
                    LblIdGrp.Text = userID_Student;
                    MessageJs.DeleteUnreadStudent(LblIdUser.Text, "true", "-1");
                }

            }
        }



        protected void LnkNameConatct_Click(object sender, EventArgs e)
        {
            LinkButton lBtn = sender as LinkButton;
            string id = ((LinkButton)sender).CommandArgument.ToString();
            LblNameOnChat.Text = lBtn.Text;
            LblIdGrp.Text = "";
        }

        protected void BtnGrp_Click(object sender, EventArgs e)
        {
            LblNameOnChat.Text = "گفتگو دفاع";
            LblIdGrp.Text = userID_Student;
            RfrhDtLstMessage(true);
        }



    }
}
