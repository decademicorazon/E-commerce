
using ExperimentoAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace ExperimentoAPI.Data

{
    public class InventoryDbContext : DbContext
    {

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
        public DbSet<Producto> productos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Carrito> carritos { get; set; }
        public DbSet<Producto2> productos2 { get; set; }
       
        public DbSet<CarritoDetalle> CarritoDetalles { get; set; }
        public DbSet<Categoria> categorias { get; set; }

        public DbSet<Venta> ventas { get; set; }
        public DbSet<DetalleVenta> detallesVenta { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().ToTable("productos");


            modelBuilder.Entity<Carrito>()
                .HasOne(ca => ca.Usuario)
                .WithMany(co => co.Carritos)
                .HasForeignKey(ca => ca.UsuarioId);

            modelBuilder.Entity<CarritoDetalle>()
                .HasOne(cd => cd.Carrito)
                .WithMany(ca => ca.Detalles)
                .HasForeignKey(cd => cd.CarritoId);

            modelBuilder.Entity<CarritoDetalle>()
                .HasOne(cd => cd.Producto)
                .WithMany()
                .HasForeignKey(cd => cd.ProductoId);

            modelBuilder.Entity<DetalleVenta>().
                HasOne<Venta>()
                .WithMany(v => v.detallesVenta)
                .HasForeignKey(d => d.ventaId);

            modelBuilder.Entity<Producto2>()
                .HasOne(p=>p.categoria)
                .WithMany(c=>c.productos)
                .HasForeignKey(p=>p.idCategoria);




            
        }


        public DbSet<ExperimentoAPI.Models.Consumidor> Consumidor { get; set; } = default!;
    }
}
