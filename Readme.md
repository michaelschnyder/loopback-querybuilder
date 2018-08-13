Loopback QueryBuilder
=====================

This is a small litte sample project that shows how to use `System.Linq.ExpressionVisitor` to convert expressions in anything you desire. 
In my case, I wanted to convert expressions on a `Queryable` to be used on a rather strange Rest-Style backend called [Lookpback.io](https://loopback.io/). Their specification is partially specified on their [Online documentation](https://loopback.io/doc/en/lb3/Where-filter.html). But it seems that their format is heavily inspired by [SailsJs/Waterline](http://waterlinejs.org/) and MongoDb. See sources for more details.

## Sample
```c#
var builder = new LookbackQueryBuilder<Car>();

var query = builder.Where(car => car.Id = 2);
// Result: { where: { id: 2 } }

var query = builder.Where(car => car.Id = 2 && car.Name == "Audi");
// Result: { where: { and: [ { id: 2 }, { name: 'Audi' } ] } }

var query = builder.Where(car => car.Name.Contains("au"));
// Result: { where: { name: { 'like': '%di%' } } }

var query = builder.Where(car => car.Name.Contains("foo") && car.Name == "bla");
// Result: { where: { and: [ { name: { 'like': '%foo%' } }, { name: 'bla' } ] } }

var query = builder.Where(car => car.Name.IsPerfect == true);
// Result: { where: { isPerfect: true } }
```

## Supported Operations
Please note that the aim is **not** so fully support all possible combinations

### Operators
The following operations are supported and covered by tests.
* **Equality** (`==`): Implemented for `string`, `int` and `bool`
* **Contains** (`x.Contains()`): Implemented for `string`)

### Combination
* **And** (`&&`) for `string`, `int` and `bool` Equality-Expressions, including `Contains()`

Any other operations, combinations are currently not supported.

## Additional Sources
* [Waterline Query Syntax](https://sailsjs.com/documentation/concepts/models-and-orm/query-language)
* [Waterline Query Examples](https://github.com/sailshq/waterline-query-docs/blob/master/docs/criteria.md)
* [LINQ: Building an IQueryable Provider – Part II](https://blogs.msdn.microsoft.com/mattwar/2007/07/31/linq-building-an-iqueryable-provider-part-ii/) by Matt Warren - MSFT
* [Mongo DB - C# Client Documentation](https://mongodb-documentation.readthedocs.io/en/latest/ecosystem/tutorial/use-linq-queries-with-csharp-driver.html)


