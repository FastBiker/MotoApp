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
        CreateXml();
        QueryXml();
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
            
        }
        document.Save("fuel.xml");
    }
}
