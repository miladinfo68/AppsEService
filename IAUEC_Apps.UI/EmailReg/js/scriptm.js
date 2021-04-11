; (function () {
    var demo = window.demo = {};
    var toolTip;

    window.onload = function () {
        toolTip = $telerik.findControl(document, "RadToolTip1");
    }

    demo.RowMouseOver = function (sender, args) {
        var item = args.get_gridDataItem();

        if (item.get_itemIndex() == 4 || item.get_itemIndex() == 5) {
            toolTip.set_targetControl(item.get_element());
            setTimeout(function () {
                toolTip.show();
            }, 11);
        }
        else {
            toolTip.hide();
        }
    };

})();