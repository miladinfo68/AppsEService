using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Web.UI;
using IAUEC_Apps.DTO.University.Exam;
using System.IO;
using System.Linq;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ChangeExamDateByParams : System.Web.UI.Page
    {

        ExamBusiness EBusiness = new ExamBusiness();
        List<ExamQuestionInfo> list = new List<ExamQuestionInfo>();
        private CommonBusiness cmb = null;

        void BindDrpExamPlaceCities(int cityID = -1)
        {
            ddl_Cities.Items.Clear();

            ddl_Cities.DataSource = EBusiness.GetAllExamPlaceCities(cityID);
            ddl_Cities.DataValueField = "ID";
            ddl_Cities.DataTextField = "Name_City";
            ddl_Cities.DataBind();
            ddl_Cities.Items.Insert(0, new ListItem("انتخاب کنید", "-2"));
            ddl_Cities.Items.Insert(1, new ListItem("همه", "-1"));
        }

        void BindDrpExamsDate()
        {
            ddl_ExamDate.Items.Clear();

            ddl_ExamDate.DataSource = EBusiness.Get_Exam_dateexam(true);
            ddl_ExamDate.DataTextField = "dateexam";
            ddl_ExamDate.DataValueField = "dateexam";
            ddl_ExamDate.DataBind();
            ddl_ExamDate.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));
        }

        void BindDrpExamSaatsByExamDate(string examDate)
        {
            ddl_Saat.Items.Clear();

            ddl_Saat.DataSource = EBusiness.GetSaatExamByDateExam(examDate);
            ddl_Saat.DataTextField = "saatexam";
            ddl_Saat.DataValueField = "saatexam";
            ddl_Saat.DataBind();
            ddl_Saat.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));
        }


        void RefreshGrid(string term = null, string examPlaceID = "-1", int did = -1, string examDate = "-1", string examTime = "-1")
        {
            if (string.IsNullOrEmpty(ddl_Cities.SelectedValue) || ddl_Cities.SelectedValue?.ToString() == "-2")
            {
                ddl_ExamDate.SelectedValue = "-1";
                ddl_Saat.SelectedValue = "-1";

                RadG_AllDids.DataSource = new DataTable();
                RadG_AllDids.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                var dateexam = (string.IsNullOrEmpty(ddl_ExamDate.SelectedValue) || ddl_ExamDate.SelectedValue == "-1") ? "-1" : ddl_ExamDate.SelectedValue;
                var saatexam = (string.IsNullOrEmpty(ddl_Saat.SelectedValue) || ddl_Saat.SelectedValue == "-1") ? "-1" : ddl_Saat.SelectedValue;

                dt = EBusiness.GetAllDidsByInputFilters(term, int.Parse(examPlaceID), did, examDate, examTime);

                RadG_AllDids.DataSource = dt;
                RadG_AllDids.DataBind();
            }

        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  


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

                BindDrpExamPlaceCities();
                BindDrpExamsDate();
            }

        }

        protected void ddl_ExamDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ExamDate.SelectedValue?.ToString() != "-1")
            {
                BindDrpExamSaatsByExamDate(ddl_ExamDate.SelectedValue);
                RefreshGrid(null, ddl_Cities.SelectedValue, -1, ddl_ExamDate.SelectedValue, ddl_Saat.SelectedValue);
            }
            else
            {
                ddl_Saat.DataSource = new DataTable();
                ddl_Saat.DataBind();

                RadG_AllDids.DataSource = new DataTable();
                RadG_AllDids.DataBind();
            }

        }
        protected void ddl_Saat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Saat.SelectedValue?.ToString() != "-1" && ddl_ExamDate.SelectedValue?.ToString() != "-1")
            {
                RefreshGrid(null, ddl_Cities.SelectedValue, -1, ddl_ExamDate.SelectedValue, ddl_Saat.SelectedValue);
            }
            else
            {
                ddl_ExamDate.SelectedValue = "-1";

                ddl_Saat.DataSource = new DataTable();
                ddl_Saat.DataBind();

                RadG_AllDids.DataSource = new DataTable();
                RadG_AllDids.DataBind();
            }
        }

        protected void RadG_AllDids_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadG_AllDids.DataSource = EBusiness.GetAllDidsByInputFilters(null, int.Parse(ddl_Cities.SelectedValue), -1, ddl_ExamDate.SelectedValue.ToString(), ddl_Saat.SelectedValue.ToString());
            GridFilterMenu menu = this.RadG_AllDids.FilterMenu;
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


        protected void btnChangeExamDateForAllDids_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(ddl_Cities.SelectedValue) && ddl_Cities.SelectedValue != "-2" &&
                !string.IsNullOrEmpty(ddl_ExamDate.SelectedValue) && ddl_ExamDate.SelectedValue != "-1" //&&
                                                                                                        //!string.IsNullOrEmpty(ddl_Saat.SelectedValue) && ddl_Saat.SelectedValue != "-1"
                )
            {
                //var someRecords = GetListOfCancledExamDids();
                //if (someRecords.Count > 0)
                //{
                //    //EBusiness.AddOrUpdate_DLExamQuestionsPermission(someRecords);
                //    RefreshGrid(null, ddl_Cities.SelectedValue, -1, ddl_ExamDate.SelectedValue, ddl_Saat.SelectedValue);
                //}
                ClearLableText();

                lblExamDate.Text = ddl_ExamDate.SelectedValue;
                lblExamTime.Text = ddl_Saat.SelectedValue == "-1" ? "" : ddl_Saat.SelectedValue;

                string scrp = "function f(){showLightBox(); $find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }
            else
            {
                rwm.RadAlert("پارامترهای ورودی به درستی انتخاب نشده اند", null, 100, "پیام", "");
            }
        }

        protected void RadG_AllDids_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Filter" || e.CommandName == "Page") return;
                
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                var qId = commandArgs[0] ?? "-1";
                var did = commandArgs[1] ?? "-1";
                var term = commandArgs[2] ?? "-1";
                var examDate = commandArgs[3] ?? "-1";
                var examTime = commandArgs[4] ?? "-1";
                var courseCode = commandArgs[5] ?? "-1";
                var courseName = commandArgs[6] ?? "-1";
                var profCode = commandArgs[7] ?? "-1";
                var profName = commandArgs[8] ?? "-1";
                var profMobile = commandArgs[9] ?? "-1";
                var newExamDate = commandArgs[10] ?? "";

                if (e.CommandName == "CancleExam")
                {
                    ClearLableText();
                    lblQID.Text = qId.ToString();
                    hdnTerm.Value = term;
                    lblDid.Text = did.ToString();
                    lblExamDate.Text = examDate;
                    lblExamTime.Text = examTime;
                    hdn_CourseCode.Value = courseCode;
                    lblCourseName.Text = courseName;
                    hdn_ProfCode.Value = profCode;
                    lblProfName.Text = profName;
                    lblProfMobile.Text = profMobile;
                    txtNewExamDate.Text = newExamDate;
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "openRadWindow();", true);
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }
        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            RefreshGrid(null, ddl_Cities.SelectedValue, -1, ddl_ExamDate.SelectedValue, ddl_Saat.SelectedValue);
        }


        protected void btnSaveNewExamDateForCanceledDids_Click(object sender, EventArgs e)
        {
            //if (txtNewExamDate.Text.Trim() == "" || txtNewExamDate.Text.Trim() == "" || txtExamReasonCancelation.Text.Trim() == "")
            if (txtNewExamDate.Text.Trim() == "" || txtExamReasonCancelation.Text.Trim() == "")
            {
                rwm.RadAlert("پر کردن فیلدهای علت لغو و تاریخ امتحان ضروری می باشد", null, 100, "پیام", "");
                return;
            }

            List<ExamQuestionInfo> lst = new List<ExamQuestionInfo>();
            if (lblDid.Text.Trim() != "")
            {
                var item = new ExamQuestionInfo();
                item.ExamQuestionID = int.Parse(lblQID.Text);
                item.Term = hdnTerm.Value;
                item.Did = lblDid.Text;
                item.ExamDate = lblExamDate.Text;
                item.ExamTime = lblExamTime.Text;
                item.CourseCode = int.Parse(hdn_CourseCode.Value);
                item.CourseName = lblCourseName.Text;
                item.ProfCode = decimal.Parse(hdn_ProfCode.Value);
                item.ProfName = lblProfName.Text;
                item.CityId = int.Parse(ddl_Cities.SelectedValue);
                item.NewExamDate = txtNewExamDate.Text;
                item.NewExamTime = lblExamTime.Text; //txtNewExamTime.Text;
                item.RejectText = txtExamReasonCancelation.Text;
                item.ApproveNewHeader = true;
                lst.Add(item);
            }
            else //دکمه انتخاب همه کلیک شده بود
            {
                foreach (GridDataItem item in RadG_AllDids.Items)
                {
                    int examQuestionID = int.Parse(item["examQuestionID"].Text);
                    string term = item["term"].Text;
                    string did = item["did"].Text;
                    string examDate = item["examDate"].Text;
                    string examTime = item["examTime"].Text;
                    int courseCode =int.Parse( item["courseCode"].Text);
                    string courseName = item["courseName"].Text;
                    string profCode = item["profCode"].Text;
                    string profName = item["profName"].Text;
                    string profMobile = item["mobile"].Text;

                    lst.Add(new ExamQuestionInfo
                    {
                        ExamQuestionID = examQuestionID,
                        Term = term,
                        Did = did,
                        ExamDate = examDate,
                        ExamTime = examTime,
                        CourseCode=courseCode ,
                        CourseName = courseName,
                        ProfCode = decimal.Parse(profCode),
                        ProfName = profName,
                        //----------------
                        CityId = int.Parse(ddl_Cities.SelectedValue),
                        NewExamDate = txtNewExamDate.Text,
                        NewExamTime = string.IsNullOrWhiteSpace(lblExamTime.Text.Trim()) ? examTime : lblExamTime.Text, //txtNewExamTime.Text ,
                        RejectText = txtExamReasonCancelation.Text,
                        ApproveNewHeader=true
                    });
                }
            }

            if (lst.Count > 0)
            {
                EBusiness.InsertIntoChangedExamQuestionsDate(lst);
                ClearLableText();
                //sendSMSForAllProfs(lst);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "closeRadWindow();refresgGrid();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "closeRadWindow();", true);
            }
        }


        void ClearLableText()
        {
            lblQID.Text = "";
            lblDid.Text = "";
            lblExamDate.Text = "";
            lblExamTime.Text = "";
            hdn_ProfCode.Value = "";
            lblCourseName.Text = "";
            lblProfName.Text = "";
            hdn_ProfCode.Value = "";
            hdnTerm.Value = "";
            lblProfMobile.Text = "";
            txtExamReasonCancelation.Text = "";
            txtNewExamDate.Text = "";
            //txtNewExamTime.Text = "";
        }

        void sendSMSForAllProfs(List<ExamQuestionInfo> lst)
        {
            if (lst != null && lst.Count > 0)
            {
                var smsText = "استاد گرامی امتحان درس ";
                var relativePath = "";
                var absPath = "";
                var zipFilePath = "";
                var transferZipDirectory = "";
                var transferZipFilePath = "";

                lst.ForEach(item =>
                {
                    absPath += Server.MapPath($"~/QueizPapers/{item.Term}/{item.ProfCode}/pdffiles/{item.Did}");
                    if (Directory.Exists(absPath))
                    {
                        zipFilePath += $"{absPath}/{item.Did}.zip";
                        if (File.Exists(zipFilePath))
                        {
                            transferZipDirectory += Server.MapPath($"~/QueizPapers/{item.Term}/{item.ProfCode}/pdffiles/MoveFiles/{item.Did}");
                            if (!Directory.Exists(transferZipDirectory))
                                Directory.CreateDirectory(transferZipDirectory);
                            transferZipFilePath = $"{transferZipDirectory}/{item.Did}_canceled_{new Random().Next(1000, 999999999)}.zip";
                            File.Move(zipFilePath, transferZipFilePath);

                            Directory.GetDirectories(absPath).ToList().ForEach(dir => Directory.Delete(dir, true));
                            //Directory.GetFiles(rootDirectory, "*.*").Where(w => !w.Contains(".zip")).ToList().ForEach(file => File.Delete(file));
                        }
                    }

                    //if (!string.IsNullOrEmpty(item.ProffosorMobile))
                    //{
                    //    cmb = new CommonBusiness();
                    //    smsText += item.CourseName;
                    //    smsText += "با کد مشخصه ی ";
                    //    smsText += item.Did;
                    //    smsText += "لغو گردید و تاریخ برگزاری مجدد ان به تاریخ ";
                    //    smsText += item.NewExamDate;
                    //    smsText += "ساعت ";
                    //    smsText += item.NewExamTime;
                    //    smsText += "موکول گردید .لطفا در اسرع وقت نسبت به بار گزاری مجدد سوال اقدام نمایید با تشکر .";
                    //    cmb.sendSMS(item.ProffosorMobile, smsText, out bool sentSms, out string smsStatusText);
                    //}

                    smsText = "";
                    relativePath = "";
                    absPath = "";
                    zipFilePath = "";
                    transferZipFilePath = "";
                });
            }
        }

    }


}