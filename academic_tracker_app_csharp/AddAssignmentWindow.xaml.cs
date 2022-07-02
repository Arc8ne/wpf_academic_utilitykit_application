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
    /// Interaction logic for AddAssignmentWindow.xaml
    /// </summary>
    public partial class AddAssignmentWindow : Window
    {
        User current_user = null;

        public AddAssignmentWindow(User current_app_user)
        {
            InitializeComponent();

            current_user = current_app_user;
        }

        private void mini_add_assignment_btn_Click(object sender, RoutedEventArgs e)
        {
            if (assignment_name_text_box.Text == "")
            {
                MessageBox.Show("Please enter in an assignment name, the assignment name field cannot be blank.");

                return;
            }

            if (assignment_type_combo_box.Text == "")
            {
                MessageBox.Show("Please select an option for the assignment type, the assignment type field cannot be blank.");

                return;
            }

            if (assignment_deadline_picker.Text == "")
            {
                MessageBox.Show("Please enter an assignment deadline with the following format (DD/MM/YYYY), the assignment deadline field cannot be blank.");

                return;
            }

            if (assignment_status_combo_box.Text == "")
            {
                MessageBox.Show("Please select an option for the assignment status, the assignment status field cannot be blank.");

                return;
            }

            Assignment new_assignment = new Assignment();

            new_assignment.task_name = assignment_name_text_box.Text;

            new_assignment.task_type = assignment_type_combo_box.Text;

            new_assignment.task_deadline = assignment_deadline_picker.Text;

            new_assignment.task_status = assignment_status_combo_box.Text;

            if (current_user.start_assignment_obj == null)
            {
                current_user.start_assignment_obj = new_assignment;
            }
            else
            {
                Assignment temp_assignment = current_user.start_assignment_obj;

                while (temp_assignment.next_assignment_obj != null)
                {
                    temp_assignment = temp_assignment.next_assignment_obj;
                }

                temp_assignment.next_assignment_obj = new_assignment;
            }

            current_user.assignments_count++;

            this.Hide();

            MessageBox.Show($"New assignment '{assignment_name_text_box.Text}' created successfully.");
        }
    }
}
