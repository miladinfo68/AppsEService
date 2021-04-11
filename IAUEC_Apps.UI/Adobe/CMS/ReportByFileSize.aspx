<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="ReportByFileSize.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.ReportByFileSize" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <asp:Literal ID="t" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%">
        <tr><td style="width: 30%">ترم را انتخاب نمایید:<telerik:RadComboBox ID="cmb_Term" Runat="server" AutoPostBack="True" EmptyMessage="انتخاب نمایید" >
            </telerik:RadComboBox>
            </td><td style="width: 10%">حجم کمتر از:(KB)</td><td style="width: 5%">
            <asp:TextBox ID="txt_FileSize" runat="server" Width="40px">0</asp:TextBox>
            </td><td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="مشاهده" />
                
                <asp:ImageButton ID="ImageButton1" runat="server"  AlternateText="ExcelML" ImageUrl="~/Adobe/images/Excel02.jpg" OnClick="ImageButton1_Click" />
                <uc1:AccessControl ID="AccessControl1" runat="server" />
            </td></tr>
        </table>

    <div  style="background-color:#808080; border-radius:5px;border:1px solid #CCCCCC; padding:5px;margin:5px 0px 5px 0px" class="col-md-12">
    
         
            <asp:Button ID="btn_AddFailedList" runat="server" Font-Bold="True"  Text="افزودن به لیست فولدر های سالم" OnClick="btn_AddFailedList_Click" />
    <telerik:RadGrid ID="grd_FileList" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="True" OnNeedDataSource="grd_FileList_NeedDataSource" Skin="Sunset" PageSize="20" AllowSorting="True">
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
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    <%--<ItemTemplate>
                        <asp:HiddenField runat="server" Value='<%#Eval("AssetID") %>' ID="assetId0" />
                    </ItemTemplate>--%>
                </telerik:GridTemplateColumn>
                
                <telerik:GridBoundColumn DataField="FileDate" HeaderText="تاریخ"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Class_Code" HeaderText="شناسه کلاس"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="term" HeaderText="ترم"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FileSize" HeaderText="(KB)حجم فایل"></telerik:GridBoundColumn>
                
        <telerik:GridTemplateColumn>
                    <ItemTemplate>
                         <a href='ShowDetailFiles.aspx?ClassCode=<%#Eval("Class_Code") %>&Date=<%#Eval("FileDate") %>&t=<%#Eval("Term") %>'>مشاهده فایل ها</a>
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
</asp:Content>
