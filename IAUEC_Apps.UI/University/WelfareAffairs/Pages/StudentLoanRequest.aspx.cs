using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.IO;
using IAUEC_Apps.Business.university.WelfareAffairs;
using IAUEC_Apps.DTO.University.WelfareAffairs;
using System.Configuration;

namespace IAUEC_Apps.UI.University.WelfareAffairs.Pages
{
    public partial class StudentLoanRequest : System.Web.UI.Page
    {
        String CurrentTerm = ConfigurationSettings.AppSettings["Term"].ToString();
        //String CurrentTerm = "96-97-1";
        String Image_Height = "100";
        String Image_Width = "230";

        WelfareAffairsBusiness buiss = new WelfareAffairsBusiness();


        void BindGridView_StudentLoansHistory()
        {
            grdvLoadRecords.DataSource = buiss.GetAllLoanByStCode(Session[sessionNames.userID_StudentOstad].ToString());
            grdvLoadRecords.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //bind grid of student loan records
                BindGridView_StudentLoansHistory();

                //get natinal-card and id-card from digitparvande
                Session["docsInDigitParvandeh"] = buiss.GetNationalCardAndID(Session[sessionNames.userID_StudentOstad].ToString());

                //get current loan 'dar hal barasi' if exist
                var Loans = buiss.GetStudentLoans(Session[sessionNames.userID_StudentOstad].ToString(), CurrentTerm);                
                var firtConfirmLoan = Loans.Where(w => w.Status == 2)?.FirstOrDefault();                

                //vam tayeid avaliye shode bood
                if (firtConfirmLoan !=null)
                {
                    pnlMsgFinalConfirm.Visible = true;
                    return;
                }

                Session["Loans"] = Loans.Where(w => w.Status == 1)?.FirstOrDefault();
                if (Session["Loans"] != null)
                {
                    pnlFirtConfirm.Visible = true;
                    BindGrid_LoandDocsByLoanType((LoanInfo)Session["Loans"], true);
                }
                else
                {
                    var res = buiss.HasConditional_Loan(Session[sessionNames.userID_StudentOstad].ToString(), CurrentTerm);
                    switch (res)
                    {
                        case 0://student has the all conditions for getting loan
                            BindGrid_LoandDocsByLoanType(null); return;

                        case 1:
                            rwmValidations.RadAlert("دانشجوی گرامی به دلیل داشتن مشروطی بیش از یک ترم شرایط لازم جهت اخذ وام را ندارید ! ", 200, 150, "", null);
                            return;

                        case 2:
                            rwmValidations.RadAlert("دانشجوی گرامی به دلیل داشتن تخلف انضباطی شرایط لازم جهت اخذ وام را ندارید ! ", 200, 150, "", null);
                            return;

                        case 3:
                            rwmValidations.RadAlert("دانشجوی گرامی به دلیل عدم انتخاب واحد در ترم جاری شرایط لازم جهت اخذ وام را ندارید ! ", 200, 150, "", null);
                            return;

                        case 4:
                            rwmValidations.RadAlert("دانشجوی گرامی  به دلیل داشتن درخواست وام در ترم جاری امکان ثبت درخواست جدید وجود ندارد ! ", 200, 150, "", null);
                            return;

                        case 5:
                            rwmValidations.RadAlert("دانشجوی گرامی فقط دانشجویان با سابقه ی تحصیلی کمتر از 5 ترم قادر به ثبت درخواست وام می باشند ! ", 200, 150, "", null);
                            return;

                        case 6:
                            rwmValidations.RadAlert("دانشجوی گرامی ثبت درخواست وام فقط تا قبل از شروع امتحانات  ترم جاری امکانپذیر می باشد ! ", 200, 150, "", null);
                            return;
                    }
                }
            }



        }

