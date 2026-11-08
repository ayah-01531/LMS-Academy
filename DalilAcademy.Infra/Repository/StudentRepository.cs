using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Entities;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Repository;
using DalilAcademy.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;


namespace DalilAcademy.Infra.Repository
{
    public class StudentRepository(AppDbContext db) : IStudentRepository
    {
        //7
        public async Task<List<StudentResponseDto>> GetAll()
        {
            return await db.Students.Select(c => new StudentResponseDto(c.Id, c.FullName, c.Email, c.phone)).ToListAsync();
        }
        



        //7
        public async Task<PageResultcs<StudentResponseDto>> GetPage(int Page, int PageSize)
        {
            var count = await db.Students.CountAsync();
            var item = await db.Students
                .OrderBy(c => c.Id)
                .Skip((Page - 1) * PageSize)//عددالكورس ايلي رح يعمل عنهم 
                .Take(PageSize)
               .Select(c => new StudentResponseDto(c.Id, c.FullName, c.Email, c.phone))
               .ToListAsync();

            return new PageResultcs<StudentResponseDto>(item, Page, PageSize, count);
        }
        public async Task<Student?> GetById(int id)
        {

            return await db.Students.FindAsync(id);
        }
        // Lecture 5 bonus: Include + ThenInclude
        public async Task<Student?> GetWithCoursesAsync(int id)
        {
            return await db.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.course)
                        .ThenInclude(c => c!.Instructor)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Student> Create(Student student)
        {
            try
            {
                db.Students.Add(student);
                await db.SaveChangesAsync();

                return student;
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is PostgresException
                {
                    SqlState: PostgresErrorCodes.UniqueViolation
                })
            {
                throw new ConflictException("A student with this email already exists.");
            }
            catch (DbUpdateException ex)
             when (ex.InnerException is PostgresException
             {
                 SqlState: PostgresErrorCodes.UniqueViolation,
                 ConstraintName: "IX_Students_Phone"
             })
            {
                throw new ConflictException("A student with this phone number already exists.");
            }


        }

        public async Task Update(Student student)
        {
            try
            {
                db.Students.Update(student);
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is PostgresException
                {
                    SqlState: PostgresErrorCodes.UniqueViolation,
                    ConstraintName: "IX_Students_Email"
                })
            {
                throw new ConflictException("A student with this email already exists.");
            }
            catch (DbUpdateException ex)
             when (ex.InnerException is PostgresException
            {
               SqlState: PostgresErrorCodes.UniqueViolation,
               ConstraintName: "IX_Students_Phone"
             })
            {
                throw new ConflictException("A student with this phone number already exists.");
            }

        }
        public async Task<bool> Delete(int id)
        {
            var student = await db.Students.FindAsync(id);

            if (student is null)
                return false;

            db.Students.Remove(student);
            await db.SaveChangesAsync();

            return true;

        }

    }
}
