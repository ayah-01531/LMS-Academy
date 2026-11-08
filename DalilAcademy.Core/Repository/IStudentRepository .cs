using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Repository
{
   public interface IStudentRepository
    {
        Task<List<StudentResponseDto>> GetAll();
        Task<PageResultcs<StudentResponseDto>> GetPage(int Page, int PageSize);

        Task<Student?> GetById(int id);
        Task<Student> Create(Student student);
        Task<Student?> GetWithCoursesAsync(int id);

        Task Update(Student student);
        Task<bool> Delete(int id);
    }
}
