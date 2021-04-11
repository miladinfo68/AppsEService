<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="Military.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.Military" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <script src="../Content/js-persian-cal.min.js"></script>
    <link href="../Content/js-persian-cal.css" rel="stylesheet" />
    <script type = "text/javascript">
        function Confirm() {
            if (performCheck()) {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("آيا مطمئن هستيد؟")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
            else {
                alert('لطفا ورودي هاي خود را بررسي کنيد!');
            }
        }
        function performCheck() {
            if (Page_ClientValidate("inputDataValidationGroup")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <style>
        .grid td, .grid th{text-align:center;}
        .spacing { margin-right:20px;}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl" >
				<div>                    
				<h3>جستجو دانشجو</h3>
                    <asp:Panel ID="panelSearch" runat="server" DefaultButton="btnSearch">
                                <asp:Label ID="lblStCode" runat="server" CssClass="spacing" ForeColor="#000A94" Font-Size="Medium">شماره دانشجويي</asp:Label>                            
                                <asp:TextBox ID="txtStCode" runat="server" MaxLength="14" Width ="103px" dir ="rtl" CssClass="marginItem" TabIndex="0" Height="25px" AutoCompleteType="Disabled"></asp:TextBox>                             
                                <asp:Label ID="lblFamily" runat="server" CssClass="spacing" ForeColor="#000A94" Font-Size="Medium">نام خانوادگي</asp:Label>                                
                                <asp:TextBox ID="txtfamily" runat="server" MaxLength="50" Width ="123px" dir ="rtl" CssClass="marginItem" TabIndex="1" Height="25px" AutoCompleteType="Disabled"></asp:TextBox>                            
                                <asp:Button ID="btnSearch" runat="server"  Text="جستجو" CssClass="btn btn-primary" OnClick="btnSearch_Click" Height="34px" Width="73px" />   
				    </asp:Panel>
                        </div>
			</div>
    <br />
    <div dir="rtl">
				<div>						
					<div style="color:black;overflow:scroll">
                        <asp:GridView ID="grdResults" runat="server" Width="1000px" CssClass="grid" CellPadding="4" HorizontalAlign="Center" ForeColor="#333333" GridLines="None" OnRowDataBound="dgvResults_RowDataBound" OnSelectedIndexChanged="dgvResults_SelectedIndexChanged" AllowPaging="True"  OnPageIndexChanging="dgvResults_PageIndexChanging"  >
                            <AlternatingRowStyle BackColor="White" HorizontalAlign="Center"/>
                        <Columns>
                            <asp:TemplateField HeaderText="رديف" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                 <asp:Label ID="lblRowNumber" Text='<%# (Container.DataItemIndex + 1) %>' runat="server" />
                                </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:TemplateField>
                           <asp:ButtonField ButtonType="Button" Text ="انتخاب" HeaderText ="انتخاب" Visible="false" />
                        </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>                            
                    </div>                    
				</div>  
        <div>
                    <h3><asp:Label ID="lblEdit" runat="server"  Visible ="false"></asp:Label></h3>
                </div>
                <div>     
                    <asp:Panel ID="pnlSaveEdit" runat="server" Visible="false" DefaultButton="btnOk">    
                    <asp:RequiredFieldValidator  ID="rfvMashmulNumber" CssClass="spacing" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtmashmulNumber" ValidationGroup="inputDataValidationGroup"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblMashmulNumber" runat="server" ForeColor="#000A94" Font-Size="Medium">شماره مشمولي</asp:Label>
                    <asp:TextBox ID="txtMashmulNumber"  runat="server" MaxLength="11" Width ="100px" Height="25px" dir ="rtl" CssClass="marginItem" TabIndex="0" AutoCompleteType="Disabled"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator  ID="rfvMashmulTarikh" CssClass="spacing" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtmashmulTarikh" ValidationGroup="inputDataValidationGroup"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblMashmulTarikh" runat="server" ForeColor="#000A94" Font-Size="Medium" >تاريخ مشمولي</asp:Label>     
                    <asp:TextBox ID="txtMashmulTarikh" runat="server" MaxLength="50" Width ="123px" Height="25px" dir ="rtl" CssClass="marginItem" TabIndex="1" AutoCompleteType="Disabled"></asp:TextBox>
                    <script>
                        var objCal1 = new AMIB.persianCalendar('<%=txtMashmulTarikh.ClientID%>',
                            { extraInputID: '<%=txtMashmulTarikh.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                    </script>                     
                    <asp:Label ID="lblStatus" runat="server" ForeColor="#000A94" CssClass="spacing" Font-Size="Medium" >مجوز</asp:Label>     
                        <asp:DropDownList id="drpStatus" runat="server" >
                            <asp:ListItem Value="دائم">دائم</asp:ListItem>
                            <asp:ListItem Value="موقت">موقت</asp:ListItem>
                        </asp:DropDownList>                    
                    <asp:Button ID="btnOk" runat="server" Text="ثبت" CssClass="btn btn-success" OnClick="btnOk_Click"  OnClientClick = "Confirm()" /> 
                    <asp:Button ID="btnCancel" runat="server" Text="لغو" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                    </asp:Panel>                    
                    <br />
                </div>      
        </div>
</asp:Content>
