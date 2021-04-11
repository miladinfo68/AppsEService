<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="ShowDetailFiles.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.ShowDetailFiles" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%--<uc1:AccessControl ID="AccessControl1" runat="server" />--%><div class="col-md-3"></div>
      <div class="col-md-6" style="background-color: rgba(255, 255, 255,0.5);padding:2%;box-shadow:10px 10px 10px 10px rgba(128, 128, 128,0.1)">       
    <asp:ListView ID="lstView" runat="server"> 
        <ItemTemplate>
             <div class="col-md-12">
           <span><%# (Container.DataItemIndex+1).ToString() %> - </span>
             <a target="_blank" href='<%#Eval("Link") %>'>مشاهده رکورد<%#Eval("FileName") %></a></div>
        </ItemTemplate>
    </asp:ListView>          
          </div>
     <div class="col-md-3"></div>
</asp:Content>
