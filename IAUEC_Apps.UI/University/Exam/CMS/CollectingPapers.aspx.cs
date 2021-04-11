using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class CollectingPapers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/loginRequestCMS.aspx");
            if (!IsPostBack)
            {
                ExamBusiness EBusiness = new ExamBusiness();


                DataTable dtExamDate = new DataTable();
                dtExamDate = EBusiness.Get_Exam_dateexam();
                ddl_ExamDate.DataSource = dtExamDate;
                ddl_ExamDate.DataTextField = "dateexam";
                ddl_ExamDate.DataValueField = "dateexam";
                ddl_ExamDate.DataBind();
                ddl_ExamDate.Items.Insert(0, new ListItem("انتخاب کنید"));

            }
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
                Session[sessionNames.menuID] = menuId;
            }
            AccessControl.MenuId = Session[sessionNames.menuID].ToString();
            AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
        }

        protected void ddl_ExamDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExamBusiness EBusiness = new ExamBusiness();
            DataTable dtsaat = new DataTable();
            dtsaat = EBusiness.GetSaatExamByDateExam(ddl_ExamDate.SelectedValue);
            ddl_Saate.DataSource = dtsaat;
            ddl_Saate.DataTextField = "saatexam";
            ddl_Saate.DataBind();
        }

        protected void btn_Show_Click(object sender, EventArgs e)
        {
            if (ddl_ExamDate.SelectedIndex != 0)
            {
                ExamBusiness EBusiness = new ExamBusiness();
                DataTable dt = new DataTable();
                dt = EBusiness.GetExamQuestionsUploadedByDate_Saat( ddl_Saate.SelectedValue.ToString(),ddl_ExamDate.SelectedValue.ToString());
                if (dt.Rows.Count > 0)
                {
                    grd_CourseList.DataSource = dt;
                    grd_CourseList.DataBind();
                }
                else { rwm.RadAlert("دراین سانس کلاسی موجود نیست", null, 100, "پیام", ""); }
            }
            else
            {
                rwm.RadAlert("روز امتحان را انتخاب نمایید", null, 100, "پیام", "");
            }


        }

        protected void grd_CourseList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                Button btn_collecting = e.Item.FindControl("btn_collectPapers") as Button;
                Button btn_delivery = e.Item.FindControl("btn_deliverPapers") as Button;
                HiddenField hdn_status = e.Item.FindControl("hdn_status") as HiddenField;
                if (int.Parse(hdn_status.Value) == 3)
                {
                    btn_collecting.Enabled = true;
                    btn_delivery.Enabled = false;
                    
                }
                else if (int.Parse(hdn_status.Value) == 5)
                {
                    btn_collecting.CssClass = "btn btn-warning";
                    btn_collecting.Enabled = true;
                    btn_delivery.Enabled = true;
                }
                else
                {
                    btn_collecting.CssClass = "btn btn-defualt";
                    btn_collecting.Enabled = false;
                    btn_delivery.Enabled = false;
                }
                btn_collecting.CommandArgument = item["did"].Text;
                btn_delivery.CommandArgument = item["did"].Text;
            }
        }

        protected void grd_CourseList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            ExamBusiness examBusiness = new ExamBusiness();
            CommonBusiness cmnb = new CommonBusiness();
            int status_Paper,eventId;
            string desc;
            string did = e.CommandArgument.ToString();
            if (e.CommandName=="collecting")
            {
                status_Paper = 5;
                desc = "تجمیع اوراق";
                eventId=121;

                

                
                try
                {
                    
                    DataTable dtResault = examBusiness.GetMobileProfByDid(did);

                    //string Text = "استاد ارجمند به استحضار می رساند پاکت  پاسخ نامه های کد مشخصه " + e.CommandArgument.ToString() + " آماده تحویل می باشد، خواهشمند است با مدیر محترم دانشکده ارتباط و اقدام لازم بعمل آورید.  " + "\r\n" + "اداره امتحانات واحد الکترونیکی دانشگاه آزاد اسلامی";
                    //string Text = "استاد ارجمند،\r\n به استحضار می رساند پاکت  پاسخ نامه های کد مشخصه " + e.CommandArgument.ToString() + "آماده تحويل مي باشد. لطفاً جهت هماهنگي هاي لازم در اسرع وقت با شماره تماس مربوط به دانشكده خود ارتباط برقرار نماييد :."
                    //    + "\r\n" + "دانشكده انساني: 02142863721"
                    //    + "\r\n" + "دانشكده مديريت: 02142863722"
                    //    + "\r\n" + "دانشكده علوم پايه: 02142863723"
                    //    + "\r\n" + "دانشكده فني مهندسي: 02142863724";

                    string Text = "استاد ارجمند،\r\n به استحضار می رساند پاکت  پاسخ نامه های کد مشخصه " + e.CommandArgument.ToString() + "آماده تحويل مي باشد. لطفاً جهت هماهنگي هاي لازم در اسرع وقت با مدير و يا كارشناسان دانشكده خود ارتباط برقرار  نماييد :.";
                   
                    string asanak, statusStr;

                    if (dtResault.Rows[0]["mobile"].ToString() != null || dtResault.Rows[0]["mobile"].ToString() != "")
                    {
                        //result = cmnb.SendSMSByMobile(dtResault.Rows[0]["mobile"].ToString(), Text, username, pass, source, uri);
                        // result = cmnb.sendSMS(dtResault.Rows[0]["mobile"].ToString(), Text);
                        // string codeAsanak = result.Substring(1, (result.Length) - 2);
                        // statusStr = cmnb.ShowStatusSMS(codeAsanak);
                        // if (statusStr.Substring(12, (statusStr.Length) - 15) == "NotFound")
                        //{
                        //    string ss = "-1";
                        //    int status = Convert.ToInt32(ss);
                        //    DataTable dt = new DataTable();
                        //    dt = cmnb.GetMessage(ss);
                        //    string messageStatus = dt.Rows[0][0].ToString();
                        //    //cmnb.LogStatusMessage(dtResault.Rows[0]["code_ostad"].ToString(), codeAsanak, dtResault.Rows[0]["mobile"].ToString(), status, messageStatus, int.Parse(IdAppMessage));
                        //}
                        //else
                        //{
                        //    string ss = (statusStr.Substring(32, (statusStr.Length) - 104));
                        //    ss = Regex.Replace(ss, @"[^\d]", "");
                        //    int status = Convert.ToInt32(ss);
                        //    DataTable dt = new DataTable();
                        //    dt = cmnb.GetMessage(ss);
                        //    string messageStatus = dt.Rows[0][0].ToString();
                        //    // cmnb.LogStatusMessage(dtResault.Rows[0]["code_ostad"].ToString(), codeAsanak, dtResault.Rows[0]["mobile"].ToString(), status, messageStatus, int.Parse(IdAppMessage));
                        //}

                        bool sentSMS; string smsStatusText;
                        asanak = cmnb.sendSMS(dtResault.Rows[0]["mobile"].ToString(), Text, out sentSMS, out smsStatusText);
                        int asanakStatus = cmnb.getAsanakStatusID(asanak);
                        cmnb.LogStatusMessage(dtResault.Rows[0]["code_ostad"].ToString(), asanak, dtResault.Rows[0]["mobile"].ToString(), asanakStatus, smsStatusText, 8);

                    }
                    rwm.RadAlert("ارسال پیامک برای کد مشخه"+e.CommandArgument.ToString()+"با موفقیت انجام شد",null,null,"پیام","");
                }
                catch
                {
                    rwm.RadAlert("خطا در ارسال پیامک برای کد مشخه" + e.CommandArgument.ToString() , null, null, "پیام", "");
                }
            


            }
            else
            {
                status_Paper = 6;
                 desc = "تحویل اوراق";
                eventId=122;
            }


            examBusiness.UpdateQueizStatus(status_Paper, did, "");
                  
            var item = examBusiness.GetExamQuestionsbyDid(did);
            var questionID = item.AsEnumerable().Select(s => s.Field<int>("examQuestionID")).FirstOrDefault();
         
            //cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 167, dt.Rows[0]["coursecode"].ToString() + "دریافت اوراق", int.Parse(dt.Rows[0]["QuestionId"].ToString()));


            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), eventId, desc, questionID);
            grd_CourseList.DataSource = examBusiness.GetExamQuestionsUploadedByDate_Saat(ddl_Saate.SelectedValue.ToString(), ddl_ExamDate.SelectedValue.ToString());
            grd_CourseList.DataBind();
        }

     
    }
}