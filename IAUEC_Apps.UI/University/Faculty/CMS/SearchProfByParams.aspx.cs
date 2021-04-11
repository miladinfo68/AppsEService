using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using System.Data;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class SearchProfByParams : System.Web.UI.Page
    {
        CommonBusiness CB = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddl_Cooperation.Items.Add(new ListItem("تمام وقت 32 ساعت", "1"));
                ddl_Cooperation.Items.Add(new ListItem("نیمه وقت", "2"));
                ddl_Cooperation.Items.Add(new ListItem("ساعتی-حق التدریس", "3"));
                ddl_Cooperation.Items.Add(new ListItem("تمام وقت طرح مشمولان", "4"));
                ddl_Cooperation.Items.Add(new ListItem("بورسیه دکترا", "5"));
                ddl_Cooperation.Items.Add(new ListItem("کارمند", "6"));
                ddl_Cooperation.Items.Add(new ListItem("تمام وقت 44 ساعت", "7"));
                ddl_Cooperation.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Cooperation.Items[ddl_Cooperation.Items.Count - 1].Selected = true;

                if (Session["page"].ToString() == "2")
                {
                    if ((!string.IsNullOrWhiteSpace(Request.QueryString["Term"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Lesson"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Field"].ToString())))
                    {
                        lbl_Term.Text = Request.QueryString["Term"].ToString();
                        lbl_Lesson.Text = Request.QueryString["Lesson"].ToString();
                        lbl_Departman.Text = Request.QueryString["Departman"].ToString();
                    }
                }
                if (Session["page"].ToString() == "1")
                {
                    if ((!string.IsNullOrWhiteSpace(Request.QueryString["Term"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Departman"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Cooperation"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Daneshkade"].ToString())))
                    {
                        lbl_Term.Text = Request.QueryString["Term"].ToString();
                        lbl_Cooperation.Text = Request.QueryString["Cooperation"].ToString();
                        lbl_Departman.Text = Request.QueryString["Departman"].ToString();
                        lbl_Daneshkade.Text = Request.QueryString["Daneshkade"].ToString();
                    }
                }
                if (Session["page"].ToString() == "3")
                {
                    if ((!string.IsNullOrWhiteSpace(Request.QueryString["Term"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Day"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["NumberClass"].ToString())))
                    {
                        lbl_Term.Text = Request.QueryString["Term"].ToString();
                        lbl_NumberClass.Text = Request.QueryString["NumberClass"].ToString();
                        lbl_Lesson.Text = Request.QueryString["Day"].ToString();
                    }
                }
                if (Session["page"].ToString() == "5")
                {
                    if ((!string.IsNullOrWhiteSpace(Request.QueryString["Term"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Departman"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Daneshkade"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["AzJ"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["TaJ"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["AzD"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["TaD"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["NumberAbsence"].ToString())))
                    {
                        lbl_Term.Text = Request.QueryString["Term"].ToString();
                        lbl_Departman.Text = Request.QueryString["Departman"].ToString();
                        lbl_Daneshkade.Text = Request.QueryString["Daneshkade"].ToString();
                        lbl_AzJ.Text = Request.QueryString["AzJ"].ToString();
                        lbl_TaJ.Text = Request.QueryString["TaJ"].ToString();
                        lbl_AzD.Text = Request.QueryString["AzD"].ToString();
                        lbl_TaD.Text = Request.QueryString["TaD"].ToString();
                        lbl_NumberAbsence.Text = Request.QueryString["NumberAbsence"].ToString();
                    }
                }
                if (Session["page"].ToString() == "4")
                {
                    if ((!string.IsNullOrWhiteSpace(Request.QueryString["Term"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["FromDate"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["ToDate"].ToString())))
                    {
                        lbl_Term.Text = Request.QueryString["Term"].ToString();
                        lbl_FromDate.Text = Request.QueryString["FromDate"].ToString();
                        lbl_ToDate.Text = Request.QueryString["ToDate"].ToString();
                    }
                }
                if (Session["page"].ToString() == "6")
                {
                    if ((!string.IsNullOrWhiteSpace(Request.QueryString["Term"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Departman"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Cooperation"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Daneshkade"].ToString())))
                    {
                        lbl_Term.Text = Request.QueryString["Term"].ToString();
                        lbl_Cooperation.Text = Request.QueryString["Cooperation"].ToString();
                        lbl_Departman.Text = Request.QueryString["Departman"].ToString();
                        lbl_Daneshkade.Text = Request.QueryString["Daneshkade"].ToString();
                    }
                }
                if (Session["page"].ToString() == "7")
                {
                    if ((!string.IsNullOrWhiteSpace(Request.QueryString["Term"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Departman"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Cooperation"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Daneshkade"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Field"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Zarib"].ToString())))
                    {
                        lbl_Term.Text = Request.QueryString["Term"].ToString();
                        lbl_Cooperation.Text = Request.QueryString["Cooperation"].ToString();
                        lbl_Departman.Text = Request.QueryString["Departman"].ToString();
                        lbl_Daneshkade.Text = Request.QueryString["Daneshkade"].ToString();
                        lbl_Field.Text = Request.QueryString["Field"].ToString();
                        lbl_Zarib.Text = Request.QueryString["Zarib"].ToString();
                    }
                }
                if (Session["page"].ToString() == "8")
                {
                    if ((!string.IsNullOrWhiteSpace(Request.QueryString["Term"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["did"].ToString())))
                    {
                        lbl_Term.Text = Request.QueryString["Term"].ToString();
                        lbl_did.Text = Request.QueryString["did"].ToString();
                    }
                }
            }

        }

        protected void grd_Faculty_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = CB.GetAllInformationFaculty();
            grd_Faculty.DataSource = dt;
        }

        protected void grd_faculty_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                    Session["code_ostad"] = e.CommandArgument;
                    ClientScript.RegisterStartupScript(Page.GetType(), "Select", "CloseAndRebind();", true);
            }
        }
        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        protected void ddl_Cooperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Coop = ddl_Cooperation.SelectedValue;
        }

        protected void btn_ProfCode_Click(object sender, EventArgs e)
        {
            if (txt_Family.Text == null || txt_Family.Text == string.Empty)
            {
                txt_Family.Text = "0";
            }
            if (txt_NameEp.Text == null || txt_NameEp.Text == string.Empty)
            {
                txt_NameEp.Text = "0";
            }
            if (txt_ProfCode.Text == null || txt_ProfCode.Text == string.Empty)
            {
                txt_ProfCode.Text = "0";
            }
           DataTable dtResault = CB.GetInformationFacultyByFilter(int.Parse(txt_ProfCode.Text), txt_Family.Text, txt_NameEp.Text, int.Parse(ddl_Cooperation.SelectedValue));
           if (dtResault.Rows.Count > 0)
           {
               grd_Faculty.DataSource = dtResault;
               grd_Faculty.DataBind();
           }
           else
           {
               rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
           }
           if (txt_Family.Text == "0")
           {
               txt_Family.Text = string.Empty;
           }
           if (txt_NameEp.Text == "0")
           {
               txt_NameEp.Text = string.Empty;
           }
           if (txt_ProfCode.Text == "0")
           {
               txt_ProfCode.Text = string.Empty;
           }
        }

    }
}
