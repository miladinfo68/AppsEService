using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.university.Exam;
using Telerik.Web.UI;
using IAUEC_Apps.DTO.University.Exam;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class DlExamByParam : System.Web.UI.Page
    {
        ExamBusiness EBusiness = new ExamBusiness();
        RequestStudentCartBusiness CBusiness = new RequestStudentCartBusiness();
        CommonBusiness CB = new CommonBusiness();
        LoginBusiness logBuisness = new LoginBusiness();

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

                RefreshGrid();
            }
        }

        void RefreshGrid()
        {
            DataTable dtsaat = new DataTable();
            dtsaat = EBusiness.Get_Exam_saatexam(true);
            ddl_Saate.DataSource = dtsaat;
            ddl_Saate.DataTextField = "saatexam";
            ddl_Saate.DataBind();
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

                CheckBox chk2 = e.Item.FindControl("chk2") as CheckBox;
                //CheckBox chk3 = e.Item.FindControl("chk3") as CheckBox;

                Label lblAnswerSheet = e.Item.FindControl("lblAnswerSheet") as Label;
                Label lblNoAnswerSheet = e.Item.FindControl("lblNoAnswerSheet") as Label;
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
                e.Item.Cells[8].Text = answerSheetType;

                if (chk2.Checked)
                    lblAnswerSheet.Visible = true;
                else
                    lblNoAnswerSheet.Visible = true;

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
            if (ipaddress == "" || ipaddress == null) ipaddress = Request.ServerVariables["REMOTE_ADDR"];
            var ip = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
                                && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                    ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
                    : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (ip.Contains(",")) ip = ip.Split(',').First();

            var cmdArgs = e.CommandArgument.ToString().Split(',');
            var did = cmdArgs[0];
            var cityIdQ2 = int.Parse(cmdArgs[1]);
            var statusQ1 = int.Parse(cmdArgs[2]);
            var statusQ2 = int.Parse(cmdArgs[3]);

            int? cityIDQ2 = null;
            if ( statusQ1 == 3 && statusQ2 != -1 )
                cityIDQ2 = cityIdQ2;

            var examQ_Detail = EBusiness.Get_ExamdetailbyDid(did,null , cityIDQ2);
            var mainFormatClick = false;

            if (e.CommandName == "DlQuestionMainFormat")//فرمت اصلی سوالات بدون تغیرات
            {
                var relativePath = "QueizPapers/" + examQ_Detail.Rows[0]["tterm"].ToString() + "/" + examQ_Detail.Rows[0]["code_ostad"].ToString() + "/pdffiles/" + did.ToString();
                //    string path = Server.MapPath($"~/{relativePath}");
                CB.InsertIntoUserLogwithIP(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 67, ip, " دانلود سوالات امتحان مشخصه " + did.ToString());
                //SendFileToUser(path + "\\" + e.CommandArgument.ToString() + ".zip");
                DownloadZipFile(relativePath, $"{did.ToString()}.zip");
                mainFormatClick = true;

            }
            if (e.CommandName == "DlQuestionSinglePage" && !mainFormatClick)//حاصل از فایل سوال وپیوست بدون اطلاعات دانشجو pdf فایل 
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
                //Directory.GetDirectories(absPath).ToList().ForEach(dir => Directory.Delete(dir, true));

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
                    LastName = x.Field<string>("stLastName") ?? "",
                    StudentCode = x.Field<string>("stcode") ?? "",
                    Grade = x.Field<string>("magh") ?? "",
                    Major = x.Field<string>("nameresh") ?? "",
                    SeatHeader = x.Field<string>("SeatHeader") ?? "",
                    SeatNumber = x.Field<int?>("SeatNumber")
                }).ToList();


                TextBox lbl_Password = (TextBox)e.Item.FindControl("lbl_Password");
                var pass = lbl_Password.Text;


                //HiddenField hdn_Pass = (HiddenField)e.Item.FindControl("hdn_Pass");
                ////hdn_Pass
                //string tt = (hdn_Pass.Value.ToString());
                //byte[] strvv = Convert.FromBase64String(tt);
                //string passvv = EncryptionClass.DecryptRJ256(strvv);



                //byte[] str = Convert.FromBase64String("CZZEGhdo4eDDXI63afXArM+sXzrmFme+sB7PmNJxF+c=");
                //string pass222 = EncryptionClass.DecryptRJ256(str);


                var whiteTape = Server.MapPath("~/University/Theme/images/whitePaper.jpg");
                var questioHeaderTemplate = Server.MapPath("~/University/Theme/images/QuestionHeaderTemplate.jpg");

                var userID = Session[sessionNames.userID_Karbar].ToString();
                var absPath = Server.MapPath($"/{path}");
                var res = EBusiness.GeneratePdfQuestionForStudents(absPath, did.ToString(), pass, questioHeaderTemplate, whiteTape, userID, constQuestionFileInfo, studentList, cityIDQ2);
                DownloadZipFile($"{path}/{userID}", $"{did}_Momtahen_{userID}_2.zip");
                //Directory.GetDirectories(absPath).ToList().ForEach(dir => Directory.Delete(dir, true));
                //Directory.GetFiles(rootDirectory, "*.*").Where(w => !w.Contains(".zip")).ToList().ForEach(file => File.Delete(file));

            }
            if (e.CommandName == "ExamSheet" && !mainFormatClick)
            {
                Response.Redirect("ShowExamSheet.aspx?did=" + e.CommandArgument.ToString());
            }

        }
        private void DownloadZipFile(string zipFilePath, string fileName)
        {
            var uri = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/{zipFilePath}/{fileName}";
            HtmlMeta meta = new HtmlMeta();
            meta.Attributes.Add("HTTP-EQUIV", "refresh");
            meta.Content = ".1; URL=" + uri;
            this.Page.Header.Controls.Add(meta);           


            //var uri = Server.MapPath($"~/{zipFilePath}/{fileName}");
            //System.IO.FileInfo file = new System.IO.FileInfo(uri);
            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //Response.AddHeader("Content-Length", file.Length.ToString());
            ////Response.AddHeader("Refresh", "12;URL=" + Request.Url.AbsoluteUri);
            ////Response.AddHeader("Refresh", "3; url=" + Request.Url.AbsoluteUri);
            //Response.ContentType = "application/zip";
            //try
            //{
            //    Response.WriteFile(file.FullName);
            //    Response.AddHeader("Refresh", "3; url=" + Request.Url.AbsoluteUri);
            //    Response.Flush();
            //}
            //finally
            //{
            //    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "refreshParentPage", "hideLoading();", true);
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "hideLoading", "hideLoading();", true); 
            //    //ScriptManager.RegisterStartupScript(this, GetType(), "Script", "alert('This pops up');", true);                           
            //    //Response.End();
            //    //Response.Redirect(Request.RawUrl);
            //    //Response.Redirect(Request.Url.AbsoluteUri ,true);

            //    var script = $"alert('Some data missing!'); window.location=window.location";
            //    ClientScript.RegisterStartupScript(this.GetType(), "somekey", script,true);
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



        protected void btn_Show_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (ddl_Saate.SelectedValue.ToString() != "0")
            {
                dt = EBusiness.QuiezPaperForDLBySaat(int.Parse(Session[sessionNames.userID_Karbar].ToString()), ddl_Saate.SelectedValue.ToString());//IDshahr dade shavad
                if (dt.Rows.Count > 0)
                {
                    grd_CourseList.DataSource = dt;
                    grd_CourseList.DataBind();
                }
                else
                {
                    rwm.RadAlert("در این سانس کلاسی موجود نیست", null, 100, "پیام", "");
                    grd_CourseList.DataSource = dt;
                    grd_CourseList.DataBind();
                }
            }
            else
            {
                rwm.RadAlert("ساعت امتحان را انتخاب نمایید", null, 100, "پیام", "");
                grd_CourseList.DataSource = dt;
                grd_CourseList.DataBind();
            }
        }

    }
}