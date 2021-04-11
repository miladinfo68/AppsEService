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
    public partial class ExamQuestionPdfDownlowd : System.Web.UI.Page
    {
        ExamBusiness EBusiness = new ExamBusiness();
        byte[] fileByteArray;       
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

                
        protected void btnDownloadPdfExamQuestion_Click(object sender, EventArgs e)
        {
            if (ddlDate.SelectedIndex > 0 && ddlSans.SelectedIndex > 0 && ddlDid.SelectedIndex > 0)
            {
                var dt = EBusiness.GetAllDidsToTransferToLms(null, ddlDate.SelectedValue, ddlSans.SelectedValue, ddlDid.SelectedValue);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DownloadGeneratedPdf(dt.Rows[0]);
                }
            }
        }

        void DownloadGeneratedPdf(DataRow row)
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
            var shortNameFile = $"{did}.pdf";

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment;filename=" + shortNameFile);
            Response.BinaryWrite(fileByteArray);
            Response.End();
        }


    }
}