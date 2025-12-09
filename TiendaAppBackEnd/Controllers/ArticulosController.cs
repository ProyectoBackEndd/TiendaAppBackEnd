using Microsoft.AspNetCore.Mvc;
using TiendaApp.Models;       // Donde está tu clase Articulo
using TiendaApp.Repositories; // Donde está tu IArticuloRepository

namespace TiendaAppBackEnd.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly IArticuloRepository _repository;

        // Constructor: Aquí pedimos el Repositorio al sistema (Inyección de Dependencias)
        public ArticulosController(IArticuloRepository repository)
        {
            _repository = repository;
        }

        // 1. PÁGINA PRINCIPAL: Muestra la lista de artículos
        // GET: /Articulos
        public async Task<IActionResult> Index()
        {
            // Usamos el repositorio para buscar los datos en la BD
            var articulos = await _repository.GetAllAsync();
            return View(articulos);
        }

        // 2. FORMULARIO: Muestra la pantalla para crear uno nuevo
        // GET: /Articulos/Create
        public IActionResult Create()
        {
            return View();
        }

        // 3. GUARDAR: Recibe los datos del formulario y los manda a la BD
        // POST: /Articulos/Create
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
            // Si hay error (ej. precio negativo), mostramos el formulario de nuevo
            return View(articulo);
        }
    }
}