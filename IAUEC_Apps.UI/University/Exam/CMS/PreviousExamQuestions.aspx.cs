using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using System.Data;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class PreviousExamQuestions : System.Web.UI.Page
    {
        ExamBusiness eBusiness = new ExamBusiness();
        int Course = 0;
        string Term = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mId = Request.QueryString["id"].ToString();
                string[] id = mId.ToString().Split(new char[] { '@' });
                string menuId = "";
                for (int i = 0; i < id[1].Length; i++)
                {
                    string s = id[1].Substring(i + 1, 1);
                    if (s != "-")
                        menuId += s;
                    else
                        break;
                    Session[sessionNames.menuID] = menuId;
                }
                AccessControl.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
                FillForm();
            }
        }
        private void FillForm()
        {
            //-------Fill ddlCourse
            var courseDt = eBusiness.GetCourseListForTerms().AsEnumerable()
                .Select(s => new
                {
                    CourseName = s.Field<string>("CourseName") + " - " + s.Field<decimal>("CourseCode"),
                    CourseCode = s.Field<decimal>("CourseCode")
                });
            ddlCourse.DataSource = courseDt;
            ddlCourse.DataTextField = "CourseName";
            ddlCourse.DataValueField = "CourseCode";
            ddlCourse.DataBind();
            ddlCourse.Items.Insert(0, new RadComboBoxItem { Text = "انتخاب درس", Value = "0" });
            //---------------------

            //---------Fill ddlTerm
            var termList = eBusiness.GetPreviusTerms();
            ddlTerm.DataSource = termList;
            ddlTerm.DataBind();
            ddlTerm.Items.Insert(0, new RadComboBoxItem { Text = "انتخاب ترم", Value = "-1" });
            //---------------------
        }

        protected void btnShowQuestions_Click(object sender, EventArgs e)
        {
            Course = ddlCourse.SelectedItem != null ? Convert.ToInt32(ddlCourse.SelectedItem.Value) : 0;
            Term = ddlTerm.SelectedItem != null ? ddlTerm.SelectedItem.Value : "-1";
            pnlResult.Visible = true;
            grdResult.Rebind();
        }

        protected void grdResult_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var list = eBusiness.GetPreviusExamQuestions(Course, Term);
            Session["gridDS"] = list;
            grdResult.DataSource = list;
        }

        protected void grdResult_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "OpenQuestion":
                case "OpenAttachment":
                    var examQuestion = eBusiness.GetExamQuestionById(Convert.ToInt32(e.CommandArgument));
                    if (examQuestion.Rows.Count > 0)
                    {
                        string questionsFile = "";
                        byte[] fileByteArray;
                        string _filName = "";
                        string base64EncodedPDF = "";
                        var nocash = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                        var zipfilePath = examQuestion.Rows[0]["Address"].ToString();
                        zipfilePath = zipfilePath.Replace("..", "~");
                        var dynamicPath = zipfilePath.Substring(0, zipfilePath.LastIndexOf('/'));
                        var path = Server.MapPath(dynamicPath);
                        var filesInDir = Directory.GetFiles(path, "*.*");
                        var flg = DeleteImedatellyFiles(path);
                        if (!File.Exists(Server.MapPath(zipfilePath)))
                        {
                            var p = path.Replace("\\", " ==> ");
                            var str = string.Format("فایلی جهت نمایش در مسیر{0} یافت نشد", path);
                            rwm.RadAlert(str, null, 100, "پیام", "");
                            return;
                        }
                        ZipFile zip = ZipFile.Read(Server.MapPath(zipfilePath));
                        zip.Password = EncryptionClass.DecryptRJ256(Convert.FromBase64String(examQuestion.Rows[0]["Password"].ToString()));
                        zip.ExtractAll(path.ToString(), ExtractExistingFileAction.OverwriteSilently);
                        zip.Dispose();
                        filesInDir = Directory.GetFiles(path, "*.*");
                        string ext = "";
                        if (e.CommandName == "OpenQuestion")
                        {
                            var extsPath = filesInDir.Where(w => !w.Contains("Attached") && !w.Contains("zip")).FirstOrDefault()?.ToString();
                            if (extsPath == null) return;
                            ext = String.Join("", extsPath).Split('\\').Last().Split('.')[1];
                            if (!string.IsNullOrEmpty(ext))
                            {
                                ext = "." + ext;
                                questionsFile = dynamicPath + "/" + examQuestion.Rows[0]["did"].ToString() + ext;//?q=" + nocash;
                                _filName = Server.MapPath(questionsFile);

                                fileByteArray = System.IO.File.ReadAllBytes(_filName);
                                base64EncodedPDF = System.Convert.ToBase64String(fileByteArray);
                                Session["pdfPath"] = fileByteArray;
                                var c_type = GetMimeType(ext);
                                Session["contentType"] = c_type;

                            }
                            else
                                return;
                        }
                        else
                        {
                            var extsPath = filesInDir.Where(w => w.Contains("Attached")).FirstOrDefault()?.ToString();
                            if (extsPath == null) return;
                            ext = String.Join("", extsPath).Split('\\').Last().Split('.')[1];
                            if (!string.IsNullOrEmpty(ext))
                            {
                                ext = "." + ext;
                                questionsFile = dynamicPath + "/" + examQuestion.Rows[0]["did"].ToString() + "Attached" + ext;//?q=" + nocash;
                                _filName = Server.MapPath(questionsFile);

                                fileByteArray = System.IO.File.ReadAllBytes(_filName);
                                base64EncodedPDF = System.Convert.ToBase64String(fileByteArray);
                                Session["pdfPath"] = fileByteArray;
                                var c_type = GetMimeType(ext);
                                Session["contentType"] = c_type;

                            }
                            else
                                return;
                        }

                        DeleteImedatellyFiles(path);
                        var clientFunction = "openShowFileInPopup('ShowFilePrevious.aspx?q=" + nocash + "');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", clientFunction, true);
                    }
                    break;
            }
        }
        private bool DeleteImedatellyFiles(string path)
        {
            try
            {
                var allFilesInDirectory = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                var files = allFilesInDirectory.Where(s => s.EndsWith(".pdf") || s.EndsWith(".jpg") || s.EndsWith(".jpeg"));
                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
            catch (Exception x)
            {
                return false;
                //throw x;
            }
            return true;
        }
        private string GetMimeType(string ext)
        {
            string mimeType = "application/unknown";
            // ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        protected void grdResult_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Button btnOpenAttach = e.Item.FindControl("btnOpenAttach") as Button;
                HiddenField hdnQuestionId = e.Item.FindControl("hdnQuestionId") as HiddenField;
                if (btnOpenAttach != null && hdnQuestionId != null)
                {
                    var examQuestion = ((DataTable)Session["gridDS"]).AsEnumerable().Where(w => w.Field<int>("ID") == Convert.ToInt32(hdnQuestionId.Value)); //eBusiness.GetExamQuestionById(Convert.ToInt32(hdnQuestionId.Value));
                    if (examQuestion.Count() > 0 && !string.IsNullOrEmpty(examQuestion.FirstOrDefault().Field<string>("AttachmentAddress")))
                        btnOpenAttach.Visible = true;
                }
            }
            else if (e.Item is GridFooterItem)
                Session["gridDS"] = null;
        }
    }
}