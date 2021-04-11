<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master"
    AutoEventWireup="true" CodeBehind="ChangeExamDateByParams.aspx.cs"
    Inherits="IAUEC_Apps.UI.University.Exam.CMS.ChangeExamDateByParams" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>


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

        .rwin .row {
            margin-bottom: 10px;
            background-color: #f5f5f5;
            height: 35px;
        }

        .rwin .row .col-sm-3 {
            font-size: 16px;
            font-weight: bold;
            color: #6b6b6b;
        }

        .rwin .row .col-sm-9 {
            font-size: 14px;
            font-weight: bold;
            color: #12266f;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <script>

        function showLightBox() {
            $('#lightBox').show();
        }

        function hideLightBox() {
            $('#lightBox').hide();
        }

        function openRadWindow() {
            var window = $find('<%=RadWindow1.ClientID %>');
            window.show();
        }

        function closeRadWindow() {
            var window = $find('<%=RadWindow1.ClientID %>');
            window.close();
        }

        function refresgGrid() {
            $("#<%=btnRefreshGrid.ClientID%>").click();
        }


    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>

    <div id="lightBox"></div>
    <telerik:RadWindow ID="RadWindow1" Height="600" Width="750" AutoSizeBehaviors="HeightProportional" runat="server" OnClientClose="hideLightBox" CssClass="rwin">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <asp:HiddenField ID="hdnTerm" runat="server" />
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">کد مشخصه درس  : </div>
                                <div class="col-sm-9">
                                    <asp:Label runat="server" ID="lblDid" />
                                </div>
                            </div>
                        </div>

                        <div class="row ">
                            <div class="col-sm-12">
                                <div class="col-sm-3">شناسه سوال : </div>
                                <div class="col-sm-9">
                                    <asp:Label runat="server" ID="lblQID" />
                                </div>
                            </div>
                        </div>
                                          
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">نام درس : </div>
                                <div class="col-sm-9">
                                    <asp:HiddenField ID="hdn_CourseCode" runat="server" />
                                    <asp:Label runat="server" ID="lblCourseName" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">نام استاد  : </div>
                                <div class="col-sm-9">
                                    <asp:HiddenField ID="hdn_ProfCode" runat="server" />
                                    <asp:Label runat="server" ID="lblProfName" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">تاریخ امتحان  : </div>
                                <div class="col-sm-9">
                                    <asp:Label runat="server" ID="lblExamDate" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">ساعت امتحان: </div>
                                <div class="col-sm-9">
                                    <asp:Label runat="server" ID="lblExamTime" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">موبایل استاد: </div>
                                <div class="col-sm-9">
                                    <asp:Label runat="server" ID="lblProfMobile" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">تاریخ جدید امتحان   :</div>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtNewExamDate" MaxLength="8" CssClass="form-control" placeholder="99/01/01" />
                                </div>
                            </div>
                        </div>

                        <%--<div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">ساعت جدید امتحان   :</div>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtNewExamTime" MaxLength="5" CssClass="form-control" placeholder="13:30" />
                                </div>
                            </div>
                        </div>--%>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-3">علت لغو امتحان : </div>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtExamReasonCancelation" CssClass="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <asp:Button ID="btnSaveNewExamDateForCanceledDids" Text="ذخیره" OnClick="btnSaveNewExamDateForCanceledDids_Click" CssClass="btn btn-success" runat="server" />
                            <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_Click" CssClass="hidden" Text="RefreshGrid" runat="server" />
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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
                        <asp:DropDownList ID="ddl_Saat" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_Saat_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnChangeExamDateForAllDids" CssClass="btn btn-success btnSave" OnClick="btnChangeExamDateForAllDids_Click" runat="server" Text="لغو همه " />
                </div>
            </div>
        </div>
    </div>
    <div dir="rtl" class="topMargin">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <telerik:RadGrid ID="RadG_AllDids" runat="server" AutoGenerateColumns="false" PageSize="100" AllowPaging="true" AllowFilteringByColumn="true"
                    OnItemCommand="RadG_AllDids_ItemCommand"
                    OnNeedDataSource="RadG_AllDids_NeedDataSource"
                    EnableEmbeddedSkins="False" Skin="MyCustomSkin">
                    <MasterTableView>
                        <HeaderStyle HorizontalAlign="Center"/>
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="examQuestionID" HeaderText="شناسه سوال امتحانی" AllowFiltering="false" >
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="did" HeaderText="مشخصه کلاس" AllowFiltering="true">
                            </telerik:GridBoundColumn>

                              <telerik:GridBoundColumn DataField="courseCode" HeaderText="کد درس" AllowFiltering="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="courseName" HeaderText="نام درس" AllowFiltering="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="profCode" HeaderText="کد استاد" AllowFiltering="true">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="profName" HeaderText="نام خانوادگی و نام استاد" AllowFiltering="true">
                            </telerik:GridBoundColumn>                           
                          

                            <telerik:GridBoundColumn DataField="examDate" HeaderText="تاریخ امتحان" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="examTime" HeaderText="ساعت امتحان" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="newExamDate" HeaderText="تاریخ جدید امتحان" AllowFiltering="true">
                            </telerik:GridBoundColumn>

                              <telerik:GridBoundColumn DataField="term" HeaderText="ترم" AllowFiltering="false" ItemStyle-CssClass="" >
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="mobile" HeaderText="موبایل استاد" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                             <telerik:GridBoundColumn DataField="cityId" HeaderText="کد شهر" AllowFiltering="false" Visible="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="cityName" HeaderText="بارگزاری برای" AllowFiltering="false">
                            </telerik:GridBoundColumn>

                           

                            <telerik:GridTemplateColumn HeaderText="تغییر تاریخ امتحان" AllowFiltering="false" UniqueName="operator">
                                <ItemTemplate>
                                    <asp:Button ID="btnPaymentHasDone" runat="server" CommandName="CancleExam"
                                        CommandArgument='<%#Eval("examQuestionID").ToString()+","+ Eval("did").ToString()+"," + Eval("term").ToString()+","+Eval("examDate").ToString()+","+Eval("examTime").ToString()+","+Eval("courseCode").ToString()+","+Eval("courseName").ToString()+","+Eval("profCode").ToString()+","+Eval("profName").ToString()+","+Eval("mobile").ToString()+","+Eval("newExamDate").ToString() %>'
                                        Text="لغو" ToolTip="لغو" CssClass="btn btn-info" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>



</asp:Content>
