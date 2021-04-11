using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutSignEntry : System.Web.UI.Page
    {
        CheckOutRequestBusiness business = new CheckOutRequestBusiness();
        LoginBusiness lngB = new LoginBusiness();
        string userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userID = Session[sessionNames.userID_Karbar].ToString();
                drpUser.DataSource = business.GetUsers();
                drpUser.DataTextField = "name";
                drpUser.DataValueField = "UserId";
                drpUser.DataBind();
                drpUser.Items.Insert(0, "انتخاب کنید");
                foreach (CheckOutStatusEnum.CheckOutAllStatusEnum status in Enum.GetValues(typeof(CheckOutStatusEnum.CheckOutAllStatusEnum)))
                {
                    if (status != CheckOutStatusEnum.CheckOutAllStatusEnum.submited && status != CheckOutStatusEnum.CheckOutAllStatusEnum.end && status != CheckOutStatusEnum.CheckOutAllStatusEnum.stampPay)
                    {
                        ListItem li = new ListItem();
                        li.Text = business.GetPersianStatus(Convert.ToInt32(status));
                        li.Value = Convert.ToInt32(status).ToString();
                        drpUserStatus.Items.Add(li);
                    }
                }
                drpUserStatus.Items.Insert(0, "انتخاب کنید");
            }
        }

        protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            setUserRoleInService();
            setUserSignInCheckout();
            setSignature();
        }

        private void setUserRoleInService()
        {
            bltCheckoutRole.Items.Clear();
            bltUserRols.Items.Clear();
            Business.Common.LoginBusiness CB = new LoginBusiness();
            if (drpUser.SelectedIndex != 0)
            {
                DataTable DT;
                DT = CB.Get_UserRoles(drpUser.SelectedItem.Value);
                foreach (DataRow row in DT.Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = row["RoleName"].ToString();
                    li.Value = row["RoleId"].ToString();
                    bltUserRols.Items.Add(li);
                    setUserRoleInCheckout(Convert.ToInt32(row["RoleId"].ToString()));

                }


            }
        }

        private void setUserRoleInCheckout(int rolID)
        {
            bltCheckoutRole.Items.Clear();
            if (drpUser.SelectedIndex != 0)
            {
                DataTable DT;
                DT = business.GetListOfStatusByRoleId(rolID);
                foreach (DataRow row in DT.Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32((CheckOutStatusEnum.CheckOutAllStatusEnum)Convert.ToInt32(row[0])));
                    li.Value = row[0].ToString();
                    bltCheckoutRole.Items.Add(li);
                }


            }
        }

        private void setUserSignInCheckout()
        {
            bltSigns.Items.Clear();
            if (drpUser.SelectedIndex != 0)
            {
                DataTable DT ;
                DT = business.GetUserRole(Convert.ToInt32(drpUser.SelectedValue));
                foreach (DataRow row in DT.Rows)
                {
                    ListItem li = new ListItem();
                    li.Text = business.GetPersianStatus(Convert.ToInt32((CheckOutStatusEnum.CheckOutAllStatusEnum)Convert.ToInt32(row[0])));
                    li.Value = row[0].ToString();
                    bltSigns.Items.Add(li);
                }


            }
        }


        private void setSignature()
        {
            if (drpUser.SelectedIndex != 0 && drpUserStatus.SelectedIndex != 0)
            {
                byte[] imgBytes = business.GetSignByUserAndCartable(drpUser.SelectedValue, Convert.ToInt32(drpUserStatus.SelectedItem.Value));
                if (imgBytes != null)
                {
                    string base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    imgSign.ImageUrl = "data:image/png;base64," + base64String;
                }
                else
                {
                    imgSign.ImageUrl = "";
                }
            }
        }

        protected void btnSubmitFile_Click(object sender, EventArgs e)
        {
            string filePath = flpSignImage.PostedFile.FileName;
            string filename = Path.GetFileName(filePath);
            string ext = Path.GetExtension(filename);
            string contenttype = String.Empty;

            //Set the contenttype based on File Extension
            switch (ext)
            {
                case ".jpg":
                    contenttype = "image/jpg";
                    break;
                case ".png":
                    contenttype = "image/png";
                    break;
                case ".gif":
                    contenttype = "image/gif";
                    break;
            }
            if (contenttype != String.Empty)
            {
                Stream fs = flpSignImage.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] signImage = br.ReadBytes((Int32)fs.Length);
                bool s = business.InsertCheckOutSign(signImage, drpUser.SelectedValue.ToString(), Convert.ToInt32(drpUserStatus.SelectedValue), txtFromDate.Text,txtToDate.Text);
                CommonBusiness cb = new CommonBusiness();
                cb.InsertIntoUserLog(Convert.ToInt32(Session[sessionNames.userID_Karbar]), DateTime.Now.ToString("HH:mm"), Convert.ToInt32(Session[sessionNames.appID_Karbar]), (int)DTO.eventEnum.درج_امضا_کاربر_در_سیستم_تسویه, "کارتابل "+drpUserStatus.SelectedItem.Text+" _ تاریخ "+ txtFromDate.Text+" الی "+ txtToDate.Text, Convert.ToInt32(drpUser.SelectedValue));
                bltUserRols.Items.Add(drpUser.SelectedItem);
                string base64String = Convert.ToBase64String(signImage, 0, signImage.Length);
                imgSign.ImageUrl = "data:image/png;base64," + base64String;
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "عکس امضا با موفقیت ثبت شد";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "نوع فایل صحیح نیست" +
                  " لطفا فقط عکس با فرمت های jpg/png/gif ارسال کنید";
            }
        }

        protected void drpUserStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSignature();

            DataTable dt = business.GetSignInfByUserAndCartable(drpUser.SelectedValue, Convert.ToInt32(drpUserStatus.SelectedItem.Value));
            if (dt.Rows.Count > 0)
            {
                txtFromDate.Text = dt.Rows[0]["fromDate"].ToString();
                txtToDate.Text = dt.Rows[0]["toDate"].ToString();
            }
            else
            {
                txtFromDate.Text = "";
                txtToDate.Text = "";
            }
        }
    }
}