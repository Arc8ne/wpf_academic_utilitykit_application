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
    /// Interaction logic for TasksViewerWindow.xaml
    /// </summary>
    public partial class TasksViewerWindow : Window
    {
        UserDashboardWindow prev_dashboard_window = null;

        public TasksViewerWindow(UserDashboardWindow dashboard_window)
        {
            InitializeComponent();

            prev_dashboard_window = dashboard_window;

            Assignment temp_assignment_obj = prev_dashboard_window.prev_login_window.app_logic_obj.current_user.start_assignment_obj;

            Exam temp_exam_obj = prev_dashboard_window.prev_login_window.app_logic_obj.current_user.start_exam_obj;

            Thickness new_label_margin = new Thickness();

            new_label_margin.Top = 20;

            Color orange = new Color();

            orange.R = 251;

            orange.G = 183;

            orange.B = 65;

            orange.A = 255;

            SolidColorBrush orange_brush = new SolidColorBrush();

            orange_brush.Color = orange;

            Color red = new Color();

            red.R = 255;

            red.G = 10;

            red.B = 10;

            red.A = 255;

            SolidColorBrush red_brush = new SolidColorBrush();

            red_brush.Color = red;

            Color green = new Color();

            green.R = 0;

            green.G = 245;

            green.B = 12;

            green.A = 255;

            SolidColorBrush green_brush = new SolidColorBrush();

            green_brush.Color = green;

            if (temp_assignment_obj == null && temp_exam_obj == null)
            {
                TextBlock no_assignments_label = new TextBlock();

                no_assignments_label.Text = "You currently do not have any assignments and exams.";

                no_assignments_label.Margin = new_label_margin;

                tasks_content_grid.Children.Add(no_assignments_label);

                Grid.SetColumn(no_assignments_label, 1);

                Grid.SetRow(no_assignments_label, 3);

                Grid.SetColumnSpan(no_assignments_label, 5);
            }

            while (temp_assignment_obj != null)
            {
                TextBlock assignment_or_exam_label = new TextBlock();

                TextBlock assignment_name_label = new TextBlock();

                TextBlock assignment_type_label = new TextBlock();

                TextBlock assignment_deadline_label = new TextBlock();

                TextBlock assignment_status_label = new TextBlock();

                assignment_or_exam_label.Text = "Assignment";

                assignment_name_label.Text = temp_assignment_obj.task_name;

                assignment_type_label.Text = temp_assignment_obj.task_type;

                assignment_deadline_label.Text = temp_assignment_obj.task_deadline;

                assignment_status_label.Text = temp_assignment_obj.task_status;

                if (assignment_status_label.Text == "Not Submitted")
                {
                    assignment_status_label.Foreground = orange_brush;
                }
                else if (assignment_status_label.Text == "Overdue")
                {
                    assignment_status_label.Foreground = red_brush;
                }
                else if (assignment_status_label.Text == "Submitted")
                {
                    assignment_status_label.Foreground = green_brush;
                }

                assignment_or_exam_panel.Children.Add(assignment_or_exam_label);

                task_name_panel.Children.Add(assignment_name_label);

                task_type_panel.Children.Add(assignment_type_label);

                task_deadline_panel.Children.Add(assignment_deadline_label);

                task_status_panel.Children.Add(assignment_status_label);

                temp_assignment_obj = temp_assignment_obj.next_assignment_obj;
            }

            while (temp_exam_obj != null)
            {
                TextBlock assignment_or_exam_label = new TextBlock();

                TextBlock exam_name_label = new TextBlock();

                TextBlock exam_type_label = new TextBlock();

                TextBlock exam_date_label = new TextBlock();

                TextBlock exam_status_label = new TextBlock();

                assignment_or_exam_label.Text = "Exam";

                exam_name_label.Text = temp_exam_obj.exam_name;

                exam_type_label.Text = "Individual";

                exam_date_label.Text = temp_exam_obj.exam_date;

                exam_status_label.Text = temp_exam_obj.exam_status;

                if (exam_status_label.Text == "Upcoming")
                {
                    exam_status_label.Foreground = orange_brush;
                }
                else if (exam_status_label.Text == "Completed")
                {
                    exam_status_label.Foreground = green_brush;
                }

                assignment_or_exam_panel.Children.Add(assignment_or_exam_label);

                task_name_panel.Children.Add(exam_name_label);

                task_type_panel.Children.Add(exam_type_label);

                task_deadline_panel.Children.Add(exam_date_label);

                task_status_panel.Children.Add(exam_status_label);

                temp_exam_obj = temp_exam_obj.next_exam_obj;
            }
        }

        private void return_to_dashboard_btn_Click(object sender, RoutedEventArgs e)
        {
            prev_dashboard_window.UpdateUserStatLabels();

            prev_dashboard_window.Show();

            this.Hide();
        }
    }
}
