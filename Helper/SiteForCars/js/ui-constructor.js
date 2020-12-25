let templateUrl = 'pages/shared/car-cards-template.html';

function constructCardForCar(datas, callback) {
    importDataFromPage(templateUrl, function(template) {
        var newHtmlElements = [];
        for (var i in datas) {
            var data = datas[i];
            var jQueryElement = $(template).clone();
            jQueryElement.children(".title").html(data["name"]);
            jQueryElement.children(".desc").html(data["body"] + '/' + data["type"]);
            jQueryElement.children(".cta").children(".price").html(data["price"]);
            for (var i in data["images"])
                jQueryElement.children(".slider").append(generateImageForSlide(data["images"][i], i));
            if (data["used"])
                jQueryElement.append(generateUsedLine());
            jQueryElement.css("padding-top", "40px");
            newHtmlElements.push(jQueryElement);
        }
        callback(newHtmlElements);
    });
}

function initUICarTable(data, htmlElement) {
    constructCardForCar(data, function(el) {
        $(htmlElement).append(el);
        initSliders();
        initFilters(htmlElement);
    });
}

function rerenderUICarTable(data, htmlElement) {
    $(htmlElement).html("");
    constructCardForCar(data, function(el) {
        $(htmlElement).append(el);
        initSliders();
    });
}

function generateImageForSlide(type, index) {
    return $('<figure data-color="' + colorPairs[index] + '"><img src="' + type + '"/></figure>');
}

function generateUsedLine() {
    return $('<div class="used-line">Used</div>');
}

let colorPairs = [
    "#E24938, #A30F22",
    "#6CD96A, #00986F",
    "#4795D1, #006EB8",
    "#292a2f, #131519"
];