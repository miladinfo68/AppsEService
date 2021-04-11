<%@ Page Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="bookletReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.bookletReport" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        h4 {
            background-color: #20387c;
            color: #ffffff;
            height: 25px;
        }
        /*div{
            border: 1px solid black;
        }*/
        .btnTarhdars {
            width: 100%;
            height: 25px;
            move-to: yellow;
            background-color:#9d6ffc;
            color:white;
        }

        .row {
            text-align: center;
        }
    </style>
    <div class="container" dir="rtl">
        

        <br />
        
        <div class="row"  >
            <div class="col-md-4 col-sm-12"  >
                <div class="row" >
                    <div class="col-md-3 col-sm-3" >
                        <asp:Label Text="ترم :" runat="server" CssClass="fieldTitle" />
                    </div>
                    <div class="col-md-8 col-sm-8" >
                        <asp:DropDownList ID="drpdTerm" runat="server" Width="100%"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-12"  >
                <div class="row" >
                    <div class="col-md-3 col-sm-3" >
                        <asp:Label Text="طرح درس :" runat="server" CssClass="fieldTitle" />
                    </div>
                    <div class="col-md-8 col-sm-8" >
                        <asp:DropDownList ID="drpdTarhdars" runat="server" Width="100%">
                            <asp:ListItem Value="1">دارد</asp:ListItem>
                            <asp:ListItem Value="0">ندارد</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
             <div class="col-md-4 col-sm-12">
                <div class="row">
                    <div class="col-md-3 col-sm-3">
                        <asp:Label Text="دانشکده :" runat="server" CssClass="fieldTitle" />
                    </div>
                    <div class="col-md-8 col-sm-8">
                        <asp:DropDownList ID="drpdDanesh" runat="server" Width="100%"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
           
            <div class="col-md-4 col-sm-12">
                <div class="row">
                    <div class="col-md-3 col-sm-3">
                        <asp:Label Text="رشته :" runat="server" CssClass="fieldTitle" />
                    </div>
                    <div class="col-md-8 col-sm-8">

                        <asp:DropDownList runat="server" Width="100%" Height="25px" ID="drpdResh"></asp:DropDownList>
                    </div>
                </div>
            </div>
           
           
        </div>

        <br />
        <div class="row">
             <div class=" col-sm-12 col-md-12 center">
                <asp:Button CssClass="btnTarhdars" ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="جستجوی طرح درس" Width="150px"></asp:Button>
            </div>
        </div>
        <br />
        <br />
        <br />
        <div class="row">
            <div class="col-md-2">
                <asp:ImageButton  ID="btnExcelReport" ImageUrl="~/University/Theme/images/Excel_ExcelML.png" OnClick="btnExcelReport_Click" runat="server" Text="جستجوی طرح درس" Width="50px"></asp:ImageButton>
            </div>
        </div>
        <div class="row" style="overflow-x: scroll">
            <telerik:RadGrid ID="radGridtarh" OnExcelMLWorkBookCreated="radGridtarh_ExcelMLWorkBookCreated" runat="server" AutoGenerateColumns="false" OnItemCommand="radGridtarh_ItemCommand"
                AllowPaging="true" PageSize="100" OnNeedDataSource="radGridtarh_NeedDataSource" OnDataBinding="radGridtarh_DataBinding" AllowFilteringByColumn="true">
                <MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="کد طرح درس"  DataField="BOOKLET_ID" AllowFiltering="true" Visible="true"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="کد مشخصه" DataField="did" AllowFiltering="true" Visible="true"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="کد درس" DataField="codedars" AllowFiltering="true"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="نام درس" DataField="namedars" AllowFiltering="true"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="کد استاد" DataField="ocode" AllowFiltering="true"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="نام استاد" DataField="profName" AllowFiltering="true"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="نام رشته" DataField="nameresh" AllowFiltering="true"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="فرمت فایل" DataField="extention"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="دانلود طرح درس">
                            <ItemTemplate>
                                <asp:Button Text="دانلود" runat="server" ID="btnDlBooklet" CommandName="bookletDownload" CommandArgument='<%#Eval("BOOKLET_ID") %>' CssClass="btn btn-success" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <%--<asp:GridView ID="gridviewTarhdars" runat="server" AutoGenerateColumns="true"></asp:GridView>--%>
        </div>
        <object data="<%= EmbedSrc %>" class="embededObject" id="embededObject">
            <%--<embed src="<%= EmbedSrc %>" width="100%" height="900px" />--%>
        </object>
    </div>

    <uc1:AccessControl runat="server" ID="AccessControl" />

</asp:Content>
