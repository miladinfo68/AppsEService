using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ResourceControl.Entity;
using System.ComponentModel;
using System.Globalization;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.ResourceControlClasses;


namespace ResourceControl.DAL
{
    public class RequestDBAccess

    {

        public int AddNewRequest(RequestFR request)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@subject", request.Subject),
                new SqlParameter("@note", request.Note),
                new SqlParameter("@answernote", request.Answernote),
                new SqlParameter("@answer_time", request.Answer_time),
                new SqlParameter("@issue_time", request.Issue_time),
                new SqlParameter("@status", request.Status),
                new SqlParameter("@issuerID", request.IssuerID),
                new SqlParameter("@send_time", request.Send_time),
                new SqlParameter("@catID",request.CatID ),
                new SqlParameter("@senderID",request.SenderID ),
                new SqlParameter("@replierID",request.ReplierID ),
                new SqlParameter("@issuerName",request.IssuerName ),
                new SqlParameter("@location",request.Location ),
                new SqlParameter("@capacity",request.Capacity ),
                new SqlParameter("@courseName",request.CourseName ),
                new SqlParameter("@courseID", request.CourseDID),
                new SqlParameter("@daneshID",request.DaneshID)

            };
            return SqlDBHelper.ExecuteScalar("[Resource_Control].[sp_RequestInsert]", CommandType.StoredProcedure, parameters);
        }
        public string GetCurrentTerm()
        {

            var dt = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[GetCurrentTermForDefence]", CommandType.StoredProcedure);


            return dt.Rows[0][0] as string;
        }
        public Dictionary<string, string> GeDateRangeOfTerm(int appId, string currentTerm)
        {
            if (currentTerm == null) throw new ArgumentNullException(nameof(currentTerm));
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@appId",appId),
                new SqlParameter("@currentTerm",currentTerm)
            };
            var dt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[GeDateRangeOfTermForDefence]", CommandType.StoredProcedure, parameters);

            var resualt = new Dictionary<string, string> { { "start", dt.Rows[0][0] as string }, { "end", dt.Rows[0][1] as string } };

            return resualt;
        }
        public Dictionary<string, string> GeDateRangeOfTerm2(int appId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@appId",appId),
            };

            var dt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[GeDateRangeOfTermForDefence2]", CommandType.StoredProcedure, parameters);

            var resualt = new Dictionary<string, string> { { "start", dt.Rows[0][0] as string }, { "end", dt.Rows[0][1] as string } };

            return resualt;
        }


        public int AddNewDefenceRequest(RequestFR request)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@subject", request.Subject),
                new SqlParameter("@note", request.Note),
                new SqlParameter("@answernote", request.Answernote),
                new SqlParameter("@answer_time", request.Answer_time),
                new SqlParameter("@issue_time", request.Issue_time),
                new SqlParameter("@status", request.Status),
                new SqlParameter("@issuerID", request.IssuerID),
                new SqlParameter("@send_time", request.Send_time),
                new SqlParameter("@catID",request.CatID ),
                new SqlParameter("@senderID",request.SenderID ),
                new SqlParameter("@replierID",request.ReplierID ),
                new SqlParameter("@issuerName",request.IssuerName ),
                new SqlParameter("@location",request.Location ),
                new SqlParameter("@capacity",request.Capacity ),
                new SqlParameter("@courseName",request.CourseName ),
                new SqlParameter("@courseID", request.CourseDID),
                new SqlParameter("@daneshID",request.DaneshID)

            };
            return SqlDBHelper.ExecuteScalar("[Resource_Control].[sp_RequestInsertDefence]", CommandType.StoredProcedure, parameters); ;
        }



        public DataTable GetAllRequestByTypeAndDate(int type, string sDate, string eDate)
        {
            SqlConnection connection = new SqlConnection(new SuppConnection().Supp_con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "[Resource_Control].[sp_AllRequestByTypeAndDate]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sDate", sDate);
            cmd.Parameters.AddWithValue("@eDate", eDate);
            cmd.Parameters.AddWithValue("@type", type);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable GetRequestByUserIDandType(int userID, int type)
        {
            SqlConnection connection = new SqlConnection(new SuppConnection().Supp_con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "[Resource_Control].[sp_RequestSelectListByIssuerIDandType]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@issuerID", userID);
            cmd.Parameters.AddWithValue("@type", type);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public int UpdateRequest(RequestFR request)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(RequestDateTime));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (var item in request.DateTimeRange)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            SqlParameter param = new SqlParameter("@dateTimeList", table);
            param.SqlDbType = SqlDbType.Structured;
            SqlParameter[] parameters = new SqlParameter[]
            {
                param,
                new SqlParameter("@ID",request.ID),
                new SqlParameter("@subject", request.Subject),
                new SqlParameter("@note", request.Note),
                new SqlParameter("@answernote", request.Answernote),
                new SqlParameter("@answer_time", request.Answer_time),
                new SqlParameter("@issue_time", request.Issue_time),
                new SqlParameter("@status", request.Status),
                new SqlParameter("@issuerID", request.IssuerID),
                new SqlParameter("@send_time", request.Send_time),
                new SqlParameter("@catID",request.CatID ),
                new SqlParameter("@senderID",request.SenderID ),
                new SqlParameter("@replierID",request.ReplierID ),
                new SqlParameter("@isdeleted", request.IsDeleted),
                new SqlParameter("@issuername",request.IssuerName ),
                new SqlParameter("@location",request.Location ),
                new SqlParameter("@capacity",request.Capacity ),
                new SqlParameter("@courseName",request.CourseName ),
                new SqlParameter("@courseID", request.CourseDID),
                new SqlParameter("@daneshID",request.DaneshID)
            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[sp_RequestUpdate]", CommandType.StoredProcedure, parameters);
        }

        public int UpdateStudentRequestDB(StudentDefenceRequest request)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(RequestDateTime));
            DataTable table = new DataTable();


            SqlParameter[] parameters = new SqlParameter[]
            {

                new SqlParameter("@ID",request.Id),
                new SqlParameter("@RequestStartTime",request.RequestStartTime),
                new SqlParameter("@RequestEndTime",request.RequestEndTime),
                new SqlParameter("@RequestDate",request.RequestDate),


            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[RequestStudentUpdate]", CommandType.StoredProcedure, parameters);
        }
        public int UpdateStudentRequestDBV2(StudentDefenceRequest request)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RequestId",request.Id),
                new SqlParameter("@startTime",request.RequestStartTime),
                new SqlParameter("@endTime",request.RequestEndTime),
                new SqlParameter("@Date",request.RequestDate),
                new SqlParameter("@collegeId",request.DaneshId)
            };

            var dt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_InsertRequestDateTimeForDefenceEditRequest]", CommandType.StoredProcedure, parameters);
            return dt.Rows[0][0].ToString() == "ok" ? request.Id : Convert.ToInt32(dt.Rows[0][0]);

        }

        public int UpdateStudentRequestForEducation(StudentDefenceRequest request)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(RequestDateTime));
            DataTable table = new DataTable();


            SqlParameter[] parameters = new SqlParameter[]
            {

                new SqlParameter("@ID",request.Id),
                new SqlParameter("@RequestStartTime",request.RequestStartTime),
                new SqlParameter("@RequestEndTime",request.RequestEndTime),
                new SqlParameter("@RequestDate",request.RequestDate),
                new SqlParameter("@Status",request.Status),


            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[UpdateStudentRequestForEducation]", CommandType.StoredProcedure, parameters);
        }
        public bool IsEditedStudentRequest(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID",id)
            };

            bool result = Convert.ToBoolean(SqlDBHelper.ExecuteScalar("[Resource_Control].[IsEditedStudentRequest]", CommandType.StoredProcedure, parameters));
            return result;
        }
        public bool DeleteStudentRequest(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID",id)
            };

            bool result = Convert.ToBoolean(SqlDBHelper.ExecuteScalar("[Resource_Control].[DeleteStudentRequest]", CommandType.StoredProcedure, parameters));
            return result;
        }


        public bool DeleteRequest(int reqID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", reqID)
            };

            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_RequestDelete]", CommandType.StoredProcedure, parameters);
        }

        public RequestFR GetRequestDetails(int reqID)
        {
            RequestFR request = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", reqID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelect]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count >= 1)
                {
                    DataRow row = table.Rows[0];


                    request = new RequestFR();

                    request.ID = Convert.ToInt32(row["ID"]);
                    request.Subject = row["subject"] as string;
                    request.Note = row["note"] as string;
                    request.Answernote = row["answernote"] as string;
                    request.Answer_time = row["answer_time"] as string;
                    request.Issue_time = row["issue_time"] as string;
                    request.Status = Convert.ToInt32(row["status"]);
                    request.IssuerID = Convert.ToInt32(row["issuerID"]);
                    request.Send_time = row["send_time"] as string;
                    request.CatID = Convert.ToInt32(row["catID"]);
                    request.SenderID = Convert.ToInt32(row["senderID"]);
                    request.ReplierID = Convert.ToInt32(row["replierID"]);
                    request.IssuerName = row["issuerName"] as string;
                    request.Location = row["location_Name"] as string;
                    request.Capacity = Convert.ToInt32(row["capacity"]);
                    request.CourseName = row["courseName"] as string;
                    request.CourseDID = Convert.ToInt32(row["courseID"]);
                    request.DaneshID = Convert.ToInt32(row["daneshID"]);
                    request.Namedanesh = row["namedanesh"] as string;
                    request.Nameresh = row["nameresh"] as string;
                    request.DateTimeRange = new List<RequestDateTime>();
                    request.ResourceName = row["classname"] as string;
                    if (row["ResourceId"] != DBNull.Value)
                        request.ResourceId = Convert.ToInt32(row["ResourceId"]);

                    foreach (DataRow item in table.Rows)
                    {
                        RequestDateTime rdt = new RequestDateTime();
                        rdt.Date = item["Date"].ToString();
                        rdt.ClassName = item["ClassName"].ToString();
                        rdt.StartTime = Convert.ToInt64(item["startTime"]);
                        rdt.EndTime = Convert.ToInt64(item["endTime"]);
                        rdt.DateTimeId = Convert.ToInt32(item["DateTimeId"]);
                        rdt.MayConflict = Convert.ToBoolean(item["MayConflict"]);

                        if (row["ResourceId"] != DBNull.Value)
                            rdt.ResourceId = Convert.ToInt32(item["resourceId"] ?? 0);

                        rdt.RequestId = Convert.ToInt32(item["RequestId"]);
                        request.DateTimeRange.Add(rdt);
                    }
                }
            }

            return request;
        }


        public DataTable GetResourceStateForDefence(int reqID)
        {
            DataTable request = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@requestId", reqID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_ResourceStateForDefence]", CommandType.StoredProcedure, parameters))
            {
                request = table.Copy();
            }

            return request;
        }


        public List<RequestFR> GetRequestListOfTermJari()
        {
            List<RequestFR> requestlist = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[sp_RequestTermJariSelectAll]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }







        public List<RequestFR> GetOstadListinTerm()
        {
            List<RequestFR> requestlist = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[SP_GetOstadListinTerm]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        //  request.IssuerID = Convert.ToInt32(row["code_ostad"]);
                        request.IssuerName = row["IssuerName"] as string;
                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }

        public List<RequestFR> GetDeletedRequestListOfTermJari()
        {
            List<RequestFR> requestlist = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[sp_DeletedRequestTermJariSelectAll]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }

        public List<RequestFR> GetRequestListByIssuerID(int issuerID)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@issuerID",issuerID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelectListByIssuerID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        bool results = requestlist.Any(i => i.ID == (int)row["ID"]);
                        if (!results)
                        {
                            RequestFR request = new RequestFR();
                            request.ID = Convert.ToInt32(row["ID"]);
                            request.Subject = row["subject"] as string;
                            request.Note = row["note"] as string;
                            request.Answernote = row["answernote"] as string;
                            request.Answer_time = row["answer_time"] as string;
                            request.Issue_time = row["issue_time"] as string;
                            request.Status = Convert.ToInt32(row["status"]);
                            request.IssuerID = Convert.ToInt32(row["issuerID"]);
                            request.Send_time = row["send_time"] as string;
                            request.CatID = Convert.ToInt32(row["catID"]);
                            request.SenderID = Convert.ToInt32(row["senderID"]);
                            request.ReplierID = Convert.ToInt32(row["replierID"]);
                            request.IssuerName = row["issuerName"] as string;
                            request.Location = row["location_name"] as string;
                            request.Capacity = Convert.ToInt32(row["capacity"]);
                            request.CourseName = row["courseName"] as string;
                            request.CourseDID = Convert.ToInt32(row["courseID"]);
                            request.DaneshID = Convert.ToInt32(row["daneshID"]);
                            RequestDateTime rdt = new RequestDateTime();
                            rdt.Date = row["Date"].ToString();
                            rdt.ClassName = row["ClassName"].ToString();
                            rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            request.DateTimeRange = new List<RequestDateTime>();
                            request.DateTimeRange.Add(rdt);
                            requestlist.Add(request);
                        }
                        else
                        {
                            RequestDateTime rdt = new RequestDateTime();
                            rdt.Date = row["Date"].ToString();
                            rdt.ClassName = row["ClassName"].ToString();
                            rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            var req = requestlist.Where(i => i.ID == (int)row["ID"]).First();
                            req.DateTimeRange.Add(rdt);
                        }
                    }
                }
            }

            return requestlist;
        }

        public List<RequestFR> GetRequestListByIssuerIDAndStatus(int issuerID, int Status)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@issuerID",issuerID),
                new SqlParameter("@status",Status)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelectListByIssuerIDAndStatus]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        bool results = requestlist.Any(i => i.ID == (int)row["ID"]);
                        if (!results)
                        {
                            RequestFR request = new RequestFR();
                            request.ID = Convert.ToInt32(row["ID"]);
                            request.Subject = row["subject"] as string;
                            request.Note = row["note"] as string;
                            request.Answernote = row["answernote"] as string;
                            request.Answer_time = row["answer_time"] as string;
                            request.Issue_time = row["issue_time"] as string;
                            request.Status = Convert.ToInt32(row["status"]);
                            request.IssuerID = Convert.ToInt32(row["issuerID"]);
                            request.Send_time = row["send_time"] as string;
                            request.CatID = Convert.ToInt32(row["catID"]);
                            request.SenderID = Convert.ToInt32(row["senderID"]);
                            request.ReplierID = Convert.ToInt32(row["replierID"]);
                            request.IssuerName = row["issuerName"] as string;
                            request.Location = row["location_name"] as string;
                            request.Capacity = Convert.ToInt32(row["capacity"]);
                            request.CourseName = row["courseName"] as string;
                            request.CourseDID = Convert.ToInt32(row["courseID"]);
                            request.DaneshID = Convert.ToInt32(row["daneshID"]);
                            RequestDateTime rdt = new RequestDateTime();
                            rdt.Date = row["Date"].ToString();
                            rdt.ClassName = row["ClassName"].ToString();
                            rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            request.DateTimeRange = new List<RequestDateTime>();
                            request.DateTimeRange.Add(rdt);
                            requestlist.Add(request);
                        }
                        else
                        {
                            RequestDateTime rdt = new RequestDateTime();
                            rdt.Date = row["Date"].ToString();
                            rdt.ClassName = row["ClassName"].ToString();
                            rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            var req = requestlist.Where(i => i.ID == (int)row["ID"]).First();
                            req.DateTimeRange.Add(rdt);
                        }
                    }
                }
            }

            return requestlist;
        }


        public List<RequestFR> GetRequestListBystatusForAdmin(int status)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status",status)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelectListByStatusForAdmin]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }


        public List<RequestFR> GetRequestListBystatus(int status)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status",status)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelectListByStatus]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location_name"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }

        public List<RequestFR> GetStudentRequestListForStudentOffice(int status)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status",status)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[GetStudentRequestListForStudentOffice]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location_name"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);
                        request.Nameresh = row["nameresh"].ToString();
                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }


        public List<StudentDefenceRequestDTO> GetStudentRequestListForTechnical()
        {
            List<StudentDefenceRequestDTO> requestlist = null;



            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[GetStudentRequestListForTechnical]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<StudentDefenceRequestDTO>();

                    foreach (DataRow row in table.Rows)
                    {
                        StudentDefenceRequestDTO request = new StudentDefenceRequestDTO();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.subject = row["subject"] as string;
                        request.note = row["note"] as string;
                        request.answernote = row["answernote"] as string;
                        request.answer_time = row["answer_time"] as string;
                        request.issue_time = row["issue_time"] as string;
                        request.status = Convert.ToInt32(row["status"]);
                        request.issuerID = Convert.ToInt32(row["issuerID"]);
                        request.send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.catID = Convert.ToInt32(row["catID"]);
                        request.senderID = Convert.ToInt32(row["senderID"]);
                        request.replierID = Convert.ToInt32(row["replierID"]);
                        request.issuerName = row["issuerName"] as string;
                        //request.location = row["location_name"] as string;
                        request.capacity = Convert.ToInt32(row["capacity"]);
                        request.courseName = row["courseName"] as string;
                        request.courseID = Convert.ToInt32(row["courseID"]);
                        request.daneshID = Convert.ToInt32(row["daneshID"]);
                        request.DefenceSubject = row["DefenceSubject"] as string;
                        request.StudentCode = row["StudentCode"] as string;
                        request.StudentFullName = row["StudentFullName"] as string;
                        request.RequestDate = row["RequestDate"] as string;
                        request.StartTime = row["StartTime"] as string;
                        request.EndTime = row["EndTime"] as string;
                        request.nameresh = row["nameresh"] as string;
                        request.CollegeName = row["DaneshName"] as string;
                        request.OnlineTeacherRole = row["OnlineTeacherRole"] as string;
                        if (row["IsEquippingResource"] != DBNull.Value)
                            request.IsEquippingResource = Convert.ToBoolean(row["IsEquippingResource"]);
                        if (row["isDeleted"] != DBNull.Value)
                            request.isDeleted = Convert.ToBoolean(row["isDeleted"]);
                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }


        public List<StudentDefenceRequestDTO> GetStudentDefenceListForPazhoohesh(int isReport = 0, string term = null)
        {
            List<StudentDefenceRequestDTO> requestlist = new List<StudentDefenceRequestDTO>();//--------
            SqlParameter[] parameters = new SqlParameter[]
           {
                 new SqlParameter("@isReport", isReport),
                new SqlParameter("@term", term)
           };
            DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[GetStudentDefenceListForPazhoohesh]", CommandType.StoredProcedure, parameters);
            if (table.Rows.Count > 0)
            {
                //requestlist = new List<StudentDefenceRequestDTO>();
                foreach (DataRow row in table.Rows)
                {
                    StudentDefenceRequestDTO request = new StudentDefenceRequestDTO();
                    //request.subject = row["subject"] as string;
                    // request.note = row["note"] as string;
                    // request.answernote = row["answernote"] as string;
                    //request.answer_time = row["answer_time"] as string;
                    //request.status = Convert.ToInt32(row["status"]);
                    //request.issuerID = Convert.ToInt32(row["issuerID"]);
                    //request.send_time = row["send_time"] as string;
                    //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                    //request.Sessiondate = row["sessiondate"] as string;
                    //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                    //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                    //request.catID = Convert.ToInt32(row["catID"]);
                    //request.senderID = Convert.ToInt32(row["senderID"]);
                    //request.replierID = Convert.ToInt32(row["replierID"]);
                    //request.issuerName = row["issuerName"] as string;
                    //request.location = row["location_name"] as string;
                    //request.capacity = Convert.ToInt32(row["capacity"]);
                    //request.courseName = row["courseName"] as string;
                    //request.courseID = Convert.ToInt32(row["courseID"]);
                    //request.daneshID = Convert.ToInt32(row["daneshID"]);
                    //request.DefenceSubject = row["DefenceSubject"] as string;

                    request.ID = Convert.ToInt32(row["ID"]);
                    request.StudentCode = row["StudentCode"] as string;
                    request.StudentFullName = row["StudentFullName"] as string;
                    request.issue_time = row["issue_time"] as string;
                    request.nameresh = row["nameresh"] as string;
                    request.CollegeName = row["DaneshName"] as string;
                    request.RequestDate = row["RequestDate"] as string;
                    request.StartTime = row["StartTime"] as string;
                    request.DefenceHasDone = row["DefenceHasDone"] as bool?;
                    //request.PayedDefence = row["PayedDefence"] as bool?;
                    request.ChkPaymentDavar1 = row["ChkPaymentDavar1"] as bool?;
                    request.ChkPaymentDavar2 = row["ChkPaymentDavar1"] as bool?;


                    //request.EndTime = row["EndTime"] as string;
                    //request.OnlineTeacherRole = row["OnlineTeacherRole"] as string;
                    //if (row["IsEquippingResource"] != DBNull.Value)
                    //    request.IsEquippingResource = Convert.ToBoolean(row["IsEquippingResource"]);
                    //if (row["isDeleted"] != DBNull.Value)
                    //    request.isDeleted = Convert.ToBoolean(row["isDeleted"]);
                    requestlist.Add(request);
                }
            }
            return requestlist;
        }


        public List<RefereeInformation> GetRefereeTeachersPaymentHasNotDown(string term = null)
        {
            List<RefereeInformation> requestlist = new List<RefereeInformation>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                 //new SqlParameter("@isReport", isReport),
                new SqlParameter("@term", term)
            };
            DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetRefereeTeachersPaymentHasNotDown]", CommandType.StoredProcedure, parameters);

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    RefereeInformation request = new RefereeInformation();
                    request.RequestID = row["requestId"] as int?;
                    request.StudentCode = (row["StudentCode"] as string) ?? "";
                    request.StudentFullName = (row["StudentFullName"] as string) ?? "";
                    request.RequestDate = (row["RequestDate"] as string) ?? "";
                    request.CollegeName = (row["name_college"] as string) ?? "";
                    request.RefereeMobile = (row["DavarMobile"] as string) ?? "";
                    request.RefereeOrder = (row["MartabeOstadDavar"] as string) ?? "";
                    request.RefereeOrderValue = (row["Martabe"] as decimal?) ?? -1;
                    request.RefereeFullName = (row["fullNameDavar"] as string) ?? "";
                    request.RefereeSiba = (row["SibaOstadDavar"] as string) ?? "";
                    request.RefereePayment = (row["PaymentValue"] as string) ?? "";
                    request.DefenceHasDone = row["DefenceHasDone"] as bool?;
                    request.Term = row["Term"] as string;
                    request.RefereeType = row["TypeDaver"] as string;
                    request.ChkPaymentReferee1 = (row["ChkPaymentDavar1"] as bool?) ?? false;
                    request.ChkPaymentReferee2 = (row["ChkPaymentDavar2"] as bool?) ?? false;
                    requestlist.Add(request);
                }
            }

            return requestlist;
        }


        public List<RefereeInformation> GetRefereeTeachersPaymentHasDown(string term = null)
        {
            List<RefereeInformation> requestlist = new List<RefereeInformation>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@term", term)
            };
            DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetRefereeTeachersPaymentHasDown]", CommandType.StoredProcedure, parameters);

            if (table.Rows.Count > 0)
            {

                foreach (DataRow row in table.Rows)
                {
                    RefereeInformation request = new RefereeInformation();
                    request.RequestID = row["requestId"] as int?;
                    request.StudentCode = (row["StudentCode"] as string) ?? "";
                    request.StudentFullName = (row["StudentFullName"] as string) ?? "";
                    request.RequestDate = (row["RequestDate"] as string) ?? "";
                    request.CollegeName = (row["name_college"] as string) ?? "";
                    request.RefereeMobile = (row["DavarMobile"] as string) ?? "";
                    request.RefereeOrder = (row["MartabeOstadDavar"] as string) ?? "";
                    request.RefereeOrderValue = (row["Martabe"] as decimal?) ?? -1;
                    request.RefereeFullName = (row["fullNameDavar"] as string) ?? "";
                    request.RefereeSiba = (row["SibaOstadDavar"] as string) ?? "";
                    request.RefereePayment = (row["PaymentValue"] as string) ?? "";
                    request.DefenceHasDone = row["DefenceHasDone"] as bool?;
                    request.Term = row["Term"] as string;
                    request.RefereeType = row["TypeDaver"] as string;
                    request.ChkPaymentReferee1 = (row["ChkPaymentDavar1"] as bool?) ?? false;
                    request.ChkPaymentReferee2 = (row["ChkPaymentDavar2"] as bool?) ?? false;
                    requestlist.Add(request);


                    //RefereeInformation request = new RefereeInformation();
                    //request.RequestID = row["requestId"] as int?;
                    //request.StudentFullName = (row["StudentFullName"] as string) ?? "";
                    //request.RequestDate = (row["RequestDate"] as string) ?? "";
                    //request.CollegeName = (row["CollageName"] as string) ?? "";
                    //request.RefereeMobile = (row["RefereeMobile"] as string) ?? "";
                    //request.RefereeOrderValue = (row["Martabe"] as int?) ?? -1;
                    //request.RefereeFullName = (row["RefereeFullName"] as string) ?? "";
                    //request.StudentCode = (row["StudentCode"] as string) ?? "";
                    //request.RefereeSiba = (row["SibaNo"] as string) ?? "";
                    //request.RefereePayment = (row["Wage"] as string) ?? "";
                    //request.ChkPaymentReferee1 = row["ChkPaymentDavar1"] as bool?;
                    //request.ChkPaymentReferee2 = row["ChkPaymentDavar2"] as bool?;
                    //request.RefereeType = Convert.ToString(row["RefereeType"] as int?);
                    //requestlist.Add(request);
                }
            }

            return requestlist;
        }


        public DataTable GetStudentDefenceListByProfossorCode(decimal profossorId = -1, int requestId = -1)
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                 new SqlParameter("@profossorId", profossorId),
                new SqlParameter("@requestId", requestId)
           };
            DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetStudentDefenceListByProfossorCode]", CommandType.StoredProcedure, parameters);
            return table;
        }

        public bool UpdateRequest_RejectReason(int requestId = -1, string rejectText = null)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@requestId", requestId)
                ,new SqlParameter("@rejectText", rejectText)
            };
            var res = SqlDBHelper.ExecuteNonQuery("[Resource_Control].[SP_UpdateRequest_RejectReason]", CommandType.StoredProcedure, parameters);
            return res;
        }



        public DataTable GetAllTermsForDefence(string term = null)
        {
            SqlParameter[] parameters = { new SqlParameter("@term", term) };
            var dt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetAllTermsForDefence]", CommandType.StoredProcedure, parameters);
            return dt;
        }


        public DataTable GetAllMartabeAndWage()
        {
            var dt = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[SP_GetAllMartabeAndWage]", CommandType.StoredProcedure);
            return dt;
        }


        public DataTable GetRefereeTeachersPaymentHasDone_Report(int isReport = 0, string term = null)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@isReport", isReport),
                new SqlParameter("@term", term)
            };
            var dt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetRefereeTeachersByPayment]", CommandType.StoredProcedure, parameters);
            return dt;
        }

        public DataTable GetRefereeTeachersPayment_Report(int isPayedWage = 0, int reportType = 0, string term = null)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Paymented", isPayedWage),
                new SqlParameter("@ReportType", reportType),
                new SqlParameter("@Term", term)
            };
            var dt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetRefereeTeachersPayment_Report]", CommandType.StoredProcedure, parameters);
            return dt;
        }


        public List<StudentDefenceRequestDTO> GetStudentRequestListForTraining()
        {
            List<StudentDefenceRequestDTO> requestlist = null;



            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[GetStudentRequestListForTraining]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<StudentDefenceRequestDTO>();

                    foreach (DataRow row in table.Rows)
                    {
                        StudentDefenceRequestDTO request = new StudentDefenceRequestDTO();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.subject = row["subject"] as string;
                        request.note = row["note"] as string;
                        request.answernote = row["answernote"] as string;
                        request.answer_time = row["answer_time"] as string;
                        request.issue_time = row["issue_time"] as string;
                        request.status = Convert.ToInt32(row["status"]);
                        request.issuerID = Convert.ToInt32(row["issuerID"]);
                        request.send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.catID = Convert.ToInt32(row["catID"]);
                        request.senderID = Convert.ToInt32(row["senderID"]);
                        request.replierID = Convert.ToInt32(row["replierID"]);
                        request.issuerName = row["issuerName"] as string;
                        //request.location = row["location_name"] as string;
                        request.capacity = Convert.ToInt32(row["capacity"]);
                        request.courseName = row["courseName"] as string;
                        request.courseID = Convert.ToInt32(row["courseID"]);
                        request.daneshID = Convert.ToInt32(row["daneshID"]);
                        request.DefenceSubject = row["DefenceSubject"] as string;
                        request.StudentCode = row["StudentCode"] as string;
                        request.StudentFullName = row["StudentFullName"] as string;
                        request.RequestDate = row["RequestDate"] as string;
                        request.StartTime = row["StartTime"] as string;
                        request.EndTime = row["EndTime"] as string;
                        request.OnlineTeacherRole = row["OnlineTeacherRole"] as string;
                        request.nameresh = row["nameresh"] as string;
                        if (row["IsEquippingResource"] != DBNull.Value)
                            request.IsEquippingResource = Convert.ToBoolean(row["IsEquippingResource"]);
                        if (row["isDeleted"] != DBNull.Value)
                            request.isDeleted = Convert.ToBoolean(row["isDeleted"]);
                        if (row["IsEducateProfessor"] != DBNull.Value)
                            request.IsEducateProfessor = Convert.ToBoolean(row["IsEducateProfessor"]);
                        requestlist.Add(request);

                    }
                }
            }

            return requestlist;
        }

        //public bool IsStudentRequestInThisTerm(string issuerTime,string requestDate)
        //{
        //    bool resualt = false;

        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@issueTime",issuerTime),
        //        new SqlParameter("@requestDate",requestDate),
        //    };
        //    using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_IsStudentRequestInThisTerm]", CommandType.StoredProcedure, parameters))
        //    {
        //        if(table == )
        //    }
        //}
        public List<StudentDefenceRequestDTO> GetStudentRequestListForOffice()
        {
            List<StudentDefenceRequestDTO> requestlist = null;



            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[GetStudentRequestListForOffice]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<StudentDefenceRequestDTO>();

                    foreach (DataRow row in table.Rows)
                    {
                        StudentDefenceRequestDTO request = new StudentDefenceRequestDTO();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.subject = row["subject"] as string;
                        request.note = row["note"] as string;
                        request.answernote = row["answernote"] as string;
                        request.answer_time = row["answer_time"] as string;
                        request.issue_time = row["issue_time"] as string;
                        request.status = Convert.ToInt32(row["status"]);
                        request.issuerID = Convert.ToInt32(row["issuerID"]);
                        request.send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.catID = Convert.ToInt32(row["catID"]);
                        request.senderID = Convert.ToInt32(row["senderID"]);
                        request.replierID = Convert.ToInt32(row["replierID"]);
                        request.issuerName = row["issuerName"] as string;
                        //request.location = row["location_name"] as string;
                        request.capacity = Convert.ToInt32(row["capacity"]);
                        request.courseName = row["courseName"] as string;
                        request.courseID = Convert.ToInt32(row["courseID"]);
                        request.daneshID = Convert.ToInt32(row["daneshID"]);
                        request.DefenceSubject = row["DefenceSubject"] as string;
                        request.StudentCode = row["StudentCode"] as string;
                        request.StudentFullName = row["StudentFullName"] as string;
                        request.RequestDate = row["RequestDate"] as string;
                        request.StartTime = row["StartTime"] as string;
                        request.EndTime = row["EndTime"] as string;

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }


        public List<StudentDefenceRequestDTO> GetStudentRequestListForEducation(int daneshId)
        {
            List<StudentDefenceRequestDTO> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DaneshId",daneshId),
            };



            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[GetStudentRequestListForEducation]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<StudentDefenceRequestDTO>();

                    foreach (DataRow row in table.Rows)
                    {
                        StudentDefenceRequestDTO request = new StudentDefenceRequestDTO();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.subject = row["subject"] as string;
                        request.note = row["note"] as string;
                        request.answernote = row["answernote"] as string;
                        request.answer_time = row["answer_time"] as string;
                        request.issue_time = row["issue_time"] as string;
                        request.status = Convert.ToInt32(row["status"]);
                        request.issuerID = Convert.ToInt32(row["issuerID"]);
                        request.send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.catID = Convert.ToInt32(row["catID"]);
                        request.senderID = Convert.ToInt32(row["senderID"]);
                        request.replierID = Convert.ToInt32(row["replierID"]);
                        request.issuerName = row["issuerName"] as string;
                        //request.location = row["location_name"] as string;
                        request.capacity = Convert.ToInt32(row["capacity"]);
                        request.courseName = row["courseName"] as string;
                        request.courseID = Convert.ToInt32(row["courseID"]);
                        request.daneshID = Convert.ToInt32(row["daneshID"]);
                        request.DefenceSubject = row["DefenceSubject"] as string;
                        request.StudentCode = row["StudentCode"] as string;
                        request.StudentFullName = row["StudentFullName"] as string;
                        request.RequestDate = row["RequestDate"] as string;
                        request.StartTime = row["StartTime"] as string;
                        request.EndTime = row["EndTime"] as string;
                        request.nameresh = row["nameresh"] as string;
                        request.NezamId = row["NezamId"] as int?;
                        request.NezamName = row["NezamName"] as string;
                        request.DateRegistration = row["DateRegistration"] as DateTime?;
                        request.IsRequestEducation = (row["IsRequestEducation"] as bool?)==null|| (row["IsRequestEducation"] as bool?)==false ? false:true;
                        if (row["isDeleted"] != DBNull.Value)
                            request.isDeleted = Convert.ToBoolean(row["isDeleted"]);
                        else
                            request.isDeleted = false;
                        requestlist.Add(request);

                        request.IsSendSmsFinancial = row["IsSendSmsFinancial"] as bool?;
                        if (request.IsSendSmsFinancial == null)
                            request.IsSendSmsFinancial = false;
                        request.MsgSendSmsFinancial = row["MsgSendSmsFinancial"] as string;
                    }
                }
            }
            //var tt = requestlist.Where(x => x.isDeleted == true).ToList();
            return requestlist;
        }

        public List<FinancialPermission> GetFinancialPermission()
        {
            var requestlist = new List<FinancialPermission>();
            using (var table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[SP_GetFinancialPermission]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {

                    foreach (DataRow row in table.Rows)
                    {
                        var request = new FinancialPermission
                        {
                            StudentCode = row["stCode"].ToString(),
                            NationalCode = row["NationalCode"] as string,
                            StudentName = row["studentName"] as string,
                            PortalPermissionDate = row["PortalPermissionDate"] as string,
                            UnitSectionDate = row["UnitSectionDate"] as string,
                            Debit = row["debit"] != DBNull.Value && Convert.ToDecimal(row["debit"]) > 0,
                            Permission = row["Permission"] == DBNull.Value
                                ? (bool?)null
                                : Convert.ToBoolean(row["Permission"])
                        };
                        requestlist.Add(request);
                    }
                }
            }
            return requestlist;
        }

        public int AddOrUpdateFinancialPermission(FinancialPermission student)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@stCode", student.StudentCode),
                new SqlParameter("@permission",student.Permission)

            };
            var resualt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_AddOrUpdateFinancialPermission]", CommandType.StoredProcedure, parameters);
            return Convert.ToInt32(resualt.Rows[0][0]);
        }

        public List<RequestFR> GetRequestListBySessionDate_resID_status(string sessiondate, int resID, int status)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@resID",resID),
                new SqlParameter("@sessiondate",sessiondate),
                new SqlParameter("@status", status)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestListBySessionDateAndStatusAndResID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }

        public List<RequestFR> GetRequestListBystatusAnddaneshID(int status, int daneshID)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status",status),
                new SqlParameter("@daneshID",daneshID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelectListByStatusAndDaneshID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        bool results = requestlist.Any(i => i.ID == (int)row["ID"]);
                        if (!results)
                        {
                            RequestFR request = new RequestFR();
                            request.ID = Convert.ToInt32(row["ID"]);
                            request.Subject = row["subject"] as string;
                            request.Note = row["note"] as string;
                            request.Answernote = row["answernote"] as string;
                            request.Answer_time = row["answer_time"] as string;
                            request.Issue_time = row["issue_time"] as string;
                            request.Status = Convert.ToInt32(row["status"]);
                            request.IssuerID = Convert.ToInt32(row["issuerID"]);
                            request.Send_time = row["send_time"] as string;
                            request.CatID = Convert.ToInt32(row["catID"]);
                            request.SenderID = Convert.ToInt32(row["senderID"]);
                            request.ReplierID = Convert.ToInt32(row["replierID"]);
                            request.IssuerName = row["issuerName"] as string;
                            request.Location = row["location_name"] as string;
                            request.Capacity = Convert.ToInt32(row["capacity"]);
                            request.CourseName = row["courseName"] as string;
                            request.CourseDID = Convert.ToInt32(row["courseID"]);
                            request.DaneshID = Convert.ToInt32(row["daneshID"]);
                            //RequestDateTime rdt = new RequestDateTime();
                            //rdt.Date = row["Date"].ToString();
                            //rdt.ClassName = row["ClassName"].ToString();
                            //rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            //rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            //rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            //rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            //rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            //rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            //request.DateTimeRange = new List<RequestDateTime>();
                            //request.DateTimeRange.Add(rdt);
                            requestlist.Add(request);
                        }
                        else
                        {
                            RequestDateTime rdt = new RequestDateTime();
                            rdt.Date = row["Date"].ToString();
                            rdt.ClassName = row["ClassName"].ToString();
                            rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            var req = requestlist.Where(i => i.ID == (int)row["ID"]).First();
                            req.DateTimeRange.Add(rdt);
                        }
                    }
                }
            }

            return requestlist;
        }

        public List<RequestFR> GetRequestListBystatusAnddaneshIDForDefence(int status, int daneshID)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status",status),
                new SqlParameter("@daneshID",daneshID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelectListByStatusAndDaneshIDForDefence]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        bool results = requestlist.Any(i => i.ID == (int)row["ID"]);
                        if (!results)
                        {
                            RequestFR request = new RequestFR();
                            request.ID = Convert.ToInt32(row["ID"]);
                            request.Subject = row["subject"] as string;
                            request.Note = row["note"] as string;
                            request.Answernote = row["answernote"] as string;
                            request.Answer_time = row["answer_time"] as string;
                            request.Issue_time = row["issue_time"] as string;
                            request.Status = Convert.ToInt32(row["status"]);
                            request.IssuerID = Convert.ToInt32(row["issuerID"]);
                            request.Send_time = row["send_time"] as string;
                            request.CatID = Convert.ToInt32(row["catID"]);
                            request.SenderID = Convert.ToInt32(row["senderID"]);
                            request.ReplierID = Convert.ToInt32(row["replierID"]);
                            request.IssuerName = row["issuerName"] as string;
                            request.Location = row["location_name"] as string;
                            request.Capacity = Convert.ToInt32(row["capacity"]);
                            request.CourseName = row["courseName"] as string;
                            request.CourseDID = Convert.ToInt32(row["courseID"]);
                            request.DaneshID = Convert.ToInt32(row["daneshID"]);
                            //RequestDateTime rdt = new RequestDateTime();
                            //rdt.Date = row["Date"].ToString();
                            //rdt.ClassName = row["ClassName"].ToString();
                            //rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            //rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            //rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            //rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            //rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            //rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            //request.DateTimeRange = new List<RequestDateTime>();
                            //request.DateTimeRange.Add(rdt);
                            requestlist.Add(request);
                        }
                        else
                        {
                            RequestDateTime rdt = new RequestDateTime();
                            rdt.Date = row["Date"].ToString();
                            rdt.ClassName = row["ClassName"].ToString();
                            rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            var req = requestlist.Where(i => i.ID == (int)row["ID"]).First();
                            req.DateTimeRange.Add(rdt);
                        }
                    }
                }
            }

            return requestlist;
        }


        public List<RequestFR> GetDeletedRequestListByDaneshID(int daneshID)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@daneshID",daneshID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetDeletedRequestListByDaneshID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        bool results = requestlist.Any(i => i.ID == (int)row["ID"]);
                        if (!results)
                        {
                            RequestFR request = new RequestFR();
                            request.ID = Convert.ToInt32(row["ID"]);
                            request.Subject = row["subject"] as string;
                            request.Note = row["note"] as string;
                            request.Answernote = row["answernote"] as string;
                            request.Answer_time = row["answer_time"] as string;
                            request.Issue_time = row["issue_time"] as string;
                            request.Status = Convert.ToInt32(row["status"]);
                            request.IssuerID = Convert.ToInt32(row["issuerID"]);
                            request.Send_time = row["send_time"] as string;
                            request.CatID = Convert.ToInt32(row["catID"]);
                            request.SenderID = Convert.ToInt32(row["senderID"]);
                            request.ReplierID = Convert.ToInt32(row["replierID"]);
                            request.IssuerName = row["issuerName"] as string;
                            request.Location = row["location_name"] as string;
                            request.Capacity = Convert.ToInt32(row["capacity"]);
                            request.CourseName = row["courseName"] as string;
                            request.CourseDID = Convert.ToInt32(row["courseID"]);
                            request.DaneshID = Convert.ToInt32(row["daneshID"]);
                            //RequestDateTime rdt = new RequestDateTime();
                            //rdt.Date = row["Date"].ToString();
                            //rdt.ClassName = row["ClassName"].ToString();
                            //rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            //rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            //rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            //rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            //rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            //rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            //request.DateTimeRange = new List<RequestDateTime>();
                            //request.DateTimeRange.Add(rdt);
                            requestlist.Add(request);
                        }
                        else
                        {
                            RequestDateTime rdt = new RequestDateTime();
                            rdt.Date = row["Date"].ToString();
                            rdt.ClassName = row["ClassName"].ToString();
                            rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            var req = requestlist.Where(i => i.ID == (int)row["ID"]).First();
                            req.DateTimeRange.Add(rdt);
                        }
                    }
                }
            }

            return requestlist;
        }


        public List<RequestFR> GetRequestListBystatusAndIssuerID(int status, int IssuerID)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status",status),
                new SqlParameter("@daneshID",IssuerID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelectListByStatusAndIssuerID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        bool results = requestlist.Any(i => i.ID == (int)row["ID"]);
                        if (!results)
                        {
                            RequestFR request = new RequestFR();
                            request.ID = Convert.ToInt32(row["ID"]);
                            request.Subject = row["subject"] as string;
                            request.Note = row["note"] as string;
                            request.Answernote = row["answernote"] as string;
                            request.Answer_time = row["answer_time"] as string;
                            request.Issue_time = row["issue_time"] as string;
                            request.Status = Convert.ToInt32(row["status"]);
                            request.IssuerID = Convert.ToInt32(row["issuerID"]);
                            request.Send_time = row["send_time"] as string;
                            request.CatID = Convert.ToInt32(row["catID"]);
                            request.SenderID = Convert.ToInt32(row["senderID"]);
                            request.ReplierID = Convert.ToInt32(row["replierID"]);
                            request.IssuerName = row["issuerName"] as string;
                            request.Location = row["location_name"] as string;
                            request.Capacity = Convert.ToInt32(row["capacity"]);
                            request.CourseName = row["courseName"] as string;
                            request.CourseDID = Convert.ToInt32(row["courseID"]);
                            request.DaneshID = Convert.ToInt32(row["daneshID"]);
                            //RequestDateTime rdt = new RequestDateTime();
                            //rdt.Date = row["Date"].ToString();
                            //rdt.ClassName = row["ClassName"].ToString();
                            //rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            //rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            //rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            //rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            //rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            //rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            //request.DateTimeRange = new List<RequestDateTime>();
                            //request.DateTimeRange.Add(rdt);
                            requestlist.Add(request);
                        }
                        else
                        {
                            RequestDateTime rdt = new RequestDateTime();
                            rdt.Date = row["Date"].ToString();
                            rdt.ClassName = row["ClassName"].ToString();
                            rdt.StartTime = Convert.ToInt64(row["startTime"]);
                            rdt.EndTime = Convert.ToInt64(row["endTime"]);
                            rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                            rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                            rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                            rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                            var req = requestlist.Where(i => i.ID == (int)row["ID"]).First();
                            req.DateTimeRange.Add(rdt);
                        }
                    }
                }
            }

            return requestlist;
        }

        public List<RequestFR> GetRequestListByDaneshID(int daneshID)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@daneshID",daneshID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelectListByDaneshID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location_name"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }

        public DataTable GetRequestListBySessionDateResID(string sessiondate, int resID)
        {
            DataTable requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@sessiondate",sessiondate),
               new SqlParameter("@resID",resID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetRequestListBySessionDateResID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = table.Copy();
                    //requestlist = new List<RequestFR>();

                    //foreach (DataRow row in table.Rows)
                    //{
                    //    //bool results = requestlist.Any(i => i.ID == (int)row["ID"]);
                    //    //if (!results)
                    //    //{
                    //        RequestFR request = new RequestFR();
                    //        request.ID = Convert.ToInt32(row["ID"]);
                    //        request.Subject = row["subject"] as string;
                    //        request.Note = row["note"] as string;
                    //        request.Answernote = row["answernote"] as string;
                    //        request.Answer_time = row["answer_time"] as string;
                    //        request.Issue_time = row["issue_time"] as string;
                    //        request.Status = Convert.ToInt32(row["status"]);
                    //        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                    //        request.Send_time = row["send_time"] as string;
                    //        request.CatID = Convert.ToInt32(row["catID"]);
                    //        request.SenderID = Convert.ToInt32(row["senderID"]);
                    //        request.ReplierID = Convert.ToInt32(row["replierID"]);
                    //        request.IssuerName = row["issuerName"] as string;
                    //        request.Location = row["location_name"] as string;
                    //        request.Capacity = Convert.ToInt32(row["capacity"]);
                    //        request.CourseName = row["courseName"] as string;
                    //        request.CourseDID = Convert.ToInt32(row["courseID"]);
                    //        request.DaneshID = Convert.ToInt32(row["daneshID"]);
                    //        RequestDateTime rdt = new RequestDateTime();
                    //        rdt.Date = row["Date"].ToString();
                    //        rdt.ClassName = row["ClassName"].ToString();
                    //        rdt.StartTime = Convert.ToInt64(row["startTime"]);
                    //        rdt.EndTime = Convert.ToInt64(row["endTime"]);
                    //        rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                    //        rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                    //        rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                    //        rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                    //        request.DateTimeRange = new List<RequestDateTime>();
                    //        request.DateTimeRange.Add(rdt);
                    //        requestlist.Add(request);
                    //    //}
                    //    //else
                    //    //{
                    //    //    RequestDateTime rdt = new RequestDateTime();
                    //    //    rdt.Date = row["Date"].ToString();
                    //    //    rdt.ClassName = row["ClassName"].ToString();
                    //    //    rdt.StartTime = Convert.ToInt64(row["startTime"]);
                    //    //    rdt.EndTime = Convert.ToInt64(row["endTime"]);
                    //    //    rdt.DateTimeId = Convert.ToInt32(row["DateTimeId"]);
                    //    //    rdt.MayConflict = Convert.ToBoolean(row["MayConflict"]);
                    //    //    rdt.ResourceId = Convert.ToInt32(row["resourceId"] ?? 0);
                    //    //    rdt.RequestId = Convert.ToInt32(row["RequestId"]);
                    //    //    var req = requestlist.Where(i => i.ID == (int)row["ID"]).First();
                    //    //    req.DateTimeRange.Add(rdt);
                    //    //}
                    //}
                }
            }

            return requestlist;
        }

        public List<RequestFR> GetRequestListByStatusAfterToday(int status, string emrooz)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status",status),
                new SqlParameter("@date",emrooz)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelectListByStatusAfterDate]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location_name"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }


        public DataTable getClassInfo(int reqId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@reqID", reqId)

            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_classInfo]", CommandType.StoredProcedure, parameters);
        }
        public DataTable CheckRequestConflict(int userID, DataTable dateTimeList)
        {
            SqlParameter param = new SqlParameter("@dateTimeList", dateTimeList);
            param.SqlDbType = SqlDbType.Structured;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userID", userID),
                param
            };

            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_CheckRequestConflict]", CommandType.StoredProcedure, parameters);
        }

        public mainViewModel GetRequestCountByStatusForAdmin()
        {
            mainViewModel mvm = null;
            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[sp_GetAllStatusCountForAdmin]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }
            return mvm;
        }

        public mainViewModel GetRequestCountByStatusForAdminForDefence()
        {
            mainViewModel mvm = null;
            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[sp_GetAllStatusCountForAdminForDefence]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }
            return mvm;
        }


        public mainViewModel GetRequestCountByStatus()
        {
            mainViewModel mvm = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[sp_GetAllStatusCount]", CommandType.StoredProcedure))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }
        public mainViewModel GetRequestCountByStatusForDefence()
        {
            mainViewModel mvm = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[sp_GetAllStatusCountForDefence]", CommandType.StoredProcedure))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }

        public mainViewModel GetRequestCountByStatusAndDaneshId(int daneshId)
        {
            mainViewModel mvm = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@daneshId",daneshId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetAllStatusCountByDaneshId]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }
        public mainViewModel GetRequestCountByStatusAndDaneshIdForDefence(int daneshId)
        {
            mainViewModel mvm = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@daneshId",daneshId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetAllStatusCountByDaneshIdForDefence]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }


        public mainViewModel GetDefenceRequestCountByLocationForEducation(int daneshId)
        {
            mainViewModel mvm = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@daneshId",daneshId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetDefenceRequestCountByLocationForEducation]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }

        public mainViewModel GetDefenceRequestCountByLocationForTechnical()
        {
            mainViewModel mvm = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[SP_GetDefenceRequestCountByLocationForTechnical]", CommandType.StoredProcedure))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }

        public mainViewModel GetRequestCountByStatusAndIssuerId(int IssuerId)
        {
            mainViewModel mvm = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@IssuerId",IssuerId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetAllStatusCountByIssuerId]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }
        public mainViewModel GetRequestCountByStatusAndIssuerIdForDefence(int IssuerId)
        {
            mainViewModel mvm = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@IssuerId",IssuerId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetAllStatusCountByIssuerIdForDefence]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }

        public List<RequestFR> GetRequestListByDID(int did)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@did",did)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_RequestSelectListByDID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location_name"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }

        public int DenyRequest(RequestFR request)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID",request.ID),
                new SqlParameter("@answernote", request.Answernote),
                new SqlParameter("@answer_time", request.Answer_time),
                new SqlParameter("@replierID",request.ReplierID ),
                new SqlParameter("@Status",request.Status),

            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[sp_RequestDeny]", CommandType.StoredProcedure, parameters);

        }

        public List<RequestFR> GetRequestListByStatusAndLocationId(int status, int locId)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status", status),
                new SqlParameter("@locId",locId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetRequestListByStatusAndLocationId]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }

        public List<RequestFR> GetRequestListByLocationId(int locId)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@locId",locId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetRequestListByLocationId]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }

        public int UpdateRequestStatus(int reqId, int status)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@reqId",reqId),
                new SqlParameter("@status", status),
            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[sp_RequestUpdateStatus]", CommandType.StoredProcedure, parameters);

        }
        public int GetStCodeByReqId(int reqId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@reqId",reqId),

            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[GetStCodeByReqId]", CommandType.StoredProcedure, parameters);

        }
        public int GetStatusByReqId(int reqId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@reqId",reqId),

            };

            return SqlDBHelper.ExecuteScalar("[Resource_Control].[GetStatusByReqId]", CommandType.StoredProcedure, parameters);

        }
        public mainViewModel GetRequestCountByLocation(int location_id)
        {

            mainViewModel mvm = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@location_id",location_id)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetAllStatusCountByLocationId]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }


        public bool UpdateRequestAnswerTimeByIdrequest(int requestId)
        {
            var p = new PersianCalendar();


            string answerTime = string.Format(@"{0}/{1}/{2}", p.GetYear(DateTime.Now), p.GetMonth(DateTime.Now), p.GetDayOfMonth(DateTime.Now));
            var parameter = new SqlParameter[]
            {
               new SqlParameter("@requestId",requestId),
               new SqlParameter("@answerTime",answerTime),
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[SP_UpdateRequestAnswerTimeByIdrequest]", CommandType.StoredProcedure, parameter);
        }
        public void ActiveSendSMSFlag(int requestId)
        {

            var parameter = new SqlParameter[]
            {

               new SqlParameter("@requestId",requestId)
            };
            SqlDBHelper.ExecuteNonQuery("[Resource_Control].[SP_ActiveSendSMSFlag]", CommandType.StoredProcedure, parameter);
        }
        public int ReadSMSFlagByReqId(int requestId)
        {

            var parameter = new SqlParameter[]
            {

               new SqlParameter("@ReqId",requestId)
            };
            var res = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[ReadSMSFlagByReqId]", CommandType.StoredProcedure, parameter);
            if (res.Rows.Count > 0)
            {
                if (res.Rows[0][0] == null || string.IsNullOrEmpty(res.Rows[0][0].ToString()))
                    return 0;
                else
                    return Convert.ToInt32(res.Rows[0][0]);
            }
            else
                return 0;
        }

        public List<RequestFR> GetRequestListByStatusAndLocationIdForEdari(int status, int locationId)
        {
            List<RequestFR> requestlist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status", status),
                new SqlParameter("@locId",locationId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetRequestListByStatusAndLocationIdForEdari]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<RequestFR>();

                    foreach (DataRow row in table.Rows)
                    {
                        RequestFR request = new RequestFR();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.Subject = row["subject"] as string;
                        request.Note = row["note"] as string;
                        request.Answernote = row["answernote"] as string;
                        request.Answer_time = row["answer_time"] as string;
                        request.Issue_time = row["issue_time"] as string;
                        request.Status = Convert.ToInt32(row["status"]);
                        request.IssuerID = Convert.ToInt32(row["issuerID"]);
                        request.Send_time = row["send_time"] as string;
                        request.CatID = Convert.ToInt32(row["catID"]);
                        request.SenderID = Convert.ToInt32(row["senderID"]);
                        request.ReplierID = Convert.ToInt32(row["replierID"]);
                        request.IssuerName = row["issuerName"] as string;
                        request.Location = row["location"] as string;
                        request.Capacity = Convert.ToInt32(row["capacity"]);
                        request.CourseName = row["courseName"] as string;
                        request.CourseDID = Convert.ToInt32(row["courseID"]);
                        request.DaneshID = Convert.ToInt32(row["daneshID"]);

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }

        public mainViewModel GetRequestCountByLocationForEdari(int locationId)
        {
            mainViewModel mvm = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@location_id",locationId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetAllStatusCountByLocationIdForEdari]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }
        public mainViewModel GetRequestCountByLocationForEdariForDefence(int locationId)
        {
            mainViewModel mvm = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@location_id",locationId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetAllStatusCountByLocationIdForEdariForDefence]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }
        public mainViewModel GetDefenceRequestCountByLocationForEdari(int locationId)
        {
            mainViewModel mvm = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@location_id",locationId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetDefenceRequestCountByLocationForEdari]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];


                    mvm = new mainViewModel();

                    mvm.WaitingForSendCount = Convert.ToInt32(row["WaitingForSendCount"]);
                    mvm.SentCount = Convert.ToInt32(row["SentCount"]);
                    mvm.ApprovedCount = Convert.ToInt32(row["ApprovedCount"]);
                    mvm.DeniedCount = Convert.ToInt32(row["DeniedCount"]);
                    mvm.InformedCount = Convert.ToInt32(row["InformedCount"]);
                    mvm.LostCount = Convert.ToInt32(row["LostCount"]);
                }
            }

            return mvm;
        }


        public int GetDefenceInMeetingLength(int collegeId)
        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[SP_GetLenghtMeetingTimeByCollegeId]"
            };
            cmd.Parameters.AddWithValue("@collegeId", collegeId);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            if (dt.Rows.Count == 1)

                return Convert.ToInt32(dt.Rows[0][0]);
            else
                return 0;
        }
        public DefenceInformation GetDefenceInformation(string studentCode)
        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[SP_GetDefenceInfo]"
            };
            cmd.Parameters.AddWithValue("@stcode", studentCode);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            if (dt.Rows.Count <= 0) return new DefenceInformation();
            var defenceInformation = new DefenceInformation
            {
                StudentCode = dt.Rows[0]["stcode"]?.ToString(),
                GroupAcceptDate = dt.Rows[0]["dateTasvib"]?.ToString(),
                DefenceSubject = dt.Rows[0]["onvan_payan"]?.ToString(),
                studentGender = dt.Rows[0]["jens"]?.ToString(),
                StudentField = dt.Rows[0]["resh"]?.ToString(),
                StudentFullName = dt.Rows[0]["StudentFullName"]?.ToString(),
                CollegeId = dt.Rows[0]["id_college"].ToString().Trim() != "0" ? dt.Rows[0]["id_college"]?.ToString() : string.Empty,
                CollegeName = dt.Rows[0]["name_college"]?.ToString()
            };
            //
            if (dt.Rows[0]["Moshaver1"].ToString().Trim() != "0")
            {
                defenceInformation.FirstConsultantId = dt.Rows[0]["Moshaver1"]?.ToString();
                defenceInformation.FirstConsultantFullName = dt.Rows[0]["fullNameMoshaver1"]?.ToString();
                defenceInformation.FirstConsultantMobile = dt.Rows[0]["Moshaver1Mobile"]?.ToString()?.Trim();
                defenceInformation.FirstConsultantMail = dt.Rows[0]["Moshaver1Mail"]?.ToString();
                defenceInformation.FirstConsultantGender = dt.Rows[0]["Moshaver1jensiat"]?.ToString();
            }
            if (dt.Rows[0]["Moshaver2"].ToString().Trim() != "0")
            {
                defenceInformation.SecondConsultantId = dt.Rows[0]["Moshaver2"]?.ToString();
                defenceInformation.SecondConsultantFullName = dt.Rows[0]["fullNameMoshaver2"]?.ToString();
                defenceInformation.SecondConsultantMobile = dt.Rows[0]["Moshaver2Mobile"]?.ToString()?.Trim();
                defenceInformation.SecondConsultantMail = dt.Rows[0]["Moshaver2Mail"]?.ToString();
                defenceInformation.SecondConsultantGender = dt.Rows[0]["Moshaver2jensiat"]?.ToString();
            }
            if (dt.Rows[0]["Rahnama1"].ToString().Trim() != "0")
            {
                defenceInformation.FirstGuideId = dt.Rows[0]["Rahnama1"]?.ToString();
                defenceInformation.FirstGuideFullName = dt.Rows[0]["fullNameRahnama1"]?.ToString();
                defenceInformation.FirstGuideMobile = dt.Rows[0]["Rahnama1Mobile"]?.ToString()?.Trim();
                defenceInformation.FirstGuideMail = dt.Rows[0]["Rahnama1Mail"]?.ToString();
                defenceInformation.FirstGuideGender = dt.Rows[0]["Rahnama1jensiat"]?.ToString();
            }
            if (dt.Rows[0]["Rahnama2"].ToString().Trim() != "0")
            {
                defenceInformation.SecondGuideId = dt.Rows[0]["Rahnama2"]?.ToString();
                defenceInformation.SecondGuideFullName = dt.Rows[0]["fullNameRahnama2"]?.ToString();
                defenceInformation.SecondGuideMobile = dt.Rows[0]["Rahnama2Mobile"]?.ToString()?.Trim();
                defenceInformation.SecondGuideMail = dt.Rows[0]["Rahnama2Mail"]?.ToString();
                defenceInformation.SecondGuideGender = dt.Rows[0]["Rahnama2jensiat"]?.ToString();
            }
            if (dt.Rows[0]["DavarIn"].ToString().Trim() != "0")
            {
                defenceInformation.FirstRefereeId = dt.Rows[0]["DavarIn"]?.ToString();
                defenceInformation.FirstRefereeFullName = dt.Rows[0]["fullNameDavarIn"]?.ToString();
                defenceInformation.FirstRefereeMobile = dt.Rows[0]["DavarInMobile"]?.ToString()?.Trim();
                defenceInformation.FirstRefereeMail = dt.Rows[0]["DavarInMail"]?.ToString();
                defenceInformation.FirstRefereeGender = dt.Rows[0]["DavarINjensiat"]?.ToString();
            }
            if (dt.Rows[0]["DavarOUT"].ToString().Trim() != "0")
            {
                defenceInformation.SecondRefereeId = dt.Rows[0]["DavarOUT"]?.ToString();
                defenceInformation.SecondRefereeFullName = dt.Rows[0]["fullNameDavarOUT"]?.ToString();
                defenceInformation.SecondRefereeMobile = dt.Rows[0]["DavarOUTMobile"]?.ToString()?.Trim();
                defenceInformation.SecondRefereeMail = dt.Rows[0]["DavarOUTMail"]?.ToString();
                defenceInformation.SecondRefereeGender = dt.Rows[0]["DavarOUTjensiat"]?.ToString();
            }
            return defenceInformation;
        }

        public List<ProfessorDto> GetProfessorsRelatedStudent(int studentCode)
        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[SP_GetProfessorRelatedStudent]"
            };
            cmd.Parameters.AddWithValue("@stcode", studentCode);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var resault = new List<ProfessorDto>();
            if (dt.Rows.Count > 0)
                foreach (DataRow dtRow in dt.Rows)
                {
                    resault.Add(new ProfessorDto()
                    {
                        Id = Convert.ToInt32(dtRow["Id"]),
                        Name = string.Concat(dtRow["Name"], " ", dtRow["Family"])
                    });
                }
            return resault;
        }


        public List<CertainTimesDto> GetPreventCertainTime(string startDate)
        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[SP_GetSpeialDate]"
            };
            cmd.Parameters.AddWithValue("@startDate", startDate);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var resault = new List<CertainTimesDto>();
            if (dt.Rows.Count > 0)
                foreach (DataRow dtRow in dt.Rows)
                {
                    resault.Add(new CertainTimesDto()
                    {
                        Id = Convert.ToInt32(dtRow["Id"]),
                        StartDate = dtRow["StartDate"] as string,
                        EndDate = dtRow["EndDate"] as string,
                        Description = dtRow["Description"] as string,
                        IsForStudent = Convert.ToBoolean(dtRow["IsForStudent"]),
                        IsForEmployee = Convert.ToBoolean(dtRow["IsForEmployee"])
                    });
                }
            return resault;
        }


        public DataTable GetProfessorAttendanceInCurrentTerm(int studentCode)
        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[SP_GetProfessorAttendanceInCurrentTerm]"
            };
            cmd.Parameters.AddWithValue("@stcode", studentCode);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return dt;
        }

        public bool CanAssignmentResource(string requestDate, long startTime, long endTime, int collegeId)

        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[CheckDefenceRequest]"
            };
            cmd.Parameters.AddWithValue("@requestDate", requestDate);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@collegeId", collegeId);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CanAssignmentResourceV2(string requestDate, long startTime, long endTime, int collegeId)

        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[SP_CheckCanAssignmentResourceForAddRequest]"
            };
            cmd.Parameters.AddWithValue("@requestedDate", requestDate);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@collegeId", collegeId);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return dt.Rows.Count > 0;
        }


        public bool CanAssignmentResource(string requestDate, long startTime, long endTime, int collegeId, int reqId)

        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[CheckDefenceRequestForEducation]"
            };
            cmd.Parameters.AddWithValue("@requestDate", requestDate);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@collegeId", collegeId);
            cmd.Parameters.AddWithValue("@reqId", reqId);


            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanAssignmentResourceV2(string requestDate, long startTime, long endTime, int collegeId, int reqId)

        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[SP_CheckCanAssignmentResourceForEditRequest]"
            };
            cmd.Parameters.AddWithValue("@requestedDate", requestDate);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@collegeId", collegeId);
            cmd.Parameters.AddWithValue("@requestId", reqId);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (dt.Rows.Count > 0 && (dt.Rows.Count > 0 || dt.Rows[0][0].ToString() == "ok"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckBeforeDefenceRequest(string requestDate, long startTime, long endTime, int issuerId)

        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[CheckBeforeDefenceRequest]"
            };
            cmd.Parameters.AddWithValue("@requestDate", requestDate);
            cmd.Parameters.AddWithValue("@startTime", startTime);
            cmd.Parameters.AddWithValue("@endTime", endTime);
            cmd.Parameters.AddWithValue("@issuerId", issuerId);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasAnotherRequestInThisTerm(int issuerId)

        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[HasAnotherRequestInThisTerm]"
            };

            cmd.Parameters.AddWithValue("@issuerId", issuerId);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasAnotherRequestInThisTermForIntro(int issuerId)

        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[HasAnotherRequestInThisTermForIntro]"
            };

            cmd.Parameters.AddWithValue("@issuerId", issuerId);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (dt.Rows[0][0].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetStudentDefenceRequest(int studentId)

        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[GetListOfStudentDefenceRequest]"
            };

            cmd.Parameters.AddWithValue("@StudentId", studentId);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return dt;
        }

        public string GetStudentMobile(int studentId)
        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[GetStudentMobile]"
            };

            cmd.Parameters.AddWithValue("@StudentId", studentId);
            var dt = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                var data = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(data);
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return dt.Rows[0][0].ToString();
        }

        public void SetStudentMobile(string studentId, string mobileNumber)
        {
            var connection = new SqlConnection(new SuppConnection().Supp_con);
            var cmd = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Resource_Control].[SetStudentMobile]"
            };

            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                cmd.ExecuteNonQuery();

                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
        public DataTable GetStudentRequestAndDefInfo(int daneshID)
        {

            SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Resource_Control].[SP_GetDefenceInfoFromThesesAndRequest]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@daneshId", daneshID);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return dt;
        }

        public void UpdateStatusDefRequest(int status, int reqId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@RequestId",reqId ),
            new SqlParameter("@status",status )
            };
            SqlDBHelper.ExecuteNonQuery("[Resource_Control].[UpdatetStatusDefRequest]", CommandType.StoredProcedure, parameters);
        }

        public bool UpdateDefenceInformation_DefenceHasDone(int requestId = -1, bool defenceHasDone = false, int refereeType = -1, bool paymentStatusForReferee = false)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@requestId",requestId ),
                new SqlParameter("@defenceHasDone",defenceHasDone ),
                new SqlParameter("@refereeType",refereeType ),
                new SqlParameter("@paymentStatusForReferee",paymentStatusForReferee ),

            };
            var resualt = SqlDBHelper.ExecuteScalar("[Resource_Control].[SP_UpdateDefenceInformation_DefenceHasDone]", CommandType.StoredProcedure, parameters);
            return resualt > 0 ? true : false;
        }

        public DataTable GetDefenceInformationByRequestID(int requestID = -1)
        {
            SqlParameter[] parameters = { new SqlParameter("@RequestID", requestID) };
            var dt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetDefenceInformationByRequestID]", CommandType.StoredProcedure, parameters);
            return dt;
        }



        public bool InsertIntoRefereeWagePayment_Log(int requestId = -1, string studentName = "", string requestDate = "", string collageName = "", string refereeMobile = "", int martabe = 0, string refereefullName = "", string studentCode = "", string sibaNo = "", string wage = "", int refereeType = -1, string term = "", bool isRejected = false)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RequestID",requestId ),
                new SqlParameter("@StudentFullName",studentName ),
                new SqlParameter("@RequestDate",requestDate ),
                new SqlParameter("@CollageName",collageName ),
                new SqlParameter("@RefereeMobile",refereeMobile ),
                new SqlParameter("@Martabe",martabe ),
                new SqlParameter("@RefereefullName",refereefullName ),
                new SqlParameter("@StudentCode",studentCode ),
                new SqlParameter("@SibaNo",sibaNo ),
                new SqlParameter("@Wage",wage ),
                new SqlParameter("@refereeType",refereeType ),
                //new SqlParameter("@ChkPaymentDavar1",chkPaymentDavar1 ),
                //new SqlParameter("@ChkPaymentDavar2",chkPaymentDavar2 ),
                new SqlParameter("@Term",term ) ,
                new SqlParameter("@IsRejected",isRejected )
            };
            var resualt = SqlDBHelper.ExecuteNonQuery("[Resource_Control].[SP_InsertIntoRefereeWagePayment_Log]", CommandType.StoredProcedure, parameters);
            return resualt;
        }


        public bool UpdateRefereeWagePayment_Log(int requestId = -1, int refereeType = -1, string term = "", int martabe = 0, string wage = "", bool chkPaymentDavar = false, bool isRejected = false)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RequestID",requestId ),
                new SqlParameter("@RefereeType",refereeType ),
                new SqlParameter("@Term",term ) ,
                new SqlParameter("@Martabe",martabe ) ,
                new SqlParameter("@Wage",wage ) ,
                new SqlParameter("@ChkPaymentDavar",chkPaymentDavar ) ,
                new SqlParameter("@IsRejected",isRejected )
            };
            var resualt = SqlDBHelper.ExecuteNonQuery("[Resource_Control].[SP_UpdateRefereeWagePayment_Log]", CommandType.StoredProcedure, parameters);
            return resualt;
        }




        public bool UpdateEducateProfessorRequest(bool isEducateProfessor, int requestId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@isEducateProfessor",isEducateProfessor ),
            new SqlParameter("@RequestId",requestId )
            };
            var resualt = SqlDBHelper.ExecuteScalar("[Resource_Control].[UpdateEducateProfessorRequest]", CommandType.StoredProcedure, parameters);
            return resualt == 1 ? true : false;
        }
        public void IsDeleteDefRequest(int reqId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@RequestId",reqId ),
            };
            SqlDBHelper.ExecuteNonQuery("[Resource_Control].[IsDeleteDefRequest]", CommandType.StoredProcedure, parameters);
        }
        public List<StudentDefenceRequestDTO> GetAllOnlineDefense()
        {
            List<StudentDefenceRequestDTO> requestlist = null;



            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[GetAllOnlineDefense]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    requestlist = new List<StudentDefenceRequestDTO>();

                    foreach (DataRow row in table.Rows)
                    {
                        StudentDefenceRequestDTO request = new StudentDefenceRequestDTO();
                        request.ID = Convert.ToInt32(row["ID"]);
                        request.subject = row["subject"] as string;
                        request.note = row["note"] as string;
                        request.answernote = row["answernote"] as string;
                        request.answer_time = row["answer_time"] as string;
                        request.issue_time = row["issue_time"] as string;
                        request.status = Convert.ToInt32(row["status"]);
                        request.issuerID = Convert.ToInt32(row["issuerID"]);
                        request.send_time = row["send_time"] as string;
                        //request.ResourceID = Convert.ToInt32(row["resourceID"]);
                        //request.Sessiondate = row["sessiondate"] as string;
                        //request.Sessionstart_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionstart_time"]));
                        //request.Sessionend_time = TimeSpan.FromTicks(Convert.ToInt64(row["sessionend_time"]));
                        request.catID = Convert.ToInt32(row["catID"]);
                        request.senderID = Convert.ToInt32(row["senderID"]);
                        request.replierID = Convert.ToInt32(row["replierID"]);
                        request.issuerName = row["issuerName"] as string;
                        //request.location = row["location_name"] as string;
                        request.capacity = Convert.ToInt32(row["capacity"]);
                        request.courseName = row["courseName"] as string;
                        request.courseID = Convert.ToInt32(row["courseID"]);
                        request.daneshID = Convert.ToInt32(row["daneshID"]);
                        request.DefenceSubject = row["DefenceSubject"] as string;
                        request.StudentCode = row["StudentCode"] as string;
                        request.StudentFullName = row["StudentFullName"] as string;
                        request.RequestDate = row["RequestDate"] as string;
                        request.StartTime = row["StartTime"] as string;
                        request.EndTime = row["EndTime"] as string;
                        if (!string.IsNullOrEmpty(row["OnlineFirstTeacherId"].ToString().Trim()))
                            request.OnlineFirstTeacherId = Convert.ToInt32(row["OnlineFirstTeacherId"]);
                        if (!string.IsNullOrEmpty(row["OnlineSecondTeacherId"].ToString().Trim()))
                            request.OnlineSecondTeacherId = Convert.ToInt32(row["OnlineSecondTeacherId"]);
                        request.OnlineTeacherRole = row["OnlineTeacherRole"].ToString();
                        if (row["IsEducateProfessor"] != null && row["IsEducateProfessor"] != DBNull.Value)
                            request.IsEducateProfessor = Convert.ToBoolean(row["IsEducateProfessor"]);
                        else
                            request.IsEducateProfessor = false;

                        requestlist.Add(request);
                    }
                }
            }

            return requestlist;
        }

        public DataTable IsConflictProfessor(string requestDate, long startTime, long endTime)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {

            new SqlParameter("@requestDate",requestDate ),
            new SqlParameter("@startTime",startTime ),
            new SqlParameter("@endTime",endTime )

            };
            var resualt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[IsConflictProfessor]", CommandType.StoredProcedure, parameters);
            return resualt;
        }
        public DataTable IsConflictProfessorForEducation(string requestDate, long startTime, long endTime, int reqId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {

            new SqlParameter("@requestDate",requestDate ),
            new SqlParameter("@startTime",startTime ),
            new SqlParameter("@endTime",endTime ),
            new SqlParameter("@reqId",reqId )

            };
            var resualt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[IsConflictProfessorForEducation]", CommandType.StoredProcedure, parameters);
            return resualt;
        }

        public DataTable GetAllAccepetedDefenceRequests(int daneshId = 0)
        {
            SqlParameter[] parameters = { new SqlParameter("@DaneshId", daneshId) };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetDefenceAcceptInformation]", CommandType.StoredProcedure, parameters);
        }

        public DataTable GetStudentDefenceListForPazhoohesh_Report(int isReport = 0, string term = null)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@isReport", isReport),
                new SqlParameter("@term", term)
            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[GetStudentDefenceListForPazhoohesh]", CommandType.StoredProcedure, parameters);
        }


        public DataTable GetDefenceAcceptInformationReports(int collegeId, string startDate, string endDate, int dateType)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@collegeId",collegeId),
                new SqlParameter("@StartDate",startDate),
                new SqlParameter("@endDate",endDate),
                new SqlParameter("@dateType",dateType)
            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_DefenceAcceptInformationReports]", CommandType.StoredProcedure, parameters);
        }

        public DataTable GetHeldDefenseMeetingReports(int collegeId, string startDate, string endDate, int dateType)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@collegeId",collegeId),
                new SqlParameter("@StartDate",startDate),
                new SqlParameter("@endDate",endDate),
                new SqlParameter("@dateType",dateType)
            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_HeldDefenseMeetingReports]", CommandType.StoredProcedure, parameters);
        }

        public DataTable GetListOfOnlinePlayDefenceReports(int collegeId, string startDate, string endDate, int dateType)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@collegeId",collegeId),
                new SqlParameter("@StartDate",startDate),
                new SqlParameter("@endDate",endDate),
                new SqlParameter("@dateType",dateType)

            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetOnlinePlayDefenceReports]", CommandType.StoredProcedure, parameters);
        }

        public DataTable GetListOfOnlineProfessorDefenceReports(int collegeId, string startDate, string endDate, int dateType)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@collegeId",collegeId),
                new SqlParameter("@StartDate",startDate),
                new SqlParameter("@endDate",endDate),
                new SqlParameter("@dateType",dateType)
            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetOnlineProfessorDefenceReports]", CommandType.StoredProcedure, parameters);
        }

        public DataTable NumberOfDefensesRequestedReport(int collegeId, string startDate, string endDate, int dateType)
        {
            if (collegeId > 0)
            {
                SqlParameter[] parameters = {
                new SqlParameter("@collegeId",collegeId),
                new SqlParameter("@startDate",startDate),
                new SqlParameter("@endDate",endDate),
                new SqlParameter("@dateType",dateType)
                };
                return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[NumberOfDefensesRequestedReport]", CommandType.StoredProcedure, parameters);
            }
            else
            {
                var resualt = new DataTable();

                var collegeArray = new List<int>() { 1, 2, 3, 8 };
                foreach (var i in collegeArray)
                {
                    SqlParameter[] parameters = {
                    new SqlParameter("@collegeId",i),
                    new SqlParameter("@startDate",startDate),
                    new SqlParameter("@endDate",endDate),
                    new SqlParameter("@dateType",dateType)
                    };
                    var tempResualt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[NumberOfDefensesRequestedReport]", CommandType.StoredProcedure, parameters);
                    if (tempResualt != null && tempResualt.Rows.Count > 0)
                        resualt.Merge(tempResualt);
                }

                return resualt;
            }
        }
        public DataTable HeldStudentDefenseRequestReport(int collegeId, string startDate, string endDate, int dateType)
        {
            if (collegeId > 0)
            {
                SqlParameter[] parameters = {
                new SqlParameter("@collegeId",collegeId),
                new SqlParameter("@startDate",startDate),
                new SqlParameter("@endDate",endDate),
                new SqlParameter("@dateType",dateType)

                };
                return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[HeldStudentDefenseRequestReport]", CommandType.StoredProcedure, parameters);
            }
            else
            {
                var resualt = new DataTable();

                var collegeArray = new List<int>() { 1, 2, 3, 8 };
                foreach (var i in collegeArray)
                {
                    SqlParameter[] parameters = {
                    new SqlParameter("@collegeId",i),
                    new SqlParameter("@startDate",startDate),
                    new SqlParameter("@endDate",endDate),
                    new SqlParameter("@dateType",dateType)
                    };
                    var tempResualt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[HeldStudentDefenseRequestReport]", CommandType.StoredProcedure, parameters);
                    if (tempResualt != null && tempResualt.Rows.Count > 0)
                        resualt.Merge(tempResualt);
                }

                return resualt;
            }
        }

        public DataTable GetOnlinePlayDefenceReports(int collegeId, string startDate, string endDate, int dateType)
        {
            if (collegeId > 0)
            {
                SqlParameter[] parameters = {
                new SqlParameter("@collegeId",collegeId),
                new SqlParameter("@startDate",startDate),
                new SqlParameter("@endDate",endDate),
                new SqlParameter("@dateType",dateType)

                };
                return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[OnlinePlayDefenceReports]", CommandType.StoredProcedure, parameters);
            }
            else
            {
                var resualt = new DataTable();

                var collegeArray = new List<int>() { 1, 2, 3, 8 };
                foreach (var i in collegeArray)
                {
                    SqlParameter[] parameters = {
                    new SqlParameter("@collegeId",i),
                    new SqlParameter("@startDate",startDate),
                    new SqlParameter("@endDate",endDate),
                    new SqlParameter("@dateType",dateType)

                    };
                    var tempResualt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[OnlinePlayDefenceReports]", CommandType.StoredProcedure, parameters);
                    if (tempResualt != null && tempResualt.Rows.Count > 0)
                        resualt.Merge(tempResualt);
                }

                return resualt;
            }
        }

        public DataTable GetOnlineProfessorDefenceReports(int collegeId, string startDate, string endDate, int dateType)
        {
            if (collegeId > 0)
            {
                SqlParameter[] parameters = {
                new SqlParameter("@collegeId",collegeId),
                new SqlParameter("@startDate",startDate),
                new SqlParameter("@endDate",endDate),
                new SqlParameter("@dateType",dateType)

                };
                return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[OnlineProfessorDefenceReports]", CommandType.StoredProcedure, parameters);
            }
            else
            {
                var resualt = new DataTable();

                var collegeArray = new List<int>() { 1, 2, 3, 8 };
                foreach (var i in collegeArray)
                {
                    SqlParameter[] parameters = {
                    new SqlParameter("@collegeId",i),
                    new SqlParameter("@startDate",startDate),
                    new SqlParameter("@endDate",endDate),
                    new SqlParameter("@dateType",dateType)
                    };
                    var tempResualt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[OnlineProfessorDefenceReports]", CommandType.StoredProcedure, parameters);
                    if (tempResualt != null && tempResualt.Rows.Count > 0)
                        resualt.Merge(tempResualt);
                }

                return resualt;
            }
        }

        public DataTable GetAllAccepetedDefenceRequests2(int daneshId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@DaneshId",daneshId),
            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetDefenceAcceptInformation2]", CommandType.StoredProcedure, parameters);

        }




        public DataTable GetStRegisterd2(string stcode)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@stcode",stcode),
            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("Resource_Control.SP_GetstudentInDefenceTerm", CommandType.StoredProcedure, parameters);


        }


        public DataTable FindBedehkarForReserve(string stcode)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@stcode",stcode),
            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_FindBedehkarForReserve]", CommandType.StoredProcedure, parameters);


        }
        public DataTable GetFinancialPermissionCondition(string stcode)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@stcode",stcode),
            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetFinancialPermissionCondition]", CommandType.StoredProcedure, parameters);
        }
        public bool HasFinancialPermission(string stcode)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@stcode",stcode),
            };
            var resualt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_HasFinancialPermission]", CommandType.StoredProcedure, parameters);
            var r = false;
            if (resualt.Rows.Count > 0)
                r = Convert.ToBoolean(resualt.Rows[0]["Permission"]);
            return r;
        }


        public bool ISOnlineRequest(int requestId)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@requestId",requestId),
            };
            var resualt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_IsOnlineRequest]", CommandType.StoredProcedure, parameters);
            if (Convert.ToInt32(resualt.Rows[0][0]) > 0)
                return true;
            else
                return false;
        }

        public DataTable GetStudentDefenceRequests(string studentCode)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@studentCode",studentCode),
            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[GetStudentDefenceRequests]", CommandType.StoredProcedure, parameters);





        }

        public DataTable HasPreventedFromCurrentDate(string requestDate, bool applicant)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@requestDate",requestDate),
                new SqlParameter("@applicant",applicant)
            };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetAvoidedDate]", CommandType.StoredProcedure, parameters);
        }

        public List<string> GetRefereeParticipatingOtherDefensesSameDate(string refereeIn, string refereeOut, string requestDate, int requestId = 0)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@requestDate",requestDate),
                new SqlParameter("@RefereeIn", refereeIn ?? "000"),
                new SqlParameter("@RefereeOut", refereeOut ?? "000"),
                new SqlParameter("@requestId", requestId),
            };
            var dtResualt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[GetDavInAndOutInSpecificDate]",
                CommandType.StoredProcedure, parameters);

            var resualt = new List<string>();

            if (dtResualt.Rows.Count > 0)
            {
                resualt.AddRange(from DataRow row in dtResualt.Rows where (string)row[0] != "0" select row[0].ToString());
            }
            return resualt;
        }

        public DataTable GetAvoidTime()
        {
            return SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[GetAvoidDate]", CommandType.StoredProcedure);
        }


        public decimal GetTotalAverageByStudentCode(string stdcode)
        {
            SqlParameter[] parameters =
            {
                 new SqlParameter("@stcode",stdcode)
            };

            var result = SqlDBHelper.ExecuteScalarValue("Resource_Control.SP_GetTotalAverageByStudentCode", CommandType.StoredProcedure, parameters);
            return result;
        }

        //sadegh saryazdy
        public DataTable GetMeetingDefencesbyCollegeId(int collegeId = -1, string date = "-1")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetMeetingDefencesbyCollegeId]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@CollegeId", collegeId));
                    cmd.Parameters.Add(new SqlParameter("@dateNow", date));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetMeetingDefencesbyStcode(string stcode, string date = "-1")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetMeetingDefencesbystcode]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));
                    cmd.Parameters.Add(new SqlParameter("@dateNow", date));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetMeetingDefencesAStudentbyStcode(string stcode, string date = "-1")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetMeetingDefencesAStudentByStcode]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));
                    cmd.Parameters.Add(new SqlParameter("@dateNow", date));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetMeetingDefencesbybyOscode(string oscode, string date = "-1")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetMeetingDefencesbyOscode]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@oscode", oscode));
                    cmd.Parameters.Add(new SqlParameter("@dateNow", date));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdeteDefence_LinkMeeting(int reqId, string link)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_UpdeteDefence_LinkMeeting]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@reqId", reqId));
                    cmd.Parameters.Add(new SqlParameter("@link", link));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateDateTime_DefenceMeeting(string idDefence, string date, string startTime, string endDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_UpdateDateTime_DefenceMeeting]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@idDefence", idDefence));
                    cmd.Parameters.Add(new SqlParameter("@date", date));
                    cmd.Parameters.Add(new SqlParameter("@startTime", startTime));
                    cmd.Parameters.Add(new SqlParameter("@endTime", endDate));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateFlagMeeting_DefenceMeeting(int reqId, bool flagStartMeeting, bool flagEndMeeting)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_UpdeteDefence_FlagMeeting]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@reqId", reqId));
                    cmd.Parameters.Add(new SqlParameter("@FlagStartMeeting", flagStartMeeting));
                    cmd.Parameters.Add(new SqlParameter("@FlagEndMeeting", flagEndMeeting));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAllCollge()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetAllCollege]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetStudentInformationByStcode(string stcode = "-1")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetStudentInformation]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetTypeDefenceMeetingOnline(int typeId = -1)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetTypeDefenceMeetingOnline]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@typeID", typeId));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetDefenceMeetingsOnline(string stcode = "-1")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetDefenceMeetingsOnline]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert_StudentsAllowDefenceMeetingOnline(string stcode, int typeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_InsertStudentsAllowDefenceMeetingOnline]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));
                    cmd.Parameters.Add(new SqlParameter("@TypeDefenceMeetingId", typeId));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable CheckRequestDefenceStudent(string stcode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_CheckRequestDefenceStudent]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Delete_StudentsAllowDefenceMeetingOnline(string stcode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_DeleteAllowDefenceMeetingOnlineForStudent]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetStudentsDefenceforAOstad(string userId)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetStudentsDefenceForOstad]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@User", userId));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateFlagReject_DefenceMeeting(int requestId, int idTypeOstad, bool flagAccept)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_UpdeteDefence_FlagReject]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@requestId", requestId));
                    cmd.Parameters.Add(new SqlParameter("@idTypeOstad", idTypeOstad));
                    cmd.Parameters.Add(new SqlParameter("@flagAccept", flagAccept));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;


                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetAssistanceDefence(string stcode, int status = -1)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_GetAssistanceDefence]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));
                    cmd.Parameters.Add(new SqlParameter("@status", status));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert_AssistanceDefence(string stcode, string rDate, string rTime)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_InsertAssistanceDefence]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));
                    cmd.Parameters.Add(new SqlParameter("@rDate", rDate));
                    cmd.Parameters.Add(new SqlParameter("@rTime", rTime));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Update_AssistanceDefence(string stcode, int status, string msgAnswer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_UpdateAssistanceDefence]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));
                    cmd.Parameters.Add(new SqlParameter("@status", status));
                    cmd.Parameters.Add(new SqlParameter("@msgAnswer ", msgAnswer));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return dt.Rows[0][0].ToString() == "ok" ? true : false;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetLogMeetingDefences(string stcode = "-1")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_GetLogMeetingDefences]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int GetCountMeetingDefencesRejectByOstad(string stcode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetCountMeetingDefencesRejectByOstad]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stcode));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return int.Parse(dt.Rows[0][0].ToString());

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Insert_SpecialDescription(string startDate, string endDate, string dsc, bool IsForStudent, bool IsForEmployee, bool flagTatili)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_InsertSpecialDescription]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@StartDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@EndDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@Description", dsc));
                    cmd.Parameters.Add(new SqlParameter("@IsForStudent", IsForStudent));
                    cmd.Parameters.Add(new SqlParameter("@IsForEmployee", IsForEmployee));
                    cmd.Parameters.Add(new SqlParameter("@FlagTatili", flagTatili));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetSpecialDescription(int flagTatili = -1)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_GetSpecialDescription]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@flagTatili", flagTatili));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteSpecialDescription(int id)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_DeleteSpecialDescription]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable CheckDefenceInformationByRequestDate(string startDate, string endDate, string startTime = "-1", string justForOnline = "-1")
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_CheckDefenceInformationByRequestDate]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@startTime", startTime));
                    cmd.Parameters.Add(new SqlParameter("@justForOnline", justForOnline));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable CheckExistSpecialDate(string startDate, string endDate, int flagTatili = -1)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_CheckSpecialDescriptionByDate]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@flagTatili", flagTatili));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable InsertAvoidDate(string startDate, string endDate, string Description, bool isForStudent, bool isForEmployee)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_InsertAvoidDate]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@Description", Description));
                    cmd.Parameters.Add(new SqlParameter("@IsForEmployee", isForStudent));
                    cmd.Parameters.Add(new SqlParameter("@IsForStudent", isForEmployee));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataTable GetAvoidDate(string startDate = "-1", string endDate = "-1")
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_GetAvoidDate]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAvoidDate(int id)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_DeleteAvoidDate]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateAvoidDate(int id, string startDate, string endDate, string dsc, bool isForEmployee, bool isForStudent)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_UpdateAvoidDate]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@description", dsc));
                    cmd.Parameters.Add(new SqlParameter("@isForEmployee", isForEmployee));
                    cmd.Parameters.Add(new SqlParameter("@IsForStudent", isForStudent));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateSpecialDate(int id, string startDate, string endDate, string dsc, bool isForEmployee, bool isForStudent)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_updateSpecialDescription]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@description", dsc));
                    cmd.Parameters.Add(new SqlParameter("@isForEmployee", isForEmployee));
                    cmd.Parameters.Add(new SqlParameter("@IsForStudent", isForStudent));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetAvoidTime(string startDate = "-1", string endDate = "-1", string time = "-1", int justForOnline = -1)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_GetAvoidTime]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@justForOnline", justForOnline));
                    cmd.Parameters.Add(new SqlParameter("@time", time));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertAvoidTime(string startDate, string endDate, string time, bool justForOnline)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_InsertAvoidTime]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@time", time));
                    cmd.Parameters.Add(new SqlParameter("@JustForOnline", justForOnline));


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAvoidTime(string time, string startDate, string endDate
                                   , bool justForOnline)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_DeleteAvoidTime]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@time", time));
                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@justForOnline", justForOnline));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetEducationCalender(int id = -1, string term = "-1", int appId = -1)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_GetEducationCalender]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@term", term));
                    cmd.Parameters.Add(new SqlParameter("@appId", appId));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertEducationCalender(string startDate, string endDate, string term, int calenderType)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_InsertEducationCalender]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@calenderType", calenderType));
                    cmd.Parameters.Add(new SqlParameter("@term", term));


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateEducationCalender(int id, int calenderType, string startDate, string endDate, string term)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[Sp_UpdateEducationCalender]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@term", term));
                    cmd.Parameters.Add(new SqlParameter("@calenderType", calenderType));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataTable GetResource(int catId)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_GetResource]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@categoryID", catId));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertResourece_College_junc(string startDate, string endDate, int CollegeId, int resourceId, int isShared)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_InsertResourece_College_junc]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@CollegeId", CollegeId));
                    cmd.Parameters.Add(new SqlParameter("@resourceId", resourceId));
                    cmd.Parameters.Add(new SqlParameter("@isShared", isShared));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetResourece_College_junc(string startDate = "-1", string endDate = "-1", int CollegeId = -1, int resourceId = -1, int isShared = -1)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_GetResourece_College_junc]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@CollegeId", CollegeId));
                    cmd.Parameters.Add(new SqlParameter("@resourceId", resourceId));
                    cmd.Parameters.Add(new SqlParameter("@isShared", isShared));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetRequestDateTime(string startDate = "-1", string endDate = "-1", int resourceId = -1, int collegeId = -1)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_GetRequestDateTime]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@resourceId", resourceId));
                    cmd.Parameters.Add(new SqlParameter("@collegeId", collegeId));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateResourece_College_junc(int id, string startDate, string endDate, int resourceId
            , int collegeId, int IsShared)

        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_UpdateResourece_College_junc]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@startDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@collegeId", collegeId));
                    cmd.Parameters.Add(new SqlParameter("@resourceId", resourceId));
                    cmd.Parameters.Add(new SqlParameter("@IsShared", IsShared));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteResourece_College_junc(int id)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[sp_DeleteResourece_College_junc]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataTable GetTeachersPaymentTransportationHasNotDown(string term = null)
        {

            using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetRefereeTeachersPaymentTransportationHasNotDown]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@term", term));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();

                return dt;
            }

        }
        public bool InsertRefereeWageTransportationPayment(int id_Os, string date, string wage,
                                                            string dsc, string term, int status)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_InsertRefereeWageTransportationPayment]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@Id_Os", id_Os));
                    cmd.Parameters.Add(new SqlParameter("@Date", date));
                    cmd.Parameters.Add(new SqlParameter("@Wage", wage));

                    cmd.Parameters.Add(new SqlParameter("@status", status));
                    cmd.Parameters.Add(new SqlParameter("@term", term));
                    cmd.Parameters.Add(new SqlParameter("@dsc", dsc));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetTeachersPaymentTransportationHasDown(string term = null)
        {

            using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetRefereeWageTransportationPaymentHasDown]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@term", term));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();

                return dt;
            }
        }
        public DataTable GetTeachersPaymentTransportation(int id_os, string date)
        {

            using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetRefereeWageTransportationPayment]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@date", date));
                cmd.Parameters.Add(new SqlParameter("@id_os", id_os));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();

                return dt;
            }
        }
        public bool DeleteRefereeWageTransportationPayment(int id_Os, string date)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_DeleteTransportationPayment]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@Id_Os", id_Os));
                    cmd.Parameters.Add(new SqlParameter("@Date", date));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRefereeWageTransportationPayment(int id_Os, string date, string wage,
                                                          string dsc, string term, int status)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_UpdateRefereeWageTransportationPayment]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@Id_Os", id_Os));
                    cmd.Parameters.Add(new SqlParameter("@Date", date));
                    cmd.Parameters.Add(new SqlParameter("@Wage", wage));

                    cmd.Parameters.Add(new SqlParameter("@status", status));
                    cmd.Parameters.Add(new SqlParameter("@term", term));
                    cmd.Parameters.Add(new SqlParameter("@dsc", dsc));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable GetTeacherSignature(long osCode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
            {
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand("", conn);
                    comm.CommandText = "faculty.SP_getTeacherSignatureByOsCode";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@osCode", osCode);


                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlDataReader dr = comm.ExecuteReader();
                    dt.Load(dr);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
                return dt;
            }


        }
        public DataTable GetRefereeTeachersPaymentTransportation_Report(int isPayedWage = 0, int reportType = 0, string term = null)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Paymented", isPayedWage),
                new SqlParameter("@ReportType", reportType),
                new SqlParameter("@Term", term)
            };

            var dt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetRefereeTeachersPaymentTransportation_Report]", CommandType.StoredProcedure, parameters);
            return dt;
        }

        public DataTable GetRequestIdAcceptedByStcode(string stcode)
        {

            using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetRequestIdAcceptedByStcode]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@stcode", stcode));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();

                return dt;
            }

        }
        public bool UpdateScoreForDefence(ScoreDefence score)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_UpdateScoreDefence]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@requestId", score.RequestId));
                    cmd.Parameters.Add(new SqlParameter("@score", score.Score));
                    cmd.Parameters.Add(new SqlParameter("@flagAcceptScoreDavin", score.FlagAcceptScoreDavin == null || score.FlagAcceptScoreDavin == false ? 0 : 1));
                    cmd.Parameters.Add(new SqlParameter("@flagAcceptScoreDavOut", score.FlagAcceptScoreDavOut == null || score.FlagAcceptScoreDavOut == false ? 0 : 1));

                    cmd.Parameters.Add(new SqlParameter("@flagAcceptScoreMosh1", score.FlagAcceptScoreMosh1 == null || score.FlagAcceptScoreMosh1 == false ? 0 : 1));
                    cmd.Parameters.Add(new SqlParameter("@flagAcceptScoreMosh2", score.FlagAcceptScoreMosh2 == null || score.FlagAcceptScoreMosh2 == false ? 0 : 1));
                    cmd.Parameters.Add(new SqlParameter("@flagAcceptScoreRah1", score.FlagAcceptScoreRah1 == null || score.FlagAcceptScoreRah1 == false ? 0 : 1));
                    cmd.Parameters.Add(new SqlParameter("@flagAcceptScoreRah2", score.FlagAcceptScoreRah2 == null || score.FlagAcceptScoreRah2 == false ? 0 : 1));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ScoreDefence SelectScoreForDefence(int reqId)
        {
            using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_SelectScoreDefence]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@reqId", reqId));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ScoreDefence score = new ScoreDefence();
                da.Fill(dt);
                da.Dispose();
                if (dt != null && dt.Rows.Count > 0)
                {
                    decimal number;
                    score.Stcode = dt.Rows[0]["stcode"].ToString();
                    score.FullName = dt.Rows[0]["FullName"].ToString();
                    score.RequestDate = dt.Rows[0]["RequestDate"].ToString();
                    score.RequestId = int.Parse(dt.Rows[0]["RequestId"].ToString());
                    score.Score = decimal.TryParse(dt.Rows[0]["Score"].ToString(), out number) == false ? -1 : number;
                    score.ScoreLetters = dt.Rows[0]["ScoreLetters"].ToString();
                    score.FlagAcceptScoreDavin = dt.Rows[0]["FlagAcceptScoreDavin"].ToString().ToLower() != "true" ? false : true;
                    score.FlagAcceptScoreDavOut = dt.Rows[0]["FlagAcceptScoreDavOut"].ToString().ToLower() != "true" ? false : true;
                    score.FlagAcceptScoreMosh1 = dt.Rows[0]["FlagAcceptScoreMosh1"].ToString().ToLower() != "true" ? false : true;
                    score.FlagAcceptScoreMosh2 = dt.Rows[0]["FlagAcceptScoreMosh2"].ToString().ToLower() != "true" ? false : true;
                    score.FlagAcceptScoreRah1 = dt.Rows[0]["FlagAcceptScoreRah1"].ToString().ToLower() != "true" ? false : true;
                    score.FlagAcceptScoreRah2 = dt.Rows[0]["FlagAcceptScoreRah2"].ToString().ToLower() != "true" ? false : true;
                }
                return score;
            }

        }
        public InformationOstadForDefenceStudent SelectSignModirGroup(string stCode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("[Resource_Control].[GetSignModirGroup]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@stcode", stCode));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var signOstad = new InformationOstadForDefenceStudent();

                        signOstad.FullName = dt.Rows[0]["FullName"].ToString();
                        signOstad.Id = dt.Rows[0]["id"].ToString();
                        signOstad.Mobile = dt.Rows[0]["Mobile"].ToString();
                        signOstad.TypeOS = dt.Rows[0]["TypeOS"].ToString();
                        signOstad.AddressSignature = dt.Rows[0]["AddressSignature"].ToString();
                        return signOstad;
                    }
                    else return new InformationOstadForDefenceStudent();
                }
            }
            catch (Exception e)
            {

                return new InformationOstadForDefenceStudent();
            }
        }

        public StatusDefenceTechnichal SelectStatusDefenceTechnical(int reqId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_GetStatusDefenceTechnical]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@reqId", reqId));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var statusTechnichal = new StatusDefenceTechnichal();

                        statusTechnichal.RequestId = int.Parse(dt.Rows[0]["RequestId"].ToString());
                        statusTechnichal.StudentCode = int.Parse(dt.Rows[0]["StudentCode"].ToString());

                        statusTechnichal.FlagAcceptTechnicalDavOut = dt.Rows[0]["FlagAcceptTechnicalDavOut"].ToString() == "" ? null : (bool?)(dt.Rows[0]["FlagAcceptTechnicalDavOut"]);
                        statusTechnichal.FlagAcceptTechnicalDavIn = dt.Rows[0]["FlagAcceptTechnicalDavIn"].ToString() == "" ? null : (bool?)(dt.Rows[0]["FlagAcceptTechnicalDavIn"]);
                        statusTechnichal.FlagAcceptTechnicalMosh1 = dt.Rows[0]["FlagAcceptTechnicalMosh1"].ToString() == "" ? null : (bool?)(dt.Rows[0]["FlagAcceptTechnicalMosh1"]);
                        statusTechnichal.FlagAcceptTechnicalMosh2 = dt.Rows[0]["FlagAcceptTechnicalMosh2"].ToString() == "" ? null : (bool?)(dt.Rows[0]["FlagAcceptTechnicalMosh2"]);
                        statusTechnichal.FlagAcceptTechnicalRah1 = dt.Rows[0]["FlagAcceptTechnicalRah1"].ToString() == "" ? null : (bool?)(dt.Rows[0]["FlagAcceptTechnicalRah1"]);
                        statusTechnichal.FlagAcceptTechnicalRah2 = dt.Rows[0]["FlagAcceptTechnicalRah2"].ToString() == "" ? null : (bool?)(dt.Rows[0]["FlagAcceptTechnicalRah2"]);
                        statusTechnichal.FlagAcceptTechnicalStudent = dt.Rows[0]["FlagAcceptTechnicalStudent"].ToString() == "" ? null : (bool?)(dt.Rows[0]["FlagAcceptTechnicalStudent"]);

                        statusTechnichal.DateReasonTechnicalDavOut = dt.Rows[0]["DateReasonTechnicalDavOut"].ToString() == "" ? null : (string)(dt.Rows[0]["DateReasonTechnicalDavOut"]);
                        statusTechnichal.DateReasonTechnicalDavin = dt.Rows[0]["DateReasonTechnicalDavin"].ToString() == "" ? null : (string)(dt.Rows[0]["DateReasonTechnicalDavin"]);
                        statusTechnichal.DateReasonTechnicalMosh1 = dt.Rows[0]["DateReasonTechnicalMosh1"].ToString() == "" ? null : (string)(dt.Rows[0]["DateReasonTechnicalMosh1"]);
                        statusTechnichal.DateReasonTechnicalMosh2 = dt.Rows[0]["DateReasonTechnicalMosh2"].ToString() == "" ? null : (string)(dt.Rows[0]["DateReasonTechnicalMosh2"]);
                        statusTechnichal.DateReasonTechnicalRah1 = dt.Rows[0]["DateReasonTechnicalRah1"].ToString() == "" ? null : (string)(dt.Rows[0]["DateReasonTechnicalRah1"]);
                        statusTechnichal.DateReasonTechnicalRah2 = dt.Rows[0]["DateReasonTechnicalRah2"].ToString() == "" ? null : (string)(dt.Rows[0]["DateReasonTechnicalRah2"]);
                        statusTechnichal.DateReasonTechnicalStudent = dt.Rows[0]["DateReasonTechnicalStudent"].ToString() == "" ? null : (string)(dt.Rows[0]["DateReasonTechnicalStudent"]);

                        statusTechnichal.ReasonTechnicalDavOut = dt.Rows[0]["ReasonTechnicalDavOut"].ToString() == "" ? null : (string)(dt.Rows[0]["ReasonTechnicalDavOut"]);
                        statusTechnichal.ReasonTechnicalDavin = dt.Rows[0]["ReasonTechnicalDavin"].ToString() == "" ? null : (string)(dt.Rows[0]["ReasonTechnicalDavin"]);
                        statusTechnichal.ReasonTechnicalMosh1 = dt.Rows[0]["ReasonTechnicalMosh1"].ToString() == "" ? null : (string)(dt.Rows[0]["ReasonTechnicalMosh1"]);
                        statusTechnichal.ReasonTechnicalMosh2 = dt.Rows[0]["ReasonTechnicalMosh2"].ToString() == "" ? null : (string)(dt.Rows[0]["ReasonTechnicalMosh2"]);
                        statusTechnichal.ReasonTechnicalRah1 = dt.Rows[0]["ReasonTechnicalRah1"].ToString() == "" ? null : (string)(dt.Rows[0]["ReasonTechnicalRah1"]);
                        statusTechnichal.ReasonTechnicalRah2 = dt.Rows[0]["ReasonTechnicalRah2"].ToString() == "" ? null : (string)(dt.Rows[0]["ReasonTechnicalRah2"]);
                        statusTechnichal.ReasonTechnicalStudent = dt.Rows[0]["ReasonTechnicalStudent"].ToString() == "" ? null : (string)(dt.Rows[0]["ReasonTechnicalStudent"]);
                        return statusTechnichal;
                    }
                    return new StatusDefenceTechnichal();
                }
            }
            catch (Exception e)
            {

                return new StatusDefenceTechnichal();
            }
        }
        public bool updateStatusDefenceTechnical(StatusDefenceTechnichal statusDefence)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_UpdateStatusDefenceTechnical]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@requestId", statusDefence.RequestId));
                    cmd.Parameters.Add(new SqlParameter("@flagAcceptTechnicalDavOut", statusDefence.FlagAcceptTechnicalDavOut == null ? (object)DBNull.Value : statusDefence.FlagAcceptTechnicalDavOut));
                    cmd.Parameters.Add(new SqlParameter("@dateReasonTechnicalDavOut", statusDefence.DateReasonTechnicalDavOut == null ? (object)DBNull.Value : statusDefence.DateReasonTechnicalDavOut));
                    cmd.Parameters.Add(new SqlParameter("@reasonTechnicalDavOut", statusDefence.ReasonTechnicalDavOut == null ? (object)DBNull.Value : statusDefence.ReasonTechnicalDavOut));

                    cmd.Parameters.Add(new SqlParameter("@flagAcceptTechnicalDavIn", statusDefence.FlagAcceptTechnicalDavIn == null ? (object)DBNull.Value : statusDefence.FlagAcceptTechnicalDavIn));
                    cmd.Parameters.Add(new SqlParameter("@dateReasonTechnicalDavin", statusDefence.DateReasonTechnicalDavin == null ? (object)DBNull.Value : statusDefence.DateReasonTechnicalDavin));
                    cmd.Parameters.Add(new SqlParameter("@reasonTechnicalDavin", statusDefence.ReasonTechnicalDavin == null ? (object)DBNull.Value : statusDefence.ReasonTechnicalDavin));


                    cmd.Parameters.Add(new SqlParameter("@flagAcceptTechnicalMosh1", statusDefence.FlagAcceptTechnicalMosh1 == null ? (object)DBNull.Value : statusDefence.FlagAcceptTechnicalMosh1));
                    cmd.Parameters.Add(new SqlParameter("@dateReasonTechnicalMosh1", statusDefence.DateReasonTechnicalMosh1 == null ? (object)DBNull.Value : statusDefence.DateReasonTechnicalMosh1));
                    cmd.Parameters.Add(new SqlParameter("@reasonTechnicalMosh1", statusDefence.ReasonTechnicalMosh1 == null ? (object)DBNull.Value : statusDefence.ReasonTechnicalMosh1));


                    cmd.Parameters.Add(new SqlParameter("@flagAcceptTechnicalMosh2", statusDefence.FlagAcceptTechnicalMosh2 == null ? (object)DBNull.Value : statusDefence.FlagAcceptTechnicalMosh2));
                    cmd.Parameters.Add(new SqlParameter("@dateReasonTechnicalMosh2", statusDefence.DateReasonTechnicalMosh2 == null ? (object)DBNull.Value : statusDefence.DateReasonTechnicalMosh2));
                    cmd.Parameters.Add(new SqlParameter("@reasonTechnicalMosh2", statusDefence.ReasonTechnicalMosh2 == null ? (object)DBNull.Value : statusDefence.ReasonTechnicalMosh2));


                    cmd.Parameters.Add(new SqlParameter("@flagAcceptTechnicalRah1", statusDefence.FlagAcceptTechnicalRah1 == null ? (object)DBNull.Value : statusDefence.FlagAcceptTechnicalRah1));
                    cmd.Parameters.Add(new SqlParameter("@dateReasonTechnicalRah1", statusDefence.DateReasonTechnicalRah1 == null ? (object)DBNull.Value : statusDefence.DateReasonTechnicalRah1));
                    cmd.Parameters.Add(new SqlParameter("@reasonTechnicalRah1", statusDefence.ReasonTechnicalRah1 == null ? (object)DBNull.Value : statusDefence.ReasonTechnicalRah1));


                    cmd.Parameters.Add(new SqlParameter("@flagAcceptTechnicalRah2", statusDefence.FlagAcceptTechnicalRah2 == null ? (object)DBNull.Value : statusDefence.FlagAcceptTechnicalRah2));
                    cmd.Parameters.Add(new SqlParameter("@dateReasonTechnicalRah2", statusDefence.DateReasonTechnicalRah2 == null ? (object)DBNull.Value : statusDefence.DateReasonTechnicalRah2));
                    cmd.Parameters.Add(new SqlParameter("@reasonTechnicalRah2", statusDefence.ReasonTechnicalRah2 == null ? (object)DBNull.Value : statusDefence.ReasonTechnicalRah2));

                    cmd.Parameters.Add(new SqlParameter("@flagAcceptTechnicalStudent", statusDefence.FlagAcceptTechnicalStudent == null ? (object)DBNull.Value : statusDefence.FlagAcceptTechnicalStudent));
                    cmd.Parameters.Add(new SqlParameter("@dateReasonTechnicalStudent", statusDefence.DateReasonTechnicalStudent == null ? (object)DBNull.Value : statusDefence.DateReasonTechnicalStudent));
                    cmd.Parameters.Add(new SqlParameter("@reasonTechnicalStudent", statusDefence.ReasonTechnicalStudent == null ? (object)DBNull.Value : statusDefence.ReasonTechnicalStudent));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt.Rows[0][0].ToString() == "ok" ? true : false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool UpdateIsRejectFinancial(int reqId, bool flag)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[UpdateIsRejectFinancial]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                  
            {
             cmd.Parameters.Add(    new SqlParameter("@reqId",reqId));
                 cmd.Parameters.Add(new SqlParameter("@flag", flag));

            };


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return dt.Rows[0][0].ToString() == "ok" ? true : false;

                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool UpdateSendSmsFlagFinancial(int reqId, bool flag,string msg)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[Resource_Control].[UpdateSendSmsFlagFinancial]", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    {
                        cmd.Parameters.Add(new SqlParameter("@reqId", reqId));
                        cmd.Parameters.Add(new SqlParameter("@flag", flag));
                        cmd.Parameters.Add(new SqlParameter("@msg", msg));
                    };


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();

                    return dt.Rows[0][0].ToString() == "ok" ? true : false;

                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public DataTable GetAllAccepetedDefenceRequestsForFinaical(int daneshId = 0)
        {
            SqlParameter[] parameters = { new SqlParameter("@DaneshId", daneshId) };
            return SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_GetDefenceAcceptInformationForFinaical]", CommandType.StoredProcedure, parameters);
        }

        //public int UpdateFlagMeetingForTime()
        //{
        //    using (SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("[Resource_Control].[SP_UpdateDefence]", conn)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        cmd.Parameters.Add(new SqlParameter("@reqId", reqId));

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        ScoreDefence score = new ScoreDefence();
        //        da.Fill(dt);
        //        da.Dispose();
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            decimal number;
        //            score.Stcode = dt.Rows[0]["stcode"].ToString();
        //            score.FullName = dt.Rows[0]["FullName"].ToString();
        //            score.RequestDate = dt.Rows[0]["RequestDate"].ToString();
        //            score.RequestId = int.Parse(dt.Rows[0]["RequestId"].ToString());
        //            score.Score = decimal.TryParse(dt.Rows[0]["Score"].ToString(), out number) == false ? -1 : number;
        //            score.ScoreLetters = dt.Rows[0]["ScoreLetters"].ToString();
        //            score.FlagAcceptScoreDavin = dt.Rows[0]["FlagAcceptScoreDavin"].ToString().ToLower() != "true" ? false : true;
        //            score.FlagAcceptScoreDavOut = dt.Rows[0]["FlagAcceptScoreDavOut"].ToString().ToLower() != "true" ? false : true;
        //            score.FlagAcceptScoreMosh1 = dt.Rows[0]["FlagAcceptScoreMosh1"].ToString().ToLower() != "true" ? false : true;
        //            score.FlagAcceptScoreMosh2 = dt.Rows[0]["FlagAcceptScoreMosh2"].ToString().ToLower() != "true" ? false : true;
        //            score.FlagAcceptScoreRah1 = dt.Rows[0]["FlagAcceptScoreRah1"].ToString().ToLower() != "true" ? false : true;
        //            score.FlagAcceptScoreRah2 = dt.Rows[0]["FlagAcceptScoreRah2"].ToString().ToLower() != "true" ? false : true;
        //        }
        //        return score;
        //    }



        //    return 1;
        //}

    }
    }

