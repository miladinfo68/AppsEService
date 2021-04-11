using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.CooperationRequest.UserControls
{
    public partial class Agreement : System.Web.UI.UserControl
    {
        Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();

        public Int64 teacherCode { get; set; }
        public int HRCode { get; set; }
        public int userType { get; set; }
        public int userCode { get; set; }
        public bool signature { get; set; }
        public int agreementStatus { get; set; }
        public string agreementFile { get; set; }
        public bool canSign { get; set; }
        public string incompletedInf { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //TODO:
                //Redirect To Login If Session is Null
                if (!string.IsNullOrEmpty(agreementFile))
                    hdnAgreementFile.Value = Base64Encode(agreementFile);

            }
            FillForm();
        }

        private void FillForm()
        {
            setSignature();
            if (!string.IsNullOrEmpty(hdnAgreementFile.Value))
            {
                incompletedInf = "";
                pnlAgreement.Visible = false;
                pnlFromDB.Visible = true;
                pnlFromDB.Controls.Clear();
                replaceText(" ", "class=\"linkBakhshname\"", "target=\"_blank\"");
                pnlFromDB.Controls.Add(new LiteralControl(Base64Decode(hdnAgreementFile.Value)));
            }
            else
            {
                canSign = setTeacherInformation();
            }
            setNameOfSignataures();

        }

        private void setSignature()
        {
            //if (!signature)
            //{
            imgSignature1.Visible = false;
            //imgSignature2.Visible = false;
            //imgSignature3.Visible = false;
            //imgSignature4.Visible = false;
            imgSignature1.ImageUrl = "";
            //imgSignature2.ImageUrl = "";
            //imgSignature3.ImageUrl = "";
            //imgSignature4.ImageUrl = "";
            //}
            //else
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
        private void setTeacherSignature()
        {
            Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();

            var dtPeople = FRB.getOstadInfoFromPortal(teacherCode);
            DataTable dt;
            if (dtPeople.codeOstad > 0)
            {
                imgSignature1.Visible = true;
                int hrID = dtPeople.hrId;
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
                    if (string.IsNullOrEmpty(hdnAgreementFile.Value))
                    {
                        imgSignature1.ImageUrl = dt.Rows[0]["Signature"].ToString().Substring(1);
                    }
                    else
                    {
                        hdnAgreementFile.Value.ToString();
                    }
                }
            }

        }

        private void setUserSignature()
        {
            DataTable dt;

            switch (userCode)
            {
                case (int)DTO.RoleEnums.سرپرست_واحد:
                    replaceText("", "class=\"Sign2\">", "</span>");
                    dt = bsn.getSignature(0, (int)DTO.RoleEnums.سرپرست_واحد);
                    if (dt.Rows.Count > 0)
                        putImageIntoHTML_FromDB("dvimgSign2", dt);
                    
                    break;
            }
        }

        private void putImageIntoHTML_FromDB(string tag, DataTable dt)
        {
            string endOfTag = "</div>";
            string startOfTag = "<div class=\"" + tag + "\">";
            string newTag = "<img height=150px width=150px;  src=\"" + dt.Rows[0]["Signature"].ToString().Substring(1) + "\"/>";

            replaceText(newTag, startOfTag, endOfTag);

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
                case (int)DTO.RoleEnums.سرپرست_واحد:
                    imgTag += "2";
                    break;
            }
            indexOfImgTag_start = Base64Decode(hdnAgreementFile.Value).IndexOf(startOfTag + imgTag);
            if (indexOfImgTag_start > 0)
            {
                imgToClear = Base64Decode(hdnAgreementFile.Value).Substring(indexOfImgTag_start);
                indexOfImgTag_end = imgToClear.IndexOf(endOfTag) + endOfTag.Length;
                if (indexOfImgTag_end > 0)
                {
                    imgToClear = imgToClear.Substring(0, indexOfImgTag_end);
                    if (imgToClear.Contains(imgTag))
                        hdnAgreementFile.Value = Base64Encode(Base64Decode(hdnAgreementFile.Value).Replace(imgToClear, ""));
                }
            }


        }

        private void replaceText(string newText, string startTag, string endTag)
        {
            string text = getSpecificText(startTag, endTag);
            if (text != "")
            {
                string all = Base64Decode(hdnAgreementFile.Value).Replace(startTag + text + endTag, startTag + newText + endTag);
                hdnAgreementFile.Value = Base64Encode(all);
            }
        }
        private string getSpecificText(string startTag, string endTag)
        {
            string text = "";
            int IndexStart = Base64Decode(hdnAgreementFile.Value).IndexOf(startTag);
            if (IndexStart > 0)
            {
                IndexStart += startTag.Length;
                text = Base64Decode(hdnAgreementFile.Value).Substring(IndexStart);
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

        private bool setTeacherInformation()
        {
            Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();
            incompletedInf = "";
            var dtHR = FRB.getOstadInfoFromPortal(Convert.ToInt64(Session[sessionNames.userID_StudentOstad]));
            if (dtHR.codeOstad > 0)
            {
                if (dtHR.name != "" && dtHR.family != "")
                {
                    lblProfName.Text = dtHR.name
                        + " "
                        + dtHR.family;
                }
                else
                {
                    return false;
                    incompletedInf += "نام و نام خانوادگی,";
                }

                lblProfGender.Text = dtHR.sexIsMan ? "آقای " : "خانم ";

                return true;
            }
            return false;
        }
        public string getContentOfAgreement()
        {
            var sb = new System.Text.StringBuilder();
            var sbBegin = new System.Text.StringBuilder();
            var sbEnd = new System.Text.StringBuilder();
            string htmlCode;
            string beginTAG;
            string endTAG = "</div>";
            if (string.IsNullOrEmpty(hdnAgreementFile.Value))
            {
                //from panel A

                pnlAgreement.RenderControl(new HtmlTextWriter(new System.IO.StringWriter(sb)));
                pnlAgreement.RenderBeginTag(new HtmlTextWriter(new System.IO.StringWriter(sbBegin)));
                try
                {
                    pnlAgreement.RenderEndTag(new HtmlTextWriter(new System.IO.StringWriter(sbEnd)));
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
            ////if (string.IsNullOrEmpty(hdnContractFile.Value))
            ////{
            ////from panel A

            //pnlAgreement.RenderControl(new HtmlTextWriter(new System.IO.StringWriter(sb)));
            //pnlAgreement.RenderBeginTag(new HtmlTextWriter(new System.IO.StringWriter(sbBegin)));
            //try
            //{
            //    pnlAgreement.RenderEndTag(new HtmlTextWriter(new System.IO.StringWriter(sbEnd)));
            //}
            //catch (Exception ex) { }

            ////}
            ////else
            ////{
            ////    //from panel B--DB
            ////    pnlFromDB.RenderControl(new HtmlTextWriter(new System.IO.StringWriter(sb)));
            ////    pnlFromDB.RenderBeginTag(new HtmlTextWriter(new System.IO.StringWriter(sbBegin)));
            ////    //pnlFromDB.RenderEndTag(new HtmlTextWriter(new System.IO.StringWriter(sbEnd)));


            ////}
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
        private void setNameOfSignataures()
        {
            string signature1;
            string signature2;
            signature1 = signature ? "" : lblProfName.Text;
            DataTable dtNameOfSignature = bsn.getSignature(0, (int)DTO.RoleEnums.سرپرست_واحد);
            signature2 = string.Format("{0}  {1}", dtNameOfSignature.Rows[0]["username"].ToString(), dtNameOfSignature.Rows[0]["displayName"].ToString());


            lblSignatureName1.Text = signature1;
            lblSignatureName2.Text = signature2;

        }
    }
}