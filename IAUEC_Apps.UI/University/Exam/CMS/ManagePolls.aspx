<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="ManagePolls.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ManagePolls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .pollManagerWrapper {
            direction: rtl;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%----%>
    <div class="pollManagerWrapper">
        <div class="row">
            <div class="col-sm-12">
                <asp:LinkButton runat="server" ID="btnAddPoll" CssClass="btn btn-info" OnClick="btnAddPoll_Click">
                    <i class="fa fa-plus"></i> <span> افزودن مورد جدید</span>
                </asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <telerik:RadGrid runat="server" ID="rgvPolls" OnNeedDataSource="rgvPolls_NeedDataSource" AutoGenerateColumns="false"
                    EnableEmbeddedSkins="False" BackColor="#3A4A5B" ForeColor="White" CssClass="grdResult" OnItemCommand="rgvPolls_ItemCommand">
                    <HeaderStyle HorizontalAlign="Center" ForeColor="White" CssClass="bg-purple" />
                    <ItemStyle HorizontalAlign="Center" />
                    <AlternatingItemStyle HorizontalAlign="Center" />
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="Id" HeaderText="شناسه" ItemStyle-Width="5%"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Title" HeaderText="عنوان" ItemStyle-Width="20%"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Term" HeaderText="ترم" ItemStyle-Width="10%"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Description" HeaderText="توضیحات نظرسنجی" ItemStyle-Width="30%"
                                ItemStyle-HorizontalAlign="Right">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="نیاز به ثبت توضیحات" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%# Convert.ToBoolean(Eval("NeedComment")) ? "دارد" : "ندارد" %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="عملیات" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnEdit" CommandName="EditPoll" CommandArgument='<%# Eval("Id") %>' Text="ویرایش"
                                        CssClass="btn btn-success" />
                                    <asp:Button runat="server" ID="btnEditQuestion" CommandName="EditQuestion" CommandArgument='<%# Eval("Id") %>'
                                        Text="ویرایش سوالات" CssClass="btn btn-info" />
                                    <asp:Button runat="server" ID="btnCopyPoll" CommandName="CopyPoll" CommandArgument='<%# Eval("Id") %>'
                                        Text="کپی" CssClass="btn btn-primary" />
                                    <asp:Button runat="server" ID="btnDelete" CommandName="DeletePoll" CommandArgument='<%# Eval("Id") %>' Text="حذف"
                                        CssClass="btn btn-danger" OnClientClick="if(!confirm('آیا حذف این پرسشنامه و تمامی پاسخ های مرتبط به آن اطمینان دارید؟')) return false;" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function refreshGrid(arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest" PostBackControls="rgvPolls">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgvPolls"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="gridLoadingPanel"></telerik:RadAjaxLoadingPanel>
</asp:Content>
