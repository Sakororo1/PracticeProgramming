namespace task1_3_2
{
    class Program
    {
        static int MinLen(int num)
        {
            if (num > 9) 
            {
                return num / 9;
            }
            else { return 1; }
        }
        static void Main(string[] args)
        {
            Console.Write("Введите положительное целое число: ");
            int val=Convert.ToInt32(Console.ReadLine()); //ввод числа
            //инициализация массива размером, равным результату работы метода
            int[] a = new int[MinLen(val)];
            Random r=new Random();
            for (int i = 0; i < a.Length; i++) //заполнение массива случайными элементами
            {
                a[i] = (int)r.NextInt64(1, 10);
                while ((a.Sum()>val)) //проверка, что сумма элементов массива больше ограничения
                {
                    a[i] = (int)r.NextInt64(1, 10); //присвоению элементу массива другого значения
                }
                Console.WriteLine(a[i]);
            }
            Console.WriteLine($"Сумма: {a.Sum()}"); //вывод суммы
        }
    }
}
