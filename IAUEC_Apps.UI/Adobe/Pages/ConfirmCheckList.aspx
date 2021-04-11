<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/PageAdobeMaster.Master" AutoEventWireup="true" CodeBehind="ConfirmCheckList.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.Pages.ConfirmCheckList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../adobe/css/Grid.Sunset.css" rel="stylesheet" />
      <link href="../../adobe/css/ListViewHierarchy.css" rel="stylesheet" />
     <style>
        .textAdel{
            text-align:center;
        }
    </style>
    <script>
        function walletPaymentCallback() {
            window.location.href = window.location.href;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    لیست جلسات انتخابی پرداخت نشده
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script language="javascript" type="text/javascript">
         function postRefId(refIdValue) {
             var form = document.createElement("form");
             form.setAttribute("method", "POST");
             form.setAttribute("action", "<%= PgwSite %>");
            form.setAttribute("target", "_self");
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("name", "RefId");
            hiddenField.setAttribute("value", refIdValue);
            form.appendChild(hiddenField);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
    </script>

    
 
         <div class="col-md-12" style="text-align: right; margin-top: 10px; margin-bottom: 10px;">
        <asp:Button ID="btn_Delete" runat="server"  CssClass="Red" Text="حذف" OnClick="Delbtn_Click" ></asp:Button></div>
        <telerik:RadListView ID="lst_SelectedClass" runat="server"
            ItemPlaceholderID="ProductTitlePlaceHolder">
            <LayoutTemplate>
                <asp:Panel ID="HierarchyPanel" runat="server" CssClass="wrapper textAdel">
                    <table id="products" class="products textAdel">
                        <thead>
                            <tr>
                                
                                <th style="width:80px" class="textAdel">
                                    حذف
                                </th>
                                <th style="width:40px" class="textAdel">
                                    ردیف
                                </th>
                                 <th class="textAdel">
                                    مشخصه کلاس
                                </th>
                                  <th class="textAdel">
                                    نام درس
                                </th>
                                  <th class="textAdel">
                                    ترم کلاس
                                </th>
                                 <th class="textAdel">
                                    نام فایل
                                </th>
                                 <th class="textAdel">
                                    مبلغ
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr id="ProductTitlePlaceHolder" runat="server">
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </LayoutTemplate>
            <ItemTemplate>

                                <tr>
                                    <td style="width:80px">
                                        <asp:CheckBox runat="server" ID="chk" />
                                    </td>
                                    <td  style="width:40px" class="textAdel" runat="server">
                                        <%#Eval("rowId") %>
                                    </td>
                                    <td class="textAdel" >
                                        <%#Eval("AssetClassCode") %>
                                    </td>
                                      <td class="textAdel" >
                                        <%#Eval("namedars") %>
                                    </td>
                                    <td>
                                      <asp:Label ID="Label2" runat="server" text='<%#Eval("Term") %>' CssClass="textAdel"></asp:Label>
                                   </td>
                                   
                                    <td>
                                        جلسه مورخ : <%#Eval("FileDate") %> <asp:Label ID="RequestID" Visible="false" CssClass="textAdel" runat="server" Text='<%#Eval("RequestID") %>'></asp:Label>
                                    </td>
                                    <td>
                                      <asp:Label ID="lbl_Fee" runat="server" CssClass="textAdel" text='<%#Eval("Fee") %>'></asp:Label>
                                   </td>
                                </tr>
                            </ItemTemplate>
            
        </telerik:RadListView>
  
 <div class="col-md-12" style="margin-top: 20px; margin-bottom: 20px; text-align: left;"><span style="text-align: center; color: #000000; padding: 1%; background-color:rgba(255, 61, 61,0.2); border:1px solid red; border-radius:2px; color: #000000;"> جمع کل:  <asp:Label ID="lbl_Sum" runat="server" Text="" Font-Bold="True"></asp:Label> ریال </span></div>
    <div class="col-md-12" style="text-align: left;">
    <asp:Button ID="btn_Payment" runat="server"  CssClass="Red" Text="پرداخت " OnClick="Payment_Click" ></asp:Button></div>

    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
</asp:Content>
