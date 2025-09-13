namespace Tienda_angular.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string UsuarioName { get; set; } = default!; // 'USUARIO' column
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!; // Store hash in real impl
        public string Rol { get; set; } = default!;
        public string Estado { get; set; } = "activo";
        public string? Reset_Token { get; set; }
        public DateTime? Reset_Token_Expira { get; set; }

        public ICollection<Cliente>? Clientes { get; set; }
    }
}
