<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master"
    AutoEventWireup="true" CodeBehind="DownLoadExamQuestionsPermission.aspx.cs"
    Inherits="IAUEC_Apps.UI.University.Exam.CMS.DownLoadExamQuestionsPermission"%>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl"%>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .rtlDir {
            direction: rtl;
        }

        .topMargin {
            margin-top: 10px;
        }

        .btnSave {
            width: 117px;
        }

        .hidden {
            display: none;
        }

        #lightBox {
            display: none;
            position: fixed;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background: rgba(0,0,0,0.5);
            z-index: 3000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <script>

        function openRadWindow() {
            var window = $find('<%=RadWindowConfirm.ClientID %>');
            window.show();
        }

        function closeRadWindowConfirm() {
            setTimeout(() => { hideLightBox(); }, 100);
            var window = $find('<%=RadWindowConfirm.ClientID %>');
            window.hide();
        }

        function showLightBox() {
            $('#lightBox').show();
        }

 

    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
    <div id="lightBox"></div>
    <telerik:RadWindow ID="RadWindowConfirm" Height="350" Width="500" AutoSizeBehaviors="HeightProportional" runat="server" OnClientClose="closeRadWindowConfirm">
        <ContentTemplate>
            <div class="container" dir="rtl" style="padding: 10px">
                <div class="row">
                    <div class="col-sm-12">
                        <h4 class="alert alert-danger" style="text-align: center">آیا از انتخاب همه اطمینان دارید ؟ </h4>
                    </div>
                </div>
                <br />
                <asp:Button ID="btnConfirmation" Text="تاییـــد" OnClick="btnConfirmation_Click" CssClass="btn btn-success btnSave" runat="server" />
            </div>
        </ContentTemplate>
    </telerik:RadWindow>

    <div class="container-fluid" dir="rtl">
        <div class="row topMargin ">
            <div class="col-md-12 justify-content-between">

                <div class="col-md-9">
                    <div class="col-md-1">شهر : </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddl_Cities" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <div class="col-md-1">روز آزمون : </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddl_ExamDate" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_ExamDate_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                   
                    <div class="col-md-1">ساعت آزمون : </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddl_Saat" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_Saat_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                    </div>                  
                </div>
                <div class="col-md-3">
                    <div class="col-md-3">
                        <asp:Button ID="btnSave" CssClass="btn btn-success btnSave" OnClick="btnSave_Click" runat="server" Text="ذخیره " />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div dir="rtl" class="topMargin">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <telerik:RadGrid ID="RadG_QuestionList4Download" runat="server" AutoGenerateColumns="false" PageSize="100" AllowPaging="true" AllowFilteringByColumn="true"
                    OnItemDataBound="RadG_QuestionList4Download_ItemDataBound"
                    OnNeedDataSource="RadG_QuestionList4Download_NeedDataSource"
                    EnableEmbeddedSkins="False" Skin="MyCustomSkin">
                    <MasterTableView>
                        <HeaderStyle HorizontalAlign="Center" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="ID" HeaderText="مشخصه سوال امتحانی" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>                           

                            <telerik:GridBoundColumn DataField="Term" HeaderText="ترم" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ProfCode" HeaderText="کد استاد" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ProfName" HeaderText="نام خانوادگی و نام استاد" AllowFiltering="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="Did" HeaderText="مشخصه کلاس" AllowFiltering="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="CourseName" HeaderText="نام درس" AllowFiltering="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ExamDate" HeaderText="تاریخ امتحان" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="ExamTime" HeaderText="ساعت امتحان" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn HeaderText="وضعیت"  Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdn_IsActive" runat="server" Value='<%#Eval("IsActive").ToString()%>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>



                            <telerik:GridTemplateColumn UniqueName="colChkDid" HeaderText="انتخاب همه" AllowFiltering="false">
                                <HeaderTemplate>
                                    <asp:CheckBox Text="انتخاب همه " runat="server" ID="chkSelectAll" CssClass="chkSelectAll" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDid" runat="server" />

                                    <asp:HiddenField ID="hdn_ID" runat="server" Visible="false" Value='<%# Eval("ID").ToString() %>' />
                                    <asp:HiddenField ID="hdn_Term" runat="server" Visible="false" Value='<%# Eval("Term").ToString() %>' />
                                    <asp:HiddenField ID="hdn_ProfCode" runat="server" Visible="false" Value='<%# Eval("ProfCode").ToString() %>' />
                                    <asp:HiddenField ID="hdn_ProfName" runat="server" Visible="false" Value='<%# Eval("ProfName").ToString() %>' />
                                    <asp:HiddenField ID="hdn_Did" runat="server" Visible="false" Value='<%# Eval("Did").ToString() %>' />
                                    <asp:HiddenField ID="hdn_CourseName" runat="server" Visible="false" Value='<%# Eval("CourseName").ToString() %>' />
                                    <asp:HiddenField ID="hdnExamDate" runat="server" Visible="false" Value='<%# Eval("ExamDate").ToString() %>' />
                                    <asp:HiddenField ID="hdnExamTime" runat="server" Visible="false" Value='<%# Eval("ExamTime").ToString() %>' />
                                </ItemTemplate>

                            </telerik:GridTemplateColumn>


                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
