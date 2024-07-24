namespace MotoApp.Data.Entities
{
    public class BusinessPartner : EntityBase
    {
        public string? FirstName { get; set; }

        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}";
    }
}
