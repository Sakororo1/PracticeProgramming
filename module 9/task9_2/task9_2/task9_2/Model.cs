using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
namespace task9_2
{
    public enum StructType { EEmployee, ETask, EProject };
    internal class Model
    {        
        public struct Employee  //структура сотрудник
        {
            private int fid; //номер
            private string fname; //фио
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
            public Employee(int id, string name)
            {
                fid = id;
                fname = name;
            }
        }
        public struct Task //структура задача
        {
            private int fid; //номер
            private string fname; //название
            private int femployee_id; //название сотрудника
            public byte fprogress; //прогресс, в процентах от 0 до 100
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
            public int Employee_id
            {
                get { return femployee_id; }
                set { femployee_id = value; }
            }
            public byte Progress
            {
                get { return fprogress; }
                set
                {
                    if (value > 100) fprogress = 100;
                    else fprogress = unchecked(value);
                }
            }
            public Task(int id, string name, int employee_id, byte progress)
            {
                fid = id;
                fname = name;
                femployee_id = employee_id;
                fprogress = progress;
            }
        }
        public struct Project //структура задача
        {
            private int fid; //номер
            private string fname; //фио
            private string fstatus; //статус
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
            public string Status
            {
                get { return fstatus; }
                set { fstatus = value; }
            }
            public Project(int id, string name, string status)
            {
                fid = id;
                fname = name;
                fstatus = status;
            }
        }
        public const string c_str = "Data Source=Tasks.db"; //строка подключения
        private class BaseCreator //класс для создания базы данных
        {
            static public void CreateNewBase()  //первое создание базы данных
            {
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open(); //открытие базы данных
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        //создание таблицы с сотрудниками
                        cmd.CommandText = "create table employees(id integer primary key autoincrement, name text not null);";
                        cmd.ExecuteReader().Close();
                        //создание таблицы с задачами
                        cmd.CommandText = "create table tasks(id integer primary key autoincrement, name text not null, employee_id integer, progress integer);";
                        cmd.ExecuteReader().Close();
                        //создание таблицы с проектами
                        cmd.CommandText = "create table projects(id integer primary key autoincrement, name text not null, status text);";
                        cmd.ExecuteReader().Close();
                    }
                }

            }
        }
        public void GetData(StructType t) //получение информации
        {
            if (!File.Exists("Tasks.db"))
            {
                BaseCreator.CreateNewBase();
            }
            WorkData data;
            switch (t)
            {
                case StructType.EEmployee: //получение сотрудников
                    data = new WorkEmployees();
                    data.GetData();
                    WorkEmployees a = data as WorkEmployees;
                    OnGotEmployees?.Invoke(a.workers);
                    return;
                case StructType.EProject: //получение проектов
                    data = new WorkProjects();
                    data.GetData();
                    WorkProjects b = data as WorkProjects;
                    OnGotProjects?.Invoke(b.projects);
                    return;
                case StructType.ETask: //получение задач
                    data = new WorkTasks();
                    data.GetData();
                    WorkTasks c = data as WorkTasks;
                    OnGotTasks?.Invoke(c.tasks);
                    return;
            }
        }
        public void Delete(int index, StructType t)
        {
            WorkData data;
            switch (t)
            {
                case StructType.EEmployee:     
                    data = new WorkEmployees();
                    break;                     
                case StructType.ETask:         
                    data = new WorkTasks();    
                    break;                     
                case StructType.EProject:      
                    data = new WorkProjects(); 
                    break;                     
                default:                       
                    return;                    
            }
            data.GetData();    
            data.Delete(index);
            GetData(t);
        }
        public void ChangeData(int row, int col, StructType t, string new_val)
        {
            if (col <= 0) return; //номер нельзя менять
            WorkData data;                             
            switch (t)                                 
            {
                case StructType.EEmployee:             
                    data = new WorkEmployees();        
                    break;                             
                case StructType.ETask:                 
                    data = new WorkTasks();            
                    break;                             
                case StructType.EProject:              
                    data = new WorkProjects();         
                    break;                             
                default:                               
                    return;                            
            }
            data.GetData();
            data.ChangeData(row, col, new_val);
            GetData(t);
        }
        public void AddData(StructType t, string[] values) //добавление записи
        {
            WorkData data;                     
            switch (t)                         
            {
                case StructType.EEmployee:     
                    data = new WorkEmployees();
                    break;                     
                case StructType.ETask:         
                    data = new WorkTasks();    
                    break;                     
                case StructType.EProject:      
                    data = new WorkProjects(); 
                    break;                     
                default:                       
                    return;                    
            }
            data.AddData(values);
            GetData(t);
        }
        public void CreateDoc(StructType t,int ind) //составление отчёта
        {
            WorkData data;
            switch (t)
            {
                case StructType.EProject:
                    data = new WorkProjects();
                    data.GetData();
                    var a=data as WorkProjects;
                    using (StreamWriter w=new StreamWriter("ProjectReport.txt"))
                    {
                        w.WriteLine($"{a.projects[ind].Id}\n" +
                                    $"{a.projects[ind].Name}\n" +
                                    $"Статус: {a.projects[ind].Status}");
                    }
                    return;
                case StructType.ETask:
                    data = new WorkTasks();
                    data.GetData();
                    var b = data as WorkTasks;
                    using (StreamWriter w = new StreamWriter("TaskReport.txt"))
                    {
                        w.WriteLine($"{b.tasks[ind].Id}\n" +
                                    $"{b.tasks[ind].Name}\n" +
                                    $"Прогресс: {b.tasks[ind].fprogress}\n" +
                                    $"Назначена: {b.tasks[ind].Employee_id}");
                    }
                    return;
            }
        }
        public delegate void GotEmployees(List<Employee> Data); 
        public event GotEmployees OnGotEmployees; //получение информации о сотрудниках
        public delegate void GotTasks(List<Task> Data);
        public event GotTasks OnGotTasks; //получение информации о задачах
        public delegate void GotProjects(List<Project> Data);
        public event GotProjects OnGotProjects; //получение информации о проектах
        abstract class WorkData //базовый класс для работы с базой данных
        {
            abstract public void GetData(); //полуение информации
            abstract public void Delete(int index); //удаление записи
            abstract public void ChangeData(int row, int col, string new_val); //изменение информации
            abstract public void AddData(string[] values); // ДОБАВЛЕНО: добавление записи
        }
        class WorkEmployees : WorkData //работа с таблицей с сотрудниками
        {
            public List<Employee> workers = new List<Employee>(); //список сотрудников
            override public void GetData()
            {
                if (workers.Count > 0) workers.Clear();
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = "select * from employees";
                        using (SqliteDataReader r = cmd.ExecuteReader())
                        {
                            if (r.HasRows)
                            {
                                while (r.Read())
                                {
                                    workers.Add(new Employee(Convert.ToInt32(r.GetValue(0).ToString()), r.GetValue(1).ToString()));
                                }
                            }
                        }
                    }
                }
            }
            override public void Delete(int index)
            {
                if (index < 0 || index >= workers.Count) return;                 
                int id = workers[index].Id;  //получение номера                                    
                using (SqliteConnection c = new SqliteConnection(c_str))         
                {
                    c.Open();                                                    
                    using (SqliteCommand cmd = c.CreateCommand())                
                    {
                        cmd.CommandText = $"delete from employees where id={id}";
                        cmd.ExecuteReader();                                     
                    }
                }
            }
            override public void ChangeData(int row, int col, string new_val)
            {
                if (row < 0 || row >= workers.Count) return;            
                int id = workers[row].Id;                               
                string field;                                           
                if (col == 1)                                           
                {
                    field = "name";                                     
                    new_val = "'" + new_val + "'";                      
                }
                else                                                    
                {
                    return;                                             
                }
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();                                           
                    using (SqliteCommand cmd = c.CreateCommand())       
                    {
                        cmd.CommandText = $"update employees set {field}={new_val} where id={id}";
                        cmd.ExecuteReader();
                    }
                }
            }
            override public void AddData(string[] values)  
            {
                if (values == null || values.Length < 1) return; 
                string name = values[0]; 
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand()) 
                    {
                        cmd.CommandText = $"insert into employees values (null,'{name}')";
                        cmd.ExecuteReader();
                    }
                }
            }
        }
        class WorkTasks : WorkData //работа с таблицей с задачами
        {
            public List<Task> tasks = new List<Task>(); //список проектов
            override public void GetData()
            {
                if (tasks.Count > 0) tasks.Clear();
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = "select * from tasks";
                        using (SqliteDataReader r = cmd.ExecuteReader())
                        {
                            if (r.HasRows)
                            {
                                while (r.Read())
                                {
                                    tasks.Add(new Task(Convert.ToInt32(r.GetValue(0).ToString()), r.GetValue(1).ToString(),
                                        Convert.ToInt32(r.GetValue(2).ToString()), Convert.ToByte(r.GetValue(3).ToString())));
                                }
                            }
                        }
                    }
                }
            }
            override public void Delete(int index)
            {
                if (index < 0 || index >= tasks.Count) return;               
                int id = tasks[index].Id;                                    
                using (SqliteConnection c = new SqliteConnection(c_str))     
                {
                    c.Open();                                                
                    using (SqliteCommand cmd = c.CreateCommand())            
                    {
                        cmd.CommandText = $"delete from tasks where id={id}";
                        cmd.ExecuteReader();                                 
                    }
                }
            }
            override public void ChangeData(int row, int col, string new_val)
            {
                if (row < 0 || row >= tasks.Count) return;              
                int id = tasks[row].Id;                                 
                string field;                                  
                switch (col)                                            
                {
                    case 1: //имя                                           
                        field = "name";
                        new_val =$"'{new_val}'";                                
                        break;                                          
                    case 2:                                             
                        field = "employee_id";                          
                        break;                                          
                    case 3:                                             
                        field = "progress";                             
                        break;                                          
                    default:                                            
                        return;                                         
                }     
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();                                           
                    using (SqliteCommand cmd = c.CreateCommand())       
                    {
                        cmd.CommandText = $"update tasks set {field}={new_val} where id={id}";
                        cmd.ExecuteReader();
                    }
                }
            }
            override public void AddData(string[] values)               
            {
                if (values == null || values.Length < 3) return;        
                string name = values[0];                                
                int index = Convert.ToInt32(values[1]);
                int employee_id;
                WorkEmployees e =new WorkEmployees();
                e.GetData();
                if (index<0||index>=e.workers.Count) employee_id = 0; 
                else employee_id = e.workers[index].Id;
                int progress = Convert.ToInt32(values[2]);              
                if (progress < 0) progress = 0;                         
                if (progress > 100) progress = 100;                     
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();                                           
                    using (SqliteCommand cmd = c.CreateCommand())       
                    {
                        cmd.CommandText = $"insert into tasks values (null,'{name}', {employee_id}, {progress})";
                        cmd.ExecuteReader(); 
                    }
                }
            }
        }
        class WorkProjects : WorkData //работа с таблицей с проектами
        {
            public List<Project> projects = new List<Project>(); //список проектов
            override public void GetData()
            {
                if (projects.Count > 0) projects.Clear();
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = "select * from projects";
                        using (SqliteDataReader r = cmd.ExecuteReader())
                        {
                            if (r.HasRows)
                            {
                                while (r.Read())
                                {
                                    projects.Add(new Project(Convert.ToInt32(r.GetValue(0).ToString()), r.GetValue(1).ToString(),
                                        r.GetValue(2).ToString()));
                                }
                            }
                        }
                    }
                }
            }
            override public void Delete(int index)
            {
                if (index >= projects.Count || index < 0) return;
                int id = projects[index].Id; //получение номера
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = $"delete from projects where id={id}";
                        cmd.ExecuteReader();
                    }
                }

            }
            override public void ChangeData(int row, int col, string new_val)
            {
                int id = projects[row].Id;
                string field = ""; //изменяемое поле
                if (col == 1)
                {
                    field = "name";
                    new_val = "'" + new_val + "'";
                }
                else if (col == 2)
                {
                    field = "status";
                    new_val = "'" + new_val + "'";
                }

                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand cmd = c.CreateCommand())
                    {
                        cmd.CommandText = $"update projects set {field}={new_val} where id={id}";
                        cmd.ExecuteReader();
                    }
                }
            }
            override public void AddData(string[] values)
            {
                if (values == null || values.Length < 2) return;        
                string name = values[0];                                
                string status = values[1];                              
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();                                           
                    using (SqliteCommand cmd = c.CreateCommand())       
                    {
                        cmd.CommandText = $"insert into projects values (null,'{name}', '{status}')";
                        cmd.ExecuteReader();
                    }
                }
            }
        }
    }
}