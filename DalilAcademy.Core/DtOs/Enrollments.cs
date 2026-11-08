using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.DtOs
{
    
 public record CreateEnrollmentDto(int StudentId, int CourseId);
  public record EnrollmentResponseDto(int Id,int StudentId,string StudentName,int CourseId,string CourseTitle,DateTime EnrolledAt);



}
