<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/PagesMasterPage.Master" AutoEventWireup="true" CodeBehind="SelectExamCity.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.Pages.SelectExamCity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
          <Scripts>
              <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
              </asp:ScriptReference>
              <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
              </asp:ScriptReference>
              <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
              </asp:ScriptReference>
          </Scripts>
      </telerik:RadScriptManager>
      <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
      
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6">
              <asp:Label ID="lbl_cityName" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                <label style="float:left">محل امتحان</label>
                 
            </div>
            <div class="col-sm-3" >
                <asp:DropDownList ID="ddl_examCity" runat="server" CssClass="form-control input-sm" ></asp:DropDownList>
            </div>
            <div class="col-sm-3">

                <asp:Button CssClass="btn btn-exam" ID="btn_Save" runat="server" text="ثبت" OnClick="btn_Save_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
