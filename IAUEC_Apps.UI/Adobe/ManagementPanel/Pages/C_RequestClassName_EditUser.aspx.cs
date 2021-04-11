using IAUEC_Apps.Business.Adobe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.Pages
{
    public partial class C_RequestClassName_EditUser : System.Web.UI.Page
    {
        ManagementPanelBusiness MPB = new ManagementPanelBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DataTable DTCustomer = MPB.Get_Customers_ClassNameByName("", -2, 3);

                // لیست کلاس های درسی
                for (int i = 0; i < DTCustomer.Rows.Count; i++)
                {
                    if(int.Parse(DTCustomer.Rows[i]["ScoId"].ToString())>0)
                    { 
                        RadComboBoxItem cmbitem = new RadComboBoxItem();
                        cmbitem.Text = DTCustomer.Rows[i]["Name"].ToString();
                        cmbitem.Value = DTCustomer.Rows[i]["Id"].ToString();
                        cmbitem.Font.Name = "Tahoma";
                        cmbitem.Font.Size = 8;
                        ddl_ClassName.Items.Add(cmbitem);
                    }
                }

                // انتخاب کاربران برای اضافه کردن به کلاس
                RadComboBoxItem cmbitem2 = new RadComboBoxItem();
                cmbitem2.Text = "جستجوی کاربر";
                cmbitem2.Value = "0";
                cmbitem2.Font.Name = "Tahoma";
                cmbitem2.Font.Size = 8;
                ddl_StatusUser.Items.Add(cmbitem2);

                RadComboBoxItem cmbitem3 = new RadComboBoxItem();
                cmbitem3.Text = "ایجاد کاربر جدید";
                cmbitem3.Value = "1";
                cmbitem3.Font.Name = "Tahoma";
                cmbitem3.Font.Size = 8;
                ddl_StatusUser.Items.Add(cmbitem3);


                  

            }

        }
        


        protected void ddl_ClassName_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataTable DTClassName = MPB.Get_Customers_ClassName_ById(int.Parse(ddl_ClassName.SelectedValue));
            ChangeHtmlDataOne(DTClassName);


        }


        public void ChangeHtmlDataOne(DataTable DT)
        {
            lbl_ClassName2.Text = DT.Rows[0]["Name"].ToString();
            lbl_UserCount2.Text = DT.Rows[0]["UserCount"].ToString();
            // دریافت کاربران یک کلاس
            DataTable DTCustomerMeeting = MPB.Get_Customers_Users_InCustomerClass_ByClassId2
                (int.Parse(DT.Rows[0]["Id"].ToString()));
            int CountUser=DTCustomerMeeting.Rows.Count;

            // مجموع کاربرانی که یک کلاس می تواند داشته باشد
            int UserCanADD= (int.Parse(DT.Rows[0]["UserCount"].ToString())- CountUser);
            lbl_UserCountInUse2.Text = CountUser.ToString();            
            lbl_UserCountFree2.Text = UserCanADD.ToString();

            if (UserCanADD < 1)
                AddUser.Visible = false;
            //else
            //    AddUser.Visible = true;

            RadGrid_UserList.DataSource = DTCustomerMeeting;
            RadGrid_UserList.DataBind();


        }

        protected void RadGrid_UserList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "RemoveUser")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Session["ClassId"] = commandArgs[0];
                Session["Id"] = commandArgs[1];

                string DomainAddress = "kadobe.iauec.ac.ir";
                string DomainLogin = "h@razavi.com";
                string DomainPassword = "P@ssw0rd";
                Cookie DomainCookies = MPB.Adobe_Login(DomainAddress, DomainLogin, DomainPassword);
                string DomainCookiesValue = DomainCookies.Value;

                // دریافت لیست جلسات یک کلاس درسی
                DataTable DTMeeting = MPB.Get_Customers_Meeting_ByClassId(int.Parse(Session["ClassId"].ToString()));

                // ==================  ADOBE حذف کاربر از کلاس در 
                // کاربری که قرار است پاک شود Id
                string PrincipalID = MPB.Adobe_Get_PRINCIPALS_ByLOGIN("user" 
                    + Session["Id"].ToString()).Rows[0]["PRINCIPAL_ID"].ToString();

                string MeetingID = "";
                for (int i = 0; i < DTMeeting.Rows.Count; i++)
                {
                    MeetingID = MPB.Adobe_Get_SP_Get_ScosByName("Meeting" 
                        + DTMeeting.Rows[i]["Id"].ToString()).Rows[0]["SCO_ID"].ToString();

                    MPB.Adobe_Remove_UserOfMeeting(DomainAddress, PrincipalID, DomainCookiesValue, MeetingID);

                    // غیرفعال شدن کاربر در کلاس
                    MPB.Update_Customers_UserMeeting_ById(long.Parse(DTMeeting.Rows[i]["Id"].ToString()), -1
                        , long.Parse(Session["Id"].ToString()));
                }
                //===========  END
                

                  
                //بروز رسانی صفحه
                DataTable DT = MPB.Get_Customers_ClassName_ById(int.Parse(Session["ClassId"].ToString()));               
                ChangeHtmlDataOne(DT);
            }


        }

        protected void RadGrid_UserList_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rbtn_NewUser_Click(object sender, EventArgs e)
        {
            string ClassNameId = ddl_ClassName.SelectedValue.ToString();

            // جستجوی کاربر جدید
            if(ddl_StatusUser.SelectedValue=="0")     
                Response.Redirect("C_RequestClassName_FindUser.aspx?ClassNameId="+ ClassNameId);            
            else  // کاربر جدید      
                Response.Redirect("C_RequestClassName_NewUser.aspx?ClassNameId=" + ClassNameId);
            




        }





    }
}