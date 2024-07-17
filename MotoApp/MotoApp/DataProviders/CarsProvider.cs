namespace MotoApp.DataProviders;

using MotoApp.DataProviders.Extenions;
using MotoApp.Repositories;
using System.Drawing;
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
        var cars = _carsRepository.GetAll();
        return cars.OrderBy(x => x.Name).ToList();
    }

    public List<Car> OrderByNameDescending()
    {
        var cars = _carsRepository.GetAll();
        return cars.OrderByDescending(x => x.Name).ToList();
    }

    public List<Car> OrderByColorAndName()
    {
        var cars = _carsRepository.GetAll();
        return cars
            .OrderBy(x => x.Color)
            .ThenBy(x => x.Name)
            .ToList();
    }

    public List<Car> OrderByColorAndNameDesc()
    {
        var cars = _carsRepository.GetAll();
        return cars
            .OrderByDescending(x => x.Color)
            .ThenByDescending(x => x.Name)
            .ToList();
    }

    public List<Car> WhereStartsWith(string prefix)
    {
        var cars = _carsRepository.GetAll();
        return cars.Where(x=>x.Name.StartsWith(prefix)).ToList();
    }

    public List<Car> WhereStartsWithAndCostIsGreaterThan(string prefix, decimal cost)
    {
        var cars = _carsRepository.GetAll();
        return cars.Where(x => x.Name.StartsWith(prefix) && x.StandardCost > cost).ToList();
    }

    public List<Car> WhereColorIs(string color)
    {
        var cars = _carsRepository.GetAll();
        return cars.ByColor(color).ToList();
    }

    public Car FirstByColor(string color)
    {
        var cars = _carsRepository.GetAll();
        return cars.First(x => x.Color == color);
    }

    public Car? FirstOrDefaultByColor(string color)
    {
        var cars = _carsRepository.GetAll();
        return cars.FirstOrDefault(x => x.Color == color);
    }

    public Car FirstOrDefaultByColorOrDefault(string color)
    {
        var cars = _carsRepository.GetAll();
        return cars.FirstOrDefault(
            x => x.Color == color,
            new Car { Id = -1, Name = "NOT FOUD" });
    }

    public Car LastByColor(string color)
    {
        var cars = _carsRepository.GetAll();
        return cars.Last(x => x.Color == color);
    }

    public Car SingleById(int id)
    {
        var cars = _carsRepository.GetAll();
        return cars.Single(x => x.Id == id);
    }

    public Car? SingleOrDefaultById(int id)
    {
        var cars = _carsRepository.GetAll();
        return cars.SingleOrDefault(x => x.Id == id);
    }

    public Car SingleOrDefaultByIdOrDefault(int id)
    {
        var cars = _carsRepository.GetAll();
        return cars.SingleOrDefault(
            x => x.Id == id,
            new Car { Id = -1, Name = "NOT FOUD" });
    }

    public List<Car> TakeCars(int howMany)
    {
        var cars = _carsRepository.GetAll();
        return cars
            .OrderBy(x => x.Name)
            .Take(howMany)
            .ToList();
    }

    public List<Car> TakeCars(Range range)
    {
        var cars = _carsRepository.GetAll();
        return cars
            .OrderBy(x => x.Name)
            .Take(range)
            .ToList();
    }

    public List<Car> TakeCarsWhileNameStartsWith(string prefix)
    {
        var cars = _carsRepository.GetAll();
        return cars
            .OrderBy(x => x.Name)
            .TakeWhile(x => x.Name.StartsWith(prefix))
            .ToList();
    }

    public List<Car> SkipCars(int howMany)
    {
        var cars = _carsRepository.GetAll();
        return cars
            .OrderBy(x => x.Name)
            .Skip(howMany)
            .ToList();
    }

    public List<Car> SkipCarsWhileNameStartsWith(string prefix)
    {
        var cars = _carsRepository.GetAll();
        return cars
            .OrderBy(x => x.Name)
            .SkipWhile(x => x.Name.StartsWith(prefix))
            .ToList();
    }

    public List<string> DistinctAllColors()
    {
        var cars = _carsRepository.GetAll();
        var colors = cars
            .Select(x => x.Color)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
        return colors;
    }

    public List<Car> DistinctByColors()
    {
        var cars = _carsRepository.GetAll();
        return cars
            .DistinctBy(x => x.Color)
            .OrderBy(x => x.Color)
            .ToList();
    }

    public List<Car[]> ChunkCars(int size)
    {
        var cars = _carsRepository.GetAll();
        return cars.Chunk(size).ToList();
    }
}
