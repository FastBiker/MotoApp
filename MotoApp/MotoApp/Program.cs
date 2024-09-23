using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MotoApp;
using MotoApp.Components.CsvReader;
using MotoApp.Data;
using MotoApp.Data.Entities;
using MotoApp.Data.Repositories;

var sercices = new ServiceCollection();
sercices.AddSingleton<IApp, App>();
sercices.AddSingleton<IRepository<Employee>, ListRepository<Employee>>();
sercices.AddSingleton<IRepository<Car>, ListRepository<Car>>();
sercices.AddSingleton<ICsvReader, CsvReader>();
sercices.AddDbContext<MotoAppDbContext>(options => options
    .UseSqlServer("Data Source=LAPTOP-R6OVM9N5\\SQLEXPRESS;Initial Catalog=MotoAppStorage;Integrated Security=True;Trust Server Certificate=True"));

var serviceProvider = sercices.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
