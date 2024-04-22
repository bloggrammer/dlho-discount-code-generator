using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DLHO.DiscountCodeGenerator.Common.Models;

namespace DLHO.DiscountCodeGenerator.DataAccess.Configurations;
internal class DiscountCodeConfiguration : IEntityTypeConfiguration<DiscountCode>
{
    public void Configure(EntityTypeBuilder<DiscountCode> entity)
    {
        entity.HasKey(e => e.Code);

        entity.Property(e => e.Code)
              .IsRequired()
              .HasMaxLength(8); 

        entity.HasIndex(e => e.Code)
              .IsUnique();

    }
}