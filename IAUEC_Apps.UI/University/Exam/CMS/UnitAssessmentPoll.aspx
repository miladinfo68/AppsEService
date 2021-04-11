<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="UnitAssessmentPoll.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.UnitAssessmentPoll" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .pollWrapper, .votedMessage, .successMessage, .validationSummery, .noPoll {
            direction: rtl;
        }

        .pollQuestionComment .form-control {
            display: inline-block;
            width: 95%;
        }

        .commentBox {
            border-bottom: 1px solid #ccc;
            border-top: 1px solid #ccc;
            margin: 15px 0;
            padding: 15px 0;
        }

        .buttonsWrapper {
            text-align: center;
        }

        .pollQuestions {
            margin-bottom: 15px;
            line-height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="pnlVotedMessage" CssClass="alert alert-info votedMessage">
        <asp:Label runat="server" ID="lblVotedMessage" Text="شما قبلاً پرسشنامه ارزیابی عملکرد واحد را تکمیل نموده اید."></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlNoPoll" CssClass="alert alert-danger noPoll" Visible="false">
        <asp:Label runat="server" ID="lblNoPoll" Text="فرم ارزیابی برای ترم جاری وجود ندارد."></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlOutDate" CssClass="alert alert-warning noPoll" Visible="false">
        <asp:Label runat="server" ID="lblOutDate" Text="زمان پاسخگویی به این ارزیابی پایان یافته و یا هنوز آغاز نشده است."></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlSuccessMessage">
        <asp:Label runat="server" ID="lblsuccessMessage"></asp:Label>
    </asp:Panel>
    <asp:ValidationSummary runat="server" ID="vsPoll" ValidationGroup="poll" CssClass="alert alert-danger validationSummery"
        HeaderText="لطفا به تمامی سوالات پاسخ دهید" />
    <asp:Panel runat="server" ID="pnlPollWrapper" CssClass="pollWrapper">
        <div>
            <span>نماینده محترم واحد </span>
            <asp:Label runat="server" ID="lblUnitName"></asp:Label>
        </div>
        <div>با سلام و احترام</div>
        <asp:Repeater runat="server" ID="rptQuestions" OnItemDataBound="rptQuestions_ItemDataBound">
            <HeaderTemplate>
                <p class="pollDescription">
                    <asp:Label runat="server" ID="lblDescription"></asp:Label>
                </p>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="row pollQuestions">
                    <div class="pollQuestion">
                        <%# (Container.ItemIndex + 1).ToString() + " - " + Eval("Question") %>
                    </div>
                    <div class="pollQuestionOptions">
                        <asp:RequiredFieldValidator runat="server" ID="rfvOptions" ValidationGroup="poll" ControlToValidate="rblOptions"></asp:RequiredFieldValidator>
                        <asp:RadioButtonList runat="server" ID="rblOptions" DataTextField="Option" DataValueField="Id" RepeatDirection="Horizontal"></asp:RadioButtonList>
                    </div>
                    <asp:Panel runat="server" ID="pnlComment" CssClass="pollQuestionComment">
                        <span>توضیحات: </span>
                        <asp:TextBox runat="server" ID="txtComments" CssClass="form-control"></asp:TextBox>
                    </asp:Panel>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Panel runat="server" ID="pnlCommentBox" CssClass="commentBox">
                    <span>توضیحات تکمیلی: </span>
                    <asp:TextBox runat="server" ID="txtPollComment" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>
                <div class="buttonsWrapper">
                    <asp:Button runat="server" ID="btnSave" Text="ارسال" CssClass="btn btn-success" OnClick="btnSave_Click" ValidationGroup="poll" />
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>
