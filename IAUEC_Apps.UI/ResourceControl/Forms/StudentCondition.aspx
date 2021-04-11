<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/StudentCMS.Master" AutoEventWireup="true" CodeBehind="StudentCondition.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.StudentCondition" %>

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

        });
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>کارتابل دانشجو</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip1" data-toggle="collapse" data-target="#collapse1" title="اطلاعات دفاع خود را مشاهده فرمایید">
                        <div class="panel-title">
                            <img src="../fonts/expand.png" style="width: 2%" id="imgIcon1" alt="" />
                            <div class="header-inline-display">بررسی پیشرفت درخواست</div>
                        </div>
                    </div>
                    <div id="collapse1" class="panel-collapse collapse">
                        <div class="list-group">
                            <div class="list-group-item">

                                <div class="row">
                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading  bg-red">
                                                <img src="../fonts/presentation.png" style="width: 32px" alt="" />
                                                <h5 class="header-inline-display">میزان پیشرفت درخواست</h5>
                                            </div>
                                            <div class="panel-body">
                                                <div class="circle">

                                                    <strong>100<i>%</i></strong>
                                                </div>


                                            </div>
                                            <div class="panel-footer">
                                                <div style="margin-bottom: 1.25%; margin-top: 1.25%;">
                                                    <img src="../fonts/back.png" class="sub-img" alt="" />

                                                    <h6 class="header-inline-display">
                                                        <span>درخواست در گردش شما دارای </span>
                                                        <strong id="strongForProgress" style="font-size: large">100<i>%</i></strong>
                                                        <span>پیشرفت می باشد.</span>
                                                    </h6>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-8" style="text-align: center;">
                                        <div class="panel panel-primary">
                                            <div class="panel-heading  bg-red">
                                                <img src="../fonts/notepad (1).png" style="width: 32px" alt="" />
                                                <h5 class="header-inline-display">فرآیند بررسی درخواست</h5>
                                            </div>
                                            <div class="panel-body row" style="margin-top: 4.2%; margin-bottom: 4.25%;">

                                                <div class="col-md-3">
                                                    <img id="stateOne" src="../fonts/more-circular-symbol.png" style="width: 32px" alt="" />
                                                    ثبت درخواست
                                                </div>


                                                <div class="col-md-3">
                                                    <img id="stateTwo" src="../fonts/more-circular-symbol.png" style="width: 32px" alt="" />
                                                    تایید دانشکده
                                                </div>

                                                <div class="col-md-3">
                                                    <img id="stateThree" src="../fonts/../fonts/more-circular-symbol.png" style="width: 32px" alt="" />
                                                    تایید اداری
                                                </div>

                                                <div class="col-md-3">
                                                    <img id="stateFour" src="../fonts/more-circular-symbol.png" style="width: 32px" alt="" />
                                                    اطلاع رسانی
                                                </div>

                                            </div>
                                            <div class="panel-footer">
                                                <div id="myProgress">
                                                    <div id="myBar" style="color: #dedede">

                                                        <strong id="strongForProgressBar1">100</strong>
                                                        روز

                                                    </div>
                                                </div>
                                                <div style="text-align: center;">
                                                    <img src="../fonts/back.png" class="sub-img" alt="" />

                                                    <h6 class="header-inline-display" style="margin-bottom: 0px !important; margin-right: 0px !important">
                                                        <strong id="strongForProgressBar2" style="font-size: large">100</strong>
                                                        <span>روز دیگر تا زمان پیشنهادی برگزرای جلسه دفاع شما مانده است.</span>

                                                    </h6>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="list-group-item">
                                <div class="alert alert-success" style="text-align: center;">
                                    <h5>دانشجوی گرامی ، درخواست رزرو جلسه دفاع شما در گردش می باشد و در حال بررسی برای <span><%=ViewModel.StateText%></span> است.</h5>

                                </div>
                                <img src="../fonts/megaphone.png" style="width: 32px" alt="" />
                                <h5 class="header-inline-display">لطفا به نکات زیر توجه فرمایید :</h5>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">اگر مایل به اعمال تغییرات و یا حذف درخواست خود هستید، میتوانید از بخش "عملیات مرتبط با درخواست"  استفاده نمایید
                                </h6>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">اگر مایل به مشاهده گردش درخواست خود هستید، میتوانید از بخش "مشاهده گردش درخواست" استفاده نمایید.
                                </h6>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">اگر مایل به مشاهده درخواست های گذشته خود هستید، میتوانید از بخش "گزارش درخواست های گذشته" استفاده نمایید.
                                </h6>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">
                                    <span>برای پیگیری درخواست خود میتوانید با همراه داشتن شماره درخواست</span>
                                    <span>(44) </span>
                                    <span>با داخلی های مربوطه تماس حاصل فرمایید:</span>
                                    <span>دانشکده   135  اداری   136  </span>

                                </h6>
                                <br />
                            </div>

                        </div>


                    </div>

                </div>

            </div>
        </div>

        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip3" data-toggle="collapse" data-target="#collapse3" title="در صورت نیاز به شرکت استاد به صورت آنلاین از بخش زیر استفاده نمایید">
                        <div class="panel-title">
                            <img src="../fonts/expand.png" style="width: 2%" id="imgIcon3" alt="" />
                            <div class="header-inline-display">عملیات مرتبط با درخواست</div>
                        </div>
                    </div>
                    <div id="collapse3" class="panel-collapse collapse">
                        <div class="list-group">
                            <div class="list-group-item">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-6 ">
                                                <div style="display: inline-block" class="col-md-6">
                                                    <img src="../fonts/worker.png" style="width: 32px" alt="" />
                                                    <h5 class="header-inline-display">نقش استاد مورد نظر را انتخاب نمایید:</h5>
                                                </div>

                                            </div>
                                        </div>
                                        <div class=" col-md-6">
                                            <div style="display: inline-block" class="col-md-6">
                                                <img src="../fonts/boss.png" style="width: 32px" alt="" />
                                                <h5 class="header-inline-display">نام استاد مورد نظر را انتخاب نمایید:</h5>
                                            </div>
                                            <div style="display: inline-block" class="col-md-6">
                                            </div>
                                        </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div class="list-group-item">

                                <img src="../fonts/notepad.png" style="width: 32px" alt="" />
                                <h5 class="header-inline-display">تعیین استاد آنلاین:</h5>
                                <br />
                                <img src="../fonts/back.png" class="sub-img" alt="" />
                                <h6 class="header-inline-display">
                                    <span>اینجانب</span>
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
                            <div class="header-inline-display">مشاهده گردش درخواست</div>
                        </div>
                    </div>
                    <div id="collapse2" class="panel-collapse collapse">
                        <div class="list-group">
                            <div class="list-group-item">

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

                                        <telerik:RadTimePicker ID="txtTime" EnableTyping="false" RenderMode="Lightweight" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" CssClass="pdate">
                                            <TimeView ID="TimeView4" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
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

        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="panel-heading " style="cursor: pointer" data-tooltip="tooltip4" data-toggle="collapse" data-target="#collapse4" title="در صورت نیاز به استفاده از رایانه شخصی بخش زیر را مطالعه فرمایید">
                        <div class="panel-title">
                            <img src="../fonts/expand.png" style="width: 2%" id="imgIcon4" alt="" />
                            <div class="header-inline-display">
                                گزراش درخواست ها گذشته
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

                                        <h6 class="header-inline-display">از رایانه موجود در جلسه دفاع استفاده می نمایم (پیشنهاد می شود)</h6>

                                        <br />
                                        <img src="../fonts/back.png" class="sub-img" alt="" />
                                        <asp:RadioButton ID="rdbOwnSystem" runat="server" />

                                        <h6 class="header-inline-display">از رایانه شخصی خود استفاده می نمایم</h6>


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
    </div>

    <script src="../Scripts/jquery3.min.js"></script>
    <script src="../Scripts/circle-progress.min.js"></script>
    <script>
        $(document).ready(function () {
            $(".collapse:first").collapse('show');
            $('.circle').circleProgress({
                value: <%=ViewModel.PercentProgressRequestDegree%>,

                thickness: 7,
                fill: {
                    gradient: ["green"]
                }
            }).on('circle-animation-progress', function (event, progress) {
                $(this).find('strong').html(Math.round(<%=ViewModel.PercentProgressRequest%> * progress) + '<i>%</i>');
                $(strongForProgress).html(Math.round(<%=ViewModel.PercentProgressRequest%> * progress) + '<i>%</i>');
                });

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

            function runInprogressState(elmId) {

            }


            switch (parseInt('<%=ViewModel.StateNum%>')) {
                case 0:
                    runAnimateStateTwo();

                    break;
                case 1:
                    runAnimateStateThree();

                    break;
                case 2:
                    runAnimateStateFour();
                    break;
                case 4:

                    break;
                default:
            }

            var elem = document.getElementById("myBar");
            var num1 = document.getElementById("strongForProgressBar1");
            var num2 = document.getElementById("strongForProgressBar2");
            var width = 1;
            var id = setInterval(frame, 20);
            function frame() {
                if (width >= <%=ViewModel.PercentDayRequest%>) {
                        clearInterval(id);
                    } else {
                        width++;
                        elem.style.width = width + '%';

                        if (width <= <%=ViewModel.DayAmontRequest%>) {
                            num1.textContent = width;
                            num2.textContent = width;
                        }
                    }
                }
        });
    </script>

</asp:Content>
