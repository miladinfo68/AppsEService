using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DAO.ConatctDAO;
namespace IAUEC_Apps.Business.Conatct
{
    public class DeleteMsgBuisnes
    {
        public static bool CheckDeleteMsgPersonal(string idReciver, string idChat, string idUser)
        {
            
            DataTable dt = DeleteMessageDAO.SelectCheckforDeleteMsgPersonal(idReciver, idChat, idUser);
            if (dt == null || dt.Rows.Count == 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckDeleteMsgGroup(string idGrp, string idChat, string idUser)
        {
        
            DataTable dt = DeleteMessageDAO.SelectCheckforDeleteMsgGrp(idGrp, idChat, idUser);
            if (dt == null || dt.Rows.Count == 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool DeleteMsg(string chatID)
        {
           
            DataTable dt = DeleteMessageDAO.DeleteMsg(chatID);
            if(dt!=null&&dt.Rows.Count>0&&dt.Rows[0]["Accept"].ToString().Trim()=="1")
            {
                return true;
            }
            return false;
        }

    }
}
