using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.DTO.University.Graduate;
using IAUEC_Apps.Business.university.GraduateAffair;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class showGraduateDocument : System.Web.UI.Page
    {
        GraduateFormsBusiness GFB = new GraduateFormsBusiness();
        Business.university.Request.CheckOutNaghsBusiness naghs = new Business.university.Request.CheckOutNaghsBusiness();
        Business.university.Request.CheckOutRequestBusiness bsn = new Business.university.Request.CheckOutRequestBusiness();
        string stampMsg = "", paymentReceiptMsg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["stcode_GraduateDoc"] != null)
                {
                    txtSearchStcode.Text = Session["stcode_GraduateDoc"].ToString();
                    searchStudent();
                    getStudentInformation(Session["stcode_GraduateDoc"].ToString());
                }
                Session["stcode_GraduateDoc"] = null;
            }
        }

        private void getStudentInformation(string stcode)
        {
            StudentFeraghatDocument SFD = new StudentFeraghatDocument();
            DataTable dtTemp = new DataTable();
            SFD = GFB.getStudentFeraghatDocument(stcode);
            if (SFD.stcode != null)
            {
                lnkBtnEnterDate.Visible = !SFD.isOnline;
                lblNotFound.Visible = false;
                dvShowReport.Visible = true;
                setComponentsValue(SFD);
            }
            else
            {
                lblNotFound.Visible = true;
                dvShowReport.Visible = false;
            }
        }

        private void searchStudent()
        {
            DataTable dtTemp = new DataTable();
            string stcode, Family;
            btnAddStudentAsGraduate.Visible = false;
            if (!Business.Common.CommonBusiness.IsNumeric(txtSearchStcode.Text) && txtSearchStcode.Text != "")
                return;
            GraduateFormsDTO GFD = new GraduateFormsDTO();
            if (txtSearchStcode.Text.Trim() != "" && txtSearchFamnily.Text.Trim() == "")
            {
                stcode = txtSearchStcode.Text.Trim();
                Family = string.Empty;
            }
            else if (txtSearchStcode.Text.Trim() == "" && txtSearchFamnily.Text.Trim() != "")
            {
                stcode = string.Empty;
                Family = txtSearchFamnily.Text.Trim();
            }
            else
            {
                stcode = txtSearchStcode.Text.Trim();
                Family = txtSearchFamnily.Text.Trim();
            }
            DataTable dt = new DataTable();

            //to bind database with data gridview            
            dt = GFB.searchStudentInfo_FeraghatDocument(stcode, Family);

            dtTemp = dt.Copy();
            dtTemp.Columns.RemoveAt(7);
            grdResults.DataSource = dtTemp;
            grdResults.DataBind();
            lblNotFound.Visible = dtTemp.Rows.Count == 0;
            grdResults.Visible = dtTemp.Rows.Count > 0;
            if (lblNotFound.Visible)
                CanAddStudentAsGraduate(stcode);
        }

        private void CanAddStudentAsGraduate(string stcode)
        {
            var hasRequest = GFB.getStudentRequestes(stcode);
            bool canAdd = false;
            if (hasRequest.Rows.Count == 0)
                canAdd = false;
            else
            {
                if (hasRequest.Rows[0]["idvazkol"].ToString() == "7")
                {
                    DataRow[] dr = hasRequest.Select("RequestTypeID=15  and RequestLogID<>5");
                    canAdd = dr.Length == 0;
                }
                else
                {
                    canAdd = false;
                }
                if (canAdd)
                {
                    int isbachelor = bsn.GetIsBachelor(stcode);
                    if (bsn.hasPassedCoursesToSubmitGraduateRequest(stcode, isbachelor))
                    {
                        Business.university.Request.CheckOutPajooheshBusiness pbsn = new Business.university.Request.CheckOutPajooheshBusiness();
                        canAdd = pbsn.IsFinilized(stcode);
                    }
                }
            }
            if (canAdd)
            {

                btnAddStudentAsGraduate.Visible = true;
                ViewState["stcode_ADD"] = stcode;
            }
        }

        private void addStudentToStudentRequest(string stcode)
        {

            DTO.University.Request.FeraghatTahsilDTO oFeraghat = new DTO.University.Request.FeraghatTahsilDTO();
            var bFeraghat = new Business.university.Request.FeraghatTahsilBusiness();

            string CreateDate = DateTime.Now.ToPeString(), note = "ثبت درخواست فراغت از تحصیل";
            var reqID = bsn.InsertInToStudentRequest(stcode, 15, Convert.ToInt32(DTO.University.Request.CheckOutStatusEnum.FareghReqStatus.end), Convert.ToInt32(DTO.University.Request.CheckOutStatusEnum.FareghReqStatus.end).ToString(), "", CreateDate, note, 0,  true);
            if (reqID > 0)
            {
                AddVahedSodur(stcode, reqID);

                oFeraghat.Id = 0;
                oFeraghat.RizNomarat = 0;
                oFeraghat.GovahiMovaghat = 0;
                oFeraghat.DaneshNameh = 0;
                oFeraghat.Stcode = stcode;
                oFeraghat.StudentRequestId = reqID;
                var SFD = GFB.getStudentFeraghatDocument(stcode);
                SFD.HasStamp = true;
                SFD.HasPaymentReceipt = true;
                GFB.UpdateFeraghatTahsil_GraduateDocument(SFD);
                bFeraghat.UpdateMadarekStatus(oFeraghat, Convert.ToInt32(Session[sessionNames.userID_Karbar]), true, false);
                setLog("ثبت تسویه حساب فارغ التحصیلی از صفحه مشاهده پرونده فارغ التحصیلان", reqID, (int)DTO.eventEnum.ثبت_درخواست_تسویه);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeVahedSodur();", true);
            }
        }

        private bool AddVahedSodur(string stcode, int reqID)
        {
            var result = bsn.ApproveCheckOutRequestByCurrentStatus(Session[sessionNames.userID_Karbar].ToString(), Convert.ToInt32(DTO.University.Request.CheckOutStatusEnum.FareghReqStatus.end), reqID, 15, false, Convert.ToInt32(drpMahaleSodooreMadrak.SelectedItem.Value));
            //confirmMessage.
            return true;
        }

        private void setComponentsValue(StudentFeraghatDocument SFD)
        {
            txtBaygani.Visible = true;
            txtNewBaygani.Visible = false;
            btnCreateArchiveCode.Visible = false;
            var disableColor = ddlGender.BackColor;
            var enableColor = System.Drawing.Color.White;
            picStudent.ImageUrl = "~University/CooperationRequest/Image/noPic.png";
            txtStcode.Text = SFD.stcode;
            txtRequestLog.Text = SFD.RequestStatus_String;
            txtBaygani.Text = SFD.ArchivesCode;
            if (txtBaygani.Text == "")
            {
                txtBaygani.Visible = false;
                txtNewBaygani.Visible = true;
                btnCreateArchiveCode.Visible = true;
            }
            //List<DTO.University.Request.MahaleSodoor> dtVahed = bsn.GetListOfVahed();
            //dvSerialAndDocNum.Visible = SFD.vahedSodur == dtVahed.Select((x)=>new {x.Vahed ,x.Id = 1 });
            txtDateSendPackToVahedSodur.Text = SFD.dateSendPack;
            txtDateSodurDanesh.Text = SFD.dateDaneshnameSodur;
            txtDateTahvilDanesh.Text = SFD.dateDaneshnameTahvil;
            txtDateVorudDanesh.Text = SFD.dateDaneshnameVorud;
            txtDateEnterStudentDep.Text = SFD.DateEnterStudentDep;
            lblCountEnterStudentDep.InnerText = SFD.countEnterStudentDep > 1 ? string.Format("{0} {1}", SFD.countEnterStudentDep, "بار") : "";
            txtDateSodurGovahi.Text = SFD.dateGovahiSodur;
            txtDateTahvilGovahi.Text = SFD.dateGovahiTahvil;
            txtDateVorudGovahi.Text = SFD.dateGovahiVorud;
            txtDateRejToDep.Text = SFD.DateRejToDep;
            txtDateErsalRiznomre.Text = SFD.dateRiznomreErsal;
            txtDateSodurRiznomre.Text = SFD.dateRiznomreSodur;
            txtDateTahvilRiznomre.Text = SFD.dateRiznomreTahvil;
            txtDateVorudRiznomre.Text = SFD.dateRiznomreVorud;
            txtFamily.Text = SFD.family;
            ddlGender.SelectedValue = SFD.genderValue.ToString();
            txtGerayesh.Text = SFD.gerayesh;
            cbxFish.Checked = !SFD.HasPaymentReceipt; cbxFish.Enabled = SFD.RequestStatus <= 23;
            cbxTambr.Checked = !SFD.HasStamp; cbxTambr.Enabled = SFD.RequestStatus <= 23;
            txtIDDMeli.Text = SFD.idd_meli;
            txtLastTerm.Text = SFD.lastTerm;
            txtMaghta.Text = SFD.maghta;
            cbxMashmul.Checked = SFD.mashmul; cbxMashmul.Enabled = (!SFD.isOnline && SFD.genderValue == 1 && SFD.RequestStatus <= 23);
            txtMavaredKhas.Text = SFD.SpecialTips;
            txtPhone.Text = SFD.mobile;
            txtName.Text = SFD.name;
            cbxPaymentToVahedSodur.Checked = SFD.PaiedToVahedSodoor;
            txtFatherName.Text = SFD.pName;
            txtReshte.Text = SFD.reshte;
            txtResRejToDep.Text = SFD.ResultRejToDep;
            txtNameVahedSodur.Text = SFD.vahedSodur;
            cbxVamdar.Checked = SFD.vamdar;
            txtDocArchiveCode.Text = SFD.docArchiveID;
            picStudent.DataValue = SFD.StudentImage;
            txtArchiveCode_Govahi.Text = SFD.archiveID_Movaghat.ToString();
            txtArchiveCode_Daneshname.Text = SFD.archiveID_Daneshname.ToString();
            txtArchiveCode_Riznomre.Text = SFD.archiveID_Riznomre.ToString();
            txtSerialDaneshname.Text = SFD.serialNumber_Daneshname;
            txtSerialMovaghat.Text = SFD.serialNumber_Movaghat;
            txtDocNumDaneshname.Text = SFD.documentNumber_Daneshname;
            txtDocNumMovaghat.Text = SFD.documentNumber_Movaghat;
            cbxFish.BackColor = cbxFish.Enabled ? enableColor : disableColor;
            cbxTambr.BackColor = cbxTambr.Enabled ? enableColor : disableColor;
            cbxMashmul.BackColor = cbxMashmul.Enabled ? enableColor : disableColor;
        }

        protected void btnSearchStudent_Click(object sender, EventArgs e)
        {
            searchStudent();
            setComponentsValue(new StudentFeraghatDocument());
            dvShowReport.Visible = false;
        }

        protected void btnRegisterChanges_Click(object sender, EventArgs e)
        {
            //confirmMessage.Text = "آیا از تایید تغییرات انجام شده اطمینان دارید";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void rbConfirmed_RegisterChanges_Click(object sender, EventArgs e)
        {
            bool sentSMS; string smsStatus;
            StudentFeraghatDocument SFD = new StudentFeraghatDocument();
            var s = GFB.getStudentFeraghatDocument(txtStcode.Text);
            bool mashmulChanged = false;

            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            DataTable dtMsg = CB.GetAppIDMessage(0, 12, 1, 1);
            if (dtMsg.Rows.Count > 0)
            {
                paymentReceiptMsg = dtMsg.Rows[0]["text"].ToString();
                stampMsg = paymentReceiptMsg;
            }

            int requestID = s.RequestID;
            SFD.stcode = txtStcode.Text;
            SFD.HasStamp = !cbxTambr.Checked;
            SFD.HasPaymentReceipt = !cbxFish.Checked;
            SFD.mashmul = cbxMashmul.Checked;
            SFD.SpecialTips = txtMavaredKhas.Text;
            SFD.RequestID = requestID;
            SFD.serialNumber_Daneshname = txtSerialDaneshname.Text.Trim();
            SFD.serialNumber_Movaghat = txtSerialMovaghat.Text.Trim();
            SFD.documentNumber_Daneshname = txtDocNumDaneshname.Text.Trim();
            SFD.documentNumber_Movaghat = txtDocNumMovaghat.Text.Trim();
            if (!s.isOnline)
            {
                if (s.mashmul != SFD.mashmul)
                    mashmulChanged = true;
            }
            string description = getChangeset(SFD);
            int archiveID = 0;
            if (GFB.UpdateFeraghatTahsil_GraduateDocument(SFD, mashmulChanged))
            {
                if (SFD.HasStamp != s.HasStamp)
                {
                    if (SFD.HasStamp)
                    {
                        Business.university.Request.CheckOutNaghsBusiness chNaghs = new Business.university.Request.CheckOutNaghsBusiness();
                        var naghsDT = chNaghs.GetAllNaghsByReqId(requestID);
                        if (naghsDT.Rows.Count > 0)
                        {
                            DataRow[] dr = naghsDT.Select("RequestLogID=29");
                            if (dr.Length > 0)
                            {
                                int naghsID = Convert.ToInt32(dr[0]["naghsID"]);
                                chNaghs.ResolveNaghsById(naghsID);
                                archiveID = GFB.insertDocArchiveId(SFD.RequestID);
                            }
                        }

                    }
                    else
                    {
                        naghs.InsertNaghs(new DTO.University.Request.CheckOutNaghsDTO
                        {

                            IsResolved = false,
                            NaghsMessage = stampMsg,
                            RequestLogId = 29,
                            ResolveDate = "",
                            ResolveMessage = "",
                            StCode = SFD.stcode,
                            StudentRequestId = SFD.RequestID,
                            SubmitDate = DateTime.Now.ToPeString(),
                            Erae_Be = "0"
                        });
                        setLog("لطفا تمبر خریداری فرمایید.", requestID, (int)DTO.eventEnum.ارسال_پیام_تسویه);
                        CB.sendSMS(1, SFD.stcode, stampMsg, out sentSMS, out smsStatus);

                    }
                }
                if (SFD.HasPaymentReceipt != s.HasPaymentReceipt)
                {
                    if (SFD.HasPaymentReceipt)
                    {
                        Business.university.Request.CheckOutNaghsBusiness chNaghs = new Business.university.Request.CheckOutNaghsBusiness();
                        var naghsDT = chNaghs.GetAllNaghsByReqId(requestID);
                        if (naghsDT.Rows.Count > 0)
                        {
                            DataRow[] dr = naghsDT.Select("RequestLogID=30");
                            if (dr.Length > 0)
                            {
                                int naghsID = Convert.ToInt32(dr[0]["naghsID"]);
                                chNaghs.ResolveNaghsById(naghsID);
                                archiveID = GFB.insertDocArchiveId(SFD.RequestID);

                            }
                        }
                    }
                    else
                    {

                        naghs.InsertNaghs(new DTO.University.Request.CheckOutNaghsDTO
                        {

                            IsResolved = false,
                            NaghsMessage = paymentReceiptMsg,
                            RequestLogId = 30,
                            ResolveDate = "",
                            ResolveMessage = "",
                            StCode = SFD.stcode,
                            StudentRequestId = SFD.RequestID,
                            SubmitDate = DateTime.Now.ToPeString(),
                            Erae_Be = "0"
                        });
                        setLog("لطفا فیش واریزی تهیه فرمایید.", requestID, (int)DTO.eventEnum.ارسال_پیام_تسویه);
                        string asanak = CB.sendSMS(1, SFD.stcode, paymentReceiptMsg, out sentSMS, out smsStatus);
                    }
                }
                if (description.Trim() != "")
                    setLog(description, requestID, (int)DTO.eventEnum.ایجاد_تغییر_در_صفحه_مشاهده_پرونده_دانشجویان_فارغ_التحصیل);
            }
            getStudentInformation(SFD.stcode);
        }

        protected void grdResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnView")
            {
                int rowInd = Convert.ToInt32(e.CommandArgument);
                grdResults.SelectedIndex = rowInd;
                getStudentInformation(grdResults.SelectedRow.Cells[3].Text);
                dvShowReport.Visible = true;
            }
        }

        protected void grdResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchStudent(); ;
            dvShowReport.Visible = false;
        }

        protected void grdResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResults.PageIndex = e.NewPageIndex;
            searchStudent();
        }

        private string getChangeset(StudentFeraghatDocument SFD_new)
        {
            StudentFeraghatDocument SFD_old = new StudentFeraghatDocument();
            SFD_old = GFB.getStudentFeraghatDocument(txtStcode.Text);


            string changes = "", spaceCharUsed = "   -   ", spaceChar = "";
            //if (SFD_new.ArchivesCode != SFD_old.ArchivesCode)
            //{
            //    changes += SFD_old.ArchivesCode == "" ? "درج کد بایگانی " + SFD_new.ArchivesCode : (SFD_new.ArchivesCode == "" ? "حذف کد بایگانی" : "تغییر کد بایگانی به " + SFD_new.ArchivesCode);
            //    spaceChar = spaceCharUsed;
            //}
            if (SFD_new.mashmul != SFD_old.mashmul)
            {
                changes += "مشمول سربازی: '" + (SFD_new.mashmul ? "هست" : "نیست") + "' وضعیت قبلی: " + SFD_old.mashmulName;
                spaceChar = spaceCharUsed;
            }
            if (SFD_new.HasPaymentReceipt != SFD_old.HasPaymentReceipt)
            {
                changes += spaceChar + (!SFD_new.HasPaymentReceipt ? "درج تیک برای 'فیش واریزی ندارد'" : "حذف تیک از 'فیش واریزی ندارد'");
                spaceChar = spaceCharUsed;
            }
            if (SFD_new.HasStamp != SFD_old.HasStamp)
            {
                changes += spaceChar + (!SFD_new.HasStamp ? "درج تیک برای 'تمبر ندارد'" : "حذف تیک از 'تمبر ندارد'");
                spaceChar = spaceCharUsed;
            }
            //if (SFD_new.PaiedToVahedSodoor != SFD_old.PaiedToVahedSodoor)
            //{
            //    changes += spaceChar + (SFD_new.PaiedToVahedSodoor ? "درج تیک برای 'پرداخت به واحد صادر کننده'" : "حذف تیک از 'پرداخت به واحد صادر کننده'");
            //    spaceChar = spaceCharUsed;
            //}
            if (SFD_new.SpecialTips != SFD_old.SpecialTips)
            {
                changes += spaceChar + "موارد خاص:" + SFD_new.SpecialTips;
            }

            if (SFD_new.serialNumber_Movaghat != SFD_old.serialNumber_Movaghat)
            {
                changes += spaceChar + "شماره سریال مدرک موقت:" + SFD_new.SpecialTips;
            }
            if (SFD_new.serialNumber_Daneshname != SFD_old.serialNumber_Daneshname)
            {
                changes += spaceChar + "شماره سریال دانشنامه:" + SFD_new.SpecialTips;
            }
            if (SFD_new.documentNumber_Daneshname != SFD_old.documentNumber_Daneshname)
            {
                changes += spaceChar + "شماره مدرک دانشنامه:" + SFD_new.SpecialTips;
            }
            if (SFD_new.documentNumber_Movaghat != SFD_old.documentNumber_Movaghat)
            {
                changes += spaceChar + "شماره مدرک گواهی موقت:" + SFD_new.SpecialTips;
            }
            return changes;
        }

        private void setLog(string description, int requestID, int eventID)
        {
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            int userId;//کاربری که لاگین کرده
            //eventType//کد کاری که انجام شده. 
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            int modifyId;//کد درخواست ویرایش شده. ویرایش اطلاعات فردی،اطلاعات تماس و ...
            userId = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            appId = 12;
            modifyId = requestID;
            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, eventID, description, modifyId);
        }

        protected void lnkBtnEnterDate_Click(object sender, EventArgs e)
        {
            Session["stcode_GraduateDoc"] = txtStcode.Text;
            Session["fromGraduateDoc"] = true;
            Response.Redirect("FeraghatTahsil.aspx");

        }

        protected void btnAddStudentAsGraduate_Click(object sender, EventArgs e)
        {
            List<DTO.University.Request.MahaleSodoor> dtVahed = bsn.GetListOfVahed();

            drpMahaleSodooreMadrak.DataSource = dtVahed;
            drpMahaleSodooreMadrak.DataTextField = "vahed";
            drpMahaleSodooreMadrak.DataValueField = "id";
            drpMahaleSodooreMadrak.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "AddVahedSodur();", true);
        }

        protected void btnCreateArchiveCode_Click(object sender, EventArgs e)
        {
            if (txtNewBaygani.Text != "")
            {
                bool res = bsn.insertCodeBayegan(txtStcode.Text, txtNewBaygani.Text);
                if (res)
                {
                    setLog(string.Format("{0} {1}  {2} {3}","ثبت کد بایگانی",txtNewBaygani.Text,"برای",txtStcode.Text),0, (int)DTO.eventEnum.ثبت_کد_بایگانی_مشابه_با_سیدا_در_خدمات);
                    txtNewBaygani.Visible = false;
                    txtNewBaygani.Text = "";
                    btnCreateArchiveCode.Visible = false;
                    txtBaygani.Visible = true;
                    StudentFeraghatDocument SFD = new StudentFeraghatDocument();
                    SFD = GFB.getStudentFeraghatDocument(txtStcode.Text);
                    setComponentsValue(SFD);

                }
            }
        }

        protected void btnPrintDaneshname_Click(object sender, EventArgs e)
        {
            Session["printDocument_Type"] = "daneshname";
            Session["printDocument_Stcode"] = txtStcode.Text;
            Response.Write("<script>window.open('../../GraduateAffair/CMS/GraduateForms.aspx','_blank')</script>");
        }

        protected void btnPrintGovahi_Click(object sender, EventArgs e)
        {
            Session["printDocument_Type"] = "govahi";
            Session["printDocument_Stcode"] = txtStcode.Text;
            Response.Write("<script>window.open('../../GraduateAffair/CMS/GraduateForms.aspx','_blank')</script>");
        }

        protected void btnSubmitVahedSorud_Click(object sender, EventArgs e)
        {
            addStudentToStudentRequest(ViewState["stcode_ADD"].ToString());
            searchStudent();
        }
    }
}