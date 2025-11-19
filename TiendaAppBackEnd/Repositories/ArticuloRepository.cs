using TiendaApp.Data;
using TiendaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TiendaApp.Repositories
{
    // ArticuloRepository implementa el contrato IArticuloRepository
    public class ArticuloRepository : IArticuloRepository
    {
        // Campo privado para guardar el contexto de la BD
        private readonly AppDbContext _context;

        // Constructor: Recibimos el contexto por Inyección de Dependencias
        public ArticuloRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Articulo articulo)
        {
            _context.Articulos.Add(articulo);
        }

        public void Delete(int id)
        {
            var articulo = _context.Articulos.Find(id);
            if (articulo != null)
            {
                _context.Articulos.Remove(articulo);
            }
        }

        public IEnumerable<Articulo> GetAll()
        {
            return _context.Articulos.ToList();
        }

        public Articulo GetById(int id)
        {
            return _context.Articulos.Find(id);
        }

        public void Update(Articulo articulo)
        {
            // Le indicamos a EF Core que este objeto está siendo modificado
            _context.Entry(articulo).State = EntityState.Modified;
        }

        public void Save()
        {
            // Este método ejecuta la transacción y guarda todos los cambios pendientes
            _context.SaveChanges();
        }
    }
}