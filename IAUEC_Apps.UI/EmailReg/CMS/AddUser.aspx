<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/CMSEmailMaster.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.CMS.AddUser" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista">
            </telerik:RadWindowManager>
    <table class="table-responsive" style="width:100%">
        <tr>
            <td style="width: 20%">نام:</td>
            <td>
                <asp:TextBox ID="txt_Name" runat="server"></asp:TextBox> 
            </td>
        </tr>
        <tr>
            <td style="width: 20%">نام کاربری:</td>
            <td>
                <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td >رمز عبور:</td>
            <td >
                <asp:TextBox ID="txt_Pass" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>سمت کاربر:</td>
            <td>
                <asp:DropDownList ID="ddl_Role" runat="server">
                    <asp:ListItem Value="0">انتخاب نمایید...</asp:ListItem>
                    <asp:ListItem Value="1">مدیر ارشد</asp:ListItem>
                    <asp:ListItem Value="2">مدیر سامانه</asp:ListItem>
                    <asp:ListItem Value="3">مدیر شبکه</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td>
                
                <asp:Button ID="Btn_Reg" runat="server" CssClass="Orange" OnClick="BtnReg_Click" Text="ثبت" />
                
                <uc1:AccessControl ID="AccessControl1" runat="server" />
                
            </td>
        </tr>
    </table>
</asp:Content>
