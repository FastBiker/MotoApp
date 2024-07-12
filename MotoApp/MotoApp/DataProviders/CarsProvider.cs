namespace MotoApp.DataProviders;

using MotoApp.Repositories;
using System.Text;

public class CarsProvider : ICarsProvider
{
    private readonly IRepository<Car> _carsRepository;

    public CarsProvider(IRepository<Car> carsRepository)
    {
        _carsRepository = carsRepository;
    }

    public List<string> GetUniqueCarColor()
    {
        var cars = _carsRepository.GetAll();
        var colors = cars.Select(x => x.Color).Distinct().ToList();
        return colors;
    }


    public decimal GetMinimumPriceOfAllCars()
    {
        var cars = _carsRepository.GetAll();
        return cars.Select(x =>x.ListPrice).Min();
    }

    public List<Car> GetSpecificColumns()
    {
        var cars = _carsRepository.GetAll();
        var list = cars.Select(car => new Car
        {
            Id = car.Id,
            Name = car.Name,
            Type = car.Type,
        }).ToList();
        return list;
    }

    public string AnonymusClass()
    {
        var cars = _carsRepository.GetAll();
        var list = cars.Select(car => new
        {
            Identifier = car.Id,
            ProductName = car.Name,
            ProductType = car.Type,
        });

        StringBuilder sb = new(2048);
        foreach (var car in list)
        {
            sb.AppendLine($"Product ID: {car.Identifier}");
            sb.AppendLine($"Product Name: {car.ProductName}");
            sb.AppendLine($"Product Type: {car.ProductType}");
        }
        return sb.ToString();
    }

    public List<Car> FilterCars(decimal minPrice)
    {
        throw new NotImplementedException();
    }

    public List<Car> OrderByName()
    {
        throw new NotImplementedException();
    }

    public List<Car> OrderByNameDescending()
    {
        throw new NotImplementedException();
    }

    public List<Car> OrderByColorAndName()
    {
        throw new NotImplementedException();
    }

    public List<Car> OrderByColorAndNameDesc()
    {
        throw new NotImplementedException();
    }
}
