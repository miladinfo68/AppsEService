<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="TAPresentDetailsReport.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.TAPresentDetailsReport" %>
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
     <asp:Panel ID="p" runat="server" DefaultButton="btn_Search">
    <div dir="rtl">
        <asp:Label ID="lbl_FieldStar" runat="server" Text="فیلدهای ستاره دار باید پر شود" ForeColor="Red"></asp:Label>
        <br />
        *<asp:Label ID="lbl_CodeOstad" runat="server" Text="کداستادیار"></asp:Label>
        <asp:TextBox ID="txt_ProfCode2" runat="server" Width="150"></asp:TextBox>
        *<asp:Label ID="lbl_CodeClass" runat="server" Text="کد کلاس"></asp:Label>
        <asp:TextBox ID="txt_ClassCode1" runat="server" Width="150"></asp:TextBox>
        <asp:Label ID="lbl_Erfaq" runat="server" Text="ارفاق"></asp:Label>
        <asp:TextBox ID="txt_Erfaq" runat="server" Width="150"></asp:TextBox>
        <br />
        <asp:Label ID="lbl_txtDateStart" runat="server" Text="از تاریخ"></asp:Label>
        <asp:TextBox ID="txt_DateStart" runat="server" Width="150" placeholder="1393/06/17"></asp:TextBox>
        <asp:Label ID="lbl_DateEnd" runat="server" Text="تا تاریخ"></asp:Label>
        <asp:TextBox ID="txt_DateEnd" runat="server" Width="150" placeholder="1393/06/17"></asp:TextBox>
        <asp:Label ID="lbl_SelectSemester" runat="server" Text="انتخاب نیمسال"></asp:Label>
        <asp:DropDownList ID="ddl_Nimsal" runat="server"></asp:DropDownList>
        <asp:Button ID="btn_Search" runat="server" Text="جستجو" OnClick="btn2_Click" />         
    </div>
         </asp:Panel>
    <div dir="rtl">
        <asp:Label ID="lbl_TeacherName" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_LessonName" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_TimeErfagh" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_CountHazer" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_countQayeb" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_CountLate" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_CountSoon" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_Image" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    <asp:ImageButton ID="ConvertExcel" runat="server" ImageUrl="~/Adobe/images/Excel02.jpg"
            OnClick="ConvertExcel_Click" AlternateText="Convert To Excel" Width="40"/>

    <telerik:RadGrid ID="grd_TAPresentDetailsReport" AllowPaging="true" PageSize="20" runat="server"  AutoGenerateColumns="False"  Skin="MyCustomSkin" CellSpacing="0" GridLines="None" EnableEmbeddedSkins="False" >   
        

        <MasterTableView Dir="RTL">
            <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />

            <Columns >
              
                  <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>                                 
                <telerik:GridBoundColumn DataField="SumOfTime" HeaderText="مجموع دقایق حضور">
                </telerik:GridBoundColumn>   
                <telerik:GridBoundColumn DataField="TimeClass" HeaderText="مجموع دقایق کلاس">
                </telerik:GridBoundColumn> 
         
              
            </Columns>

        </MasterTableView>


    </telerik:RadGrid>
 
     <uc1:AccessControl ID="AccessControl1" runat="server" />
 
</asp:Content>
