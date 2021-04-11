<%@ Page Title="" Language="C#" MasterPageFile="~/University/Support/MasterPage/CMSSupportMaster.Master" AutoEventWireup="true" CodeBehind="ProfessorResetPassword.aspx.cs" Inherits="IAUEC_Apps.UI.University.Support.CMS.ProfessorResetPassword" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <telerik:RadWindowManager ID="rwm_Validations" runat="server">
    </telerik:RadWindowManager>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" dir="rtl">
        <div class="panel panel-purple">
            <div class="panel panel-heading">
                <h3>تغییر رمز عبور استاد </h3>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-3">
                        کد استاد
                        <asp:TextBox ID="txtProfessorCode" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        رمز عبور جدید
                        <asp:TextBox ID="txtNewPassword" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnReset" runat="server" CssClass="btn"  text="ذخیره" OnClick="btnReset_Click"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-9">
                        <h1 class="alert " id="searchStatus" runat="server"></h1>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
