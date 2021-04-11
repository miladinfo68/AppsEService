<%@ Page Title="" Language="C#" MasterPageFile="~/University/Support/MasterPage/CMSSupportMaster.Master" AutoEventWireup="true" CodeBehind="GetSecurityCode.aspx.cs" Inherits="IAUEC_Apps.UI.University.Support.CMS.GetSecurityCode" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        table {
            direction: rtl;
            text-align: center;
            border: 1px solid #006400;
        }

        td, th {
            direction: rtl;
            text-align: center;
            border: 1px solid #dddddd;
            padding: 8px;
            color: #000000;
        }

        th {
            color: #008080;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="content" dir="rtl">
        <div id="divAlarm" class="alert alert-error" runat="server" Visible="False" >
            <div>
                استادی با این کدملی موجود نمی باشد
            </div>
        </div>
        <div class="row form form-group">
            <div class="col-md-1">
                کدملی استاد:
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtCodeMeli" runat="server" CssClass="form-control "></asp:TextBox>
            </div>
             <div class="col-md-2">
                
                <asp:Button ID="btn_SearchSecityCode" runat="server" Text="جستجو" OnClick="btn_FindSecurityCode_Click" ValidationGroup="btnSearch" CssClass="btn btn-success" />
            </div>
            <div class="col-md-3">
                <asp:RequiredFieldValidator runat="server" ErrorMessage="لطفا کد ملی را وارد نمایید" ControlToValidate="txtCodeMeli" ValidationGroup="btnSearch" ForeColor="red"></asp:RequiredFieldValidator>

            </div>
        </div> <uc1:AccessControl ID="AccessControl1" runat="server" />
        
        <div id="divSecurityCode" class="row" runat="server" visible="False" enableviewstate="True">

            <div class="panel panel-success">
                <div class="panel-heading" style="color: darkseagreen; font-size: large;">
                    نتیجه بازیابی کد امنیتی
                </div>
                <div class="panel-body well">
                    <table class="table">
                        <tr>
                            <th>نام</th>
                            <th>نام خانوادگی</th>
                            <th>کدملی</th>
                            <th>کدامنیتی</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblName" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblFamily" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblCodeMeli" /></td>
                            <td style="color: #8b0000">
                                <asp:Label runat="server" ID="lblSecuritCode" /></td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
