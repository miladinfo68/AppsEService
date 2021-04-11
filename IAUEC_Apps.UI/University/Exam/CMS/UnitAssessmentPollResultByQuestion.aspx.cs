using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class UnitAssessmentPollResultByQuestion : System.Web.UI.Page
    {
        ExamBusiness eBusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        int PollId = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadForm();
            }
        }

        private void LoadForm()
        {
            var choose = new ListItem { Text = "انتخاب کنید", Value = "-1" };
            ////---------Fill ddlTerms
            //var terms = cmnb.SelectAllTerm();
            //ddlTerms.DataTextField = "tterm";
            //ddlTerms.DataSource = terms;
            //ddlTerms.DataBind();
            //ddlTerms.Items.Insert(0, choose);
            ////---------------------
            //ddlPolls.Items.Insert(0, choose);

            //---------Fill ddlQuestions
            var questions = eBusiness.GetQuestionsOfPoll(PollId);
            ddlQuestions.DataTextField = "Question";
            ddlQuestions.DataValueField = "Id";
            ddlQuestions.DataSource = questions;
            ddlQuestions.DataBind();
            //---------------------
            ddlQuestions.Items.Insert(0, choose);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (ddlTerms.SelectedItem.Value != "-1" && ddlPolls.SelectedItem.Value != "-1" && ddlQuestions.SelectedItem.Value != "-1")
            if(ddlQuestions.SelectedItem.Value != "-1")
            {
                pnlResult.Visible = true;
                rgvResult.DataSource = eBusiness.GetPollAnswersByQuestion(Convert.ToInt32(ddlQuestions.SelectedItem.Value));
                rgvResult.DataBind();
            }
            //else show error
        }

        protected void rgvResult_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "ShowDetails":
                    RadWindowManager editPollWindowManager = new RadWindowManager();
                    RadWindow editPollWindow = new RadWindow();
                    var args = e.CommandArgument.ToString().Split(new string[] { "%%%" }, StringSplitOptions.None);
                    editPollWindow.NavigateUrl = "../CMS/ShowPollAnswerDetails.aspx?cid=" + args[0] + "&t=" + args[1];
                    editPollWindow.ID = "RadWindow1";
                    editPollWindowManager.Width = System.Web.UI.WebControls.Unit.Pixel(800);
                    editPollWindowManager.Height = System.Web.UI.WebControls.Unit.Pixel(500);
                    editPollWindow.VisibleOnPageLoad = true;
                    editPollWindowManager.Windows.Add(editPollWindow);
                    ContentPlaceHolder editPollContentPlaceHolder;
                    editPollContentPlaceHolder =
                     (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                    editPollContentPlaceHolder.Controls.Add(editPollWindow);
                    break;
            }
        }

        protected void ddlTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            //---------Fill ddlPolls
            //var polls = ;// cmnb.SelectAllTerm();
            //ddlPolls.Items.Clear();
            //ddlPolls.DataTextField = "tterm";
            //ddlPolls.DataSource = terms;
            //ddlPolls.DataBind();
            //ddlPolls.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
            //---------------------
        }

        protected void ddlPolls_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}