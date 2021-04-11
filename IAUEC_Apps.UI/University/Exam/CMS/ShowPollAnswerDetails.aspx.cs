using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.DTO.University.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ShowPollAnswerDetails : System.Web.UI.Page
    {
        List<PollAnswerDTO> answers = new List<PollAnswerDTO>();
        PollCommentDTO comment = new PollCommentDTO();
        ExamBusiness eb = new ExamBusiness();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cid = Request.QueryString["cid"];
                var t = Request.QueryString["t"];
                if (!string.IsNullOrEmpty(cid) && !string.IsNullOrEmpty(t))
                    LoadAnswerDetails(Convert.ToInt32(cid), t);
                else
                {
                    //close and rebind
                }
            }
           
        }

        private void LoadAnswerDetails(int cityId, string term)
        {
            var PollId = eb.GetPollByTypeAndTerm(1, Request.QueryString["t"]).Id;
            answers = eb.GetPollAnswersByTermAndCityId(term: term, cityId: cityId, showDetails: true);
            comment = eb.GetPollComment(pollId: PollId, targetId: cityId);
            rptQuestions.DataSource = eb.GetQuestionsOfPoll(PollId);
            rptQuestions.DataBind();
        }

        protected void rptQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var PollId = eb.GetPollByTypeAndTerm(1, Request.QueryString["t"]).Id;
            var data = (PollQuestionDTO)e.Item.DataItem;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var comments = (Panel)e.Item.FindControl("pnlComment");
                //var options = (RadioButtonList)e.Item.FindControl("rblOptions");
                var lblAnswer = (Label)e.Item.FindControl("lblAnswer");
                if (comments != null)
                {
                    if (data.NeedComment)
                        comments.Visible = true;
                    else
                        comments.Visible = false;
                }
                if (lblAnswer != null)
                {
                    //options.DataSource = data.PollOptions;
                    //options.DataBind();
                    var answer = answers.Select(s => s.PollOptionId).Intersect(data.PollOptions.Select(s => s.Id));
                    if (answer.Count() > 0)
                    {
                        var option = data.PollOptions.FirstOrDefault(w => w.Id == answer.FirstOrDefault());
                        lblAnswer.Text = option.Option + " (" + option.Point + " امتیاز)";
                    }
                    else
                        lblAnswer.Text = "بدون پاسخ";
                    //options.SelectedValue = answer.FirstOrDefault().ToString();
                }

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                var pnlCommentBox = (Panel)e.Item.FindControl("pnlCommentBox");
                var lblPollComment = (Label)e.Item.FindControl("lblPollComment");
                if (pnlCommentBox != null && lblPollComment != null)
                {
                    if (eb.GetPollById(PollId).NeedComment)
                    {
                        lblPollComment.Text = comment.Comment;
                        pnlCommentBox.Visible = true;
                    }
                    else
                        pnlCommentBox.Visible = false;
                }
            }
        }
    }
}