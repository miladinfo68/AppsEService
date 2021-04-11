<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="SessionMonitoring_Meetings.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.SessionMonitoring_Meetings" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <asp:Panel ID="p" runat="server" DefaultButton="btn_Filter">
    
    
    <uc1:AccessControl ID="AccessControl1" runat="server" />  

    <div dir="rtl">
        <asp:Label ID="lbl_KamtarAz" runat="server" Text="مدت زمان کمتر از" ForeColor="Black"></asp:Label>
        <asp:TextBox ID="txt_MinMinute" runat="server" Width="150" Text="0"></asp:TextBox>
        <asp:Label ID="lbl_Min1" runat="server" Text="دقیقه" ForeColor="Black"></asp:Label>
        <br />
        <asp:Label ID="lbl_BishtarAz" runat="server" Text="مدت زمان بیشتر از" ForeColor="Black"></asp:Label>
        <asp:TextBox ID="txt_MaxMinute" runat="server" Width="150" Text="0"></asp:TextBox>
        <asp:Label ID="lbl_Min2" runat="server" Text="دقیقه" ForeColor="Black"></asp:Label>
        <br />
        <asp:Label ID="lbl_UserKamtarAz" runat="server" Text="کاربران کمتر از" ForeColor="Black"></asp:Label>
        <asp:TextBox ID="txt_MinUser" runat="server" Width="150" Text="0"></asp:TextBox>
        <asp:Label ID="lbl_User1" runat="server" Text="نفر" ForeColor="Black"></asp:Label>
        <br />
        <asp:Label ID="lbl_UserBishtarAz" runat="server" Text=" کاربران بیشتر از" ForeColor="Black"></asp:Label>
        <asp:TextBox ID="txt_MaxUser" runat="server" Width="150" Text="0"></asp:TextBox>
        <asp:Label ID="lbl_User2" runat="server" Text="نفر" ForeColor="Black"></asp:Label>
        <br />



        *<asp:Label ID="lbl_Note" runat="server" Text="در صورت قراردادن مقدار صفر، بی تاثیر می شوند" ForeColor="Red"></asp:Label>


        <asp:Button ID="btn_Filter" runat="server" Text="فیلتر" OnClick="btn_Filter_Click" />      
 
    </div>
    <div dir="rtl">
        <br />
        <asp:Label ID="lbl_CountOfOnlineClass" runat="server" Text="-" ForeColor="Black"></asp:Label>
        <br />
        <asp:Label ID="lbl_CountOfUserOnlineInClass" runat="server" Text="-" ForeColor="Black"></asp:Label>
    </div>

    <telerik:RadGrid ID="grd_Session" AllowPaging="true" PageSize="100" runat="server" AutoGenerateColumns="False"  GridLines="None" OnNeedDataSource="grd_Session_NeedDataSource">   
        <MasterTableView Dir="RTL">                              
            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>                            
                <telerik:GridBoundColumn DataField="NAME" HeaderText="نام کلاس">
                </telerik:GridBoundColumn>   
                <telerik:GridBoundColumn DataField="code" HeaderText="مشخصه کلاس">
                </telerik:GridBoundColumn> 
                <telerik:GridBoundColumn DataField="CountOFUser" HeaderText="مجموع کاربران"> 
                </telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="TimeStart" HeaderText="زمان شروع">
                </telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="ShamsiDate" HeaderText="زمان شروع به شمسی">
                </telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="Day" HeaderText="مدت کلاس به روز">
                </telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="TotalTime" HeaderText="مدت کلاس به دقیقه">
                </telerik:GridBoundColumn>     
                <telerik:GridHyperLinkColumn DataNavigateUrlFields= "Link" Text="مشاهده کلاس" Target="_blank"
                    DataNavigateUrlFormatString= "{0}">
                </telerik:GridHyperLinkColumn>  
                <telerik:GridHyperLinkColumn DataNavigateUrlFields= "SCO_ID,NAME" Text="مشاهده اعضاء"
                    DataNavigateUrlFormatString= "~/Adobe/CMS/SessionMonitoring_Users.aspx?SCO_ID={0}&NAME={1}">
                </telerik:GridHyperLinkColumn>         
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>    
        </asp:Panel>
</asp:Content>
