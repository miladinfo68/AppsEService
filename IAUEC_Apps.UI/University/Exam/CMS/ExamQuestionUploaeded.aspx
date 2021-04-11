<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="ExamQuestionUploaeded.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ExamQuestionUploaeded" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }
    </style>
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <script type="text/javascript">
        function openModal() {
            $('#exampleModal').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div dir="rtl">

        <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
        <table>
            <tr>
                <td style="font-size: medium; color: #000000; font-weight: bold;">دانشکده را انتخاب نمایید:
                </td>
                <td>

                    <asp:DropDownList ID="ddl_Danesh" runat="server" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>

                </td>

                <td style="color: #000000; font-size: medium; font-weight: bold;">وضعیت:</td>
                <td>
                    <asp:DropDownList ID="ddl_status" runat="server" ForeColor="Black" CssClass="form-control input-sm">


                        <asp:ListItem Value="0">همه موارد</asp:ListItem>
                        <asp:ListItem Value="1">به اداره امتحانات ارسال نشده است</asp:ListItem>
                        <asp:ListItem Value="2">سوالات ارسال شده</asp:ListItem>
                        <asp:ListItem Value="3">سوالات تایید شده</asp:ListItem>
                        <asp:ListItem Value="4">سوالات رد شده</asp:ListItem>
                        <asp:ListItem Value="5">تجمیع اوراق</asp:ListItem>
                        <asp:ListItem Value="6">تحویل اوراق</asp:ListItem>

                    </asp:DropDownList>
                </td>
                <td style="color: #000000; font-size: medium; font-weight: bold;">ترم:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlTerm" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btn_Save" runat="server" Text="نمایش" OnClick="btn_Save_Click" CssClass="btn btn-exam" />
                    <asp:Button ID="btn_excel" runat="server" Text="تبدیل به فایل اکسل" CssClass="btn btn-success" OnClick="btn_excel_Click" />
                </td>
            </tr>

        </table>
        <br />
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="exampleModalLabel">مشاهده وضعیت های قبلی سوالات</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid" dir="rtl">
                            <div class="row" style="border: 1px solid rgba(170, 102, 204,0.9); background-color: rgba(170, 102, 204,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        تاریخ
                                    </div>
                                    <div class="col-md-2">
                                        ساعت
                                    </div>
                                    <div class="col-md-3">
                                        وضعیت
                                    </div>
                                    <div class="col-md-5">
                                        توضیحات
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">

                                <asp:ListView ID="lst_history" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-12" style="border-style: none none solid none; border-width: 0px 0px 1px 1px; border-color: rgba(170, 102, 204,0.7);">
                                            <div class="col-md-2">
                                                <%#Eval("LogDate") %>
                                            </div>
                                            <div class="col-md-2">
                                                <%#Eval("LogTime") %>
                                            </div>
                                            <div class="col-md-3">
                                                <%#Eval("EventName") %>
                                            </div>
                                            <div class="col-md-5">
                                                <%--  <div style="/*text-wrap:inherit*/"> --%>
                                                <%#Eval("Description") %>

                                                <%--</div>--%>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

                    </div>
                </div>
            </div>
        </div>
        <telerik:RadGrid ID="grd_ExamQuestion" AllowFilteringByColumn="True" AllowPaging="True" PageSize="20" runat="server" AutoGenerateColumns="False" OnNeedDataSource="grd_ExamQuestion_NeedDataSource" Skin="MyCustomSkin" EnableEmbeddedSkins="false" EnableViewState="false" OnExcelMLWorkBookCreated="grd_ExamQuestion_ExcelMLWorkBookCreated" OnItemCommand="grd_ExamQuestion_ItemCommand" OnItemDataBound="grd_ExamQuestion_ItemDataBound">
            <MasterTableView>
                <EditFormSettings>
                    <EditColumn CancelImageUrl="Cancel.gif" InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif">
                    </EditColumn>
                </EditFormSettings>
                <HeaderStyle HorizontalAlign="Center" />
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="idostad" HeaderText="کد استاد" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="osname" HeaderText="نام استاد" AllowFiltering="true">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="mobile" HeaderText="موبایل" AllowFiltering="true">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="did" HeaderText="مشخصه کلاس" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="dateexam" HeaderText="تاریخ امتحان" AllowFiltering="true">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="saatexam" HeaderText="ساعت امتحان" AllowFiltering="true">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="namedars" HeaderText="نام درس" AllowFiltering="false">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="FirstUploadDate" HeaderText=" تاریخ اولین آپلود" AllowFiltering="true">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="ReciveDateExamSheet" HeaderText=" تاریخ دریافت اوراق" AllowFiltering="true">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="LastModifiedDate" HeaderText="آخرین تاریخ ارسال" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="QStatus" HeaderText="وضعیت سوالات" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false">


                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:HiddenField ID="status" runat="server" Value='<%#Eval("status") %>' />
                            <asp:Button ID="btn_history" CssClass="btn btn-primary" runat="server" CommandArgument='<%#Eval("QuestionId") %>' CommandName="view_history" Text="تاریخچه" />
                            <%--  <asp:Button ID="btn_sms" CssClass="btn btn-warning"  runat="server" CommandArgument='<%#Eval("did") %>' CommandName="SMS" Text="ارسال پیامک تجمیع" />--%>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <FilterItemStyle ForeColor="Black" />
            <FilterMenu EnableEmbeddedSkins="False">
            </FilterMenu>
            <HeaderContextMenu EnableEmbeddedSkins="False">
            </HeaderContextMenu>
        </telerik:RadGrid>
        <asp:Label ID="lbl_Resault" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_Status" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>
