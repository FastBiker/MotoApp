using MotoApp.Data;
using MotoApp.Entities;
using MotoApp.Repositories;

//var employeeRepository = new GenericRepository<Employee, int>();
//employeeRepository.Add(new Employee { FirstName = "Adam" });
//employeeRepository.Add(new Employee { FirstName = "Piotr" });
//employeeRepository.Add(new Employee { FirstName = "Zuzia" });
//employeeRepository.Save();

var sqlRepository = new SqlRepository(new MotoAppDbContext());
sqlRepository.Add(new Employee { FirstName = "Adam" });
sqlRepository.Add(new Employee { FirstName = "Piotr" });
sqlRepository.Add(new Employee { FirstName = "Zuzia" });
sqlRepository.Save();

var emp = sqlRepository.GetById(1);
Console.WriteLine(emp.ToString());