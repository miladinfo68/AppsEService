using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.DTO.EmailClasses;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.EmailReg.Pages
{
    public partial class CreateEmail : System.Web.UI.Page
    {
        StudentBuisiness EmailBus = new StudentBuisiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                lbl_Student.Text = Session[sessionNames.userID_StudentOstad].ToString();
            }
        }

        protected void btn_CreateEmail_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnB = new CommonBusiness();
            ActiveDirectoryBusiness adB = new ActiveDirectoryBusiness();
            Email_ClassBusiness emB = new Email_ClassBusiness();
            StudentBuisiness stB = new StudentBuisiness();

          
           // bool mobileCheck1 = CommonBusiness.ValidateMobile(txt_Mobile.Text.ToString());           
            
           // bool CheckLetter=false;
            //if (txt_Email.Text != "")
            //    CheckLetter = char.IsLetter(txt_Email.Text.First());
            if (adB.Get_FindUser_SamAccountName(lbl_Student.Text) )
            {
                 RadWindowManager1.RadAlert("شما دارای پست الکترونیکی می باشید ", 500, 200, "پیام", "");
            }
            else if (adB.Get_FindUser_SamAccountName(txt_Email.Text) || emB.CheckEmailName(txt_Email.Text))
                {
                    RadWindowManager1.RadAlert(" پست الکترونیکی  تکراری می باشد، از نام  دیگری استفاده نمایید", 500, 200, "پیام", "");
                    txt_Email.Text = "";
                    txt_Email.Focus();
                }
            else if (!CommonBusiness.IsEnglishLetter(txt_Name.Text))
            {
                RadWindowManager1.RadAlert("لطفا نام خود را انگلیسی وارد نمایید", 500, 200, "پیام", "");
                txt_Name.Text = "";                
            }
            else if (!CommonBusiness.IsEnglishLetter(txt_Family.Text))
            {
                RadWindowManager1.RadAlert("لطفا نام خانوادگی خود را انگلیسی وارد نمایید", 500, 200, "پیام", "");
                txt_Family.Text = "";
            }
            else if (!char.IsLetter(txt_Email.Text.First()))
                RadWindowManager1.RadAlert(" در ابتدای نام پست الکترونیکی نمی توانید از عدد استفاده کنید "
                    , 500, 200, "پیام", "CallBackConfirm1");
            else if (txt_Email.Text.Length < 6 || txt_Email.Text.Length > 26 || cmnB.CheckLettersIsEnglishCharacters(txt_Email.Text) == false)
                RadWindowManager1.RadAlert("نام پست الکترونیکی باید حداقل 6 و حداکثر 25 کاراکتر و نوع حروف انگلیسی باشد"
                    , 500, 200, "پیام", "");
            else if (!cmnB.CheckPasswordIsValidate(txt_Pass.Text, txt_Email.Text))
            {
                RadWindowManager1.RadAlert("کلمه عبور وارد شده مطابق قوانین ذکر شده نمی باشد"
                    , 500, 200, "پیام", "");
                txt_Pass.Text = "";
                txt_Rpass.Text = "";
                txt_Pass.Focus();
            }
            else if (txt_SEmail.Text.ToString() != "" && !CommonBusiness.ValidateEmail(txt_SEmail.Text.ToString()))
            {
                RadWindowManager1.RadAlert("پست الکترونیکی دوم را درست وارد نمایید", 500, 200, "پیام", "");
                txt_SEmail.Text = "";
                txt_SEmail.Focus();
            }
            //   else if(txt_Pass.Text.Contains(txt_Email.Text))
            //{
            //    RadWindowManager1.RadAlert("از پست الکترونیکی نباید در پسورد استفاده گردد", 500, 200, "پیام", "");
    
            //   }
            else
            {
                if (btn_SelectType.SelectedItem.Value == "1" && CommonBusiness.ValidateMobile(txt_Mobile.Text.ToString()) == false)
                {
                    RadWindowManager1.RadAlert("لطفا شماره موبایل را بدرستی وارد نمایید", 500, 200, "پیام", "");
                    txt_Mobile.Text = "";
                }
                else if (btn_SelectType.SelectedItem.Value == "0" && txt_SEmail.Text == "")
                {
                    RadWindowManager1.RadAlert("لطفا پست الکترونیکی  را وارد نمایید", 500, 200, "پیام", "");
                }
                else if (btn_SelectType.SelectedItem.Value == "2" && (txt_SEmail.Text == "" || CommonBusiness.ValidateMobile(txt_Mobile.Text.ToString()) == false))
                    RadWindowManager1.RadAlert("لطفا  پست الکترونیکی و شماره موبایل را وارد نمایید", 500, 200, "پیام", "");
                else
                {
                      try
                        {
                            Email_Class Email_Class = EmailGetInfo(txt_Email.Text.ToLower());
                          //
                            DataTable dtsemail = new DataTable();
                            dtsemail = EmailBus.GetEmailRequestStatus(Session[sessionNames.userID_StudentOstad].ToString());
                            if (dtsemail.Rows.Count > 0 && dtsemail.Rows[dtsemail.Rows.Count - 1]["Status"].ToString() != "2")
                            {
                                RadWindowManager1.RadAlert("شما قبلا درخواست داده اید", 500, 200, "پیام", "");

                            }
                            //
                            else
                            {
                                emB.Create_Email(Email_Class);

                                if (chk_Mobile.Checked)
                                    stB.Update_Mobile(lbl_Student.Text, txt_Mobile.Text);
                                Session["Email"] = txt_Email.Text.ToString();

                                DataTable dthasSt = new DataTable();
                                dthasSt = stB.GetstFromStudentSupInfo(Session[sessionNames.userID_StudentOstad].ToString());
                                if (dthasSt.Rows.Count == 0)
                                    stB.InsertIntoStudentSupInfo(Session[sessionNames.userID_StudentOstad].ToString(), txt_Name.Text, txt_Family.Text);
                                txt_Email.Text = "";
                                txt_Mobile.Text = "";
                                txt_Pass.Text = "";
                                txt_Rpass.Text = "";
                                txt_SEmail.Text = "";
                                cmnB.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 18, "ثبت با موفقیت درخواست ایمیل");

                                RadWindowManager1.RadAlert(" درخواست شما با موفقیت ثبت گردیده و در حال بررسی می باشد" +
                                   "نتیجه آن حداکثر پس از سه روز کاری به اطلاع شما خواهد رسید", 500, 200, "پیام", "CallBackConfirmok");
                            }
                        }
                        catch (Exception)
                        {
                            RadWindowManager1.RadAlert(" ثبت ناموفق بود، مجددا پست الکترونیکی خود را وارد نمایید", 500, 200, "پیام", "");
                        }
                   
                    txt_Email.Text = "";
                    txt_Mobile.Text = "";
                    txt_Pass.Text = "";
                    txt_Rpass.Text = "";
                    txt_SEmail.Text = "";
                }
            }
           
        }

        public Email_Class EmailGetInfo(string Email_Edit)
        {
            UserAccessBusiness userB = new UserAccessBusiness();
            Email_Class Email_Class = new Email_Class();
            Email_Class.Stcode = lbl_Student.Text;
            Email_Class.Email_Address = Email_Edit;
            Email_Class.Password = userB.EncryptPass(txt_Pass.Text);
            Email_Class.Date_Save = DateTime.Now;
            Email_Class.CEMAIL = txt_SEmail.Text;
            Email_Class.Mobile = txt_Mobile.Text;
            if (btn_SelectType.SelectedItem.Value == "1")
                Email_Class.ConnectType = 1;
            else if (btn_SelectType.SelectedItem.Value == "2")
                Email_Class.ConnectType = 2;
            else
                Email_Class.ConnectType = 0;
            if (chk_Email.Checked)
                Email_Class.UpdateEmail = true;
            else
                Email_Class.UpdateEmail = false;
            return Email_Class;
        }

              
        
    }
}