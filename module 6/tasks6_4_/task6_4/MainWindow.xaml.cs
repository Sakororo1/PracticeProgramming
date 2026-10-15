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