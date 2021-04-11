<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationAssignmentResource.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationAssignmentResource" %>
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

        .drp {
            border-radius: 10px;
            border: 1px solid #106062;
        }
    </style>
    <script>
        function confirmAspButton1(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا با حذف دانشجو موافقید؟", aspButtonCallbackFn, 550, 100, null, "Confirm");
        }

        $(document).ready(function () {
            $(".btnChange").click(function () {
              
                var id = $(this).attr("a_id");
                var startTime = $(this).attr("a_startTime");
                var endTime = $(this).attr("a_endTime");
                var CollegeId = $(this).attr("a_CollegeId");
                var ResourceId = $(this).attr("a_ResourceId");
                var IsShared = $(this).attr("a_IsShared") =="True"?1:0;

                $("input[id*= txtVirDate1]").val(startTime);
                $("input[id*= txtVirDate2]").val(endTime);
                $("input[id*= LabelIdVir]").val(id);
                $("select[id*= drpVirCollegeId]").val(CollegeId).change();
                $("select[id*= drpVirResourceId]").val(ResourceId).change();
                $("select[id*= drpVirIsShared]").val(IsShared).change();
                $('#modalChange').modal("show");

            });
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate1',
                { extraInputID: 'ContentPlaceHolder1_txtDate1', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate2',
                { extraInputID: 'ContentPlaceHolder1_txtDate2', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('PageTitle_txtVirDate1',
                { extraInputID: 'PageTitle_txtVirDate1', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('PageTitle_txtVirDate2',
                { extraInputID: 'PageTitle_txtVirDate2', extraInputFormat: 'yyyy/mm/dd' });






        });

        function onLoadRadTimePicker1(sender, args) {
            txtTime1 = sender;
        }
        function onLoadRadTimePicker2(sender, args) {
            txtTime2 = sender;
        }
        function onLoadRadTimePicker2(sender, args) {
            txtVirDate1 = sender;
        }
        function onLoadRadTimePicker2(sender, args) {
            txtVirDate2 = sender;
        }
    </script>
    <style>
        a.pcalBtn {
            position: relative;
            margin-right: -22px;
            vertical-align: middle;
        }

        .modal {
            opacity: 1
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

        .btn {
            padding: 2px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
        <h3>
        <asp:Literal ID="pt" runat="server"></asp:Literal>
    </h3>
    <div class="modal" tabindex="-1" role="dialog" id="modalChange">
        <div class="modal-dialog" role="document" style="width:98%">
            <div class="modal-content" style="width:98%">
                <div class="modal-header">
                    <h5 class="modal-title">ویرایش تخصیص منابع دفاع</h5>


                </div>
                <div class="modal-body">

                    <div id="div_Main" style="padding-left: 15%; padding-right: 5%; margin-bottom: 0px" class="pnl_First">

                        <asp:Panel ID="pnl_First" runat="server">


                            <div class="row">
                                <div class="col-md-12 ">
                                    <div class="row">
                                        <div class="col-md-4  ">
                                            <asp:HiddenField ID="LabelIdVir" runat="server"></asp:HiddenField>


                                            <asp:TextBox ID="txtVirDate1" runat="server" CssClass="pdate  " MaxLength="9" ToolTip="آغاز تاریخ ثبت" />
                                            <span style="margin-left: 40px">تاریخ آغاز</span>
                                            <img src="../fonts/calendar.png" style="width: 32px" alt="" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Virregister" runat="server" CssClass="alert" ControlToValidate="txtVirDate1" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-4">


                                            <asp:TextBox ID="txtVirDate2" runat="server" CssClass="pdate " MaxLength="9" ToolTip="پایان تاریخ ثبت" />
                                            <span style="margin-left: 40px">تاریخ پایان</span>
                                            <img src="../fonts/calendar.png" style="width: 32px" alt="" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Virregister" runat="server" CssClass="alert" ControlToValidate="txtVirDate2" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>


                                        </div>
                                           <div class="col-md-4">
                                               </div>

                                    </div>
                                    
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-4">
                                    <span style="float: right">نام دانشکده </span>

                                    <span style="float: right; margin-right: 10px">
                                        <asp:DropDownList ID="drpVirCollegeId" runat="server" CssClass="form-control"  ForeColor="Black" >
                                        </asp:DropDownList>
                                    </span>
                                </div>
                                <div class="col-md-4">
                                    <span style="float: right; margin-right: 22PX">نـام منـبع </span>
                                    <span style="float: right; margin-right: 10PX">
                                        <asp:DropDownList ID="drpVirResourceId" runat="server" Font-Size="14px" CssClass="form-control"  ForeColor="Black"  >
                                        </asp:DropDownList>
                                    </span>
                                </div>
                                <div class="col-md-4">
                                    <span style="float: right;">
                                    منبع اشتراکی
                                          </span>
                                    <span style="float: right;margin-right:10px">
                                        <asp:DropDownList ID="drpVirIsShared"  runat="server" CssClass="form-control" Width="200px" Font-Size="14PX" Font-Bold="true" 
                                                                >
                                                                <asp:ListItem Text="بله " Value="1" />
                                                                <asp:ListItem Text="خیر" Value="0" />
                                                            </asp:DropDownList>
                                        </span>
                                </div>
                            </div>
                                </div>
                            </div>
                        </asp:Panel>


                    </div>
                </div>
                <div class="modal-footer">
                    <span style="float: left">
                        <asp:Button OnClick="BtnVirChange_Click" ID="BtnVirChange" ValidationGroup="Virregister" CssClass="btn btn-primary" runat="server" Text="ثبت تغییرات" Width="120px" Height="32px" />
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
    <div class="modal fade AlertChange" tabindex="-1" role="dialog" aria-labelledby="AlertChange" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">

                    <p id="msgAlert"></p>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary " data-dismiss="modal">تایید</button>
                </div>

            </div>
        </div>
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
                        <input type="button" value="تایید" class="rwOkBtn btn btn-danger" style="width: 120px" onclick="$find('{0}').close(true); return false;" />
                    </div>
                </div>
            </AlertTemplate>
        </telerik:RadWindowManager>
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="bg-purple">
                        <img src="../fonts/commitment.png" style="width: 32px; float: right" alt="" />
                        <h5 class="header-inline-display" style="font-family: 'B Titr'">تخصیص منابع دفاع</h5>
                    </div>
                    <div class="list-group-item" style="background: #a4935312;">

                        <div class="row" style="border: 1PX black solid">
                            <div class="col-lg-12"> 
                            <div class="row" style="margin-top:2px" >
                                <div class="col-md-3 ">
                                       <img src="../fonts/calendar.png" style="width: 32px" alt="" />
                                    <span>تاریخ آغاز</span>
                                 


                                    <asp:TextBox ID="txtDate1" runat="server" CssClass="pdate " MaxLength="9" ToolTip="آغاز تاریخ " />
                                    <asp:RequiredFieldValidator ID="RFVtxtDate1" ValidationGroup="register" runat="server" CssClass="alert" ControlToValidate="txtDate1" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <img src="../fonts/calendar.png" style="width: 32px" alt="" />
                                    <span>تاریخ پایان</span>

                                    <asp:TextBox ID="txtDate2" runat="server" CssClass="pdate " MaxLength="9" ToolTip="پایان تاریخ " />
                                    <asp:RequiredFieldValidator ID="RFVtxtDate2" ValidationGroup="register" runat="server" CssClass="alert" ControlToValidate="txtDate2" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>


                                </div>
                            </div>


                            <div class="row" style="margin-top: 10px">
                                <div class="col-lg-3">
                                    <span style="float: right">نام دانشکده </span>

                                    <span style="float: right; margin-right: 10px">
                                        <asp:DropDownList ID="drpCollegeId" runat="server" CssClass="form-control" ForeColor="Black" >
                                        </asp:DropDownList>
                                    </span>
                                </div>
                                <div class="col-lg-4">
                                    <span style="float: right; margin-right: 22PX">نـام منـبع </span>
                                    <span style="float: right; margin-right: 10PX">
                                        <asp:DropDownList ID="drpResource" runat="server" Font-Size="14px" CssClass="form-control" ForeColor="Black" >
                                        </asp:DropDownList>
                                    </span>
                                </div>
                                <div class="col-lg-4">
                                    <span style="float: right;">
                                    منبع اشتراکی
                                          </span>
                                    <span style="float: right;margin-right:10px">
                                        <asp:DropDownList ID="drpIsShared"  runat="server" CssClass="form-control" Width="200px" Font-Size="14PX" Font-Bold="true" 
                                                               >
                                                                <asp:ListItem Text="بله " Value="1" />
                                                                <asp:ListItem Text="خیر" Value="0" />
                                                            </asp:DropDownList>
                                        </span>
                                </div>
                            </div>

                            <div class="row">
                                <asp:Button ID="btnSave" runat="server" Text="ثبت" ValidationGroup="register" Width="120px" Height="32px" OnClick="btnSave_Click" class="btn btn-success" Style="float: left; margin-left: 5%" />
                            </div>
                        </div>
                            </div>
                        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%; margin-top: 1%">


                            <telerik:RadGrid ID="grdAssignmentResource" runat="server" AutoGenerateColumns="False"
                                BackColor="#ffcccc"
                                PageSize="10"
                                CssClass="table table-responsive table-stripted backColortable"
                                EnableEmbeddedSkins="false"
                                EmptyDataText="رکوردی برای این جدول وجود ندارد"
                                AllowPaging="True"
                                OnNeedDataSource="grdAssignmentResource_NeedDataSource"
                                AllowFilteringByColumn="True" Skin="MyCustomSkin">

                                <MasterTableView DataKeyNames="Id" NoMasterRecordsText="رکوردی برای این جدول وجود ندارد" Dir="RTL">
                                    <NoRecordsTemplate>
                                        <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 1%; margin-right: 1%;">
                                            <h5>هیچ رکوردی وجود ندارد</h5>
                                        </div>
                                    </NoRecordsTemplate>
                                    <Columns>

                                        <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="70px" DataField="startDate" HeaderText="تاریخ شروع">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="70px" DataField="endDate" HeaderText="تاریخ پایان">
                                        </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="70px" DataField="resourceName" HeaderText="نام منبع">
                                        </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="70px" DataField="collegeName" HeaderText="نام دانشکده">
                                        </telerik:GridBoundColumn>
                                   <telerik:GridTemplateColumn HeaderText="منبع اشتراکی " HeaderStyle-VerticalAlign="Middle" 
                                            AllowFiltering="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblIsShared"  runat="server"
                                                         Text='<%#((bool)Eval("IsShared")==true)?"بله":"خیر"%>' CssClass="text-center"
                                                        
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="شماره" HeaderStyle-VerticalAlign="Middle"
                                            AllowFiltering="false" Visible="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblId" runat="server"
                                                        Text='<%#Eval("id")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                                                     <telerik:GridTemplateColumn HeaderText="شماره" HeaderStyle-VerticalAlign="Middle"
                                            AllowFiltering="false" Visible="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblstartDate" runat="server"
                                                        Text='<%#Eval("startDate")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="شماره" HeaderStyle-VerticalAlign="Middle"
                                            AllowFiltering="false" Visible="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblendDate" runat="server"
                                                        Text='<%#Eval("endDate")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                                                              <telerik:GridTemplateColumn HeaderText="شماره منبع" HeaderStyle-VerticalAlign="Middle"
                                            AllowFiltering="false" Visible="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblResourceId" runat="server"
                                                        Text='<%#Eval("ResourceId")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                                                              <telerik:GridTemplateColumn HeaderText="شماره دانشکده" HeaderStyle-VerticalAlign="Middle"
                                            AllowFiltering="false" Visible="false">
                                            <ItemTemplate>
                                                <div class="text-center">
                                                    <asp:Label ID="lblCollegeId" runat="server"
                                                        Text='<%#Eval("CollegeId")%>'
                                                        ForeColor="Black"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="اعمال" AllowFiltering="false">
                                            <ItemTemplate>
                                                <button type="button" class="btn btn-info btnChange" style="width: 120px; height: 32px"
                                                    a_starttime='<%#Eval("StartDate")%>'
                                                    a_endtime='<%#Eval("EndDate")%>'
                                                    a_CollegeId='<%#Eval("CollegeId")%>'
                                                    a_ResourceId='<%#Eval("ResourceId")%>'
                                                    a_id='<%#Eval("Id")%>'
                                                    a_IsShared='<%#Eval("IsShared")%>'>
                                                    ویرایش 
                                                </button>
                                       <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text=" حذف  " OnClick="btnDelete_Click" Width="120px"
                                                    OnClientClick="confirmAspButton1(this); return false;" />

                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                    </Columns>

                                </MasterTableView>

                            </telerik:RadGrid>

                        </div>
                        <br />



                    </div>

                </div>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {
            $(".collapse").collapse('show');
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate1',
                { extraInputID: 'ContentPlaceHolder1_txtDate1', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate2',
                { extraInputID: 'ContentPlaceHolder1_txtDate2', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate2',
                { extraInputID: 'ContentPlaceHolder1_txtDate2', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate2',
                { extraInputID: 'ContentPlaceHolder1_txtDate2', extraInputFormat: 'yyyy/mm/dd' });
        

        });


    </script>

    <script src="../Scripts/js-persian-cal.min.js"></script>
</asp:Content>
