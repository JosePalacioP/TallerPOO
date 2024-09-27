using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EFCoreTaller
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Elige una de las tablas disponibles en la base de datos:\n1.Producto\n2.Persona\n3.Cliente\n4.Empresa\n5.Factura\n6.ProductosPorFactura\n7.Vendedor");
            var opt = Console.ReadLine();
            Console.WriteLine("¿Qué operación realizará?\n1. Insertar nuevo registro\n2. Actualizar un registro\n3. Eliminar un registro\n4. Listar registros");
            var opt2 = Console.ReadLine();
            
            switch(opt2)
            {
                case "1":
                    if(opt == "1"){
                        InsertarRegistro<Producto>(new Producto { codigo = Console.ReadLine(), nombre = Console.ReadLine(), stock = Convert.ToInt32(Console.ReadLine()), valorUnitario = Convert.ToDouble(Console.ReadLine()) });
                    } else if(opt == "2"){
                        InsertarRegistro<Persona>(new Persona { codigo = Console.ReadLine(), email = Console.ReadLine(), nombre = Console.ReadLine(), telefono = Console.ReadLine() });
                    } else if(opt == "3"){
                        InsertarRegistro<Cliente>(new Cliente { credito = Convert.ToDouble(Console.ReadLine()) });
                    } else if(opt == "4"){
                        InsertarRegistro<Empresa>(new Empresa { codigo = Console.ReadLine(), nombre = Console.ReadLine(), usuario = new Cliente{ credito = Convert.ToDouble(Console.ReadLine())} });
                    } else if(opt == "5"){
                        InsertarRegistro<Factura>(new Factura { fecha = Console.ReadLine(), numero = Convert.ToInt32(Console.ReadLine()), total = Convert.ToDouble(Console.ReadLine()), productoXFactura = new ProductosPorFactura{ cantidad = Convert.ToInt32(Console.ReadLine()), subtotal = Convert.ToDouble(Console.ReadLine())} });
                    } else if(opt == "6"){
                        InsertarRegistro<ProductosPorFactura>(new ProductosPorFactura { cantidad = Convert.ToInt32(Console.ReadLine()), subtotal = Convert.ToDouble(Console.ReadLine()) });
                    } else if(opt == "7"){
                        InsertarRegistro<Vendedor>(new Vendedor { carne = Convert.ToInt32(Console.ReadLine()), direccion = Console.ReadLine() });
                    }
                    break;
                case "2":
                    Console.WriteLine("Reemplazar por: ");
                    var reem = Console.ReadLine();
                    Console.WriteLine("En el campo: ");
                    var camp = Console.ReadLine();
                    Console.WriteLine("Que tenga valor igual a: ");
                    var vigual = Console.ReadLine();
                    if(opt == "1"){
                        ActualizarRegistro<Producto>(reem, p => p.{camp} = vigual);
                    } else if(opt == "2"){
                        ActualizarRegistro<Persona>(reem, p => p.{camp} = vigual);
                    } else if(opt == "3"){
                        ActualizarRegistro<Cliente>(reem, p => p.{camp} = vigual);
                    } else if(opt == "4"){
                        ActualizarRegistro<Empresa>(reem, p => p.{camp} = vigual);
                    } else if(opt == "5"){
                        ActualizarRegistro<Factura>(reem, p => p.{camp} = vigual);
                    } else if(opt == "6"){
                        ActualizarRegistro<ProductosPorFactura>(reem, p => p.{camp} = vigual);
                    } else if(opt == "7"){
                        ActualizarRegistro<Vendedor>(reem, p => p.{camp} = vigual);
                    }
                    break;
                case "3":
                    Console.WriteLine("Específicamente lo que desea eliminar: ");
                    var dele = Console.ReadLine();
                    if(opt == "1"){
                        EliminarRegistro<Producto>(dele);
                    } else if(opt == "2"){
                        EliminarRegistro<Persona>(dele);
                    } else if(opt == "3"){
                        EliminarRegistro<Cliente>(dele);
                    } else if(opt == "4"){
                        EliminarRegistro<Empresa>(dele);
                    } else if(opt == "5"){
                        EliminarRegistro<Factura>(dele);
                    } else if(opt == "6"){
                        EliminarRegistro<ProductosPorFactura>(dele);
                    } else if(opt == "7"){
                        EliminarRegistro<Vendedor>(dele);
                    }
                    break;
                case "4":
                    if(opt == "1"){
                        ListarRegistros<Producto>();
                    } else if(opt == "2"){
                        ListarRegistros<Persona>();
                    } else if(opt == "3"){
                        ListarRegistros<Cliente>();
                    } else if(opt == "4"){
                        ListarRegistros<Empresa>();
                    } else if(opt == "5"){
                        ListarRegistros<Factura>();
                    } else if(opt == "6"){
                        ListarRegistros<ProductosPorFactura>();
                    } else if(opt == "7"){
                        ListarRegistros<Vendedor>();
                    }
                    break;
                default:
                    Console.WriteLine("Opción no válida");
                    break;
            }
        }

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
        static void ActualizarRegistro<T>(string codigo, Action<T> actualizar) where T : class
        {
            using (var context = new TallerContext())
            {
                var entidad = context.Set<T>().Find(codigo);

                if (entidad != null)
                {
                    actualizar(entidad);
                    context.SaveChanges();
                    Console.WriteLine($"{typeof(T).Name} actualizado con éxito");
                }
                else
                {
                    Console.WriteLine($"{typeof(T).Name} no encontrado");
                }
            }
        }

        // Método genérico para eliminar registros
        static void EliminarRegistro<T>(string codigo) where T : class
        {
            using (var context = new TallerContext())
            {
                var entidad = context.Set<T>().Find(codigo);
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
                foreach (var registro in registros)
                {
                    Console.WriteLine(registro);
                }
            }
        }
    }

    // Las entidades (Producto, Persona, Cliente, etc.)
    public class Persona
    {
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
        public string codigo { get; set; }
        public string nombre { get; set; }
        public Cliente usuario { get; set; }
    }

    public class Factura
    {
        public DateTime fecha { get; set; }
        public long numero { get; set; }
        public double total { get; set; }
        public ProductosPorFactura productoXFactura { get; set; }
    }

    public class Producto
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int stock { get; set; }
        public double valorUnitario { get; set; }
    }

    public class ProductosPorFactura
    {
        public int cantidad { get; set; }
        public double subtotal { get; set; }
    }

    public class Vendedor : Persona
    {
        public int carne { get; set; }
        public string direccion { get; set; }
    }

    // Contexto de base de datos
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
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCoreExampleDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().ToTable("producto"); //Tablas
            modelBuilder.Entity<Persona>().ToTable("persona"); //Tabla Prsona
            modelBuilder.Entity<Cliente>().ToTable("cliente");
            modelBuilder.Entity<Empresa>().ToTable("empresa");
            modelBuilder.Entity<Factura>().ToTable("factura");
            modelBuilder.Entity<ProductosPorFactura>().ToTable("productosPorFactura");
            modelBuilder.Entity<Vendedor>().ToTable("vendedor");
        }
    }
}