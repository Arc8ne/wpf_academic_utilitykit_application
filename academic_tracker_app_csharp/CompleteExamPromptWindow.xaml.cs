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
    /// Interaction logic for CompleteExamPromptWindow.xaml
    /// </summary>
    public partial class CompleteExamPromptWindow : Window
    {
        ApplicationLogic current_app_logic = null;

        public CompleteExamPromptWindow(ApplicationLogic app_logic)
        {
            InitializeComponent();

            current_app_logic = app_logic;
        }

        private void complete_exam_prompt_btn_Click(object sender, RoutedEventArgs e)
        {
            if (exam_name_box.Text == "")
            {
                MessageBox.Show("Please enter in the name of the exam, the exam name field cannot be blank.");

                return;
            }

            Exam temp_exam_obj = current_app_logic.current_user.start_exam_obj;

            while (temp_exam_obj != null)
            {
                if (temp_exam_obj.exam_name == exam_name_box.Text)
                {
                    if (temp_exam_obj.exam_status == "Completed")
                    {
                        this.Hide();

                        MessageBox.Show($"An exam with the name '{temp_exam_obj.exam_name}' has already been marked as completed.");

                        return;
                    }

                    temp_exam_obj.exam_status = "Completed";

                    this.Hide();

                    MessageBox.Show($"Exam '{temp_exam_obj.exam_name}' marked as completed successfully.");

                    return;
                }

                temp_exam_obj = temp_exam_obj.next_exam_obj;
            }

            this.Hide();

            MessageBox.Show($"No exams with the name '{exam_name_box.Text}' were found.");
        }
    }
}
