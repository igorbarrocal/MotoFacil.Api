using System.ComponentModel.DataAnnotations;

namespace MotoFacilAPI.Application.Dtos
{
    /// <summary>
    /// Dados do serviço realizado em uma moto
    /// </summary>
    public class ServicoDto
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>Troca de óleo</example>
        [Required]
        public string Descricao { get; set; } = string.Empty;

        /// <example>2025-09-25T14:00:00Z</example>
        [Required]
        public DateTime Data { get; set; }

        /// <example>1</example>
        [Required]
        public int UsuarioId { get; set; }

        /// <example>1</example>
        [Required]
        public int MotoId { get; set; }

        public List<LinkDto> Links { get; set; } = new();
    }
}