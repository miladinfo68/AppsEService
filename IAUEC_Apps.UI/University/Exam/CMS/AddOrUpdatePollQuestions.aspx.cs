using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.DTO.University.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class AddOrUpdatePollQuestions : System.Web.UI.Page
    {
        ExamBusiness eb = new ExamBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pid = Request.QueryString["pid"];
                if (string.IsNullOrEmpty(pid))
                    Response.Redirect("/");
            }
        }
        protected void rgvQuestions_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var pid = Request.QueryString["pid"];
            rgvQuestions.DataSource = eb.GetQuestionsOfPoll(Convert.ToInt32(pid));
        }
        
        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            rgvQuestions.MasterTableView.IsItemInserted = true;
            rgvQuestions.Rebind();
        }

        protected void rgvQuestions_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var ddlNeedComment = (DropDownList)e.Item.FindControl("ddlNeedComment");
            var txtQuestion = (TextBox)e.Item.FindControl("txtQuestion");
            if (ddlNeedComment != null && txtQuestion != null)
            {
                var question = new PollQuestionDTO
                {
                    NeedComment = Convert.ToBoolean(Convert.ToInt32(ddlNeedComment.SelectedItem.Value)),
                    Question = txtQuestion.Text,
                    PollId = Convert.ToInt32(Request.QueryString["pid"])
                };
                if (eb.AddOrUpdatePollQuestion(question))
                    rgvQuestions.Rebind();
            }
        }

        protected void rgvQuestions_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var ddlNeedComment = (DropDownList)e.Item.FindControl("ddlNeedComment");
            var txtQuestion = (TextBox)e.Item.FindControl("txtQuestion");
            var hdnQuestionId = (HiddenField)e.Item.FindControl("hdnQuestionId");
            if (ddlNeedComment != null && txtQuestion != null && hdnQuestionId != null)
            {
                var question = new PollQuestionDTO
                {
                    Id = Convert.ToInt32(hdnQuestionId.Value),
                    NeedComment = Convert.ToBoolean(Convert.ToInt32(ddlNeedComment.SelectedItem.Value)),
                    Question = txtQuestion.Text,
                    PollId = Convert.ToInt32(Request.QueryString["pid"])
                };
                if (eb.AddOrUpdatePollQuestion(question))
                    rgvQuestions.Rebind();
            }
        }

        protected void rgvQuestions_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if(e.Item.ItemType == Telerik.Web.UI.GridItemType.EditFormItem)
            {
                var ddlNeedComment = (DropDownList)e.Item.FindControl("ddlNeedComment");
                if(ddlNeedComment != null)
                {
                    var question = (PollQuestionDTO)e.Item.DataItem;
                    if (question.NeedComment)
                        ddlNeedComment.SelectedValue = "1";
                    else
                        ddlNeedComment.SelectedValue = "0";
                }
            }
        }

        protected void rgvQuestions_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (eb.DeletePollQuestion(Convert.ToInt32(e.CommandArgument.ToString())))
                rgvQuestions.Rebind();
        }
    }
}