<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/PageAdobeMaster.Master" AutoEventWireup="true" CodeBehind="SelectAct.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.Pages.SelectAct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <div class="container">
        <div class="row" style="margin-top: 2%;padding-top: 8px; padding-bottom: 8px;border-radius:8px">
                         <div class="col-md-12">
                               <div class="col-md-3">
                                   
                               </div>
                               <div class="col-md-5">
              <div class="registrationForm">
        <div class="titleBar" style="text-align: center">
            <%--<div class="titleBarBgr"></div>--%>
            <span></span>
        </div>
           <div class="col-md-12" style="margin-bottom: 7%;margin-top:7%">
                                       <telerik:RadButton ID="rdb_ClassList" runat="server" Text="لیست کلاس ها" CssClass="RadButton RadButton_Silk rbSkinnedButton rbPrimaryButton btnSubmit" Skin="Silk" Font-Bold="True" Font-Names="Tahoma" Width="100%" OnClick="RadButton1_Click">
                                       </telerik:RadButton>
                                   </div>
                                   <div class="col-md-12" style="margin-bottom: 7%">
                                       <telerik:RadButton ID="rdb_Payment" runat="server" Text="پرداخت" CssClass="RadButton RadButton_Silk rbSkinnedButton rbPrimaryButton btnSubmit" Skin="Silk" Font-Bold="True" Font-Names="Tahoma" Width="100%">
                                       </telerik:RadButton>
                                   </div>
                                   <div class="col-md-12" style="margin-bottom: 7%">
                                       <telerik:RadButton ID="rdb_ReceiveFile" runat="server" Text="دریافت فایل" CssClass="RadButton RadButton_Silk rbSkinnedButton rbPrimaryButton btnSubmit" Skin="Silk" Font-Bold="True" Font-Names="Tahoma" Width="100%">
                                       </telerik:RadButton>
                                   </div>
    </div>
                                
                               </div>  <div class="col-md-4"></div></div>
            </div>
            </div>
</asp:Content>
