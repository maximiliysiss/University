
// обрабока удаления истории может быть только на стороне background сервиса
chrome.runtime.onMessage.addListener(
    (request, sender, sendResponse) => {
		// если история, то все удалим
        if (request.code === "history") {
            chrome.history.deleteRange({
                startTime: request.start,
                endTime: request.end
            }, function () {
            });
            sendResponse({ message: "success" });
        }
        sendResponse({ message: "error" });
    });