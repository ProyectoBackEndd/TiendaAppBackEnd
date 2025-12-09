using TiendaApp.Models;

namespace TiendaApp.Repositories
{
    public interface IArticuloRepository
    {
        // Fíjate que ahora todos devuelven "Task" (son asincrónicos)
        Task<List<Articulo>> GetAllAsync();
        Task<Articulo?> GetByIdAsync(int id);
        Task AddAsync(Articulo articulo);
        Task UpdateAsync(Articulo articulo);
        Task DeleteAsync(int id);
    }
}