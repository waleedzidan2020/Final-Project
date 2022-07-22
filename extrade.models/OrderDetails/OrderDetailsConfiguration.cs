using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class orderdetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(p => new {p.OrderId,p.ProductId} );
            builder.Property(p => p.ID).ValueGeneratedOnAdd();
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.OrderId).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.SubPrice).IsRequired();


        }
    }
}
