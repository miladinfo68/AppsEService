using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Conatct.Functions;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class EducationOnlineDefensePlayback : System.Web.UI.Page
    {
        const string passAdobeUser = "4sx0pvauo4nleowu5ugvkkx9l0bpsbe";
       static Business.Adobe.AdobeBusiness adobeBusiness = new Business.Adobe.AdobeBusiness();
       static AdobeConnectDTO adobeConnectDTO = new AdobeConnectDTO();
        RequestHandler requestHandler = new RequestHandler();
       static private CommonBusiness commonBusiness = new CommonBusiness();
        string user;
       static string userName;
       static string RoleName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              userName=  Session[sessionNames.userName_Karbar]?.ToString();
              RoleName = Session[sessionNames.roleText]?.ToString();
                drpCollegeId1();

                drpCollegeId.SelectedValue = "-1";
     
            }
            RegPcal();
           
        }
        /*VazMeeting
         1-online
         2-past
         3.future   
        */
        public void drpCollegeId1()
        {

            DataTable dt= commonBusiness.SelectAllDaneshkade();
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

                        // bool flagStartMeeting = row["FlagStartMeeting"] == null || row["FlagStartMeeting"].ToString() == "False" ? false : true;
                        //bool FlagEndMeeting = row["FlagEndMeeting"] == null || row["FlagEndMeeting"].ToString() == "False" ? false : true;
                        //int flagvaz = DatePersian.CompareDate(row["RequestDate"].ToString(),
                        //  row["startTime"].ToString(), row["endTime"].ToString());




                        //if (!flagStartMeeting)
                        //{
                        //    if (flagvaz == -1)
                        //    {
                        //        row["vazName"] = "زمان شروع فرانرسیده";
                        //        row["vazId"] = "3";
                        //        row["vazSrc"] = "Abi.gif";
                        //    }
                        //    else if (flagvaz == 0)
                        //    {
                        //        row["vazName"] = "باتاخیرآغازمی شود";
                        //        row["vazId"] = "3";
                        //        row["vazSrc"] = "Abi.gif";
                        //    }
                        //    else if(flagvaz==1)
                        //    {
                        //        row["vazName"] = "خاتمه یافته";
                        //        row["vazId"] = "2";
                        //        row["vazSrc"] = "Grey.gif";
                        //    }
                        //}
                        //    else if(FlagEndMeeting)
                        //    { 
                        //        //else if (flagvaz == 1)
                        //        {
                        //            row["vazName"] = "خاتمه یافته";
                        //            row["vazId"] = "2";
                        //            row["vazSrc"] = "Grey.gif";
                        //        }
                        //    }
                        //    else if(flagStartMeeting&&!FlagEndMeeting)
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

        public void RfrhgrdDefenceMeetings(int collegeId=-1)
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




        protected void grdDsiplayDefence_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }





        protected void grdDsiplayDefence_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RfrhgrdDefenceMeetings(int.Parse(drpCollegeId.SelectedItem.Value.ToString()));
        }

        private void RegPcal()
        {
            string scrp1 = "var objCal1 = new AMIB.persianCalendar('" + pcal1.ClientID + "', {extraInputID: '" + pcal1.ClientID + "',extraInputFormat: 'yyyy/mm/dd'        ,onchange: function( pdate ){checkCal();}}); ";

            string scrp = "setTimeout(function(){ " + scrp1 +"}, 300);";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, scrp, true);
        }
        private void EnableShowResultMessage(string msg)
        {
            lblShowErrorMessage.Text = msg;
            lblShowErrorMessage.Visible = true;
        }

        protected void BtnChangeDate_Click(object sender, EventArgs e)
        {
            var fromDate = this.pcal1.Text;
            var startTime = RadTimePicker1.SelectedTime?.ToString().Substring(0, 5);
            var endTime = RadTimePicker2.SelectedTime?.ToString().Substring(0, 5);
            var curId =this.txtCurId.Text ;
            if (requestHandler.UpdateDateTime_DefenceMeetingBusiness(curId, fromDate, startTime, endTime) == true)
                txtCurId.Text = "عملیات با موفقیت انجام شد";
            else
                txtCurId.Text = "عملیات دارای خطا می باشد";


        }
        [WebMethod]
        public static string GetDefenceLink(string vazid, string studentCode, string resLink, string userId)
        {
            //System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            //GridDataItem item = (GridDataItem)btn.NamingContainer;
            //Label vazId = (Label)item.FindControl("lblVazId");


       
                string link = "http://" + OpenMeeting(resLink, userId, studentCode);
                //Response.Redirect(link);
                return link;
          
        }
        [WebMethod]
        public static string BtnLinkTesti( string userId)
        {
            
            

                var userAdobe = userId;

                string firtsName = userName + "-"; ;//stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
                                              //string lastName = "کارشناس پژوهش-"; //stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
                string lastName = RoleName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);



               adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_9";//name.Text;

            

            return  "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);
         

        }

        [WebMethod]
        public static string BtnLinkTesti2(string userId)
        {



            var userAdobe = userId;

            string firtsName = userName + "-"; ;//stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
                                          // string lastName = "کارشناس پژوهش-"; //stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            string lastName = RoleName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);



            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_2";//name.Text;



            return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);


        }
        [WebMethod]
        public static string BtnLinkTesti3(string userId)
        {



            var userAdobe = userId;

            string firtsName = userName + "-"; ;//stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
                                          //  string lastName = "کارشناس پژوهش-"; //stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            string lastName = RoleName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);



            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_3";//name.Text;



            return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);


        }
        [WebMethod]
        public static string BtnLinkTesti4(string userId)
        {



            var userAdobe = userId;

            string firtsName = userName + "-"; ;//stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
                                          //string lastName = "کارشناس پژوهش-"; //stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            string lastName = RoleName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);



            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_4";//name.Text;



            return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);


        }
        [WebMethod]
        public static string BtnLinkTesti5(string userId)
        {



            var userAdobe = userId;

            string firtsName = userName + "-"; ;//stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
                                          // string lastName = "کارشناس پژوهش-"; //stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            string lastName = RoleName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);



            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_5";//name.Text;



            return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);


        }
        [WebMethod]
        public static string BtnLinkTesti6(string userId)
        {



            var userAdobe = userId;

            string firtsName = userName + "-"; ;//stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
                                          //string lastName = "کارشناس پژوهش-"; //stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            string lastName = RoleName;
            adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);



            adobeConnectDTO.MeetingUrlPath = "/st99900999_13981107_6";//name.Text;



            return "http://" + adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);


        }
        //protected void lnkLinkDefence_Click(object sender, EventArgs e)


        //{ 
        // //   else
        //    //{
        //    //    RadWindowManager1.RadAlert("جلسه دفاع در حال حاضر در دسترس نیست", 500, 100, "خطا", "");
        //    //    return;
        //    //}


        //    string link = "http://" +OpenMeeting(sender);
        //    Response.Redirect(link);
        //}

        protected void drpCollegeId_SelectedIndexChanged(object sender, EventArgs e)
        {

            RfrhgrdDefenceMeetings(int.Parse(drpCollegeId.SelectedItem.Value.ToString()));
            grdDsiplayDefence.DataBind();

        }

        protected void btnOpenMeeting_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Button btnOpenMeeting = (Button)item.FindControl("btnOpenMeeting");
            Button btnEndMeeting = (Button)item.FindControl("btnEndMeeting");
            Button btnEndedMeeting = (Button)item.FindControl("btnEndedMeeting");
            Label lblstudentcode = (Label)item.FindControl("lblstudentcode");
            Label lblRequestid=(Label)item.FindControl("lblRequestid");
            btnOpenMeeting.Visible = false;
            btnEndMeeting.Visible = true;
            btnEndedMeeting.Visible = false;

            var userId = Session[sessionNames.userID_Karbar].ToString();
            //requestHandler.UpdateFlagMeeting_DefenceMeeting(lblstudentcode.Text, true, false);
            requestHandler.UpdateFlagMeeting_DefenceMeeting(lblRequestid.Text.Trim(), true, false);
            commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
              , 11, 227
              , "باز کردن دفاع آنلاین به صورت سیستمی ", int.Parse(lblRequestid.Text.Trim()));

            OpenMeeting(sender);
            RfrhgrdDefenceMeetings(int.Parse(drpCollegeId.SelectedItem.Value.ToString()));
            grdDsiplayDefence.DataBind();
        }

        protected void btnEndMeeting_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Button btnOpenMeeting = (Button)item.FindControl("btnOpenMeeting");
            Button btnEndMeeting = (Button)item.FindControl("btnEndMeeting");
            Button btnEndedMeeting = (Button)item.FindControl("btnEndedMeeting");
            Label lblstudentcode = (Label)item.FindControl("lblstudentcode");
            Label lblRequestid = (Label)item.FindControl("lblRequestid");
            btnOpenMeeting.Visible = false;
            btnEndMeeting.Visible = false;
            btnEndedMeeting.Visible = true;
            btnEndedMeeting.Text = "جلسه خاتمه یافته";
            btnEndedMeeting.Enabled = false;

            //  requestHandler.UpdateFlagMeeting_DefenceMeeting(lblstudentcode.Text, true, true);
            requestHandler.UpdateFlagMeeting_DefenceMeeting(lblRequestid.Text.Trim(), true, true);

            var userId = Session[sessionNames.userID_Karbar].ToString();
            commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
              , 11, 228
              , "بستن دفاع آنلاین به صورت سیستمی ", int.Parse(lblRequestid.Text.Trim()));
            RfrhgrdDefenceMeetings(int.Parse(drpCollegeId.SelectedItem.Value.ToString()));
            grdDsiplayDefence.DataBind();
        }

        public static string OpenMeeting(string resLink, string userId,string studentCode)
        {

            //System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            //GridDataItem item = (GridDataItem)btn.NamingContainer;
            //Label vazId = (Label)item.FindControl("lblVazId");
            //Label stcode = (Label)item.FindControl("lblstudentcode");


            {
                LoginBusiness lgb = new LoginBusiness();

                var userAdobe = userId;//Session[sessionNames.userID_Karbar].ToString();

               
                string firtsName = userName+"-";//stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
                string lastName = RoleName; //stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);


                //var Link = item.FindControl("resLink") as Label;

                adobeConnectDTO.MeetingUrlPath = "/" + resLink.Replace("/", "");//name.Text;

            }

            commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
          , 11, 229
          , "ورود به دفاع آنلاین به صورت سیستمی به عنوان هاست ", int.Parse(studentCode));
            return adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);
         

        }
        public string OpenMeeting(object sender)
        {

            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Label vazId = (Label)item.FindControl("lblVazId");
            Label stcode = (Label)item.FindControl("lblstudentcode");

            //    if (vazId.Text == "1")
            {
                LoginBusiness lgb = new LoginBusiness();

                var userAdobe = Session[sessionNames.userID_Karbar].ToString();

                string firtsName = Session[sessionNames.userName_Karbar]?.ToString();//stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
                string lastName = "کارشناس پژوهش-"; //stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
                adobeConnectDTO.SetValueDefult(userAdobe, passAdobeUser, firtsName, lastName);


                var Link = item.FindControl("resLink") as Label;

                adobeConnectDTO.MeetingUrlPath = "/" + Link.Text.Replace("/", "");//name.Text;

            }
            string userId = Session[sessionNames.userID_Karbar].ToString();
            commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                      , 11, 229
                      , "ورود به دفاع آنلاین به صورت سیستمی به عنوان هاست ", int.Parse(stcode.Text));
            return adobeBusiness.OpenMeetingAsHost(adobeConnectDTO);

        }




        //#################################


    }
}
