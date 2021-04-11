<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="SynchronizeSystem.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.SynchronizeSystem" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
     <link rel="stylesheet" type="text/css" media="all" href="../css/aqua/theme.css" title="Aqua" />
    	<!-- import the Jalali Date Class script -->
		<script type="text/javascript" src="../js/jalali.js"></script>
		
		<!-- import the calendar script -->
		<script type="text/javascript" src="../js/calendar.js"></script>
		
		<!-- import the calendar script -->
		<script type="text/javascript" src="../js/calendar-setup.js"></script>
		
		<!-- import the language module -->
		<script type="text/javascript" src="../js/lang/calendar-fa.js"></script>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <table style="width:100%">
       <tr>
          <%-- <td style="width: 15%">روز را انتخاب نمایید:</td>
           <td style="width: 20%">
               <asp:DropDownList ID="drp_Day" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small">
                   <asp:ListItem Value="1">شنبه</asp:ListItem>
                   <asp:ListItem Value="2">یکشنبه</asp:ListItem>
                   <asp:ListItem Value="3">دوشنبه</asp:ListItem>
                   <asp:ListItem Value="4">سه شنبه</asp:ListItem>
                   <asp:ListItem Value="5">چهارشنبه</asp:ListItem>
                   <asp:ListItem Value="6">پنج شنبه</asp:ListItem>
                   <asp:ListItem Value="7">جمعه</asp:ListItem>
               </asp:DropDownList>
           </td>--%>
          <td><%-- <Label>تاریخ</Label>
                <asp:TextBox ID="txt_Date" runat="server" placeholder="1394/06/21"></asp:TextBox>--%></td>
               
               
              <td style="width: 15%; ">از تاریخ:</td>
                <td style="width: 20%; ">
                    <div class="example">
                        <input id="date_input_1" type="text" runat="server"  />
                        <img id="date_btn_1" src="../images/cal.png" style="vertical-align: top;" />
                        <script type="text/javascript">

                            Calendar.setup({
                                inputField: "ContentPlaceHolder1_date_input_1",   // id of the input field
                                button: "date_btn_1",   // trigger for the calendar (button ID)
                                ifFormat: "%Y/%m/%d",       // format of the input field
                                dateType: 'jalali',
                                weekNumbers: false
                            });
			    </script>
                        <script type="text/javascript">

                            setActiveStyleSheet(document.getElementById("defaultTheme"), "Aqua");
		        </script>
                    </div>
                </td>
                <td style="width: 10%; height: 50px;">تا تاریخ:</td>
                <td style="width: 20%;">
                    <div>
                        <input id="date_input_2" type="text" runat="server"  />
                        <img id="date_btn_2" src="../images/cal.png" style="vertical-align: top;" />
                        <script type="text/javascript">

                            Calendar.setup({
                                inputField: "ContentPlaceHolder1_date_input_2",   // id of the input field
                                button: "date_btn_2",   // trigger for the calendar (button ID)
                                ifFormat: "%Y/%m/%d",       // format of the input field
                                dateType: 'jalali',
                                weekNumbers: false
                            });
			        </script>
                    </div>
                    <uc1:AccessControl ID="AccessControl2" runat="server" />
                </td>
           <td>
     <asp:Button ID="btn_Synch" runat="server" Text="synch"  OnClick="btn_synch_Click"/>
           </td>
       </tr>
   </table>
    <uc1:AccessControl ID="AccessControl1" runat="server" />
</asp:Content>
