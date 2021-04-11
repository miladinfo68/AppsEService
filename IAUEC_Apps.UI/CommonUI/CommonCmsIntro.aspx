<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommonCmsIntro.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.CommonCmsIntro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="<%= bootstrapAddress %>" rel="stylesheet" />
    <script src="../University/Theme/js/jquery.min.js"></script>

    <link rel="stylesheet" type="text/css" href="../University/Theme/cache/compressed.css" />
    
    <link href="fonts/css/font-awesome.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>


    <style>
        html {
            background-color: #0F6D32;
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
    </style>
    <noscript><style>
                  #tileContainer {
                      display: block;
                  }
              </style></noscript>
</head>
<body class="full desktop">
    <script type="text/javascript">
        function submitform() {
            document.forms["form1"].submit();
        }
    </script>
    <script type="text/javascript">
        function openLmsModal() {
            $('#lmsModal').modal('show');
        }
        function closeLmsModal() {
            $('#lmsModal').modal('hide');
        }
    </script>
    <form id="form1" runat="server">
        <input type="hidden" runat="server" id="userCode" name="userCode" style="width: 200px;" />
        <input type="hidden" runat="server" id="e_Code" name="e_Code" style="width: 200px;" />
        <input type="hidden" runat="server" id="e_Status" name="e_Status" style="width: 200px;" />
        <%--   <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <input type="hidden" runat="server" id="LogStatus" name="LogStatus" style="width: 200px;" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>



        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
        <img src="../University/Theme/images/metro_green_angular_low_res.jpg" alt='background-image' id='bgImage' />
        <div id="wrapper">
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
                                                <br />
                                                <asp:Literal ID="confirmMessage" runat="server" Text="مدیر گروه گرامی، لطفا جهت مشاهده آرشیو کلاسهای برگزار شده تا تاریخ 1399/10/19 دکمه 'آرشیو کلاسها' و جهت شرکت در کلاسها دکمه 'ورود به سامانه مدیریت یادگیری' را فشار دهید" />
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Button runat="server" Height="70px" OnClick="lmsNew_Click" Text="ورود به سامانه مدیریت یادگیری-کلاس آنلاین " CssClass="form-control" ForeColor="White" BackColor="#0123a5" Font-Size="X-Large" ID="lmsNew" />

                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Button runat="server" Height="70px" OnClick="lmsOld_Click" Text="مشاهده آرشیو کلاسها" CssClass="form-control" ForeColor="White" BackColor="#01a516" Font-Size="X-Large" ID="lmsOld" />

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
                <div class="col-md-11">
                    <ul class="nav nav-pills right">
                        <li style="display: block; background-color: blue;">
                            <a id="ChangePassword" runat="server" onserverclick="ChangePassword_ServerClick">
                                <i class="fa fa-key" style="color: whitesmoke"></i>
                            </a>
                        </li>
                        <li style="display: block; background-color: red">
                            <a id="Logout" runat="server" onserverclick="Logout_ServerClick">
                                <i class="fa fa-sign-out" style="color: whitesmoke"></i>

                            </a>
                        </li>
                        <li><span style="color: white">---</span></li>
                        <li><span style="font-size: 16px" id="usernamelbl" runat="server"></span></li>
                    </ul>

                </div>
            </div>
            <div id="centerWrapper">
                <div id="tileContainer">


                    <%--پست الکترونیکی--%>
                    <a id="Apps2" runat="server" onserverclick="Apps2_ServerClick" class="tile tileFlipText vertical group0 " style="margin-top: 45px; margin-left: 0px; width: 200px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='45-0-300'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #FF8000;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(111,59,0,0.9)">
                                    <img title='' alt='' style='margin-top: 0px; margin-left: 0px;'
                                        src="../University/Theme/images/pen.png" height="60" width="60" />
                                    پست الکترونیکی    	</h3>
                            </div>
                            <div class='flipBack' style="background: #ffbb33;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(255,136,0,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 10px;" src="../University/Theme/images/pen2.png" height="50" width="50">
                                        پست الکترونیکی    
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>

                    <%--خدمات اساتید--%>
                    <a id="Apps7" runat="server" onserverclick="Apps7_ServerClick" class="tile tileCenteredSlide left group0 " style="margin-top: 45px; margin-left: 205px; width: 200px; height: 145px; background: rgb(195, 0, 69); box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='355-0-610'>

                        <div class="container1">
                            <h3 style="text-shadow: 1px 1px 5px rgba(91,0,48,0.9)">خدمات اساتید و دانشجویان   	</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl" style="text-shadow: 1px 1px 5px rgba(91,0,48,0.9)">کلیک نمایید...</h5>
                        </div>

                    </a>
                    <%--ویرایش اطلاعات فردی--%>
                    <a id="Apps4" runat="server" onserverclick="Apps4_ServerClick" class="tile tileSlideshow group0 " style="margin-top: 45px; margin-left: 410px; width: 200px; height: 145px; background: #6950ab; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='45-310-300' data-n="0">

                        <div class='imgWrapperBack' style="width: 200px; height: 145px">
                            <img src='empty' alt='' />
                        </div>
                        <div class='imgWrapper' style="width: 200px; height: 145px">
                            <img src="../University/Theme/images/tiles.png" alt='' />
                        </div>

                        <div class='imgText'>ویرایش : آدرس-کدپستی-تلفن-عکس پرسنلی-موبایل</div>
                        <script>slideshowTiles["tileSlideshow0-2-0"] = [["img\/features\/tiles.png", "img\/features\/modern.png", "img\/features\/seo.png", "img\/features\/devices.png"], ["", "", "", ""], ["Many types of tiles", "Using the latest technology", "SEO optimized", "Responsive Webdesign"], "slide-right, slide-up, slide-down,slide-left", 4000]</script>

                        <div class='tileLabelWrapper top' style='border-top-color: #FF8000;'>
                            <div class='tileLabel top'></div>
                        </div>
                    </a>
                    <%--کارت دانشجویی--%>
                    <a id="Apps5" runat="server" onserverclick="Apps5_ServerClick" class="tile tileCenteredSlide left group0 " style="margin-top: 200px; margin-left: 0px; width: 200px; height: 145px; background: rgb(51, 181, 229); box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='200-310-300'>

                        <div class="container1">
                            <h3 style="text-shadow: 1px 1px 5px rgba(0,57,78,0.9)">
                                <img title='' alt='' style='margin-top: 0px; margin-left: 0px;'
                                    src="../University/Theme/images/box_info.png" height="100" width="100" />


                                دریافت کارت دانشجویی    	</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl" style="text-shadow: 1px 1px 5px rgba(0,57,78,0.9)">جهت بررسی ارسال کارت دانشجویی از طریق پست اینجا کلیک نمایید...</h5>
                        </div>

                    </a>
                    <%--<%-- شهریه --%>
                    <%--<a id="tuitional" runat="server" onserverclick="tuitional_ServerClick" class="tile tileFlipText vertical group0 " style="margin-top: 200px; margin-left: 205px; width: 200px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-310'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: rgba(237, 0, 255, 0.86)">
                                
                                <h3 style="text-shadow: 1px 1px 5px rgba(237, 0, 255, 0.86)"><span class="fa fa-money"></span> شهریه</h3>
                            </div>
                            <div class='flipBack' style="background: rgb(185, 149, 187);color:rgba(237, 0, 255, 0.86);">
                                <h5 style="text-shadow: 1px 1px 5px rgb(206, 175, 208); ">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        شهریه - بنیاد و بهزیستی         
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>--%>

                    <%-- شهریه --%>
                    <a id="tuitional" runat="server" onserverclick="tuitional_ServerClick" class="tile tileFlipText vertical group0 " style="margin-top: 200px; margin-left: 205px; width: 200px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-310'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #137258">

                                <h3 style="text-shadow: 1px 1px 5px rgba(4, 148, 109, 0.30)"><span class="fa fa-money"></span>کیف پول</h3>
                            </div>
                            <div class='flipBack' style="background: #b6e2d6;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(142, 247, 218, 0.80);">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #137258; font-family: IranNastaliq">
                                        کیف پول - شهریه - بنیاد و بهزیستی         
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>

                    <%--گواهی اشتغال--%>
                    <a id="Apps3" runat="server" onserverclick="Apps3_ServerClick" class="tile tileFlipText horizontal group0  " style="margin-top: 200px; margin-left: 410px; width: 200px; height: 145px; background: #333; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='45-0-300'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #2962ff;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(88,31,203,0.9); font-size: x-large">گواهی اشتغال به تحصیل

                                </h3>
                            </div>
                            <div class='flipBack' style="background: #000000;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(1,137,78,0.9)">
                                    <div style="font-weight: 200; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        ارسال گواهی اشتغال از طریق پست          
                                    </div>
                                </h5>
                            </div>

                        </div>

                    </a>


                    <%--امتحانات--%>
                    <a id="A6" runat="server" class="tile tileCenteredSlide left group0 " onserverclick="A6_ServerClick" style="margin-top: 355px; margin-left: 0px; width: 200px; height: 145px; background: rgb(94, 53, 177); box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='200-310-300'>

                        <div class="container1">
                            <h3 style="text-shadow: 1px 1px 5px rgba(74,20,140,0.9)">امتحانات    	</h3>
                        </div>
                        <div class="container2">
                            <h5 style="text-shadow: 1px 1px 5px rgba(74,20,140,0.9)" dir="rtl">کلیک نمایید...</h5>
                        </div>

                    </a>

                    <%--مديريت كلاس ها و فايل ها--%>
                    <a id="Apps1" runat="server" onserverclick="Apps1_ServerClick" class="tile tileCenteredSlide left group0 " style="margin-top: 355px; margin-left: 205px; width: 200px; height: 145px; background: rgb(255, 68, 68); box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-810'>

                        <div class="container1">
                            <h3 style="text-shadow: 1px 1px 5px rgba(204,0,0,0.9)">مديريت كلاس ها و فايل ها    	</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl" style="text-shadow: 1px 1px 5px rgba(204,0,0,0.9)">کلیک نمایید...</h5>
                        </div>

                    </a>

                    <%--تسویه حساب--%>
                    <a id="a_CheckOut" runat="server" onserverclick="a_CheckOut_ServerClick" class="tile tileFlipText vertical group0 " style="margin-top: 355px; font-size: x-small; margin-left: 410px; width: 200px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='45-0-300'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #00C851;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(0,126,51,0.9)">
                                    <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 10px;" src="images/Checkout.png" height="90" width="90">
                                    تسویه حساب        	</h3>
                            </div>
                            <div class='flipBack' style="background: #aa66cc;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(153,51,204,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 10px;" src="images/Checkout.png" height="90" width="90">
                                        تسویه حساب          
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>

                    <%--سامانه مدیریت یادگیری--%>
                    <a id="lmsLink" runat="server" onserverclick="lmsLink_ServerClick" class="tile tileCenteredSlide left group0 " style="margin-top: 510px; margin-left: 0px; width: 200px; height: 145px; background: rgb(73, 221, 162); box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-810'>

                        <div class="container1">
                            <h3 style="text-shadow: 1px 1px 5px rgba(45, 135, 100, 0.90)">سامانه مدیریت یادگیری</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl" style="text-shadow: 1px 1px 5px rgba(45, 135, 100, 0.90)">کلیک نمایید...</h5>
                        </div>

                    </a>



                    <%--پورتال پژوهش--%>
                    <a id="A1" runat="server" onserverclick="A1_ServerClick" class="tile tileFlipText vertical group0 " style="margin-top: 510px; margin-left: 205px; width: 200px; height: 145px; background: #333; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-210'>
                        <div class='flipContainer' style='' dir="rtl">
                            <div class='flipFront' style="background: #f29c00">

                                <img src="../University/Theme/images/metro_slide_Portal_200x145.jpg" alt="" title="" />
                            </div>
                            <div class='flipBack'>
                                <img src="../University/Theme/images/metro_slide_Portal_200x145.jpg" alt="" title="" />
                            </div>
                        </div>

                    </a>

                    <%--LOGO--%>
                    <a id="aNormal" runat="server" visible="true" class="tile" style="margin-top: 45px; margin-left: 615px; width: 500px; display: block; text-align: center;" data-pos="45-635-500">
                        <div style="width: 100%">
                            <img src="../University/Theme/images/Azad_University_logo.png" style="width: 100px" />
                        </div>
                        <div style="width: 100%">
                            <h3 style="color: #000000; width: 100%">سامانه خدمات الکترونیکی دانشگاه آزاد اسلامی واحد الکترونیکی  </h3>
                        </div>

                    </a>

                    <%--ورود به بخش استاد--%>
                    <a id="headOfDepartmentPanel" runat="server" visible="False" onserverclick="headOfDepartmentPanel_OnServerClick" class="tile tileFlipText horizontal group0 " style="margin-top: 260px; margin-left: 620px; width: 500px; height: 84px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-310'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #FF8000;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(111,59,0,0.9)">ورود به بخش استاد        	</h3>
                            </div>
                            <div class='flipBack' style="background: #ffbb33;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(255,136,0,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        ورود به بخش استاد          
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>


                    <%--LOGO--%>
                    <a id="aPrivate" runat="server" visible="false" class="tile tileFlipText horizontal group0 " style="margin-top: 45px; margin-left: 615px; width: 500px; height: 300px; display: block; text-align: center;" data-pos="45-635-500">
                        <div class='flipContainer'>
                            <div class='flipFront'>
                                <div style="width: 100%">
                                    <img runat="server" id="imgFrontLogo" src="../University/Theme/images/Azad_University_logo.png" style="width: 100px" />
                                </div>
                                <div style="width: 100%">
                                    <h3 style="color: #000000; width: 100%">فرا رسیدن سال 1398 بر شما تهنیت باد</h3>
                                </div>
                            </div>
                            <div class='flipBack'>
                                <h5 style="text-shadow: 1px 1px 5px rgba(88,31,203,0.9); background-color: rgba(0, 0, 0, 0.00);">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0; font-family: IranNastaliq">
                                        <img runat="server" id="imgBackLogo" style="width: 100%; height: 300px; display: inline-flex" />

                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>
                    <%--گزارشات  سیدا--%>
                    <a id="a_Reports" runat="server" onserverclick="a_Reports_ServerClick" class="tile tileFlipText horizontal group0 " style="margin-top: 355px; margin-left: 620px; width: 245px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-310'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #A173FF;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(88,31,203,0.9)">
                                    <img title='' alt='' style='margin-top: 0px; margin-left: 0px;'
                                        src="../University/Theme/images/report-icon.png" height="50" width="50" />
                                    گزارشات  سیدا        	</h3>
                            </div>
                            <div class='flipBack' style="background: #00E883;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(1,137,78,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 5px;" src="../University/Theme/images/report-icon.png" height="50" width="50">
                                        گزارشات  سیدا          
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>

                    <%--کارگزینی هیات علمی--%>
                    <a id="a_HR" runat="server" onserverclick="a_HR_ServerClick" class="tile tileFlipText vertical group0 " style="margin-top: 355px; margin-left: 875px; width: 245px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-310'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #B2E500">
                                <h3 style="text-shadow: 1px 1px 5px rgba(29,83,7,0.9)">کارگزینی هیات علمی        	</h3>
                            </div>
                            <div class='flipBack' style="background: #ffd600;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(186,66,1,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        کارگزینی هیات علمی          
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>

                    <%--رزرواسیون--%>
                    <a id="a_reservation" runat="server" onserverclick="a_reservation_ServerClick" class="tile tileFlipText horizontal group0 " style="margin-top: 510px; margin-left: 410px; width: 200px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-310'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #00E883;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(1,137,78,0.9)">
                                    <img title='' alt='' style='margin-top: 0px; margin-left: 0px;'
                                        src="images/class-reservation.png" height="70" width="70" />
                                    هماهنگی رزرو کلاس       	</h3>
                            </div>
                            <div class='flipBack' style="background: #A173FF;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(88,31,203,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 10px;" src="images/class-reservation.png" height="70" width="70">
                                        هماهنگی رزرو کلاس          
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>


                    <%--سامانه ارزشیابی اساتید--%>
                    <a id="btnProfessorEvaluation" runat="server" onserverclick="btnProfessorEvaluation_ServerClick" class="tile tileCenteredSlide left group0"
                        style="margin-top: 510px; margin-left: 620px; width: 245px; height: 145px; background: rgb(0, 149, 255); box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-810'>
                        <div class="container1">
                            <h3 style="text-shadow: 1px 1px 5px rgba(45, 135, 100, 0.90)">سامانه ارزشیابی اساتید و واحد</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl" style="text-shadow: 1px 1px 5px rgba(45, 135, 100, 0.90)">کلیک نمایید...</h5>
                        </div>
                    </a>

                    <%--صندوق رفاه--%>
                    <a id="a_WelfareAffair" runat="server" onserverclick="a_WelfareAffair_ServerClick" class="tile tileFlipText vertical group0 " style="margin-top: 510px; margin-left: 875px; width: 245px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-310'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: rgb(0,0,127)">
                                <h3 style="text-shadow: 1px 1px 5px rgba(29,83,7,0.9)">صندوق رفاه        	</h3>
                            </div>
                            <div class='flipBack' style="background: rgb(127,0,127);">
                                <h5 style="text-shadow: 1px 1px 5px rgba(186,66,1,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        صندوق رفاه          
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>



                </div>


            </div>

        </div>


    </form>

</body>
</html>
