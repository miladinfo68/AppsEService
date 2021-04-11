<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationAvoidTime.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationAvoidTime" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
        <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>

    </title>
    <link href="../Style/js-persian-cal.css" rel="stylesheet" />
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
        /*-----------------------------*/
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

        .btn {
            padding: 6px 0 !important;
        }

        .btnInfo {
            background-color: #647ed1 !important;
            border-color: #293875;
            border-radius: 10px;
        }

        .modal {
            opacity: 1
        }

        .drp {
            border-radius: 10px;
            border: 1px solid #106062;
        }
    </style>
    <script>
        $(document).ready(function () {
            $(".btnChange").click(function () {
             
                var id = $(this).attr("a_id");
                var startTime = $(this).attr("a_startTime");
                var endTime = $(this).attr("a_endTime");
                var forEmployees = $(this).attr("a_forEmployees");
                var forStudents = $(this).attr("a_forStudents");
                var Dsc = $(this).attr("a_dsc");
                $("input[id*= txtVirDate1]").val(startTime);
                $("input[id*= txtVirDate2]").val(endTime);
                $("input[id*= LabelIdVir]").val(id);
                if (forEmployees == "True") {
                    $("input[id*= chkVirForEmployee]").attr('checked', true);
                }
                if (forStudents == "True") {
                    $("input[id*= chkVirForStudent]").attr('checked', true);
                }
                $("input[id*= txtVirDsc]").val(Dsc);
                $('#modalChange').modal("show");

            });
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtStartDate',
                { extraInputID: 'ContentPlaceHolder1_txtStartDate', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtEndDate',
                { extraInputID: 'ContentPlaceHolder1_txtEndDate', extraInputFormat: 'yyyy/mm/dd' });


        });
        function onLoadRadTimePicker1(sender, args) {
            txtTime = sender;
        }
    </script>
    <style>
        a.pcalBtn {
            position: relative;
            margin-right: -22px;
            vertical-align: middle;
        }

        .RadPicker .rcSelect {
            left: 1px;
        }

        .rcTimePopup {
            border-right: 1px solid #cdcdcd;
            border-left: 1px solid #cdcdcd;
        }

        .RadInput .RadInput_Default .RadInputRTL .RadInputRTL_Default {
            border-right: 1px solid #cdcdcd;
        }

        .RadPicker .RadInput > input {
            float: right !important;
        }

        .RadPicker .riTextBox {
            padding-left: 4.286em !important;
            text-align: center !important;
        }

        img {
            margin-left: 2px;
        }

        .buttonmargin {
            margin-bottom: initial !important;
        }

        .textBoxSearch {
            border-radius: 5px !important;
        }

        .textAlignCenter {
            text-align: center;
            color: black;
        }

        .marginBtn {
            margin-bottom: initial !important;
        }

        .tableBorder {
            border: 2px solid #73879c !important;
            background-color: #1a82c3 !important;
            color: aliceblue !important;
        }

            .tableBorder th {
                border: 1px solid #73879c;
            }

        .backColortable {
            background-color: #1A82C3;
        }

        .table-hover {
            background-color: #c5dbf3 !important;
        }

            .table-hover > tbody > tr:not(.tableBorder):hover {
                background-color: #038677 !important;
                color: whitesmoke;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
        <h3>
        <asp:Literal ID="pt" runat="server"></asp:Literal>
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <uc1:AccessControl runat="server" ID="AccessControl" />
    <telerik:RadWindowManager ID="RadWindowManager2" runat="server">
        <AlertTemplate>
            <div dir="rtl" style="padding: 5px" class="rwDialog rwAlertDialog">
                <div class="rwDialogContent" style="text-align: center">
                    <div style="color: black; font-size: 13px;" class="rwDialogMessage">
                        {1}
                    </div>
                </div>
                <br />
                <div class="rwDialogButtons text-center">
                    <input type="button" value="تایید" class="rwOkBtn btn btn-danger" onclick="$find('{0}').close(true); return false;" />
                </div>
            </div>
        </AlertTemplate>
    </telerik:RadWindowManager>
    <div class="container">

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <AlertTemplate>
                <div dir="rtl" style="padding: 5px" class="rwDialog rwAlertDialog">
                    <div class="rwDialogContent" style="text-align: center">
                        <div style="color: black; font-size: 13px;" class="rwDialogMessage">
                            {1}
                        </div>
                    </div>
                    <br />
                    <div class="rwDialogButtons text-center">
                        <input type="button" value="تایید" class="rwOkBtn btn btn-danger" onclick="$find('{0}').close(true); return false;" />
                    </div>
                </div>
            </AlertTemplate>
        </telerik:RadWindowManager>
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="bg-purple">
                    <img src="../fonts/stopwatch.png" style="width: 32px; float: right" alt="" />
                        <h5 class="header-inline-display" style="font-family: 'B Titr'"> بستن ساعت برگزاری دفاع</h5>
                    </div>
                    <div class="list-group-item" style="background: #a4935312">

                        <div style="border: 1px solid green; padding: 10px">
                            <div class="row">

                                <div class="col-md-3 ">
                                    <img src="../fonts/calendar.png" style="width: 32px" alt="" />
                                    <span style="font-size: 14px">تاریخ شروع
                                    </span>

                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="pdate " MaxLength="9" ToolTip=" تاریخ شروع" />
                                    <asp:RequiredFieldValidator ID="RFVtxtDate1" ValidationGroup="register" runat="server" CssClass="alert" ControlToValidate="txtStartDate" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 ">
                                    <img src="../fonts/calendar.png" style="width: 32px" alt="" />
                                    <span style="font-size: 14px">تاریخ پایان
                                    </span>

                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="pdate " MaxLength="9" ToolTip=" تاریخ پایان" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="register" runat="server" CssClass="alert" ControlToValidate="txtEndDate" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <span>
                                    <img src="../fonts/stopwatch.png" style="width: 32px; float: right" alt="" />
                                    </span>
                                    <h5 class="header-inline-display" style="margin-left: 5%; font-size: 14px;float:right">ساعت:</h5>
                                    
                                    <span style="float:right">
                                    <telerik:RadTimePicker ID="txtTime" EnableTyping="false" RenderMode="Lightweight" runat="server" Culture="fa-IR" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" CssClass="pdate">
                                              <TimeView ID="TimeView4" Interval="02:00:00" runat="server" TimeFormat="HH:mm" Columns="2" StartTime="09:00" EndTime="19:00">
                                                    </TimeView>
                                        <DateInput ID="DateInput1" runat="server">
                                            <ClientEvents OnLoad="onLoadRadTimePicker1"></ClientEvents>
                                        </DateInput>
                                    </telerik:RadTimePicker>
                                    <asp:RequiredFieldValidator ID="RFVtxtTime" runat="server" ValidationGroup="register" CssClass="alert" ControlToValidate="txtTime" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا زمان مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                        </span>

                                </div>
                                <div class="col-md-3">
                                         <span style="margin-left: 14px;font-size:14px">فقط برای آنلاین ‌‌ها
                                    </span>
                                    <asp:CheckBox ID="chkJustForOnline" runat="server" />
                               

                                </div>


                                <br />
                                <div class="row">
                                    <asp:Button ID="btnSave" runat="server" Text="ثبت" ValidationGroup="register" Width="100px" OnClick="btnSave_Click" class="btn btn-success" Style="float: left; margin-top: 10px; margin-left: 20px" />
                                </div>
                                <br />
                                <br />
                            </div>
                        </div>
                        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%; margin-top: 10px">


                            <telerik:RadGrid ID="grdAvoidTime" runat="server" AutoGenerateColumns="False"
                                BackColor="#ffcccc"
                                PageSize="10"
                                CssClass="table table-responsive table-stripted backColortable"
                                OnNeedDataSource="grdAvoidTime_NeedDataSource"
                                EnableEmbeddedSkins="false"
                                EmptyDataText="رکوردی برای این جدول وجود ندارد"
                                AllowPaging="True"
                              
                                AllowFilteringByColumn="True" Skin="MyCustomSkin">

                                <MasterTableView NoMasterRecordsText="رکوردی برای این جدول وجود ندارد" Dir="RTL"
                                    Font-Size="Large">
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 1%; margin-right: 1%;">
                                            <h5>هیچ رکوردی وجود ندارد</h5>
                                        </div>
                                    </NoRecordsTemplate>
                                    <Columns>


                                        <telerik:GridTemplateColumn HeaderText="تاریخ شروع  " HeaderStyle-VerticalAlign="Middle"  AllowFiltering="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblStartDate" runat="server"
                                                        Text='<%#Eval("StartDate")%>' CssClass="text-center"
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText=" تاریخ پایان" HeaderStyle-VerticalAlign="Middle"
                                            AllowFiltering="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblEndDate" runat="server"
                                                        Text='<%#Eval("EndDate")%>' CssClass="text-center"
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="ساعت بستن  " HeaderStyle-VerticalAlign="Middle" 
                                            AllowFiltering="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblTime" runat="server"
                                                        Text='<%#Eval("Time")%>' CssClass="text-center"
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="برای فقط آنلاین " HeaderStyle-VerticalAlign="Middle" 
                                            AllowFiltering="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblJustForOnline"  runat="server"
                                                         Text='<%#((bool)Eval("JustForOnline")==true)?"بله":"خیر"%>' CssClass="text-center"
                                                        
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>



                                        <telerik:GridTemplateColumn HeaderText="اعمال" AllowFiltering="false" ItemStyle-Width="10%">
                                            <ItemTemplate>


                                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text=" حذف  " OnClick="btnDelete_Click" Width="120px"
                                                    OnClientClick="confirmAspButton1(this); return false;" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                    </Columns>

                                </MasterTableView>

                            </telerik:RadGrid>

                        </div>



                    </div>

                </div>
            </div>
        </div>
    </div>

    <script>          
        function confirmAspButton1(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا با حذف ساعت موافقید؟", aspButtonCallbackFn, 550, 100, null, "Confirm");
        }
    </script>
    <script src="../Scripts/js-persian-cal.min.js"></script>
</asp:Content>

