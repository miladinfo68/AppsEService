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
    public partial class ManagePolls : System.Web.UI.Page
    {
        ExamBusiness eb = new ExamBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void rgvPolls_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var polls = eb.GetAllPolls();
            rgvPolls.DataSource = polls;
        }

        protected void btnAddPoll_Click(object sender, EventArgs e)
        {
            RadWindowManager rwmAddPoll = new RadWindowManager();
            RadWindow editPollWindow = new RadWindow();
            editPollWindow.NavigateUrl = "../CMS/AddOrUpdatePoll.aspx";
            editPollWindow.ReloadOnShow = true;
            editPollWindow.ID = "RadWindow3";
            editPollWindow.DestroyOnClose = true;
            editPollWindow.ShowContentDuringLoad = false;
            editPollWindow.ReloadOnShow = true;
            rwmAddPoll.Width = Unit.Pixel(800);
            rwmAddPoll.Height = Unit.Pixel(500);
            editPollWindow.VisibleOnPageLoad = true;
            rwmAddPoll.Windows.Add(editPollWindow);
            ContentPlaceHolder editPollContentPlaceHolder;
            editPollContentPlaceHolder =
             (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            editPollContentPlaceHolder.Controls.Add(editPollWindow);
        }

        protected void rgvPolls_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditPoll":
                    RadWindowManager rwmEditPoll = new RadWindowManager();
                    RadWindow editPollWindow = new RadWindow();
                    editPollWindow.NavigateUrl = "../CMS/AddOrUpdatePoll.aspx?pid=" + e.CommandArgument.ToString();
                    editPollWindow.ReloadOnShow = true;
                    editPollWindow.ID = "RadWindow1";
                    editPollWindow.DestroyOnClose = true;
                    editPollWindow.ShowContentDuringLoad = false;
                    editPollWindow.ReloadOnShow = true;
                    rwmEditPoll.Width = Unit.Pixel(800);
                    rwmEditPoll.Height = Unit.Pixel(500);
                    editPollWindow.VisibleOnPageLoad = true;
                    rwmEditPoll.Windows.Add(editPollWindow);
                    ContentPlaceHolder editPollContentPlaceHolder;
                    editPollContentPlaceHolder =
                     (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                    editPollContentPlaceHolder.Controls.Add(editPollWindow);
                    rwmEditPoll.Windows.Clear();
                    break;
                case "EditQuestion":
                    RadWindowManager rwmEditQuestion = new RadWindowManager();
                    RadWindow editQuestionWindow = new RadWindow();
                    editQuestionWindow.NavigateUrl = "../CMS/AddOrUpdatePollQuestions.aspx?pid=" + e.CommandArgument.ToString();
                    editQuestionWindow.ReloadOnShow = true;
                    editQuestionWindow.ID = "RadWindow2";
                    editQuestionWindow.DestroyOnClose = true;
                    editQuestionWindow.ShowContentDuringLoad = false;
                    editQuestionWindow.ReloadOnShow = true;
                    rwmEditQuestion.Width = Unit.Pixel(900);
                    rwmEditQuestion.Height = Unit.Pixel(500);
                    editQuestionWindow.VisibleOnPageLoad = true;
                    rwmEditQuestion.Windows.Add(editQuestionWindow);
                    ContentPlaceHolder editQuestionContentPlaceHolder;
                    editQuestionContentPlaceHolder =
                     (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                    editQuestionContentPlaceHolder.Controls.Add(editQuestionWindow);
                    rwmEditQuestion.Windows.Clear();
                    break;
                case "CopyPoll":
                    RadWindowManager rwmCopyPoll = new RadWindowManager();
                    RadWindow copyPollWindow = new RadWindow();
                    copyPollWindow.NavigateUrl = "../CMS/CopyPoll.aspx?pid=" + e.CommandArgument.ToString();
                    copyPollWindow.ReloadOnShow = true;
                    copyPollWindow.ID = "RadWindow4";
                    copyPollWindow.DestroyOnClose = true;
                    copyPollWindow.ShowContentDuringLoad = false;
                    copyPollWindow.ReloadOnShow = true;
                    rwmCopyPoll.Width = Unit.Pixel(800);
                    rwmCopyPoll.Height = Unit.Pixel(80);
                    copyPollWindow.VisibleOnPageLoad = true;
                    rwmCopyPoll.Windows.Add(copyPollWindow);
                    ContentPlaceHolder copyPollContentPlaceHolder;
                    copyPollContentPlaceHolder =
                     (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                    copyPollContentPlaceHolder.Controls.Add(copyPollWindow);
                    break;
                case "DeletePoll":
                    if(eb.DeletePoll(Convert.ToInt32(e.CommandArgument.ToString())))
                        rgvPolls.Rebind();
                    break;
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                var polls = eb.GetAllPolls();
                rgvPolls.DataSource = polls;
                rgvPolls.Rebind();
            }
        }
    }
}