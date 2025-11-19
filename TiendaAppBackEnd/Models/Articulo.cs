using System.ComponentModel.DataAnnotations;

namespace TiendaApp.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Stock { get; set; }

        public DateTime FechaAlta { get; set; } = DateTime.Now;
    }
}