namespace task2_2_2
{
    class Program
    {      
        class Library //библиотека
        {
            private List<Book> fBooks = new List<Book>(); //список книг

            public void AddBook(string author, string title, uint year) //добавление книги
            {
                fBooks.Add(new Book(author,title,year));
            }
            public bool RemoveBook(string title) //удаление книги по названию
            {
                for (int i = 0; i < fBooks.Count; i++)
                {
                    if (fBooks[i].GetTitle() == title)
                    {
                        fBooks.RemoveAt(i);
                        return true;
                    }
                }
                Console.WriteLine($"Книга \"{title}\" не найдена");
                return false;
            }
            public void FindByAuthor(string author) //поиск книг по автору
            {
                bool found = false;
                foreach (Book book in fBooks)
                {
                    if (book.GetAuthor() == author)
                    {
                        Console.WriteLine($"{book}");
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Ничего не найдено");
                }
            }
            public void FindByYear(uint year) //поиск книг по году издания
            {
                bool found = false;
                foreach (Book book in fBooks)
                {
                    if (book.GetYear() == year)
                    {
                        Console.WriteLine($"{book}");
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Ничего не найдено");
                }
            }
            public void SortByAuthor() //сортировка по автору
            {
                for (int i = 0; i < fBooks.Count - 1; i++)
                {
                    for (int j = 0; j < fBooks.Count - 1 - i; j++)
                    {
                        if (string.Compare(fBooks[j].GetAuthor(), fBooks[j + 1].GetAuthor()) > 0)
                        {
                            Book temp = fBooks[j];
                            fBooks[j] = fBooks[j + 1];
                            fBooks[j + 1] = temp;
                        }
                    }
                }
            }
            public void SortByTitle() //сортировка по названию
            {
                for (int i = 0; i < fBooks.Count - 1; i++)
                {
                    for (int j = 0; j < fBooks.Count - 1 - i; j++)
                    {
                        if (string.Compare(fBooks[j].GetTitle(), fBooks[j + 1].GetTitle()) > 0)
                        {
                            Book temp = fBooks[j];
                            fBooks[j] = fBooks[j + 1];
                            fBooks[j + 1] = temp;
                        }
                    }
                }
            }
            public void SortByYear() //сортировка по году издания
            {
                for (int i = 0; i < fBooks.Count - 1; i++)
                {
                    for (int j = 0; j < fBooks.Count - 1 - i; j++)
                    {
                        if (fBooks[j].GetYear() > fBooks[j + 1].GetYear())
                        {
                            Book buf = fBooks[j];
                            fBooks[j] = fBooks[j + 1];
                            fBooks[j + 1] = buf;
                        }
                    }
                }
            }
            public void PrintAll() //вывод всех книг
            {
                Console.WriteLine($"В библиотеке {fBooks.Count} книг:");
                foreach (Book book in fBooks)
                {
                    Console.WriteLine($"{book}");
                }
            }
            class Book //класс книга
            {
                private string fAuthor; //автор
                private string fTitle; //название
                private uint fYear; //год издания

                public Book(string author, string title, uint year) //конструктор
                {
                    fAuthor = author;
                    fTitle = title;
                    fYear = year;
                }
                public string GetAuthor() //получение автора
                {
                    return fAuthor;
                }
                public string GetTitle() //получение названия
                {
                    return fTitle;
                }
                public uint GetYear() //получение года издания
                {
                    return fYear;
                }
                public override string ToString() //текстовое представление книги
                {
                    return $"\"{fTitle}\", {fAuthor}, {fYear}";
                }
            }
        }
        static void Main(string[] args)
        {
            Library library = new Library(); //создание библиотеки
            //добавление книг
            library.AddBook("Пушкин А.С.", "Евгений Онегин", 1833);
            library.AddBook("Толстой Л.Н.", "Война и мир", 1869);
            library.AddBook("Достоевский Ф.М.", "Преступление и наказание", 1866);
            library.AddBook("Булгаков М.А.", "Мастер и Маргарита", 1967);
            library.AddBook("Пушкин А.С.", "Руслан и Людмила", 1820);
            Console.WriteLine("Вывод всех книг");
            library.PrintAll();
            Console.WriteLine("Поиск по автору (Пушкин) и году (1866): ");
            library.FindByAuthor("Пушкин А.С."); //поиск по автору
            library.FindByYear(1866); //поиск по году
            //удаление книги
            Console.WriteLine("Удаление книги \"Война и мир\"");
            library.RemoveBook("Война и мир");
            library.PrintAll();
            //сортировки
            Console.WriteLine("Сортировка по названию");
            library.SortByTitle();
            library.PrintAll();
            Console.WriteLine("Сортировка по году");
            library.SortByYear();
            library.PrintAll();
            Console.WriteLine("Сортировка по автору");
            library.SortByAuthor();
            library.PrintAll();
        }
    }
}