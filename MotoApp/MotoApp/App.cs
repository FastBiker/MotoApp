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
    }
}
