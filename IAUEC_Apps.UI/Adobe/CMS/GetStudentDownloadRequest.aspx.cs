using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class GetStudentDownloadRequest : System.Web.UI.Page
    {
        DownloadRequestBusiness DlBusiness = new DownloadRequestBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddl_term.Items.Insert(0, "انتخاب نمایید");
                ddl_term.Items.Insert(1, "90-91-1");
                ddl_term.Items.Insert(2, "90-91-2");
                ddl_term.Items.Insert(3, "91-92-1");
                ddl_term.Items.Insert(4, "91-92-2");
                ddl_term.Items.Insert(5, "92-93-1");
                ddl_term.Items.Insert(6, "92-93-2");
                ddl_term.Items.Insert(7, "93-94-1");
                ddl_term.Items.Insert(8, "93-94-2");
                ddl_term.Items.Insert(9, "94-95-1");
                ddl_term.Items.Insert(10, "94-95-2");
                ddl_term.Items.Insert(11, "95-96-1");
                ddl_term.Items.Insert(12, "95-96-2");
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
                AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
           
            //ddl_term.DataBind();
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = DlBusiness.GetStudentDownloadRequest(txt_StCode.Text, ddl_term.SelectedItem.Text);
            if (dt.Rows.Count > 0)
            {
                grd_StudentDownloadRequest.Visible = true;
                grd_StudentDownloadRequest.DataSource = dt;
                grd_StudentDownloadRequest.DataBind();
            }
            else
            {
                grd_StudentDownloadRequest.Visible = false;
                
                rwmValidations.RadAlert("موردی یافت نشد", null, 100, "پیام", "");
               
            }
        }

        protected void ddl_term_DataBinding(object sender, EventArgs e)
        {
           
        }
    }
}