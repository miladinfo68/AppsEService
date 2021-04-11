<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/PageAdobeMaster.Master" AutoEventWireup="true" CodeBehind="ClassList.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.Pages.ClassList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../css/ListViewHierarchy.css" rel="stylesheet" />
    <script src="../js/scripts.js"></script>
    <script>
        function CallBackConfirm2(arg) {
            if (arg)
                window.location.href = "../../CommonUI/IntroPage.aspx"
        }
    </script>
    <%--<style>
        .RadWindow_rtl{direction:rtl;}
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    درخواست فایل
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwm_Validations" runat="server" Height="500px" Modal="True" Width="650px">
    </telerik:RadWindowManager>
    <asp:Panel ID="p" runat="server" DefaultButton="btn_Select">

        <%--<telerik:RadListView ID="lstclass" runat="server">
        <ItemTemplate>
            <div><span>نام کلاس : <%#Eval("namedars") %></span><asp:label ID="ClassCode" runat="server" Text='<%#Eval("ClassCode") %>' Visible="false"></asp:label></div>
            <telerik:RadListView runat="server" ID="lstDetail">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chk" /><span>جلسه مورخ : <%#Eval("FileDate") %></span><asp:Label ID="AssetID" Visible="false" runat="server" Text='<%#Eval("AssetID") %>'></asp:Label><a href='ViewFiles.aspx?ClassCode=<%#Eval("ClassCode") %>'>مشاهده فایل ها</a>

                </ItemTemplate>
            </telerik:RadListView>
        </ItemTemplate>

    </telerik:RadListView>--%>
        <p style="font-weight: bold; font-size: small; color: black">*برای دریافت فایل جلسه مورد نظر ،جلسه را انتخاب و سپس بر روی دکمه پرداخت کلیک نمایید.</p>
        <p style="color: black; font-size: small; padding-right: 5px;"><i class="fa fa-circle" style="color: red; margin-right: 5px;"></i>در حال حاضر فایل دانلود مورد نظر موجود نمی باشد.</p>
        <p style="color: black; font-size: small; padding-right: 5px;"><i class="fa fa-circle" style="color: green; margin-right: 5px;"></i>درخواست دانلود فایل مورد نظر امکان پذیر می باشد. </p>
        <p style="color: black; font-size: small; padding-right: 5px;"><i class="fa fa-circle" style="color: #FFDE00; margin-right: 5px;"></i>درخواست فایل مورد نظر قبلا ثبت شده است.</p>
        <asp:Label ID="lblstcode" runat="server" Visible="false"></asp:Label>
        <telerik:RadListView ID="RadListView2" runat="server" ItemPlaceholderID="ProductTitlePlaceHolder">
            <LayoutTemplate>
                <asp:Panel ID="HierarchyPanel" runat="server" CssClass="wrapper">
                    <table id="products" class="products">
                        <thead>
                            <tr>
                                <th class="expand"></th>
                                <th>لیست کلاس های ترم جاری
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
                    <td class="expand">
                        <img src="../images/SinglePlus.gif" alt="toggle" onclick="toggleOrderDetails(this)" />
                    </td>
                    <td colspan="4">مشخصه: <%#Eval("ClassCode") %>---<%# Eval("CourseName") %>---<span style="font-weight: bold; padding-right: 5px; padding-left: 5px;">نام استاد:</span>
                        <%# Eval("ProfName") %>---<span style="font-weight: bold; padding-right: 5px; padding-left: 5px;"> روز برگزاری کلاس:</span><%#Eval("ClassDateTime") %>
                        <span style="font-weight: bold; padding-right: 5px; padding-left: 5px;">زمان برگزاری کلاس:</span><%#Eval("ClassStartTime") %>
                        <asp:Label ID="ClassCode" runat="server" Text='<%#Eval("ClassCode") %>' Visible="false"></asp:Label>
                        <%--                        <asp:Button ID="btnOtherClasess" runat="server" Text="کلاسهای مشابه" CssClass="btn btn-primary" OnClick="btnOtherClasess_Click" CommandArgument=" <%# Eval("ClassCode") %>" />--%>
                        <asp:LinkButton ID="btnOtherClasess" runat="server" Visible='<%#Eval("MergeCode")!=null?false:true %>' CustomParameter='<%#Eval("ClassCode")%>' CssClass="btn btn-primary" OnClick="btnOtherClasess_Click">کلاسهای مشابه</asp:LinkButton>
                        <%--  <asp:HiddenField ID="hiddenArg" runat="server" Value=" <%# Eval("ClassCode") %>" />--%>
                    </td>
                </tr>
                <tr class="orders" style="display: none;">
                    <td class="expand"></td>
                    <td colspan="4">
                        <telerik:RadListView ID="RadListView3" runat="server" OnItemDataBound="RadListView3_ItemDataBound" ItemPlaceholderID="OrderDetailsPlaceHolder">
                            <LayoutTemplate>
                                <table>
                                    <asp:PlaceHolder ID="OrderDetailsPlaceHolder" runat="server"></asp:PlaceHolder>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Term" runat="server" Text='<%#Eval("Term") %>' Visible="false"></asp:Label><asp:Literal ID="ltr" runat="server"></asp:Literal>
                                        <asp:CheckBox runat="server" ID="chk_MeetingDate" />
                                        جلسه مورخ :<asp:Label ID="lbl_FDate" runat="server" Text=' <%#Eval("FileDate") %>'></asp:Label>
                                        <asp:Label ID="lbl_AssetID" Visible="false" runat="server" Text='<%#Eval("AssetID") %>'></asp:Label>
                                        <asp:Label ID="lbl_ClassCode" runat="server" Visible="false" Text='<%#Eval("Class_Code") %>'></asp:Label>
                                        <a href='ViewFiles.aspx?ClassCode=<%#Eval("Class_Code") %>&Date=<%#Eval("FileDate") %>&Ast=<%#Eval("AssetID") %>&t=<%#Eval("Term") %>'>مشاهده فایل ها</a>
                                        <asp:Label ID="lbl_Message" runat="server" Visible="false" ForeColor="Red" Font-Names="tahoma" Font-Size="Small" Text="فایل موجود نمی باشد"></asp:Label>
                                    </td>

                                </tr>
                            </ItemTemplate>
                        </telerik:RadListView>
                    </td>
                </tr>
            </ItemTemplate>
        </telerik:RadListView>



        <telerik:RadWindow ID="RadWindow1" AutoSize="false" Height="600" runat="server" Width="1050" Modal="True" >
            <ContentTemplate>


                <telerik:RadListView ID="RadListView1" runat="server" ItemPlaceholderID="ProductTitlePlaceHolder2">
                    <LayoutTemplate>
                        <asp:Panel ID="HierarchyPanel1" runat="server" CssClass="wrapper">
                            <table id="tblproducts" class="products">
                                <thead>
                                    <tr>
                                        <th class="expand"></th>
                                        <th>لیست کلاس های ترم جاری
                                        </th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr id="ProductTitlePlaceHolder2" runat="server">
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td colspan="4">
                                مشخصه: <%#Eval("MergeCode")!=null?Eval("MergeCode"):Eval("ClassCode") %>---<%# Eval("CourseName") %>---<span style="font-weight: bold; padding-right: 5px; padding-left: 5px;">نام استاد:</span>
                                <%# Eval("ProfName") %>---<span style="font-weight: bold; padding-right: 5px; padding-left: 5px;"> روز برگزاری کلاس:</span><%#Eval("ClassDateTime") %>
                                <span style="font-weight: bold; padding-right: 5px; padding-left: 5px;">زمان برگزاری کلاس:</span><%#Eval("ClassStartTime") %>
                                <asp:Label ID="ClassCode1" runat="server" Text='<%#Eval("ClassCode") %>' Visible="false"></asp:Label>
                                 <img class="expand" src="../images/SinglePlus.gif" alt="toggle" onclick="toggleOrderDetails(this)" style="display: initial;"/>
                            </td>

                        </tr>
                        <tr class="orders" style="display: none;">
                            <td class="expand"></td>
                            <td colspan="4">
                                <telerik:RadListView ID="radListView4" runat="server" OnItemDataBound="radListView4_ItemDataBound" ItemPlaceholderID="OrderDetailsPlaceHolder2">
                                    <LayoutTemplate>
                                        <table>
                                            <asp:PlaceHolder ID="OrderDetailsPlaceHolder2" runat="server"></asp:PlaceHolder>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Term1" runat="server" Text='<%#Eval("Term") %>' Visible="false"></asp:Label>

                                                جلسه مورخ :<asp:Label ID="lbl_FDate1" runat="server" Text=' <%#Eval("FileDate") %>'></asp:Label>
                                                <asp:Label ID="lbl_AssetID1" Visible="false" runat="server" Text='<%#Eval("AssetID") %>'></asp:Label>
                                                <asp:Label ID="lbl_ClassCode1" runat="server" Visible="false" Text='<%#Eval("Class_Code") %>'></asp:Label>
                                                <a href='ViewFiles.aspx?ClassCode=<%#Eval("Class_Code") %>&Date=<%#Eval("FileDate") %>&Ast=<%#Eval("AssetID") %>&t=<%#Eval("Term") %>'>مشاهده فایل ها</a>
                                                <asp:Label ID="lbl_Message1" runat="server" Visible="false" ForeColor="Red" Font-Names="tahoma" Font-Size="Small" Text="فایل موجود نمی باشد"></asp:Label>
                                                <asp:CheckBox runat="server" ID="chk_MeetingDate1" />
                                                <asp:Literal ID="ltr1" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </telerik:RadListView>
                            </td>
                        </tr>



                    </ItemTemplate>
                </telerik:RadListView>
                
                 <asp:Button ID="btn_select1" runat="server" CssClass="Redbtn" Text="پرداخت"  OnClick="btn_select1_Click" />
            </ContentTemplate>
        </telerik:RadWindow>



        <script>

            function closeRadWindow() {
                var window = $find('<%=RadWindow1.ClientID %>');
                window.close();
                refresgGrid();
            }

        </script>



        <asp:Button ID="btn_Select" runat="server" CssClass="Redbtn" Text="پرداخت" OnClick="btnSelect_Click" Visible="false" />


        <iframe src="http://www.iauec.ac.ir" width="0" height="0"></iframe>
    </asp:Panel>
</asp:Content>
