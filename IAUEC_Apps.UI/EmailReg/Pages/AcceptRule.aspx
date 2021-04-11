<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/PageEmailMaster.Master" AutoEventWireup="true" CodeBehind="AcceptRule.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.Pages.AcceptRule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:panel ID="Panel_Rule" Direction="RightToLeft" runat="server" BorderWidth ="3px" HorizontalAlign="Right">
        <asp:Label ID="lbl_ShowTextRule" runat="server" Font-Names="Tahoma" Font-Size="Small" dir="rtl"> </asp:Label>
       <br />
    </asp:panel>
    <asp:CheckBox ID="chk_AcceptRule" runat="server" Text="با قوانین ذکر شده در کادر فوق موافقم." TextAlign="Right" style="float: right" />
    <br />
    <br />
    <%--<button id="btn_Accept" runat="server" text ="تایید و ادامه" onclick="btn_AcceptRule_click" style="width:25px; font-size:small" ></button> --%>
    <asp:Button id="btn_Accept" runat="server" text ="تایید و ادامه" onclick="btn_AcceptRule_click" style="font-size:small" Width="100px" />
    <telerik:RadWindowManager ID ="rwm" runat ="server"></telerik:RadWindowManager>
</asp:Content>
