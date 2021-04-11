<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="ProfPresentReports.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.ProfPresentReports" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <link href="../css/Grid.MyCustomSkin.css" rel="stylesheet" />
    <link href="../css/Input.MyCustomSkin.css" rel="stylesheet" />
    <link href="../css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../css/Ajax.MyCustomSkin.css" rel="stylesheet" />
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <asp:Panel ID="p" runat="server" DefaultButton="btn_Prof">
       <div dir="rtl"> 
             <asp:Label ID="lbl_Daneshkade" runat="server" Text="دانشکده" Width="100px"></asp:Label>
                 <asp:DropDownList ID="ddl_Daneshkade" runat="server" Width="150px" ></asp:DropDownList>
        
        <asp:Label ID="lbl_Nimasl" runat="server" Text="انتخاب نیمسال"></asp:Label>
        <asp:DropDownList ID="ddl_Nimsal" runat="server"></asp:DropDownList>
        <asp:Button ID="btn_Prof" runat="server" Text="مشاهده" OnClick="btn2_Click" />
    </div>
        </asp:Panel>
    <asp:ImageButton ID="ConvertExcel" runat="server" ImageUrl="~/Adobe/images/Excel02.jpg"
            OnClick="ConvertExcel_Click" AlternateText="Convert To Excel" Width="40"/>
    <uc1:AccessControl ID="AccessControl1" runat="server" />
    <telerik:RadGrid ID="grd_ProfPresentReport" AllowPaging="true" PageSize="100" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="MyCustomSkin"  EnableEmbeddedSkins="False" OnNeedDataSource="grd_ProfPresentReports_NeedDataSource">        
        <MasterTableView Dir="RTL">
            <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />

            
            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>
                 <telerik:GridBoundColumn DataField="LOGIN" HeaderText="کد استاد"  >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ProfName" HeaderText="نام استاد"  >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="did" HeaderText="مشخصه کلاس">
                </telerik:GridBoundColumn> 
                 <telerik:GridBoundColumn DataField="namedanesh" HeaderText="نام دانشکده">
                </telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="namedars" HeaderText="نام درس">
                </telerik:GridBoundColumn>    
                <telerik:GridBoundColumn DataField="Sum_Vahed" HeaderText="مجموع واحد">
                </telerik:GridBoundColumn> 
                <telerik:GridBoundColumn DataField="name_mahal" HeaderText="نحوه تدریس">
                </telerik:GridBoundColumn>                
                <telerik:GridBoundColumn DataField="totalTime" HeaderText="مجموع دقایق حضور">
                </telerik:GridBoundColumn>                   
                <telerik:GridBoundColumn DataField="meetingCount" HeaderText="جلسات حضور">
                </telerik:GridBoundColumn>  
               
          
              
               <%-- <telerik:GridHyperLinkColumn   DataNavigateUrlFields= "code,LOGIN,Leniency,tterm" Text="مشاهده جزئیات"
                    DataNavigateUrlFormatString= "~/Adobe/CMS/ProfPresentDetailsReport.aspx?code={0}&LOGIN={1}&Leniency={2}&tterm={3}">
                </telerik:GridHyperLinkColumn> --%>
            </Columns>

        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
