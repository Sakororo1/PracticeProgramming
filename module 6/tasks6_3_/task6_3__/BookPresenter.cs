using System;
using System.Collections.Generic;
using System.Windows;
namespace task6_3__
{
    internal class BookPresenter //класс для взаимодействия BookWorker и Form1
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
}
