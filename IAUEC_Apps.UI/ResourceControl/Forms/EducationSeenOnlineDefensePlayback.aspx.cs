using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Conatct.Functions;
using IAUEC_Apps.DTO.AdobeClasses;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class EducationSeenOnlineDefensePlayback : System.Web.UI.Page
    {
        const string passAdobeUser = "4sx0pvauo4nleowu5ugvkkx9l0bpsbe";
        static Business.Adobe.AdobeBusiness adobeBusiness = new Business.Adobe.AdobeBusiness();
        static AdobeConnectDTO adobeConnectDTO = new AdobeConnectDTO();
        RequestHandler requestHandler = new RequestHandler();
        private CommonBusiness commonBusiness = new CommonBusiness();
        string user;
        public int AccessUser()
        {
            LoginBusiness LoginB = new LoginBusiness();
            string userId = Session[sessionNames.userID_Karbar].ToString();
            DataTable userRoles = LoginB.Get_UserRoles(userId);
            int UserRole = int.Parse(userRoles.Rows[0]["RoleId"].ToString());
            if (UserRole == 1 || UserRole == 2 || UserRole == 9 || UserRole == 10)
            {
                drpCollegeId.Visible = true;
                litCollege.Visible = false;
                return -1;
            }
            else
            {
                litCollege.Visible = true;
                drpCollegeId.Visible = false;
                if (UserRole == 17 || UserRole == 28)
                {
                    litCollege.Text = "دانشکده علـوم انـساني";
                    return 1;

                }
                else if (UserRole == 15 || UserRole == 26)
                {
                    litCollege.Text = "دانشکده فني و مهندسي";
                    return 2;
                }
                else if (UserRole == 16 || UserRole == 27)
                {
                    litCollege.Text = "دانشکده مديريت";
                    return 3;
                }
                else if (UserRole == 67 || UserRole == 68)
                {
                    litCollege.Text = "دانشکده علوم پايه و فناوري هاي نوين";
                    return 8;
                }


            }
            return -2;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpCollegeId1();
                int accessUser = AccessUser();
                if (accessUser != -2)
                    drpCollegeId.SelectedValue = accessUser.ToString();
                else
                    Response.Redirect("../../CommonUI/CommonCmsIntro.aspx");
                Session[sessionNames.userName_Karbar].ToString();
            }


        }
        /*VazMeeting
         1-online
         2-past
         3.future   
        */
        public void drpCollegeId1()
        {

            DataTable dt = commonBusiness.SelectAllDaneshkade();
            DataRow drrow = dt.NewRow();
            drrow["id"] = "-1";
            drrow["namedanesh"] = "همه موارد";
            dt.Rows.Add(drrow);
            dt.DefaultView.Sort = "id asc";
            drpCollegeId.DataValueField = "id";
            drpCollegeId.DataTextField = "namedanesh";
            drpCollegeId.DataSource = dt;
            drpCollegeId.DataBind();
        }
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

        public void RfrhgrdDefenceMeetings(int collegeId = -1)
        {
            user = Session[sessionNames.userID_Karbar].ToString();
            DataTable dt = new DataTable();
            dt = requestHandler.GetMeetingDefencesbyCollegeIdBusiness(collegeId);

            if (dt != null && dt.Rows.Count > 0)
            {

                grdDsiplayDefence.DataSource = VazMeeting(dt);
            }
            else
                grdDsiplayDefence.DataSource = string.Empty;

            GridFilterMenu menu = grdDsiplayDefence.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }


                }

            }

        }
    

        protected void grdDsiplayDefence_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RfrhgrdDefenceMeetings(int.Parse(drpCollegeId.SelectedItem.Value.ToString()));
        }






        //protected void lnkLinkDefence_Click(object sender, EventArgs e)


        //{
        //    System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
        //    GridDataItem item = (GridDataItem)btn.NamingContainer;
        //    Label vazId = (Label)item.FindControl("lblVazId");


        //    if (vazId.Text == "1")
        //    {
        //        string link = "http://" + OpenMeeting(sender);
        //        Response.Redirect(link);
        //    }
        //    else
        //    {
        //        RadWindowManager1.RadAlert("جلسه دفاع در حال حاضر در دسترس نیست", 500, 100, "خطا", "");
        //        return;
        //    }

        //}

        [WebMethod]
        public static string GetDefenceLink(string vazid, string studentCode, string resLink, string userId,string userName="")
        {
            //System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            //GridDataItem item = (GridDataItem)btn.NamingContainer;
            //Label vazId = (Label)item.FindControl("lblVazId");


            if (vazid == "1")
            {
                string link = "http://" + OpenMeeting(resLink, userId, userName);
                //Response.Redirect(link);
                return link;
            }
            else
            {
                //RadWindowManager1.RadAlert("جلسه دفاع در حال حاضر در دسترس نیست", 500, 100, "خطا", "");
                return "false";
            }
        }
        [WebMethod]
        public static string BtnLinkTesti(string userId,string userName)
        {
            LoginBusiness lgb = new LoginBusiness();

            var userAdobe = userId;

            DataTable dtuserRoles = lgb.Get_UserRoles(userAdobe);
            string firtsName = userName;
            string lastName = dtuserRoles.Rows[0]["RoleName"].ToString();

            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_9";//name.Text;
            return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);

        }
        [WebMethod]
        public static string BtnLinkTesti2(string userId, string userName)
        {
            LoginBusiness lgb = new LoginBusiness();

            var userAdobe = userId;

            DataTable dtuserRoles = lgb.Get_UserRoles(userAdobe);
            string firtsName = userName;
            string lastName = dtuserRoles.Rows[0]["RoleName"].ToString();

            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_2";//name.Text;
            return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);

        }
        [WebMethod]
        public static string BtnLinkTesti3(string userId, string userName)
        {
            LoginBusiness lgb = new LoginBusiness();

            var userAdobe = userId;

            DataTable dtuserRoles = lgb.Get_UserRoles(userAdobe);
            string firtsName = userName;
            string lastName = dtuserRoles.Rows[0]["RoleName"].ToString();

            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_3";//name.Text;
            return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);

        }
        [WebMethod]
        public static string BtnLinkTesti4(string userId, string userName)
        {
            LoginBusiness lgb = new LoginBusiness();

            var userAdobe = userId;

            DataTable dtuserRoles = lgb.Get_UserRoles(userAdobe);
            string firtsName = userName;
            string lastName = dtuserRoles.Rows[0]["RoleName"].ToString();

            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_4";//name.Text;
            return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);
        }
            [WebMethod]
            public static string BtnLinkTesti5(string userId, string userName)
            {
                LoginBusiness lgb = new LoginBusiness();

                var userAdobe = userId;

                DataTable dtuserRoles = lgb.Get_UserRoles(userAdobe);
                string firtsName = userName;
                string lastName = dtuserRoles.Rows[0]["RoleName"].ToString();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_5";//name.Text;
                return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);

            }
            [WebMethod]
            public static string BtnLinkTesti6(string userId, string userName)
            {
                LoginBusiness lgb = new LoginBusiness();

                var userAdobe = userId;

                DataTable dtuserRoles = lgb.Get_UserRoles(userAdobe);
                string firtsName = userName;
                string lastName = dtuserRoles.Rows[0]["RoleName"].ToString();

                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);
                adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_6";//name.Text;
                return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);

            }
        

        protected void drpCollegeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int accessUser = AccessUser();
            if (accessUser != -2)
            {
                if (accessUser == -1)
                {
                    RfrhgrdDefenceMeetings(int.Parse(drpCollegeId.SelectedItem.Value.ToString()));

                }
                else
                    RfrhgrdDefenceMeetings(accessUser);
                grdDsiplayDefence.DataBind();
            }
            else
                Response.Redirect("../../CommonUI/CommonCmsIntro.aspx");

        }



        public static string OpenMeeting(string resLink, string userId,string username)
        {

            //System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            //GridDataItem item = (GridDataItem)btn.NamingContainer;
            //Label vazId = (Label)item.FindControl("lblVazId");
            //Label stcode = (Label)item.FindControl("lblstudentcode");

           
            {
                LoginBusiness lgb = new LoginBusiness();

                var userAdobe = userId;//Session[sessionNames.userID_Karbar].ToString();
              
                DataTable dtuserRoles = lgb.Get_UserRoles(userAdobe);

                string firtsName = username;
                string lastName = dtuserRoles.Rows[0]["RoleName"].ToString(); 
              
                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);


                //var Link = item.FindControl("resLink") as Label;

                adobeConnectDTO.MeetingUrlPath = "/" + resLink.Replace("/", "");//name.Text;

            }
           
            
            return adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);

        }


            
             

        }



        //#################################


    }

