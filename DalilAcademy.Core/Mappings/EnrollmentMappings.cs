using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace DalilAcademy.Core.Mappings
{
    public static class EnrollmentMappings
    {
        public static EnrollmentResponseDto ToDto(this Enrollment enrollment) => new(
       enrollment.Id,
       enrollment.studentId,
       enrollment.student?.FullName ?? string.Empty,
       enrollment.courseId,
       enrollment.course?.Title ?? string.Empty,
       enrollment.EnrollmentAt);
    }
}

