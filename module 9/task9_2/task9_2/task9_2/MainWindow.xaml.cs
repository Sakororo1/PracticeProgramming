using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace task9_2
{
    public partial class MainWindow : Window
    {
        Presenter p;
        public MainWindow()
        {
            InitializeComponent();
            Radio_T.IsChecked= true;
            Radio_P.IsChecked= false;
            Radio_E.IsChecked= false;
        }

        private void Radio_E_Checked(object sender, RoutedEventArgs e)
        {
            Employee_Name.Visibility = Visibility.Visible;
            Task_Name.Visibility = Visibility.Collapsed;
            Project_Name.Visibility = Visibility.Collapsed;
        }

        private void Radio_P_Checked(object sender, RoutedEventArgs e)
        {
            Employee_Name.Visibility = Visibility.Collapsed;
            Task_Name.Visibility = Visibility.Collapsed;
            Project_Name.Visibility =  Visibility.Visible;
        }

        private void Radio_T_Checked(object sender, RoutedEventArgs e)
        {
            Employee_Name.Visibility = Visibility.Collapsed;
            Task_Name.Visibility = Visibility.Visible;
            Project_Name.Visibility = Visibility.Collapsed;
        }

        private void Button_add_Click(object sender, RoutedEventArgs e)
        {
            if (Radio_T.IsChecked==true)
            {
                p.AddTask();
            }
            if (Radio_P.IsChecked == true)
            {
                p.AddProject();
            }
            if (Radio_E.IsChecked == true)
            {
                p.AddEmployee();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            p=Presenter.GetInstance(this);
            p.GetData();
        }
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit) return; 
            if (p == null) return;
            if (!(e.EditingElement is TextBox tb)) return;
            string new_val = tb.Text;                  
            var grid = (DataGrid)sender;  //определение таблицы
            int col = e.Column.DisplayIndex;
            int row = grid.SelectedIndex;   
            if (row < 0) return;       
            try
            {
                if (grid == DataGridEmployees) p.ChangeEmployee(col, new_val);    // изменение сотрудника
                else if (grid == DataGridProjects) p.ChangeProject(col, new_val); // изменение проекта
                else if (grid == DataGridTasks) p.ChangeTask(col, new_val);       // изменение задачи
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения: " + ex.Message);
            }
        }
        private void Button_doc_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridProjects.SelectedIndex>=0)
            {
                p.CreateProjectDoc();
            }
            else if (DataGridTasks.SelectedIndex>=0)
            {
                p.CreateTaskDoc();
            }

        }
        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            if (Radio_T.IsChecked == true) p.DeleteTask();
            else if (Radio_P.IsChecked == true) p.DeleteProject(); 
            else if (Radio_E.IsChecked == true) p.DeleteEmployee();
        }
    }
}
