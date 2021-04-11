using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using System.Data;
using System.Text;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class InsertExamOption : System.Web.UI.Page
    {
        ExamBusiness Ebusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx");
            if (!IsPostBack)
            {
                ddl_Calc.Items.Add(new ListItem("انتخاب نمایید", "0"));
                ddl_Calc.Items.Add(new ListItem("می باشد", "1"));
                ddl_Calc.Items.Add(new ListItem("نمی باشد", "2"));
                ddl_Jozve.Items.Add(new ListItem("انتخاب نمایید", "0"));
                ddl_Jozve.Items.Add(new ListItem("می باشد", "1"));
                ddl_Jozve.Items.Add(new ListItem("نمی باشد", "2"));
                txt_ExamTime.Text = "90";
                if (Ebusiness.GetIdgroupBydid(Request.QueryString["did"].ToString()))
                {
                    book_pnl.Visible = true;
                }
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_ExamTime.Text != "" && ddl_Calc.SelectedValue != "0" && ddl_Jozve.SelectedValue != "0")
            {
                if (!CommonBusiness.IsNumeric(txt_ExamTime.Text))
                {
                    rwm.RadAlert("زمان آزمون را صحیح وارد نمایید", null, 100, "خطا ", "");
                }
                else if (Request.QueryString["examTime"].ToString() != "11:00" && (int.Parse(txt_ExamTime.Text) > 90 || int.Parse(txt_ExamTime.Text) < 30))
                {
                    rwm.RadAlert("حداقل زمان آزمون 30 و حداکثر 90 دقیقه می باشد", null, 100, "خطا", "");
                }
                else if (Request.QueryString["examTime"].ToString() == "11:00" && (int.Parse(txt_ExamTime.Text) > 120 || int.Parse(txt_ExamTime.Text) < 30))
                    rwm.RadAlert("حداقل زمان آزمون 30 و حداکثر 120 دقیقه می باشد", null, 100, "خطا", "");
                else
                {
                    if (book_pnl.Visible && rdb_book.SelectedValue == "0")
                    {
                        rwm.RadAlert("شرایط استفاده از کتاب قانون تعیین نشده است", null, 100, "هشدار", "");
                    }
                    else if (book_pnl.Visible && rdb_book.SelectedValue == "1" && txt_book.Text == "")
                        rwm.RadAlert("نام کتاب قانون ذکر نشده است", null, 100, "هشدار", "");
                    else
                    {


                        //if (chk_ans1.Checked || chk_ans2.Checked || chk_ans3.Checked)
                        if (chk_ans1.Checked || chk_ans2.Checked)
                        {
                            string did = Request.QueryString["did"].ToString();
                            //var item = Ebusiness.GetExamQuestionsbyDid(did);
                            //var questionID = item.AsEnumerable().Select(s => s.Field<int>("ID")).FirstOrDefault();

                            bool calc = false;
                            bool Note = false;
                            bool lawbook = false;
                            if (rdb_book.SelectedValue == "1")
                                lawbook = true;
                            if (ddl_Calc.SelectedValue.ToString() == "1")
                                calc = true;
                            else
                                calc = false;
                            if (ddl_Jozve.SelectedValue.ToString() == "1")
                                Note = true;
                            else
                                Note = false;
                            Ebusiness.InsertIntoExamQuestion(did, int.Parse(txt_ExamTime.Text), calc, Note, chk_ans1.Checked, chk_ans2.Checked, false, lawbook, txt_book.Text);
                            //Ebusiness.InsertIntoExamQuestion(did, int.Parse(txt_ExamTime.Text), calc, Note, chk_ans1.Checked, chk_ans2.Checked, chk_ans3.Checked, lawbook, txt_book.Text);
                            cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), 8, 19, "تعیین شرایط ازمون");

                            ScriptManager.RegisterStartupScript(this, GetType(), "btn_Save", "CloseModal();", true);
                        }
                        else
                            rwm.RadAlert("نحوه پاسخگویی به سوالات انتخاب نشده است", null, 100, "هشدار", "");

                    }
                }
            }
            else
            {
                rwm.RadAlert("کلیه آیتم ها تکمیل گردد", null, 100, "پیام", "");
            }
        }
        protected void chk_ans1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ans1.Checked)
            {
                chk_ans2.Enabled = false;
                //chk_ans3.Enabled = false;
                chk_ans2.Checked = false;
                //chk_ans3.Checked = false;
            }
            else
            {
                chk_ans2.Enabled = true;
                //chk_ans3.Enabled = true;               
            }
        }

        protected void chk_ans2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ans2.Checked)
            {
                chk_ans1.Enabled = false;
                chk_ans1.Checked = false;
            }
            else
            {
                //if (!chk_ans3.Checked)               
                //    chk_ans1.Enabled = true;  
                chk_ans1.Enabled = true;
            }
        }

        //protected void chk_ans3_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chk_ans3.Checked)
        //    {
        //        chk_ans1.Enabled = false;
        //        chk_ans1.Checked = false;
        //        lblChk3.Visible=true;
        //    }
        //    else
        //    {
        //        lblChk3.Visible = false;
        //        if (!chk_ans2.Checked)
        //        {
        //            chk_ans1.Enabled = true;    
        //        }
        //    }
        //}

        protected void btn_accept_Click(object sender, EventArgs e)
        {
            desc.Visible = false;
            pnl.Visible = true;
            chk_ans2.Checked = true;
        }


    }
}