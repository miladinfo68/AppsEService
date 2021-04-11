<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="CollectingPapers.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.CollectingPapers" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
     <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
     <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
     <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <uc1:AccessControl runat="server" ID="AccessControl" />
        <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
   <div
        class="container-fluid"  dir="rtl">
       <div class="row">
        <div class="col-md-12">


             <div class="col-md-2">
                              روز آزمون:</div>
            <div class="col-md-2">
                           <asp:DropDownList ID="ddl_ExamDate" runat="server" OnSelectedIndexChanged="ddl_ExamDate_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                         </div>
                             
            <div class="col-md-2">
                            ساعت آزمون:
                </div>
            <div class="col-md-2">
                           <asp:DropDownList ID="ddl_Saate" runat="server"></asp:DropDownList>
                          </div>  
            <div class="col-md-2">
                   <asp:Button ID="btn_Show" CssClass="btn-success" OnClick="btn_Show_Click" runat="server" Text="نمایش" Width="59px" />

            </div>

                         
                        
                   </div>
        </div>

   </div>
      <div dir="rtl">
          <br />
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="center"  BorderStyle="Groove">
        
            <telerik:RadGrid ID="grd_CourseList" runat="server" AutoGenerateColumns="false" OnItemCommand="grd_CourseList_ItemCommand" OnItemDataBound="grd_CourseList_ItemDataBound"  EnableEmbeddedSkins="False"  Skin="MyCustomSkin">
                <MasterTableView>
                    <HeaderStyle  HorizontalAlign="Center" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="did" HeaderText="مشخصه کلاس">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="namedars" HeaderText="نام درس">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="osname" HeaderText="نام خانوادگی و نام استاد">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="dateexam" HeaderText="تاریخ امتحان">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="saatexam" HeaderText="ساعت امتحان">
                        </telerik:GridBoundColumn>
                       
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <asp:Button ID="btn_collectPapers" runat="server" CommandName="collecting" Text=" تجمیع اوراق و ارسال پیامک"   CommandArgument='<%#Eval("did") %>' CssClass="btn btn-info" />
                                <asp:Button ID="btn_deliverPapers" runat="server" CommandName="delivery" Text=" تحویل اوراق به اساتید"    CommandArgument='<%#Eval("did") %>' CssClass="btn btn-warning"/>
                                 <asp:HiddenField ID="hdn_status" runat="server" Value='<%#Eval("status") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                     

                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

        </asp:Panel>
    </div>
    <div>

<%--        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />--%>
    </div>
</asp:Content>