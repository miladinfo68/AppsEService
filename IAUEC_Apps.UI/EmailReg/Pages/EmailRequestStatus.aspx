<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/PageEmailMaster.Master" AutoEventWireup="true" CodeBehind="EmailRequestStatus.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.Pages.EmailRequestStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grd_EmailStatus" runat="server" AutoGenerateColumns="False" Skin="Sunset" OnItemCommand="grd_EmailStatus_ItemCommand" OnNeedDataSource="grd_ListAfterStdRequest_NeedDataSource">
        <MasterTableView>
            <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center" />
            <ItemStyle Font-Names="tahoma" Font-Size="Small" />
            <AlternatingItemStyle  Font-Names="tahoma" Font-Size="Small" />
            <Columns>
            
<%--                <telerik:GridBoundColumn DataField="id" Visible="false">
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn DataField="PersianDate" HeaderText="تاریخ ثبت درخواست" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                </telerik:GridBoundColumn>
                <%--<telerik:GridBoundColumn DataField="Stcode" HeaderText="شماره دانشجویی" HeaderStyle-Width="120px" ItemStyle-Width="120px">
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn DataField="Email_Address" HeaderText="پست الکترونیکی">
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="vaziat" HeaderText="وضعیت درخواست">
                </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="Description" HeaderText="علت رد درخواست" Visible="true">
                </telerik:GridBoundColumn>
                 <%--<telerik:GridButtonColumn  Visible="false" Text="ثبت درخواست جدید" CommandName="NewRequest" HeaderText="جهت ثبت درخواست جدید روی لینک زیر کلیک نمایید">
                 </telerik:GridButtonColumn>--%>
             
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
