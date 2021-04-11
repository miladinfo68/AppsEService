using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class ConvertLogins : System.Web.UI.Page
    {
        DAO.ConverterDAO cdao = new DAO.ConverterDAO();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            var dt = cdao.GetAllStudentsLogins();

            foreach (DataRow row in dt.Rows)
                cdao.InsertStudentsLogin(row["stcode"].ToString(), CommonBusiness.EncryptPass(row["password_stu"].ToString()));
            lblResult.Text = "Students Inserted!";
            //foreach (DataRow row in dt.Rows)
            //    lblResult.Text += "$$" + row["stcode"] + ":" + CommonBusiness.DecryptPass(row["Password"].ToString()) + "$$";
        }

        protected void btnConvertProfessors_Click(object sender, EventArgs e)
        {
            var dt = cdao.GetAllProfessorsLogins();

            foreach (DataRow row in dt.Rows)
                cdao.InsertProfessorsLogin(row["ocode"].ToString(), CommonBusiness.EncryptPass(row["password_ost"].ToString()));
            lblResult.Text = "Professors Inserted!";
        }

        protected void btnManual_Click(object sender, EventArgs e)
        {
            lblOutput.Text = CommonBusiness.EncryptPass(txtInput.Text);
        }

        protected void btndoSomthing_Click(object sender, EventArgs e)
        {
            DAO.CommonDAO.CommonDAO commonDAO = new DAO.CommonDAO.CommonDAO();
            string query = @"Select i.id_info_peo,i.scan_document from hr.dbo.Tbl_ScanningDocumentsAndPhotos i
                                    where ext like '%pdf%' and status in(0, 1) and doc_type=10";
            var dt = commonDAO.doSomthing(query, "doSomthingEasilly");
            if (!System.IO.Directory.Exists("C://personellyPicture"))
                System.IO.Directory.CreateDirectory("C://personellyPicture");
            string fileName = "C://personellyPicture/";
            foreach (DataRow dr in dt.Rows)
            {
                 fileName= "C://personellyPicture/"+ dr["id_info_peo"].ToString()+".pdf";
                File.WriteAllBytes(fileName, (byte[])dr["scan_document"]);
            }
        }
    }
}