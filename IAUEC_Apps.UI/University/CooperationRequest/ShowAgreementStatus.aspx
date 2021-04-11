<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAgreementStatus.aspx.cs" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" Inherits="IAUEC_Apps.UI.University.CooperationRequest.ShowAgreementStatus" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="direction: rtl;">

        <div class="row">
            <div class="col-md-2">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Text="اکسل" OnClick="btnExcel_Click" />
            </div>
        </div>
        <div>

            <telerik:RadGrid runat="server" ID="grdAgreement" PageSize="50" AutoGenerateColumns="false" AllowPaging="true"
                EnableEmbeddedSkins="false" AllowFilteringByColumn="True" OnNeedDataSource="grdAgreement_NeedDataSource"
                Skin="MyCustomSkin">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                            <ItemTemplate>
                                <%# Container.DataSetIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="code_ostad" HeaderText="کد استاد"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کد ملی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="نام و نام خانوادگی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="mobile" HeaderText="تلفن همراه"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="agreementStatus" HeaderText="وضعیت تفاهم نامه"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="vazfal" HeaderText="وضعیت همکاری استاد"></telerik:GridBoundColumn>

                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>

    </div>
    
    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
