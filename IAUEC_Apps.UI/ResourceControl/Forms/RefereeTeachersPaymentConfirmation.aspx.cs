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
using System.IO;
using System.Text;

namespace ResourceControl.PL.Forms
{
    public partial class RefereeTeachersPaymentConfirmation : System.Web.UI.Page
    {

        private List<RefereeInformation> _reqlist = null;
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
                Session["drpMartabe"] = _requestHandler.GetAllMartabeAndWage();
                BindGrid(int.Parse(drpRefereeWageStatus.SelectedValue), drpTerms.SelectedValue);
            }

        }


        private void BindGrid(int refereeWageStatus = 0, string term = null)
        {
            if (refereeWageStatus == 0)//لیست داوران که حق و زحمه دریافت نکردن
            {
                List<RefereeInformation> ds = _requestHandler.GetRefereeTeachersPaymentHasNotDown(term)?.ToList();
                grdRefereePayment.DataSource = ds;
                grdRefereePayment.DataBind();
            }
            else if (refereeWageStatus == 1) //لیست داوران که حق و زحمه دریافت کردن   
            {
                List<RefereeInformation> ds = _requestHandler.GetRefereeTeachersPaymentHasDown(term)?.ToList();
                grdRefereePayment.DataSource = ds;
                grdRefereePayment.DataBind();

            }
        }


        protected void grdRefereePayment_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (drpRefereeWageStatus.SelectedValue == "0")
            {
                grdRefereePayment.DataSource = _requestHandler.GetRefereeTeachersPaymentHasNotDown(drpTerms.SelectedValue).ToList();
            }
            else if (drpRefereeWageStatus.SelectedValue == "1")
            {
                grdRefereePayment.DataSource = _requestHandler.GetRefereeTeachersPaymentHasDown(drpTerms.SelectedValue).ToList();
            }
            else
            {
                grdRefereePayment.DataSource = new List<RefereeInformation>();
            }

            GridFilterMenu menu = this.grdRefereePayment.FilterMenu;
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

        protected void grdRefereePayment_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Filter" || e.CommandName == "Page") return;

                var res = false;
                var userID = int.Parse(Session[sessionNames.userID_Karbar].ToString());
                cmb = new CommonBusiness();

                //getting multi passed params
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

                int reqId = int.Parse(commandArgs[0] ?? "-1");
                var studentFullName = commandArgs[1] ?? "";
                var requestDate = commandArgs[2] ?? "";           
                var refereeMobile = commandArgs[3] ?? "";
                var refereeFullName = commandArgs[4] ?? "";
                var studentCode = commandArgs[5] ?? "";
                int refereeType = int.Parse(commandArgs[6]?.ToString() ?? "-1");
                var salary= commandArgs[7] ?? "";


                var msgPayment = "";
                //################################################
                if (e.CommandName == "PaymentHasDone")
                {

                    res = _requestHandler.UpdateDefenceInformation_DefenceHasDone(reqId, true, refereeType, true);
                    cmb.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 214, string.Format("{0}", "پرداخت مبلغ دفاع به استاد"), reqId);
                    
                    if (!string.IsNullOrEmpty(refereeMobile))
                    {
                        msgPayment += "استاد گرانقدر؛";
                        msgPayment += "\r\n";
                        msgPayment += "مبلغ حق الزحمه داوري پايان نامه ";
                        msgPayment += studentFullName;
                        msgPayment += " برگزار شده در تاريخ ";
                        msgPayment += requestDate;
                        msgPayment += " به مبلغ ";
                        msgPayment += salary;
                        msgPayment += " ریال به شماره حساب سركارعالي/جنابعالي واريز گرديد. ";
                        msgPayment += "\r\n";
                        msgPayment += "معاونت پژوهشي واحد الكترونيكي دانشگاه آزاد اسلامي";

                        cmb.sendSMS(refereeMobile, msgPayment, out bool sentSms, out string smsStatusText);
                    }


                    BindGrid(Convert.ToInt32(drpRefereeWageStatus.SelectedValue), drpTerms?.SelectedValue);
                }
                //################################################
                else if (e.CommandName == "PaymentNotDone")
                {

                    res = _requestHandler.UpdateDefenceInformation_DefenceHasDone(reqId, true, refereeType, false);
                    cmb.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 215, string.Format("{0}", "اصلاحی عدم پرداخت مبلغ دفاع به استاد"), reqId);
                    
                    BindGrid(Convert.ToInt32(drpRefereeWageStatus.SelectedValue), drpTerms?.SelectedValue?.ToString());

                }
                else if (e.CommandName == "RejectDefenseRequest")
                {
                    //var isOk = _requestHandler.InsertIntoRefereeWagePayment_Log(reqId, studentFullName, requestDate, collegeName, refereeMobile, martabe, refereeFullName, studentCode, refereeSiba, salary, refereeType, drpTerms.SelectedValue, true);
                    lblRejRequest.Text = reqId.ToString();
                    Session["RefMobile"] = refereeMobile;//need to send sms to ostad

                    if (!string.IsNullOrEmpty(refereeMobile))
                    {
                        msgPayment += "استاد گرانقدر؛";
                        msgPayment += "\r\n";
                        msgPayment += "مبلغ حق الزحمه داوري پايان نامه ";
                        msgPayment += studentFullName;
                        msgPayment += " برگزار شده در تاريخ ";
                        msgPayment += requestDate;
                        msgPayment += " بدليل نقص در حكم كارگزيني يا شماره حساب سركارعالي/جنابعالي واريز نگرديد  ";
                        msgPayment += " جهت رفع نقص و انجام اصلاحات به سامانه خدمات الكترونيكي به آدرس ";
                        msgPayment += "\r\n";
                        msgPayment += " service.iauec.ac.ir ";
                        msgPayment += "\r\n";
                        msgPayment += " مراجعه فرماييد ";
                        msgPayment += "\r\n";
                        msgPayment += " معاونت پژوهشي واحد الكترونيكي دانشگاه آزاد اسلامي ";

                        cmb.sendSMS(refereeMobile, msgPayment, out bool sentSms, out string smsStatusText);
                    }
                    cmb.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 221, string.Format("{0}", "عدم پرداخت حق الزحمه دفاع استاد به دلیل نقص مرتبه"), reqId);

                    //send sms to theacher in modal box
                    string scrp = "function f(){showLightBox(); $find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
                }
               
                //################################################
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

        protected void grdRefereePayment_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //DataTable dsMartabe = null;
            if (e.Item is GridDataItem)
            {
                GridDataItem itemAmount = (GridDataItem)e.Item;
                Button btnPaymentHasDone = e.Item.FindControl("btnPaymentHasDone") as Button;
                Button btnPaymentNotDone = e.Item.FindControl("btnPaymentNotDone") as Button;
                Button btnRejectDefenseRequest = e.Item.FindControl("btnRejectDefenseRequest") as Button;

                HiddenField hdn_ChkPaymentReferee1 = e.Item.FindControl("hdn_ChkPaymentReferee1") as HiddenField;
                HiddenField hdn_ChkPaymentReferee2 = e.Item.FindControl("hdn_ChkPaymentReferee2") as HiddenField;
                HiddenField hdn_RefereeType = e.Item.FindControl("hdn_RefereeType") as HiddenField;

                switch (drpRefereeWageStatus.SelectedValue)
                {
                    case "0"://داورنی که حق الزحمه دریافت نکرده اند
                        btnPaymentHasDone.Visible = true;
                        btnRejectDefenseRequest.Visible = true;
                        if (
                            (hdn_ChkPaymentReferee1 != null && hdn_ChkPaymentReferee1.Value == "True" && hdn_RefereeType.Value == "1")
                            || (hdn_ChkPaymentReferee2 != null && hdn_ChkPaymentReferee2.Value == "True" && hdn_RefereeType.Value == "2")
                            )
                        {
                            btnPaymentHasDone.Enabled = false;

                            btnPaymentHasDone.BackColor = System.Drawing.Color.Gray;
                            btnPaymentHasDone.ForeColor = System.Drawing.Color.White;

                        }
                        break;

                    case "1"://داورنی که حق الزحمه دریافت  کرده اند
                        btnPaymentNotDone.Visible = true;
                        if (
                            (hdn_ChkPaymentReferee1 != null && hdn_ChkPaymentReferee1.Value == "False" && hdn_RefereeType.Value == "1")
                            || (hdn_ChkPaymentReferee2 != null && hdn_ChkPaymentReferee2.Value == "False" && hdn_RefereeType.Value == "2")
                            )
                        {
                            btnPaymentNotDone.Enabled = false;
                            btnPaymentNotDone.BackColor = System.Drawing.Color.Gray;
                            btnPaymentNotDone.ForeColor = System.Drawing.Color.White;
                        }
                        break;

                    default:
                        break;
                }



            }
        }

        protected void drpRefereeWageStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(Convert.ToInt32(drpRefereeWageStatus.SelectedValue), drpTerms?.SelectedValue?.ToString());
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



        protected void bt1ExportExcle_Click(object sender, ImageClickEventArgs e)
        {
            var dt = _requestHandler.GetRefereeTeachersPayment_Report(int.Parse(drpRefereeWageStatus.SelectedValue.ToString()), 0, drpTerms.SelectedValue);

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
            catch (Exception x)
            {
                //throw x;
            }
            Response.End();
        }

        protected void btnExportText_Click(object sender, ImageClickEventArgs e)
        {
            ExportReportAsText();
        }

        public void ExportReportAsText()
        {
            var dt = _requestHandler.GetRefereeTeachersPayment_Report(int.Parse(drpRefereeWageStatus.SelectedValue.ToString()), 1, drpTerms.SelectedValue);
            StringBuilder strBuilder = new StringBuilder();
            //string[] columnNames = dt.Columns.Cast<DataColumn>().Where(w => !w.ColumnName.ToString().Contains("ChkPaymentDavar")).Select(x => x.ColumnName).ToArray();
            string[] columnNames = new[] { "نام استاد داور", "شماره دانشجویی", "شماره حساب داور", "مبلغ پرداختی" };
            var concat_all_columnnames = string.Join(",", columnNames);
            strBuilder.AppendFormat("{0}", concat_all_columnnames);
            strBuilder.AppendFormat("{0}", "\r\n\r\n");

            var fdavar = "";
            var stcod = "";
            var siba = "";
            var pay = "";


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                fdavar = string.IsNullOrEmpty(dt.Rows[i]["RefereefullName"]?.ToString().Trim()) ? "0" : dt.Rows[i]["RefereefullName"]?.ToString().Trim();
                stcod = string.IsNullOrEmpty(dt.Rows[i]["StudentCode"]?.ToString().Trim()) ? "0" : dt.Rows[i]["StudentCode"]?.ToString().Trim();
                siba = string.IsNullOrEmpty(dt.Rows[i]["SibaNo"].ToString().Trim()) ? "0" : dt.Rows[i]["SibaNo"].ToString().Trim();
                pay = string.IsNullOrEmpty(dt.Rows[i]["Wage"]?.ToString().Trim()) ? "0" : dt.Rows[i]["Wage"]?.ToString().Trim();

                strBuilder.AppendFormat("{0},{3},{2},{1}", fdavar, stcod, siba, pay);
                strBuilder.AppendLine();
            }
            try
            {
                MemoryStream ms = new MemoryStream();
                TextWriter tw = new StreamWriter(ms);
                tw.WriteLine(strBuilder);
                tw.Flush();
                byte[] bytes = ms.ToArray();
                ms.Close();
                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment;filename=DefenceRefereePayment.txt");
                Response.BinaryWrite(bytes);
                Response.End();
            }
            catch (Exception x)
            {
                throw x;
            }

            //}

        }

        protected void drpTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(Convert.ToInt32(drpRefereeWageStatus.SelectedValue), drpTerms?.SelectedValue);
        }


        protected void btnRejectRequest_Click(object sender, EventArgs e)
        {
            var refereeMobile = Session["RefMobile"]?.ToString();
            if (!string.IsNullOrEmpty(refereeMobile))
            {
                var note = txtRejectRequest.Text.Trim();
                if (note != "")
                {
                    //cmb.sendSMS(Session["RefMobile"].ToString(), note, out bool sentSms, out string smsStatusText);
                    txtRejectRequest.Text = "";
                    Session["RefMobile"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeRadWindow1();refresgGrid();", true);
                }
                else
                {
                    RadWindowManager1.RadAlert("کاربر گرامی علت رد درخواست ذکر گردد.", 400, 100, "پیام سیستم", null);
                }
            }
        }

        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            BindGrid(int.Parse(drpRefereeWageStatus.SelectedValue), drpTerms.SelectedValue);
        }
    }
}