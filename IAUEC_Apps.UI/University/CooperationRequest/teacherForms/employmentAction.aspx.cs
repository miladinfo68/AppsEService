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

namespace IAUEC_Apps.UI.University.CooperationRequest.teacherForms
{
    public partial class employmentAction : System.Web.UI.Page
    {
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        ProfessorRequestBusiness ProfReqBuss = new ProfessorRequestBusiness();
        CommonBusiness CB = new CommonBusiness();
        List<ChangedInfoDTO> ChangeList = new List<ChangedInfoDTO>();
        string customError = string.Empty;
        public bool ShowView = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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

        public static bool saveHokm()
        {
            //var m = drpMartabe.SelectedItem.Value;
            return true;
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
                //pnlSabeghe.Visible = true;

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
                //pnlSabeghe.Visible = true;
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
            //btnSubmitChanges.Visible = rblIsHeiat.SelectedItem.Value == "1";
            //btnSubmitChangesNo.Visible = rblIsHeiat.SelectedItem.Value != "1";
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
                //RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
                return false;
            }
            if (!txtDateEjraHokm.Text.isPersianDateCorrect())
            {
                msg = string.Format("تاریخ اجرای حکم اشتباه وارد شده است. لطفا آن را اصلاح فرمایید.");
                //RadWindowManager1.RadAlert(msg, 400, 100, "پیام سیستم", "");
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

    }
}