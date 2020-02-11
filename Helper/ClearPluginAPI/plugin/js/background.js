
chrome.runtime.onMessage.addListener(
    (request, sender, sendResponse) => {
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