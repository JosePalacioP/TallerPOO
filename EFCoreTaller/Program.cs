// Importamos los espacios de nombres necesarios
using System; // Para usar Console y otras clases básicas de C#
using System.Linq; // Para usar métodos de LINQ como ToList() y First()
using Microsoft.EntityFrameworkCore; // Para usar Entity Framework Core
// Definimos el espacio de nombres de nuestro proyecto
namespace EFCoreTaller
{
    // Definimos la clase principal del programa
    class Program
    {
        // El método Main es el punto de entrada de nuestra aplicación
        static void Main(string[] args){
           Console.WriteLine("¿Qué operación realizará?\n1.Insertar nuevo registro en una tabla\n2.Actualizar un registro de una tabla\n3.Eliminar registro de una tabla");
           var opt = Console.ReadLine();
        }
    {
        // Creamos un nuevo contexto de base de datos dentro de un bloque using
        // Esto asegura que los recursos se liberen correctamente al finalizar
        using (var context = new ProductoContext())
    {
        // Nos aseguramos de que la base de datos esté creada
        context.Database.EnsureCreated();
        // Creamos un nuevo objeto Producto
        var producto = new Producto { codigo = "123", nombre = "PRO1", stock = 10, valorUnitario = 1200 };
        // Añadimos el producto al contexto
        text.Productos.Add(producto);
        // Guardamos los cambios en la base de datos
        context.SaveChanges();
        // Imprimimos un mensaje de confirmación
        Console.WriteLine($"Producto añadido con éxito: {producto.nombre} al precio de {producto.valorUnitario}");
    }
}

}
// Definimos la clase Producto que representa nuestra entidad en la base de datos
public class Producto{
    public string codigo {get; set;}
    public string nombre {get; set;}
    public int stock {get; set;}
    public double valorUnitario {get; set;}
}

// Definimos la clase ProductoContext que hereda de DbContext
// Esta clase es responsable de la conexión y operaciones con la base de datos
public class ProductoContext : DbContext
{
    // Definimos una propiedad DbSet<Producto> que representa nuestra tabla de libros
    public DbSet<Producto> Productos { get; set; }
    // Sobrescribimos el método OnConfiguring para configurar la conexión a la base de datos
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Usamos SQL Server y especificamos la cadena de conexión
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCoreExampleDB;Trusted_Connection=True;");
    }
    // Sobrescribimos el método OnModelCreating para configurar el modelo de la base de datos
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Especificamos que la entidad Producto se mapea a una tabla llamada "producto"
        modelBuilder.Entity<Producto>().ToTable("producto");
    }
}
}