<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="insertTuition.aspx.cs" MasterPageFile="~/University/Tuitional/masterPages/tuitionalMasterpage.Master" Inherits="IAUEC_Apps.UI.University.Tuitional.insertTuition" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>




<asp:Content ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" dir="rtl" style="padding: 20px">
                <telerik:RadWindowManager ID="rAlert" runat="server" Width="60%" BackColor="#ffffcc" ForeColor="#660066" Font-Size="X-Large"></telerik:RadWindowManager>

        <div class="row" style="background-color: lightgoldenrodyellow">
            <div class="col-md-12">
                <h3 style="color: orangered">قبل از شروع به بارگزاری موارد زیر را بخوانید:</h3>
                <ul>

                    <li>
                        <span>برنامه به گونه ای نوشته شده که از شیتهای مخفی(hide) صرف نظر میکند. بنابراین در صورتی که نیاز به بررسی شیت خاصی ندارید میتوانید آن را مخفی نمایید.</span>
                    </li>

                    <li>
                        <span>در تمام شیت ها سرستونهای اصلی در ردیف سوم و از ستون A (یعنی سلول A3)قرار داده شود.</span>
                    </li>
                    <li>
                        <span>در تمام شیت ها ، کد ترم مناسب، در سلول A1 همان شیت قرار گیرد.این سلول پس از پایان عملیات به صورت اتوماتیک حذف خواهد شد. </span>
                    </li>
                    <li>
                        <span>شهریه ثابت و شهریه متغیر و جمع شهریه پس از آخرین سلول دارای مقدار(مقدار میتواند شامل عدد، حرف و حتی رنگ باشد) قرار خواهد گرفت.</span>
                    </li>
                </ul>
                <h4 style="color: mediumpurple">روند انجام کار</h4>
                <ul style="color: rebeccapurple; text-align: justify">
                    <li>
                        <span>لطفا کد نامه آیکنی را در باکس </span><span class="text  border-blue">شماره نامه آیکن</span><span> وارد فرمایید.</span>
                    </li>
                    <li>
                        <span>پس از درج شماره نامه آیکن، نسبت به بارگذاری فایل اکسل مورد نظر اقدام فرمایید و سپس دکمه</span><span class="btn btn-warning btn-sm">  بررسی فایل</span><span>  را فشار دهید.</span>
                    </li>
                    <li>
                        <span>پس از بررسی فایل، شیت های موجود در اکسل در لیست کشویی نشان داده می شوند.  با انتخاب هر کدام از موارد لیست کشویی، سرستون های آن شیت در جدول نمایش داده خواهند شد.</span>
                    </li>
                    <li>
                        <span>مواردی که در جلوی عبارات سرستون به صورت لیست کشویی نمایش داده میشوند نشان دهنده موارد مورد نیاز جهت اخذ مبلغ شهریه می باشند. لطفا دقت فرمایید برای تمام شیت ها، تمام موارد انتخاب شوند.</span>
                    </li>
                    <li>
                        <span>پس از انتخاب هر کدام از موارد مورد نیاز از لیست کشویی روبروی سرستون ها لازم است حتما دکمه</span><span class="btn btn-info btn-sm">  اعمال</span><span>  مقابل آن فشرده شود</span>
                    </li>
                    <li>
                        <span>در صورتی که ستونی را به اشتباه به یکی از گزینه های لیست کشویی روبروی آن تخصیص داده اید و دکمه اعمال را نیز فشرده اید، میتوانید به جای مورد انتخاب شده، گزینه </span><span style="font-weight: bolder; color: black">انتخاب کنید </span><span>را انتخاب فرمایید و دوباره دکمه اعمال مقابل آن را فشار دهید</span>
                    </li>
                    <li>
                        <span>پس از انتخاب تمام موارد مورد نیاز، دکمه </span><span class="btn btn-success btn-sm">ثبت در پایگاه داده</span><span>  را فشار دهید</span>
                    </li>
                    <li>
                        <span>در صورتی که مایل به باز شدن دسترسی کارت ورود به جلسه دانشجویان هستید دکمه</span><span class="btn btn-dark btn-sm">باز شدن دسترسی کارت ورود به جلسه</span><span>را فشار دهید</span>
                    </li>
                    <li>
                        <span>در صورتی که ستونی به موارد مشخص شده اختصاص نیافته باشد، پس از فشردن دکمه آپدیت اکسل، به صورت پیامی قرمز رنگ به شما اخطار خواهد داد. لطفا پس از خواند متن به دقت، نسبت به تخصیص تمام موارد ذکر شده اقدام فرمایید.</span>
                    </li>
                    <li>
                        <span>میتوانید با فشردن دکمه </span><span class="btn btn-warning btn-sm">بارگزاری مجدد</span><span>  نسبت به بارگزاری دوباره و انجام مراحل بارگزاری و تخصیص ستون از ابتدا اقدام فرمایید</span>
                    </li>
                </ul>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-3 border-blue">
                <asp:Label CssClass="text " Text="شماره نامه آیکن" runat="server"></asp:Label>
                <asp:TextBox  ID="txtIcanNumber" runat="server" Enabled="true"></asp:TextBox>
            </div>

            <div class="col-md-3" style="padding-left: 20px">
                <telerik:RadAsyncUpload ID="uploadFile" Localization-Select="انتخاب فایل" runat="server" MaxFileInputsCount="1" AllowedFileExtensions=".xlsx,.xls" Width="200px"></telerik:RadAsyncUpload>
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnUploadExcel" CssClass="btn btn-warning" runat="server" Text="بررسی فایل" OnClick="btnUploadExcel_Click" />
            </div>
            <div class="col-md-3"></div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <h2 id="message" runat="server" style="color: red"></h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblSheets" runat="server" Visible="false">شیتهای موجود در اکسل</asp:Label>
                <asp:DropDownList ID="drpSheets" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="drpSheets_SelectedIndexChanged" runat="server" DataValueField="sheetNumber" DataTextField="sheetText"></asp:DropDownList>
                <asp:Label ID="lblTerm" Text="" runat="server"></asp:Label>
            
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnReset" CssClass="btn btn-warning" runat="server" Visible="false" Text="بارگزاری مجدد" OnClick="btnReset_Click" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnInsertTuition" CssClass="btn btn-success left_col" Visible="false" runat="server" Text="ثبت در پایگاه داده" OnClick="btnInsertTuition_Click" />

            </div>
            <div class="col-md-2">
                <asp:Button ID="btnOpenCartAccess" CssClass="btn btn-dark left_col" Visible="false" runat="server" Text="باز شدن دسترسی کارت ورود به جلسه" OnClick="btnOpenCartAccess_Click" />

            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="grdColumns" Visible="false" OnItemCommand="grdColumns_ItemCommand" AutoGenerateColumns="false" runat="server" OnItemDataBound="grdColumns_ItemDataBound">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="rowNum" HeaderText="ردیف"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="sheetColumn" HeaderText="سرستون های موجود در شیت"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="لیست موارد مورد نیاز">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpListColumn" runat="server"></asp:DropDownList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="عملیات">
                                <ItemTemplate>
                                    <asp:Button ID="btnChangeColumn" Text="اعمال" runat="server" CommandArgument='<%#Eval("rowNum") %>' CssClass="btn btn-info" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>
    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>

