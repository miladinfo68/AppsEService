<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="professorsByStatus.aspx.cs" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" Inherits="IAUEC_Apps.UI.University.CooperationRequest.professorsByStatus" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>


<asp:Content ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>

    <style>
        .pcalWrapper {
            position: relative;
        }

            .pcalWrapper > a.pcalBtn {
                position: absolute;
                left: 7px;
                top: 0;
                bottom: 0;
                margin: auto;
            }

            .pcalWrapper > input {
                padding-left: 30px;
            }

        .pcalBtn.disabled {
            pointer-events: none;
            cursor: default;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>

</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" dir="rtl">
        <div class="panel panel-success">
            <div class="panel-heading">
                <span>محدودسازی موارد جستجو(تمام موارد اختیاری هستند)</span>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <span style="font-size: larger">محدود سازی تاریخ</span>
                    </div>
                    <div class="col-md-1 ">
                        <span class="left">از تاریخ:</span>
                    </div>
                    <div class="col-md-1">
                        <div class="pcalWrapper">

                            <asp:TextBox ID="fromDate" runat="server" CssClass="form-control form-inline pcal" MaxLength="10" ToolTip="از تاریخ" />

                            <%--<asp:HiddenField runat="server" ID="hdnFromDate" />--%>
                        </div>
                    </div>
                    <div class="col-md-1 ">
                        <span class="left">تا تاریخ:</span>
                    </div>
                    <div class="col-md-1">
                        <div class="pcalWrapper">

                            <asp:TextBox ID="toDate" runat="server" CssClass="form-control form-inline pcal" MaxLength="10" ToolTip="تا تاریخ" />

                            <%--<asp:HiddenField runat="server" ID="hdnFromDate" />--%>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="جستجو" CssClass="btn btn-success" Width="200px" />
                    </div>
                </div>
            </div>
        </div>

        <div class="panel">
            <div class="panel-heading">
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" Visible="false" ID="ddlEvent">
                            <asp:ListItem Value="73" Text="تایید دانشکده"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="overflow: auto">
                    <div class="col-md-12" style="overflow: auto">
                        <telerik:RadGrid ID="grdProfessors" OnNeedDataSource="grdProfessors_NeedDataSource" runat="server" AutoGenerateColumns="false" OnItemCommand="grdProfessors_ItemCommand" AllowSorting="true" AllowFilteringByColumn="true">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="hrID" Visible="false"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ocode" HeaderText="کد استادی" AllowSorting="true" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="nationalCode" HeaderText="کد ملی" AllowSorting="true" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="pName" HeaderText="نام" AllowSorting="true" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="pLastName" HeaderText="نام خانوادگی" AllowSorting="true" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="approveDate" HeaderText="تاریخ تایید" AllowSorting="true" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="mobile" HeaderText="تلفن همراه" AllowSorting="true" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn DataField="" HeaderText="" AllowSorting="true" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="" HeaderText="" AllowSorting="true" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="" HeaderText="" AllowSorting="true" AllowFiltering="true"></telerik:GridBoundColumn>--%>
                                    <telerik:GridTemplateColumn AllowFiltering="false" AllowSorting="false">
                                        <ItemTemplate>
                                            <asp:Button Text="مشاهده جزئیات" runat="server" CommandName="showDetail" CommandArgument='<%#Eval("hrID") %>' CssClass="btn btn-success" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>

    </div>

<uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
