<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TuitionPayment.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.TuitionPayment" %>

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
            background: #e2e2e2;
            border: 1px solid #ddd;
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
            padding: 70px;
        }
    </style>

    <script type="text/javascript">
         function postRefId(refIdValue) {
             var form = document.createElement("form");
             form.setAttribute("method", "POST");
             form.setAttribute("action", "<%= PgwSite %>");
             form.setAttribute("target", "_self");
             var hiddenField = document.createElement("input");
             hiddenField.setAttribute("name", "RefId");
             hiddenField.setAttribute("value", refIdValue);
             form.appendChild(hiddenField);
             document.body.appendChild(form);
             form.submit();
             document.body.removeChild(form);
         }
     </script>
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
                        <asp:Panel ID="pnlWaiting" runat="server" Visible="false">
                            <div>
                                <img src="images/loading.gif" />
                            </div>
                            <div>
                                <span>درحال انتقال به درگاه بانک، لطفاً منتظر بمانید...</span>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlError" Visible="false">
                            <div>
                                <span>خطا در انتقال به درگاه، لطفاً مجدداً تلاش نمایید.</span>
                            </div>
                            <div>
                                <a href="http://automation.iauec.ac.ir/Payments_Students_Gateway.aspx">بازگشت</a>
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
