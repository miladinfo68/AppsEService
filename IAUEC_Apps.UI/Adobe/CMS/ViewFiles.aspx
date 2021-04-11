<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="ViewFiles.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.ViewFiles" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
     <link href="../../University/Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Input.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Ajax.MyCustomSkin.css" rel="stylesheet" />
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%" class="table-responsive">
        <tr>
            <td style="width: 15%; font-size: small;">ترم را وارد نمایید:</td>
            <td>
                <telerik:RadComboBox ID="DDL_Term" Runat="server" AutoPostBack="True" EmptyMessage="انتخاب نمایید" OnSelectedIndexChanged="DDL_Term_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
            <td>
                <uc1:AccessControl ID="AccessControl1" runat="server" />
            </td>
        </tr>
    </table>
    <telerik:RadGrid ID="grd_ViewFiles" runat="server" AllowPaging="true" PageSize="20" AllowFilteringByColumn="true" AutoGenerateColumns="false" OnNeedDataSource="grd_ViewFiles_NeedDataSource" 
        OnPreRender="grdViewFiles_PreRender" Skin="MyCustomSkin" EnableEmbeddedSkins="false" EnableViewState="false" >
        <MasterTableView AllowFilteringByColumn="true">
            <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center" />
            <ItemStyle Font-Names="tahoma" Font-Size="Small" />
            <AlternatingItemStyle  Font-Names="tahoma" Font-Size="Small" />
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="namedanesh" HeaderText="نام دانشکده" AllowFiltering="true"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="nameresh" HeaderText="نام رشته" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Class_Code" HeaderText="شناسه کلاس" AllowFiltering="true"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="namedars" HeaderText="نام درس" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FileDate" HeaderText="جلسه مورخ" AllowFiltering="false"></telerik:GridBoundColumn>
              
                <telerik:GridTemplateColumn AllowFiltering="false">
                    <ItemTemplate>
                            <a href='ShowDetailFiles.aspx?ClassCode=<%#Eval("Class_Code") %>&Date=<%#Eval("FileDate") %>&t=<%#Eval("Term") %>'>مشاهده فایل ها</a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>

</asp:Content>
