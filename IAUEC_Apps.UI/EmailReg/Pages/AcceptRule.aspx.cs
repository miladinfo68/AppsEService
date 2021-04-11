using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using System.Data;

namespace IAUEC_Apps.UI.EmailReg.Pages
{
    public partial class AcceptRule : System.Web.UI.Page
    {
        AcceptRuleBusiness Accept = new AcceptRuleBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt = Accept.Rule(1);
                string rules="";
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    rules = rules + "</br>" + dt.Rows[i]["Text"].ToString() + "</br>";

                }

                lbl_ShowTextRule.Text = rules;
                //lbl_ShowTextRule.Text = dt.Rows[0]["Text"].ToString();
                //lbl_ShowTextRule1.Text = dt.Rows[1]["Text"].ToString();
                //lbl_ShowTextRule2.Text = dt.Rows[2]["Text"].ToString();
                //lbl_ShowTextRule3.Text = dt.Rows[3]["Text"].ToString();
            }
        }

        protected void btn_AcceptRule_click(object sender, EventArgs e)
        {
            if (chk_AcceptRule.Checked == true)
                Response.Redirect("CreateEmail.aspx");
            else
                rwm.RadAlert("لطفا با انتخاب چک لیست موارد فوق را تایید نمایید", 0, 100, "خطا" ,"");
         
        }
    }
}