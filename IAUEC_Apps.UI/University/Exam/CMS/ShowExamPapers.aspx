<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master"
    AutoEventWireup="true" CodeBehind="ShowExamPapers.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ShowExamPapers" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }

        .w100 {
            width: 100%;
        }
       /*55555555555555555555*/
        .h500 {
            min-height: 500px;
        }

        .object-height {
            min-height: 750px;
        }

        .display_flex_center {
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .t_align_lign{
            text-align:left;
        }
    </style>
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />



    <script type="text/javascript">
        //$(function () {
        //    $("body").on("contextmenu", function (e) {
        //        alert();
        //        return false;
        //    });
        //});


        function openShowFileInPopup(path) {

            setTimeout(function () { window.radopen(path, "UserListDialog"); }, 1000);
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }

        }

        function ConfRemoveAttach() {
            if (!confirm('آیا می خواهید پیوست فایل را حذف نمایید؟')) { return false; }
        }

     <%--   function closeRadWindow1() {
            var window = $find('<%=RadWindow1.ClientID %>');
            window.close();
        }--%>


    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />

    <telerik:RadCodeBlock ID="blk" runat="server">
        <script type="text/javascript">
            function openModal() {
                $('#exampleModal').modal('show');
            }

            function refreshGrid(arg) {
                if (!arg) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                    $find("<%= grd_Class.ClientID %>").get_masterTableView().rebind();
                }
            }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadWindowManager ID="rwm" runat="server" Width="800px" Height="600px"></telerik:RadWindowManager>

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
                                            <%#Eval("Description") %>
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

    <div class="col-md-12" id="div_pnl" runat="server">
        <asp:Panel ID="Confirmpnl" runat="server" Visible="false" HorizontalAlign="Center">
            <div class="col-md-4"></div>
            <div class="col-md-3" style="border: 1px solid rgba(170, 102, 204,0.7); border-radius: 5px; padding-bottom: 8px; background-color: rgba(170, 102, 204,0.1)">
                <p style="color: #FF0000; font-weight: bold">آیا مطمئن هستید؟</p>
                <%--  <telerik:RadButton ID="conf" runat="server" Text="بله" Font-Names="Tahoma" OnClick="conf_Click" Skin="Hay"></telerik:RadButton>
                <telerik:RadButton ID="notConf" runat="server" Text=" خیر " Font-Names="tahoma" OnClick="notConf_Click" Skin="Windows7"></telerik:RadButton>--%>
                <telerik:RadButton ID="RadButton1" runat="server" Text="بله" Font-Names="Tahoma" OnClick="conf_Click"></telerik:RadButton>
                <telerik:RadButton ID="RadButton2" runat="server" Text=" خیر " Font-Names="tahoma" OnClick="notConf_Click"></telerik:RadButton>


            </div>
            <div class="col-md-4"></div>
        </asp:Panel>
    </div>
    <br />
    <div class="container-fluid" id="div_Main" runat="server" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); border-radius: 5px; margin-bottom: 1%; padding: 1%; color: #000">
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    دانشکده را انتخاب نمایید:
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddl_Danesh" runat="server" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btn_Save" runat="server" Text="نمایش" OnClick="btn_Save_Click" CssClass="btn btn-exam" />
                </div>

          
                <%-- <div class="col-md-2">
                          <asp:Button ID="btn_conf" runat="server" Text="نمایش تایید شده ها" OnClick="btn_collect_Click" CssClass="btn btn-primary" />
                         </div>--%>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-4" style="padding-top: 5px">
                    <div class="col-md-12">
                        <asp:RegularExpressionValidator ControlToValidate="txt_did" ValidationExpression="\d+" ValidationGroup="did" Display="Dynamic" ForeColor="Red" ErrorMessage="کد مشخصه فقط میتواند شامل اعداد باشد" runat="server"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-md-6">کد مشخصه را وارد نمایید:</div>
                    <div class="col-md-5">
                        <asp:TextBox ID="txt_did" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="col-md-8" style="border: 1px solid rgba(170, 102, 204,0.9); background-color: rgba(255, 255, 255,0.6); border-radius: 5px 5px 5px 5px; padding-top: 5px">
                    <div class="col-md-2">
                        <asp:Button ID="btn_review" ValidationGroup="did" runat="server" Text="بررسی مجدد" CssClass="btn btn-success" OnClick="btn_review_Click" />
                    </div>
                    <%--<div class="col-md-2"><asp:Button ID="btn_collect" runat="server" Text="تجمیع" CssClass="btn btn-primary" OnClick="btn_collect_Click"  /></div>--%>
                    <div class="col-md-1" style="border-right: 1px solid rgba(170, 102, 204,0.9); top: -5px; height: 40px; bottom: -5px"></div>
                    <div class="col-md-2">نحوه پاسخگویی:</div>
                    <div class="col-md-1" style="margin-left: 16px">
                        <asp:Button ID="btn_anssheet" ValidationGroup="did" runat="server" Text=" تشریحی" CssClass="btn btn-warning" OnClick="btn_anssheet_Click" />
                    </div>
                    <%--      <div class="col-md-1">
                        <asp:Button ID="btn_testsheet" runat="server" Text="تستی" ValidationGroup="did" CssClass="btn btn-info" OnClick="btn_testsheet_Click" />
                    </div>--%>
                    <div class="col-md-1">
                        <asp:Button ID="btn_ans" runat="server" Text="در برگه" ValidationGroup="did" CssClass="btn btn-danger" OnClick="btn_ans_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid HorizontalAlign="Center" ID="grd_Class" runat="server" AllowPaging="True" Visible="False" AutoGenerateColumns="False" PageSize="20" OnItemDataBound="grd_Class_ItemDataBound" OnItemCommand="grd_Class_ItemCommand" AllowFilteringByColumn="True" Skin="MyCustomSkin" EnableEmbeddedSkins="false"
                    OnNeedDataSource="grd_Class_NeedDataSource">
                    <MasterTableView DataKeyNames="coursecode">
                        <ItemStyle />
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                <ItemTemplate><%# Container.ItemIndex + 1 %> </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn AllowFiltering="true" DataField="QuestionId" HeaderText="شناسه سوال" Visible="false"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="true" DataField="coursecode" HeaderText="کد کلاس"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="false" DataField="namedars" HeaderText="نام درس"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="false" DataField="dateexam" HeaderText="تاریخ امتحان"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="true" DataField="osname" HeaderText="نام استاد"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="false" DataField="FirstUploadDate" HeaderText="تاریخ اولین بارگذاری"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="false" DataField="LastModifiedDate" HeaderText="آخرین تغییر"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="false" DataField="AnswerSheetType" HeaderText="نوع پاسخنامه"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="false" DataField="typeQuestion" HeaderText="نوع سوال"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="false" DataField="saatexam" HeaderText="ساعت امتحان" Visible="false"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="false" DataField="cityId" HeaderText="شهر امتحان" Visible="false"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn AllowFiltering="false" DataField="q2Sataus" HeaderText="وضعیت سوال دوم" Visible="false"></telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn AllowFiltering="false">
                                <HeaderTemplate>سوال</HeaderTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                <ItemTemplate>
                                    <%--   <asp:Button ID="btn_QueizPaper" runat="server" Enabled="true"  CommandName="Open" CommandArgument='<%#Eval("coursecode")  + ";" + Eval("QuestionId") + ";" + Eval("cityId") + ";" + Eval("q2Sataus") %>'   CssClass="btn btn-info"  Text="نمایش سوال"  />--%>
                                    <asp:Button ID="btnShowMergedQ_AttPdf" runat="server" Enabled="true" CommandName="OpenMergedPdfFile" CommandArgument='<%#Eval("coursecode")  + ";" + Eval("QuestionId") + ";" + Eval("cityId") + ";" + Eval("q2Status")+ ";" + Eval("Status") %>' CssClass="btn btn-info" Text="نمایش سوال و پیوست" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn AllowFiltering="false" FilterControlWidth="50px">
                                <HeaderTemplate>پیوست</HeaderTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:HiddenField ID="att" runat="server" Value='<%#Eval("AttachmentAddress") %>' />
                                    <asp:HiddenField ID="q1Status" runat="server" Value='<%#Eval("Status") %>' />
                                    <asp:HiddenField ID="q2Status" runat="server" Value='<%#Eval("q2Status") %>' />
                                    <%--   <asp:Button ID="btn_Attachment" CssClass="btn btn-warning" runat="server" Enabled="true" Text="نمایش پیوست" Visible="true" CommandName="OpenAttach" CommandArgument='<%#Eval("coursecode")  + ";" + Eval("QuestionId") %>' />   
                                    <asp:Button ID="btnRemoveAttachment" CssClass="btn btn-danger" runat="server" CommandArgument='<%#Eval("coursecode")  + ";" + Eval("QuestionId")%>' CommandName="RemoveAttach" Enabled="true" Text="حذف پیوست" Visible="true" OnClientClick="ConfRemoveAttach();" />--%>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn AllowFiltering="false">
                                <HeaderTemplate>وضعیت </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Status" runat="server" Visible="false"></asp:Label>
                                    <asp:Button ID="btn_Taeid" CssClass="btn btn-success" runat="server" CommandArgument='<%#Eval("coursecode")  + ";" + Eval("QuestionId") + ";" + Eval("cityId") + ";" + Eval("q2Status") + ";" + Eval("Status") %>' CommandName="Taeid" Text="تایید" Visible="false" />
                                    <asp:Button ID="btn_Raad" CssClass="btn btn-danger" runat="server" CommandArgument='<%#Eval("coursecode")  + ";" + Eval("QuestionId")+ ";" + Eval("cityId") + ";" + Eval("q2Status") + ";" + Eval("Status")  %>' CommandName="Raad" Text="رد" Visible="false" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn AllowFiltering="false">
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="btn_history" CssClass="btn btn-primary" runat="server" CommandArgument='<%#Eval("coursecode")  + ";" + Eval("QuestionId") + ";" + Eval("cityId") + ";" + Eval("q2Status")+ ";" + Eval("Status") %>' CommandName="view_history" Text="تاریخچه" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                        </Columns>
                        <EditFormSettings>
                            <EditColumn>
                            </EditColumn>
                        </EditFormSettings>

                    </MasterTableView>



                </telerik:RadGrid>
            </div>
        </div>
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    </telerik:RadAjaxManager>
    <asp:Label ID="lbl_Resault" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Status" runat="server" Visible="false"></asp:Label>

</asp:Content>
