using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections.Generic;
using Ionic.Zip;
using System.Web;
using System.ComponentModel;
using Telerik.Web.UI.GridExcelBuilder;

namespace IAUEC_Apps.UI.University.Request.CMS
{

    public partial class CheckOutReview : System.Web.UI.Page
    {
        CheckOutRequestBusiness reqBsn = new CheckOutRequestBusiness();
        CheckOutRefahBusiness RefahBsn = new CheckOutRefahBusiness();
        CheckOutMaliBusiness MaliBusiness = new CheckOutMaliBusiness();
        StuPresentBusiness StudentBsn = new StuPresentBusiness();
        CommonBusiness commonBsn = new CommonBusiness();
        FeraghatTahsilBusiness fraghatBusiness = new FeraghatTahsilBusiness();
        LoginBusiness lngB = new LoginBusiness();
        DataTable userdt = new DataTable();
        bool checkmoadl = true;
        int item;
        int flag = 0;
        const string vs_currentCartable = "currentStatus", vs_reqID = "requestId", vs_stcode = "studentCode", vs_ReqType = "requestType", vs_stNaghs = "stcode_Naghs";
        public string embedSrc = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                fillDropdownCartable();
                BindData((Convert.ToInt32(drpUserRoles.SelectedItem.Value)), false);

            }
        }

        private void fillDropdownCartable()
        {
            drpUserRoles.DataSource = null;
            drpUserRoles.DataBind();

            userdt = lngB.Get_UserRoles(Session[sessionNames.userID_Karbar].ToString());
            var listOfRole = new List<int>();


            if (userdt.Rows.Count > 0)
            {
                foreach (DataRow item in userdt.Rows)
                {
                    var temp = Convert.ToInt32(item[1]);
                    if (ISCheckoutRole(temp))
                        listOfRole.Add(temp);
                    else
                        grd_CheckOutList.Visible = false;
                    drpUserRoles.Visible = false;
                    lblKartabl.Visible = false;
                }
                if (listOfRole.Contains(30))
                    item = 30;
            }


            var listKartabl = RoleBind(listOfRole);
            if (listKartabl.Count != 0 && listKartabl != null)
            {
                drpUserRoles.DataSource = listKartabl;
                drpUserRoles.DataTextField = "Text";
                drpUserRoles.DataValueField = "Value";
                drpUserRoles.DataBind();

                drpUserRoles.Enabled = true;
                drpUserRoles.Visible = true;
                if (drpUserRoles.Items[0].Value != null)
                {
                    drpUserRoles.SelectedValue = drpUserRoles.Items[0].Value;
                }
                else
                {
                    showMessage("این سامانه برای شما غیر فعال است");
                }

                flag = 0;
                ViewState.Add("flag", flag);
            }
        }

        private List<ListItem> RoleBind(List<int> listOfRole)
        {
            var listOfStatus = new List<int>();
            var listOfItems = new List<ListItem>();
            if (listOfRole.Count > 0)
            {
                drpUserRoles.Visible = true;
                lblKartabl.Visible = true;
                grd_CheckOutList.Visible = true;
            }

            foreach (var Role in listOfRole)
            {
                var tblStatus = reqBsn.GetListOfStatusByRoleId(Role);
                if (tblStatus != null && tblStatus.Rows.Count > 0)
                {
                    foreach (DataRow sts in tblStatus.Rows)
                    {
                        if (!listOfStatus.Contains(Convert.ToInt32(sts["status"])))
                        {
                            listOfStatus.Add(Convert.ToInt32(sts["status"]));
                        }
                    }
                }
            }
            foreach (var item in listOfStatus)
            {
                listOfItems.Add(new ListItem
                {
                    Text = reqBsn.GetPersianStatus(item),
                    Value = item.ToString()
                });

            }
            return listOfItems;
        }

        protected void drpUserRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (drpUserRoles.SelectedIndex > -1)
            {
                ViewState["ThesisTypeSelected"] = 0;
                grd_CheckOutList.CurrentPageIndex = 0;
                grd_CheckOutList.Visible = true;
                //Session.Add("activeRoleStatus", drpUserRoles.SelectedValue);
                BindData((Convert.ToInt32(drpUserRoles.SelectedItem.Value)), false);

                switch (Convert.ToInt32(drpUserRoles.SelectedItem.Value))
                {
                    //                    case (int)CheckOutStatusEnum.FareghReqStatus.fareghotahsilan_ok:
                    case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.refah:
                        malidesc.Visible = true;
                        naghsdesc.Visible = false;
                        entekhabvahed.Visible = true;
                        hozur.Visible = true;

                        break;
                    case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.maali:
                        malidesc.Visible = true;
                        naghsdesc.Visible = false;
                        entekhabvahed.Visible = true;
                        hozur.Visible = true;

                        break;
                    case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.mashmulan:
                        malidesc.Visible = false;
                        naghsdesc.Visible = false;
                        entekhabvahed.Visible = false;
                        hozur.Visible = false;
                        break;
                    case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat:

                        naghsdesc.Visible = true;
                        malidesc.Visible = false;
                        entekhabvahed.Visible = false;
                        hozur.Visible = false;

                        break;

                    case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.ersal_sodoor:
                        malidesc.Visible = false;
                        naghsdesc.Visible = true;
                        entekhabvahed.Visible = false;
                        hozur.Visible = false;

                        break;
                    default:
                        malidesc.Visible = false;
                        naghsdesc.Visible = false;
                        entekhabvahed.Visible = false;
                        hozur.Visible = false;

                        break;
                }

            }
            else
            {
                grd_CheckOutList.Visible = false;
            }
            lblMessage.Text = "";
            rwMessage.VisibleOnPageLoad = false;
            rwReasonFrequency.VisibleOnPageLoad = false;
            txtMsg.Text = "";

        }

        private void BindData(int status, bool isneeddatasource)
        {
            DataTable reqList = getRequestList(status);
            ViewState.Add(vs_currentCartable, status);

            grd_CheckOutList.DataSource = reqList;
            rdGridExcel.DataSource = reqList;
            rdGridExcel.DataBind();

            if (!isneeddatasource)
                grd_CheckOutList.DataBind();

            setFilterMenu(grd_CheckOutList.FilterMenu);

        }

        private DataTable getRequestList(int cartableValue)
        {
            DataTable reqList;
            int roleId = Convert.ToInt32(Session["roleId"]);
            int daneshId = reqBsn.GetDaneshKadeIdByRoleId(roleId);

            grd_CheckOutList.Columns.FindByDataField("dateUploadThesis").Visible = cartableValue == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.pajohesh;
            lblThesisType.Visible = cartableValue == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.pajohesh;
            ddlThesisType.Visible = cartableValue == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.pajohesh;
            btnDlExcel.Visible = true;//= cartableValue == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.pajohesh;
            if (ddlThesisType.Visible)
            {
                ViewState["ThesisTypeSelected"] = "1";
            }
            if (ViewState["ThesisTypeSelected"] != null && ViewState["ThesisTypeSelected"].ToString() == "1")
            {
                reqList = reqBsn.GetListOFRequestByNextStatus_BythesisFileStatus(cartableValue, Convert.ToInt32(ddlThesisType.SelectedItem.Value));
            }
            else if (cartableValue == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshkade
                || cartableValue == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.takmil_parvande
                || cartableValue == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive)
            {
                if (daneshId == 0)
                {
                    reqList = reqBsn.GetListOFRequestByNextStatus(cartableValue);

                }
                else
                {
                    reqList = reqBsn.GetListOFRequestByNextStatusAndDaneshId(cartableValue, daneshId);
                }
            }
            else
            {
                reqList = reqBsn.GetListOFRequestByNextStatus(cartableValue);
            }
            return reqList;
        }

        private void setFilterMenu(GridFilterMenu menu)
        {
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

        private bool HasAcountNo(string stCode)
        {
            return MaliBusiness.HasAcountNo(stCode);

        }

        private void LoadRadWindowNaghs(string StudentRequestId, string stcode)
        {
            ViewState.Add(vs_reqID, StudentRequestId);
            ViewState.Add(vs_stNaghs, stcode);
            CheckOutNaghsBusiness NaghsBusiness = new CheckOutNaghsBusiness();
            DataTable dtNaghs = NaghsBusiness.GetAllNaghsByReqId(Convert.ToInt32(StudentRequestId));
            grdNaghs1.DataSource = dtNaghs;
            grdNaghs1.DataBind();
            string scrp5 = "function f(){$find(\"" + RadWindowNaghs.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp5, true);
        }
        private void LoadRadWindowVahed(string StudentRequestId, string stcode)
        {
            ViewState.Add(vs_reqID, StudentRequestId);
            ViewState.Add("stVahed", stcode);
            List<MahaleSodoor> dtVahed = reqBsn.GetListOfVahed();
            //List<string> lstVahed = new List<string>();
            //foreach (var item in dtVahed.Rows)
            //{

            //    lstVahed.Add(item.ToString());
            //}
            drpMahaleSodooreMadrak.DataSource = dtVahed;
            drpMahaleSodooreMadrak.DataTextField = "vahed";
            drpMahaleSodooreMadrak.DataValueField = "id";
            drpMahaleSodooreMadrak.DataBind();
            string scrp5 = "function f(){$find(\"" + RadWindowVahed.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp5, true);
        }
        private void LoadRadwindowBedehi()
        {
            grdDebit.DataSource = null;
            grdDebit.DataBind();
            DataTable dt = null;
            DataTable dtRefah = null;

            string stcode = ViewState[vs_stcode].ToString();
            string stName = ViewState["stName"].ToString();
            int eraeBe = Convert.ToInt32(ViewState[vs_currentCartable]);

            lblStCode.Text = stcode;
            lblStName.Text = stName;
            lbl_meli.Text = ViewState["idd_meli"].ToString();
            lbl_mobile.Text = ViewState["mobile"].ToString();
            dtRefah = RefahBsn.GetAllDebitByStcode(stcode);

            if (eraeBe == (int)CheckOutStatusEnum.EnserafReqStatus.refah_ok)
            {
                bool hasvezaratLoan = dtRefah.AsEnumerable()
                                        .Where(i => i.Field<int>("debittypeId") == 1)
                                        .Any();
                if (hasvezaratLoan)
                {
                    btnShowLastMaghta.Visible = true;
                }
                grdDebit.DataSource = dtRefah;
                grdDebit.DataBind();
                grdDebit.Columns[3].Visible = true;
            }
            if (eraeBe == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
            {
                dvBedehiSida.Visible = true;
                btnShowLastMaghta.Visible = false;
                dt = MaliBusiness.GetAllMaliDebitByStcode(stcode);
                if (dt.Rows[0]["sidabedehi"] != null)
                {

                    var sidBed = dt.Rows[0]["sidabedehi"].ToString();
                    var _3dig = String.Format("{0:n0}", Convert.ToInt32(sidBed));
                    lblBedehiSida.Text = _3dig.ToString();
                }
                if (ViewState[vs_ReqType].ToString() == ((int)CheckOutStatusEnum.CheckOutType.taqir_reshte).ToString())
                {
                    DataTable newStInfo = MaliBusiness.GetNewStCode(stcode);
                    if (newStInfo.Rows.Count > 0)
                    {
                        dvNewStCode.Visible = true;
                        lblNewStCode.Text = newStInfo.Rows[0]["stcode"].ToString();
                    }
                }
                if (MaliBusiness.GetAllMaliDebitByStcode(stcode).Rows[0]["RefID"].ToString() != "0")
                {
                    grdDebit.DataSource = MaliBusiness.GetAllMaliDebitByStcode(stcode);
                    grdDebit.DataBind();
                    grdDebit.Columns[3].Visible = false;
                }

            }
            txtFishNumber.Text = "";
            txtAmount.Text = "";
            txtNumber.Text = "";
            txtFishDate.Text = "";
            txtTasvieAmount.Text = "";
            txtNote.Text = "";
            drpDebitType.SelectedIndex = 0;




            //************************************




            string scrp = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        }

        protected void btnSubmitMsg_Click(object sender, EventArgs e)
        {
            userdt = lngB.Get_UserRoles(Session[sessionNames.userID_Karbar].ToString());
            ViewState.Add("UserRoleId", userdt.Rows[0][1]);
            string msg = txtMsg.Text;
            if (msg != string.Empty)
            {
                flag = (int)ViewState["flag"];

                int reqID = (int)ViewState[vs_reqID];
                int currentstatus = (int)ViewState[vs_currentCartable];
                int reqType = (int)ViewState[vs_ReqType];
                int status = Convert.ToInt32(ViewState[vs_currentCartable]);



                msg = reqBsn.SendMessage(Session[sessionNames.userID_Karbar].ToString(), reqID, txtMsg.Text);

                fraghatBusiness.sendSmsForMsg(reqID, ViewState[vs_stcode].ToString());
                rwMessage.VisibleOnPageLoad = false;
                if (drpUserRoles.SelectedIndex > 0)
                    BindData((Convert.ToInt32(drpUserRoles.SelectedValue)), false);
                else
                    BindData(Convert.ToInt32(reqBsn.GetStatusOfRole(Convert.ToInt32(ViewState["UserRoleId"].ToString()))), false);

            }
            else
            {
                msg = "لطفا متن پیام را وارد کنید";
                showMessage(msg);
            }
        }

        protected void btnDebitSubmit_Click(object sender, EventArgs e)
        {
            string stcode = ViewState[vs_stcode].ToString();
            string fishNumberRegex = "^[A-Za-z0-9\\u0600-\u06FF\\s _]*[A-Za-z0-9\\u0600-\\u06FF\\s][A-Za-z0-9\\u0600-\\u06FF\\s _]{0,20}$";
            string textAndNumberRegex = "^[A-Za-z0-9\\u0600-\u06FF\\s _]*[A-Za-z0-9\\u0600-\\u06FF\\s][A-Za-z0-9\\u0600-\\u06FF\\s _]{0,200}$";
            string dateRegex = "\\d{4}(?:/\\d{1,2}){2}";
            string strNumber = null;
            string strAmount = null;
            string strTasvieAmount = null;
            string strNote = null;
            string strFishNumber = null;
            string strFishDate = null;
            string strError = null;
            int eraeBe = Convert.ToInt32(ViewState[vs_currentCartable]);

            if (!string.IsNullOrWhiteSpace(txtNumber.Text) && !string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                if (!new Regex(fishNumberRegex).IsMatch(txtNumber.Text))
                {
                    strError = "شماره وارد شده نامعتبر است";
                    goto label;
                }
                else
                {
                    strNumber = txtNumber.Text;
                }
                if (!new Regex(fishNumberRegex).IsMatch(txtAmount.Text))
                {
                    if (txtAmount.Text != string.Empty)
                    {
                        strError = "مبلغ وارد شده نامعتبر است";
                        goto label;
                    }
                }
                else
                {
                    strAmount = txtAmount.Text;
                }
                if (!new Regex(fishNumberRegex).IsMatch(txtTasvieAmount.Text))
                {
                    if (txtTasvieAmount.Text != string.Empty)
                    {
                        strError = "مبلغ وارد شده نامعتبر است";
                        goto label;
                    }
                }
                else
                {
                    strTasvieAmount = txtTasvieAmount.Text;
                }
                if (!string.IsNullOrWhiteSpace(txtFishNumber.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtFishDate.Text))
                    {
                        if (!new Regex(fishNumberRegex).IsMatch(txtFishNumber.Text))
                        {
                            strError = "شماره فیش وارد شده نامعتبر است.";
                            goto label;
                        }
                        else
                        {

                            strFishNumber = txtFishNumber.Text;
                        }
                        if (!new Regex(dateRegex).IsMatch(txtFishDate.Text))
                        {
                            strError = "فرمت تاریخ وارد شده صحیح نمی باشد.";
                            goto label;
                        }
                        else
                        {
                            strFishDate = txtFishDate.Text;
                        }
                    }
                    else
                    {
                        strError = "در صورت درج شماره فیش درج تاریخ آن الزامی است.";
                        goto label;
                    }
                }
                if (!string.IsNullOrWhiteSpace(txtNote.Text))
                {
                    if (!new Regex(textAndNumberRegex).IsMatch(txtNote.Text))
                    {
                        strError = "توضیحات وارد شده نامعتبر است";
                        goto label;
                    }
                }
            }
            else
            {
                strError = "درج شماره و مبلغ اجباری است";
                goto label;
            }

        label: if (!string.IsNullOrWhiteSpace(strError))
            {
                lblDebitError.Text = strError;
                lblDebitError.Visible = true;


                LoadRadwindowBedehi();
                return;
            }
            if (eraeBe == (int)CheckOutStatusEnum.EnserafReqStatus.refah_ok)
            {
                RefahBsn.InsertUpdateDebit(Session[sessionNames.userID_Karbar].ToString(), stcode, strNumber, strAmount, strTasvieAmount, strNote, Convert.ToInt32(drpDebitType.SelectedValue), strFishNumber, strFishDate);
            }
            if (eraeBe == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
            {
                MaliBusiness.InsertUpdateDebit(Session[sessionNames.userID_Karbar].ToString(), stcode, strNumber, strAmount, strNote, Convert.ToInt32(drpDebitType.SelectedValue), strFishNumber, strFishDate);
            }
            lblDebitError.Text = "";


            LoadRadwindowBedehi();
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                BindData((Convert.ToInt32(ViewState[vs_currentCartable])), false);
            }
        }

        protected void btnDeleteTerm_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                count = MaliBusiness.RemoveVahedInTerm(Session[sessionNames.userID_Karbar].ToString(), ViewState[vs_stcode].ToString());
            }
            catch (Exception)
            {

                throw;
            }
            string strMessage;
            if (count > 0)
            {
                strMessage = "انتخاب واحد دانشجو با موفقیت حذف شد.";
            }
            else
            {
                strMessage = "واحدی حذف نشد!";
            }
            showMessage(strMessage,"پیام سیستم", "ReloadPage");
        }

        protected void btnShowLastMaghta_Click(object sender, EventArgs e)
        {
            DataTable dt = RefahBsn.GetLastUniInfo(ViewState[vs_stcode].ToString());
            grdPastMaghtaInfo.DataSource = dt;
            grdPastMaghtaInfo.DataBind();

            ClearAllAddressFields();

            DataTable dtAddress = RefahBsn.GetStudentAddress(ViewState[vs_stcode].ToString());
            if (dtAddress.Rows.Count > 0)
            {
                lblProvince.Text = dtAddress.Rows[0]["province"].ToString();
                lblCity.Text = dtAddress.Rows[0]["city"].ToString();
                lblStreet.Text = dtAddress.Rows[0]["street"].ToString();
                lblAlley.Text = dtAddress.Rows[0]["alley"].ToString();
                lblPelak.Text = dtAddress.Rows[0]["pelak"].ToString();
                lblZipCode.Text = dtAddress.Rows[0]["ZipCode"].ToString();
                lblPhone.Text = dtAddress.Rows[0]["phone"].ToString();
                lblMobile.Text = dtAddress.Rows[0]["mobile"].ToString();
                lblEmail.Text = dtAddress.Rows[0]["email"].ToString();
                lblRabetPhone.Text = dtAddress.Rows[0]["RabetPhone"].ToString();
                lblRabetMobile.Text = dtAddress.Rows[0]["RabetMobile"].ToString();
            }

            string scrp3 = "var objCal4 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_RadWindow1_C_txtFishDate', {extraInputID: 'ctl00_ContentPlaceHolder1_RadWindow1_C_txtFishDate',extraInputFormat: 'yyyy/mm/dd'});" + "function f(){$find(\"" + RadWindowLastMaghta.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp3, true);
        }

        protected void grdDebit_ItemCommand(object sender, GridCommandEventArgs e)
        {
            DropDownList drpDebitType = (DropDownList)e.Item.FindControl("drpDebitType");
            TextBox txtNumber = (TextBox)e.Item.FindControl("TB_DebitNumber");
            TextBox txtAmount = (TextBox)e.Item.FindControl("TB_DebitAmount");
            TextBox txtTasvie = (TextBox)e.Item.FindControl("TB_TotalLoanAmount");
            TextBox txtFishNumber = (TextBox)e.Item.FindControl("TB_FishNumber");
            TextBox txtFishDate = (TextBox)e.Item.FindControl("TB_FishDate");
            TextBox txtNote = (TextBox)e.Item.FindControl("TB_Note");

            if (e.CommandName == "Edit")
            {
                LoadRadwindowBedehi();
            }

            if (e.CommandName == "Update")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                StudentCheckOutDebit debit = new StudentCheckOutDebit();
                debit.RefID = Convert.ToInt64(dataItem.GetDataKeyValue("RefID").ToString());
                debit.StCode = ViewState[vs_stcode].ToString();
                debit.DebitNumber = txtNumber.Text;
                debit.TotalLoanAmount = txtTasvie.Text;
                debit.DebitTypeID = Convert.ToInt32(drpDebitType.SelectedValue);
                debit.DebitAmount = txtAmount.Text;
                debit.FishNumber = txtFishNumber.Text;
                debit.FishDate = txtFishDate.Text;
                debit.Note = txtNote.Text;


                int userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);

                int eraeBe = Convert.ToInt32(ViewState[vs_currentCartable]);
                try
                {
                    int counter = 0;
                    string message;
                    if (eraeBe == (int)CheckOutStatusEnum.EnserafReqStatus.refah_ok)
                    {
                        counter = RefahBsn.UpdateDebit(debit, userId);
                    }
                    if (eraeBe == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
                    {
                        counter = MaliBusiness.UpdateMaliDebit(debit, userId);
                    }
                    if (counter > 0)
                    {
                        message = "بدهی مورد نظر ویرایش شد.";
                    }
                    else
                    {
                        message = "خطا! لطفا مجددا تلاش کنید.";
                    }
                    showMessage(message);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (e.CommandName == "deleteDebit")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                long RefID = Convert.ToInt64(dataItem.GetDataKeyValue("RefID").ToString());
                string stcode = ViewState[vs_stcode].ToString();
                int debitTypeID = Convert.ToInt32(drpDebitType.SelectedValue);
                //string amount = dataItem["DebitAmount"].Text;
                string amount = dataItem["DebitNumber"].Text;
                int eraeBe = Convert.ToInt32(ViewState[vs_currentCartable]);
                try
                {
                    bool counter = false;
                    string message;
                    if (eraeBe == (int)CheckOutStatusEnum.EnserafReqStatus.refah_ok)
                    {
                        counter = RefahBsn.DeleteDebit(RefID, Convert.ToInt32(Session[sessionNames.userID_Karbar]), stcode, debitTypeID, amount);
                    }
                    if (eraeBe == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
                    {
                        counter = MaliBusiness.DeleteMaliDebit(RefID);
                    }
                    if (counter)
                    {
                        message = "بدهی مورد نظر حذف شد.";
                    }
                    else
                    {
                        message = "خطا! لطفا مجددا تلاش کنید.";
                    }

                    showMessage(message);
                }
                catch (Exception)
                {

                    throw;
                }
                LoadRadwindowBedehi();
            }
        }

        protected void grdDebit_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            LoadRadwindowBedehi();
        }

        protected void grdDebit_CancelCommand(object sender, GridCommandEventArgs e)
        {
            LoadRadwindowBedehi();
        }

        protected void grdDebit_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                DropDownList drpDebitType = (DropDownList)e.Item.FindControl("drpDebitType");
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                drpDebitType.Items.FindByValue(rowView["DebitTypeID"].ToString()).Selected = true;
            }
        }
        protected void txtNaghsDescription_click(object sender, EventArgs e)
        {
            // btnSubmitMsg.Enabled = true;
        }
        protected void btnSubmitNaghs_Click(object sender, EventArgs e)
        {
            if (txtNaghsDescription.Text != "")
            {
                CheckOutNaghsDTO oNaghs = new CheckOutNaghsDTO();
                CheckOutNaghsBusiness NaghsBus = new CheckOutNaghsBusiness();
                oNaghs.StudentRequestId = Convert.ToInt32(ViewState[vs_reqID]);
                oNaghs.StCode = ViewState[vs_stNaghs].ToString();
                if (Convert.ToInt32(ViewState[vs_currentCartable]) == Convert.ToInt32(((int)CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat).ToString()))
                {
                    oNaghs.RequestLogId = ((int)CheckOutStatusEnum.CheckOutAllStatusEnum.takmil_parvande);
                    oNaghs.Erae_Be = ((int)CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat).ToString();
                }
                else if (Convert.ToInt32(ViewState[vs_currentCartable]) == Convert.ToInt32(((int)CheckOutStatusEnum.CheckOutAllStatusEnum.ersal_sodoor).ToString()))
                {
                    oNaghs.RequestLogId = ((int)CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat);
                    oNaghs.Erae_Be = ((int)CheckOutStatusEnum.CheckOutAllStatusEnum.ersal_sodoor).ToString();
                }

                oNaghs.SubmitDate = DateTime.Now.ToPeString();
                oNaghs.NaghsMessage = "نقص: " + txtNaghsDescription.Text;
                int id = NaghsBus.InsertNewNaghs(oNaghs);
                setLog(DTO.eventEnum.نقص_پرونده_فارغ_التحصیلی, oNaghs.NaghsMessage, oNaghs.StudentRequestId);
                if (id > 0)
                {
                    lblNaghsMessage.Text = "نقص پرونده با موفقیت درج شد.";
                    txtNaghsDescription.Text = "";
                }
                else
                {
                    lblNaghsMessage.Text = "خطا در درج نقص ! لطفا مجددا تلاش کنید یا با مدیر سامانه تماس بگیرید..";
                }
                LoadRadWindowNaghs(oNaghs.StudentRequestId.ToString(), oNaghs.StCode);
                txtNaghsDescription.Text = "";


            }
            else
            {
                showMessage("علت نقص پرونده را ذکر کنید... ");

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
                var stcode = ViewState[vs_stNaghs].ToString();
                //int count = 
                NaghsBus.DeleteNaghs(Convert.ToInt32(naghsId), Convert.ToInt32(Erae_be), Convert.ToInt32(RequestLogId), Convert.ToInt32(ViewState[vs_reqID]));
            }
        }

        protected void grdNaghs1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                int reqLogId = Convert.ToInt32(rowView["RequestLogId"]);
                int erae_Be;
                if (rowView["Erae_Be"] == DBNull.Value)
                {
                    erae_Be = (int)CheckOutStatusEnum.FareghReqStatus.pajohesh_ok;
                }
                else if (Convert.ToInt32(rowView["Erae_Be"]) == 0)
                {
                    erae_Be = (int)CheckOutStatusEnum.EnserafReqStatus.daneshkade_ok;
                }
                else if (Convert.ToInt32(rowView["Erae_Be"]) == Convert.ToInt32(rowView["RequestLogId"]) && Convert.ToInt32(rowView["Erae_Be"]) == 25)
                {

                    erae_Be = (int)CheckOutStatusEnum.FareghReqStatus.vrood_moavenat_ok;
                }
                else
                {
                    erae_Be = Convert.ToInt32(rowView["Erae_Be"]);
                }

                bool isResolved = Convert.ToBoolean(rowView["IsResolved"]);

                GridDataItem item = (GridDataItem)e.Item;

                if (erae_Be == (int)CheckOutStatusEnum.FareghReqStatus.vrood_moavenat_ok && reqLogId == 25)
                {
                    TableCell cellreqLodId = (TableCell)item["RequestLogId"];
                    cellreqLodId.Text = "";
                    TableCell cellerae_Be = (TableCell)item["Erae_Be"];
                    cellerae_Be.Text = "معاونت دانشجویی";
                }
                else
                {
                    TableCell cellreqLodId = (TableCell)item["RequestLogId"];
                    cellreqLodId.Text = reqBsn.GetPersianStatus(reqLogId);
                    TableCell cellerae_Be = (TableCell)item["Erae_Be"];
                    cellerae_Be.Text = reqBsn.GetPersianStatus(erae_Be);
                }

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

        protected void grdPastMaghtaInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                Button btnGetScan = (Button)e.Row.FindControl("btnGetScan");

                //if (File.Exists(dr["MadrakURL"].ToString()))
                //{
                //    btnGetScan.PostBackUrl = dr["MadrakURL"].ToString();
                //}
            }
        }

        protected void grdPastMaghtaInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "download")
            {
                hidClientField.Value = e.CommandArgument.ToString();
                string scrp = "downloadfile()";
                ScriptManager.RegisterStartupScript(this, GetType(), "dlimg", scrp, true);
            }
        }

        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            BindData(Convert.ToInt32(ViewState[vs_currentCartable]), false);
        }

        //protected int convertReqType(string req)
        //{
        //    if (req == " تغيير رشته")
        //    {
        //        return 13;
        //    }
        //    if (req == " اخراج")
        //    {
        //        return 14;
        //    }
        //    if (req == "فارغ التحصيلي")
        //    {
        //        return 15;
        //    }
        //    if (req == "انصراف")
        //    {
        //        return 16;
        //    }
        //    if (req == "انتقالی")
        //    {
        //        return 17;
        //    }
        //    return -1;//no match found
        //}

        protected void grd_CheckOutList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {

                GridDataItem curruntRow = (GridDataItem)(((ImageButton)e.CommandSource).NamingContainer);

                var reqID = int.Parse(((Label)curruntRow.Cells[5].FindControl("lblReqId")).Text);
                var curStatus = reqBsn.GetCheckOutStatusByreqID(reqID);
                HiddenField hdnField = (HiddenField)e.Item.FindControl("hdnField");
                if (hdnField != null)
                {
                    if (curStatus.ToString() != hdnField.Value)
                    {
                        showMessage(".این درخواست قبلاً تایید شده است");
                        grd_CheckOutList.Rebind();
                        return;
                    }

                }


                string msg = "";
                int currentStatus = Convert.ToInt32(ViewState[vs_currentCartable]);//sargol:beshe eraeBe ya nextStatus
                GridDataItem itemAmount = (GridDataItem)e.Item;
                int reqType = int.Parse(itemAmount["RequestTypeID"].Text);
                string stcode = itemAmount["StCode"].Text;
                string stName = itemAmount["name"].Text;
                HiddenField hdnThesis = new HiddenField();
                ViewState.Add(vs_ReqType, reqType);
                ViewState.Add(vs_reqID, reqID);
                //ViewState.Add("currentstatus", currentStatus);
                ImageButton btnReady = (ImageButton)itemAmount.FindControl("btnReady");
                ImageButton imgIsReady = (ImageButton)itemAmount.FindControl("imgIsReady");
                DataTable dtThesis;
                string script;
                switch (e.CommandName)
                {
                    case "StudentMessage":
                        lblMsgStudent.Text = e.CommandArgument.ToString();
                        script = "function f(){$find(\"" + winStudentMessage.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, script, true);

                        var RequsetID = Convert.ToInt32(reqID);

                        reqBsn.UpdateStatusOfStMsg(RequsetID);
                        break;
                    case "dlThesis":
                        dtThesis = reqBsn.getThesisByStudentID(stcode);
                        if (dtThesis.Rows.Count == 1)
                        {

                            if (dtThesis.Rows[0]["thesis_doc"] != DBNull.Value &&
                                dtThesis.Rows[0]["thesis_pdf"] != DBNull.Value &&
                                File.Exists(Server.MapPath("..\\pages\\" + dtThesis.Rows[0]["thesis_doc"].ToString())) &&
                                    File.Exists(Server.MapPath("..\\pages\\" + dtThesis.Rows[0]["thesis_pdf"].ToString())))
                            {
                                string zipPath = dtThesis.Rows[0]["thesis_pdf"].ToString().Substring(0, dtThesis.Rows[0]["thesis_pdf"].ToString().Length - 4) + ".zip";
                                if (File.Exists(Server.MapPath(zipPath)))
                                    File.Delete(Server.MapPath(zipPath));
                                using (ZipFile zip = new ZipFile())
                                {
                                    FileStream streamDoc = null;
                                    FileStream streamPDF = null;
                                    streamDoc = new FileStream(Server.MapPath("..\\pages\\" + dtThesis.Rows[0]["thesis_doc"].ToString()), FileMode.Open, FileAccess.ReadWrite);

                                    streamPDF = new FileStream(Server.MapPath("..\\pages\\" + dtThesis.Rows[0]["thesis_pdf"].ToString()), FileMode.Open, FileAccess.ReadWrite);
                                    zip.AddEntry(stcode + ".pdf", streamPDF);
                                    zip.AddEntry(stcode + dtThesis.Rows[0]["thesis_doc"].ToString().Substring(dtThesis.Rows[0]["thesis_doc"].ToString().LastIndexOf(".")), streamDoc);

                                    zip.Save(Server.MapPath("..\\pages\\" + zipPath));
                                }

                                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath("..\\pages\\" + zipPath));
                                HttpContext.Current.Response.Clear();
                                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                                HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                                HttpContext.Current.Response.ContentType = "application/zip";
                                HttpContext.Current.Response.WriteFile(file.FullName);
                                HttpContext.Current.Response.Flush();
                                HttpContext.Current.Response.SuppressContent = true;
                                HttpContext.Current.ApplicationInstance.CompleteRequest();
                                HttpContext.Current.Response.End();

                                if (File.Exists(zipPath))
                                    File.Delete(zipPath);
                            }
                            else
                            {
                                showMessage("فایل پایان نامه یافت نشد.", "پایان نامه");
                            }
                        }
                        else
                        {
                            showMessage("خطا در دریافت فایل پایان نامه", "پایان نامه");
                        }
                        //dl thesis from db
                        break;
                    case "dlThesisDoc":
                        dtThesis = reqBsn.getThesisByStudentID(stcode);
                        if (dtThesis.Rows.Count == 1)
                        {
                            if (dtThesis.Rows[0]["thesis_doc"] != DBNull.Value &&
                                 File.Exists(Server.MapPath("..\\pages\\" + dtThesis.Rows[0]["thesis_doc"].ToString())))
                            {
                                string ext = dtThesis.Rows[0]["thesis_doc"].ToString().Substring(dtThesis.Rows[0]["thesis_doc"].ToString().LastIndexOf(".") + 1);


                                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath("..\\pages\\" + dtThesis.Rows[0]["thesis_doc"].ToString()));
                                HttpContext.Current.Response.Clear();
                                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=Thesis_" + file.Name);
                                HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                                HttpContext.Current.Response.ContentType = "application/" + ext;
                                HttpContext.Current.Response.WriteFile(file.FullName);
                                HttpContext.Current.Response.Flush();
                                HttpContext.Current.Response.SuppressContent = true;
                                HttpContext.Current.ApplicationInstance.CompleteRequest();
                                HttpContext.Current.Response.End();



                            }
                            else
                            {
                                showMessage("فایل پایان نامه یافت نشد.",  "پایان نامه");
                            }
                        }
                        else
                        {
                            showMessage("خطا در دریافت فایل پایان نامه", "پایان نامه");
                        }
                        break;
                    case "dlThesisPDF":
                        dtThesis = reqBsn.getThesisByStudentID(stcode);
                        if (dtThesis.Rows.Count == 1)
                        {
                            if (dtThesis.Rows[0]["thesis_pdf"] != DBNull.Value &&
                                 File.Exists(Server.MapPath("..\\pages\\" + dtThesis.Rows[0]["thesis_pdf"].ToString())))
                            {

                                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath("..\\pages\\" + dtThesis.Rows[0]["thesis_pdf"].ToString()));
                                HttpContext.Current.Response.Clear();
                                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=Thesis_" + file.Name);
                                HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                                HttpContext.Current.Response.ContentType = "application/pdf";
                                HttpContext.Current.Response.WriteFile(file.FullName);
                                HttpContext.Current.Response.Flush();
                                HttpContext.Current.Response.SuppressContent = true;
                                HttpContext.Current.ApplicationInstance.CompleteRequest();
                                HttpContext.Current.Response.End();
                            }
                            else
                            {
                                showMessage("فایل پایان نامه یافت نشد.", "پایان نامه");
                            }
                        }
                        else
                        {
                            showMessage("خطا در دریافت فایل پایان نامه", "پایان نامه");
                        }
                        break;
                    case "denyThesis":
                        ViewState[vs_stcode] = stcode;
                        txtMsgThesis.Text = "";
                        script = "function f(){$find(\"" + rwSendMsg_Thesis.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, script, true);
                        //تغییر وضعیت  پایان نامه
                        break;
                    case "approve":
                        //بررسی بروز رسانی صفحه

                        if (reqType == (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil && ViewState[vs_currentCartable].ToString() == CheckOutStatusEnum.CheckOutAllStatusEnum.pajohesh.ToString())//"15")
                        {
                            dtThesis = reqBsn.getThesisByStudentID(stcode);
                            if (dtThesis.Rows.Count == 0 ||
                                dtThesis.Rows[0]["status"].ToString() != "1")
                            {
                                showMessage("کاربر گرامی، دانشجوی مورد نظر فاقد پایان نامه تایید شده میباشد.");
                                return;
                            }
                        }
                        DataTable dt = RefahBsn.GetAllDebitByStcode(stcode);
                        var bestankari = dt.AsEnumerable()
                                                  .Where(r => r.Field<int>("DebitTypeID") == (int)CheckOutRefahEnum.DebitType.Bestankar);

                        int eraeBe = Convert.ToInt32(ViewState[vs_currentCartable]);
                        switch ((CheckOutStatusEnum.CheckOutAllStatusEnum)eraeBe)
                        {
                            case CheckOutStatusEnum.CheckOutAllStatusEnum.daneshkade:
                                string cantSubmit;
                                if (canSubmit(reqID,reqType,out cantSubmit))
                                {

                                checkmoadl = false;
                                //if (reqType == (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil)
                                //{
                                string scrp = "function f(){$find(\"" + rwCheckStudentAverage.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);

                                }
                                else
                                {
                                    showMessage(cantSubmit);
                                }
                                return;
                            //}
                            //break;

                            case CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi:
                                msg = updateRequestStatus(currentStatus, reqID, reqType);
                                if (reqType == Convert.ToInt32(CheckOutStatusEnum.CheckOutType.fareq_tahsil))
                                {
                                    fraghatBusiness.sendSmsForUpload(reqID, stcode);
                                }
                                break;
                            case CheckOutStatusEnum.CheckOutAllStatusEnum.refah:
                            case CheckOutStatusEnum.CheckOutAllStatusEnum.maali:
                                dt = MaliBusiness.GetAllMaliDebitByStcode(stcode);
                                if (dt.Rows[0]["sidabedehi"] != null)
                                {
                                    bool r1 = !HasAcountNo(stcode);
                                    bool r2;
                                    if (eraeBe == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.refah)
                                    {
                                        r2 = MaliBusiness.HasDebitRefah(stcode);
                                    }
                                    else
                                    {
                                        r2 = MaliBusiness.HasDebitAcountNo(stcode);
                                    }

                                    if (r1 && r2)
                                    {
                                        showMessage("به علت وارد نکردن مشخصات حساب توسط دانشجو این درخواست قابل تایید نمی باشد");
                                    }
                                    else
                                    {
                                        msg = updateRequestStatus( currentStatus, reqID, reqType,false,0,true,stcode);
                                    }
                                }

                                break;
                            case CheckOutStatusEnum.CheckOutAllStatusEnum.takmil_parvande://حذف_دبیرخانه  
                                //var dtNAghsFish = reqBsn.GetfishNaghsIdBystcode(stcode);
                                var dtNaghs = reqBsn.getStampRecieptDefect(reqID);
                                if (dtNaghs.Rows.Count > 0)
                                {
                                    string smsBody;
                                    string result = "";
                                    bool sentSMS;
                                    DataTable dtMsg = commonBsn.GetAppIDMessage(0, 12, 1, 3);
                                    smsBody = dtMsg.Rows[0][0].ToString();

                                    string smsStatusText;
                                    result = commonBsn.sendSMS(1, stcode, smsBody, out sentSMS, out smsStatusText);


                                    msg = updateRequestStatus( currentStatus, reqID, reqType);

                                }
                                else
                                {
                                    msg = updateRequestStatus(currentStatus, reqID, reqType);
                                }
                                break;
                            case CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat:
                                LoadRadWindowVahed(reqID.ToString(), stcode);
                                HdnSt.Value = stcode;
                                HdnReq.Value = reqID.ToString();
                                break;

                            default:
                                msg = updateRequestStatus(currentStatus, reqID, reqType);
                                break;
                        }


                        break;
                    case "msg":
                        txtMsg.Text = "";
                        ViewState.Add(vs_reqID, reqID);
                        ViewState.Add(vs_currentCartable, currentStatus);
                        ViewState.Add(vs_stcode, stcode);
                        script = "function f(){$find(\"" + rwMessage.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, script, true);

                        break;
                    case "refah":
                        HiddenField idd_meli = (HiddenField)itemAmount.FindControl("idd_meli");
                        HiddenField mobile = (HiddenField)itemAmount.FindControl("mobile");
                        ViewState.Add(vs_stcode, stcode);
                        ViewState.Add("stName", stName);
                        ViewState.Add(vs_currentCartable, currentStatus);
                        ViewState.Add("idd_meli", idd_meli.Value);
                        ViewState.Add("mobile", mobile.Value);
                        LoadRadwindowBedehi();
                        break;
                    case "naghs":
                        LoadRadWindowNaghs(e.CommandArgument.ToString(), stcode);
                        break;
                    case "Ready":
                        reqBsn.UpdateReadyRequest(reqID, Session[sessionNames.userID_Karbar].ToString());


                        btnReady.Visible = false;
                        imgIsReady.Visible = true;
                        break;
                    case "mashmoolApprove":
                        msg = updateRequestStatus(currentStatus, reqID, reqType, true);
                        break;
                    case "History":
                        reqID = Convert.ToInt32(e.CommandArgument);

                        stcode = itemAmount["stcode"].Text;
                        string reqDate = itemAmount["CreateDate"].Text;
                        ViewState.Add(vs_reqID, reqID);
                        ViewState.Add("stName", stName);
                        ViewState.Add("reqDate", reqDate);

                        lst_history.DataSource = commonBsn.GetUserLogByModifyId(int.Parse(e.CommandArgument.ToString()), 12);
                        lst_history.DataBind();
                        info1.InnerText = "نام دانشجو:" + stName;
                        info2.InnerText = "شماره درخواست:" + reqID;
                        info3.InnerText = "تاریخ درخواست:" + reqDate;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                        break;
                    case "checkoutReason":
                        ViewState[vs_stcode] = itemAmount["stcode"].Text;
                        openCheckoutReason(reqID);
                        break;
                }
                if (!string.IsNullOrWhiteSpace(msg) && checkmoadl)
                {
                    lblMessage.Text = msg;
                    lblMessage.Visible = true;
                    showMessage(msg);
                }

                BindData(currentStatus, false);
                //}
            }
        }

        private string updateRequestStatus(int currentStatus, int reqID, int reqType,bool isMashmul=false,int idVahed=0,bool updateStudentInfoInReg=false,string stcode="")
        {
            string msg=reqBsn.ApproveCheckOutRequestByCurrentStatus(Session[sessionNames.userID_Karbar].ToString(), currentStatus, reqID, reqType,isMashmul,idVahed);
            reqBsn.UpdateStudentLastUpdate(reqID);
            if(updateStudentInfoInReg)
                reqBsn.UpdateStatusInReg(stcode, reqType, currentStatus);

            return msg;
        }

        private void showMessage(string msg,string title="پیام سیستم",string callBackFunction="")
        {
            RadWindowManager1.RadAlert(msg, 300, 100, title, callBackFunction);
        }
        protected void grd_CheckOutList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            int Erae_be;
            int reqType;
            string stcode;
            if (e.Item is GridDataItem)
            {
                #region get components

                GridDataItem itemAmount = (GridDataItem)e.Item;
                stcode = itemAmount["StCode"].Text;
                System.Web.UI.WebControls.Image btnreq = (System.Web.UI.WebControls.Image)itemAmount.FindControl("imgTypeRequest");
                System.Web.UI.WebControls.Image btnmsg = (System.Web.UI.WebControls.Image)itemAmount.FindControl("imgSenMessage");
                System.Web.UI.WebControls.Image btnHasNaghs = (System.Web.UI.WebControls.Image)itemAmount.FindControl("imgHasNaghs");
                ImageButton btnThesis = (ImageButton)itemAmount.FindControl("btnDownloadThesis");
                ImageButton btnThesisDoc = (ImageButton)itemAmount.FindControl("btnDownloadThesisDoc");
                ImageButton btnThesisPdf = (ImageButton)itemAmount.FindControl("btnDownloadThesisPDF");
                ImageButton btnDenyThesis = (ImageButton)itemAmount.FindControl("btnDenyThesis");
                ImageButton btnShowCheckoutReason = (ImageButton)itemAmount.FindControl("btnShowCheckoutReason");
                var reqID = Int64.Parse(((Label)itemAmount.Cells[5].FindControl("lblReqId")).Text);
                DataRowView rowView = (DataRowView)itemAmount.DataItem;
                ImageButton btnSendMsg = (ImageButton)itemAmount.FindControl("btnSendMsg");
                ImageButton Msg = (ImageButton)itemAmount.FindControl("msg");


                ImageButton btnRefah = (ImageButton)itemAmount.FindControl("btnRefah");
                ImageButton lblHozoorHour = (ImageButton)itemAmount.FindControl("imgHozoorHour");
                ImageButton lblDateSabt = (ImageButton)itemAmount.FindControl("imgEntekhabVahedDate");
                ImageButton btnnaghs = (ImageButton)itemAmount.FindControl("btnnaghs");
                ImageButton btnApprove = (ImageButton)itemAmount.FindControl("btnApprove");

                ImageButton imgIsReady = (ImageButton)itemAmount.FindControl("imgIsReady");
                ImageButton btnReady = (ImageButton)itemAmount.FindControl("btnReady");
                #endregion get components

                #region show|hide thesis buttons

                DataTable dtThesis = reqBsn.getThesisByStudentID(stcode);//get thesis from db
                bool thesisUploaded = dtThesis.Rows.Count == 1;
                bool thesisAccepted = false;
                if (thesisUploaded && dtThesis.Rows[0]["status"] != DBNull.Value)
                    thesisAccepted = dtThesis.Rows[0]["status"].ToString() == "1";
                btnThesis.Visible = false; // showBtnDlThesis(thesisUploaded, itemAmount["RequestTypeID"].Text);
                btnThesisDoc.Visible = showBtnDlThesis(thesisUploaded, itemAmount["RequestTypeID"].Text);
                btnThesisPdf.Visible = showBtnDlThesis(thesisUploaded, itemAmount["RequestTypeID"].Text);
                if (dtThesis.Rows.Count > 0)
                    itemAmount["dateUploadThesis"].Text = string.IsNullOrEmpty(dtThesis.Rows[0]["logdate"].ToString()) ? "" : dtThesis.Rows[0]["logdate"].ToString();
                ScriptManager.GetCurrent(this).RegisterPostBackControl(btnThesisPdf);
                ScriptManager.GetCurrent(this).RegisterPostBackControl(btnThesisDoc);
                ScriptManager.GetCurrent(this).RegisterPostBackControl(btnThesis);
                btnDenyThesis.Visible = false;

                if (itemAmount["RequestTypeID"].Text == "15")
                    if (thesisAccepted)
                    {
                        btnThesis.ImageUrl = "~/University/Theme/images/downloadThesis.png ";
                        btnDenyThesis.Visible = drpUserRoles.SelectedValue == ((int)CheckOutStatusEnum.FareghReqStatus.pajohesh_ok).ToString();
                    }
                    else
                    {
                        btnThesis.ImageUrl = "~/University/Theme/images/downloadThesis - Denied.png ";
                    }
                #endregion show|hide thesis buttons

                btnShowCheckoutReason.Visible = showBtnCheckoutReason(Convert.ToInt32(itemAmount["RequestTypeID"].Text), reqID);
                switch (Convert.ToInt32(itemAmount["RequestTypeID"].Text))
                {
                    case (int)CheckOutStatusEnum.CheckOutType.taqir_reshte:
                        btnreq.ImageUrl = "~/University/Theme/images/change.png";
                        btnreq.Attributes["title"] = "تغییر رشته";
                        break;
                    case (int)CheckOutStatusEnum.CheckOutType.ekhraj:
                        btnreq.ImageUrl = "~/University/Theme/images/dismiss.png";
                        btnreq.Attributes["title"] = "اخراج";
                        break;
                    case (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil:
                        btnreq.ImageUrl = "~/University/Theme/images/graduate.png";
                        btnreq.Attributes["title"] = "فارغ التحصیل";
                        break;
                    case (int)CheckOutStatusEnum.CheckOutType.enseraf:
                        btnreq.ImageUrl = "~/University/Theme/images/cancel.png";
                        btnreq.Attributes["title"] = "انصراف";
                        break;
                    case (int)CheckOutStatusEnum.CheckOutType.enteqali:
                        btnreq.ImageUrl = "~/University/Theme/images/TransferIcon.png";
                        btnreq.Attributes["title"] = "انتقالی";
                        break;
                }

                var bitRead = false;
                if (rowView["IsRead"] != DBNull.Value)
                    bitRead = Convert.ToBoolean(rowView["IsRead"]);

                if ((!String.IsNullOrWhiteSpace(rowView["StudentMessage"].ToString())) && bitRead == false)
                {
                    Msg.Visible = true;
                }


                if (String.IsNullOrWhiteSpace(rowView["Message"].ToString())
                    || (!String.IsNullOrWhiteSpace(rowView["Message"].ToString()) && rowView["Message"].ToString().Contains("نقص: ")))
                {
                    btnmsg.Visible = true;
                    btnmsg.ImageUrl = "~/University/Theme/images/tasvie-new.png";
                    btnmsg.Attributes["title"] = "پیام ارسال نشده است";
                }

                var naghs = false;
                if (!string.IsNullOrEmpty(rowView["HasNaghs"].ToString()))
                    naghs = bool.Parse(rowView["HasNaghs"].ToString());




                if (naghs)
                {
                    btnHasNaghs.Visible = true;
                    btnHasNaghs.ImageUrl = "~/University/Theme/images/faileddoc.png";
                    btnHasNaghs.Attributes["title"] = "دانشجو نقص مدرک داشته است";
                    //itemAmount.BackColor = Color.FromName("#FFBA00");
                }
                var ReqID = Convert.ToInt32(rowView["StudentRequestID"]);// (int)ViewState["reqID"];
                var checkID = Convert.ToInt32(reqBsn.CheckIsReady(ReqID));
                Erae_be = Convert.ToInt32(ViewState[vs_currentCartable]);
                if (Erae_be == (int)CheckOutStatusEnum.FareghReqStatus.ersal_sodoor_ok)
                {
                    if (checkID == 1)
                    {
                        imgIsReady.Visible = true;
                        btnReady.Visible = false;
                        imgIsReady.Attributes["title"] = "پرونده دانشجو آماده ارسال می باشد";
                    }
                    if (checkID == 0)
                    {
                        imgIsReady.Visible = false;
                        btnReady.Visible = true;
                        btnReady.Attributes["title"] = "پرونده آماده ارسال شود";
                    }
                }
                else
                {
                    btnReady.Visible = false;
                    imgIsReady.Visible = false;
                }




                btnRefah.Visible = (Erae_be == (int)CheckOutStatusEnum.EnserafReqStatus.refah_ok);

                

                if (Erae_be == (int)CheckOutStatusEnum.EnserafReqStatus.maali_ok)
                {
                    lblHozoorHour.Visible = true;
                    lblDateSabt.Visible = true;
                    btnRefah.Visible = true;
                    btnDeleteTerm.Visible = true;
                }
                else
                {
                    lblHozoorHour.Visible = false;
                    lblDateSabt.Visible = false;
                    if (Erae_be != (int)CheckOutStatusEnum.EnserafReqStatus.refah_ok)
                        btnRefah.Visible = false;
                    btnDeleteTerm.Visible = false;
                }

                btnnaghs.Visible=(Erae_be == (int)CheckOutStatusEnum.FareghReqStatus.ersal_sodoor_ok || Erae_be == (int)CheckOutStatusEnum.FareghReqStatus.vrood_moavenat_ok);
                
                btnApprove.Visible = (Erae_be != (int)CheckOutStatusEnum.EnserafReqStatus.end);
                
                reqType = Convert.ToInt32(rowView["RequestTypeID"]);


                if (rowView["nezam"].ToString() == "7")
                {
                    itemAmount.BackColor = Color.FromName("#A173FF");
                }

                if (Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.maali )
                {

                    var dt2 = MaliBusiness.GetAllMaliDebitByStcode(stcode);
                    var Sid = dt2.Rows[0]["sidabedehi"];
                    if (Convert.ToInt32(Sid) > 0)
                    {
                        itemAmount.BackColor = Color.FromName("#FF4F4F");
                    }
                    else
                    {
                        itemAmount.BackColor = Color.FromName("#B3FFAE");
                    }
                    
                    Label lblhour = (Label)itemAmount.FindControl("lblhour");

                    if (reqType == 14 || reqType == 16)
                    {

                        DataTable dt = new DataTable();
                        dt = StudentBsn.GetTotalTimeInAllClassesByStcode(stcode);
                        if (dt.TableName == "Exception")
                            lblhour.Text = "خطا در محاسبه ساعت حضور";

                        else
                        {

                            if (dt.Rows.Count > 0)
                            {
                                lblhour.Text = "مدت زمان حضور دانشجو(دقیقه)" + dt.Rows[0]["SumOfTime"].ToString();
                            }
                            else
                            {
                                lblhour.Text = "0";
                            }
                        }
                    }
                    else
                    {
                        lblhour.Text = "-";
                    }
                }
                if (Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat
                    || Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.ersal_sodoor)
                {
                    if (MaliBusiness.HasDebitRefahVezarat(stcode))
                    {
                        itemAmount.BackColor = Color.FromName("#ff9900");
                    }
                }
                if (Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.mashmulan
                    || Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive
                    )
                {

                    if (Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive
                    || Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat
                    || Erae_be == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.ersal_sodoor)
                    {
                        if (MaliBusiness.HasDebitRefahVezarat(stcode))
                        {
                            itemAmount.BackColor = Color.FromName("#ff9900");
                        }
                    }

                    btnSendMsg.Visible = true;
                    if (reqType == (int)CheckOutStatusEnum.CheckOutType.ekhraj || reqType == (int)CheckOutStatusEnum.CheckOutType.enseraf)
                    {

                        if (reqBsn.GetIsBachelor(stcode) == 1)
                        {
                            itemAmount["Def_Date"].Text = "دانشجوی کارشناسی";
                        }
                        else
                        {
                            itemAmount["Def_Date"].Text = "فاقد تاریخ دفاع";
                        }
                        bool bayganiOk = Convert.ToBoolean(rowView["BayganiOk"]);
                        if (reqBsn.isMashmool(stcode) && !bayganiOk)
                        {
                            btnApprove.Enabled = false;
                        }
                        else
                        {
                            btnApprove.Enabled = true;
                        }
                    }
                }

            }
        }

        protected void grd_CheckOutList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (drpUserRoles.SelectedIndex > -1)
            {
                grd_CheckOutList.Visible = true;
                //Session.Add("activeRoleStatus", drpUserRoles.SelectedValue);
                BindData((Convert.ToInt32(drpUserRoles.SelectedValue)), true);

            }
            else
            {
                userdt = lngB.Get_UserRoles(Session[sessionNames.userID_Karbar].ToString());
                foreach (DataRow item in userdt.Rows)
                {

                    int i = Convert.ToInt32(item["RoleId"]);
                    int currentstatus;
                    if (ISCheckoutRole(i))
                    {
                        currentstatus = reqBsn.GetStatusOfRole(i);//Convert.ToInt32(userdt.Rows[i][1])
                        BindData(currentstatus, true);
                    }
                }

                //var listOfRole = new List<int>();
                //if (userdt.Rows.Count > 1)
                //{
                //    foreach (DataRow item in userdt.Rows)
                //    {
                //        listOfRole.Add(Convert.ToInt32(item[1]));
                //    }
                //    if (listOfRole.Contains(30))
                //        rol = 30;
                //}


                ////////////////////////////////////////////////
                //if (!(/*userdt.Rows.Count > 1 ||*/ rol == 54 || rol == 1 || rol == 21 || rol == 35 || rol == 30 ||
                //      rol == 50 || rol == 66 || rol == 62 || rol == 51 || rol == 52 || rol == 53 || rol == 15 ||
                //      rol == 16 || rol == 17 || rol == 67 || rol == 28 || rol == 27 || rol == 26 || rol == 68))
                //{
                //  BindData(currentstatus, true);
                //    ////////////////////////////////////////////////
                //    drpUserRoles.SelectedValue = "0";
                //}
                //else
                //{
                //    BindData(currentstatus, true);
                //}

                ////grd_CheckOutList.Visible = false;
            }
            lblMessage.Text = "";
            rwMessage.VisibleOnPageLoad = false;
            rwReasonFrequency.VisibleOnPageLoad = false;
            //RadWindow1.VisibleOnPageLoad = false;
            txtMsg.Text = "";
        }

        private void ClearAllAddressFields()
        {
            lblProvince.Text = string.Empty;
            lblCity.Text = string.Empty;
            lblStreet.Text = string.Empty;
            lblAlley.Text = string.Empty;
            lblPelak.Text = string.Empty;
            lblZipCode.Text = string.Empty;
            lblPhone.Text = string.Empty;
            lblMobile.Text = string.Empty;
            lblEmail.Text = string.Empty;
            lblRabetPhone.Text = string.Empty;
            lblRabetMobile.Text = string.Empty;
        }

        protected void btnCloseMsg_Click(object sender, EventArgs e)
        {
            pnlUserMessage.Visible = false;
            BindData(Convert.ToInt32(drpUserRoles.SelectedItem.Value), false);



        }

        protected void btnCloseNaghs_Click(object sender, EventArgs e)
        {
            BindData(Convert.ToInt32(drpUserRoles.SelectedItem.Value), false);
        }

        private static bool ISCheckoutRole(int role)
        {
            if (role == 15 || role == 16 || role == 17 || role == 1 ||
                            role == 26 || role == 27 || role == 28 ||
                            role == 67 || role == 68 || role == 32 ||
                            role == 2 || role == 21 || role == 22 ||
                            role == 9 || role == 10 || role == 64 ||
                            role == 23 || role == 49 || role == 50 ||
                            role == 66 || role == 62 || role == 4 ||
                            role == 5 || role == 72 || role == 69 ||
                            role == 70 || role == 71 || role == 29 ||
                            role == 6 || role == 7 || role == 18 ||
                            role == 30 || role == 35 || role == 76 || role==83 ||
                            role == 51 || role == 52 || role == 53)
                return true;
            else
                return false;
        }

        private bool showBtnDlThesis(bool thesisFileUploaded, string requestTypeID)
        {
            if (drpUserRoles.SelectedValue != ((int)CheckOutStatusEnum.FareghReqStatus.pajohesh_ok).ToString()
                ||
                !thesisFileUploaded
                ||
                requestTypeID != "15")
                return false;

            return true;
        }

        private bool showBtnCheckoutReason(int requestTypeID, Int64 requestID)
        {
            if ((int)CheckOutStatusEnum.CheckOutType.enseraf == requestTypeID && drpUserRoles.SelectedValue == ((int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshkade).ToString())
            {
                var reasons = reqBsn.SelectCheckOutReasons(requestID);
                if (reasons.Rows.Count > 0)
                {
                    if (reasons.Rows[0]["reason"] != DBNull.Value)
                        return true;
                }
            }
            return false;
        }
        protected void btnMsgThesis_Click(object sender, EventArgs e)
        {
            userdt = lngB.Get_UserRoles(Session[sessionNames.userID_Karbar].ToString());
            ViewState.Add("UserRoleId", userdt.Rows[0][1]);
            string msg = txtMsgThesis.Text;
            if (msg != string.Empty)
            {
                flag = (int)ViewState["flag"];

                int reqID = (int)ViewState[vs_reqID];
                int currentstatus = (int)ViewState[vs_currentCartable];
                int reqType = (int)ViewState[vs_ReqType];
                int status = Convert.ToInt32(ViewState[vs_currentCartable]);


                if (reqBsn.denyThesis(ViewState[vs_stcode].ToString()))
                {
                    reqBsn.SendMessageOfDenyThesis(Session[sessionNames.userID_Karbar].ToString(), reqID, msg);
                    fraghatBusiness.sendSmsForDenythes(reqID, ViewState[vs_stcode].ToString());

                }
                rwSendMsg_Thesis.VisibleOnPageLoad = false;
                if (drpUserRoles.SelectedIndex > 0)
                    BindData((Convert.ToInt32(drpUserRoles.SelectedValue)), false);
                else
                    BindData(Convert.ToInt32(reqBsn.GetStatusOfRole(Convert.ToInt32(ViewState["UserRoleId"].ToString()))), false);

            }
            else
            {
                msg = "لطفا متن پیام را وارد کنید";
                showMessage(msg);
            }
        }

        protected void ddlThesisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["ThesisTypeSelected"] = 1;
            BindData((Convert.ToInt32(drpUserRoles.SelectedItem.Value)), false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            BindData(Convert.ToInt32(drpUserRoles.SelectedItem.Value), false);
        }

        protected void btnDlExcel_Click(object sender, ImageClickEventArgs e)
        {
            //if (drpUserRoles.SelectedItem.Value == "15")
                exportToExcel(rdGridExcel, "تسویه حساب-کارتابل " 
                    + drpUserRoles.SelectedItem.Text 
                    + (ddlThesisType.Visible ? "-" + ddlThesisType.SelectedItem.Text : "")
                    + "-" + DateTime.Now.ToPeString("yyyymmdd")+"-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second
                    );
        }

        protected void rdGridExcel_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rdGridExcel.DataSource = getRequestList_excel();
        }

        private DataTable getRequestList_excel()
        {
            int daneshId = reqBsn.GetDaneshKadeIdByRoleId(Convert.ToInt32(Session["roleId"]));
            DataTable reqList;
            if (ViewState["ThesisTypeSelected"] != null && ViewState["ThesisTypeSelected"].ToString() == "1")
            {
                reqList = reqBsn.GetListOFRequestByNextStatus_BythesisFileStatus(Convert.ToInt32(drpUserRoles.SelectedItem.Value), Convert.ToInt32(ddlThesisType.SelectedItem.Value));
            }
            else
                 reqList = reqBsn.GetListOFRequestByNextStatus_Excel(Convert.ToInt32(drpUserRoles.SelectedItem.Value),daneshId);
            return reqList;
        }

        protected void rdGridExcel_ExcelMLWorkBookCreated(object sender, GridExcelMLWorkBookCreatedEventArgs e)
        {
            int r = 0;
            foreach (RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
            {


                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (r != 0)
                    {
                        if (r % 2 == 0)
                            row.Cells[i].StyleValue = "Style1";
                        else
                            row.Cells[i].StyleValue = "Style2";
                    }
                    else
                        row.Cells[i].StyleValue = "styleHeader";
                }
                r++;

            }
            StyleElement styleHeader = new StyleElement("styleHeader");
            styleHeader.InteriorStyle.Pattern = InteriorPatternType.Solid;
            styleHeader.InteriorStyle.Color = System.Drawing.Color.White;
            styleHeader.FontStyle.FontName = "Tahoma";
            styleHeader.FontStyle.Bold = true;
            styleHeader.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(styleHeader);
            StyleElement style = new StyleElement("Style1");
            style.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style.InteriorStyle.Color = System.Drawing.Color.FromArgb(162, 226, 255);
            style.FontStyle.FontName = "Tahoma";
            style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(style);
            StyleElement style2 = new StyleElement("Style2");
            style2.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            style2.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style2.InteriorStyle.Color = System.Drawing.Color.FromArgb(217, 243, 255);
            style2.FontStyle.FontName = "Tahoma";
            e.WorkBook.Styles.Add(style2);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int reqID = Convert.ToInt32(HdnReq.Value);
            var stcode = HdnSt.Value;
            int currentstatus = Convert.ToInt32(ViewState[vs_currentCartable]);
            
            var msg = updateRequestStatus(currentstatus, reqID, (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil, false, Convert.ToInt32(drpMahaleSodooreMadrak.SelectedItem.Value));
            BindData(Convert.ToInt32(drpUserRoles.SelectedItem.Value), false);
       


        }

        private bool canSubmit(Int64 requestID,int requestTypeID,out string response)
        {
            if ((int)CheckOutStatusEnum.CheckOutType.enseraf == requestTypeID && drpUserRoles.SelectedValue == ((int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshkade).ToString())
            {
                var reasons = reqBsn.SelectCheckOutReasons(requestID);
                if (reasons.Rows.Count > 0)
                {
                    if (reasons.Rows[0]["reason"] != DBNull.Value && reasons.Rows[0]["frequencyID"] != DBNull.Value)
                    {
                        response = "";
                        return true;
                    }
                    else
                    {
                        response = "لطفا ابتدا دسته بندی مربوط به علت انصراف دانشجو را انتخاب فرمایید سپس درخواست را تایید کنید";
                        return false;
                    }
                }
            }
            response = "";
            return true;
        }

        protected void btnSaveFrequencyReason_Click(object sender, EventArgs e)
        {
            if (ddlFrequencyReasons.SelectedItem.Value != "0")
            {
                var fID = reqBsn.updateCheckoutReasonFrequency(Convert.ToInt64(ViewState[vs_reqID]), ViewState[vs_stcode].ToString(), Convert.ToInt32(ddlFrequencyReasons.SelectedItem.Value));
                setLog(DTO.eventEnum.ثبت_دلیل_تسویه_در_لیست_پرتکرارها,
                    "دلیل پرتکرار انتخاب شده: " + ddlFrequencyReasons.SelectedItem.Text + "(کد " + ddlFrequencyReasons.SelectedItem.Value + ")", fID);
            }
            rwReasonFrequency.VisibleOnPageLoad = false;

        }

        protected void btnAvgIsAuthorized_Click(object sender, EventArgs e)
        {
            var reqID = (int)ViewState[vs_reqID];
            CheckOutNaghsBusiness oNaghsBus = new CheckOutNaghsBusiness();
            int naghsId = Convert.ToInt32(oNaghsBus.GetNaghsIdByReqId(reqID));
            if (naghsId > 0)
            {
                bool flag = oNaghsBus.ResolveNaghsById(naghsId);
                if (flag)
                {

                    showMessage("نقص پرونده بر طرف شد.", "پیام");
                }
                else
                {
                    showMessage("خطا در هنگام برطرف سازی نقص لطفا مجددا تلاش نمایید.", "پیام");
                }
            }
            //  DataRowView rowView = (DataRowView)itemAmount.DataItem;
            string msg = "";
            var stcode = HdnSt.Value;
            int reqType = (int)ViewState[vs_ReqType];
            int currentstatus = (int)ViewState[vs_currentCartable];
            reqBsn.UpdateStatusInReg(stcode, reqType, currentstatus);
            msg = updateRequestStatus(currentstatus, reqID, reqType);
            BindData(Convert.ToInt32(drpUserRoles.SelectedItem.Value), false);
        }

        protected void btnAvgIsUnacceptable_Click(object sender, EventArgs e)
        {
            BindData(Convert.ToInt32(drpUserRoles.SelectedItem.Value), false);
        }

        private void exportToExcel(Telerik.Web.UI.RadGrid grid, string excelName = "exportExcel")
        {
            grid.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grid.ExportSettings.IgnorePaging = true;
            grid.ExportSettings.ExportOnlyData = true;
            grid.ExportSettings.OpenInNewWindow = true;
            grid.ExportSettings.UseItemStyles = true;
            grid.ExportSettings.FileName = excelName;
            grid.MasterTableView.ExportToExcel();
        }

        private void setLog(DTO.eventEnum eventType, string description, int modifyID)
        {
            int appId = 12;
            commonBsn.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), appId, (int)eventType, description, modifyID);
        }

        private void openCheckoutReason(Int64 requestID)
        {
            var reasons = reqBsn.SelectCheckOutReasons(requestID);
            if (reasons.Rows.Count > 0)
            {
                lblStudentCheckoutReason.Text = reasons.Rows[0]["reason"].ToString();
            }
            var frequencyReasons = reqBsn.SelectFrequencyReasons();
            ddlFrequencyReasons.DataSource = frequencyReasons;
            ddlFrequencyReasons.DataValueField = "id";
            ddlFrequencyReasons.DataTextField = "reason";
            ddlFrequencyReasons.DataBind();
            ListItem selectItem = new ListItem("انتخاب کنید", "0");
            ddlFrequencyReasons.Items.Insert(0, selectItem);
            if (reasons.Rows[0]["frequencyID"] != DBNull.Value)
                ddlFrequencyReasons.SelectedValue = ddlFrequencyReasons.Items.FindByValue(reasons.Rows[0]["frequencyID"].ToString()) != null ? reasons.Rows[0]["frequencyID"].ToString() : "0";
            string script = "function f(){$find(\"" + rwReasonFrequency.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, script, true);
        }
    }
}