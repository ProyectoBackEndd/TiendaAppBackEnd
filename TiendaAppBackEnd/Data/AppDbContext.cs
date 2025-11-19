using Microsoft.EntityFrameworkCore;
using TiendaApp.Models;

namespace TiendaApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Articulo> Articulos { get; set; }

        // Aquí agregamos el método OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuramos la precisión: 18 dígitos en total, 2 dígitos después del punto decimal.
            // Esto es estándar para manejo de dinero.
            modelBuilder.Entity<Articulo>()
                .Property(a => a.Precio)
                .HasPrecision(18, 2);
        }
    }
}