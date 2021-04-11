using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.Adobe;
using System.Data;
using System.Data.SqlClient;

namespace IAUEC_Apps.Business.Adobe
{
    public class SessionMonitoringBusiness
    {
        SessionMonitoringDAO SessionMonitoringDAO = new SessionMonitoringDAO();
        RecordsBusiness RecordsBusiness = new RecordsBusiness();
        AdobeBusiness AdobeBusiness = new AdobeBusiness();
        SettingBusiness sb = new SettingBusiness();
        public DataTable GetMeetingsOnline()
        {
            DataTable OnlineDT_Final = new DataTable();
            try
            {
                DataTable OnlineDT = SessionMonitoringDAO.GetMeetingsOnline();
                //DataTable OnlineDT = GetMeetingOnline_UrlLink2();   
                var setting = sb.GetSettingByTermC(System.Configuration.ConfigurationManager.AppSettings["Term"]);
                DAO.Adobe.SettingDAO sdao = new DAO.Adobe.SettingDAO();

                string adobe_domain = setting.DomainName;
                string adobe_user = setting.hName;
                string adobe_pass = setting.hpass;
                // اضافه کردن لینک کلاس

                OnlineDT_Final.Columns.Add("NAME", typeof(string));
                OnlineDT_Final.Columns.Add("code", typeof(string));
                OnlineDT_Final.Columns.Add("URL_PATH", typeof(string));
                OnlineDT_Final.Columns.Add("TimeStart", typeof(string));
                OnlineDT_Final.Columns.Add("CountOFUser", typeof(string));
                OnlineDT_Final.Columns.Add("TotalTime", typeof(string));
                OnlineDT_Final.Columns.Add("SCO_ID", typeof(string));
                OnlineDT_Final.Columns.Add("Link", typeof(string));
                OnlineDT_Final.Columns.Add("ShamsiDate", typeof(string));
                OnlineDT_Final.Columns.Add("Day", typeof(string));

                for (int i = 0; i < OnlineDT.Rows.Count; i++)
                {
                    DataRow row = OnlineDT_Final.NewRow();
                    row["NAME"] = OnlineDT.Rows[i]["NAME"].ToString();
                    row["code"] = OnlineDT.Rows[i]["code"].ToString();
                    row["URL_PATH"] = OnlineDT.Rows[i]["URL_PATH"].ToString();
                    row["TimeStart"] = OnlineDT.Rows[i]["TimeStart"].ToString();
                    row["CountOFUser"] = OnlineDT.Rows[i]["CountOFUser"].ToString();
                    row["TotalTime"] = OnlineDT.Rows[i]["TotalTime"].ToString();
                    row["SCO_ID"] = OnlineDT.Rows[i]["SCO_ID"].ToString();
                    row["ShamsiDate"] = OnlineDT.Rows[i]["ShamsiDate"].ToString();
                    row["Day"] = OnlineDT.Rows[i]["Day"].ToString();
                    row["Link"] = AdobeBusiness.OpenMeetingAsHost(adobe_domain, adobe_user, adobe_pass, OnlineDT.Rows[i]["SCO_ID"].ToString(), OnlineDT.Rows[i]["_val"].ToString(), "view", sdao.Decrypt(setting.aPass, true));
                    //row["Link"] = AdobeBusiness.OpenMeetingAsView("http://live.iauec.ac.ir/", "vuser", "", OnlineDT.Rows[i]["SCO_ID"].ToString(), OnlineDT.Rows[i]["_val"].ToString(), "view");

                    OnlineDT_Final.Rows.Add(row);
                }

                return OnlineDT_Final;
            }
            catch (Exception em)
            {

                return OnlineDT_Final;
            }
           

            

        }


        /// <summary>
        /// Filter By Minute Of Class & Filter By Count of Users
        /// </summary>
        /// <returns></returns>
        public DataTable GetMeetingsOnline_WithFilter(long MinMinuteFilter, long MaxMinuteFilter
            , long MinUserFilter, long MaxUserFilter)
        {
           
            if (MinMinuteFilter == 0)
                MinMinuteFilter = 100000;

            if (MinUserFilter == 0)
                MinUserFilter = 100000;

            DataTable DT = GetMeetingsOnline();

            DataTable DT2 = new DataTable();
            DT2.Columns.Add("NAME", typeof(string));
            DT2.Columns.Add("code", typeof(string));
            DT2.Columns.Add("URL_PATH", typeof(string));
            DT2.Columns.Add("TimeStart", typeof(string));
            DT2.Columns.Add("CountOFUser", typeof(string));
            DT2.Columns.Add("TotalTime", typeof(string));
            DT2.Columns.Add("SCO_ID", typeof(string));
            DT2.Columns.Add("Link", typeof(string));
            DT2.Columns.Add("ShamsiDate", typeof(string));
            DT2.Columns.Add("Day", typeof(string));

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (long.Parse(DT.Rows[i]["TotalTime"].ToString()) < MinMinuteFilter
                    && long.Parse(DT.Rows[i]["TotalTime"].ToString()) > MaxMinuteFilter)
                {
                    DataRow row = DT2.NewRow();
                    row["NAME"] = DT.Rows[i]["NAME"].ToString();
                    row["code"] = DT.Rows[i]["code"].ToString();
                    row["URL_PATH"] = DT.Rows[i]["URL_PATH"].ToString();
                    row["TimeStart"] = DT.Rows[i]["TimeStart"].ToString();
                    row["CountOFUser"] = DT.Rows[i]["CountOFUser"].ToString();
                    row["TotalTime"] = DT.Rows[i]["TotalTime"].ToString();
                    row["SCO_ID"] = DT.Rows[i]["SCO_ID"].ToString();
                    row["Link"] = DT.Rows[i]["Link"].ToString();
                    row["ShamsiDate"] = DT.Rows[i]["ShamsiDate"].ToString();
                    row["Day"] = DT.Rows[i]["Day"].ToString();
                    DT2.Rows.Add(row);
                }
            }

