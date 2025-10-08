using Microsoft.EntityFrameworkCore;
using MotoFacilAPI.Domain.Entities;
using MotoFacilAPI.Domain.ValueObjects;

namespace MotoFacilAPI.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Moto> Motos => Set<Moto>();
        public DbSet<Servico> Servicos => Set<Servico>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do Value Object Email no Usuário
            modelBuilder.Entity<Usuario>(b =>
            {
                b.OwnsOne(u => u.Email, eo =>
                {
                    eo.Property(e => e.Value).HasColumnName("Email").IsRequired();
                });

                // Relacionamento: Usuário → Motos (1:N)
                b.HasMany(u => u.Motos)
                 .WithOne()
                 .HasForeignKey(m => m.UsuarioId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.Navigation(u => u.Motos).AutoInclude(false);
            });

            // Relacionamento: Moto → Serviços (1:N)
            modelBuilder.Entity<Moto>(b =>
            {
                b.HasMany(m => m.Servicos)
                 .WithOne()
                 .HasForeignKey(s => s.MotoId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            // Relacionamento: Serviço → Usuário (N:1)
            modelBuilder.Entity<Servico>(b =>
            {
                b.HasOne(s => s.Usuario)
                 .WithMany()
                 .HasForeignKey(s => s.UsuarioId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(s => s.Moto)
                 .WithMany()
                 .HasForeignKey(s => s.MotoId)
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}