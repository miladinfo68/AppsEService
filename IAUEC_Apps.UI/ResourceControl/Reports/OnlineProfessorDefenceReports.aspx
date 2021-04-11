<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="OnlineProfessorDefenceReports.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Reports.OnlineProfessorDefenceReports" %>

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
        #ContentPlaceHolder1_ButtonClick {
            display: inline-block !important;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#historyModal').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>گزارش جلسات دفاع های شرکت استاد به صورت آنلاین در بازه زمانی مشخص به تفکیک دانشکده
    </h3>
    <asp:Literal runat="server" ID="pt" Visible="False"></asp:Literal>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container">
        <div class="row" dir="rtl">
            <div class="panel panel-primary" style="margin-right: 1%; margin-left: 1%">
                <div class="panel-heading">
                    <h3 class="panel-title">جستجو تعداد جلسات اساتید به صورت آنلاین</h3>
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


        <div dir="rtl" class="row" style="margin-left: 0.01% !important; margin-right: 0.01% !important; margin-bottom: 1.5% !important; text-align: center">
            <div class="col-md-3" runat="server" id="divHumanities" visible="False">
                <div class="alert alert-info alertBox">
                    <h3 class="alert alert-danger">
                        <span>علـوم انـسانی</span>
                    </h3>
                    <div class="row">
                        <div class="col-md-4">
                            <h4 class="litDec">
                                <span>
                                    <asp:Literal ID="litHumanitiesWait" runat="server">
                                    </asp:Literal>
                                </span>
                            </h4>
                            <hr class="hrWithoutMargin" />

                            <h5><span>منتظر پخش</span></h5>
                        </div>
                        <div class="col-md-4">
                            <h4 class="litDec">
                                <span>
                                    <asp:Literal ID="litHumanitiesHeld" runat="server">
                                    </asp:Literal>
                                </span>
                            </h4>
                            <hr class="hrWithoutMargin" />

                            <h5><span>پخش شده</span></h5>
                        </div>
                        <div class="col-md-4">
                            <h4 class="litDec">
                                <span>
                                    <asp:Literal ID="litHumanitiesLosed" runat="server">
                                    </asp:Literal>
                                </span>
                            </h4>
                            <hr class="hrWithoutMargin" />

                            <h5><span>از دست رفته</span></h5>
                        </div>

                    </div>

                </div>
            </div>
            <div class="col-md-3" runat="server" id="divTechnicalEngineering" visible="False">

                <div class="alert alert-info alertBox">
                    <h3 class="alert alert-danger">
                        <span>فني و مهندسی</span>
                    </h3>
                    <div class="col-md-4">
                        <h4 class="litDec">
                            <span>
                                <asp:Literal ID="litTechnicalEngineeringWait" runat="server">
                                </asp:Literal>
                            </span>
                        </h4>
                        <hr class="hrWithoutMargin" />

                        <h5><span>منتظر پخش</span></h5>
                    </div>
                    <div class="col-md-4">
                        <h4 class="litDec">
                            <span>
                                <asp:Literal ID="litTechnicalEngineeringHeld" runat="server">
                                </asp:Literal>
                            </span>
                        </h4>
                        <hr class="hrWithoutMargin" />

                        <h5><span>پخش شده</span></h5>
                    </div>
                    <div class="col-md-4">
                        <h4 class="litDec">
                            <span>
                                <asp:Literal ID="litTechnicalEngineeringLosed" runat="server">
                                </asp:Literal>
                            </span>
                        </h4>
                        <hr class="hrWithoutMargin" />

                        <h5><span>از دست رفته</span></h5>
                    </div>

                </div>
            </div>
            <div class="col-md-3" runat="server" id="divManagement" visible="False">
                <div class="alert alert-info alertBox">
                    <h3 class="alert alert-danger">
                        <span>مديريت</span>
                    </h3>
                    <div class="col-md-4">
                        <h4 class="litDec">
                            <span>
                                <asp:Literal ID="litManagementWait" runat="server">
                                </asp:Literal>
                            </span>
                        </h4>
                        <hr class="hrWithoutMargin" />

                        <h5><span>منتظر پخش</span></h5>
                    </div>
                    <div class="col-md-4">
                        <h4 class="litDec">
                            <span>
                                <asp:Literal ID="litManagementHeld" runat="server">
                                </asp:Literal>
                            </span>
                        </h4>
                        <hr class="hrWithoutMargin" />

                        <h5><span>پخش شده</span></h5>
                    </div>
                    <div class="col-md-4">
                        <h4 class="litDec">
                            <span>
                                <asp:Literal ID="litManagementLosed" runat="server">
                                </asp:Literal>
                            </span>
                        </h4>
                        <hr class="hrWithoutMargin" />

                        <h5><span>از دست رفته</span></h5>
                    </div>

                </div>

            </div>
            <div class="col-md-3" runat="server" id="divScience" visible="False">
                <div class="alert alert-info alertBox">
                    <h3 class="alert alert-danger">
                        <span>علوم پايه</span>
                    </h3>
                    <div class="col-md-4">
                        <h4 class="litDec">
                            <span>
                                <asp:Literal ID="litScienceWait" runat="server">
                                </asp:Literal>
                            </span>
                        </h4>
                        <hr class="hrWithoutMargin" />

                        <h5><span>منتظر پخش</span></h5>
                    </div>
                    <div class="col-md-4">
                        <h4 class="litDec">
                            <span>
                                <asp:Literal ID="litScienceHeld" runat="server">
                                </asp:Literal>
                            </span>
                        </h4>
                        <hr class="hrWithoutMargin" />

                        <h5><span>پخش شده</span></h5>
                    </div>
                    <div class="col-md-4">
                        <h4 class="litDec">
                            <span>
                                <asp:Literal ID="litScienceLosed" runat="server">
                                </asp:Literal>
                            </span>
                        </h4>
                        <hr class="hrWithoutMargin" />

                        <h5><span>از دست رفته</span></h5>
                    </div>

                </div>
            </div>
        </div>



        <div class="row" dir="rtl" style="margin-left: 0.01% !important; margin-right: 0.01% !important; margin-bottom: 5% !important;">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanelResualt" runat="server">
                    <ContentTemplate>
                        <telerik:RadGrid HorizontalAlign="Center" Visible="False" ID="grdResualtList" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" AllowSorting="True" AllowFilteringByColumn="True" Skin="MyCustomSkin" EnableEmbeddedSkins="false"
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
                                    <telerik:GridBoundColumn AllowFiltering="True" AllowSorting="True" DataField="شماره درخواست" HeaderText="شماره درخواست"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="کد دانشجویی" HeaderText="کد دانشجویی">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="نام و نام خانوادگی" HeaderText="نام و نام خانوادگی" HeaderStyle-Width="130px">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="دانشکده" HeaderText="دانشکده">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="تاریخ برگزرای جلسه" HeaderText="تاریخ برگزرای">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="تاریخ ثبت درخواست" HeaderText="تاریخ ثبت">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="وضعیت" HeaderText="وضعیت">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="جزئیات" UniqueName="Details" HeaderStyle-Width="130px">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnShowDetail" runat="server" OnClick="btnShowDetail_OnClick" Text="نمایش" CssClass="btn btn-success" CommandArgument='<%#Eval("شماره درخواست")%>' />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnHistory" runat="server" OnClick="btnHistory_OnClick" Text="تاریخچه" CssClass="btn btn-primary btnPadding"  CommandArgument='<%#Eval("شماره درخواست")%>' />
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>


                            </MasterTableView>



                        </telerik:RadGrid>




                        <div class="modal fade" id="historyModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" style="background-color: aqua;">
                            <div class="modal-dialog" role="document" style="width: 70%">
                                <div class="modal-content bg-info border-dark">

                                    <div class="modal-header" dir="rtl">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="font-size: -webkit-xxx-large; float: left; float: left; margin-left: 1%;">
                                            <span aria-hidden="true" style="margin: auto; line-height: initial;">&times;
                                            </span>
                                        </button>
                                        <div class="modal-header bg-orange" dir="rtl">
                                            <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه درخواست</h4>
                                        </div>
                                    </div>
                                    <div class="modal-body">
                                        <table class="table table-responsive table-bordered table-head table-hover center-margin" dir="rtl" style="border-bottom-color: black">
                                            <tr class="bg-blue-sky">
                                                <th>نام کاربر</th>
                                                <th>تاریخ</th>
                                                <th>ساعت</th>
                                                <th>وضعیت</th>
                                                <th>توضیحات</th>
                                            </tr>

                                            <asp:ListView ID="lst_history" runat="server">
                                                <ItemTemplate>
                                                    <tr class="bg-blue" style="text-align: center;">
                                                        <td>
                                                            <%#Eval("Name") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("LogDate") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("LogTime") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("EventName") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("Description") %>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </table>

                                    </div>

                                </div>

                            </div>
                        </div>

                    </ContentTemplate>

                </asp:UpdatePanel>

            </div>
        </div>





    </div>
    <%--    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" CssClass="radWindow">
    </telerik:RadWindowManager>--%>
    <telerik:RadWindow ID="RadWindow3" runat="server" Width="1200px" Height="650px" Skin="Glow" Modal="True">
        <ContentTemplate>
            <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl">
                        <div class="heading2 bg-green" style="padding: 5px">
                            <h3>
                            جزئیات درخواست :
                        </div>
                        <br />
                        <div class="row center-margin">
                            <div style="border: 5px solid #A9A9A9; margin-left: 5%; margin-right: 5%;">
                                <div style="margin: 5%;">
                                    <table class="table table-responsive" style="font-weight: bolder;">
                                        <tr>
                                            <td>
                                                <strong>موضوع جلسه دفاع : </strong>
                                                <asp:Label ID="lblDarkhast" CssClass="text-primary" runat="server" />
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <strong>وضعیت : </strong>
                                                        <asp:Label ID="lblStatue" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-4">
                                                        <strong>شماره درخواست : </strong>
                                                        <asp:Label ID="lblRequestId" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>تاریخ ثبت درخواست : </strong>
                                                        <asp:Label ID="lbldateOfRequest" CssClass="text-primary" runat="server" />
                                                    </div>
                                                </div>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <strong>تاریخ درخواستی برگزاری جلسه:</strong>
                                                        <asp:Label ID="lblRequest" CssClass="text-primary" runat="server" />

                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>ساعت شروع: </strong>
                                                        <asp:Label ID="lblTime1" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>ساعت پایان: </strong>
                                                        <asp:Label ID="lblTime2" CssClass="text-primary" runat="server" />

                                                    </div>
                                                </div>
                                            </td>

                                        </tr>

                                        <tr runat="server" id="divFirstOnlineTeacher">
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <strong>نام اولین استاد آنلاین : </strong>
                                                        <asp:Label ID="lblfirstTeacherName" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>شماره موبایل:</strong>
                                                        <asp:Label ID="lblfirstTeacherMobile" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-4">
                                                        <strong>ایمیل:</strong>
                                                        <asp:Label ID="lblfirstTeacherEmail" CssClass="text-primary" runat="server" />
                                                    </div>

                                                </div>

                                            </td>
                                        </tr>
                                        <tr runat="server" id="divSecondOnlineTeacher">
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <strong>نام دومین استاد آنلاین : </strong>
                                                        <asp:Label ID="lblSecondTeacherName" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>شماره موبایل:</strong>
                                                        <asp:Label ID="lblSecondTeacherMobile" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-4">
                                                        <strong>ایمیل:</strong>
                                                        <asp:Label ID="lblSecondTeacherEmail" CssClass="text-primary" runat="server" />
                                                    </div>

                                                </div>

                                            </td>
                                        </tr>

                                        <tr class="hidden">
                                            <td>
