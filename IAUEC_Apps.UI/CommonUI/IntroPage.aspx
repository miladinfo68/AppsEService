<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IntroPage.aspx.cs"
    Inherits="IAUEC_Apps.UI.CommonUI.IntroPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
  
    <link href="<%= bootstrapAddress %>" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../University/Theme/cache/compressed.css" />
    <link href="fonts/css/font-awesome.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <%--<link href="../University/CooperationRequest/css/bootstrap.min.css" rel="stylesheet" />--%>




    <style>
        html {
            background-color: #fff;
        }

        #centerWrapper {
            margin: 0 auto 15px auto;
        }

        #bgImage {
            position: fixed;
            top: 0;
            left: 0;
            z-index: -4;
            min-width: 115%;
            min-height: 100%;
            -webkit-transition: margin-left 450ms linear;
            -moz-transition: margin-left 450ms linear;
            -o-transition: margin-left 450ms;
            -ms-transition: margin-left 450ms;
            transition: margin-left 450ms;
        }

        .tile {
            -webkit-transition-property: box-shadow, margin-left, margin-top;
            -webkit-transition-duration: 0.25s, 0.5s, 0.5s;
            -moztransition-property: box-shadow, margin-left, margin-top;
            -moz-transition-duration: 0.25s, 0.5s, 0.5s;
            -o-transition-property: box-shadow, margin-left, margin-top;
            -o-transition-duration: 0.25s, 0.5s, 0.5s;
            -ms-transition-property: box-shadow, margin-left, margin-top;
            -ms-transition-duration: 0.25s, 0.5s, 0.5s;
            transition-property: box-shadow, margin-left, margin-top;
            transition-duration: 0.25s, 0.5s, 0.5s;
        }

        #RadWindowWrapper_UserListDialog .rwTable {
            height: 900px !important;
        }

        #walletMenuBox {
            display: none;
            position: absolute;
            min-width: 120px;
            background: #00c851;
            padding: 10px;
            box-shadow: 0px 4px 5px 0px #777;
        }

        #btnWalletWrapper {
            /*display: none;*/
            background: #00c851;
        }

            #btnWalletWrapper hr {
                margin-top: 10px;
                margin-bottom: 10px;
            }

            #btnWalletWrapper:hover #walletMenuBox {
                display: block;
            }

        .walletMenuItem {
        }

            .walletMenuItem a, .walletMenuItem a:hover, .walletMenuItem a:active, .walletMenuItem a:focus {
                text-decoration: none;
                color: #000;
                display: block;
                padding: 8px;
            }

                .walletMenuItem a:hover {
                    background: rgba(255,255,255,0.2);
                }

        @media screen and (min-width: 700px) {
            #a_LMS {
                height: 158px !important;
                margin-top: 680px;
                margin-left: 0px;
                width: 300px;
                box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)
            }
        }

        #a_LMS {
            margin-top: 680px;
            margin-left: 0px;
            width: 300px;
            height: 160px;
            box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)
        }
    </style>
    <%--<script type='text/javascript'>var _gaq = _gaq || []; _gaq.push(['_setAccount', 'UA-30159978-1']); _gaq.push(['_trackPageview']); (function () { var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true; ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js'; var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s); })();</script> --%>
    <!--[if IE]>
    <script src="js/html5.js"></script>
     <![endif]-->
    <!--[if lt IE 9]>
    <script type="text/javascript" language="javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="js/html5.js"></script>
	<![endif]-->
    <!--[if gte IE 9]><!-->
    <%--<script src="../University/Theme/js/jquery.min.js"></script>--%>
    <script src="../University/Theme/js/html5.js"></script>
    <![endif]-->
    <!--[if !IE]>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min.js"></script>
	<![endif]-->

    <script type="text/javascript">window.jQuery || document.write('<\/script><script type="text/javascript" src="../University/Theme/js/jquery1102.js"><\/script>')</script>
    <script type="text/javascript" src="../University/Theme/js/plugins.js"></script>
    <script>
        scale = 145;
        spacing = 10;
        theme = 'theme_default';
        $group.titles = ["Home", "Download", "Support"];
        $group.spacingFull = [5, 5, 5];
        $group.inactive.opacity = "1";
        $group.inactive.clickable = "1";
        $group.showEffect = 0;
        $group.direction = "horizontal";

        mouseScroll = "1";

        siteTitle = 'دانشگاه آزاد اسلامی واحد الکترونیکی';
        siteTitleHome = 'صفحه اول';
        showSpeed = 400;
        hideSpeed = 300;
        scrollSpeed = 550;

        device = "desktop";
        scrollHeader = "1";
        disableGroupScrollingWhenVerticalScroll = "";

        /*For background image*/
        bgMaxScroll = "130";
        bgScrollSpeed = "450";

        /*For responsive */
        autoRearrangeTiles = "1";
        autoResizeTiles = "1";
        rearrangeTreshhold = 3;

        /*Locale */
        lang = "en";
        l_pageNotFound = "404 - Page not Found";
        l_pageNotFoundDesc = "<h2 class='margin-t-0'>404 - Page not Found</h2>We're sorry, the page you're looking for is not found.";
        l_menu = "Menu";
        l_goToFullSiteRedirect = "You'll be redirected to the full site";
        panelDim = '0.6'; hidePanelOnClick = '1'; panelGroupScrolling = ''; pageTitles = new Array(); pageTitles['typography.php'] = 'Typography'; pageTitles['changelog.php'] = 'Changelog and Bugs'; pageTitles['features.php'] = 'Features'; pageTitles['about.php'] = 'About'; pageTitles['tutorials.php'] = 'Docs and Tutorials'; pageTitles['contact.php'] = 'Contact'; pageTitles['download.php'] = 'Download'; pageTitles['buytemplate.php'] = 'Buy the Metro UI Template'; pageTitles['demo.php'] = 'Demo sites and examples'; pageTitles['download-full.php'] = 'Download the full Metro UI template'; pageTitles['download-lite.php'] = 'Download the lite Metro UI template'; pageURL = new Array(); pageURL['typography.php'] = 'Typography'; pageURL['changelog.php'] = 'bugs'; pageURL['features.php'] = 'Features'; pageURL['about.php'] = 'about'; pageURL['tutorials.php'] = 'tutorials'; pageURL['contact.php'] = 'Contact'; pageURL['download.php'] = 'Download'; pageURL['download-full.php'] = 'download full'; pageURL['download-lite.php'] = 'download lite'; pageURL['buytemplate.php'] = 'buy'; pageURL['demo.php'] = 'demo';</script>
    <script type="text/javascript" defer src="../University/Theme/cache/compressed.js"></script>
    <style>
        #catchScroll {
            background: rgb(30,30,30);
            -ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=00)';
            filter: alpha(opacity=00);
            -moz-opacity: 0;
            -khtml-opacity: 0;
            opacity: 0;
        }

        .topIconsWrapper {
            z-index: 999;
            margin-top: 15px;
        }

        .nav > li > a {
            background-color: transparent;
            opacity: 0.8;
        }

            .nav > li > a:hover, .nav > li > a:focus {
                background-color: transparent;
                opacity: 1;
            }

        #RadWindowWrapper_UserListDialog {
            left: 0 !important;
            top: 0 !important;
            width: 100% !important;
        }
        /*.rwTable{
            height:900px !important;
        }*/
    </style>
    <noscript><style>
                  #tileContainer {
                      display: block;
                  }
              </style></noscript>
