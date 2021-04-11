<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="AcceptedDefenseMeetingFromOfficeReports.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Reports.AcceptedDefenseMeetingFromOfficeReports" %>

<%@ Import Namespace="IAUEC_Apps.UI.ResourceControl.Forms" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>

    </title>

    <link href="../../University/Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
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


        .textBoxInput {
            border: 1px solid;
            border-radius: 5px;
            text-align: center;
            min-width: 200px;
            min-height: 27px !important;
        }

        input.textBoxInput :focus {
            border-color: #008080
        }

        .btnWith {
            width: 97px;
        }

        a.pcalBtn {
            position: relative;
            margin-right: -22px;
            margin-top: -1px;
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
        #ContentPlaceHolder1_ButtonClick {
            display: inline-block !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">

    <h3>گزارش جلسات دفاع های تایید شده اداری در بازه زمانی مشخص به تفکیک دانشکده
          <asp:Literal runat="server" ID="pt" Visible="False"></asp:Literal>
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />

    <div class="container">
        <div class="row" dir="rtl">
            <div class="panel panel-primary" style="margin-right: 1%; margin-left: 1%">
                <div class="panel-heading">
                    <h3 class="panel-title">جستجو جلسات دفاع تایید شده</h3>
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
                            <label style="width: 68px">دانشکده:</label>

                            <asp:DropDownList ID="CollegeIddrp" CssClass="textBoxInput" runat="server">
                                <asp:ListItem Selected="True" Value="0">کلیه دانشکده ها</asp:ListItem>
                                <asp:ListItem Value="1">علـوم انـساني</asp:ListItem>
                                <asp:ListItem Value="2">فني و مهندسي</asp:ListItem>
                                <asp:ListItem Value="3">مديريت</asp:ListItem>
                                <asp:ListItem Value="8">علوم پايه و فناوري هاي نوين</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-3">
                            <label style="width: 68px">نوع تاریخ:</label>

                            <asp:DropDownList ID="drpDateType" CssClass="textBoxInput" runat="server">
                                <asp:ListItem Value="1">هر دو</asp:ListItem>
                                <asp:ListItem Value="2">تاریخ ثبت درخواست</asp:ListItem>
                                <asp:ListItem Value="3">تاریخ دفاع</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>

                </div>
                <div class="panel-footer">
                    <div class="row" dir="ltr">
                        <div class="col-md-7"></div>
                        <div class="col-md-5">
                            <asp:Button ID="ExcleExportBtn" runat="server" Text="خروجی اکسل" CssClass="btn btn-warning btnWith" OnClick="ExcleExportBtn_OnClick" />

                            <asp:UpdatePanel ID="ButtonClick" runat="server">
                                <ContentTemplate>
                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                                <asp:Image ID="imgUpdateProgress123" runat="server" ImageUrl="../Images/loading-svg-spinning-4.gif" AlternateText="درحال بارگزاری ..." ToolTip="درحال بارگزاری ..." Style="padding: 10px; position: fixed; top: 28%; left: 38%;" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                    <asp:Button ID="SearchBtn" runat="server" Text="جستجو" CssClass="btn btn-success btnWith" OnClick="SearchBtn_OnClick"></asp:Button>
                                    <asp:Button ID="ClearBtn" runat="server" Text="پاک کردن" CssClass="btn btn-danger btnWith" OnClick="ClearBtn_OnClick" />
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>

                    </div>
                </div>
            </div>
        </div>





        <div class="row" dir="rtl" style="margin-left: 0.01% !important; margin-right: 0.01% !important; margin-bottom: 5% !important; margin-top: 2% !important;">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanelResualt" runat="server">
                    <ContentTemplate>
                        <telerik:RadGrid HorizontalAlign="Center" Visible="False" ID="grdResualtList" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="10" AllowSorting="True" AllowFilteringByColumn="True" Skin="MyCustomSkin" EnableEmbeddedSkins="false"
                            OnNeedDataSource="grdResualtList_OnNeedDataSource">
                            <MasterTableView DataKeyNames="کد دانشجویی">

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
                                            <%# (CurrentPage.CurrentPageNumberValue*CurrentPage.PageSizeValue)+(Container.ItemIndex + 1) %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="70px" DataField="کد دانشجویی" HeaderText="کد دانشجویی">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="نام و نام خانوادگی" HeaderText="نام و نام خانوادگی">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="دانشکده" HeaderText="دانشکده">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="تاریخ برگزرای جلسه" HeaderText="تاریخ برگزرای جلسه">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="دفاع آنلاین" HeaderText="دفاع آنلاین">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="پخش آنلاین" HeaderText="پخش آنلاین">
                                    </telerik:GridBoundColumn>
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