<div class="row">
                                                    <div class="col-md-3">
                                                        <strong>استفاده از رایانه شخصی: </strong>
                                                        <asp:Label ID="lblOwnSysytem" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <strong>دفاع آنلاین:</strong>
                                                        <asp:Label ID="lblOnlineDef" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-3">
                                                        <strong>پخش آنلاین:</strong>
                                                        <asp:Label ID="lblOnlineShow" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    
                                                    <div class="col-md-3">
                                                        <strong>برگزاری جلسه دفاع به صورت آنلاین:</strong>
                                                        <asp:Label ID="lblOnlineDoingMeeting" CssClass="text-primary"                                       runat="server" />
                                                    </div>
                                                </div>

                                            </td>

                                        </tr>

                                        <%--                                        <tr>
                                            <td>
                                                <div class="row">
                                                                                                     <div class="col-md-4">
                                                        <strong>توضیحات: </strong>
                                                        <asp:Label ID="lblTozieh" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <strong>
                                                            <asp:Literal ID="litDenyNot" runat="server" Visible="False" Text=""></asp:Literal>

                                                        </strong>
                                                        <asp:Label ID="lblDenyNot" Visible="False" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-6">
                                                        <strong>
                                                            <asp:Literal ID="lblheader" runat="server" Visible="False" Text=""></asp:Literal></strong>
                                                        <asp:Label ID="lblDateOfResponse" Visible="False" CssClass="text-primary" runat="server" />
                                                    </div>

                                                </div>

                                            </td>


                                        </tr>--%>
                                        <tr>
                                            <td runat="server" id="tdGrdResult" visible="False" colspan="2">
                                                <div class="table-responsive text-center">
                                                    <asp:GridView ID="grdResult" runat="server" DataKeyNames="datetimeid" AutoGenerateColumns="false" CssClass="table table-responsive backGroundForGrdDate text-center">
                                                        <HeaderStyle CssClass="bg-blue-sky" />
                                                        <RowStyle ForeColor="#29343B"></RowStyle>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ردیف">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="تاریخ">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Eval("Date").ToString() %>' runat="server" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtDate" Text='<%# Eval("Date").ToString() %>' runat="server" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ساعت شروع">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("StartTime"))).ToString("hh\\:mm") %>' runat="server" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:RequiredFieldValidator ErrorMessage="لطفا زمان آغاز را مشخص کنید" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                                                    <telerik:RadTimePicker ID="RadTimePicker1" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                        <TimeView ID="TimeView4" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                                        </TimeView>
                                                                    </telerik:RadTimePicker>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ساعت پایان">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("EndTime"))).ToString("hh\\:mm") %>' runat="server" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:RequiredFieldValidator ErrorMessage="لطفا زمان پایان را مشخص کنید" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />
                                                                    <telerik:RadTimePicker ID="RadTimePicker2" runat="server" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                        <TimeView ID="TimeView4" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                                        </TimeView>
                                                                    </telerik:RadTimePicker>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="کلاس">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClassName" Text='<%# Eval("ClassName") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="dateTimeId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lblDateTimeId" Text='<%# Eval("DateTimeId") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="reqid" DataField="RequestId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                </div>
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

    <script>
        var cal1 = new AMIB.persianCalendar('<%=StartDateTxt.ClientID%>',
            { extraInputID: '<%=StartDateTxt.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
        var cal2 = new AMIB.persianCalendar('<%=EndDateTxt.ClientID%>',
            { extraInputID: '<%=EndDateTxt.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
    </script>
</asp:Content>
