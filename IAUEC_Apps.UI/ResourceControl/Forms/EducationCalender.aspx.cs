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
    public partial class EducationCalender : System.Web.UI.Page
    {
        private RequestHandler _requestHandler = new RequestHandler();
        private CommonBusiness commonBusiness = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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

            if (txtDate1.Text == "" || txtDate2.Text == "" || txtTaSal.Text == ""
                || txtAzSal.Text == "" || txtTerm.Text == "")
                return;
            bool flagVir = false;
            string curTerm = CreateTerm(txtAzSal.Text, txtTaSal.Text, txtTerm.Text);
            string message = Validation(txtDate1.Text, txtDate2.Text, curTerm, flagVir);
            if (message.Contains("ok"))
            {
                var userId = Session[sessionNames.userID_Karbar].ToString();
                _requestHandler.EnterEducationCalender(txtDate1.Text, txtDate2.Text, curTerm, 11);
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
               , 11, 240
               , "ایجاد ترم جاری برای دفاع");
            }
            else
            {
                RadWindowManager1.RadAlert(message, 500, 100, "خطا", "");
                return;
            }

            rfrshDisplayEducationCalender();
            grdEducationCalender.Rebind();
            txtAzSal.Text = "";
            txtTaSal.Text = "";
            txtTerm.Text = "";
            txtDate1.Text = "";
            txtDate2.Text = "";

        }
        public string Validation(string dateBegin, string dateEnd, string curTerm, bool flagVir)
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
            int startYear = int.Parse(curTerm.Substring(0, 2));
            int endYear = int.Parse(curTerm.Substring(3, 2));
            int term = int.Parse(curTerm.Substring(6, 1));
            if (startYear < 0 || endYear < 0 || term < 0 || term > 4)
            {
                return "فرمت ترم وارد شده اشتباه است";
            }

            if (!flagVir)
            {
                DataTable dt = _requestHandler.GetEducationCalender(11, -1, curTerm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return "چنین ترمی موجود است";
                }
            }
            else
            {
                DataTable dt = _requestHandler.GetEducationCalender(11, -1, curTerm);
                if (dt != null && dt.Rows.Count > 1)
                {
                    return "چنین ترمی موجود است";
                }
            }

            return "ok";
        }




        protected void BtnVirChange_Click(object sender, EventArgs e)
        {
            if (txtVirDate1.Text == "" || txtVirDate2.Text == "" || LabelIdVir.Value == "" || txtVirTaSal.Text == ""
                || txtVirAzSal.Text == "" || txtVirTerm.Text == "") return;
            bool flagVir = true;
            string curTerm = CreateTerm(txtVirAzSal.Text, txtVirTaSal.Text, txtVirTerm.Text);
            string msg = Validation(txtVirDate1.Text, txtVirDate2.Text, curTerm, flagVir);
            if (msg.Contains("ok"))
            {
                var userId = Session[sessionNames.userID_Karbar].ToString();
                _requestHandler.UpdateEducationCalender(int.Parse(LabelIdVir.Value), txtVirDate1.Text, txtVirDate2.Text, curTerm, 11);
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
            , 11, 241
            , "ویرایش ترم جاری برای دفاع");
            }
            else
            {
                RadWindowManager1.RadAlert(msg, 500, 100, "خطا", "");
                return;
            }
      
            rfrshDisplayEducationCalender();
            grdEducationCalender.Rebind();
            txtVirAzSal.Text = "";
            txtVirTaSal.Text = "";
            txtVirTerm.Text = "";
            txtVirDate1.Text = "";
            txtVirDate2.Text = "";


        }


        public void rfrshDisplayEducationCalender()
        {
            DataTable dt = _requestHandler.GetEducationCalender(11);

            if (dt != null && dt.Rows.Count > 0)
                grdEducationCalender.DataSource = dt;
            else
                grdEducationCalender.DataSource = string.Empty;
            GridFilterMenu menu = grdEducationCalender.FilterMenu;
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


        //public void SetTerm()
        //    {
        //        if (txtDate1.Text == "" || txtDate2.Text == "") return;
        //        if (txtDate1.Text.Substring(0, 4) == txtDate2.Text.Substring(0, 4))
        //        {
        //            txtAzSal.Text = int.Parse(txtDate1.Text.Substring(2, 2)).ToString();
        //            txtTaSal.Text = (int.Parse(txtDate1.Text.Substring(2, 2)) + 1).ToString();
        //            txtTerm.Text = "1";
        //        }
        //        if (txtDate1.Text.Substring(0, 4) != txtDate2.Text.Substring(0, 4))
        //        {
        //            txtAzSal.Text = (int.Parse(txtDate2.Text.Substring(2, 2)) - 1).ToString();
        //            txtTaSal.Text = (int.Parse(txtDate2.Text.Substring(2, 2))).ToString();
        //            txtTerm.Text = "2";
        //        }
        //    }

        //protected void txtDate1_TextChanged(object sender, EventArgs e)
        //{
        //    SetTerm();
        //}

        //protected void txtDate2_TextChanged(object sender, EventArgs e)
        //{
        //    SetTerm();
        //}

        protected void grdEducationCalender_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rfrshDisplayEducationCalender();
        }
        public string CreateTerm(string azSal, string taSal, string term)
        {
            return azSal + "-" + taSal + "-" + term;
        }
    }
}
