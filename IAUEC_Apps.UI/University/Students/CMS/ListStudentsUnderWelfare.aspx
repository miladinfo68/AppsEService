<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListStudentsUnderWelfare.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.ListStudentsUnderWelfare"  %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
        <h3 style="color:blue">
       گزارش دانشجویان تحت پوشش بهزیستی
    </h3>
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
                            <telerik:AjaxUpdatedControl ControlID="txt_Students" LoadingPanelID="LsitLoadingPanel"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>                 
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="LsitLoadingPanel"></telerik:RadAjaxLoadingPanel>


      <div class="row">
        <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_Field" runat="server" Text="رشته تحصیلی :" Width="180px"></asp:Label></div>
                  <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Field" runat="server" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
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
            <sup></sup>
            </span>
       </div> 
        <div class="col-md-1"><asp:Label ID="lbl_Students" Text="شماره دانشجویی :" runat="server" Width="180px" ></asp:Label></div>
        <div class="col-md-1"><asp:TextBox ID="txt_Students" runat="server" Width="150px" CssClass="form-control" ></asp:TextBox></div>
        <div class="col-md-1"><asp:Button ID="btn_Click" runat="server" Text="انتخاب کنید" Width="110px" class="btn btn-success btn-lg" OnClick="btn_Click_Click" /> </div>
    </div>              
           
         <div class="row">
        <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_Degree" runat="server" Text="مقطع تحصیلی :" Width="180px"></asp:Label></div>
                  <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Degree" runat="server" OnSelectedIndexChanged="ddl_Degree_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
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
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_Dorpar" runat="server" Text="سیستم آموزشی :" Width="180px"></asp:Label></div>
                  <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Dorpar" runat="server" OnSelectedIndexChanged="ddl_Dorpar_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Dorpar" EventName="SelectedIndexChanged" />
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
        <div class="col-md-1"><asp:Label ID="lbl_Sex" runat="server" Text="جنسیت :" Width="180px"></asp:Label></div>
                  <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Sex" runat="server" OnSelectedIndexChanged="ddl_Sex_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Sex" EventName="SelectedIndexChanged" />
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
        <div class="col-md-1"><asp:Label ID="lbl_VaziatStu" runat="server" Text="وضعیت دانشجو :" Width="180px"></asp:Label></div>
        <div class="col-md-1">
         <asp:UpdatePanel ID="UpdatePanel5" runat="server">
           <ContentTemplate>
                 <asp:DropDownList ID="ddl_VaziatStu" runat="server" OnSelectedIndexChanged="ddl_VaziatStu_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
           </ContentTemplate>
           <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="ddl_VaziatStu" EventName="SelectedIndexChanged" />
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
        <div class="col-md-1"><asp:Label ID="lbl_SalVorod" runat="server" Text="سال ورود :" Width="180px"></asp:Label></div>
                  <div class="col-md-1"><asp:TextBox ID="txt_SalVorod" runat="server" width="40px"></asp:TextBox>
            </div>
          </div>

     <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3">
           <asp:RadioButton ID="rdb_Behzisti" runat="server" ValidationGroup="1" GroupName="Group" text="تحت پوشش بهزیستی" AutoPostBack="true"/>
       </div>
    </div>

     <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3">
           <asp:RadioButton ID="rdb_Isargar" runat="server" ValidationGroup="2" GroupName="Group" text="ایثارگر" AutoPostBack="true" Checked="true"/>
       </div>
    </div>

      <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div class="col-md-1">
          <asp:Button id="btn_Show" runat="server" Text="نمایش اطلاعات" Font-Bold="true" ForeColor="RoyalBlue" OnClick="btn_Show_Click" CssClass="btn btn-success"/>
      </div></div> 

 <asp:ImageButton ID="img_ExportToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
      AlternateText="ExcelML" OnClick="img_ExportToExcel_Click" Visible="false"/>

             <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
             <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
        </div>


        <asp:GridView ID="grd_Show1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
             <Columns>
                    <asp:BoundField DataField="familyy" HeaderText="نام و نام خانوادگی" />
                    <asp:BoundField DataField="idd_meli" HeaderText="کد ملی" />
                    <asp:BoundField DataField="TypeIsargar" HeaderText="نوع ایثارگری" />
                    <asp:BoundField DataField="Nesbat" HeaderText="نسبت" />
                    <asp:BoundField DataField="modat" HeaderText="مدت" />
                    <asp:BoundField DataField="code_rayane" HeaderText="کد رایانه" />
                    <asp:BoundField DataField="darsad_tahod" HeaderText="درصد تعهد" />
                </Columns>
       </asp:GridView>
</asp:Content>
