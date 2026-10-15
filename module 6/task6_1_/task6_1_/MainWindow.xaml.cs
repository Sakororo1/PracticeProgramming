using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;
namespace task6_1_
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        class SQLiteWorkers //класс для работы с базой данных
        {
            const string c_str = "Data Source=D:\\sqlite-tools-win-x64-3530400\\workers.db";
            static private SQLiteWorkers Instance;
            private MainWindow form;
            private List<Worker> workers = new List<Worker>();
            struct Worker //тип данных сотрудник
            {
                public object fEmployee_number;
                public object fEmployee_name;
                public object fJob_title;
            }
            private SQLiteWorkers(MainWindow f)
            {
                form = f;
            }
            public static SQLiteWorkers GetInstance(MainWindow f)
            {
                if (Instance == null)
                {
                    Instance = new SQLiteWorkers(f);
                }
                return Instance;
            }
            private string GetCell(int row, int col) //получение значения ячейки
            {
                if (row < 0)
                {
                    return "";
                }
                if (row >= form.dataGrid.Items.Count)
                {
                    return "";
                }
                string[] arr = form.dataGrid.Items[row] as string[];
                if (arr == null)
                {
                    return "";
                }
                return arr[col];
            }
            public void FillTable() //заполнение таблицы
            {
                GetTable(); //получение информации
                //временная отвязка метода
                form.dataGrid.CellEditEnding -= form.dataGrid_CellEditEnding;
                List<string[]> rows = new List<string[]>();
                for (int i = 0; i < workers.Count; i++)
                {
                    rows.Add(new string[3]); //добавление строки
                    rows[i][0] = workers[i].fEmployee_number.ToString(); //заполнение значениями
                    rows[i][1] = workers[i].fEmployee_name.ToString();
                    rows[i][2] = workers[i].fJob_title.ToString();
                }
                form.dataGrid.ItemsSource = rows;
                //возвращение метода
                form.dataGrid.CellEditEnding += form.dataGrid_CellEditEnding;
            }
            private void GetTable() //получение информации из базы данных
            {
                if (workers.Count > 0)
                {
                    workers.Clear();
                }
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand s = c.CreateCommand())
                    {
                        s.CommandText = "SELECT * FROM workers";
                        using (SqliteDataReader r = s.ExecuteReader())
                        {
                            if (r.HasRows) //проверка, что есть данные
                            {
                                while (r.Read()) //построчное считывание данных
                                {
                                    Worker new_worker;
                                    new_worker.fEmployee_number = r.GetValue(0); //получение данных
                                    new_worker.fEmployee_name = r.GetValue(1);
                                    new_worker.fJob_title = r.GetValue(2);
                                    workers.Add(new_worker); //добавление данных в список
                                }
                            }
                        }
                    }
                }
            }
            public void AddWorker(string name, string job_title) //добавление сотрудника
            {
                if (name == "" || job_title == "") //проверка, что строки не пустые
                {
                    return;
                }
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand s = c.CreateCommand())
                    {
                        //определение номера, который нужно добавить
                        int lastRow = form.dataGrid.Items.Count - 1;
                        string num = GetCell(lastRow, 0);
                        s.CommandText = $"insert into workers values({Convert.ToInt32(num) + 1},'{name}','{job_title}');";
                        s.ExecuteReader();
                    }
                }
                FillTable();
            }
            public void RemoveWorker(string id) //удаление сотрудника
            {
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    SqliteCommand s = c.CreateCommand();
                    s.CommandText = $"delete from workers where employee_number={id};";
                    s.ExecuteReader();
                }
                FillTable();
            }
            public void EditWorker(int row, int col, string newValue)
            {
                if (col == 0) //менять табельный номер нельзя
                {
                    return;
                }
                string field = "";
                if (col == 1) //изменение имени
                {
                    field = "name";
                }
                else if (col == 2) //изменение должности
                {
                    field = "job_title";
                }
                else
                {
                    return;
                }
                string num = GetCell(row, 0); //номер сотрудника, у которого нужно изменить значение
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand s = c.CreateCommand())
                    {
                        s.CommandText = $"update workers set {field}='{newValue}' where employee_number={num};";
                        s.ExecuteReader();
                    }
                }
            }
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
        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
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