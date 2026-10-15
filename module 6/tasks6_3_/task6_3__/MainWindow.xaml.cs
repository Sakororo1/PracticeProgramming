using System;
using System.Windows;
namespace task6_3__
{
    public struct Book //структура книги
    {
        public string id; //номер
        public string author; //автор
        public string title; //название
        public string count; //количество книг
    }
    public struct RBook //структура аренда книг
    {
        public string id; //номер
        public string reader_name; //имя читателя
        public string book_id; //номер книги
    }   
    public partial class MainWindow : Window
    {
        internal BookPresenter b;
        public Window1 w;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox_Search.TextChanged -= TextBox_Search_TextChanged;
            w = new Window1(this);
            b = BookPresenter.GetInstance(this); //инициализация объекта
            dataGrid_books.Width = this.Width - 50;
            dataGrid_books.Columns[0].Width = dataGrid_books.Width / 8;
            dataGrid_books.Columns[1].Width = dataGrid_books.Width / 3;
            dataGrid_books.Columns[2].Width = dataGrid_books.Width / 3;
            dataGrid_books.Columns[3].Width = dataGrid_books.Width / 8;
            b.GetBooks();
            b.GetRent();
            TextBox_Search.TextChanged += TextBox_Search_TextChanged;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dataGrid_books.Width = this.Width - 50;
            dataGrid_books.Columns[0].Width = dataGrid_books.Width / 8;
            dataGrid_books.Columns[1].Width = dataGrid_books.Width / 3;
            dataGrid_books.Columns[2].Width = dataGrid_books.Width / 3;
            dataGrid_books.Columns[3].Width = dataGrid_books.Width / 8;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            w.Activate();
            w.Show();
            b.GetRent();
            this.Hide();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown(); //закрытие программы
        }
        private void Button_Rent_Click(object sender, RoutedEventArgs e)
        {
            b.RentBook();
        }
        private void TextBox_Search_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            b.SearchBook();
        }
    }
}