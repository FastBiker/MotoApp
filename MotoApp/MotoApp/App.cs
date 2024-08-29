namespace MotoApp;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MotoApp.Components.CsvReader;
using MotoApp.Components.CsvReader.Models;
using MotoApp.Data.Entities;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Linq;


public class App : IApp
{
    private readonly ICsvReader _csvReader;
    public App(ICsvReader csvReader)
    {
        _csvReader = csvReader;
    }

    public void Run()
    {
        Console.WriteLine("Pogrupowanie danych z pliku 'fuel.csv'");
        Console.WriteLine("-------------------------------------------");

        var cars = _csvReader.ProcessCars("Resourses\\Files\\fuel.csv");
        var manufacturers = _csvReader.ProcessManufacturers("Resourses\\Files\\manufacturers.csv");

        var groups = cars
            .GroupBy(x => new { x.Manufacturer, x.Displacement })
            //.Select(g => new
            //{
            //    Name = g.Key,

            //})
            .OrderBy(x => x.Key.Manufacturer)
            .ThenBy(x => x.Key.Displacement);

        foreach (var group in groups)
        {
            Console.WriteLine($"{group.Key.Manufacturer}, {group.Key.Displacement}");
            Console.WriteLine($"\t MaxDisplacement: {group.Max(x => x.Displacement)}");

            foreach (var element in group)
                {
                Console.WriteLine($"\t {element.Name} / {element.Displacement} / {element.Cylinders} / {element.Combined}");
                }
            //Console.WriteLine($"\t CombinedSum: {group.Sum}");
            //Console.WriteLine($"\t CombinedAverage: {group.Average}");
            //Console.WriteLine($"\t MaxDisplacement: {group.Max}");
            //Console.WriteLine($"\t MinDisplacement: {group.Min}");
            Console.WriteLine();

        }

        JoinCars();
        CreateXml();

        QueryXml();
    }


    private void JoinCars()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Połączenie 2 plików CSV i pogrupowanie danych");
        Console.WriteLine("----------------------------------------------");
        Console.ResetColor();
        Console.WriteLine();

        var cars = _csvReader.ProcessCars("Resourses\\Files\\fuel.csv");
        var manufacturers = _csvReader.ProcessManufacturers("Resourses\\Files\\manufacturers.csv");

        int CombinedSum = cars.Sum(x => x.Combined);

        var groups = manufacturers
            .GroupJoin(cars,
            manufacturer => manufacturer.Name,
            car => car.Manufacturer,
            (m, g) =>
                new
                {
                    m.Name,
                    Cars = g
                })
            .OrderBy(x => x.Name);

        foreach (var element in groups)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Manufacturer: {element.Name.ToUpper()}");
            Console.WriteLine("-------------------------");
            //Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t Count: {element.Cars.Count()}");
            Console.WriteLine($"\t Max: {element.Cars.Max(x => x.Displacement)}");
            Console.WriteLine($"\t Min: {element.Cars.Min(x => x.Displacement)}");
            Console.WriteLine($"\t Average: {element.Cars.Average(x => x.Displacement)}");
            Console.WriteLine();

            foreach (var car in element.Cars) 
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\t {car.Name} / {car.Displacement} / {car.Cylinders} / {car.Combined}");
                Console.WriteLine();
                Console.ResetColor ();
            }

        }

        Console.WriteLine("Połączenie 2 plików CSV, następnie pogrupowanie");
        Console.WriteLine("-------------------------------------------");

        var carsInCountry = cars.Join(
            manufacturers,
            x => x.Manufacturer,
            x => x.Name,
            (car, manufacturer) =>
                 new
                 {
                     manufacturer.Country,
                     car.Manufacturer,
                     car.Name,
                     car.Combined,
                     car.Cylinders,
                     car.Displacement
                 })
            .OrderByDescending(x => x.Combined)
            .ThenBy(x => x.Manufacturer);

        foreach (var car in carsInCountry)
        {
            Console.WriteLine($"Country: {car.Country}");
            Console.WriteLine($"\t Manufacturer: {car.Manufacturer}");
            Console.WriteLine($"\t Name: {car.Name}");
            Console.WriteLine($"\t Combined: {car.Combined}");
            Console.WriteLine();

        }
    }

    private void QueryXml()
    {
        Console.WriteLine("Odczyt z pliku 'fuel.xml");
        Console.WriteLine("--------------------------");
        Console.WriteLine();

        var document = XDocument.Load("fuel.xml");
        var names = document
            .Element("Cars")?
            .Elements("Car")
            .Where(x => x.Attribute("Model")?.Value == "X3 xDrive28d")
            .Select(x => x.Attribute("Combined")?.Value)
            .Distinct()
            .OrderDescending();

        foreach (var name in names)
        {
            Console.WriteLine(name);
        }

    }

    private void CreateXml()
    {
        Console.WriteLine("Połączenie 2 plików CSV i zapisanie ich w pliku xml");
        Console.WriteLine("-------------------------------------------");

        var recordsCars = _csvReader.ProcessCars("Resourses\\Files\\fuel.csv");
        var recordsManufacturers = _csvReader.ProcessManufacturers("Resourses\\Files\\manufacturers.csv");

        var document = new XDocument();

        var carsAllInformation = recordsManufacturers
            .GroupJoin(
            recordsCars,
            manufacturer => manufacturer.Name,
            car => car.Manufacturer,
            (m, g) =>
            new
            {
                m.Name,
                m.Country,
                Cars = g
            })
            .OrderBy(x => x.Name);

        foreach (var element in carsAllInformation)
        {
            var cars = new XElement("Manufacuturers", carsAllInformation
                .Select(element =>
                new XElement("Manufacturer",
                    new XAttribute("Name", element.Name),
                    new XAttribute("Country", element.Country),
                        new XElement("Cars",
                            new XAttribute("Country", element.Country),
                            new XAttribute("CombinedSum", element.Cars.Sum(x => x.Combined)),
                            from car in element.Cars
                            select
                                new XElement("Car",
                                    new XAttribute("Model", car.Name),
                                    new XAttribute("Combined", car.Combined))
                                ))));

            document.Add(cars);
            document.Save("fuel.xml");
        }
    }
}
