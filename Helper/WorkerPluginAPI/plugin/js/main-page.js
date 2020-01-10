$(function () {

    var location = window.localStorage;

    authAction('worker', function () {
        ajaxGet("api/workercheks/status", function (data) {

        }, null, {
            "Authorization": location.getItem('worker-accessToken')
        });
    });


    $('#signout').click(function () {
        location.removeItem('worker-accessToken');
        location.removeItem('worker-refreshToken');
        window.location = 'popup.html';
    });

    $('#action').click(function () {
    });
});