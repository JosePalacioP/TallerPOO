using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace EFCoreTaller
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- Menú Principal ---");
                Console.WriteLine("Elige una de las tablas disponibles en la base de datos:");
                Console.WriteLine("1. Producto");
                Console.WriteLine("2. Persona");
                Console.WriteLine("3. Cliente");
                Console.WriteLine("4. Empresa");
                Console.WriteLine("5. Factura");
                Console.WriteLine("6. ProductosPorFactura");
                Console.WriteLine("7. Vendedor");
                Console.WriteLine("8. Salir");
                Console.Write("Opción: ");
                var tablaOpt = Console.ReadLine();

                if (tablaOpt == "8")
                {
                    Console.WriteLine("Saliendo del programa...");
                    break;
                }

                Console.WriteLine("\n¿Qué operación realizará?");
                Console.WriteLine("1. Insertar nuevo registro");
                Console.WriteLine("2. Actualizar un registro");
                Console.WriteLine("3. Eliminar un registro");
                Console.WriteLine("4. Listar registros");
                Console.Write("Opción: ");
                var operOpt = Console.ReadLine();

                try
                {
                    switch (operOpt)
                    {
                        case "1":
                            InsertarRegistroMenu(tablaOpt);
                            break;
                        case "2":
                            ActualizarRegistroMenu(tablaOpt);
                            break;
                        case "3":
                            EliminarRegistroMenu(tablaOpt);
                            break;
                        case "4":
                            ListarRegistrosMenu(tablaOpt);
                            break;
                        default:
                            Console.WriteLine("Opción no válida");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        #region Menús de Operaciones

        static void InsertarRegistroMenu(string tablaOpt)
        {
            switch (tablaOpt)
            {
                case "1":
                    var producto = new Producto();
                    Console.Write("Ingrese el código: ");
                    producto.codigo = Console.ReadLine();
                    Console.Write("Ingrese el nombre: ");
                    producto.nombre = Console.ReadLine();
                    producto.stock = LeerEntero("Ingrese el stock: ");
                    producto.valorUnitario = LeerDouble("Ingrese el valor unitario: ");
                    InsertarRegistro(producto);
                    break;

                case "2":
                    var persona = new Persona();
                    Console.Write("Ingrese el código: ");
                    persona.codigo = Console.ReadLine();
                    Console.Write("Ingrese el email: ");
                    persona.email = Console.ReadLine();
                    Console.Write("Ingrese el nombre: ");
                    persona.nombre = Console.ReadLine();
                    Console.Write("Ingrese el teléfono: ");
                    persona.telefono = Console.ReadLine();
                    InsertarRegistro(persona);
                    break;

                case "3":
                    var cliente = new Cliente();
                    Console.Write("Ingrese el código: ");
                    cliente.codigo = Console.ReadLine();
                    Console.Write("Ingrese el email: ");
                    cliente.email = Console.ReadLine();
                    Console.Write("Ingrese el nombre: ");
                    cliente.nombre = Console.ReadLine();
                    Console.Write("Ingrese el teléfono: ");
                    cliente.telefono = Console.ReadLine();
                    cliente.credito = LeerDouble("Ingrese el crédito: ");
                    InsertarRegistro(cliente);
                    break;

                case "4":
                    var empresa = new Empresa();
                    Console.Write("Ingrese el código: ");
                    empresa.codigo = Console.ReadLine();
                    Console.Write("Ingrese el nombre: ");
                    empresa.nombre = Console.ReadLine();
                    Console.WriteLine("Ingrese los detalles del usuario (Cliente):");
                    var usuario = new Cliente();
                    Console.Write("  Código del usuario: ");
                    usuario.codigo = Console.ReadLine();
                    Console.Write("  Email del usuario: ");
                    usuario.email = Console.ReadLine();
                    Console.Write("  Nombre del usuario: ");
                    usuario.nombre = Console.ReadLine();
                    Console.Write("  Teléfono del usuario: ");
                    usuario.telefono = Console.ReadLine();
                    usuario.credito = LeerDouble("  Crédito del usuario: ");
                    empresa.usuario = usuario;
                    InsertarRegistro(empresa);
                    break;

                case "5":
                    var factura = new Factura();
                    factura.fecha = LeerFecha("Ingrese la fecha (yyyy-MM-dd): ");
                    factura.numero = LeerLong("Ingrese el número de factura: ");
                    factura.total = LeerDouble("Ingrese el total: ");
                    Console.WriteLine("Ingrese los detalles del producto por factura:");
                    var productoXFactura = new ProductosPorFactura();
                    productoXFactura.cantidad = LeerEntero("  Cantidad: ");
                    productoXFactura.subtotal = LeerDouble("  Subtotal: ");
                    factura.productoXFactura = productoXFactura;
                    InsertarRegistro(factura);
                    break;

                case "6":
                    var productosPorFactura = new ProductosPorFactura();
                    productosPorFactura.cantidad = LeerEntero("Ingrese la cantidad: ");
                    productosPorFactura.subtotal = LeerDouble("Ingrese el subtotal: ");
                    InsertarRegistro(productosPorFactura);
                    break;

                case "7":
                    var vendedor = new Vendedor();
                    Console.Write("Ingrese el código: ");
                    vendedor.codigo = Console.ReadLine();
                    Console.Write("Ingrese el email: ");
                    vendedor.email = Console.ReadLine();
                    Console.Write("Ingrese el nombre: ");
                    vendedor.nombre = Console.ReadLine();
                    Console.Write("Ingrese el teléfono: ");
                    vendedor.telefono = Console.ReadLine();
                    vendedor.carne = LeerEntero("Ingrese el carné: ");
                    Console.Write("Ingrese la dirección: ");
                    vendedor.direccion = Console.ReadLine();
                    InsertarRegistro(vendedor);
                    break;

                default:
                    Console.WriteLine("Opción de tabla no válida para inserción");
                    break;
            }
        }

        static void ActualizarRegistroMenu(string tablaOpt)
        {
            Console.WriteLine("Ingrese el valor de la clave primaria del registro a actualizar:");
            string clave = Console.ReadLine();

            Console.WriteLine("¿Qué campo desea actualizar?");
            string campo = Console.ReadLine();

            Console.WriteLine("Ingrese el nuevo valor:");
            string nuevoValor = Console.ReadLine();

            switch (tablaOpt)
            {
                case "1":
                    ActualizarRegistro<Producto>(clave, campo, nuevoValor);
                    break;
                case "2":
                    ActualizarRegistro<Persona>(clave, campo, nuevoValor);
                    break;
                case "3":
                    ActualizarRegistro<Cliente>(clave, campo, nuevoValor);
                    break;
                case "4":
                    ActualizarRegistro<Empresa>(clave, campo, nuevoValor);
                    break;
                case "5":
                    ActualizarRegistro<Factura>(clave, campo, nuevoValor);
                    break;
                case "6":
                    ActualizarRegistro<ProductosPorFactura>(clave, campo, nuevoValor);
                    break;
                case "7":
                    ActualizarRegistro<Vendedor>(clave, campo, nuevoValor);
                    break;
                default:
                    Console.WriteLine("Opción de tabla no válida para actualización");
                    break;
            }
        }

        static void EliminarRegistroMenu(string tablaOpt)
        {
            Console.WriteLine("Ingrese el valor de la clave primaria del registro a eliminar:");
            string clave = Console.ReadLine();

            switch (tablaOpt)
            {
                case "1":
                    EliminarRegistro<Producto>(clave);
                    break;
                case "2":
                    EliminarRegistro<Persona>(clave);
                    break;
                case "3":
                    EliminarRegistro<Cliente>(clave);
                    break;
                case "4":
                    EliminarRegistro<Empresa>(clave);
                    break;
                case "5":
                    EliminarRegistro<Factura>(clave);
                    break;
                case "6":
                    EliminarRegistro<ProductosPorFactura>(clave);
                    break;
                case "7":
                    EliminarRegistro<Vendedor>(clave);
                    break;
                default:
                    Console.WriteLine("Opción de tabla no válida para eliminación");
                    break;
            }
        }

        static void ListarRegistrosMenu(string tablaOpt)
        {
            switch (tablaOpt)
            {
                case "1":
                    ListarRegistros<Producto>();
                    break;
                case "2":
                    ListarRegistros<Persona>();
                    break;
                case "3":
                    ListarRegistros<Cliente>();
                    break;
                case "4":
                    ListarRegistros<Empresa>();
                    break;
                case "5":
                    ListarRegistros<Factura>();
                    break;
                case "6":
                    ListarRegistros<ProductosPorFactura>();
                    break;
                case "7":
                    ListarRegistros<Vendedor>();
                    break;
                default:
                    Console.WriteLine("Opción de tabla no válida para listar");
                    break;
            }
        }

        #endregion

        #region Métodos CRUD Genéricos

        // Método genérico para insertar registros
        static void InsertarRegistro<T>(T entidad) where T : class
        {
            using (var context = new TallerContext())
            {
                context.Set<T>().Add(entidad);
                context.SaveChanges();
                Console.WriteLine($"{typeof(T).Name} añadido con éxito");
            }
        }

        // Método genérico para actualizar registros
        static void ActualizarRegistro<T>(string clave, string campo, string nuevoValor) where T : class
        {
            using (var context = new TallerContext())
            {
                // Obtener la propiedad clave
                var keyProp = typeof(T).GetProperties().FirstOrDefault(p => p.Name.Equals("codigo", StringComparison.OrdinalIgnoreCase) ||
                                                                           p.Name.Equals("numero", StringComparison.OrdinalIgnoreCase) ||
                                                                           p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
                if (keyProp == null)
                {
                    Console.WriteLine("No se encontró una propiedad clave primaria.");
                    return;
                }

                // Convertir la clave al tipo correcto
                object claveConvertida;
                try
                {
                    claveConvertida = Convert.ChangeType(clave, keyProp.PropertyType);
                }
                catch
                {
                    Console.WriteLine("Clave inválida para el tipo de dato de la clave primaria.");
                    return;
                }

                var entidad = context.Set<T>().Find(claveConvertida);

                if (entidad != null)
                {
                    var propiedad = typeof(T).GetProperty(campo, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (propiedad == null)
                    {
                        Console.WriteLine($"La propiedad '{campo}' no existe en la entidad {typeof(T).Name}.");
                        return;
                    }

                    try
                    {
                        object valorConvertido = Convert.ChangeType(nuevoValor, propiedad.PropertyType);
                        propiedad.SetValue(entidad, valorConvertido);
                        context.SaveChanges();
                        Console.WriteLine($"{typeof(T).Name} actualizado con éxito");
                    }
                    catch
                    {
                        Console.WriteLine("Error al convertir el valor al tipo de dato correcto.");
                    }
                }
                else
                {
                    Console.WriteLine($"{typeof(T).Name} no encontrado");
                }
            }
        }

        // Método genérico para eliminar registros
        static void EliminarRegistro<T>(string clave) where T : class
        {
            using (var context = new TallerContext())
            {
                // Obtener la propiedad clave
                var keyProp = typeof(T).GetProperties().FirstOrDefault(p => p.Name.Equals("codigo", StringComparison.OrdinalIgnoreCase) ||
                                                                           p.Name.Equals("numero", StringComparison.OrdinalIgnoreCase) ||
                                                                           p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
                if (keyProp == null)
                {
                    Console.WriteLine("No se encontró una propiedad clave primaria.");
                    return;
                }

                // Convertir la clave al tipo correcto
                object claveConvertida;
                try
                {
                    claveConvertida = Convert.ChangeType(clave, keyProp.PropertyType);
                }
                catch
                {
                    Console.WriteLine("Clave inválida para el tipo de dato de la clave primaria.");
                    return;
                }

                var entidad = context.Set<T>().Find(claveConvertida);
                if (entidad != null)
                {
                    context.Set<T>().Remove(entidad);
                    context.SaveChanges();
                    Console.WriteLine($"{typeof(T).Name} eliminado con éxito");
                }
                else
                {
                    Console.WriteLine($"{typeof(T).Name} no encontrado");
                }
            }
        }

        // Método genérico para listar registros
        static void ListarRegistros<T>() where T : class
        {
            using (var context = new TallerContext())
            {
                var registros = context.Set<T>().ToList();
                if (registros.Count == 0)
                {
                    Console.WriteLine($"No hay registros en la tabla {typeof(T).Name}");
                    return;
                }

                Console.WriteLine($"\n--- Lista de {typeof(T).Name} ---");
                foreach (var registro in registros)
                {
                    Console.WriteLine(RegistroToString(registro));
                }
            }
        }

        // Método para convertir una entidad a string utilizando reflexión
        static string RegistroToString<T>(T entidad)
        {
            var props = typeof(T).GetProperties();
            List<string> propValores = new List<string>();
            foreach (var prop in props)
            {
                var valor = prop.GetValue(entidad, null);
                propValores.Add($"{prop.Name}: {valor}");
            }
            return string.Join(", ", propValores);
        }

        #endregion

        #region Métodos Auxiliares

        static int LeerEntero(string mensaje)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out valor))
                    return valor;
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero.");
            }
        }

        static long LeerLong(string mensaje)
        {
            long valor;
            while (true)
            {
                Console.Write(mensaje);
                if (long.TryParse(Console.ReadLine(), out valor))
                    return valor;
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
            }
        }

        static double LeerDouble(string mensaje)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                if (double.TryParse(Console.ReadLine(), out valor))
                    return valor;
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
            }
        }

        static DateTime LeerFecha(string mensaje)
        {
            DateTime valor;
            while (true)
            {
                Console.Write(mensaje);
                if (DateTime.TryParse(Console.ReadLine(), out valor))
                    return valor;
                Console.WriteLine("Entrada inválida. Por favor, ingrese una fecha en formato yyyy-MM-dd.");
            }
        }

        #endregion
    }

    #region Entidades

    public class Persona
    {
        public int Id { get; set; } // Clave primaria
        public string codigo { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
    }

    public class Cliente : Persona
    {
        public double credito { get; set; }
    }

    public class Empresa
    {
        public int Id { get; set; } // Clave primaria
        public string codigo { get; set; }
        public string nombre { get; set; }
        public Cliente usuario { get; set; }
    }

    public class Factura
    {
        public long numero { get; set; } // Clave primaria
        public DateTime fecha { get; set; }
        public double total { get; set; }
        public ProductosPorFactura productoXFactura { get; set; }
    }

    public class Producto
    {
        public int Id { get; set; } // Clave primaria
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int stock { get; set; }
        public double valorUnitario { get; set; }
    }

    public class ProductosPorFactura
    {
        public int Id { get; set; } // Clave primaria
        public int cantidad { get; set; }
        public double subtotal { get; set; }
    }

    public class Vendedor : Persona
    {
        public int carne { get; set; }
        public string direccion { get; set; }
    }

    #endregion

    #region Contexto de Base de Datos

    public class TallerContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<ProductosPorFactura> ProductosPorFacturas { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Asegúrate de que la cadena de conexión sea correcta
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCoreExampleDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar tablas y claves primarias si es necesario
            modelBuilder.Entity<Producto>().ToTable("producto");
            modelBuilder.Entity<Persona>().ToTable("persona");
            modelBuilder.Entity<Cliente>().ToTable("cliente");
            modelBuilder.Entity<Empresa>().ToTable("empresa");
            modelBuilder.Entity<Factura>().ToTable("factura");
            modelBuilder.Entity<ProductosPorFactura>().ToTable("productosPorFactura");
            modelBuilder.Entity<Vendedor>().ToTable("vendedor");

            // Configurar relaciones
            modelBuilder.Entity<Empresa>()
                .HasOne(e => e.usuario)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.productoXFactura)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    #endregion
}