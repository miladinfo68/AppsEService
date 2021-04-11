<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WalletTopup.aspx.cs" Inherits="IAUEC_Apps.UI.University.Wallet.Pages.WalletTopup" %>

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

        .walletTopupWrapper {
            margin-top: 15px;
            margin-bottom: 15px;
        }

        .bank {
            display: inline-block;
            width: 84px;
            height: 85px;
            background: url(../../Theme/images/banks.jpg) no-repeat #f00;
            border-radius: 7px;
            margin: 10px 0 6px 11px;
            box-shadow: 0 0 5px 0 rgba(0,0,0,.26);
            cursor: pointer;
            position: relative;
            overflow: hidden;
            text-align: center;
        }

            .bank label {
                opacity: 0;
            }

            .bank.mellat {
                background-position: -337px -80px;
                background-size: 565px;
            }
    </style>

    <script src="../../Theme/js/jquery-3.5.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container walletTopupWrapper">
            <div id="validationError" class="alert alert-danger"></div>
            <asp:Panel runat="server" ID="pnlValidationError" CssClass="alert alert-danger" Visible="false">
                <asp:Label runat="server" ID="lblServerValidationMessage"></asp:Label>
            </asp:Panel>
            <div class="row bankAmount">
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
                <div class="col-sm-3 bankSelectionTitle">درگاه بانکی:</div>
                <div class="col-sm-9 bankSelection">
                    <asp:RadioButtonList runat="server" ID="rblBanks" CssClass="bank mellat">
                        <asp:ListItem Text="بانک ملت" Value="1" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                    <%--<asp:RadioButton runat="server" ID="rbMellatBank" GroupName="selectedBank" Checked="true" />
                        <input type="radio" name="selectedBank" value="1" checked="checked" />--%>
                    <%--<span class="name">بانک ملت</span>--%>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:Button runat="server" ID="btnRequestPayment" CssClass="btn btn-success form-control btnRequestPayment" Text="پرداخت" OnClick="btnRequestPayment_Click" OnClientClick="return validate();" />
                    <%--<a href="#" id="btnRequestPayment" class="btn btn-success form-control">پرداخت</a>--%>
                </div>
            </div>
        </div>


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

                $(".bankAmount").inputFilter(function (value) {
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
                if (!$("input[name='rblBanks']:checked").val())
                    msg += '<div>درگاه بانکی را انتخاب نمایید.</div>';

                if (msg == '') {
                    $('#validationError').hide();
                    return true;
                }
                else {
                    $('.btnRequestPayment').blur();
                    $('#validationError').html(msg);
                    $('#validationError').show();
                    return false;
                }
            }
        </script>

    </form>
</body>
</html>
