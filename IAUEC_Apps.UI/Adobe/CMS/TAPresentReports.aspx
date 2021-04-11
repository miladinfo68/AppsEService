<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="TAPresentReports.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.TAPresentReports" %>
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
     <asp:Panel ID="p" runat="server" DefaultButton="btn_See">
        <div dir="rtl"> 
            <asp:Label ID="lbl_Erfagh" runat="server" Text="ارفاق"></asp:Label>
            <asp:TextBox ID="txt_Leniency" runat="server" Text="0" ></asp:TextBox>
            <asp:Label ID="lbl_SelectSemester" runat="server" Text="انتخاب نیمسال"></asp:Label>
            <asp:DropDownList ID="ddl_Nimsal" runat="server"></asp:DropDownList>    
            <asp:Label ID="Label1" runat="server" Text="انتخاب دپارتمان"></asp:Label>
            <asp:DropDownList ID="ddl_Department" runat="server"></asp:DropDownList>    
            <asp:Button ID="btn_See" runat="server" Text="مشاهده" OnClick="btn2_Click" />
        </div>
  </asp:Panel>
    <asp:ImageButton ID="ConvertExcel" runat="server" ImageUrl="~/Adobe/images/Excel02.jpg"
            OnClick="ConvertExcel_Click" AlternateText="Convert To Excel" Width="40"/>

        

    <telerik:RadGrid ID="grd_TAPresentReport" runat="server" AllowPaging="true" PageSize="20"  Skin="MyCustomSkin"  CellSpacing="0" GridLines="None" EnableEmbeddedSkins="False"  >  
        <MasterTableView Dir="RTL" AutoGenerateColumns="false" >
           <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />

            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Department" HeaderText="نام دانشکده"  >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="GroupName" HeaderText="نام گروه"  >
                </telerik:GridBoundColumn>   
                <telerik:GridBoundColumn DataField="LOGIN" HeaderText="کد استادیار"  >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TeacherName" HeaderText="نام استادیار"  >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="code" HeaderText="مشخصه کلاس">
                </telerik:GridBoundColumn> 
                <telerik:GridBoundColumn DataField="LessonName" HeaderText="نام درس">
                </telerik:GridBoundColumn>                 
                <telerik:GridBoundColumn DataField="SumOfTime" HeaderText="مجموع دقایق حضور">
                </telerik:GridBoundColumn>   
                <telerik:GridBoundColumn DataField="TimeClass" HeaderText="مجموع دقایق کلاس">
                </telerik:GridBoundColumn> 
                <telerik:GridBoundColumn DataField="Hozoor" HeaderText="جلسات حضور">
                </telerik:GridBoundColumn>  
               
          
              
                <telerik:GridHyperLinkColumn   DataNavigateUrlFields= "code,LOGIN,Leniency,tterm" Text="مشاهده جزئیات"
                    DataNavigateUrlFormatString= "~/Adobe/CMS/TAPresentDetailsReport.aspx?code={0}&LOGIN={1}&Leniency={2}&tterm={3}">
                </telerik:GridHyperLinkColumn> 
            </Columns>

            <EditFormSettings>
                <EditColumn CancelImageUrl="Cancel.gif" InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif">
                </EditColumn>
            </EditFormSettings>

        </MasterTableView>

<%--<FilterMenu EnableEmbeddedSkins="False"></FilterMenu>

<HeaderContextMenu EnableEmbeddedSkins="False"></HeaderContextMenu>--%>
    </telerik:RadGrid>





     <uc1:AccessControl ID="AccessControl1" runat="server" />





</asp:Content>
