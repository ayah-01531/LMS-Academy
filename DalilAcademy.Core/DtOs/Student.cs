using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DtOs
{
    public record CreateStudentDto(string FullName, string Email, string Phone);

    public record UpdateStudentDto(string FullName, string Email, string Phone);

    public record StudentResponseDto(int Id, string FullName, string Email, string Phone);
}



