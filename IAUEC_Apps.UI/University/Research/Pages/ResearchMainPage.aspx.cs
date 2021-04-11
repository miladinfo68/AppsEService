using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.Business.university.Research;
using Telerik.Web;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;


namespace IAUEC_Apps.UI.University.Research.Pages
{
    public partial class ResearchMainPage : System.Web.UI.Page
    {
        
        RequestStudentCartBusiness stubausiness = new RequestStudentCartBusiness();
        ResearchBusiness Business = new ResearchBusiness();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            string usercode = Session[sessionNames.userID_StudentOstad].ToString();
            Business.university.Research.ResearchBusiness b = new Business.university.Research.ResearchBusiness();
            DataTable dtAllowEnterToPortal = b.PortalEntryPermition();
            var studentPermit = dtAllowEnterToPortal.Select("stcode='" + Session[sessionNames.userID_StudentOstad].ToString() + "'").Length > 0;
            
            //if (CommonBusiness.IsNumeric(usercode))
            //{
                DataTable dt = new DataTable();
               dt = Business.CheckStInSecondTerm(Session[sessionNames.userID_StudentOstad].ToString());
             DataTable stInfodt = new DataTable();
             stInfodt = stubausiness.GetStudentsInfo(usercode);
            //DataTable dtResh=new DataTable();
            //dtResh=Business.StInSomFields(int.Parse(stInfodt.Rows[0]["idresh"].ToString()));

            if (int.Parse(stInfodt.Rows[0]["grade"].ToString()) == 2 
                || int.Parse(stInfodt.Rows[0]["grade"].ToString()) == 1 
                || int.Parse(stInfodt.Rows[0]["grade"].ToString()) == 3 
                || int.Parse(stInfodt.Rows[0]["grade"].ToString()) == 8)
            {
                rwm_Message.RadAlert(" فقط دانشجویان ارشد و دکتری می توانند از این سیستم استفاده نمایند", null, 100, "خطا", "CallBackConfirm");
            }
            else
            {
                bool tempAccess = false;
                if ((int.Parse(dt.Rows[0][0].ToString()) > 1 || studentPermit) && usercode.Length<=9)
                {

                    //stInfodt = stubausiness.GetStudentsInfo(usercode);//etelaate daneshju khande mishavad

                    DataTable dts = new DataTable();
                    dts = Business.GetStudentFromStudentsByStcode(int.Parse(usercode));//check mikonad daneshju ra darad ya na
                    if (dts.Rows.Count == 0)//agar daneshju ra nadashtim ezafe kon
                    {
                        Business.InsertStudentInfoToStudents(int.Parse(usercode), stInfodt.Rows[0]["firstName"].ToString(), stInfodt.Rows[0]["lastName"].ToString()
                            , int.Parse(stInfodt.Rows[0]["enterYear"].ToString()), stInfodt.Rows[0]["nationalCode"].ToString(), int.Parse(stInfodt.Rows[0]["idresh"].ToString())
                            , int.Parse(stInfodt.Rows[0]["groupid"].ToString()), stubausiness.PersianCalander(), DateTime.Now.ToShortTimeString(), int.Parse(stInfodt.Rows[0]["grade"].ToString())
                            , stInfodt.Rows[0]["mobile"].ToString(), stInfodt.Rows[0]["homeAddress"].ToString(), int.Parse(stInfodt.Rows[0]["enterTerm"].ToString())
                            , stInfodt.Rows[0]["email"].ToString(), int.Parse(stInfodt.Rows[0]["gender"].ToString()), "def.png");
                    }
                    else
                    {
                        LoginBusiness logBusiness = new LoginBusiness();
                        logBusiness.updatePortalStudentInfo(Session[sessionNames.userID_StudentOstad].ToString());
                    }
                    e_Status.Value = "0";
                    form1.Action = "http://thesis.iauec.ac.ir/Index.aspx";
                    userCode.Value = usercode;

                    e_Code.Value = "iauec_unit503";
                    ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);
                }
                else
                {
                    rwm_Message.RadAlert(" دانشجویانی که در ترم اول می باشند و یا ورودی 1399 به بعد هستند، نمی توانند از این سیستم استفاده نمایند", null, 100, "خطا", "CallBackConfirm");

                }
            }
            
            
                
            
            

        }
    }
}