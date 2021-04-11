<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="StudentEditRequest.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.StudentEditRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../Style/pretty-checkbox.min.css" rel="stylesheet" />


    <link href="../Style/StudentStyle.css" rel="stylesheet" />
    <script>

        $(document).ready(function () {

            $('[data-tooltip="tooltip1"]').tooltip();
            $('[data-tooltip="tooltip1"]').on('click',
                function () {
                    if ($('#imgIcon1').attr('src') === '../fonts/expand.png') {

                        $('#imgIcon1').attr('src', '../fonts/collapse.png');
                    } else {
                        $('#imgIcon1').attr('src', '../fonts/expand.png');
                    }
                });

            $('[data-tooltip="tooltip2"]').tooltip();
            $('[data-tooltip="tooltip2"]').on('click',
                function () {
                    if ($('#imgIcon2').attr('src') === '../fonts/expand.png') {


                        $('#imgIcon2').attr('src', '../fonts/collapse.png');
                    } else {
                        $('#imgIcon2').attr('src', '../fonts/expand.png');


                    }
                });

            $('[data-tooltip="tooltip3"]').tooltip();
            $('[data-tooltip="tooltip3"]').on('click',
                function () {
                    if ($('#imgIcon3').attr('src') === '../fonts/expand.png') {
                        $('#imgIcon3').attr('src', '../fonts/collapse.png');
                    } else {
                        $('#imgIcon3').attr('src', '../fonts/expand.png');
                    }
                });

            $('[data-tooltip="tooltip4"]').tooltip();
            $('[data-tooltip="tooltip4"]').on('click',
                function () {
                    if ($('#imgIcon4').attr('src') === '../fonts/expand.png') {
                        $('#imgIcon4').attr('src', '../fonts/collapse.png');
                    } else {
                        $('#imgIcon4').attr('src', '../fonts/expand.png');
                    }
                });

            $('[type="checkbox"]').css('vertical-align', 'middle').css('margin-top', '-1px').css('width', '3%');



            var rdbOwnSystem = document.getElementById('ContentPlaceHolder1_rdbOwnSystem');
            if (rdbOwnSystem.checked == false) {
                $('#ContentPlaceHolder1_rdbUniversitySystem').prop("checked", true);
            }

            $('#ContentPlaceHolder1_rdbUniversitySystem').change(
                function () {

                    if (this.checked) {
                        $('#ContentPlaceHolder1_rdbOwnSystem').prop("checked", false);
                    }
                });
            $('#ContentPlaceHolder1_rdbOwnSystem').change(
                function () {

                    if (this.checked) {
                        $('#ContentPlaceHolder1_rdbUniversitySystem').prop("checked", false);
                    }
                });




            var rdbOwnSystem = document.getElementById('ContentPlaceHolder1_rdbOwnSystem');
            if (rdbOwnSystem.checked == false) {
                $('#ContentPlaceHolder1_rdbUniversitySystem').prop("checked", true);
            } else {
                $('#ContentPlaceHolder1_rdbUniversitySystem').prop("checked", false);
            }
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

    <h3>ثبت درخواست رزرو جلسه دفاع</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

    <hr />

    <div class="container">
        <asp:HiddenField ID="LblLastDate" runat="server"  ></asp:HiddenField>
        <asp:HiddenField ID="LblLastTime" runat="server"  ></asp:HiddenField>
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip1" data-toggle="collapse" data-target="#collapse1" title="اطلاعات دفاع دانشجو در این بخش قابل مشاهده است">
                        <div class="panel-title">
                            <img src="../fonts/expand.png" style="width: 2%" id="imgIcon1" alt="" />
                            <div class="header-inline-display">اطلاعات جلسه دفاع دانشجو</div>
                        </div>
                    </div>
                    <div id="collapse1" class="panel-collapse collapse">
                        <div class="list-group">
                            <div class="list-group-item">
                                <img src="../fonts/writing.png" style="width: 32px" alt="" />
                                <h5 class="header-inline-display">موضوع:</h5>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">
                                    <asp:Literal ID="litSubject" runat="server"></asp:Literal>
                                </h6>
                            </div>
                            <div class="list-group-item">
                                <img src="../fonts/lecture.png" style="width: 32px" alt="" />
                                <h5 class="header-inline-display">استاد راهنما:</h5>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">
                                    <asp:Literal ID="litGuide" runat="server"></asp:Literal>

                                </h6>

                            </div>
                            <div class="list-group-item">
                                <img src="../fonts/conference.png" style="width: 32px" alt="" />
                                <h5 class="header-inline-display">استاد مشاور:</h5>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">
                                    <asp:Literal ID="litConsultent" runat="server"></asp:Literal>
                                </h6>
                            </div>
                            <div class="list-group-item">
                                <img src="../fonts/law.png" style="width: 32px" alt="" />
                                <h5 class="header-inline-display">داور:</h5>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">
                                    <asp:Literal ID="litReferee" runat="server"></asp:Literal>
                                </h6>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>

        <asp:HiddenField ID="hdnrequestId" runat="server" />
        <asp:HiddenField ID="hdnUserId" runat="server" />
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip2" data-toggle="collapse" data-target="#collapse2" title="زمان مورد درخواست خود را برای جلسه دفاع ویرایش نمایید">
                        <div class="panel-title">
                            <img src="../fonts/expand.png" style="width: 2%" id="imgIcon2" alt="" />
                            <div class="header-inline-display">ویرایش زمان درخواست رزرو جلسه دفاع</div>
                        </div>
                    </div>
                    <div id="collapse2" class="panel-collapse collapse">
                        <div class="list-group">
                            <div class="list-group-item">
                                <div class="row" style="margin-right: 1px;">
                                    <img src="../Images/administrator.png" style="width: 32px" alt="" />
                                    <h5 class="header-inline-display">وضعیت اساتید مرتبط با دفاع شما:</h5>
                                    <br />
                                    <div class="container" style="text-align: center">
                                        <div class="row" dir="rtl" style="margin-left: 1%; margin-right: 1%;">
                                            <br />
                                            <div class="alert alert-info">
                                                دانشجوي محترم بدينوسيله به استحضار مي­رساند اركان دفاع (شامل اساتيد راهنما، مشاور و داور يا داوران) مطابق برنامه زمانبندي ذيل جهت برگزاري دفاع ساير دانشجويان در دانشگاه حاضر خواهند بود.
                                            </div>

                                            <asp:GridView runat="server" ID="grdAttendanceProfessores"
                                                AutoGenerateColumns="False"
                                                CssClass="table table-responsive table-striped table-hover"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="برنامه ای برای اساتید در سامانه موجود نمی باشد">
                                                <HeaderStyle CssClass="tableBorder"></HeaderStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ردیف" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex +1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Name" HeaderText="نام و نام خانوادگی" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Date" HeaderText="تاریخ" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="StartTime" HeaderText="ساعت شروع" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="EndTime" HeaderText="ساعت پایان" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Kind" HeaderText="جلسه" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Role" HeaderText="سمت" ItemStyle-HorizontalAlign="Center" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6 ">

                                        <img src="../fonts/calendar.png" style="width: 32px" alt="" />
                                        <h5 class="header-inline-display" style="margin-left: 5%">تاریخ:</h5>

                                        <asp:TextBox ID="txtDate" runat="server" CssClass="pdate disabled" MaxLength="9" ToolTip="تاریخ درخواستی دفاع" />
                                        <asp:RequiredFieldValidator ID="RFVtxtDate" ValidationGroup="register" runat="server" CssClass="alert" ControlToValidate="txtDate" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <img src="../fonts/stopwatch.png" style="width: 32px" alt="" />
                                        <h5 class="header-inline-display" style="margin-left: 5%">ساعت:</h5>

                                        <telerik:RadTimePicker ID="txtTime" EnableTyping="false" RenderMode="Lightweight" runat="server" Culture="fa-IR" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" CssClass="pdate">
                                            <TimeView ID="TimeView4" Interval="01:00:00" runat="server" TimeFormat="HH:mm" Columns="2" StartTime="08:00" EndTime="20:00">
                                            </TimeView>
                                            <DateInput ID="DateInput1" runat="server">
                                                <ClientEvents OnLoad="onLoadRadTimePicker1"></ClientEvents>
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                        <asp:RequiredFieldValidator ID="RFVtxtTime" runat="server" ValidationGroup="register" CssClass="alert" ControlToValidate="txtTime" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا زمان مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row hidden" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip3" data-toggle="collapse" data-target="#collapse3" title="در صورت نیاز به شرکت استاد به صورت آنلاین از بخش زیر استفاده نمایید">
                        <div class="panel-title">
                            <img src="../fonts/expand.png" style="width: 2%" id="imgIcon3" alt="" />
                            <div class="header-inline-display">حضور استاد به صورت آنلاین در جلسه دفاع(تنها یکی از اساتید راهنما و مشاور می توانند آنلاین باشند)</div>
                        </div>
                    </div>
                    <div id="collapse3" class="panel-collapse collapse ">
                        <div class="list-group">
                            <div class="list-group-item">
                                <div class="row">
                                    <div class="col-md-6">
                                        <img src="../fonts/notepad.png" style="width: 32px" alt="" />
                                        <h5 class="header-inline-display">تعیین استاد آنلاین:</h5>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <asp:CheckBox ID="ckbAprroveOnline" runat="server" OnCheckedChanged="ckbAprroveOnline_OnCheckedChanged" AutoPostBack="True" />

                                        <h6 class="header-inline-display">
                                            <span>اینجانب</span>
                                            <span>
                                                <asp:Literal ID="litStudentName" runat="server"></asp:Literal></span>

                                            <span>متقاضی حضور استاد به صورت آنلاین هستم.</span>

                                        </h6>
                                    </div>
                                    <div class="col-md-6" style="visibility:hidden">
                                        <div class="alert alert-info" style="text-align: center">در صورت نیاز به پخش آنلاین  تیک مربوطه را بزنید</div>
                                        <img src="../fonts/notepad.png" style="width: 32px" alt="" />
                                        <h5 class="header-inline-display">تایید پخش آنلاین:</h5>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <asp:CheckBox ID="ckbOnlineShow" runat="server" Checked="true" Visible="false" />

                                        <h6 class="header-inline-display">

                                            <span>جلسه به صورت آنلاین پخش می شود</span>

                                        </h6>

                                    </div>
                                </div>
                            </div>

                            <div runat="server" id="OnlineTeacherPanel" visible="False" class="list-group-item">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <div style="display: inline-block" class="col-md-6">
                                                    <img src="../fonts/worker.png" style="width: 32px" alt="" />
                                                    <h5 class="header-inline-display">نقش استاد مورد نظر را انتخاب نمایید:</h5>
                                                </div>
                                                <div style="display: inline-block" class="col-md-6">
                                                    <asp:DropDownList ID="drpRoleTeacher" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="drpRoleTeacher_OnSelectedIndexChanged">
                                                        <asp:ListItem Text="انتخاب نمایید" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="استاد راهنما" Value="guide"></asp:ListItem>
                                                        <asp:ListItem Text="اساتید مشاور" Value="consultant"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidatorrcbOnlineTeacherRole" ValidationGroup="register" runat="server" Display="Dynamic" ForeColor="red" CssClass="alert" OnServerValidate="CustomValidatorrcbOnlineTeacherRole_OnServerValidate" ErrorMessage="لطفا نقش استاد مورد نظر را انتخاب نمایید"></asp:CustomValidator>
                                                </div>
                                            </div>
                                            <div class=" col-md-6">
                                                <div style="display: inline-block" class="col-md-6">
                                                    <img src="../fonts/boss.png" style="width: 32px" alt="" />
                                                    <h5 class="header-inline-display">نام استاد مورد نظر را انتخاب نمایید:</h5>
                                                </div>
                                                <div style="display: inline-block" class="col-md-6">

                                                    <telerik:RadComboBox RenderMode="Lightweight" CssClass="form-control" ID="rcbOnlineTeacher" runat="server" CheckBoxes="true" Width="240">
                                                        <Localization AllItemsCheckedString="همه موارد" />
                                                    </telerik:RadComboBox>
                                                    <asp:CustomValidator ID="CustomValidatorrcbOnlineTeacher" ValidationGroup="register" runat="server" Display="Dynamic" ForeColor="red" CssClass="alert" OnServerValidate="CustomValidatorrcbOnlineTeacher_OnServerValidate" ErrorMessage="لطفا استاد مورد نظر را انتخاب نمایید">
                                                        
                                                    </asp:CustomValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
        <asp:Panel runat="server" ID="panelDoingDefence">
            <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
                <div class="panel-group">
                    <div class="panel panel panel-primary">
                        <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip3" data-toggle="collapse" data-target="#collapse6">
                            <div class="panel-title">
                                <img src="../fonts/expand.png" style="width: 2%" id="imgIcon6" alt="" />
                                <div class="header-inline-display">برگزاری جلسه دفاع به صورت آنلاین</div>
                            </div>
                        </div>

                        <div id="collapse6" class="panel-collapse collapse">
                            <div class="list-group">
                                <div class="list-group-item">

                                    <img src="../fonts/notepad.png" style="width: 32px" alt="" />
                                    <h5 class="header-inline-display">تعیین نحوه برگزاری جلسه دفاع:</h5>
                                    <br />
                                    <img src="../fonts/back.png" class="sub-img" alt="" />

                                    <asp:CheckBox ID="chkDoingOnlineDefence" runat="server" />


                                    <h6 class="header-inline-display">
                                        <span>اینجانب</span>
                                        <span>
                                            <asp:Literal ID="litStOnline" runat="server"></asp:Literal></span>

                                        <span>متقاضی برگزاری جلسه دفاع به صورت آنلاین هستم.</span>
                                        <span style="color:red">هزینه جلسه دفاع:</span>
                                        <asp:Label runat="server" ID="lblMoney" ForeColor="Red"></asp:Label>
                                    </h6>
                                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:CustomValidator ID="CustomValidatorckbAprroveOnline" ValidationGroup="register" runat="server" CssClass="alert" ErrorMessage="برای تایید حضور استاد به صورت آنلاین باید تیک مربوط را بزنید" ForeColor="red" Display="Dynamic" OnServerValidate="CustomValidatorckbAprroveOnline_OnServerValidate"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>

        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%;display: none !important">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip4" data-toggle="collapse" data-target="#collapse4" title="در صورت نیاز به استفاده از رایانه شخصی بخش زیر را مطالعه فرمایید">
                        <div class="panel-title">
                            <img src="../fonts/expand.png" style="width: 2%" id="imgIcon4" alt="" />
                            <div class="header-inline-display">
                                رایانه مورد استفاده در جلسه دفاع
                            </div>
                        </div>
                    </div>
                    <div id="collapse4" class="panel-collapse collapse">
                        <div class="list-group">
                            <div class="list-group-item">

                                <div class="row">
                                    <div class="col-md-6 ">

                                        <img src="../fonts/computer.png" style="width: 32px" alt="" />
                                        <h5 class="header-inline-display">رایانه مورد استفاده:</h5>

                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <asp:RadioButton ID="rdbUniversitySystem" runat="server" />

                                        <h6 class="header-inline-display">از رایانه موجود در جلسه دفاع استفاده می نمایم (توصیه می شود)</h6>

                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <asp:RadioButton ID="rdbOwnSystem" runat="server" />

                                        <h6 class="header-inline-display">از رایانه شخصی خود استفاده می نمایم(مسئولیت عملکرد صحیح سیستم بر عهده خود دانشجو می باشد)</h6>


                                    </div>
                                </div>

                            </div>
                            <div class="list-group-item">

                                <img src="../fonts/megaphone.png" style="width: 32px" alt="" />
                                <h5 class="header-inline-display">در صورت استفاده از رایانه شخصی، سیستم شما باید دارای شرایط  زیر باشد:</h5>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">دارای خروجی VGA 
                                </h6>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">دارای مرورگر(ترجیحا FireFox)
                                </h6>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">دارای Flash Player همان مرورگر
                                </h6>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">دارای نرم افزار Adobe Connect
                                </h6>




                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%; float: left;">
            <asp:Button ID="btnRegister" runat="server" Text="ویرایش و ثبت درخواست" CssClass="btn btn-primary" OnClick="btnRegister_OnClick" ValidationGroup="register" />
            <asp:Button ID="btnBack" runat="server" Text="انصراف" CssClass="btn btn-danger" OnClick="btnBack_OnClick" />
        </div>


    </div>
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

    <script>

        $(document).ready(function () {



            $(".collapse").collapse('show');
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate',
                { extraInputID: 'ContentPlaceHolder1_txtDate', extraInputFormat: 'yyyy/mm/dd' });



        });
    </script>
</asp:Content>
