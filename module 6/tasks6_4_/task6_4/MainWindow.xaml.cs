using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;
namespace task6_4
{
    public class Finance //запись о расходе/доходе
    {
        //конструктор
        public Finance(int id, string name, double sum, bool type)
        {
            fid = id;
            fname = name;
            fsum = sum;
            ftype = type;
        }
        public int fid { get; set; } //номер
        public string fname { get; set; } //название
        public double fsum { get; set; } //сумма
        public bool ftype { get; set; } //тип, false = 0 - трата, true = 1 - доход
        public string ftypeDisplay //отображение типа для combobox
        {
            get
            {
                if (ftype) return "Доход";
                else return "Расход";
            }
        }
    }
    public class WorkFinance : INotifyPropertyChanged //класс для работы с базой данных
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
        public void AddFinance(string name, double sum, bool type) //добавление записи
        {
            if (name == "" || sum < 0) return; //отмена, если данные некорректны
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
                    s.CommandText = $"insert into finance values(null,'{name}',{sum.ToString(CultureInfo.InvariantCulture)},{stype});";
                    s.ExecuteReader();
                }
            }
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
                new_val="'"+new_val+"'";
            }
            else if (col == 2) //изменение суммы
            {
                Regex r = new Regex(@"^d+(?:\.\d{1,2})?$"); //регулярное выражение для определения чисел
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
    public class FinanceViewModel : INotifyPropertyChanged //view model
    {
        static FinanceViewModel Instance;
        WorkFinance w; //объект для работы с базой данных
        private FinanceViewModel()
        {
            w = WorkFinance.GetInstance();
            w.OnError += msg => ErrorMessage = msg;  //при ошибке строка с ошибкой меняется 
            w.GetFinance();
        }
        public static FinanceViewModel GetInstance()
        {
            if (Instance == null)
            {
                Instance = new FinanceViewModel();
            }
            return Instance;
        }
        public ObservableCollection<Finance> Finances
        {
            get { return w.Finances; }
        }
        private string _newName = ""; //название
        public string NewName //свойство для привязки к TextBox
        {
            get { return _newName; }
            set
            {
                _newName = value;
                OnPropertyChanged();
            }
        }
        private string _newSum = ""; //сумма
        public string NewSum  //свойство суммы
        {
            get { return _newSum; }
            set
            {
                _newSum = value;
                OnPropertyChanged();
            }
        }
        private bool _newType; //тип
        public bool NewType //свойство для типа
        {
            get { return _newType; }
            set
            {
                _newType = value;
                OnPropertyChanged();
            }
        }
        private Finance _selectedFinance;  //выбранная запись
        public Finance SelectedFinance  //свойство для выбранной записи
        {
            get { return _selectedFinance; }
            set
            {
                _selectedFinance = value;
                OnPropertyChanged();
            }
        }
        private string _errorMessage; //текст ошибки
        public string ErrorMessage //свойство для текста ошибки
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        public void GetFinance()
        {
            w.GetFinance();
        }
        public void Add()
        {
            if (!double.TryParse(NewSum, NumberStyles.Any, CultureInfo.InvariantCulture, out var sum))
            {
                ErrorMessage = "Неверный формат суммы";
                return;                                 
            }
            w.AddFinance(NewName, sum, NewType);
            w.GetFinance();
            NewName = "";
            NewSum = "";
            NewType = false;
            ErrorMessage = "";
        }
        public void Delete()
        {
            var index = w.Finances.IndexOf(SelectedFinance); //индекс выбранной строки
            if (index < 0) return;
            w.DeleteFinance(index);
            ErrorMessage = "";
        }
        public void SaveEditedRow(int row, int col, string value)
        {
            w.UpdateFinance(col, row, value);
        }
        public void SetType(int row, string displayValue) //сохранить правку из ComboBox-ячейки
        {
            if (row < 0 || row >= w.Finances.Count) return;
            bool new_type = displayValue == "Доход";
            if (w.Finances[row].ftype != new_type)
            {
                w.UpdateFinance(row);
            }
            ErrorMessage = "";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); //сообщаем какое свойство
        }
    }
    public partial class MainWindow : Window     
    {
        private  FinanceViewModel vm;
        public MainWindow()           
        {
            InitializeComponent();   
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            vm = FinanceViewModel.GetInstance(); 
            DataContext = vm; 
        }
        private void Add_Click(object sender, RoutedEventArgs e) 
        {
            vm.Add();
        }
        private void Delete_Click(object sender, RoutedEventArgs e) 
        {
            vm.Delete(); 
        }
        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction != DataGridEditAction.Commit) return; //отмена изменения
            if (e.Row.GetIndex() < 0) return;
            int row = e.Row.GetIndex();    
            int col = e.Column.DisplayIndex;
            var textBox = e.EditingElement as TextBox; //изменение названия или суммы
            if (textBox != null)
            {
                vm.SaveEditedRow(row, col, textBox.Text);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.Width= Width-50;
            dataGrid.Columns[0].Width=dataGrid.Width/8;
            dataGrid.Columns[1].Width = dataGrid.Width / 3;
            dataGrid.Columns[2].Width = dataGrid.Width / 3;
            dataGrid.Columns[3].Width = dataGrid.Width / 6;
        }
        private void TypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;                       
            var finance = combo?.DataContext as Finance;          
            if (finance == null) return;                          
            int row = vm.Finances.IndexOf(finance);               
            if (row < 0) return;                                  
            vm.SetType(row, combo.SelectedItem.ToString());   //изменение типа     
        }
    }
}