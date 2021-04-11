using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class AssignExamClassParticipants : System.Web.UI.Page
    {
        ExamBusiness ExamBusiness = new ExamBusiness();
        private readonly LoginBusiness _loginBusiness = new LoginBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid(true);
                BindCityList();
                BindUsersList();
                ClearCustomExaminerBox();
            }
        }
        protected void BindUsersList()
        {
            ddlExaminerName.DataSource = ExamBusiness.GetExaminer();
            ddlExaminerName.DataTextField = "UserName";
            ddlExaminerName.DataValueField = "ExaminerID";
            ddlExaminerName.DataBind();
            ddlExaminerName.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
        }
        protected void BindCityList()
        {
            var cityName = ExamBusiness.GetAllExamPlaceAddress().AsEnumerable()
                .Where(w => w.Field<bool>("IsActive"))
                .Select(s => new { Name = s.Field<string>("Name_City"), Id = s.Field<int>("ID") });
            ddlExaminerPlaceId.DataSource = cityName;
            ddlExaminerPlaceId.DataTextField = "Name";
            ddlExaminerPlaceId.DataValueField = "Id";
            ddlExaminerPlaceId.DataBind();
            ddlExaminerPlaceId.Items.Insert(0, "انتخاب نمایید");
            ddlExaminerPlaceId.SelectedIndex = 0;
        }
        protected void BindGrid(bool setSessions = false)
        {
            gvParticipants.DataSource = ExamBusiness.ListAllExamClassParticipants();
            gvParticipants.DataBind();
        }

        //protected void btnSaveList_Click(object sender, EventArgs e)
        //{
        //    foreach(var item in gvParticipants.Items)
        //    {
        //        var chkAssign = (CheckBox)((Telerik.Web.UI.GridItem)item).FindControl("chkAssign");
        //        var hdnExaminerID = (HiddenField)((Telerik.Web.UI.GridItem)item).FindControl("hdnExaminerID");
        //        var hdnExaminerName = (HiddenField)((Telerik.Web.UI.GridItem)item).FindControl("hdnExaminerName"); 
        //        var hdnExamPlaceID = (HiddenField)((Telerik.Web.UI.GridItem)item).FindControl("hdnExamPlaceID"); 
        //        var hdnUserName = (HiddenField)((Telerik.Web.UI.GridItem)item).FindControl("hdnUserName"); 
        //        var hdnPassword = (HiddenField)((Telerik.Web.UI.GridItem)item).FindControl("hdnPassword"); 
        //        var hdnEPass = (HiddenField)((Telerik.Web.UI.GridItem)item).FindControl("hdnEPass"); 
        //        if (chkAssign != null && hdnExaminerID != null && hdnExaminerName != null && hdnExamPlaceID != null && hdnUserName != null && hdnPassword != null)
        //        {
        //            int? examinerPlaceID;
        //            if (string.IsNullOrEmpty(hdnExamPlaceID.Value)) examinerPlaceID = null;
        //            else examinerPlaceID = Convert.ToInt32(hdnExamPlaceID.Value);

        //            var examinerId = Convert.ToInt32(hdnExaminerID.Value);

        //            //if (ExamBusiness.ListExaminerExamPlace(examinerId).Rows.Count == 0)
        //            //    ExamBusiness.Insert_ExaminerInfo(examinerId, Convert.ToInt32(examinerPlaceID), hdnExaminerName.Value,
        //            //        Mobile: string.Empty,
        //            //        Email: string.Empty,
        //            //        StartDate: string.Empty,
        //            //        EndDate: string.Empty,
        //            //        pass: string.Empty,
        //            //        UserId: examinerId);

        //            if (chkAssign.Checked)
        //                ExamBusiness.AddOrUpdateExamClassParticipants(
        //                    examinerId: Convert.ToInt32(hdnExaminerID.Value),
        //                    examinerName: hdnExaminerName.Value,
        //                    examinerPlaceId: examinerPlaceID,
        //                    examinerUserName: hdnUserName.Value,
        //                    password: hdnPassword.Value,
        //                    ePass: hdnEPass.Value);

        //            else if (((List<int>)Session["Participants"]).Contains(Convert.ToInt32(hdnExaminerID.Value)))
        //                ExamBusiness.DeleteExamClassParticipants(Convert.ToInt32(hdnExaminerID.Value));
        //        }
        //    }
        //    BindGrid(true);
        //}

        protected void btnAddExaminer_Click(object sender, EventArgs e)
        {
            var examiner = ExamBusiness.ListAllExamClassParticipants(Convert.ToInt32(ddlExaminerName.SelectedItem.Value));
            if (examiner.Rows.Count == 0)
            {
                var user = ExamBusiness.GetExaminer().AsEnumerable().Where(w => w.Field<int>("ExaminerId") == Convert.ToInt32(ddlExaminerName.SelectedItem.Value)).FirstOrDefault();
                var dt = ExamBusiness.ListExaminerExamPlace(ConfigurationManager.AppSettings["Exam_Term"].ToString(), Convert.ToInt32(user["ExaminerId"]));
                ExamBusiness.AddOrUpdateExamClassParticipants(
                                examinerId: Convert.ToInt32(ddlExaminerName.SelectedItem.Value),
                                examinerName: user["ExaminerName"].ToString(),
                                examinerPlaceId: Convert.ToInt32(user["ExamPlaceID"]),
                                examinerUserName: user["UserName"].ToString(),
                                password: user["UserName"].ToString() + "@1398$",
                                ePass: txtLmsPass.Text);

                BindGrid();
                RadWindowManager1.RadAlert("ثبت با موفقیت انجام شد.", 400, 100, "پیام سیستم", null);
            }
            else
                RadWindowManager1.RadAlert("ممتحن قبلا برای دوره جاری ثبت شده است.", 400, 100, "پیام سیستم", null);
        }
        protected void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            var randomPass = RandomString();
            txtGeneratePassword.Text = randomPass;
            txtLmsPass.Attributes.Add("value", randomPass);
        }
        protected void btnLMSGeneratePassword_Click(object sender, EventArgs e)
        {
            var randomPass = RandomString();
            txtLMSGeneratePassword.Text = randomPass;
            txtPassword.Attributes.Add("value", randomPass);
        }
        protected void btnPasswordReset_Click(object sender, EventArgs e)
        {
            var isDone = true;
            var userIds = GetUserByRoleId();
            foreach (var userId in userIds)
            {
                var randomPass = RandomString();
                var res = ChangePasswordAndEnable(randomPass, userId);
                if (!res)
                {
                    lbl_Message.Text = "خطا در انجام عملیات";
                }
                _loginBusiness.UpdateExaminerPalcePasswordByExaminerID(randomPass, userId);
            }
            if (!isDone)
                lbl_ErrprMessage.Text = "خطا در انجام عملیات";
            lbl_Message.Text = "عملیات با موفقیت انجام شد.";
            
        }

        private static string RandomString(int length = 8)
        {
            const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rnd = new Random();
            var chars = Enumerable.Range(0, length)
                .Select(x => pool[rnd.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }

        private List<int> GetUserByRoleId(int roleId = 34)
        {
            return _loginBusiness.GetUserIdsByRoleId(roleId);
        }

        private bool ChangePasswordAndEnable(string password, int userId)
        {
            try
            {
                _loginBusiness.ChangePasswordAndEnable(CommonBusiness.EncryptPass(password), userId);
                return true;
            }
            catch
            {
                return false;

            }
        }



        protected void btnAddCustomExaminer_Click(object sender, EventArgs e)
        {
            ExamBusiness.AddOrUpdateExamClassParticipants(
                                id: string.IsNullOrEmpty(hdnItemId.Value) ? 0 : Convert.ToInt32(hdnItemId.Value),
                                examinerId: -1,
                                examinerName: txtExaminerName.Text,
                                examinerPlaceId: Convert.ToInt32(ddlExaminerPlaceId.SelectedItem.Value),
                                examinerUserName: txtUserName.Text,
                                password: txtPassword.Text);

            BindGrid();
            ClearCustomExaminerBox();
            RadWindowManager1.RadAlert("ثبت با موفقیت انجام شد.", 400, 100, "پیام سیستم", null);
        }

        protected void gvParticipants_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            gvParticipants.DataSource = ExamBusiness.ListAllExamClassParticipants();
        }

        protected void gvParticipants_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditCustome":
                    hdnItemId.Value = e.CommandArgument.ToString();
                    var examiner = ExamBusiness.ListAllExamClassParticipants(id: Convert.ToInt32(e.CommandArgument)).Rows[0];
                    txtExaminerName.Text = examiner["ExaminerName"].ToString();
                    ddlExaminerPlaceId.SelectedValue = examiner["ExaminerPalceId"].ToString();
                    txtUserName.Text = examiner["ExaminerUserName"].ToString();
                    txtPassword.Text = examiner["password"].ToString();
                    btnCancelEdit.Visible = true;
                    break;
                case "DeleteExaminer":
                    ExamBusiness.DeleteExamClassParticipants(id: Convert.ToInt32(e.CommandArgument));
                    BindGrid();
                    break;
            }
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            ClearCustomExaminerBox();
        }
        private void ClearCustomExaminerBox()
        {
            hdnItemId.Value = null;
            txtExaminerName.Text = string.Empty;
            ddlExaminerPlaceId.SelectedIndex = 0;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            btnCancelEdit.Visible = false;
        }
    }
}