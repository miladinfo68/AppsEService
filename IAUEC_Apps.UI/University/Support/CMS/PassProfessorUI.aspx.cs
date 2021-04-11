using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Support;
using IAUEC_Apps.UI.SMS_WebReference;
using System.Configuration;
using IAUEC_Apps.DTO.CommonClasses;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.DTO.EmailClasses;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Support;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.UI.University.Support
{
    public partial class PassProfessorUI : System.Web.UI.Page
    {

        public static PassProfessorDTO PPD = new PassProfessorDTO();
        CommonBusiness CB = new CommonBusiness();
        PassProfessorBusiness profBusiness = new PassProfessorBusiness();
        CommonBusiness ProfCommonBusiness = new CommonBusiness();
        public DataTable dtDepartman = new DataTable();
        public DataTable dtTerm = new DataTable();

        const string strPleaseSelect = "لطفا نوع درخواست را انتخاب کنید", strHello = "با سلام و احترام", strFrom = " دانشگاه آزاد واحد الکترونیکی", strRecordNotFound = "رکوردی وجود ندارد";
        public static string mobile, to, code, password, subject, body;
        protected void Page_Load(object sender, EventArgs e)
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

                DataTable dtTypeRequest = CB.SelectAllDaneshkade();
                DataTable dtTypeRequestSMS = profBusiness.GetTypeRequest();
                ddl_RequestSend.DataTextField = "NameRequest";
                ddl_RequestSend.DataValueField = "id";
                ddl_RequestSend.DataSource = dtTypeRequestSMS;
                ddl_RequestSend.DataBind();
                ddl_RequestSend.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_RequestSend.Items[ddl_RequestSend.Items.Count - 1].Selected = true;
                AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            }

        }
        //ramezanian-940309-start
        protected void BtnSearch_click(object sender, EventArgs e)
        {
            FillTbOstad1();
        }

        private void FillTbOstad1()
        {
            try
            {
                grd_PassProf.Visible = false;
                if (PPD.TypeSend == null || PPD.TypeSend == "0")
                {
                    RadWindowManager1.RadAlert(strPleaseSelect, 0, 100, "پیام", "");
                }
                else
                {
                    //string NameOstad = txt_Name.Text;
                    //string CodeOstad1 = txt_Code.Text;
                    //string CodeMelli = txt_CodeMelli.Text;

                    //int CodeOstad;

                    if (txt_Name.Text == string.Empty && txt_Code.Text == string.Empty && txt_CodeMelli.Text == string.Empty)
                    {
                        RadWindowManager1.RadAlert("لطفا جستجو را بر اساس یکی از آیتم ها انجام دهید", 0, 100, "هشدار", "");
                    }

                    else
                    {
                        //if (txt_Code.Text == "")
                        //    txt_Code.Text = "0";
                        ////else
                        //    //CodeOstad1 = txt_Code.Text;

                        //if (txt_CodeMelli.Text == "")
                        //    txt_CodeMelli.Text = "0";
                        ////else
                        ////    CodeMelli = txt_CodeMelli.Text;
                        //if (txt_Name.Text == "")
                        //    txt_Name.Text = "0";
                        //else
                        //    NameOstad = txt_Name.Text;
                        DataTable dt = new DataTable();
                        PassProfessorDTO.ostadType typeOfOstad = PassProfessorDTO.ostadType.آموزشی;
                        if (Convert.ToInt32(ddl_RequestSend.SelectedValue) == (int)PassProfessorDTO.enumTypeSend.ارسال_رمز_پورتال_پژوهش)
                            typeOfOstad = PassProfessorDTO.ostadType.استاد_پژوهشی;


                        dt = profBusiness.SelectRowOstad(txt_Code.Text, txt_Name.Text, txt_CodeMelli.Text, typeOfOstad);
                        if (dt.Rows.Count == 0)
                        {
                            RadWindowManager1.RadAlert("کاربری با مشخصات وارد شده یافت نشد", 0, 100, "خطا", "");
                        }
                        else
                        {
                            grd_PassProf.Dispose();
                            grd_PassProf.DataSource = dt;
                            grd_PassProf.DataBind();
                            grd_PassProf.Visible = true;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                string resault = ex.Message;
            }

            finally
            {
                //txt_Code.Text = txt_Name.Text = string.Empty;
            }

        }
        //ramezanian-930309-end
        //ارسال اس ام اس و یا ایمیل بعد از موافقت
        //ramezanian-930309-start
        protected void rbConfirm_OK_Click(object sender, EventArgs e)
        {
            if (Session["SendType"].ToString() == "0")
            {
                try
                {


                    bool sentSMS; string smsStatusText;
                    lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString(), out sentSMS, out smsStatusText);
                    int asanakStatus = ProfCommonBusiness.getAsanakStatusID(lbl_Resault.Text);
                    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), lbl_Resault.Text, PPD.mobile.ToString(), asanakStatus, smsStatusText, int.Parse(PPD.IDAppMessage.ToString()));
                    RadWindowManager1.RadAlert("به منظور پیگیری وضعیت ارسال پیامک بر روی آیکن پیگیری پیامک کلیک نمایید", null, 100, "پیام", "");

                    //lbl_Resault.Text = ProfCommonBusiness.SendSMSByMobile(PPD.mobile.ToString(), PPD.msg.ToString(), username, pass, source, uri);
                    //Lbl_Status.Text = ProfCommonBusiness.ShowStatusSMS(codeAsanak);
                    //if (Lbl_Status.Text.Substring(12, (Lbl_Status.Text.Length) - 15) == "NotFound")
                    //{
                    //    string ss = "-1";
                    //    int status = Convert.ToInt32(ss);
                    //    DataTable dt = new DataTable();
                    //    dt = ProfCommonBusiness.GetMessage(ss);
                    //    string messageStatus = dt.Rows[0][0].ToString();
                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));
                    //    RadWindowManager1.RadAlert("به منظور پیگیری وضعیت ارسال پیامک بر روی آیکن پیگیری پیامک کلیک نمایید", null, 100, "پیام", "");
                    //}
                    //else
                    //{
                    //    string ss = (Lbl_Status.Text.Substring(32, (Lbl_Status.Text.Length) - 104));

                    //     ss = Regex.Replace(ss, @"[^\d]", "");

                    //    //ss.Contains('')
                    //    int status = Convert.ToInt32(ss);
                    //    DataTable dt = new DataTable();
                    //    dt = ProfCommonBusiness.GetMessage(ss);
                    //    string messageStatus = dt.Rows[0][0].ToString();
                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));
                    //    RadWindowManager1.RadAlert("به منظور پیگیری وضعیت ارسال پیامک بر روی آیکن پیگیری پیامک کلیک نمایید", null, 100, "پیام", "");
                    //}
                }


                catch (Exception ex)
                {
                    RadWindowManager1.RadAlert("خطا در ارسال پیامک", null, 100, "خطا", "");
                }
            }


            else if (Session["SendType"].ToString() == "1")
            {
                try
                {
                    ProfCommonBusiness.SendEmail(PPD.to.ToString(), PPD.subject.ToString(), PPD.body.ToString());
                    RadWindowManager1.RadAlert("ایمیل با موفقیت ارسال شد", null, 100, "پیغام", "");
                }

                catch (Exception e1)
                {

                    RadWindowManager1.RadAlert("خطا در ارسال پست الکترونیکی:" + e1.Message, null, 100, "خطا", "");
                }

            }
            else if (Session["SendType"].ToString() == "10")
            {
                try
                {
                    //lbl_Resault.Text = ProfCommonBusiness.SendSMSByMobile(PPD.mobile.ToString(), PPD.msg.ToString(), username, pass, source, uri);
                    //lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString());
                    //string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
                    //Lbl_Status.Text = ProfCommonBusiness.ShowStatusSMS(codeAsanak);
                    //if (Lbl_Status.Text.Substring(12, (Lbl_Status.Text.Length) - 15) == "NotFound")
                    //{
                    //    string ss = "-1";
                    //    int status = Convert.ToInt32(ss);
                    //    DataTable dt = new DataTable();
                    //    dt = ProfCommonBusiness.GetMessage(ss);
                    //    string messageStatus = dt.Rows[0][0].ToString();
                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));
                    //    RadWindowManager1.RadAlert("به منظور پیگیری وضعیت ارسال پیامک بر روی آیکن پیگیری پیامک کلیک نمایید", null, 100, "پیام", "");
                    //}
                    //else
                    //{
                    //    string ss = (Lbl_Status.Text.Substring(32, (Lbl_Status.Text.Length) - 104));
                    //    ss = Regex.Replace(ss, @"[^\d]", "");
                    //    int status = Convert.ToInt32(ss);
                    //    DataTable dt = new DataTable();
                    //    dt = ProfCommonBusiness.GetMessage(ss);
                    //    string messageStatus = dt.Rows[0][0].ToString();
                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));
                    //    RadWindowManager1.RadAlert("به منظور پیگیری وضعیت ارسال پیامک بر روی آیکن پیگیری پیامک کلیک نمایید", null, 100, "پیام", "");
                    //    ProfCommonBusiness.SendEmail(PPD.to.ToString(), PPD.subject.ToString(), PPD.body.ToString());
                    //    RadWindowManager1.RadAlert("رمز شما از طریق ایمیل با موفقیت ارسال شد به منظور پیگیری از وضعیت ارسال پیامک بر روی آیکن پیگیری وضعیت کلیک نمایید", null, 100, "پیام", "");
                    //}

                    bool sentSMS; string smsStatusText;
                    lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString(), out sentSMS, out smsStatusText);
                    int asanakStatus = ProfCommonBusiness.getAsanakStatusID(lbl_Resault.Text);
                    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), lbl_Resault.Text, PPD.mobile.ToString(), asanakStatus, smsStatusText, int.Parse(PPD.IDAppMessage.ToString()));

                }

                catch (Exception)
                {
                    RadWindowManager1.RadAlert("  خطا در ارسال پیامک یا پست الکترونیکی", null, 100, "خطا", "");
                }
            }
        }
        //ramezanian-940309-end

        //ramezanin-940309-start
        protected void grd_PassProf_ItemCommand1(object sender, GridCommandEventArgs e)
        {
            string[] index = e.CommandArgument.ToString().Split(new char[] { ',' });
            #region ارسال_رمز_خدمات_الکترونیکی
            if (PPD.TypeSend == ((int)PassProfessorDTO.enumTypeSend.ارسال_رمز_خدمات).ToString())
            {
                //ارسال اس ام اس
                if (e.CommandName == "SendSMS")
                {
                    Session["SendType"] = "0";
                    PPD.mobile = index[4].ToString();
                    PPD.code = index[0].ToString();

                    string encodePassword=index[5].ToString();
                    string decodePassword = CommonBusiness.DecryptPass(encodePassword);
                    PPD.password = decodePassword;


                    if (PPD.mobile != string.Empty)
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = CB.GetAppIDMessage(0, 9, 1, 1);
                        PPD.msg = dt1.Rows[0]["Text"].ToString() + "(service.iauec.ac.ir)" + "شما می توانید از" + "\r\n" + "نام کاربری : " + PPD.code + "\r\n" + "رمز عبور :" + PPD.password + "استفاده نمایید." + "\r\n" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                        PPD.IDAppMessage = dt1.Rows[0]["ID"].ToString();
                        //گذاشتن پیغام
                        confirmMessage.Text = "آیا موافق به ارسال رمز از طریق پیامک هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("شماره موبایل موجود نمی باشد", 0, 100, "پیغام", "");
                    }


                }

                // ارسال ایمیل
                if (e.CommandName == "SendEmail")
                {
                    Session["SendType"] = "1";
                    //string[] index = e.CommandArgument.ToString().Split(new char[] { ',' });
                    PPD.to = index[3].ToString();
                    PPD.code = index[0].ToString();
                    PPD.password = index[5].ToString();

                    string encodePassword = index[5].ToString();
                    string decodePassword = CommonBusiness.DecryptPass(encodePassword);
                    PPD.password = decodePassword;


                    PPD.subject = " سامانه ارسال رمز به اساتید در واحد الکترونیکی";
                    DataTable dt1 = new DataTable();
                    dt1 = CB.GetAppIDMessage(0, 9, 1, 1);

                    PPD.body = dt1.Rows[0]["Text"].ToString() + "\r\n" + "(Service.iauec.ac.ir)" + "\r\n" + "شما می توانید از" + "نام کاربری : " + PPD.code + " و رمز عبور:" + PPD.password + " استفاده نمایید." + "\r\n" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                    //"<HTML><div dir='rtl' style='font-family:Tahoma'>" + "استاد گرامی" + "<br/> " + " جهت ورود به سامانه اتوماسیون به آدرس" + "(automation.iauec.ac.ir)،" + "<br/>" + "شما می توانید از" + "<br/>" + "نام کاربری:" + PPD.code + "<br/> " + " و رمز عبور: " + PPD.password + "<br/>" + "استفاده نمایید" + "<br/>" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی" + "</div></HTML>";

                    if (PPD.to != string.Empty)
                    {
                        confirmMessage.Text = "آیا موافق به ارسال رمز از طریق پست الکترونیکی هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);

                    }
                    else
                    {
                        RadWindowManager1.RadAlert("پست الکترونیکی موجود نمی باشد", null, 100, "خطا", "");
                    }
                }

                // ارسال اس ام اس و ایمیل

                if (e.CommandName == "SendSmsEmail")
                {
                    PPD.code = index[0].ToString();
                    PPD.to = index[3].ToString();
                    PPD.mobile = index[4].ToString();

                    string encodePassword = index[5].ToString();
                    string decodePassword = CommonBusiness.DecryptPass(encodePassword);
                    PPD.password = decodePassword;


                    PPD.subject = " سیستم ارسال رمز به اساتید در  دانشگاه آزاد اسلامی واحد الکترونیکی";
                    if (PPD.to != string.Empty && PPD.mobile != string.Empty)
                    {
                        DataTable dt2 = new DataTable();
                        dt2 = CB.GetAppIDMessage(0, 9, 1, 1);
                        PPD.IDAppMessage = dt2.Rows[0]["ID"].ToString();
                        PPD.msg = dt2.Rows[0]["Text"].ToString() + "(service.iauec.ac.ir)" + "شما می توانید از" + "\r\n" + "نام کاربری : " + PPD.code + "\r\n" + " و رمز عبور:" + PPD.password + "استفاده نمایید." + "\r\n" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                        PPD.body = dt2.Rows[0]["Text"].ToString() + "\r\n" + "(service.iauec.ac.ir)" + "\r\n" + "شما می توانید از" + "نام کاربری : " + PPD.code + " و رمز عبور:" + PPD.password + " استفاده نمایید." + "\r\n" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                        //"<HTML><div dir='rtl' style='font-family:Tahoma'>" + "استاد گرامی" + "<br/> " + " جهت ورود به سامانه اتوماسیون به آدرس" + "(automation.iauec.ac.ir)،" + "<br/>" + "شما می توانید از" + "<br/>" + "نام کاربری: " + PPD.code + "<br/> " + " و رمز عبور: " + PPD.password + "<br/>" + "استفاده نمایید" + "<br/>" + "/namespace" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی" + "</div></HTML>";

                        confirmMessage.Text = "آیا موافق به ارسال رمز از طریق پیامک و پست الکترونیکی هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
                        Session["SendType"] = "10";
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("پست الکترونیکی و یا ایمیل موجود نمی باشد", null, 100, "خطا", "");
                    }

                }
                if (e.CommandName == "ShowStatusMessage")
                {
                    DataTable dtAppSms = CB.GetAppIDMessage(0, 9, 1, 1);
                    ShowStatusMessage(index[0].ToString(), Convert.ToInt32(dtAppSms.Rows[0]["ID"]));
                }
            }
            #endregion

            #region ارسال_ثبت_نمرات
            else if (PPD.TypeSend == ((int)PassProfessorDTO.enumTypeSend.ارسال_ثبت_نمرات).ToString())
            {
                if (e.CommandName == "SendSMS")
                {
                    Session["SendType"] = "0";
                    PPD.mobile = index[4].ToString();
                    PPD.code = index[0].ToString();
                    if (PPD.mobile != string.Empty)
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = CB.GetAppIDMessage(0, 9, 2, 1);
                        PPD.msg = "با سلام و احترام" + "\r\n" + dt1.Rows[0]["Text"].ToString() + "\r\n" + "دانشگاه آزاد واحد الکترونیکی";
                        PPD.IDAppMessage = dt1.Rows[0]["ID"].ToString();
                        //گذاشتن پیغام
                        confirmMessage.Text = "آیا برای اطلاع رسانی از سامانه ثبت نمره با ارسال پیامک موافق هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("شماره موبایل موجود نمی باشد", 0, 100, "پیغام", "");
                    }
                }

                // ارسال ایمیل
                if (e.CommandName == "SendEmail")
                {
                    Session["SendType"] = "1";
                    PPD.to = index[3].ToString();
                    PPD.code = index[0].ToString();
                    PPD.password = index[5].ToString();
                    PPD.subject = " سامانه ثبت نمرات در واحد الکترونیکی";
                    DataTable dt1 = new DataTable();
                    dt1 = CB.GetAppIDMessage(1, 9, 2, 1);
                    PPD.body = "با سلام و احترام " + "<br/> " + dt1.Rows[0]["Text"].ToString() + "<br/>" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                    if (PPD.to != string.Empty)
                    {
                        confirmMessage.Text = "آیا برای اطلاع رسانی از سامانه ثبت نمره با ارسال پست الکترونیکی موافق هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);

                    }
                    else
                    {
                        RadWindowManager1.RadAlert("پست الکترونیکی موجود نمی باشد", null, 100, "خطا", "");
                    }
                }

                // ارسال اس ام اس و ایمیل

                if (e.CommandName == "SendSmsEmail")
                {
                    
                    PPD.code = index[0].ToString();
                    PPD.to = index[3].ToString();
                    PPD.mobile = index[4].ToString();
                    PPD.password = index[5].ToString();
                    PPD.subject = "سیستم ثبت نمرات در دانشگاه آزاد اسلامی واحد الکترونیکی";
                    if (PPD.to != string.Empty && PPD.mobile != string.Empty)
                    {

                        DataTable dt2 = new DataTable();
                        dt2 = CB.GetAppIDMessage(0, 9, 2, 1);
                        PPD.msg = "با سلام و احترام" + "\r\n" + dt2.Rows[0]["Text"].ToString() + "\r\n" + "دانشگاه آزاد واحد الکترونیکی";
                        PPD.IDAppMessage = dt2.Rows[0]["ID"].ToString();
                        DataTable dt1 = new DataTable();
                        dt1 = CB.GetAppIDMessage(1, 9, 2, 1);
                        PPD.body = "با سلام و احترام " + "<br/> " + dt1.Rows[0]["Text"].ToString() + "<br/>" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                        confirmMessage.Text = "آیا برای اطلاع رسانی از سامانه ثبت نمره با ارسال پیامک و پست الکترونیکی موافق هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
                        Session["SendType"] = "10";
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("پست الکترونیکی و یا ایمیل موجود نمی باشد", null, 100, "خطا", "");
                    }

                }
                if (e.CommandName == "ShowStatusMessage")
                {
                    DataTable dtAppSms = CB.GetAppIDMessage(0, 9, 2, 1);
                    ShowStatusMessage(index[0].ToString(),Convert.ToInt32(dtAppSms.Rows[0]["ID"]));
                }
            }
            #endregion

            #region ارسال_شرکت_در_کلاس_امتحانات
            else if (PPD.TypeSend == ((int)PassProfessorDTO.enumTypeSend.ارسال_شرکت_در_کلاس_امتحانات).ToString())
            {
                if (e.CommandName == "SendSMS")
                {
                    Session["SendType"] = "0";
                    PPD.mobile = index[4].ToString();
                    PPD.code = index[0].ToString();


                    if (PPD.mobile != string.Empty)
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = CB.GetAppIDMessage(0, 9, 3, 1);
                        PPD.msg = "با سلام و احترام" + "\r\n" + dt1.Rows[0]["Text"].ToString() + "\r\n" + "دانشگاه آزاد واحد الکترونیکی";
                        PPD.IDAppMessage = dt1.Rows[0]["ID"].ToString();
                        //گذاشتن پیغام
                        confirmMessage.Text = "آیا جهت اطلاع رسانی از سامانه شرکت در کلاس امتحانات با ارسال پیامک موافق هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("شماره موبایل موجود نمی باشد", 0, 100, "پیغام", "");
                    }
                }

                // ارسال ایمیل
                if (e.CommandName == "SendEmail")
                {
                    Session["SendType"] = "1";
                    PPD.to = index[3].ToString();
                    PPD.code = index[0].ToString();
                    PPD.password = index[5].ToString();
                    PPD.subject = " سامانه شرکت در کلاس امتحانات در دانشگاه آزاد اسلامی واحد الکترونیکی";
                    DataTable dt1 = new DataTable();
                    dt1 = CB.GetAppIDMessage(1, 9, 3, 1);
                    PPD.body = "با سلام و احترام " + "<br/> " + dt1.Rows[0]["Text"].ToString() + "<br/>" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                    if (PPD.to != string.Empty)
                    {
                        confirmMessage.Text = "آیا جهت اطلاع رسانی از سامانه شرکت در کلاس امتحانات با ارسال پست الکترونیکی موافق هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);

                    }
                    else
                    {
                        RadWindowManager1.RadAlert("پست الکترونیکی موجود نمی باشد", null, 100, "خطا", "");
                    }
                }

                // ارسال اس ام اس و ایمیل

                if (e.CommandName == "SendSmsEmail")
                {
                    PPD.code = index[0].ToString();
                    PPD.to = index[3].ToString();
                    PPD.mobile = index[4].ToString();
                    PPD.password = index[5].ToString();
                    PPD.subject = " سیستم شرکت در کلاس امتحانات در دانشگاه آزاد اسلامی واحد الکترونیکی";
                    if (PPD.to != string.Empty && PPD.mobile != string.Empty)
                    {

                        DataTable dt2 = new DataTable();
                        dt2 = CB.GetAppIDMessage(0, 9, 3, 1);
                        PPD.msg = "با سلام و احترام" + "\r\n" + dt2.Rows[0]["Text"].ToString() + "\r\n" + "دانشگاه آزاد واحد الکترونیکی";
                        PPD.IDAppMessage = dt2.Rows[0]["ID"].ToString();
                        DataTable dt1 = new DataTable();
                        dt1 = CB.GetAppIDMessage(1, 9, 3, 1);
                        PPD.body = "با سلام و احترام " + "<br/> " + dt1.Rows[0]["Text"].ToString() + "<br/>" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                        confirmMessage.Text = "آیا جهت اطلاع رسانی از سامانه شرکت در کلاس امتحانات با ارسال پیامک و پست الکترونیکی موافق هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
                        Session["SendType"] = "10";
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("پست الکترونیکی و یا ایمیل موجود نمی باشد", null, 100, "خطا", "");
                    }

                }
                if (e.CommandName == "ShowStatusMessage")
                {
                    DataTable dtAppSms = CB.GetAppIDMessage(0, 9, 3, 1);
                    ShowStatusMessage(index[0].ToString(), Convert.ToInt32(dtAppSms.Rows[0]["ID"]));
                }
            }
            #endregion

            #region ارسال_رمز_پورتال_پژوهش
            else if (PPD.TypeSend == ((int)PassProfessorDTO.enumTypeSend.ارسال_رمز_پورتال_پژوهش).ToString())
            {
                if (e.CommandName == "SendSMS")
                {
                    Session["SendType"] = "0";
                    PPD.mobile = index[4].ToString();
                    PPD.code = index[0].ToString();

                    string encodePassword = index[5].ToString();
                    string decodePassword = CommonBusiness.DecryptPass(encodePassword);
                    PPD.password = decodePassword;




                    if (PPD.mobile != string.Empty)
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = CB.GetAppIDMessage(0, 9, 5, 1);
                        PPD.msg = dt1.Rows[0]["Text"].ToString() + "\r\n" + "نام کاربری : " + PPD.code + "\r\n" + "رمز عبور :" + PPD.password + "\r\n" + "از طریق آدرس" + "\r\n" + "http://service.iauec.ac.ir" + "\r\n" + "به پورتال پژوهشی واحد الکترونیکی وارد شوید" + "\r\n" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                        PPD.IDAppMessage = dt1.Rows[0]["ID"].ToString();
                        //گذاشتن پیغام
                        confirmMessage.Text = "آیا موافق به ارسال رمز اساتید پورتال پژوهش از طریق پیامک موافق هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("شماره موبایل موجود نمی باشد", 0, 100, "پیغام", "");
                    }
                }

                // ارسال ایمیل
                if (e.CommandName == "SendEmail")
                {
                    Session["SendType"] = "1";
                    PPD.to = index[3].ToString();
                    PPD.code = index[0].ToString();
                    PPD.password = index[5].ToString();

                    string encodePassword = index[5].ToString();
                    string decodePassword = CommonBusiness.DecryptPass(encodePassword);
                    PPD.password = decodePassword;


                    PPD.subject = " سامانه ارسال رمز به اساتید در واحد الکترونیکی";
                    PPD.body = "استاد گرامی" + "<br/> " + " جهت ورود به پورتال پژوهشی به آدرس" + "(http://service.iauec.ac.ir)،" + "<br/>" + "  شما می توانید از اطلاعات کاربری زیر استفاده نمایید." + "<br/>" + "نام کاربری:" + PPD.code + "<br/> " + " رمز عبور: " + PPD.password + "<br/>" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                    if (PPD.to != string.Empty)
                    {
                        confirmMessage.Text = "آیا موافق به ارسال رمز از طریق پست الکترونیکی هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);

                    }
                    else
                    {
                        RadWindowManager1.RadAlert("پست الکترونیکی موجود نمی باشد", null, 100, "خطا", "");
                    }
                }

                // ارسال اس ام اس و ایمیل

                if (e.CommandName == "SendSmsEmail")
                {
                    PPD.code = index[0].ToString();
                    PPD.to = index[3].ToString();
                    PPD.mobile = index[4].ToString();
                    PPD.password = index[5].ToString();

                    string encodePassword = index[5].ToString();
                    string decodePassword = CommonBusiness.DecryptPass(encodePassword);
                    PPD.password = decodePassword;


                    PPD.subject = " سیستم ارسال رمز به اساتید در  دانشگاه آزاد اسلامی واحد الکترونیکی";
                    if (PPD.to != string.Empty && PPD.mobile != string.Empty)
                    {
                        DataTable dt2 = new DataTable();
                        dt2 = CB.GetAppIDMessage(0, 9, 5, 1);
                        PPD.IDAppMessage = dt2.Rows[0]["ID"].ToString();
                        PPD.msg = dt2.Rows[0]["Text"].ToString() + "\r\n" + "نام کاربری : " + PPD.code + "\r\n" + "رمز عبور :" + PPD.password + "\r\n" + "از طریق آدرس" + "\r\n" + "http://service.iauec.ac.ir" + "\r\n" + "به پورتال پژوهشی واحد الکترونیکی وارد شوید";
                        PPD.body = "استاد گرامی" + "<br/> " + " جهت ورود به سامانه خدمات الکترونیکی به آدرس" + "(service.iauec.ac.ir)،" + "<br/>" + "شما می توانید از" + "<br/>" + "نام کاربری: " + PPD.code + "<br/> " + " و رمز عبور: " + PPD.password + "<br/>" + "استفاده نمایید" + "<br/>" + "/namespace" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                        confirmMessage.Text = "آیا موافق به ارسال رمز از طریق پیامک و پست الکترونیکی هستید?";
                        string script = "function f(){radopen(null, 'rw_customConfirm'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
                        Session["SendType"] = "10";
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("پست الکترونیکی و یا ایمیل موجود نمی باشد", null, 100, "خطا", "");
                    }

                }
                if (e.CommandName == "ShowStatusMessage")
                {
                    DataTable dtAppSms = CB.GetAppIDMessage(0, 9, 5, 1);
                    ShowStatusMessage(index[0].ToString(), Convert.ToInt32(dtAppSms.Rows[0]["ID"]));
                }
            }
            #endregion
        }

        private void ShowStatusMessage(string codeOstad, int appID)
        {
            DataTable dtAsanak = CB.GetCodeAsanak(codeOstad, appID);
            if (dtAsanak.Rows.Count > 0)
            {
                var lastAsanak = dtAsanak.Compute("max(id)", "");
                if (lastAsanak != null)
                {
                    lbl_Resault.Text = dtAsanak.Select("id=" + lastAsanak)[0]["codeAsanak"].ToString();
                }
            }
            if (lbl_Resault.Text != "")
            {
                string codeAsanak = lbl_Resault.Text;

                Lbl_Status.Text = ProfCommonBusiness.ShowStatusSMS(codeAsanak);
                int asanakStatus = ProfCommonBusiness.getAsanakStatusID(codeAsanak);
                ProfCommonBusiness.UpdateLogMessageStatus(asanakStatus.ToString(), Lbl_Status.Text, codeAsanak);
                RadWindowManager1.RadAlert(Lbl_Status.Text, null, 100, "پیگیری", "");
            }
            else if (lbl_Resault.Text == "")
            {
                RadWindowManager1.RadAlert("پیامی ارسال نشده است", null, 100, "خطا", "");
            }
        }

        protected void ddl_RequestSend_SelectedIndexChanged(object sender, EventArgs e)
        {
            PPD.TypeSend = ddl_RequestSend.SelectedValue;
            if (rdb_Group.Checked == true)
            {
                if (ddl_RequestSend.SelectedValue != "0" || ddl_RequestSend.SelectedValue == null)
                {
                    pnl_Prof.Visible = false;
                    lbl_Daneshkade.Visible = true;
                    ddl_Daneshkade.Visible = true;
                    DataTable dtDaneshkade = CB.SelectAllDaneshkade();
                    ddl_Daneshkade.DataTextField = "namedanesh";
                    ddl_Daneshkade.DataValueField = "id";
                    ddl_Daneshkade.DataSource = dtDaneshkade;
                    ddl_Daneshkade.DataBind();
                    ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                    ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;
                }
            }
            if (rdb_Single.Checked == true)
            {
                if (ddl_RequestSend.SelectedValue != "0" || ddl_RequestSend.SelectedValue == null)
                {
                    lbl_Daneshkade.Visible = false;
                    ddl_Daneshkade.Visible = false;
                    lbl_Departman.Visible = false;
                    ddl_Departman.Visible = false;
                    btn_SendGroup.Visible = false;
                    pnl_Prof.Visible = true;
                }
            }

            if ((rdb_Group.Checked || rdb_Single.Checked) && (txt_Code.Text != "" || txt_CodeMelli.Text != "" || txt_Name.Text != ""))
            {
                FillTbOstad1();
            }
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            PPD.Daneshkade = ddl_Daneshkade.SelectedValue;

            if (PPD.Daneshkade != "0")
            {
                lbl_Departman.Visible = true;
                ddl_Departman.Visible = true;
                dtDepartman = CB.GetAllDepartman(int.Parse(PPD.Daneshkade));
                ddl_Departman.DataTextField = "namegroup";
                ddl_Departman.DataValueField = "id";
                ddl_Departman.DataSource = dtDepartman;
                ddl_Departman.DataBind();
                ddl_Departman.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Departman.Items[ddl_Departman.Items.Count - 1].Selected = true;
                btn_SendGroup.Visible = true;
            }
        }

        protected void ddl_Departman_SelectedIndexChanged(object sender, EventArgs e)
        {
            PPD.Departman = ddl_Departman.SelectedValue;
        }

        protected void InfoSend_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Group.Checked == true)
            {
                if (ddl_RequestSend.SelectedValue != "0" || ddl_RequestSend.SelectedValue == null)
                {
                    pnl_Prof.Visible = false;
                    lbl_Daneshkade.Visible = true;
                    ddl_Daneshkade.Visible = true;
                    DataTable dtDaneshkade = CB.SelectAllDaneshkade();
                    ddl_Daneshkade.DataTextField = "namedanesh";
                    ddl_Daneshkade.DataValueField = "id";
                    ddl_Daneshkade.DataSource = dtDaneshkade;
                    ddl_Daneshkade.DataBind();
                    ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                    ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;
                }
                else
                {
                    RadWindowManager1.RadAlert(strPleaseSelect, 300, 0, "پیام", "");
                }
            }
            if (rdb_Single.Checked == true)
            {
                if (ddl_RequestSend.SelectedValue != "0" || ddl_RequestSend.SelectedValue == null)
                {
                    lbl_Daneshkade.Visible = false;
                    ddl_Daneshkade.Visible = false;
                    lbl_Departman.Visible = false;
                    ddl_Departman.Visible = false;
                    btn_SendGroup.Visible = false;
                    pnl_Prof.Visible = true;
                }
                else
                {
                    RadWindowManager1.RadAlert(strPleaseSelect, 300, 0, "پیام", "");
                }
            }
            grd_PassProf.Visible = rdb_Single.Checked;
        }

        // ramezanian-940602-start
        protected void btn_SendGroup_Click(object sender, EventArgs e)
        {
            if (PPD.TypeSend == null || PPD.TypeSend == "0")
            {
                RadWindowManager1.RadAlert(strPleaseSelect, 0, 100, "پیام", "");
            }
            else
            {
                PassProfessorDTO.ostadType typeOfOstad = PassProfessorDTO.ostadType.آموزشی;
                if (Convert.ToInt32(ddl_RequestSend.SelectedValue) == (int)PassProfessorDTO.enumTypeSend.ارسال_رمز_پورتال_پژوهش)
                    typeOfOstad = PassProfessorDTO.ostadType.استاد_پژوهشی;

                #region ارسال_رمز_خدمات_الکترونیکی
                if (PPD.TypeSend == ((int)PassProfessorDTO.enumTypeSend.ارسال_رمز_خدمات).ToString())
                {
                    DataTable dtResault = new DataTable();
                    if (PPD.Departman == null)
                    {
                        PPD.Departman = "0";
                    }
                    dtResault = profBusiness.GetProfessorInfoGroup(int.Parse(PPD.Daneshkade), int.Parse(PPD.Departman), typeOfOstad);
                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert(strRecordNotFound, 0, 100, "پیام", "");
                    }
                    else
                    {
                        int j = 0;
                        for (int i = 0; i < dtResault.Rows.Count; i++)
                        {
                            if (dtResault.Rows[j]["mobile"].ToString() != string.Empty)
                            {
                                PPD.mobile = dtResault.Rows[j]["mobile"].ToString();
                                PPD.code = dtResault.Rows[j]["Code_Ostad"].ToString();
                                PPD.password = dtResault.Rows[j]["password_ost"].ToString();

                                string encodePassword = dtResault.Rows[j]["password_ost"].ToString();
                                string decodePassword = CommonBusiness.DecryptPass(encodePassword);
                                PPD.password = decodePassword;


                                DataTable dt1 = new DataTable();
                                dt1 = CB.GetAppIDMessage(0, 9, 1, 1);
                                PPD.msg = dt1.Rows[0]["Text"].ToString() + "(service.iauec.ac.ir)" + "شما می توانید از" + "\r\n" + "نام کاربری : " + PPD.code + "\r\n" + "رمز عبور :" + PPD.password + "استفاده نمایید." + "\r\n" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                                PPD.IDAppMessage = dt1.Rows[0]["ID"].ToString();
                                try
                                {
                                    //lbl_Resault.Text = ProfCommonBusiness.SendSMSByMobile(PPD.mobile.ToString(), PPD.msg.ToString(), username, pass, source, uri);
                                    //lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString());
                                    //string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
                                    //Lbl_Status.Text = ProfCommonBusiness.ShowStatusSMS(codeAsanak);
                                    //if (Lbl_Status.Text.Substring(12, (Lbl_Status.Text.Length) - 15) == "NotFound")
                                    //{
                                    //    string ss = "-1";
                                    //    int status = Convert.ToInt32(ss);
                                    //    DataTable dt = new DataTable();
                                    //    dt = ProfCommonBusiness.GetMessage(ss);
                                    //    string messageStatus = dt.Rows[0][0].ToString();
                                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));

                                    //}
                                    //else
                                    //{
                                    //    string ss = (Lbl_Status.Text.Substring(32, (Lbl_Status.Text.Length) - 104));
                                    //    ss = Regex.Replace(ss, @"[^\d]", "");
                                    //    int status = Convert.ToInt32(ss);
                                    //    DataTable dt = new DataTable();
                                    //    dt = ProfCommonBusiness.GetMessage(ss);
                                    //    string messageStatus = dt.Rows[0][0].ToString();
                                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));

                                    //}

                                    bool sentSMS; string smsStatusText;
                                    lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString(), out sentSMS, out smsStatusText);
                                    int asanakStatus = ProfCommonBusiness.getAsanakStatusID(lbl_Resault.Text);
                                    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), lbl_Resault.Text, PPD.mobile.ToString(), asanakStatus, smsStatusText, int.Parse(PPD.IDAppMessage.ToString()));

                                }
                                catch (Exception)
                                {
                                    RadWindowManager1.RadAlert("خطا در ارسال پیامک", null, 100, "خطا", "");
                                }

                            }
                            j++;
                        }

                    }

                }
                #endregion

                #region ارسال پیامک به استاد برای ثبت نمرات
                else if (PPD.TypeSend == ((int)PassProfessorDTO.enumTypeSend.ارسال_ثبت_نمرات).ToString())
                {
                    DataTable dtResault = new DataTable();
                    if (PPD.Departman == null)
                    {
                        PPD.Departman = "0";
                    }
                    dtResault = profBusiness.GetProfessorInfoGroup(int.Parse(PPD.Daneshkade), int.Parse(PPD.Departman), typeOfOstad);
                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert(strRecordNotFound, 0, 100, "پیام", "");
                    }
                    else
                    {
                        int j = 0;
                        for (int i = 0; i < dtResault.Rows.Count; i++)
                        {
                            if (dtResault.Rows[j]["mobile"].ToString() != string.Empty)
                            {
                                PPD.mobile = dtResault.Rows[j]["mobile"].ToString();
                                PPD.code = dtResault.Rows[j]["Code_Ostad"].ToString();
                                DataTable dt1 = new DataTable();
                                dt1 = CB.GetAppIDMessage(0, 9, 2, 1);
                                // PPD.msg = "با سلام و احترام" + "\r\n" + dt1.Rows[0]["Text"].ToString() + "\r\n" + "دانشگاه آزاد واحد الکترونیکی";
                                PPD.msg = string.Format("{0} \r\n {1} \r\n {2}", strHello, dt1.Rows[0]["Text"].ToString(), strFrom);
                                PPD.IDAppMessage = dt1.Rows[0]["ID"].ToString();
                                try
                                {
                                    //lbl_Resault.Text = ProfCommonBusiness.SendSMSByMobile(PPD.mobile.ToString(), PPD.msg.ToString(), username, pass, source, uri);
                                    //lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString());
                                    //string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
                                    //Lbl_Status.Text = ProfCommonBusiness.ShowStatusSMS(codeAsanak);
                                    //if (Lbl_Status.Text.Substring(12, (Lbl_Status.Text.Length) - 15) == "NotFound")
                                    //{
                                    //    string ss = "-1";
                                    //    int status = Convert.ToInt32(ss);
                                    //    DataTable dt = new DataTable();
                                    //    dt = ProfCommonBusiness.GetMessage(ss);
                                    //    string messageStatus = dt.Rows[0][0].ToString();
                                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));

                                    //}
                                    //else
                                    //{
                                    //    string ss = (Lbl_Status.Text.Substring(32, (Lbl_Status.Text.Length) - 104));
                                    //    ss = Regex.Replace(ss, @"[^\d]", "");
                                    //    int status = Convert.ToInt32(ss);
                                    //    DataTable dt = new DataTable();
                                    //    dt = ProfCommonBusiness.GetMessage(ss);
                                    //    string messageStatus = dt.Rows[0][0].ToString();
                                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));

                                    //}
                                    bool sentSMS; string smsStatusText;
                                    lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString(), out sentSMS, out smsStatusText);
                                    int asanakStatus = ProfCommonBusiness.getAsanakStatusID(lbl_Resault.Text);
                                    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), lbl_Resault.Text, PPD.mobile.ToString(), asanakStatus, smsStatusText, int.Parse(PPD.IDAppMessage.ToString()));

                                }
                                catch (Exception)
                                {
                                    RadWindowManager1.RadAlert("خطا در ارسال پیامک", null, 100, "خطا", "");
                                }

                            }
                            j++;
                        }

                    }
                }
                #endregion

                #region ارسال پیامک به استاد برای شرکت در کلاس امتحانات
                else if (PPD.TypeSend == ((int)PassProfessorDTO.enumTypeSend.ارسال_شرکت_در_کلاس_امتحانات).ToString())
                {
                    DataTable dtResault = new DataTable();
                    if (PPD.Departman == null)
                    {
                        PPD.Departman = "0";
                    }
                    dtResault = profBusiness.GetProfessorInfoGroup(int.Parse(PPD.Daneshkade), int.Parse(PPD.Departman), typeOfOstad);
                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert(strRecordNotFound, 0, 100, "پیام", "");
                    }
                    else
                    {
                        int j = 0;
                        for (int i = 0; i < dtResault.Rows.Count; i++)
                        {
                            if (dtResault.Rows[j]["mobile"].ToString() != string.Empty)
                            {
                                PPD.mobile = dtResault.Rows[j]["mobile"].ToString();
                                PPD.code = dtResault.Rows[j]["Code_Ostad"].ToString();
                                DataTable dt1 = new DataTable();
                                dt1 = CB.GetAppIDMessage(0, 9, 3, 1);
                                PPD.msg = string.Format("{0} \r\n {1} \r\n {2}", strHello, dt1.Rows[0]["Text"].ToString(), strFrom);
                                PPD.IDAppMessage = dt1.Rows[0]["ID"].ToString();
                                try
                                {
                                    //lbl_Resault.Text = ProfCommonBusiness.SendSMSByMobile(PPD.mobile.ToString(), PPD.msg.ToString(), username, pass, source, uri);
                                    //lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString());
                                    //string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
                                    //Lbl_Status.Text = ProfCommonBusiness.ShowStatusSMS(codeAsanak);
                                    //if (Lbl_Status.Text.Substring(12, (Lbl_Status.Text.Length) - 15) == "NotFound")
                                    //{
                                    //    string ss = "-1";
                                    //    int status = Convert.ToInt32(ss);
                                    //    DataTable dt = new DataTable();
                                    //    dt = ProfCommonBusiness.GetMessage(ss);
                                    //    string messageStatus = dt.Rows[0][0].ToString();
                                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));

                                    //}
                                    //else
                                    //{
                                    //    string ss = (Lbl_Status.Text.Substring(32, (Lbl_Status.Text.Length) - 104));
                                    //    ss = Regex.Replace(ss, @"[^\d]", "");
                                    //    int status = Convert.ToInt32(ss);
                                    //    DataTable dt = new DataTable();
                                    //    dt = ProfCommonBusiness.GetMessage(ss);
                                    //    string messageStatus = dt.Rows[0][0].ToString();
                                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));

                                    //}
                                    bool sentSMS; string smsStatusText;
                                    lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString(), out sentSMS, out smsStatusText);
                                    int asanakStatus = ProfCommonBusiness.getAsanakStatusID(lbl_Resault.Text);
                                    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), lbl_Resault.Text, PPD.mobile.ToString(), asanakStatus, smsStatusText, int.Parse(PPD.IDAppMessage.ToString()));

                                }
                                catch (Exception)
                                {
                                    RadWindowManager1.RadAlert("خطا در ارسال پیامک", null, 100, "خطا", "");
                                }

                            }
                            j++;
                        }

                    }
                }
                #endregion

                #region ارسال_رمز_پورتال_پژوهش
                else if (PPD.TypeSend == ((int)PassProfessorDTO.enumTypeSend.ارسال_رمز_پورتال_پژوهش).ToString())
                {
                    DataTable dtResault = new DataTable();
                    if (PPD.Departman == null)
                    {
                        PPD.Departman = "0";
                    }
                    dtResault = profBusiness.GetProfessorInfoGroup(int.Parse(PPD.Daneshkade), int.Parse(PPD.Departman), typeOfOstad);
                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert(strRecordNotFound, 0, 100, "پیام", "");
                    }
                    else
                    {
                        int j = 0;
                        for (int i = 0; i < dtResault.Rows.Count; i++)
                        {
                            if (dtResault.Rows[j]["mobile"].ToString() != string.Empty)
                            {
                                PPD.mobile = dtResault.Rows[j]["mobile"].ToString();
                                PPD.code = dtResault.Rows[j]["Code_Ostad"].ToString();
                                PPD.password = dtResault.Rows[j]["password_ost"].ToString();

                                string encodePassword = dtResault.Rows[j]["password_ost"].ToString();
                                string decodePassword = CommonBusiness.DecryptPass(encodePassword);
                                PPD.password = decodePassword;


                                DataTable dt1 = new DataTable();
                                dt1 = CB.GetAppIDMessage(0, 9, 1, 1);
                                PPD.msg = dt1.Rows[0]["Text"].ToString() + "(service.iauec.ac.ir)" + "شما می توانید از" + "\r\n" + "نام کاربری : " + PPD.code + "\r\n" + "رمز عبور :" + PPD.password + "استفاده نمایید." + "\r\n" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";
                                PPD.IDAppMessage = dt1.Rows[0]["ID"].ToString();
                                try
                                {
                                    //lbl_Resault.Text = ProfCommonBusiness.SendSMSByMobile(PPD.mobile.ToString(), PPD.msg.ToString(), username, pass, source, uri);
                                    //lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString());
                                    //string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
                                    //Lbl_Status.Text = ProfCommonBusiness.ShowStatusSMS(codeAsanak);
                                    //if (Lbl_Status.Text.Substring(12, (Lbl_Status.Text.Length) - 15) == "NotFound")
                                    //{
                                    //    string ss = "-1";
                                    //    int status = Convert.ToInt32(ss);
                                    //    DataTable dt = new DataTable();
                                    //    dt = ProfCommonBusiness.GetMessage(ss);
                                    //    string messageStatus = dt.Rows[0][0].ToString();
                                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));

                                    //}
                                    //else
                                    //{
                                    //    string ss = (Lbl_Status.Text.Substring(32, (Lbl_Status.Text.Length) - 104));
                                    //    ss = Regex.Replace(ss, @"[^\d]", "");
                                    //    int status = Convert.ToInt32(ss);
                                    //    DataTable dt = new DataTable();
                                    //    dt = ProfCommonBusiness.GetMessage(ss);
                                    //    string messageStatus = dt.Rows[0][0].ToString();
                                    //    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), codeAsanak, PPD.mobile.ToString(), status, messageStatus, int.Parse(PPD.IDAppMessage.ToString()));

                                    //}

                                    bool sentSMS; string smsStatusText;
                                    lbl_Resault.Text = ProfCommonBusiness.sendSMS(PPD.mobile.ToString(), PPD.msg.ToString(), out sentSMS, out smsStatusText);
                                    int asanakStatus = ProfCommonBusiness.getAsanakStatusID(lbl_Resault.Text);
                                    ProfCommonBusiness.LogStatusMessage(PPD.code.ToString(), lbl_Resault.Text, PPD.mobile.ToString(), asanakStatus, smsStatusText, int.Parse(PPD.IDAppMessage.ToString()));

                                }
                                catch (Exception ee)
                                {
                                    RadWindowManager1.RadAlert("خطا در ارسال پیامک", null, 100, "خطا", "");
                                }

                            }
                            j++;
                        }

                    }
                }
                #endregion
            }
        }

        //ramezanian-940602-endo
    }
}






