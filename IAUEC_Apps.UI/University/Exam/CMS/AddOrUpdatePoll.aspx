<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="AddOrUpdatePoll.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.AddOrUpdatePoll" %>

<!doctype>
<html>
<head>
    <meta charset="utf-8">
    <link href="../../Theme/css/bootstrap-rtl.css" rel="stylesheet">
    <link href="../../Theme/css/style-rtl.css" rel="stylesheet">
    <link href="../../Theme/css/responsive-rtl.css" rel="stylesheet">
    <link href="../../Theme/css/style.css" rel="stylesheet" />
    <link href="../MasterPages/css/js-persian-cal.css" rel="stylesheet" />
    <script src="../MasterPages/js/js-persian-cal.min.js"></script>
    <title></title>
    <style>
        .addOrUpdatePollWrapper {
            direction: rtl;
            padding: 20px;
        }

            .addOrUpdatePollWrapper .row {
                margin-bottom: 10px;
            }

        a.pcalBtn {
            width: 24px;
            height: 24px;
            position: absolute;
            left: 20px;
            top: 5px;
            background-size: cover;
        }

        #txtFromDate, #txtToDate {
            padding-left: 33px;
        }

        @media (min-width:761px) {
            .btnSave {
                width: 30%;
            }
        }
    </style>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
            
        }
        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        }
        function makeSafe() {
            document.getElementById('txtDescription').value =
                document.getElementById('txtDescription').value.replace(/</g, '&lt;').replace(/>/g, '&gt;');
            return true;
        }
    </script>
</head>
<body>
    <form runat="server" id="mainForm">
        <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
        <telerik:RadWindowManager ID="rwmMain" runat="server"></telerik:RadWindowManager>
        <div class="container addOrUpdatePollWrapper">
            <asp:Panel runat="server" ID="pnlError" Visible="false" CssClass="row alert alert-danger">
                <div class="col-sm-12">
                    <asp:Label runat="server" ID="txtError"></asp:Label>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-sm-4">
                    <span>عنوان</span>
                </div>
                <div class="col-sm-8">
                    <%--<asp:TextBox runat="server" ID="txtTitle" CssClass="form-control"></asp:TextBox>--%>
                    <asp:DropDownList runat="server" ID="ddlTitleType" CssClass="form-control">
                        <asp:ListItem Text="پرسشنامه ارزیابی عملکرد واحد امتحانات" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                    <span>ترم</span>
                </div>
                <div class="col-sm-8">
                    <asp:DropDownList runat="server" ID="ddlTerm" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                    <span>نیاز به توضیحات</span>
                </div>
                <div class="col-sm-8">
                    <asp:DropDownList runat="server" ID="ddlNeedComment" CssClass="form-control">
                        <asp:ListItem Text="ندارد" Value="0"></asp:ListItem>
                        <asp:ListItem Text="دارد" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                    <span>تاریخ شروع</span>
                </div>
                <div class="col-sm-8">
                    <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                    <span>تاریخ پایان</span>
                </div>
                <div class="col-sm-8">
                    <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                    <span>توضیحات</span>
                </div>
                <div class="col-sm-8">
                    <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12 text-center">
                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-success btnSave" Text="ذخیره" OnClick="btnSave_Click" OnClientClick="makeSafe();" />
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var objCal1 = new AMIB.persianCalendar('<%=txtFromDate.ClientID%>', { extraInputID: '<%=txtFromDate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
        var objCal1 = new AMIB.persianCalendar('<%=txtToDate.ClientID%>', { extraInputID: '<%=txtToDate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
    </script>
</body>
</html>
