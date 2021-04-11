<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListAsatidDarHarTerm.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ListAsatidDarHarTerm" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server"><title><asp:Literal ID="t" runat="server"></asp:Literal></title>
<%-- <link href="../../Theme/css/bootstrap.css" rel="stylesheet" />--%></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="Report-Area" >
    <div class="row">
     <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
        </div>
      <div  class="col-md-1">    <asp:Label ID="lbl_Term" Text="ترم :" runat="server"></asp:Label>    
    </div>
        <div class="col-md-2"> <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                             <ContentTemplate>
                                  <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                             </Triggers>
                       </asp:UpdatePanel></div>
    </div>
            <div class="row">
     <div class="col-md-3"></div>
              <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
                <div class="col-md-1">   <asp:Label ID="lbl_EducationGroup" runat="server" Text="گروه آموزشی :" Width="100px"></asp:Label></div>
              <div class="col-md-2">  <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                             <ContentTemplate>
                                   <asp:DropDownList ID="ddl_EducationGroup" runat="server" width="100%" OnSelectedIndexChanged="ddl_EducationGroup_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_EducationGroup" EventName="SelectedIndexChanged" />
                             </Triggers>
                       </asp:UpdatePanel></div>    
            </div>
           <div class="row">
     <div class="col-md-3"></div>
      <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
               <div class="col-md-1">  <asp:Label ID="lbl_Cooperation" runat="server" Text="نوع همکاری :"></asp:Label></div>
                <div class="col-md-2"> <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                             <ContentTemplate>
                                   <asp:DropDownList ID="ddl_Cooperation" runat="server" Width="100%" OnSelectedIndexChanged="ddl_Cooperation_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Cooperation" EventName="SelectedIndexChanged" />
                             </Triggers>
                       </asp:UpdatePanel></div>
           </div>
            <br />
               <div style="margin-right:5px;margin-left:5px">
        <div class="row">
         <div class="col-md-4"></div>
             <div class="col-md-2">   <asp:Button ID="btn_AccessCards" runat="server" Text="لیست کارت تردد"  OnClick ="btn_AccessCards_Click" CssClass="btn btn-primary"/> 
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" ToolTip="خروجی اکسل تردد"/></div>
             <div class="col-md-2"><asp:Button ID="btn_CurrentTerm" runat="server" Text="لیست اساتید ترم جاری" OnClick="btn_CurrentTerm_Click" CssClass="btn btn-primary"/> 
                  <asp:ImageButton ID="img_ExportToExcel2" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel2_Click" Visible="false" ToolTip="خروجی اکسل اساتید"/></div>
            </div>
                   </div>
              <div id="Div1" runat="server">
           
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                   <asp:BoundField DataField="name" HeaderText="نام" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="idostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="tterm" HeaderText="مهلت اعتبار" />
                    
                </Columns>
            </asp:GridView>
      </div>
             <div id="Div2" runat="server">
           
            <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="idostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="name" HeaderText="نام" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="namegroup" HeaderText="گروه" />
                    <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
                    <asp:BoundField DataField="sal_tav" HeaderText="سال تولد" />
                    <asp:BoundField DataField="namemadrak" HeaderText="آخرین مدرک تحصیلی" />
                    <asp:BoundField DataField="namemartabeh" HeaderText="مرتبه علمی" />
                    <asp:BoundField DataField="name_nahveh" HeaderText="نحوه همکاری" />
                    <asp:BoundField DataField="date_est" HeaderText="تاریخ استخدام" />
                    <asp:BoundField DataField="saat_movazaf" HeaderText="ساعت موظفی" />
                    
                    <asp:BoundField DataField="mobile" HeaderText="موبایل" />
                    <asp:BoundField DataField="tel_hom" HeaderText="تلفن منزل" />
                    <asp:BoundField DataField="add_email" HeaderText="آدرس ایمیل" />
                    <asp:BoundField DataField="vah" HeaderText="تعداد واحد تدریس" />
                    <asp:BoundField DataField="mazad" HeaderText="کسری / مازاد موظفی" />
                </Columns>
            </asp:GridView>
      </div>
           <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
          <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="1000px" Width="1050px"  ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" ScrollBarsMode="true"/><uc1:AccessControl ID="AccessControl1" runat="server" />
        </div>
</asp:Content>
