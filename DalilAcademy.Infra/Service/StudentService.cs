using DalilAcademy.Core.DtOs;
using DalilAcademy.Core.Exceptions;
using DalilAcademy.Core.Mappings;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Service;
using DalilAcademy.Infra.Repository;
using FluentValidation;

namespace DalilAcademy.Infra.Service;

public class StudentService(
    IStudentRepository studentRepository,
    IValidator<CreateStudentDto> createValidator,
    IValidator<UpdateStudentDto> updateValidator)
    : IStudentService
{
    public Task<List<StudentResponseDto>> GetAll()
    {
      return  studentRepository.GetAll();
    }
    public Task<PageResultcs<StudentResponseDto>> GetPage(int Page, int PageSize)
    {
        return studentRepository.GetPage(Page, PageSize);

    }

    public async Task<StudentResponseDto> GetById(int id)
    {
        var student = await studentRepository.GetById(id)
            ?? throw new NotFoundException($"Student {id} was not found.");  // 404

        return student.ToDto();
    }

    public async Task<StudentResponseDto> Create(CreateStudentDto dto)
    {
        await createValidator.ValidateAndThrowAsync(dto); //  400
        

        var student = dto.ToEntity();
        var created = await studentRepository.Create(student);         // duplicate email → 409

        return created.ToDto();
    }

    public async Task Update(int id, UpdateStudentDto dto)
    {
        await updateValidator.ValidateAndThrowAsync(dto);                    //  400

        var student = await studentRepository.GetById(id)
            ?? throw new NotFoundException($"Student {id} was not found.");  //  404

        student.UpdateFromDto(dto);

        await studentRepository.Update(student);                       // duplicate email → 409
    }

    public async Task Delete(int id)
    {
        if (!await studentRepository.Delete(id))
            throw new NotFoundException($"Student {id} was not found.");     //  404
    }

    // Lecture 5 bonus
    public async Task<List<CourseResponseDto>> GetCourses(int id)
    {
        var student = await studentRepository.GetWithCoursesAsync(id)
            ?? throw new NotFoundException($"Student {id} was not found.");  //  404

        return student.Enrollments
            .Where(e => e.course is not null)
            .Select(e => e.course!.ToDto())
            .ToList();
    }
}
