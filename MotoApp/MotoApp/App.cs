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
            .Element("Manufacturers")?
            .Elements("Car")?
            .Where(x => x.Attribute("Combined")?.Value == "22")
            .Select(x => x.Attribute("Model")?.Value)
            .Distinct()
            .OrderDescending();

        if (names != null ) 
        {
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
        else 
        {
            Console.WriteLine("Nie ma takiego samochodu");
        }

    }

    private void CreateXml()
    {
        Console.WriteLine("Połączenie 2 plików CSV i zapisanie ich w pliku xml");
        Console.WriteLine("-------------------------------------------");

        var recordsCars = _csvReader.ProcessCars("Resourses\\Files\\fuel.csv");
        var recordsManufacturers = _csvReader.ProcessManufacturers("Resourses\\Files\\manufacturers.csv");

        var document = new XDocument(new XElement("Manufacturers"));

        var carsAllInformations = recordsManufacturers
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

        foreach (var element in carsAllInformations)
        {
                var manufacturerElements = new XElement("Manufacturer",
                    new XAttribute("Name", element.Name),
                    new XAttribute("Country", element.Country),
                        new XElement("Cars",
                            new XAttribute("Country", element.Country),
                            new XAttribute("CombinedSum", element.Cars.Sum(x => x.Combined)),
                            element.Cars.Select(car => 
                                new XElement("Car",
                                    new XAttribute("Model", car.Name),
                                    new XAttribute("Combined", car.Combined))
                                )));

            document.Root.Add(manufacturerElements);
        } 
        document.Save("fuel.xml");
    }
}
