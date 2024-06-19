using MotoApp.DataProviders;
using MotoApp.Entities;
using MotoApp.Repositories;

namespace MotoApp;

public class App : IApp
{
    private readonly IRepository<Employee> _employeesRepository;
    private readonly IRepository<Car> _carsRepository;
    private readonly ICarsProvider _carsProvider;

    public App(
        IRepository<Employee> employeesRepository, 
        IRepository<Car> carsRepository, 
        ICarsProvider carsProvider)
    {
        _employeesRepository = employeesRepository;
        _carsRepository = carsRepository;
        _carsProvider = carsProvider;
    }

    public void Run()
    {
        Console.WriteLine("I'm here in Run() method");

        //adding
        var employees = new[]
        {
        new Employee { FirstName = "Kleofas" },
        new Employee { FirstName = "Hieronim" },
        new Employee { FirstName = "Teofil" }
        };

        foreach (var employee in employees)
        {
            _employeesRepository.Add(employee);
        }

        _employeesRepository.Save();

        //reading

        var items = _employeesRepository.GetAll();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }

        //cars
        var cars = GenerateSampleCars();
        foreach (var car in cars)
        {
            _carsRepository.Add(car);
        }

        foreach (var car in _carsProvider.FilterCars(1000))
        {
            Console.WriteLine(car);
        }
    }

    public static List<Car> GenerateSampleCars()
    {
        return new List<Car>()
        {
            new Car {
            Id = 680,
            Name = "Fiat 125p",
            Color = "yellow",
            StandardCost = 1059.31M,
            ListPrice = 1431.50M,
            Type = "sedan"
            }
        };
    }
}
