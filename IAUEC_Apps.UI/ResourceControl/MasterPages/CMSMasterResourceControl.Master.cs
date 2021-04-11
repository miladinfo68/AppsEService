using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using System.Data;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.MasterPages
{
    public partial class CMSMasterResourceControl : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
            else
            {
                Session[sessionNames.appID_Karbar] = "11";
                int RoleID = Convert.ToInt32(Session["RoleID"]);
                int DaneshId = 0;
                // int SectionId = 0;
                switch (int.Parse(Session["SectionId"].ToString()))
                {
                    case 11:

                        //ensani
                        DaneshId = 1;
                        break;
                    case 9:
                        //fani
                        DaneshId = 2;
                        break;
                    case 10:
                        //modiriat
                        DaneshId = 3;
                        break;
                    case 14:
                        //Oloom payeh
                        DaneshId = 8;
                        break;
                    case 13:

                        //kootah modat
                        DaneshId = 5;
                        break;
                    default:
                        break;
                }
                Session.Add("DaneshId", DaneshId);

                LoginBusiness logBusiness = new LoginBusiness();
                //stName.InnerText = Session["name"].ToString();
                DataTable dt = new DataTable();
                dt = logBusiness.Get_Menu_ByUserIdAndAppId(int.Parse(Session[sessionNames.appID_Karbar].ToString()), int.Parse(Session[sessionNames.userID_Karbar].ToString()), int.Parse(Session[sessionNames.sectionID].ToString()));
                string cat = "";
                DataTable dtmenu = new DataTable();
                dtmenu.Columns.Add("MenuLink");
                dtmenu.Columns.Add("linkCode");
                dtmenu.Columns.Add("icon");
                dtmenu.Columns.Add("MenuName");
                DataRow dr;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (cat == "")
                    {
                        cat = dt.Rows[i]["CategoryId"].ToString();
                        lstreportMenu.Text += "<li><a>" + dt.Rows[i]["CategoryName"].ToString() + "<span class='fa fa-chevron-down'> </span></a><ul class='nav child_menu' style='display: none'>";

                    }

                    if (cat != dt.Rows[i]["CategoryId"].ToString())
                    {

                        lstreportMenu.Text += " </ul></li>";
                        cat = dt.Rows[i]["CategoryId"].ToString();
                        lstreportMenu.Text += "<li><a>" + dt.Rows[i]["CategoryName"].ToString() + "<span class='fa fa-chevron-down'> </span></a><ul class='nav child_menu' style='display: none'>";
                        lstreportMenu.Text += "  <li><a href='" + "../" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + generaterandomstr() + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr() + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";
                    }
                    else
                        lstreportMenu.Text += "  <li><a href='" + "../" + dt.Rows[i]["MenuLink"].ToString() + "?id=" + generaterandomstr() + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr() + "'><span>" + dt.Rows[i]["MenuName"].ToString() + "</span></a></li>";


                    dr = dtmenu.NewRow();
                    dr["MenuLink"] = dt.Rows[i]["MenuLink"].ToString();
                    dr["icon"] = dt.Rows[i]["icon"].ToString();
                    dr["MenuName"] = dt.Rows[i]["MenuName"].ToString();
                    string link = generaterandomstr() + "@A" + dt.Rows[i]["MenuId"].ToString() + "-" + generaterandomstr();
                    dr["linkCode"] = link;
                    if (RoleID != 1)
                    {
                        if (dr["MenuLink"].ToString().ToLower().Contains("officeuserreview"))
                        {
                            Session["linkeeee"] = dr["MenuLink"].ToString() + "?id=" + link;
                        }
                        if (dr["MenuLink"].ToString().ToLower().Contains("educationuserreview"))
                        {
                            Session["linkeeee"] = dr["MenuLink"].ToString() + "?id=" + link;
                        }
                        if (dr["MenuLink"].ToString().ToLower().Contains("educationstudentreview"))
                        {
                            Session["DefenceLink1"] = dr["MenuLink"].ToString() + "?id=" + link;
                        }
                        if (dr["MenuLink"].ToString().ToLower().Contains("officestudentreview"))
                        {
                            Session["DefenceLink1"] = dr["MenuLink"].ToString() + "?id=" + link;
                        }
                        if (dr["MenuLink"].ToString().ToLower().Contains("technicalstudentreview"))
                        {
                            Session["DefenceLink1"] = dr["MenuLink"].ToString() + "?id=" + link;
                        }
                    }
                    else
                    {
                        Session["linkeeee"] = "forms/EducationUserReview.aspx" + "?id=" + link;
                        Session["DefenceLink1"] = "forms/EducationStudentReview.aspx" + "?id=" + link;
                        Session["linke2"] = "forms/OfficeUserReview.aspx" + "?id=" + link;
                        Session["DefenceLink2"] = "forms/OfficeStudentReview.aspx" + "?id=" + link;
                    }


                    //if (dt.Rows[i]["MenuName"].ToString() == "بررسی درخواست های ثبت شده")
                    //{
                    //    HiddenField1.Value = dt.Rows[i]["MenuLink"].ToString() + "?id=" + link;
                    //}
                    //if (dt.Rows[i]["MenuName"].ToString() == "بررسی درخواست ها")
                    //{
                    //    HiddenField2.Value = dt.Rows[i]["MenuLink"].ToString() + "?id=" + link;
                    //}

                }
            }

            if (!(Session["IsGroupManger"] == null) && Convert.ToBoolean(Session["IsGroupManger"]))
            {
                changePass.Visible = false;

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
        }

        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        protected void changePass_ServerClick(object sender, EventArgs e)
        {
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = "../../CommonUI/ChangePassword.aspx";
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
            windowManager.Windows.Add(widnow1);
            this.form1.Controls.Add(widnow1);
        }

        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../CommonUI/CommonCmsIntro.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session[sessionNames.userID_Karbar] = null;
            if (!(Session["IsGroupManger"] == null) && Convert.ToBoolean(Session["IsGroupManger"]))
            {
                Session[sessionNames.userID_StudentOstad] = null;

                Session["LogStatus"] = "0-0";
                Response.Redirect("~/CommonUI/login.aspx");
            }
            Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
        }


    }
}