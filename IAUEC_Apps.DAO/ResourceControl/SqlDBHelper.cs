using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using IAUEC_Apps.DAC.Connections;
using System.Reflection;
using System.Collections;

namespace ResourceControl.DAL
{
    class SqlDBHelper
    {
        static string CONNECTION_STRING = new IAUEC_Apps.DAC.Connections.SuppConnection().Supp_con;
        //ConfigurationManager.ConnectionStrings["Resource_ControlConnectionString"].ConnectionString;
        //SqlConnection CONNECTION_STRING = new SqlConnection(new SuppConnection().Supp_con);  
        internal static DataTable ExecuteSelectCommand(string CommandName, CommandType cmdType)
        {
            DataTable table = null;
            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            table = new DataTable();
                            da.Fill(table);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            return table;
        }

        internal static DataTable ExecuteParamerizedSelectCommand(string CommandName, CommandType cmdType, SqlParameter[] param)
        {
            DataTable table = new DataTable();

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(param);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);
                        }
                    }
                    catch (Exception x)
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Close();
                            cmd.Dispose();
                        }
                        throw x;
                    }
                }
            }

            return table;
        }

        internal static bool ExecuteNonQuery(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception x)
                    {
                        throw x;
                    }
                }
            }

            return (result > 0);
        }

        internal static int ExecuteNonQueryList<T>(string CommandName, CommandType cmdType, SqlParameter[] parameters, List<T> List)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {

                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(parameters);

                    con.Open();


                    foreach (var item in List)
                    {
                        var Properties = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic
                                                                    | BindingFlags.Static | BindingFlags.Instance
                                                                    | BindingFlags.FlattenHierarchy);

                        for (int i = 0; i < parameters.Count(); i++)
                        {
                            cmd.Parameters[i].Value = item.GetType().GetProperty(cmd.Parameters[i].SourceColumn).GetValue(item, null);
                        }

                        result = cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }
            return result;
        }

        internal static int ExecuteScalar(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        result = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception x)
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                        throw x;
                    }
                }
            }

            return result;
        }

        internal static decimal ExecuteScalarValue(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            decimal result = 0;

            using (SqlConnection con = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        var scalarValue = cmd.ExecuteScalar()?.ToString();
                        result = string.IsNullOrEmpty(scalarValue) ? 0 : decimal.Parse(scalarValue);
                    }
                    catch (Exception x)
                    {
                        if (con.State == ConnectionState.Open) con.Close();
                        throw x;
                    }
                }
            }

            return result;
        }
    }
}

