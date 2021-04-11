<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="C_RequestClassName_EditUser.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.Pages.C_RequestClassName_EditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <telerik:RadComboBox ID="ddl_ClassName" runat="server"
            Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="false"            
            Label="انتخاب کلاس: " Skin="Office2010Silver"
            AutoPostBack="true"
            OnSelectedIndexChanged="ddl_ClassName_SelectedIndexChanged" >
        </telerik:RadComboBox>
    </div>
    <div>
        <asp:Label ID="lbl_ClassName" runat="server" Text="نام دوره: "></asp:Label>
        <asp:Label ID="lbl_ClassName2" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_UserCount" runat="server" Text="ظرفیت کلاس: "></asp:Label>
        <asp:Label ID="lbl_UserCount2" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_UserCountInUse" runat="server" Text="ظرفیت پر شده: "></asp:Label>
        <asp:Label ID="lbl_UserCountInUse2" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbl_UserCountFree" runat="server" Text="ظرفیت خالی: "></asp:Label> 
        <asp:Label ID="lbl_UserCountFree2" runat="server" Text=""></asp:Label> 
    </div>

    <!-- بسته به نوع دوره یکی از این دو مورد نمایش داده می شود -->
    <div runat="server" id="ClassDayTime1" visible="false">
        <asp:Label ID="lbl_ClassDayTime1" runat="server" Text="روز برگزاری: "></asp:Label>
        <asp:Label ID="lbl_ClassDayTimeInfo1" runat="server" Text=""></asp:Label>
    </div>
    <div runat="server" id="ClassDayTime2" visible="false">
        <asp:Label ID="lbl_ClassDayTime2" runat="server" Text="روزهای برگزاری:"></asp:Label><br />
        <telerik:RadGrid ID="RadGrid_ClassDayTime2" AllowPaging="true" PageSize="20" runat="server"></telerik:RadGrid>
    </div>

    <!-- لیست کاربران -->
    <div>
        <asp:Label ID="lbl_UserList" runat="server" Text="لیست کاربران"></asp:Label><br />
        <telerik:RadGrid ID="RadGrid_UserList" runat="server" AutoGenerateColumns="False" 
            CellSpacing="0" GridLines="None" Skin="Default"  
            OnItemCommand="RadGrid_UserList_ItemCommand" AllowPaging="true" PageSize="20" OnItemDataBound="RadGrid_UserList_ItemDataBound">        
            <MasterTableView Dir="RTL">
                <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
                <HeaderStyle Font-Names="tahoma" />
                <AlternatingItemStyle Font-Names="tahoma" />                        
                <Columns >
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                    </telerik:GridTemplateColumn>                
                    <telerik:GridBoundColumn DataField="UserAccess" HeaderText="نوع کاربر">
                    </telerik:GridBoundColumn>      
                    <telerik:GridBoundColumn DataField="Name" HeaderText="نام کاربر"  >
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Family" HeaderText="نام خانوادگی">
                    </telerik:GridBoundColumn> 
                    <telerik:GridBoundColumn DataField="FatherName" HeaderText="نام پدر">
                    </telerik:GridBoundColumn>    
                    <telerik:GridBoundColumn DataField="NationalCode" HeaderText="شماره ملی">
                    </telerik:GridBoundColumn>                       
                    <telerik:GridBoundColumn DataField="IdNumber" HeaderText="شماره شناسنامه">
                    </telerik:GridBoundColumn>  

                    <telerik:GridTemplateColumn>
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderTemplate>
                            حذف کاربر
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="ibtn_RemoveUser" runat="server"  ImageUrl="../../images/A2.png"
                                Width="20px" Height="20px" CommandName="RemoveUser" 
                                CommandArgument='<%# Eval("ClassId")+","+Eval("Id") %>'/>
                         
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>    

                    <telerik:GridTemplateColumn>
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderTemplate>
                            کاربرجدید
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lbl_NewUser" runat="server" Text="جدید" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  


                </Columns>
            </MasterTableView>
        </telerik:RadGrid>

        <!-- اگر تعداد کاربران با مجموع کاربران یکی شد، این قسمت نباید دیده شود-->
        <div runat="server" id="AddUser" visible="false">
            <telerik:RadComboBox ID="ddl_StatusUser" runat="server"
                Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="false"            
                Label="نوع کاربر: " Skin="Office2010Silver" >
            </telerik:RadComboBox>
            <telerik:RadButton ID="rbtn_NewUser" runat="server" Text="RadButton" OnClick="rbtn_NewUser_Click"></telerik:RadButton>
        </div>
        

    </div>




</asp:Content>
