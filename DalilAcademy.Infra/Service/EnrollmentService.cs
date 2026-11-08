using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;

namespace DalilAcademy.Infra.Service;

public class EnrollmentService(
    IStudentRepository students,
    ICourseRepository courses,
    IEnrollmentRepository enrollments)
    : IEnrollmentService
{
    public async Task<EnrollmentResponseDto> EnrollAsync(
        int studentId, int courseId)
    {
        var student = await students.GetById(studentId)
            ?? throw new NotFoundException($"Student {studentId} was not found.");  //  404

        var course = await courses.GetById(courseId)
            ?? throw new NotFoundException($"Course {courseId} was not found.");    // 404

        var enrollment = new Enrollment
        {
            studentId = studentId,
            student = student,
            courseId = courseId,
            course = course,
            EnrollmentAt = DateTime.UtcNow
        };

        await enrollments.CreateAsync(enrollment);   // already enrolled → ConflictException → 409

       
        return enrollment.ToDto();
    }
   public async Task<List<EnrollmentResponseDto>> GetMyCoursesAsync(int userId)
    {
        var result = await enrollments.GetByStudentUserId(userId);

        return result
            .Select(e => new EnrollmentResponseDto(
                e.Id,
                e.studentId,
                e.student.FullName,
                e.courseId,
                e.course.Title,
                e.EnrollmentAt))
            .ToList();
    }

}
