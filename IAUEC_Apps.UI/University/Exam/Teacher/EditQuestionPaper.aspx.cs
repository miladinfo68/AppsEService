using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.university.Exam;
using Ionic.Zip;
using System.IO;
using IAUEC_Apps.Business.Common;
using System.Text;
using IAUEC_Apps.DTO.University.Exam;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class EditQuestionPaper : System.Web.UI.Page
    {
        ExamBusiness Ebusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        public string PdfFileSource;
        public string PdfMimeType { get; set; } = "application/pdf";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string did = Request.QueryString["did"].ToString();
                int? cityIdQ2 = int.Parse(Request.QueryString["cityId"].ToString());
                //int qId = int.Parse(Request.QueryString["qID"].ToString());
                //string saatExam = Request.QueryString["examTime"].ToString();
                //string dateExam = Request.QueryString["examDate"].ToString();
                int statusQ2 = int.Parse(Request.QueryString["statusQ2"].ToString());
                int statusQ1 = int.Parse(Request.QueryString["statusQ1"].ToString());


                ddl_Cal.Items.Add(new ListItem("انتخاب نمایید", "0"));
                ddl_Cal.Items.Add(new ListItem("می باشد", "1"));
                ddl_Cal.Items.Add(new ListItem("نمی باشد", "2"));
                ddl_Note.Items.Add(new ListItem("انتخاب نمایید", "0"));
                ddl_Note.Items.Add(new ListItem("می باشد", "1"));
                ddl_Note.Items.Add(new ListItem("نمی باشد", "2"));

                int? cityIDQ2 = null;
                if (statusQ1 == 3 && statusQ2 !=-1)
                    cityIDQ2 = cityIdQ2;

                var examQ = Ebusiness.GetExamQuestionsbyDid(did, null, cityIDQ2);
                txt_ExamTime.Text = examQ.Rows[0]["ExamTime"].ToString();
                if (bool.Parse(examQ.Rows[0]["Calculator"].ToString()) == true)
                {
                    ddl_Cal.SelectedValue = "1";
                }
                else
                {
                    ddl_Cal.SelectedValue = "2";
                }
                if (bool.Parse(examQ.Rows[0]["Note"].ToString()) == true)
                {
                    ddl_Note.SelectedValue = "1";
                }
                else
                {
                    ddl_Note.SelectedValue = "2";
                }

                if (Ebusiness.GetIdgroupBydid(Request.QueryString["did"].ToString() ))
                {
                    book_pnl.Visible = true;
                    if (bool.Parse(examQ.Rows[0]["LawBook"].ToString()) == true)
                    {
                        rdb_book.SelectedValue = "1";
                    }
                    else
                        rdb_book.SelectedValue = "2";

                    txt_book.Text = examQ.Rows[0]["BookName"].ToString();
                }
                if (bool.Parse(examQ.Rows[0]["AnswerSheet1"].ToString()) == true)
                    chk_ans1.Checked = true;
                if (bool.Parse(examQ.Rows[0]["AnswerSheet2"].ToString()) == true)
                    chk_ans2.Checked = true;

                SectionManagement();

            }
        }
        void SectionManagement(bool pnl1 = true, bool pnl2 = false, bool pnl3 = false)
        {
            pnl_Qoptions.Visible = pnl1;
            pnl_UploadQ_Att.Visible = pnl2;
            pnl_GeneratedPdf_MergedQuestion_Att.Visible = pnl3;

        }
        public string generaterandomstr(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-@#!";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        bool SaveFile(HttpPostedFile file, string dirPath, string fileName)
        {
            bool done = true;
            try
            {
                //delete all files(zip pdf or jpg) in this folder;
                //DirectoryInfo dir = new DirectoryInfo(dirPath);
                //dir.GetFiles().ToList().ForEach(f => { f.Delete(); });
                string fullPath = $"{dirPath}/{fileName}";
                fileUploader.SaveAs(fullPath);
            }
            catch (Exception x)
            {
                done = false;
                //throw x;
            }
            return done;
        }
        bool SaveAttachFile(HttpPostedFile file, string dirPath, string fileName)
        {
            bool done = true;
            try
            {
                //delete attached files in this folder;
                //DirectoryInfo dir = new DirectoryInfo(dirPath);
                //dir.GetFiles().Where(w => w.Name.Contains("Attached")).ToList().ForEach(f => { f.Delete(); });
                string fullPath = $"{dirPath}/{fileName}";
                AttachUploader.SaveAs(fullPath);
            }
            catch (Exception x)
            {
                done = false;
                //throw x;
            }
            return done;
        }
        protected void btnRegisterAndContinue_Click(object sender, EventArgs e)
        {
            string did = Request.QueryString["did"].ToString();
            if (!CommonBusiness.IsNumeric(txt_ExamTime.Text))
            {
                rwm.RadAlert("زمان آزمون را صحیح وارد نمایید", null, 100, "خطا ", "");
            }
            else if (Request.QueryString["examTime"].ToString() != "11:00" && (int.Parse(txt_ExamTime.Text) > 90 || int.Parse(txt_ExamTime.Text) < 30))
            {
                rwm.RadAlert("حداقل زمان آزمون 30 و حداکثر 90 دقیقه می باشد", null, 100, "خطا", "");
            }
            else if (Request.QueryString["examTime"].ToString() == "11:00" && (int.Parse(txt_ExamTime.Text) > 120 || int.Parse(txt_ExamTime.Text) < 30))
            {
                rwm.RadAlert("حداقل زمان آزمون 30 و حداکثر 120 دقیقه می باشد", null, 100, "خطا", "");
            }
            else if (ddl_Cal.SelectedValue == "0" || ddl_Note.SelectedValue == "0")
            {
                rwm.RadAlert("همه گزینه ها انتخاب گردد", null, 100, "خطا", "");
            }
            else
            {
                if (book_pnl.Visible && rdb_book.SelectedValue == "0")
                {
                    rwm.RadAlert("شرایط استفاده از کتاب قانون تعیین نشده است", null, 100, "هشدار", "");
                }
                else if (book_pnl.Visible && rdb_book.SelectedValue == "1" && txt_book.Text == "")
                {
                    rwm.RadAlert("نام کتاب قانون ذکر نشده است", null, 100, "هشدار", "");
                }
                else
                {
                    if (chk_ans1.Checked || chk_ans2.Checked)
                    {
                        bool cal = false;
                        bool note = false;
                        bool lawbook = false;
                        if (rdb_book.SelectedValue == "1") lawbook = true;
                        if (ddl_Cal.SelectedValue.ToString() == "1") cal = true;
                        if (ddl_Note.SelectedValue.ToString() == "1") note = true;

                        //int did = int.Parse(Request.QueryString["did"].ToString());
                        int? cityIdQ2 = int.Parse(Request.QueryString["cityId"].ToString());
                        //int qId = int.Parse(Request.QueryString["qID"].ToString());
                        //string saatExam = Request.QueryString["examTime"].ToString();
                        //string dateExam = Request.QueryString["examDate"].ToString();
                        int statusQ2 = int.Parse(Request.QueryString["statusQ2"].ToString());
                        int statusQ1 = int.Parse(Request.QueryString["statusQ1"].ToString());


                        int? cityIDQ2 = null;
                        if (statusQ1 == 3 && statusQ2 != -1)
                            cityIDQ2 = cityIdQ2;

                        var examQ = Ebusiness.GetExamQuestionsbyDid(did, null, cityIDQ2);
                        if (bool.Parse(examQ.Rows[0]["Calculator"].ToString()) == cal
                            && bool.Parse(examQ.Rows[0]["Note"].ToString()) == note
                            && examQ.Rows[0]["ExamTime"].ToString() == txt_ExamTime.Text
                            && bool.Parse(examQ.Rows[0]["LawBook"].ToString()) == lawbook
                            && examQ.Rows[0]["BookName"].ToString() == txt_book.Text
                            )
                        {
                            SectionManagement(false, true, false);
                            if (int.Parse(examQ.Rows[0]["Status"].ToString()) != 3)
                            {
                                //var quizSheetCode = new Random().Next(0, 999999).ToString("D6");
                                Ebusiness.TemplateDownloaded(did, true);

                                if (chk_ans1.Checked != bool.Parse(examQ.Rows[0]["AnswerSheet1"].ToString()) || chk_ans2.Checked != bool.Parse(examQ.Rows[0]["AnswerSheet2"].ToString()))
                                {
                                    Ebusiness.UpdateExamOption(did, int.Parse(txt_ExamTime.Text), cal, note, chk_ans1.Checked, chk_ans2.Checked, false, lawbook, txt_book.Text);
                                }
                            }
                        }
                        else
                        {
                            rwm.RadAlert("شرایط آزمون توسط شما ویرایش شده است برای ادامه روند دانلود فرمت جدید الزامی می باشد", null, 100, "هشدار", "");
                        }
                    }
                    else
                        rwm.RadAlert("نحوه پاسخگویی به سوالات انتخاب نشده است", null, 100, "هشدار", "");
                }
            }

        }


        protected void btn_DL_Click(object sender, EventArgs e)
        {

            if (int.Parse(txt_ExamTime.Text) > 90 || int.Parse(txt_ExamTime.Text) < 30)
            {
                rwm.RadAlert("حداقل زمان آزمون 30 و حداکثر 90 دقیقه می باشد", null, 100, "خطا", "");
                return;
            }
            if (!chk_ans1.Checked && !chk_ans2.Checked)
            {
                rwm.RadAlert("نحوه پاسخگویی به سوالات انتخاب گردد", null, 100, "خطا", "");
                return;
            }

            string did = Request.QueryString["did"].ToString();
            int? cityIdQ2 =int.Parse(Request.QueryString["cityId"].ToString());
            int statusQ1 = int.Parse(Request.QueryString["statusQ1"].ToString());
            int statusQ2 = int.Parse(Request.QueryString["statusQ2"].ToString());
            //string dateExam = Request.QueryString["examDate"].ToString();


            int? cityIDQ2 = null;
            if (statusQ1 <8 && statusQ2 == -1) cityIDQ2 = null;
            if (statusQ1 == 3 && statusQ2 != -1)   cityIDQ2 = cityIdQ2;

            bool cal = false;
            bool note = false;
            bool lawbook = false;
            if (rdb_book.SelectedValue == "1") lawbook = true;
            if (ddl_Cal.SelectedValue.ToString() == "1") cal = true;
            if (ddl_Note.SelectedValue.ToString() == "1") note = true;
            Ebusiness.UpdateExamOption(did, int.Parse(txt_ExamTime.Text), cal, note, chk_ans1.Checked, chk_ans2.Checked, false, lawbook, txt_book.Text, cityIDQ2);

            var examQ = Ebusiness.ShowQueizPaperByDid(did, cityIDQ2);
            cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), 8, 20, examQ.Rows[0]["QuestionId"].ToString());

            string calc;
            string Note;
            string book = "";
            if (examQ.Rows[0]["Calculator"].ToString() == "True") calc = "می باشد";
            else calc = "نمی باشد";
            if (examQ.Rows[0]["Note"].ToString() == "True") Note = "باز می باشد";
            else Note = "بسته می باشد";
            if (examQ.Rows[0]["LawBook"].ToString() == "True")
            {
                book = "<tr><td   colspan='2' style='font-size: 11px;font-weight:bold;width:50%;padding: 2px 15px 2px 5px'>استفاده از کتاب قانون مجاز می باشد";
                if (examQ.Rows[0]["BookName"].ToString() != "")
                    book += "***" + examQ.Rows[0]["BookName"].ToString() + "***</td></tr>";
            }
            else
            {
                if (Ebusiness.GetIdgroupBydid(did))
                    book = "<tr><td  colspan='2' style='font-size: 11px;font-weight:bold;width:50%;padding: 2px 15px 2px 5px'>استفاده از کتاب قانون مجاز نمی باشد";
            }

            if (examQ.Rows[0]["Status"].ToString() != "3")
            {
                //=========== make random code for questions
                //var quizSheetCode = new Random().Next(0, 999999).ToString("D6");
                Ebusiness.TemplateDownloaded(did, true);
            }

            //=========== 

            var examDetail = Ebusiness.Get_ExamdetailbyDid(did, null, cityIDQ2);
            string term = examDetail.Rows[0]["tterm"].ToString().Substring(0, 5);
            string vorudi = examDetail.Rows[0]["tterm"].ToString().Substring(examDetail.Rows[0]["tterm"].ToString().Length - 1, 1);
            if (vorudi == "1") vorudi = "اول";
            if (vorudi == "2") vorudi = "دوم";
            string[] trm = term.ToString().Split(new char[] { '-' });
            string[] exdate = examDetail.Rows[0]["dateexam"].ToString().Split(new char[] { '/' });    

            string content = "<body dir='rtl' style='margin-right:7px'> " +
                "<table style='border-style: solid; border-width: 1px; width:99%; margin:10px; border-spacing: 0px;border-collapse: collapse;' border='1' dir='rtl' >" +
                "<tr><td style='width:90%;padding-left:20px; padding-top: 0px;'>" +
            "<table style='border-style: solid; border-width: 1px; width:100%; margin-right:20px; margin-left:10px; margin-bottom:15px;border-spacing:0px; font-family: tahoma; font-size: 10px;border-collapse: collapse;' border='1' dir='rtl'>" +

            "<tr>" +
            "<td style='padding: 2px 5px 2px 5px'> امتحانات پایان ترم نیمسال : " + "<span style='font-weight:bold'>" + vorudi + "  سال تحصیلی  " + "<span style='font-weight:bold'>" + trm[1] + "</span><span style='font-weight:bold'>" + "-" + "</span><span sstyle='font-weight:bold'>" + trm[0] + "</span>" + "</td>" +
            "<td style='padding: 2px 5px 2px 5px'> کد برگه آزمون : <span style='font-weight:bold'></span></td>" +
            "</tr>" +

            "<tr>" +
            "<td style='padding: 2px 5px 2px 5px'>عنوان درس : " + "<span style='font-weight:bold'>" + examDetail.Rows[0]["namedars"].ToString() + "</span>" + "</td>" +
            "<td style='padding: 2px 5px 2px 5px'>مدت زمان برگزاری امتحان : " + "<span style='font-weight:bold'>" + examQ.Rows[0]["ExamTime"].ToString() + " دقیقه " + "</span>" + "</td></tr>" +

            "<tr>" +
            "<td style='padding: 2px 5px 2px 5px'>نام و نام خانوادگی استاد : " + "<span style='font-weight:bold'>" + examDetail.Rows[0]["osname"].ToString() + "</span></td>" +
            "<td style='padding: 2px 5px 2px 5px'>شماره دانشجویی : " + "</td>" +
            "</tr>" +

            "<tr>" +
            "<td style='padding: 2px 5px 2px 5px'>تاریخ برگزاری امتحان : " + "<span style='font-weight:bold;text-align: right' dir='rtl'>" + exdate[2] + "</span>/<span style='font-weight:bold;text-align: right' dir='rtl'>" + exdate[1] + "</span>/<span style='font-weight:bold;text-align: right' dir='rtl'>" + exdate[0] + "</span>" + "</td>" +
            "<td style='padding: 2px 5px 2px 5px'>نام دانشجو : " + "</td>" +
            "</tr>" +

            "<tr>" +
            "<td style='padding: 2px 5px 2px 5px'>ساعت برگزاری امتحان : " + "<span style='font-weight:bold'>" + examDetail.Rows[0]["saatexam"].ToString() + "</span>" + "</td>" +
            "<td style='padding: 2px 5px 2px 5px'> نام خانوادگی دانشجو : " + "</td>" +
            "</tr>" +

            "<tr>" +
            "<td style='padding: 2px 5px 2px 5px'> رشته تحصیلی : " + "<span style='font-weight:bold'>" + examDetail.Rows[0]["nameresh"].ToString() + "</span>" + "</td>" +
            "<td style='padding: 2px 5px 2px 5px'>مقطع تحصیلی  : " + "<span style='font-weight:bold'>" + examDetail.Rows[0]["magh"].ToString() + "</span>" + "</td>" +
            "</tr>" +
            "</table>" +

            "<table style='width:100%;font-family:Tahoma; font-size: small; margin-right: 15px;'>" +
            "<tr><td style='font-size: 10px;width:50%;padding: 2px 15px 2px 5px'>" + "مجاز به استفاده از ماشین حساب" + " " + "<span style='font-weight:bold'>" + calc + "</span>" + "</td>" +
            "<td style='font-size: 10px;width:50%'>" + " امتحان جزوه " + "<span style='font-weight:bold'>" + Note + "</span>" + "</td>" +
            "</tr>" +
            book +
            "</table></td><td style='text-align: center; padding-top: 5px; padding-bottom: 5px;'>" +
            "<img width='100' height='130' src='http://service.iauec.ac.ir/University/Theme/images/Azad_University_logo.png' />" +
            "<p style=' font-family: iranNastaliq; text-align: center; color: #647ED1; padding-top: -3px; margin-top: -3px;'>واحد الکترونیکی</p>" +
            "</td></tr>" +
            "</table>" +
            "<table style='border-style: solid; border-width: 1px; width:99%; margin:10px; margin-top:50px; border-spacing: 0px; border-collapse: collapse;' border='1' dir='rtl' >" +
            // "<tr><td style='width:93%; width:74vh;' ></td><td style='font-family: tahoma; font-size: 10px; text-align: center;width:7%'>بارم نمرات</td></tr>" +
            "<tr><td style='width:93%;width:74vh;font-family: 'b nazanin';font-size: 12px'><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/></td><td style='font-family: tahoma; font-size: 10px; text-align: center;width:7%'>&nbsp;</td></tr>" +
            "</table></body>";


            RadEditor1.Content = content;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "PERSIAN_CHARTSET";
            HttpContext.Current.Response.ContentType = "application/msword";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            string strFileName = "ExamTemplate" + "-" + examDetail.Rows[0]["coursecode"].ToString() + ".doc";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);


            StringBuilder strHTMLContent = new StringBuilder();
            //strHTMLContent.Append("<body>");
            strHTMLContent.Append(RadEditor1.Content);
            //strHTMLContent.Append("</body>");

            HttpContext.Current.Response.Write(strHTMLContent);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Flush();

        }
        protected void btnSaveQ_Att_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (fileUploader.HasFile)
                {

                    string QExt = "pdf";
                    string AttExt = "jpg";
                    string fullPathQuestionFile = "";
                    string fullPathAttachFile = "";
                    string fullPathZipFile = "";

                    //helper class to transfer data to db
                    var Q_Att_Dto = new ExamQuestionDto();
                    ViewState["Qs_Attach22"] = null;

                    if (AttachUploader.HasFile)
                    {
                        AttExt = CommonBusiness.getFileExtension(AttachUploader.FileName).ToLower();
                    }

                    if ((QExt != "pdf" && QExt != "jpg") || (AttExt != "pdf" && AttExt != "jpg"))
                    {
                        rwm.RadAlert("فرمت صحیح انتخاب گردد", null, 100, "خطا", "");
                    }
                    else
                    {
                        int qId = int.Parse(Request.QueryString["qID"].ToString());
                        string did = Request.QueryString["did"].ToString();
                        string saatExam = Request.QueryString["examTime"].ToString();
                        string dateExam = Request.QueryString["examDate"].ToString();
                        int? cityIdQ2 = int.Parse(Request.QueryString["cityId"].ToString());
                        int statusQ2 = int.Parse(Request.QueryString["statusQ2"].ToString());
                        int statusQ1 = int.Parse(Request.QueryString["statusQ1"].ToString());

                        int? cityIDQ2 = null;
                        if (statusQ1 == 3 && statusQ2 != -1)
                            cityIDQ2 = cityIdQ2;

                        var examQ = Ebusiness.GetExamQuestionsbyDid(did,null , cityIDQ2);

                        if (examQ != null && bool.Parse(examQ.Rows[0]["TemplateDownloaded"].ToString()) == true)
                        {
                            var userID = Session[sessionNames.userID_StudentOstad].ToString();
                            //var questionID = examQ.AsEnumerable().Select(s => s.Field<int>("ID")).FirstOrDefault();
                            var relativePath = $"~/QueizPapers/{examQ.Rows[0]["Term"].ToString()}/{userID}/pdfFiles/{did.ToString()}";
                            string absolutePath = Server.MapPath(relativePath);

                            byte[] hashPassword = Convert.FromBase64String(examQ.Rows[0]["Password"].ToString());
                            string filepass = EncryptionClass.DecryptRJ256(hashPassword);

                            //string filepass = generaterandomstr(8);


                            //اگر امتحان لغو کلی شده برای همه شهرها
                            if (qId > 0 && (statusQ2 == 8 || statusQ2 == 11) && cityIdQ2 == -1)
                            {
                                fullPathZipFile = $"{absolutePath}/{did.ToString()}_canceled_1.zip";
                                //Q_Att_Dto.HashedPass = EncryptionClass.EncryptRJ(filepass);
                            }
                            //اگر امتحان لغو شده برای برای یه شهر خاص
                            else if (qId > 0 && (statusQ2 == 8 || statusQ2 == 11) && cityIdQ2 > 0)
                            {
                                fullPathZipFile = $"{absolutePath}/{did.ToString()}_canceled_2.zip";
                                //Q_Att_Dto.HashedPass = EncryptionClass.EncryptRJ(filepass);
                            }

                            else
                            {
                                fullPathZipFile = $"{absolutePath}/{did.ToString()}.zip";
                            }


                            //Directory.GetDirectories(absolutePath).ToList().ForEach(dir => Directory.Delete(dir, true));
                            //Directory.GetFiles(absolutePath, "*.*").ToList().ForEach(file => File.Delete(file));

                            if (fileUploader.HasFile)//آپلود سوالات
                            {
                                Q_Att_Dto.IsExistQuestionFile = true;
                                Q_Att_Dto.Did = did.ToString();
                                Q_Att_Dto.QuestionId = qId.ToString();//questionID.ToString();
                                Q_Att_Dto.UserId = userID;
                                Q_Att_Dto.HashedPass = examQ.Rows[0]["Password"].ToString();//EncryptionClass.EncryptRJ(filepass);
                                Q_Att_Dto.RelativePath = relativePath;

                                QExt = CommonBusiness.getFileExtension(fileUploader.FileName).ToLower();
                                if (!Directory.Exists(absolutePath))
                                    Directory.CreateDirectory(absolutePath);

                                fullPathQuestionFile = $"{absolutePath}/{did}.{QExt}";
                                if (QExt == "pdf" || QExt == "jpg")
                                {
                                    //var file = fileUploader.PostedFile;
                                    var qfSaved = SaveFile(fileUploader.PostedFile, absolutePath, did + "." + QExt);
                                    if (!qfSaved)
                                    {
                                        rwm.RadAlert("خطا در روند بارگذاری فایل سوالات! لطفا مجدداً بارگذاری نمائید.", null, 100, "", "");
                                        return;
                                    }
                                    using (ZipFile zip = new ZipFile())
                                    {
                                        zip.Password = filepass;
                                        FileStream stream = null;
                                        using (stream = new FileStream(fullPathQuestionFile, FileMode.Open, FileAccess.ReadWrite))
                                        {
                                            zip.AddEntry(did.ToString() + "." + QExt, stream);
                                            zip.Save(fullPathZipFile);
                                        }
                                    }
                                }
                            }

                            if (AttachUploader.HasFile)//آپلود سوالت پیوست
                            {
                                Q_Att_Dto.IsExistAttachFile = true;
                                AttExt = CommonBusiness.getFileExtension(AttachUploader.FileName).ToLower();
                                fullPathAttachFile = $"{absolutePath}/{did}Attached.{AttExt}";
                                if (AttExt == "jpg" || AttExt == "pdf")
                                {
                                    Q_Att_Dto.AttExt = AttExt;
                                    var att_fSaved = SaveAttachFile(AttachUploader.PostedFile, absolutePath, did + "Attached." + AttExt);
                                    if (!att_fSaved)
                                    {
                                        rwm.RadAlert("خطا در روند بارگذاری فایل  پیوست سوالات! لطفا مجدداً بارگذاری نمائید.", null, 100, "", "");
                                        return;
                                    }
                                    using (ZipFile zip = new ZipFile(fullPathZipFile))
                                    {
                                        zip.Password = filepass;
                                        FileStream stream = null;
                                        stream = new FileStream(fullPathAttachFile, FileMode.Open, FileAccess.ReadWrite);
                                        zip.AddEntry(did.ToString() + "Attached." + AttExt, stream);
                                        zip.Save(fullPathZipFile);
                                    }
                                }
                            }

                            Directory.GetFiles(absolutePath, "*.*").Where(w => !w.Contains(".zip")).ToList().ForEach(file => File.Delete(file));

                            if (File.Exists(fullPathZipFile))
                            {

                                var whiteTape = Server.MapPath("~/University/Theme/images/whitePaper.jpg");
                                var questioHeaderTemplate = Server.MapPath("~/University/Theme/images/QuestionHeaderTemplate.jpg");

                                var examQ_Detail = Ebusiness.Get_ExamdetailbyDid(did, null, cityIDQ2);//.AsEnumerable().Select(s=>s).FirstOrDefault();

                                var constQuestionFileInfo = new ExamStudentDTO();
                                constQuestionFileInfo.TypeNimsal = examQ_Detail.Rows[0]["typeNimsal"].ToString();
                                constQuestionFileInfo.CourseTitle = examQ_Detail.Rows[0]["namedars"].ToString();
                                constQuestionFileInfo.ProfossorFullName = examQ_Detail.Rows[0]["osname"].ToString();
                                constQuestionFileInfo.ExamDate = examQ_Detail.Rows[0]["dateexam"].ToString();
                                constQuestionFileInfo.ExamTime = examQ_Detail.Rows[0]["saatexam"].ToString();
                                constQuestionFileInfo.KeyCode = examQ_Detail.Rows[0]["keyCode"].ToString();
                                constQuestionFileInfo.ExamDuration = examQ_Detail.Rows[0]["examTime"].ToString();
                                constQuestionFileInfo.Calculator = examQ_Detail.Rows[0]["calculator"].ToString();
                                constQuestionFileInfo.Note = examQ_Detail.Rows[0]["note"].ToString();
                                constQuestionFileInfo.LowBook = examQ_Detail.Rows[0]["LowBook"].ToString();
                                constQuestionFileInfo.ClassCode = examQ_Detail.Rows[0]["ClassCode"].ToString();
                                constQuestionFileInfo.Grade = examQ_Detail.Rows[0]["magh"].ToString();
                                constQuestionFileInfo.Major = examQ_Detail.Rows[0]["nameresh"].ToString();


                                Ebusiness.ChangeTemplateOfQuestion(absolutePath, did, filepass, constQuestionFileInfo, questioHeaderTemplate, whiteTape, userID, cityIDQ2);

                                var lastPdfPath = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/{relativePath.Replace("~/", "")}/{userID}/{did}_Momtahen_{userID}_1.pdf?ts={DateTime.Now.Ticks}";
                                PdfFileSource = lastPdfPath;

                                ViewState["Qs_Attach22"] = Q_Att_Dto;

                                SectionManagement(false, false, true);

                            }
                            else
                            {
                                rwm.RadAlert("هیچ فایلی ارسال نشده است. لطفا مجددا فایل خود را ارسال نمایید", null, 100, "", "");
                            }
                        }
                        else
                        {
                            rwm.RadAlert("ابتدا فرم سوالات دانلود شود", null, 100, "خطا", "");
                        }
                    }
                }
                else
                {
                    rwm.RadAlert("فایل سوالات انتخاب شود ", null, 100, "خطا", "");
                }
            }

        }

        protected void btnBackToOptionPanel_ServerClick(object sender, EventArgs e)
        {
            SectionManagement(true, false, false);
        }


        protected void chk_ans1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ans1.Checked)
            {
                chk_ans2.Enabled = false;
                //chk_ans3.Enabled = false;
                chk_ans2.Checked = false;
                //chk_ans3.Checked = false;
            }
            else
            {
                chk_ans2.Enabled = true;
                //chk_ans3.Enabled = true;

            }
        }
        protected void chk_ans2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ans2.Checked)
            {
                chk_ans1.Enabled = false;
                chk_ans1.Checked = false;
            }
            else
            {
                //if (!chk_ans3.Checked)                
                //    chk_ans1.Enabled = true;
                chk_ans1.Enabled = true;
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int qId = int.Parse(Request.QueryString["qID"].ToString());
            string did = Request.QueryString["did"].ToString();
            int cityIdQ2 = int.Parse(Request.QueryString["cityId"].ToString());
            int statusQ2 = int.Parse(Request.QueryString["statusQ2"].ToString());
            //string saatExam = Request.QueryString["examTime"].ToString();
            //string dateExam = Request.QueryString["examDate"].ToString();

            if ((ExamQuestionDto)ViewState["Qs_Attach22"] != null)
            {
                var Q_Att = (ExamQuestionDto)ViewState["Qs_Attach22"];
                //سوال دوم
                if (qId > 0 && (statusQ2 == 8 || statusQ2 == 11 ) && cityIdQ2==-1)
                {
                    var relPath = $"{Q_Att.RelativePath}/{Q_Att.Did}_canceled_1.zip";
                    Ebusiness.UpdateExamQuestionsCancled(qId , relPath ,Q_Att.HashedPass,9 , cityIdQ2);                   
                    ViewState["Qs_Attach22"] = null;
                }
                else if (qId > 0 && (statusQ2 == 8 || statusQ2 == 11) && cityIdQ2 >0)
                {
                     var relPath = $"{Q_Att.RelativePath}/{Q_Att.Did}_canceled_2.zip";
                    Ebusiness.UpdateExamQuestionsCancled(qId, relPath, Q_Att.HashedPass, 9, cityIdQ2);
                    ViewState["Qs_Attach22"] = null;
                }
                else
                {
                    //log for question
                    if (Q_Att.IsExistQuestionFile)
                    {
                        //change q status=2
                        var relPath = $"{Q_Att.RelativePath}/{Q_Att.Did}.zip";
                        Ebusiness.UploadExamFile(relPath, Q_Att.HashedPass, Q_Att.Did, 2);
                        cmnb.InsertIntoStudentLog(Q_Att.UserId, DateTime.Now.ToShortTimeString(), 8, 24, Q_Att.QuestionId);
                    }

                    //log for attach
                    if (Q_Att.IsExistAttachFile)
                    {
                        Ebusiness.UploadAttachment(Q_Att.Did, $"{Q_Att.RelativePath}/{Q_Att.Did}Attached.{Q_Att.AttExt}");
                        cmnb.InsertIntoStudentLog(Q_Att.UserId, DateTime.Now.ToShortTimeString(), 8, 25, Q_Att.QuestionId);
                    }
                    else
                    {
                        Ebusiness.UploadAttachment(Q_Att.Did, "");
                    }

                    var absolutePath = Server.MapPath(Q_Att.RelativePath);
                    Directory.GetDirectories(absolutePath).ToList().ForEach(dir => Directory.Delete(dir, true));
                    //Directory.GetFiles(absolutePath, "*.*").ToList().ForEach(file => File.Delete(file));

                    ViewState["Qs_Attach22"] = null;
                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "closeRadWindow1", "CloseModal();", true);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            var Q_Att = (ExamQuestionDto)ViewState["Qs_Attach22"];
            var absolutePath = Server.MapPath(Q_Att.RelativePath);
            Directory.GetDirectories(absolutePath).ToList().ForEach(dir => Directory.Delete(dir, true));
            Directory.GetFiles(absolutePath, "*.*").Where(w => !w.Contains(".zip")).ToList().ForEach(file => File.Delete(file));

            PdfFileSource = null;
            ViewState["Qs_Attach22"] = null;
            SectionManagement(false, true, false);
        }

        private static void ConvertTopdf2(string name, string pre, string filepath, string fname)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            wordApp.Visible = false;

            // file from

            object filename = filepath + name + "." + pre; // input 
            // file to
            object newFileName = filepath + "pdfFiles\\" + fname + "\\" + name + ".pdf"; // output
            if (!Directory.Exists(filepath + "pdfFiles\\" + fname))
                Directory.CreateDirectory(filepath + "pdfFiles\\" + fname);
            object missing = System.Type.Missing;

            // open document
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref missing, ref missing, ref missing, ref missing, ref missing,
                           ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                           ref missing, ref missing, ref missing);

            // formt to save the file, this case PDF
            object formatoArquivo = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

            // changes in paper size

            doc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;

            // changes orietation paper
            doc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientPortrait;

            // other changes
            doc.PageSetup.LeftMargin = 10;
            doc.PageSetup.RightMargin = 10;


            // save file
            doc.SaveAs(ref newFileName, ref formatoArquivo, ref missing, ref missing, ref missing, ref missing, ref missing,
                           ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

            doc.Close(ref missing, ref missing, ref missing);

            wordApp.Quit(ref missing, ref missing, ref missing);

        }
        private static void ConvertTopdf(string name, string pre, string filepath)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            wordApp.Visible = false;

            // file from

            object filename = filepath + name + "." + pre; // input 
            // file to
            object newFileName = filepath + "pdfFiles\\" + name + "\\" + name + ".pdf"; // output
            if (!Directory.Exists(filepath + "pdfFiles\\" + name))
                Directory.CreateDirectory(filepath + "pdfFiles\\" + name);
            object missing = System.Type.Missing;

            // open document
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref missing, ref missing, ref missing, ref missing, ref missing,
                           ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                           ref missing, ref missing, ref missing);

            // formt to save the file, this case PDF
            object formatoArquivo = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

            // changes in paper size

            doc.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;

            // changes orietation paper
            doc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientPortrait;

            // other changes
            doc.PageSetup.LeftMargin = 10;
            doc.PageSetup.RightMargin = 10;


            // save file
            doc.SaveAs(ref newFileName, ref formatoArquivo, ref missing, ref missing, ref missing, ref missing, ref missing,
                           ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);

            doc.Close(ref missing, ref missing, ref missing);

            wordApp.Quit(ref missing, ref missing, ref missing);
            //using (ZipFile zip = new ZipFile())
            //{
            //    zip.Password = filepass;
            //    FileStream stream = null;
            //    stream = new FileStream(newFileName.ToString(), FileMode.Open, FileAccess.ReadWrite);
            //    zip.AddEntry(name + ".pdf", stream);

            //    zip.Save(filepath + "pdfFiles\\" + name + "\\" + name + ".zip");
            //    System.IO.File.Delete(newFileName.ToString());
            //}
        }
        protected void cvfileUploader_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            double filesize = fileUploader.FileContent.Length;
            if (filesize <= 4000000)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
        protected void cvAttachUploader_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            double filesize = AttachUploader.FileContent.Length;
            if (filesize <= 4000000)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }


    }
}