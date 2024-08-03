namespace MotoApp;

using MotoApp.Components.CsvReader;
using System;
using System.Linq;
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
        var document = XDocument.Load("fuel.xml");
        var names = document
            .Element("Cars")?
            .Elements("Car")
            .Where(x => x.Attribute("Displacement")?.Value == 6.0.ToString())
            .Select(x => x.Attribute("Manufacturer")?.Value)
            .Distinct()
            .OrderDescending();

        foreach (var name in names) 
        {
            Console.WriteLine(name);
        }
        
    }

    private void CreateXml()
    {
        var records = _csvReader.ProcessCars("Resourses\\Files\\fuel.csv");

        var document = new XDocument();

        var cars = new XElement("Cars", records
            .Select(x =>
                new XElement("Car",
                    new XAttribute("Manufacturer", x.Manufacturer),
                    new XAttribute("Name", x.Name),
                    new XAttribute("Displacement", x.Displacement))));

        document.Add(cars);
        document.Save("fuel.xml");

    }
}
