using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI
{
    public partial class ElmahLogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnLogin_OnServerClick(object sender, EventArgs e)
        {
            if (FormsAuthentication.Authenticate(UserName.Value, PassWord.Value))
            {
                FormsAuthentication.SetAuthCookie(UserName.Value, false);
                Response.Redirect("~/elmah.axd");
            }
        }
    }
}