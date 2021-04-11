using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class FinalApproveMali : System.Web.UI.Page
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
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            grdResualtList.Visible = true;
            grdResualtList.DataSource = null;
            grdResualtList.Rebind();
        }
        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            StartDateTxt.Text = "";
            EndDateTxt.Text = "";
            drpUniversity.SelectedValue = 0.ToString();
            grdResualtList.Visible = false;


            //divHumanities.Visible = false;
            //divTechnicalEngineering.Visible = false;
            //divManagement.Visible = false;
            //divScience.Visible = false;
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
                        DateSodoorGovahi = item.DateSodoorGovahi,
                        Shahriye=item.shahriye,
                        Takhfif=item.takhfif,
                        enteghalBestankar=item.enteghalBestankar,
                        Pay=item.shahriye-item.takhfif-item.enteghalBestankar//item.pay,
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
                ws.Cells["k1"].Value = "شهریه";
                ws.Cells["L1"].Value = "تخفیف";
                ws.Cells["M1"].Value = "انتقال بستانکار";
                ws.Cells["N1"].Value = "قابل پرداخت";

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
            grdResualtList.DataSource = GetData();
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
            //var fareghNull = reqBusiness.GetListOFApproveList();
            //var nullFaregh = RequestHandler.ConvertDataTableToList<ApproveList>(fareghNull)
            //    .Where(w=> w.ApproveDateFaregh != null && w.ApproveDateMali != null)
            //    .Select(s => s.StudentRequestId);
            switch (drpReportType.SelectedItem.Value)
            {
                case "1":
                    return vahedList.Where(w => w.DateApprovePay != null && w.DateSendToPay != null);//nullFaregh.Contains(w.StudentRequestID)

                case "2":
                    return vahedList;


            }
            return vahedList.Where(w => w.DateApprovePay != null && w.DateSendToPay != null);//nullFaregh.Contains(w.StudentRequestID)
        }
    }
}
