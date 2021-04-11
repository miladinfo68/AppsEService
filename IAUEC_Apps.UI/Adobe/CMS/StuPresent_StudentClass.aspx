<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="StuPresent_StudentClass.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.StuPresent_StudentClass" %>
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
    <asp:Panel ID="p" runat="server" DefaultButton="btn_StudentTime">
        <div dir="rtl">
            <asp:Label ID="lbl_FieldStar" runat="server" Text="فیلد ستاره دار باید پر شود" ForeColor="Red"></asp:Label>
            <br />
            *<asp:Label ID="lbl_Student" runat="server" Text="شماره دانشجویی"></asp:Label>
            <asp:TextBox ID="txt_Stcode" runat="server" Width="150"></asp:TextBox>
            <asp:Label ID="lbl_SelectSemester" runat="server" Text="انتخاب نیمسال"></asp:Label>
            <asp:DropDownList ID="ddl_Nimsal" runat="server"></asp:DropDownList>
            <asp:Button ID="btn_StudentTime" runat="server" Text="مشاهده" OnClick="btn_StudentFinder_Click" />      
            <uc1:AccessControl ID="AccessControl1" runat="server" />
        </div>
        <telerik:RadGrid ID="grd_StuPresentStudentClass" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="False"  Skin="MyCustomSkin" CellSpacing="0" GridLines="None" EnableEmbeddedSkins="False" >    
            <MasterTableView Dir="RTL">            
                <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
                <HeaderStyle Font-Names="tahoma" />
                <AlternatingItemStyle Font-Names="tahoma" />                     
                <Columns >
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                    <telerik:GridBoundColumn DataField="Stcode" HeaderText="شماره دانشجویی">
                    </telerik:GridBoundColumn>             
                    <telerik:GridBoundColumn DataField="StName" HeaderText="نام دانشجو">
                    </telerik:GridBoundColumn>    
                    <telerik:GridBoundColumn DataField="ClassCode" HeaderText="مشخصه درس">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="LessonName" HeaderText="نام درس">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TeacherName" HeaderText="نام استاد">
                    </telerik:GridBoundColumn>    
                    <telerik:GridBoundColumn DataField="SumOfTime" HeaderText="مجموع حضور">
                    </telerik:GridBoundColumn>           
                    <telerik:GridHyperLinkColumn   DataNavigateUrlFields= "stcode,ClassCode,tterm" Text="مشاهده جزئیات"
                        DataNavigateUrlFormatString= "~/Adobe/CMS/StuPresent_StudentClassInfo.aspx?stcode={0}&ClassCode={1}&tterm={2}">
                    </telerik:GridHyperLinkColumn> 
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </asp:Panel>
</asp:Content>
