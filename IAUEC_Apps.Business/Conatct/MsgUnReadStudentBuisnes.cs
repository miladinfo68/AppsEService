using System.Data;
using IAUEC_Apps.DAO.ConatctDAO;
namespace IAUEC_Apps.Business.Conatct
{
  public  class MsgUnReadStudentBuisnes
    {
        #region Read
        public static DataTable GetUnReadMsgCountStudent(string idUser)
        {

            return MsgUnReadStudentDAO.SelectCountUnReadStudentDAO(idUser);
        }
        public static DataTable GetUnReadMsgStudent(string idUser)
        {

            return MsgUnReadStudentDAO.SelectConatctUnreadStudentDAO(idUser);
        }
        #endregion
        #region Delete
        public static DataTable DeleteUnReadMsgStudent(string idUser,int FlagGroup,int sender=-1)
        {
             MsgUnReadStudentDAO.DeleteConatctUnreadStudentDAO(idUser, FlagGroup, sender );
             return MsgUnReadStudentBuisnes.GetUnReadMsgCountStudent(idUser);
        }
        #endregion
    }
}
