using System;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;


namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class ConfirmStudentCardNotPrinted : System.Web.UI.Page
    {
        CommonBusiness CB = new CommonBusiness();
        /// <summary>
        ///ایجاد نموده ایم Request_StudentCartDAO یک شئ از کلاس
        /// </summary>
     
        /// <summary>
        ///ایجاد نموده ایم RequestStudentCartBusiness یک شئ از کلاس
        /// </summary>
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();
        //CommonDAO dao = new CommonDAO();
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void grd_CartRequest_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dt = new DataTable();
            dt = CartBusiness.GetCartRequest(1);
            grd_CardRequest.DataSource = dt;

        }



        protected void grd_CartRequest_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();

            if (e.CommandName == "printAddress")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Session["stcode"] = commandArgs[0];
                Session["RequestTypeID"] = commandArgs[1];
                Session["StudentRequestID"] = commandArgs[2];

                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 47, "", Convert.ToInt32(Session["StudentRequestID"]));
                this.StiWebViewer1.ResetReport();
                DataTable dt = new DataTable();
                dt = CartBusiness.GetCartRequest(1);
                grd_CardRequest.DataSource = dt;
                grd_CardRequest.DataBind();
                DataTable dt1 = new DataTable();
                dt1 = CartBusiness.Get_StudentAddress(Session["stcode"].ToString(), int.Parse(Session["RequestTypeID"].ToString()));
                StiWebViewer1.Visible = true;
                StiReport rpt = new StiReport();
                rpt.Load(Server.MapPath("../Reports/Packet1.mrt"));
                rpt.Dictionary.Databases.Clear();
                rpt.Dictionary.Databases.Add(new StiSqlDatabase("SuppConnection", CB.ReportConnection.ToString()));
                rpt.Compile();
                rpt.CompiledReport.DataSources["[Request].[SP_Get_StudentAddress]"].Parameters["@stcode"].ParameterValue = Session["stcode"].ToString();
                rpt.RegData(dt1);
                StiWebViewer1.Report = rpt;
                StiWebViewer1.Visible = true;
            }

        }

        protected void grd_CardRequest_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {


                GridDataItem item = (GridDataItem)e.Item;
                GridDataItem dataItem = e.Item as GridDataItem;
                TableCell controlcell = (TableCell)item["EditCommandColumn"];
                Button btn_Print = e.Item.FindControl("btn_Print") as Button;
                HiddenField hd_Field = e.Item.FindControl("hd_Field") as HiddenField;
                TableCell cell = dataItem["stcode"];
                btn_Print.CommandArgument = cell.Text + "," + "1" + "," + hd_Field.Value.ToString();
            }
        }
    }
}