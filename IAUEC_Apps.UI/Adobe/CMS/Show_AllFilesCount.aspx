<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="Show_AllFilesCount.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.Show_AllFilesCount" %>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="p" runat="server" DefaultButton="btn_ShowCount">     
        <table class="nav-justified">
        <tr>
            <td style="width: 15%; ">ترم را انتخاب نمایید:</td>            
            <td style="width: 20%; ">      
		        <telerik:RadComboBox ID="rcb_Term" runat="server" ></telerik:RadComboBox>
        </td>
        <td style="width: 10%; height: 50px;">&nbsp;</td>
            <td  style="width: 20%;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
            <tr>
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
                    <uc1:AccessControl ID="AccessControl1" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btn_ShowCount" runat="server" Font-Bold="True" OnClick="btn_ShowCount_Click" Text="نمایش" />
                </td>
            </tr>
        <tr>
            <td style="width: 15%">تعداد کل:</td>
            <td style="width: 20%; height: 20px;">
                <asp:Label ID="lblAllCount" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 10%; height: 20px;"></td>
            <td style="height: 20px" colspan="2">تعداد فولدرهای موجود:<asp:Label ID="lblExistFolder" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">تعداد رکوردها:</td>
            <td style="width: 20%">
                <asp:Label ID="lblRecordCount" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 10%">&nbsp;</td>
            <td colspan="2">تعداد فولدرهای نا موجود:<asp:Label ID="lblNotExistFolder" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">تعداد رکوردهای ناموجود:</td>
            <td style="width: 20%">
                <asp:Label ID="lblNotExistRecords" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 10%">&nbsp;</td>
            <td colspan="2">
                تعداد رکوردهای کم حجم:<asp:Label ID="lbl_Mp3Length" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
            <tr>
                <td style="width: 15%">تعداد رکورد کم حجم سالم:</td>
                <td style="width: 20%">
                    <asp:Label ID="lbl_LowLengthCorrect" runat="server" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 10%">&nbsp;</td>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2"><asp:ImageButton ID="ExportToExcelImg1" runat="server" ImageUrl="~/Adobe/images/Excel02.jpg"
             AlternateText="ExcelML" OnClick="ExportToExcelImg_Click" /></td>
                <td colspan="3"><asp:ImageButton ID="ExportToExcelImg2" runat="server" ImageUrl="~/Adobe/images/Excel02.jpg"
             AlternateText="ExcelML" OnClick="ExportToExcelImg2_Click" /></td>
            </tr>
        <tr>
            <td style="background-color: #666666; color: #FFFFFF; font-weight: bold; text-align: center;" colspan="2">لیست رکوردها(کم حجم و ناموجود)</td>
            <td colspan="3">
                <table class="nav-justified" style="margin-right: 10px; background-color: #666666; color: #FFFFFF; font-weight: bold; text-align: center;">
                    <tr>
                        <td>لیست فولدرها(ناموجود)</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="nav-justified" style="vertical-align: top">
                    <tr>
                        <td style="vertical-align: top">
                
                <telerik:RadGrid ID="grd_Records" runat="server" AutoGenerateColumns="false" OnItemDataBound="grd_Records_ItemDataBound" AllowPaging="True" PageSize="30" OnNeedDataSource="grd_Records_NeedDataSource" AllowSorting="True">
                    <MasterTableView AutoGenerateColumns="false">
                     <ItemStyle Font-Names="tahoma" Font-Size="Small" />
                       <HeaderStyle Font-Names="tahoma" Font-Size="Small" />
                        <AlternatingItemStyle  Font-Names="tahoma" Font-Size="Small" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                             <ItemTemplate>
                           <asp:Label ID="lbl_Desc" runat="server" ForeColor="Red" Font-Names="tahoma"></asp:Label>
                        </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Class_Code" HeaderText="مشخصه کلاس"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Session" HeaderText="جلسه"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FileDate" HeaderText="تاریخ"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LowLength" ItemStyle-ForeColor="White" ItemStyle-Width="1px" ></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="3">
                <table class="nav-justified" style="margin-right: 10px">
                    <tr>
                        <td>
            <telerik:RadGrid ID="Grd_Folders" runat="server" AutoGenerateColumns="false" AllowPaging="True" OnNeedDataSource="Grd_Folders_NeedDataSource" PageSize="30" AllowSorting="True">
                <MasterTableView AutoGenerateColumns="false">
                      <ItemStyle Font-Names="tahoma" Font-Size="Small" />
                       <HeaderStyle Font-Names="tahoma" Font-Size="Small" />
                     <AlternatingItemStyle  Font-Names="tahoma" Font-Size="Small" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Class_Code" HeaderText="مشخصه کلاس"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Session" HeaderText="جلسه"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FileDate" HeaderText="تاریخ"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                </telerik:RadGrid> 
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>   
    </asp:Panel>
</asp:Content>
