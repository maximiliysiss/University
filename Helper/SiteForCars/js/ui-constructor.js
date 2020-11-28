let templateUrl = 'pages/shared/car-cards-template.html';

function constructCardForCar(data, callback) {
    importDataFromPage(templateUrl, function(template) {
        var jQueryElement = $(template);
        jQueryElement.children(".title").each(function(x) {
            this.innerHtml = data["name"];
        });
        callback(jQueryElement);
    });
}