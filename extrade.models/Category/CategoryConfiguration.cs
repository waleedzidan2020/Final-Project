using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class categoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(p=>p.ID);
            builder.Property(p => p.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.NameAr).IsRequired().HasMaxLength(30);
            builder.Property(p => p.NameEn).IsRequired().HasMaxLength(30);
            builder.Property(p => p.Image).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired().HasColumnType("date default getdate()");


        }
    }
}
