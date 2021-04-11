<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="MilitaryReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.MilitaryReport" %>

<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
         <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />
    <style>
        .grid td, .grid th{text-align:center;}
        .spacing { margin-right:20px; margin-bottom:15px;}
        .centerItem {
            margin-right: 35px;
        }
                       .rcbInner {
            height: 36px !important;
            border-top: 1px solid #cccccc !important;
            border-right: 1px solid #cccccc !important;
            border-bottom: 1px solid #cccccc !important;
            border-left: none !important;
            color: #555555 !important;
            padding-bottom:5px;
        }

        .rcbActionButton {
            height: 36px !important;
            background-color: white !important;
            background-image: none;
            border-top: 1px solid #cccccc !important;
            border-left: 1px solid #cccccc !important;
            border-bottom: 1px solid #cccccc !important;
            border-right: none !important;
            padding-bottom:5px;
        }

        .RadComboBox_Default .rcbActionButton {
            background-image: none !important;
            padding-bottom:5px;
        }

        .RadComboBox_Default .rcbInput {
            height: 20px !important;
            font-family: Yekan,'B Yekan' !important;
            font-size: 14px !important;
            font-weight: bold !important;
            padding-right: 11px !important;
            color: #555555 !important;
            padding-bottom:5px;
        }

        .rcbItem, rcbHovered {
            font-family: Yekan,'B Yekan' !important;
            font-size: 13px !important;
            font-weight: bold !important;
            color: #555555 !important;
            padding-bottom:5px;
        }

        .RadComboBoxDropDown_Default .rcbHovered {
            background-color: #2fa4e7 !important;
            color: white !important;
            font-family: Yekan,'B Yekan' !important;
            font-weight: bold !important;
            padding-bottom:5px;
        }
        .labelMargin {margin-right:10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
     <asp:Literal ID="pt" runat="server"></asp:Literal>

    
    <script>
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl">
        <div>
            <h3>جستجوی دانشجو</h3>
            <br />            
            <asp:Panel ID="panelSearch" runat="server" DefaultButton="btnSearch"> 
            <div class="row">               
                <div class="col-md-4">
                رشته: <telerik:RadComboBox ID="cmbReshte" runat="server"  HighlightTemplatedItems="True" width="75%" MarkFirstMatch="True" Filter="Contains"  AllowCustomText="true" ExpandDirection="Down" Culture="(Default)" OnSelectedIndexChanged="cmbReshte_SelectedIndexChanged" ></telerik:RadComboBox>
                </div>
                 <div class="col-md-2">
                سال ورود: <asp:TextBox ID="txtSalVorood"  runat="server" MaxLength="11" Width ="100px" Height="25px" dir ="rtl" CssClass="marginItem" TabIndex="0" AutoCompleteType="Disabled"></asp:TextBox>
                 </div>
                <div class="col-md-1">
                    
                </div>
              <div class="col-md-3">
                مقطع: <telerik:RadComboBox ID="cmbMaghta" runat="server" Width="60%" AllowCustomText="false" OnSelectedIndexChanged="cmbMaghta_SelectedIndexChanged"></telerik:RadComboBox>                      
                </div>
            </div>
                <br />
            <div class="row">
                  <div class="col-md-3">
                    وضعیت: <telerik:RadComboBox ID="cmbVaziat" width="60%" runat="server" AllowCustomText="false" OnSelectedIndexChanged="cmbVaziat_SelectedIndexChanged" style="height: 16px"></telerik:RadComboBox>
                      </div>
                <div class="col-md-1">         
                    
                </div>
                <div class="col-md-3">
                    مجوز: <telerik:RadComboBox ID="cmbMojavez" runat="server" AllowCustomText="false" OnSelectedIndexChanged="cmbMojavez_SelectedIndexChanged"></telerik:RadComboBox>
                </div>
                <div class="col-md-1">
                    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="btn btn-primary" OnClick="btnSearch_Click" />       
                </div>
                <div class="col-md-2">
                    
                </div>
            </div>               
                
            <br />           
            </asp:Panel>
        </div>
    </div>
    <br />
    <div dir="rtl" class="row">
        <telerik:RadGrid ID="grdResult" runat="server" Width="100%" PagerStyle-Mode="NumericPages" PagerStyle-Position="Bottom" AllowPaging ="True" CellSpacing="0" GridLines="None" OnNeedDataSource="grdResult_NeedDataSource" EnableEmbeddedSkins="false" Skin="MyCustomSkin" >

<PagerStyle Mode="NumericPages"></PagerStyle>
        </telerik:RadGrid>
    </div>
    <br />
    <div class="row">
                    <div class="col-md-4">
                        
                    </div>
                    <div class="col-md-2">
                        <asp:Button runat="server" ID="btnExportToExcel" Text="دریافت فایل Excel" CssClass="btn btn-success" OnClick="btnExportToExcel_Click" Visible="false" />                        
                    </div>
                </div>
    
    <uc1:AccessControl ID="AccessControl1" runat="server" />
</asp:Content>
