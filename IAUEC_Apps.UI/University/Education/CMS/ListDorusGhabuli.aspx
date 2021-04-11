<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="ListDorusGhabuli.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.ListDorusGhabuli" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListDorusGhabuli.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.ListDorusGhabuli" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
  <title><asp:Literal ID="t" runat="server"></asp:Literal></title>     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal>
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
       <div class="col-md-1">
                    <asp:Label ID="lbl_Term" runat="server" Text="ترم :" Width="80px"></asp:Label></div>
       <div class="col-md-1">
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                       <ContentTemplate>
                            <asp:DropDownList ID="ddl_Term" runat="server" Width="200px" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel></div></div>

       <div class="row">
      <div class="col-md-3"></div>
      <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
        </div>
       <div class="col-md-1">
                    <asp:Label ID="lbl_Degree" runat="server" Text="مقطع :" Width="80px"></asp:Label></div>
       <div class="col-md-1">
                 <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                       <ContentTemplate>
                            <asp:DropDownList ID="ddl_Degree" runat="server" Width="200px" OnSelectedIndexChanged="ddl_Degree_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Degree" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel></div></div>

        <div class="row">
      <div class="col-md-3"></div>
            <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div> 
       <div class="col-md-1"><asp:Label Id="lbl_Field" runat="server" text="نام رشته :" width="80px"></asp:Label></div>
        <div class="col-md-1">
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                       <ContentTemplate>
                          <asp:DropDownList ID="ddl_Field" runat="server" Width="200px" OnSelectedIndexChanged="ddl_CodeField_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm">
                          </asp:DropDownList>
            
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel></div></div>

        <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div>
       <div class="col-md-1">
                    <asp:Label ID="lbl_Geraesh" runat="server" Text="گرایش :" Width="80px"></asp:Label></div>
      <div class="col-md-1">
             <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                       <ContentTemplate>
                        <asp:DropDownList ID="ddl_Geraesh" runat="server" Width="200px" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddl_Geraesh_SelectedIndexChanged" >                        
                        </asp:DropDownList>
                       </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Geraesh" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel></div></div>
                    
        <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div>
       <div class="col-md-1">
                    <asp:Label ID="lbl_CodeDars" runat="server" Text="نام درس :" Width="80px"></asp:Label></div>
      <div class="col-md-1">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                       <ContentTemplate>
                            <asp:DropDownList ID="ddl_CodeDras" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddl_CodeDras_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_CodeDras" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel></div></div>
                   
           <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3"><asp:CheckBox ID="chk_ListStu" runat="server" Text="  لیست دانشجویان قبول شده" /></div></div>
           <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3">
           <asp:RadioButton ID="rd_Ghabuli" runat ="server" Text="تعداد قبولی ها" Checked="true" /></div></div>

      <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div class="col-md-1"><asp:Button ID="btn_ShowReport" runat="server" OnClick="btn_ShowReport_Click" Text="نمایش اطلاعات" CssClass="btn btn-success"/></div></div> 
       
        <div id="Div5" runat="server">
           <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
                 AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
             <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
                  RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                    <Columns>
                    <asp:BoundField DataField="sal_vorod" HeaderText="سال ورود" />
                    <asp:BoundField DataField="nkol" HeaderText="تعداد کل" />
                    <asp:BoundField DataField="vaz_adi" HeaderText="تعداد وضعیت عادی" />
                    <asp:BoundField DataField="vaz_faregh" HeaderText="تعداد فارغ التحصیلی" />
                    <asp:BoundField DataField="n_sabtename" HeaderText="تعداد ثبت نام" />
                    <asp:BoundField DataField="n_ghaboli" HeaderText="تعداد قبولی" />
                    <asp:BoundField DataField="n_termjary" HeaderText="تعداد ترم جاری" />
                    <asp:BoundField DataField="nkol" HeaderText="باقیمانده" />
                </Columns>
            </asp:GridView>
            </div> 

      <div id="Div1" runat="server">
        <asp:ImageButton ID="img_ExportToExcel2" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel2_Click" Visible="false" />
          <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="sal_vorod" HeaderText="سال ورود" />
                    <asp:BoundField DataField="name" HeaderText="نام " />
                     <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                     <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="namedars" HeaderText=" درس" />
                    <asp:BoundField DataField="magh" HeaderText="مقطع تحصیلی" />
                    <asp:BoundField DataField="dorpar1" HeaderText="سیستم آموزشی" />
                   <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                </Columns>
            </asp:GridView>
            </div>         
    <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />  
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager><uc1:AccessControl ID="AccessControl1" runat="server" />
    </div>
</asp:Content>
