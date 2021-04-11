<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchOfStudent.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.SearchOfStudent" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<!DOCTYPE html>
<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <div> <p style="font-weight: 700;font-size:x-large;text-align:right;direction:rtl;font-family:Tahoma">
          جستجوی دانشجو</p>
        </div>
    <br />
       
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
    <br />
        <div class="Report-Area">
        <div class="row" style="direction:rtl">
            <div class="col-md-3"></div>
            <div class="col-md-1">
                <asp:Label ID="lbl_StCode" runat="server" Width="150px" Text="شماره دانشجویی :"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txt_StCode" runat="server" MaxLength="14"></asp:TextBox>
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
               <asp:Label ID="lbl_IdMeli" runat="server" Width="150px" Text=" کد ملی :"></asp:Label>
           </div>
           <div class="col-md-2">
               <asp:TextBox ID="txt_IdMeli" runat="server" ></asp:TextBox>
         </div>
        </div>

        <div class="row" style="direction:rtl">
          <div class="col-md-3"></div>
             <div class="col-md-1">
                <asp:Label ID="lbl_Deg" runat="server" Text="مقطع :"></asp:Label></div>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UP1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_Degree" runat="server" Width="150px" AutoPostBack="true" CssClass="form-control input-sm" OnSelectedIndexChanged="ddl_Degree_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Degree" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div class="col-md-2">
                    <asp:Label ID="lbl_StatusStu" runat="server" Text="وضعیت دانشجو :"></asp:Label>
                </div>
                <div class="col-md-2">
                   <asp:UpdatePanel ID="UP2" runat="server" >
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_StatusStu" runat="server" Width="150px" AutoPostBack="true" CssClass="form-control input-sm" OnSelectedIndexChanged="ddl_StatusStu_SelectedIndexChanged"></asp:DropDownList> 
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_StatusStu" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>           
            </div>

          <div class="row" style="direction:rtl">
          <div class="col-md-3"></div>
             <div class="col-md-1">
                <asp:Label ID="lbl_Field1" runat="server" Text="رشته :"></asp:Label></div>

            <div class="col-md-2">
                <asp:UpdatePanel ID="UP3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_Field" Width="150px" AutoPostBack="true" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
              <div class="col-md-1">
              </div>
         <div class="col-md-2">
         <asp:Button ID="btn_stCode" runat="server" Text="جستجو " Width="110px" class="btn btn-success btn-lg" OnClick="btn_stCode_Click"/>
         </div>
      </div>
          
          
        <telerik:RadGrid ID="grd_Student" runat="server" OnItemCommand="grd_Student_ItemCommand"
        AutoGenerateColumns="false" HorizontalAlign="Center"
        CellSpacing="0" GridLines="None" 
            OnNeedDataSource="grd_Student_NeedDataSource1">
         <MasterTableView DataKeyNames="stcode">
              <ItemStyle Font-Names="tahoma" /> 
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />        
                <Columns> 
                    <telerik:GridTemplateColumn>  
                    <ItemTemplate>
                        <telerik:RadButton ID="btn_Select" Text="انتخاب" UniqueName="Select" runat="server" CommandName="Select" CommandArgument='<%#Eval("stcode") %>'></telerik:RadButton>
                        </ItemTemplate>
                        </telerik:GridTemplateColumn>
                     <telerik:GridBoundColumn DataField="name_vazkol" HeaderText="وضعیت کلی" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="payed" HeaderText="مانده نهایی" AllowFiltering="true">
                    </telerik:GridBoundColumn> 
                    <telerik:GridBoundColumn DataField="magh" HeaderText="مقطع" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="idresh" HeaderText="رشته" AllowFiltering="true">
                    </telerik:GridBoundColumn> 
                    <telerik:GridBoundColumn DataField="namep" HeaderText="نام پدر" AllowFiltering="true">
                    </telerik:GridBoundColumn> 
                    <telerik:GridBoundColumn DataField="idd" HeaderText="شماره شناسنامه" AllowFiltering="true">
                    </telerik:GridBoundColumn>   
                    <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کدملی" AllowFiltering="true">
                    </telerik:GridBoundColumn>  
                    <telerik:GridBoundColumn DataField="name" HeaderText="نام و نام خانوادگی" AllowFiltering="true">
                    </telerik:GridBoundColumn> 
                     <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" AllowFiltering="true">
                    </telerik:GridBoundColumn>               
                     <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                     </Columns>
            </MasterTableView>
            </telerik:RadGrid>
           <telerik:RadWindowManager ID="rwd" runat="server" ></telerik:RadWindowManager>
    <asp:Label ID="lbl_Daneshkade" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Field" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Degree" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Education" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Sex" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_SalVorod" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_NimsalVorod" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_TypeAccepted" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_StatusStu1" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Isargar" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Term" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Semat" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_FamilySemat" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_TarikhSodor" runat="server" Visible="false"></asp:Label>  
       </div>
    </form>
</body>
</html>
