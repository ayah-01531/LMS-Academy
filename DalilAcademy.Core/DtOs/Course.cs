namespace DalilAcademy.Core.DtOs
{
    
         
public record CourseResponseDto(int Id, string Title, decimal Price, string? InstructorName);

    public record UpdateCourseDto(string Title, decimal Price);

    public record CreateCourseDto(string Title, decimal Price, int InstructorId);
}
