using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.ResourceControl;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.ResourceControlClasses;
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
    public partial class EducationAllowStudentSelectUnit : System.Web.UI.Page
    {
        RequestStudentCartBusiness cartBusiness = new RequestStudentCartBusiness();
        AccessStudentOperationHandler operationHandler = new AccessStudentOperationHandler();
        AccessStudentOperationModel accessModel = new AccessStudentOperationModel();

        RequestHandler _requestHandler = new RequestHandler();
        private CommonBusiness commonBusiness = new CommonBusiness();
        string pTitle = "دانشجویان مجاز توسط مالی ";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pt.Text = pTitle;
                btnSave.Enabled = false;
                RfrhgrdDisplayStundetFinancial();
            }
        }
        public void RfrhgrdDisplayStundetFinancial()
        {

            List<AccessStudentOperationModel> modelOut = operationHandler.GetAllForFinancial(accessModel);


            if (modelOut != null && modelOut.Count > 0)
            {

                grdDisplayStundetDefenceOnline.DataSource = modelOut;
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


        protected void grdDisplayStundetDefenceOnline_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RfrhgrdDisplayStundetFinancial();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;


            if (txtStCode.Text.Trim() == "") return;

            DataTable dt = new DataTable();
            accessModel.StudentCode = txtStCode.Text.Trim();
            accessModel.FlagAllowFinancial = true;
            accessModel.FlagAllowSelectUnit = false;
            AccessStudentOperationModel modelOut = operationHandler.GetAStudent(accessModel);
            if (modelOut != null && modelOut.id > 0)
            {
                RadWindowManager1.RadAlert("به این دانشجو اجازه دفاع آنلاین در ترم جاری داده شده است.", 500, 100, "خطا", "");
            }
            else
            {
                //save 
                long accept = operationHandler.EnterAStudent(accessModel);
                if (accept > 0)
                {
                    var userId = Session[sessionNames.userID_Karbar].ToString();

                    commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                        , 11, 223
                        , "اجازه به دانشجو برای برگزاری دفاع به صورت آنلاین", int.Parse(txtStCode.Text));

                }
                txtStCode.Text = "";
                LblnameStudent.Text = "";
            }

            RfrhgrdDisplayStundetFinancial();
            grdDisplayStundetDefenceOnline.Rebind();

        }

        protected void txtStCode_TextChanged(object sender, EventArgs e)
        {
            if (txtStCode.Text.Trim() == "") return;
            DataTable dtStudentInformation = cartBusiness.GetStudentsInfo(txtStCode.Text);
            if (dtStudentInformation != null && dtStudentInformation.Rows.Count > 0)
            {
                LblnameStudent.Text = dtStudentInformation.Rows[0]["FullName"].ToString();
                btnSave.Enabled = true;

            }
            else
            {
                btnSave.Enabled = false;
                LblnameStudent.Text = "";
            }


        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridDataItem item = (GridDataItem)btn.NamingContainer;
            Label lblstudentcode = (Label)item.FindControl("lblstudentcode");
            DataTable dt = _requestHandler.CheckRequestDefenceStudent(lblstudentcode.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                RadWindowManager1.RadAlert(" این دانشجو دفاع در گردش دارد.", 500, 100, "خطا", "");
            }
            else
            {
                //delete

                var userId = Session[sessionNames.userID_Karbar].ToString();
                commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
               , 11, 224
               , "حذف اجازه به دانشجو برای برگزاری دفاع به صورت آنلاین", int.Parse(lblstudentcode.Text));
            }
            RfrhgrdDisplayStundetFinancial();
            grdDisplayStundetDefenceOnline.Rebind();
        }
    }
}
