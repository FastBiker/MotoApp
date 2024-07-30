using MotoApp.Components.CsvReader;

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

        var groups = cars
            .GroupBy(x => x.Manufacture)
            .Select(g => new
            {
                Name = g.Key,
                Max = g.Max(c => c.Combined),
                Average = g.Average(c => c.Combined)
            })
            .OrderBy(x => x.Name);

        foreach ( var group in groups) 
        {
            Console.WriteLine($"{group.Name}");
            Console.WriteLine($"\t Max: {group.Max}");
            Console.WriteLine($"\t Average: {group.Average}");
        }
    }
}
