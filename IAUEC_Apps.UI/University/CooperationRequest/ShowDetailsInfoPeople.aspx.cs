using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class ShowDetailsInfoPeople : System.Web.UI.Page
    {
       
          

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btn_TaeedParvande_Click(object sender, EventArgs e)
        {
            confirmMessage.Text = "آیا از تایید و یا نقص مدارک مطمئن هستید";
            string script = "function f(){radopen(null, 'RadWindow1'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {

        }

        
    }
}