namespace task1_1_4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[10]; //создание массива из десяти элементов
            Random r= new Random(); //объект для случайных чисел
            for (byte i=0; i<a.Length; i++) //цикл для заполнения массива
            {
                a[i] = (int) r.NextInt64(-100,101); //присванивание элементу случайного числа
                Console.WriteLine(a[i]); //вывод элемента
            }
            int S = 0; //сумма элементов массива
            foreach(int i in a) //цикл для вычисления суммы элементов массива
            {
                S += i;
            }
            Console.WriteLine($"Сумма элементов массива {S}"); //вывод суммы
        }
    }
}
