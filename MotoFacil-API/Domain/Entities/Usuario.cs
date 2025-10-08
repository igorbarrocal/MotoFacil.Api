using MotoFacilAPI.Domain.ValueObjects;
using MotoFacilAPI.Domain.Entities;

namespace MotoFacilAPI.Domain.Entities
{
    /// <summary>
    /// Agregado Raiz: Usuário
    /// </summary>
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public Email Email { get; private set; } // Value Object

        // Navegação: um usuário pode ter várias motos
        public List<Moto> Motos { get; private set; } = new();

        private Usuario() { } // EF Core

        public Usuario(string nome, Email email)
        {
            AlterarNome(nome);
            Email = email;
        }

        public void AlterarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.", nameof(nome));
            Nome = nome.Trim();
        }

        public void AlterarEmail(Email novoEmail)
        {
            Email = novoEmail ?? throw new ArgumentNullException(nameof(novoEmail), "Email obrigatório");
        }
    }
}