        protected void rdbList_GuarantorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPannel_ByGuarantorType(rdbList_GuarantorType.SelectedValue);
        }

        protected void DrpLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPannel_ByLoanype(DrpLoanType.SelectedValue);
            switch (DrpLoanType.SelectedValue)
            {
                case "2": Disable_Uploaders_NatinalCard_IdCard(Sec_MidTime); break;                
                case "4": Disable_Uploaders_NatinalCard_IdCard(Sec_MehreIran); break;
            }
        }
        void ShowPannel_ByGuarantorType(string guarantor)
        {
            //ضامن ندارم
            if (guarantor == "1")
            {
                this.Sec_ShortTime.Visible = true;
                this.Sec_MidTime.Visible = false;
                //this.Sec_LongTime.Visible = false;
                this.Sec_MehreIran.Visible = false;
                this.Sec_LoanType.Visible = false;
            }
            else if (guarantor == "2")//ضامن دارم
            {
                this.Sec_ShortTime.Visible = false;
                this.Sec_LoanType.Visible = true;
                //this.DrpLoanType.SelectedValue = GetLoanType().ToString();
            }
        }

        void ShowPannel_ByLoanype(string loanType)
        {

            switch (loanType)
            {
                case "2"://میان مدت
                    this.Sec_MidTime.Visible = true;
                    //this.Sec_LongTime.Visible = false;
                    this.Sec_MehreIran.Visible = false;
                    break;

                case "3"://بلند مدت
                    //this.Sec_LongTime.Visible = true;
                    this.Sec_MidTime.Visible = false;
                    this.Sec_MehreIran.Visible = false;
                    break;

                case "4"://مهر ایران
                    this.Sec_MehreIran.Visible = true;
                    this.Sec_MidTime.Visible = false;
                    //this.Sec_LongTime.Visible = false;
                    break;

                case "1"://کوتاه مدت
                default:
                    this.Sec_MidTime.Visible = false;
                    //this.Sec_LongTime.Visible = false;
                    this.Sec_MehreIran.Visible = false;
                    break;
            }
        }



        protected void btnShortTime_Click(object sender, EventArgs e)
        {
            if (CkeckValidations(Sec_ShortTime))
            {
                AddOrUpdateLoanAndDocs(Sec_ShortTime);

            }
            else
                rwmValidations.RadAlert("مدرکی جهت بارگذاری انتخاب نشده است", null, 100, "پیام", "");
        }

        protected void btnMidTime_Click(object sender, EventArgs e)
        {
            if (CkeckValidations(Sec_MidTime))
                AddOrUpdateLoanAndDocs(Sec_MidTime);
            else
                rwmValidations.RadAlert("مدرکی جهت بارگذاری انتخاب نشده است", null, 100, "پیام", "");

        }

        protected void btnLongTime_Click(object sender, EventArgs e)
        {
            //if (CkeckValidations(Sec_LongTime))
            //    AddOrUpdateLoanAndDocs(Sec_LongTime);
            //else
            //    rwmValidations.RadAlert("مدرکی جهت بارگذاری انتخاب نشده است", null, 100, "پیام", "");


        }

        protected void btnMehreIran_Click(object sender, EventArgs e)
        {
            if (CkeckValidations(Sec_MehreIran))
                AddOrUpdateLoanAndDocs(Sec_MehreIran);
            else
                rwmValidations.RadAlert("مدرکی جهت بارگذاری انتخاب نشده است", null, 100, "پیام", "");
        }

        private void AddOrUpdateLoanAndDocs(Control sectionName)
        {
            int currentInsertedLoanID = 0;
            var loanDocs = new List<LoanDocInfoDTO>();

            //get loan with status=1 ==>dar hale baresi
            var allLoans = buiss.GetStudentLoans(Session[sessionNames.userID_StudentOstad].ToString(), CurrentTerm).Where(w => w.Status == 1);
            var loan = allLoans?.FirstOrDefault();

            if (loan == null)//add mode
            {
                var newLoan = new LoanInfo();
                newLoan.StudentCode = Session[sessionNames.userID_StudentOstad].ToString();
                newLoan.Term = CurrentTerm;
                newLoan.LoanType = GetLoanType();
                newLoan.Status = 1;//sate darkhast
                newLoan.ReqRegisterDate = DateTime.Now;
                //this method returns current inserted id ==>scope_identity() from side of sql
                currentInsertedLoanID = buiss.AddOrUpdateLoan(newLoan);
            }
            else
            {
                currentInsertedLoanID = loan.LoanId; //update mode
            }

            //inserted a recorde with LoanId=0 in LoansStudent table that is an error
            if (currentInsertedLoanID == 0)
            {
                buiss.DeleteLoan(Session[sessionNames.userID_StudentOstad].ToString(), CurrentTerm);
                return;
            }

            sectionName.Controls.OfType<FileUpload>().ToList().ForEach(uploader =>
            {
                var doc = SaveFilesOnServer(uploader, currentInsertedLoanID, uploader.Attributes["data-DocName"].ToString(), loan?.LoanDocs);
                if (doc != null)
                    loanDocs.Add(doc);
            });

            if (loanDocs.Count > 0)
                buiss.AddOrUpdateLoansDocs(loanDocs, Session[sessionNames.userID_StudentOstad].ToString());

            //to rebinde grid feching new data is need
            Session["Loans"] = buiss.GetStudentLoans(Session[sessionNames.userID_StudentOstad].ToString(), CurrentTerm)?.Where(w => w.Status == 1).FirstOrDefault();

            BindGrid_LoandDocsByLoanType(Session["Loans"] as LoanInfo, true);
        }

        //LoanDocInfoDTO SaveFilesOnServer(RadAsyncUpload uploader, int loanID, string docName = null, List<LoanDocInfoDTO> docs = null)
        LoanDocInfoDTO SaveFilesOnServer(FileUpload uploader, int loanID, string docName = null, List<LoanDocInfoDTO> docs = null)
        {
            LoanDocInfoDTO loanDocLocal = null;
            if (uploader != null && uploader.HasFile)//uploader.UploadedFiles.Count > 0
            {
                var loanType = -1;
                if (uploader.ID.Contains("ShortTime")) loanType = 1;
                else if (uploader.ID.Contains("MidTime")) loanType = 2;
                else if (uploader.ID.Contains("LongTime")) loanType = 3;
                else if (uploader.ID.Contains("MehreIran")) loanType = 4;

                //===================  
                int? docId = docs?.Where(w => w.DocName == docName).FirstOrDefault()?.DocId;
                string relPath = "";
                string absPath = "";
                string standardedFileName = "UnKnown.jpg";
                string docTitle = "";
                byte docType = 0;
                byte docStatus = 1;

                switch (loanType)
                {
                    //بدون ضامن
                    case 1:
                        relPath += "../Files/" + CurrentTerm + "/ShortTime/" + Session[sessionNames.userID_StudentOstad].ToString();
                        break;

                    //با ضامن
                    case 2://میان مدت
                        relPath += "../Files/" + CurrentTerm + "/MidTime/" + Session[sessionNames.userID_StudentOstad].ToString();
                        break;
                    //-----------
                    case 3://بلند مدت
                        relPath += "../Files/" + CurrentTerm + "/LongTime/" + Session[sessionNames.userID_StudentOstad].ToString();
                        break;
                    //-----------
                    case 4://مهر ایران
                        relPath += "../Files/" + CurrentTerm + "/MehreIran/" + Session[sessionNames.userID_StudentOstad].ToString();
                        break;
                }
                try
                {
                    absPath = Server.MapPath(relPath);
                    if (!Directory.Exists(absPath))
                        Directory.CreateDirectory(absPath);

                    var ext = uploader.PostedFile.FileName.Split('.').Last();
                    standardedFileName = docName + "." + ext;
                    var fullPath = absPath + "/" + standardedFileName;

                    if (File.Exists(fullPath))
                        File.Delete(fullPath);

                    uploader.SaveAs(fullPath);
                    uploader.Dispose();
                    uploader.PostedFile.InputStream.Dispose();
                    uploader = null;

                    var result = GetDocType_DocTitle(docName);

                    docTitle = result.Split(',')[0];
                    docType = byte.Parse(result.Split(',')[1]);

                    loanDocLocal = new LoanDocInfoDTO()
                    {
                        DocId = docId,
                        LoanId = loanID,
                        DocPath = relPath,
                        DocName = docName,
                        DocTitle = docTitle,
                        DocType = docType,
                        DocStatus = docStatus
                    };
                }
                catch (Exception x) { throw x; }
            }
            return loanDocLocal;
        }

        byte GetLoanType()
        {
            byte loanType = 1;   //کوتاه مدت 
            if (rdbList_GuarantorType.SelectedValue == "2")
            {
                switch (DrpLoanType.SelectedValue)
                {
                    case "2": loanType = 2; break;//میان مدت
                    case "3": loanType = 3; break;//بلند مدت
                    case "4": loanType = 4; break;//مهر ایران
                }
            }
            return loanType;
        }


        string GetDocType_DocTitle(string docName)
        {
            byte DocType = 0;
            string DocTitle = "UnKnown";
            switch (docName)
            {
                case "ShortTime_Form":
                    DocTitle = "فرم وام کوتاه مدت ";
                    DocType = 1;
                    break;


                case "MidTime_Form":
                    DocTitle = "فرم وام میان مدت ";
                    DocType = 2;
                    break;


                case "LongTime_Form":
                    DocTitle = "فرم وام بلند مدت ";
                    DocType = 3;
                    break;


                case "MehreIran_Form":
                    DocTitle = "فرم وام قرض الحسنه مهر ایران ";
                    DocType = 4;
                    break;


                case "MidTime_Stu_ID":
                case "LongTime_Stu_ID":
                case "MehreIran_ID":
                    DocTitle = "کپی شناسنامه دانشجو ";
                    DocType = 5;
                    break;


                case "MidTime_Stu_NationalCard":
                case "LongTime_Stu_NationalCard":
                case "MehreIran_NationalCard":
                    DocTitle = "کپی کارت ملی دانشجو ";
                    DocType = 6;
                    break;


                case "MidTime_Guarantor_ID":
                case "LongTime_Guarantor_ID":
                case "MehreIran_Guarantor_ID":
                    DocTitle = "کپی شناسنامه ضامن ";
                    DocType = 7;
                    break;


                case "MidTime_Guarantor_NationalCard":
                case "LongTime_Guarantor_NationalCard":
                case "MehreIran_Guarantor_NationalCard":
                    DocTitle = "کپی کارت ملی ضامن ";
                    DocType = 8;
                    break;


                case "MidTime_Guarantor_Kargozini":
                case "LongTime_Guarantor_Kargozini":
                    DocTitle = " کپی حکم کارگزینی ضامن";
                    DocType = 9;
                    break;


                case "MidTime_Guarantor_Fish":
                case "LongTime_Guarantor_Fish":
                    DocTitle = "فیش حقوقی ضامن ";
                    DocType = 10;
                    break;

                //case "MidTime_Recognizance":
                case "LongTime_Recognizance":
                case "MehreIran_Recognizance":
                    DocTitle = "تعهدنامه محضری ";
                    DocType = 11;
                    break;


                case "ShortTime_Check":
                case "MidTime_Check":
                    DocTitle = "چک ضمانت";
                    DocType = 12;
                    break;
            }
            return DocTitle + "," + DocType.ToString();
        }


        private void BindGrid_LoandDocsByLoanType(LoanInfo loan, bool hideSubmitBTN = false)
        {
            if (loan != null)
            {
                switch (loan.LoanType)
                {
                    case 1:
                        Sec_ShortTime.Visible = true;
                        ShowHideComponentsBySectionName(Sec_ShortTime, loan, hideSubmitBTN);
                        break;

                    case 2:
                        Sec_MidTime.Visible = true;
                        ShowHideComponentsBySectionName(Sec_MidTime, loan, hideSubmitBTN);
                        break;

                    //case 3:
                    //    Sec_LongTime.Visible = true;
                    //    ShowHideComponentsBySectionName(Sec_LongTime, loan, hideSubmitBTN);
                    //    break;

                    case 4:
                        Sec_MehreIran.Visible = true;
                        ShowHideComponentsBySectionName(Sec_MehreIran, loan, hideSubmitBTN);
                        break;
                }
                BindGridView_StudentLoansHistory();
            }
            else
            {
                Sec_LoanSelection.Visible = true;
                Sec_Guarantor.Visible = true;
                ShowPannel_ByGuarantorType(rdbList_GuarantorType.SelectedValue);
            }
        }

        void ShowHideComponentsBySectionName(Control sectionName, LoanInfo loan, bool hideSubmitBTN)
        {
            bool Id_Card = false; //cat shenasnameh ==>100
            bool Na_Card = false; //cat cart meli  ==>101

            List<FileUpload> uploaders = null;
            List<Label> lables = null;
            List<Image> images = null;

            string res = Disable_Uploaders_NatinalCard_IdCard(sectionName);
            Id_Card = Convert.ToBoolean(res.Split(';')[0].Split('=')[1]);
            Na_Card = Convert.ToBoolean(res.Split(';')[1].Split('=')[1]);


            var ctrlUploders = sectionName.Controls.OfType<FileUpload>().ToList();
            var ctrlLables = sectionName.Controls.OfType<Label>().ToList();
            var ctrlImages = sectionName.Controls.OfType<Image>().ToList();

            if (Id_Card || Na_Card)
            {
                if (Id_Card && !Na_Card)
                {
                    uploaders = ctrlUploders.Where(uploader => (!uploader.ID.Contains("_Stu_ID"))).ToList();
                    lables = ctrlLables.Where(lbl => (!lbl.ID.Contains("_Stu_ID"))).ToList();
                    images = ctrlImages.Where(img => (!img.ID.Contains("_Stu_ID"))).ToList();
                }
                else if (!Id_Card && Na_Card)
                {
                    uploaders = ctrlUploders.Where(uploader => (!uploader.ID.Contains("_Stu_NationalCard"))).ToList();
                    lables = ctrlLables.Where(lbl => (!lbl.ID.Contains("_Stu_NationalCard"))).ToList();
                    images = ctrlImages.Where(img => (!img.ID.Contains("_Stu_NationalCard"))).ToList();
                }
                else if (Id_Card && Na_Card)
                {
                    uploaders = ctrlUploders
                                .Where(uploader => (!uploader.ID.Contains("_Stu_ID")) && (Na_Card && !uploader.ID.Contains("_Stu_NationalCard")))
                                .ToList();

                    lables = ctrlLables
                                .Where(lbl => (!lbl.ID.Contains("_Stu_ID")) && (Na_Card && !lbl.ID.Contains("_Stu_NationalCard")))
                                .ToList();

                    images = ctrlImages
                                .Where(img => (!img.ID.Contains("_Stu_ID")) && (Na_Card && !img.ID.Contains("_Stu_NationalCard")))
                                .ToList();

                }
            }
            else
            {
                uploaders = ctrlUploders;
                lables = ctrlLables;
                images = ctrlImages;
            }


            uploaders.ForEach(uploader =>
            {
                var doc = loan.LoanDocs.FirstOrDefault(s => s.DocName == uploader.Attributes["data-DocName"]);
                if (doc != null)
                {

                    var IMG = images.FirstOrDefault(img => img.ID.Contains(doc.DocName));
                    if (IMG != null && doc.DocStatus != 3)
                    {
                        var imgPath = doc.DocPath + "/" + doc.DocName + ".jpg?ts=" + DateTime.Now.Ticks;
                        IMG.Attributes.Add("width", Image_Width);
                        IMG.Attributes.Add("height", Image_Height);
                        IMG.ImageUrl = imgPath;
                    }

                    //set text of DocStatus lable
                    switch (doc.DocStatus)
                    {
                        case 1://first status uploading doc
                            uploader.Enabled = false;
                            uploader.BackColor = System.Drawing.Color.LightGray;
                            lables.FirstOrDefault(lbl => lbl.Attributes["data-DocName"] == doc.DocName).Text = "در حال بررسی";
                            break;

                        case 2: //accept doc
                            uploader.Enabled = false;
                            uploader.BackColor = System.Drawing.Color.Gray;
                            lables.FirstOrDefault(lbl => lbl.Attributes["data-DocName"] == doc.DocName).Text = "تایید مدرک";
                            break;

                        case 3://reject doc
                            uploader.Enabled = true;
                            lables.FirstOrDefault(lbl => lbl.Attributes["data-DocName"] == doc.DocName).Text = "رد مدرک";
                            break;
                    }

                    //set text of doc-status column for this document
                    lables.FirstOrDefault(
                                lbl => lbl.Attributes["data-DocType"] ==  doc.DocType.ToString()
                            ).Text = string.IsNullOrEmpty(doc.Description) ? "--" : doc.Description;

                }
            });

            //set text of description or message column 
            lables.Where(lbl => !lbl.ID.Contains("msg")).ToList().ForEach(lbl => { if (lbl.Text == "") lbl.Text = "بارگذاری نشده"; });

            //show and hide buttons in sections
            switch (loan.LoanType)
            {
                case 1: btnShortTime.Visible = loan.LoanDocs.Any(a => a.DocStatus == 3) || uploaders.Any(a => a.Enabled == true) ? true : false; break;
                case 2: btnMidTime.Visible = loan.LoanDocs.Any(a => a.DocStatus == 3) || uploaders.Any(a => a.Enabled == true) ? true : false; break;
                //case 3: btnLongTime.Visible = loan.LoanDocs.Any(a => a.DocStatus == 3) || uploaders.Any(a => a.Enabled == true) ? true : false; break;
                //case 4: btnMehreIran.Visible = loan.LoanDocs.Any(a => a.DocStatus == 3) || uploaders.Any(a => a.Enabled == true) ? true : false; break;
            }

            //call afetr loan and docs
            if (hideSubmitBTN)
            {
                Sec_LoanSelection.Visible = false;
                Sec_Guarantor.Visible = Sec_LoanType.Visible = false;

            }

        }

        string Disable_Uploaders_NatinalCard_IdCard(Control sectionName)
        {
            var digitDocs = Session["docsInDigitParvandeh"] as List<DigitParvande>;

            bool Id_Card = false; //cat shenasnameh ==>100
            bool Na_Card = false; //cat cart meli  ==>101

            List<Label> lables = null;
            List<FileUpload> uploaders = null;

            var ctrlUploders = sectionName.Controls.OfType<FileUpload>()
                .Where(uploader => (uploader.ID.Contains("_Stu_ID") || uploader.ID.Contains("_Stu_NationalCard")))
                .ToList();

            var ctrlLables = sectionName.Controls.OfType<Label>()
                             .Where(uploader => (uploader.ID.Contains("_Stu_ID") || uploader.ID.Contains("_Stu_NationalCard")))
                             .ToList();

            string res = "";

            if (digitDocs.Count > 0)
            {
                digitDocs.ForEach(doc =>
                {
                    switch ((int)doc.Cat)
                    {
                        case 100: Id_Card = true; break;
                        case 101: Na_Card = true; break;
                        default: break;
                    }
                });

                if (Id_Card || Na_Card)
                {
                    if (Id_Card && !Na_Card)
                    {
                        uploaders = ctrlUploders.Where(uploader => (uploader.ID.Contains("_Stu_ID"))).ToList();
                        lables = ctrlLables.Where(lbl => (lbl.ID.Contains("_Stu_ID"))).ToList();
                    }
                    else if (!Id_Card && Na_Card)
                    {
                        uploaders = ctrlUploders.Where(uploader => (uploader.ID.Contains("_Stu_NationalCard"))).ToList();
                        lables = ctrlLables.Where(lbl => (lbl.ID.Contains("_Stu_NationalCard"))).ToList();
                    }
                    else if (Id_Card && Na_Card)
                    {
                        uploaders = ctrlUploders;
                        lables = ctrlLables;
                    }
                }

                uploaders.ForEach(up =>
                {
                    if (up.ID.Contains("_Stu_ID"))
                    {
                        up.Enabled = false;
                        up.BackColor = System.Drawing.Color.Gray;

                        lables.Where(lbl => lbl.ID.Contains("_Stu_ID")).ToList().ForEach(lbl =>
                        {
                            if (lbl.ID.Contains("_msg")) lbl.Text = "--";
                            else lbl.Text = "در بایگانی موجود می باشد";
                        });
                    }
                    else if (up.ID.Contains("_Stu_NationalCard"))
                    {
                        up.Enabled = false;
                        up.BackColor = System.Drawing.Color.Gray;
                        lables.Where(lbl => lbl.ID.Contains("_Stu_NationalCard")).ToList().ForEach(lbl =>
                        {
                            if (lbl.ID.Contains("_msg")) lbl.Text = "--";
                            else lbl.Text = "در بایگانی موجود می باشد";
                        });
                    }
                });
            }

            res += Id_Card ? "Id_Card=true" : "Id_Card=false";
            res += ";";
            res += Na_Card ? "Na_Card=true" : "Na_Card=false";
            return res;
        }

        bool CkeckValidations(Control sectionName)
        {
            bool res = false;
            var fileUploads = sectionName.Controls.OfType<FileUpload>().ToList();

            if (fileUploads.Any(uploader => uploader.Enabled == true && uploader.HasFile))
            {
                fileUploads.Where(w => w.Enabled == true && w.HasFile).ToList().ForEach(file =>
                {
                    var ext = (file.FileName.Split('.'))[1].ToLower().Trim();
                    if (ext != "jpg" )
                    {
                        rwmValidations.RadAlert("می تواند باشد jpg  نوع فایل انتخابی فقط ", null, 100, "پیام", "");
                        return;
                    }
                    else
                    {
                        if (file.PostedFiles[0].ContentLength > 1024 * 1024)//more than 1mega byte
                        {
                            rwmValidations.RadAlert("سایز فایل انتخابی نمی تواند بیشتر از یک مگابایت باشد", null, 100, "پیام", "");
                            return;
                        }
                        else
                        {
                            res = true;
                        }
                    }
                });
            }
            return res;
        }


        //==============================
        protected void grdvLoadRecords_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var loanID = e.CommandArgument.ToString();
            var cmdName = e.CommandName;
            if (cmdName == "show_popup")
            {
                RadWindowManager rwmViewDetails = new RadWindowManager();
                RadWindow viewDetailsWindow = new RadWindow();
                viewDetailsWindow.NavigateUrl = "../Pages/ShowDocs_StudentLoan.aspx?loanID=" + e.CommandArgument.ToString();
                viewDetailsWindow.DestroyOnClose = true;
                viewDetailsWindow.ShowContentDuringLoad = false;
                viewDetailsWindow.ReloadOnShow = true;
                viewDetailsWindow.ID = "RadWindow1";
                viewDetailsWindow.CssClass = "rad_window_dir_rtl";
                rwmViewDetails.Width = Unit.Pixel(700);
                rwmViewDetails.Height = Unit.Pixel(500);
                
                viewDetailsWindow.VisibleOnPageLoad = true;
                rwmViewDetails.Windows.Add(viewDetailsWindow);
                ContentPlaceHolder ViewDetailsContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                ViewDetailsContentPlaceHolder.Controls.Add(viewDetailsWindow);
                rwmViewDetails.Windows.Clear();


            }
        }
    }
}