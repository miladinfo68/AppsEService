<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/PageAdobeMaster.Master" AutoEventWireup="true" CodeBehind="PaymentList.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.Pages.PaymentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    لیست پرداخت ها
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grd_PaymentList" runat="server" AutoGenerateColumns="False" Skin="Sunset" CellSpacing="0" GridLines="None">
        <MasterTableView DataKeyNames="stcode">
            <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center" />
            <ItemStyle  Font-Names="tahoma" Font-Size="Small" />
            <AlternatingItemStyle  Font-Names="tahoma" Font-Size="Small" />
            <Columns>
                 
                 <telerik:GridBoundColumn DataField="OrderId" HeaderText="شماره سفارش" AllowFiltering="False">

                 </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="Amount" HeaderText="مبلغ(ریال)" DataFormatString="{0:N0}" AllowFiltering="False"   >

                 </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="PersianDate" HeaderText="تاریخ" AllowFiltering="False">

                 </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TraceNumber" HeaderText="شماره پیگیری" AllowFiltering="False">

                 </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="AppStatus" HeaderText="وضعیت پرداخت" AllowFiltering="False">

                     <ItemStyle ForeColor="#0099ff"/>
                 </telerik:GridBoundColumn>  
                    
                    
                           
             </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
