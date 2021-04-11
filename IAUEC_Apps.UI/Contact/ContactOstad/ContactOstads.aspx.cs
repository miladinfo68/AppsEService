using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Conatct;

namespace IAUEC_Apps.UI.Contact.ContactOstad
{
    public partial class ConatctOstads : System.Web.UI.Page
    {
        protected  string UserOstad = "200";
        
        protected void Page_Load(object sender, EventArgs e)
        {
         
            UserOstad += Session[sessionNames.userID_StudentOstad].ToString();
            Session["ContactStudent"] = ContactBuisnes.GetContactStudentALL(UserOstad);
            LblIdUser.Text = UserOstad;
           // "20056"
        }

        protected void LinkOstadType_Click(object sender, EventArgs e)
        {
            LinkButton LinkClicked  = (LinkButton)sender;

            //if (LinkClicked.ID == "LBMosh1")
            //{
            //    Response.Redir` ect()
            //}
            //else if (LinkClicked == "Link2")
            //{
            //    //Do something else;
            //}
            ////and so on
        }
    }
}