using MotoApp.DataProviders;
using MotoApp.Entities;
using MotoApp.Repositories;
using System.Drawing;

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

        foreach (var car in _carsProvider.TakeCarsWhileNameStartsWith("Z"))
        {
            Console.WriteLine(car);
        }

        //foreach (var car in _carsProvider.FilterCars(5000))
        //{
        //    Console.WriteLine(car);
        //}

        //Console.WriteLine(_carsProvider.SingleOrDefaultByIdOrDefault(17));

    }

    public static List<Car> GenerateSampleCars()
    {
        return new List<Car>()
        {
            new Car {
            Id = 1,
            Name = "Fiat 125p",
            Color = "yellow/creamy",
            StandardCost = 1059.31M,
            ListPrice = 1431.50M,
            Type = "sedan"
            },
            new Car {
            Id = 2,
            Name = "Land Rover Ninety",
            Color = "yellow/white",
            StandardCost = 62256.12M,
            ListPrice = 65498.60M,
            Type = "samochód terenowy"
            },
            new Car {
            Id = 3,
            Name = "Jeep Wrangler",
            Color = "black",
            StandardCost = 63308.93M,
            ListPrice = 52350.33M,
            Type = "samochód terenowy"
            },
            new Car {
            Id = 4,
            Name = "Jeep Wrangler",
            Color = "red",
            StandardCost = 63308.93M,
            ListPrice = 39789.342M,
            Type = "samochód terenowy"
            },
            new Car {
            Id = 5,
            Name = "Saab 97 II (Sonett III)",
            Color = "blue metallic",
            StandardCost = 60516.00M,
            ListPrice = 64350.60M,
            Type = "coupe"
            },
            new Car {
            Id = 6,
            Name = "Ford Mustang SVO",
            Color = "white",
            StandardCost = 28141.00M,
            ListPrice = 32050.60M,
            Type = "coupe"
            },
            new Car {
            Id = 7,
            Name = "Porsche 911 turbo (III)",
            Color = "red",
            StandardCost = 310000.00M,
            ListPrice = 320000.60M,
            Type = "coupe"
            },
            new Car {
            Id = 8,
            Name = "Mitsubishi Colt (II)",
            Color = "blue metallic",
            StandardCost = 51260.00M,
            ListPrice = 5200.60M,
            Type = "hatchback"
            },
            new Car {
            Id = 9,
            Name = "Saab 9000 Turbo",
            Color = "white",
            StandardCost = 60516.00M,
            ListPrice = 64350.60M,
            Type = "liftback"
            },
            new Car {
            Id = 10,
            Name = "Lamborgini 400GT Espada",
            Color = "white",
            StandardCost = 605000.00M,
            ListPrice = 603500.60M,
            Type = "coupe"
            },
            new Car {
            Id = 11,
            Name = "Ford Sierra",
            Color = "yellow",
            StandardCost = 7900.00M,
            ListPrice = 8500.60M,
            Type = "liftback"
            },
        };
    }
}
