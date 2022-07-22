using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class productConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p=>p.ID);
            builder.Property(p => p.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.NameAr).HasMaxLength(40).IsRequired();
            builder.Property(p => p.NameEn).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(800).IsRequired();
            builder.Property(p => p.CategoryID).IsRequired();
            builder.Property(p => p.VendorID).IsRequired();
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.Status).HasDefaultValue(ProductStatus.pending);
            builder.Property(p => p.ModifiedDate).IsRequired().HasColumnType("date default getdate()");




        }
    }
}
