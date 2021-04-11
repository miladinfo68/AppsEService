<%@ Page Title="" Language="C#" MasterPageFile="~/University/WelfareAffairs/MasterPages/StudentLoanCMS.Master" AutoEventWireup="true" CodeBehind="StudentLoanRequestsReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.WelfareAffairs.CMS.StudentLoanRequestsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <style>
        .StudentLoanRequestsReportWrapper {
            direction: rtl;
        }

        .formLabel {
            line-height: 30px;
        }

        .searchBox {
            margin-bottom: 15px;
            background: #ddd;
            padding: 15px 10px 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="StudentLoanRequestsReportWrapper">
        <div class="row">
            <div class="col-sm-12">
                <div class="searchBox">
                    <div class="col-sm-4">
                        <div class="col-sm-4">
                            <span class="formLabel">شماره دانشجویی:</span>
                        </div>
                        <div class="col-sm-8">
                            <asp:TextBox runat="server" ID="txtStudentCode" CssClass="form-control" MaxLength="14"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="col-sm-4">
                            <span class="formLabel">ترم:</span>
                        </div>
                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" ID="ddlTerm" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="col-sm-4">
                            <span class="formLabel">وضعیت:</span>
                        </div>
                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                <asp:ListItem Text="انتخاب کنید" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="درحال بررسی" Value="1"></asp:ListItem>
                                <asp:ListItem Text="تائید شده" Value="2"></asp:ListItem>
                                <asp:ListItem Text="رد شده" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-2" style="text-align: center;">
                        <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <telerik:RadGrid runat="server" ID="rgvLoanRequests" AutoGenerateColumns="false" EnableEmbeddedSkins="False" Skin="MyCustomSkin"
                    OnNeedDataSource="rgvLoanRequests_NeedDataSource" OnItemCommand="rgvLoanRequests_ItemCommand" AllowPaging="true" PageSize="50">
                    <HeaderStyle HorizontalAlign="Center" />
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="شناسه درخواست" DataField="LoanId"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="نام و نام خانوادگی">
                                <ItemTemplate>
                                    <span><%# Eval("StudentFirstname") + " " + Eval("StudentLastname") %></span>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="نوع درخواست" DataField="LoanTypeTitle"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="شماره دانشجویی" DataField="StudentCode"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="عملیات">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnViewDetails" Text="جزئیات" CommandName="ViewDetails"
                                        CommandArgument='<%# Eval("LoanId") %>' CssClass="btn btn-info" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>
    <script>
        function Rebind() {
            __doPostBack('RebindeGrid', null);
        }
    </script>
</asp:Content>
