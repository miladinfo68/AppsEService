<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="RequestDistributionOfStudent.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.RequestDistributionOfStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div id="div_Main" style="padding-left:15%;padding-right:5%;">
    <asp:Panel ID="pnl_Main" runat="server" HorizontalAlign="Center" Width="100%" Direction="RightToLeft" >
        <table style="direction:rtl;text-align:right;width:100%;" runat="server" dir="rtl"  >
            <tr>
                <td style="font-family: 'B Nazanin';width:10%; font-size: medium;">
  <asp:Label ID="lbl_Ostan" runat="server" Text="انتخاب استان" Font-Bold="True" Font-Names="B Nazanin" ForeColor="Black"></asp:Label>
                </td>
                <td >
                     <asp:DropDownList ID="ddl_Province" runat="server" ForeColor="Black"></asp:DropDownList>
                </td>
                <td style="font-family: 'B Nazanin';width:10%; font-size: medium;">
                      <asp:Button ID="btn_Show" runat="server" Text="مشاهده" OnClick="btn2_Click" CssClass="btn btn-exam" />
                </td>
            </tr>
            
       
      

        </table>  
      
                    
      
   
    <div dir="rtl">
    <asp:ImageButton ID="imgbtn_ConvertExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
            OnClick="ConvertExcel_Click" AlternateText="Convert To Excel" Width="40"/>
    <telerik:RadGrid ID="grd_StudentPerProvince" runat="server" AutoGenerateColumns="False" ForeColor="Black" EnableEmbeddedSkins="False"> 
        <MasterTableView Dir="RTL">
            <EditFormSettings>
                <EditColumn CancelImageUrl="Cancel.gif" InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif">
                </EditColumn>
            </EditFormSettings>
            <HeaderStyle HorizontalAlign="Center" CssClass="bg-purple" />
            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Province" HeaderText="نام استان"  >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentCount" HeaderText="تعداد دانشجو"  >
                </telerik:GridBoundColumn>           
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
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



</asp:Content>
