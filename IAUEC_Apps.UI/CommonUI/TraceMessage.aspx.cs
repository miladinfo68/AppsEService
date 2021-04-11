using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using System.Data;
using System.Configuration;
using Telerik.Web.UI;
using IAUEC_Apps.DTO.CommonClasses;
using System.Globalization;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class TraceMessage : System.Web.UI.Page
    {
        public DataTable dtDate = new DataTable();
        public int AppID;
        public int Status;
        CommonBusiness CB = new CommonBusiness();
        public static TraceMessageDTO TMD = new TraceMessageDTO();
        public static string Text;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddl_Application.Items.Add(new ListItem("ایمیل", "2"));
                ddl_Application.Items.Add(new ListItem("ساپورت", "9"));
                ddl_Application.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Application.Items[ddl_Application.Items.Count - 1].Selected = true;
            }
        }
        protected void btn_SearchCode_Click(object sender, EventArgs e)
        {
            string Code = txt_Code.Text;
            DataTable dt = new DataTable();
            DataTable dtSearchCode = CB.GetSearchStudentOrProf(txt_Code.Text);
            if (dtSearchCode.Rows.Count == 0)
            {
                RadwindowManager1.RadAlert("شماره دانشجویی یا کد استاد صحیح وارد نشده است", 0, 100, "پیام", "");
            }
            else
            {
                if (ddl_IdStatus.Visible == true)
                {
                    AppID = int.Parse(Session[sessionNames.appID_Karbar].ToString());
                    Status = int.Parse(Session["Status"].ToString());
                    dt = CB.GetAppIDMessage(0, AppID, 1, Status);
                    if (dt.Rows.Count == 0)
                    {
                        RadwindowManager1.RadAlert("پیامکی ارسال نشده است", 0, 100, "پیغام", "");
                        lbl_CodeAsanak.Visible = false;
                        txt_CodeAsanak.Visible = false;
                    }
                    else
                    {
                        int IDRow = int.Parse(dt.Rows[0]["ID"].ToString());
                        DataTable dt2 = CB.GetCodeAsanak(Code, IDRow);
                        if (dt2.Rows.Count == 0)
                        {
                            RadwindowManager1.RadAlert("پیامکی ارسال نشده است", 0, 100, "پیغام", "");
                            lbl_CodeAsanak.Visible = false;
                            txt_CodeAsanak.Visible = false;
                        }
                        else
                        {
                            btn_TraceMessage.Visible = true;
                            if (dt.Rows.Count == 1)
                            {
                                txt_CodeAsanak.Visible = true;
                                txt_CodeAsanak.Text = dt.Rows[0]["codeAsanak"].ToString();

                            }
                            else
                            {
                                DataTable dtNew = new DataTable();
                                dtNew.Columns.Add("date", typeof(string));
                                dtNew.Columns.Add("codeAsanak", typeof(string));
                                DataRow dr = dtNew.NewRow();
                                DateTime dtMiladi = new DateTime();
                                // ok
                                if (!IsPostBack)
                                {
                                    for (Int32 i = 0; i < dt.Rows.Count; i++)
                                    {

                                        dtMiladi = DateTime.Parse(dt.Rows[i]["date"].ToString());
                                        PersianCalendar p = new PersianCalendar();
                                        string date2 = p.GetYear(dtMiladi).ToString() + "/" + p.GetMonth(dtMiladi).ToString() + "/" + p.GetDayOfMonth(dtMiladi).ToString();
                                        string codeAsanak = Convert.ToString(dt.Rows[i]["codeAsanak"].ToString());
                                        dr["date"] = date2;
                                        dr["codeAsanak"] = codeAsanak;
                                        dtNew.Rows.Add(dr.ItemArray);
                                    }
                                }
                                grd_ShowMessage.DataSource = dtNew;
                                grd_ShowMessage.DataBind();
                            }
                        }
                    }
                }
                if (ddl_IdStatus.Visible == false)
                {
                    AppID = int.Parse(Session[sessionNames.appID_Karbar].ToString());
                    dt = CB.GetAppIDMessage(0, AppID, 1, 1);
                    if (dt.Rows.Count == 0)
                    {
                        RadwindowManager1.RadAlert("پیامکی ارسال نشده است", 0, 100, "پیام", "");
                        lbl_CodeAsanak.Visible = false;
                        txt_CodeAsanak.Visible = false;
                    }
                    else
                    {

                        int IDRow = int.Parse(dt.Rows[0]["ID"].ToString());
                        dt = CB.GetCodeAsanak(Code, IDRow);
                        if (dt.Rows.Count == 0)
                        {
                            RadwindowManager1.RadAlert("پیامکی ارسال نشده است", 0, 100, "پیام", "");
                            lbl_CodeAsanak.Visible = false;
                            txt_CodeAsanak.Visible = false;
                        }
                        else
                        {
                            btn_TraceMessage.Visible = true;
                            if (dt.Rows.Count == 1)
                            {
                                lbl_CodeAsanak.Visible = true;
                                txt_CodeAsanak.Visible = true;
                                txt_CodeAsanak.Text = dt.Rows[0]["codeAsanak"].ToString();
                                //btn_SearchCode.Visible = true;
                            }
                            else
                            {
                                DataTable dtNew = new DataTable();
                                dtNew.Columns.Add("date", typeof(string));
                                dtNew.Columns.Add("codeAsanak", typeof(string));
                                DataRow dr = dtNew.NewRow();
                                // ok
                                //if (!IsPostBack)
                                //{
                                DateTime dtMiladi = new DateTime();
                                for (Int32 i = 0; i < dt.Rows.Count; i++)
                                {
                                    dtMiladi = DateTime.Parse(dt.Rows[i]["date"].ToString());
                                    PersianCalendar p = new PersianCalendar();
                                    string date2 = p.GetYear(dtMiladi).ToString() + "/" + p.GetMonth(dtMiladi).ToString() + "/" + p.GetDayOfMonth(dtMiladi).ToString();
                                    string codeAsanak = Convert.ToString(dt.Rows[i]["codeAsanak"].ToString());
                                    dr["date"] = date2;
                                    dr["codeAsanak"] = codeAsanak;
                                    dtNew.Rows.Add(dr.ItemArray);
                                }
                                //}
                                grd_ShowMessage.DataSource = dtNew;
                                grd_ShowMessage.DataBind();

                            }
                        }
                    }
                }
            }
        }

        protected void btn_TraceMessage_Click(object sender, EventArgs e)
        {
            grd_ShowMessage.Visible = false;


            bool sentSMS;
            lbl_Status.Text = CB.ShowStatusSMS(txt_CodeAsanak.Text, out sentSMS);
            string alertTitle = sentSMS?"پیگیری":"پیام";
            RadwindowManager1.RadAlert(lbl_Status.Text, 0, 100, alertTitle, "");

            lbl_CodeAsanak.Visible = false;
            txt_CodeAsanak.Visible = false;
            btn_TraceMessage.Visible = false;
            //Session["Status"] = Session["AppID"] = null;
        }


        protected void ddl_Application_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Application.SelectedValue == "9")
            {
                ddl_IdStatus.Visible = false;
                lbl_IdSatus.Visible = false;
                Session[sessionNames.appID_Karbar] = int.Parse(ddl_Application.SelectedValue);
            }
            else if (ddl_Application.SelectedValue == "2")
            {
                if (ddl_IdStatus.Visible == false && lbl_IdSatus.Visible == false)
                {
                    ddl_IdStatus.Visible = true;
                    lbl_IdSatus.Visible = true;
                }
                if (ddl_IdStatus.Items.Count == 0)
                {
                    ddl_IdStatus.Items.Add(new ListItem("انتخاب کنید", "0"));
                    ddl_IdStatus.Items[ddl_IdStatus.Items.Count - 1].Selected = true;
                    ddl_IdStatus.Items.Add(new ListItem("تأیید اولیه", "3"));
                    ddl_IdStatus.Items.Add(new ListItem("رد نهایی", "2"));
                    ddl_IdStatus.Items.Add(new ListItem("تایید نهایی", "4"));
                }
                ddl_IdStatus.SelectedValue = "0";
                Session[sessionNames.appID_Karbar] = int.Parse(ddl_Application.SelectedValue);
            }
        }

        //protected void grd_ShowMessage_ItemCommand(object sender, GridCommandEventArgs e)
        //{
        //        if (e.CommandName == "Select")
        //        {
        //            foreach (GridDataItem item in grd_ShowMessage.SelectedItems)
        //            {
        //                TMD.codeAsanak = item.GetDataKeyValue("codeAsanak").ToString();
        //                txt_CodeAsanak.Visible = true;
        //                txt_CodeAsanak.Text = TMD.codeAsanak;
        //                btn_TraceMessage.Visible = true;
        //                lbl_CodeAsanak.Visible = true;
        //            }

        //        }
        //}

        protected void ddl_IdStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Status"] = int.Parse(ddl_IdStatus.SelectedValue);
        }

        protected void grd_ShowMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_CodeAsanak.Text = grd_ShowMessage.SelectedRow.Cells[1].Text;
            txt_CodeAsanak.Visible = true;
            btn_TraceMessage.Visible = true;
            lbl_CodeAsanak.Visible = true;
        }
    }
}