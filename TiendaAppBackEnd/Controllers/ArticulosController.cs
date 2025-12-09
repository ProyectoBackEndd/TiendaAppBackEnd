using Microsoft.AspNetCore.Mvc;
using TiendaApp.Models;
using TiendaApp.Repositories;

namespace TiendaAppBackEnd.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly IArticuloRepository _repository;

        // Constructor
        public ArticulosController(IArticuloRepository repository)
        {
            _repository = repository;
        }

        // 1. PÁGINA PRINCIPAL (Index)
        public async Task<IActionResult> Index()
        {
            // Usamos el repositorio para buscar los datos en la BD
            var articulos = await _repository.GetAllAsync();
            return View(articulos);
        }

        // 2. CREAR (GET) Muestra la pantalla para crear uno nuevo
        public IActionResult Create()
        {
            return View();
        }

        // 3. CREAR (POST) Recibe los datos del formulario y los manda a la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(articulo);
                // Si sale bien, volvemos a la lista
                return RedirectToAction(nameof(Index));
            }
            // Si hay error, mostramos el formulario de nuevo
            return View(articulo);
        }

       
        // 4. EDITAR (GET) Busca el artículo y muestra el formulario con los datos cargados
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();

            var articulo = await _repository.GetByIdAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }

        // 5. EDITAR (POST) Recibe los cambios y actualiza la BD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Articulo articulo)
        {
            if (id != articulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(articulo);
                }
                catch
                {
                    // Si ocurre un error al guardar (opcional: manejar concurrencia)
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(articulo);
        }

        // 6. ELIMINAR (GET) Muestra la pantalla de confirmación 
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();

            var articulo = await _repository.GetByIdAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }

        // 7. ELIMINAR (POST) Ejecuta el borrado real cuando el usuario confirma
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}