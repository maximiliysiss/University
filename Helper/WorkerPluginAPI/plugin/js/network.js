
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

function ajaxGet(method, success, codeHandler = null, headers = null) {

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
		method: "GET",
		contentType: "application/json",
		success: success,
		error: failureFunc
	});
}

function isAuthorize(mainPart, onSuccess) {
	localStorage = window.localStorage;
	var accessToken = localStorage.getItem(mainPart + "-accessToken");
	var refreshToken = localStorage.getItem(mainPart + "-refreshToken");

	if (accessToken && refreshToken && accessToken.length > 0 && refreshToken.length > 0) {
		ajaxGet("api/auth/login", function () {
			onSuccess();
		}, function (code) {
			switch (code) {
				case 401:
					{
						ajaxPost("api/auth/refresh", null, function (data) {
							localStorage.setItem(mainPart + '-accessToken', data.accessToken);
							localStorage.setItem(mainPart + '-refreshToken', data.refreshToken);
							onSuccess();
						}, null, { "token": accessToken, "refreshToken": refreshToken });
					}
					break;
			}
		}, { "Authorization": "Bearer " + accessToken });
	}
}

function authAction(mainPart, action) {
	isAuthorize(mainPart, function () { action(); });
}