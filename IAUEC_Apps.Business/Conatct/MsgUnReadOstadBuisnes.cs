using System.Data;
using IAUEC_Apps.DAO.ConatctDAO;
namespace IAUEC_Apps.Business.Conatct
{
  public  class MsgUnReadOstadBuisnes
    {
        #region Read
        public static DataTable GetUnReadMsgCountOstad(string idUser)
        {

            return MsgUnreadOstadDAO.SelectCountUnReadOstadDAO(idUser);
        }
        public static DataTable GetUnReadMsgOstad(string idUser)
        {

            return MsgUnreadOstadDAO.SelectConatctUnreadOstadDAO(idUser);
        }
        #endregion
        #region Delete
        public static DataTable DeleteUnReadMsgOstad(string idUser,int FlagGroup,int Sender=-1)
        {
             MsgUnreadOstadDAO.DeleteConatctUnreadOstadDAO(idUser, FlagGroup, Sender);
             return MsgUnReadOstadBuisnes.GetUnReadMsgCountOstad(idUser);
        }
        #endregion
    }
}
