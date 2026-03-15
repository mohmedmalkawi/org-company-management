using Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Organizations;

internal sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("organizations");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name)
            .IsRequired();

        builder.Property(o => o.Code)
            .IsRequired();

        builder.Property(o => o.Country)
            .IsRequired();

        builder.Property(o => o.Phone)
            .IsRequired();

        builder.Property(o => o.FullAddress)
            .IsRequired();

        builder.Property(o => o.CreationDate)
            .IsRequired();

        builder.HasIndex(o => o.Code)
            .IsUnique();
    }
}
