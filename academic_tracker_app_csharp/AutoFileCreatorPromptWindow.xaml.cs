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
    /// Interaction logic for AutoFileCreatorPromptWindow.xaml
    /// </summary>
    public partial class AutoFileCreatorPromptWindow : Window
    {
        ApplicationLogic current_app_logic = null;

        public AutoFileCreatorPromptWindow(ApplicationLogic app_logic)
        {
            InitializeComponent();

            current_app_logic = app_logic;
        }

        private void create_files_btn_Click(object sender, RoutedEventArgs e)
        {
            if (file_extension_text_box.Text == "")
            {
                MessageBox.Show("File extension field cannot be blank. Please enter a file extension(e.g. .txt for text files, .py for Python source code files etc).");

                return;
            }

            if (file_name_prefix_box.Text == "")
            {
                MessageBox.Show("Please enter a file name prefix, the file name prefix field cannot be blank.");

                return;
            }

            if (file_count_box.Text == "")
            {
                MessageBox.Show("Please enter the number of files that you want to create.");

                return;
            }

            try
            {
                Convert.ToInt32(file_count_box.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("A FormatException error has occurred, please try again and ensure that you have entered a number into the number of files to create field.");

                return;
            }

            int files_to_create = Convert.ToInt32(file_count_box.Text);

            if (files_to_create <= 0)
            {
                MessageBox.Show("The number of files you would like to create must be greater than 0.");

                return;
            }

            if (folder_path_box.Text == "")
            {
                MessageBox.Show("Please enter the path to the folder/directory that you want the files to be created in. The folder path field cannot be blank.");

                return;
            }

            if (folder_path_box.Text != "" && !Directory.Exists(folder_path_box.Text))
            {
                MessageBox.Show($"The folder/directory at the path you have provided ({folder_path_box.Text}) does not exist.");

                return;
            }

            for (int i = 0; i < files_to_create; i++)
            {
                string final_file_path = folder_path_box.Text + @"\" + file_name_prefix_box.Text + Convert.ToString(i + 1) + file_extension_text_box.Text;

                //MessageBox.Show("Result file path: " + final_file_path);

                FileStream returned_file_stream = File.Create(final_file_path);

                if (chosen_file_text_box.Text != "")
                {
                    StreamWriter new_stream_writer = new StreamWriter(returned_file_stream);

                    //MessageBox.Show(chosen_file_text_box.Text);

                    new_stream_writer.WriteLine(chosen_file_text_box.Text);

                    new_stream_writer.Close();
                }

                returned_file_stream.Close();
            }

            this.Hide();

            MessageBox.Show($"{files_to_create} {file_extension_text_box.Text} files created successfully.");
        }
    }
}
