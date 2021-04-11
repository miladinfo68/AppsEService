<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="CheckOutNaghs.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutNaghs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h2>مدیریت نواقص پرونده</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class=" container" dir="rtl">
        <h3>درخواست های دارای نقص پرونده</h3>
        <asp:DropDownList ID="drpUserRoles" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpUserRoles_SelectedIndexChanged"></asp:DropDownList>
        <asp:GridView ID="grdNaghsList" DataKeyNames="NaghsId" OnRowDataBound="grdNaghsList_RowDataBound" OnRowCommand="grdNaghsList_RowCommand" CssClass="table table-bordered table-condensed" runat="server" AutoGenerateColumns="false">
            <HeaderStyle CssClass="bg-blue" />
            <Columns>
                <asp:TemplateField HeaderText="ردیف">
                    <ItemTemplate>
                        <asp:Label Text="<%# Container.DataItemIndex+1 %>" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="شماره درخواست" DataField="StudentRequestID" />
                <asp:BoundField HeaderText="شماره دانشجویی" DataField="StCode" />
                <asp:BoundField HeaderText="گیرنده" DataField="Erae_Be" />
                <asp:BoundField HeaderText="فرستنده" DataField="RequestLogID" />
                <asp:BoundField HeaderText="تاریخ اعلام نقص" DataField="SubmitDate" />
                <asp:BoundField HeaderText="توضیحات نواقص" DataField="NaghsMessage" />
                <asp:BoundField HeaderText="توضیحات رفع نواقص" DataField="ResolveMessage" />
                <asp:TemplateField HeaderText="درج پیام">
                    <ItemTemplate>
                        <asp:Button Text="درج پیام" runat="server" CommandName="message" CommandArgument='<%# Eval("NaghsId") %>' CssClass="btn btn-dark" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="عملیات">
                    <ItemTemplate>
                        <asp:Button Text="رفع نقص" runat="server" CommandName="resolve" CommandArgument='<%# Eval("NaghsId") %>' CssClass="btn btn-danger" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <telerik:RadWindow ID="rdwMessage" runat="server">
                    <ContentTemplate>
                        <div dir="rtl" class="container">
                            <h4>توضیحات رفع نقص</h4>
                            <asp:TextBox ID="txtResolveDescription" runat="server" />
                            <asp:Button ID="btnSubmitResoleDesc" CssClass="btn btn-success" OnClick="btnSubmitResoleDesc_Click" Text="ثبت توضیحات" runat="server" />
                        </div>
                        <asp:HiddenField ID="hdfNaghsId" runat="server" />
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </div>
</asp:Content>
