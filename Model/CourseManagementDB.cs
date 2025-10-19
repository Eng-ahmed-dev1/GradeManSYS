using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManagmnetSYS.Model
{
    internal class CourseManagementDB : DbContext
    {
        public CourseManagementDB(){}
        public CourseManagementDB(DbContextOptions<CourseManagementDB> options):base(options){ }

        public DbSet<Users> Users { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<StudentCourses> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string con = "Data Source=DESKTOP-2008HFE\\SQLEXPRESS;Initial Catalog=CourseManagementDB;Integrated Security=True;Trust Server Certificate=True";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(con);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourses>().HasKey(x=>new {x.StudentId , x.CourseId} );
        }
    }
}
