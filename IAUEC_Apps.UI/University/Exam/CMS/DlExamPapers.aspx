<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="DlExamPapers.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.DlExamPapers" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .passwordText {
            direction: ltr;
        }

        .copyText {
            background: transparent;
            border: none;
            cursor: pointer;
        }

        #snackbar {
            visibility: hidden;
            min-width: 250px;
            margin-left: -125px;
            background-color: #333;
            color: #fff;
            text-align: center;
            border-radius: 2px;
            padding: 16px;
            position: fixed;
            z-index: 1;
            left: 50%;
            bottom: 30px;
            font-size: 17px;
        }

            #snackbar.show {
                visibility: visible;
                -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
                animation: fadein 0.5s, fadeout 0.5s 2.5s;
            }

        @-webkit-keyframes fadein {
            from {
                bottom: 0;
                opacity: 0;
            }

            to {
                bottom: 30px;
                opacity: 1;
            }
        }

        @keyframes fadein {
            from {
                bottom: 0;
                opacity: 0;
            }

            to {
                bottom: 30px;
                opacity: 1;
            }
        }

        @-webkit-keyframes fadeout {
            from {
                bottom: 30px;
                opacity: 1;
            }

            to {
                bottom: 0;
                opacity: 0;
            }
        }

        @keyframes fadeout {
            from {
                bottom: 30px;
                opacity: 1;
            }

            to {
                bottom: 0;
                opacity: 0;
            }
        }

        .btn-full-with {
            width: 100%;
        }

        .loadingWrapper {
            position: fixed;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background: rgba(0,0,0,0.7);
            direction: rtl;
            display: none;
            z-index: 9999;
        }

        .loadingInner {
            width: 300px;
            height: 300px;
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            color: #fff;
            text-align: center;
            margin: 150px auto;
        }

            .loadingInner img {
                width: 200px;
                height: 200px;
                text-align: center;
            }

            .loadingInner div {
                margin-top: 20px;
                font-size: 26px;
            }

        .btn-streach {
            width: 90%;
        }
    </style>

    <script>
        $(function () {

            $('.copyText').click(function (e) {
                e.preventDefault();
                $(this).select();
                document.execCommand("copy");
                $('#snackbar').addClass('show');
                setTimeout(function () {
                    $('#snackbar').removeClass('show');
                }, 3000);
            });


            $('.coverLayer').click(function (e) {
                $('.loadingWrapper').show();
            });

        });

        function hideLoading() {        
            $('.loadingWrapper').hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
    <script>
        function CallBackConfirm(arg) {
            if (arg)
                window.location.href = "../../../../CommonUI/CommonCmsIntro.aspx"
        }
    </script>
    <div dir="rtl">

        <div class="loadingWrapper">
            <div class="loadingInner">
                <img src="../MasterPages/images/loading.gif" />
                <div>در حال دانلود سوالات لطفا منتظر بمانید ...</div>
            </div>
        </div>

        <asp:Panel ID="pnl_Main" runat="server" HorizontalAlign="center" BorderColor="Black" BorderStyle="Groove">
            <telerik:RadGrid ID="grd_CourseList" runat="server" AutoGenerateColumns="false" OnItemCommand="grd_CourseList_ItemCommand" OnItemDataBound="grd_CourseList_ItemDataBound" OnNeedDataSource="grd_CourseList_NeedDataSource" EnableEmbeddedSkins="False" Skin="MyCustomSkin">
                <MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="did" HeaderText="مشخصه کلاس">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="namedars" HeaderText="نام درس">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ostad" HeaderText="نام خانوادگی و نام استاد">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="dateexam" HeaderText="تاریخ امتحان">
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn AllowFiltering="false" AllowSorting="false" HeaderText="اکتیو" Visible="False">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdn_IsActive" runat="server" Value='<%#Eval("IsActive") == DBNull.Value ? "0":Eval("IsActive") %>' />
                                <asp:HiddenField ID="hdn_ApproveNewHeader" runat="server" Value='<%#Eval("ApproveNewHeader") == DBNull.Value ? null : Eval("ApproveNewHeader") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>



                        <telerik:GridBoundColumn DataField="saateexam" HeaderText="ساعت امتحان">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="false" DataField="AnswerSheetType" HeaderText="نوع پاسخنامه">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <div class="btn-wrappper">
                                    <div class="row">
                                        <asp:Button ID="btnDlQuestionMergedFiles" runat="server" CommandName="DlQuestionMergedFiles" Text="دانلود سوالات به همراه اطلاعات دانشجویان" CommandArgument='<%#Eval("did") +","+ Eval("cityId").ToString()+","+ Eval("q1Status").ToString()+","+ Eval("q2Status").ToString() %>' CssClass="btn btn-success coverLayer btn-streach" Visible="false"/>
                                    </div>
                                    <div class="row">
                                        <asp:Button ID="btnDlQuestionSinglePage" runat="server" CommandName="DlQuestionSinglePage" Text="دانلود سوالات بدون اطلاعات دانشجو" CommandArgument='<%#Eval("did") +","+ Eval("cityId").ToString()+","+ Eval("q1Status").ToString()+","+ Eval("q2Status").ToString() %>' CssClass="btn btn-success coverLayer btn-streach" Visible="false" />
                                    </div>
                                    <div class="row">
                                        <asp:Button ID="btnDlQuestionMainFormat" runat="server" CommandName="DlQuestionMainFormat" Text="دانلود سوالات با فرمت اصلی" CommandArgument='<%#Eval("did") +","+ Eval("cityId").ToString()+","+ Eval("q1Status").ToString()+","+ Eval("q2Status").ToString() %>' CssClass="btn btn-success btn-streach" Visible="false" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>


                        <telerik:GridTemplateColumn>
                            <HeaderTemplate>پسورد فایل زیپ</HeaderTemplate>
                            <ItemTemplate>
                                <%-- <asp:CheckBox ID="chk2" runat="server" Checked='<%#Eval("AnswerSheet2") %>' Visible="false" />
                                <asp:CheckBox ID="chk3" runat="server" Checked='<%#Eval("AnswerSheet3") %>' Visible="false" />--%>
                                <asp:HiddenField ID="hdn_Pass" runat="server" Value='<%#Eval("Password") %>' />
                                <span class="passwordText">
                                    <asp:TextBox runat="server" CssClass="copyText" ID="lbl_Password" ReadOnly="true"></asp:TextBox></span><%--<asp:Label ID="lbl_Password" runat="server"></asp:Label>--%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div id="snackbar">پسورد کپی شد.</div>
        </asp:Panel>
    </div>
    <div>

        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />
    </div>



</asp:Content>
