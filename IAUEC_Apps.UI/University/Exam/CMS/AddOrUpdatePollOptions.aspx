<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="AddOrUpdatePollOptions.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.AddOrUpdatePollOptions" %>

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
        <div class="addOrUpdatePollOptionsWrapper container">
            <div class="row">
                <div class="col-sm-12">
                    <asp:Button runat="server" ID="btnAddOption" Text="افزودن گزینه جدید" CssClass="btn btn-info" OnClick="btnAddOption_Click" />
                    <asp:Button runat="server" ID="btnGoBack" Text="بازگشت" CssClass="btn btn-warning btnGoBack" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <telerik:RadGrid runat="server" ID="rgvOptions" OnNeedDataSource="rgvOptions_NeedDataSource" AutoGenerateColumns="false"
                        OnUpdateCommand="rgvOptions_UpdateCommand" OnInsertCommand="rgvOptions_InsertCommand" OnDeleteCommand="rgvOptions_DeleteCommand"
                        EnableEmbeddedSkins="False" Skin="MyCustomSkin">
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
                                <telerik:GridTemplateColumn HeaderText="گزینه" ItemStyle-Width="45%">
                                    <ItemTemplate>
                                        <span><%# Eval("Option") %></span>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtOption" CssClass="form-control" Text='<%# Eval("Option") %>'></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnOptionId" Value='<%# Eval("Id") %>' />
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="امتیاز" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <span><%# Eval("Point") %></span>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txtPoint" CssClass="form-control" Text='<%# Eval("Point") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="عملیات" ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnEdit" CommandName="Edit" Text="ویرایش" CssClass="btn btn-primary" />
                                        <asp:Button runat="server" ID="btnDelete" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' Text="حذف"
                                            CssClass="btn btn-danger" OnClientClick="if(!confirm('آیا حذف این گزینه و پاسخ های مربوط به آن اطمینان دارید؟')) return false;" />
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

            $('.btnGoBack').click(function (e) {
                e.preventDefault();
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                oWindow.setUrl('../CMS/AddOrUpdatePollQuestions.aspx?pid=' + getUrlVars()['pid']);
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
