using IAUEC_Apps.Business.Conatct;
using IAUEC_Apps.UI.Contact.Functions;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;


namespace IAUEC_Apps.UI.Contact.ContactOstad
{
    public partial class ContactByStudent : System.Web.UI.Page
    {
        protected static string userID_Ostad;
        protected static string idGrp = "1";
        protected static bool flagGroup = false;
        protected static string UserStudent = "";
        protected const string OstadUserPrev = "200";


        //[DllImport("winmm.dll", CharSet =  CharSet.Ansi)]
        //public static extern long mciSendString(string command, StringBuilder retString, int returnPath, IntPtr callBack);

        //public ContactByStudent()
        //{
        //       mciSendString("open new Type waveaudio alias recsound ", null, 0, IntPtr.Zero);
        //       //mciSendString("set recsound bitpersample 8", null, 0, IntPtr.Zero);//8,16
        //       //mciSendString("set recsound samplespersec 11025", null, 0, IntPtr.Zero);//11025,22050,44100
        //       //mciSendString("set recsound channels 1", null, 0, IntPtr.Zero);
        //      //mciSendString("set recsound bitspersample 8 samplespersec 8000 channels 1", null, 0, IntPtr.Zero);
        //}
        //[System.Web.Services.WebMethod]
        //public static void RecordVoice()//(string userID, string idOnChat, string idGrp, Object msg, int idMsgReplayed)
        //{

        //   //  mciSendString("set recsound samplespersec 44100", null, 0, IntPtr.Zero);
        //     mciSendString("record recsound", null, 0, IntPtr.Zero);
        //    // return null;

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
        ////[System.Web.Services.WebMethod]
        ////public static void RecordVoice()
        ////{
        ////    SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
        ////    Grammar dictationGrammar = new DictationGrammar();
        ////    recognizer.LoadGrammar(dictationGrammar);
        ////    try
        ////    {
        ////       // button1.Text = "Speak Now";
        ////        recognizer.SetInputToDefaultAudioDevice();
        ////        RecognitionResult result = recognizer.Recognize();
        ////       // button1.Text = result.Text;
        ////    }
        ////    catch (InvalidOperationException exception)
        ////    {
        ////        //button1.Text = String.Format("Could not recognize input from default aduio device. Is a microphone or sound card available?\r\n{0} - {1}.", exception.Source, exception.Message);
        ////    }
        ////    finally
        ////    {
        ////        recognizer.UnloadAllGrammars();
        ////    }
        ////}

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                if (Request.QueryString["Flag_Grp"] != null && Request.QueryString["IdGrpOrPerson"] != null)
                {
                    UserStudent = Request.QueryString["IdGrpOrPerson"];
                    flagGroup = bool.Parse(Request.QueryString["Flag_Grp"]);

                }
                else
                {
                    flagGroup = true;
                }
                userID_Ostad = OstadUserPrev + Session[sessionNames.userID_StudentOstad].ToString();
                RfrhDtLstContact();


                LblIdGrp.Text = "";
                TxtIdOnChat.Text = "";

                if (flagGroup == true)
                    LblIdGrp.Text = UserStudent;
                else
                    TxtIdOnChat.Text = UserStudent;

                LblIdUser.Text = userID_Ostad;
                RfrhDtLstMessage(flagGroup);
                if (flagGroup == true)
                {
                    BtnGrp.Visible = true;
                    BtnPesonal.Visible = false;
                }
                else if (flagGroup == false)
                {
                    BtnGrp.Visible = false;
                    BtnPesonal.Visible = true;
                }

            }
        }



        public void RfrhDtLstMessage(bool flagGroup)
        {

            DataTable dt;
            DtLstMesages.DataSource = null;
            DtLstMesages.DataBind();
            if (flagGroup == false && TxtIdOnChat.Text.Trim() != "")
            {
                dt = MessagePersonalBuisnes.GetMessagePersonal(userID_Ostad, UserStudent);
            }
            else
            {
                LblNameOnChat.Text = "گفتگو دفاع";
                dt = MesageGroupBuisnes.GetMessageGroup(userID_Ostad, UserStudent);
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
            if (userID_Ostad != null && userID_Ostad != "" && UserStudent != "")
            {
                DataTable dt = ContactBuisnes.GetContactStudentByOstad(UserStudent);
                dt = Functions.AddDefualt.GetDefualt(dt);
                DtlstContact.DataSource = dt;
            }
            DtlstContact.DataBind();

        }


        protected void BtnGrp_Click(object sender, EventArgs e)
        {
            LblNameOnChat.Text = "گفتگو دفاع";
            TxtIdOnChat.Text = "";
            LblIdGrp.Text = UserStudent;
            RfrhDtLstMessage(true);
            MessageJs.DeleteUnreadStudent(userID_Ostad, "true", "-1");
        }
        protected void BtnPesonal_Click(object sender, EventArgs e)
        {
            LblNameOnChat.Text = "گفتگو شخصی";
            TxtIdOnChat.Text = UserStudent;
            LblIdGrp.Text = "";
            RfrhDtLstMessage(false);
            MessageJs.DeleteUnreadStudent(userID_Ostad, "false", UserStudent);
        }



    }
}