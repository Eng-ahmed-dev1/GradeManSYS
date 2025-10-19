using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoursesManagmnetSYS.Model
{
    /*
    StudentCourseId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    Grade DECIMAL(5,2) NULL,
    CONSTRAINT FK_Student FOREIGN KEY(StudentId) REFERENCES Users(UserId),
    CONSTRAINT FK_Course FOREIGN KEY(CourseId) REFERENCES Courses(CourseId),
     
     */
    internal class StudentCourses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentCourseId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Grade { get; set; }  

        [ForeignKey(nameof(StudentId))]
        public Users Student { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Courses Course { get; set; }
    }
}
