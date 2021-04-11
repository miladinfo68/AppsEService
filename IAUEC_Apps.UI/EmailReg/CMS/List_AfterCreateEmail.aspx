<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/CMSEmailMaster.Master" AutoEventWireup="true" CodeBehind="List_AfterCreateEmail.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.CMS.List_AfterCreateEmail" %>

<%@ Register Src="../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../University/Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />
    <style>
    .RadGrid .rgFilterRow input {
            height:25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   
    <script type="text/javascript">
        $("[id*=chkHeader]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkRow]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>


    <script>
        function CallBackConfirm(arg) {
            if (arg)
                window.location.href = "List_AfterCreateEmail.aspx";
        }
    </script>
    <script type="text/javascript">
        function closeCustomConfirm() {
            $find("<%=rw_customConfirm.ClientID %>").close();
         }
    </script>


    <telerik:RadWindowManager runat="server" ID="RadWindowManager2">
        <Windows>
            <telerik:RadWindow ID="rw_customConfirm" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
                Width="300px" Height="200px" runat="server">
                <ContentTemplate>
                    <div class="rwDialogPopup radconfirm">
                        <div class="rwDialogText">
                            <asp:Literal ID="confirmMessage" Text="" runat="server" />
                        </div>
                        <div>
                            <telerik:RadButton runat="server" ID="rbConfirm_OK" Text="بله" OnClick="rbConfirm_OK_Click">
                            </telerik:RadButton>
                            <telerik:RadButton runat="server" ID="rbConfirm_Cancel" Text="خیر" OnClientClicked="closeCustomConfirm">
                            </telerik:RadButton>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <fieldset dir="rtl"><legend></legend></fieldset>
    
        <uc1:AccessControl ID="AccessControl1" runat="server" />
        <div class="container" dir="rtl">
           
            <div class="row">
        <asp:ImageButton ID="ExportToExcelImg" runat="server" ImageUrl="~/EmailReg/images/Excel_ExcelML.png"
            AlternateText="ExcelML" OnClick="ExportToExcelImg_Click" />
             </div>
                <div class="row">
                    <div class="col-md-12">
        <telerik:RadGrid Width="100%" ID="grd_ListAfterCreateEmail" FilterItemStyle-Height="23px" AllowFilteringByColumn="true" AllowPaging="true" PageSize="20" runat="server" AutoGenerateColumns="False" AllowMultiRowSelection="true" EnableEmbeddedSkins="False" OnNeedDataSource="RadGrid1_NeedDataSource">
            <ClientSettings Selecting-AllowRowSelect="true">
            </ClientSettings>
            <MasterTableView DataKeyNames="Id">

                <HeaderStyle  CssClass="bg-orange" Font-Names="tahoma"  HorizontalAlign="Center" />
               
              
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkHeader" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>

                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                        <ItemTemplate>
                            <%# (Container.ItemIndex+1).ToString() %>
                        </ItemTemplate>
                       
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn AllowFiltering="true" DataField="name" HeaderText="نام و نام خانوادگی" FilterControlWidth="100px" FilterImageUrl="../images/filter2.png" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="true" DataField="nameen" FilterControlWidth="100px" FilterImageUrl="../images/filter2.png" HeaderText="نام و نام خانوادگی انگلیسی">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="false" DataField="Email_Address" HeaderText="پست الکترونیکی درخواستی" HeaderStyle-Width="120px" ItemStyle-Width="120px">

                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="false" DataField="OldEmail" HeaderText="پست الکترونیکی قبلی">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="false" DataField="magh" HeaderText="مقطع">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="false" DataField="nameresh" HeaderText="رشته">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="false" DataField="sal_vorod" HeaderText="سال ورود">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="false" DataField="PersianDate" HeaderText="تاریخ درخواست">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn AllowFiltering="false" DataField="id" Visible="false"></telerik:GridBoundColumn>

                </Columns>
            </MasterTableView>

        </telerik:RadGrid>
                        
        <asp:Label ID="lbl_Resault" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Lbl_Status" runat="server" Visible="false"></asp:Label>
                        </div>
            </div>
          
   
   
        <div class="row">
              <div class="col-md-6">
                </div>
              
            <div class="col-md-6">
                 <asp:Button ID="btn_Taeid" runat="server" Width="60px" CssClass="btn-warning" Text="تایید" OnClick="Button1_Click" />
                </div>
              </div>
               
</div>
    
</asp:Content>
