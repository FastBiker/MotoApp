using Microsoft.Extensions.DependencyInjection;
using MotoApp;
using MotoApp.Entities;
using MotoApp.Repositories;

var sercices = new ServiceCollection();
sercices.AddSingleton<IApp, App>();
sercices.AddSingleton<IRepository<Employee>, ListRepository<Employee>>();

var serviceProvider = sercices.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();

//using MotoApp.Data;
//using MotoApp.Entities;
//using MotoApp.Repositories;
//using MotoApp.Repositories.Extensions;

//var employeeRepository = new SqlRepository<Employee>(new MotoAppDbContext(), EmployeeAdded);
//employeeRepository.ItemAdded += EmployeeRepositoryOnItemAdded;

//static void EmployeeRepositoryOnItemAdded(object? sender, Employee e)
//{
//    Console.WriteLine($"Employee added ==> {e.FirstName} from {sender?.GetType().Name}");
//}

//    AddEmployees(employeeRepository);
//    WriteAllToConsole(employeeRepository);

//static void EmployeeAdded(object item)
//{
//    var employee = (Employee)item;
//    Console.WriteLine($"{employee.FirstName} added");
//}


//static void AddEmployees(IRepository<BusinessPartner> businessPartnerRepository)
//{
//    var businessPartners = new[]
//    {
//        new BusinessPartner { FirstName = "Adam" },
//        new BusinessPartner { FirstName = "Piotr" },
//        new BusinessPartner { FirstName = "Zuzia" }
//    };

//    businessPartnerRepository.AddBatch(businessPartners);
//    AddBatch(businessPartnerRepository, businessPartners);
//    employeeRepository.Add(new Employee { FirstName = "Adam" });
//    employeeRepository.Add(new Employee { FirstName = "Piotr" });
//    employeeRepository.Add(new Employee { FirstName = "Zuzia" });
//    employeeRepository.Save();
//}

//static void AddEmployees(IRepository<Employee> employeeRepository)
//{
//    var employees = new[]
//    {
//        new Employee { FirstName = "Kleofas" },
//        new Employee { FirstName = "Hieronim" },
//        new Employee { FirstName = "Teofil" }
//    };

//    employeeRepository.AddBatch(employees);
    //AddBatch(businessPartnerRepository, businessPartners);
    //}
    //static void AddBatch<T>(IRepository<T> repository, T[] items) 
    //    where T : class, IEntity
    //{
    //    foreach (var item in items)
    //    {
    //        repository.Add(item);
    //    }

    //    repository.Save();
//}

//static void AddBatch<T>(IRepository<T> repository, T[] items)
//            where T : class, IEntity
//{
//    foreach (var item in items)
//    {
//        repository.Add(item);
//    }

//    repository.Save();
//}

//static void WriteAllToConsole(IReadRepository<IEntity> repository)
//{
//    var items = repository.GetAll();
//    foreach (var item in items)
//    {
//        Console.WriteLine(item);
//    }
//}