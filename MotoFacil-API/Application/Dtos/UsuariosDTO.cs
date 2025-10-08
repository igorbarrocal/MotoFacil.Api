using System.ComponentModel.DataAnnotations;

namespace MotoFacilAPI.Application.Dtos
{
    /// <summary>
    /// Dados do usuário
    /// </summary>
    public class UsuarioDto
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>João Silva</example>
        [Required]
        public string Nome { get; set; } = string.Empty;

        /// <example>joao@email.com</example>
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public List<LinkDto> Links { get; set; } = new();
    }
}