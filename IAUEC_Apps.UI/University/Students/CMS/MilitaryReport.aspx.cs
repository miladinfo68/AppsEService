using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Students;
using IAUEC_Apps.DTO.University.Students;
using System.Data;
using Telerik.Web.UI;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using OfficeOpenXml;

namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class MilitaryReport : System.Web.UI.Page
    {
        MilitaryReportBusiness mrb = new MilitaryReportBusiness();
        MilitaryReportDTO mrd = new MilitaryReportDTO();

        DataTable rsht = new DataTable();
        DataTable dt = new DataTable();
        string reshte;
        string salVorood;
        string vaziat;
        string maghta;
        string mojavez;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mId = Request.QueryString["id"].ToString();
                string[] id = mId.ToString().Split(new char[] { '@' });
                string menuId = "";
                for (int i = 0; i < id[1].Length; i++)
                {
                    string s = id[1].Substring(i + 1, 1);
                    if (s != "-")
                        menuId += s;
                    else
                        break;
                    Session[sessionNames.menuID] = menuId;
                }
                AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
                Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
                rsht = CB.SelectAllField(0);
                setComboBind();                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSalVorood.Text == string.Empty && cmbReshte.SelectedValue == string.Empty && cmbVaziat.SelectedValue == "-انتخاب کنید-" && cmbMaghta.SelectedValue == "-انتخاب کنید-" && cmbMojavez.SelectedValue == "-انتخاب کنید-") //at least one text box should have a value for search
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('حداقل باید یکی از باکس ها را پر کنید')", true);
            }
            else
            {
                salVorood = txtSalVorood.Text;
                ViewState.Add("salVorood", salVorood);
                              

                BindGrid();
                
                if (dt.Rows.Count == 0)
                {
                    btnExportToExcel.Visible = false;
                }
                else
                {
                    btnExportToExcel.Visible = true;
                }

            }
        }

        //set combo box values
        private void setComboBind()
        {
            getreshte(rsht);
            getVaziat();
            getMojavez();
            getMaghta();
        }

        private void BindGrid()
        {
            mrd.reshte = (string)ViewState["reshte"];
            mrd.salVorood = (string)ViewState["salVorood"];
            mrd.vaziat = (string)ViewState["vaziat"];
            mrd.maghta = (string)ViewState["maghta"];
            mrd.mojavez = (string)ViewState["mojavez"];

            checkObject();

            //to bind database with data gridview            
            dt = mrb.GetInfoSTUD(mrd);
            ViewState.Add("dt", dt);
            grdResult.DataSource = dt;
            grdResult.DataBind();
        }

        //check fields and if they are null change to empty string
        private void checkObject()
        {
            if (mrd.reshte == null)
            {
                mrd.reshte = "";
            }
            if (mrd.salVorood == null)
            {
                mrd.salVorood = "";
            }
            if (mrd.vaziat == null)
            {
                mrd.vaziat = "";
            }
            if (mrd.maghta == null)
            {
                mrd.maghta = "";
            }
            if (mrd.mojavez == null)
            {
                mrd.mojavez = "";
            }
        }
        

        /////////////////////////////////////////////////combobox selected index change//////////////////////////////////////
        protected void cmbReshte_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            reshte = cmbReshte.SelectedValue;
            ViewState.Add("reshte", reshte);
        }

        protected void cmbMaghta_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (cmbMaghta.SelectedValue)
            {
                case "کارشناسی":
                    maghta = "1";
                    break;
                case"کاردانی":
                    maghta = "2";
                    break;
                case "ک ناپيوسته":
                    maghta = "3";
                    break;
                case "ارشد پيوسته":
                    maghta = "4";
                    break;
                case "ارشد ناپيوسته":
                    maghta = "5";
                    break;
                case "دکتری حرفه ای":
                    maghta = "6";
                    break;
                case "دکتری تخصصی":
                    maghta = "7";
                    break;
                case "کاردانی پيوسته":
                    maghta = "8";
                    break;
                default:
                    
                    break;
            }
            ViewState.Add("maghta", maghta);
        }

        protected void cmbVaziat_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (cmbVaziat.SelectedValue)
            {
                case "عادی":
                    vaziat = "1";
                    break;
                case "مهمان از":
                    vaziat = "2";
                    break;
                case "انصراف تغییر رشته":
                    vaziat = "3";
                    break;
                case "انتقال از":
                    vaziat = "4";
                    break;
                case "انصراف با اطلاع":
                    vaziat = "5";
                    break;
                case "انصراف ماده 51":
                    vaziat = "6";
                    break;
                case "فارغ التحصیل":
                    vaziat = "7";
                    break;
                case "اخراج آموزشی":
                    vaziat = "8";
                    break;
                case "اخراج انضباطی":
                    vaziat = "9";
                    break;
                case "اخراج از واحدهای تهران":
                    vaziat = "10";
                    break;
                case "اخراج از کل واحدها":
                    vaziat = "11";
                    break;
                case "محروم":
                    vaziat = "12";
                    break;
                case "فوت":
                    vaziat = "13";
                    break;
                case "شهید":
                    vaziat = "14";
                    break;
                case "مهمان از سازمان":
                    vaziat = "15";
                    break;
                case "عدم مراجعه":
                    vaziat = "16";
                    break;
                case "در شرف فارغ التحصيل":
                    vaziat = "17";
                    break;
                case "تسويه حساب- مدرک معادل":
                    vaziat = "18";
                    break;

                default:

                    break;
            }

            ViewState.Add("vaziat", vaziat);
        }

        protected void cmbMojavez_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            mojavez = cmbMojavez.SelectedValue;
            if (mojavez == "-انتخاب کنید-")
            {
                mojavez = string.Empty;
            }
            ViewState.Add("mojavez", mojavez);
        }

        protected void grdResult_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            dt = (DataTable)ViewState["dt"];
            grdResult.DataSource = dt;            
        }
                
        //////////////////////////////////////////////////set combo box item/////////////////////////////////////////////////
        
        private void getreshte(DataTable dt)
        {
            
            cmbReshte.DataSource = dt;
            cmbReshte.DataTextField = "nameresh";
            cmbReshte.DataValueField = "id";
            cmbReshte.DataBind();
        }

        private void getVaziat()
        {
            List<string> vaz = new List<string>(19);

            vaz.Add("-انتخاب کنید-");
            vaz.Add("عادی");
            vaz.Add("مهمان از");
            vaz.Add("انصراف تغییر رشته");
            vaz.Add("انتقال از");
            vaz.Add("انصراف با اطلاع");
            vaz.Add("انصراف ماده 51");
            vaz.Add("فارغ التحصیل");
            vaz.Add("اخراج آموزشی");
            vaz.Add("اخراج انضباطی");
            vaz.Add("اخراج از واحدهای تهران");
            vaz.Add("اخراج از کل واحدها");
            vaz.Add("محروم");
            vaz.Add("فوت");
            vaz.Add("شهید");
            vaz.Add("مهمان از سازمان");
            vaz.Add("عدم مراجعه");
            vaz.Add("در شرف فارغ التحصيل");
            vaz.Add("تسويه حساب- مدرک معادل");

            cmbVaziat.DataSource = vaz;
            cmbVaziat.DataBind();
        }

        private void getMojavez()
        {
            List<string> moj = new List<string>(19);

            moj.Add("-انتخاب کنید-");
            moj.Add("دائم");
            moj.Add("موقت");

            cmbMojavez.DataSource = moj;
            cmbMojavez.DataBind();
        }

        private void getMaghta()
        {
            List<string> magh = new List<string>(19);

            magh.Add("-انتخاب کنید-");
            magh.Add("کارشناسی");
            magh.Add("کاردانی");
            magh.Add("ک ناپيوسته");
            magh.Add("ارشد پيوسته");
            magh.Add("ارشد ناپيوسته");
            magh.Add("دکتری حرفه ای");
            magh.Add("دکتری تخصصی");
            magh.Add("کاردانی پيوسته");

            cmbMaghta.DataSource = magh;
            cmbMaghta.DataBind();
        }

        //////////////////////////////////////////////// end set combo box item//////////////////////////////////////////////   

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                grdResult.ExportSettings.IgnorePaging = true;
                grdResult.ExportSettings.UseItemStyles = true;
                grdResult.ExportSettings.FileName = "Report_ " + DateTime.Now.ToShortDateString() + " _ " + DateTime.Now.ToShortTimeString();
                grdResult.MasterTableView.ExportToExcel();
            }
            catch
            {
            }
        }
    }
}