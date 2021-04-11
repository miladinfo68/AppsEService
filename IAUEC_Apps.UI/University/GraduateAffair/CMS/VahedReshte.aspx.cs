using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.DTO.University.Graduate;
using IAUEC_Apps.Business.university.GraduateAffair;
using IAUEC_Apps.Business.Common;
using System.Text;
using Telerik.Web.UI;


namespace IAUEC_Apps.UI.University.GraduateAffair.CMS
{
    public partial class VahedReshte : System.Web.UI.Page
    {
        //Declare Object and Variables
        DataTable dt = new DataTable();
        VahedReshteBusiness vrb = new VahedReshteBusiness();
        VahedReshteDTO vrd = new VahedReshteDTO();
        CommonBusiness cmnb = new CommonBusiness();

        int flag;

        const string msgSuccess = "ثبت اطلاعات با موفقيت انجام شد", msgError = "ثبت اطلاعات با خطا مواجه شده است";

        //Page Load
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
                hidePanel();
                cmbDataBind();
            }
        }

        //////////////////////////////////////////////////////button Events////////////////////////////////////////////////////////

        //btnOk for reshte-vahed 
        protected void btnOkRV_Click(object sender, EventArgs e)
        {
            string confirmValues = Request.Form["confirm_value"];
            confirmValues = confirmValues.Substring(confirmValues.Length - 2, 2);
            if (confirmValues != "No")
            {
                try
                {
                    vrd.Vahed = Convert.ToInt32(cmbVahedRV.SelectedItem.Value);
                    vrd.voroodi = Convert.ToInt32(txtVoroodiRV.Text);
                    dt = vrb.GetReshteVahed();
                    bool flgSuccess = true;
                    foreach (RadComboBoxItem checkeditem in cmbReshteRV.CheckedItems)
                    {
                        DataRow[] dr = dt.Select("[رشته]='"+ checkeditem.Text+"' and [ورودی]="+txtVoroodiRV.Text.Trim());
                        if (dr.Length == 0)
                        {
                            string values = checkeditem.Value; //looping through each checked item and accessing its value.
                            vrd.reshte = Convert.ToInt32(values);
                            int id=vrb.setvahedReshte(vrd);
                            setLog(DTO.eventEnum.ثبت_رشته_واحد, "",id);
                        }
                        else
                        {
                            string msg = string.Format("{0} {1} {2} {3} {4} {5} {6}", "شما قبلا برای رشته", checkeditem.Text, "و سال ورودی", txtVoroodiRV.Text, "واحد", dr[0]["واحد"].ToString(), "را انتخاب کرده بودید.");
                            RadWinMng.RadAlert(msg, 300, 100, "ویرایش اطلاعات", "");
                            flgSuccess = false;
                        }
                    }
                    if(flgSuccess)
                    RadWinMng.RadAlert(msgSuccess, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با موفقيت انجام شد.')", true);
                }
                catch (Exception ex)
                {
                    RadWinMng.RadAlert(msgError, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با خطا مواجه شده است.')", true);
                }
                bindGrid();
            }
        }

        protected void btnOkSV_Click(object sender, EventArgs e)
        {
            string confirmValues = Request.Form["confirm_value"];
            confirmValues = confirmValues.Substring(confirmValues.Length - 2, 2);
            if (confirmValues != "No")
            {
                try
                {
                    vrd.Vahed = Convert.ToInt32(cmbVahedSV.SelectedItem.Value);
                    vrd.semat = Convert.ToInt32(cmbSematSV.SelectedItem.Value);
                    vrd.name = txtNameSV.Text;
                    int id=vrb.setSematvahed(vrd);
                    setLog(DTO.eventEnum.ثبت_سمت_واحد, "", id);
                    //cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 101, "");
                    txtNameSV.Text = "";
                    RadWinMng.RadAlert(msgSuccess, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با موفقيت انجام شد.')", true);
                }
                catch (Exception x)
                {
                    RadWinMng.RadAlert(msgError, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با خطا مواجه شده است.')", true);
                }
                bindGrid();
            }
        }

        protected void btnOkSOV_Click(object sender, EventArgs e)
        {
            string confirmValues = Request.Form["confirm_value"];
            confirmValues = confirmValues.Substring(confirmValues.Length - 2, 2);
            if (confirmValues != "No")
            {
                flag = (int)ViewState["flag"];
                try
                {
                    if (flag == 3)
                    {
                        vrd.sematName = txtSematOrVahedSOV.Text;
                        int id=vrb.setSematInfo(vrd);
                        setLog(DTO.eventEnum.ثبت_سمت, "", id);
                        //cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 107, "");
                    }
                    else if (flag == 4)
                    {
                        vrd.VahedName = txtSematOrVahedSOV.Text;
                        int id=vrb.setVahedInfo(vrd);
                        setLog(DTO.eventEnum.ثبت_واحد, "", id);
                        //cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 99, "");
                    }
                    txtSematOrVahedSOV.Text = "";
                    RadWinMng.RadAlert(msgSuccess, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با موفقيت انجام شد.')", true);
                    bindGrid();
                    hidePanel();
                    pnlSematOrVahedSOV.Visible = true;
                    pnlResults.Visible = true;
                }
                catch (Exception)
                {
                    RadWinMng.RadAlert(msgError, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با خطا مواجه شده است.')", true);
                }
            }
        }

        protected void btnOkESOV_Click(object sender, EventArgs e)
        {
            string confirmValues = Request.Form["confirm_value"];
            confirmValues = confirmValues.Substring(confirmValues.Length - 2, 2);
            if (confirmValues != "No")
            {
                try
                {
                    flag = (int)ViewState["flag"];
                    if (flag == 3)
                    {
                        vrd.id = Convert.ToInt32(grdResults.SelectedRow.Cells[1].Text);
                        vrd.sematName = txtEditSematOrVahedESOV.Text;
                        vrb.updateSematInfo(vrd);
                        setLog(DTO.eventEnum.ویرایش_سمت, "مقدار قبلی : "+ grdResults.SelectedRow.Cells[2].Text, vrd.id);
                        //cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 108, "");
                    }
                    else if (flag == 4)
                    {
                        vrd.id = Convert.ToInt32(grdResults.SelectedRow.Cells[1].Text);
                        vrd.VahedName = txtEditSematOrVahedESOV.Text;
                        vrb.updateVahedInfo(vrd);
                        setLog(DTO.eventEnum.ویراش_واحد, "مقدار قبلی : "+ grdResults.SelectedRow.Cells[2].Text, vrd.id);
                        //cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 100, "");
                    }
                    pnlEditSematOrVahedESOV.Visible = false;
                    RadWinMng.RadAlert(msgSuccess, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با موفقيت انجام شد.')", true);
                    bindGrid();
                }
                catch (Exception)
                {
                    RadWinMng.RadAlert(msgError, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با خطا مواجه شده است.')", true);
                }
            }
        }

        protected void btnCancelESOV_Click(object sender, EventArgs e)
        {
            pnlEditSematOrVahedESOV.Visible = false;
            lblEditCaptionESOV.Text = string.Empty;
            txtEditSematOrVahedESOV.Text = string.Empty;
        }

        protected void btnOkERV_Click(object sender, EventArgs e)
        {
            string confirmValues = Request.Form["confirm_value"];
            confirmValues = confirmValues.Substring(confirmValues.Length - 2, 2);
            if (confirmValues != "No")
            {
                try
                {
                    vrd.voroodi = Convert.ToInt32(grdResults.SelectedRow.Cells[3].Text);
                    vrd.Vahed = Convert.ToInt32(cmbEditVahedERV.SelectedItem.Value);
                    vrd.reshte= Convert.ToInt32(grdResults.SelectedRow.Cells[4].Text);
                    vrb.updateReshteVahedInfo(vrd);
                    setLog(DTO.eventEnum.ویرایش_رشته_واحد, "مقدار واحد قبلی : "+ grdResults.SelectedRow.Cells[1].Text, vrd.id);
                    //cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 104, "");
                    pnlEditSematVahedESV.Visible = false;
                    RadWinMng.RadAlert(msgSuccess, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با موفقيت انجام شد.')", true);
                    hidePanel();
                    pnlReshteVahedRV.Visible = true;
                    pnlResults.Visible = true;
                }
                catch (Exception)
                {
                    RadWinMng.RadAlert(msgError, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با خطا مواجه شده است.')", true);
                }
                bindGrid();
            }
        }

        protected void btnCancelERV_Click(object sender, EventArgs e)
        {
            pnlEditReshteVahedERV.Visible = false;
            //txtEditVoroodiERV.Text = string.Empty;
        }

        protected void btnOkESV_Click(object sender, EventArgs e)
        {
            string confirmValues = Request.Form["confirm_value"];
            confirmValues = confirmValues.Substring(confirmValues.Length - 2, 2);
            if (confirmValues != "No")
            {
                vrd.VahedName = grdResults.SelectedRow.Cells[1].Text;
                vrd.sematName = grdResults.SelectedRow.Cells[2].Text;
                vrd.name = txtEditNameESV.Text;
                try
                {
                    vrb.updateSematVahedInfo(vrd);
                    setLog(DTO.eventEnum.ویرایش_سمت_واحد, "مقدار قبلی نام و نام خانوادگی : "+ grdResults.SelectedRow.Cells[3].Text, vrd.id);
                    //cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 102, "");
                    pnlEditReshteVahedERV.Visible = false;
                    RadWinMng.RadAlert(msgSuccess, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با موفقيت انجام شد.')", true);
                    hidePanel();
                    pnlSematVahedSV.Visible = true;
                    pnlResults.Visible = true;
                }
                catch (Exception)
                {
                    RadWinMng.RadAlert(msgError, 300, 100, "ویرایش اطلاعات", "");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با خطا مواجه شده است.')", true);
                }
                bindGrid();
            }
        }

        protected void btnCancelESV_Click(object sender, EventArgs e)
        {
            pnlEditSematVahedESV.Visible = false;
            txtEditNameESV.Text = string.Empty;
        } 

        ////////////////////////////////////////////////////End button Events//////////////////////////////////////////////////////
        protected void rdbReshVahed_CheckedChanged(object sender, EventArgs e)
        {
                cmbDataBind();
            txtVoroodiRV.Text = string.Empty;
            hidePanel();
            pnlReshteVahedRV.Visible = true;
            pnlResults.Visible = true;
            flag = 1;
            ViewState.Add("flag", flag);
            bindGrid();
        }

        protected void rdbSematVahed_CheckedChanged(object sender, EventArgs e)
        {
                cmbDataBind();
            txtNameSV.Text = string.Empty;
            hidePanel();
            pnlSematVahedSV.Visible = true;
            pnlResults.Visible = true;
            flag = 2;
            ViewState.Add("flag", flag);
            bindGrid();
        }

        protected void rdbSemat_CheckedChanged(object sender, EventArgs e)
        {
                cmbDataBind();
            lblCaptionSOV.Text = "افزودن سمت جدید:";
            txtSematOrVahedSOV.Text = string.Empty;
            hidePanel();
            pnlSematOrVahedSOV.Visible = true;
            pnlResults.Visible = true;
            flag = 3;
            ViewState.Add("flag", flag);
            bindGrid();
            
        }

        protected void rdbVahed_CheckedChanged(object sender, EventArgs e)
        {
                cmbDataBind();
            lblCaptionSOV.Text = "افزودن واحد جدید:";
            txtSematOrVahedSOV.Text = string.Empty;
            hidePanel();
            pnlSematOrVahedSOV.Visible = true;
            pnlResults.Visible = true;
            flag = 4;
            ViewState.Add("flag", flag);
            bindGrid();
        }
   
        protected void grdResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //select row in datagrid view
            if ((int)ViewState["flag"] == 1)
            {
                e.Row.Cells[4].Visible = false;
            }
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["ondblclick"] = ClientScript.GetPostBackClientHyperlink(this.grdResults, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "براي انتخاب سطر روی آن کليک کنيد.";
        }

        protected void cmbDataBind()
        {
            CmbUniDataBind();
            CmbReshteDataBind();
            CmbSematDataBind();
        }

        protected void CmbUniDataBind()
        {
            dt = vrb.getVahed();

            cmbVahedRV.DataSource = dt;
            cmbVahedRV.DataTextField = "vahed";
            cmbVahedRV.DataValueField = "id";
            cmbVahedRV.DataBind();

            cmbVahedSV.DataSource = dt;
            cmbVahedSV.DataTextField = "vahed";
            cmbVahedSV.DataValueField = "id";
            cmbVahedSV.DataBind();

            cmbEditVahedERV.DataSource = dt;
            cmbEditVahedERV.DataTextField = "vahed";
            cmbEditVahedERV.DataValueField = "id";
            cmbEditVahedERV.DataBind();

            //cmbEditVahedESV.DataSource = dt;
            //cmbEditVahedESV.DataTextField = "vahed";
            //cmbEditVahedESV.DataValueField = "id";
            //cmbEditVahedESV.DataBind();
        }

        protected void CmbReshteDataBind()
        {
            //dt = vrb.getReshte();
            dt= cmnb.SelectAllField();
            DataTable dt2 = dt.Copy();
            dt2.Columns.Add("nameresh_id");
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                string nameresh = dt.Rows[i]["nameresh"].ToString() + "-"+dt.Rows[i]["id"].ToString();
                dt2.Rows[i]["nameresh_id"] = nameresh;
            }
            cmbReshteRV.DataSource = dt2;
            cmbReshteRV.DataTextField = "nameresh_id";
            cmbReshteRV.DataValueField = "id";
            cmbReshteRV.DataBind();

            //cmbEditReshteERV.DataSource = dt;
            //cmbEditReshteERV.DataTextField = "nameresh";
            //cmbEditReshteERV.DataValueField = "id";
            //cmbEditReshteERV.DataBind();
        }

        protected void CmbSematDataBind()
        {
            dt = vrb.getSemat();

            cmbSematSV.DataSource = dt;
            cmbSematSV.DataTextField = "semat";
            cmbSematSV.DataValueField = "id";
            cmbSematSV.DataBind();

            //cmbEditSematESV.DataSource = dt;
            //cmbEditSematESV.DataTextField = "semat";
            //cmbEditSematESV.DataValueField = "id";
            //cmbEditSematESV.DataBind();
        }

        protected void bindGrid()
        {
            flag = (int)ViewState["flag"];
            if (flag == 1)
            {
                    dt = vrb.GetReshteVahed();
                    grdResults.DataSource = dt;
                    grdResults.DataBind();
            }
            else if (flag == 2)
            {
                dt = vrb.GetSematVahed();
                grdResults.DataSource = dt;
                grdResults.DataBind();
            }
            else if (flag == 3)
            {
                dt = vrb.getSemat();
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("کد شناسه");
                dt2.Columns.Add("سِمت");
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNew = dt2.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        drNew[i] = dr[i];
                    }
                    dt2.Rows.Add(drNew);
                }
                grdResults.DataSource = dt2;
                grdResults.DataBind();
            }
            else
            {
                dt = vrb.getVahed();
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("کد شناسه");
                dt2.Columns.Add("نام واحد");
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNew = dt2.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        drNew[i] = dr[i];
                    }
                    dt2.Rows.Add(drNew);
                }
                grdResults.DataSource = dt2;
                grdResults.DataBind();
            }
        }

        protected void hidePanel()
        {
            pnlReshteVahedRV.Visible = false;
            pnlSematVahedSV.Visible = false;
            pnlSematOrVahedSOV.Visible = false;
            pnlResults.Visible = false;
            pnlEditReshteVahedERV.Visible = false;
            pnlEditSematVahedESV.Visible = false;
            pnlEditSematOrVahedESOV.Visible = false;
        }

        protected void grdResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnEdit")
            {
                int rowInd = Convert.ToInt32(e.CommandArgument);
                grdResults.SelectedIndex = rowInd;
                showPanel();
            }
        }

        protected void showPanel()
        {
            flag = (int)ViewState["flag"];
            if (flag == 1)
            {
                cmbEditVahedERV.Text = grdResults.SelectedRow.Cells[1].Text;
                pnlEditReshteVahedERV.Visible = true;
            }
            else if (flag == 2)
            {
                //cmbEditVahedESV.Text = grdResults.SelectedRow.Cells[1].Text;
                //cmbEditSematESV.Text = grdResults.SelectedRow.Cells[2].Text;
                txtEditNameESV.Text = grdResults.SelectedRow.Cells[3].Text;
                pnlEditSematVahedESV.Visible = true;
            }
            else if (flag == 3)
            {
                lblEditCaptionESOV.Text = "ویراش سمت:";
                txtEditSematOrVahedESOV.Text = grdResults.SelectedRow.Cells[2].Text;


                pnlEditSematOrVahedESOV.Visible = true;
            }
            else
            {
                lblEditCaptionESOV.Text = "ویراش واحد:";
                txtEditSematOrVahedESOV.Text = grdResults.SelectedRow.Cells[2].Text;

                pnlEditSematOrVahedESOV.Visible = true;
            }
        }

        private void setLog(DTO.eventEnum eventType, string description,int modifyID)
        {
            CommonBusiness CB = new CommonBusiness();
            int userId;//کاربری که لاگین کرده
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی فارغ التحصیلان  -  10
            string Description = description;//توضیحات اختیاری

            userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            appId = 10;

            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)eventType, Description, modifyID);
        }
    }
}