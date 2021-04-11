using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DAO.Adobe;
using IAUEC_Apps.DTO.AdobeClasses;

namespace IAUEC_Apps.Business.Adobe
{
    public class AdobeBusiness
    {


        private string Login(string _User, string _Password, string _AdobeSession, string _Server)
        {
            string SecureData;
            SecureData = _Server + "api/xml?action=login&login=" + _User + "&password=" + _Password + "&session=" + _AdobeSession;
            return GetWebPageAsString(SecureData);
        }
        private string Logout(string _AdobeSession, string _Server)
        {
            string SecureData;
            SecureData = _Server + "api/xml?action=logout&session=" + _AdobeSession;
            return GetWebPageAsString(SecureData);
        }
        private string SetUserinMeetingAsView(string _AdobeSession, string _scoid, string _userprincipaleid, string _Server)
        {
            string SecureData;
            SecureData = _Server + "api/xml?action=permissions-update&acl-id=" + _scoid + "&principal-id=" + _userprincipaleid + "&permission-id=view&session=" + _AdobeSession;
            return GetWebPageAsString(SecureData);
        }
        private string SetUserinMeetingAsHost(string _AdobeSession, string _scoid, string _userprincipaleid, string _Server)
        {
            string SecureData;
            SecureData = _Server + "api/xml?action=permissions-update&acl-id=" + _scoid + "&principal-id=" + _userprincipaleid + "&permission-id=host&session=" + _AdobeSession;
            return GetWebPageAsString(SecureData);
        }
        private string LoginAsAdmin(string _AdobeSession, string _Server, string adminPass)
        {
            string SecureData;
            //SecureData = _Server + "api/xml?action=login&login=appsuser&password=@pp$U$3r&session=" + _AdobeSession;
            SecureData = _Server + "api/xml?action=login&" + adminPass + "&session=" + _AdobeSession;
            return GetWebPageAsString(SecureData);
        }
        private string GetSessionAdobe(string _Server)
        {
            string SecureData = _Server + "api/xml?action=common-info";
            string Userdata = GetWebPageAsString(SecureData);
            if (Userdata.StartsWith("error"))
                return Userdata;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(Userdata);
            XmlNodeList elemList = xmlDocument.GetElementsByTagName("common");
            string vrs = xmlDocument.GetElementsByTagName("version")[0].InnerText;
            string _session = xmlDocument.GetElementsByTagName("cookie")[0].InnerText;
            return _session;
        }
        public static string GetWebPageAsString_sara(string url)
        {
            string xml = "";
            string oldURL = url;
            //url = url.Replace("http:", "https:");
            string line = "64,";
            try
            {

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                line += "68,";
                HttpWebResponse httpWebResponse = null;
                line += "70,";

                try
                {
                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    line += "74,";
                }
                catch (WebException exception)
                {
                    line += "78,exception.Status" + exception.Status;
                    if (exception.Status == WebExceptionStatus.ProtocolError)
                    { //get the response object from the WebException
                        line += "81,";
                        httpWebResponse = exception.Response as HttpWebResponse;
                        if (httpWebResponse == null) { return "<Error />"; }
                        line += "84,";
                    }
                }
                try
                {
                    line += "87,httpWebResponse:" + httpWebResponse;
                    httpWebResponse.GetResponseStream().ReadTimeout = 1000;
                }
                catch (Exception ex)
                {
                    line += ex.Message.Replace(" ", "");
                    //httpWebResponse.GetResponseStream().Read(buf, 0, buf.Length);
                    line += "errorInReadTime,";
                }
                Stream stream = httpWebResponse.GetResponseStream();
                line += "89,";
                StreamReader streamReader = new StreamReader(stream, Encoding.ASCII);
                line += "91,";
                xml = streamReader.ReadToEnd();
                line += "93,";
                //streamReader.Close();
                if (httpWebResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    line += "97,";
                    throw new Exception(xml);
                    line += "99,";

                }

                return xml;
            }
            catch (Exception ex)
            {
                xml = "error&&" + "line:" + line + "&&" + "url:" + url + "&&" + "oldURL:" + oldURL + "&&" + ex.Message.Replace(" ", "") + "-->" + ex.StackTrace.Replace(" ", "");
            }
            return xml;

        }
        public static string GetWebPageAsString(string url)
        {
            string xml = "";
            string oldURL = url;
            //url = url.Replace("http:", "https:");
            string line = "132,";

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                for (int i = 0; i < 3; i++)
                {
                    line += "136,";
                    try
                    {
                        using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                        {
                            line += "141,httpWebResponse:" + httpWebResponse + ",";

                            using (var responseStream = httpWebResponse.GetResponseStream())
                            {
                                line += "145,responseStream";//+ responseStream.Length + ",";
                                using (var responseReader = new StreamReader(responseStream, Encoding.ASCII))
                                {
                                    line += "148,httpWebResponse.StatusCode:" + httpWebResponse.StatusCode;
                                    xml = responseReader.ReadToEnd();
                                    if (httpWebResponse.StatusCode != System.Net.HttpStatusCode.OK)
                                    {
                                        line += "152,xml:" + xml;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        i++;
                        line += "catch"+i+":--"+ex.Message.Replace(" ", "");
                        if (i == 3)
                            throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                xml = "error&&" +"url:" + url +"&&" +  "line:" + line +  "&&" + "exp:" + ((WebException)ex).Status + "&&" + ex.Message.Replace(" ", "") + "&&" + ex.StackTrace.Replace(" ", "");

            }

            return xml;

        }
        private string OpenMeeting(string _val, string _Mode, string _AdobeSession, string _Server, bool ver9=false)
        {
            string _url = "";
            if (ver9)
                _url = _Server + _val + "/?pbMode=" + _Mode + "&page=m&width=1024&height=768, false,  screen.width, screen.height, true&session=" + _AdobeSession;
            else
                _url = _Server + _val + "/?proto=true&session=" + _AdobeSession;
            return _url;
        }
        private string OpenMeetingForDefence(string _val, string _Mode, string _AdobeSession, string _Server, bool ver9 = false)
        {
            string _url = "";
            if (ver9)
                _url = _Server + _val + "/?pbMode=" + _Mode + "&page=m&width=1024&height=768, false,  screen.width, screen.height, true&session=" + _AdobeSession;
            else
                _url = _Server + _val + "/?proto=true&session=" + _AdobeSession;
            return _url;
        }
        private string GetPrincipalId(string _Username, string _AdobeSession, string _Server)
        {
            string SecureData;
            string principalid;

            XmlDocument doc = new XmlDocument();
            SecureData = _Server + "api/xml?action=principal-list&filter-login=" + _Username + "&session=" + _AdobeSession;
            doc.LoadXml(GetWebPageAsString(SecureData));
            principalid = doc.SelectSingleNode("//results/principal-list/principal/@principal-id").Value;
            return principalid;
        }

        public string OpenMeetingAsHost(string _Server, string _UserName, string _Password, string _scoid, string _Val, string _Mode, string _aPass)
        {
            string sessionstr = GetSessionAdobe(_Server);
            if (sessionstr.StartsWith("error"))
                return sessionstr;
            LoginAsAdmin(sessionstr, _Server, _aPass);
            string principalstr = GetPrincipalId(_UserName, sessionstr, _Server);
            SetUserinMeetingAsHost(sessionstr, _scoid, principalstr, _Server);
            Logout(sessionstr, _Server);
            sessionstr = GetSessionAdobe(_Server);
            if (sessionstr.StartsWith("error"))
                return sessionstr;
            Login(_UserName, _Password, sessionstr, _Server);
            return OpenMeeting(_Val, _Mode, sessionstr, _Server,true);



        }
        public string OpenMeetingAsView(string _Server, string _UserName, string _Password, string _scoid, string _Val, string _Mode, string _aPass)
        {
            DAO.Adobe.SettingDAO sdao = new SettingDAO();

            string sessionstr = GetSessionAdobe(_Server);
            if (sessionstr.StartsWith("error"))
                return sessionstr;
            LoginAsAdmin(sessionstr, _Server, _aPass);
            string principalstr = GetPrincipalId(_UserName, sessionstr, _Server);
            SetUserinMeetingAsView(sessionstr, _scoid, principalstr, _Server);
            Logout(sessionstr, _Server);
            sessionstr = GetSessionAdobe(_Server);
            if (sessionstr.StartsWith("error"))
                return sessionstr;
            Login(_UserName, _Password, sessionstr, _Server);
            return OpenMeeting(_Val, _Mode, sessionstr, _Server);



        }
        public void DeleteMeetingByScoID(AdobeConnectDTO adobeConnectDTO)
        {
            string serviceUrl;
            serviceUrl = "http://" + adobeConnectDTO.DomainAddress +
                "/api/xml?action=sco-delete&sco-id=" + GetScoidMeeting(adobeConnectDTO) +
                "&session=" + adobeConnectDTO.DomainCookies.Value.Trim();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        }
        public void AddOrUpdateConnectionString(string term, string connectionString = null, string ConName = null, string DomainName = null, string hpass = null, string vpass = null, string hName = null, string vName = null, string adminPassword = null)
        {
            CommonBusiness cb = new CommonBusiness();
            AdobeMasterDAO adobe = new AdobeMasterDAO();
            var connString = cb.Encrypt(connectionString, true);
            var adminPass = cb.Encrypt(adminPassword, true);
            adobe.AddOrUpdateConnectionString(term, connString, ConName, DomainName, hpass, vpass, hName, vName, adminPass);
        }



        //  string DomainAddress = "kadobe.iauec.ac.ir";

        //  sadegh saryazdi  
        // const int scoId = 3042547;//FOLDER jalasat Defa  
        const int scoId = 2449315;
        public AdobeConnectDTO LoginAsAdmin(AdobeConnectDTO AdobeConnectDTO)
        {

            string serviceUrl = "http://" + AdobeConnectDTO.DomainAddress + "/api/xml?action=login"
                + "&login=" + AdobeConnectDTO.DomainLogin + "&password=" + AdobeConnectDTO.DomainPassword;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Cookie Cookies = new System.Net.Cookie();
            foreach (Cookie cook in response.Cookies)
            {
                Cookies.Value = cook.Value;
                Cookies.Name = cook.Name;
                Cookies.Domain = cook.Domain;
                Cookies.Path = cook.Path;
                Cookies.Port = cook.Port;
                Cookies.Secure = cook.Secure;
                Cookies.Expires = cook.Expires;
                Cookies.Expired = cook.Expired;
                Cookies.Discard = cook.Discard;
                Cookies.Comment = cook.Comment;
                Cookies.CommentUri = cook.CommentUri;
                Cookies.Version = cook.Version;
            }

            AdobeConnectDTO.DomainCookies = Cookies;
            return AdobeConnectDTO;
        }
        public AdobeConnectDTO LoginAsUser(AdobeConnectDTO AdobeConnectDTO)
        {

            string serviceUrl = "http://" + AdobeConnectDTO.DomainAddress + "/api/xml?action=login"
                + "&login=" + AdobeConnectDTO.UserLogin + "&password=" + AdobeConnectDTO.UserPassWord;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Cookie Cookies = new System.Net.Cookie();
            foreach (Cookie cook in response.Cookies)
            {
                Cookies.Value = cook.Value;
                Cookies.Name = cook.Name;
                Cookies.Domain = cook.Domain;
                Cookies.Path = cook.Path;
                Cookies.Port = cook.Port;
                Cookies.Secure = cook.Secure;
                Cookies.Expires = cook.Expires;
                Cookies.Expired = cook.Expired;
                Cookies.Discard = cook.Discard;
                Cookies.Comment = cook.Comment;
                Cookies.CommentUri = cook.CommentUri;
                Cookies.Version = cook.Version;
            }

            AdobeConnectDTO.DomainCookies = Cookies;
            return AdobeConnectDTO;
        }
        public AdobeConnectDTO Logout(AdobeConnectDTO AdobeConnectDTO)
        {
            string serviceUrl;
            serviceUrl = "http://" + AdobeConnectDTO.DomainAddress + "/api/xml?action=logout" +
                         "&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            AdobeConnectDTO.DomainCookies = null;
            return AdobeConnectDTO;
        }
        public string GetCheckLogin(AdobeConnectDTO AdobeConnectDTO)
        {
            string Text = "False";
            string serviceUrl = "http://" + AdobeConnectDTO.DomainAddress + "/api/xml?action=common-info"
                + "&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            XmlReader reader = XmlReader.Create(response.GetResponseStream());
            while (reader.Read())
            {
                reader.MoveToFirstAttribute();
                if (reader.NodeType == XmlNodeType.Text && AdobeConnectDTO.DomainLogin == reader.Value)
                    Text = "ok";
            }

            return Text;
        }
        public string CreateNewUser(AdobeConnectDTO AdobeConnectDTO)
        {
            string serviceUrl = "http://" + AdobeConnectDTO.DomainAddress.Trim() + "/api/xml?action=principal-update" +
                                "&first-name=" + AdobeConnectDTO.UserFirstName.Trim() +
                                "&last-name=" + AdobeConnectDTO.UserLastName.Trim() +
                                "&has-children=0" +
                                "&login=" + AdobeConnectDTO.UserLogin.Trim() +
                                "&password=4sx0pvauo4nleowu5ugvkkx9l0bpsbe" +
                                "&type=user"
                                + "&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            return GetPrincipalId(AdobeConnectDTO);
        }
        public string GetPrincipalId(AdobeConnectDTO AdobeConnectDTO)
        {
            string principalid;

            XmlDocument doc = new XmlDocument();
            string serviceUrl = "http://" + AdobeConnectDTO.DomainAddress.Trim() + "/api/xml?action=principal-list"
                                + "&filter-Login=" + AdobeConnectDTO.UserLogin.Trim()
                                + "&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();

            doc.LoadXml(GetWebPageAsString(serviceUrl));
            principalid = "";

            if (doc.OuterXml.ToString().Contains("principal-id"))
                principalid = doc.SelectSingleNode("//results/principal-list/principal/@principal-id").Value;
            return principalid;

        }
        public AdobeConnectDTO CreateNewMeeting(AdobeConnectDTO AdobeConnectDTO)
        {


            XmlDocument doc = new XmlDocument();
            string serviceUrl = "http://" + AdobeConnectDTO.DomainAddress.Trim() + "/api/xml?action=sco-update"
                              + "&type=meeting&name=" + AdobeConnectDTO.MeetingName.Trim() + AdobeConnectDTO.UserLogin.Trim()
                              + "&folder-id=" + scoId
                              + "&date-begin=" + AdobeConnectDTO.MeetingDataBegin.Year + "-" +
                                                 (AdobeConnectDTO.MeetingDataBegin.Month.ToString().Length < 2 ? "0" + AdobeConnectDTO.MeetingDataBegin.Month.ToString() : AdobeConnectDTO.MeetingDataBegin.Month.ToString()) + "-" +
                                                 (AdobeConnectDTO.MeetingDataBegin.Day.ToString().Length < 2 ? "0" + AdobeConnectDTO.MeetingDataBegin.Day.ToString() : AdobeConnectDTO.MeetingDataBegin.Day.ToString()) + "T" +
                                                 (AdobeConnectDTO.MeetingDataBegin.Hour.ToString().Length < 2 ? "0" + AdobeConnectDTO.MeetingDataBegin.Hour.ToString() : AdobeConnectDTO.MeetingDataBegin.Hour.ToString()) + ":" +
                                                 (AdobeConnectDTO.MeetingDataBegin.Minute.ToString().Length < 2 ? "0" + AdobeConnectDTO.MeetingDataBegin.Minute.ToString() : AdobeConnectDTO.MeetingDataBegin.Minute.ToString())
                            + "&date-end=" + AdobeConnectDTO.MeetingDataEnd.Year + "-" +
                                             (AdobeConnectDTO.MeetingDataEnd.Month.ToString().Length < 2 ? "0" + AdobeConnectDTO.MeetingDataEnd.Month.ToString() : AdobeConnectDTO.MeetingDataEnd.Month.ToString()) + "-" +
                                             (AdobeConnectDTO.MeetingDataEnd.Day.ToString().Length < 2 ? "0" + AdobeConnectDTO.MeetingDataEnd.Day.ToString() : AdobeConnectDTO.MeetingDataEnd.Day.ToString()) + "T" +
                                             (AdobeConnectDTO.MeetingDataEnd.Hour.ToString().Length < 2 ? "0" + AdobeConnectDTO.MeetingDataEnd.Hour.ToString() : AdobeConnectDTO.MeetingDataEnd.Hour.ToString()) + ":" +
                                             (AdobeConnectDTO.MeetingDataEnd.Minute.ToString().Length < 2 ? "0" + AdobeConnectDTO.MeetingDataEnd.Minute.ToString() : AdobeConnectDTO.MeetingDataEnd.Minute.ToString())
                              + "&url-path=" + (AdobeConnectDTO.MeetingUrlPath.Replace("/", "")).Trim()
                              + "&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return AdobeConnectDTO;
        }
        public AdobeConnectDTO SetUserinMeetingAsView(AdobeConnectDTO AdobeConnectDTO)
        {
            string serviceUrl;
            serviceUrl = "http://" + AdobeConnectDTO.DomainAddress +
                "/api/xml?action=permissions-update&acl-id=" + GetScoidMeeting(AdobeConnectDTO) +
                "&principal-id=" + AdobeConnectDTO.PrincipalID + "&permission-id=view&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return AdobeConnectDTO;
        }
        public AdobeConnectDTO SetUserinMeetingAsHost(AdobeConnectDTO AdobeConnectDTO)
        {
            string serviceUrl;
            serviceUrl = "http://" + AdobeConnectDTO.DomainAddress +
                "/api/xml?action=permissions-update&acl-id=" + GetScoidMeeting(AdobeConnectDTO) +
                "&principal-id=" + AdobeConnectDTO.PrincipalID + "&permission-id=host&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return AdobeConnectDTO;
        }
        public AdobeConnectDTO SetUserinMeetingAsPresenter(AdobeConnectDTO AdobeConnectDTO)
        {
            string serviceUrl;
            serviceUrl = "http://" + AdobeConnectDTO.DomainAddress +
                "/api/xml?action=permissions-update&acl-id=" + GetScoidMeeting(AdobeConnectDTO) +
                "&principal-id=" + AdobeConnectDTO.PrincipalID + "&permission-id=mini-host&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return AdobeConnectDTO;
        }
        public string GetScoidMeeting(AdobeConnectDTO AdobeConnectDTO)
        {
            XmlDocument doc = new XmlDocument();
            string serviceUrl;
            serviceUrl = "http://" + AdobeConnectDTO.DomainAddress +
                "/api/xml?action=sco-contents&sco-id=" + scoId +
                "&filter-type=meeting" + "&filter-url-path=" + AdobeConnectDTO.MeetingUrlPath + "/"
                + "&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();
            doc.LoadXml(GetWebPageAsString(serviceUrl));

            if (doc.OuterXml.ToString().Contains("sco-id"))
                return doc.SelectSingleNode("//results/scos/sco/@sco-id").Value;

            return "";
        }

        public XmlDocument GetReportuserMeetings(AdobeConnectDTO AdobeConnectDTO, string scoidMeeting, string loginId = "")
        {
            XmlDocument doc = new XmlDocument();
            string serviceUrl;

            serviceUrl = "http://" + AdobeConnectDTO.DomainAddress + "/api/xml?action=report-meeting-attendance"
                                                                     + "&sco-id=" + scoidMeeting
                                                                      + "&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();
            if (loginId.Trim() != "")
                serviceUrl += "&filter-login=" + loginId;
            doc.LoadXml(GetWebPageAsString(serviceUrl));
            return doc;
        }
        public XmlDocument GetMeetings(AdobeConnectDTO AdobeConnectDTO)
        {
            XmlDocument doc = new XmlDocument();
            string serviceUrl;
            serviceUrl = "http://" + AdobeConnectDTO.DomainAddress + "/api/xml?action=sco-contents&sco-id=" + scoId
                                                                   + "&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();
            doc.LoadXml(GetWebPageAsString(serviceUrl));
            return doc;
        }

        public XmlDocument GetMyMeeting(AdobeConnectDTO AdobeConnectDTO)
        {//login for current user 
            XmlDocument doc = new XmlDocument();
            string serviceUrl;
            serviceUrl = "http://" + AdobeConnectDTO.DomainAddress + "/api/xml?action=report-my-meetings"
                                                                   + "&session=" + AdobeConnectDTO.DomainCookies.Value.Trim();
            doc.LoadXml(GetWebPageAsString(serviceUrl));
            return doc;
        }
        public string OpenMeetingAsView(AdobeConnectDTO adobeConnectDTO)
        {

            adobeConnectDTO = LoginAsAdmin(adobeConnectDTO);

            string principalstr = GetPrincipalId(adobeConnectDTO);
            if (principalstr == "")
            {
                CreateNewUser(adobeConnectDTO);
                principalstr = GetPrincipalId(adobeConnectDTO);
            }
            adobeConnectDTO.PrincipalID = principalstr;
            SetUserinMeetingAsView(adobeConnectDTO);
            Logout(adobeConnectDTO);
            adobeConnectDTO = LoginAsUser(adobeConnectDTO);
            return OpenMeetingForDefence(adobeConnectDTO.MeetingUrlPath, "view", adobeConnectDTO.DomainCookies.Value.Trim(), adobeConnectDTO.DomainAddress);

        }
        public string OpenMeetingAsPresnter(AdobeConnectDTO adobeConnectDTO)
        {

            adobeConnectDTO = LoginAsAdmin(adobeConnectDTO);

            string principalstr = GetPrincipalId(adobeConnectDTO);
            if (principalstr == "")
            {
                CreateNewUser(adobeConnectDTO);
                principalstr = GetPrincipalId(adobeConnectDTO);
            }
            adobeConnectDTO.PrincipalID = principalstr;
            SetUserinMeetingAsPresenter(adobeConnectDTO);
            Logout(adobeConnectDTO);
            adobeConnectDTO = LoginAsUser(adobeConnectDTO);
            return OpenMeetingForDefence(adobeConnectDTO.MeetingUrlPath, "view", adobeConnectDTO.DomainCookies.Value.Trim(), adobeConnectDTO.DomainAddress);

        }
        public string OpenMeetingAsHost(AdobeConnectDTO adobeConnectDTO)
        {

            adobeConnectDTO = LoginAsAdmin(adobeConnectDTO);

            string principalstr = GetPrincipalId(adobeConnectDTO);
            if (principalstr == "")
            {
                CreateNewUser(adobeConnectDTO);
                principalstr = GetPrincipalId(adobeConnectDTO);
            }
            adobeConnectDTO.PrincipalID = principalstr;
            SetUserinMeetingAsHost(adobeConnectDTO);
            Logout(adobeConnectDTO);
            adobeConnectDTO = LoginAsUser(adobeConnectDTO);
            return OpenMeetingForDefence(adobeConnectDTO.MeetingUrlPath, "view", adobeConnectDTO.DomainCookies.Value.Trim(), adobeConnectDTO.DomainAddress);

        }
        //    public 
    }
}

