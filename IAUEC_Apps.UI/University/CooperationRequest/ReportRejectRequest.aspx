<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="ReportRejectRequest.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Reports.ReportRejectRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3 style="color: blue">گزارش درخواست های رد شده توسط کارگزینی
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
            <div class="col-md-5">
            </div>
            <div class="col-md-2">
                <asp:Button ID="btn_excel" runat="server" OnClick="btn_excel_Click" Enabled="true" Text="تبدیل به اکسل" CssClass="btn btn-success" />
            </div>
            <div class="col-md-5">
            </div>
        </div>
    <div dir="rtl">
        <telerik:RadGrid ID="grd_Show" runat="server" PageSize="50" BorderWidth="10px"
            AutoGenerateColumns="false" HorizontalAlign="Center" OnExcelMLWorkBookCreated="grd_Show_ExcelMLWorkBookCreated" OnNeedDataSource="grd_Show_NeedDataSource" AllowPaging="true"
            OnItemCommand="grd_Show_ItemCommand"
            EnableEmbeddedSkins="false" AllowFilteringByColumn="True" Skin="MyCustomSkin">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView DataKeyNames="ID">
                <ItemStyle Font-Names="tahoma" HorizontalAlign="Center" BorderStyle="Ridge" />
                <HeaderStyle Font-Names="tahoma" />
                <AlternatingItemStyle Font-Names="tahoma" HorizontalAlign="Center" />
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-Font-Bold="true">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="SidaCode" HeaderText="کد استاد در سیدا" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="portalCode" HeaderText="کد استاد در پرتال" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کد ملی" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="namep" HeaderText="نام پدر" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="mobile" HeaderText="موبایل" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="add_email" HeaderText="پست الکترونیکی" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="date" HeaderText="تاریخ رد درخواست" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="نمایش" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:Button ID="btn_Detail" Text="مشاهده جزئیات" runat="server" CommandName="Detail" CommandArgument='<%#Eval("ID")%>' CssClass="btn btn-success" Width="100px" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        
    </div>
</asp:Content>
