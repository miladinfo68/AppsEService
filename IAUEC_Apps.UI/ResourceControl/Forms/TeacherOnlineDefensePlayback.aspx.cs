using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Conatct.Functions;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.DTO.AdobeClasses;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class TeacherOnlineDefensePlayback : System.Web.UI.Page
    {
        const string passAdobeUser = "4sx0pvauo4nleowu5ugvkkx9l0bpsbe";
        Business.Adobe.AdobeBusiness adobeBusiness = new Business.Adobe.AdobeBusiness();
        AdobeConnectDTO adobeConnectDTO = new AdobeConnectDTO();
        RequestHandler requestHandler = new RequestHandler();
        string user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }


        }
        /*VazMeeting
         1-online
         2-past
         3.future   
        */
        public DataTable VazMeeting(DataTable dt)
        {
            dt.Columns.Add("vazName", typeof(System.String));
            dt.Columns.Add("vazId", typeof(System.Int32));
            dt.Columns.Add("vazSrc", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                if (row != null)
                {
                    if (row["RequestDate"] != null && row["startTime"] != null && row["endTime"] != null)
                    {
                        int startTime = int.Parse(row["startTime"].ToString().Substring(1, 1) == ":" ? row["startTime"].ToString().Substring(0, 1) : row["startTime"].ToString().Substring(0, 2));
                        //  int endTime = int.Parse(row["endTime"].ToString().Substring(1, 1) == ":" ? row["endTime"].ToString().Substring(0, 1) : row["endTime"].ToString().Substring(0, 2));
                        int HourNow = DateTime.Now.Hour;
                        if ((startTime) <= (HourNow + 1) && (startTime) > (HourNow - 4))
                        {
                            row["vazName"] = "در حال برگزاری";
                            row["vazId"] = "1";
                            row["vazSrc"] = "Sabz.gif";
                        }
                        else if ((startTime) > HourNow + 1)
                        {
                            row["vazName"] = "زمان شروع فرانرسیده";
                            row["vazId"] = "3";
                            row["vazSrc"] = "Abi.gif";
                        }
                        else if ((startTime) <= HourNow - 4)
                        {
                            row["vazName"] = "خاتمه یافته";
                            row["vazId"] = "2";
                            row["vazSrc"] = "Grey.gif";
                        }

                        //    bool flagStartMeeting = row["FlagStartMeeting"] == null || row["FlagStartMeeting"].ToString() == "False" ? false : true;
                        //    bool FlagEndMeeting = row["FlagEndMeeting"] == null || row["FlagEndMeeting"].ToString() == "False" ? false : true;
                        //    int flagvaz = DatePersian.CompareDate(row["RequestDate"].ToString(),
                        //      row["startTime"].ToString(), row["endTime"].ToString());
                        //    if (!flagStartMeeting)
                        //    {
                        //        if (flagvaz == -1)
                        //        {
                        //            row["vazName"] = "زمان شروع فرانرسیده";
                        //            row["vazId"] = "3";
                        //            row["vazSrc"] = "Abi.gif";
                        //        }
                        //        else if (flagvaz == 0)
                        //        {
                        //            row["vazName"] = "باتاخیرآغازمی شود";
                        //            row["vazId"] = "3";
                        //            row["vazSrc"] = "Abi.gif";
                        //        }
                        //        else if (flagvaz == 1)
                        //        {
                        //            row["vazName"] = "خاتمه یافته";
                        //            row["vazId"] = "2";
                        //            row["vazSrc"] = "Grey.gif";
                        //        }
                        //    }
                        //    else if (FlagEndMeeting)
                        //    {
                        //        //else if (flagvaz == 1)
                        //        {
                        //            row["vazName"] = "خاتمه یافته";
                        //            row["vazId"] = "2";
                        //            row["vazSrc"] = "Grey.gif";
                        //        }
                        //    }
                        //    else if (flagStartMeeting && !FlagEndMeeting)
                        //    {
                        //        row["vazName"] = "در حال برگزاری";
                        //        row["vazId"] = "1";
                        //        row["vazSrc"] = "Sabz.gif";
                        //    }
                        //    else
                        //    {
                        //        row["vazName"] = "نامشخص";
                        //        row["vazId"] = "-1";
                        //        row["vazSrc"] = "Grey.gif";

                        //    }

                        //}
                        //else
                        //    row["vaz"] = -1;


                    }
                }

            }
            return dt;
        }

        public void RfrhgrdDefenceMeetings()
        {
            user = Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dt = new DataTable();
            dt = requestHandler.GetMeetingDefencesByOscodeBusiness("200"+user);

            if (dt != null && dt.Rows.Count > 0)
            {

                grdDsiplayDefence.DataSource = VazMeeting(dt);
            }
            else
                grdDsiplayDefence.DataSource = string.Empty;

        }




       protected void grdDsiplayDefence_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    //var dataItem = grdDsiplayDefence.SelectedItems[0] as Telerik.Web.UI.GridDataItem;
        //    //var vazId = dataItem.FindControl("lblVazId") as Label;
        //    //if (vazId.Text == "1")
        //    //{
        //    //    LoginBusiness lgb = new LoginBusiness();
        //    //    FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
        //    //    var userAdobe = Session[sessionNames.userID_StudentOstad].ToString();
        //    //    DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
        //    //    if (dtHR != null && dtHR.Rows.Count > 0)
        //    //    {
        //    //        string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
        //    //        string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

        //    //        adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);


        //    //        var Link = dataItem.FindControl("lnkLinkDefence") as LinkButton;

        //    //        adobeConnectDTO.MeetingUrlPath = "/st" + Link.Text.Replace("/", "");//name.Text;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    RadWindowManager1.RadAlert("جلسه دفاع در حال حاضر در دسترس نیست", 500, 100, "خطا", "");
        //    //    return;
        //    //}


        //    //string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
        //    //Response.Redirect(link);
        }
        protected void grdDsiplayDefence_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RfrhgrdDefenceMeetings();
        }
        protected void lnkLinkDefence_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Label vazId = (Label)item.FindControl("lblVazId");


            if (vazId.Text == "1")
            {
                LoginBusiness lgb = new LoginBusiness();
                FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
                var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
                DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
                if (dtHR != null && dtHR.Rows.Count > 0)
                {
                    string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                    string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                    adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);


                    var Link = item.FindControl("resLink") as Label;

                    adobeConnectDTO.MeetingUrlPath = "/" + Link.Text.Replace("/", "");//name.Text;

                }
            }
            else
            {
                RadWindowManager1.RadAlert("جلسه دفاع در حال حاضر در دسترس نیست", 500, 100, "خطا", "");
                return;
            }


                string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
                Response.Redirect(link);
            }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnTesti_Click(object sender, EventArgs e)
        {

                LoginBusiness lgb = new LoginBusiness();
                FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
                var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
                DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
                if (dtHR != null && dtHR.Rows.Count > 0)
                {
                    string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                    string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                    adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                    adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_9";

                }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);
        }

        protected void btnTesti2_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_2";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);

        }

        protected void btnTesti3_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_3";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);
        }

        protected void btnTesti4_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_4";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);
        }

        protected void btnTesti5_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_5";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);

        }

        protected void btnTesti6_Click(object sender, EventArgs e)
        {
            LoginBusiness lgb = new LoginBusiness();
            FacultyReportsBusiness facultyReportsBusiness = new FacultyReportsBusiness();
            var userAdobe = "200" + Session[sessionNames.userID_StudentOstad].ToString();
            DataTable dtHR = facultyReportsBusiness.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (dtHR != null && dtHR.Rows.Count > 0)
            {
                string firtsName = dtHR.Rows[0]["name"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["name"].ToString().Trim();
                string lastName = dtHR.Rows[0]["family"].ToString().Trim() == "" ? "نامشخص" : dtHR.Rows[0]["family"].ToString().Trim();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_6";

            }

            string link = "http://" + adobeBusiness.OpenMeetingAsPresnter(adobeConnectDTO);
            Response.Redirect(link);
        }
    }
    }
    
