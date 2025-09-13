namespace Tienda_angular.Models
{
    public class Clientes
    {
        public int Id_Cliente { get; set; }
        public int Id_Usuario { get; set; }
        public string Nombre { get; set; } = default!;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Departamento { get; set; }
        public string? Municipio { get; set; }
        public string? Referencia { get; set; }

        public Usuario? Usuario { get; set; }
        public ICollection<Venta>? Ventas { get; set; }
    }
}
