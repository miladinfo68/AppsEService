using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.GraduateAffair;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.GraduateAffair.CMS
{
    public partial class SearchStudentByParams : System.Web.UI.Page
    {
        UniversityBusiness UniBusiness = new UniversityBusiness();
        GraduateReportBusiness GraduateBusiness = new GraduateReportBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["stcode"] = null;
        }

       
        protected void grd_Student_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = UniBusiness.GetTop50Student();
            grd_Student.DataSource = dt;
           
        }

        protected void grd_Student_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Session["stcode"] = null;
            if (e.CommandName == "Select")
            {
                foreach (GridDataItem item in grd_Student.SelectedItems)
                {
                    Session["stcode"] = item.GetDataKeyValue("stcode").ToString();
                    if(Session["page"].ToString()=="1")
                    Response.Redirect("SodureTaeidieTahsili.aspx");
                    if (Session["page"].ToString() == "2")
                        Response.Redirect("ListePishneviseGovahinamemovaghat2.aspx");
                    if (Session["page"].ToString() == "3")
                        Response.Redirect("FormeTasvieHesab.aspx");
                    if (Session["page"].ToString() == "4")
                        Response.Redirect("ListePishneviseGovahinamemovaghat.aspx");
                    if (Session["page"].ToString() == "5")
                          Response.Redirect("DaneshnamehTaMaghtaKarshenasi.aspx");
                    if (Session["page"].ToString() == "6")
                        Response.Redirect("DaneshnamehMosavab.aspx");
                    if (Session["page"].ToString() == "7")
                        Response.Redirect("DaneshnamehKardaniOmumi76Ghabl.aspx");
                    if (Session["page"].ToString() == "8")
                        Response.Redirect("DaneshnamehKardaniDarReshteh76Ghabl.aspx");
                    if (Session["page"].ToString() == "9")
                        Response.Redirect("DaneshnamehKardaniDarReshteh76BeBaad.aspx");
                    if (Session["page"].ToString() == "10")
                        Response.Redirect("DaneshnamehArshadBeBala.aspx");
                }
            }
        }
    }
}