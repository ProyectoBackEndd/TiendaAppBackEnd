using System.ComponentModel.DataAnnotations;

namespace TiendaApp.DTOs
{
    public class ArticuloDTO
    {
        // Usamos int? (nullable int) para permitir que el Id sea nulo
        // cuando estamos creando un nuevo artículo (Create).
        public int? Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        // Agregamos Stock y FechaAlta, pero sin el [Required] si vienen de la BD
        public int Stock { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}
