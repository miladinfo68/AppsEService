<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/CMSEmailMaster.Master" AutoEventWireup="true" CodeBehind="List_EmailOk.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.CMS.List_EmailOk" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../css/Grid.Office2010Silver.css" rel="stylesheet" />
        <link href="../../University/Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
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
        <div class="row">
      <asp:ImageButton ID="ExportToExcelImg" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="ExportToExcelImg_Click" />
            
      <uc1:AccessControl ID="AccessControl1" runat="server" />
            </div>
        <div class="row">
            <div class="col-md-12">
    <telerik:RadGrid ID="grd_ListEmailOk" AllowPaging="true" PageSize="20" runat="server" AutoGenerateColumns="False" OnNeedDataSource="RadGrid1_NeedDataSource"  AllowFilteringByColumn="True" EnableEmbeddedSkins="False">
        <MasterTableView>
            
            <HeaderStyle CssClass="bg-orange" Font-Names="tahoma" />
           
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="name" HeaderText="نام و نام خانوادگی"  AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../images/filter2.png">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Stcode" HeaderText="شماره دانشجویی"  AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../images/filter2.png">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="s_mobile" HeaderText="موبایل" AllowFiltering="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email_Address" HeaderText="پست الکترونیکی" AllowFiltering="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OldEmail" HeaderText="پست الکترونیکی دوم" AllowFiltering="false" >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PersianDate" HeaderText="تاریخ" AllowFiltering="false">
                </telerik:GridBoundColumn>                
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
                </div>
            </div>
    </div>
</asp:Content>
