using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
namespace task9_3
{
    public partial class MainWindow : Window
    {
        Presenter p;
        DispatcherTimer timer; //таймер проверки сроков
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            p = Presenter.GetInstance(this);
            p.GetData(); //получение данных
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(60);
            timer.Tick += Timer_Tick;
            timer.Start();
            p.CheckDeadlines(); //проверка срока
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            p.CheckDeadlines(); //проверка срока
        }
        private void Button_add_Click(object sender, RoutedEventArgs e)
        {
            p.AddTask(); //добавление задачи
        }
        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            p.DeleteTask(); //удаление задачи
        }
        private void button_done_Click(object sender, RoutedEventArgs e)
        {
            p.MarkDone(); //выполнение задачи
        }
        private void button_changeDate_Click(object sender, RoutedEventArgs e)
        {
            p.ChangeDueDate(); //изменение даты
        }
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit) return; //возврат, если ничего не сделано
            if (p == null) return;
            if (!(e.EditingElement is TextBox tb)) return; //возврат, если редактируется не текст
            if (DataGridTasks.SelectedIndex < 0) return;
            try
            {
                p.ChangeTask(e.Column.DisplayIndex, tb.Text); //изменение задачи
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения: " + ex.Message);
            }
        }
        
    }
}