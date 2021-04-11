using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Payment;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Adobe;
using Telerik.Web.UI;
using System.Configuration;
using IAUEC_Apps.UI.ir.shaparak.bpm;
using System.Data;

namespace IAUEC_Apps.UI.Adobe.MastePage
{
    public partial class PageAdobeMaster : System.Web.UI.MasterPage
    {
        DownloadRequestBusiness downloadreq = new DownloadRequestBusiness();
        public static readonly string PgwSite = ConfigurationManager.AppSettings["PgwSite"];
        public static readonly string CallBackUrl = ConfigurationManager.AppSettings["Mellat_CallBackUrl"];
        public static readonly string TerminalId = ConfigurationManager.AppSettings["Mellat_TerminalId"];
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"];
        public static readonly string UserPassword = ConfigurationManager.AppSettings["UserPassword"];

        public static string RefId = "";
        public static string PayDate = "";
        public static string PayTime = "";
      

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
        
            else
            {
           

                Label1.Text = Session[sessionNames.userID_StudentOstad].ToString();
                if (!IsPostBack)
                {
                    LoginBusiness lgnB = new LoginBusiness();
                    StuImg st = lgnB.User_Img(Label1.Text);
                    PersonalImage.DataValue = st.img;
                    LoginDTO stInfo = lgnB.Get_StInfo(Label1.Text);
                    stName.InnerText= stInfo.Name + " " + stInfo.LastName;
                    //stField.InnerText = stInfo.StReshte;
                    Reports rpt = new Reports();
                    List<ReportDownloadReqDTO> counter = new List<ReportDownloadReqDTO>();
                    int sum = 0;
                    counter = rpt.Get_SelectedAsset_NotPay(Label1.Text);
                    ShoppingCounter.InnerText = counter.Count.ToString();
                    grdShopping.DataSource = counter;
                    grdShopping.DataBind();
                    DownloadRequestBusiness dnlB = new DownloadRequestBusiness();
                    List<AssetDTO> assetlst = new List<AssetDTO>();
                    assetlst = dnlB.GetValidAssets(Label1.Text);
                    if (assetlst.Count > 0)
                        dnlNav.Visible = true;
                    if (grdShopping.MasterTableView.Items.Count > 0)
                    {
                        Paybtn.Visible = true;

                    }
                    foreach (GridDataItem item in grdShopping.MasterTableView.Items)
                    {
                        if (grdShopping.Columns[2].UniqueName == "SumPrice")
                        {
                            sum += int.Parse(item["SumPrice"].Text.Replace(",", ""));


                        }
                    }
                    Session["Fee"] = sum.ToString();

                }
                Session[sessionNames.userID_StudentOstad] = Label1.Text;
           
        }
    }

        protected void grdShopping_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Reports rpt = new Reports();
            int sum = 0;

            downloadreq.DeleteDownloadReq(Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RequestID"]));
            List<ReportDownloadReqDTO> lstdnl = new List<ReportDownloadReqDTO>();
            lstdnl = rpt.Get_SelectedAsset_NotPay(Session[sessionNames.userID_StudentOstad].ToString());
            grdShopping.DataSource = lstdnl;
            grdShopping.DataBind();
            RadListView lst = (RadListView)ContentPlaceHolder1.FindControl("lstSelectedClass");
            if (lst != null)
            {
                lst.DataSource = lstdnl;
                lst.DataBind();
            }
            //Pages.ConfirmCheckList chk = new Pages.ConfirmCheckList();
            //RadListView lst = (RadListView)chk.FindControl("lstClass");
            //lst.DataSource = lstdnl;
            //lst.DataBind();
            foreach (GridDataItem item in grdShopping.MasterTableView.Items)
            {
                if (grdShopping.MasterTableView.Items.Count > 0)
                {
                    Paybtn.Visible = true;
                }
                if (grdShopping.Columns[3].UniqueName == "SumPrice")
                {
                    sum += int.Parse(item["SumPrice"].Text.Replace(",", ""));


                }
            }
            Session["Fee"] = sum.ToString();
        }

        protected void exitButton_Click(object sender, EventArgs e)
        {

            if (Session[sessionNames.userID_StudentOstad].ToString() == "99900999")
            {
                Session["LogStatus"] = "0-0";
                LogStatus.Value = Session["LogStatus"].ToString();
                Response.Redirect("../../../University/LMS/Pages/LmsMain.aspx");
            }
            else
            {
                Session[sessionNames.userID_StudentOstad] = null;
                Session["Password"] = null;
                Session["LogStatus"] = "0-0";
                LogStatus.Value = Session["LogStatus"].ToString();
                form1.Action = "http://lms.iauec.ac.ir/Authentication.php";
                ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);
            }

           
        }
        protected void btn_home_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("../../CommonUI/IntroPage.aspx");
        }

        void SetDefaultDateTime()
        {
            PayDate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            PayTime = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }
        protected void Paybtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Pages/ConfirmCheckList.aspx");
            
        }
        protected void Returnhome_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("../../CommonUI/IntroPage.aspx");

        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {

            Session[sessionNames.userID_StudentOstad] = null;
            Session["LogStatus"] = "0-0";
            LogStatus.Value = Session["LogStatus"].ToString();
            Response.Redirect("../../University/LMS/Pages/LmsMain.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_StudentOstad] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
        }

 
    }
}