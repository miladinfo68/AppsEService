<%@ Page Language="C#" AutoEventWireup="true" Async="false" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master"
    CodeBehind="ExamQuestionPdfDownlowd.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ExamQuestionPdfDownlowd" %>

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

        .d-none {
            display: none;
        }
        .talign-center{
            text-align:center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="serachBox">
        <div class="alert alert-danger validation d-none talign-center"></div>
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
                <asp:Button runat="server" ID="btnDownloadPdfExamQuestion" Text="دانلود" CssClass="btn btn-info" OnClientClick="return validate();" OnClick="btnDownloadPdfExamQuestion_Click" />
            </div>
        </div>
    </div>
    <script>
        function validate() {
            if ($('#<%= ddlDate.ClientID %>').val() != '-1' && $('#<%= ddlSans.ClientID %>').val() != '-1' && $('#<%= ddlDid.ClientID %>').val() != '-1') {
                return true;
            }
            else {
                
                $('.validation').html('<h3>همه موارد را انتخاب نمایید.</h3>');
                $('.validation').show();
                return false;
            }            
        }
    </script>
</asp:Content>
