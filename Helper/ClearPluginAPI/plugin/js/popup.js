$(function () {

	// настройки оповещения
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

	// удалить данные и отправить данные на сервер
    function createLog(type, options) {
        var callback = function () {
            toastr.success("Успешно");
        };
        chrome.browsingData.remove({}, options, callback);
        ajaxPost('api/clearactions/', { type: type }, null);
    }

	// очистить кэш
    $('#clear-cache').click(function () {
        createLog('cache', {
            "appcache": true,
            "cache": true,
            "cacheStorage": true
        });
    });

	// очистить загрузки
    $('#clear-load').click(function () {
        createLog('load', {
            "downloads": true
        });
    });

	// очистить куки
    $('#clear-cookies').click(function () {
        createLog('cookies', {
            "cookies": true
        });
    });
	
	// очистить историю
	$('#clear-history').click(function () {
		document.location = 'clearing-history.html';
    });

});