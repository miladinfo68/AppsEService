<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="ExamPlaceListSans.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ExamPlaceListSans" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <style>
        #main {
            margin: 0 auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <div id="ttlMsg">
        <h4>گزارش گیری بر اساس محل برگزاری سانس امتحان</h4>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwmValidations" runat="server">
    </telerik:RadWindowManager>
    <br />

    <div id="content-section">
        <div id="div_Main" style="padding-left: 15%; padding-right: 5%;">
            <asp:Panel ID="pnl_Main" runat="server" HorizontalAlign="Center" Width="80%" Direction="RightToLeft">
                <div id="main" dir="rtl">
                    <fieldset>
                        <%--    <legend style="color: #000000">لیست محل برگزاری سانس</legend>
                        --%>
                        <table style="width: 100%; margin-bottom: 5px">
                            <tr>
                                <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000; font-weight: bold; text-align: left">دانشکده را انتخاب نمایید : 
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_Danesh" runat="server" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label1" runat="server" Text="*ترم : " Font-Bold="true" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                                </td>
                            </tr>


                            <tr>
                                <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000; font-weight: bold; text-align: left">نام شهر را انتخاب نمایید : 
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_shahr" runat="server" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                                </td>


                                <td style="text-align: left">
                                    <asp:Label ID="lbl_ExamDay" runat="server" Text="روز امتحان :" ForeColor="Black" Font-Size="Medium" Font-Bold="true" Font-Names="B Nazanin"></asp:Label>
                                </td>
                                <td class="auto-style1">
                                    <asp:DropDownList ID="ddl_ExamDate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_ExamDate_SelectedIndexChanged" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                                </td>

                            </tr>

                            <tr>
                                <td style="text-align: left">
                                    <asp:Label ID="lbl_Time" runat="server" Text="ساعت امتحان :" ForeColor="Black" Font-Size="Medium" Font-Bold="true" Font-Names="B Nazanin"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ExamTime" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btn_ExamPlaceList" runat="server" OnClick="btnExamPlaceList_Click" Text="محل برگزاری" CssClass="btn btn-exam" />
                        <uc1:AccessControl ID="AccessControl1" runat="server" />

                    </fieldset>
                </div>
            </asp:Panel>
        </div>
        <br />
        <br />
        <div>

            <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />
        </div>
    </div>
</asp:Content>
