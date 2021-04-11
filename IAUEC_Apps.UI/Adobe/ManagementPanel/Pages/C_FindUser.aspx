<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="C_FindUser.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.ManagementPanel.Pages.C_FindUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div >
        <asp:Label ID="lbl_ProfessorGuide" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_ProfessorAdvisor" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_ProfessorRefereeOne" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_ProfessorRefereeTwo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_Student" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_Type" runat="server" Text="" Visible="false"></asp:Label>

        <asp:Label ID="lbl_ClassName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_LatinClassName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_CountOfClass" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_CountOfUser" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_DateStart" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_DateEnd" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_DateDef" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_ClassType" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_UserType" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lbl_ClasDayTime" runat="server" Text="" Visible="false"></asp:Label>
    </div>



    <div dir="rtl">
        <h2><asp:Label ID="lbl_FinderTitle" runat="server" Text="" ></asp:Label></h2>
        <label>نام</label>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <label>نام خانوادگی</label>
        <asp:TextBox ID="txtFamily" runat="server"></asp:TextBox>
        <label>شماره ملی</label>
        <asp:TextBox ID="txtNationalCode" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Btn_Find" runat="server" Text="جستجو" OnClick="Btn_Find_Click" />
    </div>
    <br />
    
    <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="MyCustomSkin"  EnableEmbeddedSkins="False">        
        <MasterTableView Dir="RTL">
            <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />
                        
            <Columns >
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%#Container.ItemIndex+1 %> </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="UserId" HeaderText="شماره کاربر"  >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="نام کاربر"  >
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Family" HeaderText="نام خانوادگی">
                </telerik:GridBoundColumn> 
                <telerik:GridBoundColumn DataField="FatherName" HeaderText="نام پدر">
                </telerik:GridBoundColumn>                 
                <telerik:GridBoundColumn DataField="Id" HeaderText="شماره شناسنامه">
                </telerik:GridBoundColumn>   
                <telerik:GridBoundColumn DataField="NationalCode" HeaderText="شماره ملی">
                </telerik:GridBoundColumn>        
              
                <telerik:GridHyperLinkColumn   DataNavigateUrlFields= "ProfessorGuide,ProfessorAdvisor,ProfessorRefereeOne,ProfessorRefereeTwo
                    ,Student,Type,UserId,ClassName,LatinClassName,CountOfClass,CountOfUser,DateStart,DateEnd,DateDef
                    ,ClassType,UserType,ClasDayTime" Text="انتخاب"
                    DataNavigateUrlFormatString= "C_RequestFullCreate.aspx?ProfessorGuide={0}&ProfessorAdvisor={1}&ProfessorRefereeOne={2}
                    &ProfessorRefereeTwo={3}&Student={4}&Type={5}&UserId={6}&ClassName={7}&LatinClassName={8}&CountOfClass={9}
                    &CountOfUser={10}&DateStart={11}&DateEnd={12}&DateDef={13}&ClassType={14}&UserType={15}&ClasDayTime={16}">
                </telerik:GridHyperLinkColumn> 
            </Columns>

        </MasterTableView>
    </telerik:RadGrid>


</asp:Content>
