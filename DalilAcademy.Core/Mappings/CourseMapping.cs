using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entites;

namespace DalilAcademy.Core.Mappings
{
    public static class CourseMapping//داخل class static لازم method static
    {

        // Entity -> Output DTO

        public static CourseResponseDto ToDto(
        this Course course) => new(
            course.Id,
            course.Title,
            course.Price,
            course.Instructor?.FullName
        );



        public static Course ToEntity(this CreateCourseDto dto) 
        {
            return new Course{
            Title = dto.Title,
            Price = dto.Price,
            InstructorId = dto.InstructorId
            };


        }
        public static void UpdateFromDto(this Course course,UpdateCourseDto dto)
        {
            course.Title = dto.Title;
            course.Price = dto.Price;
        }
    }
}
