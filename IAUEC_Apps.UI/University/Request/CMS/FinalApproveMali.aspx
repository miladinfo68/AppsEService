﻿<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="FinalApproveMali.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.FinalApproveMali" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <link href="../../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        input[type="checkbox"] + label {
            display: none;
        }

        .RadGrid .rgFilterRow input {
            height: 25px;
        }

        .RadGrid .rgFilterRow > td, .RadGrid_MyCustomSkin .rgAltRow td {
            border: solid #00C851;
            border-width: 0 0 1px 1px;
        }

        .RadGridRTL_MyCustomSkin .rgFilterRow > td:first-child {
            border-width: 0px 1px 1px;
        }

        .RadGridRTL_MyCustomSkin .rgRow > td:first-child, .RadGridRTL_MyCustomSkin .rgAltRow > td:first-child {
            border-right-width: 1px;
        }


        .textBoxInput {
            border: 1px solid;
            border-radius: 5px;
            text-align: center;
            min-width: 200px;
            min-height: 27px !important;
        }

        input.textBoxInput :focus {
            border-color: #008080;
        }

        .btnWith {
            width: 97px;
        }

        a.pcalBtn {
            position: relative;
            margin-right: -22px;
            vertical-align: middle;
        }

        .RadWindow_Default, .RadWindow table.rwTable {
            max-height: 100%;
        }

        .RadGrid_MyCustomSkin th.rgSorted {
            background-color: #10396c;
        }

        .RadGrid_MyCustomSkin .rgHeader a {
            color: white;
        }

        .RadGrid .rgRow > td, .RadGrid .rgAltRow > td, .RadGrid .rgEditRow > td, .RadGrid .rgFooter > td, .RadGrid .rgFilterRow > td, .RadGrid .rgHeader, .RadGrid .rgResizeCol, .RadGrid .rgGroupHeader td {
            padding-left: 20px !important;
        }

        .btnPadding {
            padding: 6px 5px !important;
        }

        .litDec {
            color: #ffff00 !important;
            /*text-decoration: underline;*/
            /*font-size: 20px;*/
        }

        .hrWithoutMargin {
            margin: initial !important;
        }

        .alertBox {
            width: 96%;
            margin: 0 auto;
            display: inline-block;
        }

        alertWithoutPadding {
            padding: 0px 15px !important;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#historyModal').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>گزارش پرداخت ها بر اساس تاریخ صدور مدارک
    </h3>
    <asp:Literal runat="server" ID="pt" Visible="False"></asp:Literal>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container">
        <div class="row" dir="rtl">
            <div class="panel panel-primary" style="margin-right: 1%; margin-left: 1%">
                <div class="panel-heading">
                    <h3 class="panel-title">جستجو براساس تاریخ ورود گواهی موقت و واحد صادر کننده مدرک </h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-3">
                            <label style="width: 68px">تاریخ شروع:</label>
                            <asp:TextBox ID="StartDateTxt" CssClass="textBoxInput pdate disabled" runat="server">

                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label style="width: 68px">تاریخ پایان:</label>
                            <asp:TextBox ID="EndDateTxt" CssClass="textBoxInput pdate disabled" runat="server">

                            </asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label style="width: 68px">واحد:</label>

                            <asp:DropDownList ID="drpUniversity" CssClass="textBoxInput" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label style="width: 68px">نوع گزارش:</label>

                            <asp:DropDownList ID="drpReportType" CssClass="textBoxInput" runat="server">

                                <asp:ListItem Value="1">موارد پرداخت شده</asp:ListItem>
                                <asp:ListItem Value="2">مشاهده تمام موارد فارغ التحصیل</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <div class="row" dir="ltr">
                        <div class="col-md-7"></div>
                        <div class="col-md-5">
                            <asp:Button ID="ClearBtn" runat="server" Text="پاک کردن" CssClass="btn btn-danger btnWith" OnClick="ClearBtn_Click" />
                            <asp:Button ID="ExcleExportBtn" runat="server" Text="خروجی اکسل" CssClass="btn btn-warning btnWith" OnClick="ExcleExportBtn_Click" />
                            <asp:Button ID="SearchBtn" runat="server" Text="جستجو" CssClass="btn btn-success btnWith" OnClick="SearchBtn_Click"></asp:Button>

                        </div>

                    </div>
                </div>
            </div>
        </div>


     



        <div class="row" dir="rtl" style="margin-left: 0.01% !important; margin-right: 0.01% !important; margin-bottom: 5% !important;">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanelResualt" runat="server">
                    <ContentTemplate>
                      

                        <telerik:RadGrid HorizontalAlign="Center" Visible="False" ID="grdResualtList" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="100" AllowSorting="True" AllowFilteringByColumn="True"  Skin="MyCustomSkin" EnableEmbeddedSkins="false"
                            OnNeedDataSource="grdResualtList_NeedDataSource">
                            <MasterTableView DataKeyNames="StCode">

                                <NoRecordsTemplate>
                                    <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 30%; margin-right: 30%;">
                                        <h5>هیچ درخواستی وجود ندارد</h5>
                                    </div>
                                </NoRecordsTemplate>
                                <ItemStyle />
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                  
                                        <ItemTemplate>
                                           <%# Container.DataSetIndex + 1 %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn AllowFiltering="True"  DataField="StudentRequestID" HeaderText="شماره درخواست"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="True" DataField="StCode" HeaderText="شماره دانشجویی">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="True" DataField="name" HeaderText="نام و نام خانوادگی" HeaderStyle-Width="150px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="True" DataField="TypeName" HeaderText="نوع مدرک">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="True" DataField="DateVoroodGovahi" HeaderText="تاریخ ورود مدرک">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="True" DataField="DateSodoorGovahi" HeaderText="تاریخ صدور مدرک">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AllowSorting="True" DataField="vahed" HeaderText="محل صدور مدرک">
                                    </telerik:GridBoundColumn>
                                     <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="شهریه">
                                        <ItemTemplate>
                                            <asp:label runat="server" CssClass="h4"  Text='<%#Convert.ToInt64(Eval("shahriye")).ToString("N0", new NumberFormatInfo()
                                            {
                                                NumberGroupSizes = new[] { 3 },
                                                NumberGroupSeparator = ","
                                            }) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                      <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="تخفیف">
                                        <ItemTemplate>
                                            <asp:label runat="server" CssClass="h4" Text='<%#Convert.ToInt64(Eval("takhfif")).ToString("N0", new NumberFormatInfo()
                                            {
                                                NumberGroupSizes = new[] { 3 },
                                                NumberGroupSeparator = ","
                                            })

 %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                      <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="انتقال بستانکار">
                                        <ItemTemplate>
                                            <asp:label runat="server" CssClass="h4" Text='<%#Convert.ToInt64(Eval("enteghalBestankar")).ToString("N0", new NumberFormatInfo()
                                            {
                                                NumberGroupSizes = new[] { 3 },
                                                NumberGroupSeparator = ","
                                            })

 %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                      <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="قابل پرداخت">
                                        <ItemTemplate>
                                            <asp:label runat="server" CssClass="h4" Text='<%#(Convert.ToInt64(Eval("shahriye"))-Convert.ToInt64(Eval("takhfif"))-Convert.ToInt64(Eval("enteghalBestankar"))).ToString("N0", new NumberFormatInfo()
                                            {
                                                NumberGroupSizes = new[] { 3 },
                                                NumberGroupSeparator = ","
                                            }) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                
                                </Columns>


                            </MasterTableView>

                        </telerik:RadGrid>

                        
                    </ContentTemplate>

                </asp:UpdatePanel>

            </div>
        </div>


    </div>

    <script>
        var cal1 = new AMIB.persianCalendar('<%=StartDateTxt.ClientID%>',
            { extraInputID: '<%=StartDateTxt.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
        var cal2 = new AMIB.persianCalendar('<%=EndDateTxt.ClientID%>',
            { extraInputID: '<%=EndDateTxt.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
    </script>
</asp:Content>
