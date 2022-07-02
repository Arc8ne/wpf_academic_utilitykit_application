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
    /// Interaction logic for AddExamPromptWindow.xaml
    /// </summary>
    
    public partial class AddExamPromptWindow : Window
    {
        ApplicationLogic current_app_logic = null;

        public AddExamPromptWindow(ApplicationLogic app_logic)
        {
            InitializeComponent();

            current_app_logic = app_logic;
        }

        private void mini_add_exam_btn_Click(object sender, RoutedEventArgs e)
        {
            if (exam_name_text_box.Text == "")
            {
                MessageBox.Show("Please enter in the exam name, the exam name field cannot be blank.");

                return;
            }

            if (exam_date_picker.Text == "")
            {
                MessageBox.Show("Please enter in the date of the exam, the exam date field cannot be blank.");

                return;
            }

            if (exam_status_combo_box.Text == "")
            {
                MessageBox.Show("Please enter in the exam status, the exam status field cannot be blank.");

                return;
            }

            Exam new_exam = new Exam();

            new_exam.exam_name = exam_name_text_box.Text;

            new_exam.exam_date = exam_date_picker.Text;

            new_exam.exam_status = exam_status_combo_box.Text;

            new_exam.next_exam_obj = null;

            Exam temp_exam_obj = current_app_logic.current_user.start_exam_obj;

            if (temp_exam_obj == null)
            {
                current_app_logic.current_user.start_exam_obj = new_exam;
            }
            else
            {
                while (temp_exam_obj.next_exam_obj != null)
                {
                    temp_exam_obj = temp_exam_obj.next_exam_obj;
                }

                temp_exam_obj.next_exam_obj = new_exam;
            }

            current_app_logic.current_user.exams_count++;

            this.Hide();

            MessageBox.Show($"New exam '{new_exam.exam_name}' created successfully.");
        }
    }
}
