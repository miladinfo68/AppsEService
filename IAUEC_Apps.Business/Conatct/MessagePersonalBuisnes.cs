using System.Data;
using IAUEC_Apps.DAO.ConatctDAO;
using MD.PersianDateTime;
using System;
using System.Collections.Generic;

namespace IAUEC_Apps.Business.Conatct
{
 public   class MessagePersonalBuisnes
    {
        #region Read
        public static DataTable GetMessagePersonal(string idUser, string idContact,string idChat="-1")
        {
            
            DataTable dt = MessagePersonalDAO.SelectMessagePersonalDA(idUser, idContact, idChat);
           // dt= Functions.AddIMageSesionSt.FnDtAddIMageSesionSt(dt, idUser);
            return dt;

        }

        public static DataTable GetNewMessagePersonal(string idChat, string idSender,string idReciver)
        {
         
            DataTable dt = MessagePersonalDAO.SelectNewMessagePersonalDA(idChat, idSender, idReciver);
            //dt= Functions.AddIMageSesionSt.FnDtAddIMageSesionSt(dt, idUser);
            return dt;

        }

        #endregion
        #region Insert
        public static DataTable EnterMessagePersonal(string idSender, string IdReciver, string message, bool flagReplayed, int idReplayed,int flagTypeFile = 1, string format = "")

        {
            string persianDateChat = PersianDateTime.Now.ToShortDateString();
            DateTime dateChat = DateTime.Now;
            string TimeChat = PersianDateTime.Now.ToShortTimeString();
            if (dateChat.Hour >= 12)
            {
                TimeChat = TimeChat.Replace('ق', 'ب');
            }
            return MessagePersonalDAO.InsertMessagePersonalDAO(idSender, IdReciver, message,
                                                        dateChat, persianDateChat, TimeChat,
                                                        (flagReplayed == false ? 0 : 1) ,idReplayed, flagTypeFile,format);

        }

        #endregion
    }
}
