// See https://aka.ms/new-console-template for more information
using AddVsAddRange;
using Domain.Models.Settings;
using EntityFrameworkNews.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();
services.AddDbContext<ApplicationDbContext>(x =>
    x.UseSqlServer(Connection.Learning));

services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


var serviceProvider = services.BuildServiceProvider();
var dbService = serviceProvider.GetService<IApplicationDbContext>();

AdditionService additionService = new(dbService);

Console.WriteLine("AddUsersWithSaveChangesInLoop");
additionService.AddUsersWithSaveChangesInLoop();
Console.WriteLine();

Console.WriteLine("AddUsersWithOnlyOneSaveChanges");
additionService.AddUsersWithOnlyOneSaveChanges();
Console.WriteLine();

Console.WriteLine("AddRangeUsers");
additionService.AddRangeUsers();
Console.WriteLine();