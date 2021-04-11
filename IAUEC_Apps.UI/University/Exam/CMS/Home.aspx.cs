using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleId"] != null)
            {
                switch (Session["RoleId"].ToString())
                {

                    case "2":
                        LNK_Chair.Visible = false;
                        LNK_CityExam.Visible = false;
                        LNK_ClassRange.Visible = false;
                        LNK_Report.Visible = false;
                        break;
                    case "3":
                        LNK_Examplace.Visible = false;
                        LNK_ChairList.Visible = false;
                        LNK_Present.Visible = false;
                        LNK_ClassRange.Visible = false;
                        LNK_Report.Visible = false;
                        break;
                }
            }
            else
                Response.Redirect("Login.aspx");
        }
    }
}