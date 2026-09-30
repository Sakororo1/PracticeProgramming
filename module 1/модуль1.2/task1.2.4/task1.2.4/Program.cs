namespace task1_2_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите целое положительное число K (размер массива): ");
            int K=Convert.ToInt32(Console.ReadLine()); //ввод K
            Console.Write("Введите целое число A: ");
            int A = Convert.ToInt32(Console.ReadLine()); //ввод A
            Console.Write("Введите целое число B: ");
            int B = Convert.ToInt32(Console.ReadLine()); //ввод B
            if (B < A) //Перестановка A и B, если B меньше A
            {
                int buf = A;
                A = B;
                B = A;
            }
            int[] m= new int[K];
            Random r= new Random();
            Console.WriteLine("Массив:");
            for (int i = 0; i < m.Length; i++) //заполнение массива случайными элементами
            {
                m[i] = (int)r.NextInt64(A,B); //присвоение элементу случайного значения
                Console.WriteLine(m[i]); //вывод элемента
            }
            int val = m.Max();
            int max_ind = m.IndexOf(val); //определение индекса максимального элемента
            Console.WriteLine($"Индекс максимального элемента: {max_ind}");
            val=m.Min();
            int min_ind=m.IndexOf(val); //определение индекса минимального элемента
            Console.WriteLine($"Индекс минимального элемента: {min_ind}");
            //вывод элементов между максимальным и минимальным элементами
            for (int i=Math.Min(max_ind,min_ind);i<=Math.Max(max_ind,min_ind);i++)
            {
                Console.WriteLine(m[i]);
            }
        }
    }
}

