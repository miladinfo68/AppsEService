using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Skins;
using System.Data;
using IAUEC_Apps.Business;
using System.Drawing;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class ManageSignatures : System.Web.UI.Page
    {
        Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
        Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();
        Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
        const string updateOrInsert = "upORIns";
        const string userTypeRole = "userRole";
        const string hrID = "hrID";
        const string ostadCode = "code_Ostad";
        const string userCode = "userCode";
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
                fillDropDown();
                string tempFolderPath = Server.MapPath("~/University/CooperationRequest/Signatures/temp");
                Directory.CreateDirectory(tempFolderPath);
                var createDate = Directory.GetCreationTime(tempFolderPath);
                if (createDate < DateTime.Now.Date)
                {
                    var dir = new DirectoryInfo(tempFolderPath);
                    dir.Delete(true);
                    Directory.CreateDirectory(tempFolderPath);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState[updateOrInsert] = "up";
            ViewState[userTypeRole] = "1";
            imgSignature.ImageUrl = null;
            btnDeleteSignature.Visible = false;
            int HRid = 0;
            if (string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                ShowMessage("لطفا نام کاربری را وارد فرمایید.", false, true);
                return;
            }
            HRid = getHrID();
            if (HRid > 0)
            {
                DataTable dtSignature;
                dtSignature = bsn.getSignature(HRid, Convert.ToInt32(ViewState[userTypeRole]));
                if (dtSignature.Rows.Count == 1)
                {
                    ViewState[hrID] = HRid;
                    ViewState[userCode] = txtSearch.Text.ToString().Trim();
                    if (dtSignature.Rows[0]["signature"] != DBNull.Value)
                    {
                        ViewState[updateOrInsert] = "up";
                        btnDeleteSignature.Visible = true;
                        imgSignature.ImageUrl = dtSignature.Rows[0]["signature"].ToString() + "?nc=" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                    }
                    else
                    {
                        imgSignature.ImageUrl = "~/University/CooperationRequest/Image/noPic.png";
                        ViewState[updateOrInsert] = "ins";
                    }
                    lblFirstName.Text = dtSignature.Rows[0]["displayName"].ToString().Trim();
                    lblUserCode.Text = dtSignature.Rows[0]["userName"].ToString().Trim();

                    pnlSearchResult.Visible = true;
                    pnlEditSignature.Visible = true;
                    pnlSearchMessage.Visible = false;
                }
                else if (dtSignature.Rows.Count != 1)
                {
                    ShowMessage("کاربری با مشخصات وارد شده یافت نشد.", false, true);
                }
            }
            else
            {
                string msg = "استادی با وضعیت مشغول به همکاری و کد وارد شده در سامانه یافت نشد.";
                if (HRid == -3)
                {
                    msg = "اطلاعات استاد به صورت نادرستی ذخیره شده است. لطفا با مدیر سامانه تماس یگیرید.";
                }
                ShowMessage(msg, false, true);

                //age ba code vared shode chand karbar vojud dasht
            }
            //}

        }

        protected void btnSearchUser_Click(object sender, EventArgs e)
        {
            ViewState[updateOrInsert] = "up";
            ViewState[userTypeRole] = ddlUserType.SelectedItem.Value;
            imgSignature.ImageUrl = null;
            int HRid = 0;
            if (ddlUserType.SelectedItem.Value != "0")
            {
                //HRid = getCode();
                //if (HRid > -1)
                //{
                DataTable dtSignature = new DataTable();
                dtSignature = bsn.getSignature(HRid, Convert.ToInt32(ViewState[userTypeRole]));
                if (dtSignature.Rows.Count == 1)
                {
                    ViewState[hrID] = HRid;
                    ViewState[userCode] = dtSignature.Rows[0]["UserId"].ToString();
                    if (dtSignature.Rows[0]["signature"] != DBNull.Value)
                    {
                        ViewState[updateOrInsert] = "up";
                        imgSignature.ImageUrl = dtSignature.Rows[0]["signature"].ToString() + "?nc=" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                    }
                    else
                    {
                        imgSignature.ImageUrl = "~/University/CooperationRequest/Image/noPic.png";
                        ViewState[updateOrInsert] = "ins";
                    }
                    lblFirstName.Text = dtSignature.Rows[0]["displayName"].ToString().Trim();
                    lblUserCode.Text = dtSignature.Rows[0]["userName"].ToString().Trim();

                    pnlSearchResult.Visible = true;
                    pnlEditSignature.Visible = true;
                    pnlSearchMessage.Visible = false;
                }
                else if (dtSignature.Rows.Count != 1)
                {
                    ShowMessage("کاربری با مشخصات وارد شده یافت نشد.", false, true);
                }
                //}
                //else
                //{
                //    ShowMessage("کد وارد شده نادرست است.", false, true);

                //    //age ba code vared shode chand karbar vojud dasht
                //}
            }
            btnDeleteSignature.Visible = false;
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (ViewState[hrID] == null || ViewState[userTypeRole] == null)
                return;
            if (rapSignature.UploadedFiles.Count == 0)
                return;
            string pre = getFileExtension(rapSignature.UploadedFiles[0].FileName);
            string path = saveAsScan();

            if (pre.ToLower() != "jpeg" && pre.ToLower() != "jpg")
            {
                ShowMessage("لطفا فایل با فرمت jpeg و یا jpg انتخاب فرمایید.", false, false);
                return;
            }
            if (path != "")
            {
                try
                {
                    if (bsn.UpdateOrInsertSignature(Convert.ToInt32(ViewState[userCode]), Convert.ToInt32(ViewState[userTypeRole]), path + "." + pre, Convert.ToInt32(ViewState[hrID])))
                    {
                        var file = rapSignature.UploadedFiles[0];
                        System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream);
                        img.Save(Server.MapPath(path + "." + pre), System.Drawing.Imaging.ImageFormat.Jpeg);
                        setLog();
                        ShowMessage("تغییرات با موفقیت ذخیره شد.", true, false);
                    }
                    else
                    {
                        ShowMessage("خطا در ذخیره امضا", false, false);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("خطا در آپلود امضا. لطفا مجددا تلاش فرمایید.", false, false);

                }
            }
            else
            {

            }
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtSearch.Visible = ddlUserType.SelectedItem.Value == "1";
            rfvSearchText.Enabled = ddlUserType.SelectedItem.Value == "1";

        }
        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {

        }

        protected void rapSignature_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            var tempFolderpath = Server.MapPath("~/University/CooperationRequest/Signatures/temp/");
            var radTempFolderPath = Server.MapPath("~/App_Data/RadUploadTemp/");
            var fileName = ((FileStream)e.File.InputStream).Name.Split('\\').Last();
            if (File.Exists(tempFolderpath + fileName))
                File.Delete(tempFolderpath + fileName);
            File.Copy(radTempFolderPath + fileName, tempFolderpath + fileName);
            var nc = "?nc=" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second
                + DateTime.Now.Millisecond;
            imgSignature.ImageUrl = "~/University/CooperationRequest/Signatures/temp/" + fileName + nc;
            e.File.InputStream.Close();
        }

        private void fillDropDown()
        {
            List<int> l = new List<int>(new int[] { (int)DTO.RoleEnums.مسئول_حق_التدریس, (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی, (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی, (int)DTO.RoleEnums.سرپرست_واحد });
            foreach (int i in l)
            {
                DataTable dt = bsn.getSignature(0, i);
                ListItem li;
                if (dt.Rows.Count == 1)
                {
                    li = new ListItem(dt.Rows[0]["userName"].ToString() + "-" + dt.Rows[0]["displayName"].ToString(), i.ToString());
                    ddlUserType.Items.Add(li);
                }
            }
        }

        private void ShowMessage(string msg, bool isSuccess, bool hideSearchResults)
        {
            lblMessageText.Text = msg;
            pnlSearchMessage.Visible = true;
            if (hideSearchResults)
            {
                pnlSearchResult.Visible = false;
                pnlEditSignature.Visible = false;
            }
            if (isSuccess)
                pnlSearchMessage.CssClass = pnlSearchMessage.CssClass.Replace("alert-danger", "alert-success");
            else
                pnlSearchMessage.CssClass = pnlSearchMessage.CssClass.Replace("alert-success", "alert-danger");

        }

        private int getHrID()
        {
            ViewState[ostadCode] = -1;
            switch (Convert.ToInt32(ViewState[userTypeRole]))
            {
                case 1://استاد

                    DataTable dtInfoPeople = FRB.GetOstadInfoFromHR(Convert.ToInt32(txtSearch.Text.ToString().Trim()));
                    if (dtInfoPeople.Rows.Count == 1)
                    {
                        //ViewState[ostadCode] = txtSearch.Text.ToString().Trim();
                        return Convert.ToInt32(dtInfoPeople.Rows[0]["ID"].ToString().Trim());
                    }
                    else if (dtInfoPeople.Rows.Count > 1)
                    {
                        return -3;
                    }
                    else
                        return -1;
                case (int)DTO.RoleEnums.سرپرست_واحد://سرپرست دانشگاه
                case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی://پاراف سمت راست
                case (int)DTO.RoleEnums.مسئول_حق_التدریس://پاراف سمت چپ
                case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی://پاراف سمت چپ
                    return 0;
            }
            return -2;
        }

        private string getUserTypeName_en(int userType)
        {
            switch (userType)
            {
                case 1:
                    return "Teachers";
                case (int)DTO.RoleEnums.سرپرست_واحد://سرپرست دانشگاه
                case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی://پاراف سمت راست
                case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی://پاراف سمت چپ
                    return "Users";
                default:
                    return "";
            }
        }

        private static string getFileExtension(string fileName)
        {
            string extension = "";
            char[] arr = fileName.ToCharArray();
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '.')
                {
                    index = i;
                }
            }
            for (int x = index + 1; x < arr.Length; x++)
            {
                extension = extension + arr[x];
            }
            return extension;
        }

        private string saveAsScan()
        {
            try
            {
                string Path = @"~/University/CooperationRequest/Signatures/" + getUserTypeName_en(Convert.ToInt32(ViewState[userTypeRole]));
                //string urlPath = Path + "/" + ViewState[userCode].ToString();
                if (!Directory.Exists(Path))
                    System.IO.Directory.CreateDirectory(Server.MapPath(Path));
                return Path + "/" + getFileName();
            }
            catch (Exception ex)
            { return ""; }
        }

        private string getFileName()
        {
            switch (Convert.ToInt32(ViewState[userTypeRole]))
            {
                case 1://استاد
                    return ViewState[hrID].ToString();
                case (int)DTO.RoleEnums.سرپرست_واحد://سرپرست دانشگاه
                case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی://پاراف سمت راست
                case (int)DTO.RoleEnums.مسئول_حق_التدریس://پاراف سمت چپ
                case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی://پاراف سمت چپ
                    return ViewState[userTypeRole].ToString() + "_" + ViewState[userCode].ToString();
            }
            return "0";
        }

        private void setLog()
        {
            DTO.eventEnum eventType = DTO.eventEnum.درج_امضای_استاد;
            int user_Code = 0;
            switch (Convert.ToInt32(ViewState[userTypeRole]))
            {
                case 1:
                    switch (ViewState[updateOrInsert].ToString())
                    {
                        case "up":
                            eventType = DTO.eventEnum.به_روز_رسانی_امضای_استاد;
                            break;
                        case "ins":
                            eventType = DTO.eventEnum.درج_امضای_استاد;
                            break;
                        case "dlt":
                            eventType = DTO.eventEnum.حذف_امضای_استاد;
                            break;

                    }
                    user_Code = Convert.ToInt32(ViewState[hrID]);
                    break;
                case (int)DTO.RoleEnums.سرپرست_واحد://سرپرست دانشگاه
                case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی://پاراف سمت راست
                case (int)DTO.RoleEnums.مسئول_حق_التدریس://پاراف سمت چپ
                case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی://پاراف سمت چپ
                    switch (ViewState[updateOrInsert].ToString())
                    {
                        case "up":
                            eventType = DTO.eventEnum.به_روز_رسانی_امضای_کاربر;
                            break;
                        case "ins":
                            eventType = DTO.eventEnum.درج_امضای_کاربر;
                            break;
                    }
                    user_Code = Convert.ToInt32(ViewState[userCode]);
                    break;
            }
            int userId;//کاربری که لاگین کرده
            //eventType//کد کاری که انجام شده. 
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            string description;//توضیحات اختیاری
            int modifyId;//کد درخواست ویرایش شده. ویرایش اطلاعات فردی،اطلاعات تماس و ...
            userId = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            appId = 13;
            modifyId = user_Code;
            description = "";
            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)eventType, description, modifyId);
        }

        protected void btnDeleteSignature_Click(object sender, EventArgs e)
        {
            if (ViewState[hrID] == null || ViewState[userTypeRole] == null)
                return;
            if (imgSignature.ImageUrl == "")
                return;
            try
            {
                if (bsn.deleteSignature(Convert.ToInt64(ViewState[userCode]), Convert.ToInt32(ViewState[userTypeRole]), Convert.ToInt32(ViewState[hrID])))
                {
                    ViewState[updateOrInsert] = "dlt";
                    setLog();
                    ShowMessage("حذف امضا با موفقیت ذخیره شد.", true, false);
                }
                else
                {
                    ShowMessage("خطا در حذف امضا", false, false);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("خطا در حذف امضا. لطفا مجددا تلاش فرمایید.", false, false);

            }
        }

    }

}