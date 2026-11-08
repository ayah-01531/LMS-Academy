using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Repository
{
    public interface ICourseRepository
    {
        Task<List<CourseResponseDto>> GetAll();
        Task<PageResultcs<CourseResponseDto>> GetPage(int Page, int PageSize, decimal ?minPrice, decimal ?maxPrice);
        Task<Course?> GetById(int id);
        Task<Course> Create(Course course);
        Task<List<CourseResponseDto>> GetCoursesById(int studentId);
        Task Update(Course course);
        Task <bool>Delete(int id);
       Task<bool> TitleExists(string title);
        Task<int> GetCount();
    }
}
