namespace Tienda_angular.Models
{
    public class Categoria
    {
        public int Id_Categoria { get; set; }
        public string Nombre { get; set; } = default!;
        public string? Descripcion { get; set; }
        public int? Id_Categoria_Padre { get; set; }

        public Categoria? CategoriaPadre { get; set; }
        public ICollection<Categoria>? Hijas { get; set; }
        public ICollection<Productos>? Productos { get; set; }
    }
}
