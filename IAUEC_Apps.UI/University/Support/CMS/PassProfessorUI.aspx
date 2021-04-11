<%@ Page Title="" Language="C#" MasterPageFile="~/University/Support/MasterPage/CMSSupportMaster.Master" AutoEventWireup="true" CodeBehind="PassProfessorUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Support.PassProfessorUI" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="css/Style1.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .validation {
        }

        /*.auto-style1 {
            width: 416px;
        }*/
    </style>
    <link href="../../Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function closeCustomConfirm() {
            $find("<%=rw_customConfirm.ClientID %>").close();
    }
    </script>
    <asp:UpdatePanel runat="server" ID="UP2">
        <ContentTemplate>
            <asp:Panel runat="server" DefaultButton="btn_SendGroup" ID="pnl_Send" Width="100%" HorizontalAlign="Center" Direction="RightToLeft" BorderStyle="Groove" BackColor="MintCream">
                <div style="text-align: center;">
                    <table style="border: medium">

                        <tr>
                            <td>
                                <asp:Label ID="lbl_RequstSend" runat="server" Text="نوع درخواست را مشخص کنید :"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_RequestSend" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_RequestSend_SelectedIndexChanged" Width="250px"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddl_RequestSend" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 10px"></td>
                            <td style="width: 10px"></td>
                            <td style="width: 10px"></td>
                            <td>
                                <asp:Label ID="lbl_SelectSend" Text="نوع ارسال را مشخص کنید :" runat="server"></asp:Label>
                            </td>

                            <td>
                                <asp:RadioButton ID="rdb_Single" runat="server" GroupName="InfoSend" ValidationGroup="0" Text="فردی" AutoPostBack="true" OnCheckedChanged="InfoSend_CheckedChanged" />
                                <%--                      <asp:UpdatePanel runat="server" ID="hj">
                          <ContentTemplate>
                              <asp:RadioButton ID="rdb_Single" runat="server" GroupName="InfoSend" ValidationGroup="0" Text="فردی" AutoPostBack="true" OnCheckedChanged="InfoSend_CheckedChanged"/>
                          </ContentTemplate>
                      </asp:UpdatePanel>--%>
                            </td>

                            <td>
                                <asp:RadioButton ID="rdb_Group" runat="server" GroupName="InfoSend" ValidationGroup="1" Text="گروهی" AutoPostBack="true" OnCheckedChanged="InfoSend_CheckedChanged" />

                                <%--             <asp:UpdatePanel ID="UP3" runat="server">
                            <ContentTemplate>
                                 <asp:RadioButton ID="rdb_Group" runat="server" GroupName="InfoSend" ValidationGroup="1" Text="گروهی" AutoPostBack="true" OnCheckedChanged="InfoSend_CheckedChanged" />        
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdb_Group"/>
                            </Triggers>
                        </asp:UpdatePanel>  --%>
                                                    
                            </td>
                        </tr>
                        <caption>
                            <br />
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_Daneshkade" runat="server" Text="دانشکده :" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_Daneshkade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" Visible="false" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_Departman" runat="server" Text="گروه :" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_Departman" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Departman_SelectedIndexChanged" Visible="false" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btn_SendGroup" runat="server" OnClick="btn_SendGroup_Click" Text="ارسال" Visible="false" />
                                </td>
                            </tr>
                        </caption>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="Update5" runat="server">
        <ContentTemplate>
            <asp:Panel DefaultButton="btn_Search" runat="server" ID="pnl_Prof" Visible="false" Width="100%" HorizontalAlign="Center" Direction="RightToLeft" BackColor="MintCream" BorderStyle="Groove">
                <div style="text-align: center;">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_Tavajoh" runat="server" Font-Names="tahoma" Text="توجه : جستجو بر اساس کد استاد یا نام خانوادگی یا کد ملی امکان پذیر است" text-align="center" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_ProfCode" runat="server" BorderColor="#CC00CC" dir="rtl" Font-Names="tahoma" Font-Size="Small" Text="کد استاد :"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Code" runat="server" AutoComplete="off" dir="rtl" Style="margin-right: 0px" Width="100px"></asp:TextBox>
                            </td>
                            <td style="width: 65px"></td>
                            <td style="grid-row-align: center">
                                <asp:Label ID="lbl_ProfName" runat="server" dir="rtl" Font-Names="tahoma" Font-Size="Small" Text=" نام خانوادگی:"></asp:Label>
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="txt_Name" runat="server" AutoComplete="off" dir="rtl" Width="120px"></asp:TextBox>
                            </td>
                            <td style="width: 65px"></td>
                            <td>
                                <asp:Label ID="lbl_CodeMelli" runat="server" dir="rtl" Font-Names="Tahoma" Font-Size="Small" Text="کد ملی:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_CodeMelli" runat="server" AutoComplete="off" dir="rtl" Width="100px"></asp:TextBox>
                            </td>
                            <td style="width: 65px"></td>
                            <td>
                                <asp:Button ID="btn_Search" runat="server" Font-Names="tahoma" Font-Size="Small" OnClick="BtnSearch_click" Text="جستجو" Width="103px" />
                                <uc1:AccessControl ID="AccessControl1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="UP54" runat="server">
        <ContentTemplate>
            <div dir="rtl">
                <telerik:RadGrid ID="grd_PassProf" runat="server" ActiveItemStyleWidth="100%" AutoGenerateColumns="False" CellSpacing="0" EnableEmbeddedSkins="False" GridLines="None" OnItemCommand="grd_PassProf_ItemCommand1" Skin="MyCustomSkin">
                    <MasterTableView>
                        <%--<HeaderStyle BackColor="#335B8C" ForeColor="Black" HorizontalAlign="Center" Font-Size="Small" />--%>
                        <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center" />
                        <Columns>
                            <%--<telerik:GridButtonColumn HeaderText ="نحوه ارسال رمز" ItemStyle-HorizontalAlign ="Center" ImageUrl ="sent-SMS.png" CommandName="SendSMS" Text ="SMS" ></telerik:GridButtonColumn>--%>
                            <telerik:GridBoundColumn DataField="Code_ostad" HeaderText="کد استاد" ItemStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="name" HeaderText="نام  " ItemStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" ItemStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="add_email" HeaderText="ایمیل" ItemStyle-HorizontalAlign="Center" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="mobile" HeaderText="موبایل" ItemStyle-HorizontalAlign="Center">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="add_home" HeaderText="آدرس" ItemStyle-HorizontalAlign="Center" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="ارسال ایمیل">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_SendEmail" runat="server" CommandArgument='<%#Eval("Code_ostad")+","+Eval("name")+","+Eval("family")+","+Eval("add_email")+","+Eval("mobile")+ ","+Eval("password_ost") %>' CommandName="SendEmail" ImageUrl="~/University/Theme/images/mail.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridButtonColumn HeaderText ="نحوه ارسال رمز" ItemStyle-HorizontalAlign ="Center" ImageUrl ="sent-Mail.png" Text="Email" CommandName ="SendEmail" CommandArgument ='<%#Eval("add_email")+","+Eval("Code_ostad") %>' ></telerik:GridButtonColumn>--%>
                            <telerik:GridTemplateColumn HeaderText="ارسال پیامک">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_SendSMS" runat="server" CommandArgument='<%#Eval("Code_ostad")+","+Eval("name")+","+Eval("family")+","+Eval("add_email")+","+Eval("mobile")+ ","+Eval("password_ost") %>' CommandName="SendSMS" ImageUrl="~/University/Theme/images/sms.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="password_ost" HeaderText="نمایش رمز" ItemStyle-HorizontalAlign="Center" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="ارسال پیامک وایمیل">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_SendSmsEmail" runat="server" CommandArgument='<%#Eval("Code_ostad")+","+Eval("name")+","+Eval("family")+","+Eval("add_email")+","+Eval("mobile")+ ","+Eval("password_ost") %>' CommandName="SendSmsEmail" ImageUrl="~/University/Theme/images/Email-SMS.png" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="پیگیری وضعیت ارسال پیامک">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btn_ShowStatusMessage" runat="server" CommandName="ShowStatusMessage" CommandArgument='<%#Eval("Code_ostad")%>' ImageUrl="~/University/Theme/images/TrackerSMS.jpg" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <br />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="rw_customConfirm" runat="server" Behaviors="Close, Move" Height="200px" Modal="true" VisibleStatusbar="false" Width="300px">
                <ContentTemplate>
                    <div class="rwDialogPopup radconfirm">
                        <div class="rwDialogText">
                            <asp:Literal ID="confirmMessage" runat="server" Text="آیا موافق به ارسال رمز از طریق پیامک هستید" />
                        </div>
                        <div>
                            <telerik:RadButton ID="rbConfirm_OK" runat="server" OnClick="rbConfirm_OK_Click" Text="بله">
                            </telerik:RadButton>
                            <telerik:RadButton ID="rbConfirm_Cancel" runat="server" OnClientClicked="closeCustomConfirm" Text="خیر">
                            </telerik:RadButton>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <br />
    <br />
    <br />
    <asp:Label ID="lbl_Resault" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="Lbl_Status" runat="server" Visible="false"></asp:Label>
    <br />


</asp:Content>
