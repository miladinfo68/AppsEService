using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using System.IO;
using IAUEC_Apps.Business.Common;
using Ionic.Zip;
using Telerik.Web.UI;
using IAUEC_Apps.DTO.University.Exam;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class UploadExamQuestion : System.Web.UI.Page
    {
        ExamBusiness eBusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();

        public string PdfFileSource;
        public string PdfMimeType { get; set; } = "application/pdf";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //#############################################
        protected void btn_Save_Click(object sender, EventArgs e)
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
                    ViewState["Qs_Attach"] = null;

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
                        string did =Request.QueryString["did"].ToString();
                        var examQ = eBusiness.GetExamQuestionsbyDid(did, null, null);

                        if (examQ != null && bool.Parse(examQ.Rows[0]["TemplateDownloaded"].ToString()) == true)
                        {
                            var userID = Session[sessionNames.userID_StudentOstad].ToString();
                            var questionID = examQ.AsEnumerable().Select(s => s.Field<int>("examQuestionID")).FirstOrDefault();
                            var relativePath = $"~/QueizPapers/{examQ.Rows[0]["Term"].ToString()}/{userID}/pdfFiles/{did}";
                            string absolutePath = Server.MapPath(relativePath);
                            fullPathZipFile = $"{absolutePath}/{did}.zip";
                            string filepass = generaterandomstr(8);

                            //Directory.GetDirectories(absolutePath).ToList().ForEach(dir => Directory.Delete(dir, true));
                            //Directory.GetFiles(absolutePath, "*.*").Where(w=>w.Contains("_canceled")).ToList().ForEach(file => File.Delete(file));

                            if (fileUploader.HasFile)//آپلود سوالات
                            {
                                Q_Att_Dto.IsExistQuestionFile = true;
                                Q_Att_Dto.Did = did.ToString();
                                Q_Att_Dto.QuestionId = questionID.ToString();
                                Q_Att_Dto.UserId = userID;
                                Q_Att_Dto.HashedPass = EncryptionClass.EncryptRJ(filepass);
                                Q_Att_Dto.RelativePath = relativePath;

                                QExt = CommonBusiness.getFileExtension(fileUploader.FileName).ToLower();
                                if (!Directory.Exists(absolutePath))
                                    Directory.CreateDirectory(absolutePath);

                                fullPathQuestionFile = $"{absolutePath}/{did.ToString()}.{QExt}";
                                if (QExt == "pdf" || QExt == "jpg")
                                {
                                    //var file = fileUploader.PostedFile;
                                    var qfSaved = SaveFile(fileUploader.PostedFile, absolutePath, did.ToString() + "." + QExt);
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
                                fullPathAttachFile = $"{absolutePath}/{did.ToString()}Attached.{AttExt}";
                                if (AttExt == "jpg" || AttExt == "pdf")
                                {
                                    Q_Att_Dto.AttExt = AttExt;
                                    var att_fSaved = SaveAttachFile(AttachUploader.PostedFile, absolutePath, did.ToString() + "Attached." + AttExt);
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

                                var examQ_Detail = eBusiness.Get_ExamdetailbyDid(did, null, null);

                                var constQuestionFileInfo = examQ_Detail.AsEnumerable().Select(x => new ExamStudentDTO()
                                {
                                    TypeNimsal = x.Field<string>("typeNimsal"),
                                    CourseTitle = x.Field<string>("namedars"),
                                    ProfossorFullName = x.Field<string>("osname"),
                                    ExamDate = x.Field<string>("dateexam"),
                                    ExamTime = x.Field<string>("saatexam"),
                                    KeyCode = x.Field<string>("keyCode"),
                                    ExamDuration = x.Field<string>("examTime"),
                                    Calculator = x.Field<string>("calculator"),
                                    Note = x.Field<string>("note"),
                                    LowBook = x.Field<string>("LowBook"),
                                    ClassCode = x.Field<string>("ClassCode").ToString(),
                                    Grade = x.Field<string>("magh").ToString(),
                                    Major = x.Field<string>("nameresh").ToString()
                                }).FirstOrDefault();

                                eBusiness.ChangeTemplateOfQuestion(absolutePath, did.ToString(), filepass, constQuestionFileInfo, questioHeaderTemplate, whiteTape, userID);

                                pnlMainQuestion_Att_Uploaded.Visible = false;
                                pnl_GeneratedPdf_MergedQuestion_Att.Visible = true;

                                var lastPdfPath = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/{relativePath.Replace("~/", "")}/{userID}/{did}_Momtahen_{userID}_1.pdf?ts={DateTime.Now.Ticks}";
                                PdfFileSource = lastPdfPath;

                                ViewState["Qs_Attach"] = Q_Att_Dto;

                                //var buff = System.IO.File.ReadAllBytes(asbPathMerged_Q_Att);
                                //string base64String = Convert.ToBase64String(buff, 0, buff.Length);
                                //SrcMergedFile = $"data:{PdfMimeType};base64,{base64String}";

                                //string scrp = "function f(){showLightBox(); $find(\"" +.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                //ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);



                                //ContentPlaceHolder mpContentPlaceHolder ;
                                //mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                                //mpContentPlaceHolder.Controls.Add(widnow1);


                                //var Encrypted_Password_File = EncryptionClass.EncryptRJ(filepass);
                                //eBusiness.UploadExamFile(relativePath + did.ToString() + ".zip", Encrypted_Password_File, did, 2);
                                //eBusiness.UploadAttachment(did, relativePath + did.ToString() + "Attached." + AttExt);//Address

                                //cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), 8, 22, questionID.ToString());
                                //cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), 8, 21, questionID.ToString());

                                //eBusiness.UpdateQueizStatus(2, did, "");
                                //ScriptManager.RegisterStartupScript(this, GetType(), "btn_Save", "CloseModal();", true);




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
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if ((ExamQuestionDto)ViewState["Qs_Attach"] != null)
            {
                var Q_Att = (ExamQuestionDto)ViewState["Qs_Attach"];
                //log for question
                if (Q_Att.IsExistQuestionFile)
                {
                    //change q status=2
                    eBusiness.UploadExamFile($"{Q_Att.RelativePath}/{Q_Att.Did}.zip", Q_Att.HashedPass, Q_Att.Did, 2);
                    cmnb.InsertIntoStudentLog(Q_Att.UserId, DateTime.Now.ToShortTimeString(), 8, 22, Q_Att.QuestionId);
                }

                //log for attach
                if (Q_Att.IsExistAttachFile)
                {
                    eBusiness.UploadAttachment(Q_Att.Did, $"{Q_Att.RelativePath}/{Q_Att.Did}Attached.{Q_Att.AttExt}");
                    cmnb.InsertIntoStudentLog(Q_Att.UserId, DateTime.Now.ToShortTimeString(), 8, 21, Q_Att.QuestionId);
                }
                //eBusiness.UpdateQueizStatus(2, int.Parse(Q_Att.Did), "");
                var absolutePath = Server.MapPath(Q_Att.RelativePath);
                Directory.GetDirectories(absolutePath).ToList().ForEach(dir => Directory.Delete(dir, true));
                ViewState["Qs_Attach"] = null;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "closeRadWindow1", "CloseModal();", true);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            pnlMainQuestion_Att_Uploaded.Visible = true;
            pnl_GeneratedPdf_MergedQuestion_Att.Visible = false;

            var Q_Att = (ExamQuestionDto)ViewState["Qs_Attach"];
            var absolutePath = Server.MapPath(Q_Att.RelativePath);
            Directory.GetDirectories(absolutePath).ToList().ForEach(dir => Directory.Delete(dir, true));
            Directory.GetFiles(absolutePath, "*.*").ToList().ForEach(file => File.Delete(file));

            PdfFileSource = null;
            ViewState["Qs_Attach"] = null;

            //var script= "function closeRadWinInParentPage() {var window = $find('<%=RadWindow1.ClientID %>'); window.close();}";
            //ScriptManager.RegisterStartupScript(this, GetType(), "closeRadWindow1", "script", true);
        }

        private static void ConvertTopdf(string name, string pre, string filepath, string filepass)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            wordApp.Visible = false;



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
            using (ZipFile zip = new ZipFile())
            {
                zip.Password = filepass;
                FileStream stream = null;
                stream = new FileStream(newFileName.ToString(), FileMode.Open, FileAccess.ReadWrite);
                zip.AddEntry(name + ".pdf", stream);

                zip.Save(filepath + "pdfFiles\\" + name + "\\" + name + ".zip");
                System.IO.File.Delete(newFileName.ToString());
            }
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
                DirectoryInfo dir = new DirectoryInfo(dirPath);
                dir.GetFiles().ToList().ForEach(f => { f.Delete(); });

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
            using (ZipFile zip = ZipFile.Read(filepath + "pdfFiles\\" + fname + "\\" + fname + ".zip"))
            {

                ZipEntry z = zip[name + "." + pre];
                if (z == null)
                {
                    FileStream stream = new FileStream(filepath + "pdfFiles\\" + fname + "\\" + name + ".pdf", FileMode.Open, FileAccess.ReadWrite);
                    zip.AddEntry(name + ".pdf", stream);
                    zip.Save();
                    System.IO.File.Delete(filepath + "pdfFiles\\" + fname + "\\" + name + ".pdf");
                }
                else
                    System.IO.File.Delete(filepath + "pdfFiles\\" + fname + "\\" + name + ".pdf");
            }






        }
        bool SaveAttachFile(HttpPostedFile file, string dirPath, string fileName)
        {
            bool done = true;
            try
            {
                //delete attached files in this folder;
                DirectoryInfo dir = new DirectoryInfo(dirPath);
                dir.GetFiles().Where(w => w.Name.Contains("Attached")).ToList().ForEach(f => { f.Delete(); });

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


