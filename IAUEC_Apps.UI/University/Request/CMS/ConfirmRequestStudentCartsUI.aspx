<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSRequestMaster.Master" AutoEventWireup="true" CodeBehind="ConfirmRequestStudentCartsUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.ConfirmRequestStudentCartsUI" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }
    </style>
    <table style="width: 100%; text-align: right; float: right;">
        <tr>
            <td>&nbsp;</td>
            <td>


                <uc1:AccessControl ID="AccessControl1" runat="server" />


                <br />

                <div dir="rtl">

                    <telerik:RadGrid ID="grd_CardRequest" runat="server" FilterItemStyle-Height="23px" AllowFilteringByColumn="true" AutoGenerateColumns="false" OnNeedDataSource="grd_CartRequest_NeedDataSource" OnItemCommand="grd_CartRequest_ItemCommand" ActiveItemStyle-Width="100%" EnableEmbeddedSkins="False">

                        <MasterTableView>
                            <HeaderStyle CssClass="bg-blue" HorizontalAlign="Center" Font-Size="Small" Font-Names="tahoma" />



                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="name" HeaderText="نام" ItemStyle-HorizontalAlign="Center" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" />
                                <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" ItemStyle-HorizontalAlign="Center" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" />
                                <telerik:GridBoundColumn DataField="salvorud" HeaderText="سال ورود" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="reshname" HeaderText="رشته" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" />
                                <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ درخواست" AllowFiltering="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RequestTypeID" HeaderText="نوع درخواست" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                <telerik:GridBoundColumn DataField="StudentRequestID" HeaderText="آی دی" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" Visible="false" />

                                <telerik:GridTemplateColumn AllowFiltering="false">

                                    <HeaderTemplate>
                                        پرینت گرفته شد
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Button ID="btn_Taeid" Text="پرینت گرفته شد" CommandName="taeiddarkhast" Font-Names="tahoma" runat="server" CommandArgument='<%#Eval("stcode")+","+ Eval("RequestTypeID")+","+Eval("StudentRequestID") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="false" DataField="Date" HeaderText="تاریخ درخواست"></telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings UserControlName="TextBoxCodePost.ascx"
                                EditFormType="WebUserControl">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                            </EditFormSettings>

                        </MasterTableView>

                    </telerik:RadGrid>
                </div>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="height: 23px"></td>
            <td style="text-align: center; height: 23px;">
                <asp:Label ID="lbl_Msg" runat="server" Text="&quot;درخواست کارت موجود نمی باشد&quot;" Visible="false"></asp:Label>
            </td>
            <td style="height: 23px"></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <div>

        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />
    </div>
</asp:Content>
