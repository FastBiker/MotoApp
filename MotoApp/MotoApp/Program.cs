using MotoApp.Data;
using MotoApp.Entities;
using MotoApp.Repositories;

var employeeRepository = new SqlRepository<Employee>(new MotoAppDbContext());
AddEmployees(employeeRepository);
AddManagers(employeeRepository);
WriteAllToConsole(employeeRepository);


static void AddEmployees(IRepository<Employee> employeeRepository) 
{
    employeeRepository.Add(new Employee { FirstName = "Adam" });
    employeeRepository.Add(new Employee { FirstName = "Piotr" });
    employeeRepository.Add(new Employee { FirstName = "Zuzia" });
    employeeRepository.Save();
}

static void AddManagers(IWriteRepository<Manager> managerRepository)
{
    managerRepository.Add(new Manager { FirstName = "Tomek" });
    managerRepository.Add(new Manager { FirstName = "Przemek" });
    managerRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository) 
{
        var items = repository.GetAll();
        foreach ( var item in items) 
        {
            Console.WriteLine(item);
        }
}
