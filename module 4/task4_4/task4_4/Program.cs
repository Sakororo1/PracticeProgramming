namespace task4_4
{
    class Program
    {
        interface IBook //интерфейс книга
        {
            public bool IsAvailable(); //определение доступности книги
            public void GiveBook(); //выдача книги
        }
        class MasterAndMargaret : IBook //Книга "Мастер и Маргарита
        {
            ushort Count; //количество книг
            static MasterAndMargaret Instance;
            private MasterAndMargaret(ushort count) //конструктор
            {
                Count=count;
            }
            public static MasterAndMargaret GetInstance(ushort count)
            {
                if (Instance==null)
                {
                    Instance=new MasterAndMargaret(count);
                }
                return Instance;
            }
            public bool IsAvailable() //определение доступности книги
            {
                if (Count > 0) //проверка, что книги есть
                {
                    Console.WriteLine($"Книга \"Мастер и Маргарита\" доступна ({Count} книг)");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Книга \"Мастер и Маргарита\" недоступна ({Count} книг)");
                    return false; 
                }
            }
            public void GiveBook() 
            {
                if (IsAvailable()) //проверка, что книга доступна
                {
                    Console.WriteLine("Выдана книга \"Мастер и Маргарита\"");
                    Count--; //уменьшение количества на один
                }
            }
        }
        class CrimeAndPunishment : IBook //Книга "Преступление и наказание"
        {
            ushort Count; //количество книг

            static CrimeAndPunishment Instance;
            private CrimeAndPunishment(ushort count) //конструктор
            {
                Count = count;
            }
            public static CrimeAndPunishment GetInstance(ushort count)
            {
                if (Instance == null)
                {
                    Instance = new CrimeAndPunishment(count);
                }
                return Instance;
            }
            public bool IsAvailable() //определение доступности книги
            {
                if (Count > 0)
                {
                    Console.WriteLine($"Книга \"Преступление и наказание\" доступна ({Count} книг)");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Книга \"Преступление и наказание\" недоступна ({Count} книг)");
                    return false;
                }
            }
            public void GiveBook()
            {
                if (IsAvailable()) //проверка, что книга доступна
                {
                    Console.WriteLine("Выдана книга \"Преступление и наказание\"");
                    Count--; //уменьшение количества на один
                }

            }
        }
        class WarAndPeace : IBook //Книга "Война и мир"
        {
            ushort Count; //количество книг

            static WarAndPeace Instance;
            private WarAndPeace(ushort count) //конструктор
            {
                Count = count;
            }
            public static WarAndPeace GetInstance(ushort count)
            {
                if (Instance == null)
                {
                    Instance = new WarAndPeace(count);
                }
                return Instance;
            }
            public bool IsAvailable() //определение доступности книги
            {
                if (Count > 0) //проверка, что книга есть
                {
                    Console.WriteLine($"Книга \"Преступление и наказание\" доступна ({Count} книг)");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Книга \"Преступление и наказание\" недоступна ({Count} книг)");
                    return false;
                }
            }
            public void GiveBook()
            {
                if (IsAvailable()) //проверка, что книга доступна
                {
                    Console.WriteLine("Выдана книга \"Преступление и наказание\"");
                    Count--; //уменьшение количества на один
                }

            }
        }
        static void Main(string[] args)
        {
            Random r= new Random();
            IBook MM = MasterAndMargaret.GetInstance((ushort)r.Next(1,20));
            IBook C = CrimeAndPunishment.GetInstance((ushort)r.Next(1, 20));
            IBook WP = WarAndPeace.GetInstance((ushort)r.Next(1,20));
            MM.GiveBook(); //выдача каждой книги
            C.GiveBook();
            WP.GiveBook();
            Console.WriteLine("Выдача книги, пока она не станет недоступна:\n");
            do
            {
                MM.GiveBook();
            } while (MM.IsAvailable());
        }
    }
}
