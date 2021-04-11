using IAUEC_Apps.Business.Adobe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.CMS
{
    public partial class C_Report_RequestStatus : System.Web.UI.Page
    {
        ManagementPanelBusiness MPB = new ManagementPanelBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable DTCustomer = MPB.Get_Customers();
                for (int i = 0; i < DTCustomer.Rows.Count; i++)
                    ddl_Customers.Items.Add(DTCustomer.Rows[i]["Name"].ToString());
                                
                ddl_Status.Items.Add("همه درخواست ها");
                ddl_Status.Items.Add("درحال بررسی");
                ddl_Status.Items.Add("رد شده");
                ddl_Status.Items.Add("پذیرفته شده");                
            }
        }

        protected void btn_ShowRequest_Click(object sender, EventArgs e)
        {
            InfoMeeting.Visible = false;
            InfoUser.Visible = false;
            InfoClassDayTime.Visible = false;

            int Status = 0;
            if (ddl_Status.SelectedValue == "درحال بررسی")
                Status = 0;
            else if (ddl_Status.SelectedValue == "رد شده")
                Status = -1;
            else if (ddl_Status.SelectedValue == "پذیرفته شده")
                Status = 1;
            else
                Status = -2;// همه درخواست ها
                      
            DataTable DTCustomer = MPB.Get_Customers_ClassNameByName(ddl_Customers.SelectedValue, Status, -1);

            DataTable DTSource = new DataTable();
            DTSource.Columns.Add("Id", typeof(string));
            DTSource.Columns.Add("CustomerId", typeof(string));
            DTSource.Columns.Add("Name", typeof(string));
            DTSource.Columns.Add("UserCount", typeof(string));
            DTSource.Columns.Add("DateStart", typeof(string));
            DTSource.Columns.Add("DateEnd", typeof(string));
            DTSource.Columns.Add("MeetingAccess", typeof(string));
            DTSource.Columns.Add("Status", typeof(string));

            for (int i = 0; i < DTCustomer.Rows.Count; i++)
            {
                DataRow row = DTSource.NewRow();
                row["Id"] = DTCustomer.Rows[i]["Id"].ToString();
                row["CustomerId"] = DTCustomer.Rows[i]["CustomerId"].ToString();
                row["Name"] = DTCustomer.Rows[i]["Name"].ToString();
                row["UserCount"] = DTCustomer.Rows[i]["UserCount"].ToString();
                row["DateStart"] = DTCustomer.Rows[i]["DateStart"].ToString();
                row["DateEnd"] = DTCustomer.Rows[i]["DateEnd"].ToString();

                if (DTCustomer.Rows[i]["MeetingAccess"].ToString() == "denied")
                    row["MeetingAccess"] = "فقط اعضاء کلاس";
                else
                    row["MeetingAccess"] = "امکان حضور مهمان";

                if (DTCustomer.Rows[i]["ScoId"].ToString() == "0")
                    row["Status"] = "درحال بررسی";
                else if (DTCustomer.Rows[i]["ScoId"].ToString() == "-1")
                    row["Status"] = "رد شده";
                else
                    row["Status"] = "پذیرفته شده";

                DTSource.Rows.Add(row);
            }

            RadGrid1.DataSource = DTSource;
            RadGrid1.DataBind();   

        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "UserInfo")
            {
                InfoMeeting.Visible = false;
                InfoUser.Visible = true;
                InfoClassDayTime.Visible = false;
                InfoRejectReason.Visible = false;

                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Session["Id"] = commandArgs[0];
                Session["Name"] = commandArgs[1];

                DataTable DT = MPB.Get_Customers_Users_InCustomerClass_ByClassId(int.Parse(Session["Id"].ToString()));
                DataTable DTSource = new DataTable();

                DTSource.Columns.Add("ClassName", typeof(string));
                DTSource.Columns.Add("Name", typeof(string));
                DTSource.Columns.Add("Family", typeof(string));
                DTSource.Columns.Add("UserName", typeof(string));
                //DTSource.Columns.Add("Password", typeof(string));
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DataRow row = DTSource.NewRow();
                    row["ClassName"] = Session["Name"].ToString();
                    row["Name"] = DT.Rows[i]["Name"].ToString();
                    row["Family"] = DT.Rows[i]["Family"].ToString();
                    row["UserName"] = "user" + DT.Rows[i]["Id"].ToString();

                    //string Ncode = DT.Rows[i]["NationalCode"].ToString();
                    //if (Ncode == "" || Ncode.Length < 8)
                    //    row["Password"] = DT.Rows[i]["UserPass"].ToString();
                    //else
                    //    row["Password"] = DT.Rows[i]["NationalCode"].ToString();

                    DTSource.Rows.Add(row);
                }

                RadGrid3.DataSource = DTSource;
                RadGrid3.DataBind();
            }

            if (e.CommandName == "MeetingInfo")
            {
                InfoMeeting.Visible = true;
                InfoUser.Visible = false;
                InfoClassDayTime.Visible = false;
                InfoRejectReason.Visible = false;

                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Session["Id"] = commandArgs[0];
                Session["Name"] = commandArgs[1];

                DataTable DT = MPB.Get_Customers_Meeting_ByClassId(int.Parse(Session["Id"].ToString()));
                DataTable DTSource = new DataTable();
                DTSource.Columns.Add("ClassName", typeof(string));
                DTSource.Columns.Add("MeetingLink", typeof(string));

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DataRow row = DTSource.NewRow();
                    row["ClassName"] = Session["Name"].ToString();
                    //row["MeetingLink"] = "~/kadobe.iauec.ac.ir"+"/Meeting" + DT.Rows[i]["Id"].ToString(); 
                    row["MeetingLink"] = "http://kadobe.iauec.ac.ir/Meeting" + DT.Rows[i]["Id"].ToString();
                    DTSource.Rows.Add(row);
                }

                RadGrid2.DataSource = DTSource;
                RadGrid2.DataBind();

            }
            if (e.CommandName == "ClassDayTimeInfo")
            {
                InfoMeeting.Visible = false;
                InfoUser.Visible = false;
                InfoClassDayTime.Visible = true;
                InfoRejectReason.Visible = false;

                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Session["Id"] = commandArgs[0];
                Session["Name"] = commandArgs[1];

                DataTable DT = MPB.Get_Customers_ClassDayTime_ByClassId(int.Parse(Session["Id"].ToString()));
                DataTable DTSource = new DataTable();
                DTSource.Columns.Add("ClassName", typeof(string));
                DTSource.Columns.Add("DayName", typeof(string));
                DTSource.Columns.Add("BEGIN_HOUR", typeof(string));
                DTSource.Columns.Add("END_HOUR", typeof(string));

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DataRow row = DTSource.NewRow();
                    row["ClassName"] = Session["Name"].ToString();
                    row["DayName"] = DT.Rows[i]["DayName"].ToString();
                    row["BEGIN_HOUR"] = DT.Rows[i]["BEGIN_HOUR"].ToString();
                    row["END_HOUR"] = DT.Rows[i]["END_HOUR"].ToString();
                    DTSource.Rows.Add(row);
                }

                RadGrid4.DataSource = DTSource;
                RadGrid4.DataBind();
            }
            if (e.CommandName == "RejectReason")
            {
                InfoMeeting.Visible = false;
                InfoUser.Visible = false;
                InfoClassDayTime.Visible = false;
                InfoRejectReason.Visible = true;
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                Session["Id"] = commandArgs[0];
                Session["Name"] = commandArgs[1];
                lbl_InfoRejectReason_ClassName.Text = "کلاس: " + Session["Name"].ToString() + " به دلایل زیر رد شده است";
                DataTable DTx = MPB.Get_Customers_ClassName_RejectReason(int.Parse(Session["Id"].ToString()));
                txt_Detail.Text = DTx.Rows[0]["Text"].ToString();                
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                TableCell cell = (TableCell)item["Status"];
                Button btn_Meeting = e.Item.FindControl("btn_MeetingInfo") as Button;
                Button btn_User = e.Item.FindControl("btn_UserInfo") as Button;
                Button btn_ClassDayTime = e.Item.FindControl("btn_ClassDayTimeInfo") as Button;
                Button btn_RejectReason = e.Item.FindControl("btn_RejectReason") as Button;
                
                if (cell.Text == "پذیرفته شده")
                {
                    btn_Meeting.Enabled = true;
                    btn_User.Enabled = true;
                    btn_ClassDayTime.Enabled = true;
                    btn_RejectReason.Enabled = false;
                }
                else if (cell.Text == "رد شده")
                {
                    btn_Meeting.Enabled = false;
                    btn_User.Enabled = false;
                    btn_ClassDayTime.Enabled = false;
                    btn_RejectReason.Enabled = true;
                }
                else
                {
                    btn_Meeting.Enabled = false;
                    btn_User.Enabled = false;
                    btn_ClassDayTime.Enabled = false;
                    btn_RejectReason.Enabled = false;
                }


            }
        }







    }
}