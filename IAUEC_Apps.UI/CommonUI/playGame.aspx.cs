using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class playGame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["g"] != "")
            {
                try
                {
                    int gameId = Convert.ToInt32(Request.QueryString["g"]);
                    getGame(gameId);
                }
                catch(Exception ex)
                {

                }
            }
            

        }
        private void getGame(int gameID)
        {
            Business.Common.gameBusiness gb = new Business.Common.gameBusiness();
            string gameLink=gb.getGameLink(gameID);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), gameID.ToString(), "setSWFfile('" + gameLink + "')", true);
        }
    }
}