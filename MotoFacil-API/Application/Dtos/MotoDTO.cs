using System.ComponentModel.DataAnnotations;
using MotoFacilAPI.Domain.Enums;

namespace MotoFacilAPI.Application.Dtos
{
    public class MotoDto
    {
        public int Id { get; set; }

        [Required]
        public string Placa { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(ModeloMoto), ErrorMessage = "Modelos válidos: MottuSport, MottuE, MottuPop")]
        public ModeloMoto Modelo { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public List<LinkDto> Links { get; set; } = new();
    }
}