</head>
<body class="full desktop">
    <script type="text/javascript">
        function openLmsModal() {
            $('#lmsModal').modal('show');
        }
        function closeLmsModal() {
            $('#lmsModal').modal('hide');
        }
    </script>
    <script type="text/javascript">
        function submitform() {
            document.forms["form1"].submit();
        }
        function openShowFileInPopup(path) {
            debugger;
            setTimeout(function () { window.radopen(path, "UserListDialog"); }, 1000);

            return false;
        }
        function CloseModal() {
            var window = $find('UserListDialog');
            window.close();
        }
        function CloseTuitionModal() {
            var window = $find('PayTuition');
            window.close();
        }
    </script>


    <form id="form1" runat="server" method="post">
        <div dir="rtl">
            <div class="modal fade" id="lmsModal" tabindex="-1" role="dialog" aria-labelledby="lmsModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="exampleModalLabel">انتخاب سامانه مدیریت یادگیری</h4>
                        </div>
                        <div class="modal-body">
                            <div class="container-fluid" dir="rtl">
                                <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); font-size: larger; background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                                    <div class="rwDialogPopup radconfirm">
                                        <div class="rwDialogText" style="line-height: 30px; font-size: x-large; font-family: Tahoma,IranNastaliq">
                                           
                                            <p style="margin-top: auto;">
                                                جهت حضور در کلیه کلاس های آنلاین (جلسات اصلی و جبرانی)، بر روی لینک "ورود سامانه مدیریت یادگیری -کلاسهای آنلاین" کلیک نمایید.

                                            </p>
                                            <p style="margin-top: auto;">
                                                برای مشاهده کلاسهای ضبط شده قبل از تاریخ 20 دی ماه به لینک "مشاهده آرشیو کلاس ها" مراجعه نمایید.
                                            </p>
