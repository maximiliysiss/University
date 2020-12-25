loadedData = [];
activeFilters = []

function initFilters(forElement) {
    var types = getFilters("type");
    var bodies = getFilters("body");
    $(".filter-data[field='type']").append(types);
    $(".filter-data[field='body']").append(bodies);

    $(".filter").on("click", function() {
        var parent = $(this).parent();
        var filterType = parent.attr("field");

        var elem = $(this);

        var filter = { "field": filterType, "val": elem.html() };

        if (elem.hasClass('filter-active')) {
            elem.removeClass('filter-active');
            var indexOf = activeFilters.indexOf(filter);
            activeFilters.splice(indexOf, 1);
        } else {
            elem.addClass('filter-active');
            activeFilters.push(filter);
        }

        applyFilters(forElement);
    });
}

function getFilters(type) {
    return [...new Set([...new Set(loadedData.map(function(x) { return x[type] }))].map(function(x) { return createFilter(x); }))];
}

function createFilter(data) {
    return $('<div class="btn filter">' + data + '</div>');
}

function applyFilters(forElement) {
    var loadByFilters = loadedData;

    var grouper = groupBy("field");
    var groupedByField = grouper(activeFilters);

    for (var i in groupedByField) {
        var filter = groupedByField[i].map(function(x) { return x["val"]; });
        loadByFilters = loadByFilters.filter(function(x) {
            return filter.includes(x[i]);
        });
    }
    rerenderUICarTable(loadByFilters, forElement);
}

function groupBy(key) {
    return function group(array) {
        return array.reduce((acc, obj) => {
            const property = obj[key];
            acc[property] = acc[property] || [];
            acc[property].push(obj);
            return acc;
        }, {});
    };
}