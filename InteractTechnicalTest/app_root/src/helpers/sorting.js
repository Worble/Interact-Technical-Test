var sortByName = function (products, reverse) {
    var sortedProducts = products;
    sortedProducts.sort(function (a, b) {
        var nameA = a.Name.toUpperCase();
        var nameB = b.Name.toUpperCase();
        if (nameA < nameB) {
            return reverse ? 1 : -1;
        }
        if (nameA > nameB) {
            return reverse ? -1 : 1;
        }

        return 0;
    });
    return sortedProducts;
}

var sortByCost = function (products, reverse) {
    var sortedProducts = products;
    sortedProducts.sort(function (a, b) {
        if (a.Cost < b.Cost) {
            return reverse ? 1 : -1;
        }
        if (a.Cost > b.Cost) {
            return reverse ? -1 : 1;
        }

        return 0;
    });
    return sortedProducts;
}

export { sortByName, sortByCost }
