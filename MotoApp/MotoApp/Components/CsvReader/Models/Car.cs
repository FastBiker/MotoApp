namespace MotoApp.Components.CsvReader.Models;

public class Car
{
    public int Year { get; set; } // rok produkcji
    public string Manufacture {  get; set; } // marka
    public string Name { get; set; } // model
    public double Displacement { get; set; } // pojemność skokowa
    public int Cylinders { get; set; } // ilość cylindrów
    public int City { get; set; } // zużycie paliwa w mieście (średnia liczba przejechanych mil [1 mila = 1,609344 km] na galon [1 gal ameryk = 3,785411784 l; 1 gal ang = 4,54 l] zużytego paliwa)
    public int Highway { get; set; } // zużycie paliwa na autostradzie (j.w.)
    public int Combined { get; set;} // przeciętne zużycie paliwa w trybie mieszanym [City + Highways] (j.w.)
}