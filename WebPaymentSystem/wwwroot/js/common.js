function LoadData() {
    $.ajax({
        type: "GET",
        data: { 'SortString': GetSortString(), 'CustomerName': GetSearchText(), 'PaymentDate': GetSearchDate(), 'PageNumber': PageNumber, 'PageLenght': PageLenght },
        url: "/GetPayments",
        headers: {
            "Accept": 'text/json',
        },
        success: function (msg) {
            myList = msg;
            buildHtmlTable('#DataTable');
            RefreshPageInfo();
        },
        error: function (xhr, status, error) {
            alert(error + ": " + xhr.responseJSON);
        }
    });
}
function RefreshPageInfo() {
    $("#PageInfo").text('Page number: ' + PageNumber);
    $("#PageSize").text('Page size: ' + PageLenght);
}
var myList = [];
var PageNumber = 1;
var PageLenght = 3;
function buildHtmlTable(selector) {
    $(selector).empty();
    var columns = addAllColumnHeaders(myList, selector);

    for (var i = 0; i < myList.length; i++) {
        var row$ = $('<tr/>');
        for (var colIndex = 0; colIndex < columns.length; colIndex++) {
            var cellValue = myList[i][columns[colIndex]];
            if (cellValue == null) cellValue = "";
            if (colIndex == 2) {
                let date = new Date(cellValue);
                row$.append($('<td/>').html(date.getDate() + '.' + date.getMonth() + '.' + date.getFullYear()));
            }
            else
                row$.append($('<td/>').html(cellValue));
        }
        $(selector).append(row$);
    }
}
function PagePlus(IsSize) {
    if (IsSize)
        PageLenght++;
    else
        PageNumber++;
    LoadData();
}
function PageMinus(IsSize) {
    if (IsSize) {
        if (PageLenght > 1)
            PageLenght--;
    }
    else {
        if (PageNumber > 1)
            PageNumber--;
    }
    LoadData();
}
function SetSort(columnIndex) {
    if (SortColumnIndex == undefined)
        SortColumnIndex = columnIndex;
    else {
        if (SortColumnIndex == columnIndex)
            SortMode = SortMode == 'asc' ? 'desc' : 'asc';

        if (SortColumnIndex != columnIndex) {
            SortMode = 'asc';
            SortColumnIndex = columnIndex;
        }
    }
    LoadData();
}
var SortColumnIndex;
var SortMode;
var columnSet = [];

function GetSortString() {
    if (SortColumnIndex == undefined) return null;
    if (SortColumnIndex == 0) return 'Name ' + (SortMode == undefined ? 'asc' : SortMode);
    else if (SortColumnIndex == 1) return 'Sum ' + (SortMode == undefined ? 'asc' : SortMode);
    else return 'PaymentDate ' + (SortMode == undefined ? 'asc' : SortMode);
}

function GetSearchText() {
    var txt = $("#CustomerNameTb").val();
    return txt.length == 0 ? null : txt;
}

function GetSearchDate() {
    var date = $("#datepicker").val();
    if (date != null) return date;
    else return null;
}

function GetColumnNameSortMode(ColumnIndex) {
    if (SortColumnIndex == ColumnIndex) {
        return SortMode == undefined ? '&#9650;' : SortMode == 'asc' ? '&#9650;' : '&#9660;';
    }
    else return '';
}

function GetColumnName(key) {
    if (key == 'customerName') return 'Контрагент ' + GetColumnNameSortMode(0);
    else if (key == 'sum') return 'Платеж ' + GetColumnNameSortMode(1);
    else return 'Дата платежа ' + GetColumnNameSortMode(2);
}

function addAllColumnHeaders(myList, selector) {
    var headerTr$ = $("<tr/>");
    columnSet = [];
    for (var i = 0; i < myList.length; i++) {
        var rowHash = myList[i];
        for (var key in rowHash) {
            if ($.inArray(key, columnSet) == -1) {
                columnSet.push(key);
                headerTr$.append($("<th onclick='SetSort(" + $.inArray(key, columnSet) + ")'/>").html(GetColumnName(key)));
            }
        }
    }
    $(selector).append(headerTr$);

    return columnSet;
}

