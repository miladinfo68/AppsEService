using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class gameCenter : System.Web.UI.Page
    {
        string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (getPlayerCode()=="0")
                Response.Redirect("intropage.aspx");
            getData();
        }

        protected void btnLink_Click(object sender, EventArgs e)
        {
            
            url = "playGame.aspx?g=" + ((ImageButton)sender).CommandArgument;
            setLog(Convert.ToInt32(((ImageButton)sender).CommandArgument));
            Response.Redirect(url);
        }

        private void getData()
        {
            gameBusiness gb = new gameBusiness();
            System.Data.DataTable dt = gb.getActiveGames();

            dlGames.DataSource = dt;
            dlGames.DataBind();
        }
        private string getPlayerCode()
        {
            if (Session[sessionNames.userID_Karbar] != null)
                return Session[sessionNames.userID_Karbar].ToString();
            if (Session[sessionNames.userID_StudentOstad] != null)
                return Session[sessionNames.userID_StudentOstad].ToString();
            return "0";

        }
        private int getPlayerType()
        {
            if (Session[sessionNames.userID_Karbar] != null)
                return 3;
            if(Session[sessionNames.userID_StudentOstad] != null)
            {
                if (Session["UserType_lms"].ToString() == "2")
                    return 2;
                if (Session["UserType_lms"].ToString() == "3")
                    return 1;
            }
            return 0;
        }


        private void setLog(int gameID)
        {
            gameBusiness gb = new gameBusiness();
            gb.setGameLog(gameID, getPlayerCode(), getPlayerType());
        }
    }
}