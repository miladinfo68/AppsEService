<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="CheckOutReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutReport" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <script src="../Content/js-persian-cal.min.js"></script>
    <link href="../Content/js-persian-cal.css" rel="stylesheet" />
    <style>
        .AlignCmb {
            text-align: center;
            padding-bottom:20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl">
        <div class="row" style="margin:10px;">
            <div class="col-md-3" style="margin-left: 40px;">
                بخش:
                <telerik:RadComboBox ID="cmbType" runat="server" Width="80%" CssClass="AlignCmb" EmptyMessage="--انتخاب کنید--"></telerik:RadComboBox>
            </div>
        </div>
        <div class="row" style="margin-right:10px;">
            <div class="col-md-3">
                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ForeColor="Red" Text="*" ValidationGroup="search" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                از تاریخ:
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="AlignCmb"></asp:TextBox>
                <script>
                    var objCal1 = new AMIB.persianCalendar('<%=txtFromDate.ClientID%>',
                        { extraInputID: '<%=txtFromDate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                </script>
            </div>
            <div class="col-md-3">
                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ForeColor="Red" Text="*" ValidationGroup="search" ControlToValidate="txtToDate"></asp:RequiredFieldValidator>
                تا تاریخ:
                <asp:TextBox ID="txtToDate" runat="server" CssClass="AlignCmb"></asp:TextBox>
                <script>
                    var objCal2 = new AMIB.persianCalendar('<%=txtToDate.ClientID%>',
                        { extraInputID: '<%=txtToDate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                </script>
            </div>
            <div class="col-md-1">
                <asp:Button ID="btnSearch" runat="server" Text="جستجو" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </div>
        </div>
    <br />
    <div class="row">        
        <telerik:RadGrid ID="grdResults" runat="server" AutoGenerateColumns="false" AllowPaging="true" MasterTableView-ShowHeadersWhenNoRecords="true" CssClass="table table-bordered table-condensed">
            <HeaderStyle CssClass="bg-primary" />
            <MasterTableView>
            <Columns>
                <telerik:GridTemplateColumn HeaderText="ردیف">
                    <ItemTemplate>
                        <asp:Label ID="Label1" Text="<%# Container.DataSetIndex + 1 %>" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="شماره درخواست" DataField="ModifyId" />
                <telerik:GridBoundColumn HeaderText="نام" DataField="Name" />
                <telerik:GridBoundColumn HeaderText="تاریخ" DataField="LogDate" />
                <telerik:GridBoundColumn HeaderText="ساعت" DataField="LogTime" />
                <telerik:GridBoundColumn HeaderText="توضیحات" DataField="Description" />
            </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    </div>
    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
