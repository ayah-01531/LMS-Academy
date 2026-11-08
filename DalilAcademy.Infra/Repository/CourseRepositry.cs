using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entites;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DalilAcademy.Infra.Repository
{
    public class CourseRepositry(AppDbContext db): ICourseRepository
    {

        public async Task<bool> TitleExists(string title)
        {
            return await db.Courses.AnyAsync(c => c.Title == title);
        }

        public async Task<List<CourseResponseDto>> GetAll()
        {
            return await db.Courses.Select(c => new CourseResponseDto(c.Id, c.Title, c.Price, c.Instructor.FullName)).ToListAsync();

        }
        //7
        public async Task<List<CourseResponseDto>> GetCoursesById(int studentId)
        {
            return await db.Enrollments
                .Where(e => e.studentId == studentId)
                .Select(e => new CourseResponseDto(
                    e.course.Id,
                    e.course.Title,
                    e.course.Price,
                    e.course.Instructor.FullName))
                .ToListAsync();
        }
        public async Task<PageResultcs<CourseResponseDto>> GetPage(int Page, int PageSize, decimal? minPrice, decimal? maxPrice)
        {
            

            var count = await db.Courses.Where(c => (minPrice == null || c.Price >= minPrice) &&
                    (maxPrice == null || c.Price <= maxPrice)).CountAsync();
            var item= await db.Courses
                .Where(c => (minPrice == null || c.Price >= minPrice) &&(maxPrice == null || c.Price <= maxPrice))
                .OrderBy(c=>c.Id)
                .Skip((Page-1)* PageSize)
                .Take(PageSize)
               . Select(c => new CourseResponseDto(c.Id, c.Title, c.Price, c.Instructor.FullName))
               .ToListAsync();

            return new PageResultcs<CourseResponseDto>(item, Page, PageSize, count);
        }
        public async Task<Course?> GetById(int id)
            {

            return await db.Courses.FindAsync(id);//دور على الكورس ايل ايل هاد ال id
            }
        public async Task< Course> Create(Course course)
            {
            try
            {
                db.Courses.Add(course);
                await db.SaveChangesAsync();

                await db.Entry(course).Reference(c => c.Instructor).LoadAsync();

                return course;
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is PostgresException
                {
                    SqlState: PostgresErrorCodes.UniqueViolation
                })
            {
                throw new ConflictException("A course with this title already exists.");
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is PostgresException
                {
                    SqlState: PostgresErrorCodes.ForeignKeyViolation
                })
            {
                throw new NotFoundException(
                    $"Instructor {course.InstructorId} was not found.");
            }

        }

        public async Task Update( Course course)
            {
            db.Courses.Update(course);

            await db.SaveChangesAsync();
            
        }
        public async Task<bool> Delete(int id)
            {
            var course = await db.Courses.FindAsync(id);
            if (course is null)
             return false;

            db.Courses.Remove(course);
            db.SaveChanges();
            return true;

        }

        public async Task<int> GetCount()
        {
            return await db.Courses.CountAsync();
        }



    }
}
