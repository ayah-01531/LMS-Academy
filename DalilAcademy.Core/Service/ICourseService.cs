using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Service
{
    public interface ICourseService
    {
        Task<List<CourseResponseDto>> GetAll();
        Task<PageResultcs<CourseResponseDto>> GetPage(int Page, int PageSize,decimal ?minPrice,decimal ?maxPrice);
        Task<CourseResponseDto> GetById(int id);
        Task<CourseResponseDto> Create(CreateCourseDto course);
        Task<List<CourseResponseDto>> GetCoursesByStudentId(int studentId);
        Task Update(int id, UpdateCourseDto course);
        Task Delete(int id);
        Task<int> GetCount();
    }
}
