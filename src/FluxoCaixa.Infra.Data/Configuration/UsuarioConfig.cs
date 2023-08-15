using FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Infra.Data.Configuration
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(60);
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Senha)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.ConfirmarSenha)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.DataCriacao)
                .IsRequired();

            builder.Property(x => x.DataAlteracao);
        }
    }
}
