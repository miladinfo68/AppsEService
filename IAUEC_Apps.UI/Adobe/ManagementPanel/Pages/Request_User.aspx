<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="Request_User.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.Pages.Request_User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista">
    </telerik:RadWindowManager>

    <div>
        <asp:Label ID="lbl_TypeAccount" runat="server" Text="انتخاب نوع کاربری"></asp:Label>
        <asp:DropDownList ID="ddl_TypeAccount" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="lbl_NameFa" runat="server" Text="نام (فارسی)"></asp:Label>
        <asp:TextBox ID="txt_NameFa" runat="server" Text="" ></asp:TextBox>
        <asp:Label ID="lbl_FamilyFa" runat="server" Text="نام خانوادگی (فارسی)"></asp:Label>
        <asp:TextBox ID="txt_FamilyFa" runat="server" Text="" ></asp:TextBox>
        <br />
        <asp:Label ID="lbl_NameEn" runat="server" Text="نام (لاتین)"></asp:Label>
        <asp:TextBox ID="txt_NameEn" runat="server" Text="" ></asp:TextBox>
        <asp:Label ID="lbl_FamilyEn" runat="server" Text="نام خانوادگی (لاتین)"></asp:Label>
        <asp:TextBox ID="txt_FamilyEn" runat="server" Text="" ></asp:TextBox>
        <br />
        <asp:Label ID="lbl_NationalID" runat="server" Text="شماره ملی"></asp:Label>
        <asp:TextBox ID="txt_NationalID" runat="server" Text="" MaxLength="10" ></asp:TextBox>
        <asp:Label ID="lbl_Mobile" runat="server" Text="شماره موبایل"></asp:Label>
        <asp:TextBox ID="txt_Mobile" runat="server" Text="" ></asp:TextBox>
        <br />
        <asp:Label ID="lbl_EmailAddress" runat="server" Text="پست الکترونیکی"></asp:Label>
        <asp:TextBox ID="txt_EmailAddress" runat="server" Text="" ></asp:TextBox>
        <br />
        <asp:Label ID="lbl_UserName" runat="server" Text="نام کاربری"></asp:Label>
        <asp:TextBox ID="txt_UserName" runat="server" Text="" ></asp:TextBox>
        <asp:Label ID="lbl_Password" runat="server" Text="کلمه عبور"></asp:Label>
        <asp:TextBox ID="txt_Password" runat="server" Text="" ></asp:TextBox>
        <br />
        
        <asp:Button ID="btn_RegisterRequest" runat="server" Text="ثبت درخواست" OnClick="btn_RegisterRequest_Click" />
    </div>




    
</asp:Content>
