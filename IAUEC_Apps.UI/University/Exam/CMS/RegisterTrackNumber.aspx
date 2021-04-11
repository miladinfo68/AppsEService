<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="RegisterTrackNumber.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.RegisterTrackNumber" %>

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

        .form-label {
            line-height: 38px;
        }

        .registerTrackNumberWrapper {
            direction: rtl;
        }

        .grdResult .form-control {
            color: #000;
        }
        .buttonsWrapper{margin-top:15px;}
        .grdResult .btn-success{border:none;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container registerTrackNumberWrapper">
        <div class="messageBox row">
            <div class="col-sm-12">
                <asp:ValidationSummary runat="server" ID="vlsRegisterTrackNumber" ValidationGroup="show" CssClass="alert alert-danger" />
                <asp:Panel runat="server" ID="pnlSuccessMessageBox" CssClass="alert alert-success" Visible="false">
                    <asp:Label runat="server" ID="lblSuccessMessage"></asp:Label>
                </asp:Panel>
            </div>
        </div>
        <div class="searchBox">
            <div class="row">
                <div class="col-sm-1">
                    <span class="form-label">تاریخ امتحان</span>
                </div>
                <div class="col-sm-2">
                    <asp:RequiredFieldValidator runat="server" ID="rfvExamDate" Display="None" ErrorMessage="تاریخ امتحان را انتخاب کنید."
                        ValidationGroup="show" ControlToValidate="ddlExamDate" InitialValue="-1"></asp:RequiredFieldValidator>
                    <asp:DropDownList runat="server" ID="ddlExamDate" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-sm-1">
                    <asp:Button runat="server" ID="btnShowResult" Text="نمایش" CssClass="btn btn-info" OnClick="btnShowResult_Click"
                        ValidationGroup="show" />
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlResult" CssClass="resultBox" Visible="false">
            <div class="row">
                <div class="col-sm-12">
                    <telerik:RadGrid runat="server" ID="grdResult" AutoGenerateColumns="false" OnNeedDataSource="grdResult_NeedDataSource"
                        EnableEmbeddedSkins="False" BackColor="#3A4A5B" ForeColor="White" CssClass="grdResult">
                        <MasterTableView>
                            <HeaderStyle HorizontalAlign="Right" ForeColor="White" CssClass="bg-purple" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" FilterImageUrl="Filter.gif" HeaderText="ردیف"
                                    SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                        <asp:HiddenField runat="server" ID="hdnExamPaperId" Value='<%# Eval("ExamPaperId") %>' />
                                        <asp:HiddenField runat="server" ID="hdnExamPlaceId" Value='<%# Eval("ExamPlaceId") %>' />
                                        <asp:HiddenField runat="server" ID="hdnExamDate" Value='<%# Eval("ExamDate") %>' />
                                        <asp:HiddenField runat="server" ID="hdnExamTime" Value='<%# Eval("ExamTime") %>' />
                                        <%--<asp:HiddenField runat="server" ID="hdnQuestionId" Value='<%# Eval("QuestionId") %>' />--%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%--<telerik:GridBoundColumn HeaderText="نام درس" DataField="namedars"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="نام استاد" DataField="osname"></telerik:GridBoundColumn>--%>
                                <%--<telerik:GridBoundColumn HeaderText="مشخصه کلاس" DataField="did"></telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn AllowFiltering="false" FilterImageUrl="Filter.gif" HeaderText="محل آزمون"
                                    SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblExamPlaceName" Text='<%# Eval("Name_City") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="ساعت آزمون" DataField="ExamTime"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="تاریخ ثبت کد مرسوله">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <%# GetDateString(Eval("TrackNumberRegisterDate")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" FilterImageUrl="Filter.gif" HeaderText="کد مرسوله پستی"
                                    SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtTrackNumber" Text='<%# Eval("TrackNumber") %>' CssClass="form-control">
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>

            <div class="row buttonsWrapper">
                <div class="col-sm-12">
                    <asp:Button runat="server" ID="btnSave" Text="ذخیره تغییرات" CssClass="btn btn-success" OnClick="btnSave_Click" />
                </div>
            </div>
            
        </asp:Panel>
        <asp:Panel ID="pnlSecretariatResult" runat="server" CssClass="resultBox" Visible="false">
            <div class="row">
                <div class="col-sm-12">
                    <telerik:RadGrid runat="server" ID="grdSecretariatResult" AutoGenerateColumns="false" EnableEmbeddedSkins="False"
                        OnNeedDataSource="grdSecretariatResult_NeedDataSource" BackColor="#3A4A5B" ForeColor="White" CssClass="grdResult"
                        OnItemCommand="grdSecretariatResult_ItemCommand" OnItemDataBound="grdSecretariatResult_ItemDataBound">
                        <MasterTableView>
                            <HeaderStyle HorizontalAlign="Right" ForeColor="White" CssClass="bg-purple" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" FilterImageUrl="Filter.gif" HeaderText="ردیف"
                                    SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                        <asp:HiddenField runat="server" ID="hdnExamPlaceId" Value='<%# Eval("ExamPlaceId") %>' />
                                        <asp:HiddenField runat="server" ID="hdnExamDate" Value='<%# Eval("ExamDate") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%--<telerik:GridBoundColumn HeaderText="نام درس" DataField="namedars"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="نام استاد" DataField="osname"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ساعت آزمون" DataField="saatexam"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="مشخصه کلاس" DataField="did"></telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn HeaderText="محل آزمون" DataField="Name_City"></telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn HeaderText="کد مرسوله پستی" DataField="TrackNumber"></telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn HeaderText="کد مرسوله پستی">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTrackNumber"
                                            Text='<%# string.IsNullOrEmpty(Eval("TrackNumber").ToString()) ? "-" : Eval("TrackNumber").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="تاریخ ثبت کد مرسوله">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <%# GetDateString(Eval("TrackNumberRegisterDate")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="تاریخ دریافت مرسوله">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <%# GetDateString(Eval("SecretariatReceiveDate")) %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" FilterImageUrl="Filter.gif" HeaderText="عملیات"
                                    SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnReceived" Text="دریافت شد" CommandName="SecretariatReceived" CssClass="btn btn-success" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </asp:Panel>
    
    </div>
</asp:Content>
