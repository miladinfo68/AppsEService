<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="EducationReports.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.EducationReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div> <p style="font-weight: 700;font-size:x-large;text-align:right;direction:rtl;font-family:Tahoma">
      آموزش</p>
    <p style="font-weight: 700;font-size:x-large;text-align:right;direction:rtl;font-family:Tahoma">
        &nbsp;</p>
       </div>
    <br />
    <div>
<telerik:RadMenu ID="RadMenu1" CssClass="mainMenu" runat="server" Skin="Silk" ShowToggleHandle="true" align="rtl">
            <Items>
                <telerik:RadMenuItem Text="گزارشات آموزشی">
                    
                    <Items>
                        <telerik:RadMenuItem Text="لیست بر اساس شماره کلاس" NavigateUrl="ListNumberClass.aspx" runat="server"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="لیست بر اساس کلاس" NavigateUrl="ListClass.aspx" runat="server" ></telerik:RadMenuItem>
<%--                    <telerik:RadMenuItem Text="گزارش فرم های طراحی شده" runat="server" NavigateUrl="ReportFormDesign.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="گزارش لیست های طراحی شده" runat="server"></telerik:RadMenuItem>--%>
                        <telerik:RadMenuItem Text="لیست مغایرت نمرات ثبت شده" runat="server" NavigateUrl="ContrastScore.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="لیست دانشجویان عدم مجوز ثبت نام" runat="server" NavigateUrl="NotRegistrationLicense.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="برنامه هفتگی گروه" runat="server" NavigateUrl="BarnameHaftegi.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="لیست دروس قبولی بر اساس رشته" runat="server" NavigateUrl="ListDorusGhabuli.aspx"></telerik:RadMenuItem>
<%--                     <telerik:RadMenuItem Text="صدور گواهی ثبت شده وب" runat="server" NavigateUrl="ListEshteghalBeTahsil.aspx"></telerik:RadMenuItem>--%>
                    </Items>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenu>
        </div>
    </asp:Content>
