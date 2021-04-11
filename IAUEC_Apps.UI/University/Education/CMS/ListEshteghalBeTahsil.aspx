<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="ListEshteghalBeTahsil.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.ListEshteghalBeTahsil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="../../Theme/css/theme.css" title="Aqua" />
    <script src="../../Theme/js/calendar-setup.js"></script>
    <script src="../../Theme/js/calendar.js"></script>
    <script src="../../Theme/js/jalali.js"></script>
    <script src="../../Theme/js/lang/calendar-fa.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:center;font-family:Tahoma;font-size:small;">
    <asp:panel id="pnl_main" runat="server" BorderStyle="Groove" Width="100%" HorizontalAlign="Center" Direction="RightToLeft" BackColor="SkyBlue">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbl_FromDate" Text="از تاریخ :" runat="server" Width="150px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_FromDate" dir="ltr" Text="  /  /  " runat="server"></asp:TextBox>
                </td>
                <td>
                <input id="date_input_1" type="text" runat="server"  /><img id="date_btn_1" src="../../Theme/images/cal.png" style="vertical-align: top;" />
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
                </td>
            </tr>
            <tr>
                <td>
                   <asp:Label ID="lbl_ToDate" Text="تاتاریخ :" runat="server" Width="150px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_ToDate" dir="ltr" Text="94/04/03" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                   <asp:Label ID="lbl_Term" Text="ترم :" runat="server" Width="150px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Term" runat="server" Width="130px" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_Show" runat="server" OnClick="btn_Show_Click" Text="نمایش درخواست ها" />
                </td>
<%--                <td>
                    <asp:Button ID="btn_Exit" runat="server" Text="خروج" Width="150px" OnClick="btn_Exit_Click" />
                </td>--%>
            </tr>
        </table>
        </asp:panel>
        </div>
        
         <div id="main" style="background:bisque no-repeat scroll 0% 0%" >
        <telerik:RadGrid ID="grd_Student" runat="server" AllowFilteringByColumn="True" AllowMultiRowSelection="false" AutoGenerateColumns="False" CellSpacing="0" GridLines="None">
         <MasterTableView DataKeyNames="stcode">
              <ItemStyle Font-Names="tahoma" /> 
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />        
                <Columns>      
                     <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" AllowFiltering="true" AutoPostBackOnFilter="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="family" HeaderText="نام و نام خانوادگی" AllowFiltering="true" AutoPostBackOnFilter="true">
                    </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="term" HeaderText="ترم" AllowFiltering="true" AutoPostBackOnFilter="true">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="sharh" HeaderText="جهت ارائه به" AllowFiltering="true" AutoPostBackOnFilter="true">
                    </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="datesabt" HeaderText="تاریخ " AllowFiltering="true" AutoPostBackOnFilter="true">
                    </telerik:GridBoundColumn>
                     <telerik:GridButtonColumn CommandName="Select" Text="انتخاب" UniqueName="Select" HeaderText="چاپ">
            </telerik:GridButtonColumn>
                     </Columns>
            </MasterTableView>
            </telerik:RadGrid>
           
    </div>
</asp:Content>
