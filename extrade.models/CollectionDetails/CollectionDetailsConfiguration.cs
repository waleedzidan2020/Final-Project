using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class CollectionDetailsConfiguration : IEntityTypeConfiguration<CollectionDetails>
    {
        public void Configure(EntityTypeBuilder<CollectionDetails> builder)
        {
            builder.ToTable("CollectionDetails");
            builder.HasKey(p=>new {p.ProductID , p.CollectionID });
            builder.Property(p => p.ProductID).IsRequired();
            builder.Property(p => p.CollectionID).IsRequired();
            
        }
    }
}
