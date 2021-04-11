<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="ShowFiles.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.ShowFiles" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<%--<%@ Reference Control="~/CommonUI/AccessControl.ascx">--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../University/Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Input.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Ajax.MyCustomSkin.css" rel="stylesheet" />
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="width:100%">
          انتخاب ترم :
    <telerik:RadComboBox ID="rcb_Term" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="rcbTerm_SelectedIndexChanged"></telerik:RadComboBox>
          <%--<uc1:AccessControl ID="AccessControl1" runat="server" />--%>
    </div>
    <telerik:RadGrid ID="grd_ShowFile" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource" Skin="MyCustomSkin" EnableEmbeddedSkins="false" AutoGenerateColumns="false" 
        OnItemDataBound="RadGrid1_ItemDataBound" AllowFilteringByColumn="True" AllowPaging="True" PageSize="50" OnItemCommand="RadGrid1_ItemCommand">
        <MasterTableView>
            <HeaderStyle Font-Names="tahoma" Font-Size="Small" />
            <ItemStyle Font-Names="tahoma" Font-Size="Small" />
            <AlternatingItemStyle Font-Names="tahoma" Font-Size="Small" />
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="AssetID" HeaderText="" Visible="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FileDate" HeaderText="جلسه" AllowFiltering="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Class_Code" HeaderText="مشخصه کلاس"></telerik:GridBoundColumn>
        
                <telerik:GridBoundColumn DataField="Term" Visible="True" HeaderText="ترم" AllowFiltering="false"></telerik:GridBoundColumn>
              <telerik:GridTemplateColumn AllowFiltering="false">
                  <HeaderTemplate>
                      <h6>حجم فایل ها(کیلو بایت)</h6>
                  </HeaderTemplate>
                  <ItemTemplate>
                   
                      <asp:Label ID="lbl_FileSize" runat="server"></asp:Label>
                  </ItemTemplate>
              </telerik:GridTemplateColumn>
                   <telerik:GridTemplateColumn AllowFiltering="false">
                  <HeaderTemplate>
                      <h6>دانلود فایل</h6>
                  </HeaderTemplate>
                  <ItemTemplate>
                      <asp:Button ID="btn_Download_flv" runat="server" Text="دریافت فایل(flv)"  CommandName="flv"  CommandArgument='<%#Eval("AssetID") %>'/>
                        <asp:Button ID="btn_Download_mp3" runat="server" Text="دریافت فایل(mp3)" CommandName="mp3"  CommandArgument='<%#Eval("AssetID") %>'/>
                       <asp:Button ID="btn_Download_avi" runat="server" Text="دریافت فایل(avi)" CommandName="avi"  CommandArgument='<%#Eval("AssetID") %>'/>
                  </ItemTemplate>
              </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
