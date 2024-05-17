namespace MotoApp.Entities
{
    public class BusinessPartner : EntitiyBase
    {
        public string? FirstName { get; set; }

        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}";
    }
}
