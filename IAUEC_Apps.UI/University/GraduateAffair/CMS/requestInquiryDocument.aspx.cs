using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university;
using IAUEC_Apps.Business;
using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using Stimulsoft.Report.Dictionary;

namespace IAUEC_Apps.UI.University.GraduateAffair.CMS
{
    public partial class requestInquiryDocument : System.Web.UI.Page
    {
        Business.university.GraduateAffair.GraduateFormsBusiness gBsn = new Business.university.GraduateAffair.GraduateFormsBusiness();
        Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRequestDate_New.Value = DateTime.Now.ToPeString();
                var inq = gBsn.getStudentInquiry();
                grdConfirmation.DataSource = inq;
                grdConfirmation.DataBind();
            }
        }

        private void searchInquiryToUpdate()
        {
            searchToUpdate();
        }

        private void setInquiryValueToUpdate(System.Data.DataRow inq)
        {
            //txtDocAcceptDate_Update.Text = inq["documentAcceptDate"].ToString();
            txtRequestDate_Update.Value = inq["letterDate"].ToString();
            txtLetterNumber_Update.Text = inq["letterNumber"].ToString();
            txtNote_Update.Text = inq["note"].ToString();
            txtToPresentTo_Update.Text = inq["toPresentTo"].ToString();
            hdnInquiryID.Value = inq["id"].ToString();
        }
        
        private void searchToUpdate()
        {
            var inq = gBsn.getStudentInquiry(txtStcode_Update.Text.Trim(), Convert.ToInt32(ddlInquiryType_Update.SelectedItem.Value));
            ddlInquiries.Items.Clear();
            if (inq.Rows.Count > 0)
            {
                foreach (System.Data.DataRow dr in inq.Rows)
                {
                    ddlInquiries.Items.Add(new ListItem() { Text = dr["requestName"].ToString(), Value = dr["id"].ToString() });
                }
                ViewState["inquiey"] = inq;
                setInquiryValueToUpdate(inq.Rows[0]);
            }
            else
            {
                txtRequestDate_Update.Value = "";
                txtLetterNumber_Update.Text = "";
                txtNote_Update.Text = "";
                txtToPresentTo_Update.Text = "";
                hdnInquiryID.Value = "";
            }
        }

        private void deleteInquiry()
        {
            gBsn.deleteInquiry(Convert.ToInt32(ddlInquiries.SelectedItem.Value));
            setLog(Convert.ToInt32(ddlInquiries.SelectedItem.Value), "", DTO.eventEnum.حذف_درخواست_استعلام_گواهی_موقت_یا_دانشنامه);
            searchToUpdate();
            showMessage("حذف تاییدیه انتخابی با موفقیت انجام شد.");
        }

        private void setLog(int inquiryID,string Description,DTO.eventEnum eventType)
        {
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            int userId;//کاربری که لاگین کرده           
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            string description;//توضیحات اختیاری
            int modifyId;//کد درخواست ویرایش شده. ویرایش اطلاعات فردی،اطلاعات تماس و ...
            userId = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            appId = 10;
            modifyId = inquiryID;
            description = Description;
            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)eventType, description, modifyId);
        }

        private void showMessage(string text)
        {
            ltrMsgText.Text = text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_ShowMessage();", true);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var inq = gBsn.getStudentInquiry(txtStcode_Search.Text.Trim(),txtNationalCode_Search.Text, Convert.ToInt32(ddlConfirmationType.SelectedItem.Value));
            grdConfirmation.DataSource = inq;
            grdConfirmation.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_ConfirmNewInquiry();", true);

            
        }

        protected void btnSearchToUpdate_Click(object sender, EventArgs e)
        {
            searchInquiryToUpdate();
        }

        protected void btnTab_Click(object sender, EventArgs e)
        {
            dvEdit.Visible = false;
            dvNew.Visible = false;
            dvSearch.Visible = false;
            switch ((sender as Button).CommandArgument)
            {
                case "search":
                    dvSearch.Visible = true;
                    var inq = gBsn.getStudentInquiry(txtStcode_Search.Text.Trim(), txtNationalCode_Search.Text, Convert.ToInt32(ddlConfirmationType.SelectedItem.Value));
                    grdConfirmation.DataSource = inq;
                    grdConfirmation.DataBind();
                    break;
                case "new":
                    dvNew.Visible = true;

                    break;
                case "edit":
                    dvEdit.Visible = true;

                    break;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_ConfirmUpdateInquiry();", true);

        }

        protected void ddlInquiries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInquiries.SelectedItem.Value != "")
            {
                var inq = (System.Data.DataTable)ViewState["inquiey"];
                var inqRow=inq.Select("id=" + ddlInquiries.SelectedItem.Value);
                if (inqRow.Length > 0)
                    setInquiryValueToUpdate(inqRow[0]);
            }
        }

        protected void grdConfirmation_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "ShowReport")
            {
                StiWebViewer1.ResetReport();
                //DTO.University.Graduate.GraduateFormsDTO GFD = new DTO.University.Graduate.GraduateFormsDTO();


                StiReport rpt = new StiReport();
                string[] arg = e.CommandArgument.ToString().Split(',');
                int inquiryId=Convert.ToInt32(arg[0]);
                int inquiryType=Convert.ToInt32(arg[1]);
                string stcode=arg[2].ToString();
                //GFD.stCode = stcode;
                //var dr = gBsn.getCourseReportInfo(GFD);

                rpt.Load(Server.MapPath("../Reports/TaeedTahsili.mrt"));
                rpt.Dictionary.Databases.Clear();
                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection.ToString()));
                rpt.Compile();
                rpt.CompiledReport.DataSources["Graduate.SP_getFormTaeedieTahsili"].Parameters["@stcode"].ParameterValue = stcode;
                rpt.CompiledReport.DataSources["Graduate.SP_getFormTaeedieTahsili"].Parameters["@Type"].ParameterValue = inquiryType;
                rpt.CompiledReport.DataSources["Graduate.SP_getFormTaeedieTahsili"].Parameters["@InquiryID"].ParameterValue = inquiryId;
                rpt.CompiledReport.DataSources["[dbo].[SP_GetStudentPic]"].Parameters["@stcode"].ParameterValue = stcode;
                //rpt.RegData(dr);
                StiWebViewer1.Visible = true;
                StiWebViewer1.Report = rpt;
            }
        }

        protected void btnDeleteInquiry_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_ConfirmDeleteInquiry();", true);
        }
        

        protected void mdlBtnConfirmNewInquiry_Click(object sender, EventArgs e)
        {
            DTO.University.Graduate.Inquiry inquiry = new DTO.University.Graduate.Inquiry();
            inquiry.inquiryType = Convert.ToInt32(ddlInquiryType_New.SelectedItem.Value);
            inquiry.letterDate = txtRequestDate_New.Value;
            inquiry.letterNumber = txtLetterNumber_New.Text;
            inquiry.note = txtNote_New.Text;
            inquiry.stcode = txtStcode_Insert.Text;
            inquiry.toPeresentTo = txtToPresentTo_New.Text;
            int inquiryID=gBsn.insertStudentInquiry(inquiry);
            setLog(inquiryID, string.Format("{0} - {1}:{2} - {3}:{4} - {5}:{6} - {7}:{8} - {9}:{10}","درج استعلام",
                "شماره دانشجویی",inquiry.stcode,"نوع استعلام",inquiry.inquiryType,"شماره نامه", inquiry.letterNumber, 
                 "تاریخ نامه", inquiry.letterDate,"جهت ارائه به",inquiry.toPeresentTo, "توضیحات", inquiry.note), DTO.eventEnum.درج_یا_ویرایش_استعلام_گواهی_موقت_یا_دانشنامه);

            showMessage("اطلاعات تاییدیه با موفقیت ثبت شد.");
        }

        protected void mdlBtnConfirmUpdateInquiry_Click(object sender, EventArgs e)
        {
            DTO.University.Graduate.Inquiry inquiry = new DTO.University.Graduate.Inquiry();
            inquiry.inquiryType = Convert.ToInt32(ddlInquiryType_Update.SelectedItem.Value);
            inquiry.letterDate = txtRequestDate_Update.Value;
            inquiry.letterNumber = txtLetterNumber_Update.Text;
            inquiry.note = txtNote_Update.Text;
            inquiry.stcode = txtStcode_Update.Text;
            inquiry.toPeresentTo = txtToPresentTo_Update.Text;
            inquiry.inquiryID = Convert.ToInt32(hdnInquiryID.Value);
            gBsn.updateStudentInquiry(inquiry);

            setLog(inquiry.inquiryID, string.Format("{0} - {1}:{2} - {3}:{4} - {5}:{6} - {7}:{8} - {9}:{10}", "درج استعلام",
                "شماره دانشجویی", inquiry.stcode, "نوع استعلام", inquiry.inquiryType, "شماره نامه", inquiry.letterNumber,
                 "تاریخ نامه", inquiry.letterDate, "جهت ارائه به", inquiry.toPeresentTo, "توضیحات", inquiry.note), DTO.eventEnum.درج_یا_ویرایش_استعلام_گواهی_موقت_یا_دانشنامه);
            searchInquiryToUpdate();
            showMessage("اطلاعات تاییدیه با موفقیت ثبت شد.");

        }

        protected void mdlBtnConfirmDeleteInquiry_Click_Click(object sender, EventArgs e)
        {
            deleteInquiry();
        }
    }
}