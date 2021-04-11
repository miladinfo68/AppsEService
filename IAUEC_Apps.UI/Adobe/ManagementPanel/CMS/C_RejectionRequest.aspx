<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="C_RejectionRequest.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.CMS.C_RejectionRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista">
    </telerik:RadWindowManager>

    <div>
        <asp:Label ID="lbl_Id" runat="server" Text="" Visible="false"></asp:Label>
        
        
        <telerik:RadTextBox runat="server" Label="رد درخواست" ID="txt_Detail" EmptyMessage="نوشتن دلایل" 
            TextMode="MultiLine" Height="25%" Width="50%" Resize="None">
        </telerik:RadTextBox><br />
        <telerik:RadButton runat="server" ID="btn_Save" Text="ثبت" CssClass="submitButton" 
            OnClick="btn_Save_Click"></telerik:RadButton>
        <telerik:RadButton runat="server" ID="btn_Cancel" Text="بازگشت بدون تغییر" CssClass="submitButton" 
          OnClick="btn_Cancel_Click"></telerik:RadButton>

    </div>
</asp:Content>
