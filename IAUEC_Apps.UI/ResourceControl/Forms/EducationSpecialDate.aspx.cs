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
    public partial class EducationSpecialDate : System.Web.UI.Page
    {

        private RequestHandler _requestHandler = new RequestHandler();
        private CommonBusiness commonBusiness = new CommonBusiness();
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
        public string Validation(string dateBegin, string dateEnd,bool flagVir)
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
            DataTable dtCheckDefence = _requestHandler.CheckDefenceInformationByRequestDate(dateBegin, dateEnd);
            if (dtCheckDefence != null && dtCheckDefence.Rows.Count > 0)
            {
                return "در این بازه تاریخ دفاع درگردش موجود است";
            }
            if (!flagVir)
            {
                DataTable dtCheckSpecialDate = _requestHandler.CheckExistSpecialDate(dateBegin, dateEnd);

                if (dtCheckSpecialDate != null && dtCheckSpecialDate.Rows.Count > 0)
                {
                    return "چنین بازه تاریخی  موجود است.";
                }
               
            }
            else
            { 
                    DataTable dtCheckSpecialDate = _requestHandler.CheckExistSpecialDate(dateBegin, dateEnd);

                if (dtCheckSpecialDate != null && dtCheckSpecialDate.Rows.Count > 1)
                {
                    return "چنین بازه تاریخی  موجود است.";
                }
            }
            return "ok";
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtStartDate.Text == "" || txtEndDate.Text == "")
                return;

            string msg = Validation(txtStartDate.Text, txtEndDate.Text,false);
            if (msg.Contains("ok"))
            {
                var userId = Session[sessionNames.userID_Karbar].ToString();
                _requestHandler.EnterSpecialDate(txtStartDate.Text.Trim(), txtEndDate.Text, txtDsc.Text.Trim()
                                                , chkForEmployee.Checked, chkForStudent.Checked);
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
             , 11, 242
             , "ثبت بستن تاریخ  برگزاری دفاع");
            }
            else
            {
                RadWindowManager1.RadAlert(msg, 500, 100, "خطا", "");
                return;
            }
            rfrshDisplaySpecialDescription();
            grdSpecialDescription.Rebind();
            txtStartDate.Text = "";
            txtEndDate.Text = "";
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
                _requestHandler.DeleteSpecialDescription(int.Parse(lblid.Text));
                var userId = Session[sessionNames.userID_Karbar].ToString();
                CommonBusiness commonBusiness = new CommonBusiness();
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
       , 11, 243
       , "حذف  بستن تاریخ  برگزاری دفاع");

                rfrshDisplaySpecialDescription();
                grdSpecialDescription.Rebind();
            }

        }

        protected void grdSpecialDescription_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rfrshDisplaySpecialDescription();
        }
        public void rfrshDisplaySpecialDescription()
        {
            DataTable dt = _requestHandler.GetSpecialDescription();

            if (dt != null && dt.Rows.Count > 0)
                grdSpecialDescription.DataSource = dt;
            else
                grdSpecialDescription.DataSource = string.Empty;
            GridFilterMenu menu = grdSpecialDescription.FilterMenu;
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

        protected void BtnVirChange_Click(object sender, EventArgs e)
        {
            if (txtVirDate1.Text == "" || txtVirDate2.Text == "" || LabelIdVir.Value == "") return;
            string msg = Validation(txtVirDate1.Text, txtVirDate2.Text,true);
            if (msg.Contains("ok"))
            {
                var userId = Session[sessionNames.userID_Karbar].ToString();
                _requestHandler.UpdateSpecialDescription(int.Parse(LabelIdVir.Value), txtVirDate1.Text, txtVirDate2.Text, txtVirDsc.Text
                    , chkVirForEmployee.Checked, chkVirForStudent.Checked);
                CommonBusiness commonBusiness = new CommonBusiness();
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
       , 11, 244
       , "ویرایش  بستن تاریخ  برگزاری دفاع");
                txtVirDsc.Text = "";
                txtVirDate1.Text = "";
                txtVirDate2.Text = "";
                chkVirForEmployee.Checked = false;
                chkVirForStudent.Checked = false;
            }
            else
            {
                RadWindowManager1.RadAlert(msg, 500, 100, "خطا", "");
                return;
            }
            rfrshDisplaySpecialDescription();
            grdSpecialDescription.Rebind();

        }
    }
}
