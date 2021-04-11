<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="StuPresent_Studentinfo.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.StuPresent_Studentinfo" %>
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
    <asp:Panel ID="p" runat="server" DefaultButton="btn_StudentFinder">
    <div dir="rtl">
        <asp:Label ID="lbl_Family" runat="server" Text="نام خانوادگی"></asp:Label>
        <asp:TextBox ID="txt_LastName" runat="server" Width="150" Text=""></asp:TextBox>
        <asp:Label ID="lbl_Name" runat="server" Text="نام"></asp:Label>
        <asp:TextBox ID="txt_FirstName" runat="server" Width="150" Text=""></asp:TextBox>
        <asp:Label ID="lbl_StudentCode" runat="server" Text="شماره دانشجویی"></asp:Label>
        <asp:TextBox ID="txt_StudentCode" runat="server" Width="150" Text=""></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="انتخاب نیمسال"></asp:Label>
        <asp:DropDownList ID="ddl_Nimsal" runat="server"></asp:DropDownList>
        <asp:Button ID="btn_StudentFinder" runat="server" Text="جستجو" OnClick="btn_StudentFinder_Click" />
    </div>
    <uc1:AccessControl ID="AccessControl1" runat="server" />
    <telerik:RadGrid ID="grd_StuPreSentStuInfo" runat="server" AutoGenerateColumns="False" AllowPaging="true"   Skin="MyCustomSkin" CellSpacing="0" GridLines="None" EnableEmbeddedSkins="False" PageSize="60">    
        <MasterTableView Dir="RTL">            
            <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />                     
            <RowIndicatorColumn Visible="False">
            </RowIndicatorColumn>
            <ExpandCollapseColumn Created="True">
            </ExpandCollapseColumn>
            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="name" HeaderText="نام">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی">
                </telerik:GridBoundColumn>              
                <telerik:GridBoundColumn DataField="namep" HeaderText="نام پدر">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="sal_vorod" HeaderText="سال ورود">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="nameresh" HeaderText="رشته تحصیلی">
                </telerik:GridBoundColumn>
                <telerik:GridHyperLinkColumn   DataNavigateUrlFields= "stcode,tterm" Text="مشاهده جزئیات"
                    DataNavigateUrlFormatString= "~/Adobe/CMS/StuPresent_StudentClass.aspx?stcode={0}&tterm={1}">
                </telerik:GridHyperLinkColumn> 
            </Columns>
        </MasterTableView>
        <PagerStyle FirstPageToolTip="صفحه اول" LastPageToolTip="آخرین صفحه" NextPagesToolTip="صفحه بعدی" NextPageToolTip="صفحه بعدی" PageSizeLabelText="تعداد اطلاعات در صفحه:" PrevPagesToolTip="صفحه قبلی" PrevPageToolTip="صفحه قبلی" />
    </telerik:RadGrid>
        </asp:Panel>
</asp:Content>
