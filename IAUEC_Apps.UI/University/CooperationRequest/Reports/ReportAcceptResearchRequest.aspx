<%@ Page Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="ReportAcceptResearchRequest.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Reports.ReportAcceptResearchRequest" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3 style="color: #007E33">گزارش درخواست های پذیرفته شده توسط واحد پژوهش
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

            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="portalCode" HeaderText="کد استاد" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کد ملی" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="namep" HeaderText="نام پدر" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="martabeh" HeaderText="مرتبه" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="madrak" HeaderText="آخرین مدرک تحصیلی" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="name_resh" HeaderText="رشته" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="date" HeaderText="تاریخ تایید توسط پژوهش" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="نمایش" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btn_Detail" Text="مشاهده جزئیات" runat="server" CommandName="Detail" CommandArgument='<%#Eval("ID")%>' CssClass="btn btn-warning" Width="100px" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        
    </div>
</asp:Content>
