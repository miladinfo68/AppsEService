<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="ExamListByPlace.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ExamListByPlace" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
     <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
       <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl ID="AccessControl1" runat="server" />
    <telerik:RadWindowManager ID="rwmValidations" runat="server"></telerik:RadWindowManager>
    <div dir="rtl">
        <table style="width: 50%; margin:20px auto; margin-bottom: 5px">
            <tr>
                <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000; font-weight: bold; width: 10%">دانشکده را انتخاب نمایید:
                </td>
                <td style="width: 30%">
                    <asp:DropDownList ID="ddl_Danesh" runat="server" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000; font-weight: bold;">*ترم :
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_Date" runat="server" Text="روز امتحان" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_ExamDate" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_ExamPlace" runat="server" Text="*محل امتحان" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_ExamPlace2" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <br />
                    <asp:Button ID="btn_ExamListByPlace" runat="server" Text="نمایش اطلاعات" CssClass="btn btn-exam" OnClick="btn_ExamListByPlace_Click" />
                </td>
            </tr>
        </table>
        <div>
            <cc1:StiWebViewer ID="StiWebViewer2" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />

        </div>
    </div>
</asp:Content>
