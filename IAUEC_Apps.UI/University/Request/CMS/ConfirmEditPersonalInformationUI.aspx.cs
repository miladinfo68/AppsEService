using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.Sql;
using System.Web.UI.WebControls;

using IAUEC_Apps.Business.university.Request;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class ConfirmEditPersonalInformationUI : System.Web.UI.Page
    {
        /// <summary>
        /// درخواست های ویرایش دانشجویان در این جدول ذخیره می گردد
        /// </summary>
        DataTable dt = new DataTable();
        /// <summary>
        /// ایجاد نموده ایم RequestStudentEditInfDAO یک شئ از کلاس
        /// </summary>
        //RequestStudentEditInfDAO DAO = new RequestStudentEditInfDAO();
        /// <summary>
        /// ایجاد نموده ایم Request_StudentCartDAO یک شئ از کلاس
        /// </summary>
        //Request_StudentCartDAO DAOc = new Request_StudentCartDAO();
        /// <summary>
        /// ایجاد نموده ایم EditPersonalInformationBusiness یک شئ از کلاس
        /// </summary>
        EditPersonalInformationBusiness EditBusiness = new EditPersonalInformationBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[sessionNames.userID_Karbar] == null)
                    Response.Redirect("/CommonUI/LoginRequestCMS.aspx");

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
            }
            AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();

            PersiaFiltering(grd_EditeRequest);
            PersiaFiltering(grd_Piceditrequest);
        }
        /// <summary>
        /// این متد برای پرکردن اطلاعات گیریدویو که شامل درخواست های ویرایش می باشد، به کار می رود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        protected void grd_EditeRequest_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            dt = EditBusiness.GetEditRequest();
            grd_EditeRequest.DataSource = dt;
        }
        /// <summary>
        /// این متد برای آپدیت کردن محتوای گیریدویو به کار رفته است
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AjaxRequestEventArgs"/> instance containing the event data.</param>
        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {

                DataTable dts = new DataTable();
                dts = EditBusiness.GetEditRequest();
                grd_EditeRequest.DataSource = dts;
                grd_EditeRequest.DataBind();
            }
        }

        protected void grd_EditeRequest_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName == "info")
            {
                Response.Redirect("../CMS/EditInformationUI.aspx?stcode=" + e.CommandArgument.ToString());

            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    RadWindowManager windowManager = new RadWindowManager();
        //    RadWindow widnow1 = new RadWindow();
        //    widnow1.NavigateUrl = "../CMS/EditInformationUI.aspx?stcode=99900999";
        //    widnow1.ID = "RadWindow1";
        //    widnow1.VisibleOnPageLoad = true;
        //    windowManager.Windows.Add(widnow1);
        //    ContentPlaceHolder mp;
        //    mp = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
        //    mp.Controls.Add(widnow1);

        //}

        protected void grd_Piceditrequest_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dtpic = new DataTable();
            dtpic = EditBusiness.GetPicEditRequest();
            grd_Piceditrequest.DataSource = dtpic;
        }

        protected void grd_Piceditrequest_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "pic")
            {

                Response.Redirect("../CMS/EditPersonalImage.aspx?stcode=" + e.CommandArgument.ToString());

            }
        }

        protected void PersiaFiltering(RadGrid rgv)
        {
            GridFilterMenu menu = rgv.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" ||
                        menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {
                    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }

                }
            }

        }



    }


}
