<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="RegistrationReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.RegistrationReport" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
       <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style type="text/css">
    .rcbItem
    {
        font-family: tahoma;
      
    }
    .rcbHovered
{
    font-family: Tahoma;
    font-weight:bold;
}

</style>
      <link rel="stylesheet" type="text/css" media="all" href="../../../Adobe/css/aqua/theme.css" title="Aqua" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
         <asp:Literal ID="pt" runat="server"></asp:Literal>
                      </asp:Content> 
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7);background-color:rgba(231, 230, 230,0.3);padding: 1%;border-radius:5px; margin-bottom:1%;">
        <div class="row">
      <div class="col-md-1">:دانشکده</div>
                 <div class="col-md-2">
                     <telerik:RadComboBox ID="ddl_Daneshkade" Runat="server" EmptyMessage="انتخاب نمایید..." Skin="Windows7"  AutoPostBack="true">
                     </telerik:RadComboBox>
                 </div> 
    <div class="col-md-2">
                     <asp:Button CssClass="btn btn-warning"  id="View_rptt" runat="server" OnClick="View_rptt_Click" Text="مشاهده گزارش" /></div>
            </div>
</asp:Content>
