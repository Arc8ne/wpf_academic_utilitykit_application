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
    /// Interaction logic for SubmitAssignmentPromptWindow.xaml
    /// </summary>
    public partial class SubmitAssignmentPromptWindow : Window
    {
        ApplicationLogic current_app_logic = null;

        public SubmitAssignmentPromptWindow(ApplicationLogic app_logic)
        {
            InitializeComponent();

            current_app_logic = app_logic;
        }

        private void submit_assignment_prompt_btn_Click(object sender, RoutedEventArgs e)
        {
            Assignment temp_assignment_obj = current_app_logic.current_user.start_assignment_obj;

            while (temp_assignment_obj != null)
            {
                if (temp_assignment_obj.task_name == assignment_name_box.Text)
                {
                    if (temp_assignment_obj.task_status == "Submitted")
                    {
                        this.Hide();

                        MessageBox.Show($"Assignment '{assignment_name_box.Text}' has already been submitted.");

                        return;
                    }

                    temp_assignment_obj.task_status = "Submitted";

                    current_app_logic.current_user.assignments_submitted++;

                    this.Hide();

                    MessageBox.Show($"Assignment '{assignment_name_box.Text}' submitted successfully.");

                    return;
                }

                temp_assignment_obj = temp_assignment_obj.next_assignment_obj;
            }

            MessageBox.Show($"No assignment with the name '{assignment_name_box.Text}' was found.");
        }
    }
}
