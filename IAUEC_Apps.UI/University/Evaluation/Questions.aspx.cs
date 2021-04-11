using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Evaluation
{
    public partial class Questions : System.Web.UI.Page
    {
        private static readonly EvaluationBusiness _evaluationBusiness = new EvaluationBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("../../CommonUI/login.aspx");
            if (!IsPostBack)
            {
                //if (Request.Form["Method"] == "GetQuestions")
                //{
                //    GetQuestions();
                //    return;
                //}
                ContentPlaceHolder c = Page.Master.FindControl("PageTitle") as ContentPlaceHolder;
                ContentPlaceHolder cb = Page.Master.FindControl("SubPageTitle") as ContentPlaceHolder;
                HiddenField uf = Page.Master.FindControl("UserIdField") as HiddenField;
                LiteralControl l = new LiteralControl();
                LiteralControl lb = new LiteralControl();
                HiddenField luf = new HiddenField();
                uf.Value= Session[sessionNames.userID_StudentOstad].ToString();
                l.Text = "دانشجوی گرامی";//"لطفا تمامی سوالات را پاسخ دهید";
                lb.Text = "به منظور رفع مشکلات احتمالی در روند برگزاری آزمون الکترونیکی و بهبود شرایط برگزاری آزمون ها در ترم های آینده، لطفا فرم نظرسنجی را تکمیل نمایید تا گامی دیگر در جهت افزایش ضریب رضایتمندی شما برداریم.";//"این فرم به منظور اطلاع از میزان رضایت شما از کیفیت خدمات ارائه شده به حضورتان تقدیم می گردد. خواهشمند است با تکمیل آن، ما را در خدمات رسانی بهتر یاری نمایید.";
                c.Controls.Add(l);
                cb.Controls.Add(lb);
                //uf.Controls.Add(luf);
            }
           
           
        }
        [WebMethod]
        public static bool IsUserEvaluated(int userId)
        {
            return _evaluationBusiness.IsUserevaluated(userId);
        }
        [WebMethod]
        public static List<QuestionDTO> GetQuestions()
        {
            return _evaluationBusiness.GetQuestions();
        }
        [WebMethod]
        public static bool InsertStudentAnswer(string[] Answers, string Comment,int userId)
        {
            var isUserevaluated = _evaluationBusiness.IsUserevaluated(userId);
            if (!isUserevaluated)
            {
                var studentAnswerList = Answers.ToModel(userId);
                try
                {
                    _evaluationBusiness.InsertStudentAnswers(studentAnswerList);
                    _evaluationBusiness.InsertStudentComment(Comment, userId);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;

        }
        public class Model
        {
            public string[] Answers { get; set; }
            public string[] LastAnswer { get; set; }
            public string Comments { get; set; }
        }
    }
}