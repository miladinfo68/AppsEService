using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.Graduate;

namespace IAUEC_Apps.DAO.University.GraduateAffair
{
    public class VahedReshteDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        #region get

        public DataTable GetVahed()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Graduate].[SP_GetvahedInfo]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }

        

        public DataTable GetSemat()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Graduate].[SP_GetSematInfo]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }

        public DataTable GetReshteVahed()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[Graduate].[SP_GetReshteVahedInfo]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }

        public DataTable GetSematVahed()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[Graduate].[SP_GetSematVahedInfo]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }

        #endregion

        #region set

        public int setVahedReshte(VahedReshteDTO vrd)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "graduate.SP_setVahedReshte";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@voroodi", vrd.voroodi);
            cmd.Parameters.AddWithValue("@Vahed", vrd.Vahed);
            cmd.Parameters.AddWithValue("@reshte", vrd.reshte);
            cmd.Connection = conn;

            try
            {
                conn.Open();
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return id;
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return 0;
                throw;
            }

        }

        public int setSematVahed(VahedReshteDTO vrd)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "graduate.SP_setSematvahed";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@semat", vrd.semat);
            cmd.Parameters.AddWithValue("@Vahed", vrd.Vahed);
            cmd.Parameters.AddWithValue("@name", vrd.name);
            cmd.Connection = conn;

            try
            {
                conn.Open();
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return id;
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return 0;
                throw;
            }
        }

        public int setVahedInfo(VahedReshteDTO vrd)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "graduate.SP_setVahedInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vahed", vrd.VahedName);
            cmd.Connection = conn;

            try
            {
                conn.Open();
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return id;
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return 0;
                throw;
            }
        }

        public int setSematInfo(VahedReshteDTO vrd)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "graduate.SP_setSematInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@semat", vrd.sematName);
            cmd.Connection = conn;

            try
            {
                conn.Open();
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return id;
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return 0;
                throw;
            }
        }
        #endregion

        #region update

        public void updateVahedInfo(VahedReshteDTO vrd)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "graduate.SP_updateVahedInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", vrd.id);
            cmd.Parameters.AddWithValue("@Vahed", vrd.VahedName);
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }

        public void updateSematInfo(VahedReshteDTO vrd)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "graduate.SP_updateSematInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", vrd.id);
            cmd.Parameters.AddWithValue("@semat", vrd.sematName);
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }

        public void updateSematVahedInfo(VahedReshteDTO vrd)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "graduate.SP_updateSematVahedInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sematname", vrd.sematName);
            cmd.Parameters.AddWithValue("@Vahedname", vrd.VahedName);
            cmd.Parameters.AddWithValue("@name", vrd.name);
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }

        public void updateReshteVahedInfo(VahedReshteDTO vrd)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "graduate.SP_updateReshteVahedInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@reshte", vrd.reshte);
            cmd.Parameters.AddWithValue("@Vahed", vrd.Vahed);
            cmd.Parameters.AddWithValue("@voroodi", vrd.voroodi);
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }

        #endregion
    }
}
