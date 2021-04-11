<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/PageAdobeMaster.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.Pages.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Button ID="btn_Pay" runat="server" Text="پرداخت" OnClick="btnPay_Click" />
</asp:Content>

