<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/PageAdobeMaster.Master" AutoEventWireup="true" CodeBehind="DownloadFile.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.Pages.DownloadFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../css/ListViewHierarchy.css" rel="stylesheet" />
    <style>
        .textAdel{
            text-align:center;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    دریافت فایل ها
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lbl_User" runat="server" Visible="false"></asp:Label>
            <telerik:RadListView ID="lstDownload" runat="server"
            ItemPlaceholderID="ProductTitlePlaceHolder" CssClass="textAdel" OnItemCommand="lstDownload_ItemCommand" OnItemDataBound="lstDownload_ItemDataBound">
            <LayoutTemplate>
                <asp:Panel ID="HierarchyPanel" runat="server" CssClass="wrapper">
                    <table id="tbl_products" class="products textAdel table table-responsive">
                        <thead>
                            <tr>
                                
                                
                                <th >
                                    ردیف
                                </th>
                                 <th >
                                   مشخصه کلاس
                                </th>
                               
                                 <th >
                                    نام فایل
                                </th>
                               
                                 <th >
                                    نام درس
                                </th>
                                 <th ">
                                    ترم
                                </th>
                                  <th>
                                    دریافت فایل
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
                                    
                                    <td style="width:40px">
                                        <%#Eval("rowId") %>
                                    </td>
                                    <td style="width:100px">
                                        <asp:Label ID="lbl_Term" runat="server" Text='<%#Eval("Term") %>' Visible="false"></asp:Label>
                                       <asp:Label ID="lbl_ClassCode" runat="server" Text='<%#Eval("Class_Code") %>'></asp:Label>
                                        <asp:Label ID="lbl_Date" runat="server" Text='<%#Eval("FileDate") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td  style="width:300px">
                                        جلسه مورخ : <%#Eval("FileDate") %> <asp:Label ID="AssetID" Visible="false" runat="server" Text='<%#Eval("AssetID") %>'></asp:Label>
                                    </td>
                                 
                                    <td class="textAdel" >
                                        <%#Eval("namedars") %>
                                    </td>
                                    <td>
                                      <asp:Label ID="Label2" runat="server" text='<%#Eval("Term") %>' CssClass="textAdel"></asp:Label>
                                   </td>
                                    <td>
                                       <%--<asp:Label ID="lbl_URl" runat="server" Visible="false" Text='<%#Eval("URL_File") %>'></asp:Label>--%>
                                       <asp:Button ID="btn_Download_flv" runat="server" Text="(flv)دریافت فایل" CommandName="dnlbtnFlv" CommandArgument='<%#Eval("AssetID") %>'  Enabled="false"/>
                                       <asp:Button ID="btn_Download_avi" runat="server" Text="(avi)دریافت فایل" CommandName="dnlbtnavi" CommandArgument='<%#Eval("AssetID") %>' Enabled="false"/>
                                       <asp:Button ID="btn_Download_mp3" runat="server" Text="(MP3)دریافت فایل" CommandName="dnlbtnMp3" CommandArgument='<%#Eval("AssetID") %>' Enabled="false"/>
                                   </td>
                                </tr>
                            </ItemTemplate>
            
        </telerik:RadListView>

</asp:Content>
