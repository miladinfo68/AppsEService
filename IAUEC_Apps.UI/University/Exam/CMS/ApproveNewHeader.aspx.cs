using IAUEC_Apps.Business.university.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using IAUEC_Apps.Business.Common;
using System.IO;
using Ionic.Zip;
using IAUEC_Apps.DTO.University.Exam;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ApproveNewHeader : System.Web.UI.Page
    {
        ExamBusiness examBusiness = new ExamBusiness();
        bool BindGrid = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                t.Text = "بررسی سربرگ سوالات";
                FillPage();
            }
        }

        private void FillPage()
        {
            //Fill College DDL
            ddlCollege.DataSource = examBusiness.GetAllDaneshkade();
            ddlCollege.DataTextField = "namedanesh";
            ddlCollege.DataValueField = "id";
            ddlCollege.DataBind();
            ddlCollege.Items.Add(new ListItem("همه", "0"));
            ddlCollege.Items[ddlCollege.Items.Count - 1].Selected = true;

            //Fill Date DDL
            ddlExamDate.DataSource = examBusiness.Get_Exam_dateexam();
            ddlExamDate.DataTextField = "dateexam";
            ddlExamDate.DataValueField = "dateexam";
            ddlExamDate.DataBind();
            ddlExamDate.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });

        }

        protected void grd_Class_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void grd_Class_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!BindGrid && e.RebindReason != Telerik.Web.UI.GridRebindReason.ExplicitRebind) return;
            var ds = examBusiness.ShowQuiezPaperHeader(
                int.Parse(ddlCollege.SelectedValue.ToString())
                , int.Parse(ddlStatus.SelectedValue.ToString())
                , (ddlExamDate.SelectedIndex > 0 ? ddlExamDate.SelectedItem.Text : "")
                , (ddlExamDate.SelectedIndex > 0 ? ddlExamTime.SelectedItem.Text : ""));
            //if (ddlExamDate.SelectedIndex > 0)
            //    grd_Class.DataSource = ds.AsEnumerable().Where(w => w.Field<string>("dateexam") == ddlExamDate.SelectedItem.Text && w.Field<string>("saatexam") == ddlExamTime.SelectedItem.Text);
            //else
            grd_Class.DataSource = ds.AsEnumerable();
            GridFilterMenu menu = grd_Class.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }

                }
            }
            BindGrid = false;
        }

        protected void grd_Class_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Filter") return;

            string did = "0";
            var dtdet = new DataTable();
            var path = string.Empty;
            var absPath = string.Empty;
            var pass = string.Empty;
            var hdnPassword = (HiddenField)e.Item.FindControl("hdnPassword");


            if (e.CommandName == "ShowNewHeaderForAll" || e.CommandName == "ShowNewHeader" || e.CommandName == "ShowOldHeader")
            {
                did = e.CommandArgument.ToString();
                dtdet = examBusiness.Get_ExamdetailbyDid(did);
                path = $"QueizPapers/{dtdet.Rows[0]["tterm"].ToString()}/{dtdet.Rows[0]["code_ostad"].ToString()}/pdffiles/{e.CommandArgument.ToString()}";
                absPath = Server.MapPath($"~/{path}");
                var base64HashesPassword = Convert.FromBase64String(hdnPassword.Value);
                pass = EncryptionClass.DecryptRJ256(base64HashesPassword);
            }

            var QuestionId = Convert.ToInt32(e.CommandArgument.ToString());
            var constQuestionFileInfo = new ExamStudentDTO();
            var whiteTape = Server.MapPath("~/University/Theme/images/whitePaper.jpg");
            var questioHeaderTemplate = Server.MapPath("~/University/Theme/images/QuestionHeaderTemplate.jpg");
            var userID = Session[sessionNames.userID_Karbar].ToString();


            switch (e.CommandName)
            {
                case "ShowNewHeaderForAll":

                    constQuestionFileInfo = dtdet.AsEnumerable().Select(x => new ExamStudentDTO()
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
                        ClassCode = x.Field<string>("ClassCode").ToString()

                    }).FirstOrDefault();

                    var dt = examBusiness.ExamAnswerSheetbyDid(did, int.Parse(Session[sessionNames.userID_Karbar].ToString()));
                    var studentList = dt.AsEnumerable().Select(x => new ExamStudentDTO()
                    {
                        FirstName = x.Field<string>("stFirstName"),
                        LastName = x.Field<string>("stLastName"),
                        StudentCode = x.Field<string>("stcode"),
                        Grade = x.Field<string>("magh"),
                        Major = x.Field<string>("nameresh"),
                        SeatHeader = x.Field<string>("SeatHeader"),
                        SeatNumber = x.Field<int?>("SeatNumber")
                    }).ToList();


                    examBusiness.GeneratePdfQuestionForStudents(absPath, did.ToString(), pass, questioHeaderTemplate, whiteTape, userID , constQuestionFileInfo, studentList);
                    Session["BigFile"] = true;
                    ShowFiles($"{path}/{userID}/{did}_Momtahen_{userID}_2.zip", pass);
                    break;

                case "ShowNewHeader":

                    constQuestionFileInfo = dtdet.AsEnumerable().Select(x => new ExamStudentDTO()
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
                        ClassCode = x.Field<string>("ClassCode").ToString()

                    }).FirstOrDefault();


                    examBusiness.ChangeTemplateOfQuestion(absPath, e.CommandArgument.ToString(), pass, constQuestionFileInfo, questioHeaderTemplate, whiteTape, userID);
                    ShowFiles($"{path}/{userID}/{did}_Momtahen_{userID}_1.zip", pass);
                    break;

                case "ShowOldHeader":

                    constQuestionFileInfo = dtdet.AsEnumerable().Select(x => new ExamStudentDTO()
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
                        ClassCode = x.Field<string>("ClassCode").ToString()

                    }).FirstOrDefault();


                    examBusiness.ChangeTemplateOfQuestion(absPath, e.CommandArgument.ToString(), pass, constQuestionFileInfo, questioHeaderTemplate, whiteTape, userID);
                    ShowFiles($"{path}/{did}.zip", pass);
                    break;

                case "ApproveNewHeader":
                    examBusiness.SetApproveNewHeader(QuestionId, true);
                    BindGrid = true;
                    grd_Class.Rebind();
                    break;
                case "RejectNewHeader":
                    examBusiness.SetApproveNewHeader(QuestionId, false);
                    BindGrid = true;
                    grd_Class.Rebind();
                    break;
            }
        }

        private void ShowFiles(string zipfilePath, string pass)
        {
            byte[] fileByteArray;
            string _filName = "";
            var nocash = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
            var path = Server.MapPath("~/" + zipfilePath.Substring(0, zipfilePath.LastIndexOf('/')));
            var filesInDir = Directory.GetFiles(path, "*.*");
            if (!File.Exists(Server.MapPath("~/" + zipfilePath)))
            {
                var p = path.Replace("\\", " ==> ");
                var str = string.Format("فایلی جهت نمایش در مسیر{0} یافت نشد", p);
                rwm.RadAlert(str, null, 100, "پیام", "");
                return;
            }
            ZipFile zip = ZipFile.Read(Server.MapPath("~/" + zipfilePath));
            zip.Password = pass;
            zip.ExtractAll(path.ToString(), ExtractExistingFileAction.OverwriteSilently);
            zip.Dispose();
            filesInDir = Directory.GetFiles(path, "*.*");
            string ext = "";


            var extsPath = filesInDir.Where(w => !w.Contains("Attached") && !w.Contains("zip")).FirstOrDefault()?.ToString();
            if (extsPath == null) return;
            ext = String.Join("", extsPath).Split('\\').Last().Split('.')[1];
            if (!string.IsNullOrEmpty(ext))
            {
                ext = "." + ext;
                _filName = path + "\\" + extsPath.Split('\\').Last();

                fileByteArray = System.IO.File.ReadAllBytes(_filName);
                Session["pdfPath"] = fileByteArray;
                var c_type = GetMimeType(ext);
                if (string.IsNullOrEmpty(c_type))
                {
                    rwm.RadAlert("فرمت فایل ارسالی صحیح نیست", null, 100, "پیام", "");
                    return;
                }
                Session["contentType"] = c_type;

            }
            else
                return;



            DeleteImmediatelyFiles(path);
            var clientFunction = "openShowFileInPopup('ShowFile.aspx?q=" + nocash + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popuup", clientFunction, true);
        }
        private bool DeleteImmediatelyFiles(string path)
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
            }
            return true;
        }

        private string GetMimeType(string ext)
        {
            switch (ext)
            {
                case ".pdf":
                    return "application/pdf";
                case ".jpeg":
                case ".jpg":
                    return "image/jpeg";
                default: return string.Empty;
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void btnFillGrid_Click(object sender, EventArgs e)
        {
            BindGrid = true;
            grd_Class.Rebind();
        }

        protected void ddlExamDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExamDate.SelectedValue != "-1")
            {
                //Fill Time DDL
                ddlExamTime.DataSource = examBusiness.GetSaatExamByDateExam(ddlExamDate.SelectedItem.Text);
                ddlExamTime.DataTextField = "saatexam";
                ddlExamTime.DataValueField = "saatexam";
                ddlExamTime.DataBind();
            }
            else
            {
                ddlExamTime.Items.Clear();
            }
        }


    }
}