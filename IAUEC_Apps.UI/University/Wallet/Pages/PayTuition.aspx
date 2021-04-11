<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayTuition.aspx.cs" Inherits="IAUEC_Apps.UI.University.Wallet.Pages.PayTuition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Theme/lib/bootstrap-4.3.1-dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            direction: rtl;
            text-align: right;
        }

        .payTuitionWrapper {
            margin-top: 15px;
            margin-bottom: 15px;
        }
        .btnRequestPayment{
            margin-top: 15px;
        }
    </style>

    <script src="../../Theme/js/jquery-3.5.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container payTuitionWrapper">
            <div id="validationError" class="alert alert-danger"></div>
            <asp:Panel runat="server" ID="pnlValidationError" CssClass="alert alert-danger" Visible="false">
                <asp:Label runat="server" ID="lblServerValidationMessage"></asp:Label>
            </asp:Panel>
            <div class="row tuitionAmount">
                <div class="col-sm-3">
                    <span>مبلغ:</span>
                </div>
                <div class="col-sm-8">
                    <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control amountToPay" MaxLength="11"></asp:TextBox>
                </div>
                <div class="col-sm-1">
                    <span>ريال</span>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:Button runat="server" ID="btnRequestPayment" CssClass="btn btn-success form-control btnRequestPayment" Text="پرداخت با کیف پول" OnClick="btnRequestPayment_Click" OnClientClick="return validate();" />
                </div>
            </div>
        </div>
    </form>
    <script>
        $(function () {
            $('#validationError').hide();
            var clearOldValue = false;
                function numberWithCommas(x) {
                    var parts = x.toString().split(".");
                    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    return parts.join(".");
                }
                $.fn.inputFilter = function (inputFilter, selector) {
                    return this.on("input keydown keyup mousedown mouseup select contextmenu drop", selector, function () {

                        if (clearOldValue) {
                            this.oldValue = '';
                            clearOldValue = false;
                        }
                        if (inputFilter(this.value)) {
                            this.value = this.value.replace(/,/g, '');
                            this.value = numberWithCommas(this.value);
                            if (parseInt(this.value.replace(/,/g, '')) == 0)
                                this.value = 0;
                            this.value = this.value.replace(/^0+/, '');
                            this.oldValue = this.value;
                            this.oldSelectionStart = this.selectionStart;
                            this.oldSelectionEnd = this.selectionEnd;
                        } else if (this.hasOwnProperty("oldValue")) {
                            this.value = this.oldValue;
                            this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                        }
                    });
            };
            $(".tuitionAmount").inputFilter(function (value) {
                    if (/^(\s*|\d+)+(?:,\d+)*$/.test(value)) {
                        return true;
                    }
                    return false;
                }, '.amountToPay');
        });
        function validate() {
                var msg = '';
                if ($('.amountToPay').val() == '')
                    msg += '<div>مبلغ را وارد نمایید.</div>';

                if (msg == '') {
                    $('#validationError').hide();
                    if (confirm("مبلغ " + $('.amountToPay').val() + "ريال بابت پرداخت شهریه از کیف پول شما کسر خواهد گردید."))
                        return true;
                    else
                        return false;
                }
                else {
                    $('.btnRequestPayment').blur();
                    $('#validationError').html(msg);
                    $('#validationError').show();
                    return false;
                }
            }
    </script>
</body>
</html>
