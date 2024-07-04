using System.Text;
using MotoApp.Entities;

public class Car : EntityBase
{
    public string Name { get; set; }

    public string Color { get; set; }

    public decimal StandardCost { get; set; }

    public decimal ListPrice { get; set; }

    public string Type { get; set; }

    //Calculated Properties
    public int? NameLength { get; set; }

    public decimal? TotalSales { get; set; }

    #region ToString Override
    public override string ToString()
    {
        StringBuilder sb = new(1024);

        sb.Append($"{Name} ID: {Id}\n");
        sb.Append($" Color: {Color} Type: {(Type ?? "n/a")}\n");
        sb.Append($" Cost: {StandardCost:c} Price: {ListPrice:c}\n" );
        if (NameLength.HasValue) 
        {
            sb.AppendLine($" Name Lenght: {NameLength}");
        }
        if (TotalSales.HasValue)
        {
            sb.AppendLine($" Total Sales: {TotalSales:c}");
        }
        return sb.ToString();
    }
}
#endregion