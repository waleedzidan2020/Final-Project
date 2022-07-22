using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class VendorImageConfiguration : IEntityTypeConfiguration<VendorImage>
    {
        public void Configure(EntityTypeBuilder<VendorImage> builder)
        {
            builder.ToTable("VendorImage");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.VendorID).IsRequired();
            builder.Property(p => p.Image).HasMaxLength(400).IsRequired();
            
            
        }
    }
}
