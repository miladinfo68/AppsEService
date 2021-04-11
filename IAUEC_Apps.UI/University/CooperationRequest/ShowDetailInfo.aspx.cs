using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.DTO.University.Faculty;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.CooperationRequest.Reports
{
    public partial class ShowDetailInfo : System.Web.UI.Page
    {
        InfoPeople ip = new InfoPeople();

        CommonBusiness CB = new CommonBusiness();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        const string prvPage = "PreviewPageAddress";
        protected void Page_Load(object sender, EventArgs e)
        {
            //ListItem choose = new ListItem("انتخاب کنید");
            if (!IsPostBack)
            {
                Session[prvPage] = Request.UrlReferrer !=null? Request.UrlReferrer.ToString(): Session[prvPage];
                if ((!string.IsNullOrWhiteSpace(Request.QueryString["ID"].ToString())))
                {
                    addFieldsToControls();
                }
            }
        }

        private void addFieldsToControls()
        {
            DataTable dtResault = FRB.GetInfoPeoByFilter(int.Parse(Request.QueryString["ID"].ToString()));
            if (dtResault.Rows.Count > 0 && dtResault.Rows[0]["scan_document"] != DBNull.Value)
            {
                Div1.Visible = true;
                rlv.DataSource = dtResault;
                rlv.DataBind();
            }

            if (dtResault.Rows.Count == 0)
                return;
            addPersonalInfToControls(dtResault);
            addWorkInfoToControls(dtResault);
            addContactInfoToControls(dtResault);
            try
            {
                DataTable dt4 = FRB.GetFilePDF(int.Parse(Request.QueryString["ID"].ToString()));
                if (dt4.Rows.Count > 0)
                {
                    btn_ShowPDF.Visible = true;
                }
                else
                {
                    btn_ShowPDF.Visible = false;
                }

                if (lbl_Taeed2.Text != string.Empty)
                {
                    lbl_Taeed2.Text = string.Empty;
                }
            }
            catch (Exception ex1) { }

        }

        private void addPersonalInfToControls(DataTable dtResault)
        {

            DataTable cityList = CB.GetNameCity_fcoding();
            txtFirstName.Text = dtResault.Rows[0]["name"].ToString();
            txtFamily.Text = dtResault.Rows[0]["family"].ToString();
            txtFatherName.Text = dtResault.Rows[0]["namep"].ToString();
            txtShCode.Text = dtResault.Rows[0]["idd"].ToString();
            txtCodeMeli.Text = dtResault.Rows[0]["idd_meli"].ToString();
            if (dtResault.Rows[0]["sex"].ToString() == "1")
            {
                rdblGender.Items[0].Selected = true;
            }
            else if (dtResault.Rows[0]["sex"].ToString() == "2")
            {
                rdblGender.Items[1].Selected = true;
            }

            txtYearBorn.Text = dtResault.Rows[0]["sal_tav"].ToString();

            drpLastMaghta.SelectedValue = dtResault.Rows[0]["idmadrak"].ToString();
            if (dtResault.Rows[0]["IsRetired"].ToString() == "1")
            {
                chk_IsRetired.Checked = true;
            }

            drpUniName.DataSource = CB.GetNameUni_fcoding();
            drpUniName.DataTextField = "namecoding";
            drpUniName.DataValueField = "id";
            drpUniName.DataBind();
            drpUniName.Items.Add(new ListItem("انتخاب نشده", "0"));
            if (dtResault.Rows[0]["university"] != DBNull.Value)
                drpUniName.SelectedValue = dtResault.Rows[0]["university"].ToString();
            else
                drpUniName.SelectedValue = "0";


            drpReshte.DataSource = CB.SelectField_fcoding();
            drpReshte.DataTextField = "nameresh";
            drpReshte.DataValueField = "id";
            drpReshte.DataBind();
            drpReshte.Items.Add(new ListItem("سایر", "0"));
            if (dtResault.Rows[0]["idresh"] != DBNull.Value)
            {
                drpReshte.SelectedValue = dtResault.Rows[0]["idresh"].ToString();
            }
            else
                drpReshte.Items[drpReshte.Items.Count - 1].Selected = true;

            drpNezam.DataSource = CB.GetStatusMilitary_fcoding();
            drpNezam.DataTextField = "namecoding";
            drpNezam.DataValueField = "id";
            drpNezam.DataBind();
            drpNezam.Items.Add(new ListItem("سایر", "0"));
            drpNezam.SelectedValue = dtResault.Rows[0]["status_nezam"].ToString();


            DataTable countryTable = CB.GetNameCountry_fcoding();
            DataRow[] drSelectCountry = countryTable.Select("id<56");
            drpCountry.DataSource = drSelectCountry.CopyToDataTable();
            drpCountry.DataTextField = "namecoding";
            drpCountry.DataValueField = "id";
            drpCountry.DataBind();
            drpCountry.Items.Add(new ListItem("انتخاب نشده", "-1"));

            if (dtResault.Rows[0]["country"] == DBNull.Value)
            {
                drpCountry.Items[drpCountry.Items.Count - 1].Selected = true;
            }
            else
            {
                drpCountry.SelectedValue = dtResault.Rows[0]["country"].ToString();
            }
            drpUniversityType.SelectedValue = string.IsNullOrEmpty(dtResault.Rows[0]["MadrakUniType"].ToString()) ? "0" : dtResault.Rows[0]["MadrakUniType"].ToString();
            txtYearGetMadrak.Text = dtResault.Rows[0]["sal_madrak"].ToString();
            txtSanavat.Text = dtResault.Rows[0]["sanavat_tadris"].ToString();
            rdblMarriage.SelectedValue = dtResault.Rows[0]["marital_status"].ToString();
            if (dtResault.Rows[0]["marital_status"].ToString() == "1")
            {
                rdblMarriage.Items[0].Selected = true;
            }
            else
            {
                rdblMarriage.Items[1].Selected = true;
            }
            if (dtResault.Rows[0]["BimehTypeId"] != DBNull.Value)
            {
                if (Convert.ToInt16(dtResault.Rows[0]["BimehTypeId"]) > 0)
                {
                    rdblBimehStatus.SelectedValue = "1";
                    drpBimehType.SelectedValue = dtResault.Rows[0]["BimehTypeId"].ToString();
                    txtInsuranceNumber.Text = dtResault.Rows[0]["num_bime"].ToString();
                }

            }


            drpBirthCity.DataSource = cityList;
            drpBirthCity.DataTextField = "Title";
            drpBirthCity.DataValueField = "ID";
            drpBirthCity.DataBind();

            if (dtResault.Rows[0]["mahal_tav"].ToString() == "0")
            {
                drpBirthCity.Items.Add(new ListItem("انتخاب کنید", "0"));
                drpBirthCity.Items[drpBirthCity.Items.Count - 1].Selected = true;
            }
            else
            {
                drpBirthCity.SelectedValue = dtResault.Rows[0]["mahal_tav"].ToString();
            }

            drpMahalSodoor.DataSource = cityList;
            drpMahalSodoor.DataTextField = "Title";
            drpMahalSodoor.DataValueField = "ID";
            drpMahalSodoor.DataBind();

            if (dtResault.Rows[0]["mahal_sodor"].ToString() == "0")
            {
                drpMahalSodoor.Items.Add(new ListItem("انتخاب کنید", "0"));
                drpMahalSodoor.Items[drpMahalSodoor.Items.Count - 1].Selected = true;
            }
            else
            {
                drpMahalSodoor.SelectedValue = dtResault.Rows[0]["mahal_sodor"].ToString();
            }
            addDepratements(int.Parse(dtResault.Rows[0]["ID"].ToString()));

        }
        private void addWorkInfoToControls(DataTable dtResault)
        {
            ddlPastUniType.SelectedValue = string.IsNullOrEmpty(dtResault.Rows[0]["Uni_khedmatType"].ToString()) ? "0" : dtResault.Rows[0]["Uni_KhedmatType"].ToString();
            drpHireType.SelectedValue = dtResault.Rows[0]["type_estekhdam"].ToString();

            drpPastUni.DataSource = CB.GetNameUni_fcoding();
            drpPastUni.DataTextField = "namecoding";
            drpPastUni.DataValueField = "ID";
            drpPastUni.DataBind();
            drpPastUni.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            if (dtResault.Rows[0]["uni_khedmat"] != DBNull.Value)
                drpPastUni.SelectedValue = dtResault.Rows[0]["uni_khedmat"].ToString();
            else
                drpPastUni.SelectedValue = "0";

            ddlPastUniType.SelectedValue = string.IsNullOrEmpty(dtResault.Rows[0]["uni_khedmatType"].ToString()) ? "0" : dtResault.Rows[0]["uni_khedmatType"].ToString();
            if (dtResault.Rows[0]["nahveh_hamk"] != DBNull.Value)
                rdblHireType.SelectedValue = dtResault.Rows[0]["nahveh_hamk"].ToString();
            //if (dtResault.Rows[0]["nahveh_hamk"].ToString() == "44")
            //{
            //    rdblHireType.Items[0].Selected = true;
            //}
            //else if (dtResault.Rows[0]["nahveh_hamk"].ToString() == "8")
            //{
            //    rdblHireType.Items[2].Selected = true;
            //}
            //else if (dtResault.Rows[0]["nahveh_hamk"].ToString() == "32")
            //{
            //    rdblHireType.Items[1].Selected = true;
            //}
            //else if (dtResault.Rows[0]["nahveh_hamk"].ToString() == "12")
            //{
            //    rdblHireType.Items[3].Selected = true;
            //}

            txtDateSodoorHokm.Text = dtResault.Rows[0]["date_hokm"].ToString();
            txtDateEjraHokm.Text = dtResault.Rows[0]["date_runhokm"].ToString();
            txtHokmNumber.Text = dtResault.Rows[0]["number_hokm"].ToString();
            txtHokmAmount.Text = dtResault.Rows[0]["MablaghHokm"].ToString();

            if (dtResault.Rows[0]["martabeh"].ToString() == "-2" ||
                dtResault.Rows[0]["martabeh"].ToString() == "0" ||
                dtResault.Rows[0]["martabeh"].ToString() == "8")
            {
                dvHeiatElmi.Visible = false;
                pnlSabeghe.Enabled = false;
                lbl_HeiatElmi.Text = "8";
            }
            else
            {
                lbl_HeiatElmi.Text = String.Empty;
                dvHeiatElmi.Visible = true;
                pnlSabeghe.Enabled = true;

                drpMartabe.SelectedValue = dtResault.Rows[0]["martabeh"].ToString();

                txtPaye.Text = dtResault.Rows[0]["payeh"].ToString();

            }
            if (dtResault.Rows[0]["BoundHour"].ToString() == "1")
            {
                chkBoundHour.Checked = true;
            }
            if (dtResault.Rows[0]["Cooperation"].ToString() == "3")
            {
                chk_Cooperation.Items[0].Selected = true;
                chk_Cooperation.Items[1].Selected = true;
            }

            if (dtResault.Rows[0]["Cooperation"].ToString() == "1")
            {
                chk_Cooperation.Items[0].Selected = true;
            }

            if (dtResault.Rows[0]["Cooperation"].ToString() == "2")
            {
                chk_Cooperation.Items[1].Selected = true;
            }
        }
        private void addContactInfoToControls(DataTable dtResault)
        {
            txtHomePhone.Text = dtResault.Rows[0]["tel_home"].ToString();
            txtWorkPhone.Text = dtResault.Rows[0]["tel_kar"].ToString();
            txtMobileNumber.Text = dtResault.Rows[0]["mobile"].ToString();
            txtLivingAddress.Text = dtResault.Rows[0]["add_home"].ToString();
            txtLivingZipCode.Text = dtResault.Rows[0]["code_posti"].ToString();
            txtWorkingAddress.Text = dtResault.Rows[0]["add_kar"].ToString();
            txtEmail.Text = dtResault.Rows[0]["add_email"].ToString();


            drpProvince1.DataSource = CB.GetOstan();
            drpProvince1.DataTextField = "Title";
            drpProvince1.DataValueField = "ID";
            drpProvince1.DataBind();
            drpProvince1.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            if (dtResault.Rows[0]["code_ostan_home"] != DBNull.Value)
            {
                drpProvince1.SelectedValue = dtResault.Rows[0]["code_ostan_home"].ToString();
            }
            else
                drpProvince1.SelectedValue = "0";

            drpProvince2.DataSource = CB.GetOstan();
            drpProvince2.DataTextField = "Title";
            drpProvince2.DataValueField = "ID";
            drpProvince2.DataBind();
            drpProvince2.Items.Insert(0, new ListItem("انتخاب کنید", "0"));

            if (dtResault.Rows[0]["code_ostan_work"] != DBNull.Value)
            {


                drpProvince2.SelectedValue = dtResault.Rows[0]["code_ostan_work"].ToString();

            }
            else
            {
                drpProvince2.SelectedValue = "0";

            }
            drpLivingCity.DataSource = CB.getShahrestan(Convert.ToInt32(drpProvince1.SelectedValue));
            drpLivingCity.DataTextField = "Title";
            drpLivingCity.DataValueField = "ID";
            drpLivingCity.DataBind();
            drpLivingCity.Items.Insert(0, new ListItem("انتخاب کنید"));
            if (dtResault.Rows[0]["code_city_home"] != DBNull.Value)
            {
                drpLivingCity.SelectedValue = dtResault.Rows[0]["code_city_home"].ToString();
            }
            else
                drpLivingCity.SelectedValue = "0";

            drpWorkingCity.DataSource = CB.getShahrestan(Convert.ToInt32(drpProvince2.SelectedValue));
            drpWorkingCity.DataTextField = "Title";
            drpWorkingCity.DataValueField = "ID";
            drpWorkingCity.DataBind();
            drpWorkingCity.Items.Insert(0, new ListItem("انتخاب کنید"));
            if (dtResault.Rows[0]["code_city_work"] != DBNull.Value)
            {
                drpWorkingCity.SelectedValue = dtResault.Rows[0]["code_city_work"].ToString();

            }
            else
            {
                drpWorkingCity.SelectedValue = "0";

            }
            txtSiba.Text = dtResault.Rows[0]["siba"].ToString();

        }

        private void addDepratements(int infoPeopleId)
        {
            DataTable dtAllDaneshkade = new DataTable();
            DataTable dtDaneshkade = new DataTable();
            DataTable dtGroup = new DataTable();
            dtAllDaneshkade = CB.SelectAllDaneshkade();
            chbkDaneshkade.DataValueField = "id";
            chbkDaneshkade.DataTextField = "namedanesh";
            chbkDaneshkade.DataSource = dtAllDaneshkade;
            chbkDaneshkade.DataBind();

            dtGroup = FRB.GetGroupByCode(infoPeopleId);
            if (dtGroup.Rows.Count != 0)
            {
                string Resault = "idgroup in (";
                foreach (DataRow dr in dtGroup.Rows)
                {
                    Resault += dr["idgroup"].ToString() + "" + ",";
                }
                Resault += ")";
                string Field = Resault.Replace(",)", ")").Replace("(,", "(");
                dtDaneshkade = FRB.GetDaneshkadeByGroup(Field);

                Session["Field"] = Field;
                foreach (DataRow item in dtDaneshkade.Rows)
                {
                    chbkDaneshkade.Items.FindByValue(item["iddanesh"].ToString()).Selected = true;
                }
                if (chbkDaneshkade.SelectedIndex != -1)
                {
                    DataTable dtt = new DataTable();

                    foreach (ListItem itemm in chbkDaneshkade.Items)
                    {
                        if (itemm.Selected)
                        {
                            dtt.Merge(FRB.GetDepartmentList(Convert.ToInt32(itemm.Value)));
                        }
                    }
                    chbkGroup.DataSource = dtt;
                    chbkGroup.DataTextField = "namegroup";
                    chbkGroup.DataValueField = "idgroup";
                    chbkGroup.RepeatColumns = 4;
                    chbkGroup.RepeatDirection = RepeatDirection.Horizontal;
                    chbkGroup.DataBind();
                    List<string> departmanList = FRB.GetGroupList(infoPeopleId);


                    foreach (ListItem lch in chbkGroup.Items)
                    {
                        if (departmanList.Contains(lch.Value))
                        {
                            lch.Selected = true;
                        }
                    }

                    //foreach (string var in departmanList)
                    //{
                    //    if(dtt.Select("idgroup="+var).Length>0)
                    //    chbkGroup.Items.FindByValue(var).Selected = true;
                    //}
                }
            }
        }


        protected void btn_ShowPDF_Click(object sender, EventArgs e)
        {
            DataTable dt4 = FRB.GetFilePDF(int.Parse(Request.QueryString["ID"].ToString()));
            if (dt4.Rows.Count == 0)
            {
                rwd.RadAlert("رزومه ای بارگذاری نشده است", 0, 100, "پیام", "");
            }
            else
            {
                if (dt4.Rows[0]["ext"].ToString() == "jpg")
                {
                    Response.ContentType = "application/jpg";// doc.DOCUMENT_TYPE;
                }
                else
                {
                    Response.ContentType = "pdf";// doc.DOCUMENT_TYPE;
                }
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename=" + dt4.Rows[0]["name"].ToString() + " " + dt4.Rows[0]["family"].ToString() + "." + dt4.Rows[0]["ext"].ToString());
                Response.BinaryWrite((byte[])dt4.Rows[0]["scan_document"]);
                Response.Flush();
                Response.End();
            }
        }


        protected void rlv_ItemDataBound1(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
        {
            RadListViewDataItem row = e.Item as RadListViewDataItem;
            if (DataBinder.Eval(row.DataItem, "scan_document") == DBNull.Value)
                return;
            //HiddenField HF = (HiddenField)e.Item.FindControl("HiddenField1");
            Label lbl = (Label)e.Item.FindControl("lbl_Name");
            object var = row.DataItem.ToString();
            string myValue = DataBinder.Eval(row.DataItem, "ext").ToString();
            string result = myValue.ToLower();
            if (result.Contains("pdf"))
            {
                Button btn = (Button)e.Item.FindControl("btn_Madrak");
                btn.Visible = true;
                //HF.Value = DataBinder.Eval(row.DataItem, "doc_type").ToString();
                lbl.Text = DataBinder.Eval(row.DataItem, "document_name").ToString();
            }
            else
            {
                Button btn = (Button)e.Item.FindControl("btn_ShowPicture");
                btn.Visible = true;
                RadBinaryImage img = (RadBinaryImage)e.Item.FindControl("RadBinaryImage1");
                img.Visible = true;
                byte[] binaryData = (byte[])(DataBinder.Eval(row.DataItem, "scan_document"));
                img.DataValue = binaryData;
                //HF.Value = DataBinder.Eval(row.DataItem, "doc_type").ToString();
                lbl.Text = DataBinder.Eval(row.DataItem, "document_name").ToString();
            }

            if (DataBinder.Eval(row.DataItem, "s1").ToString() != "0")
            {
                RadioButton rdbTaeed = (RadioButton)e.Item.FindControl("rdb_Taeed");
                RadioButton rdbNaghs = (RadioButton)e.Item.FindControl("rdb_Naghs");
                TextBox txt = (TextBox)e.Item.FindControl("txt_Sharh");
                txt.Text = DataBinder.Eval(row.DataItem, "Description").ToString();
                if (DataBinder.Eval(row.DataItem, "s1").ToString() == "1")
                {
                    rdbTaeed.Checked = true;
                }
                else
                {
                    rdbNaghs.Checked = true;
                }
            }
        }

        protected void rlv_ItemCommand1(object sender, Telerik.Web.UI.RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] index = e.CommandArgument.ToString().Split(new char[] { ',' });
                var button = sender as Button;
                for (int i = 0; i < 19; i++)
                {
                    if (int.Parse(index[0].ToString()) == i)
                    {
                        DataTable dt4 = FRB.GetInfoPeoByFilterPDF(int.Parse(Request.QueryString["ID"].ToString()), int.Parse(index[0].ToString()));
                        Response.ContentType = "pdf";// doc.DOCUMENT_TYPE;
                        Response.Clear();
                        Response.AddHeader("content-disposition", "attachment; filename=" +dt4.Rows[0]["document_name"].ToString()+" "+ dt4.Rows[0]["name"].ToString() + " " + dt4.Rows[0]["family"].ToString() +  (dt4.Rows[0]["ext"].ToString().Contains(".")?"":".") + dt4.Rows[0]["ext"].ToString());
                        Response.BinaryWrite((byte[])dt4.Rows[0]["scan_document"]);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            if (e.CommandName == "ShowPic")
            {
                string[] index = e.CommandArgument.ToString().Split(new char[] { ',' });
                var button = sender as Button;
                for (int i = 0; i < 19; i++)
                {
                    if (int.Parse(index[0].ToString()) == i)
                    {
                        //Session["ID"] = Request.QueryString["ID"].ToString();
                        Response.Redirect("ShowPicture.aspx?" + "ID" + "=" + int.Parse(Request.QueryString["ID"].ToString()) + "&" + "TypePic" + "=" + int.Parse(index[0].ToString()));
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Session[prvPage].ToString());
        }
    }
}