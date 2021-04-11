using System.Data;
using IAUEC_Apps.DAO.ConatctDAO;

namespace IAUEC_Apps.Business.Conatct
{
    public class ContactBuisnes
    {
        #region Read
        static DataTable dt = new DataTable();
        public static DataTable GetConatctOstads(string stCode)
        {
            
           return ContactDAO.SelectConatctOstad(stCode);
            
         //  return Functions.RowDataTableOstad.GetDataTableOstad(dt);

        }
        public static DataTable GetConatctStudentForType(string stCode ,int IdType)
        {
          
            string stTypeOStad = Functions.FnTypeOstad.GetTypeOstad(IdType);
            return ContactDAO.SelectConatctStudentForType(stCode, stTypeOStad);
        
        }
        public static DataTable GetContactStudentALL(string stCode)
        {
            return ContactDAO.SelectConatctStudentAll(stCode);

        }
        public static DataTable GetStages(string stCode)
        {
            return ContactDAO.SelectStages(stCode);

        }
        public static string FullNameSt(string stCode)
        {
            DataTable dtSt = ContactBuisnes.GetContactAStudent(stCode);
            string fullNameSt = "";
            if (dtSt != null && dtSt.Rows.Count > 0)
                fullNameSt = dtSt.Rows[0]["FullName"].ToString();
            return fullNameSt;
        }
        public static DataTable GetContactAStudent(string stCode)
        {
            return ContactDAO.SelectContactAStudent(stCode);
        }
        public static DataTable GetContactStudentByOstad(string stCode)
        {
            
            DataTable dtst = ContactDAO.SelectContactAStudent(stCode);
            dt = ContactBuisnes.GetConatctOstads(stCode);

            return Functions.AddStudentByOstadDt.GetOstadAddStudent(dtst, dt);
        }
        #endregion

    }
}

