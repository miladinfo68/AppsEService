<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="RegistrationOfStudent.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.RegistrationOfStudent" %>
<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="RegistrationOfStudent.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.RegistrationOfStudent"  %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <script>
   //get a reference to the current RadWindow 
     function CloseAndRebind(args) {
        GetRadWindow().BrowserWindow.refreshGrid(args);
        GetRadWindow().close();
    }

    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)

        return oWindow;
    }
                          
</script>
   <div class="Report-Area" dir="rtl">
        <div> <p style="font-weight: 700;font-size:x-large;text-align:right;direction:rtl;font-family:Tahoma">
          جستجوی دانشجو</p>
        </div>
    <br />
       
    <br />
  <div id="main" style="background:bisque no-repeat scroll 0% 0%" >
        <telerik:RadGrid ID="grd_Student" runat="server" OnItemCommand="grd_Student_ItemCommand" AllowFilteringByColumn="True" AutoGenerateColumns="false"
        CellSpacing="0" GridLines="None" 
            OnNeedDataSource="grd_Student_NeedDataSource">
         <MasterTableView DataKeyNames="stcode">
              <ItemStyle Font-Names="tahoma" /> 
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />        
                <Columns>      
                     <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="name" HeaderText="نام و نام خانوادگی" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کدملی" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="idd" HeaderText="شماره شناسنامه" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="namep" HeaderText="نام پدر" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="idresh" HeaderText="رشته" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="magh" HeaderText="مقطع" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="codebayegan" HeaderText="کدبایگانی" AllowFiltering="true">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn DataField="payed" HeaderText="مانده نهایی" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="name_vazkol" HeaderText="وضعیت کلی" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn>  
                    <ItemTemplate>
                        <asp:Button ID="btn_Select" Text="انتخاب"  runat="server" CommandName="Select" CommandArgument='<%#Eval("stcode") %>'/>
                        </ItemTemplate>
                        </telerik:GridTemplateColumn>
<%--                     <telerik:GridButtonColumn CommandName="Select" Text="انتخاب" UniqueName="Select">
            </telerik:GridButtonColumn>--%>
                     </Columns>
            </MasterTableView>
            </telerik:RadGrid>
       <asp:Label ID ="lbl_Field" runat="server" Visible="false"></asp:Label>
       <asp:Label ID ="lbl_Education" runat="server" Visible="false"></asp:Label>
       <asp:Label ID ="lbl_Sex" runat="server" Visible="false"></asp:Label>
       <asp:Label ID ="lbl_Degree" runat="server" Visible="false"></asp:Label>
       <asp:Label ID="lbl_Salvorud" runat="server" Visible="false"></asp:Label>
   <%--</div>--%>

   </div>
</asp:Content>
