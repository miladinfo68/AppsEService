<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/Tuitional/masterPages/tuitionalMasterpage.Master" CodeBehind="PayFromWallet.aspx.cs" Inherits="IAUEC_Apps.UI.University.Wallet.CMS.PayFromWallet" %>

<asp:Content runat="server" ContentPlaceHolderID="HeaderplaceHolder">
        <style>
        .RadGrid_MyCustomSkin .rgSave, .RadGrid_MyCustomSkin .rgUpdate, .RadGrid_MyCustomSkin .rgCancel, .RadGrid_MyCustomSkin .rgEdit, .RadGrid_MyCustomSkin .rgDel, .RadGrid_MyCustomSkin .rgExpand, .RadGrid_MyCustomSkin .rgCollapse, .RadGrid_MyCustomSkin .rgFilter, .RadGrid_MyCustomSkin .rgAdd, .RadGrid_MyCustomSkin .rgRefresh, .RadGrid_MyCustomSkin .rgSortAsc, .RadGrid_MyCustomSkin .rgSortDesc, .RadGrid_MyCustomSkin .rgUngroup, .RadGrid_MyCustomSkin .rgPagePrev, .RadGrid_MyCustomSkin .rgPageNext, .RadGrid_MyCustomSkin .rgPageFirst, .RadGrid_MyCustomSkin .rgPageLast {
            background-image: url('../../Theme/images/radActionsSprite.png');
            width: 30px;
            height: 30px;
        }

        RadGrid_MyCustomSkin .rgHeader,
        .RadGrid_MyCustomSkin th.rgResizeCol {
            background-color: #e8e8e8;
            border-bottom: 1px solid white;
            border-right: 1px solid white;
        }

        .RadGrid_MyCustomSkin input.rgFilterBox {
            border-color: #98A4B1;
            font-weight: 100;
            font-size: 12px;
            line-height: 23px;
            font-family: "Segoe UI", Arial, Helvetica, sans-serif;
            color: black;
            height: 23px;
            width: 80%;
            padding: 2px 5px 3px;
            background: none;
        }

        .RadGrid_MyCustomSkin input.rgFilter {
            width: 30px;
            height: 30px;
            background-position: -111px -4509px;
            background-color: #444444;
            border: 1px solid #999999;
        }

        .RadGrid_MyCustomSkin .rgHeaderWrapper {
            background-color: #dbdfe4;
        }

        .RadGrid_MyCustomSkin .rgHeaderDiv {
            margin-left: 0;
        }

        .positive {
            background: rgba(38, 185, 154, 0.88);
            color: #000;
        }

        .negative {
            background: rgba(231, 76, 60, 0.88);
            color: #000;
        }

        .RadGridRTL .rgHeader {
            text-align: center !important;
            background: #ddd;
        }

        .rgFilterRow > td:first-child input.rgFilterBox {
            width: 50%;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageTitle"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="container" dir="rtl">
        <telerik:RadWindowManager ID="rAlert" runat="server" Width="60%" BackColor="#ffffcc" ForeColor="#660066" Font-Size="X-Large"></telerik:RadWindowManager>

        <div class="panel panel-info">
            <div class="panel-heading">

                <div class="row">
                    <div class="col-md-3"></div>
                    <div class="col-md-1">
                        <span>کد دانشجویی:</span>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtSearchStudent" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearchStudent" runat="server" Text="جستجو" CssClass="form-control btn btn-info" OnClick="btnSearchStudent_Click" />
                    </div>
                    <div class="col-md-4"></div>
                </div>

            </div>
            <div class="panel-body" id="dvStudentInf" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-6 text-left">
                        <asp:Label Text="نام:    " runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6 text-right">
                        <asp:Label runat="server" ID="lblName"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 text-left">
                        <asp:Label Text="نام خانوادگی:    " runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6 text-right">
                        <asp:Label runat="server" ID="lblLastName"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 text-left">
                        <asp:Label Text="شماره دانشجویی:    " runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6 text-right">
                        <asp:Label runat="server" ID="lblStudentID"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 text-left">
                        <asp:Label Text="کد ملی:    " runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6 text-right">
                        <asp:Label runat="server" ID="lblNationalCode"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6 text-left">
                        <asp:Label Text="مبلغ مانده در کیف پول:    " runat="server"></asp:Label>
                    </div>
                    <div class="col-md-6 text-right">
                        <asp:Label runat="server" ID="lblRemainingMoney"></asp:Label>
                        <asp:Button ID="btnShowLastTransactions" runat="server" CssClass="btn btn-primary" Text="مشاهده تراکنش های دانشجو" OnClick="btnShowLastTransactions_Click" Visible="true" />
                    </div>

                </div>
                <br />
                <br />
                <br />
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-1 text-left">
                        <span>مبلغ</span>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1 text-left">
                        <span>بابت</span>
                    </div>
                    <div class="col-md-2 text-right">
                        <asp:DropDownList ID="drpPayType" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnPay" runat="server" Text="کسر از کیف پول" CssClass="btn form-control btn-success" OnClick="btnPay_Click" />
                    </div>
                    <div class="col-md-2"></div>
                </div>
            </div>

        </div>
        <telerik:RadWindow ID="rwTransactionHistory" runat="server" Width="900" Height="650" CenterIfModal="true">
            <ContentTemplate>
                <div class="container">
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <telerik:RadGrid runat="server" ID="grdTransaction" AutoGenerateColumns="false" OnNeedDataSource="grdTransaction_NeedDataSource" AllowPaging="true" AllowFilteringByColumn="true" AllowSorting="true"
                                EnableEmbeddedSkins="false" Skin="MyCustomSkin" OnItemDataBound="grdTransaction_ItemDataBound" PageSize="30">
                                <MasterTableView CssClass="table table-bordered" Dir="RTL">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Id" HeaderText="شناسه تراکنش" ItemStyle-Width="100"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Amount" HeaderText="مبلغ تراکنش"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TransactionType" HeaderText="نوع تراکنش"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Remaining" HeaderText="مبلغ باقیمانده"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ تراکنش"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Description" HeaderText="توضیحات"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </div>
</asp:Content>
