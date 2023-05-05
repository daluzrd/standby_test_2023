using Domain.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Username)
            .IsRequired();

        builder.Property(prop => prop.Password)
            .IsRequired();

        builder.HasOne(prop => prop.Role)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}