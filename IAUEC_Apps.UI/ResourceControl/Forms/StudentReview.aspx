<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/ResourceControlDefenceStudent.Master" AutoEventWireup="true" CodeBehind="StudentReview.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.StudentReview" %>

<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="IAUEC_Apps.UI.University.GraduateAffair.CMS" %>

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

            $('#ContentPlaceHolder1_ckbAprroveOnline').change(
                function () {

                    $('#OnlineTeacherPanel').toggle();
                });
        });

    </script>

    <script type="text/javascript">
        function onLoadRadTimePicker1(sender, args) {
            txtTime = sender;
        }

        function confirmAspButton(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 250, 100, null, "Confirm");
        }
    </script>
    <style>
        .circles {
            margin-bottom: -10px;
        }

        .circle {
            width: 100px;
            display: inline-block;
            position: relative;
            text-align: center;
            line-height: 1.2;
        }

            .circle canvas {
                vertical-align: top;
            }

            .circle strong {
                position: absolute;
                top: 30px;
                left: 0;
                width: 100%;
                text-align: center;
                line-height: 40px;
                font-size: 30px;
            }

                .circle strong i {
                    font-style: normal;
                    font-size: 0.6em;
                    font-weight: normal;
                }

            .circle span {
                display: block;
                color: #aaa;
                margin-top: 12px;
            }

        img {
            margin-left: 2px;
        }

        #myProgress {
            width: 100%;
            background-color: #dedede;
            border-radius: 5px;
            /*border: 1px solid;*/
        }

        #myBar {
            width: 1%;
            height: 20px;
            background-color: green;
            border-radius: 5px;
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
    <link href="../Style/bootstrap-rtl.min.css" rel="stylesheet" />
    <link href="../Style/StudentStyle.css" rel="stylesheet" />
    <style>
        .tableBorder {
            border: 2px solid whitesmoke;
        }

            .tableBorder th {
                border: 1px solid whitesmoke;
            }

        .backColortable {
            background-color: #1A82C3;
        }

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

        .radWindow {
            text-align: center;
        }
    </style>
    <script>

        $(document).ready(function () {

            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate',
                { extraInputID: 'ContentPlaceHolder1_txtDate', extraInputFormat: 'yyyy/mm/dd' });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" CssClass="radWindow">
    </telerik:RadWindowManager>

    <h3>کارتابل دانشجو</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:HiddenField ID="LblLastDate" runat="server"  ></asp:HiddenField>
        <asp:HiddenField ID="LblLastTime" runat="server"  ></asp:HiddenField>
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip1" data-toggle="collapse" data-target="#collapse1" title="در این بخش می توانید از امکان مشاهده پیشرفت درخواست در حال گردش، اطلاعات درخواست ،بایگانی درخواست های گذشته و ویرایش و حذف درخواست استفاده فرمایید.">
                        <div class="panel-title">
                            <img src="../fonts/expand.png" style="width: 2%" id="imgIcon1" alt="" />
                            <div class="header-inline-display">بررسی پیشرفت درخواست</div>
                        </div>
                    </div>
                    <div id="collapse1" class="panel-collapse collapse">
                        <div class="list-group">
                            <div class="list-group-item">

                                <div class="row">
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading  bg-red">
                                                <img src="../fonts/presentation.png" style="width: 32px" alt="" />
                                                <h5 class="header-inline-display">اطلاعات درخواست</h5>
                                            </div>
                                            <div class="panel-body">

                                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                                <h5 class="header-inline-display">
                                                    <span style="margin-left: 5px;">شماره درخواست: </span>
                                                    <span>
                                                        <asp:Literal ID="litReqId" runat="server"></asp:Literal>
                                                    </span>


                                                </h5>
                                                <br />
                                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                                <h5 class="header-inline-display">
                                                    <span style="margin-left: 5px;">تاریخ ثبت درخواست: </span>
                                                    <span>
                                                        <asp:Literal ID="litReqRegisterDate" runat="server"></asp:Literal>
                                                    </span>


                                                </h5>

                                                <br />
                                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                                <h5 class="header-inline-display">
                                                    <span style="margin-left: 5px;">تاریخ جلسه دفاع: </span>
                                                    <span>
                                                        <asp:Literal ID="litReqDenfenceDate" runat="server"></asp:Literal>
                                                    </span>


                                                </h5>
                                                <br />
                                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                                <h5 class="header-inline-display">
                                                    <span style="margin-left: 5px;">ساعت جلسه دفاع: </span>
                                                    <span>
                                                        <asp:Literal ID="litDefenceTime" runat="server"></asp:Literal>
                                                    </span>


                                                </h5>
                                                <br />
                                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                                <h5 class="header-inline-display">
                                                    <span style="margin-left: 5px;">موبایل: </span>
                                                    <span>
                                                        <asp:Literal ID="litStudentMobile" runat="server"></asp:Literal>
                                                    </span>


                                                </h5>
                                                <br />
                                            </div>

                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdnRequestId" runat="server" />
                                    <asp:HiddenField ID="hdnStatus" runat="server" />
                                    <div class="col-md-8" style="text-align: center;">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading  bg-red">
                                                <img src="../fonts/notepad (1).png" style="width: 32px" alt="" />
                                                <h5  class="header-inline-display">فرآیند بررسی درخواست</h5>
                                            </div>
                                            <div class="panel-body row" style="margin-top: 3.5%;">

                                                <div class="col-md-2">
                                                    <img id="stateOne" src="../fonts/more-circular-symbol.png" style="width: 32px" alt="" />
                                                    ثبت درخواست
                                                </div>


                                                <div class="col-md-2">
                                                    <img id="stateTwo" src="../fonts/more-circular-symbol.png" style="width: 32px" alt="" />
                                                    تایید دانشکده
                                                </div>
                                                                                                <div class="col-md-3">
                                                    <img id="stateThree" src="../fonts/more-circular-symbol.png" style="width: 32px" alt="" />
                                                    تایید مالی
                                                </div>
                                                <div class="col-md-2">
                                                    <img id="stateFour" src="../fonts/more-circular-symbol.png" style="width: 32px" alt="" />
                                                    تایید فنی
                                                </div>
                                                <div class="col-md-2">
                                                    <img id="stateFive" src="../fonts/more-circular-symbol.png" style="width: 32px" alt="" />
                                                    تایید اداری
                                                </div>


                                            </div>
                                            <div class="panel-footer">
                                                <div class="alert <%=AlertText %>" style="text-align: center; margin-top: 2.5%">
                                                    <h5>
                                                        <%=NextStatusText%>
                                                    </h5>

                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="list-group-item">
                                <div class="row">

                                    <div class="alert alert-warning col-md-12">
                                        <img src="../fonts/megaphone.png" style="width: 32px" alt="" />
                                        <h4 class="header-inline-display">تذکر :</h4>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <h5 class="header-inline-display">اگر مایل به اعمال تغییرات و یا حذف درخواست خود هستید، میتوانید از بخش 
                                           
                                            <a href="#OpPanle" onclick="document.getElementById('editPanle').click();" style="color: #5a738e">"عملیات مرتبط با درخواست" </a>
                                            استفاده نمایید
                                        </h5>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <h5 class="header-inline-display">اگر مایل به مشاهده درخواست های گذشته خود هستید، میتوانید از بخش
                                            
                                             <a href="#OpPanle" onclick="document.getElementById('historyPanle').click();" style="color: #5a738e">"سوابق درخواست" </a>

                                            خود استفاده نمایید.
                                        </h5>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <h5 class="header-inline-display">
                                            <span>برای پیگیری درخواست خود میتوانید با همراه داشتن شماره درخواست</span>

                                            <span>به بخش مربوطه تیکت بزنید.</span>

                                        </h5>

                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <h5 class="header-inline-display">تا قبل از تایید دانشکده تنها یک بار امکان ویرایش برای شما وجود دارد.
                                        </h5>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <h5 class="header-inline-display">تا قبل از تایید دانشکده امکان حذف درخواست برای شما وجود دارد.
                                        </h5>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <h5 class="header-inline-display">در صورتی که بعد از تایید دانشکده درخواست ویرایش یا حذف دارید می توانید به دانشکده خود تیکت بزنید.
                                        </h5>

                                    </div>

                                </div>


                            </div>
                            <div class="list-group-item">
                                <img src="../fonts/oprator.png" style="width: 32px; margin-right: 10px;" alt="" />
                                <h5 class="header-inline-display">عملیات مرتبط با درخواست:</h5>
                                <div id="OpPanle" class="row bg-green" style="border-radius: 5px;">

                                    <!-- Nav tabs -->
                                    <ul class="nav nav-tabs" role="tablist" style="margin-right: 1%;">
                                        <li style="text-align: right" role="presentation" class="active">
                                            <a href="#profile" id="editPanle" aria-controls="profile" role="tab" data-toggle="tab">ویرایش درخواست</a>
                                        </li>
                                        <li style="text-align: right" role="presentation">
                                            <a href="#home" aria-controls="home" role="tab" data-toggle="tab">حذف درخواست</a>
                                        </li>
                                        <li role="presentation">
                                            <a href="#messages" id="historyPanle" aria-controls="messages" role="tab" data-toggle="tab">سوابق درخواست </a>
                                        </li>

                                    </ul>

                                    <!-- Tab panes -->
                                    <div class="tab-content col-md-12">
                                        <div role="tabpanel" class="tab-pane fade" id="home">
                                            <div style="margin-top: 2%; margin-bottom: 2%">
                                                <h5 class="header-inline-display">
                                                    <span>در صورت درخواست حذف رزرو جلسه دفاع روی دکمه</span>
                                                    <u>درخواست حذف</u>
                                                    <span>کلیک نمایید.</span>
                                                </h5>
                                                <asp:Button ID="deleteRequest" runat="server" Text="حذف درخواست" CssClass="btn btn-danger" OnClick="deleteRequest_OnServerClick" OnClientClick="confirmAspButton(this); return false;" />
                                            </div>

                                        </div>
                                        <div role="tabpanel" class="tab-pane fade in active" id="profile">
                                            <br />
                                            <div class="container" style="color: #5A738E">


                                                <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
                                                    <div class="panel-group">
                                                        <div class="panel panel panel-primary">
                                                            <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip2" data-toggle="collapse" data-target="#collapse2" title="در صورت نیاز زمان مورد درخواست خود را برای جلسه دفاع ویرایش نمایید.">
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
                                                                            <h5 class="header-inline-display">برنامه اساتید مرتبط با دفاع شما:</h5>
                                                                            <br />
                                                                            <div class="container" style="text-align: center">
                                                                                <div class="row" dir="rtl" style="margin-left: 1%; margin-right: 1%;">
                                                                                    <br />
                                                                                    <div class="alert alert-info">
                                                                                        دانشجوي محترم بدينوسيله به استحضار مي­رساند اركان دفاع (شامل اساتيد راهنما، مشاور و داور يا داوران) مطابق برنامه زمانبندي ذيل جهت برگزاري دفاع ساير دانشجويان در دانشگاه حاضر خواهند بود.
                                                                                    </div>

                                                                                    <asp:GridView runat="server" ID="grdAttendanceProfessores" AutoGenerateColumns="False"
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
                                                            <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip3" data-toggle="collapse" data-target="#collapse3" title="در صورت نیاز به ویرایش شرکت استاد به صورت آنلاین می توانیداز بخش زیر استفاده نمایید.">
                                                                <div class="panel-title">
                                                                    <img src="../fonts/expand.png" style="width: 2%" id="imgIcon3" alt="" />
                                                                    <div class="header-inline-display">ویرایش درخواست حضور استاد به صورت آنلاین در جلسه دفاع(تنها یکی از اساتید راهنما و مشاور می توانند آنلاین باشند)</div>
                                                                </div>
                                                            </div>
                                                            <div id="collapse3" class="panel-collapse collapse ">
                                                                <div class="list-group">
                                                                    <div class="list-group-item">

                                                                        <img src="../fonts/notepad.png" style="width: 32px" alt="" />
                                                                        <h5 class="header-inline-display">تعیین استاد آنلاین:</h5>
                                                                        <br />
                                                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                                                        <asp:CheckBox ID="ckbAprroveOnline" runat="server" />
                                                                        <h6 class="header-inline-display">

                                                                            <span>متقاضی حضور استاد به صورت آنلاین هستم.</span>

                                                                        </h6>


                                                                    </div>

                                                                    <div id="OnlineTeacherPanel" class="list-group-item">


                                                                        <div class="row">

                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                <ContentTemplate>

                                                                                    <div class="col-md-6 ">
                                                                                        <div style="display: inline-block" class="col-md-7">
                                                                                            <img src="../fonts/worker.png" style="width: 32px" alt="" />
                                                                                            <h5 class="header-inline-display">نقش استاد مورد نظر را انتخاب نمایید:</h5>
                                                                                        </div>
                                                                                        <div style="display: inline-block" class="col-md-5">
                                                                                            <asp:DropDownList ID="drpRoleTeacher" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="drpRoleTeacher_OnSelectedIndexChanged">
                                                                                                <asp:ListItem Text="انتخاب نمایید" Value=""></asp:ListItem>
                                                                                                <asp:ListItem Text="استاد راهنما" Value="guide"></asp:ListItem>
                                                                                                <asp:ListItem Text="اساتید مشاور" Value="consultant"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <asp:CustomValidator ID="CustomValidatorrcbOnlineTeacherRole" ValidationGroup="register" runat="server" Display="Dynamic" ForeColor="red" CssClass="alert" OnServerValidate="CustomValidatorrcbOnlineTeacherRole_OnServerValidate" ErrorMessage="لطفا نقش استاد مورد نظر را انتخاب نمایید"></asp:CustomValidator>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class=" col-md-6">
                                                                                        <div style="display: inline-block" class="col-md-7">
                                                                                            <img src="../fonts/boss.png" style="width: 32px" alt="" />
                                                                                            <h5 class="header-inline-display">نام استاد مورد نظر را انتخاب نمایید:</h5>
                                                                                        </div>
                                                                                        <div style="display: inline-block" class="col-md-5">


                                                                                            <telerik:RadComboBox RenderMode="Lightweight" CssClass="form-control" ID="rcbOnlineTeacher" runat="server" CheckBoxes="true" Width="180">
                                                                                                <Localization AllItemsCheckedString="همه موارد" />
                                                                                            </telerik:RadComboBox>
                                                                                            <asp:CustomValidator ID="CustomValidatorrcbOnlineTeacher" ValidationGroup="register" runat="server" Display="Dynamic" ForeColor="red" CssClass="alert" OnServerValidate="CustomValidatorrcbOnlineTeacher_OnServerValidate" ErrorMessage="لطفا استاد مورد نظر را انتخاب نمایید"></asp:CustomValidator>
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

                                                </div>


                                                <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%; display: none !important">
                                                    <div class="panel-group">
                                                        <div class="panel panel panel-primary">
                                                            <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip4" data-toggle="collapse" data-target="#collapse4" title="در صورت نیاز به تغییر رایانه مورد استفاده در جلسه دفاع می توانید از بخش زیر استفاده نمایید.">
                                                                <div class="panel-title">
                                                                    <img src="../fonts/expand.png" style="width: 2%" id="imgIcon4" alt="" />
                                                                    <div class="header-inline-display">
                                                                        تغییر رایانه مورد استفاده در جلسه دفاع
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="collapse4" class="panel-collapse collapse">
                                                                <div class="list-group">
                                                                    <div class="list-group-item">

                                                                        <div class="row">
                                                                            <div class="col-md-12 ">

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

                                                                            <asp:CheckBox ID="chkDoingOnlineDefence"  runat="server" />


                                                                            <h6 class="header-inline-display">
                                                                                <span>اینجانب</span>
                                                                                <span>
                                                                                    <asp:Literal ID="litStOnline"  runat="server"></asp:Literal></span>

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
                                                <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%; float: left;">
                                                    <asp:Button ID="btnRegister" runat="server" Text="ویرایش درخواست" CssClass="btn btn-primary" OnClick="btnRegister_OnClick" ValidationGroup="register" />
                                                    <asp:Button ID="btnBack" runat="server" Text="انصراف" CssClass="btn btn-danger" OnClick="btnBack_OnClick" />
                                                </div>

                                            </div>

                                        </div>
                                        <div role="tabpanel" class="tab-pane" id="messages">
                                            <br />
                                            <div class="container" style="text-align: center">
                                                <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
                                                    <asp:GridView ID="grdLastRequest" runat="server" AutoGenerateColumns="False" CssClass="table table-responsive table-stripted backColortable">
                                                        <HeaderStyle CssClass="tableBorder"></HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="شماره درخواست">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server"
                                                                        Text='<%#Eval("ID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="تاریخ ثبت">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblissue_time" runat="server"
                                                                        Text='<%#Eval("issue_time")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="تاریخ دفاع">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRequestDate" runat="server"
                                                                        Text='<%#Eval("RequestDate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="وضعیت">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRequestState" runat="server"
                                                                        Text='<%# Convert.ToBoolean(Eval("isDeleted")) || Eval("RequestDate").ToString().StringPersianDateToGerogorianDate() < DateTime.Now ? "غیرفعال":"فعال" %>' CssClass="btn" Width="100"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
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

        </div>




    </div>




    <!-- Modal -->
    <div class="modal fade modal-lg" id="myModal" role="dialog" style="background-color: rgba(4, 21, 27, 0.6);">
        <div class="modal-dialog" style="width: 90%; margin-top: 19%;">

            <!-- Modal content-->
            <div class="modal-content" style="background-color: #820000;">
                <div class="modal-header">
                    <img src="../fonts/megaphone.png" style="width: 32px" alt="" />
                    <h1 class="modal-title header-inline-display">پیام سیستم</h1>
                </div>
                <div class="modal-body">
                    
                    <asp:Panel ID="PanelColege" runat="server">
                             <img src="../fonts/back.png" class="sub-img" alt="" />
                            <h3 class="header-inline-display">درخواست شما به علت
                                <u>
                                    <asp:Literal ID="litIsDeleted" runat="server"></asp:Literal>

                                </u>
                                توسط دانشکده لغو گردید.
                            </h3>
                    </asp:Panel>
                    <asp:Panel ID="PanelTeacher" runat="server">
                             <img src="../fonts/back.png" class="sub-img" alt="" />
                            <h3 class="header-inline-display">درخواست شما توسط استاد 
                          <u>
                                    <asp:Literal ID="litostad" runat="server"></asp:Literal>

                                </u>
                                 لغو گردید.
                            </h3>
                    </asp:Panel>
                    <br />
                    <img src="../fonts/back.png" class="sub-img" alt="" />
                    <h3 class="header-inline-display">در صورت نیاز می توانید از طریق دکمه 
                        <u>ثبت درخواست جدید</u>
                        ، درخواست جدیدی را ثبت نمایید.
                    </h3>
                    <br />
                </div>
                <div class="modal-footer">
                    <button type="button" runat="server" id="redirectToNew" onserverclick="redirectToNew_OnServerClick" class="btn btn-warning" style="margin-bottom: initial;" data-dismiss="modal">ثبت درخواست جدید</button>

                    <button type="button" runat="server" id="redirectToBack" onserverclick="redirectToBack_OnServerClick" class="btn btn-success" style="margin-bottom: initial;" data-dismiss="modal">بازگشت</button>

                </div>
            </div>

        </div>
    </div>
    <div class="modal fade modal-lg" id="myModal1" role="dialog" style="background-color: rgba(4, 21, 27, 0.6);">
        <div class="modal-dialog" style="width: 90%; margin-top: 19%;">

            <!-- Modal content-->
            <div class="modal-content" style="background-color: #820000;">
                <div class="modal-header">
                    <img src="../fonts/megaphone.png" style="width: 32px" alt="" />
                    <h1 class="modal-title header-inline-display">پیام سیستم</h1>
                </div>
                <div class="modal-body">
                    <img src="../fonts/back.png" class="sub-img" alt="" />
                    <h3 class="header-inline-display">درخواست شما به علت
                        <u>گذشت زمان درخواستی از زمان کنونی

                        </u>
                        لغو گردید.
                    </h3>
                    <br />
                    <img src="../fonts/back.png" class="sub-img" alt="" />
                    <h3 class="header-inline-display">در صورت نیاز می توانید از طریق دکمه 
                        <u>ثبت درخواست جدید</u>
                        ، درخواست جدیدی را ثبت نمایید.
                    </h3>
                    <br />
                </div>
                <div class="modal-footer">
                    <button type="button" runat="server" id="redirectToNew1" onserverclick="redirectToNew_OnServerClick" class="btn btn-warning" style="margin-bottom: initial;" data-dismiss="modal">ثبت درخواست جدید</button>

                    <button type="button" runat="server" id="redirectToBack1" onserverclick="redirectToBack_OnServerClick" class="btn btn-success" style="margin-bottom: initial;" data-dismiss="modal">بازگشت</button>

                </div>
            </div>

        </div>
    </div>








    <script src="../Scripts/jquery3.min.js"></script>
    <script src="../Scripts/circle-progress.min.js"></script>
    <script>

        $(document).ready(function () {



            $('#myTabs a').click(function (e) {
                e.preventDefault();
                $(this).tab('show');
            });

            $(".collapse:first").collapse('show');


            function runAnimateStateOne() {
                var timemiliSec = 400;
                $('#stateOne').fadeOut(800, function () {
                    $('#stateOne').attr("src", "../fonts/stopwatch-1.png");
                    $('#stateOne').fadeIn(timemiliSec, function () {
                        $('#stateOne').fadeOut(timemiliSec, function () {
                            $('#stateOne').attr("src", "../fonts/success.png");
                            $('#stateOne').fadeIn(timemiliSec);
                        });
                    });
                });
            }

            function runAnimateStateTwo() {
                var timemiliSec = 400;
                $('#stateOne').fadeOut(800, function () {
                    $('#stateOne').attr("src", "../fonts/stopwatch-1.png");
                    $('#stateOne').fadeIn(timemiliSec, function () {
                        $('#stateOne').fadeOut(timemiliSec, function () {
                            $('#stateOne').attr("src", "../fonts/success.png");
                            $('#stateOne').fadeIn(timemiliSec, function () {
                                $('#stateTwo').fadeOut(timemiliSec, function () {
                                    $('#stateTwo').attr("src", "../fonts/stopwatch-1.png");
                                    $('#stateTwo').fadeIn(timemiliSec);
                                });
                            });
                        });
                    });
                });
            }

            function runAnimateStateThree() {
                var timemiliSec = 400;
                $('#stateOne').fadeOut(800, function () {
                    $('#stateOne').attr("src", "../fonts/stopwatch-1.png");
                    $('#stateOne').fadeIn(timemiliSec, function () {
                        $('#stateOne').fadeOut(timemiliSec, function () {
                            $('#stateOne').attr("src", "../fonts/success.png");
                            $('#stateOne').fadeIn(timemiliSec, function () {
                                $('#stateTwo').fadeOut(timemiliSec, function () {
                                    $('#stateTwo').attr("src", "../fonts/stopwatch-1.png");
                                    $('#stateTwo').fadeIn(timemiliSec, function () {
                                        $('#stateTwo').fadeOut(timemiliSec, function () {
                                            $('#stateTwo').attr("src", "../fonts/success.png");
                                            $('#stateTwo').fadeIn(timemiliSec, function () {
                                                $('#stateThree').fadeOut(timemiliSec, function () {
                                                    $('#stateThree').attr("src", "../fonts/stopwatch-1.png");
                                                    $('#stateThree').fadeIn(timemiliSec);
                                                });
                                            });
                                        });

                                    });
                                });
                            });
                        });
                    });
                });

            }

            function runAnimateStateFour() {
                var timemiliSec = 400;
                $('#stateOne').fadeOut(800, function () {
                    $('#stateOne').attr("src", "../fonts/stopwatch-1.png");
                    $('#stateOne').fadeIn(timemiliSec, function () {
                        $('#stateOne').fadeOut(timemiliSec, function () {
                            $('#stateOne').attr("src", "../fonts/success.png");
                            $('#stateOne').fadeIn(timemiliSec, function () {
                                $('#stateTwo').fadeOut(timemiliSec, function () {
                                    $('#stateTwo').attr("src", "../fonts/stopwatch-1.png");
                                    $('#stateTwo').fadeIn(timemiliSec, function () {
                                        $('#stateTwo').fadeOut(timemiliSec, function () {
                                            $('#stateTwo').attr("src", "../fonts/success.png");
                                            $('#stateTwo').fadeIn(timemiliSec, function () {
                                                $('#stateThree').fadeOut(timemiliSec, function () {
                                                    $('#stateThree').attr("src", "../fonts/stopwatch-1.png");
                                                    $('#stateThree').fadeIn(timemiliSec, function () {
                                                        $('#stateThree').fadeOut(timemiliSec, function () {
                                                            $('#stateThree').attr("src", "../fonts/success.png");
                                                            $('#stateThree').fadeIn(timemiliSec, function () {
                                                                $('#stateFour').fadeOut(timemiliSec, function () {
                                                                    $('#stateFour').attr("src", "../fonts/stopwatch-1.png");
                                                                    $('#stateFour').fadeIn(timemiliSec);
                                                                });
                                                            });
                                                        });
                                                    });
                                                });
                                            });
                                        });

                                    });
                                });
                            });
                        });
                    });
                });
            }

            function runAnimateStateFive() {
                var timemiliSec = 400;
                $('#stateOne').fadeOut(800, function () {
                    $('#stateOne').attr("src", "../fonts/stopwatch-1.png");
                    $('#stateOne').fadeIn(timemiliSec, function () {
                        $('#stateOne').fadeOut(timemiliSec, function () {
                            $('#stateOne').attr("src", "../fonts/success.png");
                            $('#stateOne').fadeIn(timemiliSec, function () {
                                $('#stateTwo').fadeOut(timemiliSec, function () {
                                    $('#stateTwo').attr("src", "../fonts/stopwatch-1.png");
                                    $('#stateTwo').fadeIn(timemiliSec, function () {
                                        $('#stateTwo').fadeOut(timemiliSec, function () {
                                            $('#stateTwo').attr("src", "../fonts/success.png");
                                            $('#stateTwo').fadeIn(timemiliSec, function () {
                                                $('#stateThree').fadeOut(timemiliSec, function () {
                                                    $('#stateThree').attr("src", "../fonts/stopwatch-1.png");
                                                    $('#stateThree').fadeIn(timemiliSec, function () {
                                                        $('#stateThree').fadeOut(timemiliSec, function () {
                                                            $('#stateThree').attr("src", "../fonts/success.png");
                                                            $('#stateThree').fadeIn(timemiliSec, function () {
                                                                $('#stateFour').fadeOut(timemiliSec, function () {
                                                                    $('#stateFour').attr("src", "../fonts/stopwatch-1.png");
                                                                    $('#stateFour').fadeIn(timemiliSec, function () {

                                                                        $('#stateFour').fadeOut(timemiliSec, function () {
                                                                            $('#stateFour').attr("src", "../fonts/success.png");
                                                                            $('#stateFour').fadeIn(timemiliSec, function () {
                                                                                $('#stateFive').attr("src", "../fonts/stopwatch-1.png");
                                                                                $('#stateFive').fadeIn(timemiliSec);
                                                                            });
                                                                              
                                                                        });
                                                                    });
                                                                });
                                                            });
                                                        });
                                                    });
                                                });
                                            });
                                        });

                                    });
                                });
                            });
                        });
                    });
                });
            }
            function runAnimateStateSix() {
                var timemiliSec = 400;
                $('#stateOne').fadeOut(800, function () {
                    $('#stateOne').attr("src", "../fonts/stopwatch-1.png");
                    $('#stateOne').fadeIn(timemiliSec, function () {
                        $('#stateOne').fadeOut(timemiliSec, function () {
                            $('#stateOne').attr("src", "../fonts/success.png");
                            $('#stateOne').fadeIn(timemiliSec, function () {
                                $('#stateTwo').fadeOut(timemiliSec, function () {
                                    $('#stateTwo').attr("src", "../fonts/stopwatch-1.png");
                                    $('#stateTwo').fadeIn(timemiliSec, function () {
                                        $('#stateTwo').fadeOut(timemiliSec, function () {
                                            $('#stateTwo').attr("src", "../fonts/success.png");
                                            $('#stateTwo').fadeIn(timemiliSec, function () {
                                                $('#stateThree').fadeOut(timemiliSec, function () {
                                                    $('#stateThree').attr("src", "../fonts/stopwatch-1.png");
                                                    $('#stateThree').fadeIn(timemiliSec, function () {
                                                        $('#stateThree').fadeOut(timemiliSec, function () {
                                                            $('#stateThree').attr("src", "../fonts/success.png");
                                                            $('#stateThree').fadeIn(timemiliSec, function () {
                                                                $('#stateFour').fadeOut(timemiliSec, function () {
                                                                    $('#stateFour').attr("src", "../fonts/stopwatch-1.png");
                                                                    $('#stateFour').fadeIn(timemiliSec, function () {

                                                                        $('#stateFour').fadeOut(timemiliSec, function () {
                                                                            $('#stateFour').attr("src", "../fonts/success.png");
                                                                            $('#stateFour').fadeIn(timemiliSec, function () {
                                                                                $('#stateFive').attr("src", "../fonts/stopwatch-1.png");
                                                                                $('#stateFive').fadeIn(timemiliSec, function () {
                                                                                  
                                                                                    $('#stateFive').fadeOut(timemiliSec, function () {
                                                                                        $('#stateFive').attr("src", "../fonts/success.png");
                                                                                        $('#stateFive').fadeIn(timemiliSec);
                                                                                    });
                                                                                });
                                                                            });

                                                                        });
                                                                    });
                                                                });
                                                            });
                                                        });
                                                    });
                                                });
                                            });
                                        });

                                    });
                                });
                            });
                        });
                    });
                });
            }


            var isTechnical = '<%=Istechnical%>';

            switch (parseInt('<%=StatusNumber%>')) {
                case 0:
                case 3:
                    runAnimateStateTwo();

                    break;
                case  9: 
                    runAnimateStateThree();

                    break;
                case 1:

                    if (isTechnical.toLowerCase() === 'true')

                        runAnimateStateFour();
                    else
                        runAnimateStateFive();

                    break;
                case 6:
                    runAnimateStateFive();
                    break;
                case 2:
                    runAnimateStateSix();
                    break;
                default:
                    break;
            }

            debugger;

            var isCheckedTeacher = '<%=IsNotOnline%>';
            if (isCheckedTeacher == 'True')
                $('#OnlineTeacherPanel').hide();
            else
                $('#OnlineTeacherPanel').show();


            $("span[id*='ContentPlaceHolder1_grdLastRequest_lblRequestState']").each(function () {

                if (this.innerText == 'غیرفعال') {

                    $(this).addClass("btn-danger");
                } else {

                    $(this).addClass("btn-success");
                }

            });


<%--            var isChecked = '<%= IsNotOnline%>';
            if (isChecked ==='True') {
                $('#OnlineTeacherPanel').hide();
            }--%>


            var rdbOwnSystem = document.getElementById('ContentPlaceHolder1_rdbOwnSystem');
            if (rdbOwnSystem.checked == false) {
                $('#ContentPlaceHolder1_rdbUniversitySystem').prop("checked", true);
            } else {
                $('#ContentPlaceHolder1_rdbUniversitySystem').prop("checked", false);
            }




            var isDelted = '<%=IsDeleted%>';
            if (isDelted === 'True') {
                $('#myModal').modal({ backdrop: 'static', keyboard: false });

                $('#myModal').modal('show');
            }
            var isWaste = '<%=IsWaste %>';

            if (isWaste === 'True' && isDelted !== 'True') {
                $('#myModal1').modal({ backdrop: 'static', keyboard: false });

                $('#myModal1').modal('show');
            }
        });



    </script>




</asp:Content>

