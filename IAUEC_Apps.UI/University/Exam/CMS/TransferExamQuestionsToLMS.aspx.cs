using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.DTO.University.Exam;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class TransferExamQuestionsToLMS : System.Web.UI.Page
    {
        ExamBusiness EBusiness = new ExamBusiness();
        byte[] fileByteArray;
        static List<TransferdObject> list;
        readonly string LmsAzmoon = ConfigurationManager.AppSettings["LmsAzmoon"];

        //private static string _codeTerm =
        //    string.Concat((ConfigurationManager.AppSettings["Exam_Term"]).ToString().Split('-')[0],
        //        (ConfigurationManager.AppSettings["Exam_Term"]).ToString().Split('-')[2]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //AccessControl.MenuId = Session[sessionNames.menuID].ToString();
                //AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();

                var choose = new ListItem { Text = "انتخاب کنید", Value = "-1" };
                DataTable dtExamDate = new DataTable();
                dtExamDate = EBusiness.Get_Exam_dateexam(true);
                ddlDate.DataSource = dtExamDate;
                ddlDate.DataTextField = "dateexam";
                ddlDate.DataValueField = "dateexam";
                ddlDate.DataBind();
                ddlDate.Items.Insert(0, choose);
                ddlSans.Items.Insert(0, choose);
                ddlDid.Items.Insert(0, choose);
            }
        }

        protected async void btnTransfer_Click(object sender, EventArgs e)
        {
            //btnTransfer.Enabled = false;
            //System.Threading.Thread.Sleep(10000);
            if (ddlDate.SelectedIndex > 0)
            {
                var ddl_Sans = ddlSans.SelectedIndex > 0 ? ddlSans.SelectedValue : null;
                string ddl_Did = null;
                if (ddlDid.SelectedIndex > 0)
                    ddl_Did = ddlDid.SelectedValue;

                var dt = EBusiness.GetAllDidsToTransferToLms(null, ddlDate.SelectedValue, ddl_Sans, ddl_Did);

                if (dt != null && dt.Rows.Count > 0)
                {
                    PdfTransferOption item;
                    list = new List<TransferdObject>();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var bytArr = GeneratPdf(dt.Rows[i]);
                        if (bytArr != null)
                        {
                            var courseCode = dt.Rows[i]["courseCode"].ToString();
                            var lmsCourseCode = dt.Rows[i]["lmsCourseCode"].ToString();
                            var courseName = dt.Rows[i]["courseName"].ToString();
                            var dateExam = dt.Rows[i]["dateexam"].ToString().ToGregorian();
                            var timeExam = dt.Rows[i]["saatexam"].ToString();
                            var timeDuration = int.Parse(dt.Rows[i]["ExamTime"].ToString());
                            var courseClassCode= dt.Rows[i]["course_classCode"].ToString(); 

                            var hour = int.Parse(timeExam.Substring(0, 2));
                            var minute = int.Parse(timeExam.Substring(3, 2));

                            var standardDate = new DateTime(dateExam.Year, dateExam.Month, dateExam.Day, hour, minute, 0);
                            //var standardDate = new DateTime(dateExam.Year, dateExam.Month, dateExam.Day, hour, minute, 0, DateTimeKind.Utc);
                            //Int32 unixTimestampStartDate = (Int32)(standardDate.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;               
                            //var _endDatefalke = standardDate.AddMinutes(timeDuration);
                            var _endDate = standardDate.AddMinutes(timeDuration);
                            //var a= _endDate.Subtract(_endDatefalke).TotalMinutes;

                            //var endDate = new DateTime(_endDate.Year, _endDate.Month, _endDate.Day, _endDate.Hour, _endDate.Minute, 0, DateTimeKind.Utc);
                            //Int32 unixTimestampEndDate = (Int32)(endDate.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                            //var sub = unixTimestampEndDate - unixTimestampStartDate;

                            item = new PdfTransferOption();
                            //item.ShortName = courseCode;
                            //item.Name = string.Format("{0} {1}_{2}", "دریافت سوالات و بارگذاری پاسخنامه درس ", courseName, courseCode);

                            item.ShortName = lmsCourseCode;
                            item.Name = string.Format("{0} {1}_{2}", "دریافت سوالات و بارگذاری پاسخنامه درس ", courseName, courseClassCode);

                            item.Start = standardDate.ToString("yyyy-MM-dd HH:mm:ss"); //unixTimestampStartDate.ToString();
                            item.End = _endDate.ToString("yyyy-MM-dd HH:mm:ss");//unixTimestampEndDate.ToString();
                            item.FileBytes = bytArr;
                            //item.FileName = $"{dt.Rows[i]["coursecode"].ToString()}_Momtahen_{ Session[sessionNames.userID_Karbar].ToString()}.pdf";
                            item.FileName = $"{lmsCourseCode}_Momtahen_{ Session[sessionNames.userID_Karbar].ToString()}.pdf";
                            item.Uri = LmsAzmoon;


                            var res = await Upload(item);

                        }
                    }

                    if (list.Count > 0)
                    {
                        //Control table = pnlTransferedDids.FindControl("tblTransferedDids");

                        HtmlTableRow row;
                        HtmlTableCell cell;

                        row = new HtmlTableRow();

                        cell = new HtmlTableCell();
                        cell.InnerText = "مشخصه کلاس";
                        row.Cells.Add(cell);

                        cell = new HtmlTableCell();
                        cell.InnerText = "وضعیت";
                        row.Cells.Add(cell);
                        tblTransferedDids.Rows.Add(row);

                        foreach (var obj in list)
                        {
                            row = new HtmlTableRow();

                            cell = new HtmlTableCell();
                            cell.InnerText = obj.Did;
                            row.Cells.Add(cell);

                            cell = new HtmlTableCell();
                            cell.InnerText = obj.Status ? "ارسال فایل موفق" : "ارسال فایل ناموفق";
                            row.Cells.Add(cell);

                            tblTransferedDids.Rows.Add(row);
                        }
                    }

                }

            }
            hdnRunFlag.Value = "0";
        }


        protected void ddlSans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSans.SelectedIndex > 0)
            {
                ddlDid.Items.Clear();
                DataTable dtDid = EBusiness.GetDidByDateAndSans(ddlDate.SelectedItem.Value, ddlSans.SelectedItem.Value);
                ddlDid.DataSource = dtDid;
                ddlDid.DataTextField = "did";
                ddlDid.DataValueField = "did";
                ddlDid.DataBind();
                ddlDid.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
            }
            else
            {
                ddlDid.Items.Clear();
                ddlDid.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
            }
        }

        protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDate.SelectedIndex > 0)
            {
                ddlSans.Items.Clear();
                DataTable dtSans = EBusiness.GetSansByDate(ddlDate.SelectedItem.Value);
                ddlSans.DataSource = dtSans;
                ddlSans.DataTextField = "saatexam";
                ddlSans.DataValueField = "saatexam";
                ddlSans.DataBind();
                ddlSans.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
            }
            else
            {
                ddlSans.Items.Clear();
                ddlSans.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
                ddlDid.Items.Clear();
                ddlDid.Items.Insert(0, new ListItem { Text = "انتخاب کنید", Value = "-1" });
            }
        }


        byte[] GeneratPdf(DataRow row)
        {
            string did = "-1";
            string QuesId = "-1";
            int? cityId = null;
            int q2Status = -1;
            int q1Status = -1;
            string fullPathZipFile;
            //===================================
            did = row["coursecode"].ToString();
            QuesId = row["QuestionId"].ToString();
            cityId = int.Parse(row["cityId"].ToString());
            q2Status = int.Parse(row["q2Status"].ToString());
            q1Status = int.Parse(row["Status"].ToString());

            int? cityIDQ2 = null;
            if (q2Status != -1 && q1Status == 3)
                cityIDQ2 = cityId;

            var quizPapere = EBusiness.ShowQueizPaperByDid(did, cityIDQ2);


            var dynamicPath = "~/QueizPapers/" + quizPapere.Rows[0]["tterm"].ToString() + "/" + quizPapere.Rows[0]["code_ostad"].ToString() + "/pdffiles/" + quizPapere.Rows[0]["coursecode"].ToString();
            string path = Server.MapPath(dynamicPath);

            if (cityIDQ2 == -1)
            {
                fullPathZipFile = $"{path}/{did.ToString()}_canceled_1.zip";
            }
            else if (cityIDQ2 > 0)
            {
                fullPathZipFile = $"{path}/{did.ToString()}_canceled_2.zip";
            }
            else
            {
                fullPathZipFile = $"{path}/{did.ToString()}.zip";
            }

            byte[] base64HashesPassword = Convert.FromBase64String(quizPapere.Rows[0]["Password"].ToString());
            string pass = EncryptionClass.DecryptRJ256(base64HashesPassword);

            var item = EBusiness.GetExamQuestionsbyDid(did, null, cityIDQ2);

            var whiteTape = Server.MapPath("~/University/Theme/images/whitePaper.jpg");
            var questioHeaderTemplate = Server.MapPath("~/University/Theme/images/QuestionHeaderTemplate.jpg");
            var examQ_Detail = EBusiness.Get_ExamdetailbyDid(did, null, cityIDQ2);

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



            var userID = Session[sessionNames.userID_Karbar].ToString();
            EBusiness.ChangeTemplateOfQuestion(path, did.ToString(), pass, constQuestionFileInfo, questioHeaderTemplate, whiteTape, userID, cityIDQ2);
            var generatedPdfPath = (Server.MapPath($"{dynamicPath}/{userID}/{did}_Momtahen_{userID}_1.pdf"));

            fileByteArray = System.IO.File.ReadAllBytes(generatedPdfPath);
            //Directory.GetDirectories(path).ToList().ForEach(dir => Directory.Delete(dir, true));
            //File.Delete(generatedPdfPath);
            return fileByteArray;

        }


        private async static Task<string> Upload(PdfTransferOption option)
        {
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(new StringContent(option.Name, Encoding.UTF8), "name");
                formData.Add(new StringContent(option.ShortName, Encoding.UTF8), "shortname");
                //formData.Add(new StringContent(_codeTerm + option.ShortName, Encoding.UTF8), "shortname");
                //formData.Add(new StringContent("123456789", Encoding.UTF8), "shortname");
                formData.Add(new StringContent(option.Start, Encoding.UTF8), "start");
                formData.Add(new StringContent(option.End, Encoding.UTF8), "end");

                if (!(option.FileBytes is null))
                    formData.Add(new ByteArrayContent(option.FileBytes), "repo_upload_file", option.FileName);
                if (!(option.FileStream is null))
                    formData.Add(new StreamContent(option.FileStream), "repo_upload_file", option.FileName);
                var response = await client.PostAsync(option.Uri, formData);
                var status200 = response.IsSuccessStatusCode;
                if (!status200) return null;
                var res = await response.Content.ReadAsStringAsync();
                string jsonResult = JsonConvert.DeserializeObject<TransferdObjectResult>(res).Result;
                if (!string.IsNullOrEmpty(jsonResult) && CommonBusiness.IsNumeric(jsonResult) && decimal.Parse(jsonResult) > 0)
                {
                    list.Add(new TransferdObject() { Did = option.ShortName, Status = response.IsSuccessStatusCode });
                }
                return res;
            }
        }



        public class PdfTransferOption
        {
            public string Name { get; set; }
            public string ShortName { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public byte[] FileBytes { get; set; }
            public Stream FileStream { get; set; }
            public string FileName { get; set; }
            public string Uri { get; set; }
            public PdfTransferOption()
            {
                FileStream = null;
                FileBytes = null;
            }
        }

        public class TransferdObject
        {
            public string Did { get; set; }
            public bool Status { get; set; }
        }

        public class TransferdObjectResult
        {
            public string Result { get; set; }
            public string Error { get; set; }
        }

    }
}