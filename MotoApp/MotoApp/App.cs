namespace MotoApp;

using MotoApp.Components.CsvReader;
using MotoApp.Data;
using MotoApp.Data.Entities;
using System.Xml.Linq;

public class App : IApp
{
    private readonly ICsvReader _csvReader;
    private readonly MotoAppDbContext _motoAppDbContext;

    public App(ICsvReader csvReader, MotoAppDbContext motoAppDbContext)
    {
        _csvReader = csvReader;
        _motoAppDbContext = motoAppDbContext;
        _motoAppDbContext.Database.EnsureCreated();
    }

    public void Run()
    {
        //InsertData();
        //ReadAllCarsFromDb();
        //ReadGroupedCarsFromDb();

        var cayman = this.ReadFirst("Cayman");
        if(cayman == null) 
        {
            Console.WriteLine("This name dosen't exist");
            return;
        }
        else 
        {
            cayman.Name = "Misie Gumisie" ;
            _motoAppDbContext.SaveChanges();
        }
    }

    private Car? ReadFirst(string name) 
    {
        return _motoAppDbContext.Cars.FirstOrDefault(x => x.Name == name);
    }

    private void ReadGroupedCarsFromDb()
    {
        var groups = _motoAppDbContext
            .Cars
            .GroupBy (x => x.Manufacturer)
            .Select (x => new
            {
                Name = x.Key,
                Cars = x.ToList()
            })
            .ToList();

        foreach (var group in groups) 
        {
            Console.WriteLine(group.Name);
            Console.WriteLine("============");

            foreach (var car in group.Cars) 
            {
                Console.WriteLine($"\t {car.Name}: {car.Combined}");
            }
            Console.WriteLine();
        }
    }

    private void ReadAllCarsFromDb()
    {
        var carsFromDb = _motoAppDbContext.Cars.ToList();

        foreach (var carFromDb in carsFromDb)
        {
            Console.WriteLine($"\t {carFromDb.Name}: {carFromDb.Combined}");
        }
    }

    private void InsertData() 
    {
        var cars = _csvReader.ProcessCars("Resourses\\Files\\fuel.csv");

        foreach (var car in cars)
        {
            _motoAppDbContext.Cars.Add(new Car()
            {
                Manufacturer = car.Manufacturer,
                Name = car.Name,
                Year = car.Year,
                Cylinders = car.Cylinders,
                Displacement = car.Displacement,
                City = car.City,
                Highway = car.Highway,
                Combined = car.Combined,
            });
        }

        _motoAppDbContext.SaveChanges();
    }
}
