<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.Report" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        .reportWrapper .RadPivotGrid td {
            text-align: right;
        }

        .reportWrapper {
            direction: rtl;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <div id="ttlMsg">
        <h4> گزارش گیری کلی به تفکیک حوزه و سانس  امتحانی</h4>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="reportWrapper">
        <div class="row">
            <div class="col-sm-11">
                <span>ترم: </span>
                <asp:DropDownList runat="server" ID="ddlTerm" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-1">
                <asp:ImageButton ID="btn_ConvertToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
                    OnClick="btn_ConvertToExcel_Click" AlternateText="Convert To Excel" Width="40" />
            </div>
        </div>


        <uc1:AccessControl ID="AccessControl1" runat="server" />
        <telerik:RadPivotGrid ID="RadPivotGrid1" runat="server" AllowFiltering="False" AllowSorting="True" Font-Names="Tahoma" Font-Size="Small" OnNeedDataSource="RadPivotGrid1_NeedDataSource" RowTableLayout="Compact" ShowDataHeaderZone="False" ShowFilterHeaderZone="False" Width="100%">
            <PagerStyle AlwaysVisible="true" Mode="NextPrevNumericAndAdvanced" />
            <Fields>
                <telerik:PivotGridRowField Caption="شهر" DataField="Name_City" ShowGroupsWhenNoData="false">
                </telerik:PivotGridRowField>
                <telerik:PivotGridRowField Caption="روز" DataField="dateexam" ShowGroupsWhenNoData="false">
                </telerik:PivotGridRowField>
                <telerik:PivotGridColumnField Caption="ساعت" DataField="saatexam">
                </telerik:PivotGridColumnField>
                <telerik:PivotGridAggregateField Aggregate="sum" Caption="تعداد" CellStyle-Width="100px" DataField="tedad"
                    GrandTotalAggregateFormatString="مجموع: {0}" TotalFormatString="مجموع: {0}">
                    <TotalFormat Axis="Rows" Level="0" TotalFunction="NoCalculation" />
                    <CellStyle Width="100px" />
                </telerik:PivotGridAggregateField>
            </Fields>
            <TotalsSettings GrandTotalsVisibility="RowsAndColumns" />
            <ConfigurationPanelSettings EnableOlapTreeViewLoadOnDemand="True" />
        </telerik:RadPivotGrid>

    </div>
</asp:Content>

