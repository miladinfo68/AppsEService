<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationSpecialDate.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationSpecialDate" %>
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
            var objCal1 = new AMIB.persianCalendar('PageTitle_txtVirDate1',
                { extraInputID: 'PageTitle_txtVirDate1', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('PageTitle_txtVirDate2',
                { extraInputID: 'PageTitle_txtVirDate2', extraInputFormat: 'yyyy/mm/dd' });


        });
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

    <div class="modal" tabindex="-1" role="dialog" id="modalChange">
        <div class="modal-dialog" role="document" style="width: 90%;">
            <div class="modal-content" style="width: 90%;">
                <div class="modal-header">
                    <h5 class="modal-title">ویرایش بستن تاریخ برگزاری دفاع</h5>


                </div>
                <div class="modal-body">

                    <div id="div_Main" style="padding-left: 15%; padding-right: 5%; margin-bottom: 0px" class="pnl_First">

                        <asp:Panel ID="pnl_First" runat="server">


                            <div class="row">
                                <div class="col-md-12 ">
                                    <div class="row">
                                        <div class="col-md-4 ">
                                            <asp:HiddenField ID="LabelIdVir" runat="server"></asp:HiddenField>


                                            <asp:TextBox ID="txtVirDate1" runat="server" CssClass="pdate " MaxLength="9" ToolTip="آغاز تاریخ ثبت" />
                                            <span style="margin-left: 40px">تاریخ آغاز</span>
                                          <span>  <img src="../fonts/calendar.png" style="width: 32px" alt="" /></span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Virregister" runat="server" CssClass="alert" ControlToValidate="txtVirDate1" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-4">


                                            <asp:TextBox ID="txtVirDate2" runat="server" CssClass="pdate " MaxLength="9" ToolTip="پایان تاریخ ثبت" />
                                            <span style="margin-left: 40px">تاریخ پایان</span>
                                           <span> <img src="../fonts/calendar.png" style="width: 32px" alt="" /></span>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Virregister" runat="server" CssClass="alert" ControlToValidate="txtVirDate2" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>


                                        </div>

                                        <div class="col-md-2">
                                            <asp:CheckBox ID="chkVirForEmployee" runat="server" />
                                            <span style="margin-left: 14px">بـرای کـارمـنـدان</span>



                                        </div>

                                        <div class="col-md-2">
                                            <asp:CheckBox ID="chkVirForStudent" runat="server" />
                                            <span style="margin-left: 14px">بـرای دانـشـجـویان
                                            </span>

                                        </div>

                                    </div>
                                    <div class="row" style="margin-top: 13px">


                                        <div class="col-md-10">
                                            <span style="float: right; margin-bottom: 2px">علـت بستن تاریخ دفاع </span>

                                            <span>
                                                <asp:TextBox ID="txtVirDsc" runat="server" CssClass="form-control rtl" Width="100%" MaxLength="198"></asp:TextBox>
                                            </span>

                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                    </div>
                </div>
                <div class="modal-footer">
                    <span style="float: left">
                        <asp:Button OnClick="BtnVirChange_Click" ID="BtnVirChange" CssClass="btn btn-primary" runat="server" Text="ثبت تغییرات" Width="120px" Height="32px" ValidationGroup="Virregister" />
                        <button type="button" class="btn btn-secondary" data-dismiss="modal" style="width: 120px; height: 32px">انصراف</button>
                    </span>
                </div>
            </div>
        </div>
        <br />

        <br />
        <br />
        <asp:Label ID="lblShowErrorMessage" CssClass="lblShowErrorMessage" runat="server" Visible="false"></asp:Label>
        <br />
        <br />
        <br />


        <telerik:RadWindowManager ID="radWindowManager" runat="server"></telerik:RadWindowManager>
    </div>
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
                    <div class="bg-green">
                           <img src="../fonts/calendar.png" style="width: 32px;float:right" alt="" />
                        <h5 class="header-inline-display" style="font-family: 'B Titr'">بستن تاریخ برگزاری دفاع</h5>
                    </div>
                    <div class="list-group-item" style="background: #a4935312;">

                        <div style="border: 1px solid green; padding: 10px">
                            <div class="row">

                                <div class="col-md-4 ">
                                    <img src="../fonts/calendar.png" style="width: 32px" alt="" />
                                    <span style="font-size: 14px">تاریخ شروع
                                    </span>

                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="pdate " MaxLength="9" ToolTip=" تاریخ شروع" />
                                    <asp:RequiredFieldValidator ID="RFVtxtDate1" ValidationGroup="register" runat="server" CssClass="alert" ControlToValidate="txtStartDate" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4 ">
                                    <img src="../fonts/calendar.png" style="width: 32px" alt="" />
                                    <span style="font-size: 14px">تاریخ پایان
                                    </span>
                         
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="pdate " MaxLength="9" ToolTip=" تاریخ پایان" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="register" runat="server" CssClass="alert" ControlToValidate="txtEndDate" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">
                                    <span style="margin-left: 14px; font-size: 14px">بـرای کـارمـنـدان</span>

                                    <asp:CheckBox ID="chkForEmployee" runat="server" />

                                </div>

                                <div class="col-md-2">

                                    <span style="margin-left: 14px; font-size: 14px">بـرای دانـشـجـویان
                                    </span>
                                    <asp:CheckBox ID="chkForStudent" runat="server" />
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-10">
                                    <span style="font-size: 14px">علـت بستن 
                                    </span>
                       
                                    <asp:TextBox ID="txtDsc" runat="server" CssClass="form-control" Width="100%" MaxLength="198"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <asp:Button ID="btnSave" runat="server" Text="ثبت" ValidationGroup="register" Width="100px" OnClick="btnSave_Click" class="btn btn-success" Style="float: left; margin-top: 10px; margin-left: 20px" />
                            </div>
                            <br />
                            <br />
                        </div>
                        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%; margin-top: 10px">


                            <telerik:RadGrid ID="grdSpecialDescription" runat="server" AutoGenerateColumns="False"
                                BackColor="#ffcccc"
                                PageSize="10"
                                CssClass="table table-responsive table-stripted backColortable"
                                OnNeedDataSource="grdSpecialDescription_NeedDataSource"
                                EnableEmbeddedSkins="false"
                                EmptyDataText="رکوردی برای این جدول وجود ندارد"
                                AllowPaging="True"
                                AllowFilteringByColumn="True" Skin="MyCustomSkin">

                                <MasterTableView DataKeyNames="Id" NoMasterRecordsText="رکوردی برای این جدول وجود ندارد" Dir="RTL">
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 1%; margin-right: 1%;">
                                            <h5>هیچ رکوردی وجود ندارد</h5>
                                        </div>
                                    </NoRecordsTemplate>
                                    <Columns>

                                        <telerik:GridTemplateColumn HeaderText="شماره  " HeaderStyle-VerticalAlign="Middle" Visible="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblid" runat="server"
                                                        Text='<%#Eval("id")%>' CssClass="text-center"
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="dateTatili  " HeaderStyle-VerticalAlign="Middle" Visible="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lbldateTatili" runat="server"
                                                        Text='<%#Eval("StartDate")%>' CssClass="text-center"
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="20px" DataField="startDate" HeaderText="تاریخ شروع" ItemStyle-Width="10%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="20px" DataField="endDate" HeaderText="تاریخ پایان" ItemStyle-Width="10%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="70px" DataField="description" HeaderText="علت بستن" ItemStyle-Width="45%">
                                        </telerik:GridBoundColumn>
                       
                                        <telerik:GridTemplateColumn HeaderText="برای کارمند  " HeaderStyle-VerticalAlign="Middle"
                                            AllowFiltering="false" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblforEmployee" runat="server"
                                                        Text='<%#((bool)Eval("isforEmployee")==true)?"بله":"خیر"%>'
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="برای دانشجو  " HeaderStyle-VerticalAlign="Middle"
                                            AllowFiltering="false" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblforStudent" runat="server"
                                                        Text='<%#((bool)Eval("isforStudent")==true)?"بله":"خیر"%>' CssClass="text-center"
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="اعمال" AllowFiltering="false" ItemStyle-Width="15%">
                                            <ItemTemplate>

                                                <button type="button" class="btn btn-info btnChange" style="width: 80px; height: 32px"
                                                    a_starttime='<%#Eval("StartDate")%>'
                                                    a_endtime='<%#Eval("EndDate")%>'
                                                    a_foremployees='<%#Eval("IsForEmployee")%>'
                                                    a_forstudents='<%#Eval("IsForStudent")%>'
                                                    a_dsc='<%#Eval("Description")%>'
                                                    a_id='<%#Eval("Id")%>'>
                                                    ویرایش 
                                                </button>
                                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text=" حذف  " OnClick="btnDelete_Click" Width="80px"
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
            radconfirm("آیا با حذف تاریخ موافقید؟", aspButtonCallbackFn, 550, 100, null, "Confirm");
        }
    </script>
    <script src="../Scripts/js-persian-cal.min.js"></script>
</asp:Content>

