<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowDocs_StudentLoan.aspx.cs" Inherits="IAUEC_Apps.UI.University.WelfareAffairs.Pages.ShowDocs_StudentLoan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../CommonUI/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../MasterPages/css/custom.css" rel="stylesheet" />
    <style>
        html, body, #form1 {
            height: 100%;
        }

        form#form1::after {
            content: "";
        }

        .ShowLoanDetailsWrapper {
            min-height: 100%;
            direction: rtl;
            background: #eee;
            padding: 0 15px;
        }

        .infoPadding {
            padding: 0 15px;
        }

        .titleBox {
            border-right: 3px solid #ccc;
            border-bottom: 1px solid #ccc;
            padding: 5px 15px;
            color: #333;
            margin: 15px 0;
        }

        .fieldTitle {
            color: #666;
        }

        .pnl {
            margin-top: 10px;
        }
        .itemWrapper{background: #ddd; padding: 10px; margin: 5px;border: 1px solid #ccc;}
        
    </style>
    <script src="../../../CommonUI/js/jquery.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="ShowLoanDetailsWrapper">
            <div class="container">
                <div class="titleBox">اطلاعات درخواست</div>
                <div class="infoPadding">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="row">
                                        <div class="col-sm-4 fieldTitle">نام و نام خانوادگی</div>
                                        <div class="col-sm-8">
                                            <asp:Label runat="server" ID="lblFullName"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row">
                                        <div class="col-sm-4 fieldTitle">شماره دانشجویی</div>
                                        <div class="col-sm-8">
                                            <asp:Label runat="server" ID="lblStCode"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="row">
                                        <div class="col-sm-4 fieldTitle">نوع وام مورد درخواست</div>
                                        <div class="col-sm-8">
                                            <asp:Label runat="server" ID="lblLoanType"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="row">
                                        <div class="col-sm-4 fieldTitle">تاریخ درخواست</div>
                                        <div class="col-sm-8">
                                            <asp:Label runat="server" ID="lblRequestDate"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="titleBox">مدارک</div>
                <div class="row">
                    <asp:Repeater runat="server" ID="rptDocs"  OnItemDataBound="rptDocs_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-sm-4 text-center">
                                <div class="itemWrapper">
                                    <div>
                                        <a href='<%# Eval("DocPath") + "/" + Eval("DocName") + ".jpg?ts=" + DateTime.Now.Ticks %>' target="_blank">
                                            <img src='<%# Eval("DocPath") + "/" + Eval("DocName") + ".jpg?ts=" + DateTime.Now.Ticks %>' width="100" height="100" />
                                        </a>
                                    </div>
                                    <div>
                                        <span><%# Eval("DocTitle") %></span>
                                    </div>                              
                                    <asp:Panel runat="server" ID="pnlDocInfo" Visible="false" CssClass="pnl">
                                        <div>
                                            <asp:Label runat="server" ID="lblDocStatus"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label runat="server" ID="lblDesc"></asp:Label>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="titleBox">مدارک موجود در آرشیو</div>
                <div class="row">
                    <asp:Repeater runat="server" ID="rptArchiveDocs">
                        <ItemTemplate>
                            <div class="col-sm-4 text-center">
                                <div class="itemWrapper">
                                    <div>
                                        <a href='<%# "data:image/jpg;base64, " + Eval("Base64Image") %>' target="_blank">
                                            <img src='<%# "data:image/jpg;base64, " + Eval("Base64Image") %>' width="100" />
                                        </a>
                                    </div>
                                    <div>
                                        <span><%# Eval("CategoryName") %></span>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        window.onload = function () {
            $('.RadWindow').removeAttr('unselectable');
        }
    </script>
</body>
</html>

