<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="ExcellentStudentsStudying.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.ExcellentStudentsStudying" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ExcellentStudentsStudying.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.ExcellentStudentsStudying"  %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1"%>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
<title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
  <asp:Literal ID="pt" runat="server"></asp:Literal>
    <h3 style="color:blue">
       گزارش دانشجویان ممتاز
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="Report-Area" dir="rtl">
   <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
        </div>
       <div  class="col-md-1">   
            <asp:Label ID="lbl_Term" Text="ترم:" runat="server" div="center"></asp:Label>
       </div>
        <div class="col-md-1"> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                             <ContentTemplate>
                                  <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" Width="160px" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                             </Triggers>
                       </asp:UpdatePanel>
        </div>
    </div>
     
     <div class="row">
      <div class="col-md-3"></div>
                  <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div> 
      <div class="col-md-1"> <asp:Label ID="lbl_Field" runat="server" Text="رشته ارائه دهنده :" Width="160px" ></asp:Label></div>
      <div class="col-md-1">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                             <ContentTemplate>
                               <asp:DropDownList ID="ddl_Field" runat="server" Width="150px" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel>                
          </div>
    </div>

     <div class="row">
       <div class="col-md-3"></div>
         <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div> 
             <div class="col-md-1"><asp:Label ID="lbl_Degree" runat="server" Text="مقطع کلاس :" Width="160px" ></asp:Label></div>
             <div class="col-md-1">
                   <asp:UpdatePanel ID="UpdatePanel6" runat="server" >
                             <ContentTemplate>
                     <asp:DropDownList ID="ddl_Degree" runat="server" OnSelectedIndexChanged="ddl_Degree_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm">
                    </asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Degree" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel> 
         </div>
    </div>
   
     <div class="row">
     <div class="col-md-3"></div>
        <div class="col-md-1">
          <span style="color:red; font-size:small;">
           <sup>*</sup>
          </span>
        </div> 
       <div class="col-md-1">
              <asp:Label ID="lbl_SalVorod" runat="server" Text="سال ورود :" Width="180px"></asp:Label></div>
        <div class="col-md-2">
            <asp:TextBox ID="txt_SalVorod" runat="server" MaxLength="2" Width="50px" CssClass="form-control" ></asp:TextBox>
         </div>
    </div>

   <div class="row">
     <div class="col-md-3"></div>
        <div class="col-md-1">
          <span style="color:red; font-size:small;">
           <sup>*</sup>
          </span>
        </div> 
     <div class="col-md-1"><asp:Label ID="lbl_NimsalVorod" runat="server" Text="نیمسال ورود :" Width="160px" ></asp:Label></div>
       <div class="col-md-1">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
          <ContentTemplate>
          <asp:DropDownList ID="ddl_NimsalVorod" runat="server" OnSelectedIndexChanged="ddl_NimsalVorod_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm">
          </asp:DropDownList>
           </ContentTemplate>
          <Triggers>
           <asp:AsyncPostBackTrigger ControlID="ddl_NimsalVorod" EventName="SelectedIndexChanged" />
         </Triggers>
      </asp:UpdatePanel> 
    </div>
</div>
      <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
          <span style="color:red; font-size:small;">
           <sup></sup>
          </span>
        </div> 
      <div class="col-md-2">
          <asp:Button id="btn_ShowList" runat="server" Text="نمایش اطلاعات" Font-Bold="true" OnClick="btn_ShowList_Click" ForeColor="RoyalBlue" CssClass="btn btn-success"/>
      </div>
    </div>
      <asp:ImageButton ID="img_ExportToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
      AlternateText="ExcelML" OnClick="img_ExportToExcel_Click" Visible="false"/>

        <telerik:RadWindowManager ID="rwd" runat="server" ></telerik:RadWindowManager>
         <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" Visible="false" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
 
      <asp:GridView ID="grd_Show" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
             <Columns>
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="familyy" HeaderText="نام خانوادگی و نام" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="avrg_term" HeaderText="معدل" />
                </Columns>
       </asp:GridView> 
      
      </div>
</asp:Content>
