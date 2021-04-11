using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.ResourceControlClasses;
using Telerik.Web.UI;
using System.IO;
using System.Text;
using System.Data;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class RefereeTeachersPaymentTransportation : System.Web.UI.Page
    {
        
        private CommonBusiness cmb = null;
        private RequestHandler _requestHandler = new RequestHandler();
        private const string wage = "500000";
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
            if (refereeWageStatus == 0)//لیست داوران که هزینه ایاب وذهاب دریافت نکردن
            {
                DataTable dt = _requestHandler.GetRefereeTeachersPaymentHasNotDownTransportation(term);
                grdRefereePayment.DataSource = dt;
                grdRefereePayment.DataBind();
            }
            else if (refereeWageStatus == 1) //لیست داوران که هزینه ایاب وذهاب دریافت کردن   
            {
                DataTable dt = _requestHandler.GetRefereeTeachersPaymentHasDownTransportation(term);
                grdRefereePayment.DataSource = dt;
                grdRefereePayment.DataBind();

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


        protected void grdRefereePayment_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (drpRefereeWageStatus.SelectedValue == "0")
            {
                grdRefereePayment.DataSource = _requestHandler.GetRefereeTeachersPaymentHasNotDownTransportation(drpTerms.SelectedValue);
            }
            else if (drpRefereeWageStatus.SelectedValue == "1")
            {
                grdRefereePayment.DataSource = _requestHandler.GetRefereeTeachersPaymentHasDownTransportation(drpTerms.SelectedValue);
            }
            else
            {
                grdRefereePayment.DataSource = string.Empty;
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
                var requestDate = commandArgs[0] ?? "";
                var id_os = commandArgs[1] ?? "";
                var refereeMobile = commandArgs[2] ?? "";



                var msgPayment = "";
                //################################################
                if (e.CommandName == "PaymentHasDone")
                {
                    DataTable dtExits = _requestHandler.GetRefereeTeachersPaymentTransportation(int.Parse(id_os), requestDate);
                  if(dtExits!=null&&dtExits.Rows.Count>0)
                        res=_requestHandler.UpdateRefereeWageTransportationPayment(int.Parse(id_os), requestDate
                                                     , wage, "درخواست انجام شد", drpTerms.SelectedValue, 1);
                  else
                    res = _requestHandler.EnterRefereeWageTransportationPayment(int.Parse(id_os), requestDate
                                              , wage, "درخواست انجام شد", drpTerms.SelectedValue,1);
                       cmb.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 246, string.Format("{0}", "پرداخت مبلغ ایاب وذهاب به استاد"), int.Parse(id_os));

                    if (!string.IsNullOrEmpty(refereeMobile))
                    {
                        msgPayment += ",استاد محترم داور";
                        msgPayment += "\r\n";
                        msgPayment += " هزينه اياب و ذهاب به محل واحد الكترونيكي بابت داوري از پايان نامه (ها) ";
                        msgPayment += " برای تاريخ ";
                        msgPayment += requestDate;
                        msgPayment += " به مبلغ ";
                        msgPayment += wage;
                        msgPayment += " ریال به شماره حساب سركارعالي/جنابعالي واريز گرديد ";
                        msgPayment += "\r\n";
                        msgPayment += "معاونت پژوهشي واحد الكترونيكي دانشگاه آزاد اسلامي";

                        cmb.sendSMS(refereeMobile, msgPayment, out bool sentSms, out string smsStatusText);
                    }


                    BindGrid(Convert.ToInt32(drpRefereeWageStatus.SelectedValue), drpTerms?.SelectedValue);
                }
                //################################################
                else if (e.CommandName == "PaymentNotDone")
                {


                    res = _requestHandler.DeleteRefereeWageTransportationPayment(int.Parse(id_os), requestDate);
               cmb.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 247, string.Format("{0}", "اصلاحی عدم پرداخت مبلغ ایاب وذهاب به استاد"), int.Parse(id_os));

                    BindGrid(Convert.ToInt32(drpRefereeWageStatus.SelectedValue), drpTerms?.SelectedValue?.ToString());

                }
                else if (e.CommandName == "RejectDefenseRequest")
                {
                    DataTable dtExits = _requestHandler.GetRefereeTeachersPaymentTransportation(int.Parse(id_os), requestDate);
                    if (dtExits != null && dtExits.Rows.Count > 0)
                      _requestHandler.UpdateRefereeWageTransportationPayment(int.Parse(id_os), requestDate
                                    , wage, "درخواست رد شد", drpTerms.SelectedValue, 2);
                    else
                     _requestHandler.EnterRefereeWageTransportationPayment(int.Parse(id_os), requestDate
                                              , wage, "درخواست رد شد", drpTerms.SelectedValue, 2);

                    if (!string.IsNullOrEmpty(refereeMobile))
                    {
                        msgPayment += ",استاد محترم داور";
                        msgPayment += "\r\n";
                        msgPayment += " امكان پرداخت هزينه اياب و ذهاب داوري پايان نامه (ها) ";
                        msgPayment += " برای تاريخ ";
                        msgPayment += requestDate;
                        msgPayment += " بدليل نقص در حكم كارگزيني يا شماره حساب سركارعالي/جنابعالي ميسر نگرديد.  ";
                        msgPayment += " جهت رفع نقص و انجام اصلاحات لازم، در اسرع وقت به سامانه خدمات الكترونيكي واحد الكترونيكي به آدرس";
                        msgPayment += "\r\n";
                        msgPayment += " service.iauec.ac.ir ";
                        msgPayment += "\r\n";
                        msgPayment += " مراجعه فرماييد ";
                        msgPayment += "\r\n";
                        msgPayment += " معاونت پژوهشي واحد الكترونيكي دانشگاه آزاد اسلامي ";

                        cmb.sendSMS(refereeMobile, msgPayment, out bool sentSms, out string smsStatusText);
                    }
                  cmb.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 248, string.Format("{0}", "عدم پرداخت ایاب وذهاب استاد به دلیل نقص مرتبه"), int.Parse(id_os));

                    //send sms to theacher in modal box
                    string scrp = "function f(){showLightBox(); $find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
                }

                //################################################

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


                switch (drpRefereeWageStatus.SelectedValue)
                {
                    case "0"://داورنی که حق الزحمه دریافت نکرده اند
                        btnPaymentHasDone.Visible = true;
                        btnRejectDefenseRequest.Visible = true;


                        break;

                    case "1"://داورنی که حق الزحمه دریافت  کرده اند
                        btnPaymentNotDone.Visible = true;
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
            var dt = _requestHandler.GetRefereeTeachersPaymentTransportation_Report(int.Parse(drpRefereeWageStatus.SelectedValue.ToString()), 0, drpTerms.SelectedValue);


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
        
               var dt = _requestHandler.GetRefereeTeachersPaymentTransportation_Report(int.Parse(drpRefereeWageStatus.SelectedValue.ToString()), 1, drpTerms.SelectedValue);

            StringBuilder strBuilder = new StringBuilder();
            //string[] columnNames = dt.Columns.Cast<DataColumn>().Where(w => !w.ColumnName.ToString().Contains("ChkPaymentDavar")).Select(x => x.ColumnName).ToArray();
            string[] columnNames = new[] { "نام استاد داور", "تاریخ", "شماره حساب داور", "مبلغ پرداختی" };
            var concat_all_columnnames = string.Join(",", columnNames);
            strBuilder.AppendFormat("{0}", concat_all_columnnames);
            strBuilder.AppendFormat("{0}", "\r\n\r\n");

            var fdavar = "";
            var date = "";
            var siba = "";
            var pay = "";


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                fdavar = string.IsNullOrEmpty(dt.Rows[i]["fullNameDavar"]?.ToString().Trim()) ? "0" : dt.Rows[i]["fullNameDavar"]?.ToString().Trim();
                date = string.IsNullOrEmpty(dt.Rows[i]["RequestDate"]?.ToString().Trim()) ? "0" : dt.Rows[i]["RequestDate"]?.ToString().Trim();
                siba = string.IsNullOrEmpty(dt.Rows[i]["SibaOstadDavar"].ToString().Trim()) ? "0" : dt.Rows[i]["SibaOstadDavar"].ToString().Trim();
                pay = string.IsNullOrEmpty(dt.Rows[i]["Wage"]?.ToString().Trim()) ? "0" : dt.Rows[i]["Wage"]?.ToString().Trim();

                strBuilder.AppendFormat("{0},{3},{2},{1}", fdavar, date, siba, pay);
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
          //  if (!string.IsNullOrEmpty(refereeMobile))
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