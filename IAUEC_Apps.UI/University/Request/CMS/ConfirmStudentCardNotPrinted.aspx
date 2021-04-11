<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSRequestMaster.Master" AutoEventWireup="true" CodeBehind="ConfirmStudentCardNotPrinted.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.ConfirmStudentCardNotPrinted" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }
    </style>
    <div dir="rtl">

        <telerik:RadGrid ID="grd_CardRequest" AllowPaging="true" PageSize="20" FilterItemStyle-Height="23px" runat="server" AllowFilteringByColumn="true" AutoGenerateColumns="false" OnItemDataBound="grd_CardRequest_ItemDataBound" OnNeedDataSource="grd_CartRequest_NeedDataSource" OnItemCommand="grd_CartRequest_ItemCommand" ActiveItemStyle-Width="100%" EnableEmbeddedSkins="False">

            <MasterTableView>
                <HeaderStyle CssClass="bg-blue" HorizontalAlign="Center" Font-Size="Small" Font-Names="tahoma" />



                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="salvorud" HeaderText="سال ورود" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="reshname" HeaderText="رشته" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ درخواست" AllowFiltering="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RequestTypeID" HeaderText="نوع درخواست" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" Visible="false" />
                    <telerik:GridBoundColumn DataField="StudentRequestID" HeaderText="آی دی" AllowFiltering="false" ItemStyle-HorizontalAlign="Center" Visible="false" />



                    <telerik:GridTemplateColumn AllowFiltering="false">
                        <HeaderStyle HorizontalAlign="Right" />
                        <HeaderTemplate>
                            چاپ آدرس روی پاکت
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Button ID="btn_Print" Text="چاپ " CommandName="printAddress" Font-Names="tahoma" runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" EditText=" کد مرسوله پستی" UpdateText="ویرایش">
                        <ItemStyle Font-Names="Tahoma" Font-Size="Small" HorizontalAlign="center" Width="80px" ForeColor="blue" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false">
                        <ItemTemplate>
                            <asp:HiddenField ID="hd_Field" Value='<%#Eval("StudentRequestId")%>' runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings UserControlName="TextBoxCodePost.ascx"
                    EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>

            </MasterTableView>

        </telerik:RadGrid>
    </div>
    <div>

        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />
    </div>
</asp:Content>
