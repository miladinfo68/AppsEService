using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Web.UI;
using IAUEC_Apps.DTO.University.Exam;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class DownLoadExamQuestionsPermission : System.Web.UI.Page
    {
        ExamBusiness EBusiness = new ExamBusiness();
        List<ExamQuestionInfo> list = new List<ExamQuestionInfo>();


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

            ddl_Saat.DataSource = EBusiness.GetSaatExamByDateExam(examDate,true);
            ddl_Saat.DataTextField = "saatexam";
            ddl_Saat.DataValueField = "saatexam";
            ddl_Saat.DataBind();
            ddl_Saat.Items.Insert(0, new ListItem("انتخاب کنید", "-1"));
        }
                     

        void RefreshGrid(string examPlaceID="-2", string examDate = "-1" ,string examTime = "-1")
        {
            if (string.IsNullOrEmpty(ddl_Cities.SelectedValue) || ddl_Cities.SelectedValue?.ToString() == "-2")
            {
                ddl_ExamDate.SelectedValue = "-1";
                ddl_Saat.SelectedValue = "-1";

                RadG_QuestionList4Download.DataSource = new DataTable();
                RadG_QuestionList4Download.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                var dateexam = (string.IsNullOrEmpty(ddl_ExamDate.SelectedValue) || ddl_ExamDate.SelectedValue == "-1") ? "-1" : ddl_ExamDate.SelectedValue;
                var saatexam = (string.IsNullOrEmpty(ddl_Saat.SelectedValue) || ddl_Saat.SelectedValue == "-1") ? "-1" : ddl_Saat.SelectedValue;

                dt = EBusiness.DLExamQuestionsPermissionByParams(int.Parse(examPlaceID), examDate, examTime);

                RadG_QuestionList4Download.DataSource = dt;
                RadG_QuestionList4Download.DataBind();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddl_Cities.SelectedValue) && ddl_Cities.SelectedValue != "-2"     &&
                !string.IsNullOrEmpty(ddl_ExamDate.SelectedValue) && ddl_ExamDate.SelectedValue != "-1" //&&
                //!string.IsNullOrEmpty(ddl_Saat.SelectedValue) && ddl_Saat.SelectedValue != "-1"
                )
            {
                var someRecords = GetCheckedList();
                if (someRecords.Count > 0)
                {
                    EBusiness.AddOrUpdate_DLExamQuestionsPermission(someRecords);
                    RefreshGrid(ddl_Cities.SelectedValue, ddl_ExamDate.SelectedValue, ddl_Saat.SelectedValue);
                }
            }
            else
            {
                rwm.RadAlert("پارامترهای ورودی به درستی انتخاب نشده اند", null, 100, "پیام", "");
            }
        }

        //protected void ddl_Cities_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if ( string.IsNullOrEmpty(ddl_Cities.SelectedValue)  || ddl_Cities.SelectedValue?.ToString() == "-1"  )
        //    {
        //        ddl_ExamDate.SelectedValue = "-1";
        //        ddl_Saat.SelectedValue = "-1";

        //        RadG_QuestionList4Download.DataSource = new DataTable();
        //        RadG_QuestionList4Download.DataBind();
        //    }
        //    else
        //    {
        //        var dateexam = (string.IsNullOrEmpty(ddl_ExamDate.SelectedValue) || ddl_ExamDate.SelectedValue == "-1") ? "-1" : ddl_ExamDate.SelectedValue;
        //        var saatexam = (string.IsNullOrEmpty(ddl_Saat.SelectedValue) || ddl_Saat.SelectedValue == "-1") ? "-1" : ddl_Saat.SelectedValue;

        //        RefreshGrid(ddl_Cities.SelectedValue, dateexam, saatexam);
        //    }
            
        //}

        protected void ddl_ExamDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ExamDate.SelectedValue?.ToString() != "-1")
            {
                BindDrpExamSaatsByExamDate(ddl_ExamDate.SelectedValue);
                RefreshGrid(ddl_Cities.SelectedValue, ddl_ExamDate.SelectedValue, ddl_Saat.SelectedValue);
            }
            else
            {
                ddl_Saat.DataSource = new DataTable();
                ddl_Saat.DataBind();

                RadG_QuestionList4Download.DataSource = new DataTable();
                RadG_QuestionList4Download.DataBind();
            }

        }
        protected void ddl_Saat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Saat.SelectedValue?.ToString() != "-1" && ddl_ExamDate.SelectedValue?.ToString() != "-1")
            {
                RefreshGrid(ddl_Cities.SelectedValue, ddl_ExamDate.SelectedValue, ddl_Saat.SelectedValue);
            }
            else
            {
                ddl_ExamDate.SelectedValue = "-1";

                ddl_Saat.DataSource = new DataTable();
                ddl_Saat.DataBind();

                RadG_QuestionList4Download.DataSource = new DataTable();
                RadG_QuestionList4Download.DataBind();
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                Session["ChkAll"] = true;
                string scrp = "function f(){showLightBox();  $find(\"" + RadWindowConfirm.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }
        }


        List<ExamQuestionInfo> GetCheckedList(bool chkAll = false)
        {
            List<ExamQuestionInfo> lst;
            if (chkAll)
            {
                foreach (GridDataItem item in RadG_QuestionList4Download.MasterTableView.Items)
                {
                    HiddenField hdn_ID = (HiddenField)item["colChkDid"].FindControl("hdn_ID");
                    HiddenField hdn_Term = (HiddenField)item["colChkDid"].FindControl("hdn_Term");
                    HiddenField hdn_ProfCode = (HiddenField)item["colChkDid"].FindControl("hdn_ProfCode");
                    HiddenField hdn_ProfName = (HiddenField)item["colChkDid"].FindControl("hdn_ProfName");
                    HiddenField hdn_Did = (HiddenField)item["colChkDid"].FindControl("hdn_Did");
                    HiddenField hdn_CourseName = (HiddenField)item["colChkDid"].FindControl("hdn_CourseName");
                    HiddenField hdnExamDate = (HiddenField)item["colChkDid"].FindControl("hdnExamDate");
                    HiddenField hdnExamTime = (HiddenField)item["colChkDid"].FindControl("hdnExamTime");
                    list.Add(new ExamQuestionInfo()
                    {
                        ID = long.Parse(hdn_ID.Value),
                        Term = hdn_Term.Value,
                        ProfCode = decimal.Parse(hdn_ProfCode.Value),
                        ProfName = hdn_ProfName.Value,
                        Did = hdn_Did.Value,
                        CourseName = hdn_CourseName.Value,
                        ExamDate = hdnExamDate.Value,
                        ExamTime = hdnExamTime.Value,
                        CityId=int.Parse(ddl_Cities.SelectedValue) ,
                        IsActive = true
                    });
                }
                lst = list;
            }

            else
            {
                foreach (GridDataItem item in RadG_QuestionList4Download.MasterTableView.Items)
                {
                    CheckBox chkDid = (CheckBox)item["colChkDid"].FindControl("chkDid");
                    //if (chkDid != null && chkDid.Checked)
                    //{
                    HiddenField hdn_ID = (HiddenField)item["colChkDid"].FindControl("hdn_ID");
                    HiddenField hdn_Term = (HiddenField)item["colChkDid"].FindControl("hdn_Term");
                    HiddenField hdn_ProfCode = (HiddenField)item["colChkDid"].FindControl("hdn_ProfCode");
                    HiddenField hdn_ProfName = (HiddenField)item["colChkDid"].FindControl("hdn_ProfName");
                    HiddenField hdn_Did = (HiddenField)item["colChkDid"].FindControl("hdn_Did");
                    HiddenField hdn_CourseName = (HiddenField)item["colChkDid"].FindControl("hdn_CourseName");
                    HiddenField hdnExamDate = (HiddenField)item["colChkDid"].FindControl("hdnExamDate");
                    HiddenField hdnExamTime = (HiddenField)item["colChkDid"].FindControl("hdnExamTime");
                    list.Add(new ExamQuestionInfo()
                    {
                        ID = long.Parse(hdn_ID.Value),
                        Term = hdn_Term.Value,
                        ProfCode = decimal.Parse(hdn_ProfCode.Value),
                        ProfName = hdn_ProfName.Value,
                        Did = hdn_Did.Value,
                        CourseName = hdn_CourseName.Value,
                        ExamDate = hdnExamDate.Value,
                        ExamTime = hdnExamTime.Value,
                        CityId = int.Parse(ddl_Cities.SelectedValue),
                        IsActive = chkDid.Checked ? true : false
                    });
                    //}
                }
                lst = list;
            }
            return lst;
        }

        protected void btnConfirmation_Click(object sender, EventArgs e)
        {
            var selectAllChks = bool.Parse(Session["ChkAll"]?.ToString() ?? "0");
            if (selectAllChks)
            {
                var allRecords = GetCheckedList(selectAllChks);
                if (allRecords.Count > 0)
                {
                    EBusiness.AddOrUpdate_DLExamQuestionsPermission(allRecords);
                    RefreshGrid(ddl_Cities.SelectedValue, ddl_ExamDate.SelectedValue, ddl_Saat.SelectedValue);
                }
            }
            Session["ChkAll"] = false;
        }


        protected void RadG_QuestionList4Download_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HiddenField isActive = e.Item.FindControl("hdn_IsActive") as HiddenField;
                CheckBox chkDid = e.Item.FindControl("chkDid") as CheckBox;
                chkDid.Checked = int.Parse(isActive.Value?.ToString() ?? "0") > 0 ? true : false;
            }
        }

        protected void RadG_QuestionList4Download_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadG_QuestionList4Download.DataSource = EBusiness.DLExamQuestionsPermissionByParams(int.Parse(ddl_Cities.SelectedValue), ddl_ExamDate.SelectedValue.ToString(), ddl_Saat.SelectedValue.ToString());
            GridFilterMenu menu = this.RadG_QuestionList4Download.FilterMenu;
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

       
    }


}