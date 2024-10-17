using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDo_List.Helper;

namespace ToDo_List
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        FileHelper fileHelper = new FileHelper();

        public MainWindow()
        {
            InitializeComponent();

            string msg = fileHelper.getFileData();
            //txtFileData.Text = msg;

            if (!string.IsNullOrWhiteSpace(msg))
            {
                string[] tasks = msg.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (var task in tasks)
                {
                    if (!string.IsNullOrWhiteSpace(task)) // Avoid empty lines
                    {
                        txtFileData.Items.Add(task); // Populate the ListBox with tasks
                    }
                }
            }
        }

        private void addToDoListBox(string item)
        {
            lstBox.Items.Add(item);
            txtInput.Text = "";
            fileHelper.AddData(item);

            btnAdd.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
        }

        private void txtFileData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = txtFileData.SelectedItem != null;
        }


        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtInput.Text.Length == 0)
            {
                btnAdd.IsEnabled = false;
            }
            else
            {
                btnAdd.IsEnabled = true;
                if (e.Key == Key.Enter)
                {
                    addToDoListBox(txtInput.Text);
                }
            }
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnAdd.IsEnabled = txtInput.Text.Length != 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addToDoListBox(txtInput.Text);
            if(txtInput.Text.Length == 0)
            {
                btnAdd.IsEnabled = false;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtFileData.SelectedItem == null)
            {
                MessageBox.Show("No task selected to delete.");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?",
                                                      "Delete Task",
                                                      MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                txtFileData.Items.Remove(txtFileData.SelectedItem);
                UpdateFileData(); // Update the file data after deletion
            }

            btnDelete.IsEnabled = false; // Disable delete button after deletion
        }

        private void UpdateFileData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in txtFileData.Items)
            {
                sb.AppendLine(item.ToString());
            }

            fileHelper.myFileData = sb.ToString(); // Update internal file data
            fileHelper.SaveFile(); // Save the updated file data
        }

    }
}