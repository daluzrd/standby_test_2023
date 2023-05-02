using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Property(p => p.Data)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.DataAtualizacao)
                .IsRequired();

            builder.Property(p => p.Valor)
                .IsRequired()
                .HasPrecision(18, 2);
        }
    }
}