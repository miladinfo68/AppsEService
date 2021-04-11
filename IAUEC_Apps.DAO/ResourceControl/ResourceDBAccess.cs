using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ResourceControl.Entity;
using System.Data;
using System.Globalization;
using System.IO;

namespace ResourceControl.DAL
{
    public class ResourceDBAccess
    {
        public int AddNewResource(Resource resource)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", resource.Name),
                new SqlParameter("@location", resource.Location),
                new SqlParameter("@description", resource.Description),
                new SqlParameter("@categoryID",resource.CategoryID),
                new SqlParameter("@capacity",resource.Capacity)
            };
            return SqlDBHelper.ExecuteScalar("[Resource_Control].[sp_ResourceInsert]", CommandType.StoredProcedure, parameters);
        }

        public bool UpdateResource(Resource resource)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID",resource.ID),
                new SqlParameter("@name", resource.Name),
                new SqlParameter("@location",resource.Location),
                new SqlParameter("@description", resource.Description),
                new SqlParameter("@categoryID", resource.CategoryID),
                new SqlParameter("@IsDeleted",resource.IsDeleted),
                new SqlParameter("@capacity",resource.Capacity)

            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_ResourceUpdate]", CommandType.StoredProcedure, parameters);
        }

        //public bool DeleteResource(int resID)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@ID", resID)
        //    };

        //    return SqlDBHelper.ExecuteNonQuery("sp_ResourceDelete", CommandType.StoredProcedure, parameters);
        //}

        public Resource GetResourceDetails(int resID)
        {
            Resource resource = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", resID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_ResourceSelect]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    resource = new Resource();
                    resource.ID = Convert.ToInt32(row["ID"]);
                    resource.Name = row["name"] as string;
                    resource.Location = row["location_name"] as string;
                    resource.Description = row["description"] as string;
                    resource.CategoryID = Convert.ToInt32(row["categoryID"]);
                    resource.Capacity = Convert.ToInt32(row["capacity"]);
                }
            }

            return resource;
        }
        public List<Resource> GetResourceList()
        {
            List<Resource> resourcelist = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[sp_ResourceSelectAll]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    resourcelist = new List<Resource>();

                    foreach (DataRow row in table.Rows)
                    {
                        Resource resource = new Resource();
                        resource.ID = Convert.ToInt32(row["ID"]);
                        resource.Name = row["name"] as string;
                        resource.Location = row["location_name"] as string;
                        resource.Description = row["description"] as string;
                        resource.CategoryName = row["category"] as string;
                        resource.CategoryID = Convert.ToInt32(row["categoryID"]);
                        resource.Capacity = Convert.ToInt32(row["capacity"]);


                        resourcelist.Add(resource);
                    }
                }
            }

            return resourcelist;
        }

        public List<Resource> GetResourceListByCatID(int catID)
        {
            List<Resource> resourcelist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@catID", catID)
            };
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_ResourceSelectByCatID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    resourcelist = new List<Resource>();

                    foreach (DataRow row in table.Rows)
                    {
                        Resource resource = new Resource();
                        resource.ID = Convert.ToInt32(row["ID"]);
                        resource.Name = row["name"] as string;
                        resource.Location = row["location_name"] as string;
                        resource.Description = row["description"] as string;
                        resource.CategoryID = Convert.ToInt32(row["categoryID"]);
                        resource.Capacity = Convert.ToInt32(row["capacity"]);

                        resourcelist.Add(resource);
                    }
                }
            }

            return resourcelist;
        }

        public List<Resource> GetResourceListByCatLocation(int location)
        {
            List<Resource> resourcelist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@location", location)
            };
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_ResourceSelectByLocation]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    resourcelist = new List<Resource>();

                    foreach (DataRow row in table.Rows)
                    {
                        Resource resource = new Resource();
                        resource.ID = Convert.ToInt32(row["ID"]);
                        resource.Name = row["name"] as string;
                        resource.Location = row["location_name"] as string;
                        resource.Description = row["description"] as string;
                        resource.CategoryID = Convert.ToInt32(row["categoryID"]);
                        resource.Capacity = Convert.ToInt32(row["capacity"]);

                        resourcelist.Add(resource);
                    }
                }
            }

            return resourcelist;
        }
        public List<Resource> GetResourceListByCatIDandLocation(int catID, int location)
        {
            List<Resource> resourcelist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@catID", catID),
                new SqlParameter("@location", location)
            };
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_ResourceSelectByCatIDandLocation]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    resourcelist = new List<Resource>();

                    foreach (DataRow row in table.Rows)
                    {
                        Resource resource = new Resource();
                        resource.ID = Convert.ToInt32(row["ID"]);
                        resource.Name = row["name"] as string;
                        resource.Location = row["location_name"] as string;
                        resource.Description = row["description"] as string;
                        resource.CategoryID = Convert.ToInt32(row["categoryID"]);
                        resource.Capacity = Convert.ToInt32(row["capacity"]);
                        resource.IsDeleted = Convert.ToBoolean(row["IsDeleted"]);
                        resourcelist.Add(resource);
                    }
                }
            }

            return resourcelist;
        }

        public List<Resource> GetResourceListByReqID(int reqID)
        {
            List<Resource> resourcelist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@reqID", reqID)
            };
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_ResourceSelectByReqID]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    resourcelist = new List<Resource>();

                    foreach (DataRow row in table.Rows)
                    {
                        Resource resource = new Resource();
                        resource.ID = Convert.ToInt32(row["ID"]);
                        resource.Name = row["name"] as string;
                        resource.Location = row["location_name"] as string;
                        resource.Description = row["description"] as string;
                        resource.CategoryID = Convert.ToInt32(row["categoryID"]);
                        resource.Capacity = Convert.ToInt32(row["capacity"]);

                        resourcelist.Add(resource);
                    }
                }
            }
            return resourcelist;
        }
        public List<Resource> ResourceSelectByReqStudentID(int reqID)
        {
            List<Resource> resourcelist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@reqID", reqID)
            };
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_ResourceSelectByReqStudentID ]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    resourcelist = new List<Resource>();

                    foreach (DataRow row in table.Rows)
                    {
                        Resource resource = new Resource();
                        resource.ID = Convert.ToInt32(row["ID"]);
                        resource.Name = row["name"] as string;
                        resource.Location = row["location_name"] as string;
                        resource.Description = row["description"] as string;
                        resource.CategoryID = Convert.ToInt32(row["categoryID"]);
                        resource.Capacity = Convert.ToInt32(row["capacity"]);

                        resourcelist.Add(resource);
                    }
                }
            }
            return resourcelist;
        }

        public List<Resource> GetResourceListByStudentReqIDForAfterAccept(int reqID)
        {
            List<Resource> resourcelist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@reqID", reqID)
            };
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_ResourceSelectByStudentReqIDForAfterAccept]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    resourcelist = new List<Resource>();

                    foreach (DataRow row in table.Rows)
                    {
                        Resource resource = new Resource();
                        resource.ID = Convert.ToInt32(row["ID"]);
                        resource.Name = row["name"] as string;
                        resource.Location = row["location_name"] as string;
                        resource.Description = row["description"] as string;
                        resource.CategoryID = Convert.ToInt32(row["categoryID"]);
                        resource.Capacity = Convert.ToInt32(row["capacity"]);

                        resourcelist.Add(resource);
                    }
                }
            }
            return resourcelist;
        }
        public List<Resource> GetResourceListByReqIDForAfterAccept(int reqID)
        {
            List<Resource> resourcelist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@reqID", reqID)
            };
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_ResourceSelectByReqIDForAfterAccept]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    resourcelist = new List<Resource>();

                    foreach (DataRow row in table.Rows)
                    {
                        Resource resource = new Resource();
                        resource.ID = Convert.ToInt32(row["ID"]);
                        resource.Name = row["name"] as string;
                        resource.Location = row["location_name"] as string;
                        resource.Description = row["description"] as string;
                        resource.CategoryID = Convert.ToInt32(row["categoryID"]);
                        resource.Capacity = Convert.ToInt32(row["capacity"]);

                        resourcelist.Add(resource);
                    }
                }
            }
            return resourcelist;
        }

        public List<Resource> GetResourceListByLocationIdandCatId(int locId, int catId)
        {
            List<Resource> resourcelist = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@locId", locId),
                new SqlParameter("catId",catId)
            };
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetResourceListByLocationIdandCatId]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    resourcelist = new List<Resource>();

                    foreach (DataRow row in table.Rows)
                    {
                        Resource resource = new Resource();
                        resource.ID = Convert.ToInt32(row["ID"]);
                        resource.Name = row["name"] as string;
                        resource.Location = row["location_name"] as string;
                        resource.Description = row["description"] as string;
                        resource.CategoryID = Convert.ToInt32(row["categoryID"]);
                        resource.Capacity = Convert.ToInt32(row["capacity"]);

                        resourcelist.Add(resource);
                    }
                }
            }
            return resourcelist;
        }

        public int IsSourceUsed(int resourceId)
        {
            SqlParameter[] parameters = new SqlParameter[]
{
                            new SqlParameter("@resourceId",resourceId ),

};
            return SqlDBHelper.ExecuteScalar("[Resource_Control].[SP_IsSourceUsed]", CommandType.StoredProcedure, parameters);
        }

        public void Delete(int resourceId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@resourceId",resourceId ),

            };
            SqlDBHelper.ExecuteNonQuery("[Resource_Control].[SP_DeleteResource]", CommandType.StoredProcedure, parameters);
        }

        //public Resource GetResourcelink(int resID)
        //{
        //    Resource resource = null;

        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@ID", resID)
        //    };

        //    using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetResourceLinkByResID]", CommandType.StoredProcedure, parameters))

        //    return resource;
        //}


        public string GetResourcelink(int resourceId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@resID",resourceId )
            };
            var result = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetResourceLinkByResID]", CommandType.StoredProcedure, parameters);
            var link = string.Empty;
            if (result != null && result.Rows.Count > 0)
            {
                link = result.Rows[0][0] as string;
            }
            return link;
        }


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ added by mahdi jalali 1398/04/23
        public void ServerLogger(string mobile = null, string note = null)
        {
            try
            {
                var file = "~/logger.txt";
                var absPath = System.Web.Hosting.HostingEnvironment.MapPath(file);
                if (!System.IO.File.Exists(absPath))
                    System.IO.File.Create(absPath).Dispose();                
                using (StreamWriter w = File.AppendText(absPath))
                {                   
                    w.WriteLine(string.Format("\t\t{0}\t\t{1}\r\n\t\t#####################################\r\n\t\t\t",mobile,  note));
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void InsertIntoDefenceSmsLog(string fullName ,  string userID, int requestId , string mobile ,bool? smsType , string smsResult , DateTime dateSendSms)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
               {
                    new SqlParameter("@FullName", fullName),
                    new SqlParameter("@StCode_OstCode", userID),
                    new SqlParameter("@RequestId",requestId ),
                    new SqlParameter("@Mobile", mobile),
                    new SqlParameter("@SmsType", smsType),
                    new SqlParameter("@SmsResult",smsResult ),
                    new SqlParameter("@DateSendSms", dateSendSms)
               };
                SqlDBHelper.ExecuteNonQuery("[Resource_Control].[SP_InsertIntoDefenceSmsLog]", CommandType.StoredProcedure, parameters);
            }
            catch (Exception x)
            {

                throw x;
            }
           
        }

        public bool IsGreaterThan30Days_LastTime_Sms_Sent(string mobile, bool IsShounatSms = false, string loggerPath = null)
        {
            bool res = false;
            string note = "";
            if (!string.IsNullOrEmpty(mobile))
            {
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Mobile", mobile), new SqlParameter("@IsShounatSms", IsShounatSms) };
                var dt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_Select_From_tbl_StudentDefence_Log]", CommandType.StoredProcedure, parameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    note += $"CountDt : {dt.Rows.Count}\t\tLastDateSentSms : { dt.Rows[0].Field<string>("LastDateSentSms")}\t\t";                    
                    var LastDateSentSms = dt.Rows[0].Field<string>("LastDateSentSms");
                    if (!string.IsNullOrEmpty(LastDateSentSms))
                    {
                        var diffDate = (DateTime.Now.Date - LastDateSentSms.ToGregorian().Date).TotalDays;
                       
                        note += $"DiffDate : {diffDate}\t\t";
                        //اگراخرین دفاع مربوط به بیش از  30 روز پیش بود
                        if (diffDate >= 30)
                            res = true;
                    }
                }
                else //هنوز رکوردی در جدول وجود نداشت
                {
                    res = true;                    
                    note += "CountDt : Dt has no rows\t\t";
                }
            }
            note += $"IsGreaterThan :   {res}";
            ServerLogger(mobile ,note);
            return res;
        }

        public bool HasSentSmsTodayForShounat(string mobile, bool IsShounatSms = false)
        {
            bool sentSms = false;
            if (!string.IsNullOrEmpty(mobile) && IsShounatSms)
            {
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Mobile", mobile), new SqlParameter("@IsShounatSms", IsShounatSms) };
                var dt = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[SP_Select_From_tbl_StudentDefence_Log]", CommandType.StoredProcedure, parameters);
                //اگر رکوردی به تاریخ امروز وجو داشت یعنی اس ام اس ارسال شده
                if (dt != null && dt.Rows.Count > 0)
                {
                    sentSms = true;
                }
            }
            return sentSms;
        }


        //[Resource_Control].[SP_Log_Defence_LastDate_Sent_Sms]

        public bool AddOrUpdate_tbl_StudentDefence_Log(string mobile, bool IsShounatSms = false)
        {
            bool res = false;
            if (!string.IsNullOrEmpty(mobile))
            {
                SqlParameter[] parameters = new SqlParameter[] {
                    new SqlParameter("@Mobile", mobile)
                    ,new SqlParameter("@IsShounatSms", IsShounatSms)

                };
                res = SqlDBHelper.ExecuteNonQuery("[Resource_Control].[SP_AddOrUpdate_tbl_StudentDefence_Log]", CommandType.StoredProcedure, parameters);
                
            }
            return res;
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    }
}