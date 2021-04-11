using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ResourceControl.BLL;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class UniPaymentReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                CheckOutRequestBusiness reqBusiness = new CheckOutRequestBusiness();
                List<MahaleSodoor> dtVahed = reqBusiness.GetListOfVahed();
                dtVahed.Add(new MahaleSodoor { Id = 0, Vahed = "همه موارد" });
                drpUniversity.DataSource = dtVahed.OrderBy(x => x.Id);
                drpUniversity.DataTextField = "vahed";
                drpUniversity.DataValueField = "id";
                drpUniversity.DataBind();
                btnApprove.Visible = false;

            }
            if (grdResualtList.Visible)
            {
                btnApprove.Visible = true;

            }
            else
            {
                btnApprove.Visible = false;

            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            btnApprove.Visible = drpReportType.SelectedValue == "1";
            grdResualtList.Visible = true;
            grdResualtList.DataSource = null;
            btnApprove.Visible = true;
            grdResualtList.Rebind();
        }
        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            StartDateTxt.Text = "";
            EndDateTxt.Text = "";
            drpUniversity.SelectedValue = 0.ToString();
            grdResualtList.Visible = false;

        }

        protected void ExcleExportBtn_Click(object sender, EventArgs e)
        {
            var allAcceptedRequest = GetData();
            if (allAcceptedRequest.Count() <= 0) return;
            try
            {

                var pck = new OfficeOpenXml.ExcelPackage();
                var ws = pck.Workbook.Worksheets.Add("Studentlist");
                List<allAcceptedRequest> listForExcell = new List<allAcceptedRequest>();

                foreach (var item in allAcceptedRequest)
                {
                    listForExcell.Add(new DTO.University.Request.allAcceptedRequest
                    {
                        StudentRequestID = item.StudentRequestID,
                        CreateDate = item.CreateDate,
                        StCode = item.StCode,
                        name = item.name,
                        DateVoroodRizNomre = item.DateVoroodRizNomre,
                        DateSodoorRizNomre = item.DateSodoorRizNomre,
                        DateVoroodDaneshname = item.DateVoroodDaneshname,
                        DateSodoorDaneshname = item.DateSodoorDaneshname,
                        DateVoroodGovahi = item.DateVoroodGovahi,
                        DateSodoorGovahi = item.DateSodoorGovahi
                    });
                }


                ws.Cells["A1"].LoadFromCollection<allAcceptedRequest>(listForExcell, true);
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=ListOfStudents.xlsx");

                ws.Cells["A1"].Value = "شماره درخواست";
                ws.Cells["B1"].Value = "تاریخ ثبت درخواست تسویه";
                ws.Cells["C1"].Value = "شماره دانشجویی";
                ws.Cells["D1"].Value = "نام دانشجو";

                ws.Cells["E1"].Value = "تاریخ ورود ریز نمره";
                ws.Cells["F1"].Value = "تاریخ صدور ریز نمره";
                ws.Cells["G1"].Value = "تاریخ ورود دانشنامه";
                ws.Cells["H1"].Value = "تاریخ صدور دانشنامه";
                ws.Cells["I1"].Value = "تاریخ ورود گواهی موقت";
                ws.Cells["J1"].Value = "تاریخ صدور گواهی موقت";

                Response.BinaryWrite(pck.GetAsByteArray());

            }
            catch (Exception ex)
            {
                //log error
            }
            Response.End();
        }

        protected void btnShowDetail_Click(object sender, EventArgs e)
        {

        }

        protected void btnHistory_Click(object sender, EventArgs e)
        {

        }


        protected void grdResualtList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var data = GetData();
            grdResualtList.DataSource = data;
            ViewState["studentRequestList"] = data.Select(s => Convert.ToInt32(s.StudentRequestID)).ToList();
            GridFilterMenu menu = grdResualtList.FilterMenu;
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
            //var query = from c in listByVahed
            //            join o in fareghNull
            //               on c.studentRequestId equals o.CustomerID into sr
            //            from x in sr.DefaultIfEmpty()
            //            select new
            //            {
            //                CustomerID = c.CustomerID,
            //                ContactName = c.ContactName,
            //                OrderID = x.OrderID == null ? -1 : x.OrderID
            //            };
            //grdResualtList.DataSource = dt;
        }


        private IEnumerable<ListReqVahed> GetData()
        {
            CheckOutRequestBusiness reqBusiness = new CheckOutRequestBusiness();
            var idVahed = drpUniversity.SelectedItem.Value;
            var startDate = StartDateTxt.Text;
            var endDate = EndDateTxt.Text;
            var MadrakType = 2;
            var listByVahed = reqBusiness.GetListOFRequestByVahedAndDate(Convert.ToInt32(idVahed), startDate, endDate, Convert.ToInt32(MadrakType));
            var vahedList = RequestHandler.ConvertDataTableToList<ListReqVahed>(listByVahed);
            switch (drpReportType.SelectedItem.Value)
            {
                case "1":
                    return vahedList.Where(w => w.DateApprovePay == null && w.DateSendToPay == null);

                case "2":
                    return vahedList.Where(w => w.DateSendToPay != null);

                case "3":
                    return vahedList;

            }
            //if (drpReportType.SelectedItem.Value == "1")
            //return vahedList.Where(w => w.DateApprovePay == null && w.DateSendToPay == null);
            //else
            return vahedList.Where(w => w.DateSendToPay != null);

        }

        protected void Unnamed_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (sender as CheckBox);
            var resualt = checkBox.Checked;
            var requestStudentId = Convert.ToInt32(checkBox.Text);
            var _studentRequestList = ViewState["studentRequestList"] as List<int>;
            if (_studentRequestList == null)
                _studentRequestList = new List<int>();
            if (resualt)
            {
                //TODO  if Exists in tbl just updatetd else added
                if (!_studentRequestList.Exists(x => x == requestStudentId))
                    _studentRequestList.Add(requestStudentId);
            }
            else
            {
                //TODO  if Exists in tbl deleted
                if (_studentRequestList.Exists(x => x == requestStudentId))
                    _studentRequestList.Remove(requestStudentId);
            }
            ViewState["studentRequestList"] = _studentRequestList;
        }



        protected void btnApprove_Click1(object sender, EventArgs e)
        {
            if (drpReportType.SelectedItem.Value == "1")
            {
                string scrp = "function f(){$find(\"" + rdwPrint.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            CommonBusiness CommonBusiness = new CommonBusiness();
            CheckOutRequestBusiness reqBusiness = new CheckOutRequestBusiness();
            var userID = Session[sessionNames.userID_Karbar].ToString();
            var reqList = (List<int>)ViewState["studentRequestList"];
            if (reqList != null && reqList.Count > 0)
                reqBusiness.SetSendToPay(reqList, Convert.ToInt32(userID), CommonBusiness.GetIPAddress());
            foreach (int i in reqList)
            {
                CommonBusiness.InsertIntoUserLog(Convert.ToInt32(Session[sessionNames.userID_Karbar]), DateTime.Now.ToString("HH:mm"), 12, (int)DTO.eventEnum.ارسال_جهت_پرداخت_به_واحد_صادرکننده, "", i);
            }
            grdResualtList.Rebind();
            ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseModal();", true);


        }
    }
}