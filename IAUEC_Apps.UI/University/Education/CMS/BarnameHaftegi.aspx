<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="BarnameHaftegi.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.BarnameHaftegi" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="BarnameHaftegi.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.BarnameHaftegi"  %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <%--<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />--%>
   <title> <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
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
        <div class="col-md-1"><asp:Label ID="lbl_Term" runat="server" Text="ترم :" Width="50px"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Term" runat="server" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
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
            <sup></sup>
            </span>
         </div> 
       <div class="col-md-1"><asp:Label ID="lbl_Day" Text="روز تشکیل کلاس :" runat="server" Width="150px"></asp:Label></div>
          <div class="col-md-1">
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                             <ContentTemplate>
                             <asp:DropDownList ID="ddl_Day" runat="server"  Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddl_Day_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Day" EventName="SelectedIndexChanged" />
                             </Triggers>
                       </asp:UpdatePanel>
          </div></div>

      <div class="row">
      <div class="col-md-3"></div>
                 <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
       <div class="col-md-1"><asp:Label ID="lbl_Daneshkade" runat="server" Text="دانشکده :" Width="150px"></asp:Label></div>
      <div class="col-md-1">
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                             <ContentTemplate>
                             <asp:DropDownList ID="ddl_Daneshkade" runat="server" Width="150px" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Daneshkade" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel>
      </div></div>
      <div class="row">
      <div class="col-md-3"></div>
         <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
      <div class="col-md-1"><asp:Label ID="lbl_Departman" runat="server" Text="گروه :" Width="150px"></asp:Label></div>
      <div class="col-md-1">
                   <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                             <ContentTemplate>
                              <asp:DropDownList ID="ddl_Departman" runat="server" OnSelectedIndexChanged="ddl_Departman_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Departman" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel>         
        </div></div>
       <div class="row">
      <div class="col-md-3"></div>
                  <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
      <div class="col-md-1"> <asp:Label ID="lbl_Field" runat="server" Text="رشته ارائه دهنده :" Width="150px" ></asp:Label></div>
      <div class="col-md-1">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                             <ContentTemplate>
                               <asp:DropDownList ID="ddl_Field" runat="server" Width="150px" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel> 
                   
</div></div>
             <div class="row">
             <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
             <div class="col-md-1"><asp:Label ID="lbl_Degree" runat="server" Text="مقطع کلاس :" Width="150px" ></asp:Label></div>
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

</div></div>
              <div class="row">
             <div class="col-md-3"></div>
             <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
             <div class="col-md-1"><asp:Label ID="lbl_LocationClass" runat="server" Width="150px" Text="محل تشکیل کلاس :" ></asp:Label></div>
             <div class="col-md-1">
                     <asp:UpdatePanel ID="UpdatePanel7" runat="server" >
                             <ContentTemplate>
                              <asp:DropDownList ID="ddl_LocationClass" runat="server" Width="150px" OnSelectedIndexChanged="ddl_LocationClass_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_LocationClass" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel>
      </div></div>
        <div class="row">
         <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-1">
            <asp:Label ID="lbl_SaatStart" runat="server" Text="از ساعت شروع:" Width="100px"></asp:Label>
        </div>
        <div class="col-md-1">
                  <telerik:RadTimePicker ID="RTP1" runat="server" Width="150px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" >
                  <TimeView ID="TimeView4" Interval="00:15:00" Columns="8" runat="server">
                     </TimeView>
                  </telerik:RadTimePicker>

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
                <asp:Label ID="lbl_SaatEnd" runat="server" Text="تا ساعت شروع:"></asp:Label>
                </div>
            <div class="col-md-1">
                  <telerik:RadTimePicker ID="RTP2" runat="server" Width="150px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" >
                  <TimeView ID="TimeView2" Interval="00:15:00" Columns="8" runat="server">
                  </TimeView>
                  </telerik:RadTimePicker>
             </div>
                  </div>

         <div class="row">
             <div class="col-md-3"></div>
             <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
              
     <div class="col-md-2"><asp:Button ID="btn_ShowReport" runat="server" Text="نمایش اطلاعات" Width="150px" OnClick="btn_ShowReport_Click" CssClass="btn btn-success" /></div>
     </div>

     <div runat="server">
             <asp:ImageButton ID="img_ExportToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel_Click" Visible="false"/>
      </div>  
          
   <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" RegisterWithScriptManager ="false"></telerik:RadWindowManager>
    <uc1:AccessControl ID="AccessControl1" runat="server" />
                 <asp:GridView ID="gv_Show" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="tterm" HeaderText="نیمسال" />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="namegroup" HeaderText="گروه" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="namedars" HeaderText="درس" />
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="idroz1" HeaderText="روز" />
                    <asp:BoundField DataField="saat" HeaderText="ساعت" />
                    <asp:BoundField DataField="saatstart" HeaderText="ساعت شروع" />
                    <asp:BoundField DataField="zarfklass" HeaderText="ظرفیت کلاس" />
                    <asp:BoundField DataField="Mard" HeaderText="مرد" />
                    <asp:BoundField DataField="Zan" HeaderText="زن" />
                    <asp:BoundField DataField="zarfporm" HeaderText="ظرفیت پر شده کلاس" />
                </Columns>
            </asp:GridView>
</div>
</asp:Content>
