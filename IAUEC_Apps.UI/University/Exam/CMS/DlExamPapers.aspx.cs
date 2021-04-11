using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.Business.university.Request;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Exam;
using System.Web.UI.HtmlControls;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class DlExamPapers : System.Web.UI.Page
    {
        ExamBusiness EBusiness = new ExamBusiness();
        RequestStudentCartBusiness CBusiness = new RequestStudentCartBusiness();
        CommonBusiness CB = new CommonBusiness();
        LoginBusiness logBuisness = new LoginBusiness();
        protected void Page_Load(object sender, EventArgs e)
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
        }

        protected void grd_CourseList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            //Session[sessionNames.userID_Karbar] = "1074";           
            var userID = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            var dt = EBusiness.QuiezPaperForDL(userID);
            if (dt.Rows.Count > 0)
            {
                grd_CourseList.DataSource = dt;
            }
            else { rwm.RadAlert("ممتحن گرامی زمان دانلود سوالات در حال حاضر فرا نرسیده است", null, 100, "پیام", "CallBackConfirm"); }

        }

        DataTable getCurrentUserRoles(string userId)
        {
            var dt = logBuisness.Get_UserRoles(userId);
            return dt;
        }



        protected void grd_CourseList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //GridDataItem dataItem = e.Item as GridDataItem;
                //GridDataItem item = (GridDataItem)e.Item;

                Button btnDlQuestionSinglePage = e.Item.FindControl("btnDlQuestionSinglePage") as Button;
                Button btnDlQuestionMergedFiles = e.Item.FindControl("btnDlQuestionMergedFiles") as Button;
                Button btnDlQuestionMainFormat = e.Item.FindControl("btnDlQuestionMainFormat") as Button;

                TextBox lbl_Password = e.Item.FindControl("lbl_Password") as TextBox;
                HiddenField hdn_Pass = e.Item.FindControl("hdn_Pass") as HiddenField;

                HiddenField hdn_IsActive = e.Item.FindControl("hdn_IsActive") as HiddenField;
                HiddenField hdn_ApproveNewHeader = e.Item.FindControl("hdn_ApproveNewHeader") as HiddenField;

                var dtUserRoles = getCurrentUserRoles(Session[sessionNames.userID_Karbar].ToString());
                if (dtUserRoles != null && dtUserRoles.Rows.Count > 0)
                {
                    //var isRole_1_32_33 = false;
                    //for (int i = 0; i < dtUserRoles.Rows.Count; i++)
                    //{
                    //    if (dtUserRoles.Rows[i]["RoleId"].ToString() == "1"
                    //        || dtUserRoles.Rows[i]["RoleId"].ToString() == "32"
                    //        || dtUserRoles.Rows[i]["RoleId"].ToString() == "33")
                    //    {
                    //        isRole_1_32_33 = true;
                    //        break;
                    //    }
                    //}
                    //if (isRole_1_32_33)
                    //{
                    //    if (!string.IsNullOrEmpty(hdn_ApproveNewHeader.Value) && hdn_ApproveNewHeader.Value == "False")
                    //    {
                    //        btnDlQuestionMainFormat.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        btnDlQuestionSinglePage.Visible = true;
                    //    }
                    //}
                    //else
                    {
                        //TODO if  ApproveNewHeader value is false  then just show old header
                        if (!string.IsNullOrEmpty(hdn_ApproveNewHeader.Value) && hdn_ApproveNewHeader.Value == "False")
                        {
                            btnDlQuestionMainFormat.Visible = true;
                        }
                        else if (string.IsNullOrEmpty(hdn_ApproveNewHeader.Value) || hdn_ApproveNewHeader.Value == "True")  //TODO else ApproveNewHeader is true or Null then 
                        {
                            //TODO if permision is true show new header and total new header
                            if (!string.IsNullOrEmpty(hdn_IsActive.Value) && hdn_IsActive.Value == "1")
                            {
                                btnDlQuestionSinglePage.Visible = true;
                                btnDlQuestionMergedFiles.Visible = true;
                            }
                            else if (string.IsNullOrEmpty(hdn_IsActive.Value) || hdn_IsActive.Value == "0")//TODO else permision is false or null total new header
                            {
                                btnDlQuestionMergedFiles.Visible = true;
                            }
                        }
                    }
                }
                else
                {
                    //TODO if  ApproveNewHeader value is false  then just show old header
                    if (!string.IsNullOrEmpty(hdn_ApproveNewHeader.Value) && hdn_ApproveNewHeader.Value == "False")
                    {
                        btnDlQuestionMainFormat.Visible = true;
                    }
                    else if (string.IsNullOrEmpty(hdn_ApproveNewHeader.Value) || hdn_ApproveNewHeader.Value == "True")  //TODO else ApproveNewHeader is true or Null then 
                    {
                        //TODO if permision is true show new header and total new header
                        if (!string.IsNullOrEmpty(hdn_IsActive.Value) && hdn_IsActive.Value == "1")
                        {
                            btnDlQuestionSinglePage.Visible = true;
                            btnDlQuestionMergedFiles.Visible = true;
                        }
                        else if (string.IsNullOrEmpty(hdn_IsActive.Value) || hdn_IsActive.Value == "0")//TODO else permision is false or null total new header
                        {
                            btnDlQuestionMergedFiles.Visible = true;
                        }
                    }
                }



                var answerSheetType = string.Empty;
                var rrr = (DataRowView)e.Item.DataItem;
                if (Convert.ToBoolean(rrr["AnswerSheet1"]))
                {
                    answerSheetType += "پاسخگویی در برگه سوالات";
                }

                if (Convert.ToBoolean(rrr["AnswerSheet2"]))
                {
                    if (answerSheetType.Length > 0)
                        answerSheetType += " | ";
                    answerSheetType += " تشریحی ";
                }

                if (Convert.ToBoolean(rrr["AnswerSheet3"]))
                {
                    if (answerSheetType.Length > 0)
                        answerSheetType += " | ";
                    answerSheetType += " تستی ";
                }

                e.Item.Cells[9].Text = answerSheetType;                
                //TableCell cell = dataItem["did"];
                string tt = (hdn_Pass.Value.ToString());
                byte[] str = Convert.FromBase64String(tt);

                string pass = EncryptionClass.DecryptRJ256(str);
                lbl_Password.Text = pass;  
            }
        }

        protected void grd_CourseList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string ipaddress;
            ipaddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipaddress == "" || ipaddress == null)
                ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            var ip = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
            && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
           ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
           : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (ip.Contains(","))
                ip = ip.Split(',').First();

            var cmdArgs = e.CommandArgument.ToString().Split(',');
            var did = cmdArgs[0];
            var cityIdQ2 = int.Parse(cmdArgs[1]);
            var statusQ1 = int.Parse(cmdArgs[2]);
            var statusQ2 = int.Parse(cmdArgs[3]);

            int? cityIDQ2 = null;
            if (statusQ1 == 3 && statusQ2 != -1)
                cityIDQ2 = cityIdQ2;



            var mainFormatClick = false;           
            var examQ_Detail = EBusiness.Get_ExamdetailbyDid(did,null ,cityIDQ2);
            if (e.CommandName == "DlQuestionMainFormat")//فرمت اصلی سوالات بدون تغیرات
            {
                var relativePath = "QueizPapers/" + examQ_Detail.Rows[0]["tterm"].ToString() + "/" + examQ_Detail.Rows[0]["code_ostad"].ToString() + "/pdffiles/" + did.ToString();
                //    string path = Server.MapPath($"~/{relativePath}");
                CB.InsertIntoUserLogwithIP(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 67, ip, " دانلود سوالات امتحان مشخصه " + did.ToString());
                DownloadZipFile(relativePath, $"{did.ToString()}.zip");
                mainFormatClick = true;
                //SendFileToUser(path + "\\" + e.CommandArgument.ToString() + ".zip");

            }
            if (e.CommandName == "DlQuestionSinglePage" && !mainFormatClick) //حاصل از فایل سوال وپیوست بدون اطلاعات دانشجو pdf فایل  
            {
                var path = $"QueizPapers/{examQ_Detail.Rows[0]["tterm"].ToString()}/{examQ_Detail.Rows[0]["code_ostad"].ToString()}/pdffiles/{did.ToString()}";

                CB.InsertIntoUserLogwithIP(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 67, ip, " دانلود سوالات امتحان مشخصه " + did.ToString());

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

                TextBox lbl_Password = (TextBox)e.Item.FindControl("lbl_Password");
                var pass = lbl_Password.Text;
                var whiteTape = Server.MapPath("~/University/Theme/images/whitePaper.jpg");
                var questioHeaderTemplate = Server.MapPath("~/University/Theme/images/QuestionHeaderTemplate.jpg");

                var userID = Session[sessionNames.userID_Karbar].ToString();
                var absPath = Server.MapPath($"/{path}");
                var res = EBusiness.ChangeTemplateOfQuestion(absPath, did.ToString(), pass, constQuestionFileInfo, questioHeaderTemplate, whiteTape, userID, cityIDQ2);

                DownloadZipFile($"{path}/{userID}", $"{did}_Momtahen_{userID}_1.zip");
            }
            if (e.CommandName == "DlQuestionMergedFiles" && !mainFormatClick)//حاصل از فایل سوال وپیوست به همراه اطلاعات دانشجو pdf فایل  
            {
                var path = $"QueizPapers/{examQ_Detail.Rows[0]["tterm"].ToString()}/{examQ_Detail.Rows[0]["code_ostad"].ToString()}/pdffiles/{did.ToString()}";

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

                var dt = EBusiness.ExamAnswerSheetbyDid(did, int.Parse(Session[sessionNames.userID_Karbar].ToString()));
                var studentList = dt.AsEnumerable().Select(x => new ExamStudentDTO()
                {
                    FirstName = x.Field<string>("stFirstName")??"",
                    LastName = x.Field<string>("stLastName")??"",
                    StudentCode = x.Field<string>("stcode") ?? "",
                    Grade = x.Field<string>("magh") ?? "",
                    Major = x.Field<string>("nameresh") ?? "",
                    SeatHeader = x.Field<string>("SeatHeader") ?? "",
                    SeatNumber = x.Field<int?>("SeatNumber")
                }).ToList();

                TextBox lbl_Password = (TextBox)e.Item.FindControl("lbl_Password");
                var pass = lbl_Password.Text;
                var whiteTape = Server.MapPath("~/University/Theme/images/whitePaper.jpg");
                var questioHeaderTemplate = Server.MapPath("~/University/Theme/images/QuestionHeaderTemplate.jpg");

                var userID = Session[sessionNames.userID_Karbar].ToString();
                var absPath = Server.MapPath($"/{path}");
                var res = EBusiness.GeneratePdfQuestionForStudents(absPath, did.ToString(), pass, questioHeaderTemplate, whiteTape, userID, constQuestionFileInfo, studentList, cityIDQ2);

                DownloadZipFile($"{path}/{userID}", $"{did}_Momtahen_{userID}_2.zip");
            }            

        }
        private void DownloadZipFile(string zipFilePath, string fileName)
        {
            var uri = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/{zipFilePath}/{fileName}";
            HtmlMeta meta = new HtmlMeta();
            meta.Attributes.Add("HTTP-EQUIV", "refresh");
            meta.Content = ".1; URL=" + uri;
            this.Page.Header.Controls.Add(meta);

            //var zipFile = Server.MapPath($"{zipFilePath}/{fileName}");
            //if (File.Exists(zipFile))
            //{
            //    File.Delete(zipFile);
            //}
        }
        private void SendFileToUser(string strFileFullPath)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(strFileFullPath);

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/zip";
            Response.WriteFile(file.FullName);
            Response.End();
        }

    }
}