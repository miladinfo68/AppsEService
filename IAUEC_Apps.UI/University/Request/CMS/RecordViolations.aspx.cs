using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.EmailClasses;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class RecordViolations : System.Web.UI.Page
    {
        private static readonly RecordViolationBusiness _recordViolationBusiness = new RecordViolationBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void rdGridExcel_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rdGridExcel.DataSource = getRecordViolationList();
            GridFilterMenu menu = rdGridExcel.FilterMenu;
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
                {
                    if (item.Text == "NoFilter")
                        item.Text = "حذف فیلتر";
                    if (item.Text == "Contains")
                        item.Text = "شامل";
                    if (item.Text == "EqualTo")
                        item.Text = "مساوی با";

                }
            }
        }
        private DataTable getRecordViolationList()
        {
            return _recordViolationBusiness.GetRecordViolationList();
        }
        [WebMethod]
        public static bool InsertViolation(RecordViolationDTO recordViolationDTO)
        {
            return _recordViolationBusiness.InsertViolation(recordViolationDTO);
        }
        [WebMethod]
        public static RecordViolationStudentInfo GetStudentInfo(RecordViolationDTO recordViolationDTO)
        {
            return _recordViolationBusiness.GetStudentInfo(recordViolationDTO);
        }

        private bool SubmitDeleteDate(int id)
        {
            return _recordViolationBusiness.SubmitDeleteDate(id);
        }
        private bool UnSubmitDeleteDate(int id)
        {
            return _recordViolationBusiness.UnSubmitDeleteDate(id);
        }
        protected void rdGridExcel_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "RecordViolation":
                    if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
                    {
                        var hdnItemId = e.Item.FindControl("hdnItemId") as HiddenField;
                        var id = hdnItemId.Value;
                        UnSubmitDeleteDate(Convert.ToInt32(id));
                        var btnRecordViolation = e.Item.FindControl("btnRecordViolation") as Button;
                        btnRecordViolation.Text = "حذف تخلف";
                        btnRecordViolation.Style.Add("color", "Green");
                        //btnRecordViolation.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        var hdnItemId = e.Item.FindControl("hdnItemId") as HiddenField;
                        var id = hdnItemId.Value;
                        SubmitDeleteDate(Convert.ToInt32(id));
                        var btnRecordViolation = e.Item.FindControl("btnRecordViolation") as Button;
                       
                        btnRecordViolation.Text = "بازگردانی ثبت تخلف";
                        btnRecordViolation.Style.Add("color", "Red");
                        //btnRecordViolation.BackColor = System.Drawing.Color.Green;
                       
                    }
                    rdGridExcel.Rebind();
                    break;
            }
        }

        protected void rdGridExcel_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                var hdnItemId = e.Item.FindControl("hdnItemId") as HiddenField;
                var id = hdnItemId.Value;
                if (id == "11")
                {

                }
                var btnRecordViolation = item.FindControl("btnRecordViolation") as Button;
                if (btnRecordViolation != null)
                {
                    if (string.IsNullOrEmpty(btnRecordViolation.CommandArgument))
                    {
                        btnRecordViolation.Text = "حذف تخلف";
                        btnRecordViolation.Style.Add("color", "Green");
                        //btnRecordViolation.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        btnRecordViolation.Text = "بازگردانی ثبت تخلف";
                        btnRecordViolation.Style.Add("color", "Red");
                        //btnRecordViolation.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }
}