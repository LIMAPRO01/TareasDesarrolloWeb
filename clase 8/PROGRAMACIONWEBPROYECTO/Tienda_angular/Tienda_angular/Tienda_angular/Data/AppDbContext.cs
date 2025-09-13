

using CiberZone.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CiberZone.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<DetalleVenta> Detalle_Ventas => Set<DetalleVenta>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        // USUARIOS
        mb.Entity<Usuario>(e =>
        {
            e.HasKey(x => x.Id_Usuario);
            e.Property(x => x.UsuarioName).IsRequired().HasMaxLength(50);
            e.Property(x => x.Email).IsRequired().HasMaxLength(150);
            e.Property(x => x.Password).IsRequired().HasMaxLength(255);
            e.Property(x => x.Rol).IsRequired().HasMaxLength(20);
            e.Property(x => x.Estado).IsRequired().HasMaxLength(20);
            e.HasIndex(x => x.UsuarioName).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
        });

        // CLIENTES
        mb.Entity<Cliente>(e =>
        {
            e.HasKey(x => x.Id_Cliente);
            e.Property(x => x.Nombre).IsRequired().HasMaxLength(150);
            e.HasOne(x => x.Usuario)
                .WithMany(u => u.Clientes!)
                .HasForeignKey(x => x.Id_Usuario)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // CATEGORIAS con autorrelación
        mb.Entity<Categoria>(e =>
        {
            e.HasKey(x => x.Id_Categoria);
            e.Property(x => x.Nombre).IsRequired().HasMaxLength(100);
            e.HasOne(x => x.CategoriaPadre)
                .WithMany(p => p.Hijas!)
                .HasForeignKey(x => x.Id_Categoria_Padre)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // PRODUCTOS
        mb.Entity<Producto>(e =>
        {
            e.HasKey(x => x.Id_Producto);
            e.Property(x => x.Nombre).IsRequired().HasMaxLength(120);
            e.Property(x => x.Precio_Unitario).HasColumnType("decimal(10,2)");
            e.HasOne(x => x.Categoria)
                .WithMany(c => c.Productos!)
                .HasForeignKey(x => x.Id_Categoria)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // VENTAS
        mb.Entity<Venta>(e =>
        {
            e.HasKey(x => x.Id_Venta);
            e.Property(x => x.Total_Bruto).HasColumnType("decimal(10,2)");
            e.Property(x => x.Total_Impuestos).HasColumnType("decimal(10,2)");
            e.Property(x => x.Total_Neto).HasColumnType("decimal(10,2)");
            e.HasOne(x => x.Cliente)
                .WithMany(c => c.Ventas!)
                .HasForeignKey(x => x.Id_Cliente)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // DETALLE_VENTAS
        mb.Entity<DetalleVenta>(e =>
        {
            e.HasKey(x => x.Id_Detalle);
            e.Property(x => x.Cantidad).HasColumnType("decimal(10,2)");
            e.Property(x => x.Precio_Unitario).HasColumnType("decimal(10,2)");
            e.Property(x => x.Subtotal).HasColumnType("decimal(10,2)");
            e.HasOne(x => x.Venta)
                .WithMany(v => v.Detalles!)
                .HasForeignKey(x => x.Id_Venta)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Producto)
                .WithMany(p => p.Detalles!)
                .HasForeignKey(x => x.Id_Producto)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
