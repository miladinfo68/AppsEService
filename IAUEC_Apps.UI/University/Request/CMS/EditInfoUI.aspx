<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditInfoUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.EditInfoUI" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Theme/css/bootstrap-rtl.css" rel="stylesheet" />
    <link rel="stylesheet" href="../../Theme/css/bootstrap-progressbar-3.1.1-rtl.css" />
    <link rel="stylesheet" href="../../Theme/css/jquery-jvectormap-rtl.css" />
    <link href="../../Theme/css/style-rtl.css" rel="stylesheet" />
    <link href="../../Theme/css/responsive-rtl.css" rel="stylesheet" />
    <link href="../../Theme/css/style.css" rel="stylesheet" />
    <title>ویرایش اطلاعات</title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>
        <script type="text/javascript">
            function CloseAndRebind(args) {
                GetRadWindow().BrowserWindow.refreshGrid(args);
                GetRadWindow().close();
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;

                return oWindow;
            }

            function CancelEdit() {
                GetRadWindow().close();
            }
        </script>
        <asp:Panel ID="Confirmpnl" runat="server" Visible="false">
            <div class="col-md-3" style="border: 1px solid #B90000; border-radius: 5px; padding-bottom: 8px;">
                <p style="color: #FF0000; font-weight: bold">آیا از تغییرات مطمئن هستید؟</p>
                <telerik:RadButton ID="btn_Conf" runat="server" Text="بله" Font-Names="Tahoma" OnClick="conf_Click" Skin="Windows7"></telerik:RadButton>
                <telerik:RadButton ID="btn_NotConf" runat="server" Text=" خیر " Font-Names="tahoma" OnClick="notConf_Click" Skin="Windows7"></telerik:RadButton>

            </div>
        </asp:Panel>
        <div class="col-md-3">
            <telerik:RadListView ID="rdl_PersPic" runat="server" Visible="False">

                <ItemTemplate>
                    <div id="vlightbox2" class="col-md-12" style="margin-bottom: 2px; margin-left: 0px; border: 1px solid rgba(33, 178, 187,0.6); padding: 2% 1% 2% 2%; box-shadow: rgba(33, 178, 187,0.2) 5px 5px inset; border-radius: 5px; text-align: center">
                        <div class="col-md-12" style="background-color: rgba(126, 208, 223,0.5); margin-bottom: 2px">
                            <asp:Label ID="lbl_New" Text="اطلاعات جدید" runat="server" Font-Bold="True"></asp:Label>
                            <br />

                            <span style="font-weight: bold; font-size: x-small; color: #000000;">
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lbl_PersonalImg" Text="عکس پرسنلی:" Font-Bold="true" runat="server" Visible="true"></asp:Label>
                            <%--<asp:Label ID="lblReqId" runat="server" Text='<%# Eval("StudentRequestID") %>' Visible="false"></asp:Label>--%>
                            <telerik:RadBinaryImage ID="img_PersonalPic" runat="server" Height="150px" ResizeMode="Fit" Width="124px" Visible="true" />
                            <asp:RadioButton ID="rdb_ConfPic" runat="server" Checked="true" Text="قابل قبول" GroupName="c1" Width="100%" ForeColor="#000000" AutoPostBack="True" />
                            <asp:RadioButton ID="rdb_FailedPic" runat="server" Text="رد" Width="100%" GroupName="c1" ForeColor="Red" AutoPostBack="True" />
                            <asp:TextBox ID="txt_RadPic" runat="server" Visible="false" />

                        </div>
                    </div>
                </ItemTemplate>
            </telerik:RadListView>
            <br />
        </div>

        <div class="col-md-3">
            <telerik:RadListView ID="rdlv_New" runat="server">

                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </LayoutTemplate>

                <ItemTemplate>
                    <div id="vlightbox3" class="col-md-12" style="margin-bottom: 2px; margin-left: 0px; border: 1px solid rgba(33, 178, 187,0.6); padding: 2% 1% 2% 2%; box-shadow: rgba(33, 178, 187,0.2) 5px 5px inset; border-radius: 5px; text-align: center">
                        <div class="col-md-12" style="background-color: rgba(126, 208, 223,0.5); margin-bottom: 2px">
                            <asp:Label ID="lbl_New" Text="اطلاعات جدید" runat="server" Font-Bold="True"></asp:Label>
                            <br />

                        </div>

                        <br />

                        <asp:Label ID="lbl_NewContent" runat="server" Visible="true" Text='<%#Eval("NewContent")%>'></asp:Label>
                        <br />
                        <asp:Label ID="lbl_EditeTypeName1" runat="server" Visible="true" Text="مورد ویرایش:"></asp:Label>
                        <asp:Label ID="lbl_EditeTypeName" runat="server" Visible="true" Text='<%#Eval("EditeTypeName")%>'></asp:Label>
                        <asp:Label ID="lbl_EditedID" runat="server" Visible="false" Text='<%#Eval("EditedID")%>'></asp:Label>
                        <%--<asp:Label ID="lblReqId" runat="server" Visible="false"; Text='<%# Eval("StudentRequestID") %>'></asp:Label>--%>
                        <asp:RadioButton ID="rdb_ConfNewInfo" runat="server" Checked="true" Text=" قابل قبول" GroupName="c3" Width="100%" ForeColor="#000000" AutoPostBack="True" />
                        <asp:RadioButton ID="rdb_NewInfo" runat="server" Text="رد" Width="100%" GroupName="c3" ForeColor="Red" AutoPostBack="True" />
                        <asp:TextBox ID="txt_RadEdit" runat="server" Visible="false" />


                    </div>
                </ItemTemplate>
            </telerik:RadListView>
            <br />
        </div>




        <div class="col-md-3">
            <telerik:RadListView ID="rdlv_previous" runat="server">

                <ItemTemplate>
                    <div id="vlightbox1" class="col-md-12" style="margin-bottom: 2px; margin-left: 0px; border: 1px solid rgba(33, 178, 187,0.6); padding: 2% 1% 2% 2%; box-shadow: rgba(33, 178, 187,0.2) 5px 5px inset; border-radius: 5px; text-align: center">
                        <div class="col-md-12" style="background-color: rgba(126, 208, 223,0.5); margin-bottom: 2px">
                            <asp:Label ID="lbl_Prev" Text="اطلاعات قبلی" runat="server" Font-Bold="True"></asp:Label>
                            <br />
                            <asp:Label ID="lbl_PersonalImgPrev" Text="عکس پرسنلی" runat="server" Visible="false"></asp:Label>
                            <telerik:RadBinaryImage ID="img_PersonalPicPrev" runat="server" Height="150px" ResizeMode="Fit" Width="124px" Visible="false" />
                            <br />
                            <asp:Label ID="lbl_Tell" runat="server" Visible="true" Text="شماره تلفن:"></asp:Label>
                            <asp:Label ID="lbl_TellPrev" runat="server" Visible="true" Text='<%#Eval("homePhone")%>'></asp:Label>
                            <br />
                            <asp:Label ID="lbl_moblbl" runat="server" Visible="true" Text="شماره همراه:"></asp:Label>
                            <asp:Label ID="lbl_MobilePrev" runat="server" Visible="true" Text='<%#Eval("mobile") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lbl_CodePosti" runat="server" Visible="true" Text="کدپستی:"></asp:Label>
                            <asp:Label ID="lbl_CodePostiPrev" runat="server" Visible="true" Text='<%#Eval("home_postalCode")%>'></asp:Label>
                            <br />
                            <asp:Label ID="lbl_Address" runat="server" Visible="true" Text="آدرس:"></asp:Label>
                            <asp:Label ID="lbl_AddressPrev" runat="server" Visible="true" Text='<%#Eval("homeAddress")%>'></asp:Label>
                </ItemTemplate>
            </telerik:RadListView>
        </div>


        <asp:Button ID="btn_Taeid" runat="server" OnClick="btn_taeid_Click" CssClass="btn btn-info" Text="اعمال تغییرات" />

    </form>
</body>
</html>
