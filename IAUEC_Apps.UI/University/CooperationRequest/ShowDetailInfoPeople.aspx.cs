using IAUEC_Apps.Business.university.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using System.Configuration;
using IAUEC_Apps.DTO.University.Faculty;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{//96-02-24
    public partial class ShowDetailInfoPeople : System.Web.UI.Page
    {
        InfoPeople ip = new InfoPeople();
        InfoPeople ipDb = new InfoPeople();

        CommonBusiness CB = new CommonBusiness();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        int flag = 0;
        int RoleId;

        const string doc = "document", reason_CantRegister = "CantRegister", AllDocuments = "AllDocsAreOk", PrimaryDocs = "PrimaryDocsAreOk";
        protected void Page_Load(object sender, EventArgs e)
        {
            ListItem choose = new ListItem("انتخاب کنید", "0");
            RoleId = Convert.ToInt32(Session["RoleID"]);

            if (!IsPostBack)
            {

                if (!string.IsNullOrWhiteSpace(Request.QueryString["ID"].ToString()) && Session["hrId_HireRequest"].ToString() == Request.QueryString["ID"].ToString())
                {
                    DataTable dtResault = FRB.GetInfoPeoByFilter(int.Parse(Request.QueryString["ID"].ToString()));
                    if (dtResault.Rows.Count == 0)
                    {
                        showCantContinueMessage("بارگذاری صفحه با خطا همراه بود. لطفا مجددا تلاش فرمایید.");
                        Response.Redirect("CooperationRequestProfessors.aspx", false);
                        return;
                    }
                    if (dtResault.Select("scan_document is null").Length == 0)
                    {
                        Div1.Visible = true;
                        try
                        {
                            rlv.DataSource = dtResault;
                            rlv.DataBind();
                        }
                        catch (Exception ex)
                        { return; }
                    }


                    DataTable dtScan = FRB.GetRequestScanDocID(int.Parse(Request.QueryString["ID"].ToString()));

                    flag = 0;
                    ViewState.Add(PrimaryDocs, flag);


                    if (RoleId == (int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی ||
                        RoleId == (int)DTO.RoleEnums.مسئول_حق_التدریس ||
                        RoleId == (int)DTO.RoleEnums.مدیر_ارشد ||
                        RoleId == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی ||
                        RoleId == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی)
                    {
                        checkPrimaryScan(dtScan, dtResault, dtResault.Rows[0]["martabeh"].ToString() != "8");

                    }
                    else
                    {
                        // حق تایید یا ویرایش مدارک را ندارند
                        btn_ChangeInfo.Visible = false;
                        btn_TaeedParvande.Visible = false;
                        dvEditByProfessor.Visible = false;
                        flag = 1;
                        ViewState.Add(PrimaryDocs, flag);
                    }

                    LoadInfoToForm(choose, dtResault);

                    DataTable dt4 = FRB.GetFilePDF(int.Parse(Request.QueryString["ID"].ToString()));

                    btn_ShowPDF.Visible = dt4.Rows.Count > 0;


                    if (RoleId == (int)DTO.RoleEnums.مدیر_دانشکده_فنی_و_مهندسی || RoleId == (int)DTO.RoleEnums.کارشناس_دانشکده_فنی_و_مهندسی ||
                        RoleId == (int)DTO.RoleEnums.مدیر_دانشکده_انسانی || RoleId == (int)DTO.RoleEnums.کارشناس_دانشکده_انسانی ||
                        RoleId == (int)DTO.RoleEnums.کارشناس_دانشکده_مدیریت || RoleId == (int)DTO.RoleEnums.مدیر_دانشکده_مدیریت)
                    {
                        btn_TaeedParvande.Visible = false;
                        flag = 0;
                        ViewState.Add(PrimaryDocs, flag);
                        btn_Rad.Visible = false;
                        btn_ChangeInfo.Visible = false;
                    }
                    lbl_Taeed2.Text = string.Empty;

                }
                else
                {
                    Response.Redirect("CooperationRequestProfessors.aspx", false);
                    return;
                }
            }
        }

        private void LoadInfoToForm(ListItem choose, DataTable dtResault)
        {
            //DataTable cityList = CB.GetNameCity();

            #region PersonalInfo
            txtCodeMeli.Text = dtResault.Rows[0]["idd_meli"].ToString();

            txtFirstName.Text = dtResault.Rows[0]["name"].ToString();
            txtFamily.Text = dtResault.Rows[0]["family"].ToString();
            txtFatherName.Text = dtResault.Rows[0]["namep"].ToString();
            txtShCode.Text = dtResault.Rows[0]["idd"].ToString();
            txtYearBorn.Text = dtResault.Rows[0]["sal_tav"].ToString();
            if (dtResault.Rows[0]["sex"].ToString() == "1")
            {
                rdblGender.Items[0].Selected = true;
            }
            else
            {
                rdblGender.Items[1].Selected = true;
            }
            chk_Cooperation.Items[0].Selected = dtResault.Rows[0]["Cooperation"].ToString() == ((int)Hire.Hire.Cooperation.فقط_آموزشی).ToString() || dtResault.Rows[0]["Cooperation"].ToString() == ((int)Hire.Hire.Cooperation.هم_آموزشی_هم_پژوهشی).ToString();
            chk_Cooperation.Items[1].Selected = dtResault.Rows[0]["Cooperation"].ToString() == ((int)Hire.Hire.Cooperation.فقط_پژوهشی).ToString() || dtResault.Rows[0]["Cooperation"].ToString() == ((int)Hire.Hire.Cooperation.هم_آموزشی_هم_پژوهشی).ToString();

            drpBirthCity.DataSource = CB.GetNameCity_fcoding();
            drpBirthCity.DataTextField = "Title";
            drpBirthCity.DataValueField = "ID";
            drpBirthCity.DataBind();
            drpBirthCity.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            if (dtResault.Rows[0]["mahal_tav"] == DBNull.Value)
            {
                drpBirthCity.SelectedValue = "0";
            }
            else
            {
                drpBirthCity.SelectedValue = dtResault.Rows[0]["mahal_tav"].ToString();
            }

            drpMahalSodoor.DataSource = CB.GetNameCity_fcoding();
            drpMahalSodoor.DataTextField = "Title";
            drpMahalSodoor.DataValueField = "ID";
            drpMahalSodoor.DataBind();
            drpMahalSodoor.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            if (dtResault.Rows[0]["mahal_sodor"] == DBNull.Value)
            {
                drpMahalSodoor.SelectedValue = "0";
            }
            else
            {
                drpMahalSodoor.SelectedValue = dtResault.Rows[0]["mahal_sodor"].ToString();
            }
            drpNezam.DataSource = CB.GetStatusMilitary_fcoding();
            drpNezam.DataTextField = "namecoding";
            drpNezam.DataValueField = "id";
            drpNezam.DataBind();
            drpNezam.Items.Add(new ListItem("سایر", "0"));
            drpNezam.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));

            if (dtResault.Rows[0]["status_nezam"] == DBNull.Value)
                drpNezam.SelectedValue = "-1";
            else
                drpNezam.SelectedValue = dtResault.Rows[0]["status_nezam"].ToString();


            if (dtResault.Rows[0]["sex"].ToString() == "2")
                drpNezam.SelectedValue = ((int)Hire.Hire.militaryStatus.غير_مشمول).ToString();
            drpNezam.Enabled = dtResault.Rows[0]["sex"].ToString() == "1";

            rdblMarriage.SelectedValue = dtResault.Rows[0]["marital_status"].ToString();
            if (dtResault.Rows[0]["marital_status"].ToString() == "1")
            {
                rdblMarriage.Items[0].Selected = true;
            }
            else
            {
                rdblMarriage.Items[1].Selected = true;
            }

            drpLastMaghta.SelectedValue = dtResault.Rows[0]["idmadrak"].ToString();
            drpReshte.DataSource = CB.SelectField_fcoding();
            drpReshte.DataTextField = "nameresh";
            drpReshte.DataValueField = "id";
            drpReshte.DataBind();
            drpReshte.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            if (dtResault.Rows[0]["idresh"] == DBNull.Value)
            {
                drpReshte.SelectedValue = "0";
            }
            else
            {
                drpReshte.SelectedValue = dtResault.Rows[0]["idresh"].ToString();
            }

            if (dtResault.Rows[0]["MadrakUniType"] != DBNull.Value)
                drpUniversityType.SelectedValue = dtResault.Rows[0]["MadrakUniType"].ToString();
            else
                drpUniversityType.SelectedValue = "0";

            rdblBimehStatus.SelectedValue = "2";
            if (dtResault.Rows[0]["BimehTypeId"] != DBNull.Value)
            {
                if (Convert.ToInt16(dtResault.Rows[0]["BimehTypeId"]) > 0)
                {
                    rdblBimehStatus.SelectedValue = "1";
                    drpBimehType.SelectedValue = dtResault.Rows[0]["BimehTypeId"].ToString();
                    txtInsuranceNumber.Text = dtResault.Rows[0]["num_bime"].ToString().Length > 7 ? dtResault.Rows[0]["num_bime"].ToString().PadLeft(10, '0') : dtResault.Rows[0]["num_bime"].ToString();
                    drpBimehType.Enabled = true;
                    txtInsuranceNumber.Enabled = true;
                    lbl_BimeNumber.Text = txtInsuranceNumber.Text.Trim();
                }
            }
            chk_IsRetired.Checked = dtResault.Rows[0]["IsRetired"].ToString() == "1";
            txtYearGetMadrak.Text = dtResault.Rows[0]["sal_madrak"].ToString();
            txtSanavat.Text = dtResault.Rows[0]["sanavat_tadris"].ToString();


            DataTable dtCountry = CB.GetNameCountry_fcoding();
            DataRow[] drSelect = dtCountry.Select("id<56");
            drpCountry.DataSource = drSelect.CopyToDataTable();
            drpCountry.DataTextField = "namecoding";
            drpCountry.DataValueField = "id";
            drpCountry.DataBind();
            //drpCountry.Items.Add(new ListItem("سایر", "0"));
            drpCountry.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));
            if (dtResault.Rows[0]["country"] == DBNull.Value)
            {
                drpCountry.SelectedValue = "-1";
            }
            else
            {
                drpCountry.SelectedValue = dtResault.Rows[0]["country"].ToString();
            }
            drpUniName.DataSource = CB.GetNameUni_fcoding();
            drpUniName.DataTextField = "namecoding";
            drpUniName.DataValueField = "id";
            drpUniName.DataBind();
            drpUniName.Items.Add(new ListItem("سایر", "0"));
            drpUniName.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));

            drpUniversityType.SelectedValue = dtResault.Rows[0]["madrakUniType"].ToString();

            if (dtResault.Rows[0]["university"] == DBNull.Value)
            {
                drpUniName.SelectedValue = "-1";
            }
            else
            {
                drpUniName.SelectedValue = dtResault.Rows[0]["university"].ToString();
            }
            addDepratements(int.Parse(dtResault.Rows[0]["ID"].ToString()));

            #endregion PersonalInfo

            #region workInfo

            chk_Boundhour.Checked = dtResault.Rows[0]["BoundHour"].ToString() == "True";

            if (dtResault.Rows[0]["martabeh"].ToString() == "-2" ||
                dtResault.Rows[0]["martabeh"].ToString() == "0" ||
                dtResault.Rows[0]["martabeh"].ToString() == "8")
            {
                dvHeiatElmi.Visible = false;
                pnlSabeghe.Enabled = false;
                lbl_HeiatElmi.Text = "8";
            }
            else
            {
                lbl_HeiatElmi.Text = String.Empty;
                dvHeiatElmi.Visible = true;
                pnlSabeghe.Enabled = true;

                drpMartabe.SelectedValue = drpMartabe.Items.FindByValue(dtResault.Rows[0]["martabeh"].ToString()) != null ? dtResault.Rows[0]["martabeh"].ToString() : "-1";

                txtPaye.Text = dtResault.Rows[0]["payeh"].ToString();
                if (dtResault.Rows[0]["type_estekhdam"] != DBNull.Value)
                    drpHireType.SelectedValue = dtResault.Rows[0]["type_estekhdam"].ToString();

                if (dtResault.Rows[0]["uni_khedmatType"] != DBNull.Value)
                    ddlPastUniType.SelectedValue = dtResault.Rows[0]["uni_khedmatType"].ToString();

                drpPastUni.DataSource = CB.GetNameUni_fcoding();
                drpPastUni.DataTextField = "namecoding";
                drpPastUni.DataValueField = "ID";
                drpPastUni.DataBind();
                drpPastUni.Items.Add(new ListItem("سایر", "0"));
                drpPastUni.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));
                if (dtResault.Rows[0]["uni_khedmat"] == DBNull.Value)
                {
                    drpPastUni.SelectedValue = "0";
                }
                else
                {
                    drpPastUni.SelectedValue = dtResault.Rows[0]["uni_khedmat"].ToString();
                }

                txtDateSodoorHokm.Text = dtResault.Rows[0]["date_hokm"].ToString();
                txtDateEjraHokm.Text = dtResault.Rows[0]["date_runhokm"].ToString();
                txtHokmNumber.Text = dtResault.Rows[0]["number_hokm"].ToString();
                if (dtResault.Rows[0]["nahveh_hamk"] != DBNull.Value)
                    rdblHireType.SelectedValue = dtResault.Rows[0]["nahveh_hamk"].ToString();
                //if (dtResault.Rows[0]["nahveh_hamk"].ToString() == "44")
                //{
                //    rdblHireType.Items[0].Selected = true;
                //}
                //else if (dtResault.Rows[0]["nahveh_hamk"].ToString() == "8")
                //{
                //    rdblHireType.Items[2].Selected = true;
                //}
                //else if (dtResault.Rows[0]["nahveh_hamk"].ToString() == "32")
                //{
                //    rdblHireType.Items[1].Selected = true;
                //}
                //else if (dtResault.Rows[0]["nahveh_hamk"].ToString() == "12")
                //{
                //    rdblHireType.Items[3].Selected = true;
                //}
                txtMablaghHokm.Text = dtResault.Rows[0]["MablaghHokm"].ToString();
            }
            #endregion workInfo

            #region contactInfo
            txtHomePhone.Text = dtResault.Rows[0]["tel_home"].ToString();
            txtWorkPhone.Text = dtResault.Rows[0]["tel_kar"].ToString();
            txtMobileNumber.Text = dtResault.Rows[0]["mobile"].ToString();
            txtLivingAddress.Text = dtResault.Rows[0]["add_home"].ToString();
            txtWorkingAddress.Text = dtResault.Rows[0]["add_kar"].ToString();
            txtLivingZipCode.Text = dtResault.Rows[0]["code_posti"].ToString();
            txtEmail.Text = dtResault.Rows[0]["add_email"].ToString();
            txtSiba.Text = dtResault.Rows[0]["siba"].ToString();
            drpProvince1.DataSource = FRB.GetNameProvince();
            drpProvince1.DataTextField = "Title";
            drpProvince1.DataValueField = "ID";
            drpProvince1.DataBind();
            drpProvince1.Items.Insert(0, choose);
            if (dtResault.Rows[0]["code_ostan_home"] != DBNull.Value)
            {
                drpProvince1.SelectedValue = dtResault.Rows[0]["code_ostan_home"].ToString();
            }

            drpProvince2.DataSource = FRB.GetNameProvince();
            drpProvince2.DataTextField = "Title";
            drpProvince2.DataValueField = "ID";
            drpProvince2.DataBind();
            drpProvince2.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            if (dtResault.Rows[0]["code_ostan_work"] != DBNull.Value)
            {
                drpProvince2.SelectedValue = dtResault.Rows[0]["code_ostan_work"].ToString();
            }
            drpLivingCity.DataSource = CB.getShahrestan(Convert.ToInt32(drpProvince1.SelectedValue));
            drpLivingCity.DataTextField = "Title";
            drpLivingCity.DataValueField = "ID";
            drpLivingCity.DataBind();
            drpLivingCity.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            if (dtResault.Rows[0]["code_city_home"] != DBNull.Value)
            {
                drpLivingCity.SelectedValue = dtResault.Rows[0]["code_city_home"].ToString();
            }
            drpWorkingCity.DataSource = CB.getShahrestan(Convert.ToInt32(drpProvince2.SelectedValue));
            drpWorkingCity.DataTextField = "Title";
            drpWorkingCity.DataValueField = "ID";
            drpWorkingCity.DataBind();
            drpWorkingCity.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            if (dtResault.Rows[0]["code_city_work"] != DBNull.Value)
            {
                drpWorkingCity.SelectedValue = dtResault.Rows[0]["code_city_work"].ToString();
            }
            #endregion contactInfo

            rbtnNo.Checked = true;
            if (dtResault.Rows[0]["status"].ToString() == "3")
            {

                rbtnNo.Checked = false;
                rbtnYes.Checked = true;
                RbtnPeopleEditing();
                txtEditingPeopleInfo.Text = dtResault.Rows[0]["textmessage"].ToString().Trim();

            }
        }

        private void addDepratements(int infoPeopleId)
        {
            DataTable dtAllDaneshkade = new DataTable();
            DataTable dtDaneshkade = new DataTable();
            DataTable dtGroup = new DataTable();
            dtAllDaneshkade = CB.SelectAllDaneshkade();
            chbkDaneshkade.DataValueField = "id";
            chbkDaneshkade.DataTextField = "namedanesh";
            chbkDaneshkade.DataSource = dtAllDaneshkade;
            chbkDaneshkade.DataBind();

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
                {
                    chbkDaneshkade.Items.FindByValue(item["iddanesh"].ToString()).Selected = true;
                }
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

                    //foreach (string var in departmanList)
                    //{
                    //    if(dtt.Select("idgroup="+var).Length>0)
                    //    chbkGroup.Items.FindByValue(var).Selected = true;
                    //}
                }
            }
        }

        private void checkPrimaryScan(DataTable dtScan, DataTable dtResault, bool HeyateElmi)
        {

            int flag = 1;
            ViewState.Add(PrimaryDocs, flag);

            ViewState[reason_CantRegister] = "";

            if (!checkEachScan(Hire.Hire.DocType.صفحه_اول_شناسنامه, dtScan))
                addScanToNotFounds(Hire.Hire.DocType.صفحه_اول_شناسنامه);

            if (!checkEachScan(Hire.Hire.DocType.اسکن_کارت_ملی, dtScan))
                addScanToNotFounds(Hire.Hire.DocType.اسکن_کارت_ملی);

            if (!checkEachScan(Hire.Hire.DocType.عکس_پرسنلی, dtScan))
                addScanToNotFounds(Hire.Hire.DocType.عکس_پرسنلی);

            if (!checkEachScan(Hire.Hire.DocType.آخرین_مدرک_تحصیلی, dtScan))
                addScanToNotFounds(Hire.Hire.DocType.آخرین_مدرک_تحصیلی);

            if (dtResault.Rows[0]["bimehtypeId"] != DBNull.Value)
                if (Convert.ToInt32(dtResault.Rows[0]["bimehtypeId"]) != 0)
                    if (!checkEachScan(Hire.Hire.DocType.اسکن_بیمه, dtScan))
                        addScanToNotFounds(Hire.Hire.DocType.اسکن_بیمه);

            if (dtResault.Rows[0]["isretired"] != DBNull.Value)
                if (Convert.ToInt32(dtResault.Rows[0]["isretired"]) != 0)
                    if (!checkEachScan(Hire.Hire.DocType.حکم_بازنشستگی, dtScan))
                        addScanToNotFounds(Hire.Hire.DocType.حکم_بازنشستگی);

            if (dtResault.Rows[0]["country"] != DBNull.Value)
                if (Convert.ToInt32(dtResault.Rows[0]["country"]) < 56 && Convert.ToInt32(dtResault.Rows[0]["country"]) != 27)
                    if (!checkEachScan(Hire.Hire.DocType.ارزشنامه_تحصیلی_وزارت_علوم, dtScan))
                        addScanToNotFounds(Hire.Hire.DocType.ارزشنامه_تحصیلی_وزارت_علوم);
            switch ((Hire.Hire.MadrakType)Convert.ToInt32(dtResault.Rows[0]["idmadrak"]))
            {
                case Hire.Hire.MadrakType.دانشجوی_دکتری_بعد_امتحان_جامع:
                    if (!checkEachScan(Hire.Hire.DocType.گواهی_امتحان_جامع, dtScan))
                        addScanToNotFounds(Hire.Hire.DocType.گواهی_امتحان_جامع);
                    break;
            }
            if (dtResault.Rows[0]["status_Nezam"] != DBNull.Value)
                if (dtResault.Rows[0]["sex"].ToString() != "2")
                    if (Convert.ToInt32(dtResault.Rows[0]["status_Nezam"]) != (int)Hire.Hire.militaryStatus.برگ_اعزام &&
                                                Convert.ToInt32(dtResault.Rows[0]["status_Nezam"]) != (int)Hire.Hire.militaryStatus.مشمول &&
                                                Convert.ToInt32(dtResault.Rows[0]["status_Nezam"]) != (int)Hire.Hire.militaryStatus.غير_مشمول &&
                                                Convert.ToInt32(dtResault.Rows[0]["status_Nezam"]) != (int)Hire.Hire.militaryStatus.درحين_خدمت)
                    {

                        if (!checkEachScan(Hire.Hire.DocType.اسکن_کارت_پایان_خدمت, dtScan))
                            addScanToNotFounds(Hire.Hire.DocType.اسکن_کارت_پایان_خدمت);
                    }

            switch (HeyateElmi)
            {
                case true://HeyateElmi
                    if (!checkEachScan(Hire.Hire.DocType.آخرین_حکم_کارگزینی, dtScan))
                        addScanToNotFounds(Hire.Hire.DocType.آخرین_حکم_کارگزینی);

                    break;
            }
        }

        private bool checkAllScans(DataTable dtScan)
        {
            foreach (DataRow dr in dtScan.Rows)
            {
                if (!checkEachScan((Hire.Hire.DocType)Convert.ToInt32(dr["doc_type"]), dtScan))
                    return false;
            }
            return true;
        }

        private bool checkEachScan(Hire.Hire.DocType DocumentType, DataTable dtScan)
        {
            bool found = false;
            for (int i = 0; i < dtScan.Rows.Count; i++)
            {
                if (dtScan.Rows[i]["doc_type"].ToString() == ((int)DocumentType).ToString()
                    && dtScan.Rows[i]["status"].ToString() == ((int)Hire.Hire.DocStatusEnum.approved).ToString())
                {
                    found = true;
                    break;
                }

            }
            if (!found)
            {
                return false;
            }
            return true;
        }

        private void addScanToNotFounds(Hire.Hire.DocType DocumentType)
        {
            ViewState.Add(PrimaryDocs, 0);
            ViewState[reason_CantRegister] += DocumentType.ToString().Replace('_', ' ') + "،";
        }

        private bool isThereAnyUnsavedChangeInInformation(out string ChangeList)
        {
            ChangeList = "";
            if ((DTO.RoleEnums)RoleId == DTO.RoleEnums.مدیر_ارشد ||
                (DTO.RoleEnums)RoleId == DTO.RoleEnums.مسئول_حق_التدریس ||
                (DTO.RoleEnums)RoleId == DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی ||
                (DTO.RoleEnums)RoleId == DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی ||
                (DTO.RoleEnums)RoleId == DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
            {
                setInfoPeopleField();
                DataTable dtInfo = FRB.GetInfoPeoByFilter(int.Parse(Request.QueryString["ID"].ToString()));
                getInfoPeopleField(dtInfo);
                return areDiff_inf(out ChangeList);
            }
            else
                return false;

        }

        private bool isThereAnyUnsavedChangeInScans(out string changeList)
        {
            changeList = "";
            if ((DTO.RoleEnums)RoleId == DTO.RoleEnums.مدیر_ارشد ||
                (DTO.RoleEnums)RoleId == DTO.RoleEnums.مسئول_حق_التدریس ||
                (DTO.RoleEnums)RoleId == DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی ||
                (DTO.RoleEnums)RoleId == DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی ||
                (DTO.RoleEnums)RoleId == DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
            {
                DataTable dtScan = FRB.GetRequestScanDocID(int.Parse(Request.QueryString["ID"].ToString()));
                return areDiff_doc(dtScan, out changeList);
            }
            return false;
        }

        private bool areDiff_inf(out string ChangeList)
        {
            bool hasPersonalDiff = false;
            bool hasWorkDiff = false;
            bool hasContactDiff = false;
            ChangeList = "";
            #region لیست موارد تغییر یافته
            string pattern = "{0}<br/>";
            //----------------------------------------------اطلاعات فردی---------------------------------------------------------------------
            ChangeList += areDiff_inf_PersonalInformation(pattern, out hasPersonalDiff);
            //------------------------------------------سوابق فعالیت------------------------------------------------------------
            if (ip.Martabe != 8 && ipDb.Martabe != 8)
                ChangeList += areDiff_inf_WrokInformation(pattern, out hasWorkDiff);

            //------------------------------------------اطلاعات تماس-------------------------------------------------------------
            ChangeList += areDiff_inf_ContactInformation(pattern, out hasContactDiff);


            return hasPersonalDiff || hasWorkDiff || hasContactDiff;
            #endregion
        }

        private string areDiff_inf_PersonalInformation(string pattern, out bool hasDiff)
        {
            string ChangeList = "";
            hasDiff = false;
            if (ip.Name.Trim() != ipDb.Name.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "نام");
            }
            if (ip.Family.Trim() != ipDb.Family.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "نام خانوادگی");
            }
            if (ip.Fathers_Name.Trim() != ipDb.Fathers_Name.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "نام پدر");
            }
            if (ip.Field != ipDb.Field)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "رشته تحصیلی");
            }
            if (ip.Idd.Trim() != ipDb.Idd.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "شماره شناسنامه");
            }
            if (ip.Country != ipDb.Country)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "کشور");
            }
            if (ip.Uni != ipDb.Uni)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "دانشگاه اخذ مدرک");
            }
            if (ip.TypeUniMadrak != ipDb.TypeUniMadrak)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "نوع دانشگاه اخذ مدرک");
            }
            if (ip.Cooperation != ipDb.Cooperation)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "درخواست نحوه همکاری");
            }
            if (ipDb.Sex != 2)
                if (ip.Status_Nezam.Trim() != ipDb.Status_Nezam.Trim())
                {
                    hasDiff = true;
                    ChangeList += string.Format(pattern, "وضعیت نظام وظیفه");
                }
            if (ip.Madrak != ipDb.Madrak)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "مدرک نحصیلی");
            }
            if (ip.Mahal_Sodor != ipDb.Mahal_Sodor)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "محل صدور");
            }
            if (ip.Mahal_Tav != ipDb.Mahal_Tav)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "محل تولد");
            }
            if (ip.Marital_Status != ipDb.Marital_Status)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "وضعیت تاهل");
            }
            if (ip.Bime_Type != ipDb.Bime_Type)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "نوع بیمه");
            }
            if (ip.Bime_Type != 0)
                if (ip.Number_Bime.Trim() != ipDb.Number_Bime.Trim().PadLeft(10, '0'))
                {
                    hasDiff = true;
                    ChangeList += string.Format(pattern, "شماره بیمه");
                }
            if (ip.Sal_Madrak != ipDb.Sal_Madrak)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "سال اخذ مدرک");
            }
            if (ip.Sal_Tav.Trim() != ipDb.Sal_Tav.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "سال تولد");
            }
            if (ip.Sanavat_tadris.Trim() != ipDb.Sanavat_tadris.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "سنوات تدریس");
            }
            if (ip.Sex != ipDb.Sex)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "جنسیت");
            }
            return ChangeList;
        }
        private string areDiff_inf_WrokInformation(string pattern, out bool hasDiff)
        {
            string ChangeList = "";
            hasDiff = false;
            if (ip.Martabe != ipDb.Martabe)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "مرتبه دانشگاهی");
            }
            if (ip.Payeh.Trim() != ipDb.Payeh.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "پایه");
            }
            if (ip.type_estekhdam != ipDb.type_estekhdam)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "نوع استخدام");
            }
            if (ip.TypeUniKhedmat != ipDb.TypeUniKhedmat)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "نوع دانشگاه محل خدمت");
            }
            if (ip.Uni_Khedmat != ipDb.Uni_Khedmat)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "دانشگاه محل خدمت");
            }
            if (ip.Date_Hokm.Trim() != ipDb.Date_Hokm.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "تاریخ صدور حکم کارگزینی");
            }
            if (ip.Date_RunHokm.Trim() != ipDb.Date_RunHokm.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "تاریخ اجرای حکم");
            }
            if (ip.Number_Hokm.Trim() != ipDb.Number_Hokm.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "شماره حکم");
            }
            if (ip.ISRetired != ipDb.ISRetired)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "وضعیت بازنشستگی");
            }
            if (ip.BoundHour != ipDb.BoundHour)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "متقاضی تکمیل ساعت موظفی");
            }
            if (ip.base_nahveh_hamkari != ipDb.base_nahveh_hamkari)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "نحوه همکاری در دانشگاه مبداء");
            }
            if (ip.MablaghHokm.Trim() != ipDb.MablaghHokm.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "مبلغ حکم");
            }



            return ChangeList;
        }
        private string areDiff_inf_ContactInformation(string pattern, out bool hasDiff)
        {

            string ChangeList = "";
            hasDiff = false;
            if (ip.Add_Home.Trim() != ipDb.Add_Home.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "آدرس منزل");
            }
            if (ip.Add_Kar.Trim() != ipDb.Add_Kar.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "آدرس محل کار");
            }
            if (ip.Code_City_Home != ipDb.Code_City_Home)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "شهر محل سکونت");
            }
            if (ip.Code_City_Work != ipDb.Code_City_Work)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "شهر محل کار");
            }
            if (ip.Code_Ostan_Home != ipDb.Code_Ostan_Home)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "استان محل سکونت");
            }
            if (ip.Code_Ostan_Work != ipDb.Code_Ostan_Work)
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "استان محل کار");
            }
            if (ip.Code_Posti.Trim() != ipDb.Code_Posti.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "کد پستی");
            }
            if (ip.Email.Trim() != ipDb.Email.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "پست الکترونیکی");
            }
            if (ip.Mobile.Trim() != ipDb.Mobile.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "تلفن همراه");
            }
            if (ip.Siba.Trim() != ipDb.Siba.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "شماره سیبا");
            }
            if (ip.Tel_Home.Trim() != ipDb.Tel_Home.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "تلفن منزل");
            }
            if (ip.Tel_Kar.Trim() != ipDb.Tel_Kar.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "تلفن محل کار");
            }
            if (ip.TextMessage.Trim() != ipDb.TextMessage.Trim())
            {
                hasDiff = true;
                ChangeList += string.Format(pattern, "متن");
            }



            return ChangeList;
        }

        private bool areDiff_doc(DataTable dtScan, out string changeList)
        {
            changeList = "";
            string scan = "";
            foreach (RadListViewItem item in rlv.Items)
            {
                HiddenField Hf = (HiddenField)item.FindControl("HiddenField1");
                RadioButton rb = (RadioButton)item.FindControl("rdb_Taeed");
                RadioButton rb2 = (RadioButton)item.FindControl("rdb_Naghs");
                //DropDownList drp = (DropDownList)item.FindControl("ddlMadrak");
                TextBox txt = (TextBox)item.FindControl("txt_Sharh");
                Label Name = (Label)item.FindControl("lbl_Name");
                DataRow[] drScan = dtScan.Select("doc_type=" + Hf.Value);
                scan = Name.Text + "<br/>";

                if (drScan.Length == 0)
                    changeList += scan;

                else if (drScan[0]["status"].ToString() == "0" && (rb.Checked || rb2.Checked))
                    changeList += scan;

                else if (drScan[0]["status"].ToString() == "1" && !rb.Checked)
                    changeList += scan;

                else if (drScan[0]["status"].ToString() == "2" && !rb2.Checked)
                    changeList += scan;

                else if (drScan[0]["description"].ToString().Trim() != txt.Text.Trim())
                    changeList += scan;

            }
            return changeList != "";

        }

        private void getInfoPeopleField(DataTable dataTableOstad)
        {
            ipDb = new InfoPeople();
            if (dataTableOstad.Rows.Count == 0)
                return;
            ipDb.Martabe = Convert.ToInt32(dataTableOstad.Rows[0]["martabeh"]);


            ipDb.Add_Home = dataTableOstad.Rows[0]["add_Home"].ToString().Trim();
            ipDb.Add_Kar = dataTableOstad.Rows[0]["add_kar"].ToString().Trim();
            ipDb.base_nahveh_hamkari = Convert.ToInt32(dataTableOstad.Rows[0]["nahveh_hamk"]);
            ipDb.BoundHour = Convert.ToBoolean(dataTableOstad.Rows[0]["boundHour"]);
            ipDb.Code_City_Home = Convert.ToInt32(dataTableOstad.Rows[0]["code_city_home"]);
            ipDb.Code_City_Work = Convert.ToInt32(dataTableOstad.Rows[0]["code_city_work"]);
            ipDb.Code_Ostan_Home = Convert.ToInt32(dataTableOstad.Rows[0]["code_ostan_home"]);
            ipDb.Code_Ostan_Work = Convert.ToInt32(dataTableOstad.Rows[0]["code_ostan_work"]);
            ipDb.Code_Posti = dataTableOstad.Rows[0]["code_posti"].ToString().Trim();
            ipDb.Cooperation = Convert.ToInt32(dataTableOstad.Rows[0]["cooperation"]);
            ipDb.Country = Convert.ToInt32(dataTableOstad.Rows[0]["country"]);
            if (ipDb.Martabe != 8) ipDb.Date_Hokm = dataTableOstad.Rows[0]["date_hokm"].ToString().Trim(); else ipDb.Date_Hokm = "";
            if (ipDb.Martabe != 8) ipDb.Date_RunHokm = dataTableOstad.Rows[0]["date_runhokm"].ToString().Trim(); else ipDb.Date_RunHokm = "";
            ipDb.Email = dataTableOstad.Rows[0]["add_email"].ToString().Trim();
            ipDb.Family = dataTableOstad.Rows[0]["family"].ToString().Trim();
            ipDb.Fathers_Name = dataTableOstad.Rows[0]["namep"].ToString().Trim();
            ipDb.Field = Convert.ToInt32(dataTableOstad.Rows[0]["idresh"]);
            ipDb.ID = Convert.ToInt32(dataTableOstad.Rows[0]["id"]);
            ipDb.Idd = dataTableOstad.Rows[0]["idd"].ToString().Trim();
            ipDb.Idd_Meli = dataTableOstad.Rows[0]["idd_meli"].ToString().Trim();
            ipDb.ISRetired = Convert.ToInt32(dataTableOstad.Rows[0]["isretired"]);
            if (ipDb.Martabe != 8) ipDb.MablaghHokm = dataTableOstad.Rows[0]["mablaghhokm"].ToString().Trim(); else ipDb.MablaghHokm = "";
            ipDb.Madrak = Convert.ToInt32(dataTableOstad.Rows[0]["idmadrak"]);
            ipDb.Mahal_Sodor = Convert.ToInt32(dataTableOstad.Rows[0]["mahal_sodor"]);
            ipDb.Mahal_Tav = Convert.ToInt32(dataTableOstad.Rows[0]["mahal_tav"]);
            ipDb.Marital_Status = dataTableOstad.Rows[0]["marital_status"].ToString().Trim();
            ipDb.Mobile = dataTableOstad.Rows[0]["mobile"].ToString().Trim();
            ipDb.Name = dataTableOstad.Rows[0]["name"].ToString().Trim();
            ipDb.Number_Bime = "";
            if (dataTableOstad.Rows[0]["num_bime"] != DBNull.Value)
            {
                if (dataTableOstad.Rows[0]["num_bime"].ToString().Trim() != "0")
                {
                    ipDb.Number_Bime = dataTableOstad.Rows[0]["num_bime"].ToString().Trim();
                    ipDb.Bime_Type = Convert.ToInt32(dataTableOstad.Rows[0]["bimehtypeid"]);
                }
            }

            if (ipDb.Martabe != 8) ipDb.Number_Hokm = dataTableOstad.Rows[0]["number_hokm"].ToString().Trim(); else ipDb.Number_Hokm = "";
            if (ipDb.Martabe != 8) ipDb.Payeh = dataTableOstad.Rows[0]["payeh"].ToString().Trim(); else ipDb.Payeh = "";
            ipDb.Sal_Madrak = Convert.ToInt32(dataTableOstad.Rows[0]["sal_madrak"]);
            ipDb.Sal_Tav = dataTableOstad.Rows[0]["sal_tav"].ToString().Trim();
            ipDb.Sanavat_tadris = dataTableOstad.Rows[0]["sanavat_tadris"].ToString().Trim();
            ipDb.Sex = Convert.ToInt32(dataTableOstad.Rows[0]["sex"]);
            ipDb.Siba = dataTableOstad.Rows[0]["siba"].ToString().Trim();
            ipDb.status = Convert.ToInt32(dataTableOstad.Rows[0]["status"]);
            ipDb.Status_Nezam = dataTableOstad.Rows[0]["status_nezam"].ToString().Trim();
            ipDb.Tel_Home = dataTableOstad.Rows[0]["tel_home"].ToString().Trim();
            ipDb.Tel_Kar = dataTableOstad.Rows[0]["tel_kar"].ToString().Trim();
            if (ipDb.status == 3)
                ipDb.TextMessage = dataTableOstad.Rows[0]["textmessage"].ToString().Trim();
            else
                ipDb.TextMessage = "";
            ipDb.type_estekhdam = Convert.ToInt32(dataTableOstad.Rows[0]["type_estekhdam"]);
            if (dataTableOstad.Rows[0]["uni_khedmatType"] != DBNull.Value)
                ipDb.TypeUniKhedmat = Convert.ToInt32(dataTableOstad.Rows[0]["uni_khedmatType"]);
            else
                ipDb.TypeUniKhedmat = 0;
            ipDb.Uni = Convert.ToInt32(dataTableOstad.Rows[0]["university"]);
            if (dataTableOstad.Rows[0]["MadrakUniType"] != DBNull.Value)
                ipDb.TypeUniMadrak = Convert.ToInt32(dataTableOstad.Rows[0]["MadrakUniType"]);
            else
                ipDb.TypeUniMadrak = 0;
            ipDb.Uni_Khedmat = Convert.ToInt32(dataTableOstad.Rows[0]["uni_khedmat"]);
            //ip_db.userID = dataTableOstad.Rows[0]["userid"].ToString().Trim();
        }

        private void setInfoPeopleField()
        {
            ip.ID = int.Parse(Request.QueryString["ID"].ToString());
            ip.Name = txtFirstName.Text;
            ip.Family = txtFamily.Text;
            ip.Fathers_Name = txtFatherName.Text;
            ip.Idd = txtShCode.Text;
            ip.Idd_Meli = txtCodeMeli.Text;
            ip.Sal_Tav = txtYearBorn.Text;
            ip.Sex = Convert.ToInt32(rdblGender.SelectedValue);
            ip.Mahal_Tav = Convert.ToInt32(drpBirthCity.SelectedValue);
            ip.Mahal_Sodor = Convert.ToInt32(drpMahalSodoor.SelectedValue);
            ip.Madrak = Convert.ToInt32(drpLastMaghta.SelectedValue);
            if (drpNezam.SelectedValue == "سایر")
            {
                ip.Status_Nezam = "0";
            }
            else
            {
                ip.Status_Nezam = drpNezam.SelectedValue;
            }
            if (rdblMarriage.SelectedValue != "")
            {
                ip.Marital_Status = rdblMarriage.SelectedValue;
            }

            ip.Field = Convert.ToInt32(drpReshte.SelectedValue);
            if (!string.IsNullOrWhiteSpace(txtYearGetMadrak.Text))
            {
                ip.Sal_Madrak = Convert.ToInt32(txtYearGetMadrak.Text);
            }

            ip.Country = Convert.ToInt32(drpCountry.SelectedValue);
            if (drpUniName.SelectedValue == "سایر")
            {
                ip.Uni = 0;
            }
            else
            {
                ip.Uni = Convert.ToInt32(drpUniName.SelectedValue);
            }
            ip.TypeUniMadrak = Convert.ToInt32(drpUniversityType.SelectedValue);

            if (txtSanavat.Text != null)
            {
                ip.Sanavat_tadris = txtSanavat.Text;
            }

            if (lbl_HeiatElmi.Text != "8")
            {

                ip.Martabe = Convert.ToInt32(drpMartabe.SelectedValue);
                ip.Payeh = txtPaye.Text;
                ip.type_estekhdam = Convert.ToInt32(drpHireType.SelectedValue);
                ip.Uni_Khedmat = Convert.ToInt32(drpPastUni.SelectedValue);
                ip.TypeUniKhedmat = Convert.ToInt32(ddlPastUniType.SelectedValue);

                ip.Date_Hokm = txtDateSodoorHokm.Text;
                ip.Date_RunHokm = txtDateEjraHokm.Text;
                ip.Number_Hokm = txtHokmNumber.Text.Trim();
                ip.MablaghHokm = txtMablaghHokm.Text;
                if (rdblHireType.SelectedValue == "")
                {

                }
                else
                {
                    ip.base_nahveh_hamkari = Convert.ToInt32(rdblHireType.SelectedValue);
                }
                ip.Bime_Type = Convert.ToInt32(drpBimehType.SelectedValue);
                ip.Number_Bime = txtInsuranceNumber.Text;
                ip.Siba = txtSiba.Text;
                if (chk_IsRetired.Checked == true)
                {
                    ip.ISRetired = 1;
                }
                if (chk_Boundhour.Checked)
                    ip.BoundHour = true;
                else
                    ip.BoundHour = false;
            }
            else
            {
                ip.Martabe = Convert.ToInt32(lbl_HeiatElmi.Text);
                ip.Bime_Type = Convert.ToInt32(drpBimehType.SelectedValue);
                ip.Number_Bime = txtInsuranceNumber.Text;
                ip.Siba = txtSiba.Text;

                ip.Payeh = "";
                ip.Date_Hokm = "";
                ip.Date_RunHokm = "";
                ip.Number_Hokm = "";
                ip.MablaghHokm = "";
            }


            ip.Tel_Home = txtHomePhone.Text;
            ip.Tel_Kar = txtWorkPhone.Text;
            ip.Mobile = txtMobileNumber.Text;
            ip.Email = txtEmail.Text;
            if (drpProvince1.SelectedValue != "0")
            {
                ip.Code_Ostan_Home = Convert.ToInt32(drpProvince1.SelectedValue);
            }
            if (drpLivingCity.SelectedValue != "0")
            {
                ip.Code_City_Home = Convert.ToInt32(drpLivingCity.SelectedValue);
            }

            ip.Code_Posti = txtLivingZipCode.Text;
            ip.Add_Home = txtLivingAddress.Text;
            if (drpProvince2.SelectedValue != "0")
            {
                ip.Code_Ostan_Work = Convert.ToInt32(drpProvince2.SelectedValue);
            }
            if (drpWorkingCity.SelectedValue != "0")
            {
                ip.Code_City_Work = Convert.ToInt32(drpWorkingCity.SelectedValue);
            }
            ip.Add_Kar = txtWorkingAddress.Text;
            if (chk_Cooperation.Items[0].Selected == true)
            {
                ip.Cooperation = 1;
            }
            if (chk_Cooperation.Items[1].Selected == true)
            {
                ip.Cooperation = 2;
            }
            if (chk_Cooperation.Items[0].Selected == true && chk_Cooperation.Items[1].Selected == true)
            {
                ip.Cooperation = 3;
            }
            ip.status = (int)getLastStatus(ip.ID);
            if (rbtnYes.Checked)
            {

                ip.status = 3;
                if (!string.IsNullOrEmpty(txtEditingPeopleInfo.Text) || !string.IsNullOrWhiteSpace(txtEditingPeopleInfo.Text))
                {
                    ip.TextMessage = txtEditingPeopleInfo.Text;
                }
                else
                {
                    ip.TextMessage = "لطفا اطلاعات خود را ویرایش نمایید";
                }
            }
            else
            {
                //ip.status = 0;
                ip.TextMessage = string.Empty;
            }
            if (rdblBimehStatus.SelectedValue == "1")
            {
                ip.Bime_Type = Convert.ToInt32(drpBimehType.SelectedValue);
            }
            else
            {
                ip.Number_Bime = "";
            }
        }

        private DTO.StatusEnum getLastStatus(int personalId)
        {
            DataTable dtResult = FRB.GetInfoPeoByFilter(personalId);
            return (DTO.StatusEnum)Convert.ToInt32(dtResult.Rows[0]["status"]);
        }

        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        private void SendSMSToProfessor(string MobileNumber, string Message)
        {
            //lbl_Resault.Text = CB.SendSMSByMobile(MobileNumber, Message);
            //lbl_Resault.Text = CB.sendSMS(MobileNumber, Message);
            //if (lbl_Resault.Text != "1" && lbl_Resault.Text != "Username not found")
            //{
            //    string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
            //    Lbl_Status.Text = CB.ShowStatusSMS(codeAsanak);
            //    if (Lbl_Status.Text.Substring(12, (Lbl_Status.Text.Length) - 15) == "NotFound")
            //    {
            //        string ss = "-1";
            //        int status = Convert.ToInt32(ss);
            //        DataTable dt = new DataTable();
            //        dt = CB.GetMessage(ss);
            //        string messageStatus = dt.Rows[0][0].ToString();
            //    }
            //    else
            //    {
            //        string ss = (Lbl_Status.Text.Substring(32, (Lbl_Status.Text.Length) - 104));
            //        ss = Regex.Replace(ss, @"[^\d]", "");
            //        int status = Convert.ToInt32(ss);
            //        DataTable dt = new DataTable();
            //        dt = CB.GetMessage(ss);
            //        string messageStatus = dt.Rows[0][0].ToString();
            //    }
            //}
            string smsStatusText; bool sentSMS;
            CB.sendSMS(MobileNumber, Message, out sentSMS, out smsStatusText);
        }

        private int getStatus(bool Taeid)
        {
            DataTable dtInfoPeople = FRB.GetInfoPeoByFilter(int.Parse(Request.QueryString["ID"].ToString()));
            if (dtInfoPeople.Rows[0]["status"].ToString() == ((int)DTO.StatusEnum.انتقالی_از_سیدا).ToString())
                return (int)DTO.StatusEnum.انتقالی_از_سیدا;

            if (Taeid)
            {
                if (rbtnYes.Checked)
                    return (int)DTO.StatusEnum.ویرایش;

                if (RoleId == (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی || RoleId == (int)DTO.RoleEnums.کارشناس_پژوهش)
                    return (int)DTO.StatusEnum.تایید_پژوهش;

                if (RoleId == (int)DTO.RoleEnums.مدیر_ارشد || RoleId == (int)DTO.RoleEnums.مسئول_حق_التدریس || RoleId == (int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
                    return (int)DTO.StatusEnum.تایید;
            }
            else
            {
                if (RoleId == (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی || RoleId == (int)DTO.RoleEnums.کارشناس_پژوهش)
                    return (int)DTO.StatusEnum.رد_پژوهش;

                if (RoleId == (int)DTO.RoleEnums.مدیر_ارشد || RoleId == (int)DTO.RoleEnums.مسئول_حق_التدریس || RoleId == (int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
                    return (int)DTO.StatusEnum.رد;
            }
            return 0;
        }

        private void RbtnPeopleEditing()
        {
            if (rbtnYes.Checked)
            {
                lblEditingPeopleInfo.Visible = true;
                txtEditingPeopleInfo.Visible = true;
            }
            else
            {
                lblEditingPeopleInfo.Visible = false;
                txtEditingPeopleInfo.Visible = false;
            }
        }

        private bool checkValidationOfInformation(out string message)
        {
            message = "";

            #region checkNames
            if (!txtFirstName.Text.isCorrectStringInputs())
            {
                message = message.mergeMessage("نام");
            }
            if (!txtFamily.Text.isCorrectStringInputs())
            {
                message = message.mergeMessage("نام خانوادگی");
            }
            if (!txtFatherName.Text.isCorrectStringInputs())
            {
                message = message.mergeMessage("نام پدر");
            }

            #endregion

            #region checkPhones
            if (!txtMobileNumber.Text.isValidMobile())
            {
                message = message.mergeMessage("تلفن همراه");
            }
            if (!txtHomePhone.Text.isValidPhone())
            {
                message = message.mergeMessage("تلفن منزل");
            }
            if (!txtWorkPhone.Text.isValidPhone())
            {
                message = message.mergeMessage("تلفن محل کار");
            }
            #endregion

            #region checkNumericInputs
            if (!txtLivingZipCode.Text.isZipCodeValid())
                message = message.mergeMessage("کد پستی ");
            #endregion

            return message != "";
        }

        private string areAllInformationValid()
        {
            string value = "";
            if (rdblGender.SelectedValue == "0")
                value += "جنسیت،";
            if (chk_Cooperation.SelectedValue == "0")
                value += "نحوه همکاری،";

            if (drpBirthCity.SelectedValue == null)
                value += "شهر تولد،";

            if (drpMahalSodoor.SelectedValue == null)
                value += "محل صدور،";

            if (drpNezam.SelectedValue == "null")
                value += "وضعیت نظام،";

            if (rdblMarriage.SelectedValue == "0")
                value += "تاهل،";

            if (drpLastMaghta.SelectedValue == null)
                value += "مقطع تحصیلی،";

            if (drpReshte.SelectedValue == null)
                value += "رشته،";

            if (rdblBimehStatus.SelectedValue == "0")
                value += "وضعیت بیمه،";

            if (drpCountry.SelectedValue == null)
                value += "کشور،";

            if (drpUniName.SelectedValue == null)
                value += "نام دانشگاه،";

            //if (departeman)
            //    return false;
            //if (daneshkade)
            //    return false;
            if (dvHeiatElmi.Visible)
            {
                if (drpHireType.SelectedValue == null)
                    value += "نوع استخدام،";

                if (drpPastUni.SelectedValue == null)
                    value += "محل خدمت،";

            }
            if (drpProvince1.SelectedValue == "0")
                value += "استان سکونت،";

            if (drpLivingCity.SelectedValue == "0")
                value += "شهر سکونت،";
            return value;
            //if (rdblGender.SelectedValue == "0")
            //    return false;
            //if (chk_Cooperation.SelectedValue == "0")
            //    return false;
            //if (drpBirthCity.SelectedValue == null)
            //    return false;
            //if (drpMahalSodoor.SelectedValue == null)
            //    return false;
            //if (drpNezam.SelectedValue == "null")
            //    return false;
            //if (rdblMarriage.SelectedValue == "0")
            //    return false;
            //if (drpLastMaghta.SelectedValue == null)
            //    return false;
            //if (drpReshte.SelectedValue == null)
            //    return false;
            //if (rdblBimehStatus.SelectedValue == "0")
            //    return false;
            //if (drpCountry.SelectedValue == null)
            //    return false;
            //if (drpUniName.SelectedValue == null)
            //    return false;
            ////if (departeman)
            ////    return false;
            ////if (daneshkade)
            ////    return false;
            //if (dvHeiatElmi.Visible)
            //{
            //    if (drpHireType.SelectedValue == null)
            //        return false;
            //    if (drpPastUni.SelectedValue == null)
            //        return false;
            //}
            //if (drpProvince1.SelectedValue == null)
            //    return false;
            //if (drpLivingCity.SelectedValue == null)
            //    return false;

            //return true;
        }

        private void setLog(DTO.eventEnum eventType, int OstadCode, string Description)
        {
            int userId;//کاربری که لاگین کرده
            //eventType//کد کاری که انجام شده. 
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            string description;//توضیحات اختیاری
            int modifyId;//کد درخواست ویرایش شده. ویرایش اطلاعات فردی،اطلاعات تماس و ...
            userId = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            appId = 13;
            modifyId = OstadCode;
            description = Description;
            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)eventType, description, modifyId);
        }

        private void showCantContinueMessage(string reason)
        {
            msgIncorrectScan.Text = reason;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "CantRegModal();", true);
        }

        //private DTO.StatusEnum getNewStatus()
        //{
        //    DTO.StatusEnum st = DTO.StatusEnum.درحال_بررسی;
        //    if (rbtnYes.Checked)
        //        st = DTO.StatusEnum.ویرایش;
        //    if(rdblBimehStatus.SelectedValue=="1" && )
        //}



        protected void btn_ShowPDF_Click(object sender, EventArgs e)
        {
            DataTable dt4 = FRB.GetFilePDF(int.Parse(Request.QueryString["ID"].ToString()));
            if (dt4.Rows.Count == 0)
            {
                rwd.RadAlert("رزومه ای بارگذاری نشده است", 0, 100, "پیام", "");
            }
            else
            {
                if (dt4.Rows[0]["ext"].ToString() == "jpg")
                {
                    Response.ContentType = "application/jpg";// doc.DOCUMENT_TYPE;
                }
                else
                {
                    Response.ContentType = "pdf";// doc.DOCUMENT_TYPE;
                }
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename=" + dt4.Rows[0]["name"].ToString() + " " + dt4.Rows[0]["family"].ToString() + "." + dt4.Rows[0]["ext"].ToString());
                Response.BinaryWrite((byte[])dt4.Rows[0]["scan_document"]);
                Response.Flush();
                Response.End();
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (RoleId == (int)DTO.RoleEnums.مدیر_ارشد)
            {
                if (Session["Page"].ToString() == "1")
                {
                    Response.Redirect("CooperationRequest.aspx?id=" + generaterandomstr() + "@A" + "94" + "-" + generaterandomstr());
                }
                else
                {
                    Response.Redirect("CooperationRequestProfessors.aspx");
                }
            }
            else if (RoleId == (int)DTO.RoleEnums.مدیر_دانشکده_فنی_و_مهندسی || RoleId == (int)DTO.RoleEnums.مدیر_دانشکده_مدیریت || RoleId == (int)DTO.RoleEnums.مدیر_دانشکده_انسانی ||
                RoleId == (int)DTO.RoleEnums.کارشناس_دانشکده_فنی_و_مهندسی || RoleId == (int)DTO.RoleEnums.کارشناس_دانشکده_مدیریت || RoleId == (int)DTO.RoleEnums.کارشناس_دانشکده_انسانی)
            {
                Response.Redirect("CooperationRequest.aspx?id=" + generaterandomstr() + "@A" + "94" + "-" + generaterandomstr());
            }
            else
            {
                Response.Redirect("CooperationRequestProfessors.aspx");
            }
        }

        protected void btn_Rad_Click(object sender, EventArgs e)
        {
            MsgConf.Text = "آیا از رد درخواست مطمئن هستید";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "radModal();", true);
        }

        /// <summary>
        /// تایید رزومه و درخواست به طور کلی
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Taeed_Click(object sender, EventArgs e)
        {
            string msg_hasAnyChange = "", msg_ChangeList_Info = "", msg_ChangeList_Scan = "";
            if (isThereAnyUnsavedChangeInInformation(out msg_ChangeList_Info))
                msg_hasAnyChange = "اطلاعات استاد";
            if (isThereAnyUnsavedChangeInScans(out msg_ChangeList_Scan))
            {
                if (msg_hasAnyChange != "")
                    msg_hasAnyChange += " و ";
                msg_hasAnyChange += "مدارک ارسالی توسط استاد";
            }
            if (msg_hasAnyChange != "")
            {
                showCantContinueMessage(string.Format("{0} {1} {2} <br/>{3}", "شما تغییرات زیر را در", msg_hasAnyChange, "به وجود آورده اید ولی هنوز آن را ثبت نکرده اید. لطفا ابتدا تغییرات را ثبت کرده و سپس مجددا دکمه تایید را فشار دهید.", msg_ChangeList_Info + "\r\n" + msg_ChangeList_Scan));

                return;
            }

            flag = (int)ViewState[PrimaryDocs];
            int status = getStatus(true);
            if (flag == 1)
            {

                DataTable dtInfoPeople = FRB.GetInfoPeoByFilter(int.Parse(Request.QueryString["ID"].ToString()));

                if (RoleId == (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی || RoleId == (int)DTO.RoleEnums.کارشناس_پژوهش)
                {
                    DataTable dtPazhuhesh = FRB.InsertToPortalPazhuhesh(int.Parse(Request.QueryString["ID"].ToString()), txtCodeMeli.Text.ToString());
                    if (dtPazhuhesh != null && dtPazhuhesh.Rows.Count > 0)
                    {

                        try
                        {
                            FRB.UpdateInfoPeople(int.Parse(dtInfoPeople.Rows[0]["ID"].ToString()), status);
                            setLog(DTO.eventEnum.تایید_پژوهش, Convert.ToInt32(dtInfoPeople.Rows[0]["ID"].ToString()), "");
                            if (dtInfoPeople.Rows[0]["mobile"] != DBNull.Value)
                            {
                                DataTable dt1 = CB.GetAppIDMessage(0, 9, 5, 1);
                                string Message = "با سلام" + "\r\n" + "درخواست شما مورد تأیید کارشناس پژوهش قرار گرفته است" + "." + dt1.Rows[0]["Text"].ToString() + "\r\n" + "نام کاربری : " + dtPazhuhesh.Rows[0]["ocode"].ToString() + "\r\n" + "رمز عبور :" + CommonBusiness.DecryptPass(dtPazhuhesh.Rows[0]["password_ost"].ToString()) + "\r\n" + "از طریق آدرس" + "\r\n" + "https://service.iauec.ac.ir" + "\r\n" + "به پورتال پژوهشی واحد الکترونیکی وارد شوید" + "\r\n" + "معاونت فنی دانشگاه آزاد واحد الکترونیکی";

                                string MobileNumber = dtPazhuhesh.Rows[0]["mobile"].ToString();
                                SendSMSToProfessor(MobileNumber, Message);
                            }
                        }
                        catch (Exception ex)
                        {
                            showCantContinueMessage("خطا زمان تایید درخواست همکاری پژوهشی. لطفا کد زیر را به مدیر سیستم ارائه دهید: id= " + dtInfoPeople.Rows[0]["ID"].ToString() + "    " + ex.Message);
                        }
                    }
                    else
                    {
                        showCantContinueMessage("خطا در زمان تایید درخواست همکاری پژوهشی. لطفا مجددا تلاش فرمایید.");
                    }
                }
                else if (RoleId == (int)DTO.RoleEnums.مدیر_ارشد || RoleId == (int)DTO.RoleEnums.مسئول_حق_التدریس || RoleId == (int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
                {
                    try
                    {
                        FRB.UpdateInfoPeople(int.Parse(dtInfoPeople.Rows[0]["ID"].ToString()), status);
                        setLog(DTO.eventEnum.تایید_کارگزینی, Convert.ToInt32(dtInfoPeople.Rows[0]["ID"].ToString()), "");
                        //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 13, (int)DTO.eventEnum.تایید_کارگزینی, "تایید کارگزینی :" + dt2.Rows[0]["ID"].ToString(), Convert.ToInt32(dt2.Rows[0]["ID"].ToString()));
                        if (dtInfoPeople.Rows[0]["mobile"] != DBNull.Value)
                        {
                            DataTable dt1 = new DataTable();
                            dt1 = CB.GetAppIDMessage(0, 13, 1, 1);
                            string MobileNumber = dtInfoPeople.Rows[0]["mobile"].ToString();
                            string Message = dt1.Rows[0]["Text"].ToString();
                            SendSMSToProfessor(MobileNumber, Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        showCantContinueMessage("خطا زمان تایید درخواست همکاری توسط کارگزینی. لطفا کد زیر را به مدیر سیستم ارائه دهید: id= " + dtInfoPeople.Rows[0]["ID"].ToString() + "    " + ex.Message);
                    }
                }
                Response.Redirect("CooperationRequestProfessors.aspx");
            }
            else
            {
                showCantContinueMessage(string.Format("{0} {1} {2} {3}", "شما قادر به تایید این فرم نیستید زیرا", "مدارک", ViewState[reason_CantRegister], "وجود ندارد و یا به تایید نرسیده است"));
            }

        }

        protected void btn_TaeedParvande_Click(object sender, EventArgs e)
        {
            confirmMessage.Text = "آیا از تایید و یا نقص مدارک مطمئن هستید";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

        }

        /// <summary>
        /// ثبت وضعیت اسکن ها
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rbConfirm_OK1_Click(object sender, EventArgs e)
        {
            int InfoPeopleId = Convert.ToInt32(Request.QueryString["ID"]);
            DataTable dtResault = FRB.GetInfoPeoByFilter(InfoPeopleId);

            #region Update Documents Status
            foreach (RadListViewItem item in rlv.Items)
            {
                HiddenField Hf = (HiddenField)item.FindControl("HiddenField1");
                RadioButton rb = (RadioButton)item.FindControl("rdb_Taeed");
                RadioButton rb2 = (RadioButton)item.FindControl("rdb_Naghs");
                //DropDownList drp = (DropDownList)item.FindControl("ddlMadrak");
                TextBox txt = (TextBox)item.FindControl("txt_Sharh");
                Label lbl_Sharh = (Label)item.FindControl("lbl_Sharh");
                Label Name = (Label)item.FindControl("lbl_Name");
                //if (drp.SelectedValue != "0")
                //{
                //    DataTable dtName = FRB.GetNameScanDoc(int.Parse(drp.SelectedValue));
                //    Name.Text = dtName.Rows[0]["document_name"].ToString();
                //}
                if (rb.Checked == true)
                {
                    if (int.Parse(Hf.Value) > 0 && int.Parse(Hf.Value) <= (int)Enum.GetValues(typeof(Hire.Hire.DocType)).Cast<Hire.Hire.DocType>().Max())
                    {
                        FRB.UpdateStatus(int.Parse(Hf.Value), int.Parse(dtResault.Rows[0]["ID"].ToString()), 1, "", int.Parse(Hf.Value));
                    }

                    if (RoleId == (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی || RoleId == (int)DTO.RoleEnums.کارشناس_پژوهش)
                    {
                        setLog(DTO.eventEnum.تایید_مدرک_پژوهش, Convert.ToInt32(dtResault.Rows[0]["ID"].ToString()), "شماره مدرک: " + Hf.Value.ToString() + " نوع مدرک: " + Name.Text);
                        //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 13, (int)DTO.eventEnum.تایید_مدرک_پژوهش, ":کد کاربری" + dtResault.Rows[0]["ID"].ToString() + "-" + "شماره مدرک:" + Hf.Value.ToString());
                    }
                    else if (RoleId == (int)DTO.RoleEnums.مسئول_حق_التدریس || RoleId == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
                    {
                        setLog(DTO.eventEnum.تایید_مدرک_کارگزینی, Convert.ToInt32(dtResault.Rows[0]["ID"].ToString()), "شماره مدرک: " + Hf.Value.ToString() + " نوع مدرک: " + Name.Text);
                        //CB.InsertIntoUserLog(RoleId, DateTime.Now.ToString("HH:mm"), 13, (int)DTO.eventEnum.تایید_مدرک_کارگزینی, ":کد کاربری" + dtResault.Rows[0]["ID"].ToString() + "-" + "شماره مدرک:" + Hf.Value.ToString());
                    }
                    else if (RoleId == (int)DTO.RoleEnums.مدیر_ارشد)
                    {
                        setLog(DTO.eventEnum.تایید_مدرک_کارگزینی, Convert.ToInt32(dtResault.Rows[0]["ID"].ToString()), "شماره مدرک: " + Hf.Value.ToString() + " نوع مدرک: " + Name.Text);
                        //CB.InsertIntoUserLog(RoleId, DateTime.Now.ToString("HH:mm"), 13, (int)DTO.eventEnum.تایید_مدرک_کارگزینی, ":کد کاربری" + dtResault.Rows[0]["ID"].ToString() + "-" + "شماره مدرک:" + Hf.Value.ToString());
                    }
                }
                else if (rb2.Checked == true)
                {
                    if (int.Parse(Hf.Value) > 0 && int.Parse(Hf.Value) <= (int)Enum.GetValues(typeof(Hire.Hire.DocType)).Cast<Hire.Hire.DocType>().Max())
                    {
                        FRB.UpdateStatus(int.Parse(Hf.Value), int.Parse(dtResault.Rows[0]["ID"].ToString()), 2, txt.Text, int.Parse(Hf.Value));
                        txt.Visible = true;
                    }

                    if (RoleId == (int)DTO.RoleEnums.کارشناس_پژوهش || RoleId == (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی)
                    {
                        setLog(DTO.eventEnum.نقص_مدرک_پژوهش, Convert.ToInt32(dtResault.Rows[0]["ID"].ToString()), "شماره مدرک: " + Hf.Value.ToString() + " نوع مدرک: " + Name.Text + (txt.Text.Trim() == "" ? "" : " علت رد: " + txt.Text));
                        //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 13, (int)DTO.eventEnum.نقص_مدرک_پژوهش, ":کد کاربری" + dtResault.Rows[0]["ID"].ToString() + "-" + "شماره مدرک:" + Hf.Value.ToString());
                    }
                    else if (RoleId == (int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مسئول_حق_التدریس || RoleId == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
                    {
                        setLog(DTO.eventEnum.نقص_مدرک_کارگزینی, Convert.ToInt32(dtResault.Rows[0]["ID"].ToString()), "شماره مدرک: " + Hf.Value.ToString() + " نوع مدرک: " + Name.Text + (txt.Text.Trim() == "" ? "" : " علت رد: " + txt.Text));
                        //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 13, (int)DTO.eventEnum.نقص_مدرک_کارگزینی, ":کد کاربری" + dtResault.Rows[0]["ID"].ToString() + "-" + "شماره مدرک:" + Hf.Value.ToString());
                    }
                    else if (RoleId == (int)DTO.RoleEnums.مدیر_ارشد)
                    {
                        setLog(DTO.eventEnum.نقص_مدرک_کارگزینی, Convert.ToInt32(dtResault.Rows[0]["ID"].ToString()), "شماره مدرک: " + Hf.Value.ToString() + " نوع مدرک: " + Name.Text + (txt.Text.Trim() == "" ? "" : " علت رد: " + txt.Text));
                        //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 13, (int)DTO.eventEnum.نقص_مدرک_کارگزینی, ":کد کاربری" + dtResault.Rows[0]["ID"].ToString() + "-" + "شماره مدرک:" + Hf.Value.ToString());
                    }
                }
            }
            #endregion

            DataTable dtScan = FRB.GetRequestScanDocID(int.Parse(Request.QueryString["ID"].ToString()));

            ViewState.Add(PrimaryDocs, 0);
            if (RoleId == (int)DTO.RoleEnums.مدیر_ارشد || RoleId == (int)DTO.RoleEnums.مسئول_حق_التدریس || RoleId == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
            {
                checkPrimaryScan(dtScan, dtResault, dtResault.Rows[0]["martabeh"].ToString() != "8");
            }
            else
            {
                throw new NotImplementedException();
            }


            lbl_Taeed2.Text = string.Empty;

            if (!checkAllScans(dtScan))
            {
                FRB.UpdateInfoPeople(InfoPeopleId, (int)DTO.StatusEnum.ویرایش);
                DataTable dt1 = new DataTable();
                dt1 = CB.GetAppIDMessage(0, 13, 1, (int)DTO.StatusEnum.ویرایش);
                string MobileNumber = dtResault.Rows[0]["mobile"].ToString();
                string Message = dt1.Rows[0]["Text"].ToString();
                SendSMSToProfessor(MobileNumber, Message);
            }

            setLog(DTO.eventEnum.تایید_مدرک_کارگزینی, Convert.ToInt32(dtResault.Rows[0]["ID"].ToString()), "");
            //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 13, (int)DTO.eventEnum.تایید_مدرک_کارگزینی, "بررسی مدارک استاد با کد" + InfoPeopleId + "در HR ", InfoPeopleId);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal();", true);
        }

        /// <summary>
        /// ثبت تغییرات در اطلاعات
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ChangeInfo_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string res = areAllInformationValid();

                #region set InfoPeople fields
                setInfoPeopleField();
                #endregion
                if (ip.Cooperation == 0)
                {
                    showCantContinueMessage("لطفا حداقل یکی از موارد همکاری(آموزش ، پژوهش) را انتخاب فرمایید.");
                    return;
                }
                if (chbkDaneshkade.SelectedValue == "")
                {
                    showCantContinueMessage("لطفا حداقل یک دانشکده جهت همکاری را انتخاب فرمایید.");
                    return;
                }


                List<string> lsgroups =
                chbkGroup.Items.Cast<ListItem>()
                    .Where(li => li.Selected)
                    .Select(li => li.Value)
                    .ToList();
                if (lsgroups.Count == 0)
                {
                    showCantContinueMessage("لطفا حداقل یک گروه آموزشی جهت همکاری را انتخاب فرمایید.");
                    return;

                }

                if (rdblBimehStatus.SelectedValue == "1" && drpBimehType.SelectedValue == "0")
                {
                    showCantContinueMessage("لطفا نوع بیمه را انتخاب فرمایید.");
                    return;
                }

                FRB.SetProfessorGroups(ip.ID, lsgroups);
                FRB.UpdateInfoPeople(ip);
                rwd.RadAlert("اطلاعات با موفقیت ویرایش گردید", 0, 100, "پیام", "");

                if (RoleId == (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی || RoleId == (int)DTO.RoleEnums.کارشناس_پژوهش)
                {
                    setLog(DTO.eventEnum.ویرایش_اطلاعات_پژوهش, Convert.ToInt32(Request.QueryString["id"]), "ویرایش زمان بررسی درخواست همکاری");
                }
                else if (RoleId == (int)DTO.RoleEnums.مسئول_حق_التدریس || RoleId == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
                {
                    setLog(DTO.eventEnum.ویرایش_کارگزینی, Convert.ToInt32(Request.QueryString["id"]), "ویرایش زمان بررسی درخواست همکاری");
                }
                else if (RoleId == (int)DTO.RoleEnums.مدیر_ارشد)
                {
                    setLog(DTO.eventEnum.ویرایش_کارگزینی, Convert.ToInt32(Request.QueryString["id"]), "ویرایش زمان بررسی درخواست همکاری");
                }

                DataTable dtScan = FRB.GetRequestScanDocID(int.Parse(Request.QueryString["ID"].ToString()));
                DataTable dtResault = FRB.GetInfoPeoByFilter(int.Parse(Request.QueryString["ID"].ToString()));

                flag = 0;
                ViewState.Add(PrimaryDocs, flag);
                if (RoleId == (int)DTO.RoleEnums.مسئول_حق_التدریس || RoleId == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مدیر_ارشد || RoleId == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
                {
                    checkPrimaryScan(dtScan, dtResault, dtResault.Rows[0]["martabeh"].ToString() != "8");

                }
                #region فقط کاربران کارگزینی و مدیر ارشد حق تغییر دارند
                else
                {
                    #region اگر کاربر کارگزینی یا مدیر ارشد نبود
                    if (ip.Madrak == 7)
                    {
                        if (lbl_HeiatElmi.Text != "8")
                        {
                            for (int i = 0; i < dtScan.Rows.Count; i++)
                            {
                                if (dtScan.Rows[i]["doc_type"].ToString() == "9" && dtScan.Rows[i]["status"].ToString() == "1")
                                {
                                    flag = 1;
                                    ViewState.Add(PrimaryDocs, flag);
                                }
                            }
                        }
                        else
                        {
                            if (ip.Country == 27 || ip.Country == 0)
                            {
                                for (int i = 0; i < dtScan.Rows.Count; i++)
                                {
                                    if (dtScan.Rows[i]["doc_type"].ToString() == "4" && dtScan.Rows[i]["status"].ToString() == "1")
                                    {
                                        flag = 1;
                                        ViewState.Add(PrimaryDocs, flag);
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < dtScan.Rows.Count; i++)
                                {
                                    if (dtScan.Rows[i]["doc_type"].ToString() == "4" && dtScan.Rows[i]["status"].ToString() == "1")
                                    {
                                        for (int j = 0; j < dtScan.Rows.Count; j++)
                                        {
                                            if (dtScan.Rows[j]["doc_type"].ToString() == "14" && dtScan.Rows[j]["status"].ToString() == "1")
                                            {
                                                flag = 1;
                                                ViewState.Add(PrimaryDocs, flag);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (ip.Madrak == 13)
                    {
                        for (int i = 0; i < dtScan.Rows.Count; i++)
                        {
                            if (dtScan.Rows[i]["doc_type"].ToString() == "9" && dtScan.Rows[i]["status"].ToString() == "1")
                            {
                                for (int j = 0; j < dtScan.Rows.Count; j++)
                                {
                                    if (dtScan.Rows[j]["doc_type"].ToString() == "11" && dtScan.Rows[j]["status"].ToString() == "1")
                                    {
                                        for (int k = 0; k < dtScan.Rows.Count; j++)
                                        {
                                            if (dtScan.Rows[k]["doc_type"].ToString() == "15" && dtScan.Rows[k]["status"].ToString() == "1")
                                            {
                                                flag = 1;
                                                ViewState.Add(PrimaryDocs, flag);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (ip.Madrak == 4 || ip.Madrak == 5)
                    {
                        for (int i = 0; i < dtScan.Rows.Count; i++)
                        {
                            if (dtScan.Rows[i]["doc_type"].ToString() == "9" && dtScan.Rows[i]["status"].ToString() == "1")
                            {
                                if (Convert.ToInt32(ip.Payeh) >= 9)
                                {
                                    flag = 1;
                                    ViewState.Add(PrimaryDocs, flag);
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion


                if (rbtnYes.Checked)
                {
                    if (txtEditingPeopleInfo.Text.Trim() == "")
                        txtEditingPeopleInfo.Text = "لطفا اطلاعات خود را ویرایش نمایید";
                    FRB.UpdateInfoPeople(ip.ID, (int)DTO.StatusEnum.ویرایش);
                    setLog(DTO.eventEnum.بازشدن_ویرایش_اطلاعات_برای_استاد_توسط_کارگزینی, Convert.ToInt32(Request.QueryString["id"]), "پیام به استاد:  " + txtEditingPeopleInfo.Text);
                    string smsStatusText; bool sentSMS;
                    CB.sendSMS(ip.Mobile, "استاد گرامی، اطلاعات وارده توسط جنابعالی دارای نقص می باشد، جهت تکمیل و کسب اطلاعات بیشتر به سامانه ثبت اطلاعات اساتید مراجعه فرمایید.", out sentSMS, out smsStatusText);
                }

            }
        }

        /// <summary>
        /// رد کردن درخواست
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadButton_Click(object sender, EventArgs e)
        {
            int Status = getStatus(false);
            if (Status == 0)
            {
                rwd.RadAlert("شما اجازه رد کردن این درخواست را ندارید.", 0, 100, "خطا", "");
                return;
            }
            DataTable dt1 = new DataTable();
            DataTable dt2 = FRB.GetInfoPeoByFilter(int.Parse(Request.QueryString["ID"].ToString()));
            if (RoleId == (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی || RoleId == (int)DTO.RoleEnums.کارشناس_پژوهش)
            {
                dt1 = CB.GetAppIDMessage(0, 13, 1, Status);
                setLog(DTO.eventEnum.رد_پژوهش, Convert.ToInt32(dt2.Rows[0]["id"]), "");
                //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 13, (int)DTO.eventEnum.رد_پژوهش, "رد پژوهش" + dt2.Rows[0]["ID"].ToString());
            }
            else if (RoleId == (int)DTO.RoleEnums.مدیر_ارشد || RoleId == (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مسئول_حق_التدریس || RoleId == (int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی || RoleId == (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی)
            {
                dt1 = CB.GetAppIDMessage(0, 13, 1, Status);
                setLog(DTO.eventEnum.رد_کارگزینی, Convert.ToInt32(dt2.Rows[0]["id"]), "");
                //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 13, (int)DTO.eventEnum.رد_کارگزینی, "رد کارگزینی" + dt2.Rows[0]["ID"].ToString());
            }

            FRB.UpdateInfoPeople(int.Parse(dt2.Rows[0]["ID"].ToString()), Status);
            SendSMSToProfessor(dt2.Rows[0]["mobile"].ToString(), dt1.Rows[0]["Text"].ToString());

            rwd.RadAlert("درخواست رد گردید", 0, 100, "پیام", "");
            Response.Redirect("CooperationRequestProfessors.aspx");
        }

        protected void rlv_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            try
            {
                RadListViewDataItem row = e.Item as RadListViewDataItem;
                if (DataBinder.Eval(row.DataItem, "scan_document") == DBNull.Value)
                    return;
                HiddenField HF = (HiddenField)e.Item.FindControl("HiddenField1");
                Label lbl = (Label)e.Item.FindControl("lbl_Name");
                //DropDownList ddl = (DropDownList)e.Item.FindControl("ddlMadrak");
                Panel pnlMadrakOperation = (Panel)e.Item.FindControl("pnlMadrakOperation");
                if (RoleId == (int)DTO.RoleEnums.کارشناس_پژوهش || RoleId == (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی)
                {
                    pnlMadrakOperation.Visible = false;
                }
                //ddl.DataSource = FRB.GetNameMadarek();
                //ddl.DataTextField = "document_name";
                //ddl.DataValueField = "ID";
                //ddl.DataBind();
                //ddl.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl.Items[ddl.Items.Count - 1].Selected = true;
                object var = row.DataItem.ToString();
                string myValue = DataBinder.Eval(row.DataItem, "ext").ToString();
                string result = myValue.ToLower();
                if (result.Contains("pdf"))
                {
                    Button btn = (Button)e.Item.FindControl("btn_Madrak");
                    btn.Visible = true;
                    HF.Value = DataBinder.Eval(row.DataItem, "doc_type").ToString().Trim();
                    lbl.Text = DataBinder.Eval(row.DataItem, "document_name").ToString().Trim();
                }
                else
                {
                    Button btn = (Button)e.Item.FindControl("btn_ShowPicture");
                    btn.Visible = true;
                    RadBinaryImage img = (RadBinaryImage)e.Item.FindControl("RadBinaryImage1");
                    img.Visible = true;
                    byte[] binaryData = (byte[])(DataBinder.Eval(row.DataItem, "scan_document"));
                    img.DataValue = binaryData;
                    HF.Value = DataBinder.Eval(row.DataItem, "doc_type").ToString().Trim();
                    lbl.Text = DataBinder.Eval(row.DataItem, "document_name").ToString().Trim();
                }

                if (DataBinder.Eval(row.DataItem, "s1").ToString() != "0")
                {
                    RadioButton rdbTaeed = (RadioButton)e.Item.FindControl("rdb_Taeed");
                    RadioButton rdbNaghs = (RadioButton)e.Item.FindControl("rdb_Naghs");
                    TextBox txt = (TextBox)e.Item.FindControl("txt_Sharh");
                    Label lbl_Sharh = (Label)e.Item.FindControl("lbl_Sharh");
                    txt.Text = DataBinder.Eval(row.DataItem, "Description").ToString().Trim();
                    if (DataBinder.Eval(row.DataItem, "s1").ToString() == "1")
                    {
                        rdbTaeed.Checked = true;
                        txt.Enabled = false;
                        lbl_Taeed.Text += "1";
                    }
                    else
                    {
                        rdbNaghs.Checked = true;
                        txt.Enabled = true;
                        lbl_Taeed.Text += "2";
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        protected void rlv_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] index = e.CommandArgument.ToString().Split(new char[] { ',' });
                var button = sender as Button;
                for (int i = 0; i < 19; i++)
                {
                    if (int.Parse(index[0].ToString()) == i)
                    {
                        DataTable dt4 = FRB.GetInfoPeoByFilterPDF(int.Parse(Request.QueryString["ID"].ToString()), int.Parse(index[0].ToString()));
                        Response.ContentType = "pdf";
                        Response.Clear();
                        Response.AddHeader("content-disposition", "attachment; filename=" + dt4.Rows[0]["name"].ToString() + " " + dt4.Rows[0]["family"].ToString() + " " + "." + dt4.Rows[0]["ext"].ToString());
                        Response.BinaryWrite((byte[])dt4.Rows[0]["scan_document"]);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            if (e.CommandName == "ShowPic")
            {
                string[] index = e.CommandArgument.ToString().Split(new char[] { ',' });
                var button = sender as Button;
                for (int i = 0; i < 19; i++)
                {
                    if (int.Parse(index[0].ToString()) == i)
                    {
                        Response.Redirect("ShowPicture.aspx?" + "ID" + "=" + int.Parse(Request.QueryString["ID"].ToString()) + "&" + "TypePic" + "=" + int.Parse(index[0].ToString()));
                    }
                }
            }
        }

        protected void RadioButtonPeopleEditing_SelectedIndexChanged(object sender, EventArgs e)
        {
            RbtnPeopleEditing();
        }

        protected void chbkDaneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            chbkGroup.Items.Clear();
            if (chbkDaneshkade.SelectedIndex != -1)
            {
                DataTable dtt = new DataTable();

                foreach (ListItem itemm in chbkDaneshkade.Items)
                {
                    if (itemm.Selected)
                    {
                        dtt.Merge(FRB.GetDepartmentList(Convert.ToInt32(itemm.Value)));
                    }
                    else
                    {
                        // FRB.DeleteProfDepartmentByDaneshID(int.Parse(Request.QueryString["ID"].ToString()), Convert.ToInt32(itemm.Value));
                    }
                }
                chbkGroup.DataSource = dtt;
                chbkGroup.DataTextField = "namegroup";
                chbkGroup.DataValueField = "idgroup";
                chbkGroup.RepeatColumns = 4;
                chbkGroup.RepeatDirection = RepeatDirection.Horizontal;
                chbkGroup.DataBind();
                List<string> departmanList = FRB.GetGroupList(int.Parse(Request.QueryString["ID"].ToString()));
                foreach (string var in departmanList)
                {
                    if (chbkGroup.Items.FindByValue(var) != null)
                        chbkGroup.Items.FindByValue(var).Selected = true;
                }
            }
        }

        protected void drpProvince1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpProvince1.SelectedIndex != 0)
            {
                ListItem choose = new ListItem("انتخاب کنید", "0");
                DataTable cityList = CB.getShahrestan(Convert.ToInt32(drpProvince1.SelectedValue));
                drpLivingCity.DataSource = cityList;
                drpLivingCity.DataTextField = "Title";
                drpLivingCity.DataValueField = "ID";
                drpLivingCity.DataBind();
                if (!drpLivingCity.Items.Contains(choose))
                    drpLivingCity.Items.Insert(0, choose);
            }
        }

        protected void drpProvince2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpProvince2.SelectedIndex != 0)
            {
                ListItem choose = new ListItem("انتخاب کنید", "0");
                DataTable cityList = CB.getShahrestan(Convert.ToInt32(drpProvince2.SelectedValue));
                drpWorkingCity.DataSource = cityList;
                drpWorkingCity.DataTextField = "Title";
                drpWorkingCity.DataValueField = "ID";
                drpWorkingCity.DataBind();
                drpWorkingCity.Items.Insert(0, choose);
            }
        }

        protected void rfvHomeCity_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (drpLivingCity.SelectedValue == "0")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void rfv_HomeState_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (drpProvince1.SelectedValue == "0")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void rbtnDocStatus_CheckedChange(object sender, EventArgs e)
        {
            (((sender as RadioButton).NamingContainer).FindControl("txt_Sharh") as TextBox).Enabled = (((sender as RadioButton).NamingContainer).FindControl("rdb_Naghs") as RadioButton).Checked;
            if ((((sender as RadioButton).NamingContainer).FindControl("rdb_taeed") as RadioButton).Checked)
                (((sender as RadioButton).NamingContainer).FindControl("txt_Sharh") as TextBox).Text = "";
        }

        protected void customValidatorInfo_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //string message;
            //if (checkValidationOfInformation(out message))
            //{
            //    customValidatorInfo.ErrorMessage =string.Format("{0} صحیح نمیباشد ", message);
            //    customValidatorInfo.Text = "";
            //    args.IsValid = false;
            //}
            //else
            //    args.IsValid = true;
        }

        protected void rdblBimehStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdblBimehStatus.SelectedIndex != -1)
            {
                RequiredFieldValidator14.Enabled = rdblBimehStatus.SelectedValue == "1";
                if (rdblBimehStatus.SelectedValue == "1")
                {
                    if (chk_IsRetired.Checked)
                    {
                        drpBimehType.SelectedValue = "7";
                        drpBimehType.Enabled = false;
                        valBimehType.Enabled = false;
                    }
                    else
                    {
                        drpBimehType.SelectedValue = "0";
                        drpBimehType.Enabled = true;
                        valBimehType.Enabled = true;
                    }
                    txtInsuranceNumber.Enabled = true;
                    txtInsuranceNumber.Text = lbl_BimeNumber.Text;
                }
                else
                {
                    lbl_BimeNumber.Text = txtInsuranceNumber.Text.Trim();
                    drpBimehType.Enabled = false;
                    valBimehType.Enabled = false;
                    txtInsuranceNumber.Enabled = false;
                    drpBimehType.ClearSelection();
                    //drpBimehType.Items.Insert(0, choose);
                    txtInsuranceNumber.Text = string.Empty;
                }
            }
        }

        protected void rdblGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            //1:مرد
            //2:زن

            drpNezam.Enabled = rdblGender.SelectedValue == "1";

            if (rdblGender.SelectedValue == "2")
                drpNezam.SelectedValue = ((int)Hire.Hire.militaryStatus.غير_مشمول).ToString();
        }

        protected void chk_IsRetired_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_IsRetired.Checked)
            {
                rdblBimehStatus.SelectedValue = "1";
                drpBimehType.SelectedValue = "7";
                drpBimehType.Enabled = false;
                txtInsuranceNumber.Enabled = true;
                rdblBimehStatus.Enabled = false;
                txtInsuranceNumber.Text = lbl_BimeNumber.Text;
            }
            else
            {
                drpBimehType.Enabled = rdblBimehStatus.SelectedValue == "1";
                txtInsuranceNumber.Enabled = rdblBimehStatus.SelectedValue == "1";
                rdblBimehStatus.Enabled = true;
            }
        }

        protected void rfv_WorkState_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //if ((drpWorkingCity.SelectedValue != "0" || drpWorkingCity.SelectedValue != "") || drpProvince2.SelectedValue == "0")
            //{
            //    args.IsValid = false;
            //}
            //else
            //{
            args.IsValid = true;
            //}
        }

        protected void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            RbtnPeopleEditing();
        }

        protected void rfv_WorkCity_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (drpProvince2.SelectedValue != "0" && (drpWorkingCity.SelectedValue == "0" || drpWorkingCity.SelectedValue == ""))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

    }
    static class extentions
    {
        public static string mergeMessage(this string LastMessage, string AddedMessage)
        {
            if (LastMessage == "")
                return AddedMessage;
            else
                return string.Format("{0} ، {1}", LastMessage, AddedMessage);
        }
        public static bool isCorrectStringInputs(this string text)
        {
            text = text.Trim();
            if (text.Length < 3)
                return false;
            if (text.Any(char.IsDigit))
                return false;
            return true;
        }

        public static bool isValidMobile(this string text)
        {
            text = text.Trim();
            if (text.Length == 0)
                return false;
            if (!text.isInputDigit())
                return false;
            if (!text.StartsWith("09"))
                return false;
            if (text.Length != 11)
                return false;
            return true;
        }

        public static bool isValidPhone(this string text)
        {
            text = text.Trim();
            System.Text.RegularExpressions.Regex rr = new System.Text.RegularExpressions.Regex(@"^([0-9]{11})$");
            bool b = rr.IsMatch(text);
            if (text.Length == 0)
                return true;
            if (!text.isInputDigit())
                return false;
            if (!text.StartsWith("0"))
                return false;
            if (text.Length != 11)
                return false;
            return true;
        }

        public static bool isZipCodeValid(this string text)
        {
            text = text.Trim();
            if (text.Length == 0)
                return false;
            if (text.Length != 10)
                return false;
            return true;
        }

        public static bool isInputDigit(this string text)
        {
            System.Text.RegularExpressions.Regex allDigit = new System.Text.RegularExpressions.Regex(@"\d+");
            return allDigit.IsMatch(text);
        }
    }
}
