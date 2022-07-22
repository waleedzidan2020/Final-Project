using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class driverConfiuration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Driver");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.NameAr).HasMaxLength(40).IsRequired();
            builder.Property(p => p.NameEn).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Country).HasMaxLength(40).IsRequired();
            builder.Property(p => p.City).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Street).HasMaxLength(40).IsRequired();
            builder.Property(p => p.DriverLicense).HasMaxLength(40).IsRequired();
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.DriverStatus).HasDefaultValue(DriverStatus.offline);
            builder.Property(p => p.ModifiedDate).IsRequired().HasColumnType("date default getdate()");



        }
    }
}
