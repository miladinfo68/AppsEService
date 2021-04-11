<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WalletTransactions.aspx.cs" Inherits="IAUEC_Apps.UI.University.Wallet.Pages.WalletTransactions" MasterPageFile="~/University/Wallet/MasterPages/WalletPages.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
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
        .RadGrid_MyCustomSkin .rgHeaderWrapper
{
    background-color: #dbdfe4;
}
 
.RadGrid_MyCustomSkin .rgHeaderDiv
{
    margin-left: 0;
}
.positive{
    background: rgba(38, 185, 154, 0.88);
    color: #000;
}
.negative{
    background: rgba(231, 76, 60, 0.88);
    color: #000;
}
.RadGridRTL .rgHeader{
    text-align: center !important;
    background: #ddd;
}
.rgFilterRow > td:first-child input.rgFilterBox{
    width: 50%;
}

    </style>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder2">گزارش تراکنش ها</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="bodyPlaceHolder">
    <asp:ScriptManager runat="server" ID="smMain"></asp:ScriptManager>
    <div>
        <telerik:RadGrid runat="server" ID="trgTransactions" AutoGenerateColumns="false" OnNeedDataSource="trgTransactions_NeedDataSource" AllowPaging="true" AllowFilteringByColumn="true" AllowSorting="true"
            EnableEmbeddedSkins="false" Skin="MyCustomSkin" OnItemDataBound="trgTransactions_ItemDataBound" PageSize="30">
            <MasterTableView CssClass="table table-bordered">
                <Columns>
                    <telerik:GridBoundColumn DataField="Id" HeaderText="شناسه تراکنش" ItemStyle-Width="100"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Amount" HeaderText="مبلغ تراکنش"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TransactionType" HeaderText="نوع تراکنش"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Remaining" HeaderText="مبلغ باقیمانده"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ تراکنش"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Description" HeaderText="توضیحات"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
