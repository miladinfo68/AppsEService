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
    public partial class AddOrUpdatePollOptions : System.Web.UI.Page
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

        protected void rgvOptions_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var qid = Request.QueryString["qid"];
            rgvOptions.DataSource = eb.GetOptionsOfPollQuestion(Convert.ToInt32(qid));
        }

        protected void btnAddOption_Click(object sender, EventArgs e)
        {
            rgvOptions.MasterTableView.IsItemInserted = true;
            rgvOptions.Rebind();
        }

        protected void rgvOptions_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var txtPoint = (TextBox)e.Item.FindControl("txtPoint");
            var txtOption = (TextBox)e.Item.FindControl("txtOption");
            if (txtPoint != null && txtOption != null)
            {
                var option = new PollOptionDTO
                {
                    Option = txtOption.Text,
                    Point = Convert.ToInt32(txtPoint.Text),
                    PollQuestionId = Convert.ToInt32(Request.QueryString["qid"])
                };
                if (eb.AddOrUpdatePollOption(option))
                    rgvOptions.Rebind();
            }
        }

        protected void rgvOptions_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            var txtPoint = (TextBox)e.Item.FindControl("txtPoint");
            var txtOption = (TextBox)e.Item.FindControl("txtOption");
            var hdnOptionId = (HiddenField)e.Item.FindControl("hdnOptionId");
            if (txtPoint != null && txtOption != null && hdnOptionId != null)
            {
                var option = new PollOptionDTO
                {
                    Id = Convert.ToInt32(hdnOptionId.Value),
                    Option = txtOption.Text,
                    Point = Convert.ToInt32(txtPoint.Text),
                    PollQuestionId = Convert.ToInt32(Request.QueryString["qid"])
                };
                if (eb.AddOrUpdatePollOption(option))
                    rgvOptions.Rebind();
            }
        }

        protected void rgvOptions_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (eb.DeletePollOption(Convert.ToInt32(e.CommandArgument.ToString())))
                rgvOptions.Rebind();
        }
    }
}