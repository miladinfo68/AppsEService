<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="changePassword.aspx.cs" Inherits="IAUEC_Apps.UI.changePassword" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="p" runat="server" DefaultButton="btn_Change"> 
     <table class="table-responsive" style="width:100%" dir="ltr">
        <tr>
            <td dir="rtl">
                <asp:TextBox ID="txt_OldPass" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td dir="rtl" style="width: 20%; font-family: tahoma; font-size: small;">رمز عبور فعلی:&nbsp;</td>
        </tr>
        <tr>
            <td dir="rtl">
                <asp:TextBox ID="txt_NewPass" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td dir="rtl" style="width: 20%; font-family: tahoma; font-size: small;">رمز عبور جدید:</td>
        </tr>
        <tr>
            <td dir="rtl">
                <asp:TextBox ID="txt_ConfNewPass" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td dir="rtl" style="width: 20%; font-family: tahoma; font-size: small;">تکرار رمز عبور جدید:</td>
        </tr>
        <tr>
            <td dir="rtl">
                <asp:Button ID="btn_Change" runat="server" Font-Names="Tahoma" Font-Size="Small" OnClick="btnchange_Click" Text="تغییر رمز عبور" />
                <asp:Label ID="lbl_Message" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="#FF3300"></asp:Label>
                <uc1:AccessControl ID="AccessControl1" runat="server" />
            </td>
            <td dir="rtl" style="width: 20%; font-family: tahoma; font-size: small;"></td>
        </tr>
    </table>
    </asp:Panel>
</asp:Content>
