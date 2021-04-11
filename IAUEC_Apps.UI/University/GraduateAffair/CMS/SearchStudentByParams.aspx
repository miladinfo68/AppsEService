<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="SearchStudentByParams.aspx.cs" Inherits="IAUEC_Apps.UI.University.GraduateAffair.CMS.SearchStudentByParams" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .RadGrid .rgFilterRow input {
            height: 23px;
        }
    </style>
    <asp:Literal ID="pt" runat="server"></asp:Literal>

    <div class="container table-responsive" dir="rtl">
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grd_Student" CssClass="table table-hover table-condensed table-bordered" EnableEmbeddedSkins="false" AllowPaging="true" PageSize="20" runat="server" OnItemCommand="grd_Student_ItemCommand" AllowFilteringByColumn="True" AllowMultiRowSelection="false" AutoGenerateColumns="False" OnNeedDataSource="grd_Student_NeedDataSource">
                    <MasterTableView DataKeyNames="stcode" Font-Names="b nazanin" CssClass="table-hover">
                        <HeaderStyle BackColor="#0C84E4" ForeColor="#B2DBFB" Font-Bold="true" HorizontalAlign="Center" Font-Names="B Nazanin" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="name" HeaderText="نام و نام خانوادگی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="idd_meli" HeaderText="کدملی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn DataField="idd" HeaderText="شماره شناسنامه" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            </telerik:GridBoundColumn>--%>
                            <%--<telerik:GridBoundColumn DataField="namep" HeaderText="نام پدر" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="nameresh" HeaderText="رشته" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="magh" HeaderText="مقطع" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="name_dorpar" HeaderText="سیستم آموزشی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="payed" HeaderText="مانده نهایی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="name_vazkol" HeaderText="وضعیت کلی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" ItemStyle-Font-Names="b nazanin">
                                <ItemTemplate>
                                    <asp:Button ID="btn_Select" CssClass="btn btn-success btn-xs" Text="انتخاب" UniqueName="Select" runat="server" CommandName="Select" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>






</asp:Content>
