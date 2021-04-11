using System;
using System.Linq;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using System.Data;
using IAUEC_Apps.DTO.CommonClasses;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutReport : System.Web.UI.Page
    {
        CheckOutReportBusiness chkReportBusiness = new CheckOutReportBusiness();
        UserAccessSectionDTO uasDTO = new UserAccessSectionDTO();
        CheckOutReportBusiness business = new CheckOutReportBusiness();
        LoginBusiness lngB = new LoginBusiness();


        DataTable dt = new DataTable();
        string userID;
        bool isAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //string mId = Request.QueryString["id"].ToString();
            //string[] id = mId.ToString().Split(new char[] { '@' });
            //string menuId = "";
            //for (int i = 0; i < id[1].Length; i++)
            //{
            //    string s = id[1].Substring(i + 1, 1);
            //    if (s != "-")
            //        menuId += s;
            //    else
            //        break;
            //    Session["MenuId"] = menuId;
            //}
            //AccessControl.MenuId = Session["MenuId"].ToString();
            //AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();        

            userID = Session[sessionNames.userID_Karbar].ToString();

            if (!IsPostBack)
            {
                DataTable userdt = lngB.Get_UserRoles(userID);
                isAdmin = userdt.Select().ToList().Exists(row => row["RoleId"].ToString() == "36");

                if (userdt.Rows[0]["RoleId"].ToString() == "1")
                {
                    isAdmin = true;
                }

                bindList();
            }
        }

        protected void bindList()
        {            
            dt = business.GetSection();

            cmbType.DataSource = dt;
            cmbType.DataValueField = "userId";
            cmbType.DataTextField = "Name";

            cmbType.DataBind();            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            uasDTO.userId = Convert.ToInt32(cmbType.SelectedValue.ToString());
            uasDTO.StartDate = txtFromDate.Text;
            uasDTO.EndDate = txtToDate.Text;

            dt = chkReportBusiness.GetRequest(uasDTO);

            grdResults.DataSource = dt;

            grdResults.DataBind();
        }        
    }
}