<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="ShowDetailsInfoPeople.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.ShowDetailsInfoPeople" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<telerik:RadListView runat="server" ID="rlv" OnItemDataBound="rlv_ItemDataBound" OnItemCommand="rlv_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-md-4" style="border: groove">
                                        <div class="row">
                                            <div class="col-md-6" style="color: dodgerblue; margin-bottom: 20px;">
                                                <asp:Label ID="lbl_Name" runat="server"></asp:Label>
                                            </div>
                                            <div style="margin: 10px; max-height: 150px; max-width: 70px; min-height: 150px; min-width: 70px;">
                                                <asp:Button ID="btn_Madrak" runat="server" Visible="false" Text='<%#Eval("document_name") %>' CommandArgument='<%#Eval("doc_type")%>' CommandName="Select" />
                                                <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" Visible="false" AutoAdjustImageControlSize="false" ResizeMode="Fit" Width="80px" Height="70px" />
                                                <asp:Button ID="btn_ShowPicture" runat="server" Visible="false" Text="بزرگنمایی" CommandArgument='<%#Eval("doc_type")%>' CommandName="ShowPic" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                            <asp:Panel ID="pnlMadrakOperation" runat="server">
                                                <div class="col-md-4">
                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                        <ContentTemplate>
                                                            <asp:RadioButton ID="rdb_Taeed" Text="تأیید مدرک" runat="server" ValidationGroup="0" GroupName="gh" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="rdb_Taeed" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:RadioButton ID="rdb_Naghs" Text="نقص مدرک" runat="server" ValidationGroup="1" GroupName="gh" AutoPostBack="true" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="rdb_Naghs" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="lbl_Sharh" Text="علت رد:" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txt_Sharh" runat="server" MaxLength="300" Width="200" Height="100" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:Label ID="lbl_Madrak" runat="server" Text="نام مدرک"></asp:Label>
                                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                        <ContentTemplate>
                                                            
                                                            <asp:DropDownList ID="ddlMadrak" runat="server" CssClass="form-control" AutoPostBack="true" Height="40px"></asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlMadrak" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </telerik:RadListView>--%>
    <telerik:RadWindowManager ID="rwd" runat="server">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close, Move" Height="200px" Modal="true" VisibleStatusbar="false" Width="300px">
                <ContentTemplate>
                    <div class="rwDialogPopup radconfirm">
                        <div class="rwDialogText">
                            <asp:Literal ID="confirmMessage" runat="server" Text="" />
                        </div>
                        <div>
                           <%-- <telerik:RadButton ID="rbConfirm_OK1" runat="server" OnClick="rbConfirm_OK1_Click" Text="بله">
                            </telerik:RadButton>
                            <telerik:RadButton ID="rbConfirm_Cancel1" runat="server" OnClientClicked="closeCustomConfirm1" Text="خیر">
                            </telerik:RadButton>--%>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close, Move" Modal="true" VisibleStatusbar="false">
                <ContentTemplate>
                    <div class="rwDialogPopup radconfirm">
                        <div class="rwDialogText">
                            <asp:Literal ID="MsgConf" runat="server" Text="" />
                        </div>
                        <div>
                            <telerik:RadButton ID="RadButton1" runat="server" OnClick="RadButton1_Click" Text="بله">
                            </telerik:RadButton>
                            <telerik:RadButton ID="RadButton2" OnClientClicked="closeCustomConfirm1" runat="server" Text="خیر">
                            </telerik:RadButton>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <asp:Button ID="btn_TaeedParvande" runat="server" OnClick="btn_TaeedParvande_Click" Text="ثبت وضعیت مدارک" CssClass="btn btn-info" />
</asp:Content>
