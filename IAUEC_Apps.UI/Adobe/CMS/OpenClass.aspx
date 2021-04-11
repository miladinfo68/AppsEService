<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="OpenClass.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.OpenClass" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    شماره دانشجویی را وارد نمایید:
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="btn_OpenClass" runat="server" Text="باز شدن کلاس" OnClick="btn_OpenClass_Click" />
    <uc1:AccessControl ID="AccessControl1" runat="server" />
    <telerik:RadWindowManager ID="rdw" runat="server" Height="400px" 
       Width="570px">
    </telerik:RadWindowManager>
</asp:Content>
