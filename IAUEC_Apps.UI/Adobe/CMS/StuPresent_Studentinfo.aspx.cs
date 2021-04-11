using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class StuPresent_Studentinfo : System.Web.UI.Page
    {
        StuPresentBusiness StuPresentBusiness = new StuPresentBusiness();
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
                }
                Session[sessionNames.menuID] = menuId;
                AccessControl1.MenuId = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
                // تمام نیمسال های قابل قبول را بر می گرداند
                List<string> TermList=StuPresentBusiness.GetActiveTerm();                
                foreach (var item in TermList)	            
                    ddl_Nimsal.Items.Add(item.ToString());
            }
            
        }

        protected void btn_StudentFinder_Click(object sender, EventArgs e)
        {
            DataTable DT = StuPresentBusiness.GetStudentInfo(txt_StudentCode.Text, txt_LastName.Text, txt_FirstName.Text, ddl_Nimsal.SelectedValue.ToString());
            grd_StuPreSentStuInfo.DataSource = DT;
            grd_StuPreSentStuInfo.DataBind();
        }




    }
}