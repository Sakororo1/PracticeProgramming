using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;
namespace task6_4
{
    internal class WorkFinance : INotifyPropertyChanged //класс для работы с базой данных
    {
        private static WorkFinance Instance;
        private WorkFinance()
        {
            finances = new ObservableCollection<Finance>();
        }
        public static WorkFinance GetInstance()
        {
            if (Instance == null)
            {
                Instance = new WorkFinance();
            }
            return Instance;
        }
        //строка подключения
        private const string c_str = "Data Source=D:\\sqlite-tools-win-x64-3530400\\finance.db";
        private ObservableCollection<Finance> finances; //список затрат/доходов
        public ObservableCollection<Finance> Finances //свойство для списка затрат и расходов
        {
            get { return finances; }
            set
            {
                finances = value;
                OnPropertyChanged(nameof(Finances));
            }
        }
        public void GetFinance() //получение информации о затратах и доходах
        {
            if (Finances.Count > 0)
            {
                Finances.Clear();
            }
            using (SqliteConnection c = new SqliteConnection(c_str))
            {
                c.Open();
                using (SqliteCommand s = c.CreateCommand())
                {
                    s.CommandText = "SELECT * FROM finance;";
                    using (SqliteDataReader r = s.ExecuteReader())
                    {
                        if (r.HasRows) //проверка, что есть данные
                        {
                            while (r.Read())
                            {
                                Finances.Add(new Finance(Convert.ToInt32(r.GetValue(0).ToString()),
                                    r.GetValue(1).ToString(),
                                    Convert.ToDouble(r.GetValue(2).ToString()),
                                    (r.GetValue(3).ToString() == "1")));
                            }
                        }
                    }
                }
            }
        }
        public void AddFinance(string name, string sum, bool type) //добавление записи
        {
            Regex r = new Regex(@"^\d+(?:\.\d{1,2})?$"); //регулярное выражение для определения чисел
            if (!r.IsMatch(sum)) //проверка, что введённое значение является вещественным числом
            {
                OnError?.Invoke("Неверный формат"); //вызов ошибки
                GetFinance();
                return;
            }
            if (name == "" || Convert.ToDouble(sum) < 0) return; //отмена, если данные некорректны
            if (finances.Count == 0) //проверка, что записей нет
            {
                //сброс счётчика автоинкремента
                using (SqliteConnection c = new SqliteConnection(c_str))
                {
                    c.Open();
                    using (SqliteCommand s = c.CreateCommand())
                    {
                        s.CommandText = "update sqlite_sequence set seq = -1 where name='finance';";
                        s.ExecuteReader();
                    }
                }
            }
            string stype = "0"; //по умолчанию расход
            if (type)
            {
                stype = "1"; //доход
            }
            //добавление записи
            using (SqliteConnection c = new SqliteConnection(c_str))
            {
                c.Open();
                using (SqliteCommand s = c.CreateCommand())
                {
                    s.CommandText = $"insert into finance values(null,'{name}',{sum},{stype});";
                    s.ExecuteReader();
                }
            }
            GetFinance();
        }
        public void DeleteFinance(int index) //удаление записи
        {
            if (finances.Count == 0 || index >= finances.Count) { return; }
            int id = finances[index].fid; //определение номера записи
            //удаление записи
            using (SqliteConnection c = new SqliteConnection(c_str))
            {
                c.Open();
                using (SqliteCommand s = c.CreateCommand())
                {
                    s.CommandText = $"delete from finance where id={id};";
                    s.ExecuteReader();
                }
            }
            GetFinance();
        }
        public void UpdateFinance(int col, int row, string new_val) //изменение названия или значения
        {
            if (col == 0) { return; } //нельзя изменять номер
            string field; //изменяемое поле
            if (col == 1) //изменение имени
            {
                field = "name";
                new_val = "'" + new_val + "'";
            }
            else if (col == 2) //изменение суммы
            {
                Regex r = new Regex(@"^\d+(?:\.\d{1,2})?$"); //регулярное выражение для определения чисел
                if (!r.IsMatch(new_val)) //проверка, что введённое значение является вещественным числом
                {
                    OnError?.Invoke("Неверный формат"); //вызов ошибки
                    GetFinance();
                    return;
                }
                field = "sum";
            }
            else
            {
                return;
            }
            int id = finances[row].fid; //определение номера записи
            //изменение значения
            using (SqliteConnection c = new SqliteConnection(c_str))
            {
                c.Open();
                using (SqliteCommand s = c.CreateCommand())
                {
                    s.CommandText = $"update finance set {field}={new_val} where id={id};";
                    s.ExecuteReader();
                }
            }
            GetFinance();
        }
        public void UpdateFinance(int row) //изменение типа
        {
            int id = finances[row].fid; //определение номера записи
            int new_value = 1; //изначально доход
            if (finances[row].ftype) new_value = 0; //расход
            using (SqliteConnection c = new SqliteConnection(c_str))
            {
                c.Open();
                using (SqliteCommand s = c.CreateCommand())
                {
                    s.CommandText = $"update finance set type={new_value} where id={id}";
                    s.ExecuteReader();
                }
            }
            GetFinance();
        }
        public delegate void Error(string Message); //делегат для события ошибки
        public event Error OnError; //событие для передачи ошибки
        public event PropertyChangedEventHandler PropertyChanged; //событие изменения свойства
        public void OnPropertyChanged([CallerMemberName] string prop = "") //обработчик события
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
