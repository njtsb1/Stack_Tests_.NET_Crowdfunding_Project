using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Repository.Mapping
{
    public class DonationMapping : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Value)
                .IsRequired()
                .HasColumnType("decimal(9,2)");

            builder.Property(e => e.DateTime)
                .IsRequired();

            builder.HasOne(d => d.Personaldata)
                .WithMany(p => p.Donations)
                .HasForeignKey(d => d.PersonaldataId);

            builder.HasOne(d => d.AddressBilling)
                .WithMany(e => e.Donations)
                .HasForeignKey(d => d.AddressBillingId);

            // does not save card data in the database
            builder.Ignore(e => e.PaymentMethod);
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.ErrorMessages);

            builder.ToTable("Donation");
        }
    }
}