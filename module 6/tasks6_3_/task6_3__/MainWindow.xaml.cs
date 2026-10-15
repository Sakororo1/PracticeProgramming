using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Data.Sqlite;
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
    public class BookWorker //класс для работы с книгами
    {
        private static BookWorker Instance;
        public static BookWorker GetInstance()
        {
            if (Instance == null) Instance = new BookWorker();
            return Instance;
        }
        private BookWorker() { }
        //строка для подключения
        const string c_str = "Data Source=D:\\sqlite-tools-win-x64-3530400\\books.db";
        List<Book> books = new List<Book>(); //список книг
        List<RBook> rent_books = new List<RBook>(); //список аренды книг
        public delegate void BooksInfo(List<Book> books); //делегат
        public event BooksInfo OnGetBooks; //получение книг
        public event BooksInfo OnGetSearchBooks; //получение книг по поиску
        public delegate void RentInfo(List<RBook> rBooks); //делегат
        public event RentInfo OnGetRent; //получение аренды книг
        public delegate void Error(string message); //делегат для сообщений об ошибке
        public event Error OnGetError; //событие для передачи сообщения об ошибке
        public delegate void GetBook(int ind); //делегат для передачи одной книги
        public event GetBook OnGetBook; //событие для передачи книги
        public void GetBooks() //получение списка книг
        {
            if (books.Count > 0)
            {
                books.Clear();
            }
            using (SqliteConnection c = new SqliteConnection(c_str))
            {
                c.Open();
                using (SqliteCommand s = new SqliteCommand("select * from books", c))
                {
                    using (SqliteDataReader r = s.ExecuteReader())
                    {
                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                Book b = new Book();
                                b.id = r.GetValue(0).ToString();
                                b.author = r.GetValue(1).ToString();
                                b.title = r.GetValue(2).ToString();
                                b.count = r.GetValue(3).ToString();
                                books.Add(b); //добавление экземпляра в список
                            }
                        }
                    }
                }
            }
            OnGetBooks?.Invoke(books); //вызов события о получении списка книг
        }
        public void GetRent() //получение аренды
        {
            if (rent_books.Count > 0)
            {
                rent_books.Clear();
            }
            using (SqliteConnection c = new SqliteConnection(c_str))
            {
                c.Open();
                using (SqliteCommand s = new SqliteCommand("select * from rent_books", c))
                {
                    using (SqliteDataReader r = s.ExecuteReader())
                    {
                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                RBook rb = new RBook();
                                rb.id = r.GetValue(0).ToString();
                                rb.reader_name = r.GetValue(1).ToString();
                                rb.book_id = r.GetValue(2).ToString();
                                rent_books.Add(rb); //добавление экземпляра в список
                            }
                        }
                    }
                }
            }
            OnGetRent?.Invoke(rent_books); //вызов события о получении списка аренды книг
        }
        public void RentBook(int ind, string name) //аренда книги
        {
            if (ind < 0 || name == "" || name == null) { return; }
            int id = Convert.ToInt32(books[ind].id.ToString()); //получение номера книги
            int new_count = Convert.ToInt32(books[ind].count.ToString()); //получение количества книг
            new_count--; //уменьшение количества
            if (new_count < 0) //проверка, что книг нет
            {
                OnGetError?.Invoke("Нет книг для аренды"); //вызов сообщения
                return;
            }
            using (SqliteConnection c = new SqliteConnection(c_str))
            {
                c.Open();
                using (SqliteCommand s = c.CreateCommand())
                {
                    s.CommandText = $"update books set count={new_count} where id={id};";
                    s.ExecuteReader(); //изменение количества книг
                }
                using (SqliteCommand s = c.CreateCommand())
                {
                    s.CommandText = $"insert into rent_books values(null,'{name}',{id});";
                    s.ExecuteReader(); //добавление аренды
                }
            }
            GetBooks();
            GetRent(); //заново получение списка аренды
        }
        public void SearchBook(string search) //поиск книги
        {
            if (search == "")
            {
                GetBooks();
                return;
            }
            List<Book> search_result = new List<Book>(); //список книг по поиску
            for (int i = 0; i < books.Count; i++) //сначала поиск по названию книги
            {
                if (books[i].title.ToString().ToLower().Contains(search.ToLower()))
                {
                    search_result.Add(books[i]);
                }
            }
            for (int i = 0; i < books.Count; i++) //поиск по автору
            {
                if (books[i].author.ToString().ToLower().Contains(search.ToLower()))
                {
                    if (!search_result.Contains(books[i])) //проверка, что книги нет в списке
                    {
                        search_result.Add(books[i]);
                    }
                }
            }
            OnGetSearchBooks?.Invoke(search_result); //вызов события о получении списка книг по поиску
        }
        public void ReturnBook(int index) //возвращение книги
        {
            if (index < 0) { return; }
            int id = Convert.ToInt32(rent_books[index].id.ToString()); //получение номера аренды книги
            int book_id = Convert.ToInt32(rent_books[index].book_id.ToString()); //получение номера книги
            //получение количества книг, на которое нужно поменять
            int new_count = 0;
            for (int i = 0; books.Count > i; i++)
            {
                if (Convert.ToInt32(books[i].id) == book_id)
                {
                    new_count = Convert.ToInt32(books[i].count); //определение количества
                    break;
                }
            }
            new_count++; //увеличение количества
            using (SqliteConnection c = new SqliteConnection(c_str))
            {
                c.Open();
                using (SqliteCommand s = c.CreateCommand()) //удаление аренды
                {
                    s.CommandText = $"delete from rent_books where id={id};";
                    s.ExecuteReader();
                }
                using (SqliteCommand s = c.CreateCommand()) //изменение количества книг
                {
                    s.CommandText = $"update books set count={new_count} where id={book_id};";
                    s.ExecuteReader();
                }
            }
            GetRent(); //обновление аренды и книг
            GetBooks();
        }
        public void GetBookFromRent(int id) //получение книги по номеру через таблицу с арендой
        {
            if (books.Count < 0 || id > books.Count - 1 || id < 0) return;
            int ind = 0; //индекс по списку книги, который нужно выделить
            if (id >= books.Count / 2) //поиск во второй половине
            {
                for (int i = books.Count / 2; i < books.Count; i++)
                {
                    if (Convert.ToInt32(books[i].id) == id)
                    {
                        ind = i; break;
                    }
                }
            }
            else if (id < books.Count / 2) //поиск в первой половине
            {
                for (int i = 0; i < books.Count / 2 + 1; i++)
                {
                    if (Convert.ToInt32(books[i].id) == id)
                    {
                        ind = i; break;
                    }
                }
            }
            OnGetBook?.Invoke(ind); //вызов события для передачи книги
        }
    }
    public class BookPresenter //класс для взаимодействия BookWorker и Form1
    {
        private MainWindow mw; //ссылка на главное окно
        private Window1 w1; //ссылка на окно 1
        private BookWorker w; //ссылка на объект для работы с базой данных
        private static BookPresenter Instance;
        public static BookPresenter GetInstance(MainWindow window)
        {
            if (Instance == null) Instance = new BookPresenter(window);
            return Instance;
        }
        private BookPresenter(MainWindow window)
        {
            mw = window;
            w1 = mw.w; //ссылка на второе окно
            w = BookWorker.GetInstance();
            w.OnGetBooks += ShowBooks; //привязка обработчиков событий
            w.OnGetRent += ShowRent;
            w.OnGetSearchBooks += ShowSearchBooks;
            w.OnGetError += ShowError;
            w.OnGetBook += ShowBook;
        }
        public void GetBooks() //получение списка книг
        {
            w.GetBooks();
        }
        public void GetRent() //полуение списка аренды
        {
            w.GetRent();
        }
        public void RentBook() //аренда книги
        {
            w.RentBook(mw.dataGrid_books.SelectedIndex, mw.Renter_Name.Text);
        }
        public void SearchBook() //поиск книги
        {
            w.SearchBook(mw.TextBox_Search.Text);
        }
        public void ReturnBook() //вернуть книгу
        {
            w.ReturnBook(w1.DataGrid_Rent.SelectedIndex);
        }
        public void GetBookFromRent() //получение книги из таблицы с арендой
        {
            var a = w1.DataGrid_Rent.ItemsSource as List<string[]>; //получение информации из таблицы
            //передача информации из столбца с номером книги
            w.GetBookFromRent(Convert.ToInt32(a[w1.DataGrid_Rent.SelectedIndex][2]));
        }
        public void ShowBooks(List<Book> books) //вывод книг
        {
            List<string[]> s = new List<string[]>();
            for (int i = 0; i < books.Count; i++)
            {
                s.Add(new string[4]);
                s[i][0] = books[i].id;
                s[i][1] = books[i].author;
                s[i][2] = books[i].title;
                s[i][3] = books[i].count;
            }
            mw.dataGrid_books.ItemsSource = s;

        }
        public void ShowSearchBooks(List<Book> books) //вывод найденных книг
        {
            List<string[]> s = new List<string[]>();
            for (int i = 0; i < books.Count; i++) //составление списка массивов строк
            {
                s.Add(new string[4]);
                s[i][0] = books[i].id;
                s[i][1] = books[i].author;
                s[i][2] = books[i].title;
                s[i][3] = books[i].count;
            }
            mw.dataGrid_books.ItemsSource = s;
        }
        public void ShowRent(List<RBook> rBooks) //вывод таблицы с арендой
        {
            List<string[]> s = new List<string[]>();
            for (int i = 0; i < rBooks.Count; i++) //составление списка массивов строк
            {
                s.Add(new string[3]);
                s[i][0] = rBooks[i].id;
                s[i][1] = rBooks[i].reader_name;
                s[i][2] = rBooks[i].book_id;
            }
            w1.DataGrid_Rent.ItemsSource = s;
            mw.Renter_Name.Clear();
        }
        public void ShowError(string message) //вывод сообщения об ошибке
        {
            MessageBox.Show(message); //вывод сообщения
        }
        public void ShowBook(int ind) //вывод книги
        {
            w1.Hide();
            mw.Show();
            mw.dataGrid_books.SelectedItem = mw.dataGrid_books.Items[Convert.ToInt32(ind)];
        }
    }
    public partial class MainWindow : Window
    {
        public BookPresenter b;
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
        private void Window_Activated(object sender, EventArgs e)
        {
            //if (b != null) b.GetBooks();
        }
    }
}