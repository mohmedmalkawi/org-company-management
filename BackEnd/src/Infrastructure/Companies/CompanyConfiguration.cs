using Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Companies;

internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired();

        builder.Property(c => c.Code)
            .IsRequired();

        builder.Property(c => c.Country)
            .IsRequired();

        builder.Property(c => c.Phone)
            .IsRequired();

        builder.Property(c => c.FullAddress)
            .IsRequired();

        builder.Property(c => c.CreationDate)
            .IsRequired();

        builder.HasOne<Domain.Organizations.Organization>()
            .WithMany()
            .HasForeignKey(c => c.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.Code);
    }
}
