<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListTadakholKlass.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ListTadakholKlass" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
   <%--<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <telerik:RadCodeBlock runat="server" ID="code">
    <script>

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
        }
    </script></telerik:RadCodeBlock>
     <div class="Report-Area" dir="rtl">
                 <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel" OnAjaxRequest="RadAjaxManager1_AjaxRequest1">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="txt_CodeOstad" LoadingPanelID="LsitLoadingPanel"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>                 
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="LsitLoadingPanel"></telerik:RadAjaxLoadingPanel>
    <div class="row">
     <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
        </div>
      <div  class="col-md-1">  <asp:Label runat="server" ID="lbl_Term" Text="ترم :"></asp:Label>    
      </div>
        <div class="col-md-2"><asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                             <ContentTemplate>
                                 <asp:DropDownList ID="ddl_Term" runat="server" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" AutoPostBack="true" Width="127px" CssClass="form-control input-sm"></asp:DropDownList>
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
      <div  class="col-md-1"> <asp:Label ID="lbl_CodeOstad" Text="کد استاد :" runat="server" ></asp:Label></div>
                     <div class="col-md-1"><asp:TextBox ID="txt_CodeOstad" runat="server" Width="127px" CssClass="form-control"></asp:TextBox></div>
                  <div  class="col-md-1"><asp:Button ID="btn_CodeOstad" runat="server" Text="انتخاب کنید" OnClick="btn_CodeOstad_Click" CssClass="btn btn-success"/>
</div>
             </div>
             <div class="row">
     <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
        </div>
      <div  class="col-md-1"><asp:Label ID="lbl_NumberClass" runat="server" Text="شماره کلاس :"></asp:Label></div>
       <div class="col-md-2"><asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                             <ContentTemplate>
                                  <asp:DropDownList ID="ddl_NumberClass" runat="server" Width="128px" AutoPostBack="true" OnSelectedIndexChanged="ddl_NumberClass_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_NumberClass" EventName="SelectedIndexChanged" />
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
                <div  class="col-md-1"><asp:Label ID="lbl_Day" runat="server" Text="روز :"></asp:Label></div>
                <div class="col-md-1"><asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                             <ContentTemplate>
                                   <asp:DropDownList ID="ddl_Day" runat="server" Width="128px" AutoPostBack="true" OnSelectedIndexChanged="ddl_Day_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Day" EventName="SelectedIndexChanged" />
                             </Triggers>
                   </asp:UpdatePanel></div>
               </div>
 <%--               <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-2"><asp:Label ID="lbl_Clock" runat="server" Text="ساعت شروع :"></asp:Label></div>
                     <div class="col-md-3"><asp:TextBox ID="txt_Clock" runat="server" Text="  :  " MaxLength="5" CssClass="form-control" Width="50px"></asp:TextBox></div>
                </div>--%>
                 <div class="row">
     <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div  class="col-md-5"><asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                             <ContentTemplate>
                                     <asp:RadioButton ID="rdb_ListByOstad" runat="server" Text="لیست تداخل کلاس بر اساس استاد"  GroupName="ListTadakhol" ValidationGroup="0" Checked="true"/>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Day"/>
                             </Triggers>
                   </asp:UpdatePanel></div></div>
                  <div class="row">
     <div class="col-md-3"></div>
             <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div  class="col-md-5"><asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                             <ContentTemplate>
                             <asp:RadioButton ID="rdb_ListByNumberClass" runat="server" Text="لیست تداخل کلاس بر اساس شماره کلاس"  GroupName="ListTadakhol" ValidationGroup="1"/>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="rdb_ListByNumberClass"/>
                             </Triggers>
                   </asp:UpdatePanel></div></div>
               <div class="row">
     <div class="col-md-4"></div>
      <div  class="col-md-3"><asp:Button ID="btn_ShowList" runat="server" Text="نمایش لیست"  OnClick="btn_ShowList_Click" CssClass="btn btn-primary"/></div></div>
            <br />
             <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="idroz1" HeaderText="روز" />
                    <asp:BoundField DataField="saatstart" HeaderText="ساعت شروع" />
                    <asp:BoundField DataField="saatend" HeaderText="ساعت پایان" />
                    <asp:BoundField DataField="numklass" HeaderText="شماره کلاس" />
                    <asp:BoundField DataField="did_tad" HeaderText="مشخصه1" />
                    <asp:BoundField DataField="sstart" HeaderText="ساعت شروع1" />
                    <asp:BoundField DataField="send" HeaderText="ساعت پایان1" />
                    <asp:BoundField DataField="numk2" HeaderText="شماره کلاس 1" />
               </Columns>
             </asp:GridView>
             </div> 
                  <div id="Div2" runat="server">
             <asp:ImageButton ID="img_ExportToExcel2" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel2_Click" Visible="false" />
            <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="idroz1" HeaderText="روز" />
                    <asp:BoundField DataField="saatstart" HeaderText="ساعت شروع" />
                    <asp:BoundField DataField="saatend" HeaderText="ساعت پایان" />
                    <asp:BoundField DataField="numklass" HeaderText="شماره کلاس" />
                    <asp:BoundField DataField="did_tad" HeaderText="مشخصه1" />
                    <asp:BoundField DataField="sstart" HeaderText="ساعت شروع1" />
                    <asp:BoundField DataField="send" HeaderText="ساعت پایان1" />
                    <asp:BoundField DataField="numklas2" HeaderText="شماره کلاس 1" />
               </Columns>
             </asp:GridView>
             </div> 
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
           
     <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" /><uc1:AccessControl ID="AccessControl1" runat="server" />
</div>
</asp:Content>
