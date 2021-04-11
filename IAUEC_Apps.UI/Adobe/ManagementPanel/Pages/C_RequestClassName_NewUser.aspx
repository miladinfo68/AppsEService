<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="C_RequestClassName_NewUser.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.Pages.C_RequestClassName_NewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

    <div>
        <asp:Label ID="lbl_ClassId" runat="server" Text="" Visible="false"></asp:Label>

        <asp:Label ID="lbl_Name" runat="server" Text="نام"></asp:Label>
        <asp:TextBox ID="txt_Name" runat="server" Text="" ></asp:TextBox>
        <asp:Label ID="lbl_Family" runat="server" Text="نام خانوادگی"></asp:Label>
        <asp:TextBox ID="txt_Family" runat="server" Text="" ></asp:TextBox>
        <br />
        <asp:Label ID="lbl_LatinName" runat="server" Text="نام لاتین"></asp:Label>
        <asp:TextBox ID="txt_LatinName" runat="server" Text="" data-original-title="از حروف انگلیسی استفاده نمایید"   
             onKeypress="if ( event.keyCode<48 || (event.keyCode>57 && event.keyCode < 65) || (event.keyCode > 91 && event.keyCode <97) || event.keyCode>122) event.returnValue = false;" >   
        </asp:TextBox>
        <asp:Label ID="lbl_LatinFamily" runat="server" Text="نام خانوادگی لاتین"></asp:Label>
        <asp:TextBox ID="txt_LatinFamily" runat="server" Text=""  data-original-title="از حروف انگلیسی استفاده نمایید"   
             onKeypress="if ( event.keyCode<48 || (event.keyCode>57 && event.keyCode < 65) || (event.keyCode > 91 && event.keyCode <97) || event.keyCode>122) event.returnValue = false;" >   
        </asp:TextBox>
        <br />
        <asp:Label ID="lbl_Mobile" runat="server" Text="شماره موبایل"></asp:Label>
        <asp:TextBox ID="txt_Mobile" runat="server" Text="" ></asp:TextBox>
        <asp:Label ID="lbl_Email" runat="server" Text="پست الکترونیکی"></asp:Label>
        <asp:TextBox ID="txt_Email" runat="server" Text="" ></asp:TextBox>
        <br />
        <asp:Label ID="lbl_UserName" runat="server" Text="نام کاربری"></asp:Label>
        <asp:TextBox ID="txt_UserName" runat="server" Text="" ></asp:TextBox>
        <br />
        <asp:Label ID="lbl_NationalCode" runat="server" Text="کدملی"></asp:Label>
        <asp:TextBox ID="txt_NationalCode" runat="server" Text="" ></asp:TextBox>
        <asp:Label ID="lbl_IdNumber" runat="server" Text="شماره شناسنامه"></asp:Label>
        <asp:TextBox ID="txt_IdNumber" runat="server" Text="" ></asp:TextBox>
        <br />
        <telerik:RadComboBox ID="ddl_Sex" runat="server"
            Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="false"            
            Label="جنسیت: " Skin="Office2010Silver" >
        </telerik:RadComboBox>

        <telerik:RadComboBox ID="ddl_Access" runat="server"
            Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="false"            
            Label="نوع دسترسی: " Skin="Office2010Silver">        
        </telerik:RadComboBox>


    </div>
    <div>
        <telerik:RadButton ID="rbtn_NewUser" runat="server" Text="RadButton" OnClick="rbtn_NewUser_Click"></telerik:RadButton>
    </div>
    
</asp:Content>
 