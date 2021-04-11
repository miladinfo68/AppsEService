using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.university.Students;
using IAUEC_Apps.DTO.University.Students;


namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class Military : System.Web.UI.Page
    {
        //Global objects and variables
        MilitaryStatusBusiness MSB = new MilitaryStatusBusiness();
        MilitaryStatusDTO msd = new MilitaryStatusDTO();

        DataTable dt = new DataTable();

        int index;
        bool alreadyExist;
        bool isEdit;
        string stCode;
        string family;
        int flag;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStCode.Focus();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtfamily.Text == string.Empty && txtStCode.Text == string.Empty) //at least one text box should have a value for search
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('حداقل بايد يکي از باکس ها را پر کنيد')", true);
            }
            else
            {
                stCode = txtStCode.Text;
                family = txtfamily.Text;
                ViewState.Add("stCode", stCode);
                ViewState.Add("family", family);
                if (txtfamily.Text == string.Empty)
                {
                    flag = 1;//means user write in stcode textbox
                }
                else if (txtStCode.Text == string.Empty)
                {
                    flag = 2;//means user write in family textbox
                }
                else
                {
                    flag = 0; //means user write in both textboxes
                }
                msd.stCode = txtStCode.Text;
                msd.family = txtfamily.Text;
                ViewState.Add("flag", flag);
                BindGrid();
            }

            //when user search new field edit label and panel must be hidden
            pnlSaveEdit.Visible = false;
            lblEdit.Visible = false;
        }

        protected void dgvResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //select row in datagrid view
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["ondblclick"] = ClientScript.GetPostBackClientHyperlink(this.grdResults, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "براي انتخاب سطر روی آن کليک کنيد.";
        }

        protected void dgvResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            stCode = grdResults.SelectedRow.Cells[2].Text;//get stCode from selected item in data gridview
            alreadyExist = isAlreadyExist();//if stCode already exist in database function 'isAlreadyExist' returns true

            //to save values of variables after page loading
            ViewState.Add("alreadyExist", alreadyExist);
            ViewState.Add("stCode", stCode);

            //if stCode already exist in data base edit menu shows else save menu shows at the bottom of data grid view
            if (alreadyExist)
            {
                showEditField();
            }
            else
            {
                showSaveField();
            }
        }

        protected void dgvResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //paging in data grid view
            grdResults.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void BindGrid()
        {
            flag = (int)ViewState["flag"];
            if (flag == 1)
            {
                msd.stCode = (string)ViewState["stCode"];
                msd.family = string.Empty;
            }
            else if (flag == 2)
            {
                msd.stCode = string.Empty;
                msd.family = (string)ViewState["family"];
            }
            else
            {
                msd.stCode = (string)ViewState["stCode"];
                msd.family = (string)ViewState["family"];
            }

            //to bind database with data gridview            
            dt = MSB.GetInfoSTUD(msd);
            ViewState.Add("dt", dt);
            grdResults.DataSource = dt;
            grdResults.DataBind();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //this is a little difficult :|
            //get value of botton that user clicks on it
            string confirmValues = Request.Form["confirm_value"];

            //Do nothing wen user click on cancel btn in alert message
            if (confirmValues == "Yes")
            {
                //is edit means that we are in edit view
                //to change value we must in save view
                //save view means we want to change value, save a new records or edit them
                isEdit = (bool)ViewState["isEdit"];

                if (isEdit)//if we are in edit view and the user click on edit botton and want to edit records
                {
                    isEdit = false;//because when page is loading we are not in edit view again and goto save view
                    ViewState.Add("isEdit", isEdit);//to save value after loading

                    //to edit text boxes
                    txtMashmulTarikh.ReadOnly = false;
                    txtMashmulNumber.ReadOnly = false;

                    //Change botton name
                    btnOk.Text = "ثبت";
                }
                else//if we are not in edit view do...
                {
                    //msd= (MilitaryStatusDTO)ViewState["msd"];
                    alreadyExist = (bool)ViewState["alreadyExist"];

                    try
                    {
                        if (alreadyExist)//if data is already exist in data base we should edit them
                        {
                            msd.stCode = (string)ViewState["stCode"];
                            msd.family = (string)ViewState["family"];
                            msd.mashmulNumber = txtMashmulNumber.Text;
                            msd.mashmulTarikh = txtMashmulTarikh.Text;
                            msd.mashmulStatus = drpStatus.SelectedItem.Value.ToString();

                            MSB.updateMashmulNumber(msd);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با موفقيت انجام شد.')", true);
                            txtMashmulTarikh.ReadOnly = true;
                            btnOk.Text = "ويرايش";
                            ViewState.Add("isEdit", true);
                        }
                        else//else we should to save new values in data base
                        {
                            string confirmValue = Request.Form["confirm_value"];//we can delete this line :|
                            if (confirmValue == "Yes")
                            {
                                msd.stCode = (string)ViewState["stCode"];
                                msd.family = (string)ViewState["family"];
                                msd.mashmulNumber = txtMashmulNumber.Text;
                                msd.mashmulTarikh = txtMashmulTarikh.Text;
                                msd.mashmulStatus = drpStatus.SelectedItem.Value.ToString();

                                MSB.insertMashmulNumber(msd);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با موفقيت انجام شد.')", true);

                                txtMashmulNumber.Text = dt.Rows[index]["شماره مشمولی"].ToString();
                                txtMashmulTarikh.Text = dt.Rows[index]["تاریخ مشمولی"].ToString();
                                alreadyExist = isAlreadyExist();
                                ViewState.Add("alreadyExist", alreadyExist);
                                showEditField();
                            }

                        }
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ثبت اطلاعات با خطا مواجه شده است.')", true);
                    }
                    BindGrid();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //to cancel save or edit data 
            pnlSaveEdit.Visible = false;
            lblEdit.Visible = false;
            txtMashmulNumber.Text = string.Empty;
            txtMashmulTarikh.Text = string.Empty;
        }

        protected void showSaveField()
        {
            //To show selected item's name in edit label
            string family = "ثبت اطلاعات برای دانشجو:" + grdResults.SelectedRow.Cells[3].Text + " " + grdResults.SelectedRow.Cells[4].Text;
            lblEdit.Text = family;
            lblEdit.Visible = true;

            ViewState.Add("isEdit", false);

            //For enable editing
            txtMashmulTarikh.ReadOnly = false;
            txtMashmulNumber.ReadOnly = false;

            //Clear text box to write new things
            txtMashmulNumber.Text = string.Empty;
            txtMashmulTarikh.Text = string.Empty;

            //Show panel and change the botton's name
            pnlSaveEdit.Visible = true;
            btnOk.Text = "ثبت";
        }

        protected void showEditField()
        {
            dt = (DataTable)ViewState["dt"];
            //To show selected item's name in edit label
            string family = "ويرايش اطلاعات براي دانشجو:" + grdResults.SelectedRow.Cells[3].Text + " " + grdResults.SelectedRow.Cells[4].Text;
            lblEdit.Text = family;
            lblEdit.Visible = true;

            //This means we are in edit view
            isEdit = true;
            ViewState.Add("isEdit", isEdit);

            //Show information of selected item to text boxes
            txtMashmulNumber.Text = dt.Rows[index]["شماره مشمولی"].ToString();
            txtMashmulTarikh.Text = dt.Rows[index]["تاریخ مشمولی"].ToString();
            drpStatus.SelectedValue = dt.Rows[index]["مجوز"].ToString();

            //When we are in edit view we cant change the value of text boxes so we change the read only property to false
            txtMashmulTarikh.ReadOnly = true;
            txtMashmulNumber.ReadOnly = true;

            //Show panel and change the botton's name
            pnlSaveEdit.Visible = true;
            btnOk.Text = "ويرايش";
        }

        protected bool isAlreadyExist()
        {
            dt = (DataTable)ViewState["dt"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["شماره دانشجویی"].ToString() == stCode)
                {
                    if (dt.Rows[i]["شماره مشمولی"].ToString() != string.Empty)
                    {
                        index = i;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}