using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Adobe;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class GMPresentReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CommonBusiness cb = new CommonBusiness();
            ProfPresentBusiness pb = new ProfPresentBusiness();
            if (!IsPostBack)
            {
                // تمام نیمسال های قابل قبول را بر می گرداند
                
                System.Data.DataTable dt = cb.getActiveTerm_AdobeConnection();
                

                ddl_Nimsal.DataSource = removeSummerTerms(dt);
                ddl_Nimsal.DataValueField = "Term";
                ddl_Nimsal.DataTextField = "Term";
                ddl_Nimsal.DataBind();
                ddl_Nimsal.SelectedIndex = dt.Rows.Count - 1;

                ddl_MG.DataSource = pb.GetMGUsers(ddl_Nimsal.SelectedItem.Value);
                ddl_MG.DataValueField = "GroupManagerUser";
                ddl_MG.DataTextField = "GroupManagerName";
                ddl_MG.DataBind();

                manageAccessControl();
               
            }
        }

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
                AccessControl1.MenuId = menuId;
                Session[sessionNames.menuID] = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
            else
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
        }


        protected void btnshow_Click(object sender, EventArgs e)
        {
            setGridGroupManagerReport();
            grd_GMPresentReports.DataBind();

        }

        protected void ConvertExcel_Click(object sender, ImageClickEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            if (grd_GMPresentReports.MasterTableView.Items.Count > 0)
            {
                grd_GMPresentReports.ExportSettings.FileName = string.Format("GMPresentTime_{0}_{1}", "GroupManager", DateTime.Now);
                grd_GMPresentReports.ExportSettings.IgnorePaging = true;
                grd_GMPresentReports.ExportSettings.OpenInNewWindow = true;
                grd_GMPresentReports.ExportSettings.ExportOnlyData = true;
                grd_GMPresentReports.MasterTableView.UseAllDataFields = true;
                grd_GMPresentReports.MasterTableView.ExportToExcel();
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 19, "حضور غیاب مدیر گروه");

            }
        }

        protected void grd_GMPresentReports_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string Nimsal = ddl_Nimsal.Items[0].Value;
            string user = "";
            if (ddl_Nimsal.SelectedValue != "null")
            {
                Nimsal = ddl_Nimsal.SelectedValue;
            }
            if (ddl_MG.SelectedValue != "")
            {
                user = ddl_MG.SelectedValue;
            }

            ProfPresentBusiness pb = new ProfPresentBusiness();
            System.Data.DataTable dt = pb.GetMGTimeByUserName_Term(user, Nimsal);
            if (dt.Columns.Contains("error"))
            {
                //Response.Write(dt.Rows[0]["error"].ToString());
            }
            else
            {
                grd_GMPresentReports.DataSource = dt;
            }

        }

        protected void ddl_Nimsal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProfPresentBusiness pb = new ProfPresentBusiness();
            ddl_MG.DataSource = pb.GetMGUsers(ddl_Nimsal.SelectedValue);
            ddl_MG.DataValueField = "GroupManagerUser";
            ddl_MG.DataTextField = "GroupManagerName";
            ddl_MG.DataBind();
        }

        private  System.Data.DataTable removeSummerTerms(System.Data.DataTable dtTerms)
        {
            for (int i = dtTerms.Rows.Count-1; i >= 0; i--)
                if (dtTerms.Rows[i]["term"].ToString().EndsWith("3"))
                    dtTerms.Rows.RemoveAt(i);
            dtTerms.AcceptChanges();
            return dtTerms;
        }

        private void setGridGroupManagerReport()
        {
            ProfPresentBusiness pb = new ProfPresentBusiness();
            try
            {
                System.Data.DataTable dt = pb.GetMGTimeByUserName_Term(ddl_MG.SelectedValue, ddl_Nimsal.SelectedValue);
                if (dt.Columns.Contains("error"))
                {
                    //Response.Write(dt.Rows[0]["error"].ToString());
                }
                else
                {
                    grd_GMPresentReports.DataSource = dt;
                }
            }
            catch (Exception exx)
            {
                Response.Write(exx.Message);

            }
        }
    }
}