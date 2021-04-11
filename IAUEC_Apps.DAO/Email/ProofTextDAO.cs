using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.Email
{
    public class ProofTextDAO
    {
        
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        #region create
        public void CreateProofTextByProofText(string ProofText)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Email.SP_Email_CreateProofText", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProofText", SqlDbType.VarChar);
            cmd.Parameters["@ProofText"].Value = ProofText;
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
        }
        #endregion
        #region read
        public DataTable GiveAllProofText()
        {
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("Email.SP_Email_GiveAllProofText", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }
        #endregion
    }
}
