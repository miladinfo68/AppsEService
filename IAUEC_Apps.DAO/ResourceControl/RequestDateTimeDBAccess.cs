using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ResourceControl.DAL
{
    public class RequestDateTimeDBAccess
    {
        public int InsertNewDateTime(RequestDateTime dateTime)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@Date", dateTime.Date),
                new SqlParameter("@StartTime", dateTime.StartTime),
                new SqlParameter("@EndTime", dateTime.EndTime),
                new SqlParameter("@RequestId", dateTime.RequestId),
                new SqlParameter("@ResourceId", dateTime.ResourceId),
            };
            return SqlDBHelper.ExecuteScalar("[Resource_Control].[sp_InsertRequestDateTime]", CommandType.StoredProcedure, parameters); ;
        }

        public void InsertListOfDateTime(List<RequestDateTime> dateTimeList)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@Date", SqlDbType.NVarChar , 10 , "Date"),
                new SqlParameter("@StartTime",SqlDbType.BigInt , 1 , "StartTime" ),
                new SqlParameter("@EndTime", SqlDbType.BigInt , 1 ,"EndTime"),
                new SqlParameter("@RequestId", SqlDbType.Int, 1 ,"RequestId"),
                new SqlParameter("@ResourceId", SqlDbType.Int,1,"ResourceId"),
                new SqlParameter("@MayConflict",SqlDbType.Bit, 1 , "MayConflict"),
            };

            SqlDBHelper.ExecuteNonQueryList("[Resource_Control].[sp_InsertRequestDateTime]", CommandType.StoredProcedure, parameters, dateTimeList);

        }

        public int InsertNewDateTimeForDefenceV2(RequestDateTime dateTime, int collegeId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Date", dateTime.Date),
                new SqlParameter("@StartTime", dateTime.StartTime),
                new SqlParameter("@EndTime", dateTime.EndTime),
                new SqlParameter("@RequestId", dateTime.RequestId),
                new SqlParameter("@collegeId", collegeId),
            };
            return SqlDBHelper.ExecuteScalar("[Resource_Control].[sp_InsertRequestDateTimeForDefenceAddRequest]", CommandType.StoredProcedure, parameters); ;
        }


        public List<RequestDateTime> GetDateTimeListByRequestId(int requestId)
        {
            List<RequestDateTime> dateTimelist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@requestId", requestId),
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetDateTimeListByRequestId]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    dateTimelist = new List<RequestDateTime>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestDateTime dateTime = new RequestDateTime();
                        dateTime.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                        dateTime.Date = row["Date"].ToString();
                        dateTime.StartTime = Convert.ToInt64(row["StartTime"]);
                        dateTime.EndTime = Convert.ToInt64(row["EndTime"]);
                        dateTime.RequestId = Convert.ToInt32(row["RequestId"]);
                        if (row["ResourceId"] != DBNull.Value)
                            dateTime.ResourceId = Convert.ToInt32(row["ResourceId"] ?? 0);
                        dateTime.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                        dateTime.ClassName = row["ClassName"].ToString();

                        dateTimelist.Add(dateTime);
                    }
                }
            }

            return dateTimelist;

        }


        public List<RequestDateTime> GetDateTimeListByRequestIdForStudent(int requestId)
        {
            List<RequestDateTime> dateTimelist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@requestId", requestId),
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetDateTimeListByRequestIdForStudent]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    dateTimelist = new List<RequestDateTime>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestDateTime dateTime = new RequestDateTime();
                        dateTime.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                        dateTime.Date = row["Date"].ToString();
                        dateTime.StartTime = Convert.ToInt64(row["StartTime"]);
                        dateTime.EndTime = Convert.ToInt64(row["EndTime"]);
                        dateTime.RequestId = Convert.ToInt32(row["RequestId"]);
                        dateTime.ResourceId = Convert.ToInt32(row["ResourceId"] ?? 0);
                        dateTime.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                        dateTime.ClassName = row["ClassName"].ToString();

                        dateTimelist.Add(dateTime);
                    }
                }
            }

            return dateTimelist;

        }


        public List<RequestDateTime> CheckDateTimeListWithResourceId(int requestId, int ResourceId)
        {
            List<RequestDateTime> dateTimeIdlist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@requestId", requestId),
                new SqlParameter("@ResourceId", ResourceId),

            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_CheckDateTimeListWithResourceId]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    dateTimeIdlist = new List<RequestDateTime>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestDateTime dateTime = new RequestDateTime();
                        dateTime.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                        dateTime.Date = row["Date"].ToString();
                        dateTime.StartTime = Convert.ToInt64(row["StartTime"]);
                        dateTime.EndTime = Convert.ToInt64(row["EndTime"]);
                        dateTime.RequestId = Convert.ToInt32(row["RequestId"]);
                        dateTime.ResourceId = Convert.ToInt32(row["ResourceId"] ?? 0);
                        dateTime.MayConflict = Convert.ToBoolean(row["MayConflict"]);

                        dateTimeIdlist.Add(dateTime);
                    }
                }
            }

            return dateTimeIdlist;
        }

        public int CheckOneDateTimeWithResourceId(int dateTimeId, int ResourceId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@dateTimeId", dateTimeId),
                new SqlParameter("@ResourceId", ResourceId),
            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[SP_CheckOneDateTimeWithResourceId]", CommandType.StoredProcedure, parameters);
        }

        public int UpdateOneDateTimeRequest(RequestDateTime reqDateTime)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@dateTimeId", reqDateTime.DateTimeId),
                new SqlParameter("@Date", reqDateTime.Date),
                new SqlParameter("@StartTime", reqDateTime.StartTime),
                new SqlParameter("@EndTime", reqDateTime.EndTime),
                new SqlParameter("@ResourceId", reqDateTime.ResourceId)
            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[SP_UpdateOneDateTimeRequest]", CommandType.StoredProcedure, parameters);
       
        }

        public int UpdateOneDateTimeRequestForDefence(RequestDateTime reqDateTime)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@dateTimeId", reqDateTime.DateTimeId),
                new SqlParameter("@Date", reqDateTime.Date),
                new SqlParameter("@StartTime", reqDateTime.StartTime),
                new SqlParameter("@EndTime", reqDateTime.EndTime),
                new SqlParameter("@ResourceId", reqDateTime.ResourceId),
                new SqlParameter("@ReqId", reqDateTime.RequestId)
            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[SP_UpdateOneDateTimeRequestForDefence]", CommandType.StoredProcedure, parameters);
       
        }

        public int UpdateListOfDateTime(List<RequestDateTime> dateTimeList)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@DateTimeId", SqlDbType.NVarChar , 10 , "DateTimeId"),
                new SqlParameter("@Date", SqlDbType.NVarChar , 10 , "Date"),
                new SqlParameter("@StartTime",SqlDbType.BigInt , 1 , "StartTime" ),
                new SqlParameter("@EndTime", SqlDbType.BigInt , 1 ,"EndTime"),
                new SqlParameter("@RequestId", SqlDbType.Int, 1 ,"RequestId"),
                new SqlParameter("@ResourceId", SqlDbType.Int,1,"ResourceId"),
                new SqlParameter("@MayConflict",SqlDbType.Bit, 1 , "MayConflict"),
            };

           return SqlDBHelper.ExecuteNonQueryList("[Resource_Control].[SP_UpdateRequestDateTime]", CommandType.StoredProcedure, parameters, dateTimeList);

        }

        public int UpdateListOfDateTimeForDefence(List<RequestDateTime> dateTimeList)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DateTimeId", SqlDbType.NVarChar , 10 , "DateTimeId"),
                new SqlParameter("@Date", SqlDbType.NVarChar , 10 , "Date"),
                new SqlParameter("@StartTime",SqlDbType.BigInt , 1 , "StartTime" ),
                new SqlParameter("@EndTime", SqlDbType.BigInt , 1 ,"EndTime"),
                new SqlParameter("@RequestId", SqlDbType.Int, 1 ,"RequestId"),
                new SqlParameter("@ResourceId", SqlDbType.Int,1,"ResourceId"),
                new SqlParameter("@MayConflict",SqlDbType.Bit, 1 , "MayConflict"),

            };

            return SqlDBHelper.ExecuteNonQueryList("[Resource_Control].[SP_UpdateRequestDateTimeForDefence]", CommandType.StoredProcedure, parameters, dateTimeList);

        }


        public int CheckOneDateTimeWithResourceIdPlus(int dateTimeId, int resourceId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@dateTimeId", dateTimeId),
                new SqlParameter("@ResourceId", resourceId),
            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[SP_CheckOneDateTimeWithResourceIdPlus]", CommandType.StoredProcedure, parameters);
        }
        public DataTable CheckOneDateTimeWithResourceIdPlusForStudent(int dateTimeId, int resourceId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@dateTimeId", dateTimeId),
                new SqlParameter("@ResourceId", resourceId),
            };

            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[CheckOneDateTimeWithResourceIdPlusForStudent]", CommandType.StoredProcedure, parameters);
        }

    }
}
