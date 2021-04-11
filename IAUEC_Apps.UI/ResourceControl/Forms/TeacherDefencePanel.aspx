<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/ResourceControlUsers.Master"
    CodeBehind="TeacherDefencePanel.aspx.cs" Inherits="ResourceControl.PL.Forms.TeacherDefencePanel" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <h3>پنل مرتبط با دفاع استاد</h3>
    <script type="text/javascript"></script>
    <style>
        /*class is in masterpage*/
        .dashboard_graph {
            background-color: #eae9e9;
        }
        /*====================**/
        .main {
            margin-top: 100px;
            margin-bottom: 200px;
            direction: rtl;
        }

        .r {
            width: 60%;
            margin: 15px auto;
        }

        .flip-card-wrapper {
            margin: 15px;
        }

        .flip-card {
            width: 100%;
            height: 200px;
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
            background-color: #045802;
        }

        .altbg3 {
            background-color: #714d0f;
        }

        .altbg4 {
            background-color: #7e0000;
        }

        .flip-card-back {
            background-color: #2980b9;
            transform: rotateY(180deg);
        }

            .flip-card-front h1, .flip-card-back h1 {
                color: #fff;
            }

        .flip-card-front p {
            font-size: 30px;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%--   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <telerik:RadWindowManager ID="rwm_message" runat="server"></telerik:RadWindowManager>

    <div class="main">
        <div class="row r">

            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <a id="a_DefenceReservationConcordance" runat="server" onserverclick="a_DefenceReservationConcordance_ServerClick">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg1">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>هماهنگی رزو دفاع</p>
                                </div>
                                <div class="flip-card-back">
                                    <h1>کلیک کنید....</h1>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <a id="a_ClassReservationConcordance" runat="server" onserverclick="a_ClassReservationConcordance_ServerClick">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg2">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>هماهنگی رزو کلاس</p>
                                </div>
                                <div class="flip-card-back">
                                    <h1>کلیک کنید....</h1>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>


        </div>

        <div class="row r">

            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <a id="a_ChattingTeacherToStudent" runat="server" onserverclick="a_ChattingTeacherToStudent_ServerClick">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg3">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>گفتگو در خصوص پایان نامه با دانشجو</p>
                                </div>
                                <div class="flip-card-back">
                                    <h1>کلیک کنید....</h1>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <a id="a_AssistanceRequestForDefenceConcordance" runat="server" onserverclick="a_AssistanceRequestForDefenceConcordance_ServerClick">
                    <div class="flip-card-wrapper">
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front altbg4">
                                    <%--<img src="assets/images/03.png" alt="Avatar" style="width:300px;height:300px;">--%>
                                    <p>درخواست مساعدت هماهنگی دفاع</p>
                                </div>
                                <div class="flip-card-back">
                                    <h1>کلیک کنید....</h1>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
        </div>


    </div>
</asp:Content>
