using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.Business.Common;
using System.Globalization;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class RegisterTrackNumber : System.Web.UI.Page
    {
        ExamBusiness eBusiness = new ExamBusiness();
        public CommonBusiness cBusiness = new CommonBusiness();
        LoginBusiness lBusiness = new LoginBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mId = Request.QueryString["id"].ToString();
                string[] id = mId.ToString().Split(new char[] { '@' });
                string menuId = "";
                for (int i = 0; i < id[1].Length; i++)
                {
                    string s = id[1].Substring(i + 1, 1);
                    if (s != "-")
                        menuId += s;
                    else
                        break;
                    Session[sessionNames.menuID] = menuId;
                }
                AccessControl.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
                FillForm();
                Session["ExamDate"] = null;
            }
            pnlSuccessMessageBox.Visible = false;
        }

        private void FillForm()
        {
            //--------Fill ddlExamDate
            var dtExamDate = eBusiness.Get_Exam_dateexam();
            ddlExamDate.DataSource = dtExamDate;
            ddlExamDate.DataTextField = "dateexam";
            ddlExamDate.DataValueField = "dateexam";
            ddlExamDate.DataBind();
            ddlExamDate.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
            //------------------------
        }

        protected void btnShowResult_Click(object sender, EventArgs e)
        {
            Session["ExamDate"] = ddlExamDate.SelectedItem.Value;
            var userRoles = lBusiness.Get_UserRoles(Session[sessionNames.userID_Karbar].ToString());
            var roleList = new List<int>();
            foreach (DataRow row in userRoles.Rows)
                roleList.Add(Convert.ToInt32(row["RoleId"]));
            if(roleList.Contains(1)) // مدیر ارشد
            {
                pnlSecretariatResult.Visible = true;
                grdSecretariatResult.Rebind();
                pnlResult.Visible = true;
                grdResult.Rebind();
            }
            else if (roleList.Contains(76)) // دبیرخانه
            {
                pnlSecretariatResult.Visible = true;
                pnlResult.Visible = false;
                grdSecretariatResult.Rebind();
            }
            else if(roleList.Contains(34)) // ممتحن
            {
                pnlResult.Visible = true;
                pnlSecretariatResult.Visible = false;
                grdResult.Rebind();
            }
        }

        protected void grdResult_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var list = eBusiness.GetExamQuestionsByDateAndExaminerId(Session["ExamDate"].ToString(), int.Parse(Session[sessionNames.userID_Karbar].ToString()));
            grdResult.DataSource = list;
            if (list.Rows.Count > 0)
                btnSave.Visible = true;
            else
                btnSave.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (Telerik.Web.UI.GridDataItem item in grdResult.MasterTableView.Items)
            {
                HiddenField hdnExamPaperId = item.FindControl("hdnExamPaperId") as HiddenField;
                HiddenField hdnExamPlaceId = item.FindControl("hdnExamPlaceId") as HiddenField;
                HiddenField hdnExamDate = item.FindControl("hdnExamDate") as HiddenField;
                HiddenField hdnExamTime = item.FindControl("hdnExamTime") as HiddenField;
                TextBox txtTrackNumber = item.FindControl("txtTrackNumber") as TextBox;
                Label lblExamPlaceName = item.FindControl("lblExamPlaceName") as Label;
                if (hdnExamPaperId != null && txtTrackNumber != null && lblExamPlaceName != null && hdnExamPlaceId != null && hdnExamDate != null
                    && hdnExamTime != null)
                {
                    var examPaper = !string.IsNullOrEmpty(hdnExamPaperId.Value) ? Convert.ToInt32(hdnExamPaperId.Value) : 0;
                    if (eBusiness.AddOrUpdateExamPaper(examPaper, txtTrackNumber.Text, Convert.ToInt32(hdnExamPlaceId.Value)
                        , hdnExamDate.Value, hdnExamTime.Value))
                    {
                        cBusiness.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(),
                            int.Parse(Session[sessionNames.appID_Karbar].ToString()), 169, "ثبت کد مرسوله شهر " + lblExamPlaceName.Text,
                            examPaper);
                        lblSuccessMessage.Text = "تغییرات با موفقیت ذخیره شد.";
                    }
                }
            }
            pnlSuccessMessageBox.Visible = true;
        }

        protected void grdSecretariatResult_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var list = eBusiness.GetExamQuestionsByDateAndExaminerId(Session["ExamDate"].ToString());
            grdSecretariatResult.DataSource = list;
        }

        protected void grdSecretariatResult_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "SecretariatReceived":
                    Label lblTrackNumber = e.Item.FindControl("lblTrackNumber") as Label;
                    HiddenField hdnExamPlaceId = e.Item.FindControl("hdnExamPlaceId") as HiddenField;
                    HiddenField hdnExamDate = e.Item.FindControl("hdnExamDate") as HiddenField;
                    if (lblTrackNumber != null && hdnExamPlaceId != null && hdnExamDate != null)
                    {
                        if (eBusiness.SetSecretariatReceived(lblTrackNumber.Text, hdnExamDate.Value, Convert.ToInt32(hdnExamPlaceId.Value)))
                        {
                            cBusiness.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(),
                                int.Parse(Session[sessionNames.appID_Karbar].ToString()), 170, "دریافت بسته توسط دبیرخانه",
                                0);
                            lblSuccessMessage.Text = "تغییرات با موفقیت ذخیره شد.";
                            pnlSuccessMessageBox.Visible = true;
                            grdSecretariatResult.Rebind();
                        }
                    }
                    break;
            }
        }

        protected void grdSecretariatResult_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is Telerik.Web.UI.GridDataItem)
            {
                Button btnReceived = e.Item.FindControl("btnReceived") as Button;
                var isReceived = (((DataRowView)e.Item.DataItem)["SecretariatReceived"]).GetType() == typeof(DBNull) ?
                    false : Convert.ToBoolean(((DataRowView)e.Item.DataItem)["SecretariatReceived"]);
                var trackNumber = ((DataRowView)e.Item.DataItem)["TrackNumber"].ToString();
                if (string.IsNullOrEmpty(trackNumber))
                {
                    if (btnReceived != null)
                        btnReceived.Enabled = false;
                    btnReceived.Text = "ارسال نشده";
                }
                else if (isReceived)
                {
                    if (btnReceived != null)
                        btnReceived.Enabled = false;
                    e.Item.BackColor = System.Drawing.Color.FromArgb(38, 185, 154);
                }
                else
                {
                    e.Item.BackColor = System.Drawing.Color.FromArgb(240, 173, 78);
                }
            }
        }
        protected string GetDateString(object date)
        {
            if(date.GetType() == typeof(DBNull))
                return "-";
            PersianCalendar pc = new PersianCalendar();
            var miladiDate = Convert.ToDateTime(date);
            return pc.GetYear(miladiDate).ToString() + '/' + pc.GetMonth(miladiDate).ToString() + '/' + pc.GetDayOfMonth(miladiDate).ToString();
        }
    }
}