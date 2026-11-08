using DalilAcademy.Core.DtOs;

namespace DalilAcademy.Core.Service
{
    public interface IStudentService
    {
        Task<List<StudentResponseDto>> GetAll();
        Task<PageResultcs<StudentResponseDto>> GetPage(int Page, int PageSize);


        Task<StudentResponseDto> GetById(int id);

        Task<StudentResponseDto> Create(CreateStudentDto dto);

        Task Update(int id, UpdateStudentDto dto);

        Task Delete(int id);

        Task<List<CourseResponseDto>> GetCourses(int id);
    }
}
