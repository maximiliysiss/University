
function ajaxPost(method, data, success) {
    $.ajax({
        url: method,
        method: "POST",
		contentType: "application/json",
        data: JSON.stringify(data),
        success: success,
        error: function (jqxhr, status, errorMsg) {
            alert(errorMsg);
        }
    });
}