using IAUEC_Apps.Business.Common;
using IAUEC_Apps.UI.CommonUI;
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
    public partial class EducationAssignmentResource : System.Web.UI.Page
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

                drpCollegeId1();
                drpResource1();
                drpVirCollegeId1();
                drpVirResource1();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (txtDate1.Text == "" || txtDate2.Text == "" )
                return;
            bool flagVir = false;
            
               string message = Validation(txtDate1.Text, txtDate2.Text, int.Parse(drpResource.SelectedValue)
                                       , drpCollegeId.SelectedValue,int.Parse(drpIsShared.SelectedValue), flagVir);
            if (message.Contains("ok"))
            {
                _requestHandler.EnterResourece_College_junc(txtDate1.Text, txtDate2.Text, int.Parse(drpCollegeId.SelectedValue),
                   int.Parse(drpResource.SelectedValue), int.Parse(drpIsShared.SelectedValue));
                var userId = Session[sessionNames.userID_Karbar].ToString();
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                , 11, 230
                , "ثبت تخصیص منابع به دانشکده برای دفاع", int.Parse(drpCollegeId.SelectedValue));
            }
            else
            {
                RadWindowManager1.RadAlert(message, 500, 100, "خطا", "");
                return;
            }

            rfrshDisplayAssignmentResource();
            grdAssignmentResource.Rebind();
            txtDate1.Text = "";
            txtDate2.Text = "";

        }
        public string Validation(string dateBegin, string dateEnd, int ResourceId,string collegeId,int isShared, bool flagVir)
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
            DataTable dtCheckDefence = _requestHandler.GetRequestDateTime(dateBegin, dateEnd, ResourceId,int.Parse(collegeId));
          
            if (dtCheckDefence != null && dtCheckDefence.Rows.Count > 0)
            {
                return "در این بازه تاریخ دفاع درگردش موجود است";
            }
         DataTable dtCheckResoureceAssign = _requestHandler.GetResourece_College_junc(dateBegin, dateEnd,-1, ResourceId,-1);

                if (dtCheckResoureceAssign != null && dtCheckResoureceAssign.Rows.Count > 0)
                {
                    for(int i=0;i< dtCheckResoureceAssign.Rows.Count;++i)
                    { 
                        if(dtCheckResoureceAssign.Rows[i]["collegeId"].ToString()== collegeId&&!flagVir)
                             return "چنین بازه تاریخی برای این منبع در دانشکده موجود است";
                        if((bool.Parse(dtCheckResoureceAssign.Rows[i]["isShared"].ToString()) == false||isShared==0)
                        && dtCheckResoureceAssign.Rows[i]["collegeId"].ToString() != collegeId)
                        {
                            return "این منبع قابل اختصاص به این دانشکده  نیست";
                        }

                    }
                }
               
            
            return "ok";
        }

        public void drpCollegeId1()
        {
            CommonBusiness commonBusiness = new CommonBusiness();
            DataTable dt = commonBusiness.SelectAllDaneshkade();

            dt.DefaultView.Sort = "id asc";
            drpCollegeId.DataValueField = "id";
            drpCollegeId.DataTextField = "namedanesh";
            drpCollegeId.DataSource = dt;
            drpCollegeId.DataBind();
        }
        public void drpVirCollegeId1()
        {
            CommonBusiness commonBusiness = new CommonBusiness();
            DataTable dt = commonBusiness.SelectAllDaneshkade();

            dt.DefaultView.Sort = "id asc";
            drpVirCollegeId.DataValueField = "id";
            drpVirCollegeId.DataTextField = "namedanesh";
            drpVirCollegeId.DataSource = dt;
            drpVirCollegeId.DataBind();
        }

        public void drpResource1()
        {
            const int catId = 2;
            drpResource.DataValueField = "ID";
            drpResource.DataTextField = "name";
            drpResource.DataSource = _requestHandler.GetResource(catId);
            drpResource.DataBind();
        }

        public void drpVirResource1()
        {
            const int catId = 2;
            drpVirResourceId.DataValueField = "ID";
            drpVirResourceId.DataTextField = "name";
            drpVirResourceId.DataSource = _requestHandler.GetResource(catId);
            drpVirResourceId.DataBind();
        }


        protected void BtnVirChange_Click(object sender, EventArgs e)
        {
            if (txtVirDate1.Text == "" || txtVirDate2.Text == "" || LabelIdVir.Value == "" ) return;
            bool flagVir = true;

            string message = Validation(txtVirDate1.Text, txtVirDate2.Text, int.Parse(drpVirResourceId.SelectedValue)
                                    , drpVirCollegeId.SelectedValue, int.Parse(drpVirIsShared.SelectedValue), flagVir);
            if (message.Contains("ok"))
            {
                 _requestHandler.UpdateResourece_College_junc(int.Parse(LabelIdVir.Value), txtVirDate1.Text, txtVirDate2.Text
                   , int.Parse(drpVirResourceId.SelectedValue),int.Parse( drpVirCollegeId.SelectedValue), int.Parse(drpVirIsShared.SelectedValue));
                var userId = Session[sessionNames.userID_Karbar].ToString();
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                , 11, 231
                , " ویرایش تخصیص منابع به دانشکده برای دفاع", int.Parse(drpCollegeId.SelectedValue));
            }
            else
            {
                RadWindowManager1.RadAlert(message, 500, 100, "خطا", "");
                return;
            }
            rfrshDisplayAssignmentResource();
            grdAssignmentResource.Rebind();
            txtVirDate2.Text = "";
            txtVirDate2.Text = "";

        }


        public void rfrshDisplayAssignmentResource()
        {
            DataTable dt = _requestHandler.GetResourece_College_junc();

            if (dt != null && dt.Rows.Count > 0)
                grdAssignmentResource.DataSource = dt;
            else
                grdAssignmentResource.DataSource = string.Empty;
            GridFilterMenu menu = grdAssignmentResource.FilterMenu;
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

  

        protected void grdAssignmentResource_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rfrshDisplayAssignmentResource();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Label lblid = (Label)item.FindControl("lblid");
            Label lblResourceId = (Label)item.FindControl("lblResourceId");
            Label lblCollegeId = (Label)item.FindControl("lblCollegeId");
            Label lblendDate = (Label)item.FindControl("lblendDate");
            Label lblstartDate = (Label)item.FindControl("lblstartDate");
            DataTable dtCheckDefence = _requestHandler.GetRequestDateTime(lblstartDate.Text, lblendDate.Text,
                int.Parse(lblResourceId.Text), int.Parse(lblCollegeId.Text));

            if (dtCheckDefence != null && dtCheckDefence.Rows.Count > 0)
            {
                RadWindowManager1.RadAlert("در این بازه تاریخ دفاع درگردش موجود است", 500, 100, "خطا", "");
                return;
              
            }
            _requestHandler.DeleteResourece_College_junc(int.Parse(lblid.Text));
            var userId = Session[sessionNames.userID_Karbar].ToString();
            commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
            , 11, 232
            , "حذف تخصیص منابع به دانشکده", int.Parse(drpCollegeId.SelectedValue));
            rfrshDisplayAssignmentResource();
            grdAssignmentResource.Rebind();

        }
    }
}
