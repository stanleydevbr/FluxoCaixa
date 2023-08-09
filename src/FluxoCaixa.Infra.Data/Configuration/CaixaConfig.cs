using FluxoCaixa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxoCaixa.Infra.Data.Configuration
{
    public class CaixaConfig : IEntityTypeConfiguration<Caixa>
    {
        public void Configure(EntityTypeBuilder<Caixa> builder)
        {
            builder.ToTable("caixa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Lancamento)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(x => x.Tipo)
                .IsRequired();

            builder.Property(x => x.Valor)
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .IsRequired();

            builder.Property(x => x.DataAlteracao);
                

        }
    }
}
