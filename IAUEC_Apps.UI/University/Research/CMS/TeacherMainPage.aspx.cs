using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.Business.university.Research;
using IAUEC_Apps.Business.university.Request;

namespace IAUEC_Apps.UI.University.Research.CMS
{
    public partial class TeacherMainPage : System.Web.UI.Page
    {
        ResearchBusiness Business = new ResearchBusiness(); 
        RequestStudentCartBusiness stubausiness = new RequestStudentCartBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            string usercode = Session[sessionNames.userID_StudentOstad].ToString();
            if (usercode != "1032" && usercode != "1017" && usercode != "1018" && usercode != "1033")
            {
            //DataTable dtost = new DataTable();
            //DataTable dtosfromthesis = new DataTable();
            ////اطلاعات استاد را از دیتابیس آموزش بر می گرداند
            //dtost = Business.GetOstadInfoByCodeOstad(int.Parse(usercode));
            //dtosfromthesis = Business.GetProfInfoByID(int.Parse("200"+usercode));
            //if (dtosfromthesis.Rows.Count == 0)
            //{
            //    //Business.InsertIntoOS_TB(int.Parse(dtost.Rows[0]["code_ostad"].ToString()), dtost.Rows[0]["family"].ToString(), dtost.Rows[0]["name"].ToString(), dtost.Rows[0]["nameresh"].ToString(), int.Parse(dtost.Rows[0]["martabeh"].ToString()), dtost.Rows[0]["idd_meli"].ToString(), dtost.Rows[0]["mobile"].ToString(), dtost.Rows[0]["add_email"].ToString(), stubausiness.PersianCalander(), DateTime.Now.ToShortTimeString(), int.Parse(dtost.Rows[0]["type_est"].ToString()), int.Parse(dtost.Rows[0]["sex"].ToString()), dtost.Rows[0]["name_mahal_tav"].ToString(), dtost.Rows[0]["sal_tav"].ToString(), dtost.Rows[0]["name_mahal_sodor"].ToString(), dtost.Rows[0]["idd"].ToString());
            //    Business.insert_Ostad("اتومات هنگام ورود", int.Parse("200" + usercode), dtost.Rows[0]["name"].ToString(), dtost.Rows[0]["family"].ToString(), dtost.Rows[0]["nameresh"].ToString(), 0, dtost.Rows[0]["add_email"].ToString(), "", stubausiness.PersianCalander(), DateTime.Now.ToShortTimeString(), 2, int
            //        .Parse(dtost.Rows[0]["martabeh"].ToString()),"0", 0, 1, 5, 5, 0, int.Parse(dtost.Rows[0]["sex"].ToString()), 0,0, 0, "", "", "", 0, 0, "", 0, "", 1, 0, "", "", dtost.Rows[0]["mobile"].ToString(), "", dtost.Rows[0]["idd_meli"].ToString(),0);
            //}
                usercode = "200" + usercode;
            }
            e_Status.Value = "1";
            //form1.Action = "../../../EmailReg/Pages/test.aspx";
            form1.Action ="http://thesis.iauec.ac.ir/Index.aspx";
            userCode.Value = usercode;
            e_Code.Value = "iauec_unit503";
            ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);  
                
        }
    }
}