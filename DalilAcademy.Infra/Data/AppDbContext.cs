using DalilAcademy.Core.Entites;
using DalilAcademy.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DalilAcademy.Infra.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext>options):DbContext (options) //options:database orovider+connections string in program.cs
    {

        //DbSet
        public DbSet<Course> Courses=> Set<Course>();
        public DbSet<Student> Students=> Set<Student>();
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Seed only once
            //كل مرة بعمل updatedatabase رح يجي يشغل ال seed 
            optionsBuilder.UseSeeding((context, _) => 
            {
                //مابحط ال 
                if (!context.Set<Instructor>().Any())
                {//لو ما حطيت هاد رح يرجع يخزن الdataايلي اصلا مخزنة عندي كمان مرة 
                    var ahmade = new Instructor { FullName = "Ahmad Ali" };
                    var ali = new Instructor { FullName = "Ali Mousa" };
                    var raghad = new Instructor { FullName = "Raghad Amjd" };


                    context.Set<Course>().AddRange(
                        new Course { Title = "c# Fundamentals ", Price = 50, Instructor = ahmade }

                        );



                    context.SaveChanges();//يحتفظ بالتعديلات بالداتا بيز 
                }
                if (!context.Set<User>().Any())
                {

                    context.Set<User>().Add(new User
                    {
                        Email = "admin@dalil.jo",
                        Role = Roles.Admin,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("aya2001")
                    });
                    context.SaveChanges();//يحتفظ بالتعديلات بالداتا بيز 


                }

            });


        }

    }
}
