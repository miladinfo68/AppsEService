using System;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using ResourceControl.BLL;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class EducationAvoidDate : System.Web.UI.Page
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
           
            if (txtDate1.Text == "" || txtDate2.Text == "")
                return;
            bool flagVir = false;
            string message = Validation(txtDate1.Text, txtDate2.Text, flagVir);
            if (message.Contains("ok"))
            {
                _requestHandler.EnterAvoidDate(txtDate1.Text, txtDate2.Text, txtDsc.Text, chkForStudent.Checked, chkForEmployee.Checked);
                txtDsc.Text = "";
                txtDate1.Text = "";
                txtDate2.Text = "";
                chkForEmployee.Checked = false;
                chkForStudent.Checked = false;
                var userId = Session[sessionNames.userID_Karbar].ToString();
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                , 11, 233
                , "ثبت منع تاریخ درخواست برای دفاع");
            }
            else
            {
                RadWindowManager1.RadAlert(message, 500, 100, "خطا", "");
                return;
            }
            rfrshDisplayAvoidDate();
            grdAvoidDate.Rebind();


        }
        public string Validation(string dateBegin,string dateEnd,bool flagvir)
        {
            if (!_requestHandler. CheckReqDate(dateBegin))
            {
               return "تاریخ آغازین باید بعد از تاریخ امروز باشد.";
            }
            else if(!_requestHandler.CheckReqDate(dateEnd))
             { return "تاریخ پایانی باید بعد از تاریخ امروز باشد."; 
            }
            if(string.Compare(dateBegin, dateEnd) >0)
            {
                return "تاریخ انتهایی باید بعد از تاریخ ابتدایی باشد";
            }
            DataTable dtCheckDefence = _requestHandler.CheckDefenceInformationByRequestDate(dateBegin, dateEnd);
            if (dtCheckDefence != null && dtCheckDefence.Rows.Count > 0)
            {
                return "در این بازه تاریخ دفاع درگردش موجود است";
            }
            if(flagvir==false)
            { 
                DataTable dtCheckAvoidDate = _requestHandler.GetAvoidDate(dateBegin, dateEnd);
                if (dtCheckAvoidDate != null && dtCheckAvoidDate.Rows.Count > 0)
                {
                    return "چنین تاریخی موجود است";
                }
            }
            else
            {
                DataTable dtCheckAvoidDate = _requestHandler.GetAvoidDate(dateBegin, dateEnd);
                if (dtCheckAvoidDate != null && dtCheckAvoidDate.Rows.Count > 1)
                {
                    return "چنین تاریخی موجود است";
                }
            }
            return "ok";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Label lblid = (Label)item.FindControl("lblid");
     
            _requestHandler.DeleteAvoidDate(int.Parse(lblid.Text));
            var userId = Session[sessionNames.userID_Karbar].ToString();
            commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
              , 11, 235
              , " حذف  منع تاریخ درخواست برای دفاع");

            rfrshDisplayAvoidDate();
            grdAvoidDate.Rebind();

        }



        protected void BtnVirChange_Click(object sender, EventArgs e)
        {
            if (txtVirDate1.Text == "" || txtVirDate2.Text == ""|| LabelIdVir.Value=="") return;
            bool flagVir = true;
            string msg = Validation(txtVirDate1.Text,txtVirDate2.Text, flagVir);
            if(msg.Contains("ok"))
            {
                _requestHandler.UpdateAvoidDate(int.Parse(LabelIdVir.Value), txtVirDate1.Text, txtVirDate2.Text, txtVirDsc.Text
                    ,chkVirForEmployee.Checked,chkVirForStudent.Checked);
                var userId = Session[sessionNames.userID_Karbar].ToString();
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                  , 11, 234
                  , " ویرایش منع تاریخ درخواست برای دفاع");

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
            rfrshDisplayAvoidDate();
            grdAvoidDate.Rebind();
            
               

        }

        protected void grdAvoidDate_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rfrshDisplayAvoidDate();
        }
        public void rfrshDisplayAvoidDate()
        {
            DataTable dt = _requestHandler.GetAvoidDate();

            if (dt != null && dt.Rows.Count > 0)
                grdAvoidDate.DataSource = dt;
            else
                grdAvoidDate.DataSource = string.Empty;
            GridFilterMenu menu = grdAvoidDate.FilterMenu;
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