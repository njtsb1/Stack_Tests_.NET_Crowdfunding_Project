using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Crowdfunding.Domain.Entities;

namespace Crowdfunding.Repository.Mapping
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Anonymous)
                .IsRequired();

            builder.Property(e => e.MessageSupport)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasMany(p => p.Donations)
                .WithOne(d => d.Personaldata)
                .HasForeignKey(d => d.PersonaldataId);

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.ErrorMessages);

            builder.ToTable("Person");
        }
    }
}