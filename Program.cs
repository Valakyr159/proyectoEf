using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));

//SQL Server
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("SQLServerTrustedConnection"));

//PostgreSQL
//builder.Services.AddNpgsql<TareasContext>(builder.Configuration.GetConnectionString("PostgresSQLConnection"));


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => 
{
  dbContext.Database.EnsureCreated();
  return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
}
);

app.Run();
