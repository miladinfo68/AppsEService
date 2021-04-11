using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;

namespace IAUEC_Apps.UI.EmailReg.Pages
{
    public partial class EmailRequestStatus : System.Web.UI.Page
    {
        StudentBuisiness EmailBuss = new StudentBuisiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grd_ListAfterStdRequest_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = EmailBuss.GetEmailRequestStatus(Session[sessionNames.userID_StudentOstad].ToString());
            grd_EmailStatus.DataSource = dt;
          
           
            //if (dt.Rows.Count>0 && dt.Rows[0]["Status"].ToString() == "2" )
            //{
            //    grd_EmailStatus.Columns[3].Visible = true;
               
            //}


        }

        protected void grd_EmailStatus_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "NewRequest")
                Response.Redirect("CreateEmail.aspx");
        }
    }
}