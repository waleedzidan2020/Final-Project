using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace extrade.models
{
    public class UserConfiguration :IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(u => u.NameAr).HasMaxLength(30).IsRequired();
            builder.Property(u => u.Country).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Street).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Img).HasMaxLength(200);
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
        }

       
    }
}
