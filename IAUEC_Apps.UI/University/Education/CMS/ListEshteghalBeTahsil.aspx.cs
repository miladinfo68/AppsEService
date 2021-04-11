using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.DTO.University.Education;

namespace IAUEC_Apps.UI.University.Education.CMS
{
    public partial class ListEshteghalBeTahsil : System.Web.UI.Page
    {
        public static ListEshteghalBeTahsilDTO LED = new ListEshteghalBeTahsilDTO();
        EducationReportBusiness ERB = new EducationReportBusiness();
        Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtTerm = new DataTable();
            if (!IsPostBack)
            {
                dtTerm = CB.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
            }
        }

        protected void btn_Show_Click(object sender, EventArgs e)
        {
            DataTable dtResault = new DataTable();
            string FromDate = txt_FromDate.Text;
            string ToDate = txt_ToDate.Text;
            dtResault = ERB.GetListEshteghalBeTahsil(FromDate, ToDate,LED.Term);
            //dtResault = ERB.GetListEshteghalBeTahsil(FromDate, ToDate, Session["Term"].ToString());
            grd_Student.DataSource = dtResault;
            grd_Student.DataBind();
        }


        //protected void btn_Exit_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("EducationReports.aspx");
        //}

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Session["Term"] = ddl_Term.SelectedValue;
            LED.Term = ddl_Term.SelectedValue;
        }

    }
}