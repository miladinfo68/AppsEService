<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="SearchProfByParams.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.SearchProfByParams"  %>--%>
<%@ Page Title="" Language="C#"  CodeBehind="SearchProfByParams.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.SearchProfByParams" %>
<!DOCTYPE html>
<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />
<html lang="en">
<head id="header" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
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
    جستجوی استاد</p>
     </div>
    <br />
     <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
    <br />

    <div class="Report-Area">
      <div class="row" style="direction:rtl">
        <div class="col-md-3"></div>
          <div class="col-md-1">
            <asp:Label ID="lbl_StCode" runat="server" Width="150px" Text="کد استاد :"></asp:Label>
           </div>
            <div class="col-md-2">
                <asp:TextBox ID="txt_ProfCode" runat="server"></asp:TextBox>
            </div>
          <div class="col-md-2">
               <asp:Label ID="lbl_Family" runat="server" Width="150px" Text=" نام خانوادگی :"></asp:Label>
           </div>
           <div class="col-md-2">
               <asp:TextBox ID="txt_Family" runat="server" ></asp:TextBox>
           </div>
       </div>
       <div class="row" style="direction:rtl">
            <div class="col-md-3"></div>
            <div class="col-md-1">
                <asp:Label ID="lbl_NameEp" runat="server" Width="150px" Text="نام :"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txt_NameEp" runat="server"></asp:TextBox>
            </div>
          <div class="col-md-2">
               <asp:Label ID="lbl_CoopOstad" runat="server" Width="150px" Text=" نحوه همکاری :"></asp:Label>
           </div>
           <div class="col-md-2">
                    <asp:UpdatePanel ID="UP1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_Cooperation" runat="server" Width="150px" AutoPostBack="true" CssClass="form-control input-sm" OnSelectedIndexChanged="ddl_Cooperation_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Cooperation" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
           </div>
       </div>

        <div class="row" style="direction:rtl">
            <div class="col-md-3"></div>
            <div class="col-md-2"></div>
            <div class="col-md-2">
                <asp:Button ID="btn_ProfCode" runat="server" Text="جستجو " Width="110px" class="btn btn-success btn-lg" OnClick="btn_ProfCode_Click"/>
            </div>
        </div>


   <div id="main" style="background:bisque no-repeat scroll 0% 0%" >
        <telerik:RadGrid ID="grd_Faculty" runat="server" OnItemCommand="grd_faculty_ItemCommand" AllowFilteringByColumn="false"
             AllowMultiRowSelection="false" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" 
            OnNeedDataSource="grd_Faculty_NeedDataSource">
             <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
         <MasterTableView DataKeyNames="code_ostad">
              <ItemStyle Font-Names="tahoma" /> 
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />        
                <Columns>     
                     <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridBoundColumn DataField="code_ostad" HeaderText="کد استاد" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="namep" HeaderText="نام پدر" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="idd" HeaderText="شماره شناسنامه" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="name_nahveh" HeaderText="نحوه همکاری" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn>  
                    <ItemTemplate>
                        <asp:Button ID="btn_Select" Text="انتخاب" UniqueName="Select" runat="server" CommandName="Select" CommandArgument='<%#Eval("code_ostad") %>' />
                        </ItemTemplate>
                        </telerik:GridTemplateColumn>
              <%-- <telerik:GridButtonColumn CommandName="Select" Text="انتخاب" UniqueName="Select">
            </telerik:GridButtonColumn>--%>
                     </Columns>
            </MasterTableView>
            </telerik:RadGrid>
   </div>
       <asp:Label ID="lbl_Term" runat="server" Visible="false"></asp:Label>
       <asp:Label ID="lbl_Lesson" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_Departman" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_Cooperation" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_Daneshkade" runat="server" vidible="false"></asp:Label>
        <asp:Label ID="lbl_NumberClass" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_AzD" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_TaD" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_AzJ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_TaJ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_NumberAbsence" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_FromDate" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_ToDate" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_Day" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_Field" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_Zarib" runat="server" Visible="false"></asp:Label>
         <asp:Label ID="lbl_did" runat="server" Visible="false"></asp:Label>
    </div>
    <telerik:RadWindowManager ID="rwd" runat="server" ></telerik:RadWindowManager>
        </form>
</body></html>

