using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
namespace task6_3__
{
    internal class BookWorker //класс для работы с книгами
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
}
