using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace task6_4
{
    internal class FinanceViewModel : INotifyPropertyChanged //view model
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
            w.AddFinance(NewName, NewSum, NewType);
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
}
