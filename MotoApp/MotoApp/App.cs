using MotoApp.Components.CsvReader;
using System.Text.RegularExpressions;

namespace MotoApp;

public class App : IApp
{
    private readonly ICsvReader _csvReader;
    public App(ICsvReader csvReader)
    {
        _csvReader = csvReader;
    }

    public void Run()
    {
        var cars = _csvReader.ProcessCars("Resourses\\Files\\fuel.csv");
        var manufacturers = _csvReader.ProcessManufacturers("Resourses\\Files\\manufacturers.csv");

        //var groups = cars
        //    .GroupBy(x => x.Manufacturer)
        //    .Select(g => new
        //    {
        //        Name = g.Key,
        //        Max = g.Max(c => c.Combined),
        //        Average = g.Average(c => c.Combined)
        //    })
        //    .OrderBy(x => x.Name);

        //foreach ( var group in groups) 
        //{
        //    Console.WriteLine($"{group.Name}");
        //    Console.WriteLine($"\t Max: {group.Max}");
        //    Console.WriteLine($"\t Average: {group.Average}");
        //}

        var carsInCountry = cars.Join(
            manufacturers,
            x => x.Manufacture,
            x => x.Name,
            (car, manufacturer) =>
                 new
                 {
                     manufacturer.Country,
                     car.Manufacture,
                     car.Combined
                 })
            .OrderByDescending(x => x.Combined)
            .ThenBy(x => x.Manufacture);

        foreach (var car in carsInCountry)
        {
            Console.WriteLine($"Country: {car.Country}");
            Console.WriteLine($"\t Name: {car.Manufacture}");
            Console.WriteLine($"\t Combined: {car.Combined}");
        }
    }
}
