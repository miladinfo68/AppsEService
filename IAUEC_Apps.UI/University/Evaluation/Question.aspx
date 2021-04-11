<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/Exam/MasterPages/BlankMaster.Master" CodeBehind="Question.aspx.cs" Inherits="IAUEC_Apps.UI.University.Evaluation.Question" %>

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

        .pollQuestions {
            margin-bottom: 15px;
            line-height: 24px;
            overflow-x:auto;
        }
        .pollQuestion {
            font-size:17px;
            font-family:'B Nazanin';
            color:#212121;
            text-align:justify;
            margin-bottom:3px;
            font-weight:bold;
            padding:9px;
            word-break:break-word;
        }
        .pollQuestionOptions {
            font-size:16px;
            font-family:'B Nazanin';
            color:#212121;
            text-align:justify;
            padding:9px;
            word-break:break-word;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="pnlSuccessMessage">
        <asp:Label runat="server" ID="lblsuccessMessage"></asp:Label>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlPollWrapper" CssClass="pollWrapper">
        <asp:Repeater runat="server" ID="rptQuestions" OnItemDataBound="rptQuestions_ItemDataBound">
            <HeaderTemplate>
                <p class="pollDescription">
                    <asp:Label runat="server" ID="lblDescription"></asp:Label>
                </p>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="row pollQuestions">
                    <div class="pollQuestion">
                        <%# (Container.ItemIndex + 1).ToString() + " - " + Eval("Text") %>
                    </div>
                    <div class="pollQuestionOptions">
                        <asp:RequiredFieldValidator runat="server" ID="rfvOptions" ValidationGroup="poll" ControlToValidate="rblOptions"></asp:RequiredFieldValidator>
                        <asp:RadioButtonList runat="server" ID="rblOptions" DataTextField="Text" DataValueField="Id" RepeatDirection="Horizontal"></asp:RadioButtonList>
                        <asp:HiddenField runat="server" ID="hdnQuestionId" Value='<%# Eval("Id") %>' />
                        <asp:HiddenField runat="server" ID="hdnTerm" Value='<%# Eval("Term") %>' />
                    </div>
                    <%-- <asp:Panel runat="server" ID="pnlComment" CssClass="pollQuestionComment">
                        <span>توضیحات: </span>
                        <asp:TextBox runat="server" ID="txtComments" CssClass="form-control"></asp:TextBox>
                    </asp:Panel>--%>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                <%-- <asp:Panel runat="server" ID="pnlCommentBox" CssClass="commentBox">
                    <span>توضیحات تکمیلی: </span>
                    <asp:TextBox runat="server" ID="txtPollComment" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                </asp:Panel>--%>
                <div class="buttonsWrapper">
                    <asp:Button runat="server" ID="btnSave" Text="ارسال" CssClass="btn btn-success" OnClick="btnSave_Click" ValidationGroup="poll" />
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </asp:Panel>
</asp:Content>
