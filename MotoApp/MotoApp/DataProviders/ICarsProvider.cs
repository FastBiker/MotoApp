namespace MotoApp.DataProviders;

public interface ICarsProvider
{
    List<Car> FilterCars(decimal minPrice);

    List<string> GetUniqueCarColor();

    decimal GetMinimumPriceOfAllCars();


}
