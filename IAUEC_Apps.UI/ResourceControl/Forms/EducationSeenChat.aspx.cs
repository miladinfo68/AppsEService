using IAUEC_Apps.Business.Conatct;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.UI.Contact.Functions;
using ResourceControl.BLL;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class EducationSeenChat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        public void RfrhDtLstMessage(bool flagGroup)
        {

            DtLstMesages.DataSource = null;
            DtLstMesages.DataBind();
            DataTable dt;
            if (flagGroup == false && TxtIdOnChat.Text.Trim() != "")
            {
                LblNameOnChat.Text = "گفتگو شخصی";
                dt = MessagePersonalBuisnes.GetMessagePersonal(txtStCode.Text, TxtIdOnChat.Text);
            }
            else
            {
                LblNameOnChat.Text = "گفتگو دفاع";
                dt = MesageGroupBuisnes.GetMessageGroup(txtStCode.Text, txtStCode.Text);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                dt = GetDefualt(dt);
                DtLstMesages.DataSource = dt;
                DtLstMesages.DataBind();
            }


        }

        public void RfrhDtLstContact()
        {
            if (txtStCode.Text != null && txtStCode.Text != "")
            {
                DataTable dt = ContactBuisnes.GetConatctOstads(txtStCode.Text);
                dt = GetDefualt(dt);
                DtlstContact.DataSource = dt;
                DtlstContact.DataBind();
            }

        }


        protected void DtlstContact_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
        {
            if (e.CommandName == "IdName")
            {
                LblCurCount.Text = "0";
                LblSearchCount.Text = "0";
                LblSearchText.Text = "";
                TxtIdOnChat.Text = Convert.ToString(e.CommandArgument);

                if (TxtIdOnChat.Text != "" && TxtIdOnChat.Text != "0")
                {
                    RfrhDtLstMessage(false);
                    LblIdGrp.Text = "";
                    MessageJs.DeleteUnreadStudent(LblIdUser.Text, "false", TxtIdOnChat.Text);
                }

                else
                {
                    RfrhDtLstMessage(true);
                    LblIdGrp.Text = txtStCode.Text;
                    MessageJs.DeleteUnreadStudent(LblIdUser.Text, "true", "-1");
                }

            }
        }



        protected void LnkNameConatct_Click(object sender, EventArgs e)
        {
            LinkButton lBtn = sender as LinkButton;
            string id = ((LinkButton)sender).CommandArgument.ToString();
            LblNameOnChat.Text = lBtn.Text;
            LblIdGrp.Text = "";
        }

        protected void BtnGrp_Click(object sender, EventArgs e)
        {
            LblNameOnChat.Text = "گفتگو دفاع";
            LblIdGrp.Text = txtStCode.Text;
            RfrhDtLstMessage(true);
        }
        public static DataTable GetDefualt(DataTable dt)
        {

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["images"] == null || row["images"].ToString() == "")
                    {
                        var imgDef = System.Web.HttpContext.Current.Server.MapPath("~/Contact/Img/avatarContact.jpg");

                        byte[] b = System.IO.File.ReadAllBytes(imgDef);

                        row["images"] = b;
                    }
                    if (dt.Columns.Contains("IsDeleted") && row["IsDeleted"].ToString().Trim() == "True")
                    {
                        row["Message"] = "پیام پاک شده است";
                    }
                    if (dt.Columns.Contains("FlagReplayed") && row["FlagReplayed"].ToString().Trim() == "True"
                        && (((row["RplyMsg"] == null || row["RplyMsg"].ToString().Trim() == ""))
                        || ((row["IsDeletedReplay"].ToString().Trim() == "True"))))
                    {
                        row["RplyMsg"] = "پیام پاک شده است";
                    }

                }

            }
            return dt;
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            lblStudentName.Text = "";
            lblStudentName.Visible = false;
            PnlChat.Visible = false;
            txtStCode.Text = "";
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            if (txtStCode.Text.Trim() == ""||lblStudentName.Text=="") return;

                lblStudentName.Visible = true;
                LblIdGrp.Text = txtStCode.Text;
                LblIdUser.Text = txtStCode.Text;
                RfrhDtLstContact();
                TxtIdOnChat.Text = "";
                RfrhDtLstMessage(true);
                PnlChat.Visible = true;


        }

        protected void txtStCode_TextChanged(object sender, EventArgs e)
        {
            RequestHandler requestHandler = new RequestHandler();
            if (txtStCode.Text.Trim() == "") return;
            DataTable dtStudentInformation = requestHandler.GetStudentInformationByStCode(txtStCode.Text);

            if (dtStudentInformation != null && dtStudentInformation.Rows.Count > 0)
            {
                lblStudentName.Text = dtStudentInformation.Rows[0]["FullName"].ToString();
                lblStudentName.Visible = true;


            }
            else
            {
                lblStudentName.Text = "";
                lblStudentName.Visible = false;

            }
            PnlChat.Visible = false;
        }
    }
}