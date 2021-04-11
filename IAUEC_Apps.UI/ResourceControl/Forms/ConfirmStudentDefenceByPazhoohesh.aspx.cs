using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.ResourceControlClasses;
using IAUEC_Apps.UI.University.GraduateAffair.CMS;
using Telerik.Web.UI;
using Control = System.Web.UI.Control;
using ListItem = System.Web.UI.WebControls.ListItem;
using System.Data;

namespace ResourceControl.PL.Forms
{
    public partial class ConfirmStudentDefenceByPazhoohesh : System.Web.UI.Page
    {

        private List<StudentDefenceRequestDTO> _reqlist = null;
        private CommonBusiness cmb = null;
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

                BindDrpTerms();
                //دفاع های بررسی نشده
                Session["drpDefenceType"]= 1;
                Session["AllRequstsList"] = _requestHandler.GetStudentDefenceListForPazhoohesh(0,drpTerms.SelectedValue?.ToString())?.ToList();
                BindGrid(Convert.ToInt32(Session["drpDefenceType"].ToString()) , drpTerms.SelectedValue?.ToString());                
            }

        }

        protected void drpDefenceTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["drpDefenceType"]= drpDefenceTypeList.SelectedValue;
            BindGrid(Convert.ToInt32(Session["drpDefenceType"].ToString()) ,drpTerms.SelectedValue?.ToString());
        }

        private void BindGrid(int defenceType, string term = null)
        {
            _reqlist = _requestHandler.GetStudentDefenceListForPazhoohesh(0, term)?.ToList();
            if (_reqlist != null && _reqlist.Count > 0)
            {
                Session["AllRequstsList"] = _reqlist;

                switch (defenceType)
                {
                    case 1://دفاع های بررسی نشده
                        grdvPazhoohesh.DataSource = _reqlist?.Where(w => w.DefenceHasDone == null).ToList();
                        Session["FilteredRequstsList"] = _reqlist?.Where(w => w.DefenceHasDone == null).ToList();
                        break;

                    case 2://دفاع های برگذار نشده
                        grdvPazhoohesh.DataSource = _reqlist?.Where(w => w.DefenceHasDone == false).ToList();
                        Session["FilteredRequstsList"] = _reqlist?.Where(w => w.DefenceHasDone == false).ToList();
                        break;

                    case 3://دفاع های برگذار شده
                        grdvPazhoohesh.DataSource = _reqlist?.Where(w => w.DefenceHasDone == true && (w.ChkPaymentDavar1 == false || w.ChkPaymentDavar2 == false)).ToList();
                        Session["FilteredRequstsList"] = _reqlist?.Where(w => w.DefenceHasDone == true && (w.ChkPaymentDavar1 == false || w.ChkPaymentDavar2 == false)).ToList();
                        break;

                    case 4://کل دفاع ها
                        grdvPazhoohesh.DataSource = _reqlist;
                        Session["FilteredRequstsList"] = _reqlist?.ToList();
                        break;

                    default:
                        grdvPazhoohesh.DataSource = null;
                        Session["FilteredRequstsList"] = null;
                        break;

                }
                grdvPazhoohesh.Rebind();
            }
           
        }

       

        protected void grdvPazhoohesh_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            this.grdvPazhoohesh.DataSource = (List<StudentDefenceRequestDTO>)Session["FilteredRequstsList"];
            GridFilterMenu menu = this.grdvPazhoohesh.FilterMenu;
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


        protected void grdvPazhoohesh_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Filter" || e.CommandName == "Page") return;

                var res = false;
                var userID = int.Parse(Session[sessionNames.userID_Karbar].ToString());
                cmb = new CommonBusiness();
                var reqId = int.Parse(e.CommandArgument?.ToString() ?? "-1");

                if (e.CommandName == "DefenceHasDone")
                {
                    res = _requestHandler.UpdateDefenceInformation_DefenceHasDone(reqId, true);
                    cmb.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 212, string.Format("{0}", "تایید برگزاری دفاع توسط پژوهش"), reqId);
                    BindGrid(Convert.ToInt32(Session["drpDefenceType"].ToString()), drpTerms.SelectedValue?.ToString());
                }
                else if (e.CommandName == "DefenceHasNotDone")
                {

                    res = _requestHandler.UpdateDefenceInformation_DefenceHasDone(reqId, false);
                    cmb.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 213, string.Format("{0}", "تایید عدم برگزاری دفاع توسط پژوهش"), reqId);
                    BindGrid(Convert.ToInt32(Session["drpDefenceType"].ToString()), drpTerms.SelectedValue?.ToString());

                }
                
                else if (e.CommandName == "History")
                {
                    var dtlog = cmb.GetUserAndStudentLogModifyId(reqId, 11);
                    var myLog = RequestHandler.ConvertDataTableToList<logDetail>(dtlog).OrderBy(O => O.LogDate.ToGregorian()).ThenBy(x => x.LogTime.TimeToTicks());

                    lst_history.DataSource = myLog;
                    lst_history.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
              

            }
            catch (Exception x)
            {
                throw x;
            }
        }


        protected void bt1ExportExcle_OnClick(object sender, ImageClickEventArgs e)
        {
            //var daneshId = Convert.ToInt32(Session["DaneshId"]);
            //var allAcceptedRequest = _requestHandler.GetAllAccepetedDefenceRequests(daneshId );

            var allAcceptedRequest = _requestHandler.GetStudentDefenceListForPazhoohesh_Report(1,drpTerms.SelectedValue?.ToString());  //i=execl 2=text  report
            var list = allAcceptedRequest;
            var lstRows = new List<DataRow>();

            switch (Convert.ToInt32(Session["drpDefenceType"].ToString()))
            {
                case 1: //دفاع بررسی نشده

                    lstRows = list.AsEnumerable().Where(w => string.IsNullOrEmpty(w.Field<bool?>("DefenceHasDone").ToString())).ToList();                   
                    break;

                case 2://دفاع برگزار نشده
                    lstRows = list.AsEnumerable().Where(w => !string.IsNullOrEmpty(w.Field<bool?>("DefenceHasDone").ToString()) 
                                        && w.Field<bool?>("DefenceHasDone") == false).ToList();                   
                    break;

                case 3://دفاع برگزار شده
                    lstRows = list.AsEnumerable().Where(w => !string.IsNullOrEmpty(w.Field<bool?>("DefenceHasDone").ToString()) 
                                            && w.Field<bool?>("DefenceHasDone") == true                                             
                                            && (w.Field<bool>("ChkPaymentDavar1") == false || w.Field<bool>("ChkPaymentDavar2") == false))
                             .ToList();                    
                    break;

                case 4://همه دفاع ها
                    lstRows = allAcceptedRequest.AsEnumerable().ToList();
                    break;

                default:
                    break;
            }

            if (lstRows.Count() > 0)
            {
                var dt = lstRows.CopyToDataTable();

                try
                {

                    var pck = new OfficeOpenXml.ExcelPackage();
                    var ws = pck.Workbook.Worksheets.Add("ProfInfoList");

                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=DefenceList.xlsx");

                    Response.BinaryWrite(pck.GetAsByteArray());

                }
                catch (Exception ex)
                {
                    //log error
                    //throw exx
                }
                Response.End();

            }
        }

        protected void grdvPazhoohesh_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem itemAmount = (GridDataItem)e.Item;
                Button btnDefenceHasDone = e.Item.FindControl("btnDefenceHasDone") as Button;
                Button btnDefenceHasNotDone = e.Item.FindControl("btnDefenceHasNotDone") as Button;
                Button btnDefencePayed = e.Item.FindControl("btnDefencePayed") as Button;

                switch (drpDefenceTypeList.SelectedValue)
                {
                    case "1"://بررسی نشده
                        btnDefenceHasDone.Visible = true;
                        btnDefenceHasNotDone.Visible = true;
                        break;

                    case "2"://برگزار شده
                        btnDefenceHasDone.Visible = true;
                        break;

                    case "3"://برگزار شده
                        btnDefenceHasNotDone.Visible = true;
                        //btnDefencePayed.Visible = true;
                        break;

                    //case "4"://تمامی دفاع ها
                    //    btnDefenceHasDone.Visible = true;
                    //    btnDefenceHasNotDone.Visible = true;
                    //    break;

                    //case "5"://دفاع های پرداخت شده
                    //    btnDefencePayed.Visible = true;                      
                    //    break;

                    default:
                        break;
                }
            }
        }


        void BindDrpTerms()
        {
            var terms = _requestHandler.GetAllTermsForDefence();
            if (terms.Rows.Count > 0)
            {
                drpTerms.DataSource = terms;
                drpTerms.DataTextField = "term";
                drpTerms.DataValueField = "term";
                drpTerms.DataBind();
            }
        }

        protected void drpTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(Convert.ToInt32(Session["drpDefenceType"].ToString()), drpTerms?.SelectedValue?.ToString());
        }
    }
}