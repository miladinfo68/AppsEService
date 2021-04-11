using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.ResourceControlClasses;
using IAUEC_Apps.UI.University.GraduateAffair.CMS;
using ResourceControl.BLL;
using ResourceControl.Entity;
using ResourceControl.PL.Forms;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class FinancialPermission : System.Web.UI.Page
    {
        private List<IAUEC_Apps.DTO.ResourceControlClasses.FinancialPermission> _reqlist = null;
        private RequestHandler _requestHandler = new RequestHandler();
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

                ViewState["selectedState"] = 1;
            }


        }


        protected void grdDefenceList_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var financialPermission = _requestHandler.GetFinancialPermission();

            switch (Convert.ToInt32(ViewState["selectedState"]))
            {
                case 1://منتظر تایید
                    _reqlist = financialPermission.Where(x => x.Permission == null).ToList();
                    break;
                case 2://تایید شده
                    _reqlist = financialPermission.Where(x => x.Permission == true).ToList();
                    break;
                case 3://رد شده
                    _reqlist = financialPermission.Where(x => x.Permission == false).ToList();
                    break;
                case 4://کلیه درخواست ها
                    _reqlist = financialPermission.ToList();
                    break;
            }



            grdDefenceList.DataSource = _reqlist;
            GridFilterMenu menu = grdDefenceList.FilterMenu;
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

        protected void grdDefenceList_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            var status = Convert.ToInt32(ViewState["selectedState"]);
            var eItem = e.Item as GridDataItem;
            if (eItem == null) return;
            GridDataItem item = eItem;
            Button divApprove = (Button)item["operator"].FindControl("btnApprove");
            Button divAvoid = (Button)item["operator"].FindControl("btnAvoid");
            switch (status)
            {
                case 1:
                    divApprove.Enabled = true;
                    divAvoid.Enabled = false;
                    break;
                case 2:
                    divApprove.Enabled = false;
                    divAvoid.Enabled = true;
                    break;
                case 3:
                    divApprove.Enabled = true;
                    divAvoid.Enabled = false;
                    break;
                case 4:
                    divApprove.Enabled = false;
                    divAvoid.Enabled = false;
                    break;
            }

        }

        protected void grdDefenceList_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            //throw new NotImplementedException();
        }


        protected void btnApprove_OnClick(object sender, EventArgs e)
        {


            var btn = (Button)sender;
            var data = (GridDataItem)btn.NamingContainer;
            var studentCode = data["StudentCode"].Text;
            var student = _requestHandler.GetFinancialPermission().FirstOrDefault(x => x.StudentCode == studentCode);
            student.Permission = true;
            var id = _requestHandler.AddOrUpdateFinancialPermission(student);

            var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);

            var comman = new CommonBusiness();
            comman.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm")
                , 11, 185, string.Format("{0}", "تایید مالی"), Convert.ToInt32(id));

            var approveMessage = "درخواست شماره " + id.ToString() + " با موفقیت تایید گردید.";
            RadWindowManager1.RadAlert(approveMessage, 400, 100, "پیام سیستم", "closeRadWindow2");
        }


        protected void btnDenyRequest_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            var studentCode = data["StudentCode"].Text;
            var student = _requestHandler.GetFinancialPermission().FirstOrDefault(x => x.StudentCode == studentCode);
            student.Permission = false;
            var id = _requestHandler.AddOrUpdateFinancialPermission(student);
            var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            var comman = new CommonBusiness();
            comman.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm")
                , 11, 186, string.Format("{0}", "رد مالی"), Convert.ToInt32(id));
            var approveMessage = "درخواست شماره " + id.ToString() + " با موفقیت تایید گردید.";
            RadWindowManager1.RadAlert(approveMessage, 400, 100, "پیام سیستم", "closeRadWindow2");

        }


        protected void btnHistory_OnClick(object sender, ImageClickEventArgs e)
        {
            RequestHandler _reqHandler = new RequestHandler();
            CommonBusiness cmb = new CommonBusiness();
            ImageButton btn = (ImageButton)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string id = data["ID"].Text;

            var dtLog = cmb.GetUserAndStudentLogModifyId(int.Parse(id), 11);//.AsEnumerable();
            var myLog = RequestHandler.ConvertDataTableToList<logDetail>(dtLog)
                .OrderBy(o => o.LogDate.ToGregorian())
                .ThenBy(x => x.LogTime.TimeToTicks());

            //var Rows = (from row in dtLog
            //            orderby row["LogDate"].ToString().ToGregorian() 
            //            select row);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void drpRequestTypeList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(Convert.ToInt32(drpRequestTypeList.SelectedValue));
        }

        private void BindGrid(int selectedTypeRequest)
        {
            ViewState["selectedState"] = selectedTypeRequest;
            grdDefenceList.Rebind();
        }

        protected void btnRefreshGrid_OnClick(object sender, EventArgs e)
        {
            BindGrid(Convert.ToInt32(drpRequestTypeList.SelectedValue));
        }


        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            return result;
        }



        protected void bt1ExportExcle_OnClick(object sender, ImageClickEventArgs e)
        {
            var daneshId = Convert.ToInt32(Session["DaneshId"]);
            var allAcceptedRequest = _requestHandler.GetAllAccepetedDefenceRequests(daneshId);
            if (allAcceptedRequest.Rows.Count > 0)
            {
                try
                {

                    var pck = new OfficeOpenXml.ExcelPackage();
                    var ws = pck.Workbook.Worksheets.Add("ProfInfoList");

                    ws.Cells["A1"].LoadFromDataTable(allAcceptedRequest, true);
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=DefenceList.xlsx");

                    Response.BinaryWrite(pck.GetAsByteArray());

                }
                catch (Exception ex)
                {
                    //log error
                }
                Response.End();

            }
        }
    }
}