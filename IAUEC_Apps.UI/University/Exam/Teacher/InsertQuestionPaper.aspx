<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/TeacherExamMaster.Master"
    AutoEventWireup="true" CodeBehind="InsertQuestionPaper.aspx.cs"
    Inherits="IAUEC_Apps.UI.University.Exam.Teacher.InsertQuestionPaper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link rel="stylesheet" href="animate.min.css" type="text/css" />
    <style>
        .btnPostToLMS {
            font-size: 18px;
            font-weight: 400;
            padding: 20px;
            direction: rtl;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock ID="blk" runat="server">
        <script type="text/javascript">

            function refreshGrid(arg) {
                if (!arg) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                    $find("<%= grd_Dars.ClientID %>").get_masterTableView().rebind();
                }
            }


        </script>
    </telerik:RadCodeBlock>
            <%--<asp:Button runat="server" ID="btnPostToLMS" CssClass="btn btn-success col-sm-12 btnPostToLMS" OnClick="btnPostToLMS_Click" Text="جهت ورود به امتحانات ترم جاری اینجا را کلیک نمایید." />--%>
    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); border-radius: 5px; margin-bottom: 1%; padding-bottom: 1%">

        <%--     <div class="row">
            <asp:HiddenField ID="hd_LogedinUserId" runat="server" Visible="true"/>
            <asp:HiddenField ID="hd_LogedinUserIdNext" runat="server" Visible="true" />
        </div>--%>


        <div class="row">
            <div class="col-md-12">
                <p style="font-size: 15px; color: #000; padding: 1%">بارگذاری سوالات امتحان </p>
                <div class="alert alert-danger animated bounceIn"
                    style="margin-top: 1%; font-size: 15px; text-align: right; border: 1px solid rgb(155, 89, 182);">
                    توجه: استاد گرامی توجه به موارد زیر الزامی می باشد 
                    <p>*هرگونه تغییر در سربرگ سوالات منجر به رد شدن سوال شما می گردد</p>
                    <p>*چنانچه در پیش نمایش بارگزاری سوال ، فایل سوال را مشاهده نکردین 
                    از مرورگرهای فایرفاکس ، موزیلا ،واترفاکس ویا اپرا استفاده نماید</p>                     
                </div>

                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
                </telerik:RadAjaxManager>
                <telerik:RadGrid ID="grd_Dars" AutoGenerateColumns="false" OnItemCommand="grd_Dars_ItemCommand" OnItemDataBound="grd_Dars_ItemDataBound" runat="server" OnNeedDataSource="grd_Dars_NeedDataSource" EnableEmbeddedSkins="False" BackColor="#3A4A5B" ForeColor="White">
                    <MasterTableView>
                        <HeaderStyle HorizontalAlign="Right" ForeColor="White" CssClass="bg-purple" />
                        <CommandItemSettings AddNewRecordImageUrl="AddRecord.gif" ExportToCsvImageUrl="ExportToCsv.gif" ExportToExcelImageUrl="ExportToExcel.gif" ExportToPdfImageUrl="ExportToPdf.gif" ExportToWordImageUrl="ExportToWord.gif" RefreshImageUrl="Refresh.gif" />
                        <RowIndicatorColumn FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn CollapseImageUrl="SingleMinus.gif" ExpandImageUrl="SinglePlus.gif" FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" FilterImageUrl="Filter.gif" HeaderText="ردیف" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <%# Container.ItemIndex + 1 %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="examQuestionID" FilterImageUrl="Filter.gif" HeaderText="شناسه سوال" ItemStyle-HorizontalAlign="Right" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="did" FilterImageUrl="Filter.gif" HeaderText="مشخصه کلاس" ItemStyle-HorizontalAlign="Right" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="dars" FilterImageUrl="Filter.gif" HeaderText="نام درس" ItemStyle-HorizontalAlign="Right" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="date" FilterImageUrl="Filter.gif" HeaderText="تاریخ امتحان" ItemStyle-HorizontalAlign="Right" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="saat" FilterImageUrl="Filter.gif" HeaderText="ساعت امتحان" ItemStyle-HorizontalAlign="Right" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="cityId" FilterImageUrl="Filter.gif" HeaderText="کد شهر امتحانی" ItemStyle-HorizontalAlign="Right" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="cityName" FilterImageUrl="Filter.gif" HeaderText="بارگذاری برای " ItemStyle-HorizontalAlign="Right" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="status" FilterImageUrl="Filter.gif" HeaderText="وضعیت سوال اول" ItemStyle-HorizontalAlign="Right" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="statusQ2" FilterImageUrl="Filter.gif" HeaderText="وضعیت سوال دوم" ItemStyle-HorizontalAlign="Right" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="rejectReason" FilterImageUrl="Filter.gif" HeaderText="علت ردسوال برای بارگذاری سوال دوم" ItemStyle-HorizontalAlign="Right" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif" Visible="false">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="btn_Option" runat="server" BackColor="Gray" CommandName="Option" CommandArgument='<%# Eval("did").ToString()+"|"+ Eval("date").ToString()+"|"+ Eval("saat").ToString()+"|"+ Eval("cityId").ToString()+"|"+ Eval("examQuestionID").ToString()+"|"+ Eval("statusQ2").ToString() +"|"+ Eval("status").ToString() %>' Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="White" Text="تعیین شرایط آزمون" Width="120px" />
                                    <asp:Button ID="btn_DlForm" runat="server" BackColor="Gray" CommandName="DL" CommandArgument='<%# Eval("did").ToString()+"|"+ Eval("date").ToString()+"|"+ Eval("saat").ToString()+"|"+ Eval("cityId").ToString()+"|"+ Eval("examQuestionID").ToString()+"|"+ Eval("statusQ2").ToString() +"|"+ Eval("status").ToString() %>' Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="White" Text="دانلود فرم سوالات " Width="120px" />
                                    <asp:Button ID="btn_Upload" runat="server" BackColor="Gray" CommandName="Upload" CommandArgument='<%# Eval("did").ToString()+"|"+ Eval("date").ToString()+"|"+ Eval("saat").ToString()+"|"+ Eval("cityId").ToString()+"|"+ Eval("examQuestionID").ToString()+"|"+ Eval("statusQ2").ToString() +"|"+ Eval("status").ToString() %>' Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="White" Text="آپلود سوالات" Width="120px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                <HeaderTemplate>
                                    وضعیت
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Status" runat="server" Visible="false"> </asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                <HeaderTemplate>
                                    علت رد
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Reject" runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                                <HeaderTemplate>
                                    ویرایش
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="btn_Editing" runat="server" BackColor="Gray" CommandName="Editing" CommandArgument='<%# Eval("did").ToString()+"|"+ Eval("date").ToString()+"|"+ Eval("saat").ToString()+"|"+ Eval("cityId").ToString()+"|"+ Eval("examQuestionID").ToString()+"|"+ Eval("statusQ2").ToString() +"|"+ Eval("status").ToString() %>' Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="White" Text="ویرایش" Visible="false" Width="100px" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn CancelImageUrl="Cancel.gif" FilterImageUrl="Filter.gif" InsertImageUrl="Update.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif" UpdateImageUrl="Update.gif">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle FirstPageImageUrl="PagingFirst.gif" LastPageImageUrl="PagingLast.gif" NextPageImageUrl="PagingNext.gif" PrevPageImageUrl="PagingPrev.gif" />
                    </MasterTableView>
                    <PagerStyle FirstPageImageUrl="PagingFirst.gif" LastPageImageUrl="PagingLast.gif" NextPageImageUrl="PagingNext.gif" PrevPageImageUrl="PagingPrev.gif" />
                </telerik:RadGrid>

            </div>
            <div>
                <telerik:RadEditor ID="RadEditor1" runat="server" Visible="False">
                </telerik:RadEditor>
            </div>
        </div>
    </div>

</asp:Content>
