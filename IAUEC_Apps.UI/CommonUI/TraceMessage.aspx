<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TraceMessage.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.TraceMessage" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <%--<link href="../../Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />--%>
<%--    <style type="text/css">
*{-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;}*{color:#000 !important;text-shadow:none !important;background:transparent !important;-webkit-box-shadow:none !important;box-shadow:none !important;}</style>--%>
    <style type="text/css">
*{-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;}*{color:#000 !important;text-shadow:none !important;background:transparent !important;-webkit-box-shadow:none !important;box-shadow:none !important;}</style>
</head>
<body>

    <form id="form1" runat="server">
    <div style="font-family:Tahoma;font-size:small;direction:rtl">
   <table >
       <tr>
           <td style="width:10px;">
               <asp:Label ID="lbl_Code" runat="server" Text="کد(استاد یا دانشجو) :" Width="130px"></asp:Label>
           </td>
           <td style="width:50px;">
               <asp:TextBox ID="txt_Code" runat="server" Width="100px" ></asp:TextBox>
           </td>
           <td style="width:10px;">

           </td>
           
           </tr>
       <tr>
           <td>
               <asp:Label ID="lbl_Application" runat="server" Text="برنامه :"  Width="130px"></asp:Label>
           </td>
           <td style="width:50px;">
               <asp:DropDownList ID="ddl_Application" runat="server" AutoPostBack="true" Width="100px" OnSelectedIndexChanged="ddl_Application_SelectedIndexChanged"></asp:DropDownList>
           </td>
       </tr>
       <tr>
           <td>
               <asp:Label ID="lbl_IdSatus" runat="server" Text="وضعیت درخواست :"></asp:Label>
           </td>
           <td style="width:50px;">
               <asp:DropDownList ID="ddl_IdStatus" runat="server" Width="100px" OnSelectedIndexChanged="ddl_IdStatus_SelectedIndexChanged"></asp:DropDownList>
           </td>
           <td>

           </td>
           <td>
                </td>
            <td style="width:100px;">
               <asp:Button ID="btn_SearchCode" runat="server" OnClick="btn_SearchCode_Click" Text="جستجو کد آسانک" Width="200px" />
           </td>
       </tr>
       <tr>
           <td style="width:100px;">
              <asp:Label ID="lbl_CodeAsanak" visible="false" runat="server" Text="کد آسانک : "></asp:Label>
           </td>
           <td style="width:50px;">
               <asp:TextBox ID="txt_CodeAsanak" runat="server" Width="100px" Visible="false"></asp:TextBox>
           </td>

       </tr>
       <tr>
               <td>
                   <asp:ScriptManager ID="ScriptManager2" runat="server">
                   </asp:ScriptManager>
                   <asp:Button  visible="false" ID="btn_TraceMessage" runat="server" OnClick="btn_TraceMessage_Click" Text="پیگیری وضعیت ارسال پیامک" />
               </td> 
       </tr>
       <tr>
           <td>
               <asp:Label ID="lbl_Status" runat="server" Visible="false"></asp:Label>
           </td> 
       </tr>
   </table>
        <div style="background:bisque no-repeat scroll 0% 0%; font-family:Tahoma; text-align:center;">
        <asp:GridView ID="grd_ShowMessage" runat="server" AllowSorting="true" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="Wheat" Font-Names="tahoma"
      BackColor="Honeydew" visible="true" BorderWidth="2px" DataKeyNames="codeAsanak" OnSelectedIndexChanged="grd_ShowMessage_SelectedIndexChanged" HorizontalAlign="Center"
            CellSpacing="1" GridLines="None">
            <Columns>
               <asp:TemplateField HeaderText="ردیف">
                <ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate>
                </asp:TemplateField>  
                <asp:BoundField DataField="codeAsanak" HeaderText="کد آسانک"/>
                <asp:BoundField DataField="date" HeaderText="تاریخ ارسال پیامک" ItemStyle-HorizontalAlign="Center"/>
                <asp:ButtonField CommandName="Select" Text="انتخاب"/>
            </Columns>
        </asp:GridView>           
        </div>
     <%-- <div style="background:bisque no-repeat scroll 0% 0%">
          <telerik:RadGrid ID="grd_ShowMessage" runat="server" AllowSorting="true" AutoGenerateColumns="false" AllowMultiRowSelection="false" AlternatingItemStyle-BackColor="Wheat"
               Font-Names="Tahoma" BackColor="Honeydew" AllowFilteringByColumn="false" Visible="true" BorderWidth="1px" OnItemCommand="grd_ShowMessage_ItemCommand"
               CellSpacing="0" EnableEmbeddedSkins="False" GridLines="None" Width="400px">
              <MasterTableView DataKeyNames="codeAsanak">
                  <HeaderStyle Font-Names="Tahoma" HorizontalAlign="Center" Wrap="true" />
                  <Columns>
                     <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                     <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                     </telerik:GridTemplateColumn>
                      <telerik:GridBoundColumn DataField="codeAsanak" HeaderText="کد آسانک" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="date" HeaderText="تاریخ ارسال پیامک" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                      <telerik:GridButtonColumn CommandName="Select" UniqueName="Select" Text="انتخاب کنید"></telerik:GridButtonColumn>
                  </Columns>
              </MasterTableView>
          </telerik:RadGrid>
          </div>--%>
    </div>
        <telerik:RadWindowManager ID="RadwindowManager1" runat="server"></telerik:RadWindowManager>
      
    </form>
</body>
</html>
