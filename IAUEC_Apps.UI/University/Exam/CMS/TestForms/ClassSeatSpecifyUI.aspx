<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="ClassSeatSpecifyUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.TestForms.ClassSeatSpecifyUI" %>
<%@ Register src="../../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
     <div id="div_Main" style="padding-left:15%;padding-right:5%;" class="well profile_view">
    <asp:Panel ID="pnl_Main" runat="server" HorizontalAlign="Center" Width="100%" Direction="RightToLeft" >
    <table dir="rtl" style="width:100%;">
        <tr>
            
          
             <td style="font-family: 'B Nazanin'; font-size: medium;width:30%;">
                <asp:Label ID="lbl_Class" runat="server" style="text-align: center" Text="کد کلاس را انتخاب نمایید:" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="font-family: 'B titr'; font-size: medium;width:10%;">
                <asp:DropDownList ID="ddl_ClassCode" runat="server" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
            </td>
             <td style="font-family: 'B Nazanin'; font-size: medium;width:30%;">
                <asp:Label ID="lbl_Shahr" runat="server" style="text-align: center" Text="نام شهر را انتخاب نمایید:" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
            <td style="font-family: 'B titr'; font-size: medium;width:20%;">
                <asp:DropDownList ID="ddl_Shahr" runat="server" CssClass="form-control input-sm" DataTextField="City_Name" DataValueField="ID" ForeColor="Black">
                <%--<asp:ListItem Value="1">تهران</asp:ListItem>--%>
                     </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td class="auto-style5">&nbsp;</td>
            <td>
                <uc1:AccessControl ID="AccessControl1" runat="server" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
       <div>
                </div>
          <div>
                <asp:Button ID="btn_Takhsis" runat="server"  OnClick="Button1_Click" Text="تخصیص صندلی" CssClass="btn btn-exam" Font-Bold="True" />
            
                <asp:Button ID="btn_Class" runat="server"  OnClick="Button2_Click" Text="لیست کلاسها" Font-Bold="True"  Visible="false"  CssClass="btn btn-exam"  />
           </div>
        
        </asp:Panel>
         </div>

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

</asp:Content>
 
 
