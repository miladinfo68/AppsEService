<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPollAnswerDetails.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ShowPollAnswerDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <link href="../../Theme/css/bootstrap-rtl.css" rel="stylesheet" />
    <link href="../../Theme/css/style-rtl.css" rel="stylesheet" />
    <link href="../../Theme/css/responsive-rtl.css" rel="stylesheet" />
    <link href="../../Theme/css/style.css" rel="stylesheet" />
    <style>
        .pollAnswerDetailsWrapper {
            direction: rtl;
        }

        .pollQuestions {
            border-bottom: 1px solid #ccc;
            margin-bottom: 15px;
            padding-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container pollAnswerDetailsWrapper">
            <asp:Repeater runat="server" ID="rptQuestions" OnItemDataBound="rptQuestions_ItemDataBound">
                <ItemTemplate>
                    <div class="row pollQuestions">
                        <div class="col-sm-12 pollQuestion">
                            <%# (Container.ItemIndex + 1).ToString() + " - " + Eval("Question") %>
                        </div>
                        <div class="col-sm-12 pollQuestionOptions">
                            <%--<asp:RadioButtonList runat="server" ID="rblOptions" DataTextField="Option" DataValueField="Id" RepeatDirection="Horizontal"
                            Enabled="false"></asp:RadioButtonList>--%>
                            <asp:Label runat="server" ID="lblAnswer"></asp:Label>
                        </div>
                        <asp:Panel runat="server" ID="pnlComment" CssClass="pollQuestionComment">
                            <span>توضیحات: </span>
                            <asp:TextBox runat="server" ID="txtComments" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Panel runat="server" ID="pnlCommentBox" CssClass="commentBox">
                        <span>توضیحات تکمیلی: </span>
                        <%--<asp:TextBox runat="server" ID="txtPollComment" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>--%>
                        <asp:Label runat="server" ID="lblPollComment"></asp:Label>
                    </asp:Panel>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
