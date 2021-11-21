function SendAccountServiceRequest() {
    $.ajax({
        type: "GET",
        data: { 'Url': '22', 'Login': '22', 'Password': '22' },
        url: "/AuthorizeAccountService",
        success: function (msg) {
            alert(msg);
        },
        error: function (xhr, status, error) {
            alert(error + ": " + xhr.responseJSON);
        }
    });
}