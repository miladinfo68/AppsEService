<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationOnlineDefensePlayback.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationOnlineDefensePlayback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
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

    <link href="../Style/EducationOnlineDefence.css" rel="stylesheet" />
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <link rel="stylesheet" href="../../CommonUI/css/js-persian-cal.css" />
    <script src="../../CommonUI/js/js-persian-cal.min.js"></script>

    <script src="../Scripts/EducationOnlineDefensePlayback.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">

    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
    <link href="../Style/bootstrap-rtl.min.css" rel="stylesheet" />
    <link href="../Style/StudentStyle.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        var RadTimePicker1;
        var RadTimePicker2;

        function isNullOrEmpty(s) {
            return (s == null || s === "");
        }
        function strcmp(date1, date2) {
            // return date2 < date1 ? true : false;
            return false;
        }



        //==================================================
        function onLoadRadTimePicker1(sender, args) {
            RadTimePicker1 = sender;
        }

        function onLoadRadTimePicker2(sender, args) {
            RadTimePicker2 = sender;
        }


        //####################################################
        // costum validators

        function custmValid4Student(sender, args) {
<%--          <% --var time1 = $find("<%=RadTimePicker1.ClientID %>").get_dateInput().get_value();
    //var time2 = $find("<%=RadTimePicker2.ClientID %>").get_dateInput().get_value();--%>
            var time1 = $('#<%=RadTimePicker1.ClientID %>').val();
            var time2 = $('#<%=RadTimePicker2.ClientID %>').val();
            if (time2 <= time1) {
                args.IsValid = false;
                $('#spnErrorTime_Student').show();
                return false;
            } else {
                args.IsValid = true;
                $('#spnErrorTime_Student').hide();
                return true;
            }
        }
        $(document).ready(function () {
            $(".btnChangeDate").click(function () {
                debugger;
                var id = $(".btnChangeDate").attr("a_id");
                var startTime = $(".btnChangeDate").attr("a_startTime");
                var endtTime = $(".btnChangeDate").attr("a_endTime");
                var Date = $(".btnChangeDate").attr("a_date");
                var timePicker1 = $find("<%=RadTimePicker1.ClientID %>");
                timePicker1.get_dateInput().set_value(startTime);
                var timePicker2 = $find("<%=RadTimePicker2.ClientID %>");
                timePicker2.get_dateInput().set_value(endtTime);
                $("input[id *=ContentPlaceHolder2_pcal1]").val(Date);
                $("input[id*=txtCurId]").val(id);
                $('#modalChangeDate').modal("show");
            });
            $("#BtnChangeDate").click(function () {
                $("#lblAlertDate").text($("input[id*=txtCurId]").val());
                $('#modalAlertDate').modal("show");
            });


        });
    </script>
    <div id="content-section">
        <div class="modal fade modaldef" tabindex="-1" role="dialog" aria-hidden="true">
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

        <div class="modal" tabindex="-1" role="dialog" id="modalChangeDate" style="opacity: 0.9">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">تغییر تاریخ دفاع</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <asp:TextBox ID="txtCurId" runat="server"></asp:TextBox>
                    </div>
                    <div class="modal-body">

                        <div id="div_Main" style="padding-left: 15%; padding-right: 5%; margin-bottom: 0px" class="pnl_First">

                            <asp:Panel ID="pnl_First" runat="server">


                                <div class="row">
                                    <div class="col-sm-10 ">
                                        <div>
                                            <table class="table table-bordered table-condensed  tbl">
                                                <tr>
                                                    <td id="dvTime" runat="server">
                                                        <div class="row">

                                                            <asp:ValidationSummary ID="submit" runat="Server" ShowMessageBox="true" ValidationGroup="submit" />

                                                            <div id="dvStartDate" class="col-md-6">
                                                                <p id="dateofsession">تاریخ دفاع :</p>
                                                                <div id="pcalWrapper1" class="pcalWrapper">
                                                                    <asp:TextBox ID="pcal1" runat="server" CssClass="pdate form-control " />
                                                                </div>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ErrorMessage="لطفا تاریخ درخواست خود را مشخص کنید" ControlToValidate="pcal1" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="pcal1" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>
                                                            </div>


                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <span id="spnErrorDate_Student" style="display: none; color: red;">تاریخ پایان باید بیشتر از تاریخ شروع باشد</span>
                                                            </div>
                                                            <div class="col-md-6 ">
                                                                <span id="spnErrorTime_Student" style="display: none; color: red;">زمان پایان باید بیشتر اززمان شروع باشد</span>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <p>ساعت آغاز :</p>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="لطفا زمان آغاز را مشخص کنید" ValidationGroup="submit" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                                                <asp:CustomValidator ID="startTime4Student" ErrorMessage="زمان آغازین باید کمتر اززمان پایان باشد" ValidationGroup="submit" ForeColor="Red" Display="None" ClientValidationFunction="custmValid4Student" ControlToValidate="RadTimePicker1" runat="server" />
                                                                <telerik:RadTimePicker ID="RadTimePicker1" EnableTyping="false" RenderMode="Lightweight" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <TimeView ID="TimeView1" Interval="01:00:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00"></TimeView>
                                                                    <DateInput ID="DateInput1" runat="server">
                                                                        <ClientEvents OnLoad="onLoadRadTimePicker1"></ClientEvents>
                                                                    </DateInput>
                                                                </telerik:RadTimePicker>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <p>ساعت پایان :</p>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage=" لطفا زمان پایان را مشخص کنید" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />
                                                                <asp:CustomValidator ID="vldTimes" ErrorMessage="زمان پایان باید بیشتر اززمان آغاز باشد" ValidationGroup="submit" ForeColor="Red" Display="None" ClientValidationFunction="custmValid4Student" ControlToValidate="RadTimePicker2" runat="server" />

                                                                <telerik:RadTimePicker ID="RadTimePicker2" RenderMode="Lightweight" runat="server" EnableTyping="false" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                    <TimeView ID="TimeView2" Interval="01:00:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                                    </TimeView>
                                                                    <DateInput ID="DateInput2" runat="server">
                                                                        <ClientEvents OnLoad="onLoadRadTimePicker2"></ClientEvents>
                                                                    </DateInput>
                                                                </telerik:RadTimePicker>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>


                        </div>
                    </div>
                    <div class="modal-footer">

                        <asp:Button OnClick="BtnChangeDate_Click" ID="BtnChangeDate" CssClass="btn btn-primary" runat="server" Text="ثبت تغییرات" />
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">انصراف</button>
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


    </div>

    <div class="container">

        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">

                    <div class="list-group-item">
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


                        <img src="../fonts/oprator.png" style="width: 32px; margin-right: 10px;" alt="" />
                        <h5 class="header-inline-display">مشاهده دفاع‌ها</h5>
                        <button class="btn btn-danger btnLinkTesti">جلسه تستی1</button>
                        <button class="btn btn-danger btnLinkTesti2">جلسه تستی2</button>
                        <button class="btn btn-danger btnLinkTesti3">جلسه تستی3</button>
                        <button class="btn btn-danger btnLinkTesti4">جلسه تستی4</button>
                        <button class="btn btn-danger btnLinkTesti5">جلسه تستی5</button>
                        <button class="btn btn-danger btnLinkTesti6 hidden">جلسه تستی6</button>


                        <div id="OpPanle" class="row bg-green" style="border-radius: 5px; margin-top: 5px">
                            <div class="col-lg-4">
                                <h4>نام دانشکده </h4>


                                <asp:DropDownList ID="drpCollegeId" runat="server" CssClass="form-control" AutoPostBack="True" ForeColor="Black" OnSelectedIndexChanged="drpCollegeId_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <!-- Tab panes -->
                            <div class="tab-content col-md-12">
                                <div role="tabpanel" id="profile">
                                    <br />
                                    <div class="container" style="text-align: center">
                                        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">


                                            <telerik:RadGrid ID="grdDsiplayDefence" runat="server" AutoGenerateColumns="False"
                                                BackColor="#ffcccc"
                                                PageSize="10"
                                                OnNeedDataSource="grdDsiplayDefence_NeedDataSource"
                                                CssClass="table table-responsive table-stripted backColortable" OnSelectedIndexChanged="grdDsiplayDefence_SelectedIndexChanged"
                                                EnableEmbeddedSkins="false"
                                                EmptyDataText="رکوردی برای این جدول وجود ندارد"
                                                AllowPaging="True" ClientSettings-EnablePostBackOnRowClick="true"
                                                AllowFilteringByColumn="True" Skin="MyCustomSkin">
                                                <ClientSettings EnablePostBackOnRowClick="True">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                                <MasterTableView DataKeyNames="Id" NoMasterRecordsText="رکوردی برای این جدول وجود ندارد" Dir="RTL">

                                                    <Columns>
                                                        <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="70px" DataField="StudentCode" HeaderText="شماره دانشجویی">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentFullName" HeaderText="نام و نام خانوادگی">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="کد دانشجویی" HeaderStyle-VerticalAlign="Middle"
                                                            AllowFiltering="true" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstudentcode" runat="server"
                                                                    Text='<%#Eval("studentcode")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="آی دی وضعیت برگزاری" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVazId" runat="server"
                                                                    Text='<%#Eval("vazId")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="نام و نام خانوادگی" HeaderStyle-VerticalAlign="Middle"
                                                            AllowFiltering="true" Visible="false">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblName" runat="server"
                                                                        Text='<%#Eval("StudentFullName")%>' CssClass="text-center"
                                                                        ForeColor="Black"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="شماره دفاع" HeaderStyle-VerticalAlign="Middle"
                                                            AllowFiltering="false" Visible="false">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblRequestId" runat="server"
                                                                        Text='<%#Eval("RequestId")%>' CssClass="text-center"
                                                                        ForeColor="Black"></asp:Label>
                                                                </div>

                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="موضوع" HeaderStyle-VerticalAlign="Middle"
                                                            AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <div class="text-center DefenceSubject">
                                                                    <asp:Label ID="lblDefenceSubject" runat="server"
                                                                        Text='<%#Eval("DefenceSubject")%>' CssClass="text-center "
                                                                        ForeColor="Black"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="رشته" HeaderStyle-VerticalAlign="Middle"
                                                            AllowFiltering="false">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnameresh" runat="server"
                                                                    Text='<%#Eval("nameresh")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="محل برگزاری"
                                                            AllowFiltering="false" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocName" runat="server"
                                                                    Text='<%#Eval("LocName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="ساعت برگزاری" HeaderStyle-VerticalAlign="Middle" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstartTime" runat="server"
                                                                    Text='<%#Eval("startTime")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="وضعیت" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvaz" runat="server"
                                                                    Text='<%#Eval("vazName")%>'></asp:Label>

                                                                <img src="../Images/<%#Eval("vazSrc")%>" />

                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="اعمال" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <%--      <button type="button" class="btn btn-info btnChangeDate"
                                                                    a_starttime='<%#Eval("startTime")%>'
                                                                    a_endtime='<%#Eval("endTime")%>'
                                                                    a_date='<%#Eval("RequestDate")%>'
                                                                    a_id='<%#Eval("id")%>'>
                                                                    ویرایش تاریخ
                                                                </button>--%>

                                                                <%--                                                                <asp:Button ID="lnkLinkDefence" runat="server" CssClass="btn btn-success" Text=" ورود به جلسه دفاع  " OnClick="lnkLinkDefence_Click" Width="120px" OnClientClick="target ='_blank';"  />--%>
                                                                <a href="#" class="btn btn-success btnLinkDefence pad"
                                                                    data-vazid='<%#Eval("vazId")%>' data-studentcode='<%#Eval("studentcode")%>'
                                                                    data-reslink='<%#Eval("resLink")%>' style="font-size: 12px">ورود به جلسه دفاع</a>
                                                                <asp:Label ID="resLink" runat="server" Text='<%#Eval("resLink")%>' Visible="false"></asp:Label>

