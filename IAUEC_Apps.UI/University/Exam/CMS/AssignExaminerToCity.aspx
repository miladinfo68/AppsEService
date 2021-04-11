<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="AssignExaminerToCity.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.AssignExaminerToCity" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" media="all" href="../../../Adobe/css/aqua/theme.css" title="Aqua" />
	
	    
	    
		<!-- import the Jalali Date Class script -->
		<script type="text/javascript" src="../../../Adobe/js/jalali.js"></script>
		
		<!-- import the calendar script -->
		<script type="text/javascript" src="../../../Adobe/js/calendar.js"></script>
		
		<!-- import the calendar script -->
		<script type="text/javascript" src="../../../Adobe/js/calendar-setup.js"></script>
		
		<!-- import the language module -->
		<script type="text/javascript" src="../../../Adobe/js/lang/calendar-fa.js"></script>
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7);background-color:rgba(170, 102, 204,0.1);border-radius:5px; margin-bottom:1%;padding:1%;color:#000">
           <div class="row">
                <div class="col-md-12">
                    <div class="col-md-1">شهر:</div>
                    <div class="col-md-2"><asp:DropDownList ID="ddl_city" runat="server"></asp:DropDownList></div>
                    <div class="col-md-1">کاربر:</div>
                    <div class="col-md-2"><asp:DropDownList ID="ddl_user" runat="server"></asp:DropDownList></div>
                    <div class="col-md-1">نام کاربر:</div>
                    <div class="col-md-3"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_username" ErrorMessage="*" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="group1">*</asp:RequiredFieldValidator><asp:TextBox ID="txt_username" runat="server"></asp:TextBox>
                        
                    </div>
                    
               </div>
                <uc1:AccessControl ID="AccessControl1" runat="server" />
               </div>
            <div class="row">
                <div class="col-md-12">
                     <div class="col-md-1">موبایل:</div>
                    <div class="col-md-2"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txt_mobile" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ValidationGroup="group1"></asp:RequiredFieldValidator><asp:TextBox ID="txt_mobile" runat="server" Width="78px"></asp:TextBox></div>
                     <div class="col-md-1">ایمیل:</div>
                    <div class="col-md-2"><asp:TextBox ID="txt_Email" runat="server"></asp:TextBox></div>    
                     <div class="col-md-1 example">تاریخ شروع:</div>
                         <div class="col-md-1"> <input id="date_input_1" type="text" runat="server" style="width:78px"  />
                        <img id="date_btn_1" src="../../../Adobe/images/cal.png" style="vertical-align: top;" />
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
		        </script></div>
                          <div class="col-md-1">تاریخ پایان:</div>
                         <div class="col-md-1"> <input id="date_input_2" type="text" runat="server" style="width:78px" />
                        <img id="date_btn_2" src="../../../Adobe/images/cal.png" style="vertical-align: top;" />
                        <script type="text/javascript">

                            Calendar.setup({
                                inputField: "ContentPlaceHolder1_date_input_2",   // id of the input field
                                button: "date_btn_2",   // trigger for the calendar (button ID)
                                ifFormat: "%Y/%m/%d",       // format of the input field
                                dateType: 'jalali',
                                weekNumbers: false
                            });
			        </script></div>
                
                    <div class="col-md-2"><asp:Button ID="btn_Save" runat="server" Text="ثبت" OnClick="btn_Save_Click" CssClass="btn btn-success" ValidationGroup="group1"/></div>
                </div>
                </div>
                
          
             <div class="row">
                <div class="col-md-12">
                    <telerik:RadGrid ID="grd_Examiner" runat="server" AutoGenerateColumns="false" EnableEmbeddedSkins="false" Skin="MyCustomSkin" OnItemCommand="grd_Examiner_ItemCommand">
                       
                        <MasterTableView AutoGenerateColumns="false">
                             <ItemStyle />
                    <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                 <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Name_City" HeaderText="شهر"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="UserName" HeaderText="نام کاربری"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ExaminerName" HeaderText="نام ممتحن"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Mobile" HeaderText="موبایل"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Email" HeaderText="ایمیل"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="StartDate" HeaderText="شروع"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EndDate" HeaderText="پایان"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <asp:Button ID="btn_SendSMS" runat="server" CssClass="btn btn-success" CommandName="SMS" CommandArgument='<%#Eval("ID") %>' Text="ارسال پیامک" CausesValidation="False" />
                                        <asp:Button ID="btn_SendEmail" runat="server" CssClass="btn btn-warning" CommandName="Email" CommandArgument='<%#Eval("ID") %>' Text="ارسال ایمیل" CausesValidation="False" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    </div>
                 </div>  </div>
      <asp:Label ID ="lbl_Resault" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="Lbl_Status" runat="server" Visible="false"></asp:Label>
</asp:Content>
