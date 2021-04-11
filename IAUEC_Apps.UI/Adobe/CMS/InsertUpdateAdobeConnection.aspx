<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertUpdateAdobeConnection.aspx.cs" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" Inherits="IAUEC_Apps.UI.Adobe.CMS.InsertUpdateAdobeConnection" %>

<%@ Register Src="../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>

<asp:Content runat="server" ContentPlaceHolderID="HeaderplaceHolder">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageTitle">
    <asp:Literal ID="pt" runat="server"></asp:Literal>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style>
        .TextBox {
            width:100%;
            height:50px;
        }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-md-1">
                <span>ترم</span>
            </div>
            <div class="col-md-1" dir="ltr">
                <asp:DropDownList ID="ddlTerm" runat="server" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged" Visible="true" AutoPostBack="true"></asp:DropDownList>
                <telerik:RadMaskedTextBox ID="txtTerm" runat="server" Visible="False" LabelWidth="64px" Mask="##-##-#" Width="160px"></telerik:RadMaskedTextBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnNewConnection" runat="server" Text="اضافه کردن کانکشن جدید" CssClass="btn btn-danger" OnClick="btnNewConnection_Click" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnCancel" runat="server" Text="انصراف" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
            </div>
        </div>
        <br />
        <div class="row" dir="ltr">

            <div class="col-md-1">
                <span>دامنه</span>

            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtDomainName" runat="server"  CssClass="TextBox"></asp:TextBox>

            </div>

            <div class="col-md-1">
                <span>رمز ادمین</span>

            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtAdminPassword" runat="server"  CssClass="TextBox"></asp:TextBox>

            </div>
        </div>
        <br />

        <div class="row" dir="ltr">
            <div class="col-md-1">
                <span>نام رشته اتصال</span>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtConName" runat="server"  CssClass="TextBox"></asp:TextBox>
            </div>
            <div class="col-md-1">
                <span>رشته اتصال</span>

            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtConString" runat="server"  CssClass="TextBox" TextMode="MultiLine"></asp:TextBox>

            </div>
        </div>
        <br />
        <div class="row" dir="ltr">
            <div class="col-md-1">
                <span>نام هاست</span>

            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txthName" runat="server" CssClass="TextBox"></asp:TextBox>

            </div>
            <div class="col-md-1">
                <span>رمز هاست</span>

            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txthPass" runat="server" CssClass="TextBox"></asp:TextBox>

            </div>
        </div>
        <br />
        <div class="row" dir="ltr">
            <div class="col-md-1">
                <span>نام کاربر</span>

            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtvName" runat="server"  CssClass="TextBox"></asp:TextBox>

            </div>
            <div class="col-md-1">
                <span>رمز کاربر</span>

            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtvPass" runat="server" CssClass="TextBox"></asp:TextBox>

            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-2">
                <asp:Button runat="server" ID="btnUpdateInsert" Text="به روز رسانی" CssClass="btn btn-success" OnClick="btnUpdateInsert_Click" />
            </div>
        </div>
    </div>





    <uc1:AccessControl ID="AccessControl1" runat="server" />
</asp:Content>
