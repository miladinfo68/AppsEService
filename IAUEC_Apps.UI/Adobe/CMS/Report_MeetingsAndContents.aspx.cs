using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class Report_MeetingsAndContents : System.Web.UI.Page
    {
        CommonBusiness cmb = new CommonBusiness();
        RecordsBusiness rb = new RecordsBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rcbTerm.DataTextField = "Term";
                rcbTerm.DataValueField = "Term";
                rcbTerm.DataSource = cmb.getActiveTerm_AdobeConnection();
                rcbTerm.DataBind();
                rcbTerm.Items.Insert(0, new RadComboBoxItem("انتخاب نمایید"));
                string mId = Request.QueryString["id"].ToString();
                string[] id = mId.ToString().Split(new char[] { '@' });
                string menuId = "";
                for (int i = 0; i < id[1].Length; i++)
                {
                    string s = id[1].Substring(i + 1, 1);
                    if (s != "-")
                        menuId += s;
                    else
                        break;
                }
                AccessControl1.MenuId = menuId;
                Session[sessionNames.menuID] = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
           
        }
        protected void grd_Meetings_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (txt_Date.Text != "" && txt_Code.Text != "")
            {

                grd_Meetings.DataSource = rb.LinkOfMeetingsAndContentsByCodeClassAndTime(txt_Code.Text, txt_Date.Text, rcbTerm.SelectedValue);
                
            }
            else if (txt_Date.Text != "")
            {

                grd_Meetings.DataSource = rb.LinkOfMeetingsAndContentsByCodeClassAndTime("0", txt_Date.Text, rcbTerm.SelectedValue);
              
            }
            else if (txt_Code.Text != "")
            {

                grd_Meetings.DataSource = rb.LinkOfMeetingsAndContentsByCodeClassAndTime(txt_Code.Text, "nd", rcbTerm.SelectedValue);
              
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            if (Page.IsValid )
            {

             
            if (txt_Date.Text != "" && txt_Code.Text != "")
            {

                grd_Meetings.DataSource = rb.LinkOfMeetingsAndContentsByCodeClassAndTime(txt_Code.Text, txt_Date.Text, rcbTerm.SelectedValue);
                grd_Meetings.Rebind();
            }
            else if (txt_Date.Text != "")
            {

                grd_Meetings.DataSource = rb.LinkOfMeetingsAndContentsByCodeClassAndTime("0", txt_Date.Text, rcbTerm.SelectedValue);
                grd_Meetings.Rebind();
            }
         //   else if (txt_Code.Text =="")
         //   {
         //       string script = "alert(\"کد میز گرد را وارد نمایید!\");";
         //     //  var msg = "کد میز گرد را وارد نمایید";
         ////   ScriptManager.RegisterClientScriptInclude(this, GetType(), ClientID,script, true);

         //       ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, script, true);


         //       //     grd_Meetings.DataSource = rb.LinkOfMeetingsAndContentsByCodeClassAndTime(0, "nd", rcbTerm.SelectedValue);
         //       //   grd_Meetings.Rebind();
         //   }
            else
            {

                grd_Meetings.DataSource = rb.LinkOfMeetingsAndContentsByCodeClassAndTime(txt_Code.Text, "nd", rcbTerm.SelectedValue);
                 grd_Meetings.Rebind();
            }

         }
        }
        
    }
}