<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TuitionPaymentCallback.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.TuitionPaymentCallback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/commonui/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        @font-face {
            font-family: IRANSans;
            font-style: normal;
            font-weight: bold;
            src: url('fonts/IranSans/IRANSansWeb(FaNum)_Bold.eot');
            src: url('fonts/IranSans/IRANSansWeb(FaNum)_Bold.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
            url('fonts/IranSans/IRANSansWeb(FaNum)_Bold.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
            url('fonts/IranSans/IRANSansWeb(FaNum)_Bold.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
            url('fonts/IranSans/IRANSansWeb(FaNum)_Bold.ttf') format('truetype');
        }

        @font-face {
            font-family: IRANSans;
            font-style: normal;
            font-weight: 500;
            src: url('fonts/IranSans/IRANSansWeb(FaNum)_Medium.eot');
            src: url('fonts/IranSans/IRANSansWeb(FaNum)_Medium.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
            url('fonts/IranSans/IRANSansWeb(FaNum)_Medium.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
            url('fonts/IranSans/IRANSansWeb(FaNum)_Medium.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
            url('fonts/IranSans/IRANSansWeb(FaNum)_Medium.ttf') format('truetype');
        }

        @font-face {
            font-family: IRANSans;
            font-style: normal;
            font-weight: 300;
            src: url('fonts/IranSans/IRANSansWeb(FaNum)_Light.eot');
            src: url('fonts/IranSans/IRANSansWeb(FaNum)_Light.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
            url('fonts/IranSans/IRANSansWeb(FaNum)_Light.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
            url('fonts/IranSans/IRANSansWeb(FaNum)_Light.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
            url('fonts/IranSans/IRANSansWeb(FaNum)_Light.ttf') format('truetype');
        }

        @font-face {
            font-family: IRANSans;
            font-style: normal;
            font-weight: 200;
            src: url('fonts/IranSans/IRANSansWeb(FaNum)_UltraLight.eot');
            src: url('fonts/IranSans/IRANSansWeb(FaNum)_UltraLight.eot?#iefix') format('embedded-opentype'), /* IE6-8 */
            url('fonts/IranSans/IRANSansWeb(FaNum)_UltraLight.woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
            url('fonts/IranSans/IRANSansWeb(FaNum)_UltraLight.woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
            url('fonts/IranSans/IRANSansWeb(FaNum)_UltraLight.ttf') format('truetype');
        }

        @font-face {
            font-family: IRANSans;
            font-style: normal;
            font-weight: normal;
            src: url('fonts/IranSans/IRANSansWeb(FaNum).eot');
            src: url('fonts/IranSans/IRANSansWeb(FaNum).eot?#iefix') format('embedded-opentype'), /* IE6-8 */
            url('fonts/IranSans/IRANSansWeb(FaNum).woff2') format('woff2'), /* FF39+,Chrome36+, Opera24+*/
            url('fonts/IranSans/IRANSansWeb(FaNum).woff') format('woff'), /* FF3.6+, IE9, Chrome6+, Saf5.1+*/
            url('fonts/IranSans/IRANSansWeb(FaNum).ttf') format('truetype');
        }


        html, body, form {
            width: 100%;
            height: 100%;
        }

        .container-fluid {
            width: 100%;
            height: 100%;
        }

        body {
            direction: rtl;
            text-align: right;
            font-family: IRANSans;
        }

        .headerBox {
            background: #4f5a67;
            font-size: 18px;
            line-height: 50px;
            color: #fff;
        }

        .mainBoxWrapper {
            width: 100%;
            height: 85%;
        }

        .mainBox {
            position: relative;
            width: 100%;
            height: 100%;
        }

        .waitingBox {
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            max-width: 450px;
            height: 250px;
            margin: auto;
            text-align: center;
        }

            .waitingBox img {
                width: 275px;
            }

        .footerBox {
            position: fixed;
            background: #4f5a67;
            width: 100%;
            bottom: 0;
            color: white;
            text-align: center;
            height: 50px;
            border-top: 1px solid #eeeeee;
            padding-top: 15px;
            z-index: 1002;
        }

        #pnlError {
            width: 100%;
            height: 100%;
        }

        #pnlSuccess h6, #pnlError h6 {
            text-align: center;
            border-bottom: 1px solid #28a745;
            padding-bottom: 15px;
            margin-bottom: 15px;
        }
        #pnlSuccess .row {
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row headerBox">
                <div class="col-sm-2">
                    <span>دانشگاه آزاد اسلامي</span>
                </div>
                <div class="col-sm-10"></div>
            </div>
            <div class="row mainBoxWrapper">
                <div class="col-sm-12 mainBox">
                    <div class="waitingBox">
                        <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
                            <style>
                                .waitingBox{text-align: right;}
                            </style>
                            <div class="alert alert-success">
                                <h6>ﺗﺮﺍﻛﻨﺶ ﺑﺎ ﻣﻮﻓﻘﻴﺖ ﺍﻧﺠﺎﻡ ﺷﺪ</h6>
                                <div class="row">
                                    <div class="col-sm-4">شناسه سفارش</div>
                                    <div class="col-sm-8"><asp:Label runat="server" ID="lblOrderId"></asp:Label></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">کد رهگیری</div>
                                    <div class="col-sm-8"><asp:Label runat="server" ID="lblTraceNumber"></asp:Label></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">تاریخ پرداخت</div>
                                    <div class="col-sm-8"><asp:Label runat="server" ID="lblPaymentDate"></asp:Label></div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 text-center">
                                        <a class="btn btn-success" href="http://automation.iauec.ac.ir/vazmali.aspx">ادامه</a>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlError" Visible="false">
                            <div class="alert alert-danger">
                                <h6>خطا در انجام تراکنش</h6>
                            <div style="margin-bottom: 15px;">
                                <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
                            </div>
                            <div>
                                <a href="http://automation.iauec.ac.ir/Payments_Students_Gateway.aspx">بازگشت</a>
                            </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="row footerBox">
                <div class="col-sm-12">
                    <a href="http://iauec.ac.ir" style="color: White; font-size: 13pt;">©1397 دانشگاه آزاد اسلامی واحد الکترونیکی</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
