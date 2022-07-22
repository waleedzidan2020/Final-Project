using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.ToTable("Collection");
            builder.HasKey(p=>p.ID);
            builder.Property(p => p.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.MarketerID).IsRequired();
            builder.Property(p => p.NameAr).HasDefaultValue(30).IsRequired();
            builder.Property(p => p.NameEN).HasDefaultValue(30).IsRequired();
            builder.Property(p => p.Code).IsRequired();
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.ModifiedDate).IsRequired().HasColumnType("date default getdate()");

        }
    }
}
