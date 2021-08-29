using Alinta.CodingTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alinta.CodingTest.Data
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public CustomerEntityConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable($"{nameof(Customer)}");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.OwnsOne(x => x.DateOfBirth, y =>
            {
                y.Property(z => z.Day).HasColumnName(nameof(DateOfBirth.Day));
                y.Property(z => z.Month).HasColumnName(nameof(DateOfBirth.Month));
                y.Property(z => z.Year).HasColumnName(nameof(DateOfBirth.Year));
            });
        }
    }
}
