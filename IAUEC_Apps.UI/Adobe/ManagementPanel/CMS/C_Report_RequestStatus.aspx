<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="C_Report_RequestStatus.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.CMS.C_Report_RequestStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>       
        <asp:Label ID="lbl_Customers" runat="server" Text="انتخاب مشتری"></asp:Label>
        <asp:DropDownList ID="ddl_Customers" runat="server"></asp:DropDownList>        
        <br />
        <asp:Label ID="lbl_Status" runat="server" Text="انتخاب وضعیت"></asp:Label>
        <asp:DropDownList ID="ddl_Status" runat="server"></asp:DropDownList>
        <asp:Button ID="btn_ShowRequest" runat="server" Text="مشاهده درخواست" OnClick="btn_ShowRequest_Click" />
    </div>

    <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="Default"
        OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="RadGrid1_ItemDataBound" >        
        <MasterTableView Dir="RTL">
            <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />
                        
            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>                        
                <telerik:GridBoundColumn DataField="Name" HeaderText="نام کلاس">
                </telerik:GridBoundColumn> 
                <telerik:GridBoundColumn DataField="UserCount" HeaderText="تعداد کل کاربران">
                </telerik:GridBoundColumn>    
                <telerik:GridBoundColumn DataField="DateStart" HeaderText="تاریخ شروع">
                </telerik:GridBoundColumn>        
                <telerik:GridBoundColumn DataField="DateEnd" HeaderText="تاریخ پایان">
                </telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="MeetingAccess" HeaderText="وضعیت دسترسی">
                </telerik:GridBoundColumn>  
                <telerik:GridBoundColumn DataField="Status" HeaderText="وضعیت درخواست">
                </telerik:GridBoundColumn>  
                
                <telerik:GridTemplateColumn>
                    <HeaderStyle HorizontalAlign="Right" />
                    <HeaderTemplate>
                        مشاهده کاربران
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Button  ID="btn_UserInfo" Text="مشاهده کاربران"  CommandName="UserInfo" 
                            Font-Names="tahoma" runat="server"  
                            CommandArgument='<%#Eval("Id")+","+ Eval("Name") %>'  />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>

                <telerik:GridTemplateColumn>
                    <HeaderStyle HorizontalAlign="Right" />
                    <HeaderTemplate> 
                       مشاهده جلسات 
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Button  ID="btn_MeetingInfo" Text="مشاهده جلسات" CommandName="MeetingInfo" 
                            Font-Names="tahoma" runat="server"  
                            CommandArgument='<%#Eval("Id")+","+ Eval("Name") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>   
                
                <telerik:GridTemplateColumn>
                    <HeaderStyle HorizontalAlign="Right" />
                    <HeaderTemplate>
                        مشاهده روزهای برگزاری
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Button  ID="btn_ClassDayTimeInfo" Text="مشاهده روزهای برگزاری" CommandName="ClassDayTimeInfo" 
                            Font-Names="tahoma" runat="server"  
                            CommandArgument='<%#Eval("Id")+","+ Eval("Name") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>  
                <telerik:GridTemplateColumn>
                    <HeaderStyle HorizontalAlign="Right" />
                    <HeaderTemplate>
                        مشاهده دلایل رد درخواست
                    </HeaderTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Button  ID="btn_RejectReason" Text="مشاهده دلایل رد درخواست" CommandName="RejectReason" 
                            Font-Names="tahoma" runat="server"  
                            CommandArgument='<%#Eval("Id")+","+ Eval("Name") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>  
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>


    <div runat="server" id="InfoUser" visible="false">
        <telerik:RadGrid ID="RadGrid3" AllowPaging="true" PageSize="20" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="Default">        
            <MasterTableView Dir="RTL">
                <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
                <HeaderStyle Font-Names="tahoma" />
                <AlternatingItemStyle Font-Names="tahoma" />
                        
                <Columns >
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                    </telerik:GridTemplateColumn>                        
                    <telerik:GridBoundColumn DataField="ClassName" HeaderText="نام کلاس">
                    </telerik:GridBoundColumn> 
                    <telerik:GridBoundColumn DataField="Name" HeaderText="نام">
                    </telerik:GridBoundColumn>    
                    <telerik:GridBoundColumn DataField="Family" HeaderText="نام خانوادگی">
                    </telerik:GridBoundColumn>        
                    <telerik:GridBoundColumn DataField="UserName" HeaderText="نام کاربری">
                    </telerik:GridBoundColumn>  
                                                                         
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>

    <div runat="server" id="InfoMeeting" visible="false">
        <telerik:RadGrid ID="RadGrid2" AllowPaging="true" PageSize="20" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="Default">            
            <MasterTableView Dir="RTL">
                <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
                <HeaderStyle Font-Names="tahoma" />
                <AlternatingItemStyle Font-Names="tahoma" />
                        
                <Columns >
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                    </telerik:GridTemplateColumn>                        
                    <telerik:GridBoundColumn DataField="ClassName" HeaderText="نام کلاس">
                    </telerik:GridBoundColumn>                      
                    <telerik:GridHyperLinkColumn DataTextField="MeetingLink" Target="_blank"
                        DataNavigateUrlFields="MeetingLink"                 
                        DataNavigateUrlFormatString= "{0}" >
                    </telerik:GridHyperLinkColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>    

    <div runat="server" id="InfoClassDayTime" visible="false">
        <telerik:RadGrid ID="RadGrid4" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="Default">        
            <MasterTableView Dir="RTL">
                <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
                <HeaderStyle Font-Names="tahoma" />
                <AlternatingItemStyle Font-Names="tahoma" />
                        
                <Columns >
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                    </telerik:GridTemplateColumn>                        
                    <telerik:GridBoundColumn DataField="ClassName" HeaderText="نام کلاس">
                    </telerik:GridBoundColumn> 
                    <telerik:GridBoundColumn DataField="DayName" HeaderText="روز برگزاری">
                    </telerik:GridBoundColumn>    
                    <telerik:GridBoundColumn DataField="BEGIN_HOUR" HeaderText="ساعت شروع">
                    </telerik:GridBoundColumn>        
                    <telerik:GridBoundColumn DataField="END_HOUR" HeaderText="ساعت پایان">
                    </telerik:GridBoundColumn>         
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div runat="server" id="InfoRejectReason" visible="false">
        <asp:Label ID="lbl_InfoRejectReason_ClassName" runat="server" Text=""></asp:Label>
        <br />
        <telerik:RadTextBox runat="server" ID="txt_Detail" 
            TextMode="MultiLine" Height="25%" Width="50%" Resize="None" Enabled="false">
        </telerik:RadTextBox><br />
    </div>

</asp:Content>
