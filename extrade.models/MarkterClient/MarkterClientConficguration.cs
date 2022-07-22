//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace extrade.models
//{
//    public class MarkterClientConficguration : IEntityTypeConfiguration<MarkterClient>
//    {
//        public void Configure(EntityTypeBuilder<MarkterClient> builder)
//        {
//            builder.ToTable("MarkterClient");
//            builder.HasKey(p => p.ID);
//            builder.Property(p => p.ID).IsRequired().ValueGeneratedOnAdd();
//            builder.Property(p => p.UserID).IsRequired();
//            builder.Property(p => p.CodeMarketer).IsRequired();
//        }
//    }
//}
