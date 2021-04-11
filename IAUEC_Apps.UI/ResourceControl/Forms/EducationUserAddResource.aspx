<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="EducationUserAddResource.aspx.cs" Inherits="IAUEC_Apps.ResourceControl.Froms.EducationUserAddResource" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <h3>فرم بررسی و ارسال درخواست توسط آموزش</h3>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <div class="container rtl">
        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h4>درخواست جاری
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-5"> 
                            <table class="table table-condensed">
                                <tr>
                                    <td>شماره درخواست :</td>
                                    <td>
                                        <asp:Label ID="lblRequestID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>دسته بندی کلاس ها :</td>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>نام کلاس :</td>
                                    <td>
                                        <asp:Label ID="lblCourseName" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>محل درخواستی :</td>
                                    <td>
                                        <asp:Label ID="lblLocation" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-sm-4">
                            <table class="table table-condensed ">
                                <tr>
                                    <td>نام استاد :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProfessor" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>ظرفیت :</td>
                                    <td>
                                        <asp:Label ID="lblCapacity" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>امکانات درخواستی :</td>
                                    <td>
                                        <asp:CheckBoxList ID="chblOptions" runat="server" CssClass="checkbox" RepeatColumns="1" Enabled="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>توضیحات  :</td>
                                    <td>
                                        <asp:Literal ID="lblDescription" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-sm-1">

                            <table class="">
                                <tr>
                                    <td>
                                        <h5>زمان درخواستی :
                                        </h5>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>ساعت شروع :</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSessionStart" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>ساعت پایان :</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSessionEnd" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>تاریخ :</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="pcal1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-sm-2">
                            <p>عملیات</p>
                            <asp:Button ID="btnEditRequest" Text="ویرایش درخواست" runat="server" CssClass="btn btn-warning btn-block " OnClick="btnEditRequest_Click" />
                            <br />
                            <asp:TextBox ID="txtAnswerNote" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnswerNote" ErrorMessage="لطفا دلیل رد درخواست را ذکر کنید" ForeColor="Red" ValidationGroup="deny"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnDenyRequest" Text="رد درخواست" runat="server" CssClass="btn btn-block btn-danger" OnClick="btnDenyRequest_Click" ValidationGroup="deny" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12" style="max-width: 100%" dir="rtl">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h4>فهرست کلاس های پیشنهادی سامانه برای این درخواست</h4>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="grdResourceList" runat="server" DataKeyNames="ID" AllowSorting="true" OnSorting="grdResourceList_Sorting" AutoGenerateColumns="false" OnRowDataBound="grdResourceList_RowDataBound" EmptyDataText=" متاسفانه هیچ کلاسی مطابق با امکانات درخواستی پیدا نشد " CssClass=" table table-bordered tbl table-condensed rtl" OnRowCommand="grdResourceList_RowCommand">
                        <HeaderStyle CssClass="bg-blue-sky text-center" />
                        <Columns>
                            <asp:TemplateField HeaderText=" کلاس های پیشنهادی" ItemStyle-CssClass=" rtl" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label Text="نام :" runat="server" Font-Bold="true" />
                                    <asp:Label ID="lblname" Text='<%#Bind("name") %>' runat="server" CssClass="rtl"></asp:Label>
                                    <br />
                                    <asp:Label Text="محل :" runat="server" Font-Bold="true" />
                                    <asp:Label ID="lblLocation" Text='<%#Bind("location") %>' runat="server" CssClass="rtl"></asp:Label>
                                    <br />
                                    <asp:Label Text="امکانات :" Font-Bold="true" runat="server" />
                                    <asp:BulletedList runat="server" ID="bltResOptions" BulletStyle="Numbered"></asp:BulletedList>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="دیگر درخواست های فرستاده شده ولی تایید نشده برای این کلاس">
                                <ItemTemplate>
                                    <asp:GridView ID="grdRequestPendingList" runat="server" DataKeyNames="ID" CssClass="table table-bordered table-striped table-condensed rtl" AutoGenerateColumns="false">
                                        <HeaderStyle CssClass=" text-center bg-warning" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="شماره"  />
                                            <asp:BoundField DataField="note" HeaderText="توضیحات"  />
                                            <asp:BoundField DataField="issue_time" HeaderText="تاریخ ثبت درخواست" />
                                            <asp:BoundField DataField="issuerName" HeaderText="متقاضی" />
                                            <asp:BoundField DataField="sessionstart_time" HeaderText="ساعت شروع" />
                                            <asp:BoundField DataField="sessionend_time" HeaderText="ساعت پایان" />
                                        </Columns>
                                    </asp:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="پیشنهاد این کلاس">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnAddResource" Text="ارسال درخواست" CommandName="Add" CommandArgument='<%#Eval("ID")%>' CssClass="btn btn-success" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="درخواستهای تایید شده برای این کلاس">
                                <ItemTemplate>
                                    <asp:GridView ID="grdApprovedRequestList" DataKeyNames="ID" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-condensed rtl">
                                        <HeaderStyle CssClass=" text-center bg-success" />
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="شماره" />
                                            <asp:BoundField DataField="issuerName" HeaderText="استاد" />
                                            <asp:BoundField DataField="sessionstart_time" HeaderText="ساعت شروع" />
                                            <asp:BoundField DataField="sessionend_time" HeaderText="ساعت پایان" />
                                        </Columns>
                                    </asp:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function myfunction() {
            window.location.href('');
        }
    </script>
    <%--<telerik:RadWindow runat="server" ID="RadWindow2" VisibleOnPageLoad="false">
        <ContentTemplate>
            <div dir="rtl">
                <asp:Image ID="Image1" runat="server" Width="35px" Height="35px" ImageUrl="~/ResourceControl/Images/Info.png" />
                <asp:Label ID="lblSuccess" Text="متن پیام" runat="server" />
            </div>
            <br />
            <asp:Button ID="btnSubmitMsg" runat="server" Text="تایید" CssClass="btn btn-success" OnClick="btnSubmitMsg_Click" />
        </ContentTemplate>
    </telerik:RadWindow>--%>
</asp:Content>
