using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.University.Request;
using Telerik.Web;
using Telerik.Web.UI;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class EditPersonalImage : System.Web.UI.Page
    {
        
        ///ایجاد نموده ایم EditPersonalInformationBusiness یک شئ از کلاس
        /// </summary>
        EditPersonalInformationBusiness EditBusiness = new EditPersonalInformationBusiness();
       
        /// <summary>
        ///ایجاد نموده ایم RequestStudentCartBusiness یک شئ از کلاس
        /// </summary>
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();
        /// <summary>
        /// اطلاعات دانشجو در این جدول ذخیره می گردد
        /// </summary>
        DataTable dt = new DataTable();
        /// <summary>
        ///عکس پرسنلی دانشجو در این جدول ذخیره می گردد
        /// </summary>
        DataTable dtn = new DataTable();
        /// <summary>
        /// اگر دانشجو درخواست تعویض کارت دانشجویی را داشته، عکس او در این جدول ذخیره می گردد
        /// </summary>
        DataTable dts = new DataTable();
        /// <summary>
        /// کلیه درخواست های ویرایش دانشجو در این جدول ذخیره می گردد
        /// </summary>
        public static DataTable dtm = new DataTable();
        /// <summary>
        /// جنانچه درخواست ویرایش عکس پرسنلی تایید شود مقدار این متغیر تورو می گردد
        /// </summary>
        public static bool picstatus = true;
        /// <summary>
        /// چنانچه دیگر درخواست های ویرایش تایید شود مقدار این متغیر تورو می شود و چنانچه رد شود مقدار فالس خواهد شد
        /// </summary>
        public static bool NewInfStatus = true;
        /// <summary>
        ///شماره تلفن جدید که توسط دانشجو وارد شده است در این متغیر قرار می گیرد
        /// </summary>
        public static string newtel;
        /// <summary>
        ///آدرس جدید که توسط دانشجو وارد شده است در این متغیر قرار می گیرد
        /// </summary>
        public static string newaddress;
        /// <summary>
        ///شماره موبایل جدید که توسط دانشجو وارد شده است در این متغیر قرار می گیرد
        /// </summary>
        public static string newmobile;
        /// <summary>
        ///کدپستی جدید که توسط دانشجو وارد شده است در این متغیر قرار می گیرد
        /// </summary>
        public static string newcodeposti;
        /// <summary>
        ///نام شهر جدید که توسط دانشجو وارد شده است در این متغیر قرار می گیرد
        /// </summary>
        public static int newshahr;
        /// <summary>
        ///نام استان جدید که توسط دانشجو وارد شده است در این متغیر قرار می گیرد
        /// </summary>
        public static int newostan;
        /// <summary>
        /// برای جدا کردن نام استان، شهر و آدرس دانشجو از این آرایه استفاده شده است
        /// </summary>
        public static string[] Addressarg;
        /// <summary>
        /// عکس دانشجو در این آرایه قرار می گیرد
        /// </summary>
        public static byte[] newimage;
        /// <summary>
        /// با لود شدن صفحه لیست هایی شامل اطلاعات جدید و قدیم دانشجو نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            rdb_newinfos();
            rdbpic();
            Session["stcode"] = Request.QueryString["stcode"].ToString();

            if (!IsPostBack)
            {

                dts = EditBusiness.GetStudentPic(Session["stcode"].ToString());

                if (dts.Rows.Count > 0)
                {
                    if (dts.Rows.Count > 0)
                    {

                        rdl_PersPic.DataSource = dts;
                        rdl_PersPic.DataBind();
                        foreach (RadListViewDataItem lvperspic in rdl_PersPic.Items)
                        {
                            RadBinaryImage img_PersonalPic = (RadBinaryImage)lvperspic.FindControl("img_PersonalPic");

                            img_PersonalPic.DataValue = (byte[])(dts.Rows[0]["PersonalImage"]);
                            rdl_PersPic.Visible = true;

                        }

                    }


                }

                dt = CartBusiness.GetStudentsInfo(Session["stcode"].ToString());
                rdlv_previous.DataSource = dt;
                rdlv_previous.DataBind();

                if (dt.Rows[0]["pic"].ToString() != "")
                {
                    newimage = (byte[])(dt.Rows[0]["pic"]);
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (RadListViewDataItem lvid in rdlv_previous.Items)
                    {

                        Label lbl_PersonalImgPrev = (Label)lvid.FindControl("lbl_PersonalImgPrev"); RadBinaryImage img_PersonalPicPrev = (RadBinaryImage)lvid.FindControl("img_PersonalPicPrev");
                        if (dt.Rows[0]["pic"].ToString() != "")
                        {
                            byte[] array = (byte[])(dt.Rows[0]["pic"]);
                            img_PersonalPicPrev.DataValue = array;
                            img_PersonalPicPrev.Visible = true;
                            lbl_PersonalImgPrev.Visible = true;
                        }

                    }

                }




            }
        }
        protected void btn_taeid_Click(object sender, EventArgs e)
        {
            Confirmpnl.Visible = true;


        }

        /// <summary>
        ///چنانچه کلید تایید اعمال تغییرات فشرده شود، نتیجه درخواست های ویرایش مبنی بر تایید یا رد شدن آنها، اعمال می گردد 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void conf_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();

            foreach (RadListViewDataItem lvperspic in rdl_PersPic.Items)
            {

                RadioButton rdb_ConfPic = (RadioButton)lvperspic.FindControl("rdb_ConfPic");
                RadioButton rdb_FailedPic = (RadioButton)lvperspic.FindControl("rdb_FailedPic");
                RadBinaryImage img_PersonalPic = (RadBinaryImage)lvperspic.FindControl("img_PersonalPic");
                TextBox txt_RadPic = (TextBox)lvperspic.FindControl("txt_RadPic");
                rdbpic();
                if (picstatus == true)
                {
                    EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 10, 7, "");
                    dtn = EditBusiness.GetEditedIdOfStcode(Session["stcode"].ToString());

                    EditBusiness.updateEditedStuImage(Session["stcode"].ToString(), (byte[])(dtn.Rows[0]["PersonalImage"]));
                    EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 10);
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 12, Session["stcode"].ToString());
                }
                if (picstatus == false)
                {
                    EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 10, 5, txt_RadPic.Text);
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 13, Session["stcode"].ToString());


                }

            }

            Response.Redirect("ConfirmEditPersonalInformationUI.aspx?id=" + generaterandomstr(11) + "@A" + Session[sessionNames.menuID].ToString() + "-" + generaterandomstr(2));


        }
        public string generaterandomstr(int count)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, count)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        /// <summary>
        /// چنانچه کاربر کلید خیر را بزند، تغییرات اعمال شده شامل رد یا تایید درخواست تغییرات خنثی می گردد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void notConf_Click(object sender, EventArgs e)
        {
            Confirmpnl.Visible = false;


            foreach (RadListViewDataItem lvperspic in rdl_PersPic.Items)
            {
                RadioButton rdb_ConfPic = (RadioButton)lvperspic.FindControl("rdb_ConfPic");
                RadioButton rdb_FailedPic = (RadioButton)lvperspic.FindControl("rdb_FailedPic");
                TextBox txt_RadPic = (TextBox)lvperspic.FindControl("txt_RadPic");

                txt_RadPic.Text = "";
                txt_RadPic.Visible = false;
                rdb_ConfPic.Checked = true;
                rdb_FailedPic.Checked = false;
            }
        }

        public void rdbpic()
        {
            foreach (RadListViewDataItem lvperspic in rdl_PersPic.Items)
            {
                RadioButton rdb_ConfPic = (RadioButton)lvperspic.FindControl("rdb_ConfPic");
                RadioButton rdb_FailedPic = (RadioButton)lvperspic.FindControl("rdb_FailedPic");
                TextBox txt_RadPic = (TextBox)lvperspic.FindControl("txt_RadPic");
                if (rdb_ConfPic.Checked)
                {
                    txt_RadPic.Visible = false;
                    picstatus = true;
                }
                if (rdb_FailedPic.Checked)
                {
                    txt_RadPic.Visible = true;
                    picstatus = false;
                }
            }

        }

        /// <summary>
        /// این متد جهت ذخیره نتیجه تایید یا رد درخواست ویرایش اطلاعات به جز عکس پرسنلی می باشد
        /// </summary>
        public void rdb_newinfos()
        {

        }
    }
}