<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSGovahiRequestMaster.Master" AutoEventWireup="true" CodeBehind="ConfirmRequestGovahiUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.ConfirmRequestGovahiUI" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">

    <title>
       <asp:Literal ID="t" runat="server"></asp:Literal>

    </title>
    <style>
        .rptstyle {
            font-family: 'B Nazanin';
        }
    </style>
    <link href="../../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
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


        .RadGrid_MyCustomSkin th.rgSorted {
            background-color: #3498DB;
        }

        .RadGrid_MyCustomSkin .rgHeader a {
            color: white;
        }

        .RadGrid .rgRow > td, .RadGrid .rgAltRow > td, .RadGrid .rgEditRow > td, .RadGrid .rgFooter > td, .RadGrid .rgFilterRow > td, .RadGrid .rgHeader, .RadGrid .rgResizeCol, .RadGrid .rgGroupHeader td {
            padding-left: 20px !important;
        }

        .RadGrid .rgFilterRow input {
            height: 25px;
        }

        .btn-width {
            width: 106px;
        }

        .payBtn {
            float: left;
            margin-left: 7%;
            width: 106px;
        }
        .header-inline-display {
            display: inline-block;
        }
        .inlineTextbox {
            border-radius: 5px;
            margin-bottom: 5px;
            margin-top: 10px;
            height: 40px;
        }
    </style>

    <link href="../../Theme/css/StyleSheetCalendar.css" type="text/css" rel="stylesheet" />

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
</asp:Content>

<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server">
   <h1 class="alert alert-dark"> <asp:Literal ID="pt" runat="server"></asp:Literal></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock ID="blk" runat="server">
        <script type="text/javascript">


            function refreshGrid(arg) {
                if (!arg) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                    $find("<%= grd_GovahiRequest.ClientID %>").get_masterTableView().rebind();
                }

            }


        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="rwmValidations" runat="server">
    </telerik:RadWindowManager>
    <div>
        <asp:Label ID="lbl_Nimsal" runat="server" Visible="false"></asp:Label>
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="LsitLoadingPanel"></telerik:RadAjaxLoadingPanel>
    <div style="width: 100%" dir="rtl">
        <div class="row">
            <telerik:RadGrid ID="grd_GovahiRequest" AllowFilteringByColumn="true" runat="server" OnItemDataBound="grd_GovahiRequest_ItemDataBound" AutoGenerateColumns="false" OnNeedDataSource="grd_GovahiRequest_NeedDataSource1" OnItemCommand="grd_GovahiRequest_ItemCommand"  EnableEmbeddedScripts="True" EnableEmbeddedSkins="False" Skin="MyCustomSkin">

                <MasterTableView HorizontalAlign="Center" Width="100%">
                    <NoRecordsTemplate>
                        <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 1%; margin-right: 1%;">
                            <h5>هیچ درخواستی وجود ندارد</h5>
                        </div>
                    </NoRecordsTemplate>
                    <ItemStyle />
                    <HeaderStyle Font-Names="b nazanin" HorizontalAlign="Center" />

                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" AllowFiltering="true" FilterControlWidth="70px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="true" FilterControlWidth="70px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" ItemStyle-HorizontalAlign="Center" AllowFiltering="true" FilterControlWidth="70px">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="salvorud" HeaderText="سال ورود" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" >
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="reshname" HeaderText="رشته" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ درخواست" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="mashmulNumber" HeaderText="شماره مشمولی" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="mashmulTarikh" HeaderText="تاریخ مشمولی" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>


                        <telerik:GridTemplateColumn AllowFiltering="false">
                            <HeaderTemplate>ارائه به</HeaderTemplate>
                            <ItemTemplate>
                                <div class="form-group">
                                    <asp:TextBox ID="txt_EraeBe" runat="server" CssClass="form-control inlineTextbox"></asp:TextBox>
                                    
                                    <asp:Button ID="btn_Edit" Text="ویرایش" CssClass="btn btn-warning btn-width" CommandName="Edit_EraeBe" runat="server" />
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="عملیات">
                            <ItemTemplate>
                                <asp:Button ID="btn_Govahi" Text="چاپ گواهی" CssClass="btn btn-success btn-width" CommandName="printgovahi" runat="server" />
                                <asp:Button ID="btn_Rad" Text="رد درخواست" CssClass="btn btn-danger btn-width" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>

            </telerik:RadGrid>
        </div>

        <br />
        <uc1:AccessControl ID="AccessControl1" runat="server" />
    </div>
    <div>
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />
    </div>
    
    <telerik:RadWindowManager ID="rwm_Validations" runat="server">
    </telerik:RadWindowManager>
</asp:Content>
