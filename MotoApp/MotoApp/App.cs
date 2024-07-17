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
        foreach (var item in cars)
        {
            _carsRepository.Add(item);
        }

        Console.WriteLine();
        Console.WriteLine("GetUniqueCarColor");
        Console.WriteLine("-----------------");
        foreach (var item in _carsProvider.GetUniqueCarColor())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("GetMinimumPriceOfAllCars");
        Console.WriteLine("------------------------");
        Console.WriteLine(_carsProvider.GetMinimumPriceOfAllCars());

        Console.WriteLine();
        Console.WriteLine("GetSpecificColumns");
        Console.WriteLine("------------------");
        foreach (var item in _carsProvider.GetSpecificColumns())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("AnonymusClass");
        Console.WriteLine("-------------");
        Console.WriteLine(_carsProvider.AnonymusClass());

        Console.WriteLine();
        Console.WriteLine("OrderByName");
        Console.WriteLine("-----------");
        foreach (var item in _carsProvider.OrderByName())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("OrderByNameDescending");
        Console.WriteLine("---------------------");
        foreach (var item in _carsProvider.OrderByNameDescending())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("OrderByColorAndName");
        Console.WriteLine("-------------------");
        foreach (var item in _carsProvider.OrderByColorAndName())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("OrderByColorAndNameDesc");
        Console.WriteLine("-----------------------");
        foreach (var item in _carsProvider.OrderByColorAndNameDesc())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("WhereStartsWith");
        Console.WriteLine("---------------");
        foreach (var item in _carsProvider.WhereStartsWith("Lam"))
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("WhereStartsWithAndCostIsGreaterThan");
        Console.WriteLine("-----------------------------------");
        foreach (var item in _carsProvider.WhereStartsWithAndCostIsGreaterThan("F", 25000))
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("WhereColorIs Red");
        Console.WriteLine("------------");
        foreach (var item in _carsProvider.WhereColorIs("red"))
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("FirstByColor");
        Console.WriteLine("------------");
        Console.WriteLine(_carsProvider.FirstByColor("blue metallic"));

        Console.WriteLine();
        Console.WriteLine("FirstOrDefaultByColor");
        Console.WriteLine("---------------------");
        Console.WriteLine(_carsProvider.FirstOrDefaultByColor("green"));

        Console.WriteLine();
        Console.WriteLine("FirstOrDefaultByColorOrDefault");
        Console.WriteLine("------------------------------");
        Console.WriteLine(_carsProvider.FirstOrDefaultByColorOrDefault("orange"));

        Console.WriteLine();
        Console.WriteLine("LastByColor");
        Console.WriteLine("-----------");
        Console.WriteLine(_carsProvider.LastByColor("white"));

        Console.WriteLine();
        Console.WriteLine("SingleById");
        Console.WriteLine("----------");
        Console.WriteLine(_carsProvider.SingleById(9));

        Console.WriteLine();
        Console.WriteLine("SingleOrDefaultById");
        Console.WriteLine("-------------------");
        Console.WriteLine(_carsProvider.SingleOrDefaultById(12));

        Console.WriteLine();
        Console.WriteLine("SingleOrDefaultByIdOrDefault");
        Console.WriteLine("----------------------------");
        Console.WriteLine(_carsProvider.SingleOrDefaultByIdOrDefault(17));

        Console.WriteLine();
        Console.WriteLine("TakeCars");
        Console.WriteLine("-------------------");
        foreach (var item in _carsProvider.TakeCars(5))
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("TakeCars");
        Console.WriteLine("--------");
        foreach (var item in _carsProvider.TakeCars(4..9))
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("TakeCarsWhileNameStartsWith");
        Console.WriteLine("---------------------------");
        foreach (var item in _carsProvider.TakeCarsWhileNameStartsWith("F"))
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("SkipCars");
        Console.WriteLine("--------");
        foreach (var item in _carsProvider.SkipCars(8))
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("SkipCarsWhileNameStartsWith F");
        Console.WriteLine("-----------------------------");
        foreach (var item in _carsProvider.SkipCarsWhileNameStartsWith("F"))
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("DistinctAllColors");
        Console.WriteLine("-----------------");
        foreach (var item in _carsProvider.DistinctAllColors())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("DistinctByColors");
        Console.WriteLine("----------------");
        foreach (var item in _carsProvider.DistinctByColors())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
        Console.WriteLine("ChunkCars");
        Console.WriteLine("---------");
        foreach (var item in _carsProvider.ChunkCars(3))
        {
            Console.WriteLine("Chunk:");
            foreach(var i in item) 
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("####");
        }

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
