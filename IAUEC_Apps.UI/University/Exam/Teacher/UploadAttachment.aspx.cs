using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class UploadAttachment : System.Web.UI.Page
    {
        ExamBusiness eBusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (fileuploader.HasFile)
            {
                string did = Request.QueryString["did"].ToString();
                DataTable dt = new DataTable();
                dt = eBusiness.GetDidByCodeOstad(int.Parse(Session[sessionNames.userID_StudentOstad].ToString()));

                string pre = CommonBusiness.getFileExtension(fileuploader.FileName);
                string path = Server.MapPath("~/QueizPapers/" + dt.Rows[0]["tterm"].ToString() + "/" + Session[sessionNames.userID_StudentOstad].ToString() + "/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string Address = "../QueizPapers/" + dt.Rows[0]["tterm"].ToString() + "/" + Session[sessionNames.userID_StudentOstad].ToString() + "/" + did.ToString() + "Attached." + pre;

                //eBusiness.UploadExamFile(Address, "t123@456", did);
                eBusiness.UploadAttachment(did, Address);
                SaveFile(fileuploader.PostedFile, path, did + "Attached." + pre);
                ConvertTopdf(did + "Attached", pre, path, did);
                using (ZipFile zip = ZipFile.Read(path + "\\" + did + ".zip"))
                {
                    ZipEntry z = zip[did + "." + pre];
                    if (z == null)
                    {
                        FileStream stream = new FileStream(path + "\\" + did + "Attached" + "." + pre, FileMode.Open, FileAccess.ReadWrite);
                        zip.AddEntry(did + "Attached" + "." + pre, stream);
                        zip.Save();
                        System.IO.File.Delete(path + "\\" + did + "Attached" + "." + pre);
                    }
                    else
                        System.IO.File.Delete(path + "\\" + did + "Attached" + "." + pre);
                }

                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_StudentOstad].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 63, "آپلود پیوست");
                rwm.RadAlert("فایل با موفقیت آپلود شد", null, 50, "پیام", null);
                ScriptManager.RegisterStartupScript(this, GetType(), "btn_Save", "CloseModal();", true);
            }
        }
        private static void ConvertTopdf(string name, string pre, string filepath, string fname)
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
        void SaveFile(HttpPostedFile file, string path, string name)
        {
            string fileName = name;
            string pathToCheck = path + fileName;
            string tempfileName = "";
            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    tempfileName = counter.ToString() + fileName;
                    pathToCheck = path + tempfileName;
                    counter++;
                }
                fileName = tempfileName;

            }
            path += fileName;
            fileuploader.SaveAs(path);
        }
    }
}