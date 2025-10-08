using MotoFacilAPI.Domain.Enums;

namespace MotoFacilAPI.Domain.Entities
{
    /// <summary>
    /// Entidade Moto (Agregado Raiz)
    /// </summary>
    public class Moto
    {
        public int Id { get; private set; }
        public string Placa { get; private set; } = string.Empty;
        public ModeloMoto Modelo { get; private set; }
        public int UsuarioId { get; private set; }

        public List<Servico> Servicos { get; private set; } = new();

        private Moto() { }

        public Moto(string placa, ModeloMoto modelo, int usuarioId)
        {
            if (string.IsNullOrWhiteSpace(placa))
                throw new ArgumentException("Placa é obrigatória.", nameof(placa));

            Placa = placa.Trim().ToUpper();
            Modelo = modelo;
            UsuarioId = usuarioId;
        }

        public void AtualizarPlaca(string novaPlaca)
        {
            if (string.IsNullOrWhiteSpace(novaPlaca))
                throw new ArgumentException("Placa é obrigatória.", nameof(novaPlaca));
            Placa = novaPlaca.Trim().ToUpper();
        }

        public void AtualizarModelo(ModeloMoto novoModelo)
        {
            Modelo = novoModelo;
        }
    }
}