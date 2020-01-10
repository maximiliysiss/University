$(function () {

    var actionBtn = $('#action');
    var location = window.localStorage;

    function changeBtnState(type) {
        if (type === 0) {
            actionBtn.val('Ушел с работы');
        } else {
            actionBtn.val('Пришел на работу');
        }
    }

    authAction('worker', function () {
        ajaxGet("api/workerchecks/status", function (data) {
            changeBtnState(data.type);
            $("#body").css("display", "flex");
        }, null, {
            "Authorization": "Bearer " + location.getItem('worker-accessToken')
        });
    });


    $('#signout').click(function () {
        location.removeItem('worker-accessToken');
        location.removeItem('worker-refreshToken');
        window.location = 'popup.html';
    });

    $('#action').click(function () {
        authAction('worker', function () {
            ajaxGet("api/workerchecks/action", function (data) {
                changeBtnState(data.type);
            }, null, {
                "Authorization": "Bearer " + location.getItem('worker-accessToken')
            });
        });
    });
});