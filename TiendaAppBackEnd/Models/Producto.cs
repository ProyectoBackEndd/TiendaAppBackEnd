public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public bool Activo { get; set; } = true;

    public int CategoriaId { get; set; }
    public int ProveedorId { get; set; }

    public Categoria Categoria { get; set; } = null!;
    public Proveedor Proveedor { get; set; } = null!;
}