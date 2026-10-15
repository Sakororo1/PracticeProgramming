using System;
using System.Windows;
using System.Windows.Controls;
namespace task6_2_
{
    public partial class MainWindow : Window
    {
        PresentTasks p;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Load(object sender, RoutedEventArgs e)
        {
            p = PresentTasks.GetInstance(this);
            p.GetTasks(); //первое получение списка задач
        }
        private void TaskCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            object item = cb.DataContext;
            int ind = -1;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (ReferenceEquals(checkedListBox1.Items[i], item)) { ind = i; break; }
            }
            if (ind >= 0) p.UpdateTaskCompletion(ind);
        }
        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            p.AddTask(); //добавление задачи
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            p.DeleteTask();
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            p.ShowTask();
        }
    }
    struct Task //тип данных задача
    {
        public int id { get; set; } //номер
        public string name { get; set; } //название
        public string description { get; set; } //описание
        public bool state { get; set; } //состояние
    }   
}