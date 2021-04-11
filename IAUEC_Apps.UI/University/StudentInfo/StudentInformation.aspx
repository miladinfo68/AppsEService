<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/Exam/MasterPages/BlankMaster.Master" CodeBehind="StudentInformation.aspx.cs" Inherits="IAUEC_Apps.UI.University.StudentInfo.StudentInformation" %>



<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../../CommonUI/css/js-persian-cal.css" rel="stylesheet" />
    <script src="../../../CommonUI/js/js-persian-cal.min.js"></script>
    <script>
        $(() => {
            var objCal1 = new AMIB.persianCalendar('birthDay');
        });
    </script>
    <style>
        .pdate {
            position: relative;
        }

        .pcalBtn {
            left: 20px;
            position: absolute;
            top: 42px;
        }

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
            min-width: 100px;
        }

        .panel-action {
            padding-top: 11px;
            padding-right: 5px;
        }

        .container-success-message {
            text-align: right;
            display: none;
        }

        .container-error-message {
            text-align: right;
            display: none;
        }

        .container-error-message-end {
            text-align: right;
            display: none;
        }
    </style>

    <div class="panel panel-info">
        <div class="panel-heading">
        </div>
        <div class="panel-body">
            <div class="body-container">
                <div class="panel-container">
                    <div class="row container-error-message">
                        <div class="alert alert-danger">
                            <ul class="container-error-message-ul">
                            </ul>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="fatherName">نام پدر </label>
                            <input type="text" class="form-control" id="fatherName" />
                        </div>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="idNumber">شماره شناسنامه </label>
                            <input type="text" class="form-control" id="idNumber" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-lg-4 col-4">
                            <label>استان محل سکونت </label>
                            <select class="form-control state-list" id="state">
                                <option value="0">انتخاب نمایید</option>
                            </select>
                        </div>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label>شهر محل سکونت </label>
                            <select class="form-control city-list" id="city">
                                <option value="0">انتخاب نمایید</option>
                            </select>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="birthPlaceState">استان محل تولد </label>
                            <select class="form-control state-list" id="birthPlaceState">
                                <option value="0">انتخاب نمایید</option>
                            </select>
                        </div>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="birthPlaceCity">شهر محل تولد </label>
                            <select class="form-control city-list" id="birthPlaceCity">
                                <option value="0">انتخاب نمایید</option>
                            </select>
                        </div>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="birthDay">تاریخ تولد </label>
                            <input type="text" class="form-control pdate" id="birthDay" />
                        </div>
                    </div>
                    <div class="row">
                        <%--          <div class="col-md-4 col-lg-4 col-4">
                        <label>تاریخ تولد: </label>
                        <input type="text" class="form-control pdate" id="birthDay" />
                    </div>--%>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="issuePlaceState">استان محل صدور </label>
                            <select class="form-control state-list" id="issuePlaceState">
                                <option value="0">انتخاب نمایید</option>
                            </select>
                        </div>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="issuePlaceCity">شهر محل صدور </label>
                            <select class="form-control city-list" id="issuePlaceCity">
                                <option value="0">انتخاب نمایید</option>
                            </select>
                        </div>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="address">آدرس </label>
                            <textarea rows="3" cols="50" class="form-control" id="address"></textarea>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="mobile">موبایل </label>
                            <input runat="server" type="text" class="form-control" id="mobile" />
                        </div>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="phoneNumber">تلفن ثابت </label>
                            <input type="text" class="form-control" id="phoneNumber" />
                        </div>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="militrystatus">وضعیت نظام وظیفه </label>
                            <select class="form-control" id="militrystatus">
                                <option value="0">انتخاب نمایید</option>
                            </select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="gender">جنسیت </label>
                            <select class="form-control" id="gender">
                                <option value="0">انتخاب نمایید</option>
                                <option value="1">مرد</option>
                                <option value="2">زن</option>
                            </select>
                        </div>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="postalCode">کد پستی </label>
                            <input type="text" class="form-control" id="postalCode" />
                        </div>
                        <div class="col-md-4 col-lg-4 col-4">
                            <label for="email">ایمیل </label>
                            <input type="text" class="form-control" id="email" />
                        </div>
                    </div>

                </div>
                <div class="panel-action">
                    <div class="row">
                        <button class="btn btn-success" id="btnUpdate">
                            ذخیره 
                        </button>
                    </div>
                </div>
            </div>
            <div class="container-error-message-end">
                <div class="alert alert-danger">
                    خطایی هنگام ثبت اطلاعات اتفاق افتاده است.
                </div>
            </div>
        </div>

    </div>

    <script>
        var validate = true;
        $(function () {
            getStates();
            getMilitryStatus();
        });
        function getStates() {
            $.ajax({
                type: "POST",
                url: "/University/StudentInfo/StudentInformation.aspx/GetStates",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    var data = res.d;
                    console.log(res.d);
                    if (data.length >= 1) {
                        setState(data);
                    }
                },
                Error: function () {
                    console.log("error")
                }
            });
        }
        function getMilitryStatus() {
            $.ajax({
                type: "POST",
                url: "/University/StudentInfo/StudentInformation.aspx/GetMilitryStatusDTO",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (res) {
                    debugger;
                    var data = res.d;
                    console.log(res.d);
                    if (data.length >= 1) {
                        setMilitryStatus(data);
                    }
                },
                Error: function () {
                    console.log("error")
                }
            });
        }
        function setMilitryStatus(items) {
            debugger;
            $.each(items, function (i, item) {
                $('#militrystatus').append($('<option>', {
                    value: item.Id,
                    text: item.Title
                }));
            });
        }
        function setState(items) {
            debugger;
            $.each(items, function (i, item) {
                $('#state').append($('<option>', {
                    value: item.STATE_CODE,
                    text: item.STATE_NAME
                }));
                $('#birthPlaceState').append($('<option>', {
                    value: item.STATE_CODE,
                    text: item.STATE_NAME
                }));
                $('#issuePlaceState').append($('<option>', {
                    value: item.STATE_CODE,
                    text: item.STATE_NAME
                }));
            });
        }
        $(".state-list").change(function () {
            var satateId = parseInt($(this).val());
            var $city = $(this).closest('.row').find('.city-list');
            GetCity(satateId, $city);
        })
        function GetCity(stateId, $element) {
            debugger;
            $.ajax({
                type: "POST",
                url: "/University/StudentInfo/StudentInformation.aspx/GetCityDTO",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ stateCode: stateId }),
                success: function (res) {
                    debugger;
                    var data = res.d;
                    console.log(res.d);
                    if (data.length >= 1) {
                        debugger;
                        setCity(data, $element);
                    }
                },
                Error: function () {
                    console.log("error")
                }
            });
        }
        function setCity(items, $element) {
            //$element.remove();
            debugger;
            $.each(items, function (i, item) {
                $element.append($('<option>', {
                    value: item.CITY_CODE,
                    text: item.CITY_NAME
                }));
            });
        }
        $("#btnUpdate").click(function (e) {
            e.preventDefault();
            validate = true;
            debugger;
            validateFun();
            if (validate) {
                var model = {
                    Address: $("#address").val(),
                    BirthDay: $("#birthDay").val(),
                    BirthPlaceState: $("#birthPlaceState").val(),
                    BirthPlaceCity: $("#birthPlaceCity").val(),
                    IssuePlaceState: $("#issuePlaceCity").val(),
                    IssuePlaceCity: $("#issuePlaceCity").val(),
                    Mobile: $("#ContentPlaceHolder1_mobile").val(),
                    Phone: $("#phoneNumber").val(),
                    Militrystatus: $("#militrystatus").val(),
                    Gender: $("#gender").val(),
                    FatherName: $("#fatherName").val(),
                    IdNumber: $("#idNumber").val(),
                    PostalCode: $("#postalCode").val(),
                    Email: $("#email").val(),
                    State: $("#state").val(),
                    City: $("#city").val()
                }
                $.ajax({
                    type: "POST",
                    url: "/University/StudentInfo/StudentInformation.aspx/UpdateStudent",
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({ studentDTO: model }),
                    success: function (res) {
                        if (res) {
                            parent.CloseModal();
                        } else {
                            $('.container-error-message-end').css("display", "block");
                        }

                    },
                    Error: function () {
                        $(".body-container").css("display", "none");
                        $(".container-error-message").css("display", "block");
                    }
                });
            }

        })
        function validateFun() {
            $('.container-error-message').css("display", "none");
            $('.container-error-message-ul li').remove();
            if ($("#ContentPlaceHolder1_mobile").val().trim().length != 11) {
                var liTemp = "<li>شماره موبایل اشتباه وارد شده است.</li>";
                $(".container-error-message-ul").append(liTemp);
                $('.container-error-message').css("display", "block");
                validate = false;
            }
            if ($("#phoneNumber").val().trim().length != 11) {
                var liTemp = "<li>تلفن ثابت اشتباه وارد شده است.</li>";
                $(".container-error-message-ul").append(liTemp);
                $('.container-error-message').css("display", "block");
                validate = false;
            }
            $(".form-control").each(function () {
                debugger;
                if ($(this).val() == "" || $(this).val() == "0") {
                    var id = $(this).attr('id');
                    var txt = $('label[for="' + id + '"]').text();
                    if (txt != "") {
                        txt = txt + " را وارد نمایید."
                        newErrorLi = "<li>" + txt + "</li>";
                        $(".container-error-message-ul").append(newErrorLi);
                        validate = false;
                    }
                    $('.container-error-message').css("display", "block");
                }
            });
        }
    </script>

</asp:Content>

