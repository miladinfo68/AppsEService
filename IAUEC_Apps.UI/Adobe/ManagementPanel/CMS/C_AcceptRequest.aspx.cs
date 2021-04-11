using IAUEC_Apps.Business.Adobe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.CMS
{
    public partial class C_AcceptRequest : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ManagementPanelBusiness MPB = new ManagementPanelBusiness();

            if (!IsPostBack)
            {
                string Id = "";

                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("Id") != null)
                {
                    Id = HttpUtility.ParseQueryString(this.ClientQueryString).Get("Id");
                    MPB.Update_Customers_ClassName_Accept(int.Parse(Id));// قبول درخواست




                    Response.Redirect("C_Request.aspx");
                }
                else
                    Response.Redirect("C_Request.aspx");
            }




        }





    }
}