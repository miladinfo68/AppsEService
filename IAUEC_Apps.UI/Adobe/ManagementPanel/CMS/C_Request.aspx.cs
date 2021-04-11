using IAUEC_Apps.Business.Adobe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.CMS
{
    public partial class C_Request : System.Web.UI.Page
    {
        ManagementPanelBusiness MPB = new ManagementPanelBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable DTCustomer = MPB.Get_Customers();
                for (int i = 0; i < DTCustomer.Rows.Count; i++)                
                    ddl_Customers.Items.Add(DTCustomer.Rows[i]["Name"].ToString());               
                             
            }
        }

        protected void btn_ShowRequest_Click(object sender, EventArgs e)
        {
            // ScoId=0 -> هنوز بررسی نشده
            DataTable DTCustomer = MPB.Get_Customers_ClassNameByName(ddl_Customers.SelectedValue,0,-1);

            RadGrid1.DataSource = DTCustomer;
            RadGrid1.DataBind();
        }



    }
}