<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="Request_CreateClass.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.Pages.Request_CreateClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
        <asp:DropDownList ID="ddl_CountOfUser" runat="server"></asp:DropDownList>           
        <asp:Label ID="lbl_TimeClass" runat="server" Text="زمان کلاس"></asp:Label>
        <asp:DropDownList ID="ddl_TimeClass" runat="server"></asp:DropDownList>    
        <br />
        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
        </asp:CheckBoxList>
        <br />
        <asp:Button ID="btn_RegisterRequest" runat="server" Text="ثبت درخواست" OnClick="btn_RegisterRequest_Click" />
    </div>


</asp:Content>
