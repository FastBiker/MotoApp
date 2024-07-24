namespace MotoApp.Components.CsvReader;

using MotoApp.Components.CsvReader.Models;

public interface ICsvReader
{
    List<Car> ProcessCars(string filePath);
}
