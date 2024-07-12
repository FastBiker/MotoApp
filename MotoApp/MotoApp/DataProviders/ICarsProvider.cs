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
}
