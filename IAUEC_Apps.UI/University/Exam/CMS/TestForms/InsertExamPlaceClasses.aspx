<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="InsertExamPlaceClasses.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.TestForms.InsertExamPlaceClasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
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
    <div dir="rtl">
        <asp:Panel ID="pnl_Main" HorizontalAlign="Right" runat="server">
            <table style="width: 800px">
                <tr>
                    <td style="font-family: 'b nazanin'; font-size: medium; font-weight: bold; color: #000000;">نام شهر:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_City" runat="server" DataTextField="City_Name" DataValueField="ID" ForeColor="Black">
                            <%--<asp:ListItem Value="1">تهران</asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                    <td style="font-family: 'b nazanin'; font-size: medium; font-weight: bold; color: #000000;">نام کلاس</td>
                    <td>
                        <asp:TextBox ID="txt_ClassName" runat="server" Width="118px" Height="25px" ForeColor="Black"></asp:TextBox></td>
                </tr>
                <tr style="font-family: 'b nazanin'; font-size: medium; font-weight: bold; color: #000000;">
                    <td>بازه شروع:</td>
                    <td>
                        <asp:TextBox ID="txt_Start" runat="server" Width="118px" Height="25px" ForeColor="Black"></asp:TextBox></td>
                    <td style="font-family: 'b nazanin'; font-size: medium; font-weight: bold; color: #000000;">بازه پایان:</td>
                    <td>
                        <asp:TextBox ID="txt_End" runat="server" Width="118px" Height="25px" ForeColor="Black"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btn_Save" runat="server" Text="ثبت" CssClass="btn-success" OnClick="btn_Save_Click" /></td>
                </tr>
            </table>

        </asp:Panel>
        <div>
            <br />
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="LsitLoadingPanel"></telerik:RadAjaxLoadingPanel>
            <telerik:RadGrid ID="grd_ExamPlace" runat="server" OnItemCommand="grd_ExamPlace_ItemCommand" OnItemDataBound="grd_ExamPlace_ItemDataBound" AutoGenerateColumns="false" OnNeedDataSource="grd_ExamPlace_NeedDataSource" EnableEmbeddedSkins="False" BackColor="#3A4A5B">
                <MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" CssClass="bg-purple" />
                    <ItemStyle HorizontalAlign="Center" />
                    <AlternatingItemStyle HorizontalAlign="Center" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="شهر" DataField="City_Name"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="محل آزمون" DataField="ExamPlace">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="بازه شروع" DataField="StartRange">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="بازه پایان" DataField="EndRange">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <asp:Button ID="btn_Edit" runat="server" Text="ویرایش" CssClass="btn-primary" OnClick="btn_Edit_Click" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn CancelImageUrl="Cancel.gif" InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </div>

    </div>
</asp:Content>
