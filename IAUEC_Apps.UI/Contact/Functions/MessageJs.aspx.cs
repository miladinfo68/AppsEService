using IAUEC_Apps.Business.Conatct;
using System;
using System.Data;
using Newtonsoft.Json;


namespace IAUEC_Apps.UI.Contact.Functions
{
    public partial class MessageJs : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region delete
        [System.Web.Services.WebMethod]
        public static bool DeleteChat(string senderId, string groupId, string reciverId, string chatId, string userId)
        {
            if (senderId == userId)
            {

                bool flagCheckDelete = CheckDeleteMsg(senderId, chatId, groupId, reciverId);
                if (flagCheckDelete == true)
                {
                    return DeleteMsgBuisnes.DeleteMsg(chatId);
                }
            }
            return false;
        }
        [System.Web.Services.WebMethod]
        public static int DeleteUnreadOstad(string userID, string FlagGrp, string Sender = "-1")
        {
            DataTable dt = null;

            if (userID != null && userID != "" && FlagGrp != "")
            {
                int FlagGrpB = bool.Parse(FlagGrp) == false ? 0 : 1;
                dt = MsgUnReadOstadBuisnes.DeleteUnReadMsgOstad(userID, FlagGrpB, int.Parse(Sender));
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0]["CountUnRead"].ToString());
            }
            else
                return 0;

        }
        [System.Web.Services.WebMethod]
        public static int DeleteUnreadStudent(string userID, string FlagGrp, string Sender = "-1")
        {
            DataTable dt = null;
            if (userID != null && userID != "" && FlagGrp != "")
            {
                int FlagGrpB = bool.Parse(FlagGrp) == false ? 0 : 1;
                dt = MsgUnReadStudentBuisnes.DeleteUnReadMsgStudent(userID, FlagGrpB, int.Parse(Sender == "" ? "-1" : Sender));
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0]["CountUnRead"].ToString());
            }
            else
                return 0;
        }
        #endregion
        #region insert
        [System.Web.Services.WebMethod]
        public static string InsertMessage(string userID, string idOnChat, string idGrp, 
                                             string msg, int idMsgReplayed, int flagTypeFile = 1, string format = "")
        {

            bool flagReplayed = false;
            if (idMsgReplayed != 0)
            {
                flagReplayed = true;
            }
            if (msg != "")
            {
                DataTable dt;
                if (idOnChat != "")
                {
                    dt = MessagePersonalBuisnes.EnterMessagePersonal(userID, idOnChat, msg, flagReplayed, 
                                                                      idMsgReplayed, flagTypeFile, format);
                }
                else
                {

                    dt = MesageGroupBuisnes.EnterMessageGroup(userID, msg, idGrp, flagReplayed,
                                                                idMsgReplayed, flagTypeFile, format);
                    
                    if(idGrp.Trim()==userID.Trim())//detect page student
                    {
                        SendSmsContactBuisnes.InsertSendSms(idGrp.Trim());                  
                    }

                }
                dt = Functions.AddDefualt.GetDefualt(dt);
               
                string JSONString = JsonConvert.SerializeObject(dt);
                return JSONString;

            }


            return null;
        }
        #endregion
        #region check
        [System.Web.Services.WebMethod]
        public static bool CheckDeleteMsg(string senderId, string chatId, string GroupId, string ReciverId)
        {
            bool flagCheckDelete = false;

            if (GroupId.Trim() != "")
            {
                flagCheckDelete = DeleteMsgBuisnes.CheckDeleteMsgGroup(GroupId, chatId, senderId);
            }
            else if (ReciverId != "")
            {
                flagCheckDelete = DeleteMsgBuisnes.CheckDeleteMsgPersonal(ReciverId, chatId, senderId);
            }

            return flagCheckDelete;
        }
        #endregion 


    }
}



