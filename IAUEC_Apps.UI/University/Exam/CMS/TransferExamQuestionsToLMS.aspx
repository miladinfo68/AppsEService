<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master"
    CodeBehind="TransferExamQuestionsToLMS.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.TransferExamQuestionsToLMS" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        body {
            direction: rtl;
        }

        .serachBox {
            padding: 15px;
            background: #f5f7fa;
            line-height: 32px;
            position: relative;
            margin-bottom: 15px;
        }

        .over {
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background: rgba(0,0,0,0.1);
            display: none;
            z-index: 2;
        }

        #pnlDone, #pnlTransferring, .validation {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <div id="ttlMsg">
        <h4>انتقال فایل سوالات امتحان به LMS</h4>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <asp:UpdatePanel runat="server" ID="upnl">
        <ContentTemplate>
            <div class="alert alert-danger validation"></div>
            <div class="serachBox">
                <div class="row">
                    <div class="col-sm-1">
                        <span>تاریخ</span>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList runat="server" ID="ddlDate" CssClass="form-control" OnSelectedIndexChanged="ddlDate_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <span>سانس</span>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList runat="server" ID="ddlSans" CssClass="form-control" OnSelectedIndexChanged="ddlSans_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-1">
                        <span>مشخصه</span>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList runat="server" ID="ddlDid" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <div class="col-sm-3">
                        <asp:Button runat="server" ID="btnTransfer" Text="انتقال" CssClass="btn btn-info" OnClientClick="return setUI();" OnClick="btnTransfer_Click" />
                        <asp:HiddenField runat="server" ID="hdnRunFlag" Value="0" />
                    </div>
                </div>
                <div class="over">
                </div>
            </div>
            <div id="pnlTransferring" class="alert alert-info">
                <span>درحال انتقال فایل ها...</span>
            </div>
            <div id="pnlDone">
                <div class="alert alert-success"><span>انتقال فایل ها با موفقیت انجام شد.</span></div>
                <div>
                    <table runat="server" id="tblTransferedDids" class="table table-responsive table-bordered text-center"></table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function setUI() {
            $('.validation').hide();
            if ($('#<%= ddlDate.ClientID %>').val() != '-1') {
                $('#<%= hdnRunFlag.ClientID %>').val('1');
                $('#pnlTransferring').show();
                $('.over').show();
                setInterval(function () {
                    if ($('#<%= hdnRunFlag.ClientID %>').val() == '0') {
                        $('#pnlTransferring').hide();
                        $('#pnlDone').show();
                        $('.over').hide();
                    }
                }, 500);

                return true;
            }
            else {
                $('.validation').html('تاریخ را انتخاب نمایید.');
                $('.validation').show();
                return false;
            }
        }

    </script>
</asp:Content>
