using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.UserID).IsRequired();
            builder.Property(p => p.DriverID).IsRequired(false);
            builder.Property(p => p.OrderStatus).HasDefaultValue(OrderStatus.pending);
            builder.Property(p => p.ModifiedDate).HasColumnType("date default getdate()");
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
            builder.Property(p => p.ArrivalDate).IsRequired(false);
            builder.Property(p => p.ShippingDate).IsRequired(false);
            builder.Property(p => p.TotalPrice).HasDefaultValue(0);




        }
    }
}
