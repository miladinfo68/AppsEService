<%@ Page Title="" Language="C#" MasterPageFile="~/Contact/MasterPage/MasterPageConatctSt.Master" AutoEventWireup="true" CodeBehind="StudentResearchAffairsPanel.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.StudentResearchAffairsPanel" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <h3>امور پژوهشی</h3>
    <script type="text/javascript"></script>
    <style>
        /*class is in masterpage*/
        .dashboard_graph {
            background-color: #eae9e9;
        }

        .main {
            margin-top: 100px;
            margin-bottom: 200px;
            direction: rtl;
        }

        .r {
            width: 100%;
            margin: 15px auto;
        }

        .flip-card-wrapper {
            margin: 15px;
        }

        .flip-card {
            width: 100%;
            height: 120px;
            perspective: 1000px;
            display: inline-block;
            border-radius: 5px;
        }

        .flip-card-inner {
            position: relative;
            width: 100%;
            height: 100%;
            transition: transform 0.6s;
            border-radius: 5px;
            transform-style: preserve-3d;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
        }

        .flip-card:hover .flip-card-inner {
            transform: rotateY(180deg);
        }

        .flip-card-front, .flip-card-back {
            position: absolute;
            width: 100%;
            height: 100%;
            border-radius: 5px;
            backface-visibility: hidden;
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 15px;
            color: #fff;
        }

        .altbg1 {
            background-color: #192ad4;
        }

        .altbg2 {
            background-color: #562a2f;
        }

        .altbg3 {
            background-color: #714d0f;
        }

        .altbg4 {
            background-color: #7e0000;
        }

        .altbg5 {
            background-color: #494514;
        }

        .altbg6 {
            background-color: #46007e;
        }

        .altbg7 {
            background-color: #1e1f1c;
        }
                .altbg8 {
            background-color: #48a544;
        }

        .flip-card-back {
            background-color: #2980b9;
            transform: rotateY(180deg);
        }

            .flip-card-front h1, .flip-card-back h1 {
                color: #fff;
            }

        .flip-card-front p {
            font-size: 20px;
            text-align: center;
        }

        .flip-card-back p {
            font-size: 20px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
        <telerik:RadWindowManager ID="rwm_message" runat="server"></telerik:RadWindowManager>
    <!-- Modal -->






    <div class="main">
        <div class="row r">

            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                <a id="a_StudentAndTheacherChatting" runat="server" onserverclick="a_ChattingTeacherToStudent_ServerClick">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg1">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>گفتگو با استاد</p>
                                </div>
                                <div class="flip-card-back">
                                    <p>کلیک کنید....</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                <a id="a_PortalPajoheshi" runat="server" onserverclick="a_PortalPajoheshi_ServerClick">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg2">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>امور پروپوزال و پایان نامه</p>
                                </div>
                                <div class="flip-card-back">
                                    <p>کلیک کنید....</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                <a id="a_AudioAndVideoCommunication" data-toggle="modal" data-target="#exampleModal">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg4">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>ارتباط صوتی و تصویری</p>
                                </div>
                                <div class="flip-card-back">
                                    <p>کلیک کنید....</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
        </div>

        <div class="row r">

            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                <a id="a_DefenceMeetingConcordance" runat="server" onserverclick="a_Reserve_DefenceClass_ServerClick">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg3">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>هماهنگی جلسات دفاع</p>
                                </div>
                                <div class="flip-card-back">
                                    <p>کلیک کنید....</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                <a id="a_SendMessageToTeacher" data-toggle="modal" data-target="#exampleModal" onserverclick="a_SendMessageToTeacher_ServerClick">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg5">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>ارسال پیام به استاد</p>
                                </div>
                                <div class="flip-card-back">
                                    <p>کلیک کنید....</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                <a id="a_OnlineDefensePlayback" runat="server" onserverclick="a_OnlineDefensePlayback_ServerClick">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg6">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>برگزاری دفاع های آنلاین</p>
                                </div>
                                <div class="flip-card-back">
                                    <p>کلیک کنید....</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
        </div>

        <div class="row r">
            <div class="row r">

                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                    <a runat="server" id="AssistanceRequestForDefenceConcordance" onserverclick="a_AssistanceRequestForDefenceConcordance_ServerClick">
                        <div class="flip-card-wrapper">
                            <div class="flip-card">
                                <div class="flip-card-inner">
                                    <div class="flip-card-front altbg7">
                                        <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                        <p>درخواست مساعدت دفاع</p>
                                    </div>
                                    <div class="flip-card-back">
                                        <p>کلیک کنید....</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                
            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                <a id="aTestDefence" runat="server" onserverclick="aTestDefence_ServerClick">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg8">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>جلسات دفاع آنلاین آزمایشی</p>
                                </div>
                                <div class="flip-card-back">
                                    <p>کلیک کنید....</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
            </div>


        </div>
    </div>
</asp:Content>
