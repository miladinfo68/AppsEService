<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertRejectDesc.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.InsertRejectDesc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />


    <!-- Bootstrap core CSS -->

    <link href="../../../CommonUI/css/bootstrap.min.css" rel="stylesheet" />

    <link href="../../../CommonUI/fonts/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../../CommonUI/css/animate.min.css" rel="stylesheet" />

    <!-- Custom styling plus plugins -->
    <link href="css/custom.css" rel="stylesheet" />

    <style>
        .row {
            margin-top: 15px;
        }
        .reasonBox{border: 1px solid #ccc; padding: 15px; margin: 15px 0; border-radius: 5px;}
        .reasonTitle{color:#9B59B6; font-weight:bold; font-family:'B Nazanin'; font-size:18px;}
        @media(min-width:769px) {
    .col-md-5,
    .col-xs-1,
    .col-sm-1,
    .col-md-1,
    .col-lg-1,
    .col-xs-2,
    .col-sm-2,
    .col-md-2,
    .col-lg-2,
    .col-xs-3,
    .col-sm-3,
    .col-md-3,
    .col-lg-3,
    .col-xs-4,
    .col-sm-4,
    .col-md-4,
    .col-lg-4,
    .col-xs-5,
    .col-sm-5,
    .col-md-5,
    .col-lg-5,
    .col-xs-6,
    .col-sm-6,
    .col-md-6,
    .col-lg-6,
    .col-xs-7,
    .col-sm-7,
    .col-md-7,
    .col-lg-7,
    .col-xs-8,
    .col-sm-8,
    .col-md-8,
    .col-lg-8,
    .col-xs-9,
    .col-sm-9,
    .col-md-9,
    .col-lg-9,
    .col-xs-10,
    .col-sm-10,
    .col-md-10,
    .col-lg-10,
    .col-xs-11,
    .col-sm-11,
    .col-md-11,
    .col-lg-11,
    .col-xs-12,
    .col-sm-12,
    .col-md-12,
    .col-lg-12 {
        float: right;
    }
}
    </style>
</head>
<body>
    <script>
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseModal(args) {

            setTimeout(function () {
                GetRadWindow().BrowserWindow.refreshGrid(args);
                GetRadWindow().close();

            }, 3000);

        }

    </script>

    <form id="form1" runat="server">

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>

        <div id="div_pnl" runat="server" class="container">
            <asp:Panel ID="Confirmpnl" runat="server" Visible="false" HorizontalAlign="Center" CssClass="row">
                <div class="col-sm-12" style="border: 1px solid #B90000; border-radius: 5px; padding-bottom: 8px;">
                    <p style="color: #FF0000; font-weight: bold">آیا مطمئن هستید؟</p>
                    <asp:Button ID="btn_conf" runat="server" CssClass="btn btn-success" Text="بله" Font-Names="Tahoma" OnClick="conf_Click"></asp:Button>
                    <asp:Button ID="btn_notConf" runat="server" Text=" خیر " CssClass="btn btn-danger" Font-Names="tahoma" OnClick="notConf_Click"></asp:Button>
                </div>
            </asp:Panel>
        </div>
        <div dir="rtl" id="div_Main" runat="server" class="container">

            <div class="row">
                <div class="col-sm-12">
                    <span class="reasonTitle">علت رد</span>
                </div>
            </div>

            <div class="reasonBox">

                <div class="row">
                    <div class="col-sm-3">
                        <span>انتخاب متن پیش فرض</span>
                    </div>
                    <div class="col-sm-9">
                        <asp:DropDownList runat="server" ID="ddlPreDefinedReasons" OnSelectedIndexChanged="ddlPreDefinedReasons_SelectedIndexChanged"
                            AutoPostBack="true" CssClass="form-control">
                            <asp:ListItem Text="---" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="با سلام و احترام، استاد گرامی لطفا سوالات خود را در فرمت مخصوص دانلود شده از سامانه قرار دهید. با تشکر" Value="1"></asp:ListItem>
                            <asp:ListItem Text="با سلام و احترام، استاد گرامی لطفا سربرگ سوالات خود را اصلاح نمایید. باتشکر" Value="2"></asp:ListItem>
                            <asp:ListItem Text="با سلام و احترام، استاد گرامی لطفا بخش ״تعیین شرایط آزمون״ را اصلاح نمایید. باتشکر" Value="3"></asp:ListItem>
                            <asp:ListItem Text="با سلام و احترام، استاد گرامی لطفا شماره گذاری سوالات خود را اصلاح نمایید. باتشکر" Value="4"></asp:ListItem>
                            <asp:ListItem Text="با سلام و احترام، استاد گرامی سوالات شما از کادر خارج شده است، لطفا اصلاح نمایید. باتشکر" Value="5"></asp:ListItem>
                            <asp:ListItem Text="با سلام و احترام، استاد گرامی لطفا مدت زمان آزمون را اصلاح نمایید.(ماكزيمم زمان امتحانات 90 دقيقه ميباشد.) باتشکر" Value="6"></asp:ListItem>
                            <asp:ListItem Text="با سلام و احترام، استاد گرامی لطفا سایز نوشتاری سوالات خود را درشت تر نمایید. باتشکر" Value="7"></asp:ListItem>
                            <asp:ListItem Text="با سلام و احترام، استاد گرامی سوالات شما قابل مشاهده نمی باشد، لطفا مجدداً بارگذاری نمایید. باتشکر" Value="8"></asp:ListItem>
                            <asp:ListItem Text="با سلام و احترام، استاد گرامی بخش پیوست سوالات شما قابل مشاهده نمی باشد، لطفا مجدداً بارگذاری نمایید. باتشکر" Value="9"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <span>متن پیام</span>
                    </div>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txt_Reject" runat="server" ForeColor="Black" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="btn_Save" runat="server" Text="ثبت" OnClick="btn_Save_Click" BackColor="#9B59B6" Width="100px" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="White" CssClass="btn btn-default" />
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-sm-12">
                    <asp:Label ID="lbl_Resault" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:Label ID="lbl_Status" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
