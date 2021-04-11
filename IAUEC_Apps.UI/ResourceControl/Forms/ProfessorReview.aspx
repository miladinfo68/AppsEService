<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/ResourceControlUsers.Master" CodeBehind="ProfessorReview.aspx.cs" Inherits="ResourceControl.PL.Forms.ProfessorReview" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <h3>پنل اساتید</h3>
    <script type="text/javascript">
        function Confirm() {
            var confirmValue = document.createElement("INPUT");
            confirmValue.type = "hidden";
            confirmValue.name = "confirm_value";
            if (confirm("آیا می خواهید درخواست حذف شود ؟")) {
                confirmValue.value = "بله";
            } else {
                confirmValue.value = "خیر";
            }
            document.forms[0].appendChild(confirmValue);
        }

        function showWindow() {
            var oWindowCust = $find('<%= rdwDateTime.ClientID %>');
            oWindowCust.show();
            Sys.Application.remove_load(showWindow);
        }
    </script>

    <style>
        .marginItem {
            margin: 10px;
        }

        .paddingItem {
            padding: 20px;
        }

        .centerItem {
            text-align: center !important;
        }



        .tbl {
            max-width: 100%;
        }

            .tbl td {
                text-align: center !important;
            }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            vertical-align: middle;
        }
    </style>

    <script type="text/javascript">
        function openModal() {
            $('#exampleModal').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <div class="container rtl">

        <table id="tblProfessorReview" class="tbl table table-bordered table-condensed rtl">
            <tr class="bg-primary">
                <td>
                    <h4>لیست درخواستهای شما از منابع دانشگاه</h4>
                </td>
                <td>
                    <span>وضعیت درخواست :</span>
                    <asp:DropDownList ID="drpRequestStatus" runat="server" CssClass=" dropdown bg-white text-color-black" AutoPostBack="True" OnSelectedIndexChanged="drpRequestStatus_SelectedIndexChanged">
                        <asp:ListItem Text="درخواستهای منتظر ارجاع" Value="0"></asp:ListItem>
                        <asp:ListItem Text="درخواستهای ارجاع داده شده" Value="1" />
                        <asp:ListItem Text="درخواستهای تایید شده" Value="2" />
                        <asp:ListItem Text="درخواستهای رد شده" Value="3" />
                        <asp:ListItem Text="درخواستهای اطلاع رسانی شده" Value="4" />
                        <asp:ListItem Text="تمامی درخواستها" Value="5" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" dir="rtl">
                    <div class="table-responsive" style="text-align: center;">
                        <asp:GridView ID="grdProfessorReview" runat="server" CssClass="table table-bordered table-condensed table-striped centerItem" ShowHeaderWhenEmpty="true" EmptyDataText="درخواستی پیدا نشد" AutoGenerateColumns="false" OnRowDataBound="grdProfessorReview_RowDataBound" OnRowCommand="grdProfessorReview_RowCommand" HorizontalAlign="Center">
                            <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle CssClass="bg-green" />
                            <Columns>
                                <asp:TemplateField HeaderText="ردیف">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="شماره درخواست" DataField="ID" />
                                <asp:BoundField HeaderText="مشخصه کلاس" DataField="CourseDID" />
                                <asp:BoundField HeaderText="نام درس" DataField="courseName" HeaderStyle-CssClass="text-center">
                                    <HeaderStyle CssClass="text-center"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="وضعیت">
                                    <ItemTemplate>
                                        <asp:Image ID="imgStatus" runat="server" Width="20px" ImageUrl='<%# GetImage((int)Eval("status")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="دسته بندی" DataField="catID" HeaderStyle-CssClass="text-center">
                                    <HeaderStyle CssClass="text-center"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="تاریخ ثبت" DataField="issue_time" />
                                <asp:TemplateField HeaderText="زمان(های) درخواستی">
                                    <ItemTemplate>
                                        <asp:Button ID="btnShowDateTime" Text="نمایش" CssClass="btn btn-success" CommandName="showTime" CommandArgument='<%# Eval("ID") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="پاسخ" DataField="answernote" />
                                <asp:BoundField HeaderText="زمان پاسخ" DataField="answer_time" />
                                <asp:TemplateField HeaderText="عملیات" Visible="false">
                                    <ItemTemplate>

                                        <asp:Button ID="btnCancelRequest" Text="حذف" runat="server" CssClass="btn btn-danger" OnClientClick="Confirm()" CommandName="cancel1" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="تاریخچه" Visible="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnHistory" AlternateText="تاریخچه" Visible="true" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' ImageUrl="~/University/Theme/images/appHistory.png" Width="40" Height="40" CssClass="center-margin" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td>ثبت درخواست جدید</td>
                <td>
                    <asp:Button ID="txtAddReques" Text="ثبت درخواست جدید" runat="server" CssClass="btn btn-primary" OnClick="txtAddReques_Click" />
                </td>
            </tr>
        </table>
    </div>

    <telerik:RadWindowManager ID="RadWindowManager1" EnableViewState="false" runat="server">
        <Windows>
            <telerik:RadWindow ID="rdwDateTime" EnableViewState="false" runat="server" Width="700" VisibleOnPageLoad="false" AutoSize="true" AutoSizeBehaviors="Height">
                <ContentTemplate>
                    <div class="container" style="padding: 10px" dir="rtl">
                        <div class="heading2 bg-green" style="padding: 5px">
                            <h3>جزئیات درخواست :</h3>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-4" style="border: 1px solid #A9A9A9">
                                <table>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                                <strong>شماره درخواست : </strong>
                                <asp:Label ID="lblRequestId" CssClass="text-primary" runat="server" />
                                <br />
                                <strong>نام درس : </strong>
                                <asp:Label ID="lblDarsName" CssClass="text-primary" runat="server" />
                                <br />
                                <strong>وضعیت : </strong>
                                <asp:Image ID="imgStatus2" runat="server" Width="20px" />

                            </div>
                            <div class="col-md-8">
                                <asp:GridView ID="grdDateTime" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false">
                                    <HeaderStyle CssClass="bg-blue-sky" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ردیف">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="تاریخ " DataField="Date" />
                                        <asp:TemplateField HeaderText="ساعت شروع">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("StartTime"))).ToString("hh\\:mm") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ساعت پایان">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("EndTime"))).ToString("hh\\:mm") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="کلاس تخصیص یافته" DataField="ClassName" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>


    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" style="background-color: aqua;">
        <div class="modal-dialog" role="document" style="width: 70%">
            <div class="modal-content bg-info border-dark">

                <div class="modal-header" dir="rtl">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="font-size: -webkit-xxx-large; float: left; float: left; margin-left: 1%;">
                        <span aria-hidden="true" style="margin: auto; line-height: initial;">&times;
                        </span>
                    </button>
                    <div class="modal-header bg-orange" dir="rtl">
                        <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه درخواست</h4>
                    </div>
                </div>
                <div class="modal-body">
                    <table class="table table-responsive table-bordered table-head table-hover center-margin" dir="rtl" style="border-bottom-color: black">
                        <tr class="bg-blue-sky">
                            <th>نام کاربر</th>
                            <th>تاریخ</th>
                            <th>ساعت</th>
                            <th>وضعیت</th>
                            <th>توضیحات</th>
                        </tr>

                        <asp:ListView ID="lst_history" runat="server">
                            <ItemTemplate>
                                <tr class="bg-blue" style="text-align: center;">
                                    <td>
                                        <%#Eval("Name") %>
                                    </td>
                                    <td>
                                        <%#Eval("LogDate") %>
                                    </td>
                                    <td>
                                        <%#Eval("LogTime") %>
                                    </td>
                                    <td>
                                        <%#Eval("EventName") %>
                                    </td>
                                    <td>
                                        <%#Eval("Description") %>
                                    </td>

                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>

                </div>

            </div>

        </div>
    </div>

</asp:Content>
