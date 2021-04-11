<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="AddOrUpdatePollQuestions.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.AddOrUpdatePollQuestions" %>

<!doctype>
<html>
<head>
    <title></title>
    <meta charset="utf-8">
    <link href="../../Theme/css/bootstrap-rtl.css" rel="stylesheet">
    <link href="../../Theme/css/style-rtl.css" rel="stylesheet">
    <link href="../../Theme/css/responsive-rtl.css" rel="stylesheet">
    <link href="../../Theme/css/style.css" rel="stylesheet" />
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .addOrUpdatePollQuestionsWrapper {
            direction: rtl;
        }

        .rgRow td[align="center"], .rgAltRow td[align="center"], .rgEditRow td[align="center"] {
            text-align: center;
        }

        .rgEditForm {
            padding: 10px 15px;
        }
    </style>
    <script src="../../../CommonUI/js/jquery.min.js"></script>
</head>
<body>
    <form runat="server" id="mainForm">
        <asp:ScriptManager runat="server" ID="mainScriptManager"></asp:ScriptManager>
        <div class="addOrUpdatePollQuestionsWrapper container">
            <div class="row">
                <div class="col-sm-12">
                    <asp:Button runat="server" ID="btnAddQuestion" Text="افزودن سوال جدید" CssClass="btn btn-info" OnClick="btnAddQuestion_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <telerik:RadGrid runat="server" ID="rgvQuestions" OnNeedDataSource="rgvQuestions_NeedDataSource" AutoGenerateColumns="false"
                        OnUpdateCommand="rgvQuestions_UpdateCommand" OnInsertCommand="rgvQuestions_InsertCommand" OnItemDataBound="rgvQuestions_ItemDataBound"
                        OnDeleteCommand="rgvQuestions_DeleteCommand" EnableEmbeddedSkins="False" Skin="MyCustomSkin">
                        <HeaderStyle HorizontalAlign="Center" />
                        <MasterTableView
                            EditFormSettings-EditColumn-CancelText="انصراف"
                            EditFormSettings-EditColumn-InsertText="ثبت"
                            EditFormSettings-EditColumn-UpdateText="بروزرسانی">
                            <EditFormSettings>
                                <FormMainTableStyle Width="100%" />
                                <FormTableStyle Width="100%" />
                            </EditFormSettings>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="شناسه" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <span><%# Eval("Id") %></span>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="متن سوال" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <span><%# Eval("Question") %></span>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtQuestion" CssClass="form-control" Text='<%# Eval("Question") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="نیاز به توضیحات دارد؟" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%# Convert.ToBoolean(Eval("NeedComment")) ? "بلی" : "خیر" %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlNeedComment" CssClass="form-control">
                                            <asp:ListItem Text="خیر" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="بلی" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hdnQuestionId" Value='<%# Eval("Id") %>' />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList runat="server" ID="ddlNeedComment" CssClass="form-control">
                                            <asp:ListItem Text="خیر" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="بلی" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="عملیات" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnEdit" CommandName="Edit" Text="ویرایش" CssClass="btn btn-primary" />
                                        <asp:Button runat="server" ID="btnManageOptions" CommandName="ManageOptions" CommandArgument='<%# Eval("Id") %>'
                                            Text="ویرایش گزینه ها" CssClass="btn btn-info btnManageOptions" />
                                        <asp:Button runat="server" ID="btnDelete" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' Text="حذف"
                                            CssClass="btn btn-danger" OnClientClick="if(!confirm('آیا حذف این سوال و پاسخ های مربوط به آن اطمینان دارید؟')) return false;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            $('a[id$="_CancelButton"]').addClass('btn btn-danger');
            $('a[id$="_UpdateButton"], a[id$="_PerformInsertButton"]').addClass('btn btn-success');

            $('.btnManageOptions').click(function (e) {
                e.preventDefault();
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                oWindow.setUrl('../CMS/AddOrUpdatePollOptions.aspx?pid=' + getUrlVars()['pid'] + '&qid=' + $(this).closest('tr').find('td:nth-child(1) span').html());
            });
        });
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }
    </script>
</body>
</html>
