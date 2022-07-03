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
using System.IO;

namespace academic_tracker_app_csharp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    
    public class ApplicationLogic
    {
        public const string default_app_data_dir_path = @".\Academic UtilityKit Application";

        public const string default_app_data_file_path = default_app_data_dir_path + @"\app_data.daf";

        public int current_users = 0;

        public User start_user_obj;

        public User current_user = null;

        public static string check_or_create_user_dir(string user_name)
        {
            if (!Directory.Exists(ApplicationLogic.default_app_data_dir_path + @"\" + user_name))
            {
                Directory.CreateDirectory(ApplicationLogic.default_app_data_dir_path + @"\" + user_name);
            }

            return ApplicationLogic.default_app_data_dir_path + @"\" + user_name;
        }

        public static void save_users_data(User start_user_obj)
        {
            User temp_user_obj = start_user_obj;

            StreamWriter app_data_file_writer = new StreamWriter(default_app_data_file_path);

            while (temp_user_obj != null)
            {
                app_data_file_writer.WriteLine($"{temp_user_obj.user_name},{temp_user_obj.user_passwd}");

                temp_user_obj = temp_user_obj.next_user_obj;
            }

            app_data_file_writer.Close();

            temp_user_obj = start_user_obj;

            while (temp_user_obj != null)
            {
                string current_user_dir_path = check_or_create_user_dir(temp_user_obj.user_name);

                StreamWriter user_tasks_file_writer = new StreamWriter(current_user_dir_path + @"\tasks_data.daf");

                Assignment temp_assignment_obj = temp_user_obj.start_assignment_obj;

                //0 for assignment, 1 for exam

                while (temp_assignment_obj != null)
                {
                    user_tasks_file_writer.WriteLine($"0,{temp_assignment_obj.task_name},{temp_assignment_obj.task_type},{temp_assignment_obj.task_deadline},{temp_assignment_obj.task_status}");

                    temp_assignment_obj = temp_assignment_obj.next_assignment_obj;
                }

                Exam temp_exam_obj = temp_user_obj.start_exam_obj;

                while (temp_exam_obj != null)
                {
                    user_tasks_file_writer.WriteLine($"1,{temp_exam_obj.exam_name},{temp_exam_obj.exam_date},{temp_exam_obj.exam_status}");

                    temp_exam_obj = temp_exam_obj.next_exam_obj;
                }

                user_tasks_file_writer.Close();

                temp_user_obj = temp_user_obj.next_user_obj;
            }
        }
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

        public User next_user_obj = null;
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

            if (!Directory.Exists(ApplicationLogic.default_app_data_dir_path))
            {
                Directory.CreateDirectory(ApplicationLogic.default_app_data_dir_path);
            }

            if (!File.Exists(ApplicationLogic.default_app_data_file_path))
            {
                File.Create(ApplicationLogic.default_app_data_file_path);
            }
        }

        private void log_in_btn_Click(object sender, RoutedEventArgs e)
        {
            if (app_logic_obj.current_user != null)
            {
                app_logic_obj.current_user = null;
            }

            if (user_name_box.Text == "" || passwd_box.Password == "")
            {
                MessageBox.Show("You have not entered a username and/or password. Please fill in both the username and password fields and try again. They cannot be blank.");
            }
            else
            {
                User temp_user_obj = app_logic_obj.start_user_obj;

                while (temp_user_obj != null)
                {
                    if (temp_user_obj.user_name == user_name_box.Text && temp_user_obj.user_passwd == passwd_box.Password)
                    {
                        app_logic_obj.current_user = temp_user_obj;

                        break;
                    }

                    temp_user_obj = temp_user_obj.next_user_obj;
                }

                if (app_logic_obj.current_user == null)
                {
                    MessageBox.Show($"No account with the username '{user_name_box.Text}' was found.");

                    return;
                }

                UserDashboardWindow user_dashboard = new UserDashboardWindow(this);

                user_dashboard.Show();

                this.Hide();
            }
        }

        private void register_btn_Click(object sender, RoutedEventArgs e)
        {
            if (user_name_box.Text == "")
            {
                MessageBox.Show("Please enter a username, the username field cannot be blank.");

                return;
            }

            if (passwd_box.Password == "")
            {
                MessageBox.Show("Please enter a password, the password field cannot be blank.");

                return;
            }

            User new_user = new User();

            new_user.user_name = user_name_box.Text;

            new_user.user_passwd = passwd_box.Password;

            new_user.assignments_submitted = 0;

            new_user.assignments_count = 0;

            new_user.exams_finished = 0;

            new_user.exams_count = 0;

            new_user.next_user_obj = null;

            app_logic_obj.current_users++;

            if (app_logic_obj.start_user_obj == null)
            {
                app_logic_obj.start_user_obj = new_user;
            }
            else
            {
                User temp_user_obj = app_logic_obj.start_user_obj;

                /*
                Run this loop to check if the username of the account that the user is attempting to register is the same as the usernames of other 
                existing accounts
                */
                while (temp_user_obj != null)
                {
                    if (user_name_box.Text == temp_user_obj.user_name)
                    {
                        MessageBox.Show($"The username '{user_name_box.Text}' has already been taken. Please try using another username instead.");

                        return;
                    }

                    temp_user_obj = temp_user_obj.next_user_obj;
                }

                temp_user_obj = app_logic_obj.start_user_obj;

                while (temp_user_obj.next_user_obj != null)
                {
                    temp_user_obj = temp_user_obj.next_user_obj;
                }

                temp_user_obj.next_user_obj = new_user;
            }

            StreamWriter app_data_sw = new StreamWriter(ApplicationLogic.default_app_data_file_path);

            app_data_sw.WriteLine($"{new_user.user_name},{new_user.user_passwd}");

            app_data_sw.Close();

            ApplicationLogic.check_or_create_user_dir(new_user.user_name);

            MessageBox.Show("New account registered successfully.");

            //MessageBox.Show($"New user created with username '{new_user.user_name}' and password '{new_user.user_passwd}'.");
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

        private void login_window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ApplicationLogic.save_users_data(app_logic_obj.start_user_obj);

            MessageBox.Show("All application data saved successfully.");
        }
    }
}
