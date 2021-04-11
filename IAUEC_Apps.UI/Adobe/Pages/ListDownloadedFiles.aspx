<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/PageAdobeMaster.Master" AutoEventWireup="true" CodeBehind="ListDownloadedFiles.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.Pages.ListDownloadedFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    لیست فایل های دانلود شده
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grd_ListDownloadedFile" runat="server" Skin="Sunset">
        <MasterTableView DataKeyNames="AssetClassCode" AutoGenerateColumns="false">
             <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center" />
            <ItemStyle  Font-Names="tahoma" Font-Size="Small" />
            <AlternatingItemStyle  Font-Names="tahoma" Font-Size="Small" />
            <Columns>
                  <telerik:GridBoundColumn DataField="AssetClassCode" HeaderText="مشخصه کلاس">

                 </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="FileDate" HeaderText="تاریخ">

                 </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="Session" HeaderText="جلسه">

                 </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="Term" HeaderText="ترم">

                 </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="SaveDate" HeaderText="تاریخ دریافت فایل">

                 </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
