using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entities;

namespace DalilAcademy.Core.Mappings
{
  public static class StudentMapping
    {
        // Convert Student Entity to StudentResponseDto
        public static StudentResponseDto ToDto(this Student student)
        {
            return new StudentResponseDto(
                student.Id,
                student.FullName,
                student.Email,
                student.phone
            );
        }
        public static Student ToEntity(this CreateStudentDto dto)
        {
            return new Student
            {
                FullName = dto.FullName,
                Email = dto.Email,
                phone = dto.Phone,
    
            };
        }
        // Update an existing Student Entity from UpdateStudentDto
        public static void UpdateFromDto(
            this Student student,
            UpdateStudentDto dto)
        {
            student.FullName = dto.FullName;
            student.Email = dto.Email;
            student.phone = dto.Phone;
        }


    }
}
