$("link[rel='importer']").each(function() {
    var href = $(this).attr("href");
    var htmlElement = $(this);

    var afterLoadFunction = $(this).attr("onafterload");

    importDataFromPage(href, function(loadedHtml) {
        $(htmlElement).replaceWith(loadedHtml);
        if (afterLoadFunction !== undefined)
            window[afterLoadFunction](loadedHtml);
    });
});

function importDataFromPage(url, callback) {
    $.get(url, function(data) {
        var loadedHtml = $(new XMLSerializer().serializeToString(data));
        callback(loadedHtml);
    });
}