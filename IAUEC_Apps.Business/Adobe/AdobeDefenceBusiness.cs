using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.Business;
using System.Xml;
using System.Xml.Linq;
using System;
using System.Data;
using ResourceControl.BLL;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.CommonClasses;
using System.Globalization;

namespace IAUEC_Apps.Business.Adobe
{
    public class AdobeDefenceBusiness
    {
        private string CountTimeDefence(AdobeConnectDTO AdobeConnectDTO, string loginId, string scoidMeeting)
        {
            
            XmlDocument doc = new XmlDocument();
            AdobeBusiness adobeBuisnes = new AdobeBusiness();
            AdobeConnectDTO adobeConnectDTO1 = new AdobeConnectDTO();
            
            adobeConnectDTO1.DomainAddress = AdobeConnectDTO.DomainAddress;
            adobeConnectDTO1.DomainLogin = AdobeConnectDTO.DomainLogin;
            adobeConnectDTO1.DomainPassword = AdobeConnectDTO.DomainPassword;

            adobeConnectDTO1 = adobeBuisnes.LoginAsAdmin(adobeConnectDTO1);
            doc = adobeBuisnes.GetReportuserMeetings(adobeConnectDTO1, scoidMeeting, loginId);
            adobeBuisnes.Logout(adobeConnectDTO1);

            if (doc != null)
            {
                if (doc.OuterXml.ToString().Contains("row"))
                {
                    XmlNodeList nodeList = doc.SelectNodes("//results/report-meeting-attendance/row");
                    DateTime startDate;
                    DateTime endDate;
                    TimeSpan lengthTime = TimeSpan.Zero;
                    foreach (XmlNode no in nodeList)
                    {
                        startDate = Convert.ToDateTime(no["date-created"].InnerText);
                        if (no["date-end"] != null)
                        {
                            endDate = Convert.ToDateTime(no["date-end"].InnerText);
                            lengthTime += (endDate - startDate);
                        }

                       

                    }
                    return lengthTime.ToString();

                }


            }
            return "";

        }
        private bool CheckTimeDefence(AdobeConnectDTO AdobeConnectDTO, string loginId, string scoidMeeting)
        {
            const int standardLengthTime = 20;
            XmlDocument doc = new XmlDocument();
            AdobeBusiness adobeBuisnes = new AdobeBusiness();
            AdobeConnectDTO adobeConnectDTO1 = new AdobeConnectDTO();
           
            adobeConnectDTO1.DomainAddress = AdobeConnectDTO.DomainAddress;
            adobeConnectDTO1.DomainLogin = AdobeConnectDTO.DomainLogin;
            adobeConnectDTO1.DomainPassword = AdobeConnectDTO.DomainPassword;
          
            adobeConnectDTO1 =  adobeBuisnes.LoginAsAdmin(adobeConnectDTO1);
            doc = adobeBuisnes.GetReportuserMeetings(adobeConnectDTO1, scoidMeeting, loginId);
            adobeBuisnes.Logout(adobeConnectDTO1);
               
            if (doc != null)
            {
                if (doc.OuterXml.ToString().Contains("row"))
                {
                    XmlNodeList nodeList = doc.SelectNodes("//results/report-meeting-attendance/row");
                    DateTime startDate;
                    DateTime endDate;
                    TimeSpan lengthTime = TimeSpan.Zero;
                    foreach (XmlNode no in nodeList)
                    {
                        startDate = Convert.ToDateTime(no["date-created"].InnerText);
                        endDate = Convert.ToDateTime(no["date-end"].InnerText);
                        lengthTime += (endDate - startDate);
                        if (lengthTime.TotalMinutes > standardLengthTime)
                            return true;

                    }

                }


            }
            return false;

        }
        private int CountWatch(AdobeConnectDTO AdobeConnectDTO)
        {
            AdobeBusiness adobeBuisnes = new AdobeBusiness();
            XmlDocument doc = new XmlDocument();
            doc = adobeBuisnes.GetMyMeeting(AdobeConnectDTO);
            int count = 0;
            if (doc != null)
            {

                if (doc.OuterXml.ToString().Contains("meeting"))
                {
                    XmlNodeList nodeList = doc.SelectNodes("//results/my-meetings/meeting");
                    foreach (XmlNode no in nodeList)
                    {

                        bool bTime = CheckTimeDefence(AdobeConnectDTO, AdobeConnectDTO.UserLogin, no.Attributes["sco-id"].Value.ToString());
                        if (bTime == true)
                        {
                            ++count;
                        }
                    }

                }
            }

            return count;
        }
        public void  CreateMeeting(AdobeConnectDTO adobeConnectDTO)
        {
            AdobeBusiness adobeBuisnes = new AdobeBusiness();
            adobeConnectDTO = adobeBuisnes.LoginAsAdmin(adobeConnectDTO);

            adobeBuisnes.CreateNewMeeting(adobeConnectDTO);
            adobeConnectDTO= adobeBuisnes.Logout(adobeConnectDTO);
        }
        public bool CheckAcceptRequestDefence(AdobeConnectDTO adobeConnectDTO)
        {
            const int  standardCount=3;
            AdobeBusiness adobeBuisnes = new AdobeBusiness();
            adobeConnectDTO= adobeBuisnes.LoginAsAdmin(adobeConnectDTO);
            adobeConnectDTO.PrincipalID = adobeBuisnes.GetPrincipalId(adobeConnectDTO);
            adobeBuisnes.Logout(adobeConnectDTO);
            adobeConnectDTO = adobeBuisnes.LoginAsUser(adobeConnectDTO);
            int count = CountWatch(adobeConnectDTO);
            adobeBuisnes.Logout(adobeConnectDTO);

            if (count >= standardCount)
                return true;
            else
                return false;

        }
        public static DataTable GetMeetingsDefence(AdobeConnectDTO adobeConnectDTO, string typeCollege = "")
        {
            DataTable dt = new DataTable();
            XmlDocument doc = new XmlDocument();
            AdobeBusiness adobeBusiness = new AdobeBusiness();
            adobeBusiness.LoginAsAdmin(adobeConnectDTO);
            doc = adobeBusiness.GetMeetings(adobeConnectDTO);
            dt.Columns.Add("MeetingId");
            dt.Columns.Add("Name");
            dt.Columns.Add("DateStart");
            dt.Columns.Add("DateEnd");
            dt.Columns.Add("MeetingUrl");
            DataRow dr;
          
            if (doc.OuterXml.ToString().Contains("sco"))
            {
                XmlNodeList nodeList = doc.SelectNodes("//results/scos/sco");
                foreach (XmlNode no in nodeList)
                if(typeCollege==""||no["Name"].ToString().Contains(typeCollege))
                    {
                    dr = dt.NewRow();
                    dr["MeetingId"] = no.Attributes["sco-id"].Value;
                    dr["Name"] = no["name"].InnerText.ToString();
                    dr["DateStart"] = no["date-begin"].InnerText;
                    dr["DateEnd"] = no["date-end"].InnerText;
                    dr["MeetingUrl"] = no["url-path"].InnerText;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        public static DataTable GetMyMeetingDefence(AdobeConnectDTO adobeConnectDTO, string typeCollege = "")
        {
            DataTable dt = new DataTable();
            XmlDocument doc = new XmlDocument();
            AdobeBusiness adobeBusiness = new AdobeBusiness();
            adobeBusiness.LoginAsUser(adobeConnectDTO);
            doc = adobeBusiness.GetMyMeeting(adobeConnectDTO);
            dt.Columns.Add("MeetingId");
            dt.Columns.Add("Name");
            dt.Columns.Add("DateStart");
            dt.Columns.Add("DateEnd");
            dt.Columns.Add("MeetingUrl");
            DataRow dr;

            if (doc.OuterXml.ToString().Contains("sco"))
            {
                XmlNodeList nodeList = doc.SelectNodes("//results/my-meetings/meeting");
                foreach (XmlNode no in nodeList)
                    if (typeCollege == "" || no["Name"].ToString().Contains(typeCollege))
                    {
                        dr = dt.NewRow();
                        dr["MeetingId"] = no.Attributes["sco-id"].Value;
                        dr["Name"] = no["name"].InnerText.ToString();
                        dr["DateStart"] = no["date-begin"].InnerText;
                        dr["DateEnd"] = no["date-end"].InnerText;
                        dr["MeetingUrl"] = no["url-path"].InnerText;
                        dt.Rows.Add(dr);
                    }
            }
            return dt;
        }
        public string CreateLinkMeeting(string stcode,string startDateMeeting,string TimeDateMeeting )
        {  // "/r21qa0qqv7r"
            return ("ST" + stcode + "_" + startDateMeeting + "_" + TimeDateMeeting).Replace("/","").Trim();
        }


        public DataTable MeetingPresentedSt(AdobeConnectDTO adobeConnectDTO)
        {

            //domain
            DataTable dtMyMeeting = GetMyMeetingDefence(adobeConnectDTO);
            DataTable dt=new DataTable();
            RequestHandler requestHandler = new RequestHandler();
            const int startindex = 3;
            const int Length = 9;
            dt.Columns.Add("Id", typeof(System.String));
            dt.Columns.Add("StudentCode", typeof(System.String));
            dt.Columns.Add("StudentFullName", typeof(System.String));
            dt.Columns.Add("DefenceSubject", typeof(System.String));
            dt.Columns.Add("LocName", typeof(System.String));
            dt.Columns.Add("nameresh", typeof(System.String));
            dt.Columns.Add("startTime", typeof(System.String));
            dt.Columns.Add("Count", typeof(System.String));
           
            foreach (DataRow row in dtMyMeeting.Rows)
            {
               
                DataTable dtMeetingDefence;   
                string stCode = row["MeetingUrl"].ToString().Substring(startindex, Length);
                dtMeetingDefence = requestHandler.GetMeetingDefencesAStudentByStcodeBusinesss(stCode, "-1");
                string countTimeDefence = CountTimeDefence(adobeConnectDTO, adobeConnectDTO.UserLogin, row["MeetingId"].ToString());
                if (countTimeDefence != "" && dt!=null&& dtMeetingDefence.Rows.Count>0)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = dtMeetingDefence.Rows[0]["Id"];
                    dr["StudentCode"] = dtMeetingDefence.Rows[0]["StudentCode"];
                    dr["StudentFullName"] = dtMeetingDefence.Rows[0]["StudentFullName"];
                    dr["DefenceSubject"] = dtMeetingDefence.Rows[0]["DefenceSubject"];
                    dr["LocName"] = dtMeetingDefence.Rows[0]["LocName"];
                    dr["nameresh"] = dtMeetingDefence.Rows[0]["nameresh"];
                    dr["startTime"] = dtMeetingDefence.Rows[0]["startTime"];
                    dr["Count"] = countTimeDefence;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        public void DeleteMeeting(AdobeConnectDTO adobeConnectDTO)
        {
            AdobeBusiness adobeBuisnes = new AdobeBusiness();
            adobeConnectDTO = adobeBuisnes.LoginAsAdmin(adobeConnectDTO);
            adobeBuisnes.DeleteMeetingByScoID(adobeConnectDTO);
            adobeBuisnes.Logout(adobeConnectDTO);
        }
        public static bool CreateAllMeetingNotExist()
        {
            AdobeConnectDTO adobeConnectDTO=new AdobeConnectDTO();
        
            adobeConnectDTO.SetValueDefult();
            RequestHandler _requestHandler = new RequestHandler();
            DataTable dtMeetingsOnlineDb;
            dtMeetingsOnlineDb= _requestHandler.GetMeetingTotalDefencesbyCollegeIdBusiness();
            DataTable dtMeetingsAdobe;
            dtMeetingsAdobe = GetMeetingsDefence(adobeConnectDTO);

            foreach (DataRow rowDb in dtMeetingsOnlineDb.Rows)
            {
                if (rowDb["resLink"].ToString().Trim().ToLower().Trim('/') != "")
                {
                    bool flagExist = false;
                    foreach (DataRow rowAdobe in dtMeetingsAdobe.Rows)
                    {

                        if (rowDb["resLink"].ToString().Trim().ToLower().Trim('/') == rowAdobe["MeetingUrl"].ToString().Trim().ToLower().Trim('/'))
                        {
                            flagExist = true;
                            break;
                        }
                    }
                
                    if (flagExist == false)
                    {
                        CreateMeeting(rowDb["studentcode"].ToString(), rowDb["RequestDate"].ToString(),
                            rowDb["startTime"].ToString().Substring(0, 2), 1, rowDb["StudentFullName"].ToString(), true);
                    }
                 }
      
            }

         return true;

        }
        public static void CreateMeeting(string stCode, string date, string startTime, int LengthMeeting, string  stName,bool flagTotal=false,string requestId="")
        {
            RequestHandler reqH = new RequestHandler();
            

            AdobeConnectDTO adobeConnectDTO = new AdobeConnectDTO();
            LoginBusiness lgb = new LoginBusiness();
            AdobeDefenceBusiness adobeDefenceBusiness = new AdobeDefenceBusiness();
            LoginDTO stInfo = lgb.Get_StInfo(stCode.ToString());
            const string pass = "4sx0pvauo4nleowu5ugvkkx9l0bpsbe";
            string firtsName = (stInfo == null || stInfo.Name == null || stInfo.Name.Trim() == "") ? "نامشخص" : stInfo.Name;
            string lastName = (stInfo == null || stInfo.LastName == null || stInfo.LastName.Trim() == "") ? "نامشخص" : stInfo.LastName;
            adobeConnectDTO.SetValueDefult(stCode.ToString(), pass, firtsName, lastName);
            adobeConnectDTO.MeetingName = ConvertorFarsi2Arabic.Parse_Farsi2_Arabic("  جلسه دفاع " + (stName != null && stName.Trim() != "" ? stName : ""));
            string link = adobeDefenceBusiness.CreateLinkMeeting(stCode.ToString(), date, startTime.ToString());
    
        
                adobeConnectDTO.MeetingUrlPath = link;
                PersianCalendar PC = new PersianCalendar();
                adobeConnectDTO.MeetingDataBegin = PC.ToDateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(5, 2)), int.Parse(date.Substring(8, 2)), int.Parse(startTime), 0, 0, 0);
                adobeConnectDTO.MeetingDataEnd = PC.ToDateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(5, 2)), int.Parse(date.Substring(8, 2)), int.Parse(startTime) + LengthMeeting, 0, 0, 0);
         if(flagTotal)
            adobeDefenceBusiness.CreateMeeting(adobeConnectDTO);
         else 
            {
               DataTable dtExistLink = reqH.GetMeetingDefencesAStudentByStcodeBusinesss(stCode.ToString());
                if (dtExistLink != null && dtExistLink.Rows.Count > 0 && dtExistLink.Rows[0]["resLink"].ToString().Trim() != "")
                {
                    //empty
                }
                else
                {
                    adobeDefenceBusiness.CreateMeeting(adobeConnectDTO);
                    reqH.UpdateRequest_LinkMeeting(requestId.Trim(), link);
                }
            }
            
        }
    }
}

