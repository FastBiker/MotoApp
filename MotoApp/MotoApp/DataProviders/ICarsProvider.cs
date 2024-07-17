namespace MotoApp.DataProviders;

public interface ICarsProvider
{
    List<Car> FilterCars(decimal minPrice);

    // select

    List<string> GetUniqueCarColor();

    decimal GetMinimumPriceOfAllCars();

    List<Car> GetSpecificColumns();

    string AnonymusClass();

    // order by

    List<Car> OrderByName();

    List<Car> OrderByNameDescending();

    List<Car> OrderByColorAndName();

    List<Car> OrderByColorAndNameDesc();

    // where

    List<Car> WhereStartsWith(string prefix);

    List<Car> WhereStartsWithAndCostIsGreaterThan(string prefix, decimal cost);

    List<Car> WhereColorIs(string color);

    // first, last, single

    Car FirstByColor(string color);

    Car? FirstOrDefaultByColor(string color);

    Car FirstOrDefaultByColorOrDefault(string color);

    Car LastByColor(string color);

    Car SingleById(int id);

    Car? SingleOrDefaultById(int id);

    Car? SingleOrDefaultByIdOrDefault(int id);

    // Take

    List<Car> TakeCars(int howMany);

    List<Car> TakeCars(Range range);

    List<Car> TakeCarsWhileNameStartsWith(string prefix);

    // Skip

    List<Car> SkipCars(int howMany);

    List<Car> SkipCarsWhileNameStartsWith(string prefix);
}
