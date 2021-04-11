using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;


namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class ConfirmRequestStudentCartsUI : System.Web.UI.Page
    {
        /// <summary>
        ///ایجاد نموده ایم CommonBusiness یک شئ از کلاس
        /// </summary>
        CommonBusiness CB = new CommonBusiness();
        /// <summary>
        ///ایجاد نموده ایم Request_StudentCartDAO یک شئ از کلاس
        /// </summary>
        //Request_StudentCartDAO dastcast = new Request_StudentCartDAO();
        /// <summary>
        ///ایجاد نموده ایم RequestStudentCartBusiness یک شئ از کلاس
        /// </summary>
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();
        //CommonDAO dao = new CommonDAO();
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
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
                AccessControl1.MenuId = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
        }
        /// <summary>
        /// محتویات گیریدویو شامل درخواست های ارسال کارت از طریق این متد فراهم می گردد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void grd_CartRequest_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            DataTable dt = new DataTable();
            dt = CartBusiness.GetCartRequest(6);
            if (dt.Rows.Count > 0)
            {
                grd_CardRequest.DataSource = dt;
            }
            else
            {
                grd_CardRequest.Visible = false;
                lbl_Msg.Visible = true;

            }

        }


        /// <summary>
        /// تایید یا رد درخواست ارسال کارت از طریق این متد فراهم می گردد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void grd_CartRequest_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();

            if (e.CommandName == "taeiddarkhast")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Session["stcode"] = commandArgs[0];
                Session["RequestTypeID"] = commandArgs[1];
                Session["StudentRequestID"] = commandArgs[2];
                CartBusiness.UpdateStudentRequestLogID(Session["stcode"].ToString(), 1, 1, int.Parse(Session["StudentRequestID"].ToString()));
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 2, "", Convert.ToInt32(Session["StudentRequestID"]));
                DataTable dt = new DataTable();
                dt = CartBusiness.GetCartRequest(6);

                if (dt.Rows.Count > 0)
                {
                    grd_CardRequest.DataSource = dt;
                    grd_CardRequest.DataBind();
                }
                else
                {
                    grd_CardRequest.Visible = false;
                    lbl_Msg.Visible = true;
                }
                
            }
            if (e.CommandName == "printAddress")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Session["stcode"] = commandArgs[0];
                Session["RequestTypeID"] = commandArgs[1];
                Session["StudentRequestID"] = commandArgs[2];

                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 47, Session["StudentRequestID"].ToString());
                this.StiWebViewer1.ResetReport();
                DataTable dt = new DataTable();
                dt = CartBusiness.GetCartRequest(6);
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
                rpt.CompiledReport.DataSources["[Request].[SP_Get_StudentAddress]"].Parameters["@param"].ParameterValue = int.Parse(Session["RequestTypeID"].ToString());
                rpt.RegData(dt1);
                StiWebViewer1.Report = rpt;
                StiWebViewer1.Visible = true;
            }
        }

    }
}