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
    public int? NameLenght { get; set; }

    public decimal? TotalSales { get; set; }

    #region ToString Override
    public override string ToString()
    {
        StringBuilder sb = new(1024);

        sb.Append($"{Name} ID: {Id}");
        sb.Append($" Color: {Color} Type: {(Type ?? "n/a")}");
        sb.Append($" Cost: {StandardCost:c} Price: {ListPrice:c}" );
        if (NameLenght.HasValue) 
        {
            sb.AppendLine($" Name Lenght: {NameLenght}");
        }
        if (TotalSales.HasValue)
        {
            sb.AppendLine($" Total Sales: {TotalSales:c}");
        }
        return sb.ToString();
    }
}
#endregion