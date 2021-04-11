using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;

using IAUEC_Apps.DTO.University.Request;
using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections.Generic;

namespace IAUEC_Apps.UI.University.Request.CMS
{

    public partial class ChechOutCase : System.Web.UI.Page
    {
        CheckOutRequestBusiness business = new CheckOutRequestBusiness();
        CheckOutRefahBusiness Refahbusiness = new CheckOutRefahBusiness();
        CheckOutMaliBusiness MaliBusiness = new CheckOutMaliBusiness();
        StuPresentBusiness StudentBusiness = new StuPresentBusiness();

        LoginBusiness lngB = new LoginBusiness();
      
        DataTable userdt = new DataTable();
        //int userRole;
        string userID;
        int flag = 0;
        int rol = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userID = Session[sessionNames.userID_Karbar].ToString();
                userdt = lngB.Get_UserRoles(userID);
                rol = Convert.ToInt32(userdt.Rows[0][1]);
                var listOfRole = new List<int>();
                if (userdt.Rows.Count > 1)
                {
                    foreach (DataRow item in userdt.Rows)
                    {
                        listOfRole.Add(Convert.ToInt32(item[1]));
                    }
                    if (listOfRole.Contains(30))
                        rol = 30;
                }

                if (userdt.Rows.Count > 1 || rol == 1 || rol == 21 || rol == 35 || rol == 30 || rol == 50 || rol == 66 || rol == 62 || rol == 51 || rol == 52 || rol == 53)
                {
                    ToMultipleRoleMode(userdt);
                }
                else
                {
                    ToSingleRoleMode(userdt);
                }
                flag = 0;
                ViewState.Add("flag", flag);
                BindData(21, false);
            }

        }
        private void ToSingleRoleMode(DataTable userdt)
        {
            int currentstatus = business.GetStatusOfRole(Convert.ToInt32(userdt.Rows[0][1]));
            ViewState.Add("UserRoleId", userdt.Rows[0][1]);
            //Session.Add("activeRoleStatus", currentstatus);
            BindData(21, false);
        }

        private void ToMultipleRoleMode(DataTable dtu)
        {
            grd_CheckOutList.Visible = true;
            if (rol == 1)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            else if (rol == 32)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10" && li.Value == "11" || li.Value == "13")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            else if (rol == 35 || rol == 30)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10" && li.Value == "21" || li.Value == "22" || li.Value == "23")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            //************************************************************
            else if (rol == 66)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10" && li.Value == "12" || li.Value == "20")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            else if (rol == 51)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10" && li.Value == "12" || li.Value == "20")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            else if (rol == 52)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10" && li.Value == "12" || li.Value == "20")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            else if (rol == 53)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10" && li.Value == "12" || li.Value == "20")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }

            else if (rol == 50)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10" && li.Value == "16" || li.Value == "17")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            else if (rol == 62)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10" && li.Value == "14" || li.Value == "19" || li.Value == "21" || li.Value == "22" || li.Value == "23")
                    {
                        drpUserRoles.Items.Add(li);
                    }
                }
            }
            //************************************************************
            else if (rol == 21)
            {
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                    li.Value = Convert.ToInt32(status).ToString();
                    if (li.Value != "10")
                    {
                        if (li.Value == "14" || li.Value == "19")
                        {
                            drpUserRoles.Items.Add(li);
                        }
                    }
                }
            }
            else
            {
                foreach (DataRow row in userdt.Rows)
                {

                    if (row["RoleId"].ToString() != "36")

                    {
                        CheckOutStatusEnum.CheckOutAllStatusEnum status = (CheckOutStatusEnum.CheckOutAllStatusEnum)business.GetStatusOfRole(Convert.ToInt32(row[1]));
                        ListItem li = new ListItem();
                        li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                        li.Value = Convert.ToInt32(status).ToString();
                        //if (!String.IsNullOrWhiteSpace(li.Text) && li.Value != "22")
                        if (!String.IsNullOrWhiteSpace(li.Text) && li.Value != "23")
                        {
                            drpUserRoles.Items.Add(li);
                        }
                    }
                }
            }
            if (drpUserRoles.Items.Count > 1)
            {
                drpUserRoles.Enabled = true;
                drpUserRoles.Visible = true;
                drpUserRoles.Items.Insert(0, "انتخاب کنید");
            }

            if (drpUserRoles.Items.Count == 1)
            {
                BindData(21, false);
            }

            if (drpUserRoles.SelectedIndex > 0)
            {
                BindData(21, false);
            }
        }

        protected void drpUserRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpUserRoles.SelectedIndex > 0)
            {
                grd_CheckOutList.CurrentPageIndex = 0;
                grd_CheckOutList.Visible = true;
                //Session.Add("activeRoleStatus", drpUserRoles.SelectedValue);
                BindData(21, false);
                switch (Convert.ToInt32(drpUserRoles.SelectedValue))
                {
                    //                    case (int)CheckOutStatusEnum.FareghReqStatus.fareghotahsilan_ok:
                    case (int)CheckOutStatusEnum.FareghReqStatus.vrood_moavenat_ok:

                      
                        entekhabvahed.Visible = false;
                        hozur.Visible = false;

                        break;
                    case (int)CheckOutStatusEnum.FareghReqStatus.maali_ok:
                   
                        entekhabvahed.Visible = true;
                        hozur.Visible = true;

                        break;
                    case (int)CheckOutStatusEnum.FareghReqStatus.refah_ok:
                      
                        entekhabvahed.Visible = true;
                        hozur.Visible = true;

                        break;
                    case (int)CheckOutStatusEnum.FareghReqStatus.mashmulan_ok:
                   
                        entekhabvahed.Visible = false;
                        hozur.Visible = false;

                        break;
                    default:
                   
                        entekhabvahed.Visible = false;
                        hozur.Visible = false;

                        break;
                }

            }
            else
            {
                grd_CheckOutList.Visible = false;
            }
            lblMessage.Text = "";
          
            //RadWindow1.VisibleOnPageLoad = false;
           

        }
        private void BindData(int nextstatus, bool isneeddatasource)
        {
            drpUserRoles.Visible = false;
            DataTable reqList = null;

            int roleId = Convert.ToInt32(Session["roleId"]);
            int daneshId = business.GetDaneshKadeIdByRoleId(roleId);

            if (nextstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshkade)
            {
                if (daneshId == 0)
                {
                    reqList = business.GetListOFRequestByNextStatus(nextstatus);
                }
                else
                {
                    reqList = business.GetListOFRequestByNextStatusAndDaneshId(nextstatus, daneshId);
                }
            }
            else if (nextstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive && roleId != 1)
            {
                reqList = business.GetListOFRequestByNextStatusAndArchiveRole(nextstatus, roleId);
            }
            else
            {
                // reqList = business.GetListOFRequestByNextStatus(nextstatus);

                reqList= business.GetListOFRequestByNextStatusAndDaneshId((int)CheckOutStatusEnum.CheckOutAllStatusEnum.takmil_parvande, daneshId);

            }

            ViewState.Add("status", nextstatus);
            //SwitchColumnsByStatus(nextstatus);
            grd_CheckOutList.DataSource = reqList;
            //grd_CheckOutList.EmptyDataText = "هیچ درخواست تسویه ای پیدا نشد.";
            if (!isneeddatasource)
                grd_CheckOutList.DataBind();

            GridFilterMenu menu = grd_CheckOutList.FilterMenu;
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

        protected void grd_CheckOutList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int Erae_be = 0;
            int reqType = 0;
            string stcode;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnSendMsg = (Button)e.Row.FindControl("btnSendMsg");
                Label lblUserMessage = (Label)e.Row.FindControl("lblUserMessage");
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                if (!String.IsNullOrWhiteSpace(rowView["message"].ToString()))
                {
                    lblUserMessage.Text = rowView["message"].ToString();
                    btnSendMsg.Visible = false;
                    lblUserMessage.Visible = true;
                }
                Erae_be = Convert.ToInt32(ViewState["status"]);
                reqType = Convert.ToInt32(rowView["RequestTypeID"]);
                stcode = rowView["StCode"].ToString();

                Label lblNezam = (Label)e.Row.FindControl("lblNezam");
                if (rowView["nezam"].ToString() == "7")
                {
                    lblNezam.Text = "بلی";
                }
                else
                {
                    lblNezam.Text = "خیر";
                }

                if (Erae_be == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
                {
                    int bedehi = MaliBusiness.CheckMaliCheckOut(stcode);

                    if (bedehi > 0)
                    {
                        e.Row.BackColor = Color.FromName("#FEFFAE");
                    }
                    else
                    {
                        e.Row.BackColor = Color.FromName("#B3FFAE");
                    }
                    //ImageButton lblHozoorHour = (ImageButton)e.Row.FindControl("lblHozoorHour");
                    //ImageButton lblDateSabt = (ImageButton)e.Row.FindControl("lblEntekhabVahedDate");
                    Label lblhour = (Label)e.Row.FindControl("lblhour");
                    //lblDateSabt.Text = rowView["datesabtv"].ToString();
                    if (reqType == 14 || reqType == 16)
                    {

                        DataTable dt = StudentBusiness.GetTotalTimeInAllClassesByStcode(stcode);
                        if (dt.Rows.Count > 0)
                        {
                            lblhour.Text = "مدت زمان حضور دانشجو(دقیقه)" + dt.Rows[0]["SumOfTime"].ToString();
                        }
                        else
                        {
                            lblhour.Text = "0";
                        }
                    }
                    else
                    {
                        lblhour.Text = "-";
                    }
                }

                if (Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.mashmulan 
                    ||Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive
                    )
                {
                    if (reqType == (int)CheckOutStatusEnum.CheckOutType.ekhraj || reqType == (int)CheckOutStatusEnum.CheckOutType.enseraf)
                    {
                        e.Row.Cells[5].Text = "فاقد تاریخ دفاع";
                        bool bayganiOk = Convert.ToBoolean(rowView["BayganiOk"]);
                        if (business.isMashmool(stcode) && !bayganiOk)
                        {
                            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
                            btnApprove.Enabled = false;
                        }
                        else
                        {
                            Button btnApprove = (Button)e.Row.FindControl("btnApprove");
                            btnApprove.Enabled = true;
                        }
                    }
                }
            }
        }

        protected void grd_CheckOutList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            int status = Convert.ToInt32(ViewState["status"]);
            BindData(21, false);
        }

        protected int convertReqType(string req)
        {
            if (req == " تغيير رشته")
            {
                return 13;
            }
            if (req == " اخراج")
            {
                return 14;
            }
            if (req == "فارغ التحصيلي")
            {
                return 15;
            }
            if (req == "انصراف")
            {
                return 16;
            }

            return -1;//no match found
        }

        protected void grd_CheckOutList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            int Erae_be = 0;
            int reqType = 0;
            string stcode;

            if (e.Item is GridDataItem)
            {

                GridDataItem itemAmount = (GridDataItem)e.Item;
                System.Web.UI.WebControls.Image btnreq = (System.Web.UI.WebControls.Image)itemAmount.FindControl("imgTypeRequest");
                System.Web.UI.WebControls.Image btnmsg = (System.Web.UI.WebControls.Image)itemAmount.FindControl("imgSenMessage");
                switch (itemAmount["RequestTypeID"].Text)
                {
                    case "13":
                        btnreq.ImageUrl = "~/University/Theme/images/change.png";
                        btnreq.Attributes["title"] = "تغییر رشته";
                        break;
                    case "14":
                        btnreq.ImageUrl = "~/University/Theme/images/dismiss.png";
                        btnreq.Attributes["title"] = "اخراج";
                        break;
                    case "15":
                        btnreq.ImageUrl = "~/University/Theme/images/graduate.png";
                        btnreq.Attributes["title"] = "فارغ التحصیل";
                        break;
                    case "16":
                        btnreq.ImageUrl = "~/University/Theme/images/cancel.png";
                        btnreq.Attributes["title"] = "انصراف";
                        break;
                }
                ImageButton btnSendMsg = (ImageButton)itemAmount.FindControl("btnSendMsg");
                ImageButton Msg = (ImageButton)itemAmount.FindControl("msg");
                Label lblUserMessage = (Label)itemAmount.FindControl("lblUserMessage");
                DataRowView rowView = (DataRowView)itemAmount.DataItem;
                //(grd_CheckOutList.MasterTableView.GetColumn("t") as GridTemplateColumn).Visible = false;
               
                //if (String.IsNullOrWhiteSpace(rowView["Message"].ToString()) && rowView["nezam"].ToString() == "7")
                //{
                //    itemAmount.BackColor = Color.FromName("#FFBA00");
                //}
                Erae_be = Convert.ToInt32(ViewState["status"]);
                
                ImageButton lblHozoorHour = (ImageButton)itemAmount.FindControl("imgHozoorHour");
                ImageButton lblDateSabt = (ImageButton)itemAmount.FindControl("imgEntekhabVahedDate");
               
                //ImageButton btnTaeedMashmool = (ImageButton)itemAmount.FindControl("btnTaeedMashmool");
                ImageButton btnApprove = (ImageButton)itemAmount.FindControl("btnApprove");

               
                if (Erae_be == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
                {
                    lblHozoorHour.Visible = true;
                    lblDateSabt.Visible = true;
                  
                 
                }
                else
                {
                    lblHozoorHour.Visible = false;
                    lblDateSabt.Visible = false;
                   
                     
                    //((DataControlField)grd_CheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "عملیات مالی").SingleOrDefault()).Visible = false;
                  
                }

                //if (Erae_be == (int)CheckOutStatusEnum.FareghReqStatus.fareghotahsilan_ok)
                if (Erae_be == (int)CheckOutStatusEnum.FareghReqStatus.vrood_moavenat_ok)
                {
                    //  btnnaghs.Visible = true;
                }
                else
                {
                    //  btnnaghs.Visible = false;
                }



                //if (Erae_be == (int)CheckOutStatusEnum.FareghReqStatus.mashmulan_ok ||
                //    Erae_be == (int)CheckOutStatusEnum.FareghReqStatus.archive_ok)
                //{
                //    btnTaeedMashmool.Enabled = true;
                //    btnTaeedMashmool.Visible = true;
                //}
                //else
                //{
                //    btnTaeedMashmool.Enabled = false;
                //    btnTaeedMashmool.Visible = false;
                //}

                //if (Erae_be == (int)CheckOutStatusEnum.FareghReqStatus.archive_ok)
                //{
                //    ((DataControlField)grd_CheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "تایید مشمول").SingleOrDefault()).Visible = true;
                //}
                //else
                //{
                //    ((DataControlField)grd_CheckOutList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "تایید مشمول").SingleOrDefault()).Visible = false;
                //}

                if (Erae_be == (int)CheckOutStatusEnum.EnserafReqStatus.end)
                {
                   // btnApprove.Visible = false;
                    //btnTaeedMashmool.Visible = false;
                }
                reqType = Convert.ToInt32(rowView["RequestTypeID"]);
                stcode = rowView["StCode"].ToString();

                Label lblNezam = (Label)itemAmount.FindControl("lblNezam");
                if (rowView["nezam"].ToString() == "7")
                {
                    itemAmount.BackColor = Color.FromName("#A173FF");
                }
                //if (String.IsNullOrWhiteSpace(rowView["Message"].ToString()) && rowView["nezam"].ToString() == "7")
                //{
                //    itemAmount.BackColor = Color.FromName("#FFFA71");
                //}

                if (Erae_be == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
                {
                    int bedehi = MaliBusiness.CheckMaliCheckOut(stcode);

                    if (bedehi > 0)
                    {
                        itemAmount.BackColor = Color.FromName("#FF4F4F");
                    }
                    else
                    {
                        itemAmount.BackColor = Color.FromName("#B3FFAE");
                    }
                    //ImageButton lblHozoorHour = (ImageButton)e.Row.FindControl("lblHozoorHour");
                    //ImageButton lblDateSabt = (ImageButton)e.Row.FindControl("lblEntekhabVahedDate");
                    Label lblhour = (Label)itemAmount.FindControl("lblhour");
                    //lblDateSabt.Text = rowView["datesabtv"].ToString();
                    if (reqType == 14 || reqType == 16)
                    {

                        DataTable dt = new DataTable();
                        dt = StudentBusiness.GetTotalTimeInAllClassesByStcode(stcode);
                        if (dt.TableName == "Exception")
                            lblhour.Text = "خطا در محاسبه ساعت حضور";

                        else
                        {

                            if (dt.Rows.Count > 0)
                            {
                                lblhour.Text = "مدت زمان حضور دانشجو(دقیقه)" + dt.Rows[0]["SumOfTime"].ToString();
                            }
                            else
                            {
                                lblhour.Text = "0";
                            }
                        }
                    }
                    else
                    {
                        lblhour.Text = "-";
                    }
                }

                if (Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.mashmulan
                    || Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive
                    )
                {
                   
                    if (reqType == (int)CheckOutStatusEnum.CheckOutType.ekhraj || reqType == (int)CheckOutStatusEnum.CheckOutType.enseraf)
                    {
                        itemAmount["Def_Date"].Text = "فاقد تاریخ دفاع";
                        bool bayganiOk = Convert.ToBoolean(rowView["BayganiOk"]);
                        if (business.isMashmool(stcode) && !bayganiOk)
                        {
                        }
                        else
                        {

                            //btnTaeedMashmool.Enabled = false;
                            //btnTaeedMashmool.Visible = true;
                          //  btnApprove.Enabled = true;
                        }
                    }
                }

            }
        }

        protected void grd_CheckOutList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (drpUserRoles.SelectedIndex > 0)
            {
                grd_CheckOutList.Visible = true;
                //Session.Add("activeRoleStatus", drpUserRoles.SelectedValue);
                BindData(21, true);

            }
            else
            {
                userID = Session[sessionNames.userID_Karbar].ToString();
                userdt = lngB.Get_UserRoles(userID);
                int currentstatus = business.GetStatusOfRole(Convert.ToInt32(userdt.Rows[0][1]));
                BindData(21, true);

                //grd_CheckOutList.Visible = false;
            }
            lblMessage.Text = "";
         
            //RadWindow1.VisibleOnPageLoad = false;
           
        }

        protected void grd_CheckOutList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                string msg = "";
                int reqID = Convert.ToInt32(e.CommandArgument);
                userID = Session[sessionNames.userID_Karbar].ToString();
                int currentstatus = Convert.ToInt32(ViewState["status"]);
                GridDataItem itemAmount = (GridDataItem)e.Item;
                int reqType = int.Parse(itemAmount["RequestTypeID"].Text);
                string stcode = itemAmount["StCode"].Text;
                string stName = itemAmount["name"].Text;

                ViewState.Add("reqType", reqType);
                ViewState.Add("reqID", reqID);
                ViewState.Add("currentstatus", currentstatus);

             

             

            


               

                if (!string.IsNullOrWhiteSpace(msg))
                {
                    lblMessage.Text = msg;
                    lblMessage.Visible = true;
                    RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "");
                }

                BindData(21, false);
            }
        }
    }
}