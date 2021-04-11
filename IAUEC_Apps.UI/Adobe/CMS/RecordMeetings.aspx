<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="RecordMeetings.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.RecordMeetings" %>
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
    <asp:Panel ID="p" runat="server" DefaultButton="btn_Search">
        <fieldset dir="rtl" style="padding: 2%" ><legend>  ضبط کلاس </legend>   
            <div dir="rtl"  align="center">
                <Label>نیمسال</Label>
                <telerik:RadComboBox ID="rcbTerm" runat="server"  ></telerik:RadComboBox><br />          
            </div >
            <br />
            <div dir="rtl"  align="center">
                <Label>تاریخ</Label>
                <asp:TextBox ID="txt_Date" runat="server" placeholder="1394/06/21"></asp:TextBox>
              
            </div >
            <br />
            <div dir="rtl"  align="center">
                <Label>میزگرد</Label>
                <asp:TextBox ID="txt_Code" runat="server" ></asp:TextBox>
            </div>
            <br />
        <%--    <div dir="rtl"  align="center">
                <Label>مدت بیشتر از(دقیقه)</Label>
                <asp:TextBox ID="txt_TimeOver" runat="server" ></asp:TextBox>
            </div>
            <br />--%>
            <div dir="rtl"  align="center">
                <asp:Button ID="btn_Search" Text="جستجو" runat="server" OnClick="btn_Search_Click" />
            </div>
            <br />
            <uc1:AccessControl ID="AccessControl1" runat="server" />
        </fieldset>
    
        <telerik:RadGrid ID="grd_Meetings" AllowPaging="true" PageSize="20" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" 
            OnNeedDataSource="grd_Meetings_NeedDataSource" AllowFilteringByColumn="false"  >
              <MasterTableView  >
                <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" /> 
                <HeaderStyle Font-Names="tahoma" />
                <AlternatingItemStyle Font-Names="tahoma" />
                <Columns>
                  <telerik:GridHyperLinkColumn DataNavigateUrlFields="Link" HeaderText="لینک"  Text="ضبط" Target="_blank"></telerik:GridHyperLinkColumn>
                   <%-- <telerik:GridBoundColumn DataField="URL" HeaderText="لینک" >
                    </telerik:GridBoundColumn>--%>
                    <%--   <telerik:GridBoundColumn DataField="session" HeaderText="جلسه">
                    </telerik:GridBoundColumn> --%>            
                            <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FileName" HeaderText="نام فایل">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FileType" HeaderText="نوع فایل">
                    </telerik:GridBoundColumn>
               
                    <telerik:GridBoundColumn DataField="Duration" HeaderText="(مدت(دقیقه">
                    </telerik:GridBoundColumn>  
                    <telerik:GridBoundColumn DataField="Size" HeaderText="(KB)حجم">
                    </telerik:GridBoundColumn> 
                    <%-- <telerik:GridBoundColumn DataField="ClassCode" HeaderText="کد میزگرد">
                    </telerik:GridBoundColumn>--%>
                  <%--    <telerik:GridBoundColumn DataField="LessonCode" HeaderText="کد درس">
                    </telerik:GridBoundColumn>--%>
                      <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </asp:Panel>
</asp:Content>
