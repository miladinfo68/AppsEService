<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="PaymentStatusAndEnrollment.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.PaymentStatusAndEnrollment" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title> <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3 style="color:blue">
       گزارش وضعیت پرداخت دانشجو
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
        <div class="col-md-1"><asp:Label ID="lbl_Term" runat="server" Text="ترم :"></asp:Label></div>
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
            <sup>*</sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_StatusStu" runat="server" Text="وضعیت دانشجو :" Width="150px"></asp:Label></div>
         <div class="col-md-1">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>
                   <asp:DropDownList ID="ddl_StatusStu" runat="server" OnSelectedIndexChanged="ddl_StatusStu_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
               </ContentTemplate>
               <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="ddl_StatusStu" EventName="SelectedIndexChanged" />
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
        <div class="col-md-1"><asp:Label ID="lbl_Daneshkade" runat="server" text="دانشکده :"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Daneshkade" runat="server" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Daneshkade" EventName="SelectedIndexChanged" />
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
        <div class="col-md-1"><asp:Label ID="lbl_Field" runat="server" Text="رشته تحصیلی :"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
        <div class="col-md-1"><asp:Label ID="lbl_Degree" runat="server" Text="مقطع تحصیلی :" Width="150px"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel5" runat="server">
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
       <div class="col-md-4">
            <asp:RadioButton ID="rdb_Status" runat="server" ValidationGroup="0" GroupName="ReportStudents" Text="لیست دانشجویانی که ثبت نام داشته اند ولی پرداخت نداشته اند " AutoPostBack="true" Checked="true"/>
       </div>
    </div>

     <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-4">
            <asp:RadioButton ID="rdb_Payment" runat="server" ValidationGroup="1" GroupName="ReportStudents" Text="لیست دانشجویانی که پرداخت داشته اند ولی ثبت نام نکرده اند " AutoPostBack="true"/>
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

       <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
      AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false"/>
       
     
     <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
       <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
      </div>

      <asp:GridView ID="grd_Show1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
             <Columns>
                    <asp:BoundField DataField="familyy" HeaderText="نام خانوادگی و نام" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                    <asp:BoundField DataField="Date" HeaderText="تاریخ صدور" />
                </Columns>
       </asp:GridView>



      <asp:GridView ID="grd_Show2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
             <Columns>
                    <asp:BoundField DataField="familyy" HeaderText="نام خانوادگی و نام" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                </Columns>
       </asp:GridView>
      
</asp:Content>
