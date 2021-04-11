<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherIntro.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.TeacherIntro" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <link href="<%= bootstrapAddress %>" rel="stylesheet" />
    <%--<link href="css/bootstrap.min.css" rel="stylesheet" />--%>
        <style>
        @font-face {
            font-family: 'IranNastaliq';
            src: url('fonts/IranNastaliq.eot');
            src: url('fonts/IranNastaliq.eot?#iefix') format('embedded-opentype'), url('fonts/IranNastaliq.woff') format('woff'), url('fonts/IranNastaliq.ttf') format('truetype'), url('fonts/IranNastaliq.svg#Sri-TSCRegular') format('svg');
            font-weight: normal;
            font-style: normal;
        }
        </style>
    <link rel="stylesheet" type="text/css" href="../University/Theme/cache/compressed.css" />

    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="fonts/css/font-awesome.min.css" rel="stylesheet" />
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
        .techSupport a{
            color:#f00;
            font-family: 'Arial';
    font-size: 21px;
        }
        .techSupport {
    color: #f00;
    font-weight: bold;
    font-size: 22px;
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
    </script>

    <form id="form1" runat="server" method="post">
        <input type="hidden" runat="server" id="LogStatus" name="LogStatus" style="width: 200px;" />
        <br />

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <telerik:RadWindowManager ID="rwm" runat="server" Width="800px" Height="600px"></telerik:RadWindowManager>
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
                                            <asp:Literal ID="confirmMessage" runat="server" Text="استاد گرامی، لطفا جهت مشاهده آرشیو کلاسهای برگزار شده تا تاریخ 1399/10/19 دکمه 'آرشیو کلاسها' و جهت شرکت در کلاسها دکمه 'ورود به سامانه مدیریت یادگیری' را فشار دهید" />
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
                <div class="col-md-1">
                    
                </div>
                <div class="col-md-7">
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
                <div class="col-sm-3 techSupport">
                        <span>پشتیبانی فنی ویژه اساتید: </span><a href="tel:02142863999">42863999-021</a>
                    </div>
            </div>
            <div id="centerWrapper">
                <div id="tileContainer">
                    <!--<img id="arrowRight"  src="img/Azad_University_logo.png"  alt="right arrow" style="margin-left: 722px; opacity: 0.5; display: inline;width:180px">-->


                    <a id="A1" onserverclick="A1_ServerClick" runat="server" class="tile tileCenteredSlide left group0" style="margin-top: 45px; margin-left: 310px; width: 300px; height: 145px; background: #0b6f81;"
                        data-pos='355-0-610'>

                        <div class="container1">
                            <h3>(وادآفا)امور پژوهش   	</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl">کلیک نمایید...</h5>
                        </div>

                    </a>
                    <%--                 
                        <a id="a_Reservation" onserverclick="a_Reservation_ServerClick" runat="server" class="tile tileCenteredSlide left group0 " style="margin-top: 45px; margin-left: 05px; width: 300px; height: 145px; background: rgb(56, 0, 111);"
                        data-pos='355-0-610'>

                        <div class="container1">
                            <h3>پنل مرتبط با دفاع استاد</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl">کلیک نمایید...</h5>
                        </div>

                    </a>--%>
                    <a id="a_Reservation" onserverclick="a_Reservation_ServerClick" runat="server" class="tile tileCenteredSlide left group0 " style="margin-top: 45px; margin-left: 05px; width: 300px; height: 145px; background: rgb(56, 0, 111);"
                        data-pos='355-0-610'>

                        <div class="container1">
                            <h3>هماهنگی رزرو کلاس   	</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl">کلیک نمایید...</h5>
                        </div>

                    </a>
                     
                    <a id="a_Exam" onserverclick="a_Exam_ServerClick" runat="server" class="tile tileCenteredSlide left group0 " style="margin-top: 190px; margin-left: 05px; width: 300px; height: 145px; background: rgb(0, 145, 241);"
                        data-pos='355-0-610'>

                        <div class="container1">
                            <h3>امتحانات   	</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl">کلیک نمایید...</h5>
                        </div>

                    </a>
                        
                    <a id="EditInfo" onserverclick="EditInfo_ServerClick" runat="server" class="tile tileCenteredSlide left group0 "
                        style="margin-top: 190px; margin-left: 310px; width: 300px; height: 145px; background-color: #1EA775;"
                        data-pos='555-0-610'>

                        <div class="container1">
                            <h3>ویرایش اطلاعات فردی   	</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl">کلیک نمایید...</h5>
                        </div>
                    </a>


                    <a id="ConfirmContract" onserverclick="ConfirmContract_ServerClick" runat="server" class="tile tileCenteredSlide left group0 "
                        style="margin-top: 335px; margin-left: 05px; width: 300px; height: 145px; background-color: #dd00ff;box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='555-0-910'>
                        <!--url('images/beta-fa.png') no-repeat 0 0 -->

                        <div class="container1">
                            <h3>قرارداد و تفاهم نامه</h3>
                        </div>
                        <div class="container2">
                            <h5 dir="rtl">کلیک نمایید...</h5>
                        </div>
                    </a>



                    <a id="a_LMS" runat="server" visible="true" onserverclick="a_LMS_ServerClick" class="tile tileFlipText vertical group0 "
                        style="margin-top: 335px; margin-left: 310px; width: 300px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='45-0-300'>


                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #A173FF;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(109,38,255,0.9)">
                                    <img title='' alt='' style='margin-top: 0px; margin-left: 0px;'
                                        src="../University/Theme/images/leraning.png" height="85" width="90" />
                                    سامانه مدیریت یادگیری-کلاس آنلاین         	</h3>
                            </div>
                            <div class='flipBack' style="background: #00E883;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(0,161,91,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        <img title="" alt="" style="position: relative; vertical-align: middle; top: 0px; margin-right: 10px;"
                                            src="../University/Theme/images/leraning.png" height="85" width="65" />
                                        سامانه مدیریت یادگیری-کلاس آنلاین       
                                    </div>
                                </h5>
                            </div>
                        </div>
                    </a>

                    <a id="headOfDepartmentPanel" runat="server" visible="false" onserverclick="OnEnterToHeadOfDepartmentPanel" class="row tile tileFlipText vertical group0 " style="margin-top: 410px; margin-left: 635px; width: 500px; height: 145px; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='45-0-300'>
                        <div class='flipContainer'>
                            <div class='flipFront' style="background: #FF8000;">
                                <h3 style="text-shadow: 1px 1px 5px rgba(111,59,0,0.9)">ورود به بخش مدیر گروه    	</h3>
                            </div>
                            <div class='flipBack' style="background: #ffbb33;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(255,136,0,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        ورود به بخش مدیر گروه    
                                    </div>
                                </h5>
                            </div>

                        </div>
                    </a>
                    <%--بازی
                    <a id="aGameCenter" runat="server" class="tile tileSlide group0 " onserverclick="aGameCenter_ServerClick"
                        style="margin-top: 480px; margin-left: 310px; width: 300px; height: 145px; background: #dc9d49; box-shadow: 0 8px 12px 0 rgba(0,0,0,.2),0 6px 10px 0 rgba(0,0,0,.19)"
                        data-pos='680-360-200'>
                        <h3 style="font-size: 48px; text-align: center; vertical-align: central; align-content: center; line-height: 100px">سرگرمی</h3>


                    </a>
--%>
                    <telerik:RadWindowManager ID="rwm_message" runat="server"></telerik:RadWindowManager>




                    <a class="tile" style="margin-top: 45px; margin-left: 635px; width: 500px; display: block; text-align: center;" data-pos="45-635-500">
                        <div style="width: 100%">
                            <img src="../University/Theme/images/Azad_University_logo.png" style="width: 200px" />
                        </div>
                        <div style="width: 100%">
                            <h3 style="color: #000000; width: 100%">سامانه خدمات الکترونیکی دانشگاه آزاد اسلامی واحد الکترونیکی  </h3>
                        </div>

                    </a>





                    <a id="btnExamLMS" runat="server" visible="true" onserverclick="btnExamLMS_ServerClick" class="tile tileFlipText vertical group0 "
                        style="margin-top: 480px; margin-left: 5px; width: 300px; height: 145px; "
                        data-pos='45-0-300'>


                        <div class='flipContainer'>
                            <div class='flipFront' style="background: rgb(220, 73, 73);">
                                <h3 style="text-shadow: 1px 1px 5px rgba(109,38,255,0.9)">سامانه آزمون (وادآزما)</h3>
                            </div>
                            <div class='flipBack' style="background: #9d00ff;">
                                <h5 style="text-shadow: 1px 1px 5px rgba(0,161,91,0.9)">
                                    <div style="font-weight: 300; font-size: 36px; padding: 0 0 8px 0; color: #FFF; font-family: IranNastaliq">
                                        سامانه آزمون (وادآزما)
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
