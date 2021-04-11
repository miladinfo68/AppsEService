<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="GMPresentReport.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.GMPresentReport" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
       <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
     <link href="../css/Grid.MyCustomSkin.css" rel="stylesheet" />
    <link href="../css/Input.MyCustomSkin.css" rel="stylesheet" />
    <link href="../css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../css/Ajax.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
     <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <asp:Panel ID="p" runat="server" DefaultButton="btn_show">--%>
      <div class="row">
     <%--   <asp:Label ID="lbl_User" runat="server" Text="نام کاربری"></asp:Label>
        <asp:TextBox ID="txt_User" runat="server" Text="0" ></asp:TextBox>--%>
           <div class="col-md-12">
              <div class="col-md-1" >
        <asp:Label ID="lbl_SelectSemester" runat="server" Text="انتخاب نیمسال"></asp:Label>
               </div>  <div class="col-md-2" >
        <asp:DropDownList ID="ddl_Nimsal" runat="server" OnSelectedIndexChanged="ddl_Nimsal_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
</div>
               <div class="col-md-1"> <asp:Label ID="lbl_MG" runat="server" Text="نام مدیرگروه"></asp:Label></div>
   <div class="col-md-2">
             
            <asp:UpdatePanel ID="UpdatePanel" runat="server" >
                             <ContentTemplate>
        <asp:DropDownList ID="ddl_MG" runat="server"  AutoPostBack="true" ></asp:DropDownList>
                                  </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Nimsal" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel> 
</div>
               <div class="col-md-6 " >  <asp:Button ID="btn_show" runat="server" Text="مشاهده" OnClick="btnshow_Click" /></div>
</div>
          </div>
       <%-- <asp:Label ID="lbl_SelectDepartment" runat="server" Text="انتخاب دانشکده"></asp:Label>
        <asp:DropDownList ID="ddl_Department" runat="server"></asp:DropDownList>--%>
      
   <%--   </asp:Panel>--%>
       
    <asp:ImageButton ID="ConvertExcel" runat="server" ImageUrl="~/Adobe/images/Excel02.jpg"
            OnClick="ConvertExcel_Click" AlternateText="Convert To Excel" Width="40"/>

         <telerik:RadGrid ID="grd_GMPresentReports" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="MyCustomSkin" EnableEmbeddedSkins="False" OnNeedDataSource="grd_GMPresentReports_NeedDataSource">   
        <MasterTableView Dir="RTL">
            <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />

            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>
               <%-- <telerik:GridBoundColumn DataField="Department" HeaderText="نام دانشکده"  >
                </telerik:GridBoundColumn>--%>
               <%-- <telerik:GridBoundColumn DataField="GroupName" HeaderText="نام گروه"  >
                </telerik:GridBoundColumn>          --%>      
                <telerik:GridBoundColumn DataField="LOGIN" HeaderText="کد کاربری"  >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TeacherName" HeaderText="نام گروه"  >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="code" HeaderText=" کد گروه ">
                </telerik:GridBoundColumn> 
                <telerik:GridBoundColumn DataField="PersianDate" HeaderText="تاریخ حضور">
                </telerik:GridBoundColumn>   
                 <telerik:GridBoundColumn DataField="FirstLogin" HeaderText="ساعت شروع حضور">
                </telerik:GridBoundColumn>               
                <telerik:GridBoundColumn DataField="SumOfTime" HeaderText="مجموع دقایق حضور">
                </telerik:GridBoundColumn>   
               <%-- <telerik:GridBoundColumn DataField="TimeClass" HeaderText="مجموع دقایق کلاس">
                </telerik:GridBoundColumn> 
              <%--  <telerik:GridBoundColumn DataField="Hozoor" HeaderText="جلسات حضور">
                </telerik:GridBoundColumn> --%> 
               
          
              
              <%--  <telerik:GridHyperLinkColumn   DataNavigateUrlFields= "code,LOGIN,Leniency,tterm" Text="مشاهده جزئیات"
                    DataNavigateUrlFormatString= "~/Adobe/CMS/ProfPresentDetailsReport.aspx?code={0}&LOGIN={1}&Leniency={2}&tterm={3}">
                </telerik:GridHyperLinkColumn> --%>
            </Columns>

        </MasterTableView>
    </telerik:RadGrid>




    <uc1:AccessControl ID="AccessControl1" runat="server" />




</asp:Content>
