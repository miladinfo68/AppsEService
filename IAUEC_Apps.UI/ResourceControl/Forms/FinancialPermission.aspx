<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="FinancialPermission.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.FinancialPermission" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>

    </title>
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }
    </style>
    <link href="../../University/Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
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

        .searchBox {
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 10px 15px;
            margin: 0 5px 15px;
        }

        .form-control {
            padding: 4px 12px;
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
    </style>
    <script type="text/javascript">



        function confirmAspButton(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 250, 100, null, "Confirm");
        }

        function confirmAspButton1(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 250, 100, null, "Confirm");
        }

        function refresgGrid() {
            document.getElementById("<%=btnRefreshGrid.ClientID %>").click();
        }
        function closeRadWindow3() {

            var window = $find('<%=RadWindow2.ClientID %>');
            window.close();
            refresgGrid();
        }
        function closeRadWindow2() {

            var window = $find('<%=RadWindow2.ClientID %>');
            window.close();
            refresgGrid();
        }

        function getControl() {

            var masterTable = $find("<%=grdDefenceList.ClientID%>").get_masterTableView();
            var btnApprove = masterTable.get_dataItems()[0].findControl('btnApprove');// Here 0 represents the index of crow in editmode
            return btnApprove;
        }
        $("body").on("click", getControl(), function () {

            var IsValid = true;
            // Do client side button click stuff here.
            $('#<%=grdDefenceList.ClientID%> tr').each(function () {
                // action to perform.  Use $(this) for changing each cell
                var tr = $(this)
                if (tr.hasClass("bg-danger")) {
                    IsValid = false;
                    return false;
                }

                tr.find('td').each(function () {

                    if ($(this).find('option:selected').val() == 0) {
                        args.IsValid = false;
                        return;
                    }
                });
            });

            if (IsValid) {

                var data = HTMLtbl.getData($('#<%=grdDefenceList.ClientID%>'));  // passing that table's ID //
                var parameters = {};
                parameters.array = data;

                $.ajax({
                    type: "POST",
                    url: "TechnicalStudentReview.aspx/btnSubmitFinalClick",
                    data: JSON.stringify(parameters),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        radalert("کلاس با موفقیت تایید شد.", 300, 100, "پیام", closeRadWindow, "");
                    },
                    error: function (msg) {
                        alert("خطا!!!");
                    }
                });
            }

        });

        function closeRadWindow() {
            var window = $find('<%=grdDefenceList.ClientID %>');
            window.close();
            refresgGrid();
        }

    </script>
    <script type="text/javascript">
        function openModal() {
            $('#historyModal').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">


    <h3>
        <asp:Literal ID="pt" runat="server"></asp:Literal>
    </h3>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" CssClass="radWindow">
    </telerik:RadWindowManager>
    <telerik:RadWindow ID="RadWindow2" AutoSize="true" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_OnClick" CssClass="hidden" Text="refreshGrid" runat="server" />
                    <asp:HiddenField ID="hdnfDenyReqId" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

    <telerik:RadWindow ID="RadWindow4" AutoSize="true" AutoSizeBehaviors="HeightProportional" runat="server">
    </telerik:RadWindow>

    <div class="row" dir="rtl" style="margin-top: 2%">
        <div class="col-md-3" style="margin-right: 2%;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                        <ProgressTemplate>
                            <div style="position: fixed; text-align: center; margin: auto; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #0000007d; opacity: 0.7;">
                                <div style="position: fixed; top: 40%; left: 30%; margin: auto;">
                                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../../University/Theme/images/loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; margin: auto; width: 25%; height: 25%" />
                                    <br />
                                    <span style="color: white">در حال بارگذاری...</span>

                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:DropDownList ID="drpRequestTypeList" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="drpRequestTypeList_OnSelectedIndexChanged">
                        <asp:ListItem Text="لیست دانشجویان منتظر تایید" Value="1" />
                        <asp:ListItem Text="لیست دانشجویان تایید شده" Value="2" />
                        <asp:ListItem Text="لیست دانشجویان رد شده" Value="3" />
                        <asp:ListItem Text="لیست کلیه دانشجویان" Value="4" />
                    </asp:DropDownList>
                </ContentTemplate>

            </asp:UpdatePanel>
        </div>
        <div class="col-md-3 col-md-pull-6">
<%--            <div style="float: left; margin-left: 45px; margin-top: -10px;">
                <asp:ImageButton ID="bt1ExportExcle" runat="server" ToolTip="خروجی اکسل" Width="50" ImageUrl="../Images/microsoft excel.png" OnClick="bt1ExportExcle_OnClick" />
            </div>--%>
        </div>
    </div>
    <div class="row" dir="rtl" style="margin-left: 0.01% !important; margin-right: 0.01% !important; margin-bottom: 5% !important; margin-top: 2% !important;">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:UpdateProgress ID="updateProgress" runat="server">
                        <ProgressTemplate>
                            <div style="position: fixed; text-align: center; margin: auto; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #0000007d; opacity: 0.7;">
                                <div style="position: fixed; top: 40%; left: 30%; margin: auto;">
                                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../../University/Theme/images/loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; margin: auto; width: 25%; height: 25%" />
                                    <br />
                                    <span style="color: white">در حال بارگذاری...</span>

                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <telerik:RadGrid
                        HorizontalAlign="Center"
                        ID="grdDefenceList"
                        runat="server"
                        AllowPaging="True"
                        AutoGenerateColumns="False"
                        PageSize="20"
                        AllowSorting="True"
                        OnItemDataBound="grdDefenceList_OnItemDataBound"
                        OnItemCommand="grdDefenceList_OnItemCommand"
                        AllowFilteringByColumn="True" Skin="MyCustomSkin"
                        EnableEmbeddedSkins="false"
                        OnNeedDataSource="grdDefenceList_OnNeedDataSource">
                        <MasterTableView DataKeyNames="StudentCode">

                            <NoRecordsTemplate>
                                <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 1%; margin-right: 1%;">
                                    <h5>هیچ درخواستی وجود ندارد</h5>
                                </div>
                            </NoRecordsTemplate>
                            <ItemStyle />
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                    <ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                    </ItemTemplate>

                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="NationalCode" HeaderText="کدملی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentName" HeaderText="نام و نام خانوادگی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentCode" HeaderText="شماره دانشجویی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="PortalPermissionDate" HeaderText="تاریخ تایید پرتال">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="UnitSectionDate" HeaderText="تاریخ انتخاب واحد">
                                </telerik:GridBoundColumn>


                                <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="عملیات" UniqueName="operator">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-md-6" runat="server" id="divApprove">
                                                <asp:Button ID="btnApprove" Width="65" runat="server" OnClick="btnApprove_OnClick" ToolTip="تایید" Text="تایید" CssClass="btn btn-primary" OnClientClick="confirmAspButton(this); return false;" />
                                            </div>
                                            <div class="col-md-6" runat="server" id="divAvoid">
                                                <asp:Button ID="btnAvoid" Width="65" runat="server" OnClick="btnDenyRequest_OnClick" ToolTip="لغو" Text="لغو" CssClass="btn btn-danger" OnClientClick="confirmAspButton1(this); return false;" />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
<%--                                <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="تاریخچه">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:ImageButton ID="btnHistory" AlternateText="تاریخچه" Visible="true" runat="server" OnClick="btnHistory_OnClick" ImageUrl="~/University/Theme/images/appHistory.png" Width="40" Height="40" CssClass="center-margin" />
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
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

                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <%--                                                    <div class="col-md-4">
                                                        <strong>توضیحات: </strong>
                                                        <asp:Label ID="lblTozieh" CssClass="text-primary" runat="server" />
                                                    </div>--%>
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


                                        </tr>
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



</asp:Content>
