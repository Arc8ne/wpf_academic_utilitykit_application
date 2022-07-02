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
    /// Interaction logic for RemoveTaskPromptWindow.xaml
    /// </summary>
    public partial class RemoveTaskPromptWindow : Window
    {
        ApplicationLogic current_app_logic = null;

        public RemoveTaskPromptWindow(ApplicationLogic app_logic)
        {
            InitializeComponent();

            current_app_logic = app_logic;
        }

        private void remove_task_prompt_btn_Click(object sender, RoutedEventArgs e)
        {
            if (task_type_combo_box.Text == "")
            {
                MessageBox.Show("Please choose a type. The type field cannot be blank.");

                return;
            }

            if (task_name_box.Text == "")
            {
                MessageBox.Show($"Please enter in the name of the {task_type_combo_box.Text} you would like to remove. The name field cannot be blank.");

                return;
            }

            if (task_type_combo_box.Text == "Assignment")
            {
                Assignment prev_temp_assignment_obj = null;

                Assignment temp_assignment_obj = current_app_logic.current_user.start_assignment_obj;

                while (temp_assignment_obj != null)
                {
                    if (temp_assignment_obj.task_name == task_name_box.Text)
                    {
                        if (prev_temp_assignment_obj != null)
                        {
                            prev_temp_assignment_obj = temp_assignment_obj.next_assignment_obj;
                        }
                        else
                        {
                            current_app_logic.current_user.start_assignment_obj = temp_assignment_obj.next_assignment_obj;
                        }

                        this.Hide();

                        MessageBox.Show($"Removed {task_type_combo_box.Text} '{temp_assignment_obj.task_name}' successfully.");

                        return;
                    }

                    prev_temp_assignment_obj = temp_assignment_obj;

                    temp_assignment_obj = temp_assignment_obj.next_assignment_obj;
                }
            }
            else if (task_type_combo_box.Text == "Exam")
            {
                Exam prev_temp_exam_obj = null;

                Exam temp_exam_obj = current_app_logic.current_user.start_exam_obj;

                while (temp_exam_obj != null)
                {
                    if (temp_exam_obj.exam_name == task_name_box.Text)
                    {
                        if (prev_temp_exam_obj != null)
                        {
                            prev_temp_exam_obj = temp_exam_obj.next_exam_obj;
                        }
                        else
                        {
                            current_app_logic.current_user.start_exam_obj = temp_exam_obj.next_exam_obj;
                        }

                        this.Hide();

                        MessageBox.Show($"Removed {task_type_combo_box.Text} '{temp_exam_obj.exam_name}' successfully.");

                        return;
                    }

                    prev_temp_exam_obj = temp_exam_obj;

                    temp_exam_obj = temp_exam_obj.next_exam_obj;
                }
            }

            this.Hide();

            MessageBox.Show($"{task_type_combo_box.Text} '{task_name_box.Text}' not found.");
        }
    }
}
