<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/Exam/MasterPages/BlankMaster.Master" CodeBehind="Questions.aspx.cs" Inherits="IAUEC_Apps.UI.University.Evaluation.Questions" %>



<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .txt-rigth {
            text-align: right;
        }

        .with-m {
            width: 50px;
        }

        .txt-question {
            width: 60%;
            color: red;
        }

        .tr-header {
            background-color: #EEEEEE;
        }

        .txt-center {
            text-align: center;
        }

        .k-radio, input.k-checkbox {
            display: inline;
            opacity: 0;
            width: 0;
            margin: 0;
            -webkit-appearance: none;
            overflow: hidden;
        }

        .k-checkbox-label {
            display: inline-block;
            position: relative;
            padding-left: 25.2px;
            vertical-align: text-top;
            line-height: 16px;
            cursor: pointer;
            border-style: solid;
            border-width: 0;
            cursor: pointer;
        }

            .k-checkbox-label:before {
                content: "";
                position: absolute;
                top: 0;
                left: 0;
                border-width: 1px;
                border-style: solid;
                width: 14px;
                height: 14px;
                font-size: 14px;
                line-height: 14px;
                text-align: center;
            }

            .k-checkbox-label:before {
                border-color: #ccc;
                background: #fff;
                border-radius: 3px;
            }

        .k-checkbox:checked + .k-checkbox-label:before {
            content: "\2713";
        }

        .container-last-question {
            border: 1px solid;
        }

        .tbody-user-info, td {
            height: 35px;
        }

        .thead-user-info, th {
            text-align: center;
        }

        .container-user-info {
            display: none;
        }

        .btn-success {
            display: none;
            min-width: 100px;
        }

        .container-success-message {
            text-align: right;
            display: none;
        }

        .container-error-message {
            text-align: right;
            display: none;
        }
    </style>

    <div class="panel panel-info">
        <div class="panel-heading">
        </div>
        <div class="panel-body">
            <div class="body-container">

                <table class="table table-striped table-bordered  table-questions">
                    <thead class="table-thead"></thead>
                    <tbody></tbody>
                </table>
                <div class="container-last-question">
                </div>
                <div class="container-description" style="display: none;">
                    <textarea id="userComment" placeholder="نظرات و پیشنهادات خود را در جهت ارتقای واحد الکترونیکی بیان نمایید:" class="form-control" style="border: 1px solid;" rows="7"></textarea>
                </div>
                <%--    <div style="margin-top:20px;" class="container-user-info">
                    <div style="margin-bottom:7px;">
                        مشخصات تکمیل کننده
                    </div>
                    <div>
                        <table class="table table-striped table-bordered ">
                            <thead class="thead-user-info">
                                <tr>
                                    <th>نام
                                    </th>
                                    <th>نام خانوادگی
                                    </th>
                                    <th>شماره دانشجویی
                                    </th>
                                    <th>شغل(سمت)
                                    </th>
                                    <th>(سازمان یا صنف)
                                    </th>
                                    <th>شماره موبایل
                                    </th>
                                </tr>
                            </thead>
                            <tbody class="tbody-user-info">
                                <tr>
                                    <td id="user-name"></td>
                                    <td id="user-family"></td>
                                    <td id="user-stcode"></td>
                                    <td id="user-job"></td>
                                    <td id="user-cenfe"></td>
                                    <td id="user-mobile"></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>--%>
                <div class="row" style="padding: 3px;">
                    <a class="btn btn-success" style="width: 120px;">ثبت </a>
                </div>
            </div>
            <div class="container-success-message">
                <div class="alert alert-success">
                    نظر سنجی شما با موفقیت ثبت شد.
                </div>
            </div>
            <div class="container-error-message">
                <div class="alert alert-danger">
                    خطایی هنگام ثبت اطلاعات اتفاق افتاده است.
                </div>
            </div>
        </div>
    </div>

    <script>
        var questionCount = 0;
        $(function () {
            getQuestion();

           
        });
        function getQuestion() {
            $.ajax({
                type: "POST",
                url: "/University/Evaluation/Questions.aspx/GetQuestions",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    var data = res.d;
                    console.log(res.d);

                    if (data.length >= 1) {
                        var $tableHead = $(".table-thead");
                        generateAnswerOfQuestions($tableHead, data);

                    }
                },
                Error: function () {
                    console.log("error")
                }
            });
        }
        function generateAnswerOfQuestions($tableHead, data) {
            for (var i = 0; i < data.length; i++) {

                if (data[i].SubQuestionDTOs.length >= 1 && !data[i].IsLastQuestion) {
                    var $tr = $("<tr class='tr-header'><th class='txt-rigth with-m'></th>" +
                        "<th class='txt-rigth txt-question'>* " + data[i].Text + "</th>"
                        + "</tr>");
                    if (data[i].AnswerOfQuestions.length >= 1) {

                        for (var j = 0; j < data[i].AnswerOfQuestions.length; j++) {
                            var td = "<th class='txt-center'>" + data[i].AnswerOfQuestions[j].Text + "</th>";
                            $tr.append(td);
                        }
                    }
                    $tableHead.append($tr);
                    generateSubQuestionDTOs($tableHead, data, i);
                   
                } else if (!data[i].IsLastQuestion) {
                    var $tr = $("<tr id=" + data[i].Id + " class='tr-header'><th class='txt-rigth with-m'></th>" +
                        "<th class='txt-rigth txt-question'>* " + data[i].Text + "</th>"
                        + "</tr>");
                    questionCount++;
                    if (data[i].AnswerOfQuestions.length >= 1) {


                        for (var j = 0; j < data[i].AnswerOfQuestions.length; j++) {
                            var id = (data[i].Id + data[i].AnswerOfQuestions[j].Id);
                            var td = "<th class='txt-center'><input id=" + id + " style='cursor:pointer;' class='k-checkbox' data-type=" + data[i].AnswerOfQuestions[j].Id + " data-value=" + data[i].Id + ",0," + data[i].AnswerOfQuestions[j].Id + " onchange='handleClick(" + data[i].Id + "," + data[i].AnswerOfQuestions[j].Id + ")'  type='checkbox' value='0'/><label style='padding-top: 1px;margin-left: 49px;' for=" + id + " class='k-checkbox-label'>" + data[i].AnswerOfQuestions[j].Text + "</label></th>";
                            $tr.append(td);
                        }
                    }
                    $tableHead.append($tr);
                    generateSubQuestionDTOs($tableHead, data, i);
                } else {
                    generateLastQuestion(data[i], i);
                    questionCount++;
                }
                $(".container-description").css("display", "block");
                $(".container-user-info").css("display", "block");
                $(".btn-success").css("display", "block");

                //setUserInfo();
            }
        }

        function generateSubQuestionDTOs($tableHead, data, i) {
            if (data[i].SubQuestionDTOs.length >= 1) {
                for (var j = 0; j < data[i].SubQuestionDTOs.length; j++) {
                    questionCount++;
                    if (data[i].SubQuestionDTOs[j].QuestionId == data[i].Id) {
                        var $tr = $("<tr id=" + data[i].SubQuestionDTOs[j].Id + "><td class='txt-rigth with-m'>" + (j + 1) + "</td>" +
                            "<td class='txt-rigth'>" + data[i].SubQuestionDTOs[j].Text + "</td>"
                            + "</tr>");
                        for (var z = 0; z < data[i].AnswerOfQuestions.length; z++) {
                            debugger;
                            var id = (data[i].SubQuestionDTOs[j].Id * data[i].AnswerOfQuestions[z].Id);
                            var td = "<td class='txt-center'><input id=" + id + " style='cursor:pointer;' class='k-checkbox' data-type=" + data[i].AnswerOfQuestions[z].Id + " data-value=" + data[i].SubQuestionDTOs[j].QuestionId + "," + data[i].SubQuestionDTOs[j].Id + "," + data[i].AnswerOfQuestions[z].Id + " onchange='handleClick(" + data[i].SubQuestionDTOs[j].Id + "," + data[i].AnswerOfQuestions[z].Id + ")'  type='checkbox' value='0'/><label for=" + id + " class='k-checkbox-label'></label></td>";
                            //var td = "<td class='txt-center'><label><input data-type=" + data[i].AnswerOfQuestions[z].Id + " data-value=" + data[i].SubQuestionDTOs[j].Id + "," + data[i].AnswerOfQuestions[z].Id + " onchange='handleClick(" + data[i].SubQuestionDTOs[j].Id + "," + data[i].AnswerOfQuestions[z].Id + ")' type='checkbox' name='check'><span class='label-text'></span></label></td>";
                            //var td = "<td class='txt-center'><input data-type=" + data[i].AnswerOfQuestions[z].Id + " data-value=" + data[i].SubQuestionDTOs[j].Id + "," + data[i].AnswerOfQuestions[z].Id + " onchange='handleClick(" + data[i].SubQuestionDTOs[j].Id + "," + data[i].AnswerOfQuestions[z].Id + ")'  type='checkbox' value='0'/></td>";
                            $tr.append(td);
                        }
                        $tableHead.append($tr);
                    }

                }
            }

        }
        function handleClick(trId, idCheckBox) {
            debugger;
            var $tr = $("#" + trId);
            $tr.find('input[type="checkbox"]').each(function () {
                debugger;
                if ($(this).attr("data-type") != idCheckBox) {

                    $(this).prop('checked', false);
                }

            });
        }

        function generateLastQuestion(data, i) {
            //$(".container-last-question").css("display", "block !important;");
            debugger;
            var $containerLastQuestion = $('.container-last-question');
            var $containerLastQuestionTitle = $("<div style='border: 1px solid;padding: 7px;'>" + data.Text + "</div>");
            $containerLastQuestion.append($containerLastQuestionTitle);
            for (var j = 0; j < data.AnswerOfQuestions.length; j++) {
                debugger;
                var id = (data.AnswerOfQuestions[j].Id + data.Id);
                var $containerAnswer = $("<div style='display:inline-table;border-left:1px solid;padding: 5px;'><input id=" + id + " data-value=" + data.Id + ",0," + data.AnswerOfQuestions[j].Id + " class='k-checkbox' type ='checkbox' /> <label style='padding-top: 1px;' for=" + id + " class='k-checkbox-label'>" + data.AnswerOfQuestions[j].Text + "</label></div>");
                $containerLastQuestion.append($containerAnswer);
            }

        }
     

        $(".btn-success").click(function () {
            $(this).css("display", "none");
            var answerArray = [];
            $('.table-questions .k-checkbox:checkbox:checked').each(function () {
                var dataValue = $(this).attr("data-value");
                answerArray.push(dataValue);
            });
            $('.container-last-question .k-checkbox:checkbox:checked').each(function () {
                var dataValue = $(this).attr("data-value");
                answerArray.push(dataValue);
            });
            var userComment = $("#userComment").val();
            debugger;
            if (answerArray.length >= questionCount) {
                debugger;
                saveAnswer(answerArray, userComment);
            } else {
                alert("شما به تمامی سوالات جواب نداده اید.");
                $(this).css("display", "block");
            }
            
        });

        function saveAnswer(answers, comment) {
            debugger;
            var userId = $("#UserIdField").val();
            //var model = { Answers: answers, LastAnswer: lastAnswer, Comments: comments };
            //var data = JSON.stringify({ Answers: '1', LastAnswer: '2', Comments: '3' });
            $.ajax({
                type: "POST",
                url: "/University/Evaluation/Questions.aspx/InsertStudentAnswer",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ Answers: answers, Comment: comment, userId: userId }),
                success: function (res) {
                    parent.CloseModal();
                },
                Error: function () {
                    $(".body-container").css("display", "none");
                    $(".container-error-message").css("display", "block");
                }
            });
        }

    </script>

</asp:Content>

