using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.DTO.University.Exam;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class UnitAssessmentPoll : System.Web.UI.Page
    {
        ExamBusiness eb = new ExamBusiness();
        int PollId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            var uap = eb.GetPollByTypeAndTerm(1, ConfigurationManager.AppSettings["Exam_Term"].ToString());
            PollId = uap != null ? uap.Id : 0;

            if (!IsPostBack)
            {
                var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                var examPlace = eb.ListExaminerExamPlace(ConfigurationManager.AppSettings["Exam_Term"].ToString(),userId).Rows[0];
                var userAnswers = eb.GetUserPollAnswer(userId, PollId, examPlace["ExamPlaceId"].ToString());

                if (uap.FromDate > DateTime.Now || uap.ToDate < DateTime.Now)
                {
                    pnlOutDate.Visible = true;
                    pnlPollWrapper.Visible = false;
                    pnlNoPoll.Visible = false;
                    pnlVotedMessage.Visible = false;
                }
                else if (userAnswers.Count == 0)
                {

                    pnlPollWrapper.Visible = true;
                    pnlVotedMessage.Visible = false;
                    pnlSuccessMessage.Visible = false;
                    pnlOutDate.Visible = false;
                    GetQuestions();
                    lblUnitName.Text = examPlace["Name_City"].ToString();
                }
                else
                {
                    pnlPollWrapper.Visible = false;
                    pnlNoPoll.Visible = false;
                    pnlOutDate.Visible = false;
                    pnlVotedMessage.Visible = true;
                }
            }
        }

        public void GetQuestions()
        {
            if (PollId > 0)
            {
                var questions = eb.GetQuestionsOfPoll(PollId);
                rptQuestions.DataSource = questions;
                rptQuestions.DataBind();
            }
            else
            {
                pnlPollWrapper.Visible = false;
                pnlVotedMessage.Visible = false;
                pnlNoPoll.Visible = true;
            }
        }
        protected void rptQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var data = (PollQuestionDTO)e.Item.DataItem;
            if (e.Item.ItemType == ListItemType.Header)
            {
                var description = (Label)e.Item.FindControl("lblDescription");
                if (description != null)
                    description.Text = eb.GetPollById(PollId).Description;
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var comments = (Panel)e.Item.FindControl("pnlComment");
                var options = (RadioButtonList)e.Item.FindControl("rblOptions");
                if (comments != null)
                {
                    if (data.NeedComment)
                        comments.Visible = true;
                    else
                        comments.Visible = false;
                }
                if (options != null)
                {
                    options.DataSource = data.PollOptions;
                    options.DataBind();
                }

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                var pnlCommentBox = (Panel)e.Item.FindControl("pnlCommentBox");
                if (pnlCommentBox != null)
                {
                    if (eb.GetPollById(PollId).NeedComment)
                        pnlCommentBox.Visible = true;
                    else
                        pnlCommentBox.Visible = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var res = true;
            var userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            var examPlace = eb.ListExaminerExamPlace(ConfigurationManager.AppSettings["Exam_Term"].ToString(), userId).Rows[0];
            foreach (RepeaterItem item in rptQuestions.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var rblOptions = (RadioButtonList)item.FindControl("rblOptions");
                    if (rblOptions != null)
                    {
                        var answer = new PollAnswerDTO
                        {
                            PollOptionId = Convert.ToInt32(rblOptions.SelectedItem.Value),
                            TargetObject = examPlace["ExamPlaceId"].ToString(),
                            UserId = userId
                        };
                        var question = eb.GetQuestionByOptionId(answer.PollOptionId);
                        if (question.NeedComment)
                        {
                            var txtComments = (TextBox)item.FindControl("txtComments");
                            if (txtComments != null)
                                answer.Comment = txtComments.Text;
                        }
                        if (!eb.AddOrUpdatePollAnswer(answer))
                            res = false;
                    }
                }
            }
            var txtPollComment = (TextBox)rptQuestions.Controls[rptQuestions.Controls.Count - 1].Controls[0].FindControl("txtPollComment");
            if (txtPollComment != null && !string.IsNullOrEmpty(txtPollComment.Text))
            {
                if (!eb.AddOrUpdatePollComment(new PollCommentDTO
                {
                    Comment = txtPollComment.Text,
                    PollId = PollId,
                    TargetObject = examPlace["ExamPlaceId"].ToString(),
                    UserId = userId
                }))
                    res = false;
            }
            if(res)
            {
                pnlPollWrapper.Visible = false;
                lblsuccessMessage.Text = "ارزیابی شما از عملکرد واحد با موفقیت ارسال شد.";
                pnlSuccessMessage.CssClass = "alert alert-success successMessage";
                pnlSuccessMessage.Visible = true;
            }
            else
            {
                lblsuccessMessage.Text = "خطا در ثبت اطلاعات! لطفاً مجدداً تلاش نمائید.";
                pnlSuccessMessage.CssClass = "alert alert-error successMessage";
                pnlSuccessMessage.Visible = true;
            }
        }
    }
}