using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;
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
    class WorkTasks  //класс для работы с базой данных
    {
        private static WorkTasks Instance;
        private WorkTasks()
        {
        }
        public static WorkTasks GetInstance()
        {
            if (Instance == null)
            {
                Instance = new WorkTasks();
            }
            return Instance;
        }
        const string c_str = "Data Source=D:\\sqlite-tools-win-x64-3530400\\tasks.db";
        List<Task> tasks = new List<Task>(); //список задач
        int lastAddedId = -1; //значение id последней добавленной задачи
        public delegate void GotTasks(List<Task> tasks); //делегат для передачи списка задач
        public event GotTasks EOnGettingTasks;
        
        public void GetTasks() //получение задач
        {
            if (tasks.Count > 0)
            {
                tasks.Clear();
            }
            try
            {
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open(); //открытие базы данных
                    SqliteCommand s = c.CreateCommand();
                    s.CommandText = "select * from tasks;";
                    using (SqliteDataReader r = s.ExecuteReader())
                    {
                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                Task t = new Task();
                                t.id = Convert.ToInt32(r.GetValue(0).ToString());
                                t.name = r.GetValue(1).ToString();
                                t.description = r.GetValue(2).ToString();
                                t.state = (r.GetValue(3).ToString() == "1");
                                tasks.Add(t);
                                lastAddedId = t.id;
                            }
                        }
                    }
                    EOnGettingTasks?.Invoke(tasks);
                }
            }
            catch
            {
                return;
            }
        }
        //добавление задачи
        public void AddTask(string name, string description)
        {
            if (name == "") return; //отмена добавления, если имя пустое
            try
            {
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    SqliteCommand s = c.CreateCommand();
                    s.CommandText = $"insert into tasks values({lastAddedId + 1},'{name}','{description}',0);";
                    s.ExecuteReader();
                }
                lastAddedId++;
                GetTasks();
            }
            catch { return; }
        }
        //удаление задачи
        public void DeleteTask(int ind)
        {
            if (tasks.Count == 0) return;
            if (ind < 0 || ind >= tasks.Count) return;
            int id = tasks[ind].id;
            try
            {
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    SqliteCommand s = c.CreateCommand();
                    s.CommandText = $"delete from tasks where id={id};";
                    s.ExecuteReader();
                }
                GetTasks();
            }
            catch { return; }
        }
        //изменение выполненности/невыполненности задачи
        public void UpdateTaskCompletion(int ind)
        {
            if (tasks.Count == 0) return;
            if (ind < 0 || ind >= tasks.Count) return;
            int id = tasks[ind].id;
            string state; //состояние, на которое нужно изменить
            if (tasks[ind].state) //было выполнено
            {
                state = "0";
            }
            else state = "1";
            try
            {
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    SqliteCommand s = c.CreateCommand();
                    s.CommandText = $"update tasks set state={state} where id={id};";
                    s.ExecuteReader();
                }
                GetTasks();
            }
            catch { return; }
        }
    }
    class PresentTasks
    {
        WorkTasks w;   //объект для выполнения задач
        ListBox c;     //ссылка на список
        MainWindow f;  //ссылка на форму
        private static PresentTasks Instance;
        public static PresentTasks GetInstance(MainWindow form)
        {
            if (Instance == null)
            {
                Instance = new PresentTasks(form);
            }
            return Instance;
        }
        private PresentTasks(MainWindow form)
        {
            f = form;
            c = f.checkedListBox1;
            w = WorkTasks.GetInstance();
            w.EOnGettingTasks += ShowTasks;
        }
        private List<string> Descriptions = new List<string>(); //список описаний
        //получение списка задач
        public void GetTasks()
        {
            w.GetTasks();
        }
        //вывод задач
        private void ShowTasks(List<Task> tasks)
        {
            if (Descriptions.Count > 0)
            {
                Descriptions.Clear(); //очистка списка описаний
            }
            if (c.Items.Count > 0)
            {
                c.Items.Clear(); //очистка списка задач
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                Descriptions.Add(tasks[i].description); //добавление i-того описания
                c.Items.Add(tasks[i]); //добавление задачи (шаблон сам отрисует CheckBox)
            }
        }
        //добавление задачи
        public void AddTask()
        {
            w.AddTask(f.textBox_name.Text, f.textBox_description.Text);
            f.textBox_name.Clear();
            f.textBox_description.Clear();
        }
        //удаление задачи
        public void DeleteTask()
        {
            w.DeleteTask(c.SelectedIndex);
        }
        //вывод информации о задаче
        public void ShowTask()
        {
            if (c.SelectedIndex >= 0)
            {
                Task item = (Task)c.SelectedItem;
                f.label3.Content = "Название: " + item.name;      //вывод названия
                f.label4.Content = "Описание:" + item.description; //вывод описания
            }
        }
        public void UpdateTaskCompletion(int ind)
        {
            w.UpdateTaskCompletion(ind);
        }
    }
}