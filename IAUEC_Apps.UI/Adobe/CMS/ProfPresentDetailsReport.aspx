<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="ProfPresentDetailsReport.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.ProfPresentDetailsReport" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
        <link href="../../University/Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Input.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Ajax.MyCustomSkin.css" rel="stylesheet" />
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
    <asp:Panel ID="p" runat="server" DefaultButton="btn_Search"> 
        <div dir="rtl">
            <asp:Label ID="lbl_FieldStar" runat="server" Text="فیلدهای ستاره دار باید پر شود" ForeColor="Red"></asp:Label>
            <br />
            *<asp:Label ID="lbl_ProfCode2" runat="server" Text="کداستاد"></asp:Label>
            <asp:TextBox ID="txt_ProfCode2" runat="server" Width="150"></asp:TextBox>
            *<asp:Label ID="lbl_ClassCode1" runat="server" Text="کد کلاس"></asp:Label>
            <asp:TextBox ID="txt_ClassCode1" runat="server" Width="150"></asp:TextBox>
            <asp:Label ID="lbl_Erfaq" runat="server" Text="ارفاق به ازای هر واحد درسی"></asp:Label>
            <asp:TextBox ID="txt_Erfaq" runat="server" Width="150" Text="20" ReadOnly="true" ></asp:TextBox>
            <br />
           * <asp:Label ID="lbl_DateStart" runat="server" Text="از تاریخ"></asp:Label>
            
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
                 
            *<asp:Label ID="lbl_DateEnd" runat="server" Text="تا تاریخ"></asp:Label>
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
           
            
            <asp:Label ID="lbl_Nimsal" runat="server" Text="انتخاب نیمسال"></asp:Label>
            <asp:DropDownList ID="ddl_Nimsal" runat="server"></asp:DropDownList>
            <asp:Button ID="btn_Search" runat="server" Text="جستجو" OnClick="btn2_Click" />         
        </div> 
         
        <div dir="rtl">
            <asp:Label ID="lbl_TeacherName" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lbl_LessonName" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lbl_TimeErfagh" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lbl_CountHazer" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lbl_CountQayeb" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lbl_CountLate" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lbl_CountSoon" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lbl_Image" runat="server" Text="" Visible="false"></asp:Label>
        </div>

        <asp:ImageButton ID="ConvertExcel" runat="server" ImageUrl="~/Adobe/images/Excel02.jpg"
            OnClick="ConvertExcel_Click" AlternateText="Convert To Excel" Width="40"/>
    
        <telerik:RadGrid ID="grd_ConvertToDataTable" AllowPaging="true" PageSize="20" runat="server"  Skin="MyCustomSkin"  EnableEmbeddedSkins="False" AutoGenerateColumns="False" CellSpacing="0" GridLines="None">    
            <MasterTableView Dir="RTL">            
                <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
                <HeaderStyle Font-Names="tahoma" />
                <AlternatingItemStyle Font-Names="tahoma" />         
                <Columns >
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="PersianDate" HeaderText="تاریخ">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TimeStart" HeaderText="ساعت کلاس">
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="TimeEND" HeaderText="زمان پایان">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn DataField="FirstLogin" HeaderText="شروع کلاس">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="LastLogOut" HeaderText="پایان کلاس">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SumOfTime" HeaderText="مدت زمان حضور">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TimeClass" HeaderText="مدت زمان کلاس">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Soon" HeaderText="تعجیل">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Late" HeaderText="تاخیر">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Status" HeaderText="وضعیت">
                    </telerik:GridBoundColumn>  
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <uc1:AccessControl ID="AccessControl1" runat="server" />
    </asp:Panel>
</asp:Content>
