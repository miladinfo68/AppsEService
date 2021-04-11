<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ArzeshyabiAsatid.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ArzeshyabiAsatid" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1"%>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
<title><asp:Literal ID="t" runat="server"></asp:Literal></title>
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
       <div  class="col-md-1">   
            <asp:Label ID="lbl_Term" Text="ترم:" runat="server" div="center"></asp:Label>
       </div>
        <div class="col-md-1"> <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
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
            <sup></sup>
            </span>
        </div>
             <div class="col-md-1"> <asp:Label ID="lbl_CodeOstad" Text="کداستاد :" runat="server"></asp:Label></div>
             <div class="col-md-1"><asp:TextBox ID="txt_CodeOstad" runat="server" Width="100%"></asp:TextBox></div>
             <div class="col-md-1"><asp:Button ID="btn_Select" runat="server" Text="انتخاب کنید" OnClick="btn_Select_Click" class="btn btn-success" /></div>
         </div>
         <div class="row">
               <div class="col-md-3"></div>
            <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
             </div>
             <div class="col-md-1"> <asp:Label ID="lbl_CpdeGroup" Text="گروه :" runat="server"></asp:Label></div>
             <div class="col-md-1"> <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                             <ContentTemplate>
                               <asp:DropDownList ID="ddl_CodeGroup" runat="server" Width="160px" OnSelectedIndexChanged="ddl_CodeGroup_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_CodeGroup" EventName="SelectedIndexChanged" />
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
            <div class="col-md-1"><asp:Label ID="lbl_CodeDars" runat="server" Text="درس :"></asp:Label></div>
             <div class="col-md-1"> <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                             <ContentTemplate>
                                <asp:DropDownList ID="ddl_CodeDras" runat="server" Width="160px" OnSelectedIndexChanged="ddl_CodeDras_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_CodeDras" EventName="SelectedIndexChanged" />
                             </Triggers>
                       </asp:UpdatePanel></div>
         </div>
        <br />
        <br />
           <div style="margin-right:5px;margin-left:5px">
        <div class="row">
         <div class="col-md-2"></div>
            <div class="col-md-4"><asp:Button ID="btn_EvalutionProf" class="btn btn-primary" runat="server" Text="ارزشیابی اساتید" Width="100%" OnClick ="btn_EvalutionProf_Click" /></div>
            <div class="col-md-4"><asp:Button ID="btn_EvalutionProfbyItem" runat="server" Text="ارزشیابی اساتید به تفکیک استاد،درس،رشته" Width="100%" class="btn btn-primary " OnClick ="btn_EvalutionProfbyItem_Click"/></div>
           
             </div>
         <div class="row">  
               <div class="col-md-2"></div>
                <div class="col-md-4"><asp:Button ID="btn_EvalutionProfbyItemLesson" runat="server" Text="ارزشیابی اساتید به تفکیک درس،استاد،دانشکده" Width="100%"  class="btn btn-primary " OnClick="btn_EvalutionProfbyItemLesson_Click"/></div>
             <div class="col-md-4"><asp:Button ID="btn_EvalutionAll" runat="server" Text="ارزشیابی اساتید به تفکیک استاد،دانشکده،درس، سوال" Width="100%"  class="btn btn-primary " OnClick="btn_EvalutionAll_Click"/></div>
           
         </div>   </div>
         <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="namedanesh" HeaderText="نام دانشکده" />
                    <asp:BoundField DataField="namegroup" HeaderText="نام گروه" />
                    <asp:BoundField DataField="idostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="ostadfamily" HeaderText="نام استاد" />
                    <asp:BoundField DataField="did" HeaderText="تعداد کلاس" />
                    <asp:BoundField DataField="tedadDaneshjoo" HeaderText="تعداد دانشجو" />
                    <asp:BoundField DataField="avrg" HeaderText="میانگین" />
                </Columns>
            </asp:GridView>
      </div>
                <div id="Div2" runat="server">
             <asp:ImageButton ID="img_ExportToExcel2" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel2_Click" Visible="false" />
            <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="tedad_kelas" HeaderText="تعداد کلاس" />
                    <asp:BoundField DataField="ostadid" HeaderText="کد استاد" />
                    <asp:BoundField DataField="ostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="tedadDaneshjoo" HeaderText="تعداد دانشجو" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="avrg" HeaderText="میانگین" />
                </Columns>
            </asp:GridView>
      </div>
                <div id="Div3" runat="server">
             <asp:ImageButton ID="img_ExportToExcel3" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel3_Click" Visible="false" />
            <asp:GridView ID="GridView3" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="tedad_kelas" HeaderText="تعداد کلاس" />
                    <asp:BoundField DataField="ostadid" HeaderText="کد استاد" />
                    <asp:BoundField DataField="ostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="tedadDaneshjoo" HeaderText="تعداد دانشجو" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="avrg" HeaderText="میانگین" />
                </Columns>
            </asp:GridView>
      </div>
                <div id="Div4" runat="server">
             <asp:ImageButton ID="img_ExportToExcel4" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel4_Click" Visible="false" />
            <asp:GridView ID="GridView4" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="id_Question" HeaderText="تعداد سوال" />
                    <asp:BoundField DataField="tedadKelas" HeaderText="تعداد کلاس" />
                    <asp:BoundField DataField="ostadid" HeaderText="کد استاد" />
                    <asp:BoundField DataField="ostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="tedadDaneshjoo" HeaderText="تعداد دانشجو" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="avrg" HeaderText="میانگین" />
                </Columns>
            </asp:GridView>
      </div>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
             <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" /><uc1:AccessControl ID="AccessControl1" runat="server"/>       
           </div>
</asp:Content>
