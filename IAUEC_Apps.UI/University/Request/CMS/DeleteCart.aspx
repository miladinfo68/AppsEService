<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSRequestMaster.Master" AutoEventWireup="true" CodeBehind="DeleteCart.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.DeleteCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
    <div dir="rtl">
        <asp:Panel ID="pnl_Main" runat="server" HorizontalAlign="Center">
            <table style="width: 50%">
                <tr>
                    <td>شماره دانشجویی:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_StNumber" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btn_Save" CssClass="btn btn-info" runat="server" Text="حذف" OnClick="btn_Save_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
