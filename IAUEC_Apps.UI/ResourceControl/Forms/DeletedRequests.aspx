<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="DeletedRequests.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.DeletedRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
<title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <style>
        .GridViewEditRow input[type=text] {
            width: 100px;
        }   
        /* size textboxes */
        .GridViewEditRow select {
            width: 100px;
        }
        /* size drop down lists */

        .cssPager span {
            background-color: #cccccc !important;
            color: black !important;
            font-family: 'B Yekan' !important;
            font-size: 24px !important;
            padding-left: 5px;
            padding-right: 5px;
        }

        .cssPager a {
            background-color: #ffffff !important;
            color: #808080 !important;
            font-family: 'B Yekan' !important;
            font-size: 22px !important;
            padding-left: 5px;
            padding-right: 5px;
        }

        .bTable {
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-right-radius: 3px;
            border-top-left-radius: 3px;
        }

        .buttons, button, .btn {
            margin-bottom: initial;
            margin-right: initial;
        }

        .tbl {
            max-width: 100%;
        }

            .tbl td {
                text-align: center !important;
                font-family: 'B Titr';
            }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            vertical-align: middle;
            font-family: 'B Titr';
        }

        #tdDrpRequestTypeList > select {
            float: right !important;
        }

        td {
            font-family: 'B Titr';
        }
    </style>
    <script type="text/javascript">
        function confirmAspButton(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
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
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#historyModal').modal('show');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" Visible="false" runat="server"></asp:Literal>
    <h3>گزارش درخواست های حذف شده</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="direction: rtl; min-height: 100px; overflow-y: visible">
 <style>
        .inlineTableDisplay {
            display: inline-table;
        }
        label {
    display: initial;
    max-width: 100%;
    margin-bottom: 5px;
    font-weight: bold;
    padding: 5px;
}
    </style>

           <table class=" table table-bordered tbl rtl hidden-print">
                        <tr class="bg-primary">
                            <td>تاریخ</td>
                            <td>شماره درخواست</td>
                            <td>مشخصه کلاس</td>
                            <td>متقاضی</td>
                             <td>عملیات</td>
                        </tr>
                        <tr>
                            <td>
                                <p>تاریخ :</p>
                                <asp:TextBox ID="pcal1" runat="server" />
                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="pcal1" ErrorMessage="لطفا تاریخ را انتخاب نمایید" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
<%--                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="لطفا تاریخ را به صورت صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="pcal1">*</asp:RegularExpressionValidator>--%>
                            </td>

                            <td>
                                <p>شماره درخواست :</p>
                                <asp:textbox ID="txtRequestNumber" runat="server"></asp:textbox>
                            </td>
                             <td>
                                <p>مشخصه کلاس :</p>
                                <asp:textbox ID="txtResourceNumber" runat="server" ></asp:textbox>
                            </td>

                            <td style="text-align: center;">
                                <p>متقاضی :</p>
                               
                                       
                                <p>لطفا دانشکده را انتخاب کنید</p>
                                    
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="drpChooseDanshkade" runat="server" OnSelectedIndexChanged="drpChooseDanshkade_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdown form-control" Width="200" Height="25">
                                            </asp:DropDownList>
                                        </div>
                                 
                                 <asp:RequiredFieldValidator ID="valField" ControlToValidate="RadComboBoxField" ForeColor="Red" ValidationGroup="submit" Text="*" runat="server" InitialValue="جستجو و انتخاب کنید" ErrorMessage="لطفا نام استاد را انتخاب کنید"></asp:RequiredFieldValidator>
                                        <div>
                                           <p>استاد :</p>
                                             <telerik:RadComboBox ID="RadComboBoxField" runat="server" OnSelectedIndexChanged="RadComboBoxField_SelectedIndexChanged" AutoPostBack="true" MarkFirstMatch="True" Filter="Contains" HighlightTemplatedItems="True" RenderMode="Lightweight" Width="200" AllowCustomText="false" ExpandDirection="Down" Culture="(Default)" Height="300px"></telerik:RadComboBox>
                                        </div>
                              </td>
                            <td>
                                <asp:Button ID="btnSubmit" Text="جستجو" runat="server" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="خطا " />
                               
                            </td>
                        </tr>
                    </table>
         <br />

    <asp:GridView ID="grdDeletedRequest" align="center" runat="server" OnPageIndexChanging="grdDeletedRequest_PageIndexChanging" 
              AllowPaging="true" PageSize="20"  CssClass="table table-bordered text-center table-condensed"
               AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="هیچ درخواستی پیدا نشد." 
              OnRowCommand="grdDeletedRequest_RowCommand" OnRowDataBound="grdDeletedRequest_RowDataBound" OnRowEditing="grdDeletedRequest_RowEditing"
               OnRowDeleting="grdDeletedRequest_RowDeleting">         
                                <Columns>
                                <asp:TemplateField HeaderText="ردیف">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="شماره درخواست" DataField="ID" />
                                <asp:BoundField HeaderText="مشخصه کلاس" DataField="CourseDID" />
                                <asp:BoundField HeaderText="نام درس" DataField="courseName" />
                            
                                <asp:BoundField HeaderText="تاریخ ثبت" DataField="issue_time" />
                                <asp:BoundField HeaderText="متقاضی" DataField="issuerName" />
                                <asp:BoundField HeaderText="توضیحات" DataField="note" />
                               

                                <asp:TemplateField HeaderText="مشاهده">
                                    <ItemTemplate>
                                        <asp:Button ID="btnShowDateTime" Text="نمایش" CssClass="btn btn-success" CommandName="showInfo" CommandArgument='<%# Eval("ID") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                            </Columns>
                            <PagerSettings NextPageText="بعدی" PreviousPageText="قبلی" FirstPageText="صفحه اول" LastPageText="صفحه آخر" Mode="Numeric" Position="Bottom" />
                            <PagerStyle CssClass="cssPager" />
                        </asp:GridView>

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


         <asp:HiddenField ID="hdnfReqId" runat="server" />


         <telerik:RadWindow ID="RadWindow3" runat="server" Width="850px" Height="600px" Skin="Glow" Modal="True">
        <ContentTemplate>
            <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl">
                        <div class="heading2 bg-green" style="padding: 5px">
                            <h3>جزئیات درخواست :
                                <asp:Image ID="imgStatus2" runat="server" Width="30px" /></h3>

                        </div>
                        <br />
                        <div class="row center-margin">
                            <div style="border: 5px solid #A9A9A9; margin-left: 5%; margin-right: 5%;">
                                <div style="margin: 5%;">
                                    <table class="table table-responsive" style="font-weight: bolder;">
                                        <tr>
                                            <td>
                                                <strong>شماره درخواست : </strong>
                                                <asp:Label ID="lblRequestId" CssClass="text-primary" runat="server" />
                                            </td>
                                            <td>
                                                <strong>وضعیت : </strong>
                                                <asp:Label ID="lblStatue" CssClass="text-primary" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>درس درخواست شده : </strong>
                                                <asp:Label ID="lblDarkhast" CssClass="text-primary" runat="server" />
                                            </td>
                                            <td>
                                                <strong>ساختمان: </strong>
                                                <asp:Label ID="lblLocation" CssClass="text-primary" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>تاریخ درخواستی برگزاری کلاس:</strong>
                                                <asp:Label ID="lblRequest" CssClass="text-primary" runat="server" />
                                            </td>
                                            <td>
                                                <strong>تعداد نفرات: </strong>
                                                <asp:Label ID="lblCapecity" CssClass="text-primary" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>ساعت شروع: </strong>
                                                <asp:Label ID="lblTime1" CssClass="text-primary" runat="server" />
                                            </td>
                                            <td>
                                                <strong>ساعت پایان: </strong>
                                                <asp:Label ID="lblTime2" CssClass="text-primary" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>تاریخ ثبت درخواست : </strong>
                                                <asp:Label ID="lbldateOfRequest" CssClass="text-primary" runat="server" />
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Literal ID="lblheader" runat="server" Visible="False" Text=""></asp:Literal></strong>
                                                <asp:Label ID="lblDateOfResponse" Visible="False" CssClass="text-primary" runat="server" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>توضیحات: </strong>
                                                <asp:Label ID="lblTozieh" CssClass="text-primary" runat="server" />
                                            </td>
                                            <td>
                                                <strong>
                                                    <asp:Literal ID="litDenyNot" runat="server" Visible="False" Text=""></asp:Literal>

                                                </strong>
                                                <asp:Label ID="lblDenyNot" Visible="False" CssClass="text-primary" runat="server" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>نوع مکان درخواستی: </strong>
                                                <asp:Label ID="lblPosition" CssClass="text-primary" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 55%;">
                                                <div runat="server" id="tblRangeOfDate" visible="False" class="table-responsive text-center">
                                                    <div class="bg-blue-sky">
                                                        <h4 class="text-center">زمانهای درخواستی فعلی</h4>
                                                    </div>
                                                    <asp:GridView ID="grdOldDateTime" HorizontalAlign="Center" CssClass="table table-responsive backGroundForGrdDate text-center" runat="server" AutoGenerateColumns="false">
                                                        <HeaderStyle CssClass="bg-blue-sky" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="تاریخ" DataField="date">
                                                                <ItemStyle ForeColor="#29343B"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="ساعت شروع">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("StartTime"))).ToString("hh\\:mm") %>' runat="server" ForeColor="#29343B" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ساعت پایان">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("EndTime"))).ToString("hh\\:mm") %>' runat="server" ForeColor="#29343B" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:GridView ID="grdFacilities" CssClass="table table-responsive backGroundForGrdDate text-center" runat="server" AutoGenerateColumns="false">
                                                    <HeaderStyle CssClass="bg-blue-sky" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ردیف">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ForeColor="#29343B" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="امکانات درخواستی" DataField="Name">
                                                            <ItemStyle ForeColor="#29343B"></ItemStyle>
                                                        </asp:BoundField>

                                                    </Columns>
                                                </asp:GridView>
                                            </td>


                                        </tr>
                                    </table>
                                </div>
                            </div>


                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>


      </div>
</asp:Content>
