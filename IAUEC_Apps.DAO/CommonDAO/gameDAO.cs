using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DAC.Connections;
using System.Data.SqlClient;

namespace IAUEC_Apps.DAO.CommonDAO
{
    public class gameDAO
    {
        SqlConnection SuppConnection = new SqlConnection(new SuppConnection().Supp_con);

        public DataTable getActiveGames()
        {
            SqlCommand cmd = new SqlCommand("", SuppConnection);
            cmd.CommandText = "game.sp_getAllActiveGames";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (SuppConnection.State == ConnectionState.Closed)
                    SuppConnection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                SuppConnection.Close();
            }
            catch (Exception ex)
            {
                if (SuppConnection.State == ConnectionState.Open)
                    SuppConnection.Close();
            }
            return dt;
        }
        public void setPlayLog(int gameID, string playerID, int playerType)
        {
            SqlCommand cmd = new SqlCommand("", SuppConnection);
            cmd.CommandText = "game.sp_setPlayLog";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@playerID", playerID);
            cmd.Parameters.AddWithValue("@playerType", playerType);
            cmd.Parameters.AddWithValue("@gameID", gameID);
            try
            {
                if (SuppConnection.State == ConnectionState.Closed)
                    SuppConnection.Open();
                cmd.ExecuteNonQuery();
                SuppConnection.Close();
            }
            catch (Exception ex)
            {
                if (SuppConnection.State == ConnectionState.Open)
                    SuppConnection.Close();
            }
        }
    }
}
