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

namespace IAUEC_Apps.UI.University.CooperationRequest.Teachers
{
    public partial class EditContactInfo : System.Web.UI.Page
    {
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        ProfessorRequestBusiness ProfReqBuss = new ProfessorRequestBusiness();
        CommonBusiness CB = new CommonBusiness();
        List<ChangedInfoDTO> ChangeList = new List<ChangedInfoDTO>();

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

                if (ProfReqBuss.HasPendingRequest(code_ostad, (int)RequestTypeId.EditContactInfo))
                {
                    string msg = "شما به دلیل وجود درخواست تایید نشده از این نوع درخواست نمی توانید درخواست جدید ثبت کنید.";
                    RadWindowManager1.RadAlert(msg, 400, 200, "پیام سیستم", "RedirectToMain");
                    return;
                }
                LoadInfoToControls();
            }
        }

        private void LoadInfoToControls()
        {
            int codeostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            DataTable dtResult = FRB.GetOstadInfoFromHR(codeostad);
            if (dtResult.Rows.Count == 0)
            {
                string msg = "کد استادی شما در هیچ کدام از سامانه ها فعال نیست. لطفا جهت فعال سازی با کارشناس مربوطه تماس حاصل فرمایید";
                RadWindowManager1.RadAlert(msg, 400, 200, "پیام سیستم", "RedirectToMain");
                return;
            }
            Session.Add("hrInfoPeopleId", dtResult.Rows[0]["Id"]);

            ListItem itmSelect = new ListItem("انتخاب کنید", "");
            ListItem itmOther = new ListItem("سایر", "0");
            DTO.University.Faculty.editInfoStruct editInfo = FRB.getOstadInf(codeostad);

            //Session.Add("hrInfoPeopleId", editInfo.hrId);



            DataTable dtControlToSidaList = FRB.GetAllControlToSidaFields();

            DataRow existsDrp = null;
            ChangedInfoDTO oChangeDrp = new ChangedInfoDTO();


            existsDrp = dtControlToSidaList.AsEnumerable()
                                               .Where(x => x.Field<string>("ControlName") == drpProvince1.ID + "Value")
                                               .FirstOrDefault();
            oChangeDrp = new ChangedInfoDTO();
            oChangeDrp.Code_Ostad = codeostad;
            oChangeDrp.ControlToFieldId = Convert.ToInt32(existsDrp["Id"]);
            oChangeDrp.ControlId = drpProvince1.ID + "value";
            oChangeDrp.OldValue = getOldValue(existsDrp["id"].ToString(), editInfo);// as string;
            ChangeList.Add(oChangeDrp);

            existsDrp = dtControlToSidaList.AsEnumerable()
                                               .Where(x => x.Field<string>("ControlName") == drpLivingCity.ID + "Value")
                                               .FirstOrDefault();
            oChangeDrp = new ChangedInfoDTO();
            oChangeDrp.Code_Ostad = codeostad;
            oChangeDrp.ControlToFieldId = Convert.ToInt32(existsDrp["Id"]);
            oChangeDrp.ControlId = drpLivingCity.ID + "value";
            oChangeDrp.OldValue = getOldValue(existsDrp["id"].ToString(), editInfo);// as string;
            ChangeList.Add(oChangeDrp);


            existsDrp = dtControlToSidaList.AsEnumerable()
                                               .Where(x => x.Field<string>("ControlName") == drpProvince2.ID + "Value")
                                               .FirstOrDefault();
            oChangeDrp = new ChangedInfoDTO();
            oChangeDrp.Code_Ostad = codeostad;
            oChangeDrp.ControlToFieldId = Convert.ToInt32(existsDrp["Id"]);
            oChangeDrp.ControlId = drpProvince2.ID + "value";
            oChangeDrp.OldValue = getOldValue(existsDrp["id"].ToString(), editInfo);// as string;
            ChangeList.Add(oChangeDrp);
            existsDrp = dtControlToSidaList.AsEnumerable()
                                               .Where(x => x.Field<string>("ControlName") == drpWorkingCity.ID + "Value")
                                               .FirstOrDefault();
            oChangeDrp = new ChangedInfoDTO();
            oChangeDrp.Code_Ostad = codeostad;
            oChangeDrp.ControlToFieldId = Convert.ToInt32(existsDrp["Id"]);
            oChangeDrp.ControlId = drpWorkingCity.ID + "value";
            oChangeDrp.OldValue = getOldValue(existsDrp["id"].ToString(), editInfo);// as string;
            ChangeList.Add(oChangeDrp);

            foreach (Control item in dvAddressFileds.Controls)
            {
                DataRow exists = null;
                if (item is TextBox)
                {
                    exists = dtControlToSidaList.AsEnumerable()
                                               .Where(x => x.Field<string>("ControlName") == item.ID)
                                               .FirstOrDefault();
                }

                if (exists != null)
                {
                    ChangedInfoDTO oChange = new ChangedInfoDTO();
                    oChange.Code_Ostad = codeostad;
                    oChange.ControlToFieldId = Convert.ToInt32(exists["Id"]);
                    oChange.ControlId = item.ID;
                    oChange.OldValue = getOldValue(exists["id"].ToString(), editInfo);// as string;
                    ChangeList.Add(oChange);
                }
            }

            ViewState.Add("ChangeList", ChangeList);

            txtHomePhone.Text = editInfo.telHome.ToString();

            txtWorkPhone.Text = editInfo.telKar.ToString();
            txtMobileNumber.Text = editInfo.telMobile.ToString();
            txtLivingAddress.Text = editInfo.addressHome.ToString();
            txtWorkingAddress.Text = editInfo.addressKar.ToString();
            txtLivingZipCode.Text = editInfo.codePosti.ToString();
            txtEmail.Text = editInfo.email.ToString();

            setDropDownOstanSource(drpProvince1, editInfo.ostanHome);
            setDropDownOstanSource(drpProvince2, editInfo.ostanKar);
            setDropDownShahrSource(drpLivingCity, editInfo.ostanHome, editInfo.shahrHome);
            setDropDownShahrSource(drpWorkingCity, editInfo.ostanKar, editInfo.shahrKar);

        }

        private string getOldValue(string HRFieldName, DTO.University.Faculty.editInfoStruct editInfo)
        {
            switch (HRFieldName)
            {
                case "23":
                    return editInfo.telHome;
                case "24":
                    return editInfo.telKar;
                case "25":
                    return editInfo.addressHome;
                case "26":
                    return editInfo.addressKar;
                case "27":
                    return editInfo.email;
                case "28":
                    return editInfo.telMobile;
                case "29":
                    return editInfo.codePosti;
                case "45":
                    return editInfo.ostanHome.ToString();
                case "46":
                    return editInfo.shahrHome.ToString();
                case "47":
                    return editInfo.ostanKar.ToString();
                case "48":
                    return editInfo.shahrKar.ToString();
            }
            return "";
        }

        private void setDropDownOstanSource(DropDownList drp, object selectedOstan)
        {
            drp.Items.Clear();
            drp.DataSource = CB.GetOstan();
            drp.DataTextField = "Title";
            drp.DataValueField = "ID";
            drp.DataBind();
            if (!string.IsNullOrWhiteSpace(selectedOstan.ToString()))
            {
                drp.SelectedValue = selectedOstan.ToString();
            }
            ListItem l = new ListItem();
            l.Value = "0";
            l.Text = "انتخاب کنید";


            drp.Items.Insert(0, l);
        }

        private void setDropDownShahrSource(DropDownList drp, object selectedOstan, object selectedShahr)
        {
            drp.Items.Clear();
            if (!string.IsNullOrWhiteSpace(selectedOstan.ToString()))
            {
                drp.DataSource = getShahrByOstan(Convert.ToInt32(selectedOstan));
                drp.DataTextField = "Title";
                drp.DataValueField = "ID";
                drp.DataBind();
                if (!string.IsNullOrWhiteSpace(selectedShahr.ToString()))
                {
                    drp.SelectedValue = selectedShahr.ToString();
                }
            }
            ListItem l = new ListItem();
            l.Value = "0";
            l.Text = "انتخاب کنید";

            drp.Items.Insert(0, l);
        }

        private void AddValueToChangeList(int codeostad, DataTable dtControlToSidaList, string controlValue, string controlId)
        {
            int fieldId = dtControlToSidaList.AsEnumerable()
                                              .Where(x => x.Field<string>("ControlName") == controlId)
                                              .Select(i => i.Field<int>("Id"))
                                              .First();

            ChangeList.Add(new ChangedInfoDTO(codeostad)
            {
                ProfessorRequestId = 0,
                ControlToFieldId = fieldId,
                ControlId = controlId,
                OldValue = controlValue,
                Code_Ostad = codeostad
            });
        }

        protected void drpProvince1_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selectedOstan = string.Empty;
            if (drpProvince1.SelectedIndex != 0)
                selectedOstan = drpProvince1.SelectedValue;
            setDropDownShahrSource(drpLivingCity, selectedOstan, string.Empty);
        }

        private DataTable getShahrByOstan(int ostanCode)
        {
            DataTable dtShahr = CB.getShahrestan(ostanCode);
            return dtShahr;
        }

        protected void drpProvince2_SelectedIndexChanged(object sender, EventArgs e)
        {
            object selectedOstan = string.Empty;
            if (drpProvince2.SelectedIndex != 0)
                selectedOstan = drpProvince2.SelectedValue;
            setDropDownShahrSource(drpWorkingCity, selectedOstan, string.Empty);

        }

        protected void btnSubmitChanges_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string msg = "";
                if (!isInformationValid())
                    return;
                if (ProfReqBuss.HasPendingRequest(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]), (int)RequestTypeId.EditContactInfo))
                {
                    msg = "شما به دلیل وجود درخواست تایید نشده از این نوع درخواست نمی توانید درخواست جدید ثبت کنید.";
                    RadWindowManager1.RadAlert(msg, 400, 200, "پیام سیستم", "RedirectToMain");
                    return;
                }
                List<ChangedInfoDTO> oFullChangeList = (List<ChangedInfoDTO>)ViewState["ChangeList"];
                foreach (Control item in dvAddressFileds.Controls)
                {
                    if (item is TextBox)
                    {
                        TextBox txt = (TextBox)item;
                        ChangedInfoDTO result = oFullChangeList.Find(i => i.ControlId == txt.ID);
                        if (result.OldValue != txt.Text)
                        {
                            result.NewValue = txt.Text;
                            result.State = FieldChangeState.Submitted;
                        }
                    }
                }

                ChangedInfoDTO resultDrp;
                resultDrp = oFullChangeList.Find(i => i.ControlId == drpLivingCity.ID + "value");

                if (resultDrp?.OldValue != drpLivingCity.SelectedValue && drpLivingCity.SelectedItem.Value != "0")
                {
                    resultDrp.NewValue = drpLivingCity.SelectedValue;
                    resultDrp.State = FieldChangeState.Submitted;
                }

                //------------------------------
                resultDrp = oFullChangeList.Find(i => i.ControlId == drpProvince1.ID + "value");

                if (resultDrp?.OldValue != drpProvince1.SelectedValue && drpProvince1.SelectedItem.Value != "0")
                {
                    resultDrp.NewValue = drpProvince1.SelectedValue;
                    resultDrp.State = FieldChangeState.Submitted;
                }

                //------------------------------

                resultDrp = oFullChangeList.Find(i => i.ControlId == drpProvince2.ID + "value");

                if (resultDrp?.OldValue != drpProvince2.SelectedValue && drpProvince2.SelectedItem.Value != "0")
                {
                    resultDrp.NewValue = drpProvince2.SelectedValue;
                    resultDrp.State = FieldChangeState.Submitted;
                }
                //------------------------------

                resultDrp = oFullChangeList.Find(i => i.ControlId == drpWorkingCity.ID + "value");

                if (resultDrp?.OldValue != drpWorkingCity.SelectedValue && drpWorkingCity.SelectedItem.Value != "0")
                {
                    resultDrp.NewValue = drpWorkingCity.SelectedValue;
                    resultDrp.State = FieldChangeState.Submitted;
                }

                //------------------------------

                ProfessorEditRequestDTO oEditDTO = new ProfessorEditRequestDTO();
                oEditDTO.Code_Ostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
                oEditDTO.Createdate = DateTime.Now.ToPeString();
                oEditDTO.RequestTypeID = (int)RequestTypeId.EditContactInfo; // درخواست ویرایش اطلاعات تماس
                oEditDTO.RequestLogID = (int)RequestLogId.submitted; // در حال بررسی
                oEditDTO.HR_InfoPeople_Id = Convert.ToInt32(Session["hrInfoPeopleId"]);
                oEditDTO.ChangeSet = 1;
                oEditDTO.ChangeList = new List<ChangedInfoDTO>();
                foreach (ChangedInfoDTO item in oFullChangeList)
                {
                    if (!string.IsNullOrWhiteSpace(item.NewValue) && item.NewValue != item.OldValue)
                    {
                        item.State = FieldChangeState.Submitted;
                        oEditDTO.ChangeList.Add(item);
                    }
                }
                if (oEditDTO.ChangeList.Count == 0)
                {
                    msg = "شما هیچ تغییری در این بخش به وجود نیاورده اید.";
                    RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                    return;
                }
                int Id = ProfReqBuss.AddNewEditRequest(oEditDTO);
                msg = null;
                if (Id > 0)
                {

                    CB.InsertIntoStudentLog(oEditDTO.HR_InfoPeople_Id.ToString(), DateTime.Now.ToString("HH:mm"), 13, 32, Id.ToString());
                    msg = "درخواست شما با شماره " + Id.ToString() + "با موفقیت ثبت گردید.";
                    Session["hrInfoPeopleId"] = null;
                }
                else
                {
                    msg = "خطا در هنگام ثبت درخواست ، لطفا مجددا تلاش کنید.";
                }

                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "RedirectToMain");
            }
        }


        private bool isInformationValid()
        {
            string fields = "", msg = "";
            if (drpProvince1.SelectedIndex == 0)
            {
                fields = "استان محل سکونت،";
            }
            if (drpLivingCity.SelectedIndex == 0)
            {
                fields += "شهر محل سکونت،";

            }
            if (drpProvince2.SelectedIndex != 0 && drpWorkingCity.SelectedIndex == 0)
            {
                fields += "شهر محل کار";

            }
            if (fields != "")
            {
                msg = string.Format("لطفا فیلد {0} را انتخاب فرمایید.", fields);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }
            if (!isSabetPhoneValid(txtHomePhone.Text))
            {
                msg = string.Format("شماره تلفن منزل اشتباه وارد شده است. لطفا آن را اصلاح فرمایید.");
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }
            if (!isSabetPhoneValid(txtWorkPhone.Text))
            {
                msg = string.Format("شماره تلفن محل کار اشتباه وارد شده است. لطفا آن را اصلاح فرمایید.");
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }
            if (!isMobileValid(txtMobileNumber.Text))
            {
                msg = string.Format("شماره تلفن همراه اشتباه وارد شده است. لطفا آن را اصلاح فرمایید.");
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }
            if (!isCodePostiValid(txtLivingZipCode.Text))
            {
                msg = string.Format("کد پستی اشتباه وارد شده است. لطفا آن را اصلاح فرمایید.");
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }
            return true;
        }

        private bool isSabetPhoneValid(string sabet)
        {
            if (sabet.Trim().Length > 0)
            {
                System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"\d{11}");
                if (!rgx.IsMatch(sabet))
                    return false;
                if (!sabet.StartsWith("0") ||
                    sabet.Substring(3).StartsWith("0") ||
                    sabet.Substring(3).StartsWith("1") ||
                    sabet.Substring(3).StartsWith("9"))
                    return false;
            }
            return true;
        }

        private bool isMobileValid(string mobile)
        {
            if (mobile.Trim().Length != 0)
            {
                System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"\d{11}");
                if (!rgx.IsMatch(mobile))
                    return false;
                if (!mobile.StartsWith("09"))
                    return false;
            }
            return true;
        }

        private bool isCodePostiValid(string codePosti)
        {
            if (codePosti.Trim().Length != 0)
            {
                System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"\d{10}");
                if (!rgx.IsMatch(codePosti))
                    return false;
                if (codePosti.StartsWith("0"))
                    return false;
            }
            return true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditMain.aspx");
        }
    }
}