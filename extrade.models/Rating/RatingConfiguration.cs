using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class ratingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Rating");
            builder.HasKey(p =>new {p.UserID,p.ProductID });
            builder.Property(p => p.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.UserID).IsRequired();
            builder.Property(p => p.ProductID).IsRequired();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Comment).HasMaxLength(1000);
        }
    }
}
