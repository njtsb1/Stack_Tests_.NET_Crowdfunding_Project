using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Repository.Mapping
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.ZipCode)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.TextoAddress)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Complement)
                .IsRequired(false)
                .HasMaxLength(250);

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(e => e.Telephone)
                .IsRequired()
                .HasMaxLength(15);

            builder.HasMany(e => e.Donations)
                .WithOne(d => d.AddressBilling)
                .HasForeignKey(d => d.AddressBillingId);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.ErrorMessages);

            builder.ToTable("Address");
        }
    }
}