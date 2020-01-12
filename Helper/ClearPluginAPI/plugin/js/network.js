
let baseUrl = "http://localhost:5000/";

function ajaxPost(method, data, success, codeHandler = null, headers = null) {

	var failureFunc = null;

	if (!codeHandler) {
		failureFunc = function (jqxhr, status, errorMsg) {
			alert(jqxhr.status);
		};
	} else {
		failureFunc = function (jqxhr, status, errorMsg) {
			codeHandler(jqxhr.status);
		};
	}

	$.ajax({
		headers: headers,
		url: baseUrl + method,
		method: "POST",
		contentType: "application/json",
		data: JSON.stringify(data),
		success: success,
		error: failureFunc
	});
}