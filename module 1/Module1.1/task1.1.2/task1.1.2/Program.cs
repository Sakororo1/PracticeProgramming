namespace task1_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вы можете ввести три числа, чтобы узнать их среднее арифметическое.");
            Console.Write("Введите число 1: ");
            double x1=Convert.ToDouble(Console.ReadLine()); //ввод числа 1
            Console.Write("Введите число 2: ");
            double x2 = Convert.ToDouble(Console.ReadLine()); //ввод числа 2
            Console.Write("Введите число 3: ");
            double x3 = Convert.ToDouble(Console.ReadLine()); //ввод числа 3
            //вывод среднего арифметического
            Console.WriteLine($"Среднее арифметическое трёх введённых чисел: {(x1+x2+x3)/3}");
        }
    }
}
