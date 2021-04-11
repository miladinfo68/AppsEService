using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class TAGetExcelFile : System.Web.UI.Page
    {
        TABusiness TABusiness = new TABusiness();

        protected void Page_Load(object sender, EventArgs e)
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
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadExcelFile();
        }


        #region Web Form Designer generated code

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Upload.Click += new System.EventHandler(this.btnUpload_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        protected HtmlInputFile filMyFile;
        public void UploadExcelFile()
        {
            if (FileInput.PostedFile != null)
            {
                DataTable ExcelUsers = new DataTable();
                DataTable TAUsers = new DataTable();

                // Give File
                // دریافت اکسل فایل از کاربر
                string serverFileName = Path.GetFileName(FileInput.PostedFile.FileName);

                //Save File
                // ذخیره فایل روی سرور
                //string FilePath = HttpContext.Current.Server.MapPath("~") + "UploadFiles\\";
                string FilePath = HttpContext.Current.Server.MapPath("~");
                FileInput.PostedFile.SaveAs(MapPath("~") + serverFileName);

                //Read Excel File
                // DataTable خواندن محتوا و ریختن روی 
                ExcelUsers = TABusiness.ReadExcelFile(FilePath + serverFileName);

                //Delete File
                // حذف فایل اکسل از سرور
                File.Delete(FilePath + serverFileName);

                //Read Table Data
                // خواندن داده ها از جدول استادیار                
                TAUsers = TABusiness.GetTAUsers();

                //بروز رسانی در جدول
                TABusiness.CheckTAUsers(ExcelUsers, TAUsers);

                lbl_Info.Text = "Successful";
            }
            else
            {
                lbl_Info.Text = "try again";
            }
        }





    }
}