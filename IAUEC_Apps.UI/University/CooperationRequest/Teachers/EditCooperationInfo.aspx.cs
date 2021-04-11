using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Request;
//using IAUEC_Apps.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.CooperationRequest.Teachers
{
    public partial class EditCooperationInfo : System.Web.UI.Page
    {
        ProfessorRequestBusiness ProfReqBuss = new ProfessorRequestBusiness();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        CommonBusiness CB = new CommonBusiness();
        string errorMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int code_ostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);

                if (ProfReqBuss.HasPendingRequest(code_ostad, (int)RequestTypeId.EditCooperation))
                {
                    string msg = "شما به دلیل وجود درخواست تایید نشده از این نوع درخواست نمی توانید درخواست جدید ثبت کنید.";
                    showMessage(msg,true);
                    return;
                }
                DataTable dtResult = FRB.GetOstadInfoFromHR(code_ostad);

                if (dtResult.Rows.Count == 0)
                {
                    string msg = "کد استادی شما در هیچ کدام از سامانه ها فعال نیست. لطفا جهت فعال سازی با کارشناس مربوطه تماس حاصل فرمایید";
                    RadWindowManager1.RadAlert(msg, 400, 200, "پیام سیستم", "RedirectToMain");
                    return;
                }
                Session.Add("hrInfoPeopleId", dtResult.Rows[0]["Id"]);

                int cooperation = 0;
                if (dtResult.Rows[0]["cooperation"] != DBNull.Value)
                {
                    cooperation = Convert.ToInt32(dtResult.Rows[0]["cooperation"]);
                }
                ViewState.Add("cooperation", cooperation);

                if (cooperation == 1 || cooperation == 2)
                {
                    chbkCooperation.SelectedValue = cooperation.ToString();
                }
                if (cooperation == 3)
                {
                    chbkCooperation.Items[0].Selected = true;
                    chbkCooperation.Items[1].Selected = true;
                }
                DataTable dtDanesh = CB.SelectAllDaneshkade();
                chbkDaneshkade.DataSource = dtDanesh;
                chbkDaneshkade.DataValueField = "ID";
                chbkDaneshkade.DataTextField = "namedanesh";
                chbkDaneshkade.DataBind();

                string field = getSelectedFields();
                if (field != "")
                {
                    DataTable dtDaneshkade = FRB.GetDaneshkadeByGroup(field);
                    chbkDaneshkade.ClearSelection();
                    foreach (DataRow item in dtDaneshkade.Rows)
                        if (chbkDaneshkade.Items.FindByValue(item["iddanesh"].ToString()) != null)
                            chbkDaneshkade.Items.FindByValue(item["iddanesh"].ToString()).Selected = true;
                    //foreach (DataRow item in dtDaneshkade.Rows)
                    //{
                    //    switch (item["iddanesh"].ToString())
                    //    {
                    //        case "1":
                    //            chbkDaneshkade.Items.FindByValue("1").Selected = true;
                    //            break;

                    //        case "2":
                    //            chbkDaneshkade.Items.FindByValue("2").Selected = true;
                    //            break;

                    //        case "3":
                    //            chbkDaneshkade.Items.FindByValue("3").Selected = true;
                    //            break;

                    //        case "8":
                    //            chbkDaneshkade.Items.FindByValue("8").Selected = true;
                    //            break;
                    //    }
                    //}
                    getGroupByDaneshkadeValue();
                    checkCheckBoxGroup();
                }

                addDepratements(Convert.ToInt32(Session["hrInfoPeopleId"]));
            }
        }

        private string getSelectedFields()
        {
            string Field = "";
            DataTable dtGroup = new DataTable();
            dtGroup = FRB.GetGroupByCode(int.Parse(Session["hrInfoPeopleId"].ToString()));
            if (dtGroup.Rows.Count != 0)
            {
                string Resault = "idgroup in (";
                foreach (DataRow dr in dtGroup.Rows)
                {
                    Resault += dr["idgroup"].ToString() + "" + ",";
                }
                Resault += ")";
                Field = Resault.Replace(",)", ")").Replace("(,", "(");

                Session["Field"] = Field;
            }
            return Field;
        }

        private void getGroupByDaneshkadeValue()
        {
            if (chbkDaneshkade.SelectedIndex != -1)
            {
                DataTable dtt = new DataTable();

                foreach (ListItem itemm in chbkDaneshkade.Items)
                {
                    if (itemm.Selected)
                    {
                        dtt.Merge(FRB.GetDepartmentList(Convert.ToInt32(itemm.Value)));
                    }
                }
                chbkGroup.DataSource = dtt;
                chbkGroup.DataTextField = "namegroup";
                chbkGroup.DataValueField = "idgroup";
                chbkGroup.RepeatColumns = 4;
                chbkGroup.RepeatDirection = RepeatDirection.Horizontal;
                chbkGroup.DataBind();

            }
        }

        private void checkCheckBoxGroup()
        {
            List<string> departmanList = FRB.GetGroupList(int.Parse(Session["hrInfoPeopleId"].ToString()));
            foreach (string var in departmanList)
            {
                ListItem l = chbkGroup.Items.FindByValue(var);
                if (l != null)
                    l.Selected = true;
            }
        }

        private void updateGroupOstad()
        {
            if (chbkDaneshkade.SelectedValue == "")
            {
                ValidationSummary5.ToolTip = "لطفا حداقل یک دانشکده جهت همکاری را انتخاب فرمایید.";
                ValidationSummary5.ShowValidationErrors = true;
                ValidationSummary5.HeaderText = "1";
                //msgIncorrectScan.Text = "لطفا حداقل یک دانشکده جهت همکاری را انتخاب فرمایید.";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CantRegModal();", true);
                return;

            }


            List<string> lsgroups =
            chbkGroup.Items.Cast<ListItem>()
                .Where(li => li.Selected)
                .Select(li => li.Value)
                .ToList();
            if (lsgroups.Count == 0)
            {
                ValidationSummary5.HeaderText = "لطفا حداقل یک گروه آموزشی جهت همکاری را انتخاب فرمایید.";
                //msgIncorrectScan.Text = "لطفا حداقل یک گروه آموزشی جهت همکاری را انتخاب فرمایید.";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CantRegModal();", true);
                return;

            }
            //FRB.SetProfessorGroups(int.Parse(Session["hrInfoPeopleId"].ToString()), lsgroups);
        }

        protected void btnSubmitCooperation_Click(object sender, EventArgs e)
        {
            var isChanged = false;
            ProfessorEditRequestDTO oEditDTO = new ProfessorEditRequestDTO();
            oEditDTO.Code_Ostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            oEditDTO.Createdate = DateTime.Now.ToPeString();
            oEditDTO.RequestTypeID = (int)RequestTypeId.EditCooperation; // درخواست ویرایش مشخصات فردی
            oEditDTO.RequestLogID = (int)RequestLogId.submitted; // در حال بررسی
            oEditDTO.HR_InfoPeople_Id = Convert.ToInt32(Session["hrInfoPeopleId"]);
            oEditDTO.ChangeSet = 1;
            oEditDTO.ChangeList = new List<ChangedInfoDTO>();

            if (IsValid && CheckDaneshkadeAndGroupsRelation() && checkChangesInCooperation())
            {

                int NewCooperation = 0;
                if ((chbkCooperation.Items.Cast<ListItem>().Count(li => li.Selected)) > 1)
                    NewCooperation = 3;
                else
                    NewCooperation = Convert.ToInt32(chbkCooperation.SelectedValue);
                if (CheckInfo(NewCooperation))
                {
                    updateGroupOstad();
                    if (NewCooperation != 0)
                    {
                        int OldCoopertaion = Convert.ToInt32(ViewState["cooperation"]);
                        if (OldCoopertaion != NewCooperation)
                        {
                            ChangedInfoDTO oChange = new ChangedInfoDTO();
                            oChange.Code_Ostad = oEditDTO.Code_Ostad;
                            oChange.ControlId = chbkCooperation.ID;
                            oChange.ControlToFieldId = 42;//in Tbl_TextBoxToSidaField chbkCooperation
                            oChange.NewValue = NewCooperation.ToString();
                            oChange.OldValue = OldCoopertaion.ToString();
                            oEditDTO.ChangeList.Add(oChange);

                            isChanged = true;
                        }
                    }
                }
                else
                {
                    string msg = "به دلیل  نقص اطلاعات شما، امکان ثبت در سامانه آموزش جهت تدریس وجود ندارد، لطفا ابتدا " + errorMsg + " خود را تکمیل فرمائید..";
                    showMessage(msg,true);
                    return;
                }

                //var dNewValue = string.Join(",", chbkDaneshkade.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList());
                //string dOldValue = ViewState["idDanesh"].ToString();
                //if (dNewValue != dOldValue)
                //{
                //    ChangedInfoDTO dChange = new ChangedInfoDTO();
                //    dChange.Code_Ostad = oEditDTO.Code_Ostad;
                //    dChange.ControlId = chbkDaneshkade.ID;
                //    dChange.ControlToFieldId = 53;
                //    dChange.NewValue = dNewValue;
                //    dChange.OldValue = dOldValue;
                //    oEditDTO.ChangeList.Add(dChange);

                //    isChanged = true;
                //}

                var gNewValue = string.Join(",", chbkGroup.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList());
                string gOldValue = ViewState["idGroup"].ToString();
                if (gNewValue != gOldValue)
                {
                    ChangedInfoDTO gChange = new ChangedInfoDTO();
                    gChange.Code_Ostad = oEditDTO.Code_Ostad;
                    gChange.ControlId = chbkGroup.ID;
                    gChange.ControlToFieldId = 54;
                    gChange.NewValue = gNewValue;
                    gChange.OldValue = gOldValue;
                    oEditDTO.ChangeList.Add(gChange);

                    isChanged = true;
                }

                if (isChanged)
                {
                    int Id = ProfReqBuss.AddNewEditRequest(oEditDTO);
                    string msg = null;
                    if (Id > 0)
                    {

                        CB.InsertIntoStudentLog(oEditDTO.HR_InfoPeople_Id.ToString(), DateTime.Now.ToString("HH:mm"), 13, 36, Id.ToString());
                        msg = "درخواست شما با شماره " + Id.ToString() + "با موفقیت ثبت گردید.";
                        Session["hrInfoPeopleId"] = null;
                    }
                    else
                    {
                        msg = "خطا در هنگام ثبت درخواست ، لطفا مجددا تلاش کنید.";
                    }

                    showMessage(msg,true);
                }
            }
            else if (!string.IsNullOrEmpty(errorMsg))
            {
                showMessage(errorMsg);
            }
        }

        private bool CheckDaneshkadeAndGroupsRelation()
        {
            errorMsg = string.Empty;
            var selectedDaneshkade = chbkDaneshkade.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();

            string Resault = "idgroup in (" + string.Join(",", chbkGroup.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value));
            Resault += ")";
            List<string> groupDaneshkade = FRB.GetDaneshkadeByGroup(Resault).AsEnumerable().Select(s => s.Field<decimal>("iddanesh").ToString()).ToList();
            var nullGroups = selectedDaneshkade.Except(groupDaneshkade);
            if (nullGroups.Count() > 0)
            {
                var daneshkadeNames = new List<string>();
                foreach (var item in nullGroups)
                    daneshkadeNames.Add(chbkDaneshkade.Items.Cast<ListItem>().Where(li => li.Value == item).Select(li => li.Text).FirstOrDefault());
                errorMsg = "برای " + string.Join(", ", daneshkadeNames) + " گروه آموزشی انتخاب نشده است.";
            }
            if (string.IsNullOrEmpty(errorMsg))
                return true;
            else
                return false;
        }
        private void addDepratements(int infoPeopleId)
        {
            var idDanesh = string.Empty;
            var idGroup = string.Empty;
            ViewState["idDanesh"] = idDanesh;
            ViewState["idGroup"] = idGroup;
            DataTable dtDaneshkade = new DataTable();
            DataTable dtGroup = new DataTable();
            dtGroup = FRB.GetGroupByCode(infoPeopleId);
            if (dtGroup.Rows.Count != 0)
            {
                string Resault = "idgroup in (";
                foreach (DataRow dr in dtGroup.Rows)
                {
                    Resault += dr["idgroup"].ToString() + "" + ",";
                }
                Resault += ")";
                string Field = Resault.Replace(",)", ")").Replace("(,", "(");
                dtDaneshkade = FRB.GetDaneshkadeByGroup(Field);

                Session["Field"] = Field;
                foreach (DataRow item in dtDaneshkade.Rows)
                    if (chbkDaneshkade.Items.FindByValue(item["iddanesh"].ToString()) != null)
                        chbkDaneshkade.Items.FindByValue(item["iddanesh"].ToString()).Selected = true;
                //foreach (DataRow item in dtDaneshkade.Rows)
                //{
                //    switch (item["iddanesh"].ToString())
                //    {
                //        case "1":
                //            chbkDaneshkade.Items.FindByValue("1").Selected = true;
                //            break;

                //        case "2":
                //            chbkDaneshkade.Items.FindByValue("2").Selected = true;
                //            break;

                //        case "3":
                //            chbkDaneshkade.Items.FindByValue("3").Selected = true;
                //            break;

                //        case "8":
                //            chbkDaneshkade.Items.FindByValue("8").Selected = true;
                //            break;
                //    }
                //}
                if (chbkDaneshkade.SelectedIndex != -1)
                {
                    DataTable dtt = new DataTable();

                    foreach (ListItem itemm in chbkDaneshkade.Items)
                    {
                        if (itemm.Selected)
                        {
                            dtt.Merge(FRB.GetDepartmentList(Convert.ToInt32(itemm.Value)));
                        }
                    }
                    chbkGroup.DataSource = dtt;
                    chbkGroup.DataTextField = "namegroup";
                    chbkGroup.DataValueField = "idgroup";
                    chbkGroup.RepeatColumns = 4;
                    chbkGroup.RepeatDirection = RepeatDirection.Horizontal;
                    chbkGroup.DataBind();
                    List<string> departmanList = FRB.GetGroupList(infoPeopleId);


                    foreach (ListItem lch in chbkGroup.Items)
                    {
                        if (departmanList.Contains(lch.Value))
                        {
                            lch.Selected = true;
                        }
                    }
                    foreach (DataRow row in dtDaneshkade.Rows)
                        idDanesh += row["iddanesh"].ToString() + ",";
                    //foreach (DataRow row in departmanList)
                    //    idGroup += row["idgroup"] + ",";
                    ViewState["idDanesh"] = idDanesh;
                    idGroup = string.Join(",", departmanList.ToArray());
                    ViewState["idGroup"] = idGroup.Length > 0 ? idGroup + ',' : idGroup;

                    //foreach (string var in departmanList)
                    //{
                    //    if(dtt.Select("idgroup="+var).Length>0)
                    //    chbkGroup.Items.FindByValue(var).Selected = true;
                    //}
                }
            }
        }

        //private bool CheckInfo(int newCooperation)
        //{
        //    var codeOstad = Convert.ToInt32(Session["user"]);
        //    errorMsg = string.Empty;
        //    if (newCooperation != 2)
        //    {
        //        DataTable dtResult = ProfReqBuss.GetProfessorFromResearchByCode(codeOstad);
        //        if (dtResult.Rows.Count == 0)
        //        {
        //            var profReqs = ProfReqBuss.GetAllRequestsByProfCode(codeOstad);
        //            if (profReqs.Rows.Count > 0)
        //            {
        //                var prof = FRB.GetOstadInfoFromHR(codeOstad).Rows[FRB.GetOstadInfoFromHR(codeOstad).Rows.Count - 1];

        //                // Check Existing Requests
        //                var personalInfoRequested = profReqs.AsEnumerable().Where(w => w.Field<long>("RequestTypeID") == (int)RequestTypeId.EditPersonalInfo).Count() > 0;
        //                var contactInfoRequested = profReqs.AsEnumerable().Where(w => w.Field<long>("RequestTypeID") == (int)RequestTypeId.EditContactInfo).Count() > 0;
        //                var hokmRequested = profReqs.AsEnumerable().Where(w => w.Field<long>("RequestTypeID") == (int)RequestTypeId.EditHokm).Count() > 0;

        //                // Check Existing Documents
        //                var docs = ProfReqBuss.GetAllRequestDocsByProfCode(codeOstad);
        //                if (docs.Rows.Count > 0)
        //                {
        //                    if (personalInfoRequested)
        //                    {
        //                        if (
        //                            docs.AsEnumerable().Where(w => w.Field<int>("DocType") == (int)Hire.Hire.DocType.صفحه_اول_شناسنامه).Count() > 0
        //                            && docs.AsEnumerable().Where(w => w.Field<int>("DocType") == (int)Hire.Hire.DocType.اسکن_کارت_ملی).Count() > 0
        //                            && docs.AsEnumerable().Where(w => w.Field<int>("DocType") == (int)Hire.Hire.DocType.عکس_پرسنلی).Count() > 0
        //                            && docs.AsEnumerable().Where(w => w.Field<int>("DocType") == (int)Hire.Hire.DocType.آخرین_مدرک_تحصیلی).Count() > 0
        //                            )
        //                        {
        //                            if(Convert.ToInt32(prof["sex"]) == 1
        //                                && Convert.ToInt32(prof["status_nezam"]) != (int)Hire.Hire.militaryStatus.برگ_اعزام
        //                                && Convert.ToInt32(prof["status_nezam"]) != (int)Hire.Hire.militaryStatus.درحين_خدمت
        //                                && Convert.ToInt32(prof["status_nezam"]) != (int)Hire.Hire.militaryStatus.مشمول
        //                                && Convert.ToInt32(prof["status_nezam"]) != (int)Hire.Hire.militaryStatus.غير_مشمول
        //                                && docs.AsEnumerable().Where(w => w.Field<int>("DocType") == (int)Hire.Hire.DocType.اسکن_کارت_پایان_خدمت).Count() == 0
        //                                )
        //                                personalInfoRequested = false;
        //                            if(prof["BimehTypeId"] != null
        //                                || Convert.ToInt32(prof["BimehTypeId"]) == 0
        //                                || (Convert.ToInt32(prof["BimehTypeId"]) > 0
        //                                && docs.AsEnumerable().Where(w => w.Field<int>("DocType") == (int)Hire.Hire.DocType.اسکن_بیمه).Count() == 0))
        //                                personalInfoRequested = false;
        //                            if(Convert.ToInt32(prof["university"]) == 0
        //                                || (Convert.ToInt32(prof["university"]) > 0
        //                                && (Convert.ToInt32(prof["university"]) != 27 ) && Convert.ToInt32(prof["university"]) < 56
        //                                && docs.AsEnumerable().Where(w => w.Field<int>("DocType") == (int)Hire.Hire.DocType.ارزشنامه_تحصیلی_وزارت_علوم).Count() == 0))
        //                                personalInfoRequested = false;
        //                        }
        //                        else
        //                            personalInfoRequested = false;
        //                    }
        //                    if (hokmRequested)
        //                    {
        //                        if (prof["martabeh"] == null)
        //                            hokmRequested = false;
        //                        else if(Convert.ToInt32(prof["martabeh"]) > 0
        //                            && docs.AsEnumerable().Where(w => w.Field<int>("DocType") == (int)Hire.Hire.DocType.آخرین_حکم_کارگزینی).Count() == 0)
        //                            hokmRequested = false;
        //                        else if(prof["idmadrak"] == null)
        //                            hokmRequested = false;
        //                        else if (Convert.ToInt32(prof["idmadrak"]) == (int)Hire.Hire.MadrakType.دانشجوی_دکتری_بعد_امتحان_جامع
        //                            && docs.AsEnumerable().Where(w => w.Field<int>("DocType") == (int)Hire.Hire.DocType.گواهی_امتحان_جامع).Count() == 0)
        //                            hokmRequested = false;
        //                    }
        //                }
        //                else
        //                {
        //                    personalInfoRequested = false;
        //                    contactInfoRequested = false;
        //                    hokmRequested = false;
        //                }

        //                // Set Results
        //                if (!personalInfoRequested)
        //                    errorMsg += "اطلاعات فردی";
        //                if (!contactInfoRequested)
        //                {
        //                    errorMsg += errorMsg.Length > 0 ? "، " : "";
        //                    errorMsg += "اطلاعات تماس";
        //                }
        //                if (!hokmRequested)
        //                {
        //                    errorMsg += errorMsg.Length > 0 ? "، " : "";
        //                    errorMsg += "اطلاعات کارگزینی";
        //                }
        //            }
        //            else
        //                errorMsg = "اطلاعات فردی، اطلاعات تماس و اطلاعات کارگزینی";
        //        }
        //    }
        //    if (string.IsNullOrEmpty(errorMsg))
        //        return true;
        //    else
        //        return false;
        //}

        private bool CheckInfo(int newCooperation)
        {
            var personalInfoRequested = true;
            var hokmRequested = true;
            var contactInfoRequested = true;
            var codeOstad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            errorMsg = string.Empty;
            if (newCooperation != 2)
            {
                //var prof = ProfReqBuss.GetProfessorFromResearchByCode(codeOstad);
                var prof = FRB.getOstadInf(codeOstad);
                var lastHokm = ProfReqBuss.GetLastHokmInfoByInfoPeopleID(Convert.ToInt32(Session["hrInfoPeopleId"]));
                lastHokm = new ProfessorHokmDTO();
                if (lastHokm.Code_Ostad == 0)
                {
                    var lastReqOfHokm = ProfReqBuss.GetRequestByTypeAndStatus("19", "6");
                    if (lastReqOfHokm != null)
                        if (lastReqOfHokm.Count > 0)
                        {
                            var aa = lastReqOfHokm.AsEnumerable().OrderByDescending(a => a.Id).Where(a => a.Code_Ostad == codeOstad).FirstOrDefault();
                            if (aa != null)
                            {
                                if (aa.Code_Ostad == codeOstad)
                                {
                                    int reqID = aa.Id;
                                    lastHokm = ProfReqBuss.GetNewHokmInfo(reqID);
                                }
                            }
                        }
                }
                //if (prof.Rows.Count > 0)
                if (lastHokm != null && lastHokm.Code_Ostad != 0)

                {
                    var infoId = Convert.ToInt32(prof.hrId);
                    var shenasnameDoc = ProfReqBuss.GetDocByInfoIdAndType(infoId, (int)Hire.Hire.DocType.صفحه_اول_شناسنامه);
                    //var cardMeliDoc = ProfReqBuss.GetDocByInfoIdAndType(infoId, (int)Hire.Hire.DocType.اسکن_کارت_ملی);
                    //var personalPictureDoc = ProfReqBuss.GetDocByInfoIdAndType(infoId, (int)Hire.Hire.DocType.عکس_پرسنلی);
                    var madrakDoc = ProfReqBuss.GetDocByInfoIdAndType(infoId, (int)Hire.Hire.DocType.آخرین_مدرک_تحصیلی);

                    if (!string.IsNullOrEmpty(prof.idd)
                        && !string.IsNullOrEmpty(prof.idd_Melli)
                        && !string.IsNullOrEmpty(prof.salTavalod)
                        && !string.IsNullOrEmpty(prof.fatherName)
                        && !string.IsNullOrEmpty(prof.name)
                        && !string.IsNullOrEmpty(prof.family)
                        && prof.maghta > 0
                        && prof.reshte > 0
                        && !string.IsNullOrEmpty(prof.salMadrak)
                        && prof.nameUniMadrak > 0
                        && prof.typeUniMadrak > 0
                        && !string.IsNullOrEmpty(prof.sanavat)
                        //&& !string.IsNullOrEmpty(prof.taahol)
                        && !string.IsNullOrEmpty(prof.siba)
                        && madrakDoc.Rows.Count > 0
                        && shenasnameDoc.Rows.Count > 0
                        //&& cardMeliDoc.Rows.Count > 0
                        //&& personalPictureDoc.Rows.Count > 0
                        )
                    {
                        var nezamDoc = ProfReqBuss.GetDocByInfoIdAndType(infoId, (int)Hire.Hire.DocType.اسکن_کارت_پایان_خدمت);
                        if (((prof.sexIsMan
                            //&& !string.IsNullOrEmpty(prof.Rows[0]["status_nezam"].ToString())
                            && prof.nezam != (int)Hire.Hire.militaryStatus.درحين_خدمت
                            && prof.nezam != (int)Hire.Hire.militaryStatus.غير_مشمول
                            && prof.nezam != (int)Hire.Hire.militaryStatus.برگ_اعزام
                            && prof.nezam != (int)Hire.Hire.militaryStatus.مشمول
                            && nezamDoc.Rows.Count > 0)
                            || !prof.sexIsMan
                            || (prof.nezam == (int)Hire.Hire.militaryStatus.درحين_خدمت
                            || prof.nezam == (int)Hire.Hire.militaryStatus.غير_مشمول
                            || prof.nezam == (int)Hire.Hire.militaryStatus.برگ_اعزام
                            || prof.nezam == (int)Hire.Hire.militaryStatus.مشمول)))
                        {
                            var BimeDoc = ProfReqBuss.GetDocByInfoIdAndType(infoId, (int)Hire.Hire.DocType.آخرین_مدرک_تحصیلی);
                            if ((prof.bimeType > 0
                                && !string.IsNullOrEmpty(prof.bimeNum)
                                && BimeDoc.Rows.Count > 0) || !prof.bime)
                            {
                                var vezaratDoc = ProfReqBuss.GetDocByInfoIdAndType(infoId, (int)Hire.Hire.DocType.ارزشنامه_تحصیلی_وزارت_علوم);
                                if (prof.keshvar > 0
                                    && (prof.keshvar != 27
                                    && vezaratDoc.Rows.Count > 0)
                                    || prof.keshvar == 27
                                    )
                                {
                                    var govahiJameDoc = ProfReqBuss.GetDocByInfoIdAndType(infoId, (int)Hire.Hire.DocType.گواهی_امتحان_جامع);
                                    if (prof.maghta > 0 && prof.keshvar > 0
                                        && (
                                        (prof.maghta == 13
                                        && prof.keshvar == 27
                                        && govahiJameDoc.Rows.Count > 0
                                        )
                                        || (prof.maghta != 13
                                        || prof.keshvar == 27)
                                        ))
                                    {
                                        if ((lastHokm.Martabeh >= 0
                                            && lastHokm.Payeh > 0
                                            && lastHokm.Type_Estekhdam >= 0
                                            && (lastHokm.Nahveh_Hamk > 0)
                                            && (lastHokm.Uni_Khedmat > 0)
                                            && (lastHokm.Uni_KhedmatType > 0)
                                            && !string.IsNullOrEmpty(lastHokm.Date_Hokm)
                                            && lastHokm.Number_Hokm != ""
                                            && (lastHokm.MablaghHokm > 0)
                                            && !string.IsNullOrEmpty(lastHokm.HokmUrl))
                                            || (lastHokm.Martabeh < 0)
                                            )
                                        {
                                            return true;
                                        }
                                        else
                                            hokmRequested = false;
                                    }
                                    else
                                        personalInfoRequested = false;
                                }
                                else
                                    personalInfoRequested = false;
                            }
                            else
                                personalInfoRequested = false;
                        }
                        else
                            personalInfoRequested = false;
                    }
                    else
                        personalInfoRequested = false;
                }
                else
                {
                    personalInfoRequested = false;
                    hokmRequested = false;
                }

                if (!string.IsNullOrEmpty(prof.telHome)
                    && !string.IsNullOrEmpty(prof.telMobile)
                    && !string.IsNullOrEmpty(prof.addressHome)
                    && !string.IsNullOrEmpty(prof.codePosti)
                    && !string.IsNullOrEmpty(prof.email)
                    && (prof.ostanHome > 0)
                    && (prof.shahrHome > 0)
                    )
                {
                    contactInfoRequested = false;
                }
            }
            else return true;
            // Set Results
            if (personalInfoRequested)
                errorMsg += "اطلاعات فردی";
            if (contactInfoRequested)
            {
                errorMsg += errorMsg.Length > 0 ? "، " : "";
                errorMsg += "اطلاعات تماس";
            }
            if (hokmRequested)
            {
                errorMsg += errorMsg.Length > 0 ? "، " : "";
                errorMsg += "اطلاعات کارگزینی";
            }
            if (string.IsNullOrEmpty(errorMsg))
                return true;
            else
                return false;
        }

        private bool checkChangesInCooperation()
        {
            int NewCooperation = 0;
            if ((chbkCooperation.Items.Cast<ListItem>().Count(li => li.Selected)) > 1)
                NewCooperation = 3;
            else
                NewCooperation = Convert.ToInt32(chbkCooperation.SelectedValue);

            int OldCoopertaion = Convert.ToInt32(ViewState["cooperation"]);

            switch (NewCooperation)
            {
                case 0:
                    showMessage("نحوه همکاری نمیتواند بدون مقدار باشد.");
                    return false;
                case 1:
                    if (OldCoopertaion == 2 || OldCoopertaion == 3)
                    {
                        showMessage("با توجه به سوابق موجود در سامانه پرتال پژوهشی، امکان حذف درخواست همکاری پژوهشی مقدور نمی باشد");
                        return false;
                    }
                    break;
                case 2:
                    if (OldCoopertaion == 1 || OldCoopertaion == 3)
                    {
                        showMessage("با توجه به سوابق موجود در سامانه آموزشی(خدمات الکترونیکی)، امکان حذف درخواست همکاری آموزشی مقدور نمی باشد");
                        return false;
                    }
                    break;
            }
            return true;
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (chbkCooperation.SelectedIndex != -1)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditMain.aspx");
        }

        protected void chbkDaneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            getGroupByDaneshkadeValue();
        }

        protected void customValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (chbkDaneshkade.SelectedValue == "")
            {
                customValidator1.ErrorMessage = "لطفا حداقل یک دانشکده جهت همکاری را انتخاب فرمایید.";
                args.IsValid = false;
                //msgIncorrectScan.Text = "لطفا حداقل یک دانشکده جهت همکاری را انتخاب فرمایید.";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CantRegModal();", true);
                return;

            }


            List<string> lsgroups =
            chbkGroup.Items.Cast<ListItem>()
                .Where(li => li.Selected)
                .Select(li => li.Value)
                .ToList();
            if (lsgroups.Count == 0)
            {
                customValidator1.ErrorMessage = "لطفا حداقل یک گروه آموزشی جهت همکاری را انتخاب فرمایید.";
                //msgIncorrectScan.Text = "لطفا حداقل یک گروه آموزشی جهت همکاری را انتخاب فرمایید.";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CantRegModal();", true);
                args.IsValid = false;
                return;

            }
            args.IsValid = true;
        }

        private void showMessage(string message,bool redirect=false)
        {
            RadWindowManager1.RadAlert(message, 400, 100, "پیام سیستم",redirect? "RedirectToMain":null);
        }
    }
}