namespace task1_1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите первую строку: ");
            string s1 = Console.ReadLine(); //ввод первой строки
            Console.Write("Введите вторую строку: "); 
            string s2= Console.ReadLine(); //ввод второй строки
            if (s1.Contains(s2)) //проверка, что первая строка содержит вторую
            {
                //результат, если содержит
                Console.WriteLine("Вторая строка является подстрокой первой строки");
            }
            else
            {
                //результат, если не содержит
                Console.WriteLine("Вторая строка не является подстрокой первой строки");
            }
        }
    }
}
