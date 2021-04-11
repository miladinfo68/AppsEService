using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.CooperationRequest.Teachers
{
    public partial class EditWorkInfo : System.Web.UI.Page
    {
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        ProfessorRequestBusiness ProfReqBuss = new ProfessorRequestBusiness();
        CommonBusiness CB = new CommonBusiness();
        List<ChangedInfoDTO> ChangeList = new List<ChangedInfoDTO>();
        string customError = string.Empty;
        public bool ShowView = true;

        #region ViewState
        protected override void SavePageStateToPersistenceMedium(object state)
        {

            string file = GenerateFileName();

            FileStream filestream = new FileStream(file, FileMode.Create);

            LosFormatter formator = new LosFormatter();

            formator.Serialize(filestream, state);

            filestream.Flush();

            filestream.Close();

            filestream = null;

        }

        protected override object LoadPageStateFromPersistenceMedium()
        {

            object state = null;

            StreamReader reader = new StreamReader(GenerateFileName());

            LosFormatter formator = new LosFormatter();

            state = formator.Deserialize(reader);

            reader.Close();

            return state;

        }

        private string GenerateFileName()
        {

            string file = Session.SessionID.ToString() + ".txt";

            file = Path.Combine(Server.MapPath("~/University/CooperationRequest/Teachers") + "/" + file);

            return file;

        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int code_ostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
                if (ProfReqBuss.HasPendingRequest(code_ostad, (int)RequestTypeId.EditHokm))
                {
                    ShowView = false;
                    string msg = "شما به دلیل وجود درخواست تایید نشده از این نوع درخواست نمی توانید درخواست جدید ثبت کنید.";
                    RadWindowManager2.RadAlert(msg, 500, 100, "پیام سیستم", "RedirectToMain");
                    return;
                }
                LoadInfoToForm();

            }
            if (flpHokm.UploadedFiles.Count > 0)
            {
                UploadedFile obj = flpHokm.UploadedFiles[0];
                string ScanMadarekURL;
                if (!Directory.Exists(Server.MapPath("~/University/CooperationRequest/Teachers/ScanMadarek/")))
                    Directory.CreateDirectory(Server.MapPath("~/University/CooperationRequest/Teachers/ScanMadarek/"));
                string subPath = Server.MapPath("~/University/CooperationRequest/Teachers/ScanMadarek/") + Session[sessionNames.userID_StudentOstad].ToString() + "\\";

                bool exists = System.IO.Directory.Exists(subPath);

                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);
                try
                {
                    ScanMadarekURL = subPath + "Hokm" + DateTime.Now.ToPeString("yyyy-MM-dd_HH-mm-ss") + obj.GetName();
                    obj.SaveAs(ScanMadarekURL.improveFileName(), true);
                }
                catch (Exception)
                {

                    throw;
                }
                Session.Add("ScanMadarekURL", ScanMadarekURL);
            }
        }

        private void LoadInfoToForm()
        {

            int codeostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            DataTable dtResault = FRB.GetOstadInfoFromHR(codeostad);
            if (dtResault.Rows.Count > 0)
            {
                var dataRow = dtResault.Rows[dtResault.Rows.Count - 1];
                Session.Add("hrInfoPeopleId", dataRow["Id"]);
                var lastHokm = ProfReqBuss.GetLastHokmInfoByInfoPeopleID(Convert.ToInt32(dataRow["Id"]));
                pnlSabeghe.Visible = true;

                DataTable dtUniName = CB.GetNameUni_fcoding();


                for (int i = 0; i <= dtUniName.Rows.Count - 1; i++)
                {
                    dtUniName.Rows[i][0] = dtUniName.Rows[i][0].ToString().Replace("ي", "ی");
                }
                drpPastUni.DataSource = dtUniName;
                drpPastUni.DataTextField = "namecoding";
                drpPastUni.DataValueField = "ID";
                drpPastUni.DataBind();
                drpPastUni.Items.Insert(0, new RadComboBoxItem("جستجو و انتخاب کنید", "0"));
                if (rblIsHeiat.Items.FindByValue("2") != null)
                    rblIsHeiat.SelectedValue = "2";


                if (lastHokm.HokmId > 0)
                {
                    string martabeh = lastHokm.Martabeh.ToString();
                    if (string.IsNullOrWhiteSpace(martabeh) || martabeh == "0" || martabeh == "-2" || martabeh == "8")
                    {
                        if (drpMartabe.Items.FindByValue("0") != null)
                            drpMartabe.SelectedValue = "0";
                    }
                    else
                        if (drpMartabe.Items.FindByValue(martabeh) != null)
                        drpMartabe.SelectedValue = martabeh;

                    string payeh = lastHokm.Payeh.ToString();
                    txtPaye.Text = payeh;
                    if (txtPaye.Text == "")
                        txtPaye.Text = "0";

                    string hireType = lastHokm.Type_Estekhdam.ToString();
                    if (string.IsNullOrWhiteSpace(hireType))
                    {
                        if (drpHireType.Items.FindByValue("-1") != null)
                            drpHireType.SelectedValue = "-1";
                    }
                    else
                        if (drpHireType.Items.FindByValue(hireType) != null)

                        drpHireType.SelectedValue = hireType;



                    string uniKhedmat = lastHokm.Uni_Khedmat.ToString();
                    if (string.IsNullOrWhiteSpace(uniKhedmat) || uniKhedmat == "0")
                    {
                        if (drpPastUni.Items.FindItemByValue("0") != null)
                            drpPastUni.Items.FindItemByValue("0").Selected = true;
                    }
                    else
                        if (drpPastUni.Items.FindItemByValue(uniKhedmat) != null)
                        drpPastUni.SelectedValue = uniKhedmat;

                    string uniKhedmatType = lastHokm.Uni_KhedmatType.ToString();
                    if (string.IsNullOrWhiteSpace(uniKhedmatType) || uniKhedmat == "0")
                    {
                        if (ddlPastUniType.Items.FindByValue("0") != null)
                            ddlPastUniType.SelectedValue = "0";
                    }
                    else
                        if (ddlPastUniType.Items.FindByValue(uniKhedmatType) != null)
                        ddlPastUniType.SelectedValue = uniKhedmatType;
                    if (dtResault.Rows[0]["nahveh_hamk"] != DBNull.Value)
                        if (rdblHireType.Items.FindByValue(lastHokm.Nahveh_Hamk.ToString()) != null)
                            rdblHireType.SelectedValue = lastHokm.Nahveh_Hamk.ToString();


                    string dateSodoorHokm = lastHokm.Date_Hokm.ToString();
                    txtDateSodoorHokm.Text = dateSodoorHokm;


                    string DateEjraHokm = lastHokm.Date_RunHokm.ToString();
                    txtDateEjraHokm.Text = DateEjraHokm;

                    string hokmNumber = lastHokm.Number_Hokm.ToString();
                    txtHokmNumber.Text = hokmNumber;

                    string mablaghHokm = lastHokm.MablaghHokm.ToString();
                    txtMablaghHokm.Text = mablaghHokm;

                    if (lastHokm.Martabeh < 8 && lastHokm.Martabeh > 0)
                    {
                        if (rblIsHeiat.Items.FindByValue("1") != null)
                            rblIsHeiat.SelectedValue = "1";
                        ChangeDetailsPanelStatus(true);
                    }
                    else //هیات علمی نیست
                    {
                        if (rblIsHeiat.Items.FindByValue("2") != null)
                            rblIsHeiat.SelectedValue = "2";
                        ChangeDetailsPanelStatus(false);
                    }
                    if (lastHokm.BoundHour != null)
                        chkBoundHour.Checked = Convert.ToBoolean(lastHokm.BoundHour);

                }

                hdnMartabe.Value = drpMartabe.SelectedItem.Value;
                hdnBoundHour.Value = chkBoundHour.Checked.ToString();
                hdnMablaghHokm.Value = txtMablaghHokm.Text;
                hdnHokmNumber.Value = txtHokmNumber.Text;
                hdnDateEjraHokm.Value = txtDateEjraHokm.Text;
                hdnDateSodoorHokm.Value = txtDateSodoorHokm.Text;
                hdnHireType2.Value = lastHokm.Nahveh_Hamk.ToString();
                hdnPastUniType.Value = ddlPastUniType.SelectedItem.Value;
                hdnPastUni.Value = drpPastUni.SelectedItem.Value;
                hdnHireType.Value = drpHireType.SelectedItem.Value;
                hdnPaye.Value = txtPaye.Text;
                hdnHokmImage.Value = !string.IsNullOrEmpty(lastHokm.HokmUrl) ? "1" : "0";
                if (lastHokm.Martabeh > 0 && lastHokm.Martabeh < 8)
                {
                    rblIsHeiat.SelectedValue = "1";
                    ChangeDetailsPanelStatus(true);
                }
                else
                {
                    rblIsHeiat.SelectedValue = "2";
                    ChangeDetailsPanelStatus(false);
                }
                VisibleBtn();
            }
            else
            {
                pnlSabeghe.Visible = true;
                DataTable dtUniName = CB.GetNameUni_fcoding();


                for (int i = 0; i <= dtUniName.Rows.Count - 1; i++)
                {
                    dtUniName.Rows[i][0] = dtUniName.Rows[i][0].ToString().Replace("ي", "ی");
                }
                drpPastUni.DataSource = dtUniName;
                drpPastUni.DataTextField = "namecoding";
                drpPastUni.DataValueField = "ID";
                drpPastUni.DataBind();
                drpPastUni.Items.Insert(0, new RadComboBoxItem("جستجو و انتخاب کنید", "0"));

                if (rblIsHeiat.Items.FindByValue("2") != null)
                    rblIsHeiat.SelectedValue = "2";
                VisibleBtn();
            }

        }

        private void VisibleBtn()
        {

            //if (rblIsHeiat.SelectedItem.Value == "1")
            //{
            btnSubmitChanges.Visible = rblIsHeiat.SelectedItem.Value == "1";
            btnSubmitChangesNo.Visible = rblIsHeiat.SelectedItem.Value != "1";
            //}
            //else
            //{
            //    btnSubmitChanges.Visible = false;
            //    btnSubmitChangesNo.Visible = true;
            //}
        }

        private bool isInformationValid()
        {
            string msg = "";
            if (!txtDateSodoorHokm.Text.isPersianDateCorrect())
            {
                msg = string.Format("تاریخ صدور حکم اشتباه وارد شده است. لطفا آن را اصلاح فرمایید.");
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }
            if (!txtDateEjraHokm.Text.isPersianDateCorrect())
            {
                msg = string.Format("تاریخ اجرای حکم اشتباه وارد شده است. لطفا آن را اصلاح فرمایید.");
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }
            return true;
        }

        protected void Radcombo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ViewState["ChangeList"] != null)
            {
                ChangeList = (List<ChangedInfoDTO>)ViewState["ChangeList"];
            }
            else
            {
                throw new Exception("Empty ViewState!");
            }

            if (sender is RadComboBox)
            {
                RadComboBox drp = (RadComboBox)sender;
                var olditem = ChangeList.Where(i => i.ControlId == drp.ID + "Value")
                                       .First();
                olditem.NewValue = drp.SelectedValue;
            }
            else
            {
                throw new Exception("Invalid Control Binded to Event!");
            }
        }

        private bool CustomValidation()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDateSodoorHokm.Text)
                    && !string.IsNullOrEmpty(txtDateEjraHokm.Text))
                {
                    System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                    var edParts = txtDateEjraHokm.Text.Split('/');
                    var idParts = txtDateSodoorHokm.Text.Split('/');
                    if (idParts.Length != 3 || edParts.Length != 3)
                    {
                        customError = "تاریخ صدور یا اجرای حکم صحیح نیست.";
                        return false;

                    }
                    //DateTime sodoorDate = new DateTime(Convert.ToInt32(idParts[0]), Convert.ToInt32(idParts[1]), Convert.ToInt32(idParts[2]), pc);
                    //DateTime ejraDate = new DateTime(Convert.ToInt32(edParts[0]), Convert.ToInt32(edParts[1]), Convert.ToInt32(edParts[2]), pc);
                    //if (ejraDate > sodoorDate)
                    //    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void vldPersonalImage_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (flpHokm.OnClientFileUploadFailed != "true" || rblIsHeiat.SelectedItem.Value == "2");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditMain.aspx");

        }

        protected void rblIsHeiat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblIsHeiat.SelectedItem.Value == "1")
            {
                ChangeDetailsPanelStatus(true);
            }
            else
            {
                ChangeDetailsPanelStatus(false);
            }
            VisibleBtn();
        }

        private void ChangeDetailsPanelStatus(bool status)
        {
            pnlDetails.Enabled = status;
            flpHokm.Enabled = false;
            rfvHireType.Enabled = status;
            //rfvMablaghHokm.Enabled = status;
            RequiredFieldValidator6.Enabled = status;
            //revHokmNumber.Enabled = status;
            rfvDateEjraHokm.Enabled = status;
            revDateEjraHokm.Enabled = status;
            rfvDateSodoorHokm.Enabled = status;
            revDateSodoorHokm.Enabled = status;
            rfvPaye.Enabled = status;
            RangeValidator4.Enabled = status;
            rfvMartabe.Enabled = status;
            rfvHireType2.Enabled = status;
            valUniName.Enabled = status;
        }

        protected void btnSubmitChanges_Click(object sender, EventArgs e)
        {



            if (HasHrId())
            {
                if (IsValid && CustomValidation())
                {
                    if (!isInformationValid() && rblIsHeiat.SelectedItem.Value == "1")
                        return;
                    ProfessorEditRequestDTO oEditDTO = new ProfessorEditRequestDTO();
                    oEditDTO.Code_Ostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
                    oEditDTO.Createdate = DateTime.Now.ToPeString();
                    oEditDTO.RequestTypeID = (int)RequestTypeId.EditHokm; // درخواست ویرایش حکم
                    oEditDTO.RequestLogID = (int)RequestLogId.submitted; // در حال بررسی
                    oEditDTO.HR_InfoPeople_Id = Convert.ToInt32(Session["hrInfoPeopleId"]);
                    oEditDTO.ChangeSet = 1;
                    if (Session["ScanMadarekURL"] != null)
                        oEditDTO.ScanImageUrl = Session["ScanMadarekURL"].ToString().improveFileName();

                    ProfessorHokmDTO oHokm = new ProfessorHokmDTO(oEditDTO.HR_InfoPeople_Id);
                    oHokm.Code_Ostad = oEditDTO.Code_Ostad;
                    if (!string.IsNullOrEmpty(txtHokmNumber.Text))
                        oHokm.Number_Hokm = txtHokmNumber.Text;
                    if (!string.IsNullOrEmpty(txtDateSodoorHokm.Text))
                        oHokm.Date_Hokm = txtDateSodoorHokm.Text;
                    if (!string.IsNullOrEmpty(txtDateEjraHokm.Text))
                        oHokm.Date_RunHokm = txtDateEjraHokm.Text;

                    if (!string.IsNullOrEmpty(txtMablaghHokm.Text))
                        oHokm.MablaghHokm = Convert.ToInt64(txtMablaghHokm.Text.Replace(",", ""));

                    if (!string.IsNullOrEmpty(txtPaye.Text))
                        oHokm.Payeh = Convert.ToInt32(txtPaye.Text);

                    if (drpHireType.SelectedValue != "-1")
                        oHokm.Type_Estekhdam = Convert.ToInt32(drpHireType.SelectedValue);
                    if (drpPastUni.SelectedValue != "0" && drpPastUni.SelectedValue != "")
                        oHokm.Uni_Khedmat = Convert.ToInt32(drpPastUni.SelectedValue);
                    if (ddlPastUniType.SelectedValue != "0")
                        oHokm.Uni_KhedmatType = Convert.ToInt32(ddlPastUniType.SelectedValue);
                    if (!string.IsNullOrEmpty(rdblHireType.SelectedValue))
                        oHokm.Nahveh_Hamk = Convert.ToInt32(rdblHireType.SelectedValue);
                    oHokm.DateUpload = DateTime.Now.ToPeString();
                    if (Session["ScanMadarekURL"] != null)
                        oHokm.HokmUrl = Session["ScanMadarekURL"].ToString().improveFileName();
                    oHokm.State = (int)ChangeState.submitted;
                    if (rblIsHeiat.SelectedItem.Value == "1")
                        oHokm.Martabeh = Convert.ToInt32(drpMartabe.SelectedValue);
                    else
                        oHokm.Martabeh = 8;
                    oHokm.BoundHour = chkBoundHour.Checked;

                    oEditDTO.Hokm = oHokm;

                    int Id = ProfReqBuss.AddNewEditRequest(oEditDTO);
                    string msg = null;
                    if (Id > 0)
                    {

                        CB.InsertIntoStudentLog(oEditDTO.HR_InfoPeople_Id.ToString(), DateTime.Now.ToString("HH:mm"),
                            13, 34, Id.ToString());
                        msg = "درخواست شما با شماره " + Id.ToString() + "با موفقیت ثبت گردید.";
                        Session["hrInfoPeopleId"] = null;
                    }
                    else
                    {
                        msg = "خطا در هنگام ثبت درخواست ، لطفا مجددا تلاش کنید.";
                    }

                    RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "RedirectToMain");
                }
                else if (!string.IsNullOrEmpty(customError))
                    RadWindowManager1.RadAlert(customError, 400, 100, "پیام سیستم", null);
            }
            else
            {
                RadWindowManager1.RadAlert("به دلیل کامل نبودن سامانه ثبت اطلاعات اساتید امکان این درخواست برای شما وجود ندارد", 400, 100, "پیام سیستم", "RedirectToMain");
            }

        }

        protected void btnSubmitChangesNo_OnClick(object sender, EventArgs e)
        {
            if (HasHrId())
            {
                if (IsValid)
                {
                    if (rblIsHeiat.SelectedItem.Value == "1")
                        return;
                    ProfessorEditRequestDTO oEditDTO = new ProfessorEditRequestDTO();
                    oEditDTO.Code_Ostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
                    oEditDTO.Createdate = DateTime.Now.ToPeString();
                    oEditDTO.RequestTypeID = (int)RequestTypeId.EditHokm; // درخواست ویرایش حکم
                    oEditDTO.RequestLogID = (int)RequestLogId.submitted; // در حال بررسی
                    oEditDTO.HR_InfoPeople_Id = Convert.ToInt32(Session["hrInfoPeopleId"]);
                    oEditDTO.ChangeSet = 1;
                    if (Session["ScanMadarekURL"] != null)
                        oEditDTO.ScanImageUrl = Session["ScanMadarekURL"].ToString().improveFileName();

                    ProfessorHokmDTO oHokm = new ProfessorHokmDTO(oEditDTO.HR_InfoPeople_Id);
                    oHokm.Code_Ostad = oEditDTO.Code_Ostad;
                    if (!string.IsNullOrEmpty(txtHokmNumber.Text))
                        oHokm.Number_Hokm = txtHokmNumber.Text;
                    if (!string.IsNullOrEmpty(txtDateSodoorHokm.Text))
                        oHokm.Date_Hokm = txtDateSodoorHokm.Text;
                    if (!string.IsNullOrEmpty(txtDateEjraHokm.Text))
                        oHokm.Date_RunHokm = txtDateEjraHokm.Text;
                    if (!string.IsNullOrEmpty(txtMablaghHokm.Text))
                        oHokm.MablaghHokm = Convert.ToInt64(txtMablaghHokm.Text.Replace(",", ""));
                    if (!string.IsNullOrEmpty(txtPaye.Text))
                        oHokm.Payeh = Convert.ToInt32(txtPaye.Text);
                    if (drpHireType.SelectedValue != "-1")
                        oHokm.Type_Estekhdam = Convert.ToInt32(drpHireType.SelectedValue);
                    if (drpPastUni.SelectedValue != "0" && drpPastUni.SelectedValue != "")
                        oHokm.Uni_Khedmat = Convert.ToInt32(drpPastUni.SelectedValue);
                    if (ddlPastUniType.SelectedValue != "0")
                        oHokm.Uni_KhedmatType = Convert.ToInt32(ddlPastUniType.SelectedValue);
                    if (!string.IsNullOrEmpty(rdblHireType.SelectedValue))
                        oHokm.Nahveh_Hamk = Convert.ToInt32(rdblHireType.SelectedValue);
                    oHokm.DateUpload = DateTime.Now.ToPeString();
                    if (Session["ScanMadarekURL"] != null)
                        oHokm.HokmUrl = Session["ScanMadarekURL"].ToString().improveFileName();
                    oHokm.State = (int)ChangeState.submitted;
                    if (rblIsHeiat.SelectedItem.Value == "1")
                        oHokm.Martabeh = Convert.ToInt32(drpMartabe.SelectedValue);
                    else
                        oHokm.Martabeh = 8;
                    oHokm.BoundHour = chkBoundHour.Checked;

                    oEditDTO.Hokm = oHokm;

                    int Id = ProfReqBuss.AddNewEditRequest(oEditDTO);
                    string msg = null;
                    if (Id > 0)
                    {

                        CB.InsertIntoStudentLog(oEditDTO.HR_InfoPeople_Id.ToString(), DateTime.Now.ToString("HH:mm"),
                            13, 34, Id.ToString());
                        msg = "درخواست شما با شماره " + Id.ToString() + "با موفقیت ثبت گردید.";
                        Session["hrInfoPeopleId"] = null;
                    }
                    else
                    {
                        msg = "خطا در هنگام ثبت درخواست ، لطفا مجددا تلاش کنید.";
                    }

                    RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "RedirectToMain");
                }
                else if (!string.IsNullOrEmpty(customError))
                    RadWindowManager1.RadAlert(customError, 400, 100, "پیام سیستم", null);
            }
            else
            {
                RadWindowManager1.RadAlert("به دلیل کامل نبودن سامانه ثبت اطلاعات اساتید امکان این درخواست برای شما وجود ندارد", 400, 100, "پیام سیستم", "RedirectToMain");

            }

        }

        protected bool HasHrId()
        {
            FacultyReportsBusiness FRB = new FacultyReportsBusiness();
            int codeostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            var prof = FRB.GetOstadInfoFromHR(codeostad);
            if (prof.Rows.Count > 0)
                return true;
            else
                return false;
        }

    }
}