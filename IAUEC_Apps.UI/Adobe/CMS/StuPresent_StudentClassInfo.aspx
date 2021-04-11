<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="StuPresent_StudentClassInfo.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.StuPresent_StudentClassInfo" %>
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
            <asp:Label ID="lbl_FieldStar" runat="server" Text="فیلدهای ستاره دار باید پر شود" ForeColor="Red"></asp:Label>
            <br />
            *<asp:Label ID="lbl_StudentNumber" runat="server" Text="شماره دانشجویی"></asp:Label>
            <asp:TextBox ID="txt_Stcode" runat="server" Width="150" ></asp:TextBox>
            *<asp:Label ID="lbl_Class" runat="server" Text="مشخصه کلاس"></asp:Label>
            <asp:TextBox ID="txt_ClassCode" runat="server" Width="150"></asp:TextBox>
            <asp:Label ID="lbl_SelectSemester" runat="server" Text="انتخاب نیمسال"></asp:Label>
            <asp:DropDownList ID="ddl_Nimsal" runat="server"></asp:DropDownList>
            <asp:Button ID="btn_StudentTime" runat="server" Text="مشاهده" OnClick="btn_StudentFinder_Click" />      
        </div>
        <div dir="rtl">
            <asp:Label ID="lbl_StudentName" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lbl_ClassCode" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lbl_LessonName" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="lbl_ProfName" runat="server" Text=""></asp:Label><br />
        </div><br />
    
        <asp:ImageButton ID="ConvertExcel" runat="server" ImageUrl="~/Adobe/images/Excel02.jpg"
                OnClick="ConvertExcel_Click" AlternateText="Convert To Excel" Width="40"/>
        <telerik:RadGrid ID="grd_StuPeresentStuClassInfo" AllowPaging="true" PageSize="20" runat="server" AutoGenerateColumns="False"  Skin="MyCustomSkin" CellSpacing="0" GridLines="None" EnableEmbeddedSkins="False">   
            <MasterTableView Dir="RTL">            
                <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
                <HeaderStyle Font-Names="tahoma" />
                <AlternatingItemStyle Font-Names="tahoma" />                     
                <Columns >
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                    </telerik:GridTemplateColumn>                            
                    <telerik:GridBoundColumn DataField="PersianDate" HeaderText="تاریخ کلاس">
                    </telerik:GridBoundColumn>    
                   <%-- <telerik:GridBoundColumn DataField="TimeStart" HeaderText="زمان شروع">
                    </telerik:GridBoundColumn>  
                    <telerik:GridBoundColumn DataField="TimeEND" HeaderText="زمان پایان">
                    </telerik:GridBoundColumn>  --%>
                    <telerik:GridBoundColumn DataField="SumOfTime" HeaderText="دقایق حضور">
                    </telerik:GridBoundColumn>  
                    <telerik:GridBoundColumn DataField="TimeClass" HeaderText="زمان کلاس">
                    </telerik:GridBoundColumn>           
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>


        <uc1:AccessControl ID="AccessControl1" runat="server" />
    </asp:Panel>

</asp:Content>
