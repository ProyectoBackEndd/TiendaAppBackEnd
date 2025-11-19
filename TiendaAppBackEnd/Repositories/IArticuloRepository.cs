using TiendaApp.Models;

namespace TiendaApp.Repositories
{
    // IArticuloRepository define el contrato (los métodos) que el Controlador va a usar.
    // Esto cumple con el Principio de Segregación de Interfaces.
    public interface IArticuloRepository
    {
        // Read: Obtener todos
        IEnumerable<Articulo> GetAll();

        // Read: Obtener por ID
        Articulo GetById(int id);

        // Create
        void Add(Articulo articulo);

        // Update
        void Update(Articulo articulo);

        // Delete
        void Delete(int id);

        // Guardar cambios en la BD
        void Save();
    }
}