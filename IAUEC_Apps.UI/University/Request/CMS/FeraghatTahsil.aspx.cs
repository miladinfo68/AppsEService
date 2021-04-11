using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;
using System.Linq;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class FeraghatTahsil : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

        FeraghatTahsilBusiness bsn = new FeraghatTahsilBusiness();

        FeraghatTahsilDTO oFeraghat = new FeraghatTahsilDTO();

        CheckOutRequestBusiness business = new CheckOutRequestBusiness();

        string stcode;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
            if (!IsPostBack)
            {
                ViewState["howToBindGrid"] = "BindGrid()";
                ViewState["fromGraduateDoc"] = false;
                BindGrid();
                if (Session["stcode_GraduateDoc"] != null && Session["fromGraduateDoc"].ToString() == true.ToString())
                {
                    ViewState["howToBindGrid"] = "searchStudent(oFeraghat)";

                    ViewState["fromGraduateDoc"] = true;
                    txtStcode.Text = Session["stcode_GraduateDoc"].ToString();
                    oFeraghat.Stcode = txtStcode.Text;
                    oFeraghat.family = txtFamily.Text;
                    ViewState.Add("stcode", oFeraghat.Stcode);
                    searchStudent(oFeraghat);
                }
            }
            Session["fromGraduateDoc"] = false;
            RegPcal();
        }

        private void BindGrid()
        {
            CheckOutRequestBusiness business = new CheckOutRequestBusiness();
            var dt = business.GetListOFRequestByNextStatusForFaregh((int)(CheckOutStatusEnum.CheckOutAllStatusEnum.end));

            grd_Students.DataSource = dt;
            grd_Students.DataBind();
            GridFilterMenu menu = grd_Students.FilterMenu;
            filterMenu(menu);
        }


        private void filterMenu(GridFilterMenu menu)
        {
            if (menu.Items.Count > 5)
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
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grd_Info.Visible = false;
            grd_Students.Visible = false;


            if (txtFamily.Text == string.Empty && txtStcode.Text == string.Empty) //at least one text box should have a value for search
            {
                string msg = "حداقل بايد يکي از باکس ها را پر کنيد";

                RadWindowManager1.RadAlert(msg, 300, 100, "پیام", "closeStatusPopup", "");
            }
            else
            {
                oFeraghat.Stcode = txtStcode.Text;
                oFeraghat.family = txtFamily.Text;
                ViewState["fromGraduateDoc"] = null;
                Session["stcode_GraduateDoc"] = null;
                Session["fromGraduateDoc"] = null;
                ViewState.Add("stcode", oFeraghat.Stcode);
                //DataTable check = new DataTable();
                //check = business.checkExistingRequest(oFeraghat.Stcode);
                searchStudent(oFeraghat);
                ViewState["howToBindGrid"] = "searchStudent(oFeraghat)";

            }

        }


        private void searchStudent(FeraghatTahsilDTO feraghat)
        {
            int logId = -1;

            dt = business.GetCheckOutInfoByStCodeAndFamily(feraghat);
            if (dt.Rows.Count != 0)
            {
                logId = Convert.ToInt32(dt.Rows[0]["RequestLogID"].ToString());
            }

            if (isAlreadyExist())
            {
                if (logId != Convert.ToInt32(CheckOutStatusEnum.FareghReqStatus.end))
                {
                    string msg = "درخواست دانشجوی مورد نظر هنوز توسط اداره فارغ التحصیلان تایید نشده است";

                    RadWindowManager1.RadAlert(msg, 300, 100, "پیام", "closeStatusPopup", "");
                }
                else
                {
                    grd_Students.Visible = true;
                    var ds = dt.Clone();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToInt64(row["RequestTypeID"]) == 15)
                            ds.Rows.Add(row.ItemArray);
                    }
                    grd_Students.DataSource = ds;
                    grd_Students.DataBind();
                    grd_Students.Visible = true;
                }
            }
            else
            {
                grd_Info.Visible = true;
                dt = bsn.getStudentInfo(feraghat);
                grd_Info.DataSource = dt;
                grd_Info.DataBind();

                ViewState.Add("dt", dt);
                grd_Info.Visible = true;
            }
        }
        private void LoadRadWindowNaghs(string StudentRequestId, string stcode)
        {
            ViewState.Add("reqId", StudentRequestId);
            ViewState.Add("stNaghs", stcode);
            CheckOutNaghsBusiness NaghsBusiness = new CheckOutNaghsBusiness();
            DataTable dtNaghs = NaghsBusiness.GetAllNaghsByReqId(Convert.ToInt32(StudentRequestId));
            grdNaghs1.DataSource = dtNaghs;
            grdNaghs1.DataBind();
            string scrp5 = "function f(){$find(\"" + RadWindowNaghs.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp5, true);
        }

        protected void btnSubmitStatus_Click(object sender, EventArgs e)
        {

            var btn = (Button)sender;

            if (Page.IsValid)
            {
                FeraghatTahsilBusiness bFeraghat = new FeraghatTahsilBusiness();
                FeraghatTahsilDTO oFeraghat = new FeraghatTahsilDTO();
                oFeraghat.Id = Convert.ToInt32(hdnfFeraghatId.Value);//Convert.ToInt32(ViewState["feraghatId"]);
                oFeraghat.Stcode = lblStcode.Text;
                oFeraghat.StudentRequestId = Convert.ToInt32(hdnfReqId.Value);//Convert.ToInt32(ViewState["reqId"]);
                oFeraghat.RizNomarat = Convert.ToInt32(chbkRizNomarat.Checked);
                oFeraghat.GovahiMovaghat = Convert.ToInt32(chbkGovahiMovaghat.Checked);
                oFeraghat.DaneshNameh = Convert.ToInt32(chbkDaneshNameh.Checked);
                oFeraghat.DateDaneshNameh = txtDaneshname.Text.Trim().formatDateString();
                oFeraghat.DateGovahiMovaghat = txtGovahiMovaghat.Text.Trim().formatDateString();
                oFeraghat.DateRizNomarat = txtRizNomre.Text.Trim().formatDateString();
                oFeraghat.dateSodoorDaneshname = txtSodoorDaneshname.Text.Trim().formatDateString();
                oFeraghat.dateSodoorGovahi = TxtSodoorGovahiMovaghat.Text.Trim().formatDateString();
                oFeraghat.dateSodoorRizNomre = txtSodoorRizNomre.Text.Trim().formatDateString();
                //oFeraghat.dateVoroodDaneshname = (bFeraghat.GetFeraghatMadrekStatus(oFeraghat.StudentRequestId).dateVoroodDaneshname.ToString()== null ? "" : bFeraghat.GetFeraghatMadrekStatus(oFeraghat.StudentRequestId).dateVoroodDaneshname.ToString());
                //oFeraghat.dateVoroodGovahi = (bFeraghat.GetFeraghatMadrekStatus(oFeraghat.StudentRequestId).dateVoroodGovahi.ToString() == null ? "" : bFeraghat.GetFeraghatMadrekStatus(oFeraghat.StudentRequestId).dateVoroodGovahi.ToString()); 
                //oFeraghat.dateVoroodRizNomre = (bFeraghat.GetFeraghatMadrekStatus(oFeraghat.StudentRequestId).dateVoroodRizNomre.ToString() == null ? "" : bFeraghat.GetFeraghatMadrekStatus(oFeraghat.StudentRequestId).dateVoroodGovahi.ToString());
                var madrakStatus = bFeraghat.GetFeraghatMadrekStatus(oFeraghat.StudentRequestId);
                oFeraghat.dateVoroodDaneshname = ((madrakStatus == null || string.IsNullOrEmpty(madrakStatus.dateVoroodDaneshname)) ? string.Empty : bFeraghat.GetFeraghatMadrekStatus(oFeraghat.StudentRequestId).dateVoroodDaneshname.ToString()).Trim().formatDateString();
                oFeraghat.dateVoroodGovahi = ((madrakStatus == null || string.IsNullOrEmpty(madrakStatus.dateVoroodGovahi)) ? string.Empty : bFeraghat.GetFeraghatMadrekStatus(oFeraghat.StudentRequestId).dateVoroodGovahi.ToString()).Trim().formatDateString();
                oFeraghat.dateVoroodRizNomre = ((madrakStatus == null || string.IsNullOrEmpty(madrakStatus.dateVoroodRizNomre)) ? string.Empty : bFeraghat.GetFeraghatMadrekStatus(oFeraghat.StudentRequestId).dateVoroodRizNomre.ToString()).Trim().formatDateString();


                oFeraghat.dateErsalRizNomre = txtErsalRizNomre.Text.Trim().formatDateString();



                int id = bFeraghat.UpdateMadarekStatus(oFeraghat, Convert.ToInt32(Session[sessionNames.userID_Karbar]), true, !Convert.ToBoolean(ViewState["fromGraduateDoc"]));
                string msg = "";
                if (id > 0)
                {

                    msg = "بروز رسانی انجام شد.";
                    var oCommon = new CommonBusiness();
                    var yes = "بلی";
                    var no = "خیر";
                    var checkRiz = "ندارد";
                    var checkDanesh = "ندارد";
                    var checkGovahi = "ندارد";
                    if (chbkDaneshNameh.Checked)
                        checkDanesh = "دارد";
                    if (chbkGovahiMovaghat.Checked)
                        checkGovahi = "دارد";
                    if (chbkRizNomarat.Checked)
                        checkRiz = "دارد";

                    setLog($"صدور ریز نمرات : {txtSodoorRizNomre.Text} --" + " " + $"صدور گواهی موقت : {TxtSodoorGovahiMovaghat.Text} --" + " " + $"صدور دانشنامه : {txtSodoorDaneshname.Text} --" + $"ارسال ریز نمره: {txtErsalRizNomre.Text}" + $"تحویل ریز نمرات : {txtRizNomre.Text} --" + $"تحویل گواهی موقت : {txtGovahiMovaghat.Text} --" + $"تحویل دانشنامه : {txtDaneshname.Text} --" + $"تیک ریز نمره : {checkRiz} --" + $"تیک گواهی موقت : {checkGovahi} --" + $"تیک دانشنامه : {checkDanesh} ",
                        oFeraghat.StudentRequestId,
                        (int)DTO.eventEnum.ویرایش_وضعیت_مدرک_دانشجو);
                    if (Session["stcode_GraduateDoc"] != null)
                        Response.Redirect("showGraduateDocument.aspx");
                }
                switch (ViewState["howToBindGrid"].ToString())
                {
                    case "searchStudent(oFeraghat)":
                        searchStudent(new FeraghatTahsilDTO() { Stcode = txtStcode.Text, family = txtFamily.Text });
                        break;
                    default:

                        BindGrid();
                        break;
                }

                RadWindowManager1.RadAlert(msg, 300, 100, "پیام", "closeStatusPopup", "");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseAndRebind", "CloseAndRebind(true);", true);
                //ClientScript.RegisterClientScriptBlock(GetType(), "ardalertdone", scrp);
                //chkLoan.Checked = false;
            }
            else
            {

                var msg = "تاریخ وارد شده باید قبل از تاریخ امروز باشد";
                switch (ViewState["howToBindGrid"].ToString())
                {
                    case "searchStudent(oFeraghat)":
                        searchStudent(new FeraghatTahsilDTO() { Stcode = txtStcode.Text, family = txtFamily.Text });
                        break;
                    default:

                        BindGrid();
                        break;
                }

                RadWindowManager1.RadAlert(msg, 300, 100, "پیام", "closeradwindow4", "");


            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            grd_Info.Visible = false;
            grd_Students.Visible = false;
            txtFamily.Text = string.Empty;
            txtStcode.Text = string.Empty;
        }

        protected bool isAlreadyExist()
        {
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                if (dt.Rows[i]["stcode"].ToString() == txtStcode.Text || string.Compare(txtFamily.Text, dt.Rows[i]["name"].ToString()) <= 0)
                {
                    return true;
                }
            }
            return false;
        }

        //protected void grd_Info_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    dt = (DataTable)ViewState["dt"];
        //    grd_Info.PageIndex = e.NewPageIndex;
        //    grd_Info.DataSource = dt;
        //    grd_Info.DataBind();
        //}

        protected void grd_Info_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {

                FeraghatTahsilBusiness bFeraghat = new FeraghatTahsilBusiness();
                int reqID;
                GridViewRow curruntRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                stcode = curruntRow.Cells[1].Text;
                //string note = "تایید فارغ التحصیلان";
                string note = "درحال بررسی";
                string CreateDate = DateTime.Now.ToPeString();
                reqID = business.InsertInToStudentRequest(stcode, (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil, Convert.ToInt32(CheckOutStatusEnum.CheckOutAllStatusEnum.takmil_parvande), CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat.ToString(), "", CreateDate, note, 0, true);//حذف_دبیرخانه  امکان ثبت درخواست در کدام مرحله؟
                setLog("ثبت درخواست تسویه حساب فارغ التحصیلی از صفحه وضعیت مدارک دانشجو", reqID, (int)DTO.eventEnum.ثبت_درخواست_تسویه);
                oFeraghat.Id = 0;
                oFeraghat.RizNomarat = 0;
                oFeraghat.GovahiMovaghat = 0;
                oFeraghat.DaneshNameh = 0;
                oFeraghat.Stcode = stcode;
                oFeraghat.StudentRequestId = reqID;

                bFeraghat.UpdateMadarekStatus(oFeraghat, Convert.ToInt32(Session[sessionNames.userID_Karbar]), true/*chkLoan.Checked*/);

                string msg = "با موفقیت ثبت گردید";

                RadWindowManager1.RadAlert(msg, 300, 100, "پیام", "closeStatusPopup", "");

                btnSearch_Click(sender, e);
            }
        }

        protected void grd_Info_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = bsn.getStudentInfo(oFeraghat);
            grd_Info.DataSource = dt;
            GridFilterMenu menu = grd_Info.FilterMenu;
            filterMenu(menu);
        }

        protected void grd_Students_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //dt = business.GetListOFRequestByNextStatus((int)CheckOutStatusEnum.FareghReqStatus.fareghotahsilan_ok + 1);
            dt = business.GetListOFRequestByNextStatusForFaregh((int)(CheckOutStatusEnum.FareghReqStatus.end));
            //grd_Students.DataSource = dt;
            //grd_Students.DataBind();
            grd_Students.Visible = true;
            //var ds = dt.Clone();
            //foreach (DataRow row in dt.Rows)
            //{
            //    if (Convert.ToInt64(row["RequestTypeID"]) == 15)
            //        ds.Rows.Add(row.ItemArray);
            //}
            grd_Students.DataSource = dt;


            GridFilterMenu menu = grd_Students.FilterMenu;
            filterMenu(menu);
        }

        protected void grd_Info_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void grdStudents_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        //public void ShowRad(int requestId)
        //{
        //    txtRizNomre.Text = "";
        //    txtGovahiMovaghat.Text = "";
        //    txtDaneshname.Text = "";
        //    txtSodoorDaneshname.Text = "";
        //    TxtSodoorGovahiMovaghat.Text = "";
        //    txtSodoorRizNomre.Text = "";




        //    int reqId = requestId;// Convert.ToInt32(e.CommandArgument);
        //    GridDataItem itemAmount = (GridDataItem)e.Item;
        //    TableCell st = (TableCell)itemAmount["stcode"];
        //    TableCell name = (TableCell)itemAmount["name"];
        //    lblStName.Text = name.Text;
        //    lblStcode.Text = st.Text;
        //    ViewState.Add("reqId", reqId.ToString());
        //    hdnfReqId.Value = reqId.ToString();
        //    hdnfFeraghatId.Value = 0.ToString();
        //    FeraghatTahsilBusiness bFeraghat = new FeraghatTahsilBusiness();
        //    FeraghatTahsilDTO oFeraghat = bFeraghat.GetFeraghatMadrekStatus(reqId);
        //    //FeraghatTahsilDAO daoFeraghat = new FeraghatTahsilDAO();
        //    //DataTable loan = new DataTable();

        //    //loan = daoFeraghat.getLoanInfo(oFeraghat.Stcode);

        //    //if (loan.Rows.Count > 0)
        //    //    lblVamDar.Visible = true;
        //    //else
        //    //    lblVamDar.Visible = false;

        //    if (oFeraghat != null)
        //    {
        //        if (oFeraghat.RizNomarat > 0)
        //        {
        //            chbkRizNomarat.Checked = true;

        //            if (oFeraghat.DateRizNomarat != null)
        //            {
        //                txtRizNomre.Text = oFeraghat.DateRizNomarat;

        //            }
        //            if (oFeraghat.dateSodoorRizNomre != null)
        //            {
        //                txtSodoorRizNomre.Text = oFeraghat.dateSodoorRizNomre;

        //            }

        //        }
        //        else
        //        {
        //            chbkRizNomarat.Checked = false;
        //        }

        //        if (oFeraghat.GovahiMovaghat > 0)
        //        {
        //            chbkGovahiMovaghat.Checked = true;
        //            if (oFeraghat.DateGovahiMovaghat != null)
        //            {
        //                txtGovahiMovaghat.Text = oFeraghat.DateGovahiMovaghat;

        //            }
        //            if (oFeraghat.dateSodoorGovahi != null)
        //            {
        //                TxtSodoorGovahiMovaghat.Text = oFeraghat.dateSodoorGovahi;

        //            }

        //        }
        //        else
        //        {
        //            chbkGovahiMovaghat.Checked = false;
        //        }

        //        if (oFeraghat.DaneshNameh > 0)
        //        {
        //            chbkDaneshNameh.Checked = true;
        //            if (oFeraghat.DateDaneshNameh != null)
        //            {
        //                txtDaneshname.Text = oFeraghat.DateDaneshNameh;

        //            }
        //            if (oFeraghat.dateSodoorDaneshname != null)
        //            {
        //                txtSodoorDaneshname.Text = oFeraghat.dateSodoorDaneshname;

        //            }

        //        }
        //        else
        //        {
        //            chbkDaneshNameh.Checked = false;
        //        }

        //        //     ViewState.Add("feraghatId", oFeraghat.Id.ToString());
        //        hdnfFeraghatId.Value = oFeraghat.Id.ToString();
        //    }
        //    else
        //    {
        //        chbkRizNomarat.Checked = false;
        //        chbkGovahiMovaghat.Checked = false;
        //        chbkDaneshNameh.Checked = false;
        //    }

        //    string scrp = "function f(){$find(\"" + statusPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        //    ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        //}

        protected void grd_Students_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "show")
            {
                var indexOf = e.CommandArgument.ToString().IndexOf('-');
                int reqId;
                if (indexOf == -1)
                    reqId = Convert.ToInt32(e.CommandArgument);

                else
                    reqId = Convert.ToInt32(e.CommandArgument.ToString().Split('-')[0]);
                GridDataItem itemAmount = (GridDataItem)e.Item;
                TableCell st = (TableCell)itemAmount["stcode"];
                TableCell name = (TableCell)itemAmount["name"];

                setFeraghatPopup(reqId, name.Text, st.Text);

                string scrp = "function f(){$find(\"" + statusPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }
            if (e.CommandName == "return")
            {
                var splitedCommandArgument = e.CommandArgument.ToString().Split('-');
                var requestId = splitedCommandArgument[0];
                var studentCode = splitedCommandArgument[1];
                LoadRadWindowNaghs(requestId, studentCode);
            }
        }

        private void setFeraghatPopup(int reqId, string studentName, string stcode)
        {
            txtRizNomre.Text = "";
            txtGovahiMovaghat.Text = "";
            txtDaneshname.Text = "";
            txtSodoorDaneshname.Text = "";
            TxtSodoorGovahiMovaghat.Text = "";
            txtSodoorRizNomre.Text = "";
            txtErsalRizNomre.Text = "";
            if (Convert.ToBoolean(bsn.ExistRiz(reqId)))
            {
                btnReceiptRiz.Enabled = false;
                hdnRiznomre.Value = signatureImage(1, reqId);
                dvSignatureRiznomre.Visible = true;

            }
            else
            {
                btnReceiptRiz.Enabled = true;
                hdnRiznomre.Value = "";
                dvSignatureRiznomre.Visible = false;
            }
            if (Convert.ToBoolean(bsn.ExistGovahi(reqId)))
            {
                btnReceiptGovahi.Enabled = false;
                hdnGovahi.Value = signatureImage(2, reqId);
                dvSignatureGovahi.Visible = true;
            }
            else
            {
                btnReceiptGovahi.Enabled = true;
                hdnGovahi.Value = null;
                dvSignatureGovahi.Visible = false;
            }
            if (Convert.ToBoolean(bsn.ExistDanesh(reqId)))
            {
                btnReceiptDanesh.Enabled = false;
                hdnDaneshname.Value = signatureImage(3, reqId);
                dvSignatureDaneshname.Visible = true;

            }
            else
            {
                btnReceiptDanesh.Enabled = true;
                hdnDaneshname.Value = "";
                dvSignatureDaneshname.Visible = false;
            }
            lblStName.Text = studentName;
            lblStcode.Text = stcode;
            ViewState.Add("reqId", reqId.ToString());
            hdnfReqId.Value = reqId.ToString();
            hdnfFeraghatId.Value = 0.ToString();
            FeraghatTahsilBusiness bFeraghat = new FeraghatTahsilBusiness();
            FeraghatTahsilDTO oFeraghat = bFeraghat.GetFeraghatMadrekStatus(reqId);


            hdnReqID.Value = reqId.ToString();
            if (oFeraghat != null)
            {
                lblArchiveDanesh.Text = oFeraghat.archiveCode_daneshname == 0 ? "-" : oFeraghat.archiveCode_daneshname.ToString();
                lblArchiveMovaghat.Text = oFeraghat.archiveCode_movaghat == 0 ? "-" : oFeraghat.archiveCode_movaghat.ToString();
                lblArchiveRiz.Text = oFeraghat.archiveCode_rizNomre == 0 ? "-" : oFeraghat.archiveCode_rizNomre.ToString();
                if (oFeraghat.RizNomarat > 0)
                {
                    chbkRizNomarat.Checked = true;

                    if (oFeraghat.DateRizNomarat != null)
                    {
                        txtRizNomre.Text = oFeraghat.DateRizNomarat;

                    }
                    if (oFeraghat.dateSodoorRizNomre != null)
                    {
                        txtSodoorRizNomre.Text = oFeraghat.dateSodoorRizNomre;

                    }
                    if (oFeraghat.dateErsalRizNomre != null)
                    {
                        txtErsalRizNomre.Text = oFeraghat.dateErsalRizNomre;

                    }

                }
                else
                {
                    chbkRizNomarat.Checked = false;
                    if (oFeraghat.dateErsalRizNomre != null)
                    {
                        txtErsalRizNomre.Text = oFeraghat.dateErsalRizNomre.ToString();

                    }
                    if (oFeraghat.dateSodoorRizNomre != null)
                    {
                        txtSodoorRizNomre.Text = oFeraghat.dateSodoorRizNomre.ToString();

                    }
                    if (oFeraghat.DateRizNomarat != null)
                    {
                        txtRizNomre.Text = oFeraghat.DateRizNomarat.ToString();

                    }
                }

                if (oFeraghat.GovahiMovaghat > 0)
                {
                    chbkGovahiMovaghat.Checked = true;
                    if (oFeraghat.DateGovahiMovaghat != null)
                    {
                        txtGovahiMovaghat.Text = oFeraghat.DateGovahiMovaghat;

                    }
                    if (oFeraghat.dateSodoorGovahi != null)
                    {
                        TxtSodoorGovahiMovaghat.Text = oFeraghat.dateSodoorGovahi;

                    }

                }
                else
                {
                    chbkGovahiMovaghat.Checked = false;

                    if (oFeraghat.DateGovahiMovaghat != null)
                    {
                        txtGovahiMovaghat.Text = oFeraghat.DateGovahiMovaghat.ToString();

                    }
                    if (oFeraghat.dateSodoorGovahi != null)
                    {
                        TxtSodoorGovahiMovaghat.Text = oFeraghat.dateSodoorGovahi.ToString();

                    }
                }

                if (oFeraghat.DaneshNameh > 0)
                {
                    chbkDaneshNameh.Checked = true;
                    if (oFeraghat.DateDaneshNameh != null)
                    {
                        txtDaneshname.Text = oFeraghat.DateDaneshNameh;

                    }
                    if (oFeraghat.dateSodoorDaneshname != null)
                    {
                        txtSodoorDaneshname.Text = oFeraghat.dateSodoorDaneshname;

                    }

                }
                else
                {
                    chbkDaneshNameh.Checked = false;
                    if (oFeraghat.DateDaneshNameh != null)
                    {
                        txtDaneshname.Text = oFeraghat.DateDaneshNameh.ToString();

                    }
                    if (oFeraghat.dateSodoorDaneshname != null)
                    {
                        txtSodoorDaneshname.Text = oFeraghat.dateSodoorDaneshname.ToString();

                    }
                }

                hdnfFeraghatId.Value = oFeraghat.Id.ToString();
                btnCreateArchiveCode_MadrakMovaghat.Visible = (oFeraghat.archiveCode_movaghat == 0 && oFeraghat.DateGovahiMovaghat == "");
                btnCreateArchiveCode_Daneshname.Visible = (oFeraghat.archiveCode_daneshname == 0 && oFeraghat.DateDaneshNameh == "");
                btnCreateArchiveCode_Riznomre.Visible = (oFeraghat.archiveCode_rizNomre == 0 && oFeraghat.DateRizNomarat == "");
            }
            else
            {
                chbkRizNomarat.Checked = false;
                chbkGovahiMovaghat.Checked = false;
                chbkDaneshNameh.Checked = false;
                btnCreateArchiveCode_MadrakMovaghat.Visible = true;
                btnCreateArchiveCode_Daneshname.Visible = true;
                btnCreateArchiveCode_Riznomre.Visible = true;
                lblArchiveDanesh.Text = "-";
                lblArchiveMovaghat.Text = "-";
                lblArchiveRiz.Text = "-";
            }
            CheckOutMaliBusiness MaliBusiness = new CheckOutMaliBusiness();
            if (MaliBusiness.HasAnyRefahDebit(lblStcode.Text))
            {
                vamdarBanner.Visible = true;
            }
            else
            {
                vamdarBanner.Visible = false;
            }
            CheckOutRequestBusiness reqBus = new CheckOutRequestBusiness();
            var mash = reqBus.isMashmoolferaghat(lblStcode.Text);
            if (mash != null)
            {
                if (mash > 0)
                {
                    mashmoolBanner.Visible = true;
                }
                else
                {
                    mashmoolBanner.Visible = false;
                }
            }

        }

        protected void grd_Info_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                stcode = ViewState["stcode"].ToString();
                DataTable check ;
                check = business.checkExistingRequest(stcode);

                if (check.Rows.Count == 0)//condition //////
                {
                    FeraghatTahsilBusiness bFeraghat = new FeraghatTahsilBusiness();
                    int reqID;
                    GridDataItem itemAmount = (GridDataItem)e.Item;
                    TableCell st = (TableCell)itemAmount["stcode"];
                    stcode = st.Text;
                    //string note = "تایید فارغ التحصیلان";
                    string note = "درحال بررسی";
                    string CreateDate = DateTime.Now.ToPeString();
                    reqID = business.InsertInToStudentRequest(stcode, (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil, Convert.ToInt32(CheckOutStatusEnum.CheckOutAllStatusEnum.takmil_parvande), Convert.ToInt32(CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat).ToString(), "", CreateDate, note, 0, true);//حذف_دبیرخانه  امکان ثبت درخواست تسویه در کدام مرحله؟
                    setLog("ثبت درخواست تسویه حساب فارغ التحصیلی از صفحه وضعیت مدارک دانشجو", reqID, (int)DTO.eventEnum.ثبت_درخواست_تسویه);

                    oFeraghat.Id = 0;
                    oFeraghat.RizNomarat = 0;
                    oFeraghat.GovahiMovaghat = 0;
                    oFeraghat.DaneshNameh = 0;
                    oFeraghat.Stcode = stcode;
                    oFeraghat.StudentRequestId = reqID;

                    bFeraghat.UpdateMadarekStatus(oFeraghat, Convert.ToInt32(Session[sessionNames.userID_Karbar]), true/*chkLoan.Checked*/);

                    string msg = "با موفقیت ثبت گردید";

                    RadWindowManager1.RadAlert(msg, 300, 100, "پیام", "closeStatusPopup", "");

                    btnSearch_Click(sender, e);

                }
                else
                {
                    string alert = "این درخواست قبلا ثبت شده است";
                    RadWindowManager1.RadAlert(alert, 0, 200, "هشدار", "");
                    return;
                }
            }
        }
        private void RegPcal()
        {
            string scrp1 = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_statusPopup_C_txtRizNomre', {extraInputID: 'ContentPlaceHolder1_statusPopup_C_txtRizNomre',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp2 = "var objCal2 = new AMIB.persianCalendar('ContentPlaceHolder1_statusPopup_C_txtGovahiMovaghat', {extraInputID: 'ContentPlaceHolder1_statusPopup_C_txtGovahiMovaghat',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp3 = "var objCal3 = new AMIB.persianCalendar('ContentPlaceHolder1_statusPopup_C_txtDaneshname', {extraInputID: 'ContentPlaceHolder1_statusPopup_C_txtDaneshname',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp4 = "var objCal4 = new AMIB.persianCalendar('ContentPlaceHolder1_statusPopup_C_txtSodoorRizNomre', {extraInputID: 'ContentPlaceHolder1_statusPopup_C_txtSodoorRizNomre',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp5 = "var objCal5 = new AMIB.persianCalendar('ContentPlaceHolder1_statusPopup_C_TxtSodoorGovahiMovaghat', {extraInputID: 'ContentPlaceHolder1_statusPopup_C_TxtSodoorGovahiMovaghat',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp6 = "var objCal6 = new AMIB.persianCalendar('ContentPlaceHolder1_statusPopup_C_txtSodoorDaneshname', {extraInputID: 'ContentPlaceHolder1_statusPopup_C_txtSodoorDaneshname',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp7 = "var objCal6 = new AMIB.persianCalendar('ContentPlaceHolder1_statusPopup_C_txtErsalRizNomre', {extraInputID: 'ContentPlaceHolder1_statusPopup_C_txtErsalRizNomre',extraInputFormat: 'yyyy/mm/dd'}); ";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, scrp1 + scrp2 + scrp3 + scrp4 + scrp5 + scrp6 + scrp7, true);
        }


        protected void vldTxtRizNomreTahvil_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtRizNomre.Text) && txtRizNomre.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }



        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtSodoorRizNomre.Text) && txtSodoorRizNomre.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtGovahiMovaghat.Text) && txtGovahiMovaghat.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }

        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(TxtSodoorGovahiMovaghat.Text) && TxtSodoorGovahiMovaghat.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator5_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtDaneshname.Text) && txtDaneshname.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator6_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtSodoorDaneshname.Text) && txtSodoorDaneshname.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator7_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtErsalRizNomre.Text) && txtErsalRizNomre.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void grdNaghs1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                int reqLogId = Convert.ToInt32(rowView["RequestLogId"]);
                int erae_Be = Convert.ToInt32(rowView["Erae_Be"]);
                bool isResolved = Convert.ToBoolean(rowView["IsResolved"]);

                GridDataItem item = (GridDataItem)e.Item;
                TableCell cellreqLodId = (TableCell)item["RequestLogId"];
                cellreqLodId.Text = business.GetPersianStatus(reqLogId);
                TableCell cellerae_Be = (TableCell)item["Erae_Be"];
                cellerae_Be.Text = business.GetPersianStatus(erae_Be);
                TableCell cellisResolved = (TableCell)item["IsResolved"];
                if (isResolved)
                {
                    cellisResolved.Text = "رفع نقص";
                    cellisResolved.CssClass = "text-success";
                }
                else
                {
                    cellisResolved.Text = "عدم رفع نقص";
                    cellisResolved.CssClass = "text-danger";
                }
            }
        }

        protected void grdNaghs1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string naghsId = (e.Item as GridDataItem).GetDataKeyValue("NaghsId").ToString();
                CheckOutNaghsBusiness NaghsBus = new CheckOutNaghsBusiness();
                var RequestLogId = ((int)CheckOutStatusEnum.FareghReqStatus.vrood_moavenat_ok);
                var Erae_be = ((int)CheckOutStatusEnum.FareghReqStatus.ersal_sodoor_ok);
                var stcode = ViewState["stNaghs"].ToString();
                //int count = 
                NaghsBus.DeleteNaghs(Convert.ToInt32(naghsId), Convert.ToInt32(Erae_be), Convert.ToInt32(RequestLogId), Convert.ToInt32(ViewState["reqId"]));
            }

        }

        protected void grdNaghs1_ItemCommand1(object sender, GridCommandEventArgs e)
        {

        }

        protected void grdNaghs1_ItemDataBound1(object sender, GridItemEventArgs e)
        {

        }

        protected void btnSubmitNaghs_Click(object sender, EventArgs e)
        {
            if (txtNaghsDescription.Text != "")
            {
                CheckOutNaghsDTO oNaghs = new CheckOutNaghsDTO();
                CheckOutNaghsBusiness NaghsBus = new CheckOutNaghsBusiness();
                oNaghs.StudentRequestId = Convert.ToInt32(ViewState["reqId"]);
                oNaghs.StCode = ViewState["stNaghs"].ToString();

                oNaghs.RequestLogId = ((int)CheckOutStatusEnum.FareghReqStatus.end);
                oNaghs.Erae_Be = ((int)CheckOutStatusEnum.FareghReqStatus.end).ToString();


                oNaghs.SubmitDate = DateTime.Now.ToPeString();
                oNaghs.NaghsMessage = "نقص: " + txtNaghsDescription.Text;
                int id = NaghsBus.InsertOdat(oNaghs);
                var userID = Session[sessionNames.userID_Karbar].ToString();
                business.SendOdatMessageAndInsertOdatLog(userID, oNaghs.StudentRequestId, oNaghs.NaghsMessage);
                if (id > 0)
                {
                    lblNaghsMessage.Text = "نقص پرونده با موفقیت درج شد.";
                    txtNaghsDescription.Text = "";


                    //   ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "Close();", true);


                }
                else
                {
                    lblNaghsMessage.Text = "خطا در درج نقص ! لطفا مجددا تلاش کنید یا با مدیر سامانه تماس بگیرید..";
                }
                LoadRadWindowNaghs(oNaghs.StudentRequestId.ToString(), oNaghs.StCode);
                txtNaghsDescription.Text = "";
                //   btnSubmitMsg.Enabled = false;
                //    this.btnSubmitMsg.Enabled = false;
                //   BindData(Convert.ToInt32(drpUserRoles.SelectedItem.Value), false);

            }
            else
            {
                RadWindowManager1.RadAlert("علت نقص پرونده را ذکر کنید... ", 300, 100, "پیام سیستم", "");
                //   lblNaghsMessage.Text = "علت نقص پرونده را ذکر کنید... ";

            }
        }



        protected void btnReceiptGovahi_Click(object sender, EventArgs e)
        {
            Session[sessionNames.IdentityNumber] = Convert.ToInt32(hdnfReqId.Value.ToString());
            Session[sessionNames.appID_Karbar] = 12;
            Session[sessionNames.TypeSignature] = 2;
            string scrp = "function f(){$find(\"" + rdwReceipt.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);

        }

        protected void btnReceiptDanesh_Click(object sender, EventArgs e)
        {
            Session[sessionNames.IdentityNumber] = Convert.ToInt32(hdnfReqId.Value.ToString());
            Session[sessionNames.appID_Karbar] = 12;
            Session[sessionNames.TypeSignature] = 3;
            string scrp = "function f(){$find(\"" + rdwReceipt.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        }

        protected void btnReceiptRiz_Click(object sender, EventArgs e)
        {
            Session[sessionNames.IdentityNumber] = Convert.ToInt32(hdnfReqId.Value.ToString());
            Session[sessionNames.appID_Karbar] = 12;
            Session[sessionNames.TypeSignature] = 1;
            string scrp = "function f(){$find(\"" + rdwReceipt.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="signatureType">1:riznomre   2:govahi   3:daneshname</param>
        /// <param name="reqID"></param>
        /// <returns></returns>
        private string signatureImage(int signatureType, int reqID)
        {
            var appId = 12;
            var type = signatureType;

            var business = new CommonBusiness();

            var data = business.GetSignature(
                reqID,
                appId, type);
            var signature = data.FirstOrDefault();
            if (signature != null)
                return Convert.ToBase64String(signature.Image);
            return "";
        }

        protected void btnCreateArchiveCode_MadrakMovaghat_Click(object sender, EventArgs e)
        {
            if (chbkGovahiMovaghat.Checked)
            {
                bsn.setMadrakArchiveCode(Convert.ToInt32(hdnfReqId.Value), 1);
                setFeraghatPopup(Convert.ToInt32(hdnfReqId.Value), lblStName.Text, lblStcode.Text);
                string scrp = "function f(){$find(\"" + statusPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }
        }

        protected void btnCreateArchiveCode_Daneshname_Click(object sender, EventArgs e)
        {
            if (chbkDaneshNameh.Checked)
            {
                bsn.setMadrakArchiveCode(Convert.ToInt32(hdnfReqId.Value), 2);
                setFeraghatPopup(Convert.ToInt32(hdnfReqId.Value), lblStName.Text, lblStcode.Text);
                string scrp = "function f(){$find(\"" + statusPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }
        }

        protected void btnCreateArchiveCode_Riznomre_Click(object sender, EventArgs e)
        {
            if (chbkRizNomarat.Checked)
            {
                bsn.setMadrakArchiveCode(Convert.ToInt32(hdnfReqId.Value), 3);
                setFeraghatPopup(Convert.ToInt32(hdnfReqId.Value), lblStName.Text, lblStcode.Text);
                string scrp = "function f(){$find(\"" + statusPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }
        }

        private void setLog(string description, int requestID, int eventID)
        {
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            int userId;//کاربری که لاگین کرده
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            int modifyId;//کد درخواست ویرایش شده
            userId = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            appId = 12;
            modifyId = requestID;
            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, eventID, description, modifyId);
        }

        protected void btnExportExcel_Baygani_Click(object sender, EventArgs e)
        {
            var dt = business.getAllArchiveID_GraduateDocuments();
            var ms = DataTableToExcelXlsx(dt);

            ms.WriteTo(Response.OutputStream);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + "کد بایگانی مدارک فارغ التحصیلی -" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + " - " + DateTime.Now.Hour + ".xlsx");
            Response.StatusCode = 200;
            Response.End();
        }
        private System.IO.MemoryStream DataTableToExcelXlsx(DataTable dt)
        {
            DataTable dtstcode = new DataTable();
            dtstcode.Columns.Add("codeclass", typeof(string));
            var pack = new OfficeOpenXml.ExcelPackage();
            var result = new System.IO.MemoryStream();

            var ws = pack.Workbook.Worksheets.Add("کد بایگانی مدارک");

            for (int col = 0; col < 7; col++)
            {


                ws.Cells[1, col + 1].Value = dt.Columns[col].Caption.ToString();
                //ws.Cells[1, col + 1].StyleValue = "headerStyle";
                int r = 2;
                if (dt.Rows.Count > 0)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        ws.Cells[r, col + 1].Value = dt.Rows[row][col];
                        //ws.Cells[1, col + 1].StyleValue = (r % 2 == 0 ? "styleEven" : "styleODD");
                        r++;
                    }
                }
            }
            pack.SaveAs(result);
            return result;
        }
    }
}