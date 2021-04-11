<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="C_Request.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.CMS.C_Request" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <asp:Label ID="lbl_Customers" runat="server" Text="انتخاب مشتری"></asp:Label>
        <asp:DropDownList ID="ddl_Customers" runat="server"></asp:DropDownList>
        <br />
        <asp:Button ID="btn_ShowRequest" runat="server" Text="مشاهده درخواست" OnClick="btn_ShowRequest_Click" />
    </div>




    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False"  Skin="MyCustomSkin" CellSpacing="0" GridLines="None" EnableEmbeddedSkins="False" >    
        <MasterTableView Dir="RTL">            
            <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />                     
            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>  
                <telerik:GridBoundColumn DataField="Name" HeaderText="نام درس">
                </telerik:GridBoundColumn>             
                <telerik:GridBoundColumn DataField="UserCount" HeaderText="تعداد کاربر">
                </telerik:GridBoundColumn>    
                <telerik:GridBoundColumn DataField="SessionCount" HeaderText="تعداد جلسات">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DateStart" HeaderText="تاریخ شروع">
                </telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="DateEnd" HeaderText="تاریخ پایان">
                </telerik:GridBoundColumn>  

                <telerik:GridHyperLinkColumn DataNavigateUrlFields= "Id" Text="تایید درخواست"
                    DataNavigateUrlFormatString= "C_AcceptRequest.aspx?Id={0}">
                </telerik:GridHyperLinkColumn> 
                <telerik:GridHyperLinkColumn DataNavigateUrlFields= "Id" Text="رد درخواست"
                    DataNavigateUrlFormatString= "C_RejectionRequest.aspx?Id={0}">
                </telerik:GridHyperLinkColumn> 
                          
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>



</asp:Content>
