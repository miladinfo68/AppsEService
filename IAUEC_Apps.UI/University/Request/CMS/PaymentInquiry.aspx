<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSRequestMaster.Master" AutoEventWireup="true" CodeBehind="PaymentInquiry.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.PaymentInquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }
    </style>
    <div dir="rtl">
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lbl_StNo" runat="server" Text="شماره دانشجویی:"></asp:Label>
                    <asp:TextBox ID="txt_StudentNo" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lbl_Estelam" runat="server" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">

                    <asp:Button ID="btn_Save" CssClass="btn btn-info" runat="server" Text="نمایش" OnClick="btn_Save_Click" />

                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadGrid ID="grd_Payment" runat="server" AutoGenerateColumns="false" OnItemCommand="grd_Payment_ItemCommand">
                        <MasterTableView>
                            <HeaderStyle CssClass="bg-blue" />

                            <Columns>
                                <telerik:GridBoundColumn DataField="AmountTrans" HeaderText="مبلغ پرداخت شده">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PersianDate" HeaderText="تاریخ پرداخت">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TraceNumbers" HeaderText="شماره پیگیری">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AppStatus" HeaderText="وضعیت پرداخت">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <asp:Button ID="btn_EstelamPayID" runat="server" CommandArgument='<%# Eval("OrderID") %>' CommandName="estelam" Text="استعلام پرداخت" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
