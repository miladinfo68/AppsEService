$(document).ready(function () {
   
    var test = $("table[id*=DtLstMesages").height();
    $(".dtTable").scrollTop(test);

  
    $("input[id*= SearchBtn]").click(function () {
        $(".dtTable").scrollTop(test);



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
                ;
                var $parent = $("#searchCurText_" + f);
                $parent.contents().unwrap();          
            }
                $("span[id*=LblSearchText]").val(str);
                var i = 0;
                var countSearch = 0;
                $('.dtTable tr  ').each(function () {
                    var stroldMessage = $(this).find('span[id*=DtLstMesages_Message]').html();
                    var indexMatchMessage = locations(str, stroldMessage);

                    for (k=0; k < indexMatchMessage.length; ++k,++i) {
                        var spana = "<span style='background-color:yellow ' class='searchspan' id='searchCurText_" + i + "'>";
                            
                        var spanb = "</span>";
                       
                            $(this).find('span[id*=DtLstMesages_Message]').html(
                                addTag(stroldMessage, (indexMatchMessage[k] + (k* (spana.length + spanb.length))), spana, str.length, spanb)
                        );
                        ++countSearch;
               
                    }
                    var stroldReplayMsg = $(this).find('span[id*=DtLstMesages_LblReplayMsg]').html();
                    var indexMatchReplayMsg = locations(str, stroldReplayMsg);

                    for (j = 0; j< indexMatchReplayMsg.length; ++j,++i) {
                       
                        var spana = "<span style='background-color:yellow ' class='searchspan' id='searchCurText_" + i+
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
});