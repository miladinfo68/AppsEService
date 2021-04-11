<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/PageAdobeMaster.Master" AutoEventWireup="true" CodeBehind="ViewFiles.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.Pages.ViewFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
  <span>نمایش فایل ها---</span><span style="padding-right: 7px; padding-left: 7px">مشخصه کلاس:</span><span id="ClassName" runat="server" style="padding-right: 7px; padding-left: 7px; font-weight: bold;"></span><span style="padding-right: 15px; padding-left: 15px">جلسه مورخ:</span><span id="SessionName" runat="server" style="padding-right: 7px; padding-left: 7px; font-weight: bold;"></span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:Label ID="lbl_User" runat="server" Visible="false"></asp:Label>
     <div class="col-md-3"></div>
      <div class="col-md-6" style="background-color: rgba(255, 255, 255,0.5);padding:2%;box-shadow:10px 10px 10px 10px rgba(128, 128, 128,0.1)">
    <asp:ListView ID="lstView" runat="server"> 
        <ItemTemplate>
             <div class="col-md-12">
           <span><%# (Container.DataItemIndex+1).ToString() %> - </span>
             <a target="_blank" href='<%#Eval("Link") %>'>مشاهده رکورد<%#Eval("FileName") %></a></div>
        </ItemTemplate>
    </asp:ListView>
          <asp:Button ID="btn_Select" runat="server" CssClass="Redbtn" Text="افزودن به لیست درخواست ها" OnClick="btnSelect_Click" />
          </div>
    <asp:Label ID="txt_AssetTxt" runat="server" Text="" Visible="False"></asp:Label>
     <div class="col-md-3"></div>
</asp:Content>