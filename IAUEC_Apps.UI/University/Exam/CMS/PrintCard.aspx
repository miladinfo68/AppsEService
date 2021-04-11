<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="PrintCard.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.PrintCard" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl" style="font-size: medium; font-family: 'B Nazanin'; color: #000000;">
        <legend>پرینت کارت امتحان دانشجو</legend>
        <asp:Panel ID="pnl_Main" runat="server">

            <table>
                <tr>
                    <td style="font-family: 'b nazanin'; font-size: medium; color: #000000">شماره دانشجویی را وارد نمایید:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_StNo" runat="server" ForeColor="Black"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btn_Save" OnClick="btn_Save_Click" runat="server" CssClass="btn btn-exam" Text="نمایش اطلاعات" />
                    </td>
                </tr>

            </table>
        </asp:Panel>
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />

    </div>
</asp:Content>
