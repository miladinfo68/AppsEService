<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="Request_Class.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.Pages.Request_Class" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista">
    </telerik:RadWindowManager>

    <div>
        <asp:Label ID="lbl_SelectSemester" runat="server" Text="انتخاب نیمسال"></asp:Label>
        <asp:DropDownList ID="ddl_Nimsal" runat="server"></asp:DropDownList>
        <asp:Label ID="lbl_ClassType" runat="server" Text="نوع کلاس"></asp:Label>
        <asp:DropDownList ID="ddl_ClassType" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="lbl_ClassName" runat="server" Text="نام کلاس"></asp:Label>
        <asp:TextBox ID="txt_ClassName" runat="server" Text="" ></asp:TextBox>
        <asp:Label ID="lbl_CourseName" runat="server" Text="نام درس"></asp:Label>
        <asp:TextBox ID="txt_CourseName" runat="server" Text="" ></asp:TextBox>
        <br />
        <asp:Label ID="lbl_CourseCount" runat="server" Text="انتخاب تعداد جلسات"></asp:Label>
        <asp:DropDownList ID="ddl_MeetingCount" runat="server"></asp:DropDownList>
        <asp:Label ID="lbl_CountOfUser" runat="server" Text="مجموع کاربران کلاس"></asp:Label>
        <asp:TextBox ID="txt_CountOfUser" runat="server" Text="" MaxLength="3" ></asp:TextBox>
        <br />
        <asp:Label ID="txt_Day" runat="server" Text="روز برگذاری کلاس"></asp:Label>
        <asp:DropDownList ID="ddl_Day" runat="server"></asp:DropDownList>        
        <asp:Label ID="lbl_TimeStart" runat="server" Text="ساعت شروع کلاس"></asp:Label>
        <asp:TextBox ID="txt_TimeStart" runat="server" Text="" MaxLength="5" ></asp:TextBox>
        <asp:Label ID="lbl_TimeEnd" runat="server" Text="ساعت پایان کلاس"></asp:Label>
        <asp:TextBox ID="txt_TimeEnd" runat="server" Text="" MaxLength="5" ></asp:TextBox>
        <br />
        <asp:Button ID="btn_RegisterRequest" runat="server" Text="ثبت درخواست" OnClick="btn_RegisterRequest_Click" />
    </div>









</asp:Content>
