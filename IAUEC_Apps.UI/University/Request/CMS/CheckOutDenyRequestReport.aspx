<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="CheckOutDenyRequestReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutDenyRequestReport" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <script src="../Content/js-persian-cal.min.js"></script>
    <link href="../Content/js-persian-cal.css" rel="stylesheet" />
    <style>
        .AlignGrd {
            text-align: center;
        }
    </style>
    <style>
        .grid td, .grid th{text-align:center;}
        .spacing { margin-right:20px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl">
        <div class="container">
            <div class="row" style="padding-right: 10px;">
                <div class="col-md-3">
                    <asp:RequiredFieldValidator ID="rfvSDate" runat="server" ForeColor="Red" Text="*" ValidationGroup="submit" ControlToValidate="txtSdate" ></asp:RequiredFieldValidator>
                    از تاریخ:<asp:TextBox ID="txtSdate" runat="server" MaxLength="9"></asp:TextBox>
                    <script>
                        var objCal1 = new AMIB.persianCalendar('<%=txtSdate.ClientID%>',
                            { extraInputID: '<%=txtSdate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                    </script>
                </div>
                <div class="col-md-3">
                    <asp:RequiredFieldValidator ID="rfvEDate" runat="server" ForeColor="Red" Text="*" ValidationGroup="submit" ControlToValidate="txtEdate" ></asp:RequiredFieldValidator>
                    تا تاریخ:<asp:TextBox ID="txtEdate" runat="server"></asp:TextBox>
                    <script>
                        var objCal1 = new AMIB.persianCalendar('<%=txtEdate.ClientID%>',
                            { extraInputID: '<%=txtEdate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                    </script>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSearch" runat="server" Text="جستجو" ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-3">
                    
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                </div>
                <div class="col-md-10">
                    <asp:GridView ID="grdResult" CssClass="grid"  Width="800px" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" EmptyDataText="رکوردی جهت نمایش وجود ندارد" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="ردیف">
                    <ItemTemplate>
                        <asp:Label ID="Label1" Text="<%# Container.DataItemIndex + 1 %>" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                            <asp:BoundField HeaderText="شماره درخواست"  DataField="StudentRequestID" />
                            <asp:BoundField HeaderText="تاریخ درخواست" DataField="CreateDate" />
                            <asp:BoundField HeaderText="شماره دانشجویی" DataField="StCode" />
                            <asp:BoundField HeaderText="نام دانشجو" DataField="name" />
                            <asp:BoundField HeaderText="رشته"  DataField="nameresh" />
                            <asp:BoundField HeaderText="دانشکده" DataField="namedanesh" />
                            <asp:TemplateField HeaderText="تاریخچه">
                        <ItemTemplate>
                            <asp:Button ID="btnHistory" Text="تاریخچه" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-sm btn-primary" />
                        </ItemTemplate>
                    </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
                <div class="col-md-1">
                </div>
            </div>
        </div>
    </div>

    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
