using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class AcceptContracts : System.Web.UI.Page
    {
        Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
        const string userType = "userType";

        private void manageAccessControl()
        {
            if (Request.QueryString["id"] != null)
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
                AccessControl.MenuId = menuId;
                Session[sessionNames.menuID] = menuId;
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
            {
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
                return;
            }
            manageAccessControl();
            if (!IsPostBack)
            {
                ViewState[userType] = 0;

                Business.Common.LoginBusiness lngB = new Business.Common.LoginBusiness();
                DataTable userdt = lngB.Get_UserRoles(Session[sessionNames.userID_Karbar].ToString());
                DataRow[] drRoles = userdt.Select("roleId in(" + (int)DTO.RoleEnums.مدیر_ارشد + "," + (int)DTO.RoleEnums.مسئول_حق_التدریس + "," + (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی + "," + (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی + "," + (int)DTO.RoleEnums.سرپرست_واحد + ")");
                if (drRoles.Length > 0)
                {
                    ViewState[userType] = drRoles[0]["roleId"].ToString();
                }
                //rol = Convert.ToInt32(userdt.Rows[0][1]);

                fillCartable();
                setDDlTermSource(ddlContractType.SelectedItem.Value == "1");
                //RadWindowManager1.RadAlert("Test", 300, 100, "پیام سیستم", null);
            }
        }

        private void fillCartable()
        {
            switch (Convert.ToInt32(ViewState[userType]))
            {
                case (int)DTO.RoleEnums.مدیر_ارشد:
                    List<int> l = new List<int>(new int[] { (int)DTO.RoleEnums.مسئول_حق_التدریس, (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی, (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی, (int)DTO.RoleEnums.سرپرست_واحد });
                    foreach (int i in l)
                    {
                        DataTable dt = bsn.getSignature(0, i);
                        ListItem li;
                        if (dt.Rows.Count == 1)
                        {
                            li = new ListItem(dt.Rows[0]["userName"].ToString(), i.ToString());
                            ddlUserType.Items.Add(li);
                        }
                    }
                    break;
                case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی:
                    List<int> l2 = new List<int>(new int[] { (int)DTO.RoleEnums.مسئول_حق_التدریس, (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی, (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی });
                    foreach (int i in l2)
                    {
                        DataTable dt = bsn.getSignature(0, i);
                        ListItem li;
                        if (dt.Rows.Count == 1)
                        {
                            li = new ListItem(dt.Rows[0]["userName"].ToString(), i.ToString());
                            ddlUserType.Items.Add(li);
                        }
                    }
                    ddlUserType.SelectedValue = ((int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی).ToString();
                    break;
                case (int)DTO.RoleEnums.مسئول_حق_التدریس:
                    List<int> l3 = new List<int>(new int[] { (int)DTO.RoleEnums.مسئول_حق_التدریس });
                    foreach (int i in l3)
                    {
                        DataTable dt = bsn.getSignature(0, i);
                        ListItem li;
                        if (dt.Rows.Count == 1)
                        {
                            li = new ListItem(dt.Rows[0]["userName"].ToString(), i.ToString());
                            ddlUserType.Items.Add(li);
                        }
                    }
                    ddlContractType.SelectedValue = "1";
                    ddlContractType.Enabled = false;
                    ddlUserType.SelectedValue = ((int)DTO.RoleEnums.مسئول_حق_التدریس).ToString();
                    //ListItem liType=new ListItem("قراردادهای رد شده","3");
                    //ddlContractType.Items.Add(liType);
                    break;

                case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی:
                    List<int> l12 = new List<int>(new int[] { (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی });
                    foreach (int i in l12)
                    {
                        DataTable dt = bsn.getSignature(0, i);
                        ListItem li;
                        if (dt.Rows.Count == 1)
                        {
                            li = new ListItem(dt.Rows[0]["userName"].ToString(), i.ToString());
                            ddlUserType.Items.Add(li);
                        }
                    }
                    ddlContractType.SelectedValue = "2";
                    ddlContractType.Enabled = false;
                    ddlUserType.SelectedValue = ((int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی).ToString();
                    //ListItem liType=new ListItem("قراردادهای رد شده","3");
                    //ddlContractType.Items.Add(liType);
                    break;
                case (int)DTO.RoleEnums.سرپرست_واحد:
                    List<int> l4 = new List<int>(new int[] { (int)DTO.RoleEnums.سرپرست_واحد });
                    foreach (int i in l4)
                    {
                        DataTable dt = bsn.getSignature(0, i);
                        ListItem li;
                        if (dt.Rows.Count == 1)
                        {
                            li = new ListItem(dt.Rows[0]["userName"].ToString(), i.ToString());
                            ddlUserType.Items.Add(li);
                        }
                    }
                    ddlUserType.SelectedValue = ((int)DTO.RoleEnums.سرپرست_واحد).ToString();
                    break;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["term"] = ddlTerm.SelectedItem.Text;
            if (ddlContractStatus.SelectedItem.Value != "-1")
            {
                hdnStatus.Value = ddlContractStatus.SelectedItem.Value;
                setGrdDatasource();
                grdContracts.DataBind();
            }
        }

        protected void grdContracts_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
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
                    string TC = ddlContractType.SelectedItem.Value == "1" ? DTO.contract.educationContract : DTO.contract.HeadOfDepartment;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openWin('sc=" + e.Item.Cells[3].Text + "&hc=" + teacherCode + "&uc=" + ViewState[userType].ToString() + "&vs=" + (ddlContractStatus.SelectedItem.Value.ToString() == "2" ? "s" : "v") + "&TC=" + TC + "');", true);
                    break;
                case "History":
                    Business.Common.CommonBusiness cmb = new Business.Common.CommonBusiness();
                    lst_history.DataSource = cmb.getContractHistory(teacherCode);
                    lst_history.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    break;
                    //case "AcceptContract":
                    //    Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
                    //    if (bsn.updateTeacherContractStatus(teacherCode, Convert.ToInt32(ViewState[userType])))
                    //    {
                    //        setLog(teacherCode, "");
                    //    }

                    //    break;
            }
            setGrdDatasource();
            grdContracts.DataBind();
        }

        private void showMessage(string msg)
        {
            RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", null);
        }

        protected void grdContracts_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            setGrdDatasource();
        }

        private void setGrdDatasource()
        {
            int status = 0;
            DataTable dtGetContract = new DataTable();
            DataTable dtGetContractTemp = new DataTable();
            string term = ddlTerm.SelectedItem.Text;
            if (!string.IsNullOrEmpty(hdnStatus.Value))
            {
                switch (hdnStatus.Value)
                {
                    case "1"://تایید شده
                        switch (Convert.ToInt32(ViewState[userType]))
                        {
                            case (int)DTO.RoleEnums.مسئول_حق_التدریس:
                            case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی:
                                status = 1;
                                dtGetContract = bsn.getContractByStatus(status, term);
                                status = 2;
                                dtGetContractTemp = bsn.getContractByStatus(status, term);
                                foreach (DataRow dr in dtGetContractTemp.Rows)
                                {
                                    DataRow d = dtGetContract.NewRow();
                                    for (int i = 0; i < dtGetContractTemp.Columns.Count; i++)
                                    {
                                        d[i] = dr[i];
                                    }
                                    dtGetContract.Rows.Add(d);
                                }
                                status = 3;
                                dtGetContractTemp = bsn.getContractByStatus(status, term);
                                foreach (DataRow dr in dtGetContractTemp.Rows)
                                {
                                    DataRow d = dtGetContract.NewRow();
                                    for (int i = 0; i < dtGetContractTemp.Columns.Count; i++)
                                    {
                                        d[i] = dr[i];
                                    }
                                    dtGetContract.Rows.Add(d);
                                }
                                break;
                            case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی:
                                status = 2;
                                dtGetContract = bsn.getContractByStatus(status, term);
                                status = 3;
                                dtGetContractTemp = bsn.getContractByStatus(status, term);
                                foreach (DataRow dr in dtGetContractTemp.Rows)
                                {
                                    DataRow d = dtGetContract.NewRow();
                                    for (int i = 0; i < dtGetContractTemp.Columns.Count; i++)
                                    {
                                        d[i] = dr[i];
                                    }
                                    dtGetContract.Rows.Add(d);
                                }
                                break;
                            case (int)DTO.RoleEnums.سرپرست_واحد:
                                status = 3;
                                dtGetContract = bsn.getContractByStatus(status, term);
                                break;
                        }
                        break;
                    case "2"://تایید نشده
                        switch (Convert.ToInt32(ViewState[userType]))
                        {
                            case (int)DTO.RoleEnums.مسئول_حق_التدریس:
                            case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی:
                                status = 0;
                                break;
                            case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی:
                                status = 1;
                                break;
                            case (int)DTO.RoleEnums.سرپرست_واحد:
                                status = 2;
                                break;
                        }
                        dtGetContract = bsn.getContractByStatus(status, term);
                        break;
                    case "3"://رد شده
                        switch (Convert.ToInt32(ViewState[userType]))
                        {
                            case (int)DTO.RoleEnums.مسئول_حق_التدریس:
                            case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی:
                                status = 4;
                                break;
                            default:
                                status = 5;//الکی
                                break;
                        }
                        dtGetContract = bsn.getContractByStatus(status, term);
                        break;

                }
                grdContracts.DataSource = dtGetContract;
                //grdContracts.DataBind();
                PersiaFiltering();
            }
        }

        protected void PersiaFiltering()
        {
            GridFilterMenu menu = grdContracts.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" ||
                        menu.Items[im].Text == "EqualTo")
                        im++;
                    else
                        menu.Items.RemoveAt(im);
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


        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState[userType] = ddlUserType.SelectedItem.Value;
            ddlContractType.Enabled = true;
            if (ddlUserType.SelectedItem.Value == "87")
            {
                ddlContractType.SelectedValue = "2";
                ddlContractType.Enabled = false;
                setDDlTermSource(ddlContractType.SelectedItem.Value == "1");
            }
            if (ddlUserType.SelectedItem.Value == "11")
            {
                ddlContractType.SelectedValue = "1";
                ddlContractType.Enabled = false;
                setDDlTermSource(ddlContractType.SelectedItem.Value == "1");
            }
        }

        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            setGrdDatasource();
            grdContracts.DataBind();
        }
        /*--------------------------------*/

        private void setDDlTermSource(bool isTerm)
        {
            Business.Common.CommonBusiness cbsn = new Business.Common.CommonBusiness();
            ddlTerm.Items.Clear();
            switch (isTerm)
            {
                case true:

                    DataTable dtTermJary = cbsn.SelectAllTerm();
                    DataRow[] drTerm = dtTermJary.Select("tterm>='96-97-1'");
                    foreach (DataRow dr in drTerm)
                    {
                        if (!dr["tterm"].ToString().EndsWith("3")|| (dr["tterm"].ToString().EndsWith("3") && string.Compare(dr["tterm"].ToString(),"98-99-3")>=0))
                        {
                            ListItem l = new ListItem();
                            l.Text = dr["tterm"].ToString();
                            l.Value = dr["tterm"].ToString();
                            ddlTerm.Items.Add(l);
                        }
                    }

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

        protected void ddlContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDDlTermSource(ddlContractType.SelectedItem.Value == "1");

        }
    }
}