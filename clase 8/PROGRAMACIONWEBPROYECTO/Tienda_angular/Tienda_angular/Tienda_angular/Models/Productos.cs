namespace Tienda_angular.Models
{
    public class Productos
    {
        public int Id_Producto { get; set; }
        public string Nombre { get; set; } = default!;
        public decimal Precio_Unitario { get; set; }
        public int Stock { get; set; } = 0;
        public int? Id_Categoria { get; set; }

        public Categoria? Categoria { get; set; }
        public ICollection<Detalleventas>? Detalles { get; set; }
    }
}
