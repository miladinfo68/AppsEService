<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="TAGetExcelFile.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.TAGetExcelFile" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl">
        <INPUT id="FileInput"  type="file"  name="File1" runat="server" dir="rtl">       
        <asp:button id="btn_Upload" runat="server" Text="Upload" OnClick="btnUpload_Click"></asp:button>
        <asp:Label id="lbl_Info" runat="server" ></asp:Label>
        <uc1:AccessControl ID="AccessControl1" runat="server" />
    </div>
  
</asp:Content>
