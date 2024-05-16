namespace MotoApp.Entities
{
    public class Employee : EntitiyBase
    {
        public Employee(string name) 
        {
        }

        public Employee()
        {
        }
        public string? FirstName { get; set; }

        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}";
    }
}
