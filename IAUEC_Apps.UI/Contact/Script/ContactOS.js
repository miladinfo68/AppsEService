
$(document).ready(function () {

    ///////////////////////////audio

    //webkitURL is deprecated but nevertheless
    URL = window.URL || window.webkitURL;

    var gumStream; 						//stream from getUserMedia()
    var rec; 							//Recorder.js object
    var input; 							//MediaStreamAudioSourceNode we'll be recording

    // shim for AudioContext when it's not avb. 
    var AudioContext = window.AudioContext || window.webkitAudioContext;
    var audioContext //audio context to help us record


    $('.recordVoice').on('click', function (e) {
        e.preventDefault();
        $("input[id*=txtMsg]").attr("disabled", "disabled");
        $(".recordVoice").addClass("hidden");
        $(".stopVoice").removeClass("hidden");
        var constraints = { audio: true, video: false }
        navigator.mediaDevices.getUserMedia(constraints).then(function (stream) {
            audioContext = new AudioContext();
            gumStream = stream;
            input = audioContext.createMediaStreamSource(stream);
            rec = new Recorder(input, { numChannels: 1 });
            rec.record();
        });
    });

    $(".stopVoice").click(function (e) {
       
        e.preventDefault();
        $(".stopVoice").addClass("hidden");
        $(".recordVoice").removeClass("hidden");
        $("input[id*=txtMsg]").removeAttr("disabled");
        rec.stop();
        gumStream.getAudioTracks()[0].stop();
        rec.exportWAV(createDownloadLink);


    });

    function createDownloadLink(blob) {
        var data = new FormData();
        data.append("userID", $("span[id*=LblIdUser]").text());
        data.append("idGrp", $("span[id*=LblIdGrp]").text());
        data.append("msg", "@$sound voice@$");
        data.append("idOnChat", $("input[id*=TxtIdOnChat]").val());
        data.append("idMsgReplayed", $("span[id*=ContentPlaceHolder1_LblReplyIdD]").text());
        data.append("audio_data", blob);
        $.ajax({
            url: '../SoundStore.ashx',
            type: 'POST',
            data: data,
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
               
                //   var res = JSON.parse(result);
                AddMessage(2, result)
            }


        });
    }









    ///////////////////end audio



    $(document).on('click', '.btnInsertFile', function (e) {
       
        e.preventDefault();
        var file = $(this).parent().find('.file');
        file.trigger('click');
      
    });

    $(document).on('click', '.FindNext', function () {

        var str = document.getElementById("findField").value;
        if (str == "") {
            alert("متن جستجو خالی می باشد!");
            return;
        }
        if (str.length < 3) {
            alert("طول متن جستجو باید بیش از 2کاراکتر باشد");
            return;
        }

        if (str != $("span[id*=LblSearchText]").val()) {
            var c = $("span[id*=LblSearchCount]").val()
            for (f = 0; f < c; ++f) {
               
                var $parent = $("#searchCurText_" + f);
                $parent.contents().unwrap();
            }
            $("span[id*=LblSearchText]").val(str);
            var i = 0;
            var countSearch = 0;
            $('.dtTable tr  ').each(function () {
                var stroldMessage = $(this).find('span[id*=DtLstMesages_Message]').html();
                var indexMatchMessage = locations(str, stroldMessage);

                for (k = 0; k < indexMatchMessage.length; ++k, ++i) {
                    var spana = "<span style='background-color:yellow ' class='searchspan' id='searchCurText_" + i + "'>";

                    var spanb = "</span>";

                    $(this).find('span[id*=DtLstMesages_Message]').html(
                        addTag(stroldMessage, (indexMatchMessage[k] + (k * (spana.length + spanb.length))), spana, str.length, spanb)
                    );
                    ++countSearch;

                }
                var stroldReplayMsg = $(this).find('span[id*=DtLstMesages_LblReplayMsg]').html();
                var indexMatchReplayMsg = locations(str, stroldReplayMsg);

                for (j = 0; j < indexMatchReplayMsg.length; ++j, ++i) {

                    var spana = "<span style='background-color:yellow ' class='searchspan' id='searchCurText_" + i +
                        "'>";
                    var spanb = "</span>";
                    $(this).find('span[id*=DtLstMesages_LblReplayMsg]').html(
                        addTag(stroldReplayMsg, (indexMatchReplayMsg[j] + (j * (spana.length + spanb.length))), spana, str.length, spanb)
                    );
                    ++countSearch;
                }

            });
            if (countSearch == 0) {
                alert("متنی پیدا نشد");
            }
            $("span[id*=LblSearchCount]").val(countSearch);
            $("span[id*=LblCurCount]").val(0);
        }
       
        var curCount = $("span[id*=LblCurCount]").val();
        if (curCount == $("span[id*=LblSearchCount]").val())
            curCount = 0;

        var $container = $('.dtTable'),
            $scrollTo = $('#searchCurText_' + curCount);
        $container.scrollTop(
            $scrollTo.offset().top - $container.offset().top + $container.scrollTop()
        );

        ++curCount;
        $("span[id*=LblCurCount]").val(curCount);

    });
    function addTag(strOld, idx, stra, Length, strb) {
       
        return strOld.slice(0, idx) + stra + strOld.slice(idx, idx + Length) + strb + strOld.slice(idx + Length, strOld.length);
    };
    function locations(substring, string) {
        var a = [], i = -1;
        while ((i = string.indexOf(substring, i + 1)) >= 0) a.push(i);
        return a;
    };
    $(".file").change(function (e) {
        e.preventDefault();
       
        var f = $(".file").get(0).files;
        var a = f[0].name;
        $("#AlertUploadFile").text(a);
        $('.AlertUploadFile').modal('show');
    });

    /* 
typeMsg 
1= msg
2=voice 
3=file 
*/

    $('.acceptFileSave').on('click', function (e) {

        e.preventDefault();
       
        var data = new FormData();
        var l = document.getElementsByClassName("file");
        var s = l[0]//.files[0];
        var files = $(".file").get(0).files;
       
        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            data.append("UploadedImage", files[0]);
            data.append("userID", $("span[id*=LblIdUser]").text());
            data.append("idGrp", $("span[id*=LblIdGrp]").text());
            data.append("msg", "@$file send@$");
            data.append("idOnChat", $("input[id*=TxtIdOnChat]").val());
            data.append("idMsgReplayed", $("span[id*=ContentPlaceHolder1_LblReplyIdD]").text());
        }
        $.ajax({
            url: '../FileStore.ashx',
            type: 'POST',
            data: data,
            cache: false,
            contentType: false,
            processData: false,
            success: function (result) {
               
                //   var res = JSON.parse(result);
                AddMessage(3, result)
            }


        });

    });

    function AddMessage(typeMsg, res) {
        $(".recordVoice").removeClass("hidden");
        $(".stopVoice").addClass("hidden");

        //create the wav blob and pass it on to createDownloadLink

        var row = $("[id*=DtLstMesages] tr:last-child");


        var today = new Date();
        var time = DisplayTime();

        var DatePersian = DateNowShamsi();


        if (row.length > 0) {

            row = row.clone().insertAfter("[id*=DtLstMesages] tr:last-child");

            row.find("span[id*=DtLstMesages_Message]").addClass("hidden");
            row.find("div[id*=DtLstMesages_Panelaudio]").addClass("hidden");
            row.find("div[id*=DtLstMesages_Panelaudio]").addClass("hidden");
            row.find("div[id*=DtLstMesages_PanelFile] ").addClass("hidden");

            var row = $("[id*=DtLstMesages] tr:last-child");
           
            if (typeMsg == 1) {
                row.find("span[id*=DtLstMesages_Message]").text($("input[id*=txtMsg]").val());
                row.find("span[id*=DtLstMesages_Message]").removeClass("hidden");
                $("input[id*=txtMsg]").val("");
            }
            if (typeMsg == 2) {
                row.find("div[id*=DtLstMesages_Panelaudio]  audio source").attr("src", "../SoundRecorder/" + res[0].ChatID_P + ".wav");
                row.find("div[id*=DtLstMesages_Panelaudio]  ").removeClass("hidden");
                row.find("span[id*=DtLstMesages_Message]").text(" فایل صوتی ");
            }
            if (typeMsg == 3) {
                row.find("div[id*=DtLstMesages_PanelFile] span a ").attr("href", "../FileSaver/" + res[0].ChatID_P + res[0].format);
                row.find("div[id*=DtLstMesages_PanelFile]").removeClass("hidden");
                row.find("span[id*=DtLstMesages_Message]").text(" فایل ");
                row.find("div[id*=DtLstMesages_PanelFile] span a ").text(res[0].name.replace("@$file send@$", ""));
            }

            $("input[id*=txtMsg]").val("");

            row.find("span[id*=DtLstMesages_LblReplayMsg]").text($("span[id*=ContentPlaceHolder1_LblReplyMsg]").text().replace("@$sound voice@$blob", " فایل صوتی ").replace("@$file send@$", " فایل "));
            $("span[id*=ContentPlaceHolder1_LblReplyMsg]").text("");

            row.find("span[id*=DtLstMesages_LblReplayId]").text($("span[id*=ContentPlaceHolder1_LblReplyIdD]").text());


            row.find("div[id*=ContentPlaceHolder1_DtLstMesages_PanelReplayed]").attr('id', 'ContentPlaceHolder1_DtLstMesages_PanelReplayed__' + res[0].ChatID_P);
            $("span[id*=ContentPlaceHolder1_LblReplyIdD]").text("0");

            row.find("span[id*=DtLstMesages_LblChatID]").text(res[0].ChatID_P);

            row.find("img[id*=ContentPlaceHolder1_DtLstMesages_Image4]").attr("src", "data:image/jpg;base64, " + res[0].Images);

            row.find("span[id*=DtLstMesages_Label1]").text($("span[id*=ContentPlaceHolder1_LabelNameRepaly]").text());

            row.find("span[id*=ContentPlaceHolder1_LabelNameRepaly]").text("");
            row.find("span[id*=DtLstMesages_LblNameSender]").text($("span[id=stName").text());

            row.find("span[id*=DtLstMesages_Time]").text(time);
            row.find("span[id*=DtLstMesages_Date]").text(DatePersian);
            row.find("span[id*=DtLstMesages_LblSenderId]").text($("#user").text().trim());
            row.find(".trashIcon").removeClass("hidden");
            if ($("#rowReply").css("display") == "block") {
                row.find("span[id*=DtLstMesages_PanelReplayed]").attr("Visible", "true");
                row.find("div[id*=DtLstMesages_PanelReplayed]").removeClass("False");
                $("#rowReply").css("display", "none");
            }
            else {
                row.find("span[id*=DtLstMesages_PanelReplayed]").attr("Visible", "false");
                row.find("div[id*=DtLstMesages_PanelReplayed]").addClass("False");
            }
        }
        else {

            var rowManual = "<table id='ContentPlaceHolder1_DtLstMesages' cellspacing='0' style='width:100%;border-collapse:collapse;'>" +
                "<tbody><tr><td><td><div class='lv-item media'><div id='ContentPlaceHolder1_DtLstMesages_PanelReplayed_53' class='False'>" +
                "<div class='row'> <div class='col-sm-12'><div class='row'> <div class='col-sm-5'><div class='containerReplay msg_ReplayChat '>" +
                "<h5>پاسخ</h5> <div class='msg_Replay'><div class='hidden'> <span id='ContentPlaceHolder1_DtLstMesages_LblReplayId_53'>50846</span>" +
                "</div><span id='ContentPlaceHolder1_DtLstMesages_LblReplayMsg_53' style='font-size:Smaller;'>پیام پاک شده است</span> </div>" +
                "<div class='left'><span id='ContentPlaceHolder1_DtLstMesages_Label1_53' style='font-size:X-Small;'>ابراهیم عباسی</span></div>" +
                "</div> </div></div> </div> </div> </div> <div class='lv-avatar pull-right'>" +
                "<img id='ContentPlaceHolder1_DtLstMesages_Image4_53' src='' style='height:187px;width:250px;width: 30px; height: 30px'>" +
                "</div><div class='media-body'><div class='ms-item'><span class='glyphicon glyphicon-triangle-left' style='color: #000000;'></span>" +
                "<span id='ContentPlaceHolder1_DtLstMesages_LblChatID_53' class='hide'>50847</span><span id='ContentPlaceHolder1_DtLstMesages_Message_53' style='font-size:Smaller;'>پیام پاک شده است</span>" +
                "<div id='ContentPlaceHolder1_DtLstMesages_Panelaudio_53' class='hidden'><audio controls='' style='max-width:200px'><source src='../SoundRecorder/50847.wav' type='audio/mpeg'></audio>" +
                "</div><div id='ContentPlaceHolder1_DtLstMesages_PanelFile_53' class='hidden'> <span><a class='btn btn-link' style='width: 100%' href='../FileSaver/50847.wav' download=''>پیام پاک شده است</a>" +
                " </span></div> </div><span class='glyphicon  glyphicon-reply '><i class='fa fa-reply replyIcon' data-toggle='tooltip' data-placement='left' title='پاسخ' aria-hidden='true'></i><span class='sr-only'>پاسخ</span>" +
                " <i class='fa fa-trash  trashIcon ' data-target='.deleteChat' aria-hidden='true' data-toggle='tooltip' data-placement='left' title='حذف'></i><span class='sr-only'>حذف</span></span>" +
                "<small class='ms-date'><small class='ms-date'><i class='fa fa-clock' aria-hidden='true'></i><span> <span id='ContentPlaceHolder1_DtLstMesages_Date_53' style='font-size:XX-Small;'>۱۳۹۸/۰۸/۲۰</span></span>" +
                "<span> <span id='ContentPlaceHolder1_DtLstMesages_Time_53' style='font-size:XX-Small;'>۰۹:۱۱ ق.ظ</span>-</span><span><i class='fa fa-user' aria-hidden='true'></i></span>  <span><span id='ContentPlaceHolder1_DtLstMesages_LblNameSender_53' style='font-size:XX-Small;'>ابراهیم عباسی</span>" +
                "</span> <span class='hidden'><span id='ContentPlaceHolder1_DtLstMesages_LblSenderId_53' style='font-size:XX-Small;'>2004023</span></span> </small> </small></div><small class='ms-date'> </small></div><small class='ms-date'></small></td></tr></tbody></table>";
            $("[id*=UpdatePanel1] ").append(rowManual);
            $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_PanelFile] ").addClass("hidden");
            $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_Panelaudio] ").addClass("hidden");
            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Message]").addClass("hidden");
            if (typeMsg == 1) {
                $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Message]").text($("input[id*=txtMsg]").val());
                $("input[id*=txtMsg]").val("");
                $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Message]").removeClass("hidden");
            }
            if (typeMsg == 2) {
                $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_Panelaudio]  audio source").attr("src", "../SoundRecorder/" + res[0].ChatID_P + ".wav");;
                $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_Panelaudio]  audio source").removeClass("hidden");
                $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Message]").text(" فایل صوتی ");
            }
            if (typeMsg == 3) {
                $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_PanelFile]  span a").attr("href", "../ FileSaver /" + res[0].ChatID_P + res[0].format);;
                $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_PanelFile] span a ").text(res[0].name.replace("@$file send@$", ""));
                $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_PanelFile] ").removeClass("hidden");
                $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Message]").text(" فایل ");
            }


            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Date]").text(DatePersian);
            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Time]").text(time);
            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_LblNameSender]").text($("span[id=stName").text());
            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_LblChatID]").text(res[0].ChatID_P);
            $("[id*=DtLstMesages] tr:last-child").find("img[id*=DtLstMesages_Image4]").attr("src", "data:image/jpg;base64, " + res[0].Images);
            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_LblSenderId]").text($("#user").text().trim());

        }

        var test = $("table[id*=DtLstMesages").height();
        $(".dtTable").scrollTop(test);


        $("span[id*=ContentPlaceHolder1_LblLastIdChat]").text(res[0].ChatID_P);

    }
    $('.recordVoice').on('click', function (e) {//startRecording

        e.preventDefault();
        $("input[id*=txtMsg]").attr("disabled", "disabled");
        $(".recordVoice").addClass("hidden");
        $(".stopVoice").removeClass("hidden");

        console.log("recordButton clicked");
        $.ajax({
            type: "POST",
            url: "../ContactOstad/ContactByStudent.aspx/RecordVoice",
            data: '{ }',
            contentType: 'application/json',
            dataType: "json",
        });
    });
    $(".stopVoice").click(function (e) {
        e.preventDefault();

        $(".stopVoice").addClass("hidden");
        $(".recordVoice").removeClass("hidden");
        $("input[id*=txtMsg]").removeAttr("disabled");
       
        $.ajax({

            type: "POST",
            url: "../ContactOstad/ContactByStudent.aspx/SaveVoice",
            data: '{ userID: "' + $("span[id*=LblIdUser]").text() + '",' +
                'idGrp: "' + $("span[id*=LblIdGrp]").text() + '",' +
                ' msg: "' + "@$sound voice@$" + '",' +
                'idOnChat:"' + $("input[id*=TxtIdOnChat]").val() + '",' +
                'idMsgReplayed:"' + $("span[id*=ContentPlaceHolder1_LblReplyIdD]").text() + '"}',

            contentType: 'application/json',
            dataType: "json",
            success: function (result) {
               
                var res = JSON.parse(result.d);
                AddMessage(2, res);

            }

        });
    });

    $(".cancelReplay").click(function () {
        $("#rowReply").css("display", "none");
    });

    $("#ms-menu-trigger").click(function () {
       
        var op = $("#messages-main .ms-menu ").css("opacity");
        if (op == 1) {
            $("#messages-main .ms-menu").css("opacity", '0');
            $("#messages-main .ms-menu").css("display", 'none');
        }
        else if (op == 0) {
            $("#messages-main .ms-menu").css("opacity", '1');
            $("#messages-main .ms-menu").css("display", 'block');
        }
    });
    $(document).on("click", ".trashIcon", function () {

       

        var findChat = $(this).closest(".lv-item").find("div[id*=ContentPlaceHolder1_DtLstMesages_PanelReplayed]").attr('id');
        var chatId = $(this).closest(".lv-item").find("span[id*=DtLstMesages_LblChatID]").text();
        var date = $(this).closest(".lv-item").find("span[id*=DtLstMesages_DateMiladi]").text();
        var idSender = $(this).closest(".lv-item").find("span[id*=DtLstMesages_LblSenderId]").text();

        var idOnChat = $("input[id*=TxtIdOnChat]").val();
        var idGrp = $("span[id*=LblIdGrp]").text();

        $("#findChatDelete").text(findChat);
        $("#ChatIdDelete").text(chatId);
        $("#SenderIdDelete").text(idSender);
        $("#GroupIdDelete").text(idGrp);
        $("#ReciverIdDelete").text(idOnChat);
        $("#dateDelete").text(date);
        $('.deleteChat').modal('show');

    });
    $('.acceptDelete').on('click', function (e) {
       
        e.preventDefault();
       
        $.ajax({

            type: "POST",
            url: "../Functions/MessageJs.aspx/DeleteChat",
            data: '{senderId: "' + $("#SenderIdDelete").text().trim() + '",' +
                'groupId: "' + $("#GroupIdDelete").text().trim() + '",' +
                'reciverId: "' + $("#ReciverIdDelete").text().trim() + '",' +
                'userId: "' + $("#user").text().trim() + '",' +
                'chatId: "' + $("#ChatIdDelete").text().trim() +
                '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                $('.deleteChat').modal('hide');
               
                if (result.d == true) {
                    var findChat = $("#findChatDelete").text();
                    $("#" + findChat).closest(".lv-item").find("span[id *= DtLstMesages_Message]").removeClass("hidden").text("پیام پاک شده است");
                    $("#" + findChat).parent().find("div[id*=DtLstMesages_PanelFile]").addClass("hidden");
                    $("#" + findChat).parent().find("div[id*=DtLstMesages_Panelaudio]").addClass("hidden");
                    $("#" + findChat).closest(".lv-item").find(".trashIcon").addClass("hidden");
                }
                else {
                    $("#msgAlertDeleteChat").text("حذف امکان پذیر نمی باشد");
                    $('.AlertDeleteChat').modal('show')
                }
            },
            error: function (result) {
                $('.deleteChat').modal('hide');
                $("#msgAlertDeleteChat").text("حذف امکان پذیر نمی باشد");
                $('.AlertDeleteChat').modal('show');
            }



        })
    });

    var test = $("table[id*=DtLstMesages").height();
    $(".dtTable").scrollTop(test);


    $(".RowTableStudent").click(function () {
       
        window.location = $(this).data("href");
    });
    $(".MsgUnRead").click(function (e) {
        e.preventDefault();



       
        $.ajax({
            type: "POST",
            url: "../Functions/MessageJs.aspx/DeleteUnreadOstad",
            data: '{userID: "' + $("span[id*=LblIdUser]").text().trim() + '",' +
                'FlagGrp: "' + $(this).find(".Flag_GrpB").text().trim() + '",' +
                'Sender: "' + $(this).find(".senderId").text().trim() + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                $(".badgeMsg").text(result.d);
            },
            error: function (result) {
                alert(result);
            }
        })
    });
    $(document).on("click", ".replyIcon", function () {
       
        $("#rowReply").css("display", "block");

        var message = $(this).closest(".lv-item").find("span[id*=DtLstMesages_Message]").text();
        var id = $(this).closest(".lv-item").find("span[id*=DtLstMesages_LblChatID]").text();
        var nameRp = $(this).closest(".lv-item").find("span[id*=DtLstMesages_LblNameSender]").text();
        $("span[id*=LblReplyIdD]").text(id);
        $("span[id*=LblReplyIdD]").addClass("hide");
        $("span[id*=LblReplyMsg]").text(message.replace("@$file send@$", " فایل ").replace("@$sound voice@$blob", " فایل صوتی "));
        var test = $("table[id*=DtLstMesages").height();
        $(".dtTable").scrollTop(test);
        $("span[id*=LabelNameRepaly]").text(nameRp);
        $("input[id*=txtMsg]").focus();

    });

    $(".btnInsertMesage").click(function (e) {

        e.preventDefault();
       
        $.ajax({
            type: "POST",
            url: "../Functions/MessageJs.aspx/InsertMessage",
            data: '{ userID: "' + $("span[id*=LblIdUser]").text() + '",' +
                'idOnChat:"' + $("input[id*=TxtIdOnChat]").val() + '",' +
                'idGrp: "' + $("span[id*=LblIdGrp]").text() + '",' +
                'msg: "' + $("input[id*=txtMsg]").val() + '",' +
                'idMsgReplayed:"' + $("span[id*=ContentPlaceHolder1_LblReplyIdD]").text() + '",' +
                'flagTypeFile:1,' +
                'format:".txt"}'
            ,

            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if ($("input[id*=txtMsg]").val().trim() != "") {
                    var res = JSON.parse(result.d);
                    AddMessage(1, res);
                }
            },
            error: function (result) {
                alert(result);
            }
        });
    });
    window.setInterval(function () {


        var data = new FormData();
        data.append("userID", $("span[id*=LblIdUser]").text());
        data.append("idOnChat", $("input[id*=TxtIdOnChat]").val());
        data.append("idGrp", $("span[id*=LblIdGrp]").text());
        data.append("idChatLast", $("span[id*=ContentPlaceHolder1_LblLastIdChat]").text());
        $.ajax({
            url: '../SendDynamicMsg.ashx',
            type: 'POST',
            data: data,
            cache: false,
            contentType: false,
            processData: false,


            success: function (result) {
                if (result != "") {

                    LastNewMsg(result);
                }
            }
        });

    },1000);
    function LastNewMsg(res) {


        var row = $("[id*=DtLstMesages] tr:last-child");


        var today = new Date();
        var time = DisplayTime();

        var DatePersian = DateNowShamsi();

      
        if (row.length > 0) {

            row = row.clone().insertAfter("[id*=DtLstMesages] tr:last-child");
            row.find("span[id*=DtLstMesages_Message]").addClass("hidden");
            row.find("div[id*=DtLstMesages_Panelaudio]").addClass("hidden");
            row.find("div[id*=DtLstMesages_PanelFile]").addClass("hidden");
            var row = $("[id*=DtLstMesages] tr:last-child");
           
            if (res[0].typeMsg == 1) {
                row.find("span[id*=DtLstMesages_Message]").text(res[0].Message);
                row.find("span[id*=DtLstMesages_Message]").removeClass("hidden");
                row.find("div[id*=DtLstMesages_Panelaudio] audio source").attr("src", "");
            }
            if (res[0].typeMsg == 2) {
                row.find("div[id*=DtLstMesages_Panelaudio]  audio source").attr("src", "../SoundRecorder/" + res[0].ChatID + ".wav");
                row.find("div[id*=DtLstMesages_Panelaudio]  ").removeClass("hidden");
                row.find("span[id*=DtLstMesages_Message]").text(" فایل صوتی ");
            }
            if (res[0].typeMsg == 3) {
                row.find("div[id*=DtLstMesages_PanelFile] span a ").attr("href", "../FileSaver/" + res[0].ChatID + res[0].FormatFile);
                row.find("div[id*=DtLstMesages_PanelFile]").removeClass("hidden");
                row.find("span[id*=DtLstMesages_Message]").text(" فایل ");
                row.find("div[id*=DtLstMesages_PanelFile] span a ").text(res[0].Message.replace("@$file send@$", ""));
            }
            if (res[0].RplyMsg != null)
                row.find("span[id*=DtLstMesages_LblReplayMsg]").text(res[0].RplyMsg.replace("@$sound voice@$blob", " فایل صوتی ").replace("@$file send@$", " فایل "));
            row.find("span[id*=DtLstMesages_LblReplayId]").text(res[0].RplyId);


            row.find("div[id*=ContentPlaceHolder1_DtLstMesages_PanelReplayed]").attr('id', 'ContentPlaceHolder1_DtLstMesages_PanelReplayed__' + res[0].ChatID);

            row.find("span[id*=DtLstMesages_LblChatID]").text(res[0].ChatID);

            row.find("img[id*=ContentPlaceHolder1_DtLstMesages_Image4]").attr("src", "data:image/jpg;base64, " + res[0].Images);
            
            row.find("span[id*=DtLstMesages_Label1]").text(res[0].FullNameRp);
           
            row.find("span[id*=ContentPlaceHolder1_LabelNameRepaly]").text("");
            row.find("span[id*=DtLstMesages_LblNameSender]").text(res[0].FullNameS);

            row.find("span[id*=DtLstMesages_Time]").text(time);
            row.find("span[id*=DtLstMesages_Date]").text(DatePersian);
            row.find("span[id*=DtLstMesages_LblSenderId]").text(res[0].IdNameS);

            if (res[0].RplyId != null) {
                row.find("span[id*=DtLstMesages_PanelReplayed]").attr("Visible", "true");
                row.find("div[id*=DtLstMesages_PanelReplayed]").removeClass("False");
                $("#rowReply").css("display", "none");
            }
            else {
                row.find("span[id*=DtLstMesages_PanelReplayed]").attr("Visible", "false");
                row.find("div[id*=DtLstMesages_PanelReplayed]").addClass("False");
            }
        }
        else {

            var rowManual = "<table id='ContentPlaceHolder1_DtLstMesages' cellspacing='0' style='width:100%;border-collapse:collapse;'>" +
                "<tbody><tr><td><td><div class='lv-item media'><div id='ContentPlaceHolder1_DtLstMesages_PanelReplayed_53' class='False'>" +
                "<div class='row'> <div class='col-sm-12'><div class='row'> <div class='col-sm-5'><div class='containerReplay msg_ReplayChat '>" +
                "<h5>پاسخ</h5> <div class='msg_Replay'><div class='hidden'> <span id='ContentPlaceHolder1_DtLstMesages_LblReplayId_53'>50846</span>" +
                "</div><span id='ContentPlaceHolder1_DtLstMesages_LblReplayMsg_53' style='font-size:Smaller;'>پیام پاک شده است</span> </div>" +
                "<div class='left'><span id='ContentPlaceHolder1_DtLstMesages_Label1_53' style='font-size:X-Small;'>ابراهیم عباسی</span></div>" +
                "</div> </div></div> </div> </div> </div> <div class='lv-avatar pull-right'>" +
                "<img id='ContentPlaceHolder1_DtLstMesages_Image4_53' src='' style='height:187px;width:250px;width: 30px; height: 30px'>" +
                "</div><div class='media-body'><div class='ms-item'><span class='glyphicon glyphicon-triangle-left' style='color: #000000;'></span>" +
                "<span id='ContentPlaceHolder1_DtLstMesages_LblChatID_53' class='hide'>50847</span><span id='ContentPlaceHolder1_DtLstMesages_Message_53' style='font-size:Smaller;'>پیام پاک شده است</span>" +
                "<div id='ContentPlaceHolder1_DtLstMesages_Panelaudio_53' class='hidden'><audio controls='' style='max-width:200px'><source src='../SoundRecorder/50847.wav' type='audio/mpeg'></audio>" +
                "</div><div id='ContentPlaceHolder1_DtLstMesages_PanelFile_53' class='hidden'> <span><a class='btn btn-link' style='width: 100%' href='../FileSaver/50847.wav' download=''>پیام پاک شده است</a>" +
                " </span></div> </div><span class='glyphicon  glyphicon-reply '><i class='fa fa-reply replyIcon' data-toggle='tooltip' data-placement='left' title='پاسخ' aria-hidden='true'></i><span class='sr-only'>پاسخ</span>" +
                " <i class='fa fa-trash  trashIcon ' data-target='.deleteChat' aria-hidden='true' data-toggle='tooltip' data-placement='left' title='حذف'></i><span class='sr-only'>حذف</span></span>" +
                "<small class='ms-date'><small class='ms-date'><i class='fa fa-clock' aria-hidden='true'></i><span> <span id='ContentPlaceHolder1_DtLstMesages_Date_53' style='font-size:XX-Small;'>۱۳۹۸/۰۸/۲۰</span></span>" +
                "<span> <span id='ContentPlaceHolder1_DtLstMesages_Time_53' style='font-size:XX-Small;'>۰۹:۱۱ ق.ظ</span>-</span><span><i class='fa fa-user' aria-hidden='true'></i></span>  <span><span id='ContentPlaceHolder1_DtLstMesages_LblNameSender_53' style='font-size:XX-Small;'>ابراهیم عباسی</span>" +
                "</span> <span class='hidden'><span id='ContentPlaceHolder1_DtLstMesages_LblSenderId_53' style='font-size:XX-Small;'>2004023</span></span> </small> </small></div><small class='ms-date'> </small></div><small class='ms-date'></small></td></tr></tbody></table>"
            $("[id*=UpdatePanel1] ").append(rowManual);

            $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_PanelFile] ").addClass("hidden");
            $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_Panelaudio] ").addClass("hidden");
            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Message]").addClass("hidden");

            if (res[0].typeMsg == 1) {
                $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Message]").text(res[0].Message);
                $("[id*=DtLstMesages] tr:last-child").removeClass("hidden");
            }
            if (res[0].typeMsg == 2) {
                $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_Panelaudio]  audio source").attr("src", "../SoundRecorder/" + res[0].ChatID + ".wav");;
                $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_Panelaudio]  ").removeClass("hidden");
                $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Message]").text(" فایل صوتی ");
            }
            if (res[0].typeMsg == 3) {
                $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_PanelFile]  span a").attr("href", "../ FileSaver /" + res[0].ChatID + res[0].format);;
                $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_PanelFile] span a ").text(res[0].name.replace("@$file send@$", ""));
                $("[id*=DtLstMesages] tr:last-child").find("div[id*=DtLstMesages_PanelFile] ").removeClass("hidden");
                $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Message]").text(" فایل ");

            }

            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Date]").text(DatePersian);
            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_Time]").text(time);
            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_LblNameSender]").text(res[0].IdNameS);
            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_LblChatID]").text(res[0].ChatID);
            $("[id*=DtLstMesages] tr:last-child").find("img[id*=DtLstMesages_Image4]").attr("src", "data:image/jpg;base64, " + res[0].images);
            $("[id*=DtLstMesages] tr:last-child").find("span[id*=DtLstMesages_LblSenderId]").text(res[0].IdNameS);

        }

        var test = $("table[id*=DtLstMesages").height();
        $(".dtTable").scrollTop(test);

        $("span[id*=ContentPlaceHolder1_LblLastIdChat]").text(res[0].ChatID);



    }
});