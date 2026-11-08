using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using FluentValidation;

namespace DalilAcademy.Infra.Service
{
    public class CourseService(ICourseRepository courseRepository, IValidator<CreateCourseDto> createValidator, IValidator<UpdateCourseDto> updateValidator) : ICourseService
    {
        public Task<List<CourseResponseDto>> GetAll()
        {
            return   courseRepository.GetAll();
        }
       public Task<PageResultcs<CourseResponseDto>> GetPage(int Page, int PageSize ,decimal? minPrice, decimal? maxPrice)
        {
        
            Page = Math.Max(Page, 1);
            PageSize = Math.Clamp(PageSize, 1, 50);
            return courseRepository.GetPage(Page, PageSize, minPrice, maxPrice);
        }
        public Task<List<CourseResponseDto>> GetCoursesByStudentId(int studentId)
        {
            return courseRepository.GetCoursesById(studentId);
        }
        public async Task<CourseResponseDto> GetById(int id)
        {
            var course = await courseRepository.GetById(id) ?? throw new NotFoundException($"Course {id} was not found.");   // → 404

            return course.ToDto();
        }
        public async Task<CourseResponseDto> Create(CreateCourseDto dto)
        {
            await createValidator.ValidateAndThrowAsync(dto);                    // → 400

            var course = dto.ToEntity();
            var created = await courseRepository.Create(course);           // duplicate title → 409

            return created.ToDto();
        }

        public async Task Update(int id, UpdateCourseDto dto)
        {
            await updateValidator.ValidateAndThrowAsync(dto);                    // → 400

            var course = await courseRepository.GetById(id)
                ?? throw new NotFoundException($"Course {id} was not found.");   // → 404

            course.UpdateFromDto(dto);

            await courseRepository.Update(course);                         // duplicate title → 409
        }

        public async Task Delete(int id)
        {
            if (!await courseRepository.Delete(id))
                throw new NotFoundException($"Course {id} was not found.");      // → 404
        }
        public Task<int> GetCount()
        {
            return courseRepository.GetCount();
        }

    }
}