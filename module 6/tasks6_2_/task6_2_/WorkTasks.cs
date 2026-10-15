using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace task6_2_
{
    internal class WorkTasks  //класс для работы с базой данных
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
}
