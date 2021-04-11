<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/CMSEmailMaster.Master" AutoEventWireup="true" CodeBehind="List_FinalEmailOk.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.CMS.List_FinalEmailOk" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../University/Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style>
    .RadGrid .rgFilterRow input {
            height:25px;
        }
    </style>
    <div class="container" dir="rtl">
   
         <uc1:AccessControl ID="AccessControl1" runat="server" />
        <div class="row">
    <telerik:RadGrid ID="grd_ListFinalEmailOk" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="False" OnNeedDataSource="grd_ListFinalEmailOk_NeedDataSource"  AllowFilteringByColumn="True" EnableEmbeddedSkins="False">
        <MasterTableView>
            <HeaderStyle CssClass="bg-orange" Font-Names="tahoma" />
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="name" HeaderText="نام و نام خانوادگی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../images/filter2.png">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Stcode" HeaderText="شماره دانشجویی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../images/filter2.png">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="mobile" HeaderText="موبایل" AllowFiltering="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email_Address" HeaderText="پست الکترونیکی" AllowFiltering="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OldEmail" HeaderText="پست الکترونیکی دوم" AllowFiltering="false" >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PersianDate" HeaderText="تاریخ" AllowFiltering="false" >
                </telerik:GridBoundColumn>                
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
            </div>
    
        </div>
</asp:Content>
