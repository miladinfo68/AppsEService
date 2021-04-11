<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="UnitAssessmentPollResultByQuestion.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.UnitAssessmentPollResultByQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .pollResultByQuestionWrapper {
            direction: rtl;
        }

        .searchBox {
            background: #ccc;
            padding: 15px 0;
            line-height: 32px;
        }

            .searchBox .btn {
                margin-bottom: 0;
            }

            .searchBox span {
                padding-right: 40px;
            }

        .resultBox {
            margin-top: 15px;
        }
    </style>
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pollResultByQuestionWrapper">
        <div class="row">
            <asp:ValidationSummary runat="server" ID="vsSearch" ValidationGroup="search" CssClass="alert alert-danger" />
        </div>
        <div class="row searchBox">
            <%--<div class="col-sm-1">
                <span>ترم:</span>
            </div>
            <div class="col-sm-2">
                <asp:DropDownList runat="server" ID="ddlTerms" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTerms_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-sm-1">
                <span>پرسشنامه:</span>
            </div>
            <div class="col-sm-2">
                <asp:DropDownList runat="server" ID="ddlPolls" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPolls_SelectedIndexChanged">
                </asp:DropDownList>
            </div>--%>
            <div class="col-sm-1">
                <span>سوال:</span>
            </div>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlQuestions" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-sm-1">
                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="نمایش نتایج" OnClick="btnSearch_Click" ValidationGroup="search" />
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlResult" Visible="false" CssClass="row resultBox">
            <telerik:RadGrid runat="server" ID="rgvResult" AutoGenerateColumns="false" OnItemCommand="rgvResult_ItemCommand" EnableEmbeddedSkins="False"
                Skin="MyCustomSkin">
                <HeaderStyle HorizontalAlign="Center" />
                <MasterTableView>
                    <Columns>

                        <telerik:GridTemplateColumn HeaderText=" ردیف" ItemStyle-Width="70">
                            <ItemTemplate>
                                <%# Container.RowIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridBoundColumn HeaderText="عنوان واحد" DataField="CityName"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="کاربر" DataField="Name"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="گزینه انتخابی" DataField="Option"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="امتیاز گزینه" DataField="Point"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="عملیات">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnShowDetails" CommandName="ShowDetails" Text="نمایش جزئیات"
                                    CommandArgument='<%# Eval("TargetObject") + "%%%" + Eval("Term") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </asp:Panel>
    </div>
</asp:Content>
