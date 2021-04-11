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
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Research.CMS
{
    public partial class TransferTeacherInfo : System.Web.UI.Page
    {
        ResearchBusiness business = new ResearchBusiness();
        RequestStudentCartBusiness Cbusiness = new RequestStudentCartBusiness();
        
        CommonBusiness commonb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

            //DataTable dtost = new DataTable();
            //dtost = business.GetAllOstadInfo();
            //for (int i = 0; i < dtost.Rows.Count; i++)
            //{ 
            //    if(dtost.Rows[0]["code_ostad"].ToString()!="1010")
            //{
            //    business.insert_Ostad("از طریق سیستم", int.Parse("200" + dtost.Rows[i]["code_ostad"].ToString()), dtost.Rows[i]["name"].ToString(), dtost.Rows[i]["family"].ToString(),"", 0, dtost.Rows[i]["add_email"].ToString(), "", Cbusiness.PersianCalander(), DateTime.Now.ToShortTimeString(), 2,0,"0", 0, 1, 5, 5, 0, int.Parse(dtost.Rows[i]["sex"].ToString()), 0,0, 0, "", "", "", 0, 0, "", 0, "", 1, 0, "", "", dtost.Rows[i]["mobile"].ToString(), "", dtost.Rows[i]["idd_meli"].ToString(),0);
              
            //}

            //}
                
          

            //Label1.Text = "با موفقیت انجام شد";
        }

    }
}