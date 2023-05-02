using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Codigo)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.QuantidadeEstoque)
                .IsRequired();

            builder.Property(p => p.Valor)
                .IsRequired()
                .HasPrecision(18, 2);
        }
    }
}