<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signature.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.Signature.Signature" %>

<!DOCTYPE html>
<html lang="en">

<head>

    <!-- Basic Page Needs
    –––––––––––––––––––––––––––––––––––––––––––––––––– -->
    <meta charset="utf-8">
    <title>Signature</title>
    <meta name="description" content="">
    <meta name="author" content="">

    <!-- Mobile Specific Metas
    –––––––––––––––––––––––––––––––––––––––––––––––––– -->
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- FONT
    –––––––––––––––––––––––––––––––––––––––––––––––––– -->

    <!-- CSS
    –––––––––––––––––––––––––––––––––––––––––––––––––– -->
    <link rel="stylesheet" href="css/normalize.css">
    <link rel="stylesheet" href="css/canvasH.css">
    <link rel="stylesheet" href="css/custom.css">

    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="css/evol-colorpicker.min.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-3.3.1.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/jquery-ui.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/evol-colorpicker.min.js" type="text/javascript" charset="utf-8"></script>
</head>

<body style="min-height: 800px; min-width: 1000px; background-color: #ffffff;">

    <!-- Primary Page Layout
–––––––––––––––––––––––––––––––––––––––––––––––––– -->

    <div class="container">
        <section style="margin-top: 0.5px !important;">
            <div class="row">
                <div class="one-third column">
                    <div class="row">
                        <div class="one-half column">
                            <input class="u-full-width" type="number" value="3" id="line-size-input">
                        </div>
                        <div class="one-half column">
                            <input class="colorPicker u-full-width" type="text" value="#000000" id="line-color-input">
<%--                            <div style="width: 128px;">
                                <input style="width: 100px;" id="mycolor" class="colorPicker evo-cp0" />
                                
                            </div>--%>
                        </div>
                    </div>
                </div>
                <div class="one-third column">
                    <div class="row">
                        <div class="one-half column" style="color: #ffffff">
                            .
                        </div>
                    </div>
                </div>
                <div class="one-third column">
                    <div class="row">
                        <div class="one-half column">
                            <button class="u-full-width" id="clear">جدید</button>
                        </div>
                        <div class="one-half column">
                            <button class="u-full-width" id="btnSave">ذخیره</button>
                        </div>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="twelve columns">
                    <div id="canvasHPad" style="background-color:#38444b6b  !important;"></div>
                </div>
            </div>
        </section>
    </div>

    <!-- Scripts
–––––––––––––––––––––––––––––––––––––––––––––––––– -->
    <script src="js/responsive-canvasHPad.js"></script>
    <script>
        var el = document.getElementById('canvasHPad');
        var pad = new canvasHPad(el);

        $("#line-color-input").change(setLineColor);
        //$("#line-color-input").trigger("change");

        function setLineColor(e) {
            var color = e.target.value;
            if (!color.startsWith('#')) {
                color = '#' + color;
            }
            pad.setLineColor(color);
        }
        document.getElementById('line-color-input').oninput = setLineColor;

        // setLineSize
        function setLineSize(e) {
            var size = e.target.value;
            pad.setLineSize(size);
        }
        document.getElementById('line-size-input').oninput = setLineSize;

        // undo
        // function undo() {
        //   pad.undo();
        // }
        // document.getElementById('undo').onclick = undo;

        // redo
        // function redo() {
        //   pad.redo();
        // }
        // document.getElementById('redo').onclick = redo;

        // clear
        function clear() {
            pad.clear();
        }
        document.getElementById('clear').onclick = clear;

        // resize
        window.onresize = function (e) {
            pad.resize(el.offsetWidth);
        }


        $("#btnSave").click(function (e) {

            var image = document.getElementsByTagName("canvas")[0].toDataURL("image/png");
            image = image.replace('data:image/png;base64,', '');
            $.ajax({
                type: 'POST',
                url: "Signature.aspx/UploadImage",
                data: '{ "imageData" : "' + image + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                error: function () {
                    alert('مشکلی بر روی سرور پیش آمده لطفا مجددا امتحان کنید');
                },
                success: function () {
                    pad.clear();
                    alert("امضاء با موفقیت ذخیره گردید");
                }

            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#line-color-input").colorpicker({
                hideButton: true
            });
        });
    </script>
    <!-- End Document
–––––––––––––––––––––––––––––––––––––––––––––––––– -->
</body>

</html>

