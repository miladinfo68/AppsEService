<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="FacultyReports.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.FacultyReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--        <div> <p style="font-weight: 700;font-size:x-large;text-align:right;direction:rtl;font-family:Tahoma">
      اساتید</p>
    <p style="font-weight: 700;font-size:x-large;text-align:right;direction:rtl;font-family:Tahoma">
        &nbsp;</p>--%>
       <%--</div>--%>
    <br />
   <div>
<telerik:RadMenu ID="RadMenu1" CssClass="mainMenu" runat="server" Skin="Silk" ShowToggleHandle="true" align="rtl">
            <Items>
                <telerik:RadMenuItem Text="گزارشات اساتید">
                    
                    <Items>
                        <telerik:RadMenuItem Text="فرم ابلاغ / سابقه / فرم الف و ب اساتید" NavigateUrl="ListEblaghAsatid.aspx" runat="server"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="فرم ارزشیابی اساتید" NavigateUrl="ArzeshyabiAsatid.aspx" runat="server" ></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="لیست اساتید در ترم جاری / کارت تردد" runat="server" NavigateUrl="ListAsatidDarHarTerm.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="لیست تداخل کلاس" runat="server" NavigateUrl="ListTadakholKlass.aspx"></telerik:RadMenuItem>
<%--                        <telerik:RadMenuItem Text="لیست حضور و غیاب اساتید / مدیران" runat="server" NavigateUrl="PresenceAndAbsenceProf.aspx"></telerik:RadMenuItem>--%>
                        <telerik:RadMenuItem Text="لیست ساعت غیبت و جبرانی" runat="server" NavigateUrl="ListHoursAndAbsencesOfCountervailing.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="لیست اساتیدی که ارجاع به استاد دارند /ریزش نمره" runat="server" NavigateUrl="ReferToMaster.aspx"></telerik:RadMenuItem>
<%--                        <telerik:RadMenuItem Text="صدور کارت ثبت نام اینترنتی" runat="server" NavigateUrl="RegistrationCardOnline.aspx"></telerik:RadMenuItem>--%>
                        <telerik:RadMenuItem Text="لیست حق التدریس" runat="server" NavigateUrl="ListTadris.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="تاریخ و ساعت امتحان اساتید" runat="server" NavigateUrl="ShowDateAndClockExamTheMaster.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="لیست دانشجویان افتاده" runat="server" NavigateUrl="PercentageOfFallenStudents.aspx"></telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenu>
        </div> 
</asp:Content>
