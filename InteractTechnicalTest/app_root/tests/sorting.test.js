const { sortByName, sortByCost } = require("../src/helpers/sorting.js");

test('sortByName returns an empty array when given an empty array', () => {
    expect(sortByName([], false)).toEqual([]);
});

test('sortByName sorts alphabetically on name', () => {
    var array = [{ Name: 'C' }, { Name: 'F' }, { Name: 'A' }];
    var expected = [{ Name: 'A' }, { Name: 'C' }, { Name: 'F' }];
    expect(sortByName(array, false)).toEqual(expected);
});

test('sortByName sorts reverse alphabetically on name when passed true for reverse', () => {
    var array = [{ Name: 'C' }, { Name: 'F' }, { Name: 'A' }];
    var expected = [{ Name: 'F' }, { Name: 'C' }, { Name: 'A' }];
    expect(sortByName(array, true)).toEqual(expected);
});

test('sortByCost returns an empty array when given an empty array', () => {
    expect(sortByCost([], false)).toEqual([]);
});

test('sortByCost sorts in ascending order on Cost', () => {
    var array = [{ Cost: 50 }, { Cost: 100 }, { Cost: 5 }];
    var expected = [{ Cost: 5 }, { Cost: 50 }, { Cost: 100 }];
    expect(sortByCost(array, false)).toEqual(expected);
});

test('sortByCost sorts in descending order on Cost when passed true for reverse', () => {
    var array = [{ Cost: 50 }, { Cost: 100 }, { Cost: 5 }];
    var expected = [{ Cost: 100 }, { Cost: 50 }, { Cost: 5 }];
    expect(sortByCost(array, true)).toEqual(expected);
});