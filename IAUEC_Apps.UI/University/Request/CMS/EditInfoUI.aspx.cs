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
using IAUEC_Apps.DTO.CommonClasses;
using Telerik.Web;
using Telerik.Web.UI;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class EditInfoUI : System.Web.UI.Page
    {
       
        /// <summary>
        ///ایجاد نموده ایم EditPersonalInformationBusiness یک شئ از کلاس
        /// </summary>
        EditPersonalInformationBusiness EditBusiness = new EditPersonalInformationBusiness();
       
       
        /// <summary>
        ///ایجاد نموده ایم RequestStudentCartBusiness یک شئ از کلاس
        /// </summary>
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();

        CommonBusiness cb = new CommonBusiness();

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
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
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
                newmobile = dt.Rows[0]["mobile"].ToString();
                newaddress = dt.Rows[0]["homeAddress"].ToString();
                newtel = dt.Rows[0]["homePhone"].ToString();
                newcodeposti = dt.Rows[0]["home_postalCode"].ToString();
                if (dt.Rows[0]["pic"].ToString() != "")
                {
                    newimage = (byte[])(dt.Rows[0]["pic"]);
                }

                newostan = int.Parse(dt.Rows[0]["home_state"].ToString());
                newshahr =int.Parse(dt.Rows[0]["home_city"].ToString());
                if (dt.Rows.Count > 0)
                {
                    foreach (RadListViewDataItem lvid in rdlv_previous.Items)
                    {
                        Label lbl_CodePostiPrev = (Label)lvid.FindControl("lbl_CodePostiPrev");
                        Label lbl_AddressPrev = (Label)lvid.FindControl("lbl_AddressPrev");
                        Label lbl_TellPrev = (Label)lvid.FindControl("lbl_TellPrev");
                        Label lbl_PersonalImgPrev = (Label)lvid.FindControl("lbl_PersonalImgPrev");
                        Label lbl_MobilePrev = (Label)lvid.FindControl("lbl_MobilePrev");
                        RadBinaryImage img_PersonalPicPrev = (RadBinaryImage)lvid.FindControl("img_PersonalPicPrev");
                        if (dt.Rows[0]["pic"].ToString() != "")
                        {
                            byte[] array = (byte[])(dt.Rows[0]["pic"]);
                            img_PersonalPicPrev.DataValue = array;
                            img_PersonalPicPrev.Visible = true;
                            lbl_PersonalImgPrev.Visible = true;
                        }

                    }

                }



                dtm = EditBusiness.GetSTudentEditRequest(Session["stcode"].ToString());//*darkhasthaye edite yek daneshju ra barmigardanad*
                foreach (RadListViewDataItem lst in rdlv_New.Items)
                {
                    RadioButton rdb_ConfNewInfo = (RadioButton)lst.FindControl("rdb_ConfNewInfo");
                    RadioButton rdb_NewInfo = (RadioButton)lst.FindControl("rdb_NewInfo");
                    Label lbl_EditedID = (Label)lst.FindControl("lbl_EditedID");
                }
                rdlv_New.DataSource = dtm;
                rdlv_New.DataBind();
               
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/login.aspx",false);
         

        }

        /// <summary>
        /// با فشردن کلید تایید، پنل اعمال تغییرات ظاهر می گردد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
            foreach (RadListViewDataItem lvperspic in rdl_PersPic.Items)
            {
                RadioButton rdb_ConfPic = (RadioButton)lvperspic.FindControl("rdb_ConfPic");
                RadioButton rdb_FailedPic = (RadioButton)lvperspic.FindControl("rdb_FailedPic");
                RadBinaryImage img_PersonalPic = (RadBinaryImage)lvperspic.FindControl("img_PersonalPic");
                TextBox txt_RadPic = (TextBox)lvperspic.FindControl("txt_RadPic");
                Label lblReqId = (Label)lvperspic.FindControl("lblReqId");
                rdbpic();
                if (picstatus == true)
                {
                    EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 10, 7, "");
                    dtn = EditBusiness.GetEditedIdOfStcode(Session["stcode"].ToString());

                    EditBusiness.updateEditedStuImage(Session["stcode"].ToString(), (byte[])(dtn.Rows[0]["PersonalImage"]));
                    EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 10);
                    cb.InsertIntoUserLog((int)Session[sessionNames.userID_Karbar], DateTime.Now.ToString("HH:mm"), 4, (int)eventEnum.تاييد_درخواست_ويرايش_عکس_پرسنلی, "", Convert.ToInt32(lblReqId.Text));
                }
                if (picstatus == false)
                {
                    EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 10, 5, txt_RadPic.Text);
                    cb.InsertIntoUserLog((int)Session[sessionNames.userID_Karbar], DateTime.Now.ToString("HH:mm"), 4, (int)eventEnum.رد_درخواست_ويرايش_عکس_پرسنلی, "", Convert.ToInt32(lblReqId.Text));
                }
            }
            foreach (RadListViewDataItem lst in rdlv_New.Items)
            {
                RadioButton rdb_ConfNewInfo = (RadioButton)lst.FindControl("rdb_ConfNewInfo");
                RadioButton rdb_NewInfo = (RadioButton)lst.FindControl("rdb_NewInfo");
                Label lbl_EditedID = (Label)lst.FindControl("lbl_EditedID");
                Label lblReqId = (Label)lst.FindControl("lblReqId");
                Label lbl_NewContent = (Label)lst.FindControl("lbl_NewContent");
                TextBox txt_RadEdit = (TextBox)lst.FindControl("txt_RadEdit");

                if (rdb_ConfNewInfo.Checked)
                {
                    if (int.Parse(lbl_EditedID.Text) == 6)
                    {
                        newtel = lbl_NewContent.Text;
                        EditBusiness.UpdateIsOk(Session["stcode"].ToString(), int.Parse(lbl_EditedID.Text), 7, "");
                        EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 6);
                        cb.InsertIntoUserLog((int)Session[sessionNames.userID_Karbar], DateTime.Now.ToString("HH:mm"), 4, (int)eventEnum.تاييد_درخواست_ويرايش_اطلاعات_فردي, "", Convert.ToInt32(lblReqId.Text));
                    }
                    if (int.Parse(lbl_EditedID.Text) == 9)
                    {
                        newmobile = lbl_NewContent.Text;
                        EditBusiness.UpdateIsOk(Session["stcode"].ToString(), int.Parse(lbl_EditedID.Text), 7, "");
                        cb.InsertIntoUserLog((int)Session[sessionNames.userID_Karbar], DateTime.Now.ToString("HH:mm"), 4, (int)eventEnum.تاييد_درخواست_ويرايش_اطلاعات_فردي, "", Convert.ToInt32(lblReqId.Text));
                        EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 9);
                    }
                    if (int.Parse(lbl_EditedID.Text) == 8)
                    {
                        newcodeposti = lbl_NewContent.Text;
                        EditBusiness.UpdateIsOk(Session["stcode"].ToString(), int.Parse(lbl_EditedID.Text), 7, "");
                        cb.InsertIntoUserLog((int)Session[sessionNames.userID_Karbar], DateTime.Now.ToString("HH:mm"), 4, (int)eventEnum.تاييد_درخواست_ويرايش_اطلاعات_فردي, "", Convert.ToInt32(lblReqId.Text));
                        EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 8);
                    }
                    if (int.Parse(lbl_EditedID.Text) == 7)
                    {
                        Addressarg = lbl_NewContent.Text.Split(new char[] { ',' });
                        newostan = int.Parse(Addressarg[0].ToString());
                        newshahr =int.Parse(Addressarg[1].ToString());
                        newaddress = Addressarg[2];
                        EditBusiness.UpdateIsOk(Session["stcode"].ToString(), int.Parse(lbl_EditedID.Text), 7, "");
                        cb.InsertIntoUserLog((int)Session[sessionNames.userID_Karbar], DateTime.Now.ToString("HH:mm"), 4, (int)eventEnum.تاييد_درخواست_ويرايش_اطلاعات_فردي, "", Convert.ToInt32(lblReqId.Text));
                        EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 7);
                    }

                }

                if (rdb_NewInfo.Checked)
                {
                    if (int.Parse(lbl_EditedID.Text) == 6)
                    {
                        EditBusiness.UpdateIsOk(Session["stcode"].ToString(), int.Parse(lbl_EditedID.Text), 5, txt_RadEdit.Text);
                        cb.InsertIntoUserLog((int)Session[sessionNames.userID_Karbar], DateTime.Now.ToString("HH:mm"), 4, (int)eventEnum.رد_درخواست_ويرايش_اطلاعات_فردي, "", Convert.ToInt32(lblReqId.Text));
                        EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 5, int.Parse(lbl_EditedID.Text));
                    }
                    if (int.Parse(lbl_EditedID.Text) == 7)
                    {
                        EditBusiness.UpdateIsOk(Session["stcode"].ToString(), int.Parse(lbl_EditedID.Text), 5, txt_RadEdit.Text);
                        cb.InsertIntoUserLog((int)Session[sessionNames.userID_Karbar], DateTime.Now.ToString("HH:mm"), 4, (int)eventEnum.رد_درخواست_ويرايش_اطلاعات_فردي, "", Convert.ToInt32(lblReqId.Text));
                        EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 5, int.Parse(lbl_EditedID.Text));
                    }
                    if (int.Parse(lbl_EditedID.Text) == 8)
                    {
                        EditBusiness.UpdateIsOk(Session["stcode"].ToString(), int.Parse(lbl_EditedID.Text), 5, txt_RadEdit.Text);
                        cb.InsertIntoUserLog((int)Session[sessionNames.userID_Karbar], DateTime.Now.ToString("HH:mm"), 4, (int)eventEnum.رد_درخواست_ويرايش_اطلاعات_فردي, "", Convert.ToInt32(lblReqId.Text));
                        EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 5, int.Parse(lbl_EditedID.Text));
                    }
                    if (int.Parse(lbl_EditedID.Text) == 9)
                    {
                        EditBusiness.UpdateIsOk(Session["stcode"].ToString(), int.Parse(lbl_EditedID.Text), 5, txt_RadEdit.Text);
                        cb.InsertIntoUserLog((int)Session[sessionNames.userID_Karbar], DateTime.Now.ToString("HH:mm"), 4, (int)eventEnum.رد_درخواست_ويرايش_اطلاعات_فردي, "", Convert.ToInt32(lblReqId.Text));
                        EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 5, int.Parse(lbl_EditedID.Text));
                    }

                }


            }
            EditBusiness.UpdateEditedInfoFSF2(Session["stcode"].ToString(),
                newtel, newaddress, newcodeposti, newostan, newshahr, newmobile);

            ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);

        }
        /// <summary>
        /// چنانچه کاربر کلید خیر را بزند، تغییرات اعمال شده شامل رد یا تایید درخواست تغییرات خنثی می گردد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void notConf_Click(object sender, EventArgs e)
        {
            Confirmpnl.Visible = false;
            foreach (RadListViewDataItem lst in rdlv_New.Items)
            {
                RadioButton rdb_ConfNewInfo = (RadioButton)lst.FindControl("rdb_ConfNewInfo");
                RadioButton rdb_NewInfo = (RadioButton)lst.FindControl("rdb_NewInfo");
                TextBox txt_RadEdit = (TextBox)lst.FindControl("txt_RadEdit");
                txt_RadEdit.Text = "";
                txt_RadEdit.Visible = false;
                rdb_ConfNewInfo.Checked = true;
                rdb_NewInfo.Checked = false;
            }

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
            foreach (RadListViewDataItem lst in rdlv_New.Items)
            {
                RadioButton rdb_ConfNewInfo = (RadioButton)lst.FindControl("rdb_ConfNewInfo");
                RadioButton rdb_NewInfo = (RadioButton)lst.FindControl("rdb_NewInfo");
                TextBox txt_RadEdit = (TextBox)lst.FindControl("txt_RadEdit");
                if (rdb_ConfNewInfo.Checked)
                {
                    txt_RadEdit.Visible = false;
                    NewInfStatus = true;

                }
                if (rdb_NewInfo.Checked)
                {
                    NewInfStatus = false;
                    txt_RadEdit.Visible = true;
                }

            }
        }
    }
}