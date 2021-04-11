<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="PaymentConfirm.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.PaymentConfirm" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    
    <link href="../../University/Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Input.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Ajax.MyCustomSkin.css" rel="stylesheet" />
    <!-- import the Jalali Date Class script -->
      <link rel="stylesheet" type="text/css" media="all" href="../css/aqua/theme.css" title="Aqua" />
		<script type="text/javascript" src="../js/jalali.js"></script>
		
		<!-- import the calendar script -->
		<script type="text/javascript" src="../js/calendar.js"></script>
		
		<!-- import the calendar script -->
		<script type="text/javascript" src="../js/calendar-setup.js"></script>
		
		<!-- import the language module -->
		<script type="text/javascript" src="../js/lang/calendar-fa.js"></script>
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p dir="rtl"><span>جمع کل:</span><span id="sumprice" runat="server"></span>
    </p><uc1:AccessControl ID="AccessControl1" runat="server" />
    <table dir="rtl" style="width:100%">
        <tr>
            <td>از تاریخ:</td>
            <td>
                <div class="example">			
			    <input id="date_input_1" type="text" runat="server"  /><img id="date_btn_1" src="../images/cal.png" style="vertical-align: top;" />
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
              <td>تا تاریخ:</td>
            <td>
                <div >			
			        <input id="date_input_2" type="text" runat="server"  /><img id="date_btn_2" src="../images/cal.png" style="vertical-align: top;" />
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
            </td>
            <td><asp:Button ID="btn_select" runat="server" Text="نمایش" OnClick="btn_select_Click" /></td>
        </tr>
    </table>
       <telerik:RadGrid ID="grd_Pay" runat="server" AllowPaging="True" PageSize="30" Skin="MyCustomSkin" AllowFilteringByColumn="True"  EnableEmbeddedSkins="False"
           CellSpacing="0" GridLines="None"    OnItemDataBound="PayGrid_ItemDataBound" OnItemCommand="PayGrid_ItemCommand" OnNeedDataSource="PayGrid_NeedDataSource">
        <MasterTableView AutoGenerateColumns="false"  >
            <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center"  />
            <ItemStyle  Font-Names="tahoma" Font-Size="Small" />
            <AlternatingItemStyle  Font-Names="tahoma" Font-Size="Small"  />
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                </telerik:GridTemplateColumn>
                 <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" FilterListOptions="VaryByDataType"   >
                    
                 </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="OrderId" HeaderText="شماره سفارش" AllowFiltering="False">

                 </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="Amount" HeaderText="مبلغ(ریال)" DataFormatString="{0:N0}" AllowFiltering="False"   >

                 </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="PersianDate" HeaderText="تاریخ" AllowFiltering="False" >

                 </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TraceNumber" HeaderText="شماره پیگیری" AllowFiltering="False">

                 </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="AppStatus" HeaderText="وضعیت پرداخت" AllowFiltering="False">

                     <ItemStyle ForeColor="#0099ff"/>
                     
                 </telerik:GridBoundColumn> 
                  <telerik:GridTemplateColumn AllowFiltering="false">
                     <ItemTemplate>
                      <asp:Button ID="btn_ConfirmPay" runat="server" CommandName="ConfirmPay"   CommandArgument='<%#Eval("OrderId") %>'  Text="تایید پرداخت"/>
                     </ItemTemplate>
                 </telerik:GridTemplateColumn>     
                    </Columns>
        </MasterTableView>


    </telerik:RadGrid>
    
</asp:Content>
