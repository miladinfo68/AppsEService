using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using System.Data;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.CommonUI
{
    public partial class AccessControl : System.Web.UI.UserControl
    {
        public string MenuId;
        public string Menu
        {
            get
            {
                return MenuId;
            }
            set
            {
                MenuId = value;
            }
        }
        public string UserId;
        public string UserLogin
        {
            get
            {
                return UserId;
            }
            set
            {
                UserId = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if ( Session[sessionNames.menuID]!= null && Session[sessionNames.menuID].ToString()!="0")
            {
                LoginBusiness lb = new LoginBusiness();
                UserAccessBusiness uab = new UserAccessBusiness();
                DataTable dtpermission = new DataTable();
                if (MenuId != null)
                    dtpermission = uab.Get_MenuPermissionByMenuId(int.Parse(MenuId));
                else
                    dtpermission = uab.Get_MenuPermissionByMenuId(int.Parse(Session[sessionNames.menuID].ToString()));
        
                ContentPlaceHolder mpContentPlaceHolder;
                ContentPlaceHolder ptContentPlaceHolder;
                ContentPlaceHolder HeaderplaceHolder;

                HeaderplaceHolder = (ContentPlaceHolder)Page.Master.FindControl("HeaderplaceHolder");
                ptContentPlaceHolder =(ContentPlaceHolder)Page.Master.FindControl("PageTitle");
                mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                Literal ltr = (Literal)ptContentPlaceHolder.FindControl("pt");
                ltr.Text = dtpermission.Rows[0]["MenuName"].ToString();
                Literal tltr = (Literal)HeaderplaceHolder.FindControl("t");
                tltr.Text = dtpermission.Rows[0]["MenuName"].ToString();
                for (int i = 0; i < dtpermission.Rows.Count; i++)
                {
                    switch (int.Parse(dtpermission.Rows[i]["ControlType"].ToString()))
                    {
                        case 1:
                            var btn = (Button)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if(btn != null)
                            btn.Visible = false;
                            break;
                        case 2:
                            var radbtn = (RadButton)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if (radbtn != null)
                                radbtn.Visible = false;
                            break;
                        case 3:
                            var grd = (GridView)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if (grd != null)
                                grd.Visible = false;
                            break;
                        case 4:
                            var Radgrd = (RadGrid)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if (Radgrd != null)
                                Radgrd.Visible = false;
                            break;
                        case 5:
                            var lst = (ListView)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if (lst != null)
                                lst.Visible = false;
                            break;
                        case 6:
                            var Radlst = (RadListView)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if (Radlst != null)
                                Radlst.Visible = false;
                            break;
                        case 7:
                            var img = (ImageButton)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if (img != null)
                                img.Visible = false;
                            break;
                        case 8:
                            var pivot = (RadPivotGrid)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if (pivot != null)
                                pivot.Visible = false;
                            break;
                        case 9:
                            var name = dtpermission.Rows[i]["ControlName"].ToString().Split(new char[] { '@' });
                            if (name != null)
                            {
                                var Mastergrd = (RadGrid) mpContentPlaceHolder.FindControl(name[0]);
                                if(Mastergrd !=null)
                                Mastergrd.MasterTableView.GetColumn(name[1]).Visible = false;
                            }
                            break;
                        case 10:
                            string[] Itemname = dtpermission.Rows[i]["ControlName"].ToString().Split(new char[] { '@' });

                            RadGrid MasterItemgrd = (RadGrid)mpContentPlaceHolder.FindControl(Itemname[0]);
                            if (MasterItemgrd.AllowPaging == true)
                            {
                                //MasterItemgrd.AllowPaging = false;
                                //MasterItemgrd.Rebind();
                                int c = 0;
                                if (MasterItemgrd.CurrentPageIndex == 0)
                                    c = (MasterItemgrd.CurrentPageIndex) + 1;
                                else
                                    c = (MasterItemgrd.CurrentPageIndex);
                                MasterItemgrd.MasterTableView.CurrentPageIndex = c;
                                foreach (GridDataItem item in MasterItemgrd.Items)
                                {
                                    Button rd = (Button)item.FindControl(Itemname[1]);
                                    rd.Visible = false;
                                }
                                //MasterItemgrd.AllowPaging = true;
                                //MasterItemgrd.Rebind();
                            }
                            else
                            {
                                foreach (GridDataItem item in MasterItemgrd.Items)
                                {
                                    Button rd = (Button)item.FindControl(Itemname[1]);
                                    rd.Visible = false;
                                }
                            }
                            break;
                        case 11:
                            var rdbm = (RadioButton)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if(rdbm != null)
                            rdbm.Visible = false;
                            break;
                        case 12:
                            var masterdrp = (DropDownList)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if (masterdrp != null)
                                masterdrp.Visible = false;
                            break;
                        case 13:
                            string[] grdItemname = dtpermission.Rows[i]["ControlName"].ToString().Split(new char[] { '@' });

                            GridView grdMasterItemgrd = (GridView)mpContentPlaceHolder.FindControl(grdItemname[0]);


                            foreach (GridDataItem item in grdMasterItemgrd.Rows)
                                {
                                    Button rd = (Button)item.FindControl(grdItemname[1]);
                                    rd.Visible = false;
                                }
                            
                            break;
                        case 14:
                            var pnl = (Panel)mpContentPlaceHolder.FindControl(dtpermission.Rows[i]["ControlName"].ToString());
                            if (pnl != null)
                                pnl.Visible = false;
                            break;
                    }

                }
                DataTable dtMenu = new DataTable();
                if (MenuId != null)
                    dtMenu = lb.Get_MenuPermission(int.Parse(MenuId), int.Parse(UserId));
                else
                    dtMenu = lb.Get_MenuPermission(int.Parse(Session[sessionNames.menuID].ToString()), int.Parse(Session[sessionNames.userID_Karbar].ToString()));
                if (dtMenu.Rows.Count > 0)
                {

                    for (int i = 0; i < dtMenu.Rows.Count; i++)
                    {
                        switch (int.Parse(dtMenu.Rows[i]["ControlType"].ToString()))
                        {
                            case 1:
                                var btn = (Button)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                if(btn !=null)
                                btn.Visible = true;
                                break;
                            case 2:
                                var radbtn = (RadButton)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                if (radbtn != null)
                                    radbtn.Visible = true;
                                break;
                            case 3:
                                var grd = (GridView)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                if (grd != null)
                                    grd.Visible = true;
                                break;
                            case 4:
                                var Radgrd = (RadGrid)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                if (Radgrd != null)
                                    Radgrd.Visible = true;
                                break;
                            case 5:
                                var lst = (ListView)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                if (lst != null)
                                    lst.Visible = true;
                                break;
                            case 6:
                                var Radlst = (RadListView)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                if (Radlst != null)
                                    Radlst.Visible = true;
                                break;
                            case 7:
                                var img = (ImageButton)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                if (img != null)
                                    img.Visible = true;
                                break;
                            case 8:
                                var pivot = (RadPivotGrid)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                if (pivot != null)
                                    pivot.Visible = true;
                                break;
                            case 9:
                                string[] name = dtMenu.Rows[i]["ControlName"].ToString().Split(new char[] { '@' });
                               
                                RadGrid Mastergrd = (RadGrid)mpContentPlaceHolder.FindControl(name[0]);
                                Mastergrd.MasterTableView.GetColumn(name[1]).Visible = true;
                                
                                break;
                            case 10:
                                string[] Itemname = dtMenu.Rows[i]["ControlName"].ToString().Split(new char[] { '@' });

                                RadGrid MasterItemgrd = (RadGrid)mpContentPlaceHolder.FindControl(Itemname[0]);
                         
                              int c = 0;
                              if (MasterItemgrd.CurrentPageIndex == 0)
                                  c = (MasterItemgrd.CurrentPageIndex) + 1;
                              else
                                  c = (MasterItemgrd.CurrentPageIndex);
                                MasterItemgrd.MasterTableView.CurrentPageIndex = c;
                                    foreach (GridDataItem item in MasterItemgrd.Items)
                                    {
                                        Button rd = (Button)item.FindControl(Itemname[1]);
                                        rd.Visible = true;
                                    }
                                   
                           
                                break;
                            case 11:
                                RadioButton rdbm = (RadioButton)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                rdbm.Visible = true;
                                break;
                            case 12:
                                 DropDownList masterdrp = (DropDownList)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                 masterdrp.Visible = true;
                            break;
                            case 13:
                            string[] grdItemname = dtMenu.Rows[i]["ControlName"].ToString().Split(new char[] { '@' });

                            GridView grdMasterItemgrd = (GridView)mpContentPlaceHolder.FindControl(grdItemname[0]);


                            foreach (GridDataItem item in grdMasterItemgrd.Rows)
                            {
                                Button rd = (Button)item.FindControl(grdItemname[1]);
                                rd.Visible = true;
                            }

                            break;
                            case 14:
                                var pnl = (Panel)mpContentPlaceHolder.FindControl(dtMenu.Rows[i]["ControlName"].ToString());
                                if (pnl != null)
                                    pnl.Visible = true;
                                break;
                        }

                    }
                }
            }
        }
    }
}