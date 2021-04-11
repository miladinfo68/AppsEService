<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/CMSEmailMaster.Master" AutoEventWireup="true" CodeBehind="List_AfterStudentRequest.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.CMS.List_AfterStudentRequest" %>

<%@ Register Src="../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">

    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../University/Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function CallBackConfirm(arg) {
            if (arg)
                window.location.href = "RegisterStudents.aspx";
        }
    </script>
     <style>
    .RadGrid .rgFilterRow input {
            height:25px;
        }
    </style>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista">
    </telerik:RadWindowManager>
    <div class="container">
        <fieldset dir="rtl">
            <%--<legend>مشاهده</legend>--%>
            <div class="row">
                <div class="col-md-6">
                    <asp:ImageButton ID="ExportToExcelImg" runat="server" ImageUrl="~/EmailReg/images/Excel_ExcelML.png"
                        AlternateText="ExcelML" OnClick="ExportToExcelImg_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadGrid Width="100%" ClientSettings-Scrolling-AllowScroll="true" ShowHeaderWhenEmpty="True"  AllowFilteringByColumn="true"  ID="grd_ListAfterStdRequest" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="False" EnableEmbeddedSkins="False" OnItemCommand="RadGrid1_ItemCommand" OnNeedDataSource="RadGrid1_NeedDataSource">

                        <MasterTableView >
                            <HeaderStyle CssClass="bg-orange" BackColor="#ffba42" Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center"  />
                            <ItemStyle HorizontalAlign="Center" BackColor="#fef798"/>
                            <AlternatingItemStyle HorizontalAlign="Center" BackColor="#f5e303" />
                            <FilterItemStyle HorizontalAlign="Center" CssClass="bg-orange" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                                    <ItemTemplate>
                                        <%# (Container.ItemIndex+1).ToString() %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="false" DataField="id" Visible="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="false" DataField="PersianDate" HeaderText="تاریخ" HeaderStyle-Width="100px" ItemStyle-Width="100px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../images/filter2.png" DataField="name"  HeaderText="نام و نام خانوادگی"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../images/filter2.png" DataField="Stcode" HeaderText="شماره دانشجویی" HeaderStyle-Width="220px" ItemStyle-Width="220px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="false" DataField="Email_Address" HeaderText="پست الکترونیکی" HeaderStyle-Width="420px" ItemStyle-Width="420px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="تایید" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="bntAccept11" runat="server" ImageUrl="~/EmailReg/images/A1.png" Width="20px" Height="20px"
                                            CommandArgument='<%# Eval("id").ToString() %>' CommandName="ok" />

                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="رد" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btn_NotAccept11" runat="server" ImageUrl="~/EmailReg/images/A2.png" Width="20px" Height="20px"
                                            CommandArgument='<%# Eval("id").ToString()  %>' CommandName="Notok" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:Label runat="server" ID="lbl_Resault" Visible="false"></asp:Label>
                    <asp:Label runat="server" ID="lbl_Status" Visible="false"></asp:Label>
                    <uc1:AccessControl ID="AccessControl1" runat="server" />
                </div>
            </div>
        </fieldset>
    </div>


</asp:Content>
