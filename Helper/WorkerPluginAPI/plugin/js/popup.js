$(function () {

	localStorage = window.localStorage;

	authAction("worker", function () {
		window.location = "main-page.html";
	});

	$('#login-btn').click(function () {
		var loginString = $('#login').val().trim();
		var passwordString = $('#password').val().trim();
		if (loginString.length === 0 || passwordString.length === 0) {
			alert('Введите логин/пароль');
			return;
		}
		ajaxPost('api/auth/login', { login: loginString, password: passwordString }, function (data, textStatus, jqXHR) {
			localStorage.setItem('worker-accessToken', data.accessToken);
			localStorage.setItem('worker-refreshToken', data.refreshToken);
			window.location = "main-page.html";
		}, function (code) {
			switch (code) {
				case 404:
					alert("Некорректный логин/пароль");
					break;
			}
		});
	});
});