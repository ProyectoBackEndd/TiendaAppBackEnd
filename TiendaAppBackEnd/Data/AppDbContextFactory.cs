using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TiendaApp.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // 1. Aquí se define la cadena de conexión (Connection String).
        // NOTA: Para este ejemplo, la copiamos directamente de appsettings.json
        // En una aplicación real, se usaría IConfiguration para leer el archivo.
        var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TiendaDB2025;Trusted_Connection=True;MultipleActiveResultSets=true";

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        // 2. Retornamos una nueva instancia del contexto con la conexión definida.
        return new AppDbContext(optionsBuilder.Options);
    }
}
