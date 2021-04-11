using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using IAUEC_Apps.UI.SMS_WebReference;
using IAUEC_Apps.DTO.EmailClasses;
using System.Data;
using IAUEC_Apps.Business.university.Support;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class List_AfterCreateEmail : System.Web.UI.Page
    {
        Email_ClassBusiness emB = new Email_ClassBusiness();
        PassProfessorBusiness EmailBusiness = new PassProfessorBusiness();
        CommonBusiness EmailCommonBusiness = new CommonBusiness();
        Email_ConnectBusiness emConB = new Email_ConnectBusiness();
        StudentBuisiness stB = new StudentBuisiness();
        ActiveDirectoryBusiness adB = new ActiveDirectoryBusiness();
        //public bool chk;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
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
                    Session[sessionNames.menuID] = menuId;
                }
                AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
              
            }
            catch (Exception)
            {

                Response.Write("main");
            }
           
        }

     

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                List<Email_Class> emDTO = new List<Email_Class>();
                List<Email_ConnectDTO> emConDTO = new List<Email_ConnectDTO>();
                CommonBusiness cmn = new Business.Common.CommonBusiness();
                DataTable DT = emB.GiveList_Status(3);
                if (DT.Rows.Count == 0)
                    btn_Taeid.Visible = false;
                DataColumn column;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "OldEmail";
                DT.Columns.Add(column);
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (adB.Get_FindUser_SamAccountName(DT.Rows[i]["stcode"].ToString()))
                        DT.Rows[i]["OldEmail"] = DT.Rows[i]["stcode"].ToString();
                    else
                        DT.Rows[i]["OldEmail"] = "-";
                }
                grd_ListAfterCreateEmail.DataSource = DT;
            }
            catch (Exception)
            {
                Response.Write("need");
            }
        }

      
        protected void ExportToExcelImg_Click(object sender, ImageClickEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();

            string alternateText = (sender as ImageButton).AlternateText;
            grd_ListAfterCreateEmail.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            grd_ListAfterCreateEmail.ExportSettings.IgnorePaging = true;
            grd_ListAfterCreateEmail.ExportSettings.ExportOnlyData = true;
            grd_ListAfterCreateEmail.ExportSettings.OpenInNewWindow = true;
            grd_ListAfterCreateEmail.ExportSettings.UseItemStyles = true;
            grd_ListAfterCreateEmail.ExportSettings.FileName = "ActiveListReport";
            grd_ListAfterCreateEmail.MasterTableView.ExportToExcel();

            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 19, "لیست ایمیل ها پس از ایجاد در سامانه ایمیل");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {            
            confirmMessage.Text = "آیا از تایید پست الکترونیکی اطمینان دارید?";
            string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true); 
        }

        protected void rbConfirm_OK_Click(object sender, EventArgs e)
        {
            DataTable dtMessage = new DataTable();
            CommonBusiness cmnb = new CommonBusiness();

            foreach (GridDataItem item in grd_ListAfterCreateEmail.MasterTableView.Items)
            {
                CheckBox CheckBox1 = item.FindControl("CheckBox1") as CheckBox;
                if (CheckBox1 != null && CheckBox1.Checked)
                {
                    Email_Class emDTO = new Email_Class();
                    List<Email_ConnectDTO> emConDTO = new List<Email_ConnectDTO>();
                    
                    string RequestID= item.GetDataKeyValue("Id").ToString();

                    emDTO = emB.Email_Reg_Byid(int.Parse(RequestID));
                    string stcode = emDTO.Stcode;
                    string MailText = "<html><div dir='rtl'>" + cmnb.GetAppIDMessage(1, 2, 1, 4).Rows[0]["Text"].ToString() + "</br>" + "نام کاربری:" + emDTO .Email_Address + "</br>" + "معاونت فنی دانشگاه آزاد اسلامی واحدالکترونیکی" + "</div></html>";
                    
                    //ramezaninan-940409-start
                    DataTable dtMssage=cmnb.GetAppIDMessage(0, 2, 1, 4);
                    int id_msg = int.Parse(dtMssage.Rows[0]["ID"].ToString());
                    string smsText = dtMssage.Rows[0]["Text"].ToString() + "\r\n" + "نام کاربری:" + emDTO.Email_Address + "\r\n" + "معاونت فنی دانشگاه آزاد اسلامی واحدالکترونیکی";
                    //ramezaninan-940409-end
                    
                    
                    
                    // add Reset pass
                    //if (emB.ChangePassEmail_AfterCreateEmail(stcode))
                    if (emB.ChangePassEmail_AfterCreateEmail(stcode, RequestID))
                    //==========================
                    {

                        emB.Update_Request(RequestID, "-", 4);
                        cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 9, stcode, int.Parse(RequestID));

                        if (emDTO.UpdateEmail)
                            emB.UpdateSecondEmail_fsf2(stcode, emDTO.Email_Address.ToString() + "@iauec.ac.ir");
                        if (emDTO.ConnectType == 0)
                        {
                            //Send EMail

                          //  cmnb.SendEmail(emDTO.CEMAIL, "سامانه ایجاد پست الکترونیکی دانشگاه آزاد اسلامی واحد الکترونیکی", MailText);

                            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 27, stcode + "-status4", int.Parse(RequestID));
                        }
                        else if (emDTO.ConnectType == 1)
                        {
                            //send sms
                            // از طریق وب سرویس آسانک
                            //ramezaninan-940409-start
                            //lbl_Resault.Text = cmnb.SendSMSByMobile(emDTO.Mobile, smsText, username, pass, source, uri);
                            bool sentSMS;
                            string smsStatusText;


                            lbl_Resault.Text = cmnb.sendSMS(emDTO.Mobile, smsText,out sentSMS,out smsStatusText);
                            Lbl_Status.Text = EmailCommonBusiness.getAsanakStatusID(lbl_Resault.Text).ToString();
                            
                            EmailCommonBusiness.LogStatusMessage(stcode, lbl_Resault.Text, emDTO.Mobile, Convert.ToInt32(Lbl_Status.Text), smsStatusText, id_msg);
                            
                            

                            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 28, stcode + "-status4", int.Parse(RequestID));

                        }
                        else if (emDTO.ConnectType == 2)
                        {
                            bool sentSMS; string smsStatusText;
                            //send sms
                            //ازطریق وب سرویس آسانک
                            //ramezaninan-940409-start
                            //lbl_Resault.Text = cmnb.SendSMSByMobile(emDTO.Mobile, smsText, username, pass, source, uri);
                            lbl_Resault.Text = cmnb.sendSMS(emDTO.Mobile, smsText, out sentSMS, out smsStatusText);
                            Lbl_Status.Text = EmailCommonBusiness.getAsanakStatusID(lbl_Resault.Text).ToString();

                            EmailCommonBusiness.LogStatusMessage(stcode, lbl_Resault.Text, emDTO.Mobile, Convert.ToInt32(Lbl_Status.Text), smsStatusText, id_msg);



                            //Send EMail

                            //  cmnb.SendEmail(emDTO.CEMAIL, "سامانه ایجاد پست الکترونیکی دانشگاه آزاد اسلامی واحد الکترونیکی", MailText);

                            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 29, stcode + "-status4", int.Parse(RequestID));
                        }

                        Response.Redirect("List_AfterCreateEmail.aspx?id=" + generaterandomstr(11) + "@A" + Session[sessionNames.menuID].ToString() + "-" + generaterandomstr(2), false);
                    }
                    else
                        RadWindowManager2.RadAlert("خطا در بروز رسانی صورت گرفت", 300, 200, "پیام", "");

                }
                //catch (Exception)
                //{
                //    RadWindowManager1.RadAlert("خطا در بروز رسانی", 300, 200, "پیام", "CallBackConfirm");
                //    //Response.Redirect("List_AfterCreateEmail.aspx");             
                //}
            }
        }

        public string generaterandomstr(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        
    }
}
