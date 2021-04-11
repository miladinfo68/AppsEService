using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.DTO.EmailClasses;

namespace IAUEC_Apps.UI.EmailReg.CMS
{
    public partial class RequestRejected : System.Web.UI.Page
    {
        Email_ClassBusiness em = new Email_ClassBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //When user come from Admin_List
                if (Session["Admin"].ToString() != "IAdmin")
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                
                Email_Class emDTO = new Email_Class();
                
                emDTO = em.Email_Reg_Byid(int.Parse(Session["RequestID"].ToString()));
                lblStcode.Text = emDTO.Stcode;
                Add_DropTextlist();
                txtNote.Enabled = false;

                lblEmail.Text = emDTO.Email_Address.ToString() + "@iauec.ac.ir";
            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string Note;
            if (rbtnDefualt.Checked)
                Note = Dpl_Text.SelectedValue.ToString();
            else
                Note = txtNote.Text;
            Session["stcode"] = lblStcode.Text;
            Session["Description"] = Note;
            RadWindowManager1.RadConfirm("آیا مطمئن هستید؟", "CallBackConfirm", 250, 50, null, "پیام");
        }

        public void Add_DropTextlist()
        {
            ProofTextBusiness pt = new ProofTextBusiness();
            //List<ProofText> ptDTO = new List<ProofText>();
            DataTable dt = new DataTable();
            
           dt = pt.GiveAllProofText();

           //for (int i = 0; i < ptDTO.Count; i++)
           // {
           Dpl_Text.DataSource = dt;
           Dpl_Text.DataTextField = "Prooftext";
           Dpl_Text.DataBind();

            //}

        }



        protected void rbtnDefualt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDefualt.Checked)
            {
                Dpl_Text.Enabled = true;
                txtNote.Enabled = false;
            }
            else
            {
                txtNote.Enabled = true;
                Dpl_Text.Enabled = false;
            }
        }

        protected void rbtnSpecial_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnSpecial.Checked)
            {
                txtNote.Enabled = true;
                Dpl_Text.Enabled = false;
            }
            else
            {
                Dpl_Text.Enabled = true;
                txtNote.Enabled = false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("List_AfterStudentRequest.aspx?id=" + generaterandomstr(11) + "@A" + Session[sessionNames.menuID].ToString() + "-" + generaterandomstr(2));
        }
        public string generaterandomstr(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}