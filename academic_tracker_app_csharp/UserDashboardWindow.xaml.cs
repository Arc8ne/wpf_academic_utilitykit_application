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
    /// Interaction logic for UserDashboardWindow.xaml
    /// </summary>
    public partial class UserDashboardWindow : Window
    {
        public LoginWindow prev_login_window;

        int unsubmitted_assignments_count = 0;

        int submitted_assignments_count = 0;

        int upcoming_exams_count = 0;

        int completed_exams_count = 0;

        int overdue_assignments_count = 0;

        public void UpdateUserStatLabels()
        {
            Color red = new Color();

            red.R = 255;

            red.G = 10;

            red.B = 10;

            red.A = 255;

            SolidColorBrush red_brush = new SolidColorBrush();

            red_brush.Color = red;

            unsubmitted_assignments_count_label.Text = $"{unsubmitted_assignments_count}";

            submitted_assignments_count_label.Text = $"{submitted_assignments_count}";

            upcoming_exams_count_label.Text = $"{upcoming_exams_count}";

            completed_exams_count_label.Text = $"{completed_exams_count}";

            overdue_assignments_count_label.Text = $"{overdue_assignments_count}";

            if (overdue_assignments_count > 0)
            {
                overdue_assignments_count_label.Foreground = red_brush;
            }
        }

        public UserDashboardWindow(LoginWindow prev_login_window_obj)
        {
            InitializeComponent();

            prev_login_window = prev_login_window_obj;

            welcome_label.Text = $"Hello {prev_login_window.app_logic_obj.current_user.user_name}. What would you like to do today?";

            UpdateUserStatLabels();
        }

        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            prev_login_window.user_name_box.Text = "";

            prev_login_window.passwd_box.Password = "";

            prev_login_window.unmasked_passwd_box.Text = "";

            prev_login_window.Show();

            this.Hide();
        }

        private void add_assignment_btn_Click(object sender, RoutedEventArgs e)
        {
            AddAssignmentWindow add_assignment_screen = new AddAssignmentWindow(prev_login_window.app_logic_obj.current_user);

            add_assignment_screen.Show();
        }

        private void view_tasks_btn_Click(object sender, RoutedEventArgs e)
        {
            TasksViewerWindow task_viewer_screen = new TasksViewerWindow(this);

            task_viewer_screen.Show();

            this.Hide();
        }

        private void submit_assignment_btn_Click(object sender, RoutedEventArgs e)
        {
            SubmitAssignmentPromptWindow submit_assignment_prompt_screen = new SubmitAssignmentPromptWindow(prev_login_window.app_logic_obj);

            submit_assignment_prompt_screen.Show();
        }

        private void add_exam_btn_Click(object sender, RoutedEventArgs e)
        {
            AddExamPromptWindow add_exam_prompt_screen = new AddExamPromptWindow(prev_login_window.app_logic_obj);

            add_exam_prompt_screen.Show();
        }

        private void complete_exam_btn_Click(object sender, RoutedEventArgs e)
        {
            CompleteExamPromptWindow complete_exam_prompt_screen = new CompleteExamPromptWindow(prev_login_window.app_logic_obj);

            complete_exam_prompt_screen.Show();
        }

        private void remove_task_btn_Click(object sender, RoutedEventArgs e)
        {
            RemoveTaskPromptWindow remove_task_prompt_screen = new RemoveTaskPromptWindow(prev_login_window.app_logic_obj);

            remove_task_prompt_screen.Show();
        }

        private void auto_file_creator_btn_Click(object sender, RoutedEventArgs e)
        {
            AutoFileCreatorPromptWindow auto_file_create_screen = new AutoFileCreatorPromptWindow(prev_login_window.app_logic_obj);

            auto_file_create_screen.Show();
        }
    }
}
