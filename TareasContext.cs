using Microsoft.EntityFrameworkCore;
using proyectoef.Models;

namespace proyectoef;

public class TareasContext: DbContext
{
  public DbSet<Categoria> Categorias {get;set;}
  public DbSet<Tarea> Tareas {get;set;}

  public TareasContext(DbContextOptions<TareasContext> options) :base(options) {}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    List<Categoria> categoriasInit = new List<Categoria>();

    // seed or initial data for Tarea
    categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("f05ac2fb-4b4e-4b38-9be9-c0c797374f75"), Nombre = "Actividades pendientes", Peso = 20 });
    categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("c50cfa79-9551-48d4-8b52-a15b9830d1b8"), Nombre = "Actividades personales", Peso = 50 });

    modelBuilder.Entity<Categoria>(categoria =>
    {
      categoria.ToTable("Categoria");
      categoria.HasKey(p => p.CategoriaId	);

      categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

      categoria.Property(p => p.Descripcion).IsRequired(false);

      categoria.Property(p => p.Peso);

      // This method will call the seed or initial data
      categoria.HasData(categoriasInit);
    });

    // seed or initial data for Tarea
    List<Tarea> tareasInit = new List<Tarea>();
    tareasInit.Add(new Tarea() {
      TareaId = Guid.Parse("b0a1dbe0-9532-4583-bd7b-e53ae381ac5f"),
      CategoriaId = Guid.Parse("c50cfa79-9551-48d4-8b52-a15b9830d1b8"),
      PrioridadTarea = Prioridad.Baja,
      Titulo = "Terminar de ver peliculas en Netflix",
      FechaCreacion = DateTime.Now,
      });

    tareasInit.Add(new Tarea() {
      TareaId = Guid.Parse("8d5c1a5b-d243-4fa3-8641-655b593cd081"),
      CategoriaId = Guid.Parse("f05ac2fb-4b4e-4b38-9be9-c0c797374f75"),
      PrioridadTarea = Prioridad.Media,
      Titulo = "Pago de Servicios publicos",
      FechaCreacion = DateTime.Now,
      });

    modelBuilder.Entity<Tarea>(tarea =>
    {
      tarea.ToTable("Tarea");
      tarea.HasKey(p => p.TareaId	);

      // Foreign Key
      tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
      
      tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);

      tarea.Property(p => p.Descripcion).IsRequired(false);
      tarea.Property(p => p.PrioridadTarea);
      tarea.Property(p => p.FechaCreacion);

      // Relation was already mapped with the foreign key above
      // tarea.Property(p => p.Categoria);
      
      // .Ignore will serve to avoid certain filds to be written in the database
      tarea.Ignore(p => p.Resumen);

      // This method will call the seed or initial data for Tareas
      tarea.HasData(tareasInit);


    });
  }

}