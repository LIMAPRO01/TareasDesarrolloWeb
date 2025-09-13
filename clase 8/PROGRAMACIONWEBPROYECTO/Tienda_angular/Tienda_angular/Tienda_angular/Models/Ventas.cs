namespace Tienda_angular.Models
{
    public class Ventas
    {
        public int Id_Venta { get; set; }
        public DateTime Fecha { get; set; }
        public int Id_Cliente { get; set; }
        public decimal Total_Bruto { get; set; }
        public decimal Total_Impuestos { get; set; }
        public decimal Total_Neto { get; set; }
        public string Estado { get; set; } = default!;

        public Clientes? Cliente { get; set; }
        public ICollection<Detalleventas>? Detalles { get; set; }
    }
}
