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
    public partial class EditPersonalInfo : System.Web.UI.Page
    {
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        ProfessorRequestBusiness ProfReqBuss = new ProfessorRequestBusiness();
        CommonBusiness CB = new CommonBusiness();
        List<ChangedInfoDTO> ChangeList = new List<ChangedInfoDTO>();
        const string ScanUrl = "ScanMadarekURL";
        const string scanShenasname = "scanSh";//DocType=1    controlId(49,51)
        const string scanMadrakTahsili = "scanMdk";//DocType=4    controlId(7,9)
        const string scanNezam = "scanNzm";//DocType=7    controlId(30)
        const string scanBime = "scanBme";//DocType =6   controlId(43)
        const string scanBazneshaste = "scanBzn";//DocType=18    controlId(44)
        const string scanArzeshname = "scanArz";//DocType=14    controlId(15)
        const string scanPersonelly = "scanprsnl";//DocType=10    controlId(1000)
        const string scanMeli = "scanMelli";//DocType=5    controlId(500)
        const string listOfChanges = "mChangeList";
        const string listOfRequiredScans = "scanNeeded";
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
                ViewState.Add(scanShenasname, "");
                ViewState.Add(scanMadrakTahsili, "");
                ViewState.Add(scanNezam, "");
                ViewState.Add(scanBime, "");
                ViewState.Add(scanBazneshaste, "");
                ViewState.Add(scanArzeshname, "");
                ViewState.Add(scanPersonelly, "");
                ViewState.Add(scanMeli, "");
                ViewState.Add(listOfRequiredScans, "");
                int codeostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
                if (ProfReqBuss.HasPendingRequest(codeostad, (int)RequestTypeId.EditPersonalInfo))
                {
                    string msg = "شما به دلیل وجود درخواست تایید نشده از این نوع درخواست نمی توانید درخواست جدید ثبت کنید.";
                    RadWindowManager1.RadAlert(msg, 400, 200, "پیام سیستم", "RedirectToMain");
                    return;
                }
                LoadInfoToControls();
            }
        }
        private void getScansAreNotInDB()
        {
            int hrId = Convert.ToInt32(Session["hrInfoPeopleId"]);
            DataTable dtScan = FRB.GetRequestScanDocID(hrId);
            if (dtScan.Rows.Count > 0 && dtScan.Select("status=1").Length > 0)
                dtScan = dtScan.Select("status=1").CopyToDataTable();
            else
                dtScan.Clear();
            ChangedInfoDTO cid = new ChangedInfoDTO();
            ViewState[listOfRequiredScans] = "";
            if (dtScan.Rows.Count > 0)
            {
                if (dtScan.Select("doc_type=" + 1).Length == 0)
                {
                    ViewState[listOfRequiredScans] = "49,";
                    enableScan(49);
                }
                if (dtScan.Select("doc_type=" + 4).Length == 0)
                {
                    ViewState[listOfRequiredScans] += "7,";
                    enableScan(7);
                }
                if (dtScan.Select("doc_type=" + 6).Length == 0)
                {
                    cid = ChangeList.Where(i => i.ControlToFieldId == 43).First();
                    if (cid.OldValue == "1")
                    {
                        ViewState[listOfRequiredScans] += "40,";
                        enableScan(40);
                    }
                }
                if (dtScan.Select("doc_type=" + 7).Length == 0)
                {
                    cid = ChangeList.Where(i => i.ControlToFieldId == 53).First();
                    if (cid.OldValue == "1")
                    {
                        cid = ChangeList.Where(i => i.ControlToFieldId == 30).First();
                        if (Convert.ToInt32(cid.OldValue) != (int)Hire.Hire.militaryStatus.برگ_اعزام &&
                                        Convert.ToInt32(cid.OldValue) != (int)Hire.Hire.militaryStatus.مشمول &&
                                        Convert.ToInt32(cid.OldValue) != (int)Hire.Hire.militaryStatus.درحين_خدمت &&
                                        Convert.ToInt32(cid.OldValue) != (int)Hire.Hire.militaryStatus.غير_مشمول)
                        {
                            ViewState[listOfRequiredScans] += "30,";
                            enableScan(30);
                        }
                    }

                }
                if (dtScan.Select("doc_type=" + 14).Length == 0)
                {
                    cid = ChangeList.Where(i => i.ControlToFieldId == 15).First();
                    if (cid.OldValue != "27" && cid.OldValue != "")
                    {
                        ViewState[listOfRequiredScans] += "15,";
                        enableScan(15);
                    }
                }
                if (dtScan.Select("doc_type=" + 18).Length == 0)
                {
                    cid = ChangeList.Where(i => i.ControlToFieldId == 44).First();
                    if (cid.OldValue == "True")
                    {
                        ViewState[listOfRequiredScans] += "44,";
                        enableScan(44);
                    }
                }
                if (dtScan.Select("doc_type=" + 10).Length == 0)
                {

                    ViewState[listOfRequiredScans] += "1000,";
                    enableScan(1000);

                }
                if (dtScan.Select("doc_type=" + 5).Length == 0)
                {

                    ViewState[listOfRequiredScans] += "500";
                    enableScan(500);

                }
            }
            else
            {
                enableScan(49);
                enableScan(7);
                enableScan(1000);
                enableScan(500);

                ViewState[listOfRequiredScans] = "49,7,";
                if (lbloptionalPersonelly.Visible)
                    ViewState[listOfRequiredScans] += "1000,";
                if (lblOptionalMelli.Visible)
                    ViewState[listOfRequiredScans] += "500,";
                cid = ChangeList.Where(i => i.ControlToFieldId == 43).First();
                if (cid.OldValue != null)
                    if (cid.OldValue != "0")
                    {
                        enableScan(40);
                        ViewState[listOfRequiredScans] += "40,";
                    }
                cid = ChangeList.Where(i => i.ControlToFieldId == 53).First();
                if (cid.OldValue == "1")
                {
                    cid = ChangeList.Where(i => i.ControlToFieldId == 30).First();
                    if (Convert.ToInt32(cid.OldValue) != (int)Hire.Hire.militaryStatus.برگ_اعزام &&
                                    Convert.ToInt32(cid.OldValue) != (int)Hire.Hire.militaryStatus.مشمول &&
                                    Convert.ToInt32(cid.OldValue) != (int)Hire.Hire.militaryStatus.درحين_خدمت &&
                                    Convert.ToInt32(cid.OldValue) != (int)Hire.Hire.militaryStatus.غير_مشمول)
                    {
                        ViewState[listOfRequiredScans] += "30,";
                        enableScan(30);
                    }
                }
                cid = ChangeList.Where(i => i.ControlToFieldId == 15).First();
                if (cid.OldValue != "27" && cid.OldValue != "")
                {
                    ViewState[listOfRequiredScans] += "15,";
                    enableScan(15);
                }
                cid = ChangeList.Where(i => i.ControlToFieldId == 44).First();
                if (cid.OldValue == "True")
                {
                    ViewState[listOfRequiredScans] += "44";
                    enableScan(44);
                }

            }
        }

        private void LoadInfoToControls()
        {
            ListItem itmSelect = new ListItem("انتخاب کنید", "-1");
            ListItem itmOther = new ListItem("سایر", "0");
            int codeostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            DTO.University.Faculty.editInfoStruct editInfo = FRB.getOstadInf(codeostad);

            //Session.Add("hrInfoPeopleId", editInfo.hrId);
            DataTable dtResault = FRB.GetOstadInfoFromHR(codeostad);

            if (dtResault.Rows.Count == 0)
            {
                string msg = "کد استادی شما در هیچ کدام از سامانه ها فعال نیست. لطفا جهت فعال سازی با کارشناس مربوطه تماس حاصل فرمایید";
                RadWindowManager1.RadAlert(msg, 400, 200, "پیام سیستم", "RedirectToMain");
                return;
            }
            Session.Add("hrInfoPeopleId", dtResault.Rows[0]["Id"]);


            DataTable dtControlToSidaList = FRB.GetAllControlToSidaFields();

            txtCodeMeli.Text = editInfo.idd_Melli;
            txtFirstName.Text = editInfo.name;
            txtFamily.Text = editInfo.family;
            txtFatherName.Text = editInfo.fatherName;
            AddValueToChangeList(codeostad, dtControlToSidaList, txtFatherName.Text, txtFatherName.ID);
            txtShCode.Text = editInfo.idd;
            AddValueToChangeList(codeostad, dtControlToSidaList, txtShCode.Text, txtShCode.ID);
            txtYearBorn.Text = editInfo.salTavalod;
            AddValueToChangeList(codeostad, dtControlToSidaList, txtYearBorn.Text, txtYearBorn.ID);

            drpNezam.DataSource = CB.GetStatusMilitary_fcoding();
            drpNezam.DataTextField = "namecoding";
            drpNezam.DataValueField = "id";
            drpNezam.DataBind();
            drpNezam.Items.Insert(0, new ListItem(itmSelect.Text, itmSelect.Value));
            drpNezam.Items.Insert(drpNezam.Items.Count, new ListItem(itmOther.Text, itmOther.Value));
            drpNezam.SelectedValue = editInfo.nezam.ToString();
            AddValueToChangeList(codeostad, dtControlToSidaList, drpNezam.SelectedValue, drpNezam.ID + "Value");

            rdblMarriage.SelectedValue = editInfo.taahol ? "2" : "1";
            AddValueToChangeList(codeostad, dtControlToSidaList, rdblMarriage.SelectedValue, rdblMarriage.ID);

            rblGender.SelectedValue = editInfo.sexIsMan ? "1" : "2";
            AddValueToChangeList(codeostad, dtControlToSidaList, rblGender.SelectedValue, rblGender.ID);
            if (!editInfo.sexIsMan)
                pnlMilitary.Visible = false;

            drpLastMaghta.Items.Clear();
            drpLastMaghta.DataSource = CB.GetCodingByTypeId(2);
            drpLastMaghta.DataValueField = "Id";
            drpLastMaghta.DataTextField = "namecoding";
            drpLastMaghta.DataBind();
            drpLastMaghta.Items.Insert(0, new ListItem(itmSelect.Text, itmSelect.Value));
            drpLastMaghta.SelectedValue = editInfo.maghta.ToString();
            AddValueToChangeList(codeostad, dtControlToSidaList, editInfo.maghta == 0 ? "" : editInfo.maghta.ToString(), drpLastMaghta.ID + "Value");

            DataTable dtField = CB.SelectField_fcoding();
            for (int i = 0; i <= dtField.Rows.Count - 1; i++)
            {
                dtField.Rows[i]["nameresh"] = dtField.Rows[i]["nameresh"].ToString().Replace("ي", "ی");
            }
            drpReshte.DataSource = dtField;
            drpReshte.DataTextField = "nameresh";
            drpReshte.DataValueField = "id";
            drpReshte.DataBind();
            drpReshte.Items.Insert(0, new RadComboBoxItem(itmSelect.Text, itmSelect.Value));
            drpReshte.Items.Insert(drpReshte.Items.Count, new RadComboBoxItem(itmOther.Text, itmOther.Value));
            drpReshte.SelectedValue = editInfo.reshte.ToString();
            AddValueToChangeList(codeostad, dtControlToSidaList, editInfo.reshte == 0 ? "" : editInfo.reshte.ToString(), drpReshte.ID + "Value");



            drpUniversityType.SelectedValue = editInfo.typeUniMadrak.ToString();
            AddValueToChangeList(codeostad, dtControlToSidaList, drpUniversityType.SelectedValue, drpUniversityType.ID + "Value");


            txtSiba.Text = editInfo.siba;
            AddValueToChangeList(codeostad, dtControlToSidaList, editInfo.siba, txtSiba.ID);


            txtYearGetMadrak.Text = editInfo.salMadrak;
            AddValueToChangeList(codeostad, dtControlToSidaList, editInfo.salMadrak, txtYearGetMadrak.ID);


            txtSanavat.Text = editInfo.sanavat;
            AddValueToChangeList(codeostad, dtControlToSidaList, editInfo.sanavat, txtSanavat.ID);

            DataTable dtCountrySource = CB.GetNameCountry_fcoding();
            drpCountry.DataSource = dtCountrySource.Select("id<56").CopyToDataTable();
            drpCountry.DataTextField = "namecoding";
            drpCountry.DataValueField = "id";
            drpCountry.DataBind();
            drpCountry.Items.Insert(0, new ListItem(itmSelect.Text, itmSelect.Value));
            drpCountry.SelectedValue = editInfo.keshvar.ToString();
            AddValueToChangeList(codeostad, dtControlToSidaList, editInfo.keshvar.ToString(), drpCountry.ID + "Value");

            DataTable dtUniName = CB.GetNameUni_fcoding();
            for (int i = 0; i <= dtUniName.Rows.Count - 1; i++)
            {
                dtUniName.Rows[i][0] = dtUniName.Rows[i][0].ToString().Replace("ي", "ی");
            }
            drpUniName.DataSource = dtUniName;
            drpUniName.DataTextField = "namecoding";
            drpUniName.DataValueField = "ID";
            drpUniName.DataBind();
            drpUniName.Items.Insert(0, new RadComboBoxItem(itmSelect.Text, itmSelect.Value));
            drpUniName.Items.Insert(drpUniName.Items.Count, new RadComboBoxItem(itmOther.Text, itmOther.Value));
            drpUniName.SelectedValue = editInfo.nameUniMadrak.ToString();
            AddValueToChangeList(codeostad, dtControlToSidaList, drpUniName.SelectedValue, drpUniName.ID + "Value");

            if (editInfo.bime)
            {
                rdblBimehStatus.SelectedValue = "1";
                drpBimehType.Enabled = true;
                txtInsuranceNumber.Enabled = true;
                drpBimehType.SelectedValue = editInfo.bimeType.ToString();
                txtInsuranceNumber.Text = editInfo.bimeNum;
                drpBimehType.Enabled = true;
                txtInsuranceNumber.Enabled = true;
            }
            else
            {
                rdblBimehStatus.SelectedValue = "2";
            }
            AddValueToChangeList(codeostad, dtControlToSidaList, drpBimehType.SelectedValue, drpBimehType.ID + "Value");
            AddValueToChangeList(codeostad, dtControlToSidaList, txtInsuranceNumber.Text, txtInsuranceNumber.ID);

            chbkIsRetired.Checked = editInfo.bazneshaste;

            AddValueToChangeList(codeostad, dtControlToSidaList, chbkIsRetired.Checked.ToString(), "chbkIsRetired");


            ViewState.Add(listOfChanges, ChangeList);
            getScansAreNotInDB();
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

        protected void Control_StateChanged(object sender, EventArgs e)
        {
            if (ViewState[listOfChanges] != null)
            {
                ChangeList = (List<ChangedInfoDTO>)ViewState[listOfChanges];
            }
            else
            {
                throw new Exception("Empty ViewState!");
            }
            if (sender is DropDownList)
            {
                DropDownList drp = (DropDownList)sender;
                var olditem = ChangeList.Where(i => i.ControlId == drp.ID + "Value")
                                       .First();
                olditem.NewValue = drp.SelectedValue;
                enableScan(olditem.ControlToFieldId);
            }
            else if (sender is TextBox)
            {
                TextBox txt = (TextBox)sender;
                var olditem = ChangeList.Where(i => i.ControlId == txt.ID)
                                       .First();
                olditem.NewValue = txt.Text;
                enableScan(olditem.ControlToFieldId);
            }
            else if (sender is RadioButtonList)
            {
                RadioButtonList rcbl = (RadioButtonList)sender;
                var olditem = ChangeList.Where(i => i.ControlId == rcbl.ID)
                                       .First();
                olditem.NewValue = rcbl.SelectedItem.Value;
                enableScan(olditem.ControlToFieldId);
            }
            else if (sender is CheckBox)
            {
                CheckBox chbx = (CheckBox)sender;
                var olditem = ChangeList.Where(i => i.ControlId == chbx.ID)
                                       .First();
                olditem.NewValue = chbx.Checked.ToString();
                enableScan(olditem.ControlToFieldId);
            }
        }

        private void enableScan(int controlId)
        {
            switch (controlId)
            {
                case 49:
                case 50:
                case 51:
                    dvShenasname.Visible = true;
                    break;
                case 7:
                case 9:
                    dvMadrak.Visible = true;
                    break;
                case 15:
                    dvArzeshname.Visible = drpCountry.SelectedValue != "27";
                    break;
                case 30:
                    if (drpNezam.SelectedValue != "-1" &&
                        Convert.ToInt32(drpNezam.SelectedValue) != (int)Hire.Hire.militaryStatus.درحين_خدمت &&
                        Convert.ToInt32(drpNezam.SelectedValue) != (int)Hire.Hire.militaryStatus.مشمول &&
                        Convert.ToInt32(drpNezam.SelectedValue) != (int)Hire.Hire.militaryStatus.غير_مشمول &&
                        Convert.ToInt32(drpNezam.SelectedValue) != (int)Hire.Hire.militaryStatus.برگ_اعزام)
                        dvNezam.Visible = true;
                    else
                        dvNezam.Visible = false;
                    break;
                case 40:
                case 43:
                    dvBime.Visible = rdblBimehStatus.SelectedValue == "1";
                    break;
                case 44:
                    if (chbkIsRetired.Checked)
                    {
                        dvBazneshaste.Visible = true;
                        dvBime.Visible = true;
                    }
                    else
                        dvBazneshaste.Visible = false;
                    break;
                case 1000:
                    cvPersonelly.Enabled = true;
                    lbloptionalPersonelly.Visible = false;
                    break;
                case 500:
                    cvMelli.Enabled = true;
                    lblOptionalMelli.Visible = false;
                    break;
            }
        }

        protected void Radcombo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ViewState[listOfChanges] != null)
            {
                ChangeList = (List<ChangedInfoDTO>)ViewState[listOfChanges];
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
                enableScan(olditem.ControlToFieldId);
            }
            else
            {
                throw new Exception("Invalid Control Binded to Event!");
            }
        }

        protected void btnSubmitChanges_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && isInformationValid())
            {
                if (ProfReqBuss.HasPendingRequest(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]), (int)RequestTypeId.EditPersonalInfo))
                {
                    string msg = "شما به دلیل وجود درخواست تایید نشده از این نوع درخواست نمی توانید درخواست جدید ثبت کنید.";
                    RadWindowManager1.RadAlert(msg, 400, 200, "پیام سیستم", "RedirectToMain");
                    return;
                }
                submitChanges();
            }
        }

        private void submitChanges()
        {
            bool thereIsChangeInInf = false;
            bool scanIsRequired = false;

            ProfessorEditRequestDTO oEditDTO = new ProfessorEditRequestDTO();
            List<ChangedInfoDTO> oFullChangeList = (List<ChangedInfoDTO>)ViewState[listOfChanges];
            oEditDTO.ChangeList = new List<ChangedInfoDTO>();
            foreach (ChangedInfoDTO item in oFullChangeList)
            {
                if (!string.IsNullOrWhiteSpace(item.NewValue))
                {
                    if (item.NewValue != item.OldValue)
                    {
                        item.State = FieldChangeState.Submitted;
                        oEditDTO.ChangeList.Add(item);
                    }
                }
            }
            thereIsChangeInInf = oEditDTO.ChangeList.Count > 0;

            scanIsRequired = ViewState[listOfRequiredScans].ToString() != "";

            List<string> ScanNeedList = new List<string>();

            if (scanIsRequired)
            {
                string listScan = ViewState[listOfRequiredScans].ToString();
                listScan = listScan.EndsWith(",") ? listScan.TrimEnd(',') : listScan;
                ScanNeedList = listScan.Split(',').ToList();
            }
            #region get ScanNeedList
            if (thereIsChangeInInf)
            {
                foreach (ChangedInfoDTO item in oEditDTO.ChangeList)
                {
                    switch (item.ControlToFieldId)
                    {
                        case 49:
                        case 50:
                        case 51:
                            if (!ScanNeedList.Contains("49"))
                                ScanNeedList.Add("49");
                            break;
                        case 7:
                        case 9:
                            if (!ScanNeedList.Contains("7"))
                                ScanNeedList.Add("7");

                            break;

                        case 15:
                            if (drpCountry.SelectedValue != "27" && !ScanNeedList.Contains("15"))
                            {
                                ScanNeedList.Add("15");
                            }

                            break;
                        case 30:
                            if (Convert.ToInt32(drpNezam.SelectedValue) != (int)Hire.Hire.militaryStatus.برگ_اعزام &&
                                       Convert.ToInt32(drpNezam.SelectedValue) != (int)Hire.Hire.militaryStatus.مشمول &&
                                       Convert.ToInt32(drpNezam.SelectedValue) != (int)Hire.Hire.militaryStatus.درحين_خدمت &&
                                       Convert.ToInt32(drpNezam.SelectedValue) != (int)Hire.Hire.militaryStatus.غير_مشمول &&
                                       !ScanNeedList.Contains("30"))
                            {
                                ScanNeedList.Add("30");
                            }
                            break;
                        case 44:
                            if (!ScanNeedList.Contains("44") && chbkIsRetired.Checked)
                                ScanNeedList.Add("44");
                            break;
                        case 40:
                        case 43:
                            if (rdblBimehStatus.SelectedItem.Value == "1")
                            {
                                if (!ScanNeedList.Contains("40"))
                                    ScanNeedList.Add("40");
                            }
                            else
                                ScanNeedList.Remove("40");


                            break;
                    }
                }
            }

            if (ScanNeedList.Contains("15") && drpCountry.SelectedValue == "27")
                ScanNeedList.Remove("15");
            if ((Convert.ToInt32(drpNezam.SelectedValue) == (int)Hire.Hire.militaryStatus.برگ_اعزام ||
                                       Convert.ToInt32(drpNezam.SelectedValue) == (int)Hire.Hire.militaryStatus.مشمول ||
                                       Convert.ToInt32(drpNezam.SelectedValue) == (int)Hire.Hire.militaryStatus.درحين_خدمت ||
                                       Convert.ToInt32(drpNezam.SelectedValue) == (int)Hire.Hire.militaryStatus.غير_مشمول) &&
                                       ScanNeedList.Contains("30"))
                ScanNeedList.Remove("30");
            if (ScanNeedList.Contains("44") && !chbkIsRetired.Checked)
                ScanNeedList.Remove("44");

            #endregion

            oEditDTO.ScanList = new Dictionary<int, ImageStructure>();

            foreach (string controlTofield in ScanNeedList)
            {
                ImageStructure img = getScan(Convert.ToInt32(controlTofield));
                if (!string.IsNullOrEmpty(img.imageUrl))
                    oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(Convert.ToInt32(controlTofield)), img);

            }
            if (!ScanNeedList.Contains("1000"))
            {
                ImageStructure img = getScan(1000);
                if (!string.IsNullOrEmpty(img.imageUrl))
                {
                    oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(1000), img);
                    ScanNeedList.Add("1000");
                }
            }
            if (!ScanNeedList.Contains("500"))
            {
                ImageStructure img = getScan(500);
                if (!string.IsNullOrEmpty(img.imageUrl))
                {
                    oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(500), img);
                    ScanNeedList.Add("500");
                }
            }


            if (oEditDTO.ScanList.Count != ScanNeedList.Count)
            {
                string msgErrorInUploading = "آپلود مدارک با خطا مواجه شده است و یا شما مدارک لازم را آپلود نکرده اید. لطفا دوباره امتحان کنید. در صورت دریافت دوباره این پیام لطفا از ابتدا اقدام به ویرایش اطلاعات فرمایید";
                showMessage(msgErrorInUploading);
                return;
            }
            if (ScanNeedList.Count == 0 && !thereIsChangeInInf)
            {
                string msgErrorInUploading = "شما هیچ تغییری در این بخش به وجود نیاورده اید.";
                showMessage(msgErrorInUploading);
                return;
            }
            oEditDTO.Code_Ostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            oEditDTO.Createdate = DateTime.Now.ToPeString();
            oEditDTO.RequestTypeID = (int)RequestTypeId.EditPersonalInfo; // درخواست ویرایش مشخصات فردی
            oEditDTO.RequestLogID = (int)RequestLogId.submitted; // در حال بررسی
            oEditDTO.HR_InfoPeople_Id = Convert.ToInt32(Session["hrInfoPeopleId"]);
            oEditDTO.ChangeSet = 1;

            if (Session[ScanUrl] != null)
            {
                oEditDTO.ScanImageUrl = Session["ScanMadarekURL"].ToString();
            }

            int Id = ProfReqBuss.AddNewEditRequest(oEditDTO);

            string msg = null;
            if (Id > 0)
            {
                msg = "درخواست شما با شماره " + Id.ToString() + "با موفقیت ثبت گردید.";
                CB.InsertIntoStudentLog(oEditDTO.HR_InfoPeople_Id.ToString(), DateTime.Now.ToString("HH:mm"), 13, 30, Id.ToString());
                Session["hrInfoPeopleId"] = null;
                showMessage(msg);
                Response.Redirect("EditMain.aspx");
            }
            else
            {
                msg = "خطا در هنگام ثبت درخواست ، لطفا مجددا تلاش کنید.";
                showMessage(msg);

            }
        }

        private int getDocumentIdByControlToFieldId(int ControlToFieldId)
        {
            switch (ControlToFieldId)
            {
                case 49:
                case 50:
                case 51:
                    return (int)Hire.Hire.DocType.صفحه_اول_شناسنامه;
                case 7:
                case 9:
                    return (int)Hire.Hire.DocType.آخرین_مدرک_تحصیلی;
                case 15:
                    return (int)Hire.Hire.DocType.ارزشنامه_تحصیلی_وزارت_علوم;

                case 30:
                    return (int)Hire.Hire.DocType.اسکن_کارت_پایان_خدمت;

                case 44:
                    return (int)Hire.Hire.DocType.حکم_بازنشستگی;

                case 40:
                case 43:
                    return (int)Hire.Hire.DocType.اسکن_بیمه;
                case 1000:
                    return (int)Hire.Hire.DocType.عکس_پرسنلی;
                case 500:
                    return (int)Hire.Hire.DocType.اسکن_کارت_ملی;
                default:
                    return 0;

            }
        }

        private ImageStructure getScan(int controlToFieldId)
        {
            string imageUrl = "";
            ImageStructure imgStr = new ImageStructure();
            switch (controlToFieldId)
            {

                case 1000:
                    if (ruScanPersonelly.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile objP = ruScanPersonelly.UploadedFiles[0];

                    imageUrl = saveAsScan(scanPersonelly, ref objP);
                    ruScanPersonelly.UploadedFiles[0].InputStream.Close();
                    objP.SaveAs(Server.MapPath(imageUrl));
                    break;
                case 500:
                    if (ruScanMelli.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile objM = ruScanMelli.UploadedFiles[0];

                    imageUrl = saveAsScan(scanMeli, ref objM);
                    ruScanMelli.UploadedFiles[0].InputStream.Close();
                    objM.SaveAs(Server.MapPath(imageUrl));
                    break;
                case 49:
                case 50:
                case 51:
                    if (ruScanShenasname.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj1 = ruScanShenasname.UploadedFiles[0];

                    imageUrl = saveAsScan(scanShenasname, ref obj1);
                    ruScanShenasname.UploadedFiles[0].InputStream.Close();
                    obj1.SaveAs(Server.MapPath(imageUrl));
                    break;
                case 7:
                case 9:
                    if (ruScanMadrak.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj2 = ruScanMadrak.UploadedFiles[0];
                    imageUrl = saveAsScan(scanMadrakTahsili, ref obj2);
                    ruScanMadrak.UploadedFiles[0].InputStream.Close();
                    obj2.SaveAs(Server.MapPath(imageUrl), true);

                    break;
                case 15:
                    if (ruScanArzeshname.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj3 = ruScanArzeshname.UploadedFiles[0];
                    imageUrl = saveAsScan(scanArzeshname, ref obj3);
                    ruScanArzeshname.UploadedFiles[0].InputStream.Close();
                    obj3.SaveAs(Server.MapPath(imageUrl), true);
                    break;
                case 30:
                    if (ruScanNezam.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj4 = ruScanNezam.UploadedFiles[0];
                    imageUrl = saveAsScan(scanNezam, ref obj4);
                    ruScanNezam.UploadedFiles[0].InputStream.Close();
                    obj4.SaveAs(Server.MapPath(imageUrl), true);
                    break;
                case 40:
                case 43:
                    if (ruScanBime.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj5 = ruScanBime.UploadedFiles[0];
                    imageUrl = saveAsScan(scanBime, ref obj5);
                    ruScanBime.UploadedFiles[0].InputStream.Close();
                    obj5.SaveAs(Server.MapPath(imageUrl), true);
                    break;
                case 44:
                    if (ruScanBazneshaste.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj6 = ruScanBazneshaste.UploadedFiles[0];
                    imageUrl = saveAsScan(scanBazneshaste, ref obj6);
                    ruScanBazneshaste.UploadedFiles[0].InputStream.Close();
                    obj6.SaveAs(Server.MapPath(imageUrl), true);
                    break;
                default:
                    return new ImageStructure();
            }

            if (imageUrl == "")
                return new ImageStructure();
            imgStr.imageUrl = imageUrl;
            imgStr.image = File.ReadAllBytes(Server.MapPath(imageUrl));

            return imgStr;
        }

        private string saveAsScan(string scanName, ref UploadedFile obj)
        {
            try
            {
                string Path = getPathOfScan(scanName);
                string urlPath = Path + obj.GetName().improveFileName();
                System.IO.Directory.CreateDirectory(Server.MapPath(Path));
                return urlPath;
            }
            catch (Exception ex)
            { return ""; }
        }

        private string getPathOfScan(string scanName)
        {
            string ScanMadarekURL, subPath = "~/University/CooperationRequest/Teachers/ScanMadarek/" + Session[sessionNames.userID_StudentOstad].ToString();//subPath = Server.MapPath("~/University/CooperationRequest/Teachers/ScanMadarek/") + Session["user"].ToString();

            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
            try
            {
                ScanMadarekURL = string.Format("{0}/{1}/{2}/", subPath, scanName, DateTime.Now.ToPeString("yyyyMMdd_HHmm"));
            }
            catch (Exception)
            {
                return "";
                throw;
            }
            return ScanMadarekURL;
        }

        public byte[] GetImageBytes(Stream stream)
        {
            Stream fs = stream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] img = br.ReadBytes((Int32)fs.Length);
            return img;
        }

        private bool isInformationValid()
        {
            string msg = "", msgNullValue = " نمیتواند خالی باشد. لطفا آن را اصلاح فرمایید";

            if (!txtYearGetMadrak.Text.isPersianYearCorrect())
            {
                msg = string.Format("تاریخ گرفتن مدرک اشتباه وارد شده است. لطفا آن را اصلاح فرمایید.");
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }
            if (!txtYearBorn.Text.isPersianYearCorrect())
            {
                msg = string.Format("تاریخ تولد اشتباه وارد شده است. لطفا آن را اصلاح فرمایید.");
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (int.Parse(txtYearBorn.Text) > int.Parse(txtYearGetMadrak.Text))
            {
                msg = string.Format("سال تولد نمیتواند بزرگتر از سال گرفتن مدرک باشد. لطفا آن را اصلاح فرمایید.");
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (txtFatherName.Text.Trim() == "")
            {
                msg = string.Format("{0} {1}", "نام پدر", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (txtShCode.Text.Trim() == "")
            {
                msg = string.Format("{0} {1}", "شماره شناسنامه", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (txtYearBorn.Text.Trim() == "")
            {
                msg = string.Format("{0} {1}", "سال تولد", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }


            if (txtSiba.Text.Trim() == "")
            {
                msg = string.Format("{0} {1}", "شماره سیبا", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (txtInsuranceNumber.Text.Trim() == "" && rdblBimehStatus.SelectedValue == "1")
            {
                msg = string.Format("{0} {1}", "شماره بیمه", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (drpNezam.SelectedValue == "-1")
            {
                msg = string.Format("{0} {1}", "وضعیت نظام وظیفه", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (drpLastMaghta.SelectedValue == "")
            {
                msg = string.Format("{0} {1}", "آخرین وضعیت تحصیلی", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (drpReshte.SelectedValue == "")
            {
                msg = string.Format("{0} {1}", "رشته تحصیلی", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (drpCountry.SelectedValue == "")
            {
                msg = string.Format("{0} {1}", "کشور محل اخذ مدرک", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (drpUniName.SelectedValue == "")
            {
                msg = string.Format("{0} {1}", "نام دانشگاه اخذ مدرک", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }

            if (drpBimehType.SelectedValue == "0" && rdblBimehStatus.SelectedValue == "1")
            {
                msg = string.Format("{0} {1}", "نوع بیمه", msgNullValue);
                RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }
            return true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditMain.aspx");

        }

        protected void rdblBimehStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdblBimehStatus.SelectedValue == "2") drpBimehType.SelectedValue = "0";
            drpBimehType.Enabled = rdblBimehStatus.SelectedValue == "1";
            txtInsuranceNumber.Enabled = rdblBimehStatus.SelectedValue == "1";
            if (rdblBimehStatus.SelectedItem.Value == "1")
                enableScan(40);
            Control_StateChanged(drpBimehType, null);
        }

        protected void chk_IsRetired_CheckedChanged(object sender, EventArgs e)
        {
            if (chbkIsRetired.Checked)
            {
                rdblBimehStatus.SelectedValue = "1";
                rdblBimehStatus.Enabled = false;
                drpBimehType.SelectedValue = "7";
                drpBimehType.Enabled = false;
                txtInsuranceNumber.Enabled = true;
            }
            else
            {
                List<ChangedInfoDTO> oFullChangeList = (List<ChangedInfoDTO>)ViewState[listOfChanges];
                rdblBimehStatus.Enabled = true;
                drpBimehType.Enabled = rdblBimehStatus.SelectedValue == "1";
                txtInsuranceNumber.Enabled = rdblBimehStatus.SelectedValue == "1";
            }
            Control_StateChanged(sender, null);
            Control_StateChanged(drpBimehType, null);

        }

        private void showMessage(string text)
        {
            msgIncorrectScan.Text = text;
            string txt = "CantRegModal();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", txt, true);
        }

        protected void rblGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblGender.SelectedItem.Value == "1")
            {
                pnlMilitary.Visible = true;
            }
            else
            {
                drpNezam.SelectedValue = "6";
                pnlMilitary.Visible = false;
            }
            Control_StateChanged(sender, null);
            Control_StateChanged(drpNezam, null);
        }

    }


}