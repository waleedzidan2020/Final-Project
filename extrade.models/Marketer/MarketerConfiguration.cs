using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class MarketerConfiguration : IEntityTypeConfiguration<Marketer>
    {
        public void Configure(EntityTypeBuilder<Marketer> builder)
        {
            builder.ToTable("Marketer");
            builder.HasKey(p => p.UserID);
            builder.Property(p => p.UserID).IsRequired();
            builder.Property(p => p.TaxCard).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Salary).HasDefaultValue(0);
            builder.Property(p => p.MarketerStatus).HasDefaultValue(MarketerStatus.pending);
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}
