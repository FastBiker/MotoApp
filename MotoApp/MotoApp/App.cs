using MotoApp.Entities;
using MotoApp.Repositories;

namespace MotoApp;

public class App : IApp
{
    private readonly IRepository<Employee> _employeesRepository;

    public App(IRepository<Employee> employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public void Run()
    {
        Console.WriteLine("I'm here in Run() method");

        //adding
        var employees = new[]
        {
        new Employee { FirstName = "Kleofas" },
        new Employee { FirstName = "Hieronim" },
        new Employee { FirstName = "Teofil" }
        };

        foreach (var employee in employees)
        {
            _employeesRepository.Add(employee);
        }

        _employeesRepository.Save();

        var items = _employeesRepository.GetAll();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}
