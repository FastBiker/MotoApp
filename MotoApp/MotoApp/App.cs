using MotoApp.Components.CsvReader;
using MotoApp.Components.CsvReader.Models;
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
            c => new { c.Manufacturer, c.Year },
            m => new { Manufacturer = m.Name, m.Year },
            (car, manufacturer) =>
                 new
                 {
                     manufacturer.Country,
                     manufacturer.Year,
                     car.Manufacturer,
                     car.Displacement
                 })
            .OrderByDescending(x => x.Displacement)
            .ThenBy(x => x.Manufacturer);

        foreach (var car in carsInCountry)
        {
            Console.WriteLine($"Country: {car.Country}");
            Console.WriteLine($"\t Name: {car.Manufacturer}");
            Console.WriteLine($"\t Displacement: {car.Displacement}");
            Console.WriteLine($"\t Year: {car.Year}");
        }
    }
}
