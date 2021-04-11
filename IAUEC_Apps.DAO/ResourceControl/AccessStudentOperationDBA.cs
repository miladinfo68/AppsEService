using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.ResourceControlClasses;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IAUEC_Apps.DAO.ResourceControl
{
  public  class AccessStudentOperationDBA
    {
        public List<AccessStudentOperationModel> Select(AccessStudentOperationModel access=null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    List<AccessStudentOperationModel> model = new List<AccessStudentOperationModel>();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_GetAccessStudentOperation]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", access!=null? access.StudentCode:"-1"));
                    cmd.Parameters.Add(new SqlParameter("@term", access != null ? access.Term:"-1"));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        model.Add(new AccessStudentOperationModel
                        {
                            id = int.Parse(dt.Rows[i]["id"].ToString()),
                            StudentCode = dt.Rows[i]["StudentCode"].ToString(),
                            StudentName = dt.Rows[i]["StudentName"].ToString(),
                            Term = dt.Rows[i]["Term"].ToString(),
                            FlagAllowFinancial = bool.Parse(dt.Rows[i]["FlagAllowFinancial"].ToString()),
                            FlagAllowSelectUnit = bool.Parse(dt.Rows[i]["FlagAllowSelectUnit"].ToString())
                        });



                    }
                    return model;

                }
                
            }
            catch
            {
                return new List<AccessStudentOperationModel>();
            }

        }

        public bool Update(AccessStudentOperationModel access = null)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_UpdateAccessStudentOperation]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", access.StudentCode));
                    cmd.Parameters.Add(new SqlParameter("@flagAllowFinancial", access.FlagAllowFinancial));
                    cmd.Parameters.Add(new SqlParameter("@flagAllowSelectUnit", access.FlagAllowSelectUnit));
                    cmd.Parameters.Add(new SqlParameter("@term", access.Term));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return bool.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch
            {
                return false;
            }

        }
        public int Insert(AccessStudentOperationModel model)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_InsertAccessStudentOperation]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@studentCode", model.StudentCode));
                    cmd.Parameters.Add(new SqlParameter("@studentName", model.StudentName));

                    cmd.Parameters.Add(new SqlParameter("@term", model.Term));

                    cmd.Parameters.Add(new SqlParameter("@flagAllowSelectUnit", model.FlagAllowSelectUnit));

                    cmd.Parameters.Add(new SqlParameter("@flagAllowFinancial", model.FlagAllowFinancial));


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return int.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch
            {
                return -1;
            }

        }
    }
}
