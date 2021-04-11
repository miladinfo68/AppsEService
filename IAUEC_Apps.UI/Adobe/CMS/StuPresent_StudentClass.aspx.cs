using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using System.Data;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class StuPresent_StudentClass : System.Web.UI.Page
    {
        public DateTime TermTimeStart = new DateTime(2014, 09, 15);
        public DateTime TermTimeEnd = new DateTime(2015, 01, 06);
        StuPresentBusiness StuPresentBusiness = new StuPresentBusiness();
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //List<string> TermList = StuPresentBusiness.GetActiveTerm();
                //foreach (var item in TermList)
                    //ddl_Nimsal.Items.Add(item.ToString());
                ddl_Nimsal.DataSource = StuPresentBusiness.GetActiveTerm();
                ddl_Nimsal.DataBind();

                string stcode = "";
                string tterm = "";
                stcode = HttpUtility.ParseQueryString(this.ClientQueryString).Get("stcode");
                tterm = HttpUtility.ParseQueryString(this.ClientQueryString).Get("tterm");
                txt_Stcode.Text = stcode;
                ddl_Nimsal.SelectedValue = tterm;
                if (stcode != "" && stcode != null)
                    Result(stcode, tterm);
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

            }
           
        }

        protected void btn_StudentFinder_Click(object sender, EventArgs e)
        {
            Result(txt_Stcode.Text, ddl_Nimsal.SelectedValue.ToString());  
        }

        public void Result(string StudentCode, string tterm)
        {
            DataTable DT = StuPresentBusiness.TotalTimeResult(StudentCode, TermTimeStart, TermTimeEnd, tterm);
            if (DT.Rows.Count > 0)
            {
                grd_StuPresentStudentClass.DataSource = DT;
                grd_StuPresentStudentClass.DataBind();
            }
            else
            {
                string script = "alert(\" هیچ اطلاعاتی پیدا نشد \");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                DataTable DT0 = new DataTable();
                grd_StuPresentStudentClass.DataSource = DT0;
                grd_StuPresentStudentClass.DataBind();

            }
        }



    }
}