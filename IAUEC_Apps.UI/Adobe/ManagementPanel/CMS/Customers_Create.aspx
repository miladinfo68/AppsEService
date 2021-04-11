<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="Customers_Create.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.CMS.Customers_Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista">
    </telerik:RadWindowManager>


    <div>
        <asp:Label ID="lbl_CustomerName" runat="server" Text="نام موسسه"></asp:Label>
        <asp:TextBox ID="txt_CustomerName" runat="server" Text="" ></asp:TextBox>

        <br />
        <asp:Label ID="lbl_CustomerTel" runat="server" Text="شماره تلفن"></asp:Label>
        <asp:TextBox ID="txt_CustomerTel" runat="server" Text="" ></asp:TextBox>
       
        <asp:Label ID="lbl_CustomerFax" runat="server" Text="شماره فکس"></asp:Label>
        <asp:TextBox ID="txt_CustomerFax" runat="server" Text="" ></asp:TextBox>
        <br />
        <asp:Label ID="lbl_CustomerEmail" runat="server" Text="آدرس ایمیل"></asp:Label>
        <asp:TextBox ID="txt_CustomerEmail" runat="server" Text="" ></asp:TextBox>

        <br />
        <asp:Label ID="lbl_CustomerAddress" runat="server" Text="آدرس پستی"></asp:Label>
        <asp:TextBox ID="txt_CustomerAddress" runat="server" Text="" Width="400"></asp:TextBox>

        <br />
        <asp:Label ID="lbl_CustomerUser" runat="server" Text="نام نماینده"></asp:Label>
        <asp:TextBox ID="txt_CustomerUser" runat="server" Text="" ></asp:TextBox>
    
        <asp:Label ID="lbl_CustomerUserMobile" runat="server" Text="موبایل نماینده"></asp:Label>
        <asp:TextBox ID="txt_CustomerUserMobile" runat="server" Text="" ></asp:TextBox>

        <br />
        <asp:Button ID="btn_CreateCustomer" runat="server" Text="ثبت" OnClick="btn_CreateCustomer_Click" />

    </div>
    <div runat="server" id="CustomerInfo" visible="false">
        <br />
        <asp:Label ID="lbl_UserAdobe" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_UserPass" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_CustomerInfo" runat="server" Text="اطلاعات برای کاربر چگونه ارسال شود"></asp:Label>   
        <asp:RadioButtonList ID="rbtn_SendType" runat="server" >
            <asp:ListItem value="Email">ایمیل</asp:ListItem>
            <asp:ListItem value="SMS">اس ام اس</asp:ListItem>
            <asp:ListItem value="Both">هردو</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <asp:Button ID="btn_SendInfo" runat="server" Text="ارسال" OnClick="btn_SendInfo_Click" />

    </div>
    


</asp:Content>
