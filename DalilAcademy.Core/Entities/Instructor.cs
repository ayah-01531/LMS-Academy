using System;
using System.Collections.Generic;
using System.Text;
using DalilAcademy.Core.Entites;
using DalilAcademy.Core.Entities;



namespace DalilAcademy.Core.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public List<Course> Courses { get; set; } = [];

    }
}
