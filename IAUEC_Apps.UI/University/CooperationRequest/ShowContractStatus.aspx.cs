using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class ShowContractStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session[sessionNames.roleID].ToString()) == (int)DTO.RoleEnums.کارشناس_مالی ||
                       Convert.ToInt32(Session[sessionNames.roleID].ToString()) == (int)DTO.RoleEnums.مدیر_مالی)
                {
                    ddlContractType.SelectedValue = "2";
                    ddlContractType.Enabled = false;

                }
                setDDlTermSource(ddlContractType.SelectedValue == "1");
                setGridDatasource();
                grdContract.DataBind();
                PersiaFiltering();
            }

        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            setGridDatasource();
            grdContract.DataBind();
        }

        private void setGridDatasource()
        {
            if (ddlTerm.SelectedIndex >= 0)
            {
                DataTable dt = new DataTable();
                Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
                if (Convert.ToInt32(Session[sessionNames.roleID].ToString()) == (int)DTO.RoleEnums.کارشناس_مالی ||
                       Convert.ToInt32(Session[sessionNames.roleID].ToString()) == (int)DTO.RoleEnums.مدیر_مالی)
                    dt = bsn.getContractByStatus(3, ddlTerm.SelectedValue);
                else
                    dt = bsn.getAllContracts_Status(ddlTerm.SelectedValue);
                grdContract.DataSource = null;
                if (dt.Rows.Count > 0)
                {
                    grdContract.DataSource = dt;
                }
            }
        }

        private void setTerm()
        {
            ddlTerm.Items.Clear();
            Business.Common.CommonBusiness bsn = new Business.Common.CommonBusiness();
            DataTable dtTermJary = bsn.SelectAllTerm();
            DataRow[] drTerm = dtTermJary.Select("tterm>='96-97-1'");
            foreach (DataRow dr in drTerm)
            {
                if (!dr["tterm"].ToString().EndsWith("3"))
                {
                    ListItem l = new ListItem();
                    l.Text = dr["tterm"].ToString();
                    l.Value = dr["tterm"].ToString();
                    ddlTerm.Items.Add(l);
                }
            }
        }

        protected void grdContract_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            setGridDatasource();
        }

        protected void PersiaFiltering()
        {
            Telerik.Web.UI.GridFilterMenu menu = grdContract.FilterMenu;
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
                foreach (Telerik.Web.UI.RadMenuItem item in menu.Items)
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            grdContract.ExportSettings.Excel.Format = (Telerik.Web.UI.GridExcelExportFormat)Enum.Parse(typeof(Telerik.Web.UI.GridExcelExportFormat), "ExcelML");
            grdContract.ExportSettings.IgnorePaging = true;
            grdContract.ExportSettings.ExportOnlyData = true;
            grdContract.ExportSettings.OpenInNewWindow = true;
            grdContract.ExportSettings.UseItemStyles = true;
            grdContract.ExportSettings.FileName = "وضعیت قرارداد اساتید" + " ترم " + ddlTerm.SelectedItem.Value;
            grdContract.MasterTableView.ExportToExcel();
        }

        protected void ddlContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDDlTermSource(ddlContractType.SelectedItem.Value == "1");
            setGridDatasource();
            grdContract.DataBind();
        }
        private void setDDlTermSource(bool isTerm)
        {
            Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
            ddlTerm.Items.Clear();
            switch (isTerm)
            {
                case true:

                    setTerm();

                    break;
                case false:
                    var dt = bsn.getYearToSigncontract_HOD();
                    ddlTerm.DataSource = dt;
                    ddlTerm.DataTextField = "year";
                    ddlTerm.DataValueField = "year";
                    ddlTerm.DataBind();
                    break;
            }


        }


        protected void grdContract_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            int teacherCode = 0;
            try
            {
                teacherCode = Convert.ToInt32(e.CommandArgument.ToString());
            }
            catch { }
            if (teacherCode < 1)
                return;
            switch (e.CommandName)
            {
                case "ShowContract":
                    Session["term"] = ddlTerm.SelectedItem.Value;
                    string TC = ddlContractType.SelectedItem.Value == "1" ? DTO.contract.educationContract : DTO.contract.HeadOfDepartment;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openWin('sc=" + e.Item.Cells[3].Text + "&hc=" + teacherCode + "&uc=" + Session[sessionNames.roleID].ToString() + "&vs=v" + "&TC=" + TC + "');", true);
                    break;
            }
        }

        protected void grdContract_DataBound(object sender, EventArgs e)
        {
            var col8 = grdContract.Columns[8];
            var col4 = grdContract.Columns[4];
            var col5 = grdContract.Columns[5];
            switch (ddlContractType.SelectedItem.Value)
            {
                case "1":
                    col8.Visible = false;
                    col5.Visible = false;
                    col4.Visible = true;
                    break;
                case "2":

                    col8.Visible = false;
                    col5.Visible = true;
                    col4.Visible = false;
                    switch (Convert.ToInt32(Session[sessionNames.roleID].ToString()))
                    {
                        case (int)DTO.RoleEnums.کارشناس_مالی:
                        case (int)DTO.RoleEnums.مدیر_مالی:
                        case (int)DTO.RoleEnums.مدیر_ارشد:
                            col8.Visible = true;



                            break;
                    }
                    break;
            }
        }
    }
}