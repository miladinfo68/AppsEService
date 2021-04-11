<%@ Page Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Contract.Master" AutoEventWireup="true" CodeBehind="ContractsMain.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Teachers.ContractsMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
        <link href="../css/Contract.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h1></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager runat="server" ID="rwm_message"></telerik:RadWindowManager>
    <div id="dvSelectContract" runat="server" visible="true">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <asp:Button ID="btnContract" runat="server" Text="نمایش قرارداد آموزشی" OnClick="btnContract_Click" BackColor="#0022ff" Font-Size="XX-Large" ForeColor="White" Width="100%" Height="200px" />
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnAgreement" runat="server" Text="نمایش تفاهم نامه پژوهشی" OnClick="btnAgreement_Click" BackColor="#8800ff" Font-Size="XX-Large" ForeColor="White" Width="100%" Height="200px" />

            </div>
            <div class="col-md-2"></div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <asp:Button ID="btnContract_HeadOfDepartment" runat="server" Text="قرارداد مدیر و معاون گروه" OnClick="btnContract_HeadOfDepartment_Click" BackColor="#ff4646" Font-Size="XX-Large" ForeColor="White" Width="100%" Height="200px" />
            </div>
        </div>
    </div>

    <div id="dvContract" class="contractsMainWrapper" runat="server" visible="false">
        <div class="row" style="width: 100%">
            <div class="col-sm-2">
                <asp:DropDownList runat="server" ID="ddlTerm" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged" CssClass="form-control">
                    <asp:ListItem Text="ترم را انتخاب کنید" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-2">
                <asp:Button runat="server" ID="btnShowContract" Text="مشاهده و امضای قرارداد ترم انتخابی" OnClick="btnShowContract_Click" CssClass="btn btn-info" BackColor="#0088ff" BorderColor="#0088ff" />
            </div>

            <%--<div class="col-sm-6">
                <asp:Label runat="server" ID="lblStatus"></asp:Label>
            </div>--%>
        </div>
        <div runat="server" id="divPendingBox" class="row">
            <div class="col-sm-12">
                <div runat="server" id="divPendingMessage" class="alert alert-warning"></div>
            </div>
        </div>
        <div runat="server" id="divRejectBox_Contract" class="row">
            <div class="col-sm-12">
                <div runat="server" id="divRejectMessage_Contract" class="alert alert-danger">
                    استاد گرامی، قرارداد شما توسط کارگزینی رد شده است.
                </div>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <table class="col-sm-12 table table-bordered statusTable">
                    <tbody>
                        <tr>
                            <th colspan="2">وضعیت قرارداد</th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptProgressContract">

                            <ItemTemplate>
                                <tr>
                                    <td class="col-sm-3 termTitle">
                                        <span><%# ((IDictionary<string, object>)GetDataItem())["TermTitle"] %></span>
                                    </td>
                                    <td class="col-sm-9">
                                        <div class="f1Contract-steps">
                                            <div class="f1Contract-progress">
                                                <div class="f1Contract-progress-line" data-now-value='<%# ((IDictionary<string, object>)GetDataItem())["Progress"] %>'
                                                    data-number-of-steps="3" style='<%# "width: " + ((IDictionary<string, object>)GetDataItem())["Progress"] + "%;" %>'>
                                                </div>
                                            </div>
                                            <div class='<%# GetCssClass_Contract(1) %>'>
                                                <div class="f1Contract-step-icon"><i class="fa fa-pencil"></i></div>
                                                <p runat="server" id="step1">تایید قرارداد توسط استاد</p>
                                            </div>
                                            <div class='<%# GetCssClass_Contract(2) %>'>
                                                <div class="f1Contract-step-icon"><i class="fa fa-user"></i></div>
                                                <p runat="server" id="step2" contenteditable="true">در حال بررسی توسط کارگزینی</p>
                                            </div>
                                            <div runat="server" id="dvstep3" class='<%# GetCssClass_Contract(3) %>'>
                                                <div class="f1Contract-step-icon"><i class="fa fa-check"></i></div>
                                                <p runat="server" id="step3" contenteditable="true">تایید قرارداد</p>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div id="dvAgreement" runat="server" class="agreementMainWrapper" visible="false">
        <div runat="server" id="divRejectBox_Agreement" class="row">
            <div class="col-sm-12">
                <div runat="server" id="divRejectMessage_Agreement" class="alert alert-danger">
                    استاد گرامی، تفاهم نامه شما توسط پژوهش رد شده است.
                </div>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <table class="col-sm-12 table table-bordered statusTable">
                    <tbody>
                        <tr>
                            <th colspan="2">وضعیت تفاهم نامه</th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptProgressAgreement">

                            <ItemTemplate>
                                <tr>
                                    <td class="col-sm-3 termTitle">
                                        <span><%# ((IDictionary<string, object>)GetDataItem())["TermTitle"] %></span>
                                    </td>
                                    <td class="col-sm-9">
                                        <div class="f1Agreement-steps">
                                            <div class="f1Agreement-progress">
                                                <div class="f1Agreement-progress-line" data-now-value='<%# ((IDictionary<string, object>)GetDataItem())["Progress"] %>'
                                                    data-number-of-steps="3" style='<%# "width: " + ((IDictionary<string, object>)GetDataItem())["Progress"] + "%;" %>'>
                                                </div>
                                            </div>
                                            <div class='<%# GetCssClass_Agreement(1) %>'>
                                                <div class="f1Agreement-step-icon"><i class="fa fa-pencil"></i></div>
                                                <p runat="server" id="step1">تایید تفاهم نامه توسط استاد</p>
                                            </div>
                                            <div class='<%# GetCssClass_Agreement(2) %>'>
                                                <div class="f1Agreement-step-icon"><i class="fa fa-user"></i></div>
                                                <p runat="server" id="step2" contenteditable="true">در حال بررسی توسط پژوهش</p>
                                            </div>
                                            <div runat="server" id="dvstep3" class='<%# GetCssClass_Agreement(3) %>'>
                                                <div class="f1Agreement-step-icon"><i class="fa fa-check"></i></div>
                                                <p runat="server" id="step3" contenteditable="true">تایید تفاهم نامه</p>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <asp:Button ID="btnShowAgreement" runat="server" CssClass="btn purple" Text="مشاهده و امضای تفاهم نامه" OnClick="btnShowAgreement_Click" />
    </div>
    <div id="dvContract_Department" class="contractsMainWrapper" runat="server" visible="false">
        <div class="row" style="width: 100%">
            <div class="col-sm-2">
                <asp:DropDownList runat="server" ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" CssClass="form-control">
                    <asp:ListItem Text="سال را انتخاب کنید" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-2">
                <asp:Button runat="server" ID="btnShowContractHOD" Text="مشاهده و امضای قرارداد سال انتخابی" OnClick="btnShowContractHOD_Click" CssClass="btn btn-danger" />
            </div>
        </div>
        <div runat="server" id="divPendingBoxHOD" class="row">
            <div class="col-sm-12">
                <div runat="server" id="divPendingMessageHOD" class="alert alert-warning"></div>
            </div>
        </div>
        <div runat="server" id="divRejectBox_ContractHOD" class="row">
            <div class="col-sm-12">
                <div runat="server" id="divRejectMessage_ContractHOD" class="alert alert-danger">
                    استاد گرامی، قرارداد شما توسط کارگزینی رد شده است.
                </div>

            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <table class="col-sm-12 table table-bordered statusTable">
                    <tbody>
                        <tr>
                            <th colspan="2">وضعیت قرارداد</th>
                        </tr>
                        <asp:Repeater runat="server" ID="rptProgressContractHOD">

                            <ItemTemplate>
                                <tr>
                                    <td class="col-sm-3 termTitle">
                                        <span><%# ((IDictionary<string, object>)GetDataItem())["TermTitle"] %></span>
                                    </td>
                                    <td class="col-sm-9">
                                        <div class="f1ContractHOD-steps">
                                            <div class="f1ContractHOD-progress">
                                                <div class="f1ContractHOD-progress-line" data-now-value='<%# ((IDictionary<string, object>)GetDataItem())["Progress"] %>'
                                                    data-number-of-steps="3" style='<%# "width: " + ((IDictionary<string, object>)GetDataItem())["Progress"] + "%;" %>'>
                                                </div>
                                            </div>
                                            <div class='<%# GetCssClass_ContractHOD(1) %>'>
                                                <div class="f1ContractHOD-step-icon"><i class="fa fa-pencil"></i></div>
                                                <p runat="server" id="step1">تایید قرارداد توسط استاد</p>
                                            </div>
                                            <div class='<%# GetCssClass_ContractHOD(2) %>'>
                                                <div class="f1ContractHOD-step-icon"><i class="fa fa-user"></i></div>
                                                <p runat="server" id="step2" contenteditable="true">در حال بررسی توسط کارگزینی</p>
                                            </div>
                                            <div runat="server" id="dvstep3" class='<%# GetCssClass_ContractHOD(3) %>'>
                                                <div class="f1ContractHOD-step-icon"><i class="fa fa-check"></i></div>
                                                <p runat="server" id="step3" contenteditable="true">تایید قرارداد</p>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>





</asp:Content>
