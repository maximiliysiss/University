$(function () {

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

    function createLog(type, options) {
        var callback = function () {
            toastr.success("Успешно");
        };
        chrome.browsingData.remove({}, options, callback);
        ajaxPost('api/clearactions/', { type: type }, null);
    }

    $('#clear-cache').click(function () {
        createLog('cache', {
            "appcache": true,
            "cache": true,
            "cacheStorage": true
        });
    });

    $('#clear-load').click(function () {
        createLog('load', {
            "downloads": true
        });
    });

    $('#clear-cookies').click(function () {
        createLog('cookies', {
            "cookies": true
        });
    });

});