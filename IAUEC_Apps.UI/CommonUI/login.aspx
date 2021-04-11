<%@ Page Language="C#" CodeBehind="login.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/commonui/css/normalize.css" rel="stylesheet" />
    <link href="/commonui/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        @font-face {
            font-family: Samim;
            src: url('/commonui/fonts/Samim.eot?v=1.0.2');
            src: url('/commonui/fonts/Samim.eot?v=1.0.2#iefix') format('embedded-opentype'),url('/commonui/fonts/Samim.woff?v=1.0.2') format('woff'),url('/commonui/fonts/Samim.ttf?v=1.0.2') format('truetype');
            font-weight: normal
        }

        @font-face {
            font-family: Samim;
            src: url('/commonui/fonts/Samim-Bold.eot?v=1.0.2');
            src: url('/commonui/fonts/Samim-Bold.eot?v=1.0.2#iefix') format('embedded-opentype'),url('/commonui/fonts/Samim-Bold.woff?v=1.0.2') format('woff'),url('/commonui/fonts/Samim-Bold.ttf?v=1.0.2') format('truetype');
            font-weight: bold
        }

        html, body, .mainContainer, body > form {
            height: 100%;
            direction: rtl;
            font-family: Samim;
            text-align: right;
        }

        .noMargin {
            margin-left: -15px;
            margin-right: -15px;
        }

        .mainContent {
            height: calc(100% - 95px);
        }

        .headr-row {
            background: #1565c0;
            height: 65px;
            box-shadow: 0px 3px 10px 0px #999;
            color: #fff;
            font-size: 24px;
            padding-top: 8px;
        }

        .footer-row {
            background: #1565c0;
            height: 30px;
            box-shadow: inset 0px 7px 9px -7px #333;
            text-align: center;
            color: #eee;
            padding-top: 8px;
            font-size: 10px;
        }

        .innerBox {
            background: #eee;
            border: 1px solid #ddd;
        }

        .formLogo img {
            width: 100px;
            margin-bottom: 30px;
            margin-top: 35px;
        }

        .innerBox > div {
            padding: 1.5rem 3rem;
        }

        .innerBoxContent {
            box-shadow: 0px 0px 8px 0px #999;
        }

        .bannerWrapper {
            padding: 15px;
            /*white-space: nowrap;*/
        }

            .bannerWrapper img {
                max-width: 100%;
                vertical-align: middle;
            }

        .versionText {
            font-size: 12px;
        }

        .versionTextWrapper {
            text-align: left;
        }

        .tab-group {
            list-style: none;
            padding: 0;
            margin: 0 0 10px 0;
        }

            .tab-group:after {
                content: "";
                display: table;
                clear: both;
            }

            .tab-group li a {
                display: block;
                text-decoration: none;
                padding: 10px 15px;
                background: rgba(160, 179, 176, 0.25);
                color: #a0b3b0;
                font-size: 14px;
                float: left;
                width: 50%;
                text-align: center;
                cursor: pointer;
                -webkit-transition: .5s ease;
                transition: .5s ease;
            }

                .tab-group li a:hover {
                    background: #0d47a1;
                    color: #ffffff;
                }

            .tab-group .active a {
                background: #4993f8;
                color: #ffffff;
            }

        .tab-content > div:first-child {
            display: none;
        }

        .btnRowsMargin {
            margin: 0 0 10px;
        }

        #btnResetPassword {
            width: 100%;
        }

        #btnResendCode {
            width: 65%;
        }

        #btnReturn {
            width: 31%;
            margin-right: 5px;
        }

        .verticalHelper {
            display: inline-block;
            height: 100%;
            vertical-align: middle; /*middle*/
        }

        div#cph > div > div, div#cph iframe {
            width: 100% !important;
        }

        .container-baner-font {
            height: 49%;
            text-align: center;
            font-size: 24px;
            padding: 52px;
            color: #1565c0;
            font-weight: bold;
        }

        @media screen and (min-width: 700px) {
            .container-baner-font {
                height: 58%;
                text-align: center;
                font-size: 15px;
                padding-top: 70px;
                color: #1565c0;
                font-weight: bold;
            }
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">


        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadWindowManager ID="rdw" runat="server" Height="400px"
            Width="570px">
        </telerik:RadWindowManager>
        <div id="hd">
            <asp:HiddenField runat="server" ID="hd_un" />
        </div>
        <input type="hidden" name="urlReff" runat="server" id="urlReff" />




        <div class="container-fluid mainContainer">
            <div class="row noMargin headr-row">
                <div class="col-md-10">
                    <img src="/commonui/images/logo_50_w.png" />
                    <span>سامانه خدمات الکترونیکی</span>
                </div>
                <div class="col-md-2 versionTextWrapper">
                    <asp:Label runat="server" ID="lblVersion" CssClass="versionLabel versionText"></asp:Label>
                </div>
            </div>
            <div class="row mainContent justify-content-center align-items-center">

                <div class="col-md-6 col-sm-12 col-sm-offset-3 innerBox">
                    <div class="row justify-content-center align-items-center">
                        <div class="col-md-12 col-sm-12 bg-white innerBoxContent">
                            <div class="row">
                                <div class="col-md-6 pt-3 pb-3">

                                    <div class="formLogo text-center">
                                        <img src="/commonui/images/AzadLogo.png" />
                                    </div>
                                    <div class="form" id="tabs">
                                        <ul class="tab-group">
                                            <li class="tab active"><a href="#login">ورود به سامانه</a></li>
                                            <li class="tab"><a href="#signup">بازیابی رمز عبور</a></li>
                                            <li class="tab" style="display: none"><a href="#decode">کد گشایی</a></li>
                                        </ul>
                                        <div class="tab-content">
                                            <div id="signup">
                                                <asp:UpdatePanel runat="server" ID="uplRecoverPass">
                                                    <ContentTemplate>
                                                        <asp:Panel runat="server" ID="pnlSendCode">
                                                            <input type="text" id="txtuser" runat="server" placeholder="نام کاربری سامانه خدمات الکترونیکی" class="form-control mb-2" autocomplete="off" oninvalid="this.setCustomValidity('وارد کردن نام کاربری الزامی می باشد')" />
                                                            <input type="text" id="idd_meli" runat="server" placeholder="کد ملی" class="form-control mb-2" autocomplete="off" oninvalid="this.setCustomValidity('وارد کردن کد ملی الزامی می باشد')" />
                                                            <button id="btn_Pass" runat="server" onserverclick="btn_Pass_ServerClick" class="btn btn-info form-control" type="button">دریافت کد بازیابی</button>
                                                        </asp:Panel>
                                                        <asp:Panel runat="server" ID="pnlEnterCode" Visible="false">
                                                            <input type="text" id="txtRecoveryCode" runat="server" placeholder="کد بازیابی" class="form-control mb-2" autocomplete="off" oninvalid="this.setCustomValidity('وارد کردن کد بازیابی الزامی می باشد')" />
                                                            <input type="password" id="txtRecoveryPass" runat="server" placeholder="کلمه عبور جدید" class="form-control mb-2" autocomplete="off" oninvalid="this.setCustomValidity('وارد کردن کلمه عبور الزامی می باشد')" />
                                                            <input type="password" id="txtRepetRecoveryPass" runat="server" placeholder="تکرار کلمه عبور جدید" class="form-control mb-2" autocomplete="off" oninvalid="this.setCustomValidity('وارد کردن تکرار کلمه عبور الزامی می باشد')" />
                                                            <div class="btnRowsMargin">
                                                                <button id="btnResetPassword" runat="server" onserverclick="btnResetPassword_ServerClick" class="btn btn-success" type="button">تغییر رمز عبور</button>
                                                            </div>
                                                            <div class="btnRowsMargin">
                                                                <button id="btnResendCode" runat="server" onserverclick="btnResendCode_ServerClick" class="btn btn-info" type="button">ارسال مجدد کد بازیابی</button>
                                                                <button id="btnReturn" runat="server" onserverclick="btnReturn_ServerClick" class="btn btn-warning" type="button">بازگشت</button>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel runat="server" ID="pnlMessage" Visible="false">
                                                            <div class="alert alert-success">
                                                                <span>تغییر رمز با موفقیت انجام شد</span>
                                                            </div>
                                                            <div>
                                                                <button id="btnLastReturn" runat="server" onserverclick="btnLastReturn_ServerClick" class="btn btn-info form-control">بازگشت</button>
                                                            </div>
                                                        </asp:Panel>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div id="login">
                                                <input id="txtUserName" runat="server" placeholder="نام کاربری سامانه خدمات الکترونیکی" class="form-control mb-2"
                                                    type="text" required autocomplete="off" oninvalid="this.setCustomValidity('وارد کردن نام کاربری الزامی می باشد')" />
                                                <input id="txtPassword" runat="server" placeholder="رمز عبور سامانه خدمات الکترونیکی" class="form-control mb-2"
                                                    type="password" required autocomplete="off" oninvalid="this.setCustomValidity('وارد کردن رمز عبور الزامی می باشد')" />
                                                <div id="cph" runat="server" visible="false">
                                                    <script src='https://www.google.com/recaptcha/api.js?hl=fa'></script>
                                                    <div class="g-recaptcha" data-sitekey="6LcMeRkTAAAAANJNW7aihv26REKStDar0mUyKHHJ"></div>
                                                </div>
                                                <button id="btn" runat="server" class="btn btn-success form-control">ورود به سامانه</button>
                                            </div>
                                            <div id="decode" dir="ltr" style="display: none">
                                                <input id="Text1" runat="server" placeholder="کد" class="form-control mb-2" />
                                                <input id="text2" runat="server" placeholder="دیکد" class="form-control mb-2" />
                                                <button id="btnDecode" runat="server" onserverclick="decodeText" class="btn btn-success form-control">Decode</button>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-6 bannerWrapper">
                                    <%--<span class="verticalHelper"></span>--%>
                                    <%--<img src="/commonui/images/LoginPage_Banner.jpg" />--%>
                                    <div class="container-baner-font" style="">
                                        <div>دانشگاه آزاد اسلامی</div>
                                        <div style="font-size: 18px;">واحد الکترونیکی</div>

                                    </div>
                                    <a href="http://sc.iauec.ac.ir" target="_blank"><img src="/commonui/images/3.jpg" /></a>
                                    <div class="popUpText" style="display: none;">
                                        <h5>الزام به اخذ کارت ورود به جلسه امتحانات پایان ترم
                                        </h5>
                                        <p style="text-align: justify">
                                            به اطلاع دانشجویان محترم می رساند با عنایت به آغاز امتحانات از تاریخ 14 دی ماه، کارت ورود به جلسه امتحانات از تاریخ 7 دی ماه از طریق سامانه خدمات الکترونیکی
    <a href="https://service.iauec.ac.ir">https://service.iauec.ac.ir</a>
                                            قابل رویت و دریافت می شود.
لذا خواهشمند است نسبت به دریافت و پرینت کارت ورود به جلسه به دریافت و پرینت کارت ورود به جلسه خود اقدام نمایید. لازم به ذکر است برای ورود به جلسه امتحان ارائه کارت ورود به جلسه به همراه کارت شناسایی معتبر الزامی است.
                                        </p>
                                    </div>





                                    <%--<div style="margin-top:-220px; white-space: normal;" class="alert alert-info">
                                        <p style="font-weight: bold;">دانشجوی گرامی توجه:</p>
                                        <p>
                                            <span style="display: inline-block;width:100%;text-align: justify;">جهت مشاهده بخش آنلاین جلسات دفاعیه، از طریق لینک ذیل اقدام نمایید:</span>
                                            <br /><a href="http://lms972.iauec.ac.ir">http://lms972.iauec.ac.ir</a><br />
                                        </p>
                                        <p>پشتیبانی فنی: 42863863-021</p>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid noMargin footer-row">
                <div class="col-md-12">
                    <span>تمامی حقوق این سامانه محفوظ و متعلق به واحد الکترونیکی دانشگاه آزاد اسلامی می باشد.</span>
                </div>
            </div>
        </div>



    </form>

    
    <script src="/commonui/js/jquery.min.js"></script>
    <script src="/commonui/js/index.js"></script>


    <%--<div class="popUpWrapper" >
        <a href="/" class="popUpCloseButton">X</a>
        <div class="popUpContent">
            <p>به اطلاع می رساند با توجه به خاتمه پشتیبانی از نرم افزار فلش در مرورگرها و جهت تسهیل حضور دانشجویان و اساتید محترم در کلاسهای آنلاین، از روز شنبه مورخ 20 دی ماه 1399 کلیه کلاسهای آنلاین واحد الکترونیکی بر روی نسخه جدید سامانه برگزار می گردد.</p>
            <p>لطفاً به منظور کسب آمادگی لازم جهت حضور در کلاسها، <a href="http://iauec.ac.ir/class">فایل راهنما</a> را مطالعه نمایید</p>
            <p>
                <span>پشتیبانی فنی: 02142863863</span>
                <br />
                <span>واحد الکترونیکی دانشگاه آزاد اسلامی</span>
            </p>
        </div>
    </div>--%>

    <style>
        .popUpWrapper {
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            margin: auto;
            width: 420px;
            height: 300px;/*445px;*/
            background: #fff;
            border: 1px solid #999;
            direction: rtl;
            padding: 5px;
            box-shadow: 0 0 10px 0px #555;
        }

        .popUpCloseButton {
            font-family: arial;
            font-weight: bold;
        }

        .popUpContent {
            padding: 5px;
            text-align: justify;
        }

            .popUpContent img {
                /*height: 550px;*/
                width: 100%;
            }

        .popUpText {
            border-style: double;
            border-color: mediumblue;
            padding: 5%;
            margin-bottom: 14px;
            font-family: 'B Nazanin';
            width: 100%;
            /*height:100%*/
        }
    </style>
    <script>
        $('.popUpCloseButton').on('click', function (e) {
            e.preventDefault();
            $('.popUpWrapper').hide();
        })
        $("#btn").click(function (e) {
            var a = $("#tabs .tab-group .tab.active").find("a").attr("href");

            debugger;
            if (a == "#login") {

                __doPostBack("login");
                <%--var txtUser = document.getElementById('<%=txtUserName.ClientID %>').value;
                var txtPass = document.getElementById('<%=txtPassword.ClientID %>').value;
                var data = new FormData();
                data.append("user", txtUser);
                data.append("pass", txtPass);
                $.ajax({
                    type: 'POST',
                    url: "/commonui/login.aspx/ClickME",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data:data,
                    succsess: function(r) { },
                });--%>
            }
            else if (a == "#signup") {
                __doPostBack("recover");
            }
            return false;
        });
    </script>
</body>
</html>
