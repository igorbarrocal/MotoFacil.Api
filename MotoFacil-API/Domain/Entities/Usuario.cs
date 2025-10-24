using MotoFacilAPI.Domain.ValueObjects;

namespace MotoFacilAPI.Domain.Entities
{
    /// <summary>
    /// Agregado Raiz: Usuário
    /// </summary>
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;

        // Inicializa com null! para satisfazer o compilador/nullable checks e
        // manter a invariância via construtor público. EF Core usa o ctor privado.
        public Email Email { get; private set; } = null!; // Value Object

        // Navegação: um usuário pode ter várias motos
        public List<Moto> Motos { get; private set; } = new();

        private Usuario() 
        { 
            // ctor para EF Core — Email já inicializado com null! acima
        } // EF Core

        public Usuario(string nome, Email email)
        {
            AlterarNome(nome);
            Email = email ?? throw new ArgumentNullException(nameof(email), "Email obrigatório");
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