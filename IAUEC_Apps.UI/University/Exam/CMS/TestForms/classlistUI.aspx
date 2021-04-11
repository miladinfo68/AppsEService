<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="classlistUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.TestForms.classlistUI" %>

<%@ Register Src="../../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
      <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }
    </style>
     <link href="../../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <script>
        function ReloadGrid() {
            <%--//__doPostBack('LoadGridData', '');--%>
            document.getElementById('<%= btn_sabt.ClientID %>').click();
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
      <div class="container-fluid" id="div1" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7);border-radius:5px; margin-bottom:1%;padding:1%;color:#000">
           <div class="row">
                <div class="col-md-12">
                    <div class="col-md-1">روز:</div>
                    <div class="col-md-2"><asp:DropDownList ID="ddl_day" runat="server" OnSelectedIndexChanged="ddl_day_SelectedIndexChanged"  AutoPostBack="True" ForeColor="Black" CssClass="form-control input-sm">
                        </asp:DropDownList></div>
                    <div class="col-md-1">ساعت:</div>
                    <div class="col-md-2"><asp:DropDownList ID="ddl_saat" runat="server" AutoPostBack="True" ForeColor="Black" CssClass="form-control input-sm">
                        </asp:DropDownList></div>
                    <div class="col-md-1">شهر:</div>
                    <div class="col-md-2"><asp:DropDownList ID="ddl_shahr" runat="server" AutoPostBack="True" DataTextField="City_Name" DataValueField="ID" ForeColor="Black" CssClass="form-control input-sm">
                            <%--<asp:ListItem Value="1">تهران</asp:ListItem>--%>
                        </asp:DropDownList></div>
                     <div class="col-md-1"><asp:Button ID="btn_sabt" runat="server" OnClick="Button1_Click" Text="نمایش"  CssClass="btn btn-exam" /></div>
                     <div class="col-md-1"><%--<asp:Button ID="btn_takhsis" runat="server" OnClick="Button2_Click" Text="تخصیص صندلی"  CssClass="btn btn-info" />--%></div>
                    <asp:HiddenField runat="server" ID="hdnSearchedCity" />
                    </div>
               </div>
          <div class="row">
              <div class="col-md-12">
                  <telerik:RadGrid ID="grd_Class" runat="server" AllowPaging="true" PageSize="50" OnItemDataBound="grd_Class_ItemDataBound" OnItemCommand="grd_Class_ItemCommand" 
                      AllowFilteringByColumn="True" AutoGenerateColumns="false" EnableEmbeddedSkins="False" Skin="MyCustomSkin">
                    <MasterTableView DataKeyNames="coursecode">
                        <ItemStyle />
                        <HeaderStyle HorizontalAlign="Center"/>
                        <AlternatingItemStyle />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="coursecode" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="zarfiat" HeaderText="ظرفیت" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="namedars" HeaderText="نام درس" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="osname" HeaderText="نام استاد" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false">

                                <HeaderTemplate>
                                    تعیین محل برگزاری امتحان
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="btn_Taeid" runat="server"  Enabled="true" CommandArgument='<%#Eval("coursecode")+","+ Eval("Zarfiat") %>' CommandName="bookinglist" CssClass="btn btn-success"  Text="تعیین" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>


                            <telerik:GridTemplateColumn AllowFiltering="false">

                                <HeaderTemplate>
                                    تخصیص صندلی
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="btn_Chair" runat="server"   CommandArgument='<%#Eval("coursecode")+","+ Eval("Zarfiat") + "," + Eval("Name_City") %>' CommandName="SeatSpecify" CssClass="btn btn-info"  Text="تخصیص صندلی" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
              </div>
          </div>
          </div>
    
<uc1:AccessControl ID="AccessControl1" runat="server" />

</asp:Content>

