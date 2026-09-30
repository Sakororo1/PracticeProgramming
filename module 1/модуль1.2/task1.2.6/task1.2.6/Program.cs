namespace task1_2_6
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r=new Random();
            double[] A=new double[10]; //создание массива размером 10
            Console.WriteLine("Массив:");
            for (int i = 0; i < A.Length; i++) //заполнение массива значениями от -10 до 10
            {
                A[i] = r.NextDouble()*20-10;
                Console.WriteLine(A[i]);
            }
            double[] Sorted = new double[10]; //массив для сортировки
            for (int i = 0; i < Sorted.Length; i++) //копирование массива
            {
                Sorted[i] = A[i];
            }
            //сортировка Sorted гномьей сортировкой
            int j = 0;
            double buf; //буферная переменная для перестановок
            while (j < Sorted.Length - 1) //цикл продолжается до тех пор, пока "гном" не дойдёт до конца массива
            {
                if (Sorted[j] <= Sorted[j + 1]) j++; //Если элементы находятся в правильном порядке, шаг вперёд
                else
                {
                    buf = Sorted[j]; //перестановка элементов местами
                    Sorted[j] = Sorted[j + 1];
                    Sorted[j + 1] = buf;
                    if (j != 0) j--; //Если текущий элемент не первый, то шаг назад
                }
            }
            int[] Ind=new int[10]; //создание массива для индексов
            //заполнение массива индексов
            for (int i = 0; i < Ind.Length; i++)
            {
                Ind[i]=Sorted.IndexOf(A[i]);    
            }
            Console.WriteLine("Массив индексов:");
            foreach (int i in Ind)
            {
                Console.WriteLine(i);
            }
        }
    }
}
