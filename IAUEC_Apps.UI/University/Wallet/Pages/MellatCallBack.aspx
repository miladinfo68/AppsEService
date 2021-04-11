<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MellatCallBack.aspx.cs" Inherits="IAUEC_Apps.UI.University.Wallet.Pages.MellatCallBack" MasterPageFile="~/University/Wallet/MasterPages/WalletPages.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        .boxWrapper{
            position: relative;
            height:250px;
        }
        .successBox {
            width: 45%;
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            margin: auto;
            line-height: 32px;
            font-size: 16px;
        }
        .btnReturn {
            margin-top: 30px;
        }
        .messageTitle{
            margin-bottom: 30px;
        }
    </style>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="bodyPlaceHolder">
    <div class="boxWrapper">
        <asp:Panel runat="server" ID="pnlResult" Visible="false">
        <div class="row">
            <div class="col-sm-12">
                <h4 class="text-center messageTitle">
                    <asp:Label runat="server" ID="lblMessage"></asp:Label></h4>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlDetails" Visible="false">
            <div class="row">
            <div class="col-sm-3">
                شناسه سفارش:
            </div>
            <div class="col-sm-9">
                <asp:Label runat="server" ID="lblOrderId"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                کد رهگیری:
            </div>
            <div class="col-sm-9">
                <asp:Label runat="server" ID="lblTraceNumber"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                زمان پرداخت:
            </div>
            <div class="col-sm-9">
                <asp:Label runat="server" ID="lblDateTime"></asp:Label>
            </div>
        </div>
        </asp:Panel>
        <div class="text-center btnReturn">
            <a href="/CommonUI/IntroPage.aspx" class="btn btn-secondary">بازگشت</a>
        </div>
    </asp:Panel>
    </div>

</asp:Content>
