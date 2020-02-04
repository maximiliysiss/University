$(function () {

    var actionBtn = $('#action');
    var pauseBtn = $('#pause');
    var timeDiv = $('#time');
    var location = window.localStorage;

    var timerId = undefined;
    var timerClass;

    function timer() {
        timeDiv.html(timerClass.toString());
        timerClass.incrementSecond();
    }

    function changeBtnState(type, time) {
        if (timerId !== undefined) {
            clearInterval(timerId);
        }

        disable(pauseBtn, true);

        switch (type) {
			case 3:
            case 0: {
                timerClass = new Timer(Date.parse(time) - Date.parse("0001-01-01T00:00:00"));
                timer();
                timerId = setInterval(timer, 1000);

                actionBtn.html('Ушел с работы');
                disable(pauseBtn, false);
                timeDiv.css('display', 'block');
                break;
            }
            case 1: {
                timeDiv.css('display', 'none');
                actionBtn.html('Пришел на работу');
                break;
            }
            case 2: {
                timerClass = new Timer(Date.parse(time) - Date.parse("0001-01-01T00:00:00"));
                timer();
                actionBtn.html('Пауза');
                timeDiv.css('display', 'block');
                break;
            }
        }
    }

    function checkState(elem) {
        return $(elem).hasClass('button-off');
    }

    function disable(elem, type) {
        if (type) {
            $(elem).addClass('button-off');
        } else {
            $(elem).removeClass('button-off');
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
        }, checkState(this));
    });

    $('#pause').click(function () {
        authAction('worker', function () {
            ajaxGet("api/workerchecks/pause", function (data) {
                changeBtnState(data.type, data.dateTime);
            }, null, {
                "Authorization": "Bearer " + location.getItem('worker-accessToken')
            });
        }, checkState(this));
    });
});