namespace task2_1_3
{
    class Program
    {
        class Author //класс автор
        {
            public string Name = ""; //имя
            public ushort Year = 0; //год
            private List<Book> Books; //список книг
            //конструктор класса автор
            public Author(string name, ushort year)
            {
                Name = name;
                Year = year;
                Books = new List<Book>(); //инициализация списка книг
            }
            public void AddBook(string title, ushort publishYear) //добавление книги
            {
                Books.Add(new Book(title, this, publishYear)); //добавление книги в список
            }
            //получение книги по индексу
            public string GetBookTitle(int ind)
            {
                if (ind >= Books.Count)
                {
                    return "Нет книги с таким номером";
                }
                return $"{Books[ind].Title}";
            }
            //получение года выпуска книги
            public string GetBookPublishYear(int ind)
            {
                if (ind >= Books.Count)
                {
                    return "Нет книги с таким номером";
                }
                return $"{Books[ind].PublishYear}";
            }
            //получение имени автора книги
            public string GetBookAuthorName(int ind)
            {
                if (ind >= Books.Count)
                {
                    return "Нет книги с таким номером";
                }
                return $"{Books[ind].Name}";
            }
            private class Book //класс книга
            {
                public string Title = ""; //название книги
                public string Name = ""; //имя автора
                public ushort PublishYear = 0; //год выпуска
                //два варианта конструктора
                //конструктор, получающий имя автора по объекту класса автора
                public Book(string title, Author author, ushort publishYear)
                {
                    Title = title;
                    Name = author.Name;
                    PublishYear = publishYear;
                }
            }
        }
        static void Main(string[] args)
        {
            //создание объекта класса автор
            Author Dost = new Author("Достоевский Фёдор Михайлович", 1821);
            //создание объекта класса книга
            Dost.AddBook("Преступление и наказание", 1866); //добавление книги
            Console.WriteLine("Информация об авторе:");
            Console.WriteLine($"Имя: {Dost.Name}, год рождения: {Dost.Year}");
            Console.WriteLine("Информация о книге:");
            Console.WriteLine($"Название: {Dost.GetBookTitle(0)}, имя автора: {Dost.GetBookAuthorName(0)}, " +
                             $"год выпуска: {Dost.GetBookPublishYear(0)}");
        }
    }
}
