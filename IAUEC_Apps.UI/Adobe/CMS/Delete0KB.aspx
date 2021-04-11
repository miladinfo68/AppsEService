<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="Delete0KB.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.Delete0KB" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%">
        <tr><td>ترم را انتخاب نمایید:<telerik:RadComboBox ID="cmb_Term" Runat="server" AutoPostBack="True" EmptyMessage="انتخاب نمایید" OnSelectedIndexChanged="cmb_Term_SelectedIndexChanged">
            </telerik:RadComboBox>
            </td></tr>
        </table>
            <div style="background-color:#e0dfdf;border-radius:5px;border:1px solid #808080;padding:5px;margin:5px 0px 5px 0px" class="col-md-12">
                <p style="font-weight:bold">فایل های mp3.zip</p>
    <asp:Button ID="Btn_Click" runat="server" OnClick="Button1_Click" Text="حذف فولدرهای کم حجم" Font-Names="tahoma" Font-Size="Small" Font-Bold="True" />
            <asp:Button ID="btn_DeleteArchive" runat="server" Font-Bold="True" OnClick="btn_DeleteArchive_Click" Text="افزودن به لیست فولدر های سالم" />
           
   
    <telerik:RadGrid ID="grd_DeleteInformationClass" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="True" OnNeedDataSource="grd_DeleteInformationClass_NeedDataSource" Skin="Sunset" AllowSorting="True">
        <MasterTableView>
            <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center" />
            <CommandItemStyle Font-Names="tahoma" />
            <ItemStyle HorizontalAlign="Center" />
            <AlternatingItemStyle HorizontalAlign="Center" />
            <Columns>
                <telerik:GridTemplateColumn ItemStyle-Width="5px">
                            <ItemTemplate>                   
                        <asp:CheckBox ID="chk" runat="server" Width="20px" />                        
                    </ItemTemplate>
                             <HeaderTemplate>
                <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="headerChkbox_CheckedChanged"
                  AutoPostBack="True" />
              </HeaderTemplate>
                        </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" >
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                  <%--  <ItemTemplate>
                        <asp:HiddenField runat="server" Value='<%#Eval("AssetID") %>' ID="assetId" />
                        
                    </ItemTemplate>--%>
                </telerik:GridTemplateColumn>
                
                <telerik:GridBoundColumn DataField="fd" HeaderText="تاریخ"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="classcode" HeaderText="شناسه کلاس"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="term" HeaderText="ترم" AllowSorting="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FileSize" HeaderText="(KB)حجم فایل"></telerik:GridBoundColumn>
                
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                         <a href='ShowDetailFiles.aspx?ClassCode=<%#Eval("classcode") %>&Date=<%#Eval("FileDate") %>&t=<%#Eval("Term") %>'>مشاهده فایل ها</a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn Visible="false">
                    <ItemTemplate>
                     <asp:HiddenField ID="filedate" Value='<%#Eval("FileDate") %>' runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid></div>
    <div  style="background-color:#808080; border-radius:5px;border:1px solid #CCCCCC; padding:5px;margin:5px 0px 5px 0px" class="col-md-12">
        <p style="font-weight:bold; color: #FFFFFF;">فایل های mp3.*</p>
         <asp:Button ID="btn_del" runat="server"  Text="حذف فولدرهای کم حجم" Font-Names="tahoma" Font-Size="Small" Font-Bold="True" OnClick="btn_del_Click" />
            <asp:Button ID="btn_AddFailedList" runat="server" Font-Bold="True"  Text="افزودن به لیست فولدر های سالم" OnClick="btn_AddFailedList_Click" />
    <telerik:RadGrid ID="grd_NoZipFile" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="True" OnNeedDataSource="grd_NoZipFile_NeedDataSource" Skin="Sunset" AllowSorting="True" >
        <MasterTableView>
            <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center" />
            <CommandItemStyle Font-Names="tahoma" />
             <ItemStyle HorizontalAlign="Center" />
            <AlternatingItemStyle HorizontalAlign="Center" />
            <Columns>
                <telerik:GridTemplateColumn ItemStyle-Width="5px">
                            <ItemTemplate>                   
                        <asp:CheckBox ID="chk0" runat="server" Width="20px" />                        
                    </ItemTemplate>
                             <HeaderTemplate>
                <asp:CheckBox ID="headerChkbox0" runat="server" OnCheckedChanged="headerChkbox_CheckedChanged"
                  AutoPostBack="True" />
              </HeaderTemplate>
                        </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    <%--<ItemTemplate>
                        <asp:HiddenField runat="server" Value='<%#Eval("AssetID") %>' ID="assetId0" />
                    </ItemTemplate>--%>
                </telerik:GridTemplateColumn>
                
                <telerik:GridBoundColumn DataField="fd" HeaderText="تاریخ"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="classcode" HeaderText="شناسه کلاس"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="term" HeaderText="ترم"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FileSize" HeaderText="(KB)حجم فایل"></telerik:GridBoundColumn>
                
        <telerik:GridTemplateColumn>
                    <ItemTemplate>
                         <a href='ShowDetailFiles.aspx?ClassCode=<%#Eval("classcode") %>&Date=<%#Eval("FileDate") %>&t=<%#Eval("Term") %>'>مشاهده فایل ها</a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn Visible="false">
                    <ItemTemplate>
                     <asp:HiddenField ID="filedate" Value='<%#Eval("FileDate") %>' runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid></div>
    <uc1:AccessControl ID="AccessControl1" runat="server" />
</asp:Content>
