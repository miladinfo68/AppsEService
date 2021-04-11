<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="CheckOutMashmoolan.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutMashmoolan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>سامانه تسویه حساب غیر حضوری - اداره مشمولان</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" dir="rtl">
        <div class=" col-sm-12 table-responsive">
            <h4>*لیست دانشجویان فارغ التحصیل مشمول خدمت که گواهی لغو معافیت آنها صادر نشده است.</h4>
            <asp:GridView ID="grdRequestList" CssClass="table table-bordered table-condensed table-striped" EmptyDataText="دانشجویی با این وضعیت یافت نشد." ShowHeaderWhenEmpty="true" OnRowCommand="grdRequestList_RowCommand" AutoGenerateColumns="false" runat="server">
                <HeaderStyle CssClass="bg-blue" />
                <Columns>
                    <asp:BoundField HeaderText="شماره درخواست تسویه" DataField="StudentRequestID" />
                    <asp:BoundField HeaderText="تاریخ ثبت درخواست" DataField="CreateDate" />
                    <asp:BoundField HeaderText="شماره دانشجوئی" DataField="StCode" />
                    <asp:BoundField HeaderText="نام دانشجو" DataField="name" />
                    <asp:BoundField HeaderText="تاریخ دفاع" DataField="Def_Date" />
                    <asp:BoundField HeaderText="نوع در خواست" DataField="RequestTypeName" />
                    <asp:BoundField HeaderText="وضعیت درخواست" DataField="RequestLogName" />
                    <asp:TemplateField HeaderText="عملیات">
                        <ItemTemplate>
                            <asp:Button ID="btnPrintGovahi" CommandName="print" CommandArgument='<%# Eval("StudentRequestID") %>' Text="چاپ گواهی لغو معافیت" CssClass="btn btn-danger btn-sm" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
