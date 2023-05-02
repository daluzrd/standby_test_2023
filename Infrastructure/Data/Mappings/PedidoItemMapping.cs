using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Pedido)
                .WithMany(p => p.PedidoItens)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(p => p.Produto)
                .WithMany(p => p.PedidoItems)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(p => p.Quantidade)
                .IsRequired();

            builder.Property(p => p.ValorUnitario)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(p => p.ValorTotal)
                .IsRequired()
                .HasPrecision(18, 2);         
        }
    }

}