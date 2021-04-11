<%@ Page Title="" Language="C#" MasterPageFile="~/University/Support/MasterPage/CMSSupportMaster.Master" AutoEventWireup="true" CodeBehind="SendEmail.aspx.cs" Inherits="IAUEC_Apps.UI.University.Support.CMS.SendEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <div class="content" dir="rtl">
        <div class="row">
            <div class="col-md-6">
                ایمیل:
            </div>
            <div class="col-md-6">
                <asp:TextBox ID="txt_Email" runat="server"></asp:TextBox>
            </div>
           
        </div>
        <div class="row">
             <div class="col-md-6">
                 <asp:Button ID="btn_SendEmail" runat="server" Text="ارسال" OnClick="btn_SendEmail_Click" />
            </div>
        </div>
    </div>

</asp:Content>
