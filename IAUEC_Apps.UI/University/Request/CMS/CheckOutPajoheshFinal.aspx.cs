using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;
using System.Data;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutPajoheshFinal : System.Web.UI.Page
    {
        CheckOutPajooheshBusiness business = new CheckOutPajooheshBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }
        protected void btnShowList_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtStartDate.Text) && !String.IsNullOrWhiteSpace(txtEndDate.Text))
            {
                string startDate = txtStartDate.Text;
                string endDate = txtEndDate.Text;
                BindGrid(startDate, endDate);
                dvGridHolder.Visible = true;
                //RadWindow2.VisibleOnPageLoad = false;
            }
            
        }
        private void BindGrid(string startDate, string endDate)
        {
            grdFinalStudentList.DataSource = business.GetListOfFinalizedStudent(startDate,endDate);
            grdFinalStudentList.DataBind();
        }

        protected void grdFinalStudentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName=="submit")
            {
                //GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //txtDatePaperCancel1.Text = row.Cells[4].Text;
                //txtDateRecieveDocAccept1.Text = row.Cells[5].Text;
                //string stcode = e.CommandArgument.ToString();
                //ViewState.Add("stcode", stcode);
                //RadWindow2.VisibleOnPageLoad = true;
            }
        }

        protected void btnSubmitDates_Click(object sender, EventArgs e)
        {
            //string msg1;
            //string msg2;
            //string _stcode= ViewState["stcode"].ToString();
            //string userId =Session[sessionNames.userID_Karbar].ToString();
            //if (!String.IsNullOrWhiteSpace(_stcode))
            //{
            //    msg1 = business.Add_Cancel_Date(userId,_stcode, txtDatePaperCancel1.Text);
            //    msg2 = business.Add_Receive_Date(userId,_stcode, txtDateRecieveDocAccept1.Text);
            //    RadWindow2.VisibleOnPageLoad = false;
            //    string startDate = txtStartDate.Text;
            //    string endDate = txtEndDate.Text;
            //    BindGrid(startDate, endDate);
            //    RadWindowManager1.RadAlert("بروزرسانی انجام شد.", 0, 100, "پیام", "");
            //}            
        }

        protected void grdFinalStudentList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFinalStudentList.PageIndex = e.NewPageIndex;
            string startDate = txtStartDate.Text;
            string endDate = txtEndDate.Text;
            BindGrid(startDate, endDate);
        }

        protected void grdFinalStudentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                bool isFinilized = Convert.ToBoolean(rowView["IsFinalize"]);
                if (isFinilized)
                {
                    e.Row.Cells[7].Text = "بلی";
                }
                else
                {
                    e.Row.Cells[7].Text = "خیر";
                }
            }
        }        
    }
}