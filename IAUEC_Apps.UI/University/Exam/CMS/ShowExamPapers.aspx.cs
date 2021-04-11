using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using Telerik.Web.UI;
using System.Configuration;
using IAUEC_Apps.Business.Common;
using Ionic.Zip;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using IAUEC_Apps.DTO.University.Exam;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace IAUEC_Apps.UI.University.Exam.CMS
{

    public partial class ShowExamPapers : System.Web.UI.Page
    {

        CommonBusiness CB = new CommonBusiness();
        ExamBusiness examBusiness = new ExamBusiness();
        public string PdfFileSource;
        public string PdfMimeType { get; set; } = "application/pdf";


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        private string GetMimeType(string ext)
        {
            //string mimeType = "application/unknown";
            //// ext = System.IO.Path.GetExtension(fileName).ToLower();
            //Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            //if (regKey != null && regKey.GetValue("Content Type") != null)
            //    mimeType = regKey.GetValue("Content Type").ToString();
            //return mimeType;

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
        //----------------------------
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
                //throw x;
            }
            return true;
        }
        //----------------------------
        string questionsFile = "";
        byte[] fileByteArray;
        string _filName = "";
        string base64EncodedPDF = "";
        string strFullPath = "";
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/loginRequestCMS.aspx");
            Session["act"] = "0";
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

                if (Session[sessionNames.userID_Karbar].ToString() != "1")
                {

                    //if (Session[sessionNames.userID_Karbar].ToString() == "1040") //Khademi
                    if (Session[sessionNames.userID_Karbar].ToString() == "1153") //LotfiMoghadam
                    {
                        //if (ip != "192.168.44.230") //Khademi
                        //if (ip != "192.168.44.216") //LotfiMoghadam
                       // if (ip != "192.168.44.179") //LotfiMoghadam
                        //if (ip != "192.168.47.43") //LotfiMoghadam
                        if (ip != "192.168.44.231" && ip != "192.168.12.77") //LotfiMoghadam
                        {
                            //File.AppendAllText(Server.MapPath("~/CommonUI/test.txt"), "Khademi: " + ip);
                            File.AppendAllText(Server.MapPath("~/CommonUI/test.txt"), "LotfiMoghadam: " + ip);
                            Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
                        }
                    }
                    else if (Session[sessionNames.userID_Karbar].ToString() == "1079") //Tavakoli
                    {
                        //if (ip != "192.168.44.147")
                        //if (ip != "192.168.47.111")
                        //if (ip != "192.168.44.23")
                        if (ip != "192.168.44.113") //99-00-1
                        {
                            File.AppendAllText(Server.MapPath("~/CommonUI/test.txt"), "Tavakoli: " + ip);
                            Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
                    }

                }
                DataTable dtdanesh = new DataTable();
                dtdanesh = examBusiness.GetAllDaneshkade();
                ddl_Danesh.DataSource = dtdanesh;
                ddl_Danesh.DataTextField = "namedanesh";
                ddl_Danesh.DataValueField = "id";
                ddl_Danesh.DataBind();
                ddl_Danesh.Items.Add(new ListItem("همه", "0"));
                ddl_Danesh.Items[ddl_Danesh.Items.Count - 1].Selected = true;
                //if (Session["RoleID"].ToString() == "15" || Session["RoleID"].ToString() == "26")
                //{
                //    ddl_Danesh.SelectedValue = "2";
                //    ddl_Danesh.Enabled = false;
                //}
                //else if (Session["RoleID"].ToString() == "17" || Session["RoleID"].ToString() == "28")
                //{
                //    ddl_Danesh.SelectedValue = "1";
                //    ddl_Danesh.Enabled = false;
                //}
                //else if (Session["RoleID"].ToString() == "16" || Session["RoleID"].ToString() == "27")
                //{
                //    ddl_Danesh.SelectedValue = "3";
                //    ddl_Danesh.Enabled = false;
                //}
                //else if (Session["RoleID"].ToString() == "66" || Session["RoleID"].ToString() == "67")
                //{
                //    ddl_Danesh.SelectedValue = "8";
                //    ddl_Danesh.Enabled = false;

                //}
                //else
                //{
                //    ddl_Danesh.SelectedValue = "0";

                //}
                UserAccessBusiness uacb = new UserAccessBusiness();
                int daneshID = uacb.GetDaneshIDByRoleID(int.Parse(Session["RoleID"].ToString()));
                if (daneshID > 0)
                {
                    if (ddl_Danesh.Items.FindByValue(daneshID.ToString()) != null)
                        ddl_Danesh.Items.FindByValue(daneshID.ToString()).Selected = true;
                    //ddl_Danesh.SelectedValue = daneshID.ToString();
                    ddl_Danesh.Enabled = false;
                }
                else
                {
                    ddl_Danesh.SelectedValue = daneshID.ToString();
                }
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            Session["act"] = "0";
            DataTable dt = new DataTable();
            dt = examBusiness.ShowQuiezPaper(int.Parse(ddl_Danesh.SelectedValue.ToString()), 0);
            if (dt.Rows.Count > 0)
            {
                grd_Class.Visible = true;
                grd_Class.DataSource = dt;
                grd_Class.DataBind();

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
            }
            else
            {
                rwm.RadAlert("سوالات آپلود نشده است", null, 100, "پیام", "");
            }

        }


        protected void grd_Class_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt = examBusiness.ShowQuiezPaper(int.Parse(ddl_Danesh.SelectedValue.ToString()), int.Parse(Session["act"].ToString()));
            if (e.Item is GridDataItem)
            {
                GridDataItem itemAmount = (GridDataItem)e.Item;
                Button btn_Taeid = e.Item.FindControl("btn_Taeid") as Button;
                Label lbl_Status = e.Item.FindControl("lbl_Status") as Label;
                Button btn_Raad = e.Item.FindControl("btn_Raad") as Button;
                //Button btn_Attachment = e.Item.FindControl("btn_Attachment") as Button;
                //Button btnRemoveAttachment = e.Item.FindControl("btnRemoveAttachment") as Button;
                //Button btn_collect = e.Item.FindControl("btn_collect") as Button;
                HiddenField h_att = e.Item.FindControl("att") as HiddenField;
                HiddenField statusQ1 = e.Item.FindControl("q1Status") as HiddenField;
                HiddenField statusQ2 = e.Item.FindControl("q2Status") as HiddenField;
                //Button btn_DeleteAttach = e.Item.FindControl("btn_DeleteAttach") as Button;
                #region ifs
                if (statusQ1.Value == "2" && h_att.Value == "")
                {
                    btn_Taeid.Visible = true;
                    btn_Raad.Visible = true;

                    //btn_Attachment.Visible = false;
                    //btnRemoveAttachment.Visible = false;

                    //btn_DeleteAttach.Visible = false;
                }
                if (statusQ1.Value == "2" && h_att.Value != "")
                {
                    btn_Taeid.Visible = true;
                    btn_Raad.Visible = true;

                    //btn_Attachment.Visible = true;
                    //btnRemoveAttachment.Visible = true;


                    //btn_DeleteAttach.Visible = true;
                }
                if (statusQ1.Value == "3")
                {
                    lbl_Status.Visible = true;
                    btn_Raad.Visible = false;
                    btn_Taeid.Visible = false;
                    //btn_collect.Visible = true;
                    //lbl_Status.Text = "تایید شده است";
                }
                if (statusQ1.Value == "4")
                {
                    lbl_Status.Visible = true;
                    btn_Raad.Visible = false;
                    btn_Taeid.Visible = false;
                    lbl_Status.Text = "رد شده است";
                }
                if (statusQ1.Value == "5")
                {
                    lbl_Status.Visible = true;
                    btn_Raad.Visible = false;
                    btn_Taeid.Visible = false;
                    lbl_Status.Text = "تجمیع";
                }

                if (statusQ1.Value == "6")
                {
                    lbl_Status.Visible = true;
                    btn_Raad.Visible = false;
                    btn_Taeid.Visible = false;
                    lbl_Status.Text = "تحویل";
                }
                if (statusQ1.Value == "7")
                {
                    lbl_Status.Visible = true;
                    btn_Raad.Visible = false;
                    btn_Taeid.Visible = false;
                    lbl_Status.Text = "دریافت";
                }
                if (statusQ1.Value == "3" && statusQ2.Value == "9")
                {
                    btn_Taeid.Visible = true;
                    btn_Raad.Visible = true;

                    //btn_Attachment.Visible = true;
                    //btnRemoveAttachment.Visible = true;


                    //btn_DeleteAttach.Visible = true;
                }


                #endregion ifs
                var answerSheetType = string.Empty;
                var item = (DataRowView)e.Item.DataItem;
                if (Convert.ToBoolean(item["AnswerSheet1"]))
                {
                    answerSheetType += "پاسخگویی در برگه سوالات";
                }
                if (Convert.ToBoolean(item["AnswerSheet2"]))
                {
                    if (answerSheetType.Length > 0)
                        answerSheetType += " | ";
                    answerSheetType += " تشریحی ";
                }
                if (Convert.ToBoolean(item["AnswerSheet3"]))
                {
                    if (answerSheetType.Length > 0)
                        answerSheetType += " | ";
                    answerSheetType += " تستی ";
                }
                e.Item.Cells[10].Text = answerSheetType;

                if (Convert.ToByte(item["status"]) == 9)
                {
                    btn_Taeid.Visible = true;
                    btn_Raad.Visible = true;
                }

            }

        }

        protected void grd_Class_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
                {
                if (e.CommandName == "Filter") return;
                string did = "-1";
                string QuesId = "-1";
                int? cityId = null;
                int q2Status = -1;
                int q1Status = -1;
                string fullPathZipFile;

                var comArgs = e.CommandArgument.ToString().Split(';');

                if (!string.IsNullOrEmpty(e.CommandArgument.ToString()) && comArgs.Length == 1)
                {
                    did = e.CommandArgument.ToString();
                }
                else if (!string.IsNullOrEmpty(e.CommandArgument.ToString()) && comArgs.Length > 1)
                {
                    did = comArgs[0];
                    QuesId = comArgs[1];
                    cityId = int.Parse(comArgs[2]);
                    q2Status = int.Parse(comArgs[3]);
                    q1Status = int.Parse(comArgs[4]);
                }
                else return;

                int? cityIDQ2 = null;
                if (q2Status != -1 && q1Status == 3)
                    cityIDQ2 = cityId;

                var quizPapere = examBusiness.ShowQueizPaperByDid(did, cityIDQ2);
                if (quizPapere.Rows.Count == 0) return;

                var dynamicPath = "~/QueizPapers/" + quizPapere.Rows[0]["tterm"].ToString() + "/" + quizPapere.Rows[0]["code_ostad"].ToString() + "/pdffiles/" + quizPapere.Rows[0]["coursecode"].ToString();
                string path = Server.MapPath(dynamicPath);

                byte[] base64HashesPassword = Convert.FromBase64String(quizPapere.Rows[0]["Password"].ToString());
                string pass = EncryptionClass.DecryptRJ256(base64HashesPassword);
                var nocash = DateTime.Now.ToString("yyyyMMddHHmmssFFF");
                string cmdName = e.CommandName;
                Session["Collect"] = "0";
                Session["Q2Info"] = $"{QuesId}|{did}|{cityId.ToString()}|{q2Status.ToString()}|{q1Status.ToString()}";

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
                var item = examBusiness.GetExamQuestionsbyDid(did, null, cityIDQ2);

                if (cmdName == "OpenMergedPdfFile")
                {
                    if (File.Exists(fullPathZipFile))
                    {
                        var whiteTape = Server.MapPath("~/University/Theme/images/whitePaper.jpg");
                        var questioHeaderTemplate = Server.MapPath("~/University/Theme/images/questionHeaderTemplate.jpg");
                        var examQ_Detail = examBusiness.Get_ExamdetailbyDid(did, null, cityIDQ2);

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
                        examBusiness.ChangeTemplateOfQuestion(path, did.ToString(), pass, constQuestionFileInfo, questioHeaderTemplate, whiteTape, userID, cityIDQ2);
                        fileByteArray = System.IO.File.ReadAllBytes(Server.MapPath($"{dynamicPath}/{userID}/{did}_Momtahen_{userID}_1.pdf"));
                        Session["pdfPath"] = fileByteArray;
                        Session["contentType"] = "application/pdf";
                        Directory.GetDirectories(path).ToList().ForEach(dir => Directory.Delete(dir, true));
                        CB.InsertIntoUserLog(int.Parse(userID), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 57, "مشاهده سوالات و پیوست ادغامی", int.Parse(QuesId));
                        var clientFunction = "openShowFileInPopup('ShowFile.aspx?q=" + nocash + "')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popuup", clientFunction, true);
                    }
                }
                #region C11

                ////if (cmdName == "Open" || cmdName == "OpenAttach")
                ////{
                ////    var flg = DeleteImmediatelyFiles(path);
                ////    var zipfilePath = path + "/" + dt.Rows[0]["coursecode"].ToString() + ".zip";
                ////    if (!File.Exists(zipfilePath))
                ////    {
                ////        var p = path.Replace("\\", " ==> ");
                ////        var str = string.Format("فایلی جهت نمایش در مسیر{0} یافت نشد", p);
                ////        rwm.RadAlert(str, null, 100, "پیام", "");
                ////        return;
                ////    }
                ////    ZipFile zip = ZipFile.Read(zipfilePath);
                ////    zip.Password = pass;
                ////    zip.ExtractAll(path.ToString(), ExtractExistingFileAction.OverwriteSilently);
                ////    zip.Dispose();
                ////    filesInDir = Directory.GetFiles(path, "*.*");
                ////    string ext = "";
                ////    switch (cmdName)
                ////    {
                ////        case "Open":
                ////            {
                ////                var extsPath = filesInDir.Where(w => !w.Contains("Attached") && !w.Contains("zip")).FirstOrDefault()?.ToString();
                ////                if (extsPath == null) return;
                ////                ext = String.Join("", extsPath).Split('\\').Last().Split('.')[1];
                ////                if (!string.IsNullOrEmpty(ext))
                ////                {
                ////                    ext = "." + ext;
                ////                    questionsFile = dynamicPath + "/" + dt.Rows[0]["coursecode"].ToString() + ext;//?q=" + nocash;
                ////                    _filName = Server.MapPath(questionsFile);

                ////                    fileByteArray = System.IO.File.ReadAllBytes(_filName);
                ////                    // base64EncodedPDF = System.Convert.ToBase64String(fileByteArray);
                ////                    Session["pdfPath"] = fileByteArray;
                ////                    var c_type = GetMimeType(ext);
                ////                    if (string.IsNullOrEmpty(c_type))
                ////                    {
                ////                        rwm.RadAlert("فرمت فایل ارسالی صحیح نیست", null, 100, "پیام", "");
                ////                        return;
                ////                    }
                ////                    Session["contentType"] = c_type;

                ////                }
                ////                else
                ////                    return;
                ////            }
                ////            CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 57, "مشاهده سوالات امتحان", questionID);
                ////            break;
                ////        //=================================================
                ////        case "OpenAttach":
                ////            {
                ////                var extsPath = filesInDir.Where(w => w.Contains("Attached")).FirstOrDefault()?.ToString();
                ////                if (extsPath == null) return;
                ////                ext = String.Join("", extsPath).Split('\\').Last().Split('.')[1];
                ////                //if (File.Exists(path + "/" + dt.Rows[0]["coursecode"].ToString() + "Attached.jpg") || File.Exists(path + "/" + dt.Rows[0]["coursecode"].ToString() + "Attached.jpeg"))
                ////                if (!string.IsNullOrEmpty(ext))
                ////                {
                ////                    ext = "." + ext;
                ////                    questionsFile = dynamicPath + "/" + dt.Rows[0]["coursecode"].ToString() + "Attached" + ext;//?q=" + nocash;
                ////                    _filName = Server.MapPath(questionsFile);

                ////                    fileByteArray = System.IO.File.ReadAllBytes(_filName);
                ////                    //base64EncodedPDF = System.Convert.ToBase64String(fileByteArray);
                ////                    Session["pdfPath"] = fileByteArray;
                ////                    var c_type = GetMimeType(ext);
                ////                    if (string.IsNullOrEmpty(c_type))
                ////                    {
                ////                        rwm.RadAlert("فرمت فایل ارسالی صحیح نیست", null, 100, "پیام", "");
                ////                        return;
                ////                    }
                ////                    Session["contentType"] = c_type;
                ////                }
                ////                else
                ////                    return;
                ////            }
                ////            CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 58, "مشاهده پیوست سوالات", questionID);
                ////            break;
                ////    }
                ////    DeleteImmediatelyFiles(path);
                ////    var clientFunction = "openShowFileInPopup('ShowFile.aspx?q=" + nocash + "')";
                ////    ScriptManager.RegisterStartupScript(this, this.GetType(), "popuup", clientFunction, true);
                ////}
                //////=================================================
                ////if (cmdName == "RemoveAttach")
                ////{

                ////    ExamBusiness eBusiness = new ExamBusiness();
                ////    var item22 = eBusiness.GetExamQuestionsbyDid(int.Parse(did));
                ////    if (item != null)
                ////    {
                ////        int? status = item22.AsEnumerable().Select(s => s.Field<int>("Status")).FirstOrDefault();
                ////        if (status.HasValue && status == 3 || status == 4)
                ////        {
                ////            rwm.RadAlert("کاربر محترم شما قبلا در زبانه دیگر عملیات رد یا تایید سوال را انجام داده اید لطفا پنجره مرورگر خود را بروز کرده یا ببندید", null, 100, "پیام", "");
                ////            return;
                ////        }
                ////    }
                ////    string zipPath = path + "\\" + did + ".zip";
                ////    if (!File.Exists(zipPath)) return;
                ////    using (ZipFile zip = ZipFile.Read(zipPath))
                ////    {
                ////        zip.Password = pass;
                ////        var attachment = zip.EntryFileNames.Where(w => w.StartsWith(dt.Rows[0]["coursecode"].ToString() + "Attached")).FirstOrDefault();
                ////        if (attachment == null) return;
                ////        zip.RemoveEntry(attachment);

                ////        System.GC.Collect();
                ////        System.GC.WaitForPendingFinalizers();

                ////        zip.Save();
                ////        zip.Dispose();
                ////    }
                ////    filesInDir = Directory.GetFiles(path, "*.*");
                ////    var attachFilePath = filesInDir.Where(w => w.Contains("Attached")).FirstOrDefault()?.ToString();
                ////    if (attachFilePath != null)
                ////    {
                ////        File.Delete(attachFilePath);
                ////    }
                ////    examBusiness.RemoveExamQuestionAttachment(id: Convert.ToInt32(dt.Rows[0]["ID"]));
                ////    DataTable ds = new DataTable();
                ////    ds = examBusiness.ShowQuiezPaper(int.Parse(ddl_Danesh.SelectedValue.ToString()), int.Parse(Session["act"].ToString()));

                ////    grd_Class.DataSource = ds;
                ////    grd_Class.DataBind();

                ////    CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 123, "حذف پیوست سوالات", questionID);
                ////}
                //=================================================

                #endregion
                if (cmdName == "Taeid")
                {
                    int statusQ1 = int.Parse(item.Rows[0]["Status"].ToString());
                    int statusQ2 = int.Parse(item.Rows[0]["q2Status"].ToString());
                    if ((statusQ1 == 3 || statusQ1 == 4) && statusQ2 == -1)
                    {
                        rwm.RadAlert("کاربر محترم شما قبلا در زبانه دیگر عملیات رد یا سوال را انجام داده اید لطفا پنجره مرورگر خود را بروز کرده یا ببندید", null, 100, "پیام", "");
                        return;
                    }

                    div_Main.Visible = false;
                    Confirmpnl.Visible = true;
                    //=========== generate exam keycode 
                    //examBusiness.GenerateExamKeyCode(int.Parse(did));

                }
                //=================================================
                if (cmdName == "Raad")
                {
                    if (item != null)
                    {
                        int statusQ1 = int.Parse(item.Rows[0]["Status"].ToString());
                        int statusQ2 = int.Parse(item.Rows[0]["q2Status"].ToString());
                        if ((statusQ1 == 3 || statusQ1 == 4) && statusQ2 == -1)
                        {
                            rwm.RadAlert("کاربر محترم شما قبلا در زبانه دیگر عملیات رد یا تایید سوال را انجام داده اید لطفا پنجره مرورگر خود را بروز کرده یا ببندید", null, 100, "پیام", "");
                            return;
                        }
                    }
                    RadWindowManager windowManager = new RadWindowManager();
                    RadWindow widnow1 = new RadWindow();
                    widnow1.NavigateUrl = "../CMS/InsertRejectDesc.aspx?did=" + did + "&qID=" + QuesId + "&q2CityId=" + cityId + "&q2Status=" + q2Status + "&q1Status=" + q1Status;
                    widnow1.ID = "RadWindow1";
                    windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(800);
                    windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                    widnow1.VisibleOnPageLoad = true;
                    windowManager.Windows.Add(widnow1);
                    ContentPlaceHolder mpContentPlaceHolder;
                    mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                    mpContentPlaceHolder.Controls.Add(widnow1);
                }
                //=================================================
                if (cmdName == "view_history")
                {

                    CommonBusiness cmb = new CommonBusiness();
                    lst_history.DataSource = cmb.GetUserLogByModifyId(int.Parse(QuesId), 8);
                    lst_history.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }
                //=================================================

            }
            catch (Exception ex)
            {
                rwm.RadAlert(ex.Message, null, 100, "خطا", "");
            }

        }


        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {

                DataTable dt = new DataTable();
                dt = examBusiness.ShowQuiezPaper(int.Parse(ddl_Danesh.SelectedValue.ToString()), int.Parse(Session["act"].ToString()));

                grd_Class.Visible = true;
                grd_Class.DataSource = dt;
                grd_Class.DataBind();
                DataTable dtdanesh = new DataTable();
                dtdanesh = examBusiness.GetAllDaneshkade();
                ddl_Danesh.DataSource = dtdanesh;
                ddl_Danesh.DataTextField = "namedanesh";
                ddl_Danesh.DataValueField = "id";
                ddl_Danesh.DataBind();
                ddl_Danesh.Items.Add(new ListItem("همه", "0"));
                ddl_Danesh.Items[ddl_Danesh.Items.Count - 1].Selected = true;

            }
        }

        //confirm reject question
        protected void conf_Click(object sender, EventArgs e)
        {
            var params33 = Session["Q2Info"].ToString().Split('|');
            int qID = int.Parse(params33[0] ?? "-1");
            string did = params33[1] ?? "-1";
            int q2CityId = !string.IsNullOrEmpty(params33[2]) ? int.Parse(params33[2]) : -2;
            int statusQ2 = int.Parse(params33[3] ?? "-1");
            int statusQ1 = int.Parse(params33[4] ?? "-1");

            int? cityIDQ2 = null;
            if (statusQ2 != -1 && statusQ1 == 3)
                cityIDQ2 = q2CityId;

            if (Session["Collect"].ToString() == "0")
            {
                var dtpath = examBusiness.ShowQueizPaperByDid(did, cityIDQ2);
                //string path = Server.MapPath("../../../QueizPapers/" + dtpath.Rows[0]["tterm"].ToString() + "/" + dtpath.Rows[0]["code_ostad"].ToString() + "/pdffiles/" + dtpath.Rows[0]["coursecode"].ToString());
                //DirectoryInfo di = new DirectoryInfo(path);
                //FileInfo[] fi = di.GetFiles("*.pdf");
                //foreach (FileInfo f in fi)
                //{
                //    File.Delete(path + "\\" + f.Name);
                //}
                //FileInfo[] fijpg = di.GetFiles("*.jpg");
                //foreach (FileInfo f in fijpg)
                //{
                //    File.Delete(path + "\\" + f.Name);
                //}
                if (statusQ1 == 3 && statusQ2 != -1)
                {
                    //UpdateExamQuestionsCancled(int qId = -1, string address = null, string password = null, int status = -1, int cityIdQ2 = -1, string note = "")
                    examBusiness.UpdateExamQuestionsCancled(qID, null, null, 10, q2CityId, "");
                }
                else
                {
                    examBusiness.UpdateQueizStatus(3, did, "", true);
                    //=========== generate exam keycode 
                    examBusiness.GenerateExamKeyCode(did);
                }
                CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 59, "تایید سوالات", int.Parse(dtpath.Rows[0]["QuestionId"].ToString()));

                //#####################################
                //#####################################
                //#####################################

                // ramezanian - ارسال اس ام اس

                DataTable dtResault = examBusiness.GetMobileProfByDid(did);
                DataTable dtMessage = CB.GetTextMessage(0, 8, 4, 3);
                string oldText = dtMessage.Rows[0]["Text"].ToString().Trim();
                int index = oldText.IndexOf("شما");
                var newText = oldText.Substring(0, index + 3) + "با کد مشخصه " + dtpath.Rows[0]["coursecode"].ToString() + " " + oldText.Substring(index + 3);
                //var finalText=newText.Replace(@"\r\n", Environment.NewLine);
                string IdAppMessage = dtMessage.Rows[0]["ID"].ToString();
                try
                {
                    if (dtResault.Rows[0]["mobile"].ToString() != null || dtResault.Rows[0]["mobile"].ToString() != "")
                    {
                        bool sentSMS; string smsStatusText;
                        //var finalText = newText.Replace(System.Environment.NewLine, string.Empty);
                        lbl_Resault.Text = CB.sendSMS(dtResault.Rows[0]["mobile"].ToString(), newText, out sentSMS, out smsStatusText);
                        int asanakStatus = CB.getAsanakStatusID(lbl_Resault.Text);
                        CB.LogStatusMessage(dtResault.Rows[0]["code_ostad"].ToString(), lbl_Resault.Text, dtResault.Rows[0]["mobile"].ToString(), asanakStatus, smsStatusText, int.Parse(IdAppMessage));


                    }
                }
                catch (Exception xx)
                {
                    throw xx;
                }
                div_Main.Visible = true;
                Confirmpnl.Visible = false;
                rwm.RadAlert("با موفقیت تایید شد", null, 100, "پیام", "");
                //#####################################
                //#####################################
                //#####################################
                grd_Class.Visible = true;
                var dts = examBusiness.ShowQuiezPaper(int.Parse(ddl_Danesh.SelectedValue.ToString()), int.Parse(Session["act"].ToString()));
                grd_Class.DataSource = dts;
                grd_Class.DataBind();
            }

            if (Session["Collect"].ToString() == "1")
            {
                try
                {
                    examBusiness.UpdateQueizStatus(5, did, "");
                    rwm.RadAlert("با موفقیت انجام شد", null, 100, "پیام", "");
                }
                catch (Exception x)
                {
                    rwm.RadAlert("خطا در انجام عملیات ،مجددا سعی نمایید", null, 100, "پیام", "");
                }
            }

            Session["Q2Info"] = null;
        }

        protected void notConf_Click(object sender, EventArgs e)
        {
            div_Main.Visible = true;

            Confirmpnl.Visible = false;

        }

        protected void grd_Class_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = examBusiness.ShowQuiezPaper(int.Parse(ddl_Danesh.SelectedValue.ToString()), int.Parse(Session["act"].ToString()));
            grd_Class.DataSource = dt;
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

        }

        protected void btn_review_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_did.Text))
            {
                rwm.RadAlert("لطفا کد مشخصه را وارد فرمایید.", null, 100, "پیام", "");
                return;
            }
            string did = txt_did.Text;
            DataTable dtpath = examBusiness.ShowQueizPaperByDid(did);
            DataTable dtDetail = examBusiness.GetExamPaperStatus(did);
            if (dtDetail.Rows.Count > 0)
            {
                if (dtDetail.Rows[0]["status"].ToString() == "4")
                {

                    string pathdes = Server.MapPath("~/QueizPapers/" + dtpath.Rows[0]["tterm"].ToString() + "/" + dtpath.Rows[0]["code_ostad"].ToString() + "/pdffiles/" + dtpath.Rows[0]["coursecode"].ToString());

                    string pathsrc = Server.MapPath("~/QueizPapers/" + dtpath.Rows[0]["tterm"].ToString() + "/" + dtpath.Rows[0]["code_ostad"].ToString() + "/pdffiles/MoveFiles/" + dtpath.Rows[0]["coursecode"].ToString());

                    if (Directory.Exists(pathsrc))
                    {
                        DirectoryInfo di = new DirectoryInfo(pathsrc);
                        FileInfo[] fi = di.GetFiles("*.pdf");
                        foreach (FileInfo f in fi)
                        {
                            File.Delete(pathsrc + "/" + f.Name);
                        }
                        FileInfo[] fijpg = di.GetFiles("*.jpg");
                        foreach (FileInfo f in fijpg)
                        {
                            File.Delete(pathsrc + "/" + f.Name);
                        }
                        if (!Directory.Exists(pathdes))
                            Directory.CreateDirectory(pathdes);
                        string[] files = System.IO.Directory.GetFiles(pathsrc);
                        foreach (string s in files)
                        {
                            string fileName, destFile = "";
                            fileName = System.IO.Path.GetFileName(s);
                            destFile = System.IO.Path.Combine(pathdes, fileName);
                            if (File.Exists(destFile))
                                File.Delete(destFile);
                            System.IO.File.Move(s, destFile);

                        }
                    }
                }
            }
            else
            {
                rwm.RadAlert("کد مشخصه معتبر نمی باشد", null, 100, "پیام", "");
                return;
            }

            examBusiness.TemplateDownloaded(did, true);

            examBusiness.UpdateQueizStatus(2, txt_did.Text, "");

            CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 94, "بازنگری سوال", int.Parse(dtpath.Rows[0]["QuestionId"].ToString()));

            grd_Class.DataSource = examBusiness.ShowQuiezPaper(int.Parse(ddl_Danesh.SelectedValue.ToString()), 0);
            grd_Class.DataBind();
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
        }

        protected void btn_anssheet_Click(object sender, EventArgs e)
        {

            if (txt_did.Text != "")
            {
                DataTable dtDetail = examBusiness.GetExamPaperStatus(txt_did.Text);
                if (dtDetail.Rows.Count != 1)
                {
                    rwm.RadAlert("کد مشخصه وارد شده معتبر نمی باشد", null, 100, "خطا", "");
                    return;
                }
                examBusiness.Update_answersheetStatus(2, txt_did.Text);
                rwm.RadAlert("تغییر با موفقیت انجام شد", null, 100, "پیام", "");
                btn_review_Click(null, null);
            }
            else
                rwm.RadAlert("مشخصه وارد نشده است", null, 100, "پیام", "");
        }

        //protected void btn_testsheet_Click(object sender, EventArgs e)
        //{
        //    if (txt_did.Text != "")
        //    {
        //        DataTable dtDetail = examBusiness.GetExamPaperStatus(int.Parse(txt_did.Text));
        //        if (dtDetail.Rows.Count != 1)
        //        {
        //            rwm.RadAlert("کد مشخصه وارد شده معتبر نمی باشد", null, 100, "خطا", "");
        //            return;
        //        }
        //        examBusiness.Update_answersheetStatus(3, int.Parse(txt_did.Text));
        //        rwm.RadAlert("تغییر با موفقیت انجام شد", null, 100, "پیام", "");
        //        btn_review_Click(null ,null);
        //    }
        //    else
        //        rwm.RadAlert("مشخصه وارد نشده است", null, 100, "پیام", "");
        //}

        protected void btn_ans_Click(object sender, EventArgs e)
        {
            if (txt_did.Text != "")
            {
                DataTable dtDetail = examBusiness.GetExamPaperStatus(txt_did.Text);
                if (dtDetail.Rows.Count != 1)
                {
                    rwm.RadAlert("کد مشخصه وارد شده معتبر نمی باشد", null, 100, "خطا", "");
                    return;
                }
                examBusiness.Update_answersheetStatus(1, txt_did.Text);
                rwm.RadAlert("تغییر با موفقیت انجام شد", null, 100, "پیام", "");
                btn_review_Click(null, null);
            }
            else
                rwm.RadAlert("مشخصه وارد نشده است", null, 100, "پیام", "");
        }

        protected void btn_collect_Click(object sender, EventArgs e)
        {


            Session["act"] = "1";
            DataTable dt = new DataTable();
            dt = examBusiness.ShowQuiezPaper(int.Parse(ddl_Danesh.SelectedValue.ToString()), 1);
            if (dt.Rows.Count > 0)
            {
                grd_Class.Visible = true;
                grd_Class.DataSource = dt;
                grd_Class.DataBind();

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
            }
            else
            {
                rwm.RadAlert("سوالات آپلود نشده است", null, 100, "پیام", "");
            }


        }

        public bool ExtractPdf_zipped(object commArg)
        {
            try
            {
                DataTable dt = new DataTable();
                var did = commArg.ToString();
                dt = examBusiness.ShowQueizPaperByDid(did);

                string passs = (dt.Rows[0]["Password"].ToString());
                byte[] str = Convert.FromBase64String(passs);

                string pass = EncryptionClass.DecryptRJ256(str);
                string path = Server.MapPath("~/QueizPapers/" + dt.Rows[0]["tterm"].ToString() + "/" + dt.Rows[0]["code_ostad"].ToString() + "/pdffiles/" + dt.Rows[0]["coursecode"].ToString());
                DirectoryInfo di = new DirectoryInfo(path);
                bool isextract = false;
                FileInfo[] fi = di.GetFiles("*.pdf");
                foreach (FileInfo f in fi)
                {
                    isextract = true;
                }
                FileInfo[] fijpg = di.GetFiles("*.jpg");
                foreach (FileInfo f in fijpg)
                {
                    isextract = true;
                }
                if (!isextract)
                {
                    ZipFile zip = ZipFile.Read(path + "/" + dt.Rows[0]["coursecode"].ToString() + ".zip");
                    zip.Password = pass;
                    zip.ExtractAll(path.ToString(), ExtractExistingFileAction.OverwriteSilently);
                }
                CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 58, dt.Rows[0]["coursecode"].ToString() + "مشاهده پیوست", int.Parse(dt.Rows[0]["QuestionId"].ToString()));

                return true;
            }
            catch
            {
                return false;
            }

        }

        //protected void rwm_Disposed(object sender, EventArgs e)
        //{
        //}

        //protected void btnExamQ_Approved_Click(object sender, EventArgs e)
        //{
        //}

        //protected void btnExamQ_Rejected_Click(object sender, EventArgs e)
        //{
        //}

        
    }
}