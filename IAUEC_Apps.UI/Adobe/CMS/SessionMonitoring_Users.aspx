<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="SessionMonitoring_Users.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.SessionMonitoring_Users" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lbl_MeeingName" runat="server" Text="-"></asp:Label>
    
    <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="False"  GridLines="None">   
        <MasterTableView Dir="RTL">                              
            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>                            
                <telerik:GridBoundColumn DataField="NAME" HeaderText="نام">
                </telerik:GridBoundColumn>    
                <telerik:GridBoundColumn DataField="LOGIN" HeaderText="نام کاربری">
                </telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="PermissionInfo" HeaderText="سطح دسترسی">
                </telerik:GridBoundColumn>                   
                <telerik:GridBoundColumn DataField="Device" HeaderText="نوع دسترسی">
                </telerik:GridBoundColumn>         
                <telerik:GridBoundColumn DataField="ip_address" HeaderText="IpAddress">
                </telerik:GridBoundColumn>            
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
