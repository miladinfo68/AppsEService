<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employmentActionHistory.aspx.cs" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" Inherits="IAUEC_Apps.UI.University.CooperationRequest.employmentActionHistory" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" dir="rtl">
        <telerik:RadGrid runat="server" ID="grdReport"  BorderWidth="10px" AllowFilteringByColumn="true" AllowSorting="true" Skin="MyCustomSkin" EnableEmbeddedSkins="false" AllowPaging="true" PageSize="100">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView AutoGenerateColumns="false">
            <ItemStyle Font-Names="tahoma" HorizontalAlign="Center" BorderStyle="Ridge" />
            <HeaderStyle Font-Names="tahoma" ForeColor="GreenYellow" />
            <AlternatingItemStyle Font-Names="tahoma" HorizontalAlign="Center" />
                <Columns>
                    <telerik:GridBoundColumn HeaderText="کد استاد" DataField="code_ostad"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="نام" DataField="name"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="نام خانوادگی" DataField="family"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="مرتبه" DataField="nameMartabe"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="تاریخ درخواست ویرایش" DataField="CreateDate"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
