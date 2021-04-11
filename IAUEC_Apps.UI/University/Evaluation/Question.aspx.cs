using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Evaluation
{
    public partial class Question : System.Web.UI.Page
    {
        private readonly EvaluationBusiness _evaluationBusiness = new EvaluationBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            ///_evaluationBusiness.send();
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("../../CommonUI/login.aspx");
            if (!IsPostBack)
            {
                ContentPlaceHolder c = Page.Master.FindControl("PageTitle") as ContentPlaceHolder;
                LiteralControl l = new LiteralControl();
                l.Text = "لطفا تمامی سوالات را پاسخ دهید";
                c.Controls.Add(l);
                var questions = _evaluationBusiness.GetQuestions();
                rptQuestions.DataSource = questions;
                rptQuestions.DataBind();
            }
        }

        protected void rptQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var data = (QuestionDTO)e.Item.DataItem;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var comments = (Panel)e.Item.FindControl("pnlComment");
                var options = (RadioButtonList)e.Item.FindControl("rblOptions");
                if (comments != null)
                {
                    if (data.Description != null)
                        comments.Visible = true;
                    else
                        comments.Visible = false;
                }
                if (options != null)
                {
                    options.DataSource = data.AnswerOfQuestions;
                    options.DataBind();
                }

            }
          
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var res = true;
            var userId =Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            if (userId == 0 || userId == null)
                return;
            var isOstad = Session["IsOstad"];
            foreach (RepeaterItem item in rptQuestions.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    var rblOptions = (RadioButtonList)item.FindControl("rblOptions");
                    var hdnQuestionId = (HiddenField)item.FindControl("hdnQuestionId");
                    var hdnTerm = (HiddenField)item.FindControl("hdnTerm");
                    if (rblOptions != null && hdnQuestionId != null)
                    {
                        var answer = new StudentAnswerOfQuestionDTO
                        {
                            AnswerId = Convert.ToInt32(rblOptions.SelectedItem.Value),
                            UserId = userId,
                            QuestionId = Convert.ToInt32(hdnQuestionId.Value),
                            Term = hdnTerm.Value
                        };
                        if (!_evaluationBusiness.InsertStudentAnswer(answer))
                            res = false;
                    }
                }
            }

            if (res)
            {
                pnlPollWrapper.Visible = false;
                lblsuccessMessage.Text = "ارزیابی شما با موفقیت ارسال شد.";
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