<%--                                            <asp:Literal ID="confirmMessage" runat="server" Text="جهت حضور در کلیه کلاس های آنلاین (جلسات اصلی و جبرانی)، بر روی لینک ورود سامانه مدیریت یادگیری -کلاسهای آنلاین کلیک نمایید. برای مشاهده کلاسهای ضبط شده قبل از تاریخ 20 دی ماه به لینک "مشاهده آرشیو کلاس ها" مراجعه نمایید." />--%>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button runat="server" Height="70px" OnClick="lmsNew_ServerClick" Text="ورود به سامانه مدیریت یادگیری-کلاس آنلاین" CssClass="form-control" ForeColor="White" BackColor="#0123a5" Font-Size="X-Large" ID="lmsNew" />

                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button runat="server" Height="70px" OnClick="lmsOld_ServerClick" Text="مشاهده آرشیو کلاسها" CssClass="form-control" ForeColor="White" BackColor="#01a516" Font-Size="X-Large" ID="lmsOld" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-1"></div>
            <div class="col-md-11 topIconsWrapper">
                <ul class="nav nav-pills right">
                    <li id="btnWalletWrapper">
                        <a href="/" id="btnWallet"><i class="fa fa-wallet " style="color: whitesmoke"></i></a>
                        <div id="walletMenuBox">
                            <div class="walletMenuItem">
                                اعتبار شما:
                                <asp:Label runat="server" ID="lblCurrentBalance"></asp:Label>
                            </div>
                            <hr />
                            <div class="walletMenuItem"><a href="/" id="btnWalletTopup">افزایش اعتبار</a></div>
                            <div class="walletMenuItem"><a href="/University/Wallet/Pages/WalletTransactions.aspx">گردش حساب</a></div>
                            <%--<div class="walletMenuItem"><a href="/" id="btnPayTuition">پرداخت شهریه</a></div>--%>
                        </div>
                    </li>

                    <li style="display: block; background-color: blue;">
                        <a id="ChangePassword" runat="server" onserverclick="ChangePassword_ServerClick">
                            <i class="fa fa-key" style="color: whitesmoke"></i>
                        </a>
                    </li>
                    <li style="display: block; background-color: red">
                        <a id="Logout" runat="server" onserverclick="a_exit_ServerClick">
                            <i class="fa fa-sign-out" style="color: whitesmoke"></i>
                        </a>
                    </li>
                    <li><span style="color: white">---</span></li>
                    <li><span style="font-size: 24px" id="usernamelbl" runat="server"></span></li>
                </ul>

            </div>
        </div>

        <input type="hidden" runat="server" id="LogStatus" name="LogStatus" style="width: 200px;" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <telerik:RadWindowManager ID="rwm_message" runat="server" Height="500px" Modal="True" Width="700px" Behaviors="Maximize,Minimize">
            <Windows>
                <telerik:RadWindow ID="WalletTopup" runat="server" Height="290px" Width="600px" Behaviors="Default"></telerik:RadWindow>
            </Windows>
            <Windows>
                <telerik:RadWindow ID="PayTuition" runat="server" Height="290px" Width="600px" Behaviors="Default"></telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

        <img src="../University/Theme/images/metro_green_angular_low_res.jpg" alt='background-image' id='bgImage' />
        <div id="wrapper">
            <div id="centerWrapper">
                <div id="tileContainer">
                    <!--<img id="arrowRight"  src="img/Azad_University_logo.png"  alt="right arrow" style="margin-left: 722px; opacity: 0.5; display: inline;width:180px">-->
                    <%--ایمیل--%>
                    <a id="Email" runat="server" onserverclick="Email_ServerClick" class="tile tileFlipText vertical group0 "
                        style="margin-top: 45px; margin-left: 0px; width: 300px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='45-0-300'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #FF8800;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(111,59,0,0.9); font-size: 42px;">
                                    <img title='' alt='' style='margin-top: 0px; margin-left: 10px;'
                                        src="../University/Theme/images/pen.png" height="60" width="60" />
                                    پست الکترونیکی    	</h3>
                            </div>
                            <div class='flipBack' style="background: #ffbb33;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(255,136,0,0.9);">
                                    <div style="font-weight: 300; font-size: 42px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px;" src="../University/Theme/images/pen2.png" height="50" width="50">
                                        پست الکترونیکی    
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>
                    <%--ویرایش اطلاعات فردی--%>
                    <a runat="server" id="EditInfo" onserverclick="EditInfo_ServerClick" class="tile tileSlideshow group0 "
                        style="margin-top: 45px; margin-left: 310px; width: 300px; height: 145px; background: #6950ab; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='45-310-300' data-n="0">

                        <div class='imgWrapperBack' style="width: 302px; height: 147px">
                            <img src='empty' alt='' />
                        </div>
                        <div class='imgWrapper' style="width: 302px; height: 147px">
                            <img src="../University/Theme/images/tiles.png" alt='' />
                        </div>

                        <div class='imgText'>ویرایش : آدرس-کدپستی-تلفن-عکس پرسنلی-موبایل</div>
                        <script>slideshowTiles["tileSlideshow0-2-0"] = [["img\/features\/tiles.png", "img\/features\/modern.png", "img\/features\/seo.png", "img\/features\/devices.png"], ["", "", "", ""], ["Many types of tiles", "Using the latest technology", "SEO optimized", "Responsive Webdesign"], "slide-right, slide-up, slide-down,slide-left", 4000]</script>

                        <div class='tileLabelWrapper top' style='border-top-color: #FF8000;'>
                            <div class='tileLabel top'></div>
                        </div>
                    </a>

                    <%--گواهی اشتغال--%>
                    <a id="govahi" runat="server" onserverclick="govahi_ServerClick" class="tile tileSlideFx group0 "
                        style="margin-top: 45px; margin-left: 620px; width: 300px; height: 145px; background: #333; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='45-620-300'>

                        <div class='slideText' style='font-size: large' dir="rtl">
                            ارسال گواهی اشتغال از طریق پست
                        </div>
                        <div class='imgWrapper'>
                            <img src="../University/Theme/images/metro_slide_300x150.png" alt="" title="" />
                        </div>
                        <div class='tileLabelWrapper bottom'>
                            <div class='tileLabel bottom' style='border-bottom-color: rgb(80,150,1);'></div>
                        </div>

                    </a>
                    <%--کارت دانشحویی--%>
                    <a id="StCard" runat="server" onserverclick="StCard_ServerClick" class="tile tileCenteredSlide left group0 "
                        style="margin-top: 200px; margin-left: 0px; width: 300px; height: 145px; background: #33b5e5; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='200-0-300'>

                        <div class="container1">
                            <h3 style="text-shadow: 1px 1px 5px rgba(0,57,78,0.9); font-size: 42px;">

                                <img title='' alt='' style='margin-top: 0px; margin-left: 0px;'
                                    src="../University/Theme/images/box_info.png" height="100" width="100" />

                                دریافت کارت دانشجویی    	</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl" style="text-shadow: 1px 1px 5px rgba(0,57,78,0.9); font-size: 32px;">جهت درخواست دادن ارسال کارت دانشجویی از طریق پست اینجا کلیک نمایید...</h5>
                        </div>

                    </a>

                    <%--آیکون دانشگاه--%>
                    <a class="tile" style="margin-top: 200px; margin-left: 370px; width: 200px; display: block; text-align: center;"
                        data-pos="200-370-200">
                        <div style="width: 100%">
                            <img src="../University/Theme/images/Azad_University_logo.png" style="width: 200px" />
                        </div>

                    </a>
                    <%--تسویه حساب--%>
                    <a id="a_CheckOut" runat="server" onserverclick="a_CheckOut_ServerClick" class="tile tileFlipText vertical group0 "
                        style="margin-top: 200px; margin-left: 620px; width: 300px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='200-620-300'>


                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #00C851;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(0,126,51,0.9); font-size: 42px;">
                                    <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 10px;" src="images/Checkout.png" height="90" width="90">
                                    تسویه حساب
                                </h3>
                            </div>
                            <div class='flipBack' style="background: #00E883;">

                                <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                    <h5 style="text-shadow: 1px 1px 5px rgba(153,51,204,0.9); font-size: 42px;">
                                        <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 10px;" src="images/Checkout.png" height="90" width="90">
                                        تسویه حساب          
                                    </h5>
                                </div>
                            </div>

                        </div>
                    </a>

                    <%--دانلود فایل ها--%>
                    <a id="Adobe" runat="server" onserverclick="Adobe_ServerClick" class="tile tileCenteredSlide left group0 "
                        style="margin-top: 510px; margin-left: 310px; width: 300px; height: 160px; background: rgb(255, 68, 68); box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='355-0-300'>

                        <div class="container1">
                            <h3 style="text-shadow: 1px 1px 5px rgba(204,0,0,0.9); font-size: 42px;">دانلود فایل کلاس ها</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl" style="text-shadow: 1px 1px 5px rgba(204,0,0,0.9); font-size: 32px;">برای دانلود فایل کلاس های ترم جاری کلیک نمایید...</h5>
                        </div>

                    </a>

                    <%--امتحانات
                    <a id="a2" runat="server" visible="true" onserverclick="a_Exam_ServerClick" class="tile tileSlideFx group0 "
                        style="margin-top: 355px; margin-left: 620px; width: 300px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='355-620-300'>

                        <div class="slideText" style="background: #333; font-size: 42px;">امتحانات </div>
                        <div class="imgWrapper">

                            <img title='' alt='' style='margin-top: 0px; margin-left: 0px;' src="images/exam.jpg" />
                        </div>
                        <div class='tileLabelWrapper bottom'>
                            <div class='tileLabel bottom' style='border-bottom-color: rgb(80,150,1);'></div>
                        </div>

                    </a>
