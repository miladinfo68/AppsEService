<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="InsertExamPlace.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.InsertExamPlace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
     <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="blk" runat="server">
        <script type="text/javascript">


            function refreshGrid(arg) {
                if (!arg) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                 $find("<%= grd_ExamPlace.ClientID %>").get_masterTableView().rebind();
             }

         }



        </script>
    </telerik:RadCodeBlock>
    <div style="padding-left: 15px; padding-right: 15px;">
        <asp:Panel ID="pnl_Main" runat="server" HorizontalAlign="Center" Direction="RightToLeft">
            <br />
            <table style="width: 100%;">
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000;">نام شهر</td>
                    <td>
                        <asp:TextBox ID="txt_City" runat="server" ForeColor="Black"></asp:TextBox>
                    </td>
                    <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000;">آدرس</td>
                    <td>
                        <asp:TextBox ID="txt_Address" runat="server" TextMode="MultiLine" ForeColor="Black"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btn_Save" runat="server" Text="ثبت" OnClick="btn_Save_Click" CssClass="btn btn-success" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <div dir="rtl">
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="LsitLoadingPanel"></telerik:RadAjaxLoadingPanel>

            <telerik:RadGrid ID="grd_ExamPlace" runat="server"  OnItemCommand="grd_ExamPlace_ItemCommand" OnItemDataBound="grd_ExamPlace_ItemDataBound" AutoGenerateColumns="false" OnNeedDataSource="grd_ExamPlace_NeedDataSource" EnableEmbeddedSkins="False" skin="MyCustomSkin" >
                <MasterTableView Dir="RTL">
                    <HeaderStyle HorizontalAlign="Center"/>
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Name_City" HeaderText="شهر">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Address" HeaderText="آدرس">
                        </telerik:GridBoundColumn>
                       <telerik:GridTemplateColumn  HeaderText="وضعیت">
                            <ItemTemplate><%# Convert.ToBoolean(Eval("IsActive")) ? "فعال" : "غیر فعال" %></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <asp:Button ID="btn_Edit" runat="server" Text="ویرایش" CssClass="btn btn-info" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>

    </div>
</asp:Content>
