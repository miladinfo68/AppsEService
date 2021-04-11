using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.University.Request;
using Telerik.Web.UI;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class EditInformationUI : System.Web.UI.Page
    {
        RequestGovahiBusiness govahiBusiness = new RequestGovahiBusiness();
        /// <summary>
        ///ایجاد نموده ایم CommonBusiness یک شئ از کلاس
        /// </summary>
        CommonBusiness CommonBusiness = new CommonBusiness();
        /// <summary>
        ///ایجاد نموده ایم EditPersonalInformationBusiness یک شئ از کلاس
        /// </summary>
        EditPersonalInformationBusiness EditBusiness = new EditPersonalInformationBusiness();
        /// <summary>
        ///ایجاد نموده ایم RequestStudentEditInfDAO یک شئ از کلاس
        /// </summary>
        //RequestStudentEditInfDAO DAO = new RequestStudentEditInfDAO();
        /// <summary>
        ///ایجاد نموده ایم EditPersonalInformationDTO یک شئ از کلاس
        /// </summary>
        EditPersonalInformationDTO DTO = new EditPersonalInformationDTO();
        /// <summary>
        ///ایجاد نموده ایم Request_StudentCartDAO یک شئ از کلاس
        /// </summary>
        //Request_StudentCartDAO DAOc = new Request_StudentCartDAO();
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

            Session["stcode"] = Request.QueryString["stcode"].ToString();

            if (!IsPostBack)
            {

                dt = CartBusiness.GetStudentsInfo(Session["stcode"].ToString());
                rdlv_previous.DataSource = dt;
                rdlv_previous.DataBind();

                newmobile = dt.Rows[0]["mobile"].ToString();
                string[] add = dt.Rows[0]["homeAddress"].ToString().Split('-');
                if (add.Length == 3)
                {
                    newaddress = add[2].ToString();
                }
                else
                {
                    newaddress = add[0].ToString();
                }
                newtel = dt.Rows[0]["homePhone"].ToString();
                newcodeposti = dt.Rows[0]["home_postalCode"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["home_state"].ToString()))
                    newostan = int.Parse(dt.Rows[0]["home_state"].ToString());
                //if (!string.IsNullOrEmpty(dt.Rows[0]["live_city_web"].ToString()))
                //    newshahr = int.Parse(dt.Rows[0]["live_city_web"].ToString());
                if (dt.Rows.Count > 0)
                {
                    foreach (RadListViewDataItem lvid in rdlv_previous.Items)
                    {
                        Label lbl_CodePostiPrev = (Label)lvid.FindControl("lbl_CodePostiPrev");
                        Label lbl_AddressPrev = (Label)lvid.FindControl("lbl_AddressPrev");
                        Label lbl_TellPrev = (Label)lvid.FindControl("lbl_TellPrev");
                        Label lbl_MobilePrev = (Label)lvid.FindControl("lbl_MobilePrev");



                    }

                }



                dtm = EditBusiness.GetSTudentEditRequest(Session["stcode"].ToString());
                rdlv_New.DataSource = dtm;
                rdlv_New.DataBind();
                foreach (RadListViewDataItem lst in rdlv_New.Items)
                {
                    RadioButton rdb_ConfNewInfo = (RadioButton)lst.FindControl("rdb_ConfNewInfo");
                    RadioButton rdb_NewInfo = (RadioButton)lst.FindControl("rdb_NewInfo");
                    Label lbl_EditedID = (Label)lst.FindControl("lbl_EditedID");
                    Label lbl_NewContent = (Label)lst.FindControl("lbl_NewContent");
                    HiddenField hdf_NewContent = (HiddenField)lst.FindControl("hdf_NewContent");
                    DropDownList ddlCity = (DropDownList)lst.FindControl("ddlCity");

                    if (lbl_EditedID.Text == "11")
                    {
                        DataTable dtsh = new DataTable();
                        dtsh = CommonBusiness.getCity(0,int.Parse(hdf_NewContent.Value.ToString()));
                        //if (dtsh.Rows.Count > 0)
                        //    lbl_NewContent.Text = dtsh.Rows[0]["Title"].ToString();
                        //else
                        //{
                        //lbl_NewContent.Text = "سایر";

                        //-------------------------------
                        lbl_NewContent.Visible = false;
                        var stateRequest = dtm.AsEnumerable().Where(w => w.Field<int>("EditedID") == 12);
                        var state = 0;
                        if (stateRequest.Count() > 0)
                            state = Convert.ToInt32(stateRequest.FirstOrDefault().Field<string>("NewContent"));
                        else if (!string.IsNullOrEmpty(dt.Rows[0]["home_state"].ToString()))
                            state = Convert.ToInt32(dt.Rows[0]["home_state"].ToString());
                        if (state > 0)
                        {
                            ddlCity.DataSource = CommonBusiness.getCitiesFromTblShahrestan(state);
                            ddlCity.DataTextField = "Title";
                            ddlCity.DataValueField = "ID";
                            ddlCity.DataBind();
                            ddlCity.Items.Insert(0, new ListItem { Text = "سایر", Value = "0", Selected = dtsh.Rows.Count == 0 });
                            ddlCity.Visible = true;
                        }
                        else
                        {
                            ddlCity.Visible = false;
                            lbl_NewContent.Text = "سایر";
                            lbl_NewContent.Visible = true;
                        }
                        if (ddlCity.Visible && dtsh.Rows.Count > 0)
                            ddlCity.SelectedValue = hdf_NewContent.Value.ToString();

                        //-------------------------------
                        //}

                    }
                    else if (lbl_EditedID.Text == "12")
                    {
                        DataTable dtos = new DataTable();
                        dtos = CommonBusiness.GetStateFromTblOstan(int.Parse(hdf_NewContent.Value.ToString()));
                        lbl_NewContent.Text = dtos.Rows[0]["Title"].ToString();

                    }
                    else
                    {
                        lbl_NewContent.Text = hdf_NewContent.Value.ToString();
                    }

                }

                dts = EditBusiness.GetStudentPic(Session["stcode"].ToString());


            }
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
            (DTO.TelChanged) = null;
            (DTO.AddressChanged) = null;
            (DTO.mobilechanged) = null;
            (DTO.PostCodeChanged) = null;
            (DTO.ShahrChanged) = null;
            (DTO.OstanChanged) = null;
            var description = string.Empty;
            var requestIdList = new Dictionary<int, int>();
            var editPersonalInformationList = new List<int>();

            foreach (RadListViewDataItem lst in rdlv_New.Items)
            {
                RadioButton rdb_ConfNewInfo = (RadioButton)lst.FindControl("rdb_ConfNewInfo");
                RadioButton rdb_NewInfo = (RadioButton)lst.FindControl("rdb_NewInfo");
                Label lbl_EditedID = (Label)lst.FindControl("lbl_EditedID");
                Label lbl_NewContent = (Label)lst.FindControl("lbl_NewContent");
                TextBox txt_RadEdit = (TextBox)lst.FindControl("txt_RadEdit");
                HiddenField hdf_NewContent = (HiddenField)lst.FindControl("hdf_NewContent");
                DropDownList ddlCity = (DropDownList)lst.FindControl("ddlCity");
                HiddenField hdnStudentRequestID = (HiddenField)lst.FindControl("hdnStudentRequestID");
                HiddenField hdnEditPersonalInformation = (HiddenField)lst.FindControl("hdnEditPersonalInformation");

                if (rdb_ConfNewInfo.Checked)
                {


                    if (int.Parse(lbl_EditedID.Text) == 6)
                    {
                        newtel = lbl_NewContent.Text;
                        DTO.TelChanged = true;

                    }
                    if (int.Parse(lbl_EditedID.Text) == 9)
                    {
                        newmobile = lbl_NewContent.Text;
                        DTO.mobilechanged = true;
                    }
                    if (int.Parse(lbl_EditedID.Text) == 8)
                    {
                        newcodeposti = lbl_NewContent.Text;
                        DTO.PostCodeChanged = true;

                    }
                    if (int.Parse(lbl_EditedID.Text) == 11)
                    {
                        //---------------
                        if (ddlCity != null && ddlCity.Visible == true && ddlCity.SelectedItem.Value != hdf_NewContent.Value.ToString())
                        {
                            var dtsh = CommonBusiness.getCitiesFromTblShahrestan(int.Parse(hdf_NewContent.Value.ToString()));
                            newshahr = Convert.ToInt32(ddlCity.SelectedItem.Value);
                            var requestCityName = "سایر";
                            if (dtsh.Rows.Count > 0)
                                requestCityName = dtsh.Rows[0]["Title"].ToString();
                            description = "شهر درخواست از " + requestCityName + " به " + ddlCity.SelectedItem.Text + " تغییر داده شد.";
                        }
                        else
                            newshahr = (int.Parse(hdf_NewContent.Value.ToString()));
                        //---------------

                        DTO.ShahrChanged = true;

                    }
                    if (int.Parse(lbl_EditedID.Text) == 12)
                    {
                        newostan = (int.Parse(hdf_NewContent.Value.ToString()));
                        DTO.OstanChanged = true;

                    }

                    if (int.Parse(lbl_EditedID.Text) == 7)
                    {
                        Addressarg = lbl_NewContent.Text.Split(new char[] { ',' });
                        newaddress = lbl_NewContent.Text;
                        DTO.AddressChanged = true;
                    }

                    editPersonalInformationList.Add(Convert.ToInt32(hdnEditPersonalInformation.Value));
                }

                if (rdb_NewInfo.Checked)
                {
                    if (int.Parse(lbl_EditedID.Text) == 6)
                    {
                        DTO.TelChanged = false;
                        DTO.telrad = txt_RadEdit.Text;
                        
                    }
                    if (int.Parse(lbl_EditedID.Text) == 7)
                    {
                        DTO.AddressChanged = false;
                        DTO.addressrad = txt_RadEdit.Text;

                    }
                    if (int.Parse(lbl_EditedID.Text) == 8)
                    {
                        DTO.PostCodeChanged = false;
                        DTO.codepostirad = txt_RadEdit.Text;

                    }
                    if (int.Parse(lbl_EditedID.Text) == 9)
                    {
                        DTO.mobilechanged = false;
                        DTO.mobilerad = txt_RadEdit.Text;

                    }
                    if (int.Parse(lbl_EditedID.Text) == 11)
                    {
                        DTO.ShahrChanged = false;
                        DTO.shahrrad = txt_RadEdit.Text;

                    }
                    if (int.Parse(lbl_EditedID.Text) == 12)
                    {
                        DTO.OstanChanged = false;
                        DTO.ostanrad = txt_RadEdit.Text;
                    }
                }
                requestIdList.Add(int.Parse(lbl_EditedID.Text), Convert.ToInt32(hdnStudentRequestID.Value));
            }

            //if(string.IsNullOrEmpty(newshahr))

            CommonBusiness cmnb = new CommonBusiness();
            //EditBusiness.UpdateEditedInfoFSF2(Session["stcode"].ToString(), newtel, newaddress, newcodeposti, newostan, newshahr, newmobile);
            foreach(var item in editPersonalInformationList)
                EditBusiness.updateStudentInfo(Session["stcode"].ToString(), item);

            if (DTO.TelChanged != null && DTO.TelChanged == true)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 6, 7, "");
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 6);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 4, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f=> f.Key == 6).Value);
            }
            if (DTO.TelChanged != null && DTO.TelChanged == false)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 6, 5, DTO.telrad.ToString());
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 5, 6);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 5, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 6).Value);

            }
            if (DTO.AddressChanged != null && DTO.AddressChanged == true)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 7, 7, "");
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 7);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 4, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 7).Value);

            }
            if (DTO.AddressChanged != null && DTO.AddressChanged == false)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 7, 5, DTO.addressrad.ToString());
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 5, 7);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 5, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 7).Value);

            }
            if (DTO.mobilechanged != null && DTO.mobilechanged == true)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 9, 7, "");
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 9);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 4, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 9).Value);

            }
            if (DTO.mobilechanged != null && DTO.mobilechanged == false)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 9, 5, DTO.mobilerad.ToString());
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 5, 9);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 5, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 9).Value);

            }
            if (DTO.PostCodeChanged != null && DTO.PostCodeChanged == true)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 8, 7, "");
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 8);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 4, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 8).Value);

            }
            if (DTO.PostCodeChanged != null && DTO.PostCodeChanged == false)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 8, 5, DTO.codepostirad.ToString());
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 5, 8);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 5, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 8).Value);


            }
            if (DTO.ShahrChanged != null && DTO.ShahrChanged == true)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 11, 7, description);
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 11);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 4, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 11).Value);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 184, description, requestIdList.FirstOrDefault(f => f.Key == 11).Value);

            }
            if (DTO.ShahrChanged != null && DTO.ShahrChanged == false)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 11, 5, DTO.shahrrad.ToString());
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 5, 11);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 5, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 11).Value);

            }
            if (DTO.OstanChanged != null && DTO.OstanChanged == true)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 12, 7, "");
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 7, 12);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 4, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 12).Value);

            }
            if (DTO.OstanChanged != null && DTO.OstanChanged == false)
            {
                EditBusiness.UpdateIsOk(Session["stcode"].ToString(), 12, 5, DTO.ostanrad.ToString());
                EditBusiness.UpdateStudentEditeRequestLogID(Session["stcode"].ToString(), 5, 12);
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 5, Session["stcode"].ToString(), requestIdList.FirstOrDefault(f => f.Key == 12).Value);

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

        protected void btn_NotConf_Click(object sender, EventArgs e)
        {

            Confirmpnl.Visible = false;
            foreach (RadListViewDataItem lst in rdlv_New.Items)
            {

                TextBox txt_RadEdit = (TextBox)lst.FindControl("txt_RadEdit");
                txt_RadEdit.Text = "";
                txt_RadEdit.Visible = false;
            }
        }










    }
}