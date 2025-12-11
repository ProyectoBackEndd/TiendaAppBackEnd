using Microsoft.AspNetCore.Mvc;
using TiendaApp.Models;
using TiendaApp.Repositories;
using TiendaApp.DTOs; // ¡Importante!

namespace TiendaAppBackEnd.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly IArticuloRepository _repository;

        public ArticulosController(IArticuloRepository repository)
        {
            _repository = repository;
        }

        // 1. INDEX: Aquí podemos seguir usando la entidad o mapear a una lista de DTOs.
        // Para este TP, está bien dejarlo así o hacer un mapeo rápido si quieres ser purista.
        public async Task<IActionResult> Index()
        {
            var articulos = await _repository.GetAllAsync();
            return View(articulos);
        }

        // 2. CREAR (GET): Enviamos un DTO vacío a la vista
        public IActionResult Create()
        {
            return View(new ArticuloDTO());
        }

        // 3. CREAR (POST): Recibimos un ArticuloDTO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticuloDTO articuloDto)
        {
            if (ModelState.IsValid)
            {
                // MAPEO MANUAL: DTO -> Entidad
                var articulo = new Articulo
                {
                    Descripcion = articuloDto.Descripcion,
                    Precio = articuloDto.Precio,
                    Stock = articuloDto.Stock,
                    // FechaAlta se genera sola en la clase Articulo al hacer 'new'
                };

                await _repository.AddAsync(articulo);
                return RedirectToAction(nameof(Index));
            }
            return View(articuloDto);
        }

        // 4. EDITAR (GET): Buscamos la Entidad y la convertimos a DTO para la vista
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();

            var articulo = await _repository.GetByIdAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }

            // MAPEO MANUAL: Entidad -> DTO
            var articuloDto = new ArticuloDTO
            {
                Id = articulo.Id,
                Descripcion = articulo.Descripcion,
                Precio = articulo.Precio,
                Stock = articulo.Stock,
                FechaAlta = articulo.FechaAlta
            };

            return View(articuloDto);
        }

        // 5. EDITAR (POST): Recibimos DTO, lo convertimos a Entidad y guardamos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArticuloDTO articuloDto)
        {
            if (id != articuloDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // MAPEO MANUAL: DTO -> Entidad
                    // Nota: Aquí creamos una instancia con los datos del DTO
                    var articulo = new Articulo
                    {
                        Id = articuloDto.Id.Value, // Como en el DTO el Id es nullable (int?), usamos .Value
                        Descripcion = articuloDto.Descripcion,
                        Precio = articuloDto.Precio,
                        Stock = articuloDto.Stock,
                        FechaAlta = articuloDto.FechaAlta
                    };

                    await _repository.UpdateAsync(articulo);
                }
                catch
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(articuloDto);
        }

        // ELIMINAR: Puede quedar igual o usar DTO para mostrar la confirmación.
        // Por simplicidad, lo dejaremos igual ya que solo muestra datos.
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();
            var articulo = await _repository.GetByIdAsync(id);
            if (articulo == null) return NotFound();
            return View(articulo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}