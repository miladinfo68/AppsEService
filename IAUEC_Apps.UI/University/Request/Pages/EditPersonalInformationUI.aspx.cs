using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using System;
using System.Collections.Generic;
using System.Data;
//using IAUEC_Apps.DTO.University.Request;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Request.Pages
{
    public partial class EditPersonalInformation : System.Web.UI.Page
    {

        CommonBusiness CommonBusiness = new CommonBusiness();
        System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

        EditPersonalInformationBusiness business = new EditPersonalInformationBusiness();
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();
        List<StudentsChild> childrenList = new List<StudentsChild>();
        public Stream fs;
        DataTable dt = new DataTable();
        DataTable dtmob = new DataTable();
        DataTable dtre = new DataTable();
        bool IsPostBack = false;

        protected void Page_Load(object sender, EventArgs e)
        {

            RadAsyncUploadimg.FileUploaded += new Telerik.Web.UI.FileUploadedEventHandler(RadAsyncUploadimg_FileUploaded);

            if (!Page.IsPostBack)
            {
                //if (Request.UrlReferrer == null)
                //    Response.Redirect("/CommonUI/IntroPage.aspx");
                //برای زمانی که از رزرواسیون به طور مستقیم می یاد
                if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains("StudentAddRequest"))
                    Session[sessionNames.appID_StudentOstad] = "4";

                initialComponents();
                dtre = business.GetStEditRequest(Session[sessionNames.userID_StudentOstad].ToString());

                if (dtre.Rows.Count > 0)
                {
                    grd_EditeRequestState.DataSource = dtre;
                    grd_EditeRequestState.DataBind();
                    grd_EditeRequestState.Visible = true;
                }
                else
                    grd_EditeRequestState.Visible = false;
            }
            else
            {
                IsPostBack = true;
            }
        }

        private void initialComponents()
        {
            #region panel1

            //DataTable dtPlace = CommonBusiness.GetCodingByTypeId(13);

            dt = CartBusiness.GetStudentsInfo(Session[sessionNames.userID_StudentOstad].ToString());
            //DataTable dtReg = business.getStudentInfoFromInitialRegistration(Session[sessionNames.userID_StudentOstad].ToString());

            //DataTable dtShahrestan;
            DataTable dtOstan = CommonBusiness.GetStateFromTblOstan();


            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["home_postalCode"] != DBNull.Value)
                    txt_CodePosti.Text = dt.Rows[0]["home_postalCode"].ToString();
                if (dt.Rows[0]["homePhone"].ToString() != "" && dt.Rows[0]["homePhone"].ToString().Length == 11)
                {
                    txt_Tell.Text = dt.Rows[0]["homePhone"].ToString().Substring(3, 8);
                    txt_Pishshomare.Text = dt.Rows[0]["homePhone"].ToString().Substring(0, 3);
                }
                if (dt.Rows[0]["mobile"] != DBNull.Value)
                    txt_Hamrah.Text = dt.Rows[0]["mobile"].ToString();

                if (dt.Rows[0]["homeAddress"].ToString() != "")
                    txt_Address.Text = dt.Rows[0]["homeAddress"].ToString();
                if (dt.Rows[0]["birthDay"] != DBNull.Value)
                {
                    txtBirthday.Text = dt.Rows[0]["birthDay"].ToString();
                }

                setStateAndCitySourceAndValue(ddl_Ostan, ddl_Shahr, dt.Rows[0]["home_state"], dt.Rows[0]["home_city"]);
                setStateAndCitySourceAndValue(drpBirthPlace_State, drpBirthPlace_City, dt.Rows[0]["birthPlace_state"], dt.Rows[0]["birthPlace_city"]);
                setStateAndCitySourceAndValue(drpIssuePlace_State, drpIssuePlace_City, dt.Rows[0]["issuePlace_state"], dt.Rows[0]["issuePlace_city"]);

                try
                {
                    if (dt.Rows[0]["pic"] != DBNull.Value && dt.Rows[0]["pic"].ToString() != "")
                    {
                        Session["bytes"] = (byte[])(dt.Rows[0]["pic"]);
                        img_Personal.DataValue = (byte[])(Session["bytes"]);
                    }
                }
                catch { }
            }
            RadAsyncUploadimg.Visible = false;




            #endregion panel1

            #region panel2
            DataTable dtVaziatJesmani = CommonBusiness.GetCodingByTypeId(8);
            DataTable dtDin = CommonBusiness.GetCodingByTypeId(6);
            DataTable dtMeliat = CommonBusiness.GetCodingByTypeId(16);
            ddlBodyStatus.DataSource = dtVaziatJesmani;
            ddlBodyStatus.DataTextField = "namecoding";
            ddlBodyStatus.DataValueField = "id";
            ddlBodyStatus.DataBind();
            drpReligion.DataSource = dtDin;
            drpReligion.DataTextField = "namecoding";
            drpReligion.DataValueField = "id";
            drpReligion.DataBind();


            drpNationality.DataSource = dtMeliat;
            drpNationality.DataTextField = "namecoding";
            drpNationality.DataValueField = "id";
            drpNationality.DataBind();
            if (dt.Rows[0]["email"] != DBNull.Value)
            {
                txt_Email.Text = dt.Rows[0]["email"].ToString();
            }
            if (dt.Rows[0]["religion"] != DBNull.Value)
            {
                var tempInt = 0;
                if (int.TryParse(dt.Rows[0]["religion"].ToString(), out tempInt) && tempInt > 0)
                {
                    drpReligion.SelectedValue = dt.Rows[0]["religion"].ToString();
                    CheckReligionSectIsAvailable();
                }
            }
            if (dt.Rows.Count > 0 && dt.Rows[0]["religionSect"] != DBNull.Value)
            {
                var tempInt = 0;
                if (int.TryParse(dt.Rows[0]["religionSect"].ToString(), out tempInt) && tempInt > 0)
                    drpReligionSect.SelectedValue = dt.Rows[0]["religionSect"].ToString();
            }
            if (dt.Rows[0]["physicalCondition"] != DBNull.Value)
            {
                var tempInt = 0;
                if (int.TryParse(dt.Rows[0]["physicalCondition"].ToString(), out tempInt) && tempInt > 0)
                    ddlBodyStatus.SelectedValue = dt.Rows[0]["physicalCondition"].ToString();
            }
            if (dt.Rows[0]["nationality"] != DBNull.Value)
            {
                var tempInt = 0;
                if (int.TryParse(dt.Rows[0]["nationality"].ToString(), out tempInt) && tempInt > 0)
                    drpNationality.SelectedValue = dt.Rows[0]["nationality"].ToString();
            }
            //if (dtReg.Rows.Count > 0)
            {
                if (dt.Rows[0]["residence"] != DBNull.Value)
                {
                    var tempInt = 0;
                    if (int.TryParse(dt.Rows[0]["residence"].ToString(), out tempInt) && tempInt > 0)
                        drpAccommodation.SelectedValue = dt.Rows[0]["residence"].ToString();
                }
                if (dt.Rows[0]["IntroductionMethod"] != DBNull.Value)
                {
                    var tempInt = 0;
                    if (int.TryParse(dt.Rows[0]["IntroductionMethod"].ToString(), out tempInt) && tempInt > 0)
                        drpIntroductionMethod.SelectedValue = dt.Rows[0]["IntroductionMethod"].ToString();
                }
                if (dt.Rows[0]["LastUniversityType"] != DBNull.Value)
                {
                    var tempInt = 0;
                    if (int.TryParse(dt.Rows[0]["LastUniversityType"].ToString(), out tempInt) && tempInt > 0)
                        drpBaseEducationUniType.SelectedValue = dt.Rows[0]["LastUniversityType"].ToString();
                }
                if (dt.Rows[0]["ConnectionType"] != DBNull.Value)
                {
                    var tempInt = 0;
                    if (int.TryParse(dt.Rows[0]["ConnectionType"].ToString(), out tempInt) && tempInt > 0)
                        drpConnectionType.SelectedValue = dt.Rows[0]["ConnectionType"].ToString();
                    setServiceProviderSource();
                }
                if (dt.Rows[0]["InternetProvider"] != DBNull.Value)
                {
                    var tempInt = 0;
                    if (int.TryParse(dt.Rows[0]["InternetProvider"].ToString(), out tempInt) && tempInt > 0)
                        drpServiceProvider.SelectedValue = dt.Rows[0]["InternetProvider"].ToString();
                }
                if (dt.Rows[0]["Accessories"] != DBNull.Value)
                {
                    string[] Accessories = dt.Rows[0]["Accessories"].ToString().Split(',');
                    foreach (string s in Accessories)
                    {
                        if (chbCommunicationEquipment.Items.FindByValue(s) != null)
                            chbCommunicationEquipment.Items.FindByValue(s).Selected = true;
                    }
                }
            }


            #endregion panel2
            //if (dtReg.Rows.Count > 0)
            {
                drpEduField.DataSource = CommonBusiness.SelectAllField();
                drpEduField.DataTextField = "nameresh";
                drpEduField.DataValueField = "id";
                drpEduField.DataBind();
                drpEduField.Items.Insert(0, "انتخاب نمایید..");

                drpUniName.DataSource = CommonBusiness.GetNameUni_fcoding();
                drpUniName.DataTextField = "namecoding";
                drpUniName.DataValueField = "id";
                drpUniName.DataBind();
                drpUniName.Items.Insert(0, "انتخاب نمایید..");

                #region تحصیل همزمان
                if (dt.Rows[0]["SimultaneousStudy"] != DBNull.Value)
                {
                    drpSyncEdu.SelectedValue = dt.Rows[0]["SimultaneousStudy"].ToString();
                    dvSyncEdu.Visible = drpSyncEdu.SelectedValue == "1";
                }
                if (dt.Rows[0]["SimultaneousLevel"] != DBNull.Value)
                {
                    drpEduLevel.SelectedValue = dt.Rows[0]["SimultaneousLevel"].ToString();
                }
                if (dt.Rows[0]["SimultaneousField"] != DBNull.Value)
                {
                    drpEduField.SelectedValue = dt.Rows[0]["SimultaneousField"].ToString();
                }
                if (dt.Rows[0]["SimultaneousUniType"] != DBNull.Value)
                {
                    drpUniType.SelectedValue = dt.Rows[0]["SimultaneousUniType"].ToString();
                }
                if (dt.Rows[0]["SimultaneousUni"] != DBNull.Value)
                {
                    drpUniName.SelectedValue = dt.Rows[0]["SimultaneousUni"].ToString();
                }
                if (dt.Rows[0]["SimultaneousEntrance"] != DBNull.Value)
                {
                    txtEnterYear.Text = dt.Rows[0]["SimultaneousEntrance"].ToString();
                }
                #endregion تحصیل همزمان

                #region شغل
                drpState_Work.DataSource = dtOstan;
                drpState_Work.DataTextField = "title";
                drpState_Work.DataValueField = "ID";
                drpState_Work.DataBind();

                drpCity_Work.DataTextField = "title";
                drpCity_Work.DataValueField = "ID";
                if (dt.Rows[0]["jobStatus"] != DBNull.Value)
                {
                    drpJobstatus.SelectedValue = dt.Rows[0]["jobStatus"].ToString() == "1" ?  "1" : "0";
                    dvEmploy.Visible = drpJobstatus.SelectedValue == "1";
                }
                if (dt.Rows[0]["jobTitle"] != DBNull.Value)
                {
                    txtJobTitle.Text = dt.Rows[0]["jobTitle"].ToString();
                }
                if (dt.Rows[0]["JobTime"] != DBNull.Value)
                {
                    drpHireType.SelectedValue = dt.Rows[0]["JobTime"].ToString();
                }
                if (dt.Rows[0]["JobContract"] != DBNull.Value)
                {
                    drpJobContract.SelectedValue = dt.Rows[0]["JobContract"].ToString();
                }
                if (dt.Rows[0]["JobType"] != DBNull.Value)
                {
                    drpWorkplaceType.SelectedValue = dt.Rows[0]["JobType"].ToString();
                }
                if (dt.Rows[0]["JobPosition"] != DBNull.Value)
                {
                    drpJobPosition.SelectedValue = dt.Rows[0]["JobPosition"].ToString();
                }
                if (dt.Rows[0]["workplace_phone"] != DBNull.Value)
                {
                    if (dt.Rows[0]["workplace_phone"].ToString().Length == 11)
                    {
                        txtTel_Job.Text = dt.Rows[0]["workplace_phone"].ToString().Substring(3);
                        txtZipCode_Job.Text = dt.Rows[0]["workplace_phone"].ToString().Substring(0, 3);
                    }
                }
                if (dt.Rows[0]["workPlace_postalCode"] != DBNull.Value)
                {
                    txtPostalCode_Job.Text = dt.Rows[0]["workPlace_postalCode"].ToString();
                }
                if (dt.Rows[0]["workPlace_state"] != DBNull.Value)
                {
                    drpState_Work.SelectedValue = dt.Rows[0]["workPlace_state"].ToString();
                    drpCity_Work.DataSource = CommonBusiness.getShahrestan(Convert.ToInt32(drpState_Work.SelectedValue));
                    drpCity_Work.DataBind();

                }
                if (dt.Rows[0]["workPlace_city"] != DBNull.Value)
                {
                    drpCity_Work.SelectedValue = dt.Rows[0]["workPlace_city"].ToString();
                }
                if (dt.Rows[0]["workPlaceAddress"] != DBNull.Value)
                {
                    txtAddress_work.Text = dt.Rows[0]["workPlaceAddress"].ToString();
                }
                #endregion شغل

                #region خانواده

                if (dt.Rows[0]["maritalStatus"] != DBNull.Value)
                {
                    drpMaritalStatus.SelectedValue = dt.Rows[0]["maritalStatus"].ToString();
                    dvMaritalStatus.Visible = drpMaritalStatus.SelectedValue == "1";
                }
                if (dt.Rows[0]["SpouseFirstName"] != DBNull.Value)
                {
                    txtName_Spouse.Text = dt.Rows[0]["SpouseFirstName"].ToString();
                }
                if (dt.Rows[0]["SpouseLastName"] != DBNull.Value)
                {
                    txtLastname_spouse.Text = dt.Rows[0]["SpouseLastName"].ToString();
                }
                if (dt.Rows[0]["SpouseIsEmployed"] != DBNull.Value)
                {
                    drpJobstatus_spouse.SelectedValue = dt.Rows[0]["SpouseIsEmployed"].ToString().ToLower() == "true" ? "1" : "0"; ;
                }
                if (dt.Rows[0]["SpouseJobTitle"] != DBNull.Value)
                {
                    txtJobTitle_spouse.Text = dt.Rows[0]["SpouseJobTitle"].ToString();
                }
                var childrenDT = CartBusiness.GetStudentChildren(Session[sessionNames.userID_StudentOstad].ToString());
                
                rgvChildren.DataSource = getStudentChildren(childrenDT);
                rgvChildren.DataBind();
                #endregion خانواده
            }
            //if (dtReg.Rows.Count == 0)
            //{
            //    initialRegisterDiv.Visible = false;
            //}
        }

        private void setStateAndCitySourceAndValue(DropDownList ddlState, DropDownList ddlCity, object state, object city)
        {
            DataTable dtState = CommonBusiness.GetStateFromTblOstan();
            DataTable dtCity;
            ddlState.DataTextField = "Title";
            ddlState.DataValueField = "ID";
            ddlCity.DataTextField = "Title";
            ddlCity.DataValueField = "ID";
            ddlState.DataSource = dtState;
            ddlState.DataBind();
            ddlState.Items.Insert(0, "انتخاب نمایید..");

            
            if (state != DBNull.Value)
            {
                ddlState.SelectedValue = state.ToString();
                if (city != DBNull.Value)
                {
                    dtCity = CommonBusiness.getCitiesFromTblShahrestan(int.Parse(state.ToString()));
                    ddlCity.DataSource = dtCity;
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, "انتخاب نمایید..");
                    ddlCity.Items.Add(new ListItem { Text = "سایر", Value = "0" });
                    if (city != DBNull.Value && Convert.ToInt32(city) == 0)
                        ddlCity.SelectedValue = "0";
                    else if (dtCity.Select("ID=" + city.ToString()).Count() == 0)
                        ddlCity.SelectedIndex = 0;
                    else
                        ddlCity.SelectedValue = city.ToString();
                }
                ddlState.Visible = true;
                ddlCity.Visible = true;
            }
        }

        /// <summary>
        ///اگر کد پستی توسط دانشجو اصلاح شود این متد اجرا می گردد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

        public void RadAsyncUploadimg_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            Bitmap bitmapImage = ResizeImage(RadAsyncUploadimg.UploadedFiles[0].InputStream);
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bitmapImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            Session["bytes"] = stream.ToArray();
        }


        public Bitmap ResizeImage(Stream stream)
        {
            System.Drawing.Image originalImage = Bitmap.FromStream(stream);

            int height = 100;
            int width = 100;

            Bitmap scaledImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, width, height);

                return scaledImage;
            }

        }

        /// <summary>
        /// زمانیکه درخواست ارسال می گردد این متد فراخوانی می شود تا کلیه کنترل ها پنهان گردند
        /// </summary>


        /// <summary>
        /// این گیرید ویو شامل اطلاعاتی است که دانشجو قبلا درخواست ویرایش داده است
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void grd_EditeRequeststate_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            DataTable dtre = new DataTable();
            dtre = business.GetStEditRequest(Session[sessionNames.userID_StudentOstad].ToString());
            grd_EditeRequestState.DataSource = dtre;
            grd_EditeRequestState.DataBind();
            grd_EditeRequestState.Visible = true;
        }
        /// <summary>
        /// چنانچه دانشجو استان خود را انتخاب نماید، در دراپ دان دیگر شهرهای آن نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void drd_ostan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtshahr = CommonBusiness.getCitiesFromTblShahrestan(int.Parse(ddl_Ostan.SelectedValue.ToString()));
            ddl_Shahr.DataSource = dtshahr;
            ddl_Shahr.DataBind();
            ddl_Shahr.Items.Insert(0, "انتخاب نمایید..");
            //ddl_Shahr.Items.Add(new ListItem { Text = "سایر", Value = "0" });
            //Session["ostan"] = "true";
        }

        //protected void btnEditTel_Click(object sender, EventArgs e)
        //{
        //    var noOpenRequest = true;
        //    dtre = business.GetStEditRequest(Session[sessionNames.userID_StudentOstad].ToString());
        //    for (int i = 0; i < dtre.Rows.Count; i++)
        //        if (int.Parse(dtre.Rows[i]["EditedID"].ToString()) == 6 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 5 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 7)))
        //            noOpenRequest = false;

        //    if (noOpenRequest)
        //    {
        //        //if (btnEditTel.Text == "<i class='fa fa-pencil'></i> <span>ویرایش</span>")
        //        //{
        //        //    btnEditTel.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
        //        txt_Tell.Enabled = true;
        //        txt_Pishshomare.Enabled = true;
        //        rfvPish.Enabled = true;
        //        rfvTel.Enabled = true;
        //        //}
        //        //else
        //        {
        //            if (CommonBusiness.ValidatePhoneNumber(txt_Tell.Text) == true && CommonBusiness.ValidatePreCodePhoneNumber(txt_Pishshomare.Text) == true)
        //            {
        //                int reqid = CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 6, 6, "Null", "", 1);
        //                DataTable dtmax = new DataTable();
        //                dtmax = business.GetStudentRequestID(Session[sessionNames.userID_StudentOstad].ToString(), 6, 6);
        //                business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), txt_Pishshomare.Text + txt_Tell.Text, 6, 6, int.Parse(dtmax.Rows[0]["StudentRequestID"].ToString()));
        //                CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 16, reqid.ToString());

        //                LoadGrid();
        //                //btnEditTel.Text = "<i class='fa fa-pencil'></i> <span>ویرایش</span>";
        //                txt_Tell.Enabled = false;
        //                txt_Pishshomare.Enabled = false;
        //                rfvPish.Enabled = false;
        //                rfvTel.Enabled = false;
        //            }
        //            else
        //                rwm_Validations.RadAlert("لطفا شماره تلفن و پیش شماره را به صورت صحیح پرنمایید", null, 100, "خطا", "");
        //        }
        //    }
        //    else
        //        rwm_Validations.RadAlert("بدلیل وجود درخواست در حال بررسی ویرایش تلفن، امکان ثبت درخواست جدید نمی باشد. ", null, 100, "پیام", "");
        //}

        //protected void btnEditMobile_Click(object sender, EventArgs e)
        //{
        //    var noOpenRequest = true;
        //    dtre = business.GetStEditRequest(Session[sessionNames.userID_StudentOstad].ToString());
        //    for (int i = 0; i < dtre.Rows.Count; i++)
        //        if (int.Parse(dtre.Rows[i]["EditedID"].ToString()) == 9 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 5 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 7)))
        //            noOpenRequest = false;

        //    if (noOpenRequest)
        //    {

        //        //if (btnEditMobile.Text == "<i class='fa fa-pencil'></i> <span>ویرایش</span>")
        //        //{
        //        //    btnEditMobile.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
        //        txt_Hamrah.Enabled = true;
        //        rfvMobile.Enabled = true;
        //        //}
        //        //else
        //        {
        //            if (CommonBusiness.ValidateMobile(txt_Hamrah.Text) == true)
        //            {
        //                int reqid = CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 9, 6, "Null", "", 1);
        //                DataTable dtmax = new DataTable();
        //                dtmax = business.GetStudentRequestID(Session[sessionNames.userID_StudentOstad].ToString(), 6, 9);
        //                business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), txt_Hamrah.Text, 9, 6, int.Parse(dtmax.Rows[0]["StudentRequestID"].ToString()));
        //                CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 9, reqid.ToString());

        //                LoadGrid();
        //                //btnEditMobile.Text = "<i class='fa fa-pencil'></i> <span>ویرایش</span>";
        //                txt_Hamrah.Enabled = false;
        //                rfvMobile.Enabled = false;
        //            }
        //            else
        //                rwm_Validations.RadAlert("لطفا شماره همراه را به صورت صحیح وارد نمایید", null, 100, "خطا", "");
        //        }

        //    }
        //    else
        //        rwm_Validations.RadAlert("بدلیل وجود درخواست در حال بررسی ویرایش تلفن همراه، امکان ثبت درخواست جدید نمی باشد. ", null, 100, "پیام", "");

        //}

        //protected void btnEditState_Click(object sender, EventArgs e)
        //{
        //    var noOpenRequest = true;
        //    dtre = business.GetStEditRequest(Session[sessionNames.userID_StudentOstad].ToString());
        //    for (int i = 0; i < dtre.Rows.Count; i++)
        //        if ((int.Parse(dtre.Rows[i]["EditedID"].ToString()) == 11 || int.Parse(dtre.Rows[i]["EditedID"].ToString()) == 12) && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 5 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 7)))
        //            noOpenRequest = false;

        //    if (noOpenRequest)
        //    {
        //        //if (btnEditState.Text == "<i class='fa fa-pencil'></i> <span>ویرایش</span>")
        //        //{
        //        //    btnEditState.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
        //        btnEditCity.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
        //        ddl_Ostan.Enabled = true;
        //        ddl_Shahr.Enabled = true;
        //        rfvState.Enabled = true;
        //        rfvCity.Enabled = true;
        //        //}
        //        //else
        //        {
        //            int cityId = 0;
        //            int stateId = 0;
        //            if (int.TryParse(ddl_Ostan.SelectedItem.Value, out stateId) && int.TryParse(ddl_Shahr.SelectedItem.Value, out cityId))
        //            {
        //                int reqid = CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 12, 6, "Null", "", 1);
        //                DataTable dtmax = new DataTable();
        //                dtmax = business.GetStudentRequestID(Session[sessionNames.userID_StudentOstad].ToString(), 6, 12);
        //                business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), stateId.ToString(), 12, 6, int.Parse(dtmax.Rows[0]["StudentRequestID"].ToString()));
        //                CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 12, reqid.ToString());

        //                reqid = CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 11, 6, "Null", "", 1);
        //                dtmax = new DataTable();
        //                dtmax = business.GetStudentRequestID(Session[sessionNames.userID_StudentOstad].ToString(), 6, 11);
        //                business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), cityId.ToString(), 11, 6, int.Parse(dtmax.Rows[0]["StudentRequestID"].ToString()));
        //                CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 11, reqid.ToString());
        //            }
        //            LoadGrid();
        //            //btnEditState.Text = "<i class='fa fa-pencil'></i> <span>ویرایش</span>";
        //            btnEditCity.Text = "<i class='fa fa-pencil'></i> <span>ویرایش</span>";
        //            ddl_Ostan.Enabled = false;
        //            ddl_Shahr.Enabled = false;
        //            rfvState.Enabled = false;
        //            rfvCity.Enabled = false;
        //        }
        //    }
        //    else
        //        rwm_Validations.RadAlert("بدلیل وجود درخواست در حال بررسی ویرایش شهر یا استان، امکان ثبت درخواست جدید نمی باشد. ", null, 100, "پیام", "CallBackConfirm");

        //}

        //protected void btnEditCity_Click(object sender, EventArgs e)
        //{
        //    var noOpenRequest = true;
        //    dtre = business.GetStEditRequest(Session[sessionNames.userID_StudentOstad].ToString());
        //    for (int i = 0; i < dtre.Rows.Count; i++)
        //        if ((int.Parse(dtre.Rows[i]["EditedID"].ToString()) == 11 || int.Parse(dtre.Rows[i]["EditedID"].ToString()) == 12) && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 5 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 7)))
        //            noOpenRequest = false;

        //    if (noOpenRequest)
        //    {
        //        //if (btnEditState.Text == "<i class='fa fa-pencil'></i> <span>ویرایش</span>")
        //        //{
        //        //    btnEditState.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
        //        btnEditCity.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
        //        ddl_Ostan.Enabled = true;
        //        ddl_Shahr.Enabled = true;
        //        rfvState.Enabled = true;
        //        rfvCity.Enabled = true;
        //        //}
        //        //else
        //        {
        //            int cityId = 0;
        //            int stateId = 0;
        //            if (int.TryParse(ddl_Ostan.SelectedItem.Value, out stateId) && int.TryParse(ddl_Shahr.SelectedItem.Value, out cityId))
        //            {
        //                int reqid = CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 12, 6, "Null", "", 1);
        //                DataTable dtmax = new DataTable();
        //                dtmax = business.GetStudentRequestID(Session[sessionNames.userID_StudentOstad].ToString(), 6, 12);
        //                business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), stateId.ToString(), 12, 6, int.Parse(dtmax.Rows[0]["StudentRequestID"].ToString()));
        //                CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 12, reqid.ToString());

        //                reqid = CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 11, 6, "Null", "", 1);
        //                dtmax = new DataTable();
        //                dtmax = business.GetStudentRequestID(Session[sessionNames.userID_StudentOstad].ToString(), 6, 11);
        //                business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), cityId.ToString(), 11, 6, int.Parse(dtmax.Rows[0]["StudentRequestID"].ToString()));
        //                CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 11, reqid.ToString());
        //            }
        //            LoadGrid();
        //            //btnEditState.Text = "<i class='fa fa-pencil'></i> <span>ویرایش</span>";
        //            btnEditCity.Text = "<i class='fa fa-pencil'></i> <span>ویرایش</span>";
        //            ddl_Ostan.Enabled = false;
        //            ddl_Shahr.Enabled = false;
        //            rfvState.Enabled = false;
        //            rfvCity.Enabled = false;
        //        }
        //    }
        //    else
        //        rwm_Validations.RadAlert("بدلیل وجود درخواست در حال بررسی ویرایش شهر یا استان، امکان ثبت درخواست جدید نمی باشد. ", null, 100, "پیام", "");

        //}

        //protected void btnAddress_Click(object sender, EventArgs e)
        //{
        //    var noOpenRequest = true;
        //    dtre = business.GetStEditRequest(Session[sessionNames.userID_StudentOstad].ToString());
        //    for (int i = 0; i < dtre.Rows.Count; i++)
        //        if (int.Parse(dtre.Rows[i]["EditedID"].ToString()) == 7 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 5 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 7)))
        //            noOpenRequest = false;

        //    if (noOpenRequest)
        //    {
        //        //if (btnAddress.Text == "<i class='fa fa-pencil'></i> <span>ویرایش</span>")
        //        //{
        //        //    btnAddress.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
        //        //    txt_Address.Enabled = true;
        //        //    rfvAddress.Enabled = true;
        //        //}
        //        //else
        //        {
        //            int reqid = CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 7, 6, "Null", "", 1);
        //            DataTable dtmax = new DataTable();
        //            dtmax = business.GetStudentRequestID(Session[sessionNames.userID_StudentOstad].ToString(), 6, 7);
        //            business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), txt_Address.Text, 7, 6, int.Parse(dtmax.Rows[0]["StudentRequestID"].ToString()));
        //            CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 7, reqid.ToString());

        //            LoadGrid();
        //            //btnAddress.Text = "<i class='fa fa-pencil'></i> <span>ویرایش</span>";
        //            txt_Address.Enabled = false;
        //            rfvAddress.Enabled = false;
        //        }
        //    }
        //    else
        //        rwm_Validations.RadAlert("بدلیل وجود درخواست در حال بررسی ویرایش آدرس، امکان ثبت درخواست جدید نمی باشد. ", null, 100, "پیام", "");

        //}

        //protected void btnPostalCode_Click(object sender, EventArgs e)
        //{
        //    var noOpenRequest = true;
        //    dtre = business.GetStEditRequest(Session[sessionNames.userID_StudentOstad].ToString());
        //    for (int i = 0; i < dtre.Rows.Count; i++)
        //        if (int.Parse(dtre.Rows[i]["EditedID"].ToString()) == 8 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 5 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 7)))
        //            noOpenRequest = false;

        //    if (noOpenRequest)
        //    {
        //        //if (btnPostalCode.Text == "<i class='fa fa-pencil'></i> <span>ویرایش</span>")
        //        //{
        //        //    btnPostalCode.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
        //        //    txt_CodePosti.Enabled = true;
        //        //    rfvPostalCode.Enabled = true;
        //        //}
        //        //else
        //        {
        //            if (CommonBusiness.ValidateZipCode(txt_CodePosti.Text) == true)
        //            {
        //                int reqid = CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 8, 6, "Null", "", 1);
        //                DataTable dtmax = new DataTable();
        //                dtmax = business.GetStudentRequestID(Session[sessionNames.userID_StudentOstad].ToString(), 6, 8);
        //                business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), txt_CodePosti.Text, 8, 6, int.Parse(dtmax.Rows[0]["StudentRequestID"].ToString()));
        //                CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 8, reqid.ToString());

        //                LoadGrid();
        //                //btnPostalCode.Text = "<i class='fa fa-pencil'></i> <span>ویرایش</span>";
        //                txt_CodePosti.Enabled = false;
        //                rfvPostalCode.Enabled = false;
        //            }
        //            else
        //                rwm_Validations.RadAlert("لطفا کد پستی را به صورت صحیح پرنمایید", null, 100, "خطا", "");
        //        }
        //    }
        //    else
        //        rwm_Validations.RadAlert("بدلیل وجود درخواست در حال بررسی ویرایش کدپستی، امکان ثبت درخواست جدید نمی باشد. ", null, 100, "پیام", "");
        //}

        protected void btnPic_Click(object sender, EventArgs e)
        {
            var noOpenRequest = true;
            dtre = business.GetStEditRequest(Session[sessionNames.userID_StudentOstad].ToString());
            for (int i = 0; i < dtre.Rows.Count; i++)
                if (int.Parse(dtre.Rows[i]["EditedID"].ToString()) == 10 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 5 && (int.Parse(dtre.Rows[i]["RequestLogID"].ToString()) != 7)))
                    noOpenRequest = false;

            if (noOpenRequest)
            {
                if (btnPic.Text == "<i class='fa fa-pencil'></i> <span>ویرایش</span>")
                {
                    btnPic.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
                    RadAsyncUploadimg.Visible = true;
                    rfvPic.Enabled = true;
                }
                else
                {
                    DataTable dtmax = new DataTable();
                    int reqid = CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), 10, 6, "Null", "", 1);
                    dtmax = business.GetStudentRequestID(Session[sessionNames.userID_StudentOstad].ToString(), 6, 10);

                    LoadGrid();
                    btnPic.Text = "<i class='fa fa-pencil'></i> <span>ویرایش</span>";
                    RadAsyncUploadimg.Visible = false;
                    rfvPic.Enabled = false;
                }
            }
            else
                rwm_Validations.RadAlert("بدلیل وجود درخواست در حال بررسی ویرایش عکس پرسنلی، امکان ثبت درخواست جدید نمی باشد. ", null, 100, "پیام", "");

        }

        private void setLog(int eventID, int modifyID, string description)
        {
            CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), eventID, description, modifyID);

        }

        private void showMessage(string msg, string title = "خطا")
        {
            rwm_Validations.RadAlert(msg, null, 100, title, "");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            int editType = Convert.ToInt32(linkButton.CommandArgument);
            var noOpenRequest = true;
            dtre = business.GetStEditRequest(Session[sessionNames.userID_StudentOstad].ToString());
            DataRow[] dr = dtre.Select("EditedID=" + editType + " and RequestLogID not in(5,7)");

            noOpenRequest = dr.Length == 0;

            if (noOpenRequest)
            {
                if (linkButton.Text == "<i class='fa fa-pencil'></i> <span>ویرایش</span>")
                {
                    linkButton.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
                    enableDisableControl(editType, true);
                }
                else
                {

                    var newValue = getNewValue(editType);
                    int reqid = 0, reqLog = 6;
                    if (!string.IsNullOrEmpty(newValue) || editType==(int)editField.عکس)
                    {
                        if (editType < 1000)
                        {
                            reqid = CartBusiness.InsertInToStudentRequest(Session[sessionNames.userID_StudentOstad].ToString(), editType, reqLog, "Null", "", 1);
                            if (reqid == 0)
                                reqid = -1;

                        }

                        if (reqid > -1)
                        {
                            switch (editType)
                            {
                                case (int)editField.عکس:
                                    business.InsertImageIntoEditePerInfo(Session[sessionNames.userID_StudentOstad].ToString(), (int)editField.عکس, (byte[])(Session["bytes"]), reqLog, reqid);
                                    setLog(10, reqid, reqid.ToString());
                                    showMessage("عکس جدید جهت بررسی توسط کارشناس ارسال شد.");
                                    break;
                                default:
                                    int editPersonalID = business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), newValue.changePersianNumberToLatinNumber(), editType, reqLog, reqid);
                                    if (editType >= 1000)
                                        business.updateStudentInfo(Session[sessionNames.userID_StudentOstad].ToString(), editPersonalID);
                                    int eventID = getEventID(editType);
                                    setLog(eventID, editPersonalID, reqid.ToString());
                                    showMessage("درخواست ویرایش شما با موفقیت ثبت گردید. ");
                                    break;
                            }
                        }

                    }
                    linkButton.Text = "";
                    LoadGrid();
                    enableDisableControl(editType, false);

                }
            }
            else
                rwm_Validations.RadAlert("بدلیل وجود درخواست در حال بررسی ویرایش " + ((editField)editType).ToString().Replace("_", " ") + "، امکان ثبت درخواست جدید نمی باشد. ", null, 100, "پیام", "");
        }

        private int getEventID(int editType)
        {

            switch (editType)
            {
                case (int)editField.تلفن:
                    return 16;
                case (int)editField.آدرس:
                    return 7;
                case (int)editField.کدپستی:
                    return 8;
                case (int)editField.موبایل:
                    return 9;
                case (int)editField.عکس:
                    return 10;
                case (int)editField.شهر:
                    return 11;
                case (int)editField.استان:
                    return 12;
                default:
                    return 57;
            }

        }


        private string getDescription(int editType, int reqID)
        {
            DataTable dtReg = business.getStudentInfoFromInitialRegistration(Session[sessionNames.userID_StudentOstad].ToString());
            string lastValue = "";
            switch (editType)
            {
                case (int)editField.تلفن:
                case (int)editField.آدرس:
                case (int)editField.کدپستی:
                case (int)editField.موبایل:
                case (int)editField.عکس:
                case (int)editField.شهر:
                case (int)editField.استان:
                    return reqID.ToString();

                case (int)editField.دین:
                    if (dt.Rows[0]["din"] != DBNull.Value)
                    {
                        lastValue = dt.Rows[0]["din"].ToString();
                    }
                    break;
                case (int)editField.مذهب:
                    if (dt.Rows[0]["ReligionBranches"] != DBNull.Value)
                    {
                        lastValue = dt.Rows[0]["ReligionBranches"].ToString();
                    }
                    break;
                case (int)editField.ملیت:
                    if (dt.Rows[0]["meliat"] != DBNull.Value)
                    {
                        lastValue = dt.Rows[0]["meliat"].ToString();
                    }
                    break;
                case (int)editField.اسکان:
                    if (dtReg.Rows[0]["bomi"] != DBNull.Value)
                    {
                        lastValue = dtReg.Rows[0]["bomi"].ToString();
                    }
                    break;
                case (int)editField.وضعیت_جسمانی:
                    if (dt.Rows[0]["jesm"] != DBNull.Value)
                    {
                        lastValue = dt.Rows[0]["jesm"].ToString();
                    }
                    break;
                case (int)editField.آشنایی_با_واحد:
                    if (dtReg.Rows[0]["IntroductionMethod"] != DBNull.Value)
                    {
                        lastValue = dtReg.Rows[0]["IntroductionMethod"].ToString();
                    }
                    break;

                case (int)editField.نوع_دانشگاه_مدرک_پایه:
                    if (dtReg.Rows[0]["UniversityType"] != DBNull.Value)
                    {
                        lastValue = dtReg.Rows[0]["UniversityType"].ToString();
                    }
                    break;

                case (int)editField.نحوه_اتصال:
                    if (dtReg.Rows[0]["ConnectionType"] != DBNull.Value)
                    {
                        lastValue = dtReg.Rows[0]["ConnectionType"].ToString();
                    }
                    break;

                case (int)editField.ارائه_دهنده_سرویس:
                    return drpServiceProvider.SelectedItem.Value;

                case (int)editField.تجهیزات_ارتباطی:
                    return chbCommunicationEquipment.SelectedItem.Value;

                case (int)editField.تاریخ_تولد:
                    return txtBirthday.Text;

                case (int)editField.استان_محل_تولد:
                    return drpBirthPlace_State.SelectedItem.Value;
                case (int)editField.شهر_محل_تولد:
                    return drpBirthPlace_City.SelectedItem.Value;

                case (int)editField.استان_محل_صدور:
                    return drpIssuePlace_State.SelectedItem.Value;
                case (int)editField.شهر_محل_صدور:
                    return drpIssuePlace_City.SelectedItem.Value;

                case (int)editField.ایمیل:
                    return txt_Email.Text;

                case (int)editField.تحصیل_همزمان:
                    return drpSyncEdu.SelectedItem.Value;

                case (int)editField.مقطع_همزمان:
                    return drpEduLevel.SelectedItem.Value;

                case (int)editField.رشته_همزمان:
                    return drpEduField.SelectedItem.Value;

                case (int)editField.نوع_دانشگاه_همزمان:
                    return drpUniType.SelectedItem.Value;

                case (int)editField.نام_دانشگاه_همزمان:
                    return drpUniName.SelectedItem.Value;

                case (int)editField.سال_ورود:
                    return txtEnterYear.Text;

                case (int)editField.وضعیت_اشتغال:
                    return drpJobstatus.SelectedItem.Value;

                case (int)editField.عنوان_شغل:
                    return txtJobTitle.Text;

                case (int)editField.نوع_همکاری:
                    return drpHireType.SelectedItem.Value;

                case (int)editField.نحوه_همکاری:
                    return drpJobContract.SelectedItem.Value;

                case (int)editField.نوع_اشتغال:
                    return drpWorkplaceType.SelectedItem.Value;

                case (int)editField.سمت:
                    return drpJobPosition.SelectedItem.Value;

                case (int)editField.تلفن_کار:
                    if (CommonBusiness.ValidatePhoneNumber(txtTel_Job.Text) == true && CommonBusiness.ValidatePreCodePhoneNumber(txtZipCode_Job.Text) == true)
                    {
                        return txtZipCode_Job.Text + txtTel_Job.Text;
                    }
                    else
                    {
                        rwm_Validations.RadAlert("لطفا شماره تلفن و پیش شماره محل کار خود را به صورت صحیح پرنمایید", null, 100, "خطا", "");
                    }
                    break;
                case (int)editField.کدپستی_کار:
                    if (CommonBusiness.ValidateZipCode(txtPostalCode_Job.Text) == true)
                    {
                        return txtPostalCode_Job.Text;
                    }
                    else
                    {
                        rwm_Validations.RadAlert("لطفا کد پستی محل کار را به صورت صحیح پرنمایید", null, 100, "خطا", "");
                    }
                    break;

                case (int)editField.استان_کار:
                    return drpState_Work.SelectedItem.Value;

                case (int)editField.شهرستان_کار:
                    return drpCity_Work.SelectedItem.Value;

                case (int)editField.ادرس_کار:
                    return txtAddress_work.Text;

                case (int)editField.وضعیت_تاهل:
                    return drpMaritalStatus.SelectedItem.Value;

                case (int)editField.نام_همسر:
                    return txtName_Spouse.Text;

                case (int)editField.نام_خانوادگی_همسر:
                    return txtLastname_spouse.Text;

                case (int)editField.اشتغال_همسر:
                    return drpJobstatus_spouse.SelectedItem.Value;

                case (int)editField.عنوان_شغل_همسر:
                    return txtJobTitle_spouse.Text;





            }
            return "مقدار قبلی : " + lastValue;

        }

        private void enableDisableControl(int controlType, bool enable)
        {
            switch (controlType)
            {
                case (int)editField.تلفن:
                    txt_Tell.Enabled = enable;
                    txt_Pishshomare.Enabled = enable;
                    rfvPish.Enabled = enable;
                    rfvTel.Enabled = enable;
                    break;
                case (int)editField.آدرس:
                    txt_Address.Enabled = enable;
                    rfvAddress.Enabled = enable;
                    break;
                case (int)editField.کدپستی:
                    txt_CodePosti.Enabled = enable;
                    rfvPostalCode.Enabled = enable;
                    break;
                case (int)editField.موبایل:
                    txt_Hamrah.Enabled = enable;
                    rfvMobile.Enabled = enable;
                    break;
                case (int)editField.عکس:
                    RadAsyncUploadimg.Visible = enable;
                    rfvPic.Enabled = enable;
                    break;
                case (int)editField.شهر:
                    ddl_Shahr.Enabled = enable;
                    rfvCity.Enabled = enable;
                    break;
                case (int)editField.استان:
                    ddl_Ostan.Enabled = enable;
                    ddl_Shahr.Enabled = enable;
                    rfvState.Enabled = enable;
                    rfvCity.Enabled = enable;
                    if (enable)
                        btnEditCity.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
                    break;
                case (int)editField.دین:
                    drpReligion.Enabled = enable;
                    break;
                case (int)editField.مذهب:
                    rfvReligionSect.Enabled = enable;
                    drpReligionSect.Enabled = enable;
                    break;
                case (int)editField.ملیت:
                    drpNationality.Enabled = enable;
                    rfvNationality.Enabled = enable;
                    break;
                case (int)editField.اسکان:
                    drpAccommodation.Enabled = enable;
                    rfvAccommodation.Enabled = enable;
                    break;
                case (int)editField.وضعیت_جسمانی:
                    ddlBodyStatus.Enabled = enable;
                    rfvBodyStatus.Enabled = enable;
                    break;

                case (int)editField.آشنایی_با_واحد:
                    drpIntroductionMethod.Enabled = enable;
                    rfvIntroductionMethod.Enabled = enable;
                    break;
                case (int)editField.نوع_دانشگاه_مدرک_پایه:
                    drpBaseEducationUniType.Enabled = enable;
                    rfvBaseEducationUniType.Enabled = enable;
                    break;
                case (int)editField.نحوه_اتصال:
                    drpConnectionType.Enabled = enable;
                    rfvConnectionType.Enabled = enable;
                    break;
                case (int)editField.ارائه_دهنده_سرویس:
                    drpServiceProvider.Enabled = enable;
                    rfverviceProvider.Enabled = enable;
                    break;
                case (int)editField.تجهیزات_ارتباطی:
                    chbCommunicationEquipment.Enabled = enable;
                    break;
                case (int)editField.تاریخ_تولد:
                    txtBirthday.Enabled = enable;
                    rfvBirthday.Enabled = enable;
                    break;
                case (int)editField.استان_محل_تولد:
                    drpBirthPlace_State.Enabled = enable;
                    rfvBirthPlace_state.Enabled = enable;
                    drpBirthPlace_City.Enabled = enable;
                    rfvBirthPlace_City.Enabled = enable;
                    //if (enable)
                    //    drpBirthPlace_City.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
                    break;
                case (int)editField.شهر_محل_تولد:
                    drpBirthPlace_City.Enabled = enable;
                    rfvBirthPlace_City.Enabled = enable;
                    break;
                case (int)editField.استان_محل_صدور:
                    drpIssuePlace_State.Enabled = enable;
                    rfvIssuePlace_State.Enabled = enable;
                    drpIssuePlace_City.Enabled = enable;
                    rfvIssuePlace_City.Enabled = enable;
                    //if (enable)
                        //btne.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
                    break;
                case (int)editField.شهر_محل_صدور:
                    drpIssuePlace_City.Enabled = enable;
                    rfvIssuePlace_City.Enabled = enable;
                    break;
                case (int)editField.ایمیل:
                    txt_Email.Enabled = enable;
                    rfvEmail.Enabled = enable;
                    break;
                case (int)editField.تحصیل_همزمان:
                    drpSyncEdu.Enabled = enable;
                    break;
                case (int)editField.مقطع_همزمان:
                    drpEduLevel.Enabled = enable;
                    rfvEduLevel.Enabled = enable;
                    break;
                case (int)editField.رشته_همزمان:
                    drpEduField.Enabled = enable;
                    rfvEduField.Enabled = enable;
                    break;
                case (int)editField.نوع_دانشگاه_همزمان:
                    drpUniType.Enabled = enable;
                    rfvUniType.Enabled = enable;
                    break;
                case (int)editField.نام_دانشگاه_همزمان:
                    drpUniName.Enabled = enable;
                    rfvUniName.Enabled = enable;
                    break;
                case (int)editField.سال_ورود:
                    txtEnterYear.Enabled = enable;
                    rfvEnterYear.Enabled = enable;
                    break;
                case (int)editField.وضعیت_اشتغال:
                    drpJobstatus.Enabled = enable;
                    break;
                case (int)editField.عنوان_شغل:
                    txtJobTitle.Enabled = enable;
                    rfvJobTitle.Enabled = enable;
                    break;
                case (int)editField.نوع_همکاری:
                    drpHireType.Enabled = enable;
                    rfvHireType.Enabled = enable;
                    break;
                case (int)editField.نحوه_همکاری:
                    drpJobContract.Enabled = enable;
                    rfvJobContract.Enabled = enable;
                    break;
                case (int)editField.نوع_اشتغال:
                    drpWorkplaceType.Enabled = enable;
                    rfvWorkplaceType.Enabled = enable;
                    break;
                case (int)editField.سمت:
                    rfvJobPosition.Enabled = enable;
                    drpJobPosition.Enabled = enable;
                    break;
                case (int)editField.تلفن_کار:
                    rfvZipCode_Job.Enabled = enable;
                    rfvTel_Job.Enabled = enable;
                    txtTel_Job.Enabled = enable;
                    txtZipCode_Job.Enabled = enable;
                    break;
                case (int)editField.کدپستی_کار:
                    txtPostalCode_Job.Enabled = enable;
                    rfvPostalCode_Job.Enabled = enable;
                    break;
                case (int)editField.استان_کار:
                    drpState_Work.Enabled = enable;
                    rfvState_Work.Enabled = enable;
                    drpCity_Work.Enabled = enable;
                    rfvCity_Work.Enabled = enable;
                    if (enable)
                    {
                        btnCity_Work.Text = "<i class='fa fa-save'></i> <span>ذخیره</span>";
                    }
                    break;
                case (int)editField.شهرستان_کار:
                    drpCity_Work.Enabled = enable;
                    rfvCity_Work.Enabled = enable;
                    break;
                case (int)editField.ادرس_کار:
                    rfvAddress_work.Enabled = enable;
                    txtAddress_work.Enabled = enable;
                    break;
                case (int)editField.وضعیت_تاهل:
                    drpMaritalStatus.Enabled = enable;
                    break;
                case (int)editField.نام_همسر:
                    txtName_Spouse.Enabled = enable;
                    rfvName_Spouse.Enabled = enable;
                    break;
                case (int)editField.نام_خانوادگی_همسر:
                    rfvLastname_spouse.Enabled = enable;
                    txtLastname_spouse.Enabled = enable;
                    break;
                case (int)editField.اشتغال_همسر:
                    drpJobstatus_spouse.Enabled = enable;
                    rfvJobstatus_spouse.Enabled = enable;
                    break;
                case (int)editField.عنوان_شغل_همسر:
                    txtJobTitle_spouse.Enabled = enable;
                    rfvJobTitle_spouse.Enabled = enable;
                    break;
                    //case (int)editField:
                    //        .Enabled = enable;
                    //        .Enabled = enable;
                    //    break;
                    //case (int)editField:
                    //        .Enabled = enable;
                    //        .Enabled = enable;
                    //    break;
                    //case (int)editField:
                    //        .Enabled = enable;
                    //        .Enabled = enable;
                    //    break;
                    //case (int)editField:
                    //        .Enabled = enable;
                    //        .Enabled = enable;
                    //    break;
                    //case (int)editField:
                    //        .Enabled = enable;
                    //        .Enabled = enable;
                    //    break;

            }
        }

        private string getNewValue(int editType)
        {
            switch (editType)
            {
                case (int)editField.تلفن:
                    if (CommonBusiness.ValidatePhoneNumber(txt_Tell.Text) == true && CommonBusiness.ValidatePreCodePhoneNumber(txt_Pishshomare.Text) == true)
                    {
                        return txt_Pishshomare.Text + txt_Tell.Text;
                    }
                    else
                    {
                        showMessage("لطفا شماره تلفن و پیش شماره را به صورت صحیح پرنمایید");
                    }
                    break;
                case (int)editField.آدرس:
                    return txt_Address.Text;
                case (int)editField.کدپستی:
                    if (CommonBusiness.ValidateZipCode(txt_CodePosti.Text) == true)
                    {
                        return txt_CodePosti.Text.Trim();
                    }
                    else
                    {
                        showMessage("لطفا کد پستی را به صورت صحیح پرنمایید");
                    }
                    break;

                case (int)editField.موبایل:
                    if (CommonBusiness.ValidateMobile(txt_Hamrah.Text) == true)
                    {
                        return txt_Hamrah.Text.Trim();
                    }
                    else
                    {
                        rwm_Validations.RadAlert("لطفا شماره همراه را به صورت صحیح وارد نمایید", null, 100, "خطا", "");
                        //yield return null;
                    }
                    break;
                case (int)editField.شهر:
                    int cityId;
                    if (int.TryParse(ddl_Shahr.SelectedItem.Value, out cityId))
                    {
                        return cityId.ToString();
                    }
                    else
                    {
                        //yield return null;
                    }
                    break;
                case (int)editField.استان:
                    int stateId;
                    if (int.TryParse(ddl_Ostan.SelectedItem.Value, out stateId))
                    {
                        return stateId.ToString();
                    }
                    else
                    {
                        //yield return null;
                    }
                    break;

                case (int)editField.دین:
                    return drpReligion.SelectedItem.Value;
                case (int)editField.مذهب:
                    return drpReligionSect.SelectedItem.Value;

                case (int)editField.ملیت:
                    return drpNationality.SelectedItem.Value;

                case (int)editField.اسکان:
                    return drpAccommodation.SelectedItem.Value;

                case (int)editField.وضعیت_جسمانی:
                    return ddlBodyStatus.SelectedItem.Value;

                case (int)editField.آشنایی_با_واحد:
                    return drpIntroductionMethod.SelectedItem.Value;

                case (int)editField.نوع_دانشگاه_مدرک_پایه:
                    return drpBaseEducationUniType.SelectedItem.Value;

                case (int)editField.نحوه_اتصال:
                    return drpConnectionType.SelectedItem.Value;

                case (int)editField.ارائه_دهنده_سرویس:
                    return drpServiceProvider.SelectedItem.Value;

                case (int)editField.تجهیزات_ارتباطی:
                    return string.Join(",", chbCommunicationEquipment.Items.Cast<ListItem>().Where(w => w.Selected).Select(s=> s.Value));

                case (int)editField.تاریخ_تولد:
                    var birthDateText = txtBirthday.Text.Trim();
                    if (Regex.IsMatch(birthDateText, @"^$|^([1۱][۰-۹ 0-9]{3}[/\/]([0 ۰][۱-۶ 1-6])[/\/]([0 ۰][۱-۹ 1-9]|[۱۲12][۰-۹ 0-9]|[3۳][01۰۱])|[1۱][۰-۹ 0-9]{3}[/\/]([۰0][۷-۹ 7-9]|[1۱][۰۱۲012])[/\/]([۰0][1-9 ۱-۹]|[12۱۲][0-9 ۰-۹]|(30|۳۰)))$"))
                        return txtBirthday.Text.Trim().Substring(2);
                    else
                        rwm_Validations.RadAlert("تاریخ وارد شده صحیح نیست (نمونه: 1399/06/23)", null, 100, "خطا", "");
                    return "";

                case (int)editField.استان_محل_تولد:
                    int birthStateId;
                    if (int.TryParse(drpBirthPlace_State.SelectedItem.Value, out birthStateId))
                        return birthStateId.ToString();
                    else
                        return "";
                case (int)editField.شهر_محل_تولد:
                    int birthCityId;
                    if (int.TryParse(drpBirthPlace_City.SelectedItem.Value, out birthCityId))
                        return birthCityId.ToString();
                    else
                        return "";

                case (int)editField.استان_محل_صدور:
                    int issueStateId;
                    if (int.TryParse(drpIssuePlace_State.SelectedItem.Value, out issueStateId))
                        return issueStateId.ToString();
                    else
                        return "";
                case (int)editField.شهر_محل_صدور:
                    int issueCityId;
                    if (int.TryParse(drpIssuePlace_City.SelectedItem.Value, out issueCityId))
                        return issueCityId.ToString();
                    else
                        return "";

                case (int)editField.ایمیل:
                    return txt_Email.Text;

                case (int)editField.تحصیل_همزمان:
                    return drpSyncEdu.SelectedItem.Value;

                case (int)editField.مقطع_همزمان:
                    return drpEduLevel.SelectedItem.Value;

                case (int)editField.رشته_همزمان:
                    int EduField;
                    if (int.TryParse(drpEduField.SelectedItem.Value, out EduField))
                        return EduField.ToString();
                    else
                        return "";

                case (int)editField.نوع_دانشگاه_همزمان:
                    return drpUniType.SelectedItem.Value;

                case (int)editField.نام_دانشگاه_همزمان:
                    return drpUniName.SelectedItem.Value;

                case (int)editField.سال_ورود:
                    return txtEnterYear.Text.Trim();

                case (int)editField.وضعیت_اشتغال:
                    return drpJobstatus.SelectedItem.Value;

                case (int)editField.عنوان_شغل:
                    return txtJobTitle.Text.Trim();

                case (int)editField.نوع_همکاری:
                    return drpHireType.SelectedItem.Value;

                case (int)editField.نحوه_همکاری:
                    return drpJobContract.SelectedItem.Value;

                case (int)editField.نوع_اشتغال:
                    return drpWorkplaceType.SelectedItem.Value;

                case (int)editField.سمت:
                    return drpJobPosition.SelectedItem.Value;

                case (int)editField.تلفن_کار:
                    if (CommonBusiness.ValidatePhoneNumber(txtTel_Job.Text) == true && CommonBusiness.ValidatePreCodePhoneNumber(txtZipCode_Job.Text) == true)
                    {
                        return txtZipCode_Job.Text + txtTel_Job.Text;
                    }
                    else
                    {
                        rwm_Validations.RadAlert("لطفا شماره تلفن و پیش شماره محل کار خود را به صورت صحیح پرنمایید", null, 100, "خطا", "");
                    }
                    break;
                case (int)editField.کدپستی_کار:
                    if (CommonBusiness.ValidateZipCode(txtPostalCode_Job.Text) == true)
                    {
                        return txtPostalCode_Job.Text;
                    }
                    else
                    {
                        rwm_Validations.RadAlert("لطفا کد پستی محل کار را به صورت صحیح پرنمایید", null, 100, "خطا", "");
                    }
                    break;

                case (int)editField.استان_کار:
                    return drpState_Work.SelectedItem.Value;

                case (int)editField.شهرستان_کار:
                    return drpCity_Work.SelectedItem.Value;

                case (int)editField.ادرس_کار:
                    return txtAddress_work.Text;

                case (int)editField.وضعیت_تاهل:
                    return drpMaritalStatus.SelectedItem.Value;

                case (int)editField.نام_همسر:
                    return txtName_Spouse.Text;

                case (int)editField.نام_خانوادگی_همسر:
                    return txtLastname_spouse.Text;

                case (int)editField.اشتغال_همسر:
                    return drpJobstatus_spouse.SelectedItem.Value;

                case (int)editField.عنوان_شغل_همسر:
                    return txtJobTitle_spouse.Text;




            }
            return null;
        }

        enum editField
        {
            تلفن = 6,
            آدرس = 7,
            کدپستی = 8,
            موبایل = 9,
            عکس = 10,
            استان = 12,
            شهر = 11,
            دین = 1001,
            مذهب = 1002,
            ملیت = 1003,
            اسکان = 1004,
            وضعیت_جسمانی = 1005,
            آشنایی_با_واحد = 1006,
            نوع_دانشگاه_مدرک_پایه = 1007,
            نحوه_اتصال = 1008,
            ارائه_دهنده_سرویس = 1009,
            تجهیزات_ارتباطی = 1010,
            تاریخ_تولد = 1011,
            استان_محل_تولد = 1012,
            استان_محل_صدور = 1013,
            ایمیل = 1014,
            شهر_محل_تولد = 1015,
            شهر_محل_صدور = 1016,
            تحصیل_همزمان = 1100,
            مقطع_همزمان = 1101,
            رشته_همزمان = 1102,
            نوع_دانشگاه_همزمان = 1103,
            نام_دانشگاه_همزمان = 1104,
            سال_ورود = 1105,
            وضعیت_اشتغال = 1200,
            عنوان_شغل = 1201,
            نوع_همکاری = 1202,
            نحوه_همکاری = 1203,
            نوع_اشتغال = 1204,
            سمت = 1205,
            تلفن_کار = 1206,
            کدپستی_کار = 1207,
            استان_کار = 1208,
            شهرستان_کار = 1209,
            ادرس_کار = 1210,
            وضعیت_تاهل = 1300,
            نام_همسر = 1301,
            نام_خانوادگی_همسر = 1302,
            اشتغال_همسر = 1303,
            عنوان_شغل_همسر = 1304,
            نام_فرزند = 1305,
            نام_خانوادگی_فرزند = 1306,
            جنسیت_فرزند = 1307,
            تاریخ_تولد_فرزند = 1308,

        }

        protected void drpJobstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvEmploy.Visible = drpJobstatus.SelectedItem.Value == "1";
        }

        protected void drpJobstatus_spouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvJobTitle_spouse_Text.Visible = drpJobstatus_spouse.SelectedItem.Value == "1";
            dvJobTitle_spouse.Visible = drpJobstatus_spouse.SelectedItem.Value == "1";
        }

        protected void drpSyncEdu_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvSyncEdu.Visible = drpSyncEdu.SelectedItem.Value == "1";
        }

        protected void drpReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckReligionSectIsAvailable();
        }

        protected void drpMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvMaritalStatus.Visible = drpMaritalStatus.SelectedItem.Value == "1";
        }

        protected void drpConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            setServiceProviderSource();
        }

        private void setServiceProviderSource()
        {
            drpServiceProvider.Items.Clear();
            drpServiceProvider.DataTextField = "Value";
            drpServiceProvider.DataValueField = "Key";
            switch (drpConnectionType.SelectedItem.Value)
            {
                case "-1":
                case "3":
                case "4":
                case "5":
                    drpServiceProvider.Enabled = false;
                    break;
                case "1":
                    drpServiceProvider.DataSource = GetGSMProviders();
                    drpServiceProvider.DataBind();
                    if(IsPostBack)
                        drpServiceProvider.Enabled = true;
                    break;
                case "2":
                    drpServiceProvider.DataSource = GetADSLProviders();
                    drpServiceProvider.DataBind();
                    if (IsPostBack)
                        drpServiceProvider.Enabled = true;
                    break;
            }
            drpServiceProvider.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
        }

        private Dictionary<string, string> GetADSLProviders()
        {
            var res = new Dictionary<string, string>();
            res.Add("1", "مخابرات ایران");
            res.Add("2", "پارس آنلاین");
            res.Add("3", "شاتل");
            res.Add("4", "آسیاتک");
            res.Add("5", "های وب");
            res.Add("6", "پیشگامان");
            res.Add("7", "صبانت");
            res.Add("8", "فن آوا");
            res.Add("9", "سایر");
            return res;
        }
        private Dictionary<string, string> GetGSMProviders()
        {
            var res = new Dictionary<string, string>();
            res.Add("1", "همراه اول");
            res.Add("2", "ایرانسل");
            res.Add("3", "رایتل");
            res.Add("4", "تالیا");
            return res;
        }

        protected void drpState_Work_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpCity_Work.DataSource = CommonBusiness.getShahrestan(Convert.ToInt32(drpState_Work.SelectedValue));
            drpCity_Work.DataBind();
        }

        protected void rgvChildren_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (childrenList.Count == 0)
                childrenList.Add(new StudentsChild());
            foreach (var item in childrenList)
                if (item.BirthDate > DateTime.MinValue)
                    item.ShamsiBirthDate = pc.GetYear(item.BirthDate) + "/" + pc.GetMonth(item.BirthDate) + "/" + pc.GetDayOfMonth(item.BirthDate);
            rgvChildren.DataSource = childrenList;
        }

        protected void rgvChildren_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            var ddlGender = (DropDownList)e.Item.FindControl("ddlGender");
            var dataItem = ((StudentsChild)e.Item.DataItem);
            if (ddlGender != null && !string.IsNullOrEmpty(dataItem.FirstName))
                ddlGender.SelectedValue = dataItem.Gender ? "1" : "2";
        }

        private List<StudentsChild> getStudentChildren(DataTable dtReg)
        {
            List<StudentsChild> lst = new List<StudentsChild>();
            for (int i = 0; i < dtReg.Rows.Count; i++)
            {
                if (dtReg.Rows[i]["id"] != DBNull.Value)
                {
                    lst.Add(new StudentsChild
                    {
                        BirthDate = Convert.ToDateTime(dtReg.Rows[i]["BirthDate"]),
                        FirstName = dtReg.Rows[i]["FirstName"].ToString(),
                        LastName = dtReg.Rows[i]["LastName"].ToString(),
                        Gender = Convert.ToBoolean(dtReg.Rows[i]["Gender"]),
                        Id = Convert.ToDecimal(dtReg.Rows[i]["id"]),
                        ShamsiBirthDate = dtReg.Rows[i]["ShamsiBirthDate"].ToString(),
                        stcode = dtReg.Rows[i]["stcode"].ToString(),

                    });
                }
            }
            return lst;
        }

        protected void btnAddChild_Click(object sender, EventArgs e)
        {
            childrenList = new List<StudentsChild>();
            foreach (GridDataItem item in rgvChildren.Items)
            {
                var txtFirstName = (TextBox)item.FindControl("txtFirstName");
                var txtLastName = (TextBox)item.FindControl("txtLastName");
                var ddlGender = (DropDownList)item.FindControl("ddlGender");
                var txtBirthDate = (TextBox)item.FindControl("txtBirthDate");
                if (txtFirstName != null && txtLastName != null && ddlGender != null && txtBirthDate != null)
                {
                    var gregorianBirthDate = new DateTime();
                    if (!string.IsNullOrEmpty(txtFirstName.Text) && !string.IsNullOrEmpty(txtLastName.Text) && !string.IsNullOrEmpty(txtBirthDate.Text)
                        && ddlGender.SelectedIndex > 0 && TryParsShamsiDate(txtBirthDate.Text, out gregorianBirthDate))
                    {
                        childrenList.Add(new StudentsChild
                        {
                            BirthDate = gregorianBirthDate,
                            FirstName = txtFirstName.Text.Trim(),
                            Gender = ddlGender.SelectedItem.Value == "1" ? true : false,
                            LastName = txtLastName.Text.Trim(),
                            ShamsiBirthDate = txtBirthDate.Text.Trim(),
                            stcode = Session[sessionNames.appID_StudentOstad].ToString()
                        });
                    }
                    else
                    {
                        lblErr.Visible = true;
                        return;
                    }
                }
            }
            lblErr.Visible = false;
            childrenList.Add(new StudentsChild());
            rgvChildren.DataSource = childrenList;
            rgvChildren.DataBind();
        }
        private bool TryParsShamsiDate(string shamsiDate, out DateTime gregorianDate)
        {
            gregorianDate = new DateTime();
            try
            {
                var parts = shamsiDate.Split('/');
                if (parts.Length == 3)
                {
                    var year = Convert.ToInt32(parts[0]);
                    var month = Convert.ToInt32(parts[1]);
                    var day = Convert.ToInt32(parts[2]);
                    if (year < 1300)
                        year += 1300;
                    gregorianDate = pc.ToDateTime(year, month, day, 0, 0, 0, 0);
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }

        protected void rgvChildren_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "addEditChild_Click":
                    int id = Convert.ToInt32(e.CommandArgument);

                    var txtName = (TextBox)e.Item.FindControl("txtFirstName");
                    var txtfamily = (TextBox)e.Item.FindControl("txtLastName");
                    var gender = (DropDownList)e.Item.FindControl("ddlGender");
                    var birthday = (TextBox)e.Item.FindControl("txtBirthDate");
                    var birthDate = new DateTime();
                    if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtfamily.Text) && gender.SelectedItem.Value != "-1" && !string.IsNullOrEmpty(birthday.Text) && TryParsShamsiDate(birthday.Text, out birthDate))
                    {
                        int editPersonalID_Name = business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), txtName.Text, (int)editField.نام_فرزند, 6, 0);
                        int eventID = getEventID((int)editField.نام_فرزند);
                        setLog(eventID, editPersonalID_Name, "0");


                        int editPersonalID_family = business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), txtfamily.Text, (int)editField.نام_خانوادگی_فرزند, 6, 0);
                        eventID = getEventID((int)editField.نام_خانوادگی_فرزند);
                        setLog(eventID, editPersonalID_family, "0");


                        int editPersonalID_gender = business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), gender.SelectedItem.Value == "2" ? "0" : "1", (int)editField.جنسیت_فرزند, 6, 0);
                        eventID = getEventID((int)editField.جنسیت_فرزند);
                        setLog(eventID, editPersonalID_gender, "0");

                        int editPersonalID_birthday = business.InsertIntoEditPersonalInf(Session[sessionNames.userID_StudentOstad].ToString(), birthday.Text, (int)editField.تاریخ_تولد_فرزند, 6, 0);
                        eventID = getEventID((int)editField.تاریخ_تولد_فرزند);
                        setLog(eventID, editPersonalID_birthday, "0");

                        if (id > 0)
                        {
                            business.updateStudentChild(Session[sessionNames.userID_StudentOstad].ToString(), editPersonalID_Name, id);
                            business.updateStudentChild(Session[sessionNames.userID_StudentOstad].ToString(), editPersonalID_family, id);
                            business.updateStudentChild(Session[sessionNames.userID_StudentOstad].ToString(), editPersonalID_gender, id);
                            business.updateStudentChild(Session[sessionNames.userID_StudentOstad].ToString(), editPersonalID_birthday, id);
                            showMessage("درخواست ویرایش شما با موفقیت ثبت گردید. ");

                        }
                        else
                        {
                            business.insertStudentChild(Session[sessionNames.userID_StudentOstad].ToString(), editPersonalID_Name, editPersonalID_family, editPersonalID_gender, editPersonalID_birthday);
                            showMessage("درخواست ویرایش شما با موفقیت ثبت گردید. ");

                        }
                    }
                    else if(string.IsNullOrEmpty(txtName.Text))
                        showMessage("لطفاً نام را وارد نمایید.");
                    else if (string.IsNullOrEmpty(txtfamily.Text))
                        showMessage("لطفاً نام خانوادگی را وارد نمایید.");
                    else if (gender.SelectedItem.Value == "-1")
                        showMessage("لطفاً جنسیت را انتخاب نمایید.");
                    else if (string.IsNullOrEmpty(birthday.Text))
                        showMessage("لطفاً تاریخ تولد را وارد نمایید.");
                    else if (birthDate == DateTime.MinValue)
                        showMessage("لطفاً تاریخ تولد را بصورت صحیح وارد نمایید. (مثال: 1399/09/29)");
                    break;
            }
        }

        protected void drpIssuePlace_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtshahr = CommonBusiness.getCitiesFromTblShahrestan(int.Parse(drpIssuePlace_State.SelectedValue.ToString()));
            drpIssuePlace_City.DataSource = dtshahr;
            drpIssuePlace_City.DataBind();
            drpIssuePlace_City.Items.Insert(0, "انتخاب نمایید..");
        }

        protected void drpBirthPlace_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtshahr = CommonBusiness.getCitiesFromTblShahrestan(int.Parse(drpBirthPlace_State.SelectedValue.ToString()));
            drpBirthPlace_City.DataSource = dtshahr;
            drpBirthPlace_City.DataBind();
            drpBirthPlace_City.Items.Insert(0, "انتخاب نمایید..");
        }
        private void CheckReligionSectIsAvailable()
        {
            drpReligionSect.Visible = drpReligion.SelectedItem.Value == "1";
            pnlReligionSect.Visible = drpReligion.SelectedItem.Value == "1";
        }
    }
    public class StudentsChild
    {
        public decimal Id { get; set; }
        public string stcode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ShamsiBirthDate { get; set; }
        public bool Gender { get; set; }
    }
}