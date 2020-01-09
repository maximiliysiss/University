$(function(){
	
	localStorage = window.localStorage;
	
	$('#login-btn').click(function(){
		var loginString = $('#login').val().trim();
		var passwordString = $('#password').val().trim();
		if(loginString.length == 0 || passwordString.length == 0){
			alert('Введите логин/пароль');
		}
		ajaxPost('http://localhost:5000/api/auth/login', {login: loginString, password: passwordString}, function(data){
			alert("Data: " + data);
		});
	});
});