--%>
                    <a id="btnExamLMS" onserverclick="btnExamLMS_ServerClick" runat="server" class="tile tileCenteredSlide left group0 " style="margin-top: 355px; margin-left: 0px; width: 300px; height: 145px; background: rgb(22, 167, 87);" data-pos='355-0-610'>
                        <div class="container1">
                            <h3 style="font-size: 32px">سامانه آزمون (وادآزما)</h3>
                        </div>
                        <div class="container2">
                            <h5 style="font-size: 48px" dir="rtl">کلیک نمایید...</h5>
                        </div>
                    </a>
                    <%--جلسات دفاع--%>
                    <%--<a id="a3" runat="server" visible="false" onserverclick="a_Reserve_DefenceClass" class="tile tileFlipText vertical group0 "
                        style="margin-top: 510px; margin-left: 0px; width: 455px; height: 160px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19);"
                        data-pos='510-0-455'>

                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #00bbbb;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(109,38,255,0.9)">
                                    <div style="font-weight: 300; font-size: 42px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        <img title='' alt='' style='margin-top: 8px; margin-left: 0px;'
                                            src="images/class-reservation.png" height="85" width="90" />
                                        هماهنگی جلسات دفاع         
                                    </div>
                                </h3>
                            </div>
                            <div class='flipBack' style="background: #56D4D4;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(0,161,91,0.9)">
                                    <div style="font-weight: 300; font-size: 42px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        <img title="" alt="" style="position: relative; vertical-align: middle; top: 2px; margin-right: 0px;"
                                            src="images/class-reservation.png" height="85" width="90" />
                                        هماهنگی جلسات دفاع          
                                    </div>
                                </h5>
                            </div>

                        </div>

                    </a>--%>
                    <%--پرتال--%>
                    <%--  <a id="A1" runat="server" onserverclick="A1_ServerClick" class="tile tileFlipText vertical group0 " visible="false"
                        style="margin-top: 510px; margin-left: 570px; width: 350px; height: 160px; background: rgba(0,0,0,0); box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='510-465-455'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #ffe711;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(109,38,255,0.9); font-size: 42px;">
                                    <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 10px;"
                                        src="images/Research.jpg" height="85" width="65" />
                                    پورتال پژوهش
                                </h3>
                            </div>
                            <div class='flipBack' style="background: #ffe711;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(0,161,91,0.9)">
                                    <div style="font-weight: 300; font-size: 42px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 10px;"
                                            src="images/Research.jpg" height="85" width="65" />
                                        پورتال پژوهش
                                    </div>

                                </h5>
                            </div>
                        </div>
                    </a>--%>
                    <%--مدیریت یادگیری--%>
                    <a id="a_LMS" runat="server" visible="true" onserverclick="a_LMS_ServerClick" class="tile tileFlipText vertical group0 "
                        style="margin-top: 510px; margin-left: 620px;"
                        data-pos='680-620-350'>

                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #E4BD15;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(109,38,255,0.9); font-size: 42px;">
                                    <img title='' alt='' style='margin-top: 0px; margin-left: 0px;'
                                        src="../University/Theme/images/leraning.png" height="85" width="90" />
                                    سامانه مدیریت یادگیری-کلاس آنلاین         	</h3>
                            </div>
                            <div class='flipBack' style="background: #E7CF69;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(0,161,91,0.9);">
                                    <div style="font-weight: 300; font-size: 42px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 10px;"
                                            src="../University/Theme/images/leraning.png" height="85" width="65" />
                                        سامانه مدیریت یادگیری-کلاس آنلاین        
                                    </div>
                                </h5>
                            </div>
                        </div>
                    </a>

                    <%-- ارزشیابی اساتید 
                    <a id="btnProfessorEvaluation" runat="server" onserverclick="btnProfessorEvaluation_ServerClick" class="tile tileFlipText vertical group0 "
                        style="margin-top: 510px; margin-left: 620px; width: 300px; height: 160px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='200-620-300'>


                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #f60;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(0,126,51,0.9); font-size: 42px;">ارزشیابی اساتید
                                </h3>
                            </div>
                            <div class='flipBack' style="background: #f60;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(153,51,204,0.9); font-size: 42px;">ارزشیابی اساتید
                                </h5>
                            </div>

                        </div>
                    </a>--%>

                    <%--بازی
                    <a id="aGameCenter" runat="server" class="tile tileSlide group0 " onserverclick="aGameCenter_ServerClick"
                        style="margin-top: 355px; margin-left: 0; width: 300px; height: 145px; background: #dc9d49; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='680-360-200'>
                        <h3 style="font-family: 'B Nazanin'; font-size: 48px; text-align: center; vertical-align: central; align-content: center; line-height: 100px">سرگرمی</h3>

                        <%-- <div class='slideText' dir="rtl" style="color:green">
                            ورود به صفحه بازی و سرگرمی
                        </div>
                        <div class='imgWrapper'>
                            <img src="../University/Theme/images/game.png" alt="" title="" />
                        </div>--% >
                        <%--<div class='tileLabelWrapper bottom'>
                            <div class='tileLabel bottom' style='border-bottom-color: rgb(224, 0, 178);'></div>
                        </div>--% >

                    </a>--%>
                    <%--گفتگو در خصوص پایان نامه--%>
                    <%--                    <a id="a5" runat="server" visible="false" onserverclick="a3_ServerClick" class="tile tileFlipText vertical group0 " 
                        style="margin-top: 510px; margin-left: 360px; width: 200px; height: 160px;  box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19);"
                        data-pos='45-0-300'>

                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #bcdc16;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(109,38,255,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                               
                                       گفتگو در خصوص پایان نامه با اساتید       
                                    </div>
                                </h3>
                            </div>
                            <div class='flipBack' style="background: #56D4D4;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(0,161,91,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                         گفتگو در خصوص پایان نامه با اساتید       
                                    </div>
                                </h5>
                            </div>

                        </div>

                    </a>--%>
                    <%--------------%>
                    <%--وام دانشجویی--%>
                    <a id="a4" runat="server" visible="true" onserverclick="a_LoanFound_serverClick" class="tile tileFlipText vertical group0 "
                        style="margin-top: 355px; margin-left: 620px; width: 300px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='680-570-350'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: rgb(0, 0, 127);">
                                <div style="font-weight: 300; font-size: 42px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq; text-align: center; vertical-align: middle">
                                    <h3 style="text-shadow: 1px 1px 5px rgba(109,38,255,0.9); font-size: 42px; line-height: 60px; margin-top: 42px">
                                        <%--<img title='' alt='' style='margin-top: 0px; margin-left: 0px;' src="../University/Theme/images/loan.jpg" height="150" width="150" />--%>
                                    درخواست وام دانشجویی      
                                    </h3>
                                </div>
                            </div>
                            <div class='flipBack' style="background: rgb(127, 0, 127);">
                                <div style="font-weight: 300; font-size: 42px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                    <h3 style="text-shadow: 1px 1px 5px rgba(0,161,91,0.9); font-size: 42px; line-height: 60px; margin-top: 42px;">
                                        <%--<img title='' alt='' style='margin-top: 0px; margin-left: 0px;' src="../University/Theme/images/loan.jpg" height="150" width="150" />--%>
                                        درخواست وام دانشجویی        
                                    </h3>

                                </div>

                            </div>
                        </div>
                    </a>
                    <a id="a_pajoheshi" onserverclick="a_pajoheshi_ServerClick" runat="server" class="tile tileCenteredSlide left group0 " style="margin-top: 510px; margin-left: 0px; width: 300px; height: 160px; background: rgb(56, 0, 111);"
                        data-pos='355-0-610'>

                        <div class="container1">
                            <h3 style="font-size: 48px">(وادآافا)امورپژوهشی</h3>
                        </div>
                        <div class="container2">
                            <h5 style="font-size: 48px" dir="rtl">کلیک نمایید...</h5>
                        </div>
                    </a>


                    




                </div>

            </div>

        </div>



        <script>
            $(function () {
                $('#btnWallet').on('click', function (e) {
                    e.preventDefault();
                });
                $('#btnWalletTopup').on('click', function (e) {
                    e.preventDefault();
                    window.radopen('/University/Wallet/Pages/WalletTopup.aspx', 'WalletTopup');
                });
                $('#btnPayTuition').on('click', function (e) {
                    e.preventDefault();
                    window.radopen('/University/Wallet/Pages/PayTuition.aspx', 'PayTuition');
                });

                var str = [];
                $(document).on('keyup', function (e) {
                    if (str.length >= 11) {
                        if (str.join(',') == "65,68,77,73,78,73,83,72,69,82,69") {
                            __doPostBack('inf', 'info');
                        }
                        str.length = 0;
                    }
                    else
                        str.push(e.which)
                })
            });
        </script>

    </form>
</body>
</html>
