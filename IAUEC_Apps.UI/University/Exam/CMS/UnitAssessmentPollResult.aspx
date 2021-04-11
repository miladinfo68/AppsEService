<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="UnitAssessmentPollResult.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.UnitAssessmentPollResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .pollResultWrapper {
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
            .resultBox{
                margin-top: 15px;
            }
    </style>
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pollResultWrapper">
        <div class="row">
            <asp:ValidationSummary runat="server" ID="vsSearch" ValidationGroup="search" CssClass="alert alert-danger" />
        </div>
        <div class="row searchBox">
            <div class="col-sm-2">
                <span>نام واحد:</span>
            </div>
            <div class="col-sm-2">
                <%--<asp:RequiredFieldValidator runat="server" ID="rfvUnits" ValidationGroup="search" ErrorMessage="نام واحد را انتخاب کنید."
                    Display="None" InitialValue="-1" ControlToValidate="ddlUnits"></asp:RequiredFieldValidator>--%>
                <asp:DropDownList runat="server" ID="ddlUnits" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-sm-2">
                <span>ترم:</span>
            </div>
            <div class="col-sm-2">
                <%--<asp:RequiredFieldValidator runat="server" ID="rfvTerms" ValidationGroup="search" ErrorMessage="ترم را انتخاب کنید."
                    Display="None" InitialValue="-1" ControlToValidate="ddlTerms"></asp:RequiredFieldValidator>--%>
                <asp:DropDownList runat="server" ID="ddlTerms" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-sm-2">
                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info" Text="نمایش نتایج" OnClick="btnSearch_Click" ValidationGroup="search" />
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlResult" Visible="false" CssClass="row resultBox">
            <telerik:RadGrid runat="server" ID="rgvResult" AutoGenerateColumns="false" OnItemCommand="rgvResult_ItemCommand" EnableEmbeddedSkins="False"
                Skin="MyCustomSkin">
                <HeaderStyle HorizontalAlign="Center" />
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn DataField="Title" HeaderText="عنوان پرسشنامه"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Name_City" HeaderText="واحد دانشگاهی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Term" HeaderText="ترم"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Points" HeaderText="مجموع امتیازات"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="عملیات">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnDetails" CommandName="ShowDetails" Text="نمایش جزئیات" CssClass="btn btn-info"
                                    CommandArgument='<%# Eval("TargetObject") + "%%%" + Eval("Term") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </asp:Panel>
    </div>
</asp:Content>
