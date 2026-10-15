using System;
using System.Windows;
using System.Windows.Controls;
namespace task6_1_
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        SQLiteWorkers s;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.CellEditEnding -= dataGrid_CellEditEnding;
            dataGrid.Columns[0].Header = "Табельный номер";
            dataGrid.Columns[1].Header = "Имя";
            dataGrid.Columns[2].Header = "Должность";
            s = SQLiteWorkers.GetInstance(this);
            s.FillTable(); //первое получение информации из бд
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (dataGrid.ActualWidth > 0)
            {
                dataGrid.Columns[0].Width = new DataGridLength(dataGrid.ActualWidth / 3);
                dataGrid.Columns[1].Width = new DataGridLength(dataGrid.ActualWidth / 3);
                dataGrid.Columns[2].Width = new DataGridLength(dataGrid.ActualWidth / 3);
            }
        }
        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            s.AddWorker(textBox_name.Text, textBox_job_title.Text);
            textBox_name.Clear();
            textBox_job_title.Clear();
        }
        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex < 0)
            {
                return;
            }
            string[] arr = dataGrid.Items[dataGrid.SelectedIndex] as string[];
            if (arr == null)
            {
                return;
            }
            s.RemoveWorker(arr[0]);
        }
        public void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit) //отмена изменений
            {
                return;
            }
            int row = dataGrid.Items.IndexOf(e.Row.Item);
            int col = e.Column.DisplayIndex;
            TextBox tb = e.EditingElement as TextBox;
            string newValue = "";
            if (tb != null)
            {
                newValue = tb.Text;
            }
            s.EditWorker(row, col, newValue);
        }
    }
}