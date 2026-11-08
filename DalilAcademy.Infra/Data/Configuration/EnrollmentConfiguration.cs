using DalilAcademy.Core.Entites;
using DalilAcademy.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Infra.Data.Configuration
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => new { e.studentId, e.courseId }).IsUnique();
            builder.HasOne(e => e.course).WithMany(c => c.Enrollments).
            HasForeignKey(e => e.courseId);
            builder.HasOne(e => e.student).WithMany(s=> s.Enrollments).
             HasForeignKey(e => e.studentId);
            builder.Property(e => e.EnrollmentAt).HasDefaultValueSql("now()");
        }
    }
}
