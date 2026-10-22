using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
namespace task9_3
{
    internal class Model
    {
        public struct Task //структура задача
        {
            private int fid; //номер
            private string fname; //название
            private string fpriority; //приоритет: низкий/средний/высокий
            private DateTime fdue_date; //срок выполнения
            private int fprogress; //прогресс, в процентах от 0 до 100
            public int Id
            {
                get { return fid; }
                set { fid = value; }
            }
            public string Name
            {
                get { return fname; }
                set { fname = value; }
            }
            public string Priority
            {
                get { return fpriority; }
                set { fpriority = value; }
            }
            public DateTime DueDate
            {
                get { return fdue_date; }
                set { fdue_date = value; }
            }
            public int Progress
            {
                get { return fprogress; }
                set
                {
                    if (value < 0) fprogress = 0;
                    else if (value > 100) fprogress = 100;
                    else fprogress = value;
                }
            }
            public Task(int id, string name, string priority, DateTime dueDate, int progress)
            {
                fid = id;
                fname = name;
                fpriority = priority;
                fdue_date = dueDate;
                if (progress < 0) fprogress = 0;
                else if (progress > 100) fprogress = 100;
                else fprogress = progress;
            }
        }
        public const string c_str = "Data Source=Tasks.db"; //строка подключения
        private class BaseCreator //класс для создания базы данных
        {
            static public void CreateNewBase() //первое создание базы данных
            {
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = "create table tasks(id integer primary key autoincrement, name text not null, priority text, due_date text, progress integer);";
                        cmd.ExecuteReader().Close();
                    }
                }
            }
        }
        private void CheckBase() //проверка, что база данных существует
        {
            if (!File.Exists("Tasks.db")) BaseCreator.CreateNewBase();
        }
        public void GetData() //получение информации
        {
            CheckBase();
            WorkTasks data = new WorkTasks();
            data.GetData();
            OnGotTasks?.Invoke(data.tasks);
        }
        public void AddData(string[] values) //добавление записи
        {
            CheckBase();
            WorkTasks data = new WorkTasks();
            data.AddData(values);
            GetData();
        }
        public void Delete(int index) //удаление записи
        {
            CheckBase();
            WorkTasks data = new WorkTasks();
            data.GetData();
            data.Delete(index);
            GetData();
        }
        public void ChangeData(int row, int col, string new_val) //изменение записи
        {
            if (col <= 0) return; //номер нельзя менять
            CheckBase();
            WorkTasks data = new WorkTasks();
            data.GetData();
            data.ChangeData(row, col, new_val);
            GetData();
        }
        public List<Task> GetUpcoming(int hours) //список задач с приближающимся или наступившим сроком
        {
            CheckBase();
            WorkTasks w = new WorkTasks();
            w.GetData();
            List<Task> result = new List<Task>();
            DateTime now = DateTime.Now; //текущее время
            foreach (Task t in w.tasks)
            {
                if (t.Progress >= 100) continue; //пропуск выполненных задач
                double span = (t.DueDate - now).TotalHours; //получение часов от нынешнего времени до срока
                if (span <= hours && span >= -24) result.Add(t); //в пределах hours часов до и 24 часа после срока
            }
            return result;
        }
        public delegate void GotTasks(List<Task> Data);
        public event GotTasks OnGotTasks; //получение информации о задачах
        class WorkTasks //работа с таблицей задач
        {
            public List<Task> tasks = new List<Task>(); //список задач
            public void GetData() //получение информации
            {
                if (tasks.Count > 0) tasks.Clear();
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = "select * from tasks;";
                        using (SqliteDataReader r = cmd.ExecuteReader())
                        {
                            if (r.HasRows)
                            {
                                while (r.Read())
                                {
                                    DateTime dd;
                                    string s = r.GetValue(3).ToString();
                                    if (!DateTime.TryParseExact(s, "dd-MM-yyyy HH:mm", null,
                                        System.Globalization.DateTimeStyles.None, out dd))
                                        DateTime.TryParse(s, out dd);
                                    tasks.Add(new Task(
                                        Convert.ToInt32(r.GetValue(0).ToString()),
                                        r.GetValue(1).ToString(),
                                        r.GetValue(2).ToString(),
                                        dd,
                                        Convert.ToInt32(r.GetValue(4).ToString())));
                                }
                            }
                        }
                    }
                }
            }
            public void Delete(int index) //удаление задачи
            {
                if (index < 0 || index >= tasks.Count) return;
                int id = tasks[index].Id; //получение номера
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = $"delete from tasks where id={id};";
                        cmd.ExecuteReader().Close();
                    }
                }
            }
            public void ChangeData(int row, int col, string new_val)
            {
                if (row < 0 || row >= tasks.Count) return;
                int id = tasks[row].Id;
                string field;
                switch (col)
                {
                    case 1: field = "name"; break;
                    case 2: field = "priority"; break;
                    case 3: field = "due_date"; break;
                    case 4: field = "progress"; break;
                    default: return;
                }
                if (col==1||col==2||col==3)
                {
                    new_val = "'" + new_val.Replace("'", "''") + "'";
                }
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = $"update tasks set {field}={new_val} where id={id};";
                        cmd.ExecuteReader().Close();
                    }
                }
            }
            public void AddData(string[] values)
            {
                if (values == null || values.Length < 4) return;
                string name = values[0].Replace("'", "''");
                string priority = values[1].Replace("'", "''");
                string dueDate = values[2].Replace("'", "''");
                int progress;
                int.TryParse(values[3], out progress);
                if (progress < 0) progress = 0;
                if (progress > 100) progress = 100;
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = $"insert into tasks values (null,'{name}','{priority}','{dueDate}',{progress});";
                        cmd.ExecuteReader().Close();
                    }
                }
            }
        }
    }
}