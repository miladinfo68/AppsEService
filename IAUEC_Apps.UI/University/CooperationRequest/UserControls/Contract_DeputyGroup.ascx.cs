using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IAUEC_Apps.UI.University.CooperationRequest.UserControls
{
    public partial class Contract_DeputyGroup : System.Web.UI.UserControl
    {
        public int TeacherCode { get; set; }
        public int userType { get; set; }
        public int userCode { get; set; }
        public bool signature { get; set; }
        public string year { get; set; }
        public bool canSign { get; set; }
        public string incompletedInf { get; set; }
        public string contractFile { get; set; }
        Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
        Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();
        Business.Common.CommonBusiness commonBSN = new Business.Common.CommonBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //TODO:
                //Redirect To Login If Session is Null
                if (!string.IsNullOrEmpty(contractFile))
                    hdnContractFile.Value = Base64Encode(contractFile);
            }
                FillForm();
        }

        private void FillForm()
        {
            setSignature();
            if (!string.IsNullOrEmpty(hdnContractFile.Value))
            {
                incompletedInf = "";
                pnlContractForm.Visible = false;
                pnlFromDB.Visible = true;
                pnlFromDB.Controls.Clear();
                pnlFromDB.Controls.Add(new LiteralControl(Base64Decode(hdnContractFile.Value)));
            }
            else
            {

                pnlContractForm.Visible = true;
                pnlFromDB.Visible = false;
                setUniversityInformation();
                canSign = setTeacherInformation();

                setNameOfSignataures();
            }

        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void setUniversityInformation()
        {
            string addressFooterFormat = "نشانی: {0}   تلفن تماس:{1}   کد پستی:{2}";
            DataTable dtSarparast = bsn.getSignature(0, (int)DTO.RoleEnums.سرپرست_واحد);
            if (dtSarparast.Rows.Count == 1)
                lblAgentName.Text = dtSarparast.Rows[0]["displayName"].ToString();



            DataTable dt = commonBSN.getBasicInformation((int)DTO.basicType.آدرس, 1);
            if (dt.Rows.Count > 0)
                if (dt.Rows[0]["value"] != DBNull.Value)
                    lblUniversityAddress.Text = dt.Rows[0]["value"].ToString();


            dt = commonBSN.getBasicInformation((int)DTO.basicType.تلفن, 1);
            if (dt.Rows.Count > 0 && dt.Rows[0]["value"] != DBNull.Value)
                lblUniversityPhone.Text = dt.Rows[0]["value"].ToString();



            dt = commonBSN.getBasicInformation((int)DTO.basicType.ثبت, 1);
            if (dt.Rows.Count > 0 && dt.Rows[0]["value"] != DBNull.Value)
                lblUniversityRegNo.Text = dt.Rows[0]["value"].ToString();



            dt = commonBSN.getBasicInformation((int)DTO.basicType.کدپستی, 1);
            if (dt.Rows.Count > 0 && dt.Rows[0]["value"] != DBNull.Value)
                lblUniversityPostalCode.Text = dt.Rows[0]["value"].ToString();


            lblAddressFooter.Text = string.Format(addressFooterFormat, lblUniversityAddress.Text, lblUniversityPhone.Text, lblUniversityPostalCode.Text);
        }
        private bool setTeacherInformation()
        {
            if (TeacherCode == 0)
                return false;
            DataTable dtPeople = FRB.GetOstadInfoFromHR(TeacherCode);//FRB.GetOstadInfoFromHR(Convert.ToInt32(Session["user"]));
            if (dtPeople.Rows.Count == 1)
            {
                Business.Adobe.ProfPresentBusiness pb = new Business.Adobe.ProfPresentBusiness();

                professorCode.InnerText = TeacherCode.ToString();
                lblProfGender.Text = dtPeople.Rows[0]["sex"] == DBNull.Value ? "آقای/خانم " : dtPeople.Rows[0]["sex"].ToString() == "1" ? "آقای " : "خانم ";

                lblAddress.Text = dtPeople.Rows[0]["add_home"] == DBNull.Value ? "" : dtPeople.Rows[0]["add_home"].ToString();
                lblBankAccount.Text = dtPeople.Rows[0]["siba"] == DBNull.Value ? "" : dtPeople.Rows[0]["siba"].ToString();
                lblBirthDate.Text = dtPeople.Rows[0]["sal_tav"] == DBNull.Value ? "----" : dtPeople.Rows[0]["sal_tav"].ToString();
                lblCertificateTitle.Text = dtPeople.Rows[0]["idmadrak"] == DBNull.Value ? "" : getNameOfCode(Convert.ToInt32(dtPeople.Rows[0]["idmadrak"]), 2);
                lblEmail.Text = dtPeople.Rows[0]["add_email"] == DBNull.Value ? "" : dtPeople.Rows[0]["add_email"].ToString();
                lblFatherName.Text = dtPeople.Rows[0]["namep"] == DBNull.Value ? "" : dtPeople.Rows[0]["namep"].ToString();
                lblFieldTitle.Text = dtPeople.Rows[0]["idresh"] == DBNull.Value ? "" : getNameOfCode(Convert.ToInt32(dtPeople.Rows[0]["idresh"]), 4);
                lblFirstName.Text = dtPeople.Rows[0]["name"] == DBNull.Value ? "" : dtPeople.Rows[0]["name"].ToString();
                lblLastName.Text = dtPeople.Rows[0]["family"] == DBNull.Value ? "" : dtPeople.Rows[0]["family"].ToString();
                lblFullName.Text = dtPeople.Rows[0]["name"] == DBNull.Value ? "" : dtPeople.Rows[0]["name"].ToString() + "  " + dtPeople.Rows[0]["family"].ToString();
                lblIdd.Text = dtPeople.Rows[0]["idd"] == DBNull.Value ? "" : dtPeople.Rows[0]["idd"].ToString();
                if (dtPeople.Rows[0]["IsRetired"] != DBNull.Value)
                {
                    lblJobStatus.Text = (dtPeople.Rows[0]["IsRetired"].ToString() == "0" ? "شاغل" : "بازنشسته");
                    //dvJobDetail.Visible = true;
                }
                lblMaritalStatus.Text = dtPeople.Rows[0]["marital_status"] == DBNull.Value ? "" : (dtPeople.Rows[0]["IsRetired"].ToString() == "1" ? "مجرد" : "متاهل");// ;
                lblMobile.Text = dtPeople.Rows[0]["mobile"] == DBNull.Value ? "" : dtPeople.Rows[0]["mobile"].ToString();
                lblNationalCode.Text = dtPeople.Rows[0]["idd_meli"] == DBNull.Value ? "" : dtPeople.Rows[0]["idd_meli"].ToString();
                lblPhoneNo.Text = dtPeople.Rows[0]["tel_home"] == DBNull.Value ? "----" : dtPeople.Rows[0]["tel_home"].ToString();
                lblUniversity.Text = dtPeople.Rows[0]["university"] == DBNull.Value ? "" : getNameOfCode(Convert.ToInt32(dtPeople.Rows[0]["university"]), 1);
                lblPaye.Text = dtPeople.Rows[0]["payeh"] == DBNull.Value ? "" : dtPeople.Rows[0]["payeh"].ToString();
                lblGroups.Text = pb.getActiveGroupOfGroupManagers(Convert.ToInt32(year), TeacherCode);
                lblMartabe.Text = dtPeople.Rows[0]["martabeh"] == DBNull.Value ? "" : FRB.getMartabeText(Convert.ToInt32(dtPeople.Rows[0]["martabeh"].ToString()));
                lbkSalary.Text = pb.getProfessorSalary(Convert.ToInt32(year), TeacherCode).ToString();
                DataTable dtWorkDate = bsn.getbeginAndEndWorkTimeHOD(TeacherCode, Convert.ToInt32(year));

                lblFromDate.Text = dtWorkDate.Rows[0]["beginDay"].ToString();
                lblToDate.Text = dtWorkDate.Rows[0]["endDay"].ToString();
                if (lblToDate.Text.Length > 0)
                {
                    string[] splt = lblToDate.Text.Split('/');
                    if (splt.Length == 3 && splt[1] == "12" && splt[2] == "30")
                    {
                        splt[2] = DateTime.IsLeapYear(DateTime.Now.Year - ((Convert.ToInt32(DateTime.Now.ToPeString().Substring(0, 4))) - Convert.ToInt32(splt[0]))) ? "30" : "29";
                        lblToDate.Text = string.Format("{0}/{1}/{2}", splt[0], splt[1], splt[2]);
                    }
                }

                incompletedInf = "";
                if (lblFromDate.Text == "")
                    incompletedInf += "تاریخ شروع به فعالیت سال انتخابی، ";

                if (lblToDate.Text == "")
                    incompletedInf += "تاریخ پایان فعالیت در سال انتخابی، ";

                if (lbkSalary.Text == "")
                    incompletedInf += "مبلغ قرارداد، ";

                if (lblAddress.Text == "")
                    incompletedInf += "آدرس محل سکونت، ";

                if (lblAddress.Text == "")
                    incompletedInf = "آدرس محل سکونت، ";

                if (lblBankAccount.Text == "")
                    incompletedInf += "شماره حساب بانکی، ";

                //if (lblBirthDate.Text == "")
                //    incompletedInf += "تاریخ تولد، ";

                if (lblCertificateTitle.Text == "")
                    incompletedInf += "مدرک تحصیلی، ";

                if (lblEmail.Text == "")
                    incompletedInf += "پست الکترونیکی، ";

                if (lblFatherName.Text == "")
                    incompletedInf += "نام پدر، ";

                if (lblFieldTitle.Text == "")
                    incompletedInf += "رشته تحصیلی، ";

                if (lblFirstName.Text == "")
                    incompletedInf += "نام، ";

                if (lblFullName.Text == "")
                    incompletedInf += "نام خانوادگی، ";

                if (lblIdd.Text == "")
                    incompletedInf += "شماره شناسنامه، ";

                if (lblJobStatus.Text == "")
                    incompletedInf += "وضعیت شغلی، ";

                if (lblMaritalStatus.Text == "")
                    incompletedInf += "وضعیت تاهل، ";

                if (lblLastName.Text == "")
                    incompletedInf += "نام خانوادگی، ";

                if (lblMobile.Text == "")
                    incompletedInf += "تلفن همراه، ";

                if (lblNationalCode.Text == "")
                    incompletedInf += "کد ملی، ";


                if (lblUniversity.Text == "")
                    incompletedInf += "نام دانشگاه، ";

                if (incompletedInf.Length > 3)
                    incompletedInf = incompletedInf.Substring(0, incompletedInf.Length - 2);

                return incompletedInf.Length == 0;
            }
            return dtPeople.Rows.Count == 1;
        }
        private void setSignature()
        {
            imgSignature1.Visible = false;
            imgSignature1.ImageUrl = "";
            if (signature)
            {
                setNameOfSignataures();
                if (userType > 1)
                    setUserSignature();
                else
                    setTeacherSignature();
            }
            else
            {
                clearSignature();
            }
        }
        private string getNameOfCode(int code, int type)
        {
            DataTable dt = commonBSN.GetCodingByTypeId(type);
            DataRow[] dr = dt.Select("id=" + code);
            if (dr.Length == 1)
                return dr[0]["namecoding"].ToString();
            return "";
        }

        private void setNameOfSignataures()
        {
            string signature1 = "";
            string signature2 = "";
            string signature3 = "";
            string signature4 = "";
            signature1 = signature ? "" : lblFullName.Text;
            DataTable dtNameOfSignature = bsn.getSignature(0, (int)DTO.RoleEnums.سرپرست_واحد);
            signature2 = string.Format("{0}  {1}", dtNameOfSignature.Rows[0]["username"].ToString(), dtNameOfSignature.Rows[0]["displayName"].ToString());

            //dtNameOfSignature = bsn.getSignature(0, (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی);
            //signature3 = string.Format("{0}  {1}", dtNameOfSignature.Rows[0]["username"].ToString(), dtNameOfSignature.Rows[0]["displayName"].ToString());
            //dtNameOfSignature = bsn.getSignature(0, (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی);
            //signature4 = string.Format("{0}  {1}", dtNameOfSignature.Rows[0]["username"].ToString(), dtNameOfSignature.Rows[0]["displayName"].ToString());


            if (string.IsNullOrEmpty(hdnContractFile.Value))
            {

                lblSignatureName1.Text = signature1;
                lblSignatureName2.Text = signature2;
                //lblSignatureName3.Text = signature3;
                //lblSignatureName4.Text = signature4;
            }
            //else
            //{
            //    string htmlFromDB = hdnContractFile.Value.ToString();
            //    System.Text.StringBuilder reader = new System.Text.StringBuilder();
            //    pnlFromDB.RenderControl(new HtmlTextWriter(new System.IO.StringWriter(reader)));
            //    htmlFromDB = reader.ToString();
            //    (pnlFromDB.FindControl("lblSignatureName1")).RenderControl(new HtmlTextWriter(new System.IO.StringWriter(reader)));
            //    htmlFromDB = reader.ToString();

            //}
        }

        private void setTeacherSignature()
        {

            DataTable dtPeople = FRB.GetOstadInfoFromHR(TeacherCode);
            DataTable dt;
            if (dtPeople.Rows.Count == 1)
            {
                imgSignature1.Visible = true;
                int hrID = Convert.ToInt32(dtPeople.Rows[0]["ID"]);
                dt = bsn.getSignature(hrID, 1);
                if (dt.Rows.Count == 0)
                {
                    Response.Redirect("../../../commonui/teacherIntro.aspx");
                }
                else if (dt.Rows[0]["Signature"] == DBNull.Value)
                {
                    Response.Redirect("../../../University/CooperationRequest/Teachers/contractsMain.aspx");
                }
                if (dt.Rows[0]["Signature"] != DBNull.Value)
                {
                    if (string.IsNullOrEmpty(hdnContractFile.Value))
                    {
                        imgSignature1.ImageUrl = dt.Rows[0]["Signature"].ToString().Substring(1);
                    }
                    else
                    {
                        hdnContractFile.Value.ToString();
                    }
                }
            }

        }

        private void setUserSignature()
        {
            DataTable dt;

            switch (userCode)
            {
                case (int)DTO.RoleEnums.مسئول_حق_التدریس:
                    dt = bsn.getSignature(0, (int)DTO.RoleEnums.مسئول_حق_التدریس);
                    if (dt.Rows.Count > 0)
                        putImageIntoHTML_FromDB("dvimgSign3", dt);

                    break;
                case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی:
                    dt = bsn.getSignature(0, (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی);
                    if (dt.Rows.Count > 0)
                        putImageIntoHTML_FromDB("dvimgSign3", dt);

                    break;
                case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی:
                    dt = bsn.getSignature(0, (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی);
                    if (dt.Rows.Count > 0)
                        putImageIntoHTML_FromDB("dvimgSign4", dt);
                    break;
                case (int)DTO.RoleEnums.سرپرست_واحد:
                    replaceText("", "class=\"Sign2\">", "</span>");
                    dt = bsn.getSignature(0, (int)DTO.RoleEnums.سرپرست_واحد);
                    if (dt.Rows.Count > 0)
                        putImageIntoHTML_FromDB("dvimgSign2", dt);

                    break;
            }
        }

        private void clearSignature()
        {
            string startOfTag = "<div class=\"";
            string endOfTag = ".jpg\"/></div>";
            string imgTag = "dvimgSign";
            string imgToClear = "";
            int indexOfImgTag_start = 0;
            int indexOfImgTag_end = 0;
            switch (userCode)
            {
                case (int)DTO.RoleEnums.مسئول_حق_التدریس:
                case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی:
                    imgTag += "3";
                    break;
                case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی:
                    imgTag += "4";
                    break;
                case (int)DTO.RoleEnums.سرپرست_واحد:
                    imgTag += "2";
                    break;
            }
            indexOfImgTag_start = Base64Decode(hdnContractFile.Value).IndexOf(startOfTag + imgTag);
            if (indexOfImgTag_start > 0)
            {
                imgToClear = Base64Decode(hdnContractFile.Value).Substring(indexOfImgTag_start);
                indexOfImgTag_end = imgToClear.IndexOf(endOfTag) + endOfTag.Length;
                if (indexOfImgTag_end > 0)
                {
                    imgToClear = imgToClear.Substring(0, indexOfImgTag_end);
                    if (imgToClear.Contains(imgTag))
                        hdnContractFile.Value = Base64Encode(Base64Decode(hdnContractFile.Value).Replace(imgToClear, ""));
                }
            }


        }

        private string getSpecificText(string startTag, string endTag)
        {
            string text = "";
            int IndexStart = Base64Decode(hdnContractFile.Value).IndexOf(startTag);
            if (IndexStart > 0)
            {
                IndexStart += startTag.Length;
                text = Base64Decode(hdnContractFile.Value).Substring(IndexStart);
                int IndexEnd = text.IndexOf(endTag);
                if (IndexEnd > 0)
                {
                    text = text.Substring(0, IndexEnd);
                }
                else
                    text = "";
            }
            return text;
        }

        private void replaceText(string newText, string startTag, string endTag)
        {
            string text = getSpecificText(startTag, endTag);
            if (text != "")
            {
                string all = Base64Decode(hdnContractFile.Value).Replace(startTag + text + endTag, startTag + newText + endTag);
                hdnContractFile.Value = Base64Encode(all);
            }
        }

        private void putImageIntoHTML_FromDB(string tag, DataTable dt)
        {
            string endOfTag = "</div>";
            string startOfTag = "<div class=\"" + tag + "\">";
            string newTag = "<img height=150px width=150px;  src=\"" + dt.Rows[0]["Signature"].ToString().Substring(1) + "\"/>";

            replaceText(newTag, startOfTag, endOfTag);

        }

        public string getContentOfContract()
        {
            var sb = new System.Text.StringBuilder();
            var sbBegin = new System.Text.StringBuilder();
            var sbEnd = new System.Text.StringBuilder();
            string htmlCode;
            string beginTAG;
            string endTAG = "</div>";
            if (string.IsNullOrEmpty(hdnContractFile.Value))
            {
                //from panel A

                pnlContractForm.RenderControl(new HtmlTextWriter(new System.IO.StringWriter(sb)));
                pnlContractForm.RenderBeginTag(new HtmlTextWriter(new System.IO.StringWriter(sbBegin)));
                try
                {
                    pnlContractForm.RenderEndTag(new HtmlTextWriter(new System.IO.StringWriter(sbEnd)));
                }
                catch (Exception ex) { }

            }
            else
            {
                //from panel B--DB
                pnlFromDB.RenderControl(new HtmlTextWriter(new System.IO.StringWriter(sb)));
                pnlFromDB.RenderBeginTag(new HtmlTextWriter(new System.IO.StringWriter(sbBegin)));
                //pnlFromDB.RenderEndTag(new HtmlTextWriter(new System.IO.StringWriter(sbEnd)));


            }
            htmlCode = SetPageSize(sb.ToString(), int.Parse(hdnDynamicHight.Value));
            beginTAG = sbBegin.ToString();
            if (sbEnd.Length != 0)
                endTAG = sbEnd.ToString();
            htmlCode = htmlCode.Substring(beginTAG.Length, (htmlCode.Length - beginTAG.Length - endTAG.Length));
            return htmlCode;
        }

        private string SetPageSize(string html, int height)
        {
            if (height > 200)
            {
                var lp1 = html.Substring(html.LastIndexOf("<div class=\"lp1\">") + 17);
                lp1 = lp1.Remove(lp1.IndexOf("</div>"));
                html = html.Insert(html.LastIndexOf("<div class=\"fp2\">") + 17, lp1);

                var lp2 = html.Substring(html.LastIndexOf("<div class=\"lp2\">") + 17);
                lp2 = lp2.Remove(lp2.IndexOf("</div>"));
                html = html.Insert(html.LastIndexOf("<div class=\"fp3\">") + 17, lp2);

                html = html.Remove(html.LastIndexOf("<div class=\"lp1\">") + 17, lp1.Length);
                html = html.Remove(html.LastIndexOf("<div class=\"lp2\">") + 17, lp2.Length);
            }
            return html;
        }
    }
}