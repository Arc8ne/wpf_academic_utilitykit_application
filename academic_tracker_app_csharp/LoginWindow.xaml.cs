using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace academic_tracker_app_csharp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    /// 
    public class ApplicationLogic
    {
        public const int max_users = 10;

        public User[] user_list = new User[max_users];

        public int current_users = 0;

        public User current_user = null;
    }

    public class User
    {
        public string user_name;

        public string user_passwd;

        public int assignments_submitted;

        public int assignments_count;

        public int exams_finished;

        public int exams_count;

        public Assignment start_assignment_obj = null;

        public Exam start_exam_obj = null;
    }
    public class Assignment
    {
        public string task_name;

        public string task_type;

        public string task_status;

        public string task_deadline;

        public Assignment next_assignment_obj;
    }
    public class Exam
    {
        public string exam_name;

        public string exam_status;

        public string exam_date;

        public Exam next_exam_obj;
    }

    public partial class LoginWindow : Window
    {
        public ApplicationLogic app_logic_obj = new ApplicationLogic();

        public LoginWindow()
        {
            InitializeComponent();

            app_logic_obj.current_users = 0;
        }

        private void log_in_btn_Click(object sender, RoutedEventArgs e)
        {
            if (user_name_box.Text == "" || passwd_box.Password == "")
            {
                MessageBox.Show("You have not entered a username and/or password. Please fill in both the username and password fields and try again. They cannot be blank.");
            }
            else
            {
                User new_user = new User();

                new_user.user_name = user_name_box.Text;

                new_user.user_passwd = passwd_box.Password;

                new_user.assignments_submitted = 0;

                new_user.assignments_count = 0;

                new_user.exams_finished = 0;

                new_user.exams_count = 0;

                app_logic_obj.user_list[app_logic_obj.current_users] = new_user;

                app_logic_obj.current_user = new_user;

                app_logic_obj.current_users++;

                //MessageBox.Show($"New user created with username '{new_user.user_name}' and password '{new_user.user_passwd}'.");

                UserDashboardWindow user_dashboard = new UserDashboardWindow(this);

                user_dashboard.Show();

                this.Hide();
            }
        }

        private void view_users_btn_Click(object sender, RoutedEventArgs e)
        {
            if (app_logic_obj.current_users == 0)
            {
                MessageBox.Show("There are no users currently registered in this application.");

                return;
            }

            int loop_count = 1;

            foreach (User current_user in app_logic_obj.user_list)
            {
                if (current_user == null)
                {
                    break;
                }

                MessageBox.Show($"User No. {loop_count}:\nUsername: {current_user.user_name}\nPassword: {current_user.user_passwd}");

                loop_count++;
            }
        }

        private void show_passwd_check_box_Checked(object sender, RoutedEventArgs e)
        {
            unmasked_passwd_box.Visibility = Visibility.Visible;

            unmasked_passwd_box.Text = passwd_box.Password;
        }

        private void show_passwd_check_box_Unchecked(object sender, RoutedEventArgs e)
        {
            unmasked_passwd_box.Visibility = Visibility.Hidden;

            unmasked_passwd_box.Text = passwd_box.Password;
        }
    }
}
