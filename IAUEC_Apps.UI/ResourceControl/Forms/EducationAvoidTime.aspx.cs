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
    public partial class EducationAvoidTime : System.Web.UI.Page
    {
        private RequestHandler _requestHandler = new RequestHandler();
        private CommonBusiness commonBusiness = new CommonBusiness();
        protected void SetCustomTime()
        {
            txtTime.TimeView.CustomTimeValues = _requestHandler.GetTime();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCustomTime();
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
        public string Validation(string dateBegin, string dateEnd, string time, int justForOnline)
        {
            if (!_requestHandler.CheckReqDate(dateBegin))
            {
                return "تاریخ آغازین باید بعد از تاریخ امروز باشد.";
            }
            else if (!_requestHandler.CheckReqDate(dateEnd))
            {
                return "تاریخ پایانی باید بعد از تاریخ امروز باشد.";
            }
            if (string.Compare(dateBegin, dateEnd) > 0)
            {
                return "تاریخ انتهایی باید بعد از تاریخ ابتدایی باشد";
            }
            DataTable dtCheckAvoidTime = _requestHandler.GetAvoidTime(dateBegin, dateEnd, time.Substring(0,2), justForOnline);
            if (dtCheckAvoidTime != null && dtCheckAvoidTime.Rows.Count > 0)
            {
                return "چنین بازه تاریخی  برای این ساعت وجود دارد.";
            }
            DataTable dtCheckDefence = _requestHandler.CheckDefenceInformationByRequestDate(dateBegin, dateEnd,time,justForOnline.ToString());
            if (dtCheckDefence != null && dtCheckDefence.Rows.Count > 0)
            {
                return "در این بازه تاریخی این ساعت دفاع در گردش موجود است";
            }
            return "ok";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtStartDate.Text == "" || txtEndDate.Text == "")
                return;
            string time = txtTime.SelectedTime.Value.Hours.ToString();
            int justForOnline = chkJustForOnline.Checked == true ? 1 : -1;

            string msg = Validation(txtStartDate.Text, txtEndDate.Text, time+":00", justForOnline);
            if (msg.Contains("ok"))
            {
                var userId = Session[sessionNames.userID_Karbar].ToString();
                _requestHandler.EnterAvoidTime(txtStartDate.Text.Trim(), txtEndDate.Text, time
                    , chkJustForOnline.Checked
                                           );
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
              , 11, 236
              , " ثبت بستن زمان دفاع");
                txtStartDate.Text = "";
                txtEndDate.Text = "";
            }

            else
            {
                RadWindowManager1.RadAlert(msg, 500, 100, "خطا", "");
                return;
            }
            rfrshDisplayAvoidTime();
            grdAvoidTime.Rebind();
     


        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Label lblStartDate = (Label)item.FindControl("lblStartDate");
            Label lblEndDate = (Label)item.FindControl("lblEndDate");
            Label lblTime = (Label)item.FindControl("lblTime");
            Label lblJustForOnline = (Label)item.FindControl("lblJustForOnline");
            bool justForOnline = lblJustForOnline.Text.Trim() == "بله" ? true : false;

            if (lblStartDate.Text != "" && lblEndDate.Text != "" && lblTime.Text != "")

            {

              //delete
                _requestHandler.DeleteAvoidTime(lblStartDate.Text, lblEndDate.Text, lblTime.Text, justForOnline);
                var userId = Session[sessionNames.userID_Karbar].ToString();
                CommonBusiness commonBusiness = new CommonBusiness();
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
               , 11, 237
               , " حذف بستن زمان دفاع ");

                rfrshDisplayAvoidTime();
                grdAvoidTime.Rebind();
            }

        }


        public void rfrshDisplayAvoidTime()
        {
            DataTable dt = _requestHandler.GetAvoidTime();

            if (dt != null && dt.Rows.Count > 0)
                grdAvoidTime.DataSource = dt;
            else
                grdAvoidTime.DataSource = string.Empty;
            GridFilterMenu menu = grdAvoidTime.FilterMenu;
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


        protected void grdAvoidTime_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rfrshDisplayAvoidTime();
        }
    }
}