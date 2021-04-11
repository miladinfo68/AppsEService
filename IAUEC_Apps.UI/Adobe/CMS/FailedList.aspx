<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="FailedList.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.FailedList" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <asp:Literal ID="t" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%">
        <tr><td>ترم را انتخاب نمایید:<telerik:RadComboBox ID="cmb_Term" Runat="server" AutoPostBack="True" EmptyMessage="انتخاب نمایید" OnSelectedIndexChanged="cmb_Term_SelectedIndexChanged">
            </telerik:RadComboBox>
            <uc1:AccessControl ID="AccessControl1" runat="server" />
            </td></tr>
        <tr><td>
    <asp:Button ID="Btn_returnToList" runat="server" Text="بازگرداندن به لیست فایل ها" Font-Names="tahoma" Font-Size="Small" Font-Bold="True" OnClick="Btn_returnToList_Click" />
            
            </td></tr>
    </table>
    <telerik:RadGrid ID="grd_FailedList" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="True" OnNeedDataSource="grd_FailedList_NeedDataSource" PageSize="50" Skin="Sunset">
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
                    
                </telerik:GridTemplateColumn>
                
                <telerik:GridBoundColumn DataField="FileDate" HeaderText="تاریخ"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="class_code" HeaderText="شناسه کلاس"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="term" HeaderText="ترم"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
