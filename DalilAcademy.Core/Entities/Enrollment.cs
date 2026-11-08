using DalilAcademy.Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int courseId { get; set; }
        public Course? course { get; set; }
        public int studentId { get; set; }
        public Student?student { get; set; }
        public DateTime EnrollmentAt { get; set; }




    }
}
