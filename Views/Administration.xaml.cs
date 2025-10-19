using CoursesManagmnetSYS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CoursesManagmnetSYS.Views
{
    public partial class Administration : Window
    {
        private int _adminId;

        public Administration(int adminId = 0)
        {
            InitializeComponent();
            _adminId = adminId;
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            try
            {
                using var db = new CourseManagementDB();

                if (_adminId > 0)
                {
                    var admin = db.Users.FirstOrDefault(x => x.UserId == _adminId);
                    if (admin != null)
                    {
                        AdminNameLabel.Text = $"Welcome, {admin.UserName}";
                    }
                }

                var students = db.Users
                    .Where(u => u.Role == "Student")
                    .OrderBy(u => u.UserName)
                    .ToList();
                StudentComboBox.ItemsSource = students;

                var courses = db.Courses
                    .OrderBy(c => c.CourseName)
                    .ToList();
                CourseComboBox.ItemsSource = courses;

                LoadAllStudentCourses();

                StatusLabel.Content = " Data loaded successfully";
                StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            }
            catch (Exception ex)
            {
                StatusLabel.Content = $" Error loading data: {ex.InnerException?.Message ?? ex.Message}";
                StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            }
        }

        private void LoadAllStudentCourses()
        {
            try
            {
                using var db = new CourseManagementDB();

                var studentCourses = db.StudentCourses
                    .Include(sc => sc.Student)
                    .Include(sc => sc.Course)
                    .OrderBy(sc => sc.Student.UserName)
                    .ThenBy(sc => sc.Course.CourseName)
                    .Select(sc => new
                    {
                        sc.StudentCourseId,
                        sc.StudentId,
                        sc.CourseId,
                        StudentName = sc.Student.UserName,
                        CourseName = sc.Course.CourseName,
                        Grade = sc.Grade.HasValue ? sc.Grade.Value.ToString("F2") : "Not Assigned"
                    })
                    .ToList();

                StudentsGrid.ItemsSource = studentCourses;
                StatusLabel.Content = $" Displaying {studentCourses.Count} records";
                StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkBlue);
            }
            catch (Exception ex)
            {
                StatusLabel.Content = $" Error: {ex.InnerException?.Message ?? ex.Message}";
                StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            }
        }

        private void StudentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentComboBox.SelectedItem != null && CourseComboBox.SelectedItem == null)
            {
                FilterByStudent();
            }
        }

        private void FilterByStudent()
        {
            if (StudentComboBox.SelectedValue == null) return;

            try
            {
                using var db = new CourseManagementDB();
                int selectedStudentId = (int)StudentComboBox.SelectedValue;

                var studentCourses = db.StudentCourses
                    .Include(sc => sc.Student)
                    .Include(sc => sc.Course)
                    .Where(sc => sc.StudentId == selectedStudentId)
                    .OrderBy(sc => sc.Course.CourseName)
                    .Select(sc => new
                    {
                        sc.StudentCourseId,
                        sc.StudentId,
                        sc.CourseId,
                        StudentName = sc.Student.UserName,
                        CourseName = sc.Course.CourseName,
                        Grade = sc.Grade.HasValue ? sc.Grade.Value.ToString("F2") : "Not Assigned"
                    })
                    .ToList();

                StudentsGrid.ItemsSource = studentCourses;
                StatusLabel.Content = $" Displaying {studentCourses.Count} courses for selected student";
                StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkBlue);
            }
            catch (Exception ex)
            {
                StatusLabel.Content = $" Error: {ex.InnerException?.Message ?? ex.Message}";
                StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            }
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new CourseManagementDB();

                if (StudentComboBox.SelectedValue == null && CourseComboBox.SelectedValue == null)
                {
                    LoadAllStudentCourses();
                    return;
                }

                var query = db.StudentCourses
                    .Include(sc => sc.Student)
                    .Include(sc => sc.Course)
                    .AsQueryable();

                if (StudentComboBox.SelectedValue != null)
                {
                    int studentId = (int)StudentComboBox.SelectedValue;
                    query = query.Where(sc => sc.StudentId == studentId);
                }

                if (CourseComboBox.SelectedValue != null)
                {
                    int courseId = (int)CourseComboBox.SelectedValue;
                    query = query.Where(sc => sc.CourseId == courseId);
                }

                var results = query
                    .OrderBy(sc => sc.Student.UserName)
                    .ThenBy(sc => sc.Course.CourseName)
                    .Select(sc => new
                    {
                        sc.StudentCourseId,
                        sc.StudentId,
                        sc.CourseId,
                        StudentName = sc.Student.UserName,
                        CourseName = sc.Course.CourseName,
                        Grade = sc.Grade.HasValue ? sc.Grade.Value.ToString("F2") : "Not Assigned"
                    })
                    .ToList();

                StudentsGrid.ItemsSource = results;
                StatusLabel.Content = $" {results.Count} records";
                StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkBlue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void UpdateGrade_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StudentsGrid.SelectedItem == null)
                {
                    StatusLabel.Content = " Please select a record";
                    StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Orange);
                    return;
                }

                if (string.IsNullOrWhiteSpace(GradeTextBox.Text))
                {
                    StatusLabel.Content = " Please enter a grade";
                    StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Orange);
                    return;
                }

                if (!decimal.TryParse(GradeTextBox.Text, out decimal gradeValue) || gradeValue < 0 || gradeValue > 100)
                {
                    StatusLabel.Content = " Grade must be between 0 and 100";
                    StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Orange);
                    return;
                }

                dynamic selectedItem = StudentsGrid.SelectedItem;
                int studentCourseId = selectedItem.StudentCourseId;

                using var db = new CourseManagementDB();
                var studentCourse = db.StudentCourses.FirstOrDefault(sc => sc.StudentCourseId == studentCourseId);

                if (studentCourse == null)
                {
                    MessageBox.Show("Record not found");
                    return;
                }

                studentCourse.Grade = gradeValue;
                db.SaveChanges();

                StatusLabel.Content = $" Grade updated: {gradeValue:F2}";
                StatusLabel.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);

                LoadData_Click(sender, e);
                GradeTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }
    }
}