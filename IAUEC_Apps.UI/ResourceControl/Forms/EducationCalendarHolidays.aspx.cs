using IAUEC_Apps.Business.Common;
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
   
    public partial class EducationCalendarHolidays : System.Web.UI.Page
    {
        private const bool IsForEmployee = true;
        private const bool IsForStudent = true;
        private RequestHandler _requestHandler = new RequestHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mId = Request.QueryString["id"].ToString();
                string[] id = mId.ToString().Split(new char[] { '@' });
                string menuId = "";
                for (int i = 0; i < id[1].Length; i++)
                {
                    string s = id[1].Substring(i + 1, 1);
                    if (s != "-")
                        menuId += s;
                    else
                        break;
                }
                Session[sessionNames.menuID] = menuId;
                AccessControl.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTatili.Text == "" )
                return;
            DataTable dtCheckDefence = _requestHandler.CheckDefenceInformationByRequestDate(txtTatili.Text,txtTatili.Text);
            DataTable dtCheckCalenderHoliday = _requestHandler.CheckExistCalendarHolidayDate(txtTatili.Text, txtTatili.Text);
            CommonBusiness commonBusiness = new CommonBusiness();

            if (dtCheckDefence != null && dtCheckDefence.Rows.Count > 0)
            {
                RadWindowManager1.RadAlert(" دانشجویان در این تاریخ جلسه دفاع برگزار می‌کنند", 500, 100, "خطا", "");
                return;
            }
            
            else if(dtCheckCalenderHoliday!=null&& dtCheckCalenderHoliday.Rows.Count>0)
            {
                RadWindowManager1.RadAlert("چنین تاریخ تعطیلی موجود است.", 500, 100, "خطا", "");
                return;
            }
            else {
                var userId = Session[sessionNames.userID_Karbar].ToString();
                _requestHandler.EnterCalenderHoliday(txtTatili.Text.Trim(), txtDsc.Text.Trim(), IsForStudent, IsForEmployee);
                
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                  , 11, 238
                  , "ثبت تاریخ تعطیلی برگزاری دفاع");


            }
            rfrshDisplayCalenderHoliday();
             grdCalenderHolidays.Rebind();
            txtTatili.Text = "";
            txtDsc.Text = "";

        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Label lblid = (Label)item.FindControl("lblid");
        //    Label lblDate = (Label)item.FindControl("lbldate");
            if (lblid.Text != "")
            {   //delete
                _requestHandler.DeleteCalenderHoliday(int.Parse(lblid.Text));
                var userId = Session[sessionNames.userID_Karbar].ToString();
                CommonBusiness commonBusiness = new CommonBusiness();
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                               , 11, 239
                               , "حذف تاریخ تعطیلی برگزاری دفاع");

                rfrshDisplayCalenderHoliday();
                grdCalenderHolidays.Rebind();
            }

        }

        protected void grdCalenderHolidays_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rfrshDisplayCalenderHoliday();
        }
        public void rfrshDisplayCalenderHoliday()
        {
            DataTable dt = _requestHandler.GetCalenderHoliday();

            if (dt != null && dt.Rows.Count > 0)
                grdCalenderHolidays.DataSource = dt;
            else
                grdCalenderHolidays.DataSource = string.Empty;
            GridFilterMenu menu = grdCalenderHolidays.FilterMenu;
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
    }

}