using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using IAUEC_Apps.Business.university.Students;

namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class RegistrationReport : System.Web.UI.Page
    {
        PaymentStatusAbdEnrollment sb = new PaymentStatusAbdEnrollment();
        protected void Page_Load(object sender, EventArgs e)
        {
            CommonBusiness CB = new CommonBusiness();
          
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
                    Session[sessionNames.menuID] = menuId;
                }
                AccessControl.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
                RadComboBoxItem cmbitem = new RadComboBoxItem();
                cmbitem.Text = "همه موارد";
                cmbitem.Value = "0";

             
                int daneshId = 0;
                switch (int.Parse(Session[sessionNames.roleID].ToString()))
                {
                    case 15:
                        daneshId = 2;
                        break;
                    case 16:
                        daneshId = 3;
                        break;
                    case 17:
                        daneshId = 1;
                        break;
                    case 26:
                        daneshId = 2;
                        break;
                    case 27:
                        daneshId = 3;
                        break;
                    case 28:
                        daneshId = 1;
                        break;
                    case 67:
                        daneshId = 8;
                        break;
                    case 66:
                        daneshId = 8;
                        break;
                    default:
                        break;
                }


                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = CB.SelectAllDaneshkade(daneshId);
                ddl_Daneshkade.DataBind();


                if (daneshId > 0)
                    ddl_Daneshkade.SelectedValue = daneshId.ToString();
                else
                    ddl_Daneshkade.Items.Add(cmbitem);
            }
        }

        protected void View_rptt_Click(object sender, EventArgs e)
        {
            if (ddl_Daneshkade.SelectedIndex>0)
            {
                
            
            DataTable dt = sb.GetStudentRegistrationReport(int.Parse(ddl_Daneshkade.SelectedValue));
            try
            {

                var pck = new OfficeOpenXml.ExcelPackage();
                var ws = pck.Workbook.Worksheets.Add("ProfInfoList");
                //foreach (var dt. in collection)
                //{

                //}
                ws.Cells["A1"].LoadFromDataTable(dt, true);
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=ProfInfoList.xlsx");

                Response.BinaryWrite(pck.GetAsByteArray());
                //dt.Columns.Add("StudentID", typeof(int));
                //dt.Columns.Add("StudentName", typeof(string));
                //dt.Columns.Add("RollNumber", typeof(int));
                //dt.Columns.Add("TotalMarks", typeof(int));
            }
            catch (Exception ex)
            {
                //log error
            }
            Response.End();
            }
        }

     
    }
}