using IAUEC_Apps.Business.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.University.Students;
using IAUEC_Apps.Business.university.Students;
using IAUEC_Apps.Business.university.Education;
using Telerik.Web.UI;
using System.IO;

namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class ListStudentsBasedOnIGRD : System.Web.UI.Page
    {
        public static ListStudentBasedOnIGRDDTO LSBI = new ListStudentBasedOnIGRDDTO();
        //CommonDAO dao = new CommonDAO();
        DataTable dtTerm = new DataTable();
        DataTable dtDepartman = new DataTable();
        DataTable dtDaneshkade = new DataTable();
        DataTable dtCooperation = new DataTable();
        DataTable dtVaziatNomre = new DataTable();
        DataTable dtField = new DataTable();
        CommonBusiness CB = new CommonBusiness();
        ListStudentsBasedOnIGRDBusiness LSBIB = new ListStudentsBasedOnIGRDBusiness();
        EducationReportBusiness ERB = new EducationReportBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            StiWebViewer1.Visible = false;
            if (!IsPostBack)
            {
                dtTerm = CB.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
                if (Request.QueryString["Term"] != null)
                {
                    ddl_Term.SelectedValue = Request.QueryString["Term"].ToString();
                }
                dtDaneshkade = CB.SelectAllDaneshkade();
                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = dtDaneshkade;
                ddl_Daneshkade.DataBind();
                ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;
                if (Request.QueryString["Daneshkade"] != null)
                {
                    ddl_Daneshkade.SelectedValue = Request.QueryString["Daneshkade"].ToString();
                }
                dtField = CB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                if (Request.QueryString["Field"] != null)
                {
                    ddl_Field.SelectedValue = Request.QueryString["Field"].ToString();
                }
                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "7"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;
                if (Request.QueryString["Degree"] != null)
                {
                    ddl_Degree.SelectedValue = Request.QueryString["Degree"].ToString();
                }
                ddl_Dorpar.Items.Add(new ListItem("دوره ای", "1"));
                ddl_Dorpar.Items.Add(new ListItem("پاره وقت", "2"));
                ddl_Dorpar.Items.Add(new ListItem("طرح معلمان", "3"));
                ddl_Dorpar.Items.Add(new ListItem("قراردادی", "4"));
                ddl_Dorpar.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Dorpar.Items[ddl_Dorpar.Items.Count - 1].Selected = true;
                if (Request.QueryString["Education"] != null)
                {
                    ddl_Dorpar.SelectedValue = Request.QueryString["Education"].ToString();
                }
                ddl_Sex.Items.Add(new ListItem("مرد", "1"));
                ddl_Sex.Items.Add(new ListItem("زن", "2"));
                ddl_Sex.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Sex.Items[ddl_Sex.Items.Count - 1].Selected = true;
                if (Request.QueryString["Sex"] != null)
                {
                    ddl_Sex.SelectedValue = Request.QueryString["Sex"].ToString();
                }
                ddl_NimsalVorod.Items.Add(new ListItem("مهر", "1"));
                ddl_NimsalVorod.Items.Add(new ListItem("بهمن", "2"));
                ddl_NimsalVorod.Items.Add(new ListItem("تابستان", "3"));
                ddl_NimsalVorod.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_NimsalVorod.Items[ddl_NimsalVorod.Items.Count - 1].Selected = true;
                if (Request.QueryString["NimsalVorod"] != null)
                {
                    ddl_NimsalVorod.SelectedValue = Request.QueryString["NimsalVorod"].ToString();
                }
                ddl_StatusStu.Items.Add(new ListItem("نامعلوم"  , "0"));
                ddl_StatusStu.Items.Add(new ListItem("عادی", "1"));
                ddl_StatusStu.Items.Add(new ListItem("مهمان از", "2"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف تغییر رشته", "3"));
                ddl_StatusStu.Items.Add(new ListItem("انتقال از", "4"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف با اطلاع", "5"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف ماده 51", "6"));
                ddl_StatusStu.Items.Add(new ListItem("فارغ التحصیل", "7"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج آموزشی", "8"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج انضباطی", "9"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج از واحد های تهران", "10"));
                ddl_StatusStu.Items.Add(new ListItem("محروم", "11"));
                ddl_StatusStu.Items.Add(new ListItem("فوت", "12"));
                ddl_StatusStu.Items.Add(new ListItem("شهید", "13"));
                ddl_StatusStu.Items.Add(new ListItem("مهمان سازمان", "14"));
                ddl_StatusStu.Items.Add(new ListItem("عدم مراجعه", "15"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج از کل واحدها", "16"));
                ddl_StatusStu.Items.Add(new ListItem("تسویه حساب مدرک معادل", "17"));
                ddl_StatusStu.Items.Add(new ListItem("انتخاب کنید", "18"));
                ddl_StatusStu.Items[ddl_StatusStu.Items.Count - 1].Selected = true;
                if (Request.QueryString["StatusStu"] != null)
                {
                    ddl_StatusStu.SelectedValue = Request.QueryString["StatusStu"].ToString();
                }
                ddl_AcceptedStu.Items.Add(new ListItem("آزمون سراسری", "1"));
                ddl_AcceptedStu.Items.Add(new ListItem("آزمون پاره وقت", "2"));
                ddl_AcceptedStu.Items.Add(new ListItem("آزمون قراردادی", "3"));
                ddl_AcceptedStu.Items.Add(new ListItem("انتقالی از - آزمون", "4"));
                ddl_AcceptedStu.Items.Add(new ListItem("انتقالی از - سازمان", "5"));
                ddl_AcceptedStu.Items.Add(new ListItem("انتقالی از دانشگاه دولتی", "6"));
                ddl_AcceptedStu.Items.Add(new ListItem("انتقالی غیرانتفاعی", "7"));
                ddl_AcceptedStu.Items.Add(new ListItem("مأمور به تحصیل", "8"));
                ddl_AcceptedStu.Items.Add(new ListItem("آزمون معلمان", "9"));
                ddl_AcceptedStu.Items.Add(new ListItem("آزمون ارشد ناپیوسته", "10"));
                ddl_AcceptedStu.Items.Add(new ListItem("آزمون دکتری تخصصی", "11"));
                ddl_AcceptedStu.Items.Add(new ListItem("آزمون کارشناسی ناپیوسته", "12"));
                ddl_AcceptedStu.Items.Add(new ListItem("آزمون کاردانی پیوسته", "13"));
                ddl_AcceptedStu.Items.Add(new ListItem("آزمون خاص", "14"));
                ddl_AcceptedStu.Items.Add(new ListItem("آزمون رسمی", "15"));
                ddl_AcceptedStu.Items.Add(new ListItem("بدون آزمون", "16"));
                ddl_AcceptedStu.Items.Add(new ListItem("تکمیل ظرفیت", "17"));
                ddl_AcceptedStu.Items.Add(new ListItem("بدون آزمون - سهمیه قهرمانان", "18"));
                ddl_AcceptedStu.Items.Add(new ListItem("تک درس", "19"));
                ddl_AcceptedStu.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_AcceptedStu.Items[ddl_AcceptedStu.Items.Count - 1].Selected = true;
                if (Request.QueryString["TypeAccepted"] != null)
                {
                    ddl_AcceptedStu.SelectedValue = Request.QueryString["TypeAccepted"].ToString();
                }
                ddl_Isargar.Items.Add(new ListItem("شهید", "1"));
                ddl_Isargar.Items.Add(new ListItem("جانباز", "2"));
                ddl_Isargar.Items.Add(new ListItem("آزاده", "3"));
                ddl_Isargar.Items.Add(new ListItem("جانباز-آزاده", "4"));
                ddl_Isargar.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Isargar.Items[ddl_Isargar.Items.Count - 1].Selected = true;
                if (Request.QueryString["Isargar"] != null)
                {
                    ddl_Isargar.SelectedValue = Request.QueryString["Isargar"].ToString();
                }
                ddl_VaziiatTerm.Items.Add(new ListItem("عادی", "1"));
                ddl_VaziiatTerm.Items.Add(new ListItem("مرخصی با احتساب", "2"));
                ddl_VaziiatTerm.Items.Add(new ListItem("محروم", "3"));
                ddl_VaziiatTerm.Items.Add(new ListItem("میهمان به", "4"));
                ddl_VaziiatTerm.Items.Add(new ListItem("حذف ترم", "5"));
                ddl_VaziiatTerm.Items.Add(new ListItem("انتقال به", "6"));
                ddl_VaziiatTerm.Items.Add(new ListItem("انصراف", "7"));
                ddl_VaziiatTerm.Items.Add(new ListItem("فارغ التحصیلان", "8"));
                ddl_VaziiatTerm.Items.Add(new ListItem("اخراج", "9"));
                ddl_VaziiatTerm.Items.Add(new ListItem("ارجاع به استاد", "10"));
                ddl_VaziiatTerm.Items.Add(new ListItem("مجوز ثبت نام", "11"));
                ddl_VaziiatTerm.Items.Add(new ListItem("ارجاع به فارغ التحصیلان", "12"));
                ddl_VaziiatTerm.Items.Add(new ListItem("مرخصی بدون احتساب", "13"));
                ddl_VaziiatTerm.Items.Add(new ListItem("انتقال از", "14"));
                ddl_VaziiatTerm.Items.Add(new ListItem("حذف ترم ماده 38 ت 2", "15"));
                ddl_VaziiatTerm.Items.Add(new ListItem("محروم بدون احتساب", "16"));
                ddl_VaziiatTerm.Items.Add(new ListItem("حذف ترم ماده 38", "17"));
                ddl_VaziiatTerm.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_VaziiatTerm.Items[ddl_VaziiatTerm.Items.Count - 1].Selected = true;
                dtVaziatNomre = LSBIB.GetVaznom();
                ddl_VaziiatNomre.DataTextField = "VaznomName";
                ddl_VaziiatNomre.DataValueField = "ID";
                ddl_VaziiatNomre.DataSource = dtVaziatNomre;
                ddl_VaziiatNomre.DataBind();
                ddl_VaziiatNomre.Items.Add(new ListItem("انتخاب کنید", "58"));
                ddl_VaziiatNomre.Items[ddl_VaziiatNomre.Items.Count - 1].Selected = true;
            }
            if (Session["stcode"] != null)
            {
                txt_stCode.Text = Session["stcode"].ToString();
            }
            LSBI.Salvorod = txt_SalVorod.Text;
        }

        protected void btn_stCode_Click(object sender, EventArgs e)
        {

            if (ddl_Daneshkade.SelectedValue == null)
            {
                ddl_Daneshkade.SelectedValue = "0";
            }
            if (ddl_Degree.SelectedValue.ToString() == null)
            {
                ddl_Degree.SelectedValue = "0";
            }
            if (ddl_Dorpar.SelectedValue == null)
            {
                ddl_Dorpar.SelectedValue = "0";
            }
            if (ddl_Field.SelectedValue == null)
            {
                ddl_Field.SelectedValue = "0";
            }
            if (ddl_Isargar.SelectedValue == null)
            {
                ddl_Isargar.SelectedValue = "0";
            }
            if (ddl_NimsalVorod.SelectedValue == null)
            {
                ddl_NimsalVorod.SelectedValue = "0";
            }
            if (txt_SalVorod.Text == null || txt_SalVorod.Text == string.Empty)
            {
                txt_SalVorod.Text = "0";
            }
            if (ddl_Sex.SelectedValue == null)
            {
                ddl_Sex.SelectedValue = "0";
            }
            if (ddl_StatusStu.SelectedValue == null)
            {
                ddl_StatusStu.SelectedValue = "0";
            }
            if (ddl_Term.SelectedValue == null)
            {
                ddl_Term.SelectedValue = "0";
            }
            if (ddl_VaziiatNomre.SelectedValue == null)
            {
                ddl_VaziiatNomre.SelectedValue = "0";
            }
            if (ddl_VaziiatTerm.SelectedValue == null)
            {
                ddl_VaziiatTerm.SelectedValue = "0";
            }
            if (ddl_AcceptedStu.SelectedValue == null)
            {
                ddl_AcceptedStu.SelectedValue = "0";
            }
            if (txt_stCode.Text == null || txt_stCode.Text == "")
            {
                txt_stCode.Text = "0";
            }

            Session["Page"] = 1;
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties  
            widnow1.NavigateUrl = ("../../../university/Students/CMS/SearchOfStudent.aspx?" + "Daneshkade" + "=" + ddl_Daneshkade.SelectedValue + "&" + "Field" + "=" + ddl_Field.SelectedValue + "&" + "Degree" + "=" + ddl_Degree.SelectedValue + "&" + "Education" + "=" + ddl_Dorpar.SelectedValue + "&" + "Sex" + "=" + ddl_Sex.SelectedValue + "&" + "SalVorod" + "=" + txt_SalVorod.Text + "&" + "NimsalVorod" + "=" + ddl_NimsalVorod.SelectedValue + "&" + "TypeAccepted" + "=" + ddl_AcceptedStu.SelectedValue + "&" + "StatusStu" + "=" + ddl_StatusStu.SelectedValue + "&" + "Isargar" + "=" + ddl_Isargar.SelectedValue + "&" + "Term" + "=" + ddl_Term.SelectedValue);
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code 
            widnow1.Height = 600;
            widnow1.Width = 1200;
            windowManager.Windows.Add(widnow1);
            windowManager.Height = 600;
            windowManager.Width = 1200;
            ContentPlaceHolder mpContentPlaceHolder;
            mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            mpContentPlaceHolder.Controls.Add(widnow1);        
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Daneshkade.SelectedValue == "0")
            {
                LSBI.Daneshkade = int.Parse(ddl_Daneshkade.SelectedValue.ToString());
                dtField = CB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
            }
            else
            {
                LSBI.Daneshkade = int.Parse(ddl_Daneshkade.SelectedValue.ToString());
                DataTable dtField = new DataTable();
                dtField = ERB.GetReshByDaneshkade(int.Parse(LSBI.Daneshkade.ToString()));
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
            }
        }

        protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.Field = int.Parse(ddl_Field.SelectedValue.ToString());
        }

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.Degree = int.Parse(ddl_Degree.SelectedValue.ToString());
        }

        protected void ddl_Dorpar_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.Dorpar = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        }

        protected void ddl_Sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.Sex = int.Parse(ddl_Sex.SelectedValue.ToString());
        }

        protected void ddl_NimsalVorod_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.NimsalVorod = int.Parse(ddl_NimsalVorod.SelectedValue.ToString());
        }

        protected void ddl_AcceptedStu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.TypeAccepted = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
        }

        protected void ddl_StatusStu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.StatusStu = int.Parse(ddl_StatusStu.SelectedValue.ToString());
        }

        protected void ddl_Isargar_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.Isargar = int.Parse(ddl_Isargar.SelectedValue);
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.Term = ddl_Term.SelectedValue.ToString();
        }

        protected void ddl_VaziiatNomre_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.VaziatNomre = int.Parse(ddl_VaziiatNomre.SelectedValue.ToString());
        }
        protected void ddl_VaziiatTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            LSBI.VaziatTerm = int.Parse(ddl_VaziiatTerm.SelectedValue.ToString());
        }

        protected void btn_Show_Click(object sender, EventArgs e)
        {
        //    img_ExportToExcel1.Visible = false;
        //    img_ExportToExcel2.Visible = false;
        //    img_ExportToExcel3.Visible = false;
        //    img_ExportToExcel4.Visible = false;
        //    img_ExportToExcel5.Visible = false;
        //    img_ExportToExcel6.Visible = false;
        //    img_ExportToExcel7.Visible = false;
        //    img_ExportToExcel8.Visible = false;
        //    img_ExportToExcel9.Visible = false;
        //    img_ExportToExcel10.Visible = false;
        //    if (ddl_Term.SelectedValue == "0" || ddl_Term.SelectedValue == null)
        //    {
        //        rwm.RadAlert("لطفا ترم را انتخاب کنید", 0, 100, "پیام", "");
        //    }
        //    else
        //    {
        //        if (ddl_Daneshkade.SelectedValue == null)
        //        {
        //            ddl_Daneshkade.SelectedValue = "0";
        //        }
        //        if (ddl_Degree.SelectedValue.ToString() == null)
        //        {
        //            ddl_Degree.SelectedValue = "0";
        //        }
        //        if (ddl_Dorpar.SelectedValue == null)
        //        {
        //            ddl_Dorpar.SelectedValue = "0";
        //        }
        //        if (ddl_Field.SelectedValue == null)
        //        {
        //            ddl_Field.SelectedValue = "0";
        //        }
        //        if (ddl_Isargar.SelectedValue == null)
        //        {
        //            ddl_Isargar.SelectedValue = "0";
        //        }
        //        if (ddl_NimsalVorod.SelectedValue == null)
        //        {
        //            ddl_NimsalVorod.SelectedValue = "0";
        //        }
        //        if (txt_SalVorod.Text == null || txt_SalVorod.Text == string.Empty)
        //        {
        //            txt_SalVorod.Text = "0";
        //        }
        //        if (ddl_Sex.SelectedValue == null)
        //        {
        //            ddl_Sex.SelectedValue = "0";
        //        }
        //        if (ddl_StatusStu.SelectedValue == null)
        //        {
        //            ddl_StatusStu.SelectedValue = "0";
        //        }
        //        if (ddl_Term.SelectedValue == null)
        //        {
        //            ddl_Term.SelectedValue = "0";
        //        }
        //        if (ddl_VaziiatNomre.SelectedValue == null)
        //        {
        //            ddl_VaziiatNomre.SelectedValue = "0";
        //        }
        //        if (ddl_VaziiatTerm.SelectedValue == null)
        //        {
        //            ddl_VaziiatTerm.SelectedValue = "0";
        //        }
        //        if (ddl_AcceptedStu.SelectedValue == null)
        //        {
        //            ddl_AcceptedStu.SelectedValue = "0";
        //        }
        //        if (txt_stCode.Text == null || txt_stCode.Text == "")
        //        {
        //            txt_stCode.Text = "0";
        //        }

        //        // میهمانی
        //        if (rdb_Mehmani.Checked == true)
        //        {
        //            //img_ExportToExcel1.Visible = false;
        //            //img_ExportToExcel2.Visible = false;
        //            //img_ExportToExcel3.Visible = false;
        //            //img_ExportToExcel4.Visible = false;
        //            //img_ExportToExcel5.Visible = false;
        //            //img_ExportToExcel6.Visible = false;
        //            //img_ExportToExcel7.Visible = false;
        //            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
        //            {
        //                rwm.RadAlert("لطفا ترم را انتخاب نمایید", 0, 100, "پیام", "");
        //            }

        //            else
        //            {
        //                int IS;
        //                if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //                {
        //                    //بدون وضعیت ایثارگری
        //                    IS = 1;
        //                    if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 0;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 1;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 2;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 3;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 4;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 5;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 6;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 7;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 8;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 9;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 10;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 11;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 12;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 13;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 14;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 15;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 16;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 17;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 18;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 19;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 20;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 21;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 22;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 23;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 24;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 25;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 26;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 27;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 28;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 29;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 30;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 31;
        //                    }
        //                }
        //                else
        //                {
        //                    // با وضعیت ایثارگری
        //                    IS = 2;

        //                    if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 0;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 1;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 2;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 3;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 4;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 5;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 6;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 7;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 8;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 9;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 10;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 11;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 12;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 13;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 14;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 15;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 16;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 17;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 18;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 19;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 20;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 21;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 22;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 23;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 24;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 25;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 26;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 27;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 28;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 29;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 30;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 31;
        //                    }
        //                }

        //                DataTable dtResaultGuest = LSBIB.GetReportGuestStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), 1, ddl_Term.SelectedValue);
        //                if (dtResaultGuest.Rows.Count == 0)
        //                {
        //                    rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //                }
        //                else
        //                {
        //                    img_ExportToExcel8.Visible = true;
        //                    this.StiWebViewer1.ResetReport();
        //                    StiWebViewer1.Visible = true;
        //                    StiReport rpt = new StiReport();
        //                    rpt.Load(Server.MapPath("../Report/ReportGuestStudents.mrt"));
        //                    rpt.ReportCacheMode = StiReportCacheMode.On;
        //                    rpt.Dictionary.Databases.Clear();
        //                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //                    rpt.Compile();
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@StCode"].ParameterValue = txt_stCode.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@order"].ParameterValue = int.Parse(Session["order"].ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Isargar"].ParameterValue = int.Parse(ddl_Isargar.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@IS"].ParameterValue = 1;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue.ToString();
        //                    rpt.RegData(dtResaultGuest);

        //                    //rpt.Dictionary.Synchronize();
        //                    //rpt.Show();
        //                    StiWebViewer1.Report = rpt;
        //                    StiWebViewer1.Visible = true;
        //                    //rpt.Print(true);
        //                }
        //            }
        //            if (Session["order"] != null)
        //            {
        //                Session["order"] = null;
        //            }
        //        }


        //      // عدم مراجعه ثبت نام
        //        else if (rdb_AdamMoraje.Checked == true)
        //        {
        //            //img_ExportToExcel1.Visible = false;
        //            //img_ExportToExcel2.Visible = false;
        //            //img_ExportToExcel3.Visible = false;
        //            //img_ExportToExcel4.Visible = false;
        //            //img_ExportToExcel5.Visible = false;
        //            //img_ExportToExcel6.Visible = false;
        //            //img_ExportToExcel7.Visible = false;
        //            if (ddl_VaziiatTerm.SelectedValue == null)
        //            {
        //                rwm.RadAlert("لطفا وضعیت ترم را انتخاب کنید", 0, 100, "پیام", "");
        //            }
        //            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == string.Empty)
        //            {
        //                rwm.RadAlert("لطفا ترم را انتخاب کنید", 0, 100, "پیام", "");
        //            }

        //            else
        //            {
        //                int IS;

        //                if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //                {
        //                    //بدون وضعیت ایثارگری
        //                    IS = 1;
        //                    Session["order"] = 1;
        //                }
        //                else
        //                {
        //                    IS = 2;
        //                    Session["order"] = 2;
        //                }
        //                if (txt_Term.Text == null | txt_Term.Text == string.Empty)
        //                {
        //                    rwm.RadAlert("لطفا ترم را تعیین کنید", 0, 100, "پیام", "");
        //                }
        //                else
        //                {
        //                    DataTable dtLackOfStudents = LSBIB.GetReportLackOfStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, txt_Term.Text, int.Parse(ddl_StatusStu.SelectedValue) , ddl_Term.SelectedValue);
        //                    if (dtLackOfStudents.Rows.Count == 0)
        //                    {
        //                        rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //                    }
        //                    else
        //                    {
        //                        //Report ..........  
        //                        this.StiWebViewer1.ResetReport();
        //                        img_ExportToExcel3.Visible = true;
        //                        StiWebViewer1.Visible = true;
        //                        StiReport rpt = new StiReport();
        //                        rpt.Load(Server.MapPath("../Report/ReportLackOfStudents.mrt"));
        //                        rpt.ReportCacheMode = StiReportCacheMode.On;
        //                        rpt.Dictionary.Databases.Clear();
        //                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //                        rpt.Compile();
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@StCode"].ParameterValue = txt_stCode.Text;
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@order"].ParameterValue = int.Parse(Session["order"].ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Isargar"].ParameterValue = int.Parse(ddl_Isargar.SelectedValue);
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@IS"].ParameterValue = 1;
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Term"].ParameterValue = txt_Term.Text;
        //                        rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@TermAll"].ParameterValue = ddl_Term.SelectedValue.ToString();
        //                        rpt.RegData(dtLackOfStudents);
        //                        //rpt.Dictionary.Synchronize();
        //                        //rpt.Show();
        //                        StiWebViewer1.Report = rpt;
        //                        StiWebViewer1.Visible = true;
        //                        //rpt.Print(true);
        //                    }
        //                }
        //            }

        //            if (Session["order"] != null)
        //            {
        //                Session["order"] = null;
        //            }
        //        }

        //        // آدرس و تلفن دانشجو
        //        else if (rdb_AddressTel.Checked == true)
        //        {
        //            //img_ExportToExcel1.Visible = false;
        //            //img_ExportToExcel2.Visible = false;
        //            //img_ExportToExcel3.Visible = false;
        //            //img_ExportToExcel4.Visible = false;
        //            //img_ExportToExcel5.Visible = false;
        //            //img_ExportToExcel6.Visible = false;
        //            //img_ExportToExcel7.Visible = false;
        //            int IS;

        //            if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //            {
        //                //بدون وضعیت ایثارگری
        //                IS = 1;
        //                if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 0;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 1;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 2;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 3;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 4;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 5;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 6;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 7;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 8;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 9;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 10;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 11;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 12;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 13;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 14;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 15;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 16;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 17;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 18;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 19;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 20;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 21;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 22;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 23;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 24;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 25;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 26;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 27;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 28;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 29;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 30;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 31;
        //                }
        //            }
        //            else
        //            {
        //                // با وضعیت ایثارگری
        //                IS = 2;
        //                if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 0;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 1;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 2;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 3;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 4;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 5;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 6;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 7;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 8;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 9;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 10;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 11;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 12;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 13;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 14;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 15;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 16;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 17;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 18;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 19;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 20;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 21;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 22;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 23;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 24;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 25;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 26;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 27;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 28;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 29;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 30;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 31;
        //                }
        //            }
        //            if (ddl_Daneshkade.SelectedValue == null || ddl_Daneshkade.SelectedValue == "0")
        //            {
        //                rwm.RadAlert("لطفا دانشکده را انتخاب کنید", 0, 100, "پیام", "");
        //            }
        //            else
        //            {
        //                DataTable dtResaultAddressTelStudent = LSBIB.GetReportAddressTelStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, ddl_Term.SelectedValue);
        //                if (dtResaultAddressTelStudent.Rows.Count == 0)
        //                {
        //                    rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //                }
        //                else
        //                {
        //                    //Report..............
        //                    img_ExportToExcel10.Visible = true;
        //                    this.StiWebViewer1.ResetReport();
        //                    StiWebViewer1.Visible = true;
        //                    StiReport rpt = new StiReport();
        //                    rpt.Load(Server.MapPath("../Report/ReportTelStudents.mrt"));
        //                    rpt.ReportCacheMode = StiReportCacheMode.On;
        //                    rpt.Dictionary.Databases.Clear();
        //                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //                    rpt.Compile();
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@StCode"].ParameterValue = txt_stCode.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@order"].ParameterValue = int.Parse(Session["order"].ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@Isargar"].ParameterValue = int.Parse(ddl_Isargar.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@IS"].ParameterValue = IS;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_AddressTelStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue.ToString();
        //                    rpt.RegData(dtResaultAddressTelStudent);
        //                    StiWebViewer1.Report = rpt;
        //                    StiWebViewer1.Visible = true;
        //                    //rpt.Print(true);
        //                }
        //                if (Session["order"] != null)
        //                {
        //                    Session["order"] = null;
        //                }
        //            }
        //        }



        //        // انتقالی
        //        else if (rdb_Enteghali.Checked == true)
        //        {
        //            //img_ExportToExcel1.Visible = false;
        //            //img_ExportToExcel2.Visible = false;
        //            //img_ExportToExcel3.Visible = false;
        //            //img_ExportToExcel4.Visible = false;
        //            //img_ExportToExcel5.Visible = false;
        //            //img_ExportToExcel6.Visible = false;
        //            //img_ExportToExcel7.Visible = false;
        //            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
        //            {
        //                rwm.RadAlert("لطفا ترم را انتخاب نمایید", 0, 100, "پیام", "");
        //            }
        //            else
        //            {
        //                int IS;
        //                if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //                {

        //                    //بدون وضعیت ایثارگری
        //                    IS = 1;
        //                    if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 0;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 1;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 2;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 3;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 4;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 5;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 6;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 7;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 8;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 9;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 10;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 11;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 12;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 13;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 14;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 15;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 16;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 17;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 18;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 19;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 20;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 21;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 22;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 23;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 24;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 25;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 26;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 27;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 28;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 29;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 30;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 31;
        //                    }
        //                }
        //                else
        //                {
        //                    // با وضعیت ایثارگری
        //                    IS = 2;
        //                    if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 0;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 1;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 2;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 3;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 4;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 5;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 6;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 7;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 8;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 9;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 10;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 11;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 12;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 13;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 14;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 15;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 16;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 17;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 18;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 19;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 20;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 21;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 22;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 23;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 24;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 25;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 26;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 27;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 28;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 29;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 30;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 31;
        //                    }
        //                }

        //                DataTable dtResaultTransfer = LSBIB.GetReportTransferStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, ddl_Term.SelectedValue);
        //                if (dtResaultTransfer.Rows.Count == 0)
        //                {
        //                    rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //                }
        //                else
        //                {
        //                    //Report..............
        //                    this.StiWebViewer1.ResetReport();
        //                    img_ExportToExcel9.Visible = true;
        //                    StiWebViewer1.Visible = true;
        //                    StiReport rpt = new StiReport();
        //                    rpt.Load(Server.MapPath("../Report/ReportTransferStudents.mrt"));
        //                    rpt.ReportCacheMode = StiReportCacheMode.On;
        //                    rpt.Dictionary.Databases.Clear();
        //                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //                    rpt.Compile();
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@StCode"].ParameterValue = txt_stCode.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@order"].ParameterValue = int.Parse(Session["order"].ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Isargar"].ParameterValue = int.Parse(ddl_Isargar.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@IS"].ParameterValue = 1;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue.ToString();
        //                    rpt.RegData(dtResaultTransfer);
        //                    StiWebViewer1.Report = rpt;
        //                    StiWebViewer1.Visible = true;
        //                }
        //            }

        //            if (Session["order"] != null || Session["order"] != string.Empty)
        //            {
        //                Session["order"] = null;
        //            }
        //        }

        //            // نقص پرونده داشته باشد
        //        else if (rdb_NaghsParvande.Checked == true)
        //        {
        //            //img_ExportToExcel1.Visible = false;
        //            //img_ExportToExcel2.Visible = false;
        //            //img_ExportToExcel3.Visible = false;
        //            //img_ExportToExcel4.Visible = false;
        //            //img_ExportToExcel5.Visible = false;
        //            //img_ExportToExcel6.Visible = false;
        //            //img_ExportToExcel7.Visible = false;
        //            int Isargar;
        //            int IS;
        //            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
        //            {
        //                Isargar = 1;
        //                IS = 1;
        //            }
        //            else
        //            {
        //                Isargar = 2;
        //                IS = 2;
        //            }
        //            DataTable dtIncompleteStudents = LSBIB.GetIncompleteStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), Isargar, int.Parse(ddl_AcceptedStu.SelectedValue), IS, int.Parse(ddl_StatusStu.SelectedValue));
        //            if (dtIncompleteStudents.Rows.Count == 0)
        //            {
        //                rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");

        //            }
        //            else
        //            {
        //                //Report ............................
        //                img_ExportToExcel2.Visible = true;
        //                this.StiWebViewer1.ResetReport();
        //                StiWebViewer1.Visible = true;
        //                StiReport rpt = new StiReport();
        //                rpt.Load(Server.MapPath("../Report/ReportIncompleteStudents.mrt"));
        //                rpt.ReportCacheMode = StiReportCacheMode.On;
        //                rpt.Dictionary.Databases.Clear();
        //                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //                rpt.Compile();
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@StCode"].ParameterValue = txt_stCode.Text;
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Isargar"].ParameterValue = Isargar;
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@IS"].ParameterValue = IS;
        //                rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
        //                rpt.RegData(dtIncompleteStudents);
        //                //rpt.Dictionary.Synchronize();
        //                //rpt.Show();
        //                StiWebViewer1.Report = rpt;
        //                StiWebViewer1.Visible = true;
        //                //rpt.Print(true);

        //            }

        //        }

        //            // در آوردن وضعیت نمره
        //        else if (rdb_VaziiatNomre.Checked == true)
        //        {
        //            //img_ExportToExcel1.Visible = false;
        //            //img_ExportToExcel2.Visible = false;
        //            //img_ExportToExcel3.Visible = false;
        //            //img_ExportToExcel4.Visible = false;
        //            //img_ExportToExcel5.Visible = false;
        //            //img_ExportToExcel6.Visible = false;
        //            //img_ExportToExcel7.Visible = false;
        //            int IS;
        //            if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //            {
        //                IS = 1;
        //            }
        //            else
        //            {
        //                IS = 2;
        //            }
        //            if (ddl_Term.SelectedValue == "0")
        //            {
        //                rwm.RadAlert("لطفا ترم را انتخاب نمایید", 0, 100, "پیام", "");
        //            }
        //            if (ddl_Daneshkade.SelectedValue== "0")
        //            {
        //                rwm.RadAlert("لطفا دانشکده را انتخاب نمایید", 0, 100, "پیام", "");
        //            }
        //            else
        //            {
        //                DataTable dt = LSBIB.GetNameVaznom(int.Parse(ddl_VaziiatNomre.SelectedValue.ToString()));
        //                DataTable dtVaziiatNomre = LSBIB.GetStatusScoreStudent(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_VaziiatNomre.SelectedValue), ddl_Term.SelectedValue);
        //                if (dtVaziiatNomre.Rows.Count == 0)
        //                {
        //                    rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //                }
        //                else
        //                {
        //                    //Report.................
        //                    img_ExportToExcel4.Visible = true;
        //                    this.StiWebViewer1.ResetReport();
        //                    StiWebViewer1.Visible = true;
        //                    StiReport rpt = new StiReport();
        //                    rpt.Load(Server.MapPath("../Report/ReportTestStatusScoreStudent.mrt"));
        //                    rpt.ReportCacheMode = StiReportCacheMode.On;
        //                    rpt.Dictionary.Databases.Clear();
        //                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //                    rpt.Compile();
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@StCode"].ParameterValue = txt_stCode.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@Isargar"].ParameterValue = int.Parse(ddl_Isargar.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@Vaznom"].ParameterValue = int.Parse(ddl_VaziiatNomre.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_StatusScoreStudents]"].Parameters["@IS"].ParameterValue = IS;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_GetNameVaznom]"].Parameters["@StatusScore"].ParameterValue = int.Parse(ddl_VaziiatNomre.SelectedValue);
        //                    rpt.RegData(dtVaziiatNomre);
        //                    //rpt.Dictionary.Synchronize();
        //                    //rpt.Show();
        //                    StiWebViewer1.Report = rpt;
        //                    StiWebViewer1.Visible = true;
        //                    //rpt.Print(true);
        //                }
        //            }
        //        }
        //        else if (rdb_VaziiatTerm1.Checked == true)
        //        {
        //            //img_ExportToExcel1.Visible = false;
        //            //img_ExportToExcel2.Visible = false;
        //            //img_ExportToExcel3.Visible = false;
        //            //img_ExportToExcel4.Visible = false;
        //            //img_ExportToExcel5.Visible = false;
        //            //img_ExportToExcel6.Visible = false;
        //            //img_ExportToExcel7.Visible = false;
        //            int IS;
        //            if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //            {
        //                IS = 1;
        //            }
        //            else
        //            {
        //                IS = 2;
        //            }
        //            DataTable dtStatusTermStudent = LSBIB.GetStatusTermStudent(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, int.Parse(ddl_StatusStu.SelectedValue.ToString()), ddl_Term.SelectedValue, int.Parse(ddl_VaziiatTerm.SelectedValue));
        //            if (dtStatusTermStudent.Rows.Count == 0)
        //            {
        //                rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //            }
        //            else
        //            {
        //                //Report ...........
        //                this.StiWebViewer1.ResetReport();
        //                img_ExportToExcel5.Visible = true;
        //                StiWebViewer1.Visible = true;
        //                StiReport rpt = new StiReport();
        //                rpt.Load(Server.MapPath("../Report/ReportStatusTermStudents.mrt"));
        //                rpt.ReportCacheMode = StiReportCacheMode.On;
        //                rpt.Dictionary.Databases.Clear();
        //                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //                rpt.Compile();
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@StCode"].ParameterValue = txt_stCode.Text;
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Isargar"].ParameterValue = int.Parse(ddl_Isargar.SelectedValue);
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@IS"].ParameterValue = IS;
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@VazTerm"].ParameterValue = int.Parse(ddl_VaziiatTerm.SelectedValue);
        //                rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
        //                rpt.RegData(dtStatusTermStudent);
        //                //rpt.Dictionary.Synchronize();
        //                //rpt.Show();
        //                StiWebViewer1.Report = rpt;
        //                StiWebViewer1.Visible = true;
        //                //rpt.Print(true);
        //            }


        //        }
        //        else if (rdb_FormMehmani.Checked == true)
        //        {
        //            //img_ExportToExcel1.Visible = false;
        //            //img_ExportToExcel2.Visible = false;
        //            //img_ExportToExcel3.Visible = false;
        //            //img_ExportToExcel4.Visible = false;
        //            //img_ExportToExcel5.Visible = false;
        //            //img_ExportToExcel6.Visible = false;
        //            //img_ExportToExcel7.Visible = false;
        //            int IS;
        //            if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //            {
        //                IS = 1;
        //            }
        //            else
        //            {
        //                IS = 2;
        //            }
        //            if (txt_ShomareName.Text == "")
        //            {
        //                txt_ShomareName.Text = "0";
        //            }
        //            if (txt_TarikhName.Text == "" || txt_TarikhName.Text == string.Empty)
        //            {
        //                lbl_DateName.Text = "13  /  /  ";
        //            }
        //            else
        //            {
        //                lbl_DateName.Text = txt_TarikhName.Text;
        //            }
        //            if (txt_ShomareName.Text == "0" && txt_stCode.Text == "0")
        //            {
        //                rwm.RadAlert("لطفا شماره دانشجویی یا شماره نامه را وارد نمایید", 0, 100, "پیام", "");
        //            }
        //            else
        //            {
        //                DataTable dtFormMehmani = LSBIB.GetFormStudentsGuest(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, int.Parse(ddl_StatusStu.SelectedValue), ddl_Term.SelectedValue, txt_ShomareName.Text, lbl_DateName.Text);
        //                if (dtFormMehmani.Rows.Count == 0)
        //                {
        //                    rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //                }
        //                else
        //                {
        //                    //DataTable dtNew = new DataTable();
        //                    //dtNew.Columns.Add("Harf", typeof(string));
        //                    //DataRow dr = dtNew.NewRow();
        //                    //int j = 0;
        //                    //for (Int32 i = 0; i < dtFormMehmani.Rows.Count; i++)
        //                    //{
        //                    //    DataTable Resault = LSBIB.ConvertNumberToWord(dtFormMehmani.Rows[j]["mark"].ToString());
        //                    //    dr["Harf"] = Resault.Rows[0][0].ToString();
        //                    //    dtNew.Rows.Add(dr.ItemArray);
        //                    //    j++;
        //                    //}
        //                    //dtFormMehmani.Columns.Add("Harf", typeof(string));
        //                    //for (Int32 i = 0; i < dtFormMehmani.Rows.Count; i++)
        //                    //{
        //                    //    dtFormMehmani.Rows[i]["Harf"] = dtNew.Rows[i]["Harf"];
        //                    //}

        //                    //Report ................
        //                    img_ExportToExcel1.Visible = true;
        //                    this.StiWebViewer1.ResetReport();
        //                    StiWebViewer1.Visible = true;
        //                    StiReport rpt = new StiReport();
        //                    rpt.Load(Server.MapPath("../Report/ReportFormStudentsGuest.mrt"));
        //                    rpt.ReportCacheMode = StiReportCacheMode.On;
        //                    rpt.Dictionary.Databases.Clear();
        //                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //                    rpt.Compile();
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@StCode"].ParameterValue = txt_stCode.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@Isargar"].ParameterValue = int.Parse(ddl_Isargar.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@IS"].ParameterValue = IS;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@TarikhName"].ParameterValue = lbl_DateName.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@ShomareName"].ParameterValue = txt_ShomareName.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_FormStudentsGuest]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
        //                    ((StiSqlSource)rpt.Dictionary.DataSources["[Students].[SP_FormStudentsGuest]"]).CommandTimeout = 30000;
        //                    rpt.RegData(dtFormMehmani);
        //                    //rpt.Dictionary.Synchronize();
        //                    //rpt.Show();
        //                    StiWebViewer1.Report = rpt;
        //                    StiWebViewer1.Visible = true;
        //                    //rpt.Print(true);
        //                }
        //            }
                    
        //            if (txt_ShomareName.Text == "0" ) 
        //            {
        //                 txt_ShomareName.Text = string.Empty;
        //            }
        //            if (txt_TarikhName.Text == "0")
        //            {
        //                txt_TarikhName.Text = string.Empty;
        //            }
        //        }

        //        else if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //        {
        //            //img_ExportToExcel1.Visible = false;
        //            //img_ExportToExcel2.Visible = false;
        //            //img_ExportToExcel3.Visible = false;
        //            //img_ExportToExcel4.Visible = false;
        //            //img_ExportToExcel5.Visible = false;
        //            //img_ExportToExcel6.Visible = false;
        //            //img_ExportToExcel7.Visible = false;
        //            int IS;
        //            //بدون وضعیت ایثارگری
        //            IS = 1;

        //            if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 1;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 2;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 3;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 4;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 5;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 6;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 7;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 8;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 9;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 10;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 11;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 12;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 13;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 14;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //            {
        //                Session["order"] = 15;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 16;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 17;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 18;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 19;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 20;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 21;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 22;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 23;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 24;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 25;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 26;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 27;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 28;
        //            }
        //            else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 29;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 30;
        //            }
        //            else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //            {
        //                Session["order"] = 31;
        //            }
        //            if (Session["order"] == null)
        //            {
        //                rwm.RadAlert("لطفا یکی از موارد را تیک بزنید", 0, 100, "پیام", "");
        //            }
        //            else
        //            {
        //                DataTable dtResault = LSBIB.GetReportIGRDInfo(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS);
        //                if (dtResault.Rows.Count == 0)
        //                {
        //                    rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //                }
        //                else
        //                {
        //                    this.StiWebViewer1.ResetReport();
        //                    StiWebViewer1.Visible = true;
        //                    StiReport rpt = new StiReport();
        //                    rpt.Load(Server.MapPath("../Report/ReportStudent.mrt"));
        //                    rpt.ReportCacheMode = StiReportCacheMode.On;
        //                    rpt.Dictionary.Databases.Clear();
        //                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //                    rpt.Compile();
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@StCode"].ParameterValue = txt_stCode.Text;
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@order"].ParameterValue = int.Parse(Session["order"].ToString());
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@Isargar"].ParameterValue = int.Parse(ddl_Isargar.SelectedValue);
        //                    rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@IS"].ParameterValue = IS;
        //                    //rpt.CompiledReport.DataSources["[Students].[SP_ReportStudent]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue.ToString();
        //                    rpt.RegData(dtResault);
        //                    //rpt.Dictionary.Synchronize();
        //                    //rpt.Show();
        //                    StiWebViewer1.Report = rpt;
        //                    StiWebViewer1.Visible = true;
        //                    //rpt.Print(true);
        //                }
        //            }
        //            if (Session["order"] != null)
        //            {
        //                Session["order"] = string.Empty;
        //            }
        //        }
        //        else
        //        {
        //            if (rdb_HichKodam.Checked == true || rdb_AdamMoraje.Checked == false || rdb_AddressTel.Checked == false || rdb_Enteghali.Checked == false || rdb_Mehmani.Checked == false || rdb_NaghsParvande.Checked == false || rdb_VaziiatNomre.Checked == false || rdb_VaziiatTerm1.Checked == false)
        //            {
        //                int IS;
        //                // با وضعیت ایثارگری
        //                IS = 2;
        //                if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 1;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 2;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 3;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 4;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 5;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 6;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 7;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 8;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 9;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 10;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 11;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 12;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 13;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 14;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                {
        //                    Session["order"] = 15;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 16;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 17;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 18;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 19;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 20;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 21;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 22;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 23;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 24;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 25;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 26;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 27;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 28;
        //                }
        //                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 29;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 30;
        //                }
        //                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                {
        //                    Session["order"] = 31;
        //                }
        //                if (Session["order"] == null)
        //                {
        //                    rwm.RadAlert("لطفا یکی از موارد را تیک بزنید", 0, 100, "پیام", "");
        //                }
        //                else
        //                {
        //                    DataTable dtResault = LSBIB.GetReportIGRDInfo(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS);
        //                    if (dtResault.Rows.Count == 0)
        //                    {
        //                        rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //                    }
        //                    else
        //                    {
        //                        this.StiWebViewer1.ResetReport();
        //                        StiWebViewer1.Visible = true;
        //                        img_ExportToExcel3.Visible = true;
        //                        StiReport rpt = new StiReport();
        //                        rpt.Load(Server.MapPath("../Report/ReportStudentsBasedOnIGRD.mrt"));
        //                        rpt.ReportCacheMode = StiReportCacheMode.On;
        //                        rpt.Dictionary.Databases.Clear();
        //                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //                        rpt.Compile();
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@StCode"].ParameterValue = txt_stCode.Text;
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Vazkol"].ParameterValue = int.Parse(ddl_StatusStu.SelectedValue);
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@order"].ParameterValue = int.Parse(Session["order"].ToString());
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Isargar"].ParameterValue = int.Parse(ddl_Isargar.SelectedValue);
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@IS"].ParameterValue = IS;
        //                        rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue.ToString();
        //                        rpt.RegData(dtResault);

        //                        //rpt.Dictionary.Synchronize();
        //                        //rpt.Show();
        //                        StiWebViewer1.Report = rpt;
        //                        StiWebViewer1.Visible = true;
        //                        //rpt.Print(true);
        //                    }
        //                }
        //            }
        //        }
        //        if (Session["order"] != null)
        //        {
        //            Session["order"] = null;
        //        }
        //        if (txt_stCode.Text == "0" )
        //        {
        //           txt_stCode.Text = string.Empty;
        //        }
        //        if (txt_SalVorod.Text == "0")
        //        {
        //            txt_SalVorod.Text = string.Empty;
        //        }
        //    }
        //}

        //protected void rdb_Mehmani_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdb_Mehmani.Checked == true || rdb_Enteghali.Checked == true || rdb_HichKodam.Checked == true || rdb_AddressTel.Checked == true)
        //    {
        //        lbl_ShomareName.Visible = false;
        //        txt_ShomareName.Visible = false;
        //        lbl_TarikhName.Visible = false;
        //        txt_TarikhName.Visible = false;
        //        chk_MoadelSazi.Visible = true;
        //        chk_MoadelSazi.Visible = true;
        //        chk_SabteNameBaTakhir.Visible = true;
        //        chk_TaeedieTahsili.Visible = true;
        //        chk_TakmilZarfiat.Visible = true;
        //        chk_TermJari.Visible = true;
        //        lbl_VaziiatNomre.Visible = false;
        //        ddl_VaziiatNomre.Visible = false;
        //        lbl_VaziiatTerm.Visible = false;
        //        ddl_VaziiatTerm.Visible = false;
        //        txt_Term.Visible = false;
        //    }
        //    else if (rdb_FormMehmani.Checked == true)
        //    {
        //        lbl_ShomareName.Visible = true;
        //        txt_ShomareName.Visible = true;
        //        lbl_TarikhName.Visible = true;
        //        txt_TarikhName.Visible = true;
        //        chk_MoadelSazi.Visible = false;
        //        chk_SabteNameBaTakhir.Visible = false;
        //        chk_TaeedieTahsili.Visible = false;
        //        chk_TakmilZarfiat.Visible = false;
        //        chk_TermJari.Visible = false;
        //        lbl_VaziiatNomre.Visible = false;
        //        ddl_VaziiatNomre.Visible = false;
        //        lbl_VaziiatTerm.Visible = false;
        //        ddl_VaziiatTerm.Visible = false;
        //        txt_Term.Visible = false;
        //    }
        //    else if (rdb_VaziiatNomre.Checked == true)
        //    {
        //        lbl_VaziiatNomre.Visible = true;
        //        ddl_VaziiatNomre.Visible = true;
        //        if (ddl_VaziiatNomre.SelectedValue != "58")
        //        {
        //            ddl_VaziiatNomre.SelectedValue = "58";
        //        }
        //        lbl_ShomareName.Visible = false;
        //        txt_ShomareName.Visible = false;
        //        lbl_TarikhName.Visible = false;
        //        txt_TarikhName.Visible = false;
        //        chk_MoadelSazi.Visible = false;
        //        chk_SabteNameBaTakhir.Visible = false;
        //        chk_TaeedieTahsili.Visible = false;
        //        chk_TakmilZarfiat.Visible = false;
        //        chk_TermJari.Visible = false;
        //        lbl_VaziiatTerm.Visible = false;
        //        ddl_VaziiatTerm.Visible = false;
        //        txt_Term.Visible = false;
        //    }
        //    else if (rdb_VaziiatTerm1.Checked == true)
        //    {
        //        lbl_VaziiatTerm.Visible = true;
        //        ddl_VaziiatTerm.Visible = true;
        //        if (ddl_VaziiatTerm.SelectedValue != "0")
        //        {
        //            ddl_VaziiatTerm.SelectedValue = "0";
        //        }

        //        lbl_ShomareName.Visible = false;
        //        txt_ShomareName.Visible = false;
        //        lbl_TarikhName.Visible = false;
        //        txt_TarikhName.Visible = false;
        //        chk_MoadelSazi.Visible = false;
        //        chk_SabteNameBaTakhir.Visible = false;
        //        chk_TaeedieTahsili.Visible = false;
        //        chk_TakmilZarfiat.Visible = false;
        //        chk_TermJari.Visible = false;
        //        lbl_VaziiatNomre.Visible = false;
        //        ddl_VaziiatNomre.Visible = false;
        //        txt_Term.Visible = false;

        //    }
        //    else if (rdb_AdamMoraje.Checked == true)
        //    {
        //        txt_Term.Visible = true;
        //        if (txt_Term.Text != "  -  - ")
        //        {
        //            txt_Term.Text = "  -  - ";
        //        }
        //        lbl_ShomareName.Visible = false;
        //        txt_ShomareName.Visible = false;
        //        lbl_TarikhName.Visible = false;
        //        txt_TarikhName.Visible = false;
        //        chk_MoadelSazi.Visible = false;
        //        chk_SabteNameBaTakhir.Visible = false;
        //        chk_TaeedieTahsili.Visible = false;
        //        chk_TakmilZarfiat.Visible = false;
        //        chk_TermJari.Visible = false;
        //        lbl_VaziiatNomre.Visible = false;
        //        ddl_VaziiatNomre.Visible = false;
        //        lbl_VaziiatTerm.Visible = false;
        //        ddl_VaziiatTerm.Visible = false;
        //    }
        //    else
        //    {
        //        lbl_ShomareName.Visible = false;
        //        txt_ShomareName.Visible = false;
        //        lbl_TarikhName.Visible = false;
        //        txt_TarikhName.Visible = false;
        //        chk_MoadelSazi.Visible = false;
        //        chk_SabteNameBaTakhir.Visible = false;
        //        chk_TaeedieTahsili.Visible = false;
        //        chk_TakmilZarfiat.Visible = false;
        //        chk_TermJari.Visible = false;
        //        lbl_VaziiatNomre.Visible = false;
        //        ddl_VaziiatNomre.Visible = false;
        //        lbl_VaziiatTerm.Visible = false;
        //        ddl_VaziiatTerm.Visible = false;
        //        txt_Term.Visible = false;
        //    }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
            int IS;
            if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
            {
                IS = 1;
            }
            else
            {
                IS = 2;
            }
            if (txt_stCode.Text == null)
            {
                txt_stCode.Text = "0";
            }
            if (txt_SalVorod.Text == null)
            {
                txt_SalVorod.Text = "0";
            }
            DataTable dt = LSBIB.GetFormStudentsGuest(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, int.Parse(ddl_StatusStu.SelectedValue), ddl_Term.SelectedValue, txt_ShomareName.Text, lbl_DateName.Text);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportFormStudentsGuest.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    foreach (TableCell cell in GridView1.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void img_ExportToExcel2_Click(object sender, ImageClickEventArgs e)
        {
            //int Isargar;
            //int IS;
            //if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
            //{
            //    Isargar = 1;
            //    IS = 1;
            //}
            //else
            //{
            //    Isargar = 2;
            //    IS = 2;
            //}
            //DataTable dt = LSBIB.GetIncompleteStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), Isargar, int.Parse(ddl_AcceptedStu.SelectedValue), IS, int.Parse(ddl_StatusStu.SelectedValue));
            //if (dt.Rows.Count == 0)
            //{
            //}
            //else
            //{
            //    GridView2.DataSource = dt;
            //    GridView2.DataBind();
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=ReportIncompleteStudents.xls");
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.ms-excel";
            //    using (StringWriter sw = new StringWriter())
            //    {
            //        HtmlTextWriter hw = new HtmlTextWriter(sw);
            //        foreach (TableCell cell in GridView2.HeaderRow.Cells)
            //        {
            //            cell.BackColor = GridView2.HeaderStyle.BackColor;
            //        }
            //        foreach (GridViewRow row in GridView2.Rows)
            //        {
            //            foreach (TableCell cell in row.Cells)
            //            {
            //                if (row.RowIndex % 2 == 0)
            //                {
            //                    cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
            //                }
            //                else
            //                {
            //                    cell.BackColor = GridView2.RowStyle.BackColor;
            //                }
            //                cell.CssClass = "textmode";
            //            }
            //        }
            //        GridView2.RenderControl(hw);
            //        string style = @"<style> .textmode { } </style>";
            //        Response.Write(style);
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }
            //}
        }

        protected void img_ExportToExcel3_Click(object sender, ImageClickEventArgs e)
        {
            //int IS;
            //if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
            //{
            //    //بدون وضعیت ایثارگری
            //    IS = 1;
            //    Session["order"] = 1;
            //}
            //else
            //{
            //    //با وضعیت ایثارگری
            //    IS = 2;
            //    Session["order"] = 2;
            //}
            //DataTable dt = LSBIB.GetReportLackOfStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, txt_Term.Text, int.Parse(ddl_StatusStu.SelectedValue) , ddl_Term.SelectedValue);
            //if (dt.Rows.Count == 0)
            //{
            //}
            //else
            //{
            //    GridView3.DataSource = dt;
            //    GridView3.DataBind();
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=ReportLackOfStudents.xls");
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.ms-excel";
            //    using (StringWriter sw = new StringWriter())
            //    {
            //        HtmlTextWriter hw = new HtmlTextWriter(sw);
            //        foreach (TableCell cell in GridView3.HeaderRow.Cells)
            //        {
            //            cell.BackColor = GridView3.HeaderStyle.BackColor;
            //        }
            //        foreach (GridViewRow row in GridView3.Rows)
            //        {
            //            foreach (TableCell cell in row.Cells)
            //            {
            //                if (row.RowIndex % 2 == 0)
            //                {
            //                    cell.BackColor = GridView3.AlternatingRowStyle.BackColor;
            //                }
            //                else
            //                {
            //                    cell.BackColor = GridView3.RowStyle.BackColor;
            //                }
            //                cell.CssClass = "textmode";
            //            }
            //        }

            //        GridView3.RenderControl(hw);
            //        string style = @"<style> .textmode { } </style>";
            //        Response.Write(style);
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }
            //}
            //if (Session["order"] != null || Session["order"] != string.Empty)
            //{
            //    Session["order"] = string.Empty;
            //}
        }

        protected void img_ExportToExcel4_Click(object sender, ImageClickEventArgs e)
        {
            //int IS;
            //if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
            //{
            //    IS = 1;
            //}
            //else
            //{
            //    IS = 2;
            //}
            //DataTable dt = LSBIB.GetStatusScoreStudent(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_VaziiatNomre.SelectedValue), ddl_Term.SelectedValue);
            //if (dt.Rows.Count == 0)
            //{
            //}
            //else
            //{
            //    GridView4.DataSource = dt;
            //    GridView4.DataBind();
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=ReportStatusScoreStudent.xls");
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.ms-excel";
            //    using (StringWriter sw = new StringWriter())
            //    {
            //        HtmlTextWriter hw = new HtmlTextWriter(sw);
            //        foreach (TableCell cell in GridView4.HeaderRow.Cells)
            //        {
            //            cell.BackColor = GridView4.HeaderStyle.BackColor;
            //        }
            //        foreach (GridViewRow row in GridView4.Rows)
            //        {
            //            foreach (TableCell cell in row.Cells)
            //            {
            //                if (row.RowIndex % 2 == 0)
            //                {
            //                    cell.BackColor = GridView4.AlternatingRowStyle.BackColor;
            //                }
            //                else
            //                {
            //                    cell.BackColor = GridView4.RowStyle.BackColor;
            //                }
            //                cell.CssClass = "textmode";
            //            }
            //        }

            //        GridView4.RenderControl(hw);
            //        string style = @"<style> .textmode { } </style>";
            //        Response.Write(style);
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }
            //}
        }

        protected void img_ExportToExcel5_Click(object sender, ImageClickEventArgs e)
        {
            //int IS;
            //if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
            //{
            //    IS = 1;
            //}
            //else
            //{
            //    IS = 2;
            //}
            //DataTable dt = LSBIB.GetStatusTermStudent(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, int.Parse(ddl_StatusStu.SelectedValue.ToString()), ddl_Term.SelectedValue, int.Parse(ddl_VaziiatTerm.SelectedValue));
            //if (dt.Rows.Count == 0)
            //{
            //}
            //else
            //{
            //    GridView5.DataSource = dt;
            //    GridView5.DataBind();
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=ReportStatusTermStudent.xls");
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.ms-excel";
            //    using (StringWriter sw = new StringWriter())
            //    {
            //        HtmlTextWriter hw = new HtmlTextWriter(sw);
            //        foreach (TableCell cell in GridView5.HeaderRow.Cells)
            //        {
            //            cell.BackColor = GridView5.HeaderStyle.BackColor;
            //        }
            //        foreach (GridViewRow row in GridView5.Rows)
            //        {
            //            foreach (TableCell cell in row.Cells)
            //            {
            //                if (row.RowIndex % 2 == 0)
            //                {
            //                    cell.BackColor = GridView5.AlternatingRowStyle.BackColor;
            //                }
            //                else
            //                {
            //                    cell.BackColor = GridView5.RowStyle.BackColor;
            //                }
            //                cell.CssClass = "textmode";
            //            }
            //        }

            //        GridView5.RenderControl(hw);
            //        string style = @"<style> .textmode { } </style>";
            //        Response.Write(style);
            //        Response.Output.Write(sw.ToString());
            //        Response.Flush();
            //        Response.End();
            //    }
            //}
        }

        protected void img_ExportToExcel6_Click(object sender, ImageClickEventArgs e)
        {
            //int IS;
            //if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
            //{

            //    //بدون وضعیت ایثارگری
            //    IS = 1;
            //    if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 0;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 1;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 2;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 3;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 4;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 5;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 6;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 7;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 8;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 9;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 10;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 11;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 12;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 13;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 14;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 15;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 16;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 17;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 18;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 19;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 20;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 21;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 22;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 23;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 24;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 25;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 26;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 27;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 28;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 29;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 30;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 31;
            //    }
            //}
            //else
            //{
            //    // با وضعیت ایثارگری
            //    IS = 2;
            //    if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 0;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 1;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 2;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 3;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 4;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 5;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 6;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 7;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 8;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 9;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 10;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 11;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 12;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 13;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 14;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
            //    {
            //        Session["order"] = 15;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 16;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 17;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 18;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 19;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 20;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 21;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 22;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 23;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 24;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 25;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 26;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 27;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 28;
            //    }
            //    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 29;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 30;
            //    }
            //    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
            //    {
            //        Session["order"] = 31;
            //    }
            //    DataTable dt = LSBIB.GetReportIGRDInfo(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS);
            //    if (dt.Rows.Count == 0)
            //    {
            //    }
            //    else
            //    {
            //        GridView6.DataSource = dt;
            //        GridView6.DataBind();
            //        Response.Clear();
            //        Response.Buffer = true;
            //        Response.AddHeader("content-disposition", "attachment;filename=ReportIGRDInfo.xls");
            //        Response.Charset = "";
            //        Response.ContentType = "application/vnd.ms-excel";
            //        using (StringWriter sw = new StringWriter())
            //        {
            //            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //            foreach (TableCell cell in GridView6.HeaderRow.Cells)
            //            {
            //                cell.BackColor = GridView6.HeaderStyle.BackColor;
            //            }
            //            foreach (GridViewRow row in GridView6.Rows)
            //            {
            //                foreach (TableCell cell in row.Cells)
            //                {
            //                    if (row.RowIndex % 2 == 0)
            //                    {
            //                        cell.BackColor = GridView6.AlternatingRowStyle.BackColor;
            //                    }
            //                    else
            //                    {
            //                        cell.BackColor = GridView6.RowStyle.BackColor;
            //                    }
            //                    cell.CssClass = "textmode";
            //                }
            //            }

            //            GridView6.RenderControl(hw);
            //            string style = @"<style> .textmode { } </style>";
            //            Response.Write(style);
            //            Response.Output.Write(sw.ToString());
            //            Response.Flush();
            //            Response.End();
            //        }
            //    }
            //}
            //if (Session["Order"] != null)
            //{
            //    Session["Order"] = null;
            //}
        }
        protected void img_ExportToExcel7_Click(object sender, ImageClickEventArgs e)
        {
        }
        //protected void img_ExportToExcel7_Click(object sender, ImageClickEventArgs e)
        //{
        //    int IS;
        //    if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //    {
        //        //بدون وضعیت ایثارگری
        //        IS = 1;
        //        if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 0;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 1;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 2;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 3;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 4;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 5;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 6;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 7;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 8;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 9;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 10;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 11;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 12;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 13;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 14;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 15;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 16;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 17;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 18;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 19;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 20;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 21;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 22;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 23;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 24;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 25;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 26;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 27;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 28;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 29;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 30;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 31;
        //        }
        //    }
        //    else
        //    {
        //        // با وضعیت ایثارگری
        //        IS = 2;

        //        if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 0;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 1;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 2;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 3;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 4;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 5;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 6;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 7;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 8;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 9;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 10;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 11;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 12;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 13;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 14;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 15;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 16;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 17;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 18;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 19;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 20;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 21;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 22;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 23;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 24;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 25;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 26;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 27;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 28;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 29;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 30;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 31;
        //        }
        //    }

        //    DataTable dt = LSBIB.GetReportGuestStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), 1, ddl_Term.SelectedValue);
        //    if (dt.Rows.Count == 0)
        //    {
        //    }
        //    else
        //    {
        //        GridView7.DataSource = dt;
        //        GridView7.DataBind();
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition", "attachment;filename=ReportGuestStudents.xls");
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.ms-excel";
        //        using (StringWriter sw = new StringWriter())
        //        {
        //            HtmlTextWriter hw = new HtmlTextWriter(sw);
        //            foreach (TableCell cell in GridView7.HeaderRow.Cells)
        //            {
        //                cell.BackColor = GridView7.HeaderStyle.BackColor;
        //            }
        //            foreach (GridViewRow row in GridView7.Rows)
        //            {
        //                foreach (TableCell cell in row.Cells)
        //                {
        //                    if (row.RowIndex % 2 == 0)
        //                    {
        //                        cell.BackColor = GridView7.AlternatingRowStyle.BackColor;
        //                    }
        //                    else
        //                    {
        //                        cell.BackColor = GridView7.RowStyle.BackColor;
        //                    }
        //                    cell.CssClass = "textmode";
        //                }
        //            }

        //            GridView7.RenderControl(hw);
        //            string style = @"<style> .textmode { } </style>";
        //            Response.Write(style);
        //            Response.Output.Write(sw.ToString());
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //    if (Session["Order"] != null)
        //    {
        //        Session["Order"] = null;
        //    }
        //}
        protected void RadAjaxManager1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                txt_stCode.Text = Session["stcode"].ToString();
            }
        }
        protected void img_ExportToExcel8_Click(object sender, ImageClickEventArgs e)
        {
        }
        protected void img_ExportToExcel9_Click(object sender, ImageClickEventArgs e)
        {
        }
        //protected void img_ExportToExcel8_Click(object sender, ImageClickEventArgs e)
        //{
        //    int IS;
        //    if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //    {
        //        //بدون وضعیت ایثارگری
        //        IS = 1;
        //        if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 0;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 1;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 2;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 3;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 4;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 5;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 6;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 7;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 8;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 9;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 10;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 11;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 12;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 13;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 14;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 15;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 16;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 17;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 18;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 19;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 20;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 21;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 22;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 23;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 24;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 25;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 26;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 27;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 28;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 29;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 30;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 31;
        //        }
        //    }
        //    else
        //    {
        //        // با وضعیت ایثارگری
        //        IS = 2;

        //        if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 0;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 1;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 2;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 3;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 4;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 5;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 6;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 7;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 8;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 9;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 10;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 11;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 12;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 13;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 14;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //        {
        //            Session["order"] = 15;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 16;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 17;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 18;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 19;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 20;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 21;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 22;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 23;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 24;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 25;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 26;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 27;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 28;
        //        }
        //        else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 29;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 30;
        //        }
        //        else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //        {
        //            Session["order"] = 31;
        //        }
        //    }
        //    DataTable dt = LSBIB.GetReportGuestStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), 1, ddl_Term.SelectedValue);
        //    if (dt.Rows.Count == 0)
        //    {
        //        rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //    }
        //    else
        //    {
        //        GridView8.DataSource = dt;
        //        GridView8.DataBind();
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.AddHeader("content-disposition", "attachment;filename=ReportGuestStudents.xls");
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.ms-excel";
        //        using (StringWriter sw = new StringWriter())
        //        {
        //            HtmlTextWriter hw = new HtmlTextWriter(sw);
        //            foreach (TableCell cell in GridView8.HeaderRow.Cells)
        //            {
        //                cell.BackColor = GridView8.HeaderStyle.BackColor;
        //            }
        //            foreach (GridViewRow row in GridView8.Rows)
        //            {
        //                foreach (TableCell cell in row.Cells)
        //                {
        //                    if (row.RowIndex % 2 == 0)
        //                    {
        //                        cell.BackColor = GridView8.AlternatingRowStyle.BackColor;
        //                    }
        //                    else
        //                    {
        //                        cell.BackColor = GridView8.RowStyle.BackColor;
        //                    }
        //                    cell.CssClass = "textmode";
        //                }
        //            }

        //            GridView8.RenderControl(hw);
        //            string style = @"<style> .textmode { } </style>";
        //            Response.Write(style);
        //            Response.Output.Write(sw.ToString());
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //    if (Session["Order"] != null)
        //    {
        //        Session["Order"] = null;
        //    }
        //}

        //protected void img_ExportToExcel9_Click(object sender, ImageClickEventArgs e)
        //{
        //       int IS;
        //                if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
        //                {

        //                    //بدون وضعیت ایثارگری
        //                    IS = 1;
        //                    if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 0;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 1;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 2;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 3;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 4;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 5;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 6;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 7;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 8;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 9;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 10;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 11;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 12;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 13;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 14;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 15;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 16;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 17;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 18;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 19;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 20;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 21;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 22;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 23;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 24;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 25;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 26;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 27;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 28;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 29;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 30;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 31;
        //                    }
        //                }
        //                else
        //                {
        //                    // با وضعیت ایثارگری
        //                    IS = 2;
        //                    if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 0;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 1;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 2;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 3;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 4;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 5;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 6;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 7;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 8;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 9;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 10;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 11;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 12;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 13;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 14;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
        //                    {
        //                        Session["order"] = 15;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 16;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 17;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 18;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 19;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 20;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 21;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 22;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 23;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 24;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 25;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 26;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 27;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 28;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 29;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 30;
        //                    }
        //                    else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
        //                    {
        //                        Session["order"] = 31;
        //                    }
        //                }

        //                DataTable dtResaultTransfer = LSBIB.GetReportTransferStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, ddl_Term.SelectedValue);
        //                if (dtResaultTransfer.Rows.Count == 0)
        //                {
                           
        //                }
        //                else
        //                {
        //                    GridView9.DataSource = dtResaultTransfer;
        //                    GridView9.DataBind();
        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.AddHeader("content-disposition", "attachment;filename=ReportTransferStudents.xls");
        //                    Response.Charset = "";
        //                    Response.ContentType = "application/vnd.ms-excel";
        //                    using (StringWriter sw = new StringWriter())
        //                    {
        //                        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //                        foreach (TableCell cell in GridView9.HeaderRow.Cells)
        //                        {
        //                            cell.BackColor = GridView9.HeaderStyle.BackColor;
        //                        }
        //                        foreach (GridViewRow row in GridView9.Rows)
        //                        {
        //                            foreach (TableCell cell in row.Cells)
        //                            {
        //                                if (row.RowIndex % 2 == 0)
        //                                {
        //                                    cell.BackColor = GridView9.AlternatingRowStyle.BackColor;
        //                                }
        //                                else
        //                                {
        //                                    cell.BackColor = GridView9.RowStyle.BackColor;
        //                                }
        //                                cell.CssClass = "textmode";
        //                            }
        //                        }

        //                        GridView9.RenderControl(hw);
        //                        string style = @"<style> .textmode { } </style>";
        //                        Response.Write(style);
        //                        Response.Output.Write(sw.ToString());
        //                        Response.Flush();
        //                        Response.End();
        //                    }
        //                }
        //                if (Session["Order"] != null)
        //                {
        //                    Session["Order"] = null;
        //                }
        //}

        protected void img_ExportToExcel10_Click(object sender, ImageClickEventArgs e)
        {
            int IS;
            if (ddl_Isargar.SelectedValue == null || ddl_Isargar.SelectedValue == "0")
            {
                IS = 1;
                if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 0;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 1;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 2;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 3;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 4;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 5;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 6;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 7;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 8;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 9;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 10;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 11;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 12;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 13;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 14;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 15;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 16;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 17;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 18;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 19;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 20;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 21;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 22;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 23;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 24;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 25;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 26;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 27;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 28;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 29;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 30;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 31;
                }
            }

            else
            {
                // با وضعیت ایثارگری
                IS = 2;
                if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 0;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 1;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 2;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 3;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 4;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 5;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 6;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == false)
                {
                    Session["order"] = 7;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 8;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 9;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 10;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 11;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 12;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 13;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 14;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == false)
                {
                    Session["order"] = 15;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 16;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 17;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 18;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 19;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 20;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 21;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 22;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 23;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == true && chk_TermJari.Checked == true)
                {
                    Session["order"] = 24;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 25;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 26;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 27;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == false && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 28;
                }
                else if (chk_MoadelSazi.Checked == false && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 29;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == false && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 30;
                }
                else if (chk_MoadelSazi.Checked == true && chk_SabteNameBaTakhir.Checked == true && chk_TaeedieTahsili.Checked == true && chk_TakmilZarfiat.Checked == false && chk_TermJari.Checked == true)
                {
                    Session["order"] = 31;
                }
            }
            DataTable dtResaultAddressTelStudent = LSBIB.GetReportAddressTelStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_stCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue), int.Parse(ddl_Isargar.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), IS, ddl_Term.SelectedValue);
            if (dtResaultAddressTelStudent.Rows.Count == 0)
            {
                rwm.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
            }
            else
            {
                GridView10.DataSource = dtResaultAddressTelStudent;
                GridView10.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportAddressTelStudents.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    foreach (TableCell cell in GridView10.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView10.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView10.Rows)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView10.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView10.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView10.RenderControl(hw);
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }

            if (Session["Order"] != null)
            {
                Session["Order"] = null;
            }
        }
        
    }
}