<%--                                                                <asp:Button ID="btnOpenMeeting" runat="server" CssClass="btn btn-primary pad" Text="شروع جلسه" OnClick="btnOpenMeeting_Click" Visible='<%#(Eval("FlagStartMeeting")==null&&Eval("FlagEndMeeting")==null)||((bool)Eval("FlagStartMeeting")==false)?true:false%>' Width="120px" />


                                                                <asp:Button ID="btnEndMeeting" runat="server" CssClass="btn btn-danger pad" Text="پایان جلسه" OnClick="btnEndMeeting_Click" Visible='<%#(Eval("FlagStartMeeting")==null&&Eval("FlagEndMeeting")==null)||((bool)Eval("FlagEndMeeting")==true||(bool)Eval("FlagStartMeeting")==false)?false:true%>' Font-Size="12px" />
                                                                <asp:Button ID="btnEndedMeeting" runat="server" CssClass="btn btn-warning pad" Text="جلسه خاتمه یافته" Visible='<%#(Eval("FlagStartMeeting")==null&&Eval("FlagEndMeeting")==null)||((bool)Eval("FlagEndMeeting")==false||(bool)Eval("FlagStartMeeting")==false)?false:true%>' Font-Size="12px" Enabled="false" />--%>


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
                </div>
            </div>
        </div>
    </div>
    <script>
        $(function () {
            $('.btnLinkDefence').on('click', function (e) {
                e.preventDefault();
                var btn = $(this);
                //alert(btn.attr('data-vazId') + '...' + btn.attr('data-studentCode') + '...' + btn.attr('data-resLink'));
                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/GetDefenceLink',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ vazid: btn.attr('data-vazId'), studentCode: btn.attr('data-studentCode'), resLink: btn.attr('data-resLink'), userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                        success: function (r) {

                            if (r.d == "false") {
                                $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                                $('.modalDef').modal('show');
                            }
                            else {
                                window.open(r.d, '_blank');
                            }
                        }
                    });
                });
                $('.btnLinkTesti').on('click', function (e) {
                    e.preventDefault();

                    $.ajax({
                        url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti',
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                        success: function (r) {

                            if (r.d == "false") {
                                $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                                $('.modalDef').modal('show');
                            }
                            else {
                                window.open(r.d, '_blank');
                            }
                        }
                    });
                });
            $('.btnLinkTesti2').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti2',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                        success: function (r) {

                            if (r.d == "false") {
                                $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                                $('.modalDef').modal('show');
                            }
                            else {
                                window.open(r.d, '_blank');
                            }
                        }
                    });
            });
            $('.btnLinkTesti3').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti3',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                        success: function (r) {

                            if (r.d == "false") {
                                $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                                $('.modalDef').modal('show');
                            }
                            else {
                                window.open(r.d, '_blank');
                            }
                        }
                    });
            });
            $('.btnLinkTesti4').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti4',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                        success: function (r) {

                            if (r.d == "false") {
                                $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                                $('.modalDef').modal('show');
                            }
                            else {
                                window.open(r.d, '_blank');
                            }
                        }
                    });
            });
            $('.btnLinkTesti5').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti5',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                        success: function (r) {

                            if (r.d == "false") {
                                $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                                $('.modalDef').modal('show');
                            }
                            else {
                                window.open(r.d, '_blank');
                            }
                        }
                    });
            });
            $('.btnLinkTesti6').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti6',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                        success: function (r) {

                            if (r.d == "false") {
                                $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                                $('.modalDef').modal('show');
                            }
                            else {
                                window.open(r.d, '_blank');
                            }
                        }
                    });
               });

            });
    </script>
</asp:Content>
