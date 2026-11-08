using DalilAcademy.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DalilAcademy.Infra.Data.Configuration
{
   public class UserConfiguration:IEntityTypeConfiguration<User>//لو ماكانت موجودة مارح يعطيني errorبس ما رح يطبقهم علة جدول ال users
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(s => s.Email).IsUnique();
            builder.Property(c => c.Email).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(20);




        }
    }
}
