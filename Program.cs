using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;
using proyectoef.Models;

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

// GET
app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) => 
{
  return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));
});

app.MapGet("/api/tareas/prioridad0", async ([FromServices] TareasContext dbContext) => 
{
  return Results.Ok(dbContext.Tareas.Include(p => p.Categoria).Where( p => p.PrioridadTarea == proyectoef.Models.Prioridad.Baja));
});

// POST
app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) => 
{
  tarea.TareaId = Guid.NewGuid();
  tarea.FechaCreacion = DateTime.Now;
  await dbContext.AddAsync(tarea);
  // Another way to add
  // await dbContext.Tareas.AddAsync(tarea);

  await dbContext.SaveChangesAsync();

  return Results.Ok();
});

// PUT
app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea newTarea, [FromRoute] Guid id) => 
{
  var tareaInDb = dbContext.Tareas.Find(id);
  if(tareaInDb != null)
  {
    tareaInDb.CategoriaId = newTarea.CategoriaId;
    tareaInDb.Titulo = newTarea.Titulo;
    tareaInDb.PrioridadTarea = newTarea.PrioridadTarea;
    tareaInDb.Descripcion = newTarea.Descripcion;

    await dbContext.SaveChangesAsync();

    return Results.Ok();
  }
  return Results.NotFound();
});

// DELETE
app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) => 
{
  var tareaInDb = dbContext.Tareas.Find(id);
  if(tareaInDb != null)
  {
    dbContext.Remove(tareaInDb);
    await dbContext.SaveChangesAsync();

    return Results.Ok();
  }

  return Results.NotFound();
});

app.Run();
