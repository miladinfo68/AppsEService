using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class blacklistTeachers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");

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
            AccessControl.MenuId = menuId;
            Session[sessionNames.menuID] = menuId;
            AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();


            if (!IsPostBack)
            {
                setGridBlacklistSource();
            }
        }

        protected void btnExitBlacklist_Click(object sender, EventArgs e)
        {
            Business.university.CooperationRequest.CooperationRequestBusiness cr = new Business.university.CooperationRequest.CooperationRequestBusiness();
            int result = cr.updateBlacklistTeacher(txtIddMeli.Text.Trim(), false);
            if (result > 0)
            {
                setLog(DTO.eventEnum.حذف_از_لیست_غیر_مجاز_اساتید, result, txtIddMeli.Text.Trim());
                rwm.RadAlert("کد ملی از لیست کدهای غیر مجاز خارج شد", 300, 150, "مجاز کردن کد ملی", null);

            }
            else
            {
                rwm.RadAlert("عملیات با خطا همراه بود. لطفا مجددا تلاش فرمایید", 300, 150, "مجاز کردن کد ملی", null);

            }
            setGridBlacklistSource();
            searchIddMeli();
        }

        protected void btnBlacklist_Click(object sender, EventArgs e)
        {

            Business.university.CooperationRequest.CooperationRequestBusiness cr = new Business.university.CooperationRequest.CooperationRequestBusiness();
            int result = cr.updateBlacklistTeacher(txtIddMeli.Text.Trim(), true);
            if (result > 0)
            {
                setLog(DTO.eventEnum.اضافه_به_لیست_غیر_مجاز_اساتید, result, txtIddMeli.Text.Trim());
                rwm.RadAlert("کد ملی در لیست کدهای غیرمجاز قرار گرفت", 300, 150, "غیر مجاز کردن کد ملی", null);

            }
            else
            {
                rwm.RadAlert("عملیات با خطا همراه بود. لطفا مجددا تلاش فرمایید", 300, 150, "غیر مجاز کردن کد ملی", null);

            }
            
            setGridBlacklistSource();
            searchIddMeli();

        }

        private void setGridBlacklistSource()
        {
            Business.university.CooperationRequest.CooperationRequestBusiness cr = new Business.university.CooperationRequest.CooperationRequestBusiness();
            DataTable dt = cr.getBlacklistTeachers();
            grdBlacklist.DataSource = dt;
            grdBlacklist.DataBind();

        }

        private void setLog(DTO.eventEnum eventType, int blacklistId, string Description)
        {
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            int userId;//کاربری که لاگین کرده
            //eventType//کد کاری که انجام شده. 
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            string description;//توضیحات اختیاری
            int modifyId;//کد درخواست ویرایش شده. ویرایش اطلاعات فردی،اطلاعات تماس و ...
            userId = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            appId = 13;
            modifyId = blacklistId;
            description = Description;
            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)eventType, description, modifyId);
        }

        protected void btnSearchTeacher_Click(object sender, EventArgs e)
        {
            

            if(!Business.Common.CommonBusiness.ValidateNationalCode(txtIddMeli.Text.Trim()))
            {

                rwm.RadAlert("کد ملی نامعتبر است", 300, 150, "بررسی کد ملی", null);
                return;
            }
            searchIddMeli();

        }
        private void searchIddMeli()
        {
            btnBlacklist.Visible = false;
            btnExitBlacklist.Visible = false;
            Business.university.CooperationRequest.CooperationRequestBusiness cr = new Business.university.CooperationRequest.CooperationRequestBusiness();
            DataTable dt = cr.getBlacklistTeachers();
            btnBlacklist.Visible = true;
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("idd_meli='" + txtIddMeli.Text.Trim() + "'");
                if (dr.Length > 0)
                {
                    var status = Convert.ToBoolean(dr[0]["inBlacklist"]);
                    btnExitBlacklist.Visible = status == true;
                    btnBlacklist.Visible = status == false;
                }
            }
        }
    }
}