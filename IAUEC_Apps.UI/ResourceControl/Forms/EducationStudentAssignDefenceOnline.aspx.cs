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
    public partial class EducationStudentAssignDefenceOnline : System.Web.UI.Page
    {
        private RequestHandler _requestHandler = new RequestHandler();
        private CommonBusiness commonBusiness = new CommonBusiness();
        string pTitle = "انتخاب دانشجو برای برگزاری دفاع آنلاین";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pt.Text = pTitle;    
                drpTypeDefence.Enabled = false;
                btnSave.Enabled = false;
                drpTypeDefence1();
                RfrhgrdDisplayStundetDefenceOnline();
            }
           
        }
        public void RfrhgrdDisplayStundetDefenceOnline()
        {
  
            DataTable dt = new DataTable();
            dt = _requestHandler.GetDefenceMeetingsOnline();

           if (dt != null && dt.Rows.Count > 0)
           {

                grdDisplayStundetDefenceOnline.DataSource = dt;
           }
            else
                grdDisplayStundetDefenceOnline.DataSource = string.Empty;

            GridFilterMenu menu = grdDisplayStundetDefenceOnline.FilterMenu;
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
        public void drpTypeDefence1()
        {

            drpTypeDefence.DataValueField = "TypeId";
            drpTypeDefence.DataTextField = "Dsc";
            drpTypeDefence.DataSource = _requestHandler.GetTypeDefenceMeetingOnline();
            drpTypeDefence.DataBind();
        }


        protected void grdDisplayStundetDefenceOnline_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RfrhgrdDisplayStundetDefenceOnline();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            drpTypeDefence.Enabled = false;
            
            if (txtStCode.Text.Trim() == "" || drpTypeDefence.SelectedIndex == -1||LblnameStudent.Text.Trim()=="") return;

            DataTable dt = new DataTable();
            dt = _requestHandler.GetDefenceMeetingsOnline(txtStCode.Text.Trim());
            if(dt!=null&&dt.Rows.Count>0)
            {
                RadWindowManager1.RadAlert("به این دانشجو اجازه دفاع آنلاین در ترم جاری داده شده است.", 500, 100, "خطا", "");
            }
            else
            {
                //save 
                _requestHandler.Enter_StudentsAllowDefenceMeetingOnline(txtStCode.Text.Trim()
                                                    , int.Parse(drpTypeDefence.SelectedValue));
                 var userId= Session[sessionNames.userID_Karbar].ToString();

                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                    , 11, 223
                    , "اجازه به دانشجو برای برگزاری دفاع به صورت آنلاین", int.Parse(txtStCode.Text));

                txtStCode.Text = "";
                LblnameStudent.Text = "";
            }
            drpTypeDefence.SelectedIndex = 0;
            RfrhgrdDisplayStundetDefenceOnline();
            grdDisplayStundetDefenceOnline.Rebind();

        }

        protected void txtStCode_TextChanged(object sender, EventArgs e)
        {
            if (txtStCode.Text.Trim() == "") return;
            DataTable dtStudentInformation = _requestHandler.GetStudentInformationByStCode(txtStCode.Text);
            if (dtStudentInformation != null && dtStudentInformation.Rows.Count > 0)
            {
                LblnameStudent.Text = dtStudentInformation.Rows[0]["FullName"].ToString();
                btnSave.Enabled = true;
                drpTypeDefence.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                drpTypeDefence.Enabled = false;
                drpTypeDefence.SelectedIndex = 0;
                LblnameStudent.Text = "";
            }


        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Label lblstudentcode = (Label)item.FindControl("lblstudentcode");
            DataTable dt = _requestHandler.CheckRequestDefenceStudent(lblstudentcode.Text);
            if(dt!=null&&dt.Rows.Count>0)
            {
                RadWindowManager1.RadAlert(" این دانشجو دفاع در گردش دارد.", 500, 100, "خطا", "");
            }
            else
            {
                //delete
                _requestHandler.Delete_StudentsAllowDefenceMeetingOnline(lblstudentcode.Text);
                var userId = Session[sessionNames.userID_Karbar].ToString();
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
               , 11, 224
               , "حذف اجازه به دانشجو برای برگزاری دفاع به صورت آنلاین", int.Parse(lblstudentcode.Text));
            }
            RfrhgrdDisplayStundetDefenceOnline();
            grdDisplayStundetDefenceOnline.Rebind();
        }
    }
}