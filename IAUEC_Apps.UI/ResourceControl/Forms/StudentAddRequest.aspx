<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/ResourceControlDefenceStudent.Master" AutoEventWireup="true" CodeBehind="StudentAddRequest.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.StudentAddRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../Style/pretty-checkbox.min.css" rel="stylesheet" />


    <link href="../Style/StudentStyle.css" rel="stylesheet" />
    <link href="../Content/style.css" rel="stylesheet" />
    <script>

        $(document).ready(function () {

            function ValidateCheckBox(sender, args) {
                debugger;
                if (document.getElementById("<%=chkCommitment.ClientID %>").checked == true) {
                    args.IsValid = true;
                    $("#ContentPlaceHolder1_CustomValidator1").css("visibility", "visible");
                } else {
                    args.IsValid = false;
                    $("#ContentPlaceHolder1_CustomValidator1").css("visibility", "hidden");
                }
            }

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

    <h3>ثبت درخواست رزرو جلسه دفاع</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

    <hr />

    <div class="container">

        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip1" data-toggle="collapse" data-target="#collapse1" title="اطلاعات دفاع خود را مشاهده فرمایید">
                        <div class="panel-title">
                            <img src="../fonts/expand.png" style="width: 2%" id="imgIcon1" alt="" />
                            <div class="header-inline-display">اطلاعات جلسه دفاع شما</div>
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
                                <div class="row">
                                    <div class="col-md-6">
                                        <img src="../fonts/lecture.png" style="width: 32px" alt="" />
                                        <h5 class="header-inline-display">استاد راهنما:</h5>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <h6 class="header-inline-display">
                                            <asp:Literal ID="litGuide" runat="server"></asp:Literal>

                                        </h6>
                                    </div>
                                    <div class="col-md-6">
                                        <img src="../fonts/conference.png" style="width: 32px" alt="" />
                                        <h5 class="header-inline-display">استاد مشاور:</h5>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <h6 class="header-inline-display">
                                            <asp:Literal ID="litConsultent" runat="server"></asp:Literal>
                                        </h6>
                                    </div>
                                </div>

                            </div>
                            <div class="list-group-item">
                                <div class="row">
                                    <div class="col-md-6">
                                        <img src="../fonts/law.png" style="width: 32px" alt="" />
                                        <h5 class="header-inline-display">داور:</h5>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <h6 class="header-inline-display">
                                            <asp:Literal ID="litReferee" runat="server"></asp:Literal>
                                        </h6>
                                    </div>
                                    <div class="col-md-6">
                                        <img src="../fonts/calprop.png" style="width: 32px" alt="" />
                                        <h5 class="header-inline-display">تاریخ تصویب پروپوزال:</h5>
                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <h6 class="header-inline-display">
                                            <asp:Literal ID="litAprrovPropDate" runat="server"></asp:Literal>
                                        </h6>
                                    </div>
                                </div>

                            </div>
                            <div class="list-group-item">
                                <img src="../fonts/smartphone.png" style="width: 32px" alt="" />
                                <h5 class="header-inline-display">تلفن همراه:</h5>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">
                                    <span>دانشجوی گرامی اطلاع رسانی درخصوص درخواست رزرو جلسه دفاع از طریق شماره </span>
                                    <u class="lead">
                                        <asp:Literal ID="litMobile" runat="server"></asp:Literal>
                                    </u>
                                    <span>صورت می گیرد.لذا در صورت مشاهده مغایرت در شماره تماس ثبت شده</span>
                                    <span>بر روی 

                                    </span>
                                    <span>ویرایش 
                                    </span>
                                    <span>کلیک نمایید.
                                    </span>
                                </h6>
                                <div style="display: inline-block;">
                                    <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <asp:UpdateProgress ID="updateProgress9" runat="server">
                                                <ProgressTemplate>
                                                    <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                                        <asp:Image ID="imgUpdateProgress9" runat="server" ImageUrl="../Images/loading-svg-spinning-4.gif" AlternateText="درحال بارگزاری ..." ToolTip="درحال بارگزاری ..." Style="padding: 10px; position: fixed; top: 28%; left: 38%;" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <button runat="server" id="btnRedirectToEditInfo" onserverclick="btnRedirectToEditInfo_OnServerClick" class="btn btn-warning">ویرایش</button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip2" data-toggle="collapse" data-target="#collapse2" title="زمان مورد درخواست خود را برای جلسه دفاع ثبت نمایید">
                        <div class="panel-title">
                            <img src="../fonts/expand.png" style="width: 2%" id="imgIcon2" alt="" />
                            <div class="header-inline-display">ثبت زمان درخواست رزرو جلسه دفاع</div>
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
                                <hr style="margin-top: -4px;" />
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

                                        <telerik:RadTimePicker ID="txtTime" EnableTyping="false" RenderMode="Lightweight" runat="server" Culture="fa-IR" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" CssClass="pdate" TimeView-Interval="00:30:00" TimeView-Columns="2">
                                            <%--<TimeView ID="TimeView4" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="2" StartTime="09:00" EndTime="19:00">
                                            </TimeView>--%>
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

                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:CustomValidator ID="CustomValidatorckbAprroveOnline" ValidationGroup="register" runat="server" CssClass="alert" ErrorMessage="برای تایید حضور استاد به صورت آنلاین باید تیک مربوط را بزنید" ForeColor="red" Display="Dynamic" OnServerValidate="CustomValidatorckbAprroveOnline_OnServerValidate"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>

                            <div runat="server" id="OnlineTeacherPanel" visible="False" class="list-group-item">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdateProgress ID="updateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                                    <asp:Image ID="imgUpdateProgress2" runat="server" ImageUrl="../Images/loading-svg-spinning-4.gif" AlternateText="درحال بارگزاری ..." ToolTip="درحال بارگزاری ..." Style="padding: 10px; position: fixed; top: 28%; left: 38%;" />
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
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
                                                    <asp:CustomValidator ID="CustomValidatorrcbOnlineTeacher" ValidationGroup="register" runat="server" Display="Dynamic" ForeColor="red" CssClass="alert" OnServerValidate="CustomValidatorrcbOnlineTeacher_OnServerValidate" ErrorMessage="لطفا استاد مورد نظر را انتخاب نمایید"></asp:CustomValidator>
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

                                    <asp:CheckBox ID="chkDoingOnlineDefence"  runat="server" />


                                    <h6 class="header-inline-display">
                                        <span>اینجانب</span>
                                        <span>
                                            <asp:Literal ID="litStOnline" runat="server"></asp:Literal></span>

                                        <span>متقاضی برگزاری جلسه دفاع به صورت آنلاین هستم.</span>
                                        <span style="color: red">هزینه جلسه دفاع:</span>
                                        <asp:Label runat="server" ID="lblMoney" ForeColor="red"></asp:Label>
                                    </h6>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>

        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%; display: none !important">
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
        <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:UpdateProgress ID="updateProgress5" runat="server">
                    <ProgressTemplate>
                        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                            <asp:Image ID="imgUpdateProgress5" runat="server" ImageUrl="../Images/loading-svg-spinning-4.gif" AlternateText="درحال بارگزاری ..." ToolTip="درحال بارگزاری ..." Style="padding: 10px; position: fixed; top: 28%; left: 38%;" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%; float: left;">
                    <asp:Button ID="btnRegister" runat="server" Text="ثبت درخواست" CssClass="btn btn-primary" OnClick="btnRegister_OnClick" ValidationGroup="register" />
                    <asp:Button ID="btnBack" runat="server" Text="انصراف" CssClass="btn btn-danger" OnClick="btnBack_OnClick" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

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


    <telerik:RadWindow ID="rwmMobile" runat="server" Width="400px" Height="300px" Skin="Glow" Modal="True">
        <ContentTemplate>
            <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                        <ProgressTemplate>
                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                <asp:Image ID="imgUpdateProgress1" runat="server" ImageUrl="../Images/loading-svg-spinning-4.gif" AlternateText="درحال بارگزاری ..." ToolTip="درحال بارگزاری ..." Style="padding: 10px; position: fixed; top: 28%; left: 38%;" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                    <div class="container" dir="rtl" style="width: 90%;">
                        <div class="row">
                            <div class="alert alert-danger" style="border: 5px solid; border-radius: 10px;">
                                <h3 style="text-align: center">تغییر شماره تلفن همراه</h3>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-inline" style="width: 90%; margin: 5% auto">
                                <div class="form-group">
                                    <label>شماره جدید:</label>
                                    <asp:TextBox ID="txtMbileChanged" CssClass="form-control textBoxSearch textAlignCenter" runat="server"></asp:TextBox>
                                </div>
                                <asp:Button ID="btnRegisterMobile" ValidationGroup="mobileGroup" CssClass="btn btn-danger" OnClick="btnRegisterMobile_OnClick" runat="server" Text="ثبت تغییر" />
                                <br />
                                <asp:RequiredFieldValidator ID="rfvRegisterMobile" ValidationGroup="mobileGroup" ControlToValidate="txtMbileChanged" ForeColor="red" runat="server" Display="Dynamic" ErrorMessage="شماره موبایل را وارد نمایید"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>




    <div class="modal fade modal-lg" id="myModal" role="dialog" style="background-color: rgba(4, 21, 27, 0.6);">
        <div class="modal-dialog" style="width: 90%;">

            <!-- Modal content-->
            <div class="modal-content" dir="rtl">
                <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <%--                        <asp:UpdateProgress ID="updateProgress" runat="server">
                            <ProgressTemplate>
                                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../Images/loading-svg-spinning-4.gif" AlternateText="درحال بارگزاری ..." ToolTip="درحال بارگزاری ..." Style="padding: 10px; position: fixed; top: 28%; left: 38%;" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>--%>

                        <div class="modal-header">
                            <img src="../Images/check-mark.png" style="width: 32px" class="marginBtn" alt="" />
                            <h4 class="modal-title header-inline-display marginBtn">پذیرش شرایط</h4>
                        </div>
                        <div class="modal-body">
                            <div class="container">
                                <img src="../Images/IdentityImage.png" style="width: 100px; display: block; margin: auto;" alt="" />
                                <div class="row">
                                    <div class="alert alert-danger">
                                        <h2 style=" margin-right: 20px;text-align:center">تمامی دفاع‌ها به علت شرایط به وجود آمده آنلاین برگزار می‌شود</h2>
                                    </div>


                                </div>
                                <div class="row">
                                    <ul>
                                        <li>
                                            <h3 style="color: #006d96;">دستورالعمل برگزاری جلسات دفاع</h3>

                                            <ul>
                                                <li>
                                                    <h4>	جلسات دفاع به صورت کاملاً آنلاین با اتصال صوتی و تصویری دانشجو و ارکان دفاع جلسه دفاع برگزار خواهد شد
                                                    </h4>
                                                </li>
                                                <li>
                                                    <h4>	لزوم هماهنگی دانشجو با اساتید راهنما،مشاور(در صورت وجود)و داور یا داوران در خصوص روز،ساعت و تاریخ برگزاری جلسه دفاع وسپس ثبت تاریخ هماهنگ شده در سامانه هماهنگی جلسات دفاع.
                                                    </h4>
                                                </li>
                                                <li>
                                                    <h4>	فايل مربوط به ارائه ، ميبايست به صورت پاورپوينت تهيه و با فرمت پي-دي-اف ذخيره سازي شده باشد .                                                     </h4>
                                                </li>
                                                <li>
                                                    <h4>		حجم فايل حداكثر 1MB بوده و از پس زمينه آبي رنگ و تصاوير با حجم بالا در آن استفاده نشده باشد . 
                                                    </h4>
                                                </li>
                                                <li>
                                                    <h4>	به منظور تست تجهیزات، شرکت دانشجو  و هر 3 رکن دفاع از پایان نامه در جلسات دفاع از پایان¬نامه آنلاین آزمایشی که روزانه در بخش معاونت فنی واحد برگزار می گردد، الزامي می باشد 
                                                    </h4>
                                                </li>
                                                <li>
                                                    <h4>	حضور آنلاین صوتی و تصویری استاد راهنما، مشاور (درصورت وجود) و داور یا داوران برای شروع جلسه دفاع دانشجو الزامی است. در صورت عدم حضور ارکان فوق الذکر امکان برگزاری جلسه دفاع میسر نمی باشد.</h4>
                                                </li>
                                                <li>
                                                    <h4>	جهت برگزاری جلسه دفاع در زمان مقرر و بدون تاخیر، حضور اساتید راهنما،مشاور(در صورت وجود) و داور یا داوران در زمان تعیین شده جلسه دفاع الزامی می باشد. دانشجو نیز می بایست به جهت هماهنگی های شروع جلسه حداقل 30 دقیقه جلوتر به جلسه دفاع متصل گردد.
                                                    </h4>
                                                </li>
                                                <li>
                                                    <h4>با توجه به مشكلات موجود در برقراري ارتباط و هماهنگي با اساتيد خارج از كشور، در خصوص دفاع هایی که استاد آنلاین آنها در خارج از کشور هستند، مسئوليت هماهنگي با استاد جهت ارتباط با بخش فني و انجام تست هاي لازم بر عهده دانشجو مي باشد و در غير اينصورت پشتيبان فني دفاع صرفاً در زمان برگزاري دفاع جهت رفع مشكلات احتمالي در جلسه حضور خواهند داشت.                                                    
                                                    </h4>
                                                </li>

                                            </ul>

                                        </li>
                                    </ul>

                                </div>

                                   <div class="row">
                                    <ul>
                                        <li>
                                            <h3 style="color: #006d96;">	الزامات اتصال صوتی و تصویری به جلسه دفاع</h3>

                                            <ul>
                                                <li>
                                                    <h4>	رعایت شئونات اخلاقی و اسلامی در اتصال آنلاین صورتی و تصویری به جلسه دفاع از پایان نامه الزامی می باشد.
                                                    </h4>
                                                </li>
                                                <li>
                                                    <h4>		محل قرارگیری دوربین و فاصله آن تا فرد باید به صورتی باشد که چهره فرد به صورت واضح قابل رویت باشد. 
                                                    </h4>
                                                </li>
                                                <li>
                                                    <h4>		دانشجو و اساتید جهت اتصال آنلاین می¬بایست در محل ثابت قرار گیرند به نحوی که شئونات برگزاری جلسه دفاع از پایان¬نامه رعایت گردد (اتصال در حال حرکت، حضور در اتومبیل و ... قابل پذیرش نخواهد بود). اتصال با گوشی همراه قابل قبول نبوده و دانشجو و ارکان دفاع از پایان نامه باید از لپ تاب یا pc متصل به webcam استفاده نمایند.                                               </h4>
                                                </li>
                                                <li>
                                                    <h4>			وجود هدست برای اتصال به جلسه دفاع از پایان نامه ضروری می باشد
                                                    </h4>
                                                </li>
                                                <li>
                                                    <h4>	 محیط قرارگیری جهت اتصال آنلاین نباید دارای سروصدای مختل کننده جلسه دفاع از پایان¬نامه باشد 
                                                    </h4>
                                                </li>
                                                <li>
                                                    <h4>		محل قرارگیری دوربین و فاصله آن تا فرد باید به صورتی باشد که چهره فرد به¬صورت واضح قابل رویت باشد.  </h4>
                                                </li>
                                                <li>
                                                    <h4>		پهنای باند مناسب جهت اتصال به جلسه آنلاین دفاع از پایان نامه حداقل 2 مگابیت بر ثانیه خواهد بود.د.
                                                    </h4>
                                                </li>

                                            </ul>

                                        </li>
                                    </ul>

                                </div>






                                <div class="row">
                                    <ul>
                                        <li>
                                            <h3 style="color: #006d96;">اقداماتی که دانشجو می بایستی پس از برگزاری جلسه دفاع انجام دهد</h3>

                                            <ul>
                                                <li>
                                                    <h4>	تکمیل فرم مربوط به تعیین تکلیف "پذیرش یا چاپ مقاله" یا "انصراف از مقاله" و بارگذاری آن در جلسه دفاع از پایان نامه</h4>
                                                </li>
                                                <li>
                                                    <h4>	در صورت درج اصلاحات در صورت جلسه دفاع از پایان نامه، دانشجو باید اصلاحاتی که ارکان دفاع در جلسه دفاعیه تعیین کرده اند انجام دهد و پس از تکمیل فرم اصلاحیه پایان نامه (سایت اصلی دانشگاه لینک معاونت پژوهشی )، فرم را در سامانه پاسخ گویی (تیکتینگ) برای امور پژوهشی پس از دفاع، تیکت نماید. برای تکمیل فرم در صورتی که در صورتجلسه دفاع تایید استاد داور و مشاور قید شده باشد،دانشجو ملزم به اخذ امضای این اساتید است در غیر این صورت تنها تایید استاد محترم راهنما کافی است.</h4>
                                                </li>
                                                <li>
                                                    <h4>	بارگذاری فایل پایان نامه مطابق آیین نامه نگارش پایان نامه در سامانه تسویه حساب الکترونیکی (زمانی که تسویه حساب دانشجو به مرحله تایید پژوهش برسد).</h4>
                                                </li>
                                            </ul>

                                        </li>
                                    </ul>

                                </div>

                                <div class="row" style="font-size: 20px; margin-right: 35px;font-weight: bold;">
                                    <asp:CheckBox ID="chkCommitment" ForeColor="#f70404" runat="server" CssClass="AcceptedAgreement1"
                                        AutoPostBack="true"
                                        Text="تعهد می‌نمایم که زمان جلسه را با همه ارکان دفاع در اتاق گفتگو هماهنگ نموده ام و تمامی ارکان دفاع زمان مذکور را پذیرفته اند" OnCheckedChanged="chkCommitment_CheckedChanged" />
                                    <br/>
                                    <asp:CheckBox runat="server" ForeColor="#f70404" ID="chbAccept" Text="موارد فوق را مطالعه نموده و تائید می کنم" OnCheckedChanged="chbAccept_OnCheckedChanged" AutoPostBack="true" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-md-10"></div>
                                <div class="col-md-2">
                                    <asp:Button runat="server" ID="btnAccept"  Font-Size="Large" Text="تائید" CssClass="btn btn-success marginBtn" Enabled="false" OnClick="btnAccept_OnClick" />
                                    <asp:Button runat="server" ID="btnClose" Font-Size="Large" CssClass="btn btn-info marginBtn" Text="بستن" OnClick="btnClose_OnClick" />
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>
    </div>


    <script>

        $(document).ready(function () {
            $(".collapse").collapse('show');
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate',
                { extraInputID: 'ContentPlaceHolder1_txtDate', extraInputFormat: 'yyyy/mm/dd' });

            var showConfirm = '<%=ShowConfirm%>';

            if (showConfirm === 'True') {
                $('#myModal').modal({ backdrop: 'static', keyboard: false });
                $('#myModal').modal('show');
            }


        });


    </script>
</asp:Content>
