<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="PreviousExamQuestions.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.PreviousExamQuestions" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .searchBox {
            border: 1px solid #ccc;
            padding: 10px 20px;
            border-radius: 5px;
        }

        .resultBox {
            margin-top: 15px;
            border: 1px solid #ccc;
            padding: 10px 20px;
            border-radius: 5px;
        }

        .form-control {
            direction: rtl;
        }

        .previousExamQuestionsWrapper {
            direction: rtl;
        }
    </style>
    <script type="text/javascript">

        function openShowFileInPopup(path) {
            setTimeout(function () { window.radopen(path, "UserListDialog"); }, 1000);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <telerik:RadWindowManager ID="rwm" runat="server" Width="800px" Height="600px"></telerik:RadWindowManager>
    <div class="container previousExamQuestionsWrapper">
        <div class="searchBox">
            <div class="row">
                <div class="col-sm-4">
                    <%--<asp:DropDownList runat="server" ID="ddlCourse" CssClass="form-control"></asp:DropDownList>--%>
                    <telerik:RadComboBox ID="ddlCourse" runat="server" EmptyMessage="انتخاب درس" Skin="Silk" Filter="Contains"
                        MarkFirstMatch="True" CssClass="form-control" Width="100%"></telerik:RadComboBox>
                </div>
                <div class="col-sm-2">
                    <%--<asp:DropDownList runat="server" ID="ddlTerm" CssClass="form-control"></asp:DropDownList>--%>
                    <telerik:RadComboBox ID="ddlTerm" runat="server" EmptyMessage="انتخاب ترم" Skin="Silk" Filter="Contains"
                        MarkFirstMatch="True" CssClass="form-control" Width="100%"></telerik:RadComboBox>
                </div>
                <div class="col-sm-1">
                    <asp:Button runat="server" ID="btnShowQuestions" Text="نمایش" OnClick="btnShowQuestions_Click" CssClass="btn btn-info" />
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlResult" CssClass="resultBox" Visible="false">
            <telerik:RadGrid runat="server" ID="grdResult" OnNeedDataSource="grdResult_NeedDataSource" AutoGenerateColumns="false"
                EnableEmbeddedSkins="False" BackColor="#3A4A5B" ForeColor="White" OnItemCommand="grdResult_ItemCommand"
                OnItemDataBound="grdResult_ItemDataBound">
                <MasterTableView>
                    <HeaderStyle HorizontalAlign="Right" ForeColor="White" CssClass="bg-purple" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" FilterImageUrl="Filter.gif" HeaderText="ردیف"
                            SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                            <ItemStyle HorizontalAlign="Right" />
                            <ItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                                <asp:HiddenField runat="server" ID="hdnQuestionId" Value='<%# Eval("ID") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="نام درس" DataField="namedars" FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif"
                            SortDescImageUrl="SortDesc.gif">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="نام استاد" DataField="FullName" FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif"
                            SortDescImageUrl="SortDesc.gif">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="ترم" DataField="Term" FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif"
                            SortDescImageUrl="SortDesc.gif">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false" FilterImageUrl="Filter.gif" HeaderText="عملیات"
                            SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnDownload" CommandName="OpenQuestion" CommandArgument='<%# Eval("ID") %>'
                                    Text="مشاهده سوالات" CssClass="btn btn-success" />
                                <asp:Button runat="server" ID="btnOpenAttach" CommandName="OpenAttachment" CommandArgument='<%# Eval("ID") %>'
                                    Text="مشاهده پیوست" CssClass="btn btn-info" Visible="false" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

        </asp:Panel>
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel">
    </telerik:RadAjaxManager>
</asp:Content>
