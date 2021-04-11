using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class UnitAssessmentPollResult : System.Web.UI.Page
    {
        ExamBusiness eBusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
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
            pnlResult.Visible = false;

            //---------Fill ddlUnits
            var units = eBusiness.GetAllExamPlaceAddress().AsEnumerable()
                .Where(w => w.Field<bool>("IsActive"))
                .Select(s => new { Name = s.Field<string>("Name_City"), Id = s.Field<decimal>("ID") });
            ddlUnits.DataSource = units;
            ddlUnits.DataTextField = "Name";
            ddlUnits.DataValueField = "Id";
            ddlUnits.DataBind();
            ddlUnits.Items.Insert(0, choose);
            //---------------------

            //---------Fill ddlTerms
            var terms = cmnb.SelectAllTerm();
            ddlTerms.DataTextField = "tterm";
            ddlTerms.DataSource = terms;
            ddlTerms.DataBind();
            ddlTerms.Items.Insert(0, choose);
            //---------------------
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var term = string.Empty;
            var cityId = 0;
            if (ddlTerms.SelectedIndex > 0)
                term = ddlTerms.SelectedItem.Value;
            if (ddlUnits.SelectedIndex > 0)
                cityId = Convert.ToInt32(ddlUnits.SelectedItem.Value);
            var answers = eBusiness.GetPollAnswersByTermAndCityId(term: term, cityId: cityId);
            pnlResult.Visible = true;
            rgvResult.DataSource = answers;
            rgvResult.DataBind();
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

    }
}