$(function () {

    var actionBtn = $('#action');
    var timeDiv = $('#time');
    var location = window.localStorage;

    var timerId = undefined;
    var timerClass;

    function timer() {
        timeDiv.html(timerClass.toString());
        timerClass.incrementSecond();
    }

    function changeBtnState(type, time) {
        if (type === 0) {
            if (timerId !== undefined) {
                clearInterval(timerId);
            }

            timerClass = new Timer(Date.now() - Date.parse(time));
            timer();
            timerId = setInterval(timer, 1000);

            actionBtn.html('Ушел с работы');
            timeDiv.css('display', 'block');

        } else {
            timeDiv.css('display', 'none');
            actionBtn.html('Пришел на работу');
        }
    }

    authAction('worker', function () {
        ajaxGet("api/workerchecks/status", function (data) {
            changeBtnState(data.type, data.dateTime);
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

    $('#action-btn').click(function () {
        authAction('worker', function () {
            ajaxGet("api/workerchecks/action", function (data) {
                changeBtnState(data.type, data.dateTime);
            }, null, {
                "Authorization": "Bearer " + location.getItem('worker-accessToken')
            });
        });
    });
});