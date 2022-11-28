using EntityFrameworkNews.Data;
using EntityFrameworkNews.Models.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Learning Entity Framework API", Version = "v1" }));

builder.Services.AddDbContext<ApplicationDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionString.Learning))));

var app = builder.Build();

// Configure the HTTP request pipeline.
#region Swagger
app.UseSwaggerUI(c =>
{
    // run automatically swagger when run project
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Learning Entity Framework API v1");
    c.RoutePrefix = "";
});
app.UseSwagger(x => x.SerializeAsV2 = true);
#endregion Swagger

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
