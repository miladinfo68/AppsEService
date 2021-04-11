<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="PajooheshReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.PajooheshReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" dir="rtl">
        <asp:Label ID="lblStCode" Text="شماره دانشجویی :" runat="server"></asp:Label>
        <asp:TextBox ID="txtStCode" runat="server"></asp:TextBox>
        <asp:Button ID="btnSerach" runat="server" Text="جستجو" CssClass="btn btn-group" OnClick="btnSerach_Click" />
     <%--  &nbsp&nbsp&nbsp
          <asp:Label ID="lblnullSt" runat="server" Text="اطلاعات دفاع برای این دانشجو ثبت نشده است ." ForeColor="Red" Visible="false"></asp:Label>--%>
        
         <asp:GridView ID="grdStudent" runat="server"
                  AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-stripted">
            <HeaderStyle CssClass="bg-primary" />
            <EditRowStyle CssClass="GridViewEditRow" />
            <Columns>
                <asp:TemplateField HeaderText="ردیف">
                    <ItemTemplate>
                        <asp:Label ID="lblRadif" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="نام دانشجو ">
                    <ItemTemplate>
                        <asp:Label ID="lblName" Text='<%# Eval("name").ToString() %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="نام خانوادگی دانشجو ">
                    <ItemTemplate>
                        <asp:Label ID="lblFamily" Text='<%# Eval("family").ToString() %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="شماره دانشجویی">
                    <ItemTemplate>
                        <asp:Label ID="lblStCode" Text='<%# Eval("StCode").ToString() %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="رشته">
                    <ItemTemplate>
                        <asp:Label ID="lblResh" Text='<%# Eval("nameresh").ToString() %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="تاریخ دفاع">
                    <ItemTemplate>
                        <asp:Label ID="lblDefDate" Text='<%# Eval("Def_Date").ToString() %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
         <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
     

    </div>
</asp:Content>
