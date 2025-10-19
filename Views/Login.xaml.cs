using CoursesManagmnetSYS.Model;
using CoursesManagmnetSYS.Views;
using System.Net.WebSockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoursesManagmnetSYS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Log_in(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new CourseManagementDB();

                if (string.IsNullOrWhiteSpace(txtUserName.Text))
                {
                    invaliedData.Content = "Please Enter a Valied Username";
                    txtUserName.Text = "";
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    invaliedData.Content = "Please Enter a Valied Password";
                    txtPassword.Password = "";
                    return;
                }
                var user = db.Users.FirstOrDefault(x => x.UserName == txtUserName.Text);
                if (user == null)
                {
                    invaliedData.Content = "User Not Found!!";
                    txtUserName.Text = "";
                    txtPassword.Password = "";
                    return;
                }
                if (user.Password != txtPassword.Password)
                {
                    invaliedData.Content = "The Password is incorrect!!";
                    txtPassword.Password = "";
                    return;
                }

                if (user.Role == "Admin")
                {
                    new Administration().Show();
                    this.Close();
                }

                if (user.Role == "Student")
                {
                    new StudentInformation(user.UserId).Show();
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}