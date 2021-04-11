<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/PageAdobeMaster.Master" AutoEventWireup="true" CodeBehind="Archive.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.Pages.Archive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <link href="../css/ListViewHierarchy.css" rel="stylesheet" />
    <script src="../js/scripts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
   آرشیو
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblstcode" runat="server" Visible="false"></asp:Label>
      <div style="width:100%">
          انتخاب ترم :
    <telerik:RadComboBox ID="rcbTerm" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="rcbTerm_SelectedIndexChanged"></telerik:RadComboBox>
    </div>
      <telerik:RadListView ID="RadListView2" runat="server"
            ItemPlaceholderID="ProductTitlePlaceHolder">
            <LayoutTemplate>
                <asp:Panel ID="HierarchyPanel" runat="server" CssClass="wrapper">
                    <table id="tbl_products" class="products">
                        <thead>
                            <tr>
                                <th class="expand">
                                </th>
                                <th>
                                    لیست کلاس ها
                                </th>
                               
                            </tr>
                        </thead>
                        <tbody>
                            <tr id="ProductTitlePlaceHolder" runat="server"> 
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td class="expand">
                        <img src="../images/SinglePlus.gif" alt="toggle" onclick="toggleOrderDetails(this)"/>
                    </td>
                    <td colspan="4">
                      
                       مشخصه: <%#Eval("ClassCode") %>---<%# Eval("namedars") %>---<span style="font-weight: bold; padding-right: 5px; padding-left: 5px;">نام استاد:</span>
                        <%# Eval("Ost_Name") %>---<span style="font-weight: bold; padding-right: 5px; padding-left: 5px;"> روز برگزاری کلاس:</span><%#Eval("Klas_Day") %><span style="font-weight: bold; padding-right: 5px; padding-left: 5px;">زمان برگزاری کلاس:</span><%#Eval("ClassTime") %><asp:label ID="ClassCode" runat="server" Text='<%#Eval("ClassCode") %>' Visible="false"></asp:label>
                    </td>
                </tr>
                <tr class="orders" style="display: none;">
                    <td class="expand">
                    </td>
                    <td colspan="4">
                        <telerik:RadListView ID="RadListView3" runat="server"   ItemPlaceholderID="OrderDetailsPlaceHolder"
                           >
                            <LayoutTemplate>
                                <table>
                                    <asp:PlaceHolder ID="OrderDetailsPlaceHolder" runat="server"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Term" runat="server" Text='<%#Eval("Term") %>' Visible="false"></asp:Label><asp:Literal ID="ltr" runat="server"></asp:Literal>
                                         <asp:CheckBox runat="server" ID="chk_MeetingDate" />جلسه مورخ :<asp:Label ID="lbl_FDate" runat="server" Text=' <%#Eval("FileDate") %>'></asp:Label>  
                                        <asp:Label ID="lbl_AssetID" Visible="false" runat="server" Text='<%#Eval("AssetID") %>'></asp:Label>
                                        <asp:Label ID="lbl_ClassCode" runat="server" Visible="false" Text='<%#Eval("Class_Code") %>'></asp:Label>
                                         <a href='ViewFiles.aspx?ClassCode=<%#Eval("Class_Code") %>&Date=<%#Eval("FileDate") %>&Ast=<%#Eval("AssetID") %>&t=<%#Eval("Term") %>'>مشاهده فایل ها</a>
                                    </td>
                                   
                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                                   </td>
                </tr>
            </ItemTemplate>
        </telerik:RadListView>
    <asp:Button ID="btn_Select" runat="server" CssClass="Redbtn" Text="پرداخت" OnClick="btnSelect_Click" Visible="false" />
</asp:Content>