            DataTable DT3 = new DataTable();
            DT3.Columns.Add("NAME", typeof(string));
            DT3.Columns.Add("code", typeof(string));
            DT3.Columns.Add("URL_PATH", typeof(string));
            DT3.Columns.Add("TimeStart", typeof(string));
            DT3.Columns.Add("CountOFUser", typeof(string));
            DT3.Columns.Add("TotalTime", typeof(string));
            DT3.Columns.Add("SCO_ID", typeof(string));
            DT3.Columns.Add("Link", typeof(string));
            DT3.Columns.Add("ShamsiDate", typeof(string));
            DT3.Columns.Add("Day", typeof(string));

            for (int i = 0; i < DT2.Rows.Count; i++)
            {
                if (long.Parse(DT.Rows[i]["CountOFUser"].ToString()) < MinUserFilter
                    && long.Parse(DT.Rows[i]["CountOFUser"].ToString()) > MaxUserFilter)
                {
                    DataRow row = DT3.NewRow();
                    row["NAME"] = DT2.Rows[i]["NAME"].ToString();
                    row["code"] = DT2.Rows[i]["code"].ToString();
                    row["URL_PATH"] = DT2.Rows[i]["URL_PATH"].ToString();
                    row["TimeStart"] = DT2.Rows[i]["TimeStart"].ToString();
                    row["CountOFUser"] = DT2.Rows[i]["CountOFUser"].ToString();
                    row["TotalTime"] = DT2.Rows[i]["TotalTime"].ToString();
                    row["SCO_ID"] = DT2.Rows[i]["SCO_ID"].ToString();
                    row["Link"] = DT2.Rows[i]["Link"].ToString();
                    row["ShamsiDate"] = DT2.Rows[i]["ShamsiDate"].ToString();
                    row["Day"] = DT2.Rows[i]["Day"].ToString();
                    DT3.Rows.Add(row);
                }
            }

            return DT3;

        }


        /// <summary>
        /// For Test
        /// </summary>
        /// <returns></returns>
        public DataTable GetMeetingOnline_UrlLink2()
        {
            SqlConnection conn =
                new SqlConnection("User=karimi;Password=123456;server=192.168.30.190; database=karimiadobe; connection timeout=30;");

            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select distinct s.NAME,s.URL_PATH,min(t.DATE_CREATED) as 'TimeStart' "
                + " ,count(t.PRINCIPAL_ID) as 'CountOFUser' "
                + " ,DATEPART(HOUR, GETDATE()- min(CONVERT(VARCHAR,DATEADD(MINUTE, 270, t.DATE_CREATED))))*60 "
                + " + DATEPART(MINUTE, GETDATE()-min(CONVERT(VARCHAR,DATEADD(MINUTE, 270, t.DATE_CREATED)))    ) as 'TotalTime' "
                + " ,s.SCO_ID "
                + " ,ss.NAME as 'code',substring(s.URL_PATH,2,len(s.URL_PATH)-2) as _val "
                + " ,min([dbo].[MiladiTOShamsi]( t.DATE_CREATED)) as 'ShamsiDate' "
                + " ,min(DATEPART(DAY, GETDATE()-t.DATE_CREATED)) as 'Day' "
                + " from PPS_TRANSCRIPTS t "
                + " inner join pps_scos s on s.SCO_ID=t.SCO_ID "
                + " inner join pps_scos ss on s.FOLDER_ID=ss.SCO_ID "
                + "	inner join PPS_EXT_ENUM_TYPE t2 on t2.TYPE=s.TYPE "
                + " inner join PPS_USER_SESSIONS u on u.SESSION_ID=t.SESSION_ID "
                + " inner join PPS_PERMISSIONS p on p.PERMISSION_ID=t.PERMISSION_ID "
                + " inner join PPS_PRINCIPALS p2 on p2.PRINCIPAL_ID=t.PRINCIPAL_ID "
                + " where t.status like 'i' and t2.NAME='{meeting}' group by s.NAME,s.URL_PATH,s.SCO_ID,ss.NAME ", conn);
            cmd.CommandType = CommandType.Text;

            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            rdr.Dispose();
            cmd.Connection.Close();
            cmd.Dispose();
            conn.Close();
            return dt;
        }












        public DataTable GetMeetingOnlineUser(string ScoId)
        {
            return SessionMonitoringDAO.GetMeetingOnlineUser(ScoId);
        }






    }
}
