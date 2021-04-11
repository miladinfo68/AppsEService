function pageLoad(sender, args) {
    var productsTable = $get("products");
    var firstRow;
    if (productsTable.tBodies) {
        firstRow = productsTable.tBodies[0].rows[0];
    }
    else {
        firstRow = productsTable.rows[0];
    }

    var expandImg = firstRow.cells[0].getElementsByTagName("IMG")[0];

    if (expandImg.src.indexOf("Plus") > -1) {
        toggleOrderDetails(expandImg);
    }
}

function toggleOrderDetails(sender) {
    var thisRow = sender.parentNode.parentNode;
    var nextRow = getNextRowSibling(thisRow);

    if (nextRow && nextRow.className == "orders") {
        if (nextRow.style.display == "none") {
            nextRow.style.display = "";
            sender.src = sender.src.replace("Plus", "Minus");
            thisRow.className = "expanded";
        }
        else {
            nextRow.style.display = "none";
            sender.src = sender.src.replace("Minus", "Plus");
            thisRow.className = "";
        }
    }
}

function getNextRowSibling(row) {
    var ret = row;
    do {
        ret = ret.nextSibling;
    } while (ret && ret.tagName != "TR");

    return ret;
}