using CoursesManagmnetSYS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CoursesManagmnetSYS.Views
{
    /// <summary>
    /// Interaction logic for StudentInformation.xaml
    /// </summary>
    public partial class StudentInformation : Window
    {
        private int _Stdid;
        public StudentInformation(int std)
        {
            InitializeComponent();
            _Stdid = std;
            LoadUser();
        }
        private void BackToHome(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }
        private void LoadUser()
        {
            try
            {
                using var db = new CourseManagementDB();
                var u = db.Users.FirstOrDefault(x => x.UserId == _Stdid);
                if (u != null)
                {
                    LabName.Content = u.UserName;
                }
                var uscrs = db.StudentCourses.
                    Include(e => e.Course)
                   .Include(e => e.Student)
                   .Where(e => e.StudentId == _Stdid)
                   .Select(e => new
                   {
                       Id = e.CourseId,
                       Stdname = e.Student.UserName,
                       CourseName = e.Course.CourseName,
                       Grade = e.Grade
                   }).ToList();
                GridShow.ItemsSource = uscrs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}