using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.ResourceControlClasses;
using IAUEC_Apps.UI.University.GraduateAffair.CMS;
using ResourceControl.BLL;
using ResourceControl.PL.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class EducationAssistanceDefence : System.Web.UI.Page
    {
        /* AssistanceDefence 
     *status 0 ثبت شده
     *status 1  تایید شده
     *status 2 رد شده
     */
        private RequestHandler _reqHandler = new RequestHandler();
        private CommonBusiness commonBusiness = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
            drpAssistanceDefenceTypeList.SelectedValue = "-1";
            pt.Text = "درخواست مساعدت";
            }

        }
        public void rfrshDisplayAssistanceDefence(int status = -1)
        {
            DataTable dt = _reqHandler.GetAssistanceDefence("-1", status);

            if (dt != null && dt.Rows.Count > 0)
                grdDisplayAssistanceDefence.DataSource = dt;
            else
                grdDisplayAssistanceDefence.DataSource = string.Empty;
            GridFilterMenu menu = grdDisplayAssistanceDefence.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }

                }
            }


        }
        public bool AcceptOstad(DataRow row)
        {
            if (bool.Parse(row["FlagAcceptDavin"].ToString()) == true &&
                             bool.Parse(row["FlagAcceptDavOut"].ToString()) == true &&
                             bool.Parse(row["FlagAcceptMosh1"].ToString()) == true &&
                             bool.Parse(row["FlagAcceptMosh2"].ToString()) == true &&
                             bool.Parse(row["FlagAcceptRah1"].ToString()) == true &&
                             bool.Parse(row["FlagAcceptRah2"].ToString()) == true )
            {
                return true;
            }
            else
                return false;
        }
        public DataTable StatusDefence(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("statusDsc", typeof(System.String));
                foreach (DataRow row in dt.Rows)
                {
                    if (row != null)
                    {

                        if ((int.Parse(row["status"].ToString()) == 3) && bool.Parse(row["isDeleted"].ToString()) != true &&
                                (!AcceptOstad(row)) && row["RequestDate"].ToString().StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date)
                        {
                            row["statusDsc"] = "عدم تایید استاد";
                        }
                        else if ((int.Parse(row["status"].ToString()) == 0) &&
                            AcceptOstad(row) && bool.Parse(row["isDeleted"].ToString()) != true &&
                            (row["RequestDate"].ToString().StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date)&&
                            (row["DateRegistration"].ToString()!="" && DateTime.Parse(row["DateRegistration"].ToString())<= DateTime.Now.Date.AddDays(2))
                            )
                        {
                            row["statusDsc"] = "منتظر تایید استاد";
                        }
                        else if (bool.Parse(row["isDeleted"].ToString()) != true && AcceptOstad(row) &&
                                 (int.Parse(row["status"].ToString()) == (int)ResourceControlEnums.RequestDefenceStaus.submitted))
                        {
                            row["statusDsc"] = "منتظر تایید دانشکده";
                        }

                        else if (bool.Parse(row["isDeleted"].ToString()) != true && AcceptOstad(row) &&
                                 (int.Parse(row["status"].ToString()) == (int)ResourceControlEnums.RequestDefenceStaus.educationApprove))

                        {
                            row["statusDsc"] = "تایید دانشکده";
                        }
                        else if (bool.Parse(row["isDeleted"].ToString()) != true && AcceptOstad(row) &&
                        (int.Parse(row["status"].ToString()) == (int)ResourceControlEnums.RequestDefenceStaus.approved
                        ))

                        {
                            row["statusDsc"] = "تایید اداری";
                        }
                       else  if (bool.Parse(row["isDeleted"].ToString()) == true || int.Parse(row["status"].ToString()) == 3
                            && row["RequestDate"].ToString().StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date)
                        {
                            row["statusDsc"] = "حذف شده";
                        }
                        else if (
                        (int.Parse(row["status"].ToString()) == (int)ResourceControlEnums.RequestDefenceStaus.submitted
                        || int.Parse(row["status"].ToString()) == (int)ResourceControlEnums.RequestDefenceStaus.denied)
                        && row["RequestDate"].ToString().StringPersianDateToGerogorianDate().Date < DateTime.Now.Date)
                        {
                            row["statusDsc"] = "از دست رفته";
                        }
                        else
                        {
                            row["statusDsc"] = "نامشخص";
                        }

                    }
                    }
                }
            return dt;
        }
        protected void grdDisplayAssistanceDefence_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rfrshDisplayAssistanceDefence(int.Parse (drpAssistanceDefenceTypeList.SelectedValue));

        }

        protected void btnHistory_OnClick(object sender, ImageClickEventArgs e)
        {
            RequestHandler _reqHandler = new RequestHandler();
            ImageButton btn = (ImageButton)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string stcode = data["StudentCode"].Text;
            var dtLog = _reqHandler.GetLogMeetingDefences(stcode);
            dtLog= StatusDefence(dtLog);
            lst_history.DataSource = dtLog;// Rows;
            lst_history.DataBind();
            grdDisplayAssistanceDefence.Rebind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        protected void btnDenyRequest_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string stcode = data["StudentCode"].Text;
            string IdAssistance = data["id"].Text;
            hdnStcode.Value = stcode;
            hdnIdAssistance.Value = IdAssistance;

            string scrp = "function f(){$find(\"" + RadWindow21.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);

        }
        protected void drpRequestTypeList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            rfrshDisplayAssistanceDefence(Convert.ToInt32(drpAssistanceDefenceTypeList.SelectedValue));
            grdDisplayAssistanceDefence.Rebind();
        }

        protected void btnDenyRequest1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtDenyMessage1.Text.Trim()))
            {
                lblalertMessageDeny.Visible = true;
                return;
            }
            int status = 2;//rad shode
            string stcode = hdnStcode.Value;
            string idAssistance = hdnIdAssistance.Value;

            _reqHandler.Update_AssistanceDefence(stcode, status, txtDenyMessage1.Text);
         
            var userId = Session[sessionNames.userID_Karbar].ToString();
            commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                  , 11, 226
                  , "عدم اجازه به مساعدت دانشجو برای ثبت دفاع ", int.Parse(idAssistance));

            txtDenyMessage1.Text = string.Empty;
            string denyMessage = "مساعدت شماره " + idAssistance.ToString() + " لغو گردید.";
            RadWindowManager1.RadAlert(denyMessage, 300, 100, "پیام سیستم", "closeRadWindow3");
            RadWindow21.Visible = false;
            foreach (GridDataItem row in grdDisplayAssistanceDefence.Items)
            {
                Label lblidAssistance = (Label)row.FindControl("lblid") as Label;
                Label lblStudentCode = (Label)row.FindControl("lblstcode") as Label;
                if (lblidAssistance.Text == hdnIdAssistance.Value &&
                    lblStudentCode.Text == hdnStcode.Value)
                {
                    Button btnApprove = (Button)row.FindControl("btnApprove");
                    Button btnAvoid = (Button)row.FindControl("btnAvoid");
                    Label lblAccept = (Label)row.FindControl("lblAccept");
                    lblAccept.Visible = true;
                    btnAvoid.Visible = false;
                    btnApprove.Visible = false;
                }

            }

        }

        protected void btnApprove1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAcceptRequest.Text.Trim()))
            {
                lblAlertMessageAccept.Visible = true;
                return;
            }
            int status = 1;//taeed shode

            string stcode = hdnStcode.Value;
            string idAssistance = hdnIdAssistance.Value;

            _reqHandler.Update_AssistanceDefence(stcode, status, txtAcceptRequest.Text);
            var userId = Session[sessionNames.userID_Karbar].ToString();
            commonBusiness.InsertIntoUserLog(int.Parse(userId), DateTime.Now.ToString("HH:mm")
                  ,11, 225
                  , "اجازه به مساعدت دانشجو برای ثبت دفاع ", int.Parse(idAssistance));
            txtAcceptRequest.Text = string.Empty;
            string acceptMessage = "مساعدت شماره " + idAssistance.ToString() + " تایید گردید.";
            RadWindowManager1.RadAlert(acceptMessage, 300, 100, "پیام سیستم", "closeRadWindow4");
            RadWindow22.Visible = false;
             foreach(GridDataItem row in grdDisplayAssistanceDefence.Items )
            {
                Label lblidAssistance = (Label)row.FindControl("lblid") as Label;
                Label lblStudentCode = (Label)row.FindControl("lblstcode") as Label;
                if(lblidAssistance.Text== hdnIdAssistance.Value&&
                    lblStudentCode.Text == hdnStcode.Value)
                {
                    Button btnApprove = (Button)row.FindControl("btnApprove");
                    Button btnAvoid = (Button)row.FindControl("btnAvoid");
                    Label lblAccept= (Label)row.FindControl("lblAccept");
                    lblAccept.Visible = true;
                    btnAvoid.Visible = false;
                    btnApprove.Visible = false;
                  
                }

            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string stcode = data["StudentCode"].Text;
            string IdAssistance = data["id"].Text;
            hdnStcode.Value = stcode;
            hdnIdAssistance.Value = IdAssistance;
            string scrp = "function f(){$find(\"" + RadWindow22.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);

        

        }

        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            rfrshDisplayAssistanceDefence(Convert.ToInt32(drpAssistanceDefenceTypeList.SelectedValue));

            grdDisplayAssistanceDefence.DataSource = null;
            grdDisplayAssistanceDefence.Rebind();

        }
    }
}
