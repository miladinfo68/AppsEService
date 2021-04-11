using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using System.Data;
using IAUEC_Apps.DTO.University.Request;
using System.IO;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class editTeachersInfos : System.Web.UI.Page
    {
        const string ScanUrl = "ScanMadarekURL";
        const string scanShenasname = "scanSh";//DocType=1    controlId(49,51)
        const string scanMadrakTahsili = "scanMdk";//DocType=4    controlId(7,9)
        const string scanEmtehanJame = "scanJame";//DocType=11    controlId(8)
        const string scanNezam = "scanNzm";//DocType=7    controlId(30)
        const string scanBime = "scanBme";//DocType =6   controlId(43)
        const string scanBazneshaste = "scanBzn";//DocType=18    controlId(44)
        const string scanArzeshname = "scanArz";//DocType=14    controlId(15)
        const string scanPersonelly = "scanprsnl";//DocType=10    controlId(1000)
        const string scanMeli = "scanMelli";//DocType=5    controlId(500)
        const string scanHokm = "Hokm";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initialComponents();
            }
        }

        private void setGridPersonalImageSource()
        {
            Business.university.CooperationRequest.CooperationRequestBusiness dr = new Business.university.CooperationRequest.CooperationRequestBusiness();
            grdPersonalImage.DataSource = dr.getTeachersHaveNotPersonalImage();
            grdPersonalImage.DataBind();
            grdCantUploadPErsonnelyImage.DataSource = dr.getTeachersHaveNotPersonalImage_CantUpload();
            grdCantUploadPErsonnelyImage.DataBind();
            pnlPersonalImage.Visible = true;

        }

        private void initialComponents()
        {
            ListItem itmSelect = new ListItem("انتخاب کنید", "-1");
            ListItem itmOther = new ListItem("سایر", "0");
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();


            drpNezam.DataSource = CB.GetStatusMilitary_fcoding();
            drpNezam.DataTextField = "namecoding";
            drpNezam.DataValueField = "id";
            drpNezam.DataBind();
            drpNezam.Items.Insert(0, new ListItem(itmSelect.Text, itmSelect.Value));
            drpNezam.Items.Insert(drpNezam.Items.Count, new ListItem(itmOther.Text, itmOther.Value));


            drpLastMaghta.DataSource = CB.GetCodingByTypeId(2);
            drpLastMaghta.DataValueField = "Id";
            drpLastMaghta.DataTextField = "namecoding";
            drpLastMaghta.DataBind();
            drpLastMaghta.Items.Insert(0, new ListItem(itmSelect.Text, itmOther.Value));

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

            DataTable dtCountrySource = CB.GetNameCountry_fcoding();
            drpCountry.DataSource = dtCountrySource.Select("id<56").CopyToDataTable();
            drpCountry.DataTextField = "namecoding";
            drpCountry.DataValueField = "id";
            drpCountry.DataBind();
            drpCountry.Items.Insert(0, new ListItem(itmSelect.Text, itmSelect.Value));

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


            drpPastUni.DataSource = dtUniName;
            drpPastUni.DataTextField = "namecoding";
            drpPastUni.DataValueField = "ID";
            drpPastUni.DataBind();
            drpPastUni.Items.Insert(0, new RadComboBoxItem(itmSelect.Text, itmSelect.Value));
            drpPastUni.Items.Insert(drpPastUni.Items.Count, new RadComboBoxItem(itmOther.Text, itmOther.Value));


            drpProvince_Home.DataSource = CB.GetOstan();
            drpProvince_Home.DataTextField = "Title";
            drpProvince_Home.DataValueField = "ID";
            drpProvince_Home.DataBind();
            drpProvince_Home.Items.Insert(0, new ListItem(itmSelect.Text, itmSelect.Value));
            drpProvince_Work.DataSource = CB.GetOstan();
            drpProvince_Work.DataTextField = "Title";
            drpProvince_Work.DataValueField = "ID";
            drpProvince_Work.DataBind();
            drpProvince_Work.Items.Insert(0, new ListItem(itmSelect.Text, itmSelect.Value));



        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            pnlPersonalImage.Visible = ddlEditType.SelectedItem.Value == "1";
            pnlEditPersonalInfo.Visible = ddlEditType.SelectedItem.Value == "2";
            pnlEditContactInfo.Visible = ddlEditType.SelectedItem.Value == "3";
            pnlEditEmployInfo.Visible = ddlEditType.SelectedItem.Value == "4";
            switch (ddlEditType.SelectedItem.Value)
            {
                case "1":
                    setGridPersonalImageSource();
                    PersianFiltering(grdPersonalImage);
                    PersianFiltering(grdCantUploadPErsonnelyImage);
                    break;
            }

        }
        protected void PersianFiltering(Telerik.Web.UI.RadGrid radGrid)
        {
            Telerik.Web.UI.GridFilterMenu menu = radGrid.FilterMenu;
            if (menu.Items.Count > 5)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" ||
                        menu.Items[im].Text == "IsEmpty" || menu.Items[im].Text == "NotIsEmpty" ||
                        menu.Items[im].Text == "EqualTo")
                        im++;
                    else
                        menu.Items.RemoveAt(im);
                }
                foreach (Telerik.Web.UI.RadMenuItem item in menu.Items)
                {
                    switch (item.Text)
                    {
                        case "NoFilter":
                            item.Text = "حذف فیلتر";

                            break;
                        case "Contains":
                            item.Text = "شامل";

                            break;
                        case "EqualTo":
                            item.Text = "مساوی با";

                            break;
                        case "IsEmpty":
                            item.Text = "خالی";

                            break;
                        case "NotIsEmpty":
                            item.Text = "غیر خالی";

                            break;
                    }
                }
            }
        }

        protected void btnSearchTeacher_HaveNotPersonalImage_Click(object sender, EventArgs e)
        {
            lblTeacher_PersonalImage.Text = "";
            lblTeacher_PersonalImage.ToolTip = "";
            Business.university.CooperationRequest.CooperationRequestBusiness dr = new Business.university.CooperationRequest.CooperationRequestBusiness();
            System.Data.DataTable dt = dr.getTeachersHaveNotPersonalImage();
            System.Data.DataRow[] drow = dt.Select("iddMeli='" + txtIDDMeli.Text.Trim() + "'");
            if (drow.Length == 1)
            {
                lblTeacher_PersonalImage.Text = drow[0]["name"].ToString() + " " + drow[0]["family"].ToString();
                lblTeacher_PersonalImage.ToolTip = drow[0]["hrID"].ToString();
                dvRadUpload.Visible = true;
            }
            else
            {
                dvRadUpload.Visible = false;
                lblTeacher_PersonalImage.Text = "لطفا فقط از کد ملی های موجود در جدول 'اساتید با قابلیت درج عکس' استفاده فرمایید";
                lblTeacher_PersonalImage.ToolTip = "";
            }
        }

        protected void btnUploadPersonalImage_Click(object sender, EventArgs e)
        {
            if (lblTeacher_PersonalImage.ToolTip != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalConfirm();", true);

        }

        private void uploadImage()
        {
            if (radUploadPersonalImage.UploadedFiles.Count == 1 && lblTeacher_PersonalImage.ToolTip != "")
            {
                int hrID = Convert.ToInt32(lblTeacher_PersonalImage.ToolTip);
                var img = radUploadPersonalImage.UploadedFiles[0];
                string subPath = "~/University/CooperationRequest/Teachers/ScanMadarek/editTeachersInfos/personnelyImage";
                string savePath = string.Format("{0}/{1}", subPath, hrID.ToString() + "_" + DateTime.Now.ToPeString("yyyyMMdd_HHmm") + img.GetExtension());
                Business.university.Request.ProfessorRequestBusiness prb = new Business.university.Request.ProfessorRequestBusiness();
                if (!System.IO.Directory.Exists(Server.MapPath(subPath)))
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                img.SaveAs(Server.MapPath(savePath));
                var image = System.IO.File.ReadAllBytes(Server.MapPath(savePath));
                if (prb.InsertDocumentToHr(hrID, image, 10, 1, img.GetExtension()))
                {
                    setLog(DTO.eventEnum.ویرایش_کارگزینی, "آپلود عکس پرسنلی استاد", hrID);
                    showMessage("آپلود عکس به درستی انجام شد");
                }
                else
                {
                    showMessage("آپلود عکس انجام نشد. لطفا مجددا تلاش فرمایید");
                }
            }
            else
            {
                showMessage("لطفا عکس برای استاد انتخاب فرمایید.");

            }
            setGridPersonalImageSource();
        }

        private void setLog(DTO.eventEnum eventType, string description, int modifyID)
        {
            int userId;//کاربری که لاگین کرده
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            string Description = description;//توضیحات اختیاری

            userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            appId = 13;
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)eventType, Description, modifyID);
        }

        protected void grdPersonalImage_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            Business.university.CooperationRequest.CooperationRequestBusiness dr = new Business.university.CooperationRequest.CooperationRequestBusiness();
            grdPersonalImage.DataSource = dr.getTeachersHaveNotPersonalImage();
            PersianFiltering(grdPersonalImage);

        }

        protected void grdCantUploadPErsonnelyImage_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            Business.university.CooperationRequest.CooperationRequestBusiness dr = new Business.university.CooperationRequest.CooperationRequestBusiness();
            grdCantUploadPErsonnelyImage.DataSource = dr.getTeachersHaveNotPersonalImage_CantUpload();
            PersianFiltering(grdCantUploadPErsonnelyImage);


        }

        protected void rbConfirmeUploadImage_Click(object sender, EventArgs e)
        {
            uploadImage();
        }

        private void showMessage(string msg)
        {
            ltrMsg.Text = msg;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalMsg();", true);

        }

        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            switch (((ImageButton)sender).CommandArgument)
            {
                case "grdPersonalImage":
                    exportToExcel(grdPersonalImage, "اساتید_فاقد_عکس_پرسنلی");
                    break;
                case "grdCantUploadPErsonnelyImage":
                    exportToExcel(grdCantUploadPErsonnelyImage, "فاقد_عکس_پرسنلی_بدون_امکان_آپلود");
                    break;
            }
        }

        private void exportToExcel(Telerik.Web.UI.RadGrid grid, string excelName = "exportExcel")
        {
            grid.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grid.ExportSettings.IgnorePaging = true;
            grid.ExportSettings.ExportOnlyData = true;
            grid.ExportSettings.OpenInNewWindow = true;
            grid.ExportSettings.UseItemStyles = true;
            grid.ExportSettings.FileName = excelName;
            grid.MasterTableView.ExportToExcel();
        }
        protected void grid_ExcelMLWorkBookCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
        {
            int r = 0;
            foreach (RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
            {


                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (r != 0)
                    {
                        if (r % 2 == 0)
                            row.Cells[i].StyleValue = "Style1";
                        else
                            row.Cells[i].StyleValue = "Style2";
                    }
                    else
                        row.Cells[i].StyleValue = "styleHeader";
                }
                r++;

            }
            StyleElement styleHeader = new StyleElement("styleHeader");
            styleHeader.InteriorStyle.Pattern = InteriorPatternType.Solid;
            styleHeader.InteriorStyle.Color = System.Drawing.Color.White;
            styleHeader.FontStyle.FontName = "Tahoma";
            styleHeader.FontStyle.Bold = true;
            styleHeader.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(styleHeader);
            StyleElement style = new StyleElement("Style1");
            style.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style.InteriorStyle.Color = System.Drawing.Color.FromArgb(162, 226, 255);
            style.FontStyle.FontName = "Tahoma";
            style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(style);
            StyleElement style2 = new StyleElement("Style2");
            style2.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            style2.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style2.InteriorStyle.Color = System.Drawing.Color.FromArgb(217, 243, 255);
            style2.FontStyle.FontName = "Tahoma";
            e.WorkBook.Styles.Add(style2);
        }

        protected void rblGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvNezam.Visible = rblGender.SelectedItem.Value == "1";
            pnlMilitary.Visible = rblGender.SelectedItem.Value == "1";
        }

        protected void chbkIsRetired_CheckedChanged(object sender, EventArgs e)
        {
            dvBazneshaste.Visible = chbkIsRetired.Checked;
        }

        protected void rdblBimehStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvBime.Visible = rdblBimehStatus.SelectedItem.Value == "1";
            drpBimehType.Enabled = rdblBimehStatus.SelectedItem.Value == "1";
            txtInsuranceNumber.Enabled = rdblBimehStatus.SelectedItem.Value == "1";
        }

        private ImageStructure getScan(int controlToFieldId, string professorCode)
        {

            string imageUrl;
            ImageStructure imgStr = new ImageStructure();
            switch (controlToFieldId)
            {

                case 1000:
                    if (ruScanPersonelly.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile objP = ruScanPersonelly.UploadedFiles[0];

                    imageUrl = saveAsScan(scanPersonelly, professorCode, ref objP);
                    ruScanPersonelly.UploadedFiles[0].InputStream.Close();
                    objP.SaveAs(Server.MapPath(imageUrl));
                    break;
                case 500:
                    if (ruScanMelli.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile objM = ruScanMelli.UploadedFiles[0];

                    imageUrl = saveAsScan(scanMeli, professorCode, ref objM);
                    ruScanMelli.UploadedFiles[0].InputStream.Close();
                    objM.SaveAs(Server.MapPath(imageUrl));
                    break;
                case 49:
                case 50:
                case 51:
                    if (ruScanShenasname.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj1 = ruScanShenasname.UploadedFiles[0];

                    imageUrl = saveAsScan(scanShenasname, professorCode, ref obj1);
                    ruScanShenasname.UploadedFiles[0].InputStream.Close();
                    obj1.SaveAs(Server.MapPath(imageUrl));
                    break;
                case 7:
                case 9:
                    if (ruScanMadrak.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj2 = ruScanMadrak.UploadedFiles[0];
                    imageUrl = saveAsScan(scanMadrakTahsili, professorCode, ref obj2);
                    ruScanMadrak.UploadedFiles[0].InputStream.Close();
                    obj2.SaveAs(Server.MapPath(imageUrl), true);

                    break;
                case 8:
                    if (ruScanJame.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile objJame = ruScanJame.UploadedFiles[0];
                    imageUrl = saveAsScan(scanEmtehanJame, professorCode, ref objJame);
                    ruScanJame.UploadedFiles[0].InputStream.Close();
                    objJame.SaveAs(Server.MapPath(imageUrl), true);
                    break;
                case 15:
                    if (ruScanArzeshname.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj3 = ruScanArzeshname.UploadedFiles[0];
                    imageUrl = saveAsScan(scanArzeshname, professorCode, ref obj3);
                    ruScanArzeshname.UploadedFiles[0].InputStream.Close();
                    obj3.SaveAs(Server.MapPath(imageUrl), true);
                    break;
                case 30:
                    if (ruScanNezam.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj4 = ruScanNezam.UploadedFiles[0];
                    imageUrl = saveAsScan(scanNezam, professorCode, ref obj4);
                    ruScanNezam.UploadedFiles[0].InputStream.Close();
                    obj4.SaveAs(Server.MapPath(imageUrl), true);
                    break;
                case 40:
                case 43:
                    if (ruScanBime.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj5 = ruScanBime.UploadedFiles[0];
                    imageUrl = saveAsScan(scanBime, professorCode, ref obj5);
                    ruScanBime.UploadedFiles[0].InputStream.Close();
                    obj5.SaveAs(Server.MapPath(imageUrl), true);
                    break;
                case 44:
                    if (ruScanBazneshaste.UploadedFiles.Count == 0)
                        return new ImageStructure();
                    UploadedFile obj6 = ruScanBazneshaste.UploadedFiles[0];
                    imageUrl = saveAsScan(scanBazneshaste, professorCode, ref obj6);
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
        private string saveAsScan(string scanName, string professorCode, ref UploadedFile obj)
        {
            try
            {
                string Path = getPathOfScan(scanName, professorCode);
                string urlPath = Path + obj.GetName().improveFileName();
                System.IO.Directory.CreateDirectory(Server.MapPath(Path));
                return urlPath;
            }
            catch (Exception ex)
            { return ""; }
        }
        private string getPathOfScan(string scanName, string professorCode)
        {
            string ScanMadarekURL, subPath = "~/University/CooperationRequest/Teachers/ScanMadarek/" + professorCode;//subPath = Server.MapPath("~/University/CooperationRequest/Teachers/ScanMadarek/") + Session["user"].ToString();

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

                case 8:
                    return (int)Hire.Hire.DocType.گواهی_امتحان_جامع;
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


        private DTO.University.Request.ChangedInfoDTO AddValueToChangeList(DataTable dtControlToSidaList, string newValue, string oldValue, string controlId, string professorcode = "")
        {
            professorcode = professorcode == "" ? txtSearchProfessorCode_PersonalInfo.Text : professorcode;
            int fieldId = dtControlToSidaList.AsEnumerable()
                                              .Where(x => x.Field<string>("ControlName") == controlId)
                                              .Select(i => i.Field<int>("Id"))
                                              .First();
            DTO.University.Request.ChangedInfoDTO a =
            new DTO.University.Request.ChangedInfoDTO(Convert.ToInt32(professorcode))
            {
                ProfessorRequestId = 0,
                ControlToFieldId = fieldId,
                ControlId = controlId,
                OldValue = oldValue,
                NewValue = newValue,
                Code_Ostad = Convert.ToInt32(professorcode)
            };
            return a;
        }


        protected void rblIsHeiat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void vldEmployActionScan_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }
        protected void btnSubmitChanges_PersonalInfo_Click(object sender, EventArgs e)
        {
            if (txtProfessorCode_PersonalInfo.Text != "")
            {
                DTO.University.Request.ProfessorEditRequestDTO oEditDTO = new DTO.University.Request.ProfessorEditRequestDTO();
                oEditDTO.ChangeList = new List<DTO.University.Request.ChangedInfoDTO>();

                Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();
                DTO.University.Faculty.editInfoStruct editInfo = FRB.getOstadInf(Convert.ToInt32(txtProfessorCode_PersonalInfo.Text));
                DataTable dtControlToSidaList = FRB.GetAllControlToSidaFields();

                if (editInfo.codeOstad > 0)
                {
                    if (txtFirstName.Text != editInfo.name)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtFirstName.Text, editInfo.name, txtFirstName.ID));
                    if (txtFamily.Text != editInfo.family)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtFamily.Text, editInfo.family, txtFamily.ID));
                    if (txtFatherName.Text != editInfo.fatherName)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtFatherName.Text, editInfo.fatherName, txtFatherName.ID));
                    if (txtShCode.Text != editInfo.idd)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtShCode.Text, editInfo.idd, txtShCode.ID));
                    if (txtYearBorn.Text != editInfo.salTavalod)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtYearBorn.Text, editInfo.salTavalod, txtYearBorn.ID));
                    if (drpNezam.SelectedValue != editInfo.nezam.ToString() && rblGender.SelectedValue != "2" && editInfo.nezam != 6)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpNezam.SelectedValue, editInfo.nezam.ToString(), drpNezam.ID + "Value"));
                    if (rdblMarriage.SelectedValue != (editInfo.taahol ? "2" : "1"))
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, rdblMarriage.SelectedValue, (editInfo.taahol ? "2" : "1"), rdblMarriage.ID));
                    if (rblGender.SelectedValue != (editInfo.sexIsMan ? "1" : "2"))
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, rdblMarriage.SelectedValue, (editInfo.sexIsMan ? "1" : "2"), rdblMarriage.ID));
                    if (drpLastMaghta.SelectedItem.Value != editInfo.maghta.ToString())
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpLastMaghta.SelectedItem.Value, editInfo.maghta == 0 ? "" : editInfo.maghta.ToString(), drpLastMaghta.ID + "Value"));
                    if (drpReshte.SelectedItem.Value != editInfo.reshte.ToString())
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpReshte.SelectedItem.Value, editInfo.reshte == 0 ? "" : editInfo.reshte.ToString(), drpReshte.ID + "Value"));
                    if (drpUniversityType.SelectedValue != editInfo.typeUniMadrak.ToString())
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpUniversityType.SelectedValue, editInfo.typeUniMadrak.ToString(), drpUniversityType.ID + "Value"));
                    if (txtSiba.Text != editInfo.siba)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtSiba.Text, editInfo.siba, txtSiba.ID));
                    if (txtYearGetMadrak.Text != editInfo.salMadrak)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtYearGetMadrak.Text, editInfo.salMadrak, txtYearGetMadrak.ID));
                    if (txtSanavat.Text != editInfo.sanavat)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtSanavat.Text, editInfo.sanavat, txtSanavat.ID));
                    if (drpCountry.SelectedItem.Value != editInfo.keshvar.ToString())
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpCountry.SelectedItem.Value, editInfo.keshvar.ToString(), drpCountry.ID + "Value"));
                    if (drpUniName.SelectedValue != editInfo.nameUniMadrak.ToString())
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpUniName.SelectedValue, editInfo.nameUniMadrak.ToString(), drpUniName.ID + "Value"));
                    if (drpBimehType.SelectedValue != editInfo.bimeType.ToString())
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpBimehType.SelectedValue, editInfo.bimeType.ToString(), drpBimehType.ID + "Value"));
                    if (txtInsuranceNumber.Text != editInfo.bimeNum)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtInsuranceNumber.Text, editInfo.bimeNum, txtInsuranceNumber.ID));
                    if (chbkIsRetired.Checked != editInfo.bazneshaste)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, chbkIsRetired.Checked.ToString(), editInfo.bazneshaste.ToString(), "chbkIsRetired"));
                    if (txtDescription.Text.Trim() != editInfo.description.Trim())
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtDescription.Text.Trim(), editInfo.description.Trim(), "txtDescription"));

                    oEditDTO.ScanList = new Dictionary<int, DTO.University.Request.ImageStructure>();

                    if (ruScanArzeshname.UploadedFiles.Count > 0)
                    {
                        DTO.University.Request.ImageStructure img = getScan(15, txtProfessorCode_PersonalInfo.Text);
                        if (!string.IsNullOrEmpty(img.imageUrl))
                            oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(15), img);
                    }

                    if (ruScanBazneshaste.UploadedFiles.Count > 0)
                    {
                        DTO.University.Request.ImageStructure img = getScan(44, txtProfessorCode_PersonalInfo.Text);
                        if (!string.IsNullOrEmpty(img.imageUrl))
                            oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(4), img);
                    }

                    if (ruScanBime.UploadedFiles.Count > 0)
                    {
                        DTO.University.Request.ImageStructure img = getScan(40, txtProfessorCode_PersonalInfo.Text);
                        if (!string.IsNullOrEmpty(img.imageUrl))
                            oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(40), img);
                    }
                    if (ruScanMadrak.UploadedFiles.Count > 0)
                    {
                        DTO.University.Request.ImageStructure img = getScan(7, txtProfessorCode_PersonalInfo.Text);
                        if (!string.IsNullOrEmpty(img.imageUrl))
                            oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(7), img);
                    }
                    if (ruScanMelli.UploadedFiles.Count > 0)
                    {
                        DTO.University.Request.ImageStructure img = getScan(500, txtProfessorCode_PersonalInfo.Text);
                        if (!string.IsNullOrEmpty(img.imageUrl))
                            oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(500), img);
                    }
                    if (ruScanNezam.UploadedFiles.Count > 0)
                    {
                        DTO.University.Request.ImageStructure img = getScan(30, txtProfessorCode_PersonalInfo.Text);
                        if (!string.IsNullOrEmpty(img.imageUrl))
                            oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(30), img);
                    }
                    if (ruScanShenasname.UploadedFiles.Count > 0)
                    {
                        DTO.University.Request.ImageStructure img = getScan(49, txtProfessorCode_PersonalInfo.Text);
                        if (!string.IsNullOrEmpty(img.imageUrl))
                            oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(49), img);
                    }
                    if (ruScanPersonelly.UploadedFiles.Count > 0)
                    {
                        DTO.University.Request.ImageStructure img = getScan(1000, txtProfessorCode_PersonalInfo.Text);
                        if (!string.IsNullOrEmpty(img.imageUrl))
                            oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(1000), img);
                    }
                    if (ruScanJame.UploadedFiles.Count > 0)
                    {
                        DTO.University.Request.ImageStructure img = getScan(8, txtProfessorCode_PersonalInfo.Text);
                        if (!string.IsNullOrEmpty(img.imageUrl))
                            oEditDTO.ScanList.Add(getDocumentIdByControlToFieldId(8), img);
                    }


                    oEditDTO.Code_Ostad = Convert.ToInt32(txtProfessorCode_PersonalInfo.Text);
                    oEditDTO.Createdate = DateTime.Now.ToPeString();
                    oEditDTO.RequestTypeID = (int)RequestTypeId.EditPersonalInfo; // درخواست ویرایش مشخصات فردی
                    oEditDTO.RequestLogID = (int)RequestLogId.submitted; // در حال بررسی
                    oEditDTO.HR_InfoPeople_Id = Convert.ToInt32(editInfo.hrId);
                    oEditDTO.ChangeSet = 0;
                    Business.university.Request.ProfessorRequestBusiness ProfReqBuss = new Business.university.Request.ProfessorRequestBusiness();
                    int Id = ProfReqBuss.AddNewEditRequest(oEditDTO);
                    if (ProfReqBuss.UpdateOstadInformation_AfterApprove(Id) &&
                                                    ProfReqBuss.InsertDocumentsToHr(Id, editInfo.hrId)
                                                    && ProfReqBuss.UpdateProfessorRequestStatus(Id, (int)RequestLogId.approved) > 0)
                    {

                        setLog(DTO.eventEnum.ویرایش_کارگزینی, "", Id);
                    }

                }

            }
        }

        protected void btnSubmitChanges_ContactInfo_Click(object sender, EventArgs e)
        {
            if (txtProfessorCode_ContactInfo.Text != "")
            {
                DTO.University.Request.ProfessorEditRequestDTO oEditDTO = new DTO.University.Request.ProfessorEditRequestDTO();
                oEditDTO.ChangeList = new List<DTO.University.Request.ChangedInfoDTO>();

                Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();
                DTO.University.Faculty.editInfoStruct editInfo = FRB.getOstadInf(Convert.ToInt32(txtProfessorCode_ContactInfo.Text));
                DataTable dtControlToSidaList = FRB.GetAllControlToSidaFields();

                if (editInfo.codeOstad > 0)
                {
                    if (txtHomePhone.Text != editInfo.telHome)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtHomePhone.Text, editInfo.telHome, txtHomePhone.ID, txtProfessorCode_ContactInfo.Text));
                    if (txtWorkPhone.Text != editInfo.telHome)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtWorkPhone.Text, editInfo.telKar, txtWorkPhone.ID, txtProfessorCode_ContactInfo.Text));
                    if (txtMobileNumber.Text != editInfo.telMobile)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtMobileNumber.Text, editInfo.telMobile, txtMobileNumber.ID, txtProfessorCode_ContactInfo.Text));
                    if (txtLivingZipCode.Text != editInfo.codePosti)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtLivingZipCode.Text, editInfo.codePosti, txtLivingZipCode.ID, txtProfessorCode_ContactInfo.Text));
                    if (txtWorkingAddress.Text != editInfo.addressKar)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtWorkingAddress.Text, editInfo.addressKar, txtWorkingAddress.ID, txtProfessorCode_ContactInfo.Text));
                    if (txtLivingAddress.Text != editInfo.addressHome)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, txtLivingAddress.Text, editInfo.addressHome, txtLivingAddress.ID, txtProfessorCode_ContactInfo.Text));


                    if (drpProvince_Home.SelectedItem.Value != editInfo.ostanHome.ToString())
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpProvince_Home.SelectedItem.Value, editInfo.ostanHome.ToString(), "drpProvince1Value", txtProfessorCode_ContactInfo.Text));
                    if (drpProvince_Work.SelectedItem.Value != editInfo.ostanKar.ToString())
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpProvince_Home.SelectedItem.Value, editInfo.ostanKar.ToString(), "drpProvince2Value", txtProfessorCode_ContactInfo.Text));
                    if (drpCity_Home.SelectedItem.Value != editInfo.shahrHome.ToString())
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpCity_Home.SelectedItem.Value, editInfo.shahrHome.ToString(), "drpLivingCityValue", txtProfessorCode_ContactInfo.Text));
                    if (drpCity_Work.Items.Count > 0 && drpCity_Work.SelectedItem.Value != editInfo.addressKar)
                        oEditDTO.ChangeList.Add(AddValueToChangeList(dtControlToSidaList, drpCity_Work.SelectedItem.Value, editInfo.shahrKar.ToString(), "drpWorkingCityValue", txtProfessorCode_ContactInfo.Text));


                    oEditDTO.Code_Ostad = Convert.ToInt32(txtProfessorCode_ContactInfo.Text);
                    oEditDTO.Createdate = DateTime.Now.ToPeString();
                    oEditDTO.RequestTypeID = (int)RequestTypeId.EditContactInfo; // درخواست ویرایش مشخصات تماس
                    oEditDTO.RequestLogID = (int)RequestLogId.submitted; // در حال بررسی
                    oEditDTO.HR_InfoPeople_Id = Convert.ToInt32(editInfo.hrId);
                    oEditDTO.ChangeSet = 0;
                    Business.university.Request.ProfessorRequestBusiness ProfReqBuss = new Business.university.Request.ProfessorRequestBusiness();
                    int Id = ProfReqBuss.AddNewEditRequest(oEditDTO);
                    if (ProfReqBuss.UpdateOstadInformation_AfterApprove(Id)
                                                    && ProfReqBuss.UpdateProfessorRequestStatus(Id, (int)RequestLogId.approved) > 0)
                    {

                        setLog(DTO.eventEnum.ویرایش_کارگزینی, "", Id);
                    }
                }

            }
        }

        protected void btnSubmitChanges_EmployInfo_Click(object sender, EventArgs e)
        {
            if (Business.Common.CommonBusiness.IsNumeric(txtSearchProfessorCode_EmployInfo.Text))
            {
                Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();

                DataTable dtProfessor = FRB.GetOstadInfoFromHR(Convert.ToInt32(txtSearchProfessorCode_EmployInfo.Text));
                if (dtProfessor.Rows.Count > 0)
                {
                    txtProfessorCode_EmployInfo.Text = txtSearchProfessorCode_EmployInfo.Text;
                    txtProfessorName_EmployInfo.Text = dtProfessor.Rows[0]["name"].ToString() + " " + dtProfessor.Rows[0]["family"].ToString();
                    Business.university.Request.ProfessorRequestBusiness ProfReqBuss = new Business.university.Request.ProfessorRequestBusiness();

                    var dataRow = dtProfessor.Rows[dtProfessor.Rows.Count - 1];
                    var lastHokm = ProfReqBuss.GetLastHokmInfoByInfoPeopleID(Convert.ToInt32(dataRow["Id"]));

                    ProfessorEditRequestDTO oEditDTO = new ProfessorEditRequestDTO();
                    oEditDTO.Code_Ostad = Convert.ToInt32(txtSearchProfessorCode_EmployInfo.Text);
                    oEditDTO.Createdate = DateTime.Now.ToPeString();
                    oEditDTO.RequestTypeID = (int)RequestTypeId.EditHokm; // درخواست ویرایش حکم
                    oEditDTO.RequestLogID = (int)RequestLogId.submitted; // در حال بررسی
                    oEditDTO.HR_InfoPeople_Id = Convert.ToInt32(dataRow["Id"]);
                    oEditDTO.ChangeSet = 0;


                    switch (rblIsHeiat.SelectedItem.Value)
                    {
                        case "1"://هست
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

                            oHokm.State = (int)ChangeState.submitted;
                            if (rblIsHeiat.SelectedItem.Value == "1")
                                oHokm.Martabeh = Convert.ToInt32(drpMartabe.SelectedValue);
                            else
                                oHokm.Martabeh = 8;
                            oHokm.BoundHour = chkBoundHour.Checked;

                            string subPath = "";
                            if (ruScanHokm.UploadedFiles.Count > 0)
                            {
                                UploadedFile obj = ruScanHokm.UploadedFiles[0];
                                if (!Directory.Exists(Server.MapPath("~/University/CooperationRequest/Teachers/ScanMadarek/"+ txtProfessorCode_EmployInfo.Text +"/")))
                                    Directory.CreateDirectory(Server.MapPath("~/University/CooperationRequest/Teachers/ScanMadarek/"+ txtProfessorCode_EmployInfo.Text +"/"));
                                subPath = ("~/University/CooperationRequest/Teachers/ScanMadarek/" + txtProfessorCode_EmployInfo.Text + "\\" + "Hokm" + DateTime.Now.ToPeString("yyyy-MM-dd_HH-mm-ss") + obj.GetName()).improveFileName();

                                obj.SaveAs(Server.MapPath(subPath));
                                //ruScanHokm.UploadedFiles[0].InputStream.Close();
                                oHokm.HokmUrl = Server.MapPath(subPath);
                            }
                            oEditDTO.Hokm = oHokm;
                            break;
                        case "2"://نیست
                            string martabeh = lastHokm.Martabeh.ToString();
                            if (string.IsNullOrWhiteSpace(martabeh) || martabeh == "0" || martabeh == "-2" || martabeh == "8")
                            {
                                return;
                            }

                            break;

                    }
                    int Id = ProfReqBuss.AddNewEditRequest(oEditDTO);
                    if (Id > 0)
                    {
                        ProfessorHokmDTO oNewHokm = ProfReqBuss.GetNewHokmInfo(Id);
                        oEditDTO.Hokm.EditRequestId = Id;
                        oEditDTO.Hokm.State = (int)ChangeState.approved;
                        oEditDTO.Hokm.HokmId = oNewHokm.HokmId;
                        oEditDTO.Hokm.DateRunHokmHere = txtDateEjraHokmHere.Text;
                        if (ProfReqBuss.updateHokmInThreeTables(oEditDTO.Hokm))
                            ProfReqBuss.ApproveNewHokm(oEditDTO.Hokm);
                        setLog(DTO.eventEnum.ویرایش_کارگزینی, "", Id);
                    }

                }
            }
        }

        protected void btnSearchTeacher_EmployInfo_Click(object sender, EventArgs e)
        {
            if (Business.Common.CommonBusiness.IsNumeric(txtSearchProfessorCode_EmployInfo.Text))
            {
                Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();

                DataTable dtProfessor = FRB.GetOstadInfoFromHR(Convert.ToInt32(txtSearchProfessorCode_EmployInfo.Text));
                if (dtProfessor.Rows.Count > 0)
                {
                    txtProfessorCode_EmployInfo.Text = txtSearchProfessorCode_EmployInfo.Text;
                    txtProfessorName_EmployInfo.Text = dtProfessor.Rows[0]["name"].ToString() + " " + dtProfessor.Rows[0]["family"].ToString();
                    Business.university.Request.ProfessorRequestBusiness ProfReqBuss = new Business.university.Request.ProfessorRequestBusiness();

                    var dataRow = dtProfessor.Rows[dtProfessor.Rows.Count - 1];
                    var lastHokm = ProfReqBuss.GetLastHokmInfoByInfoPeopleID(Convert.ToInt32(dataRow["Id"]));


                    if (lastHokm.HokmId > 0)
                    {
                        string martabeh = lastHokm.Martabeh.ToString();

                        pnlDetails.Enabled = true;
                        pnlDetails.Visible = true;
                        if (string.IsNullOrWhiteSpace(martabeh) || martabeh == "0" || martabeh == "-2" || martabeh == "8")
                        {
                            if (drpMartabe.Items.FindByValue("0") != null)
                                drpMartabe.SelectedValue = "0";
                            pnlDetails.Enabled = false;
                            pnlDetails.Visible = false;
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
                        if (dtProfessor.Rows[0]["nahveh_hamk"] != DBNull.Value)
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
                        }
                        else //هیات علمی نیست
                        {
                            if (rblIsHeiat.Items.FindByValue("2") != null)
                                rblIsHeiat.SelectedValue = "2";
                        }
                        if (lastHokm.BoundHour != null)
                            chkBoundHour.Checked = Convert.ToBoolean(lastHokm.BoundHour);

                    }
                }

            }
        }

        protected void btnSearchTeacher_ContactInfo_Click(object sender, EventArgs e)
        {

            if (Business.Common.CommonBusiness.IsNumeric(txtSearchProfessorCode_ContactInfo.Text))
            {
                Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();
                DTO.University.Faculty.editInfoStruct editInfo = FRB.getOstadInf(Convert.ToInt32(txtSearchProfessorCode_ContactInfo.Text));
                if (editInfo.codeOstad > 0)
                {
                    txtProfessorCode_ContactInfo.Text = editInfo.codeOstad.ToString();
                    txtProfessorName_ContactInfo.Text = editInfo.name + " " + editInfo.family;
                    txtEmail.Text = editInfo.email;
                    txtHomePhone.Text = editInfo.telHome;
                    txtWorkPhone.Text = editInfo.telKar;
                    txtMobileNumber.Text = editInfo.telMobile;
                    txtLivingAddress.Text = editInfo.addressHome;
                    txtWorkingAddress.Text = editInfo.addressKar;
                    txtLivingZipCode.Text = editInfo.codePosti;
                    if (editInfo.ostanHome > 0)
                    {
                        drpProvince_Home.SelectedValue = editInfo.ostanHome.ToString();
                        setDropdownCity(editInfo.ostanHome, drpCity_Home);
                        drpCity_Home.SelectedValue = editInfo.shahrHome.ToString();
                    }
                    if (editInfo.ostanKar > 0)
                    {
                        drpProvince_Work.SelectedValue = editInfo.ostanKar.ToString();
                        setDropdownCity(editInfo.ostanKar, drpCity_Work);
                        drpCity_Work.SelectedValue = editInfo.shahrKar.ToString();
                    }


                }
            }
        }

        private void setDropdownCity(int provinceID, DropDownList drp)
        {
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            drp.Items.Clear();
            DataTable dtShahr = CB.getShahrestan(provinceID);
            drp.DataSource = dtShahr;
            drp.DataTextField = "Title";
            drp.DataValueField = "ID";
            drp.DataBind();
            ListItem l = new ListItem();
            l.Value = "0";
            l.Text = "انتخاب کنید";

            drp.Items.Insert(0, l);
        }

        protected void btnSearchTeacher_PersonalInfo_Click(object sender, EventArgs e)
        {
            if (Business.Common.CommonBusiness.IsNumeric(txtSearchProfessorCode_PersonalInfo.Text))
            {
                Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();
                DTO.University.Faculty.editInfoStruct editInfo = FRB.getOstadInf(Convert.ToInt32(txtSearchProfessorCode_PersonalInfo.Text));
                if (editInfo.codeOstad > 0)
                {
                    txtProfessorCode_PersonalInfo.Text = editInfo.codeOstad.ToString();
                    txtProfessorName_PersonalInfo.Text = editInfo.name + " " + editInfo.family;

                    txtFirstName.Text = editInfo.name;
                    txtFamily.Text = editInfo.family;
                    txtFatherName.Text = editInfo.fatherName;
                    txtShCode.Text = editInfo.idd;
                    txtYearBorn.Text = editInfo.salTavalod;
                    if (!editInfo.sexIsMan)
                    {
                        pnlMilitary.Visible = false;
                        dvNezam.Visible = false;
                    }
                    else
                    {
                        dvNezam.Visible = true;
                        drpNezam.SelectedValue = editInfo.nezam.ToString();
                    }
                    rdblMarriage.SelectedValue = editInfo.taahol ? "2" : "1";
                    rblGender.SelectedValue = editInfo.sexIsMan ? "1" : "2";
                    drpLastMaghta.SelectedValue = editInfo.maghta.ToString();
                    drpReshte.SelectedValue = editInfo.reshte.ToString();
                    drpUniversityType.SelectedValue = editInfo.typeUniMadrak.ToString();
                    txtSiba.Text = editInfo.siba;
                    txtYearGetMadrak.Text = editInfo.salMadrak;
                    txtSanavat.Text = editInfo.sanavat;
                    dvJame.Visible = editInfo.maghta == 13;
                    drpCountry.SelectedValue = editInfo.keshvar.ToString();
                    //dvJame.Visible=editInfo.
                    dvArzeshname.Visible = editInfo.keshvar.ToString() != "27";
                    if (dvArzeshname.Visible)
                        dvJame.Visible = false;
                    drpUniName.SelectedValue = editInfo.nameUniMadrak.ToString();
                    if (editInfo.bime)
                    {
                        rdblBimehStatus.SelectedValue = "1";
                        drpBimehType.Enabled = true;
                        txtInsuranceNumber.Enabled = true;
                        drpBimehType.SelectedValue = editInfo.bimeType.ToString();
                        txtInsuranceNumber.Text = editInfo.bimeNum;
                        drpBimehType.Enabled = true;
                        txtInsuranceNumber.Enabled = true;
                        dvBime.Visible = true;
                    }
                    else
                    {
                        rdblBimehStatus.SelectedValue = "2";
                    }
                    chbkIsRetired.Checked = editInfo.bazneshaste;
                    txtDescription.Text = editInfo.description;

                }
            }
        }

        protected void drpLastMaghta_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvJame.Visible = drpLastMaghta.SelectedItem.Value == "13";
        }

        protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvArzeshname.Visible = drpCountry.SelectedItem.Value != "27";
            if (dvArzeshname.Visible)
                dvJame.Visible = false;
        }

        protected void drpProvince_Home_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDropdownCity(Convert.ToInt32(drpProvince_Home.SelectedItem.Value), drpCity_Home);
        }

        protected void drpProvince_Work_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDropdownCity(Convert.ToInt32(drpProvince_Work.SelectedItem.Value), drpCity_Work);

        }

        protected void rblIsHeiat_SelectedIndexChanged1(object sender, EventArgs e)
        {
            pnlDetails.Visible = rblIsHeiat.SelectedItem.Value == "1";
            pnlDetails.Enabled = rblIsHeiat.SelectedItem.Value == "1";
        }
    }
    public static class extentions
    {
        public static string formatDateString(this string date)
        {
            if (date.Length < 5 || !date.Contains("/"))
                return date;
            string year, month, day;
            year = date.Substring(0, date.IndexOf("/"));
            if (!year.StartsWith("13"))
                year = "13" + year;
            month = date.Substring(date.IndexOf("/") + 1, date.LastIndexOf("/") - date.IndexOf("/") - 1);
            day = date.Substring(date.LastIndexOf("/") + 1);
            string tempDate = string.Format("{0:0000}/{1:00}/{2:00}", Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
            return tempDate;
        }

        public static string improveFileName(this string fileName)
        {
            fileName = fileName.Replace("%", "");
            fileName = fileName.Replace("+", "");
            return fileName;
        }
    }
}