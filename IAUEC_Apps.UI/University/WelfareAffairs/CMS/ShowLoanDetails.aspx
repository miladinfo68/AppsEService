<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowLoanDetails.aspx.cs" Inherits="IAUEC_Apps.UI.University.WelfareAffairs.CMS.ShowLoanDetails" %>

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

        .itemWrapper {
            background: #ddd;
            padding: 10px;
            margin: 5px;
            border: 1px solid #ccc;
        }

        .paymentsGrid th {
            text-align: right;
        }

        #pnlTotal.bg-danger {
            border: 1px solid #ff9900;
            margin: -20px 0 20px;
        }

        #pnlTotal.bg-success {
            border: 1px solid #00ff99;
            margin: -20px 0 20px;
        }
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
                    <asp:Repeater runat="server" ID="rptDocs" OnItemCommand="rptDocs_ItemCommand" OnItemDataBound="rptDocs_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-sm-4 text-center">
                                <div class="itemWrapper">
                                    <div>
                                        <a href='<%# Eval("DocPath") + "/" + Eval("DocName") + ".jpg" %>' target="_blank">
                                            <img src='<%# Eval("DocPath") + "/" + Eval("DocName") + ".jpg" %>' width="100" />
                                        </a>
                                    </div>
                                    <div>
                                        <asp:label runat="server" ID="lblDoc_Title" Text='<%# Eval("DocTitle") %>' />
                                    </div>
                                    <asp:Panel runat="server" ID="pnlDocButtons" CssClass="pnl">
                                        <asp:Button runat="server" ID="btnAccept" Text="تائید" CssClass="btn btn-success" CommandName="Accept" CommandArgument='<%# Eval("DocId") %>' />
                                        <asp:Button runat="server" ID="btnReject" Text="رد" CssClass="btn btn-danger" CommandName="Reject" />
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlRejectDoc" Visible="false" CssClass="pnl">
                                        <div>
                                            <span>علت رد:</span>
                                        </div>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtDec" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div>
                                            <asp:Button runat="server" ID="btnRejectDoc" CommandName="RejectDoc" CommandArgument='<%# Eval("DocId") %>' Text="رد مدرک" CssClass="btn btn-danger" />
                                            <asp:Button runat="server" ID="btnCancelReject" CommandName="CancelReject" Text="انصراف" CssClass="btn btn-warning" />
                                        </div>
                                    </asp:Panel>
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

                <div class="titleBox">جزیئات مالی دانشجو</div>
                <div class="row">
                    <div class="col-md-12 paymentsGrid">
                        <asp:GridView runat="server" ID="grvPayments" AutoGenerateColumns="false" OnRowDataBound="grvPayments_RowDataBound" CssClass="table table-bordered" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="Term" HeaderText="ترم" />
                                <asp:TemplateField HeaderText="مبلغ قابل پرداخت (ريال)">
                                    <ItemTemplate>
                                        <span><%# string.Format("{0:n0}", Convert.ToInt32(((IAUEC_Apps.DTO.University.WelfareAffairs.PaymentRecord)Container.DataItem).Debt)) %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="مبلغ پرداخت شده (ريال)">
                                    <ItemTemplate>
                                        <span><%# string.Format("{0:n0}", Convert.ToInt32(((IAUEC_Apps.DTO.University.WelfareAffairs.PaymentRecord)Container.DataItem).Deposit)) %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="مانده بدهی (ريال)">
                                    <ItemTemplate>
                                        <span><%# string.Format("{0:n0}", Convert.ToInt32(((IAUEC_Apps.DTO.University.WelfareAffairs.PaymentRecord)Container.DataItem).DebtAmount)) %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Status" HeaderText="وضعیت" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel runat="server" ID="pnlTotal">
                            <div class="row" style="padding: 10px;">
                                <div class="col-md-2">
                                    <span>وضعیت کل: </span>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lblTotalStatus"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <span>مبلغ کل بدهی: </span>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lblTotalDebt"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(window).load(function () {
            $('.paymentsGrid thead').addClass('bg-primary');
        });
    </script>
</body>
</html>
