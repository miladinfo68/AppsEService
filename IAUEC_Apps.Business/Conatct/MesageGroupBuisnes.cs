using System.Data;
using IAUEC_Apps.DAO.ConatctDAO;
using IAUEC_Apps.Business.Conatct.Functions;
using System;

namespace IAUEC_Apps.Business.Conatct
{
 public static class MesageGroupBuisnes
    {
        #region Read
        public static DataTable GetMessageGroup(string idUser,string idGrp,string idChat="-1")
        {
           
            DataTable dt = MessageGroupDAO.SelectMessageGroupDA(idUser, idGrp,idChat);
            //dt= Functions.AddIMageSesionSt.FnDtAddIMageSesionSt(dt, idUser);
            return dt;

        }
        public static DataTable GetNewMessageGroup(string idChat, string idGrp)
        {
           
            DataTable dt = MessageGroupDAO.SelectNewMessageGroupDA(idChat, idGrp);
            //dt= Functions.AddIMageSesionSt.FnDtAddIMageSesionSt(dt, idUser);
            return dt;

        }

        #endregion
        #region Insert
        public static DataTable EnterMessageGroup(string idSender, string message,string idGrp, bool flagReplayed, int idReplayed,int flagTypeFile = 1,string format="")

        {
            string persianDateChat = DatePersian.GetDateNow();
            DateTime dateChat = DateTime.Now;
            string TimeChat = DatePersian.GetTimeNow12();

            return MessageGroupDAO.InsertMessageGroupDAO(idSender, message,
                                                 dateChat, persianDateChat, TimeChat,idGrp,
                                                 (flagReplayed == false ? 0 : 1), idReplayed, flagTypeFile, format);
        }

        #endregion
    }
}
