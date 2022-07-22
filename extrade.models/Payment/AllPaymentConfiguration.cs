using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace extrade.models
{
    public class AllPaymentConfiguration : IEntityTypeConfiguration<AllPayment>
    {
        public void Configure(EntityTypeBuilder<AllPayment> builder)
        {
            builder.ToTable("AllPayment");
            builder.HasKey("ID");
            builder.Property(p => p.ID).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p=>p.Balance).HasDefaultValue(0);
        }
    }
}
