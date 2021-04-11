<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationUserReport.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationUserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .rcbInner {
            height: 36px !important;
            border-top: 1px solid #cccccc !important;
            border-right: 1px solid #cccccc !important;
            border-bottom: 1px solid #cccccc !important;
            border-left: none !important;
            color: #555555 !important;
        }

        .rcbActionButton {
            height: 36px !important;
            background-color: white !important;
            background-image: none;
            border-top: 1px solid #cccccc !important;
            border-left: 1px solid #cccccc !important;
            border-bottom: 1px solid #cccccc !important;
            border-right: none !important;
        }

        .RadComboBox_Default .rcbActionButton {
            background-image: none !important;
        }

        .RadComboBox_Default .rcbInput {
            height: 32px !important;
            font-family: Yekan,'B Yekan' !important;
            font-size: 14px !important;
            font-weight: bold !important;
            padding-right: 11px !important;
            color: #555555 !important;
        }

        .rcbItem, rcbHovered {
            font-family: Yekan,'B Yekan' !important;
            font-size: 13px !important;
            font-weight: bold !important;
            color: #555555 !important;
        }

        .RadComboBoxDropDown_Default .rcbHovered {
            background-color: #2fa4e7 !important;
            color: white !important;
            font-family: Yekan,'B Yekan' !important;
            font-weight: bold !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>گزارشات سامانه رزرواسیون کلاس های حضوری</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" dir="rtl">
        <div class="row">
            <div id="dvDanshkade" runat="server" visible="false" class="col-sm-6">
                <asp:DropDownList ID="drpDanshkade" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpDanshkade_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ErrorMessage="لطفا دانشکده را انتخاب کنید." ControlToValidate="drpDanshkade" runat="server" ForeColor="Red" Display="Dynamic" InitialValue="انتخاب کنید" />
            </div>
            <div class="col-sm-6">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="col-md-2 control-label" Text="نام استاد :"></asp:Label>
                        <div class="col-sm-8">
                            <telerik:RadComboBox ID="drpProfessors" runat="server" MarkFirstMatch="True" Filter="Contains" HighlightTemplatedItems="True" RenderMode="Lightweight" Width="100%" AllowCustomText="false" ExpandDirection="Down" Culture="(Default)" Height="300px"></telerik:RadComboBox>
                        </div>
                        <div class=" col-sm-2">
                            <asp:Button ID="btnShowResult" runat="server" OnClick="btnShowResult_Click" Text="نمایش" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row table-responsive">
            <asp:GridView ID="grdLessons" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-striped" >
                <HeaderStyle CssClass="bg-blue" />
                <Columns>
                    <asp:BoundField HeaderText="مشخصه" DataField="DID"/>
                    <asp:BoundField HeaderText="نام" DataField="Name" />
                    <asp:BoundField HeaderText="تعداد کل درخواست های ارسالی" DataField="TotalCount"/>
                    <asp:BoundField HeaderText="تعداد ارسالی توسط استاد" DataField="Submitted"/>
                    <asp:BoundField HeaderText="تعداد تایید شده توسط آموزش"  DataField="Send"/>
                    <asp:BoundField HeaderText="تعداد تایید شده توسط اداری"  DataField="Approved"/>
                    <asp:BoundField HeaderText="تعداد رد شده" DataField="Denied"  />
                    <asp:BoundField HeaderText="تعداد اطلاع رسانی شده" DataField="Informed"/>
                    <asp:BoundField HeaderText="تعداد از دست رفته" DataField="Losed"/>
<%--                    <asp:TemplateField HeaderText="درخواست های ثبت شده">
                        <ItemTemplate>
                            <asp:GridView ID="grdRequests" runat="server" OnRowCommand="grdRequests_RowCommand" AutoGenerateColumns="false" OnRowDataBound="grdRequests_RowDataBound" CssClass="table table-bordered table-condensed table-striped">
                                <HeaderStyle CssClass="bg-blue-sky" />
                                <Columns>
                                    <asp:BoundField HeaderText="محل" DataField="Location" />
                                    <asp:TemplateField HeaderText="وضعیت">
                                        <ItemTemplate>
                                            <asp:Image ID="imgStatus" runat="server" Width="25px" Height="25px" ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="زمان ها">
                                        <ItemTemplate>
                                            <asp:Button Text="بررسی" runat="server" CssClass="btn btn-primary btn-sm" CommandName="check" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>
<%--    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <telerik:RadWindow ID="RadWindow1" AutoSize="false" Height="600" runat="server" Width="1050">
        <ContentTemplate>
            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    <div class="container" style="padding: 10px; margin: 10px; overflow: visible" dir="rtl">
                        <div class="heading">
                            <h4>تاریخ ها و ساعات درخواستی :</h4>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" style="overflow: visible !important">
                                <asp:GridView ID="grdDateTime" runat="server" DataKeyNames="datetimeid" OnRowDataBound="grdDateTime_RowDataBound" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-stripted">
                                    <HeaderStyle CssClass="bg-primary" />
                                    <EditRowStyle CssClass="GridViewEditRow" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ردیف">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="تاریخ">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("Date").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                        <asp:TemplateField HeaderText="کلاس پیشنهادی">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpResource" runat="server" Enabled="false">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdnfReqId" runat="server" />
                            </div>
                        </div>
                    </div>
                    <script type="text/javascript">
                        function GetRadWindow() {
                            var oWindow = null;
                            if (window.radWindow)
                                oWindow = window.radWindow;
                            else if (window.frameElement && window.frameElement.radWindow)
                                oWindow = window.frameElement.radWindow;
                            return oWindow;
                        }

                    </script>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>--%>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
</asp:Content>
