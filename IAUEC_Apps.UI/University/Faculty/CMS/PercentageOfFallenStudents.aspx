<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="PercentageOfFallenStudents.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.PercentageOfFallenStudents" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server"><title><asp:Literal ID="t" runat="server"></asp:Literal></title>
<%--<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />--%>
     <style>
    .class {
            top:0px;
        }
    </style>
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
      <div  class="col-md-1">  <asp:Label ID="lbl_Term" Text="ترم :" runat="server" div="center"></asp:Label>    </div>
         <div class="col-md-2"><asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" Width="160px" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
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
            <sup>*</sup>
            </span>
        </div>
      <div  class="col-md-1"> <asp:Label ID="lbl_Degree" Text="مقطع :" runat="server"></asp:Label></div>
            <div class="col-md-2"><asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                      <ContentTemplate>
                          <asp:DropDownList ID="ddl_Degree" runat="server" OnSelectedIndexChanged="ddl_Degree_SelectedIndexChanged" Width="160px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                      </ContentTemplate>
                             <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="ddl_Degree" EventName="SelectedIndexChanged" />
                             </Triggers>
                   </asp:UpdatePanel></div>
       </div>
    <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-3"><asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                             <ContentTemplate>
                                <asp:RadioButton ID="rdb_Mardudi" runat="server" Text="دانشجویان افتاده" GroupName="PercentStu" ValidationGroup="0" AutoPostBack="true" OnCheckedChanged ="rdb_Ghabuli_CheckedChanged"/>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="rdb_Mardudi" />
                             </Triggers>
                   </asp:UpdatePanel></div>
    </div>

     <div class="row">
        <div class="col-md-3"></div>
        <div  class="col-md-3"><asp:Label ID="lbl_Percent" Text="درصد دانشجویان افتاده بیشتر از :" runat="server"></asp:Label></div>
       <div class="col-md-2"><asp:TextBox ID="txt_Percent" runat="server" Width="50px" MaxLength="2"></asp:TextBox></div>
     </div>

    <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-3"><asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                             <ContentTemplate>
                                <asp:RadioButton ID="rdb_Ghabuli" runat="server" Text="دانشجویان قبول شده" GroupName="PercentStu" ValidationGroup="1" Checked="true" AutoPostBack="true" OnCheckedChanged ="rdb_Ghabuli_CheckedChanged"/>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="rdb_Ghabuli" />
                             </Triggers>
                   </asp:UpdatePanel></div>
              </div>
                  <div class="row">
                     <div class="col-md-3"></div>
                      <div  class="col-md-3"><asp:Label ID="lbl_PercentGhabul" Text="درصد دانشجویان قبول شده بیشتر از :" runat="server"></asp:Label></div>
                 <div class="col-md-2"><asp:TextBox ID="txt_PercentGhabul" runat="server" Width="50px" MaxLength="2"></asp:TextBox></div>
            </div>

    <div class="row">
     <div class="col-md-4"></div>
      <div  class="col-md-2"><asp:Button ID="btn_ShowInfo" runat="server" Text="نمایش اطلاعات" OnClick="btn_ShowInfo_Click" CssClass="btn btn-primary"/></div></div>
         <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="nameostad" HeaderText=" نام خانوادگی استاد" />
                    <%--<asp:BoundField DataField="tterm" HeaderText="نیمسال" />--%>
                    <asp:BoundField DataField="Degree" HeaderText="مقطع" />
                    <asp:BoundField DataField="zarfklas" HeaderText="ظرفیت کلاس" />
                    <asp:BoundField DataField="mardud" HeaderText="تعداد دانشجویان افتاده" />
                    <asp:BoundField DataField="CountStu" HeaderText="درصد دانشجویان افتاده" />
               </Columns>
             </asp:GridView>
             </div>

           <div id="Div2" runat="server">
             <asp:ImageButton ID="img_ExportToExcel2" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel2_Click" Visible="false" />
            <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="nameostad" HeaderText=" نام خانوادگی استاد" />
                    <%--<asp:BoundField DataField="tterm" HeaderText="نیمسال" />--%>
                    <asp:BoundField DataField="Degree" HeaderText="مقطع" />
                    <asp:BoundField DataField="zarfklas" HeaderText="ظرفیت کلاس" />
                    <asp:BoundField DataField="mardud" HeaderText="تعداد دانشجویان قبول شده" />
                    <asp:BoundField DataField="CountStu" HeaderText="درصد دانشجویان قبول شده" />
               </Columns>
             </asp:GridView>
             </div>

     <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>  
     <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />  <uc1:AccessControl ID="AccessControl1" runat="server" />
</div>
</asp:Content>
