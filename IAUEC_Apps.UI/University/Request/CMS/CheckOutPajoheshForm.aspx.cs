using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;
using System.Data;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Request;
using System.Text.RegularExpressions;
using System.Globalization;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutPajoheshForm : System.Web.UI.Page
    {
        CheckOutPajooheshBusiness business = new CheckOutPajooheshBusiness();
        string sys_msg;
        public string plHolder;
        int luckUni;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblSysMsg.Text = "";
            }


        }
        public void ViewScript()
        {
            var totalScript = String.Empty;
            if (pcal1.Enabled == true && pcal5.Enabled == true)
            {
                totalScript = totalScript + "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1', {extraInputID: 'ContentPlaceHolder1_pcal1',extraInputFormat: 'yyyy/mm/dd'}); ";
                totalScript = totalScript + "var objCal5 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal5', {extraInputID: 'ContentPlaceHolder1_pcal5',extraInputFormat: 'yyyy/mm/dd'}); ";
            }
            if (pcal6.Visible == true)
            {

                totalScript = totalScript + "var objCal6 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal6', {extraInputID: 'ContentPlaceHolder1_pcal6',extraInputFormat: 'yyyy/mm/dd'}); ";
            }
            if (pcal7.Visible == true)
            {
                totalScript = totalScript + "var objCal7 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal7', {extraInputID: 'ContentPlaceHolder1_pcal7',extraInputFormat: 'yyyy/mm/dd'}); ";
            }
            if (pcal4.Enabled == true)
            {
                totalScript = totalScript + "var objCal23 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal4', {extraInputID: 'ContentPlaceHolder1_pcal4',extraInputFormat: 'yyyy/mm/dd'}); ";
            }
            //DataTable dt = business.GetStudentInfoForPajohesh(txtStcode.Text);

            //if (dt.Rows[0]["Def_Date"] != DBNull.Value)
            //{
            //    var defDate = dt.Rows[0]["Def_Date"].ToString();
            //    if (dt.Rows[0]["DeadLineDate"] == DBNull.Value)
            //    {
            //        var splitedDate = defDate.Split('/');
            //        var cc = new DateTime(int.Parse(splitedDate[0]), int.Parse(splitedDate[1]), int.Parse(splitedDate[2]), new PersianCalendar());
            //        var d = cc.AddMonths(6);
            //        var persianDate = d.ToPeString();
            //        plHolder = persianDate;
            //        totalScript = totalScript + $"document.getElementById('ContentPlaceHolder1_pcal6').setAttribute('placeHolder', '{persianDate}');";

            //    }


            //}
            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, totalScript, true);

        }

        private List<int> listOfDaneshkade()
        {
            return new List<int>
            {
                15,16,17,26,27,24,28,67,68,66,51,52,53,57,58,59,73
            };
        }
        private List<int> listOfpajoohesh()
        {
            return new List<int>
            {
                10,9,64,65
            };
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


            if (CommonBusiness.IsNumeric(txtStcode.Text))
            {


                ClearTable();
                BindTable(txtStcode.Text);
                tblInfo.Visible = true;
            }
            ViewScript();
        }
        private void ClearTable()
        {
            pcal1.Text = "";
            pcal5.Text = "";
            rdYes1.Checked = false;
            rdNo1.Checked = false;
            rdYes2.Checked = false;
            rdNo2.Checked = false;
            pcal6.Text = "";
            pcal7.Text = "";
            RdYes3.Checked = false;
            RdNo3.Checked = false;
            txtEditThes.Text = "";
            chkbReciveEdit.Checked = false;
            txtDefPoint.Text = "";
            pcal4.Text = "";

        }

        private void BindTable(string stcode)
        {
            DataTable userdt = new DataTable();
            string userID;
            LoginBusiness lngB = new LoginBusiness();
            userID = Session[sessionNames.userID_Karbar].ToString();
            userdt = lngB.Get_UserRoles(userID);
            List<int> Rol = new List<int>();


            if (userdt.Rows.Count > 0)
            {
                foreach (DataRow item in userdt.Rows)
                {
                    var temp = Convert.ToInt32(item[1]);
                    Rol.Add(temp);
                }
            }

            DataTable dtNaghs = business.GetStudentsNaghs(stcode);


            DataTable dt = business.GetStudentInfoForPajohesh(stcode);
            if (dt.Rows.Count > 0)
            {
                dvcontainar.Visible = true;
                lblSysMsg.Text = "";
                lblStcode.Text = dt.Rows[0]["stcode"].ToString();
                lblName.Text = dt.Rows[0]["name"].ToString() + " " + dt.Rows[0]["family"].ToString();
                lblReshte.Text = dt.Rows[0]["nameresh"].ToString();
                lblSalVorood.Text = dt.Rows[0]["sal_vorod"].ToString();
                lblMaghta.Text = dt.Rows[0]["magh"].ToString();
                lblFatherName.Text = dt.Rows[0]["namep"].ToString();
                lblCodeMeli.Text = dt.Rows[0]["idd_meli"].ToString();

                string sendToPajoohesh = "";
                if (dt.Rows[0]["Date_Recieve_Doc_Accept"] != DBNull.Value)
                {
                    sendToPajoohesh = dt.Rows[0]["Date_Recieve_Doc_Accept"].ToString();
                }

                string defDate = "";
                if (dt.Rows[0]["Def_Date"] != DBNull.Value)
                {

                    defDate = dt.Rows[0]["Def_Date"].ToString();
                    if (dt.Rows[0]["DeadLineDate"] == DBNull.Value)
                    {
                        var splitedDate = defDate.Split('/');
                        var cc = new DateTime(int.Parse(splitedDate[0]), int.Parse(splitedDate[1]), int.Parse(splitedDate[2]), new PersianCalendar());
                        var d = cc.AddMonths(6);
                        var persianDate = d.ToPeString();
                        plHolder = persianDate;
                    }


                }
                string defPoint = "";
                if (dt.Rows[0]["Def_Point"] != DBNull.Value)
                {
                    defPoint = dt.Rows[0]["Def_Point"].ToString();
                }


                string sendToEdu = "";
                if (dt.Rows[0]["Date_Send_Doc_Edu"] != DBNull.Value)
                {
                    sendToEdu = dt.Rows[0]["Date_Send_Doc_Edu"].ToString();
                }

                bool hasForm = false;
                if (dt.Rows[0]["HasCancelForm"] != DBNull.Value)
                {

                    hasForm = Convert.ToBoolean(dt.Rows[0]["HasCancelForm"].ToString());
                }
                if (dtNaghs.Rows.Count > 0)
                {
                    if (dtNaghs.Rows[0]["IsResolved"].ToString() == "True")
                    {
                        chkbReciveEdit.Checked = true;
                    }
                }




                if ((dt.Rows[0]["HasCancelForm"]) == DBNull.Value)
                {
                    rdYes1.Checked = false;
                    rdNo1.Checked = false;
                    luckUni = 0;
                }

                if (dt.Rows[0]["HasCancelForm"] != DBNull.Value && Convert.ToBoolean(dt.Rows[0]["HasCancelForm"]) == true)
                {
                    rdYes1.Checked = true;
                    luckUni = 1;

                }
                else if (dt.Rows[0]["HasCancelForm"] != DBNull.Value && Convert.ToBoolean(dt.Rows[0]["HasCancelForm"]) == false)
                {
                    rdNo1.Checked = true;
                    luckUni = 1;

                }



                var contain = false;
                var pajoo = false;
                var admin = false;
                foreach (var item in Rol)
                {
                    if (listOfDaneshkade().Contains(Convert.ToInt32(item)))
                        contain = true;
                    if (listOfpajoohesh().Contains(item))
                        pajoo = true;
                    if (item == 1)
                        admin = true;


                }
                //{


                if (contain)
                {
                    if (luckUni == 0)
                    {
                        pcal1.Enabled = true;
                        pcal5.Enabled = true;
                        btnSubmitDefDate.Enabled = true;
                    }
                    else
                    {
                        pcal1.Enabled = true;// false;
                        pcal5.Enabled = true; //false;
                        btnSubmitDefDate.Enabled = true;// false;

                    }

                    rdYes1.Enabled = false;
                    rdNo1.Enabled = false;
                    rdYes2.Enabled = false;
                    rdNo2.Enabled = false;
                    pcal6.Enabled = false;
                    pcal7.Enabled = false;
                    RdYes3.Enabled = false;
                    RdNo3.Enabled = false;
                    txtEditThes.Enabled = false;
                    chkbReciveEdit.Enabled = false;
                    txtDefPoint.Enabled = false;
                    pcal4.Enabled = false;
                    dvFinal.Visible = false;

                }
                else if (pajoo)
                {

                    if (/*!(string.IsNullOrEmpty(sendToPajoohesh))*/1 == 1)
                    {


                        rdYes1.Enabled = true;
                        rdNo1.Enabled = true;
                        btnSubmitDefPoint.Enabled = true;

                        if (hasForm == true)
                        {
                            rdYes2.Enabled = true;
                            rdNo2.Enabled = true;
                        }
                        else
                        {
                            rdYes2.Enabled = false;
                            rdNo2.Enabled = false;
                        }

                        pcal6.Enabled = true;
                        pcal7.Enabled = true;
                        RdYes3.Enabled = true;
                        RdNo3.Enabled = true;
                        txtEditThes.Enabled = true;
                        chkbReciveEdit.Enabled = true;
                        txtDefPoint.Enabled = true;
                        pcal4.Enabled = true;
                        if (!string.IsNullOrEmpty(defPoint) && !string.IsNullOrEmpty(sendToEdu))
                        {
                            dvFinal.Visible = true;
                        }
                    }
                    else if (string.IsNullOrEmpty(sendToPajoohesh))
                    {
                        rdYes1.Enabled = false;
                        rdNo1.Enabled = false;
                        btnHasCancelForm.Enabled = false;
                        rdYes2.Enabled = false;
                        rdNo2.Enabled = false;
                        chkbReciveEdit.Enabled = false;
                        btnReqPaper.Enabled = false;
                        RdYes3.Enabled = false;
                        RdNo3.Enabled = false;
                        txtDefPoint.Enabled = false;
                        pcal4.Enabled = false;
                        btnSubmitDefPoint.Enabled = false;
                    }
                }
                else if (admin)//admin
                {
                    if (!((string.IsNullOrEmpty(defDate) && string.IsNullOrEmpty(sendToPajoohesh))))
                    {

                        pcal1.Enabled = true;
                        pcal5.Enabled = true;
                        btnSubmitDefDate.Enabled = true;
                        rdYes1.Enabled = true;
                        rdNo1.Enabled = true;
                        btnSubmitDefPoint.Enabled = true;

                        if (hasForm == true)
                        {
                            rdYes2.Enabled = true;
                            rdNo2.Enabled = true;
                        }
                        else
                        {
                            rdYes2.Enabled = false;
                            rdNo2.Enabled = false;
                        }

                        pcal6.Enabled = true;
                        pcal7.Enabled = true;
                        RdYes3.Enabled = true;
                        RdNo3.Enabled = true;
                        txtEditThes.Enabled = true;
                        chkbReciveEdit.Enabled = true;
                        txtDefPoint.Enabled = true;
                        pcal4.Enabled = true;
                        if (!string.IsNullOrEmpty(defPoint) && !string.IsNullOrEmpty(sendToEdu))
                        {
                            dvFinal.Visible = true;
                        }
                    }
                    else
                    {
                        pcal1.Enabled = true;
                        pcal5.Enabled = true;
                        btnSubmitDefDate.Enabled = true;
                    }
                }
                else
                {
                    pcal1.Enabled = false;
                    pcal5.Enabled = false;
                    btnSubmitDefDate.Enabled = false;
                    rdYes1.Enabled = false;
                    rdNo1.Enabled = false;
                    btnSubmitDefPoint.Enabled = false;
                    rdYes2.Enabled = false;
                    rdNo2.Enabled = false;
                    pcal6.Enabled = false;
                    pcal7.Enabled = false;
                    RdYes3.Enabled = false;
                    RdNo3.Enabled = false;
                    txtEditThes.Enabled = false;
                    chkbReciveEdit.Enabled = false;
                    txtDefPoint.Enabled = false;
                    pcal4.Enabled = false;
                    dvFinal.Visible = false;

                }


                //}

                if (!((string.IsNullOrEmpty(defDate) && string.IsNullOrEmpty(sendToPajoohesh))))
                {

                    pcal1.Text = dt.Rows[0]["Def_Date"].ToString();
                    pcal5.Text = dt.Rows[0]["Date_Recieve_Doc_Accept"].ToString();

                }

                if (dt.Rows[0]["IsFinalize"].ToString() == "True")
                {
                    // CheckBox1.Enabled = false;
                    foreach (var item in Rol)
                    {

                        if (listOfDaneshkade().Contains(Convert.ToInt32(item)))
                        {
                            pcal1.Enabled = true;
                            pcal5.Enabled = true;
                            btnSubmitDefDate.Enabled = true;
                        }
                        else if (listOfpajoohesh().Contains(item))
                        {
                            pcal1.Enabled = false;
                            pcal5.Enabled = false;
                            btnSubmitDefDate.Enabled = false;
                        }

                        rdYes1.Enabled = false;
                        rdNo1.Enabled = false;
                        rdYes2.Enabled = false;
                        rdNo2.Enabled = false;
                        pcal6.Enabled = false;
                        pcal7.Enabled = false;
                        RdYes3.Enabled = false;
                        RdNo3.Enabled = false;
                        txtEditThes.Enabled = false;
                        chkbReciveEdit.Enabled = false;
                        txtDefPoint.Enabled = false;
                        pcal4.Enabled = false;

                        //var finalDate = Convert.ToDateTime((dt.Rows[0]["FinalDate"]).ToString()).ToPeString();


                        dvFinal.Visible = false;
                        lblSysMsg.Text = "توجه :وضعیت این دانشجو در تاریخ " +
                        Convert.ToDateTime((dt.Rows[0]["Final_Date"]).ToString()).ToPeString()
                        + " تایید نهایی شده است .";
                        divlblSysMsg.Visible = true;

                        btnSubmitDefPoint.Enabled = false;
                    }
                }
                else
                {
                    divlblSysMsg.Visible = false;
                }

                if (!String.IsNullOrWhiteSpace(dt.Rows[0]["DeadLineDate"].ToString()))
                {
                    rdYes2.Checked = true;
                    lblDeadLine.Visible = true;
                    pcal6.Visible = true;
                    pcal6.Text = dt.Rows[0]["DeadLineDate"].ToString();
                    lblCancelDate.Visible = true;
                    pcal7.Text = dt.Rows[0]["Date_Paper_Cancel"].ToString();
                }
                else
                {


                    rdYes2.Checked = false;

                }


                if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Date_Paper_Cancel"].ToString()) && !String.IsNullOrWhiteSpace(dt.Rows[0]["DeadLineDate"].ToString()))
                {
                    rdYes2.Checked = true;
                    lblDeadLine.Visible = true;
                    lblCancelDate.Visible = true;
                    pcal6.Visible = true;
                    pcal6.Text = dt.Rows[0]["DeadLineDate"].ToString();
                    pcal7.Visible = true;
                    pcal7.Text = dt.Rows[0]["Date_Paper_Cancel"].ToString();
                }
                else if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Date_Paper_Cancel"].ToString()))
                {
                    rdNo2.Checked = true;
                    pcal7.Visible = true;
                    pcal7.Text = dt.Rows[0]["Date_Paper_Cancel"].ToString();
                    lblCancelDate.Visible = true;
                }

                if (!String.IsNullOrWhiteSpace(dt.Rows[0]["EditThes"].ToString()))
                {
                    RdYes3.Checked = true;
                    lblDetail.Visible = true;
                    txtEditThes.Visible = true;
                    chkbReciveEdit.Visible = true;

                    txtEditThes.Text = dt.Rows[0]["EditThes"].ToString();
                }
                else
                {
                    RdYes3.Checked = false;
                    RdNo3.Checked = false;
                    txtEditThes.Visible = false;
                    lblDetail.Visible = false;
                }




                if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Def_Point"].ToString()))
                {

                    //////CheckBox5.Checked = true;
                    //dvDefPoint.enable = true;
                    txtDefPoint.Text = dt.Rows[0]["Def_Point"].ToString();
                }
                lblSidaDefPoint.Visible = false;
                lblTxtSidaDefPoint.Visible = false;
                if (dt.Rows[0]["Def_Point"].ToString() != dt.Rows[0]["nomre"].ToString())
                {
                    lblSidaDefPoint.Visible = true;
                    lblTxtSidaDefPoint.Visible = true;
                    lblSidaDefPoint.Text = dt.Rows[0]["nomre"].ToString();
                    btnSubmitDefPoint.Enabled = true;//اگه نمره ثبت شده دفاع در خدمات با نمره دفاع در سیدا متفاوت بود، حتی اگه ثبت نهایی هم شده باشه ، باید بتونه دکمه ثبت رو بزنه و نمره ها رو با هم یکسان کنه
                }

                if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Date_Paper_Cancel"].ToString()))
                {
                    if (dt.Rows[0]["IsFinalize"].ToString() == "True")
                    {

                        //////CheckBox3.Enabled = false;
                    }
                    //    RadioButton2.Checked = true;

                    //////pcal2.Text = dt.Rows[0]["Date_Paper_Cancel"].ToString();

                }

                if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Date_Recieve_Doc_Accept"].ToString()))
                {
                    if (dt.Rows[0]["IsFinalize"].ToString() == "True")
                    {
                        //////rdoBox2.Enabled = false;
                        //////rdoBox7.Enabled = false;
                        //////rdoBox7.Checked = true;
                    }
                    //////CheckBox3.Checked = true;
                    //////pcal3.Text = dt.Rows[0]["Date_Recieve_Doc_Accept"].ToString();
                    //RadioButton1.Checked = true;
                    //txtReceiveFileinPajoohesh.Text = dt.Rows[0]["Date_Recieve_Doc_Accept"].ToString();
                }

                if (!String.IsNullOrWhiteSpace(dt.Rows[0]["Date_Send_Doc_Edu"].ToString()))
                {
                    if (dt.Rows[0]["IsFinalize"].ToString() == "True")
                    {
                        //////CheckBox4.Enabled = false;
                    }
                    //////CheckBox4.Checked = true;
                    pcal4.Text = dt.Rows[0]["Date_Send_Doc_Edu"].ToString();
                    // txtSendFileToAmouzesh.Text= dt.Rows[0]["Date_Send_Doc_Edu"].ToString();
                }

                bool hasEdit = false;
                if (dt.Rows[0]["EditThes"] != DBNull.Value)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["EditThes"].ToString()))
                    {
                        hasEdit = true;//Convert.ToBoolean(dt.Rows[0]["EditThes"].ToString());
                    }

                }
                RdYes3.Checked = hasEdit;
                RdNo3.Checked = (!hasEdit);
                tblStudentInfo.Visible = true;

            }
            else
            {
                lblSysMsg.Text = "دانشجویی با این مشخصات یافت نشد!";
                divlblSysMsg.Visible = true;
                dvcontainar.Visible = false;
            }
        }

        protected void btnSubmitDefPoint_Click(object sender, EventArgs e)
        {

            if (lblSidaDefPoint.Visible && lblSidaDefPoint.Text != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_ChangeDefPoint();", true);
            }
            else
                submitDefPoint(false);

        }

        private void submitDefPoint(bool updateDefPointToSidaPoint)
        {

            DateTime toEduDate = DateTime.Now;
            DateTime toPajDate = DateTime.Now;
            if (updateDefPointToSidaPoint)
                txtDefPoint.Text = lblSidaDefPoint.Text;
            if (CommonBusiness.ValidateNomreh(txtDefPoint.Text))
            {
                try
                {
                    sys_msg = business.Add_DefPoint(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, txtDefPoint.Text);
                }
                catch (Exception)
                {

                    throw;
                }
                lblSysMsg.Text = sys_msg;
                divlblSysMsg.Visible = true;
            }
            if (!string.IsNullOrEmpty(pcal4.Text) && !string.IsNullOrEmpty(pcal5.Text))
            {


                var splitedToPajDate = pcal5.Text.Trim().Split('/');

                //var dateNow = DateTime.Now;



                toPajDate = new DateTime(Convert.ToInt32(splitedToPajDate[0]),
                    Convert.ToInt32(splitedToPajDate[1]),
                    Convert.ToInt32(splitedToPajDate[2]), new System.Globalization.PersianCalendar());

                var splitedToEduDate = pcal4.Text.Trim().Split('/');

                //  var dateNow = DateTime.Now;
                toEduDate = new DateTime(Convert.ToInt32(splitedToEduDate[0]),
                   Convert.ToInt32(splitedToEduDate[1]),
                   Convert.ToInt32(splitedToEduDate[2]), new System.Globalization.PersianCalendar());

                if (toPajDate <= toEduDate)
                {

                    if (!String.IsNullOrWhiteSpace(pcal4.Text))
                    {


                        try
                        {
                            sys_msg = business.Add_Send_Date(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, pcal4.Text);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        lblSysMsg.Text = sys_msg;
                        divlblSysMsg.Visible = true;

                    }
                    if (!String.IsNullOrWhiteSpace(pcal4.Text) && !string.IsNullOrEmpty(txtDefPoint.Text))
                    {
                        dvFinal.Visible = true;
                        btnSubmitDefPoint.Enabled = false;
                    }
                    else
                    {
                        dvFinal.Visible = false;
                        btnSubmitDefPoint.Enabled = true;
                    }
                }
                else
                {
                    string empMsg = "تاریخ ارسال به معاونت آموزشی باید بعد از تاریخ ارسال به پژوهش باشد";
                    RadWindowManager1.RadAlert(empMsg, 0, 100, "پیام", "");
                    btnSubmitDefPoint.Enabled = true;
                }
            }
            else if (!string.IsNullOrEmpty(pcal4.Text) && string.IsNullOrEmpty(pcal5.Text))
            {
                try
                {
                    sys_msg = business.Add_Send_Date(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, pcal4.Text);
                }
                catch (Exception)
                {

                    throw;
                }
                lblSysMsg.Text = sys_msg;
                divlblSysMsg.Visible = true;
            }



            if (!string.IsNullOrEmpty(pcal4.Text) && !string.IsNullOrEmpty(txtDefPoint.Text))
            {
                dvFinal.Visible = true;


            }
            ViewScript();
            if (updateDefPointToSidaPoint)
                BindTable(lblStcode.Text);
        }

        protected void btnFinalizer_Click(object sender, EventArgs e)
        {

            CommonBusiness oCommon = new CommonBusiness();
            string smsBody = "دانشجوي محترم واحد الکترونیکی، با توجه به ثبت نمره پایان­نامه در سامانه تسویه حساب و امکان شروع فرآیند فارغ التحصیلی، خواهشمند است با مراجعه به سامانه خدمات الکترونیکی نسبت به ثبت درخواست تسویه حساب اقدام بایسته مبذول فرمایید"; //"دانشجوي محترم واحد الكترونيكي، با توجه به ثبت نمره پايان¬نامه در سامانه تسويه حساب و امكان شروع فرآيند فارغ التحصيلي، خواهشمند است در اسرع وقت با مراجعه به سامانه خدمات الكترونيكي نسبت به ثبت درخواست تسويه حساب اقدام بايسته مبذول فرماييد.";// "دانشجوي محترم واحد الکترونیکی، با توجه به ثبت نمره پایان­نامه در سامانه تسویه حساب و امکان شروع فرآیند فارغ التحصیلی، خواهشمند است در اسرع وقت  با مراجعه به سامانه خدمات الکترونیکی نسبت به ثبت درخواست تسویه حساب اقدام بایسته مبذول فرمایید.";
            string result = "";


            if (ValidateInput())
            {
                CheckOutPajoheshDTO oPajohesh = new CheckOutPajoheshDTO();

                oPajohesh.StCode = lblStcode.Text;

                if (!string.IsNullOrWhiteSpace(pcal1.Text))
                {
                    oPajohesh.Def_Date = pcal1.Text;
                }
                oPajohesh.Def_Point = txtDefPoint.Text;
                if (rdYes2.Checked)
                {
                    oPajohesh.HasPaper = true;
                    oPajohesh.DeadLineDate = pcal6.Text;
                    oPajohesh.Date_Paper_Cancel = pcal7.Text;
                }
                if (rdNo2.Checked)
                {
                    oPajohesh.HasPaper = false;
                    oPajohesh.Date_Paper_Cancel = pcal7.Text;

                }


                if (!string.IsNullOrWhiteSpace(pcal5.Text))
                {

                    oPajohesh.Date_Recieve_Doc_Accept = pcal5.Text;
                }
                oPajohesh.Date_Send_Doc_Edu = pcal4.Text;
                oPajohesh.IsFinalize = true;
                if (rdYes1.Checked)
                {
                    oPajohesh.HasCancelForm = true;//??How to send msg
                }
                if (RdYes3.Checked)
                {
                    oPajohesh.EditThes = txtEditThes.Text;
                }
                //oPajohesh.Date_Recieve_Doc_Accept = pcal5.Text;
                try
                {
                    sys_msg = business.FinizlizePajoohesh(Session[sessionNames.userID_Karbar].ToString(), oPajohesh);
                    dvFinal.Visible = false;
                    BindTable(txtStcode.Text);
                }
                catch (Exception)
                {

                    throw;
                }
                lblSysMsg.Text = sys_msg;
                divlblSysMsg.Visible = true;
                showMessage(sys_msg);

                //result = oCommon.SendSMSByUserIdAndType(smsBody, oPajohesh.StCode, 1);
                //result = oCommon.sendSMS(1, oPajohesh.StCode,smsBody );
                bool automaticSubmitRequest = registerCheckoutRequest(oPajohesh.StCode);
                if (!automaticSubmitRequest)
                {

                    string smsStatusText; bool sentSMS;
                    result = oCommon.sendSMS(1, oPajohesh.StCode, smsBody, out sentSMS, out smsStatusText);
                    CheckOutRequestBusiness reqBus = new CheckOutRequestBusiness();
                    var mash = reqBus.isMashmoolferaghat(oPajohesh.StCode);
                    if (mash != null)
                    {
                        if (mash > 0)
                        {
                            var smsBodymashmool = "دانشجوی گرامی با توجه به اینکه شما مشمول خدمت نظام وظیفه می باشید لازم است بموقع نسبت به ثبت درخواست تسویه حساب اقدام نمایید . تا مشمول غیبت سربازی نشوید.";
                            var resultMashmoolSMS = oCommon.sendSMS(1, oPajohesh.StCode, smsBodymashmool, out sentSMS, out smsStatusText);
                        }

                    }
                }
            }
            else
            {
                lblSysMsg.Text = "لطفا موارد مورد نیاز جهت تایید نهایی را درج نمایید.";
                divlblSysMsg.Visible = true;
                dvErrorMsg.Visible = true;
                ViewScript();
            }

        }

        private bool registerCheckoutRequest(string stcode)
        {
            CheckOutRequestBusiness _reqBusiness = new CheckOutRequestBusiness();
            int isbachelor = _reqBusiness.GetIsBachelor(stcode);
            var lastRequestID = _reqBusiness.exist_IdMelli(stcode);
            if (lastRequestID > 0)
            {
                showMessage("دانشجو درخواست تسویه حساب دیگری در حال گردش دارد و امکان ثبت اتوماتیک درخواست فارغ التحصیلی برای ایشان نمی باشد.");
                return true;
            }
            if (!_reqBusiness.hasPassedCoursesToSubmitGraduateRequest(stcode, isbachelor))
            {
                showMessage("تعداد واحدهای گذرانده دانشجو کمتر از سقف مجاز برای ثبت درخواست فارغ التحصیلی می باشد!");
                return false;
            }
            if (!business.IsFinilized(stcode))
            {
                return false;
            }
            
            string CreateDate = DateTime.Now.ToPeString();
            int reqID=_reqBusiness.InsertInToStudentRequest(stcode, (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil, (int)CheckOutStatusEnum.CheckOutAllStatusEnum.submited, ((int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshkade).ToString(), "",CreateDate, "در حال بررسی", 1, true);
            if (reqID > 0)
            {
                showMessage("درخواست فارغ التحصیلی به صورت اتوماتیک برای دانشجو ثبت شد.");
                setLog("ثبت درخواست فارغ التحصیلی اتوماتیک بلافاصله بعد از تایید نهایی وضعیت دفاع", reqID, (int)DTO.eventEnum.ثبت_درخواست_تسویه);
                CommonBusiness cb = new CommonBusiness();
                bool sentMsg;
                string smsStatus;
                cb.sendSMS(1, stcode, " دانشجوی گرامی؛ ضمن تبریک به مناسبت دفاع موفق از پایان­نامه، به استحضار می­رساند که فرآیند اجرایی تسویه حساب سرکارعالی/ جناب­عالی شروع گردید. جهت اطلاع از جزئیات و پيگيري مراحل لطفاً به سامانه سرویس گزینه تسویه حساب مراجعه نمایید. واحد الکترونیکی دانشگاه آزاد اسلامی",out sentMsg, out smsStatus);
                return true;
            }
            return false;
        }

        private void showMessage(string msg)
        {
            RadWindowManager1.RadAlert(msg, 0, 200, "پیام", "");
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

        private bool ValidateInput()
        {
            DateTime toEduDate = DateTime.Now;
            DateTime toPajDate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(pcal5.Text) && !string.IsNullOrEmpty(pcal4.Text))
            {


                var splitedToPajDate = pcal5.Text.Trim().Split('/');

                //var dateNow = DateTime.Now;
                toPajDate = new DateTime(Convert.ToInt32(splitedToPajDate[0]),
                   Convert.ToInt32(splitedToPajDate[1]),
                   Convert.ToInt32(splitedToPajDate[2]), new System.Globalization.PersianCalendar());

                var splitedToEduDate = pcal4.Text.Trim().Split('/');

                //  var dateNow = DateTime.Now;
                toEduDate = new DateTime(Convert.ToInt32(splitedToEduDate[0]),
                   Convert.ToInt32(splitedToEduDate[1]),
                   Convert.ToInt32(splitedToEduDate[2]), new System.Globalization.PersianCalendar());

            }
            return (CommonBusiness.ValidateNomreh(txtDefPoint.Text) && /*!String.IsNullOrWhiteSpace(pcal1.Text) &&*/ !String.IsNullOrWhiteSpace(pcal4.Text))
                   &&
                   //(!String.IsNullOrWhiteSpace(pcal5.Text)
                   //&&
                   ((rdNo1.Checked || rdYes1.Checked)
                   &&
                   (rdNo2.Checked || rdYes2.Checked)
                   &&
                   (RdNo3.Checked || RdYes3.Checked)
                   &&
                   ((string.IsNullOrEmpty(txtEditThes.Text)) || (!string.IsNullOrEmpty(txtEditThes.Text) && chkbReciveEdit.Checked == true))
                   &&
                   (toPajDate <= toEduDate));
        }

        //protected void btnSendDateForPajoohesh_Click(object sender, EventArgs e)
        //{
        //    if (!String.IsNullOrWhiteSpace(pcal5.Text))
        //    {

        //        try
        //        {
        //            sys_msg = business.Add_Send_Date_for_pajoohesh(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, pcal5.Text);
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //        lblSysMsg.Text = sys_msg;
        //    }
        //}



        protected void btnSubmitDefDate_Click(object sender, EventArgs e)
        {
            DateTime toEduDate = DateTime.Now;
            DateTime toPajDate = DateTime.Now;

            if (!string.IsNullOrEmpty(pcal5.Text))
            {


                var splitedToPajDate = pcal5.Text.Trim().Split('/');

                //var dateNow = DateTime.Now;
                toPajDate = new DateTime(Convert.ToInt32(splitedToPajDate[0]),
                   Convert.ToInt32(splitedToPajDate[1]),
                   Convert.ToInt32(splitedToPajDate[2]), new System.Globalization.PersianCalendar());

                if (!string.IsNullOrEmpty(pcal4.Text))
                {
                    var splitedToEduDate = pcal4.Text.Trim().Split('/');

                    //  var dateNow = DateTime.Now;
                    toEduDate = new DateTime(Convert.ToInt32(splitedToEduDate[0]),
                       Convert.ToInt32(splitedToEduDate[1]),
                       Convert.ToInt32(splitedToEduDate[2]), new System.Globalization.PersianCalendar());
                }
            }
            if (!string.IsNullOrEmpty(pcal5.Text))
            {


                if ((toEduDate > toPajDate))
                {

                    var regex = new Regex(@"\d{4}(?:/\d{1,2}){2}");

                    if (String.IsNullOrWhiteSpace(pcal1.Text.Trim()) && String.IsNullOrWhiteSpace(pcal5.Text.Trim()))
                    {
                        string Msg = "تاریخ برگزاری و تاریخ ارسال مدارک خالی می باشد";
                        RadWindowManager1.RadAlert(Msg, 0, 100, "پیام", "");
                    }

                    if (!String.IsNullOrWhiteSpace(pcal1.Text))
                    {
                        var resualtPcal1 = regex.Match(pcal1.Text);
                        if (!resualtPcal1.Success)
                        {
                            string Msg = "تاریخ برگزاری دفاع نا معتبر است";
                            RadWindowManager1.RadAlert(Msg, 0, 100, "پیام", "");
                        }
                        string stcode = lblStcode.Text;
                        ////////


                        var splitedDate = pcal1.Text.Trim().Split('/');

                        var dateNow = DateTime.Now;
                        var Date = new DateTime(Convert.ToInt32(splitedDate[0]),
                            Convert.ToInt32(splitedDate[1]),
                            Convert.ToInt32(splitedDate[2]), new System.Globalization.PersianCalendar());

                        if ((Date - dateNow).Days > 0)
                        {
                            RadWindowManager1.RadAlert("تاریخ برگزاری جلسه دفاع باید قبل از تاریخ روز جاری باشد", 0, 100, "پیام", "");
                            ViewScript();
                            return;
                        }
                        ///////

                        try
                        {
                            sys_msg = business.Add_Def_Date(Session[sessionNames.userID_Karbar].ToString(), pcal1.Text, stcode);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        lblSysMsg.Text = sys_msg;
                        divlblSysMsg.Visible = true;
                        //  CheckBox6.Enabled = true;
                        //  dvFinal.Visible = true;
                    }
                    if (!String.IsNullOrWhiteSpace(pcal5.Text))
                    {
                        var resualtPcal5 = regex.Match(pcal5.Text);
                        if (!resualtPcal5.Success)
                        {
                            string Msg = "تاریخ ارسال مدارک به حوزه معاونت پژوهشی نا معتبر است";
                            RadWindowManager1.RadAlert(Msg, 0, 100, "پیام", "");
                        }
                        try
                        {
                            sys_msg = business.Add_Send_Date_for_pajoohesh(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, pcal5.Text);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                        lblSysMsg.Text = sys_msg;
                        divlblSysMsg.Visible = true;
                    }
                    ViewScript();
                }
                else
                {
                    if (string.IsNullOrEmpty(pcal5.Text) && string.IsNullOrEmpty(pcal4.Text))
                    {
                        string Msg = "تاریخ ارسال مدارک به حوزه معاونت پژوهشی باید قبل از تاریخ ارسال مدرک به معاونت آموزشی باشد";
                        RadWindowManager1.RadAlert(Msg, 0, 100, "پیام", "");
                    }
                    else
                    {
                        string Msg = "تاریخ ارسال مدارک به حوزه معاونت پژوهشی نامعتبر است.";
                        RadWindowManager1.RadAlert(Msg, 0, 100, "پیام", "");
                    }

                }
            }
            else if (string.IsNullOrEmpty(pcal5.Text))
            {
                var regex = new Regex(@"\d{4}(?:/\d{1,2}){2}");
                if (!String.IsNullOrWhiteSpace(pcal1.Text))
                {
                    var resualtPcal1 = regex.Match(pcal1.Text);
                    if (!resualtPcal1.Success)
                    {
                        string Msg = "تاریخ برگزاری دفاع نا معتبر است";
                        RadWindowManager1.RadAlert(Msg, 0, 100, "پیام", "");
                    }
                    string stcode = lblStcode.Text;
                    ////////


                    var splitedDate = pcal1.Text.Trim().Split('/');

                    var dateNow = DateTime.Now;
                    var Date = new DateTime(Convert.ToInt32(splitedDate[0]),
                        Convert.ToInt32(splitedDate[1]),
                        Convert.ToInt32(splitedDate[2]), new System.Globalization.PersianCalendar());

                    if ((Date - dateNow).Days > 0)
                    {
                        RadWindowManager1.RadAlert("تاریخ برگزاری جلسه دفاع باید قبل از تاریخ روز جاری باشد", 0, 100, "پیام", "");
                        ViewScript();
                        return;
                    }
                    ///////

                    try
                    {
                        sys_msg = business.Add_Def_Date(Session[sessionNames.userID_Karbar].ToString(), pcal1.Text, stcode);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    lblSysMsg.Text = sys_msg;
                    divlblSysMsg.Visible = true;
                    //  CheckBox6.Enabled = true;
                    //  dvFinal.Visible = true;
                }
            }
            ViewScript();
        }

        protected void btnHasCancelForm_Click(object sender, EventArgs e)
        {
            CommonBusiness oCommon = new CommonBusiness();
            DataTable smsStatus = new DataTable();
            string smsBody = "دانشجوی گرامی فرم انصراف از مقاله در مدارک شما موجود نیست لطفا برای تکمیل فرم انصراف از مقاله و تحویل آن به کارشناس پژوهش دانشکده به سامانه تسویه حساب مراجعه نمایید  ";
            string result = "";
            CheckOutNaghsDTO oNaghs = new CheckOutNaghsDTO();
            CheckOutNaghsBusiness _naghsBusiness = new CheckOutNaghsBusiness();
            bool hasCancelForm;
            if (rdYes1.Checked)
            {

                hasCancelForm = true;
                rdYes2.Enabled = true;
                rdNo2.Enabled = true;
                oNaghs.StCode = lblStcode.Text;
                _naghsBusiness.Delete_Naghs(lblStcode.Text);



            }
            else
            {
                hasCancelForm = false;
                rdYes2.Enabled = false;
                rdNo2.Enabled = false;
                pcal6.Visible = false;
                pcal7.Visible = false;
                lblCancelDate.Visible = false;
                lblDeadLine.Visible = false;

            }
            if (!hasCancelForm)
            {
                var requestLogId = 27;
                oNaghs.NaghsMessage = "دانشجوی گرامی فرم انصراف از مقاله در مدارک شما موجود نیست لطفا نسبت به تکمیل فرم انصراف از مقاله و تحویل آن به کارشناس پژوهش دانشکده اقدام نمایید ";
                oNaghs.SubmitDate = DateTime.Now.ToPeString();
                oNaghs.StCode = lblStcode.Text;
                oNaghs.RequestLogId = requestLogId;
                _naghsBusiness.InsertNaghs_Article(oNaghs);
                string naghsMsg = "نقص فرم انصراف از مقاله ثبت شد";
                RadWindowManager1.RadAlert(naghsMsg, 0, 100, "پیام", "");
                rdYes2.Checked = false;
                rdNo2.Checked = false;
                pcal6.Text = string.Empty;
                pcal7.Text = string.Empty;
                btnReqPaper.Enabled = false;
                //result = oCommon.SendSMSByUserIdAndType(smsBody, lblStcode.Text, 1);
                //result = oCommon.sendSMS(1, lblStcode.Text,smsBody );

                string smsStatusText; bool sentSMS;
                result = oCommon.sendSMS(1, lblStcode.Text, smsBody, out sentSMS, out smsStatusText);

                // business.Add_DeadLineDateOrCancelDate(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, null, null);

            }

            try
            {
                sys_msg = business.Add_HasCancelForm(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, hasCancelForm);
            }
            catch (Exception)
            {

                throw;
            }
            lblSysMsg.Text = sys_msg;
            divlblSysMsg.Visible = true;

            //   btnReqPaper.Enabled = true;
            btnHasCancelForm.Enabled = false;

            ViewScript();
        }

        protected void rdNo1_CheckedChanged(object sender, EventArgs e)
        {

            btnHasCancelForm.Enabled = true;

            //if (true)
            //{
            rdYes1.Checked = false;
            //    rdYes2.Enabled = false;
            //    rdNo2.Enabled = false;

            //}
            ViewScript();
        }

        protected void rdYes1_CheckedChanged(object sender, EventArgs e)
        {

            //btnHasCancelForm.Enabled = true;
            //if (rdYes1.Checked)
            //{
            rdNo1.Checked = false;
            //    rdYes2.Enabled = true;
            //    rdNo2.Enabled = true;

            //}
            btnHasCancelForm.Enabled = true;

            ViewScript();
        }

        protected void rdYes2_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = business.GetStudentInfoForPajohesh(txtStcode.Text);
            if (rdYes2.Checked)
            {
                rdNo2.Checked = false;
                btnReqPaper.Enabled = true;
                pcal6.Visible = true;
                lblDeadLine.Visible = true;
                // pcal7.Text = string.Empty;
                pcal7.Visible = false;
                lblCancelDate.Visible = false;
                string defDate = "";
                if (dt.Rows[0]["Def_Date"] != DBNull.Value)
                {

                    defDate = dt.Rows[0]["Def_Date"].ToString();
                    if (dt.Rows[0]["DeadLineDate"] == DBNull.Value)
                    {
                        var splitedDate = defDate.Split('/');
                        var cc = new DateTime(int.Parse(splitedDate[0]), int.Parse(splitedDate[1]), int.Parse(splitedDate[2]), new PersianCalendar());
                        var d = cc.AddMonths(6);
                        var persianDate = d.ToPeString();
                        plHolder = persianDate;
                    }


                }
                pcal6.Text = plHolder;


            }
            else
            {
                rdYes2.Checked = false;
                btnReqPaper.Enabled = true;
                pcal6.Visible = false;
                lblDeadLine.Visible = false;
                pcal7.Visible = true;
                lblCancelDate.Visible = true;
            }
            ViewScript();

        }

        protected void rdNo2_CheckedChanged(object sender, EventArgs e)
        {


            if (rdNo2.Checked)
            {
                rdYes2.Checked = false;
                // pcal6.Text = string.Empty;
                btnReqPaper.Enabled = true;
                pcal6.Visible = false;
                lblDeadLine.Visible = false;
                pcal7.Visible = true;
                lblCancelDate.Visible = true;
            }
            else
            {
                rdNo2.Checked = false;
                btnReqPaper.Enabled = true;
                pcal6.Visible = true;
                lblDeadLine.Visible = true;
                pcal7.Visible = false;
                lblCancelDate.Visible = false;
            }
            ViewScript();

        }

        protected void btnReqPaper_Click(object sender, EventArgs e)
        {
            var regex = new Regex(@"\d{4}(?:/\d{1,2}){2}");
            string deadLineDate;
            string cancelDate;

            if (rdYes2.Checked)
            {

                if (!String.IsNullOrEmpty(pcal6.Text) && !String.IsNullOrEmpty(pcal7.Text))
                {

                    var resualtPcal6 = regex.Match(pcal6.Text);
                    if (!resualtPcal6.Success)
                    {
                        string Msg = "تاریخ مهلت ارائه مقاله نا معتبر است";
                        RadWindowManager1.RadAlert(Msg, 0, 100, "پیام", "");
                    }
                    var splitedDate = pcal6.Text.Trim().Split('/');

                    var dateNow = DateTime.Now;
                    var Date = new DateTime(Convert.ToInt32(splitedDate[0]),
                        Convert.ToInt32(splitedDate[1]),
                        Convert.ToInt32(splitedDate[2]), new System.Globalization.PersianCalendar());

                    if ((Date - dateNow).Days < 0)
                    {
                        RadWindowManager1.RadAlert("تاریخ مهلت ارائه مقاله باید بعد از امروز باشد", 0, 100, "پیام", "");
                        ViewScript();
                        return;
                    }

                    //var resualtPcal7 = regex.Match(pcal7.Text);
                    //if (!resualtPcal7.Success)
                    //{
                    //    string Msg = "تاریخ انصراف از مقاله نا معتبر است";
                    //    RadWindowManager1.RadAlert(Msg, 0, 100, "پیام", "");
                    //}
                    //var splitedDate2 = pcal7.Text.Trim().Split('/');

                    //var dateNow2 = DateTime.Now;
                    //var Date2 = new DateTime(Convert.ToInt32(splitedDate[0]),
                    //    Convert.ToInt32(splitedDate2[1]),
                    //    Convert.ToInt32(splitedDate2[2]), new System.Globalization.PersianCalendar());


                    //if ((Date2 - dateNow2).Days >= 0)
                    //{
                    //    RadWindowManager1.RadAlert("تاریخ تسویه با پژوهش باید قبل یا برابر امروز باشد", 0, 100, "پیام", "");
                    //    ViewScript();
                    //    return;
                    //}

                    deadLineDate = pcal6.Text;
                    cancelDate = pcal7.Text;
                }
                else
                {
                    string empMsg = "لطفا مهلت ارائه مقاله را به همراه تاریخ تسویه با پژوهش وارد نمایید";
                    RadWindowManager1.RadAlert(empMsg, 0, 100, "پیام", "");
                    deadLineDate = null;
                    btnReqPaper.Enabled = true;
                }

            }
            else
            {
                deadLineDate = null;
            }

            if (rdNo2.Checked)
            {


                if (!String.IsNullOrEmpty(pcal7.Text.Trim()))
                {

                    var resualtPcal7 = regex.Match(pcal7.Text);
                    if (!resualtPcal7.Success)
                    {
                        string Msg = "تاریخ تسویه با پژوهش نا معتبر است";
                        RadWindowManager1.RadAlert(Msg, 0, 100, "پیام", "");
                    }
                    cancelDate = pcal7.Text;

                }
                else
                {
                    string empMsg = "لطفا تاریخ تسویه با پژوهش را وارد نمایید";
                    RadWindowManager1.RadAlert(empMsg, 0, 100, "پیام", "");
                    cancelDate = null;
                }

            }
            else
            {
                cancelDate = pcal7.Text;
            }

            try
            {
                if (!string.IsNullOrEmpty(deadLineDate) || !string.IsNullOrEmpty(cancelDate))
                {
                    sys_msg = business.Add_DeadLineDateOrCancelDate(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, deadLineDate, cancelDate);
                    btnReqPaper.Enabled = false;
                }
                else
                {
                    string empMsg = "لطفا وضعیت درخواست مقاله را با درج تاریخ مربوط، مشخص نمایید";
                    RadWindowManager1.RadAlert(empMsg, 0, 100, "پیام", "");
                    lblSysMsg.Enabled = false;
                }

            }
            catch (Exception)
            {

                throw;
            }
            if (!string.IsNullOrEmpty(sys_msg))
            {
                lblSysMsg.Text = sys_msg;
                divlblSysMsg.Visible = true;
            }


            ViewScript();

        }

        protected void RdYes3_CheckedChanged(object sender, EventArgs e)
        {


            if (RdYes3.Checked)
            {
                RdNo3.Checked = false;
                btnEditThes.Enabled = true;
                txtEditThes.Visible = true;

                lblDetail.Visible = true;
                chkbReciveEdit.Visible = true;

            }
            else
            {
                RdYes3.Checked = false;
                btnEditThes.Enabled = true;
                txtEditThes.Visible = false;
                lblDetail.Visible = false;
                chkbReciveEdit.Visible = false;
            }
            ViewScript();
        }



        protected void btnEditThes_Click(object sender, EventArgs e)
        {

            var ReqLogId = 28;//اصلاح پایان نامه
            string editThes = txtEditThes.Text;
            if (RdYes3.Checked && !(String.IsNullOrEmpty(txtEditThes.Text.Trim())))
            {

                if (chkbReciveEdit.Checked == true)
                {
                    editThes = txtEditThes.Text;
                    try
                    {
                        sys_msg = business.Add_EditThesDetailWithEditForm(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, editThes, ReqLogId);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    lblSysMsg.Text = sys_msg;
                    divlblSysMsg.Visible = true;
                    btnEditThes.Enabled = false;
                }
                else
                {

                    try
                    {
                        sys_msg = business.Add_EditThesDetail(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, editThes, ReqLogId, 0);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                lblSysMsg.Text = sys_msg;
                divlblSysMsg.Visible = true;
                btnEditThes.Enabled = false;
            }
            else if (RdYes3.Checked && (String.IsNullOrEmpty(txtEditThes.Text.Trim())))
            {
                string empMsg = "در صورتی که پایان نامه اصلاحات دارد لطفا محل مربوط به توضیحات را پر نمایید";
                RadWindowManager1.RadAlert(empMsg, 0, 100, "پیام", "");
            }

            if (RdNo3.Checked)
            {
                var isDeleted = 1;
                editThes = null;
                sys_msg = business.Add_EditThesDetail(Session[sessionNames.userID_Karbar].ToString(), lblStcode.Text, editThes, ReqLogId, isDeleted);
                btnEditThes.Enabled = false;
            }

            ViewScript();
        }

        protected void RdNo3_CheckedChanged(object sender, EventArgs e)
        {


            if (RdNo3.Checked)
            {
                RdYes3.Checked = false;
                btnEditThes.Enabled = true;
                txtEditThes.Visible = false;

                lblDetail.Visible = false;
                chkbReciveEdit.Visible = false;
            }
            else
            {
                RdNo3.Checked = false;
                btnEditThes.Enabled = true;
                txtEditThes.Visible = true;
                lblDetail.Visible = true;
                chkbReciveEdit.Visible = true;

            }
            ViewScript();
        }

        protected void chkbReciveEdit_CheckedChanged(object sender, EventArgs e)
        {
            btnEditThes.Enabled = true;
            if (chkbReciveEdit.Checked == true)
            {
                btnEditThes.Enabled = true;
            }
            ViewScript();
        }

        protected void pcal4_TextChanged(object sender, EventArgs e)
        {
            btnSubmitDefPoint.Enabled = true;
        }

        protected void btnYes_DefPointUpdate_Click(object sender, EventArgs e)
        {
            submitDefPoint(true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal_ChangeDefPoint();", true);

        }

        protected void btnNo_DefPointUpdate_Click(object sender, EventArgs e)
        {
            submitDefPoint(false);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModal_ChangeDefPoint();", true);


        }
    }
}