using System;
using System.Windows;
using System.Windows.Input;
namespace task6_3__
{
    public partial class Window1 : Window
    {
        BookPresenter b;
        MainWindow mw;
        public Window1(MainWindow mwi)
        {
            InitializeComponent();
            mw = mwi;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            b = mw.b;
            DataGrid_Rent.Width = this.Width - 50;
            DataGrid_Rent.Columns[0].Width = DataGrid_Rent.Width / 4;
            DataGrid_Rent.Columns[1].Width = DataGrid_Rent.Width / 3;
            DataGrid_Rent.Columns[2].Width = DataGrid_Rent.Width / 4;
        }
        //возвращение к таблице с книгами
        private void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            mw.Show();
            b.GetBooks();
            this.Hide();
        }
        private void Button_ReturnRent_Click(object sender, RoutedEventArgs e)
        {
            b.ReturnBook();
        }
        private void DataGrid_Rent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            b.GetBookFromRent();
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            if (b!=null) b.GetRent();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown(); //закрытие программы
        }
    }
}
