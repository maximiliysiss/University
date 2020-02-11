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

    $('#clear-history').click(function () {

        var valFrom = $("#from").val();
        var valTo = $("#to").val();

        if (!valTo || !valFrom)
            return;

        var dateFrom = new Date(valFrom);
        var dateTo = new Date(valTo);

        if (dateFrom > dateTo) {
            toastr.warning("Введите правильные данные");
            return;
        }

        var callback = function () {
            toastr.success("Успешно");
        };

        chrome.runtime.sendMessage({
            code: "history",
            start: dateFrom.getTime(),
            end: dateTo.getTime()
        }, (response) => {
            if (response.message === "success")
                callback();
        });

        ajaxPost('api/clearactions/', { type: "history" }, null);
    });

    $('#back').click(function () {
        document.location = 'popup.html';
    });

});