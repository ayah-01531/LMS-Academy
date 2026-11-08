using DalilAcademy.Core.DtOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Service
{
    public interface IEnrollmentService
    {
        Task<EnrollmentResponseDto> EnrollAsync(int studentId, int courseId);
        Task<List<EnrollmentResponseDto>> GetMyCoursesAsync(int userId);
    }
}
