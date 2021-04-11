using IAUEC_Apps.Business.ResourceControl;
using ResourceControl.BLL;
using ResourceControl.Entity; 
using System;
using System.Collections.Generic;
using System.Linq;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class EducationUserReport : System.Web.UI.Page
    {
        int _daneshId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["daneshId"] != null)
                {
                    _daneshId = Convert.ToInt32(Session["daneshId"]);
                }

                if (_daneshId == 0)
                {
                    DaneshkadeHandler dHandler = new DaneshkadeHandler();
                    drpDanshkade.DataSource = dHandler.GetAllDaneshkade();
                    drpDanshkade.DataTextField = "NameDanesh";
                    drpDanshkade.DataValueField = "ID";
                    drpDanshkade.DataBind();
                    dvDanshkade.Visible = true;
                    drpDanshkade.Items.Insert(0, "انتخاب کنید");
                }
                else
                {
                    RC_UserHandler userhandler = new RC_UserHandler();
                    drpProfessors.DataSource = userhandler.GetOstadListByDaneshID(_daneshId);
                    drpProfessors.DataTextField = "Name";
                    drpProfessors.DataValueField = "ID";
                    drpProfessors.DataBind();
                }
            }
        }

        protected void drpDanshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpDanshkade.SelectedIndex != 0)
            {
                int danId = Convert.ToInt32(drpDanshkade.SelectedValue);
                RC_UserHandler userhandler = new RC_UserHandler();
                List<RC_User> ostadList = userhandler.GetOstadListByDaneshID(danId);
                if (ostadList != null)
                {
                    ostadList.ToList().ForEach(i => i.Name = i.Name.Replace("ي", "ی"));

                    Telerik.Web.UI.RadComboBoxItem other1 = new Telerik.Web.UI.RadComboBoxItem(text: "سایر", value: "0");
                    drpProfessors.DataSource = ostadList;
                    drpProfessors.DataTextField = "Name";
                    drpProfessors.DataValueField = "ID";
                    drpProfessors.DataBind();
                    drpProfessors.Items.Add(other1);
                }
                else
                {
                    drpProfessors.Items.Clear();
                    grdLessons.DataSource = null;
                    grdLessons.DataBind();
                }
            }
        }

        protected void btnShowResult_Click(object sender, EventArgs e)
        {
            int codeOstad = 0;
            if (!string.IsNullOrEmpty(drpProfessors.SelectedValue.ToString()))
                codeOstad = Convert.ToInt32(value: drpProfessors.SelectedValue.ToString());
            CourseHandler courseHandler = new CourseHandler();

            var courseList = courseHandler.GetCourseListByUserID(codeOstad);
            if (courseList != null)
            {
                var percensCourseList = courseList.Where(r => r.catID == 2).OrderBy(o => o.DID).ToList();
                var courses = courseList.Select(x => x.DID).ToList().Distinct().ToList();
                var listForBindToGrid = new List<List<Course>>();

                foreach (var courseDid in courses)
                {
                    var list = new List<Course>();
                    foreach (var item in percensCourseList)
                    {
                        if (courseDid == item.DID)
                        {
                            list.Add(item);
                        }
                    }
                    if (list.Count > 0)
                        listForBindToGrid.Add(list);
                    else
                    {
                        var courseWithoutAnyRegisterationClass = courseHandler.GetCourseListByUserID(codeOstad).FirstOrDefault(c => c.DID == courseDid);
                        listForBindToGrid.Add(new List<Course> { courseWithoutAnyRegisterationClass });
                    }
                }
                var listForCount = new List<CourseCounter>();

                foreach (List<Course> list in listForBindToGrid)
                {
                    var submittedCount = list.Count(s => s.status == 0);
                    var sendCount = list.Count(s => s.status == 1);
                    var approvedCount = list.Count(s => s.status == 2);
                    var deniedCount = list.Count(s => s.status == 3);
                    var informedCont = list.Count(s => s.status == 4);
                    var losedCont = list.Count(s => s.status == 5);

                    listForCount.Add(new CourseCounter()
                    {
                        DID = list.Select(x => x.DID).FirstOrDefault(),
                        Name = list.Select(x => x.Name).FirstOrDefault(),
                        Capacity = list.Select(x => x.Capacity).FirstOrDefault(),
                        DaneshID = list.Select(x => x.DaneshID).FirstOrDefault(),
                        saatklass = list.Select(x => x.saatklass).FirstOrDefault(),
                        status = list.Select(x => x.status).FirstOrDefault(),
                        catID = list.Select(x => x.catID).FirstOrDefault(),
                        Submitted = submittedCount,
                        Send = sendCount,
                        Approved = approvedCount,
                        Denied = deniedCount,
                        Informed = informedCont,
                        Losed = losedCont,
                        TotalCount = submittedCount + sendCount + approvedCount + deniedCount + informedCont + losedCont,
                    });

                }
                grdLessons.DataSource = listForCount;
                grdLessons.DataBind();
            }
            else
            {
                grdLessons.DataSource = null;
                grdLessons.DataBind();
                string scrp = "گزارشی در مورد استاد مورد نظر موجود نمی باشد";

                string resdirectFunc = "";
                RadWindowManager1.RadAlert(scrp, 300, 100, "پیام سیستم", resdirectFunc);
            }










        }

        //protected void grdLessons_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        CourseCounter datarow = (CourseCounter)e.Row.DataItem;
        //        int did = Convert.ToInt32(datarow.DID);
        //        GridView grdRequests = (GridView)e.Row.FindControl("grdRequests");
        //        RequestHandler reqHandler = new RequestHandler();
        //        List<RequestFR> reqList = reqHandler.GetRequestListByDID(did);
        //        grdRequests.DataSource = reqList;
        //        grdRequests.DataBind();
        //    }
        //}

        public string GetImage(int value)
        {
            if (value == 5)
            {
                return ResolveUrl("~/ResourceControl/Images/Messaging-Question-icon.png");
            }
            if (value == 4)
            {
                return ResolveUrl("~/ResourceControl/Images/informed.png");
            }
            if (value == 3)
            {
                return ResolveUrl("~/ResourceControl/Images/deny.png");
            }
            if (value == 2)
            {
                return ResolveUrl("~/ResourceControl/Images/Approved-icon.png");
            }
            if (value == 1)
            {
                return ResolveUrl("~/ResourceControl/Images/send-file-32.png");
            }
            if (value == 0)
            {
                return ResolveUrl("~/ResourceControl/Images/waiting.jpg");
            }
            else
            {
                return ResolveUrl("~/ResourceControl/Images/red_trans_question.png");
            }
        }

        //protected void grdRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Image img = (Image)e.Row.FindControl("imgStatus");
        //        RequestFR dataRow = (RequestFR)e.Row.DataItem;
        //        switch (dataRow.Status)
        //        {
        //            case 0:
        //                img.ImageUrl = GetImage(dataRow.Status);
        //                img.ToolTip = "در حال بررسی آموزش";
        //                break;
        //            case 1:
        //                img.ImageUrl = GetImage(dataRow.Status);
        //                img.ToolTip = "منتظر تایید اداری";
        //                break;
        //            case 2:
        //                img.ImageUrl = GetImage(dataRow.Status);
        //                img.ToolTip = "تایید اداری";
        //                break;
        //            case 3:
        //                img.ImageUrl = GetImage(dataRow.Status);
        //                img.ToolTip = "رد شده";
        //                break;
        //            case 4:
        //                img.ImageUrl = GetImage(dataRow.Status);
        //                img.ToolTip = "اطلاع رسانی شده";
        //                break;
        //            case 5:
        //                img.ImageUrl = GetImage(dataRow.Status);
        //                img.ToolTip = "از دست رفته";
        //                break;
        //            default:
        //                img.ImageUrl = GetImage(dataRow.Status);
        //                img.ToolTip = "نامشخص";
        //                break;
        //        }
        //    }
        //}

        //protected void grdRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName=="check")
        //    {
        //        int reqId = Convert.ToInt32(e.CommandArgument);

        //        List<Resource> resList = new List<Resource>();
        //        ResourceHandler rsh = new ResourceHandler();
        //        resList = rsh.GetResourceListByReqID(reqId);
        //        ViewState.Add("reqId", reqId);
        //        ViewState.Add("_resList", resList);

        //        RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
        //        List<RequestDateTime> _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
        //        grdDateTime.DataSource = _dateTimeList;
        //        grdDateTime.DataBind();

        //        string scrp = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        //        ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        //    }
        //}

        //protected void grdDateTime_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        RequestDateTime reqDateTime = (RequestDateTime)e.Row.DataItem;
        //        DropDownList drpResource = (DropDownList)e.Row.FindControl("drpResource");

        //        List<Resource> _resList = (List<Resource>)ViewState["_resList"];
        //        drpResource.DataSource = _resList;
        //        drpResource.DataTextField = "name";
        //        drpResource.DataValueField = "ID";
        //        drpResource.DataBind();
        //        drpResource.Items.Insert(0, new ListItem { Value = "0", Text = "انتخاب کنید..." });
        //        drpResource.Items.FindByValue(reqDateTime.ResourceId.ToString()).Selected = true;
        //    }
        //}
    }
}