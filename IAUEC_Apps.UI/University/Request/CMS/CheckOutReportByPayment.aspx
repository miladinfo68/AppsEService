<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master"
    AutoEventWireup="true" CodeBehind="CheckOutReportByPayment.aspx.cs"
    Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutReportByPayment" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../Theme/css/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <script src="../Content/js-persian-cal.min.js"></script>
    <link href="../Content/js-persian-cal.css" rel="stylesheet" />
    <style>
        .rcbItem {
            font-family: tahoma;
        }

        .rcbHovered {
            font-family: Tahoma;
            font-weight: bold;
        }

        .AlignGrd {
            text-align: center;
        }
    </style>
    <style>
        .grid td, .grid th {
            text-align: center;
        }

        .marginR {
            margin-right: 2px;
        }

        .spacing {
            margin-right: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl">
        <div class="container">
            <div class="row" style="margin: 10px;">
                <div class="col-md-3">
                    <asp:RequiredFieldValidator ID="rfvSDate" runat="server" ForeColor="Red" Text="*" ValidationGroup="submit" ErrorMessage="تاریخ شروع را انتخاب کنید" ControlToValidate="txtSdate"></asp:RequiredFieldValidator>
                    <asp:Label ID="label1" runat="server" Font-Names="b yekan">از تاریخ:</asp:Label><asp:TextBox ID="txtSdate" runat="server" CssClass="marginR" MaxLength="9"></asp:TextBox>
                    <script>
                        var objCal1 = new AMIB.persianCalendar('<%=txtSdate.ClientID%>',
                            { extraInputID: '<%=txtSdate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                    </script>
                </div>
                <div class="col-md-3">
                    <asp:RequiredFieldValidator ID="rfvEDate" runat="server" ForeColor="Red" Text="*" ErrorMessage="تاریخ پایان را انتخاب کنید" ValidationGroup="submit" ControlToValidate="txtEdate"></asp:RequiredFieldValidator>
                    <asp:Label ID="label2" runat="server" Font-Names="b yekan">تا تاریخ:</asp:Label><asp:TextBox ID="txtEdate" CssClass="marginR" runat="server"></asp:TextBox>
                    <script>
                        var objCal1 = new AMIB.persianCalendar('<%=txtEdate.ClientID%>',
                            { extraInputID: '<%=txtEdate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                    </script>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSearch" runat="server" Text="جستجو" ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnExcel" runat="server" Text="تبدیل به فایل Excel" CssClass="btn btn-success" Visible="false" OnClick="btnExcel_Click" />
                </div>
            </div>
            <div class="row" style="margin-top: 20px; margin-bottom: 20px">
                <%--  <div class="col-md-4">
                </div>--%>

                <div class="col-md-4">
                </div>
            </div>
            <div class="row bg-danger" style="margin-right: 10px">
                <asp:ValidationSummary ID="validSummary" runat="server" ForeColor="#d60000" ValidationGroup="submit" HeaderText="لطفا به موارد زیر دقت کنید" />
            </div>
            <div class="row">

                <div class="col-md-12">
                    <div class="table-responsive">
                        <telerik:RadGrid ID="grdResult" runat="server"   MasterTableView-ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" AllowPaging="true" PageSize="100" AllowFilteringByColumn="true" EnableEmbeddedSkins="false" Skin="MyCustomSkin" OnNeedDataSource="grdResult_NeedDataSource" OnExcelMLWorkBookCreated="grdResult_ExcelMLWorkBookCreated">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                                        <ItemTemplate>
                                            <%# Container.ItemIndex + 1 %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="OrderID"  HeaderText="شماره سفارش"  ></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RequestId" HeaderText="شماره درخواست" ></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AmountTrans"  HeaderText="مبلغ"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="StudentCode" HeaderText="شماره دانشجو" ></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fullname"  HeaderText="نام دانشجو"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AppStatus"  HeaderText="وضعیت"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>

            </div>
        </div>

    </div>

    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
