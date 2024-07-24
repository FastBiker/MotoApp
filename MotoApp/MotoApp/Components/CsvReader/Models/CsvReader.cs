using MotoApp.Components.CsvReader.Extensions;

namespace MotoApp.Components.CsvReader.Models;

public class CsvReader : ICsvReader
{
    public List<Car> ProcessCars(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<Car>();
        }

        var cars = File.ReadAllLines(filePath)
            .Skip(1)
            .Where(x => x.Length > 1)
            .ToCar();

        return cars.ToList();   
    }
}
