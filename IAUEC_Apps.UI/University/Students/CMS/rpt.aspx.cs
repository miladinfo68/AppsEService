using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using System.Data;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.Business.university.Students;
using Telerik.Web.UI;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;


namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class rpt : System.Web.UI.Page
    {
        //CommonDAO dao = new CommonDAO();
        CommonBusiness CB = new CommonBusiness();
        EducationReportBusiness ERB = new EducationReportBusiness();
        ListStudentsBasedOnIGRDBusiness LSBIB = new ListStudentsBasedOnIGRDBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            StiWebViewer1.Visible = false;
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
                AccessControl.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
                RadComboBoxItem cmbitem = new RadComboBoxItem();
                cmbitem.Text = "همه موارد";
                cmbitem.Value = "0";

                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataValueField = "tterm";
                ddl_Term.DataSource = CB.SelectAllTerm();
                ddl_Term.DataBind();               
                ddl_Term.Items.Add(cmbitem);
                int daneshId = 0;
                switch(int.Parse(Session[sessionNames.roleID].ToString()))
                {
                    case 15:
                        daneshId = 2;
                        break;
                    case 16:
                        daneshId = 3;
                        break;
                    case 17:
                        daneshId = 1;
                        break;
                    case 26:
                        daneshId = 2;
                        break;
                    case 27:
                        daneshId = 3;
                        break;
                    case 28:
                        daneshId = 1;
                        break;
                    case 67:
                        daneshId = 8;
                        break;
                    case 66:
                        daneshId = 8;
                        break;
                    default:
                        break;
                }
             
                    
                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = CB.SelectAllDaneshkade(daneshId);
                ddl_Daneshkade.DataBind();
               

                if (daneshId > 0)
                    ddl_Daneshkade.SelectedValue = daneshId.ToString();
                else
                    ddl_Daneshkade.Items.Add(cmbitem);
                DataTable dtField = CB.SelectAllField(daneshId);
                dtField.Columns[0].ReadOnly = false;
                for (int i = 0; i <= dtField.Rows.Count - 1; i++)
                {

                    dtField.Rows[i][0] = dtField.Rows[i][0].ToString().Replace("ي", "ی");
                }
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
            }  
        }

        protected void rdb_adamemoraje_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_adamemoraje.Checked)
                adamemorajepnl.Visible = true;
            guestpnl.Visible = false;
            naghspnl.Visible = false;
            termstatuspnl.Visible = false;
            chk_naghs.Checked = false;
            chk_termstatus.Checked = false;
            chk_takhir.Checked = false;
            
     
        }

        protected void rdb_guest_CheckedChanged(object sender, EventArgs e)
        {
            adamemorajepnl.Visible = false;
            naghspnl.Visible = false;
            termstatuspnl.Visible = false;
            guestpnl.Visible = true;
            chk_naghs.Checked = false;
            chk_termstatus.Checked = false;
            chk_takhir.Checked = false;
     
        }
        protected void rdb_enteghal_CheckedChanged(object sender, EventArgs e)
        {
            adamemorajepnl.Visible = false;
            guestpnl.Visible = false;
            naghspnl.Visible = false;
            termstatuspnl.Visible = false;
            chk_naghs.Checked = false;
            chk_termstatus.Checked = false;
            chk_takhir.Checked = false;
    
        }
        protected void rdb_takhir_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_takhir.Checked)
            {
                adamemorajepnl.Visible = false;
                guestpnl.Visible = false;
                naghspnl.Visible = false;
                rdb_adamemoraje.Enabled = false;
                rdb_adamemoraje.Checked = false;
                rdb_enteghal.Enabled = false;
                rdb_enteghal.Checked = false;
                rdb_guest.Enabled = false;
                rdb_guest.Checked = false;
                chk_naghs.Checked = false;
                rdb_moadelsazi.Checked = false;
                rdb_moadelsazi.Enabled = false;
            }
            else
            {
                chk_naghs.Enabled = true;
                rdb_adamemoraje.Enabled = true;
                rdb_enteghal.Enabled = true;
                rdb_guest.Enabled = true;
                rdb_moadelsazi.Enabled = true;
            }
        }

        protected void chk_naghs_CheckedChanged(object sender, EventArgs e)
        {
            adamemorajepnl.Visible = false;
            guestpnl.Visible = false;
            if (chk_naghs.Checked)
            {
                rdb_adamemoraje.Enabled = false;
                rdb_adamemoraje.Checked = false;
                rdb_enteghal.Enabled = false;
                rdb_enteghal.Checked = false;
                rdb_guest.Enabled = false;
                rdb_guest.Checked = false;
                chk_takhir.Enabled = false;
                chk_takhir.Checked = false;
                naghspnl.Visible = true;
                rdb_moadelsazi.Checked = false;
                rdb_moadelsazi.Enabled = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "", " Calendar.setup({inputField: 'date_input_1', button: 'date_btn_1',   ifFormat: '%Y/%m/%d', dateType: 'jalali',weekNumbers: false});", true);
            }
            else
            {
                naghspnl.Visible = false;
                ddl_naghs.SelectedValue = "0";
                date_input_1.Value = "0";
                chk_takhir.Enabled = true;
            }
               

            if (!chk_naghs.Checked && !chk_termstatus.Checked)
            {
                rdb_adamemoraje.Enabled = true;
                rdb_enteghal.Enabled = true;
                rdb_guest.Enabled = true;
                chk_takhir.Enabled = true;
                rdb_moadelsazi.Enabled = true;
            }
        }
        protected void chk_termstatus_CheckedChanged(object sender, EventArgs e)
        {
            adamemorajepnl.Visible = false;
            guestpnl.Visible = false;
            if (chk_termstatus.Checked)
            {
                termstatuspnl.Visible = true;
                rdb_adamemoraje.Enabled = false;
                rdb_adamemoraje.Checked = false;
                rdb_enteghal.Enabled = false;
                rdb_enteghal.Checked = false;
                rdb_guest.Enabled = false;
                rdb_guest.Checked = false;
                if(!chk_naghs.Checked)
                chk_takhir.Enabled = true;
                rdb_moadelsazi.Enabled = false;
            }
            else
            {
                termstatuspnl.Visible = false;
                ddl_VaziiatTerm.SelectedValue = "0";
            }
            if (!chk_naghs.Checked && !chk_termstatus.Checked)
            {
                rdb_adamemoraje.Enabled = true;             
                rdb_enteghal.Enabled = true;
                rdb_guest.Enabled = true;
                chk_takhir.Enabled = true;
                rdb_moadelsazi.Enabled = true;
            }
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ddl_Daneshkade.SelectedValue != "")
            {
                
                DataTable dtField = ERB.GetReshByDaneshkade(int.Parse(ddl_Daneshkade.SelectedValue));
                dtField.Columns[0].ReadOnly = false;
                for (int i = 0; i <= dtField.Rows.Count - 1; i++)
                {

                    dtField.Rows[i][0] = dtField.Rows[i][0].ToString().Replace("ي", "ی");
                }
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
            }
            else
            {
                DataTable dtField = CB.SelectAllField();
                dtField.Columns[0].ReadOnly = false;
                for (int i = 0; i <= dtField.Rows.Count - 1; i++)
                {

                    dtField.Rows[i][0] = dtField.Rows[i][0].ToString().Replace("ي", "ی");
                }
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
            }
        }

        protected void btn_simpleSearch_Click(object sender, EventArgs e)
        {
            if (txt_Family.Text == "")
            {
                txt_Family.Text = "0";
            }
            if (txt_IdMeli.Text == "")
            {
                txt_IdMeli.Text = "0";
            }
          
            if (txt_StCode.Text == "")
            {
                txt_StCode.Text = "0";
            }
            DataTable dtResault = ERB.GetInfoAllStudent(txt_StCode.Text, txt_Family.Text, "0", txt_IdMeli.Text, 0, 0, 0);
            if (dtResault.Rows.Count > 0)
            {
                grd_Student.DataSource = dtResault;
                grd_Student.DataBind();

            }
            else
            {
                rwd.RadAlert("رکوردی با مشخصات وارد شده یافت نشد", 0, 100, "پیام", "");
            }
            if (txt_Family.Text == "0")
            {
                txt_Family.Text = string.Empty;
            }
            if (txt_IdMeli.Text == "0")
            {
                txt_IdMeli.Text = string.Empty;
            }
           
            if (txt_StCode.Text == "0")
            {
                txt_StCode.Text = string.Empty;
            }
        }
        protected void grd_Student_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GridDataItem item = (GridDataItem)e.Item;
                TableCell cell_m = (TableCell)item["idd_meli"];
                TableCell cell_f = (TableCell)item["name"];
                txt_StCode.Text = e.CommandArgument.ToString();
                txt_IdMeli.Text = cell_m.Text;
                txt_Family.Text = cell_f.Text;
               
            }
        }

        protected void btn_advanceSearch_Click(object sender, EventArgs e)
        {
            grd_Student.Visible = false;
        }

        protected void View_rpt_ServerClick(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            img_ExportToExcel3.Visible = false;
            img_ExportToExcel4.Visible = false;
            img_ExportToExcel5.Visible = false;
            img_ExportToExcel6.Visible = false;
            img_ExportToExcel7.Visible = false;
            img_ExportToExcel8.Visible = false;
            img_ExportToExcel9.Visible = false;
            img_ExportToExcel10.Visible = false;
            if (ddl_Daneshkade.SelectedValue == "")
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                if (ddl_Degree.SelectedValue.ToString() == "")
                {
                    ddl_Degree.SelectedValue = "0";
                }
                if (ddl_Dorpar.SelectedValue == "")
                {
                    ddl_Dorpar.SelectedValue = "0";
                }
                if (ddl_Field.SelectedValue == "")
                {
                    ddl_Field.SelectedValue = "0";
                }              
                if (ddl_NimsalVorod.SelectedValue == "")
                {
                    ddl_NimsalVorod.SelectedValue = "0";
                }
                if (txt_SalVorod.Text == "")
                {
                    txt_SalVorod.Text = "0";
                }
                if (ddl_Sex.SelectedValue == "")
                {
                    ddl_Sex.SelectedValue = "0";
                }
                if (ddl_StatusStu.SelectedValue == "")
                {
                    ddl_StatusStu.SelectedValue = "0";
                }
                if (ddl_Term.SelectedValue == "")
                {
                    ddl_Term.SelectedValue = "0";
                }
                if (ddl_VaziiatTerm.SelectedValue == "")
                    ddl_VaziiatTerm.SelectedValue = "0";
                if (ddl_AcceptedStu.SelectedValue == "")
                {
                    ddl_AcceptedStu.SelectedValue = "0";
                }
                if (txt_StCode.Text == "")
                {
                    txt_StCode.Text = "0";
                }
                DataTable dtitems = new DataTable();
                dtitems.Columns.Add("items", typeof(int));
                DataRow dr;

                foreach (RadComboBoxItem chk_items in ddl_StatusStu.CheckedItems)
                {
                    if (chk_items.Checked)
                    {
                        dr = dtitems.NewRow();
                        dr["items"] = int.Parse(chk_items.Value);
                        dtitems.Rows.Add(dr);

                    }
                }
                if (dtitems.Rows.Count == 0)
                {
                    foreach (RadComboBoxItem chk_items in ddl_StatusStu.Items)
                    {
                        if (chk_items.Value != "0")
                        {
                            dr = dtitems.NewRow();
                            dr["items"] = int.Parse(chk_items.Value);
                            dtitems.Rows.Add(dr);
                        }
                    }
                }
                if (!rdb_guest.Checked && !rdb_adamemoraje.Checked && !chk_takhir.Checked && !chk_naghs.Checked && !chk_termstatus.Checked && !rdb_moadelsazi.Checked && !rdb_enteghal.Checked)
                {
                    
                    DataTable dtResaultGuest = LSBIB.GetstudentsGeneralInfo(dtitems, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_StCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue));
                    if (dtResaultGuest.Rows.Count == 0)
                    {
                        rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                    }
                    else
                    {
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportStudentGeneralInfoGroupByResh.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Students].[Sp_GetstudentsGeneralInfo]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[Sp_GetstudentsGeneralInfo]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Students].[Sp_GetstudentsGeneralInfo]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
                        rpt.CompiledReport.DataSources["[Students].[Sp_GetstudentsGeneralInfo]"].Parameters["@StCode"].ParameterValue = txt_StCode.Text;
                        rpt.CompiledReport.DataSources["[Students].[Sp_GetstudentsGeneralInfo]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[Sp_GetstudentsGeneralInfo]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Students].[Sp_GetstudentsGeneralInfo]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Students].[Sp_GetstudentsGeneralInfo]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Students].[Sp_GetstudentsGeneralInfo]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Students].[Sp_GetstudentsGeneralInfo]"].Parameters["@Vazkol"].ParameterValue = dtitems;

                        rpt.RegData(dtResaultGuest);

                        //rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                    }
                }
                else
                {

                    if (date_input_1.Value == "")
                        date_input_1.Value = "0";
                    // میهمانی
                    if (rdb_guest.Checked == true)
                    {
                        if (chk_guest_term.Checked)
                            Session["order"] = "16";
                        else
                            Session["order"] = "0";
                        DataTable dtResaultGuest = LSBIB.GetReportGuestStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_StCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(Session["order"].ToString()), int.Parse(ddl_Sex.SelectedValue), dtitems, int.Parse(ddl_AcceptedStu.SelectedValue), ddl_Term.SelectedValue);
                        if (dtResaultGuest.Rows.Count == 0)
                        {
                            rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                        }
                        else
                        {


                            img_ExportToExcel8.Visible = true;
                            this.StiWebViewer1.ResetReport();
                            StiWebViewer1.Visible = true;
                            StiReport rpt = new StiReport();
                            //StiPage page = rpt.Pages[0];
                            //StiDataBand dtb = new StiDataBand();
                            //StiHeaderBand headerBand = new StiHeaderBand();
                            //double pos = 0;
                            //double columnWidth = page.Width / 10;
                            //int nameIndex = 1;
                            //StiText hText = new StiText(new RectangleD(pos, 0, columnWidth, 0.5));
                            //hText = new StiText(new RectangleD(pos, 0, columnWidth, 0.5));

                            //hText.Text.Value = "تلفن";
                            //hText.HorAlignment = StiTextHorAlignment.Center;
                            //hText.Name = "HeaderText" + nameIndex.ToString();
                            //hText.Brush = new StiSolidBrush(Color.Orange);
                            //hText.Border.Side = StiBorderSides.All;
                            //headerBand.Components.Add(hText);

                            //StiText dataText = new StiText(new RectangleD(pos, 0, columnWidth, 0.5));

                            //dataText.Text = "{_Students___SP_GuestStudents_." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols("mobile") + "}";
                            //dataText.Name = "DataText" + nameIndex.ToString();
                            //dataText.Border.Side = StiBorderSides.All;

                            //dtb.Components.Add(dataText);

                            rpt.Load(Server.MapPath("../Report/ReportGuestStudents.mrt"));
                            rpt.ReportCacheMode = StiReportCacheMode.On;
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@StCode"].ParameterValue = txt_StCode.Text;
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Vazkol"].ParameterValue = dtitems;
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@order"].ParameterValue = int.Parse(Session["order"].ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GuestStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue.ToString();
                            rpt.RegData(dtResaultGuest);

                            //rpt.Dictionary.Synchronize();
                            //rpt.Show();
                            StiWebViewer1.Report = rpt;
                            StiWebViewer1.Visible = true;
                            //rpt.Print(true);
                        }
                    }




                    // عدم مراجعه ثبت نام
                    if (rdb_adamemoraje.Checked == true)
                    {

                        int type = 0;
                        if (rdb1.Checked)
                            type = 3;
                        if (rdb2.Checked)
                            type = 2;
                        if (rdb3.Checked)
                            type = 4;
                        if (rdb_1temadamemoraje.Checked)
                            type = 5;
                        if (rdb_2temadamemoraje.Checked)
                            type = 1;
                        if (type > 0)
                        {

                            DataTable dtLackOfStudents = LSBIB.GetReportLackOfStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_StCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), ddl_Term.SelectedValue, dtitems, type);
                            if (dtLackOfStudents.Rows.Count == 0)
                            {
                                rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                            }
                            else
                            {
                                //Report ..........  
                                this.StiWebViewer1.ResetReport();
                                img_ExportToExcel3.Visible = true;
                                StiWebViewer1.Visible = true;
                                StiReport rpt = new StiReport();
                                rpt.Load(Server.MapPath("../Report/ReportLackOfStudents.mrt"));
                                rpt.ReportCacheMode = StiReportCacheMode.On;
                                rpt.Dictionary.Databases.Clear();
                                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                                rpt.Compile();
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@StCode"].ParameterValue = txt_StCode.Text;
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Vazkol"].ParameterValue = dtitems;
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                                rpt.CompiledReport.DataSources["[Students].[SP_LackOfStudents]"].Parameters["@type"].ParameterValue = type;
                                rpt.RegData(dtLackOfStudents);
                                //rpt.Dictionary.Synchronize();
                                //rpt.Show();
                                StiWebViewer1.Report = rpt;
                                StiWebViewer1.Visible = true;
                                //rpt.Print(true);
                            }
                        }
                        else
                            rwd.RadAlert("هیج موردی انتخاب نشده است", 0, 100, "پیام", "");

                    }






                    // انتقالی
                    if (rdb_enteghal.Checked == true)
                    {

                        DataTable dtResaultTransfer = LSBIB.GetReportTransferStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_StCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), dtitems, int.Parse(ddl_AcceptedStu.SelectedValue), ddl_Term.SelectedValue);
                        if (dtResaultTransfer.Rows.Count == 0)
                        {
                            rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                        }
                        else
                        {
                            //Report..............
                            this.StiWebViewer1.ResetReport();
                            img_ExportToExcel9.Visible = true;
                            StiWebViewer1.Visible = true;
                            StiReport rpt = new StiReport();
                            rpt.Load(Server.MapPath("../Report/ReportTransferStudents.mrt"));
                            rpt.ReportCacheMode = StiReportCacheMode.On;
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@StCode"].ParameterValue = txt_StCode.Text;
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Vazkol"].ParameterValue = dtitems;
                            rpt.CompiledReport.DataSources["[Students].[SP_TransferStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue.ToString();
                            rpt.RegData(dtResaultTransfer);
                            StiWebViewer1.Report = rpt;
                            StiWebViewer1.Visible = true;
                        }
                    }

                    // نقص پرونده داشته باشد
                    if ((chk_naghs.Checked && chk_termstatus.Checked) || chk_naghs.Checked)
                    {
                        if (ddl_VaziiatTerm.SelectedValue == "")
                            ddl_VaziiatTerm.SelectedValue = "0";
                        if (ddl_naghs.SelectedValue == "")
                            ddl_naghs.SelectedValue = "0";

                        

                        DataTable dtIncompleteStudents = LSBIB.GetIncompleteStudents(dtitems, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_StCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), int.Parse(ddl_naghs.SelectedValue), date_input_1.Value, int.Parse(ddl_VaziiatTerm.SelectedValue), ddl_Term.SelectedValue);
                        if (dtIncompleteStudents.Rows.Count == 0)
                        {
                            rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");

                        }
                        else
                        {

                            //Report ............................
                            img_ExportToExcel2.Visible = true;
                            this.StiWebViewer1.ResetReport();
                            StiWebViewer1.Visible = true;
                            StiReport rpt = new StiReport();
                            rpt.Load(Server.MapPath("../Report/ReportIncompleteStudents.mrt"));
                            rpt.ReportCacheMode = StiReportCacheMode.On;
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));

                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@StCode"].ParameterValue = txt_StCode.Text;
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@Vazkol"].ParameterValue = dtitems;
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@naghsType"].ParameterValue = int.Parse(ddl_naghs.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@naghsDate"].ParameterValue = date_input_1.Value.ToString();
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@termstatus"].ParameterValue = int.Parse(ddl_VaziiatTerm.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[Sp_IncompleteStudents]"].Parameters["@term"].ParameterValue = ddl_Term.SelectedValue;
                            rpt.RegData(dtIncompleteStudents);
                            //rpt.Dictionary.Synchronize();
                            //rpt.Show();
                            StiWebViewer1.Report = rpt;
                            StiWebViewer1.Visible = true;
                            //rpt.Print(true);

                        }

                    }

                    if (chk_termstatus.Checked == true && !chk_naghs.Checked)
                    {

                        DataTable dtStatusTermStudent = LSBIB.GetStatusTermStudent(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_StCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), dtitems, ddl_Term.SelectedValue, int.Parse(ddl_VaziiatTerm.SelectedValue));
                        if (dtStatusTermStudent.Rows.Count == 0)
                        {
                            rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                        }
                        else
                        {
                            //Report ...........
                            this.StiWebViewer1.ResetReport();
                            img_ExportToExcel5.Visible = true;
                            StiWebViewer1.Visible = true;
                            StiReport rpt = new StiReport();
                            rpt.Load(Server.MapPath("../Report/ReportStatusTermStudents.mrt"));
                            rpt.ReportCacheMode = StiReportCacheMode.On;
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@StCode"].ParameterValue = txt_StCode.Text;
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Vazkol"].ParameterValue = dtitems;
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@VazTerm"].ParameterValue = int.Parse(ddl_VaziiatTerm.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_StatusTermStudents]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                            rpt.RegData(dtStatusTermStudent);
                            //rpt.Dictionary.Synchronize();
                            //rpt.Show();
                            StiWebViewer1.Report = rpt;
                            StiWebViewer1.Visible = true;
                            //rpt.Print(true);
                        }


                    }

                    if (rdb_moadelsazi.Checked)
                    {
                        //Report ............................
                        DataTable dtmoadelsazi = LSBIB.Moadelsazi(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_StCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue),dtitems, ddl_Term.SelectedValue);
                         if (dtmoadelsazi.Rows.Count == 0)
                         {
                             rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                         }
                         else
                         {
                             this.StiWebViewer1.ResetReport();
                             StiWebViewer1.Visible = true;
                             StiReport rpt = new StiReport();
                             rpt.Load(Server.MapPath("../Report/ReportStudentMoadelsazi.mrt"));
                             rpt.ReportCacheMode = StiReportCacheMode.On;
                             rpt.Dictionary.Databases.Clear();
                             rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));

                             rpt.Compile();
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@StCode"].ParameterValue = txt_StCode.Text;
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@Vazkol"].ParameterValue = dtitems;
                             rpt.CompiledReport.DataSources["[Students].[SP_Moadelsazi]"].Parameters["@term"].ParameterValue = ddl_Term.SelectedValue;
                             rpt.RegData(dtmoadelsazi);
                             //rpt.Dictionary.Synchronize();
                             //rpt.Show();
                             StiWebViewer1.Report = rpt;
                             StiWebViewer1.Visible = true;
                             //rpt.Print(true);
                         }
                    }
                    if (chk_takhir.Checked)
                    {
                        DataTable dtmoadelsazi = LSBIB.Sabtenambatakhir(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_StCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), dtitems, ddl_Term.SelectedValue,int.Parse(ddl_VaziiatTerm.SelectedValue));
                        if (dtmoadelsazi.Rows.Count == 0)
                        {
                            rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                        }
                        else
                        {
                            this.StiWebViewer1.ResetReport();
                            StiWebViewer1.Visible = true;
                            StiReport rpt = new StiReport();
                            rpt.Load(Server.MapPath("../Report/ReportStudentSabtenambatakhir.mrt"));
                            rpt.ReportCacheMode = StiReportCacheMode.On;
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));

                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@SalVorod"].ParameterValue = txt_SalVorod.Text;
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@StCode"].ParameterValue = txt_StCode.Text;
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@NimsalVorod"].ParameterValue = int.Parse(ddl_NimsalVorod.SelectedValue);
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@Sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@Paziresh"].ParameterValue = int.Parse(ddl_AcceptedStu.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@Vazkol"].ParameterValue = dtitems;
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@term"].ParameterValue = ddl_Term.SelectedValue;
                            rpt.CompiledReport.DataSources["[Students].[SP_GetStudentSabtenambatakhir]"].Parameters["@VazTerm"].ParameterValue = int.Parse(ddl_VaziiatTerm.SelectedValue);
                            rpt.RegData(dtmoadelsazi);
                            //rpt.Dictionary.Synchronize();
                            //rpt.Show();
                            StiWebViewer1.Report = rpt;
                            StiWebViewer1.Visible = true;
                            //rpt.Print(true);
                        }
                    }
                    if (Session["order"] != null)
                    {
                        Session["order"] = null;
                    }
                    if (txt_StCode.Text == "0")
                    {
                        txt_StCode.Text = string.Empty;
                    }
                    if (txt_SalVorod.Text == "0")
                    {
                        txt_SalVorod.Text = string.Empty;
                    }
                }
        }

        protected void img_ExportToExcel2_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void img_ExportToExcel3_Click(object sender, ImageClickEventArgs e)
        {
            //if (txt_StCode.Text == "")
            //{
            //    txt_StCode.Text = "0";
            //}
            //if (txt_SalVorod.Text == "")
            //    txt_SalVorod.Text = "0";
            //int type = 0;
            //if (rdb1.Checked)
            //    type = 3;
            //if (rdb2.Checked)
            //    type = 2;
            //if (rdb3.Checked)
            //    type = 4;
            //if (rdb_1temadamemoraje.Checked)
            //    type = 1;
            //if (rdb_2temadamemoraje.Checked)
            //    type = 5;
            //if (type > 0)
            //{
            //    DataTable dt = LSBIB.GetReportLackOfStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), txt_SalVorod.Text, txt_StCode.Text, int.Parse(ddl_NimsalVorod.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Sex.SelectedValue), int.Parse(ddl_AcceptedStu.SelectedValue), ddl_Term.SelectedValue, dt, type);
            //    if (dt.Rows.Count == 0)
            //    {
            //    }
            //    else
            //    {
            //        GridView3.DataSource = dt;
            //        GridView3.DataBind();
            //        Response.Clear();
            //        Response.Buffer = true;
            //        Response.AddHeader("content-disposition", "attachment;filename=ReportLackOfStudents.xls");
            //        Response.Charset = "";
            //        Response.ContentType = "application/vnd.ms-excel";
            //        using (StringWriter sw = new StringWriter())
            //        {
            //            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //            foreach (TableCell cell in GridView3.HeaderRow.Cells)
            //            {
            //                cell.BackColor = GridView3.HeaderStyle.BackColor;
            //            }
            //            foreach (GridViewRow row in GridView3.Rows)
            //            {
            //                foreach (TableCell cell in row.Cells)
            //                {
            //                    if (row.RowIndex % 2 == 0)
            //                    {
            //                        cell.BackColor = GridView3.AlternatingRowStyle.BackColor;
            //                    }
            //                    else
            //                    {
            //                        cell.BackColor = GridView3.RowStyle.BackColor;
            //                    }
            //                    cell.CssClass = "textmode";
            //                }
            //            }

            //            GridView3.RenderControl(hw);
            //            string style = @"<style> .textmode { } </style>";
            //            Response.Write(style);
            //            Response.Output.Write(sw.ToString());
            //            Response.Flush();
            //            Response.End();
            //        }
            //    }
            //}
        
        }

        protected void img_ExportToExcel4_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void img_ExportToExcel5_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void img_ExportToExcel6_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void img_ExportToExcel7_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void img_ExportToExcel8_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void img_ExportToExcel9_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void img_ExportToExcel10_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void rdb_moadelsazi_CheckedChanged(object sender, EventArgs e)
        {
            chk_naghs.Checked = false;
           
            chk_termstatus.Checked = false;
          
            chk_takhir.Checked = false;
           
            adamemorajepnl.Visible = false;
            naghspnl.Visible = false;
            termstatuspnl.Visible = false;
            guestpnl.Visible = false;
        }
        
        
    }
}