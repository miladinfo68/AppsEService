<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="CheckOutSignEntry.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutSignEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>فرم ثبت امضا</h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        a.pcalBtn {
            position: relative;
            margin-right: -22px;
            vertical-align: middle;
        }
    </style>
    <div class="container" dir="rtl">
        <div class="row">
            <div class="col-md-12">
             
                    <div class="col-md-5 table-bordered">
                        <asp:Label Text="نام کاربری :" runat="server" />
                        <asp:DropDownList runat="server" ID="drpUser" CssClass="dropdown" ControlToValidate="drpUser" AutoPostBack="True" OnSelectedIndexChanged="drpUser_SelectedIndexChanged"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا کاربر را مشخص نمایید" Text="*" ControlToValidate="drpUser" ForeColor="Red" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-7 table-bordered">
                        <div class="row">
                            <div class="col-md-4 table-bordered">

                                <p>نقش کاربر در سامانه خدمات</p>
                                <asp:BulletedList runat="server" ID="bltUserRols"></asp:BulletedList>
                            </div>
                            <div class="col-md-4 table-bordered">

                                <p>نقش در سامانه تسویه حساب</p>
                                <asp:BulletedList runat="server" ID="bltCheckoutRole"></asp:BulletedList>
                            </div>

                            <div class="col-md-4 table-bordered">

                                <p>امضای موجود</p>
                                <asp:BulletedList runat="server" ID="bltSigns"></asp:BulletedList>
                            </div>
                        </div>
                    </div>
               
            </div>

        </div>

        <div class="row">
            <div class="col-md-5 table-bordered">
                <asp:Label Text="نقش کاربر درتسویه :" runat="server"></asp:Label>
                <asp:DropDownList ID="drpUserStatus" runat="server" CssClass="dropdown" AutoPostBack="True" OnSelectedIndexChanged="drpUserStatus_SelectedIndexChanged" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="لطفا نقش کاربر را در سامانه مشخص نمایید" ForeColor="Red" Text="*" ControlToValidate="drpUserStatus" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>

            </div>
            <div class="col-md-7 table-bordered">
                از تاریخ 
                        <asp:TextBox runat="server" CssClass="pdate" ID="txtFromDate"></asp:TextBox>
                تا تاریخ
                        <asp:TextBox runat="server" CssClass="pdate" ID="txtToDate"></asp:TextBox>

            </div>
        </div>
        <div class="row">
            <div class="col-md-12 table-bordered">
                <asp:Label Text="تصویر امضا" runat="server" />
                <asp:FileUpload ID="flpSignImage" runat="server" CssClass="btn-info" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="لطفا عکس امضا را مشخص نمایید" ForeColor="Red" Text="*" ControlToValidate="flpSignImage" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                <asp:Button ID="btnSubmitFile" Text="ثبت امضا" runat="server" CssClass="btn btn-success" OnClick="btnSubmitFile_Click" ValidationGroup="a" />
                <br />
                <asp:Label ID="lblMessage" Text="" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="a" />
            </div>
        </div>


        <div class="col-md-4 text-right">
            <p>امضاء فعلی کاربر</p>
            <asp:Image ID="imgSign" runat="server" Width="100%" Height="100%" BorderStyle="Dotted" />
        </div>

    </div>

</asp:Content>
