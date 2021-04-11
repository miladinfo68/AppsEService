<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/PageAdobeMaster.Master" AutoEventWireup="true" CodeBehind="AdobeCallBack.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.Pages.AdobeCallBack" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
     <div dir="rtl">
          <asp:Label ID="lbl_Stcode" runat="server" Visible="false" ></asp:Label>
     <table width="100%" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>
                <table class="MainTable" cellspacing="5" cellpadding="1" align="center">
                    <tr class="HeaderTr">
                        <td colspan="2" align="center" height="25">
                            <asp:Label ID="lbl_PaymentStatus" runat="server" class="HeaderText" Text="وضعیت پرداخت"></asp:Label>
                        </td>
                    </tr>
                  
                    <tr>
                      
                        <td colspan="2" align="center" height="25">
                            <asp:Label ID="lbl_ResCode" runat="server" Text=""></asp:Label>
                            
                        </td>

                    </tr>
                    <tr>
                        <td class="LabelTd">
                            <asp:Label ID="lbl_OrderNumber" runat="server" Text="شماره سفارش:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_SaleOrderID" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                   
                    <tr>
                        <td class="LabelTd">
                            <asp:Label ID="lbl_NumberTracking" runat="server" Text="شماره پیگیری:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_SaleReferenceIdLabel" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        </div>

</asp:Content>
