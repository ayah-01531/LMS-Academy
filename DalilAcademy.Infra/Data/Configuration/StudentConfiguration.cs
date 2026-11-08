using DalilAcademy.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Infra.Data.Configuration
{
  public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(c=>c.Email).IsRequired().HasMaxLength(50);
            builder.HasIndex(s => s.Email).IsUnique();
            builder.Property(c => c.FullName).IsRequired().HasMaxLength(50);
            builder.Property(p => p.phone).HasMaxLength(15);
            builder.HasIndex(s => s.phone).IsUnique();
            builder.HasOne(s => s.User)
              .WithOne(u => u.Student)
              .HasForeignKey<Student>(s => s.UserId)
              .IsRequired();


        }
    }
}
