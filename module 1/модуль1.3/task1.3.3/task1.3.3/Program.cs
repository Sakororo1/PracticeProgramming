namespace task1_3_3
{
    class Program
    {
        //метод для сортировки сумм строк (гномья)
        static void GnomeSort(int[] arr)
        {
            int j = 0;
            int buf; //буферная переменная для перестановок
            while (j < arr.Length - 1) //цикл продолжается до тех пор, пока "гном" не дойдёт до конца массива
            {
                if (arr[j] <= arr[j + 1]) j++; //Если элементы находятся в правильном порядке, шаг вперёд
                else
                {
                    buf = arr[j]; //перестановка элементов местами
                    arr[j] = arr[j + 1];
                    arr[j + 1] = buf;
                    if (j != 0) j--; //Если текущий элемент не первый, то шаг назад
                }
            }
        }
        static void Main(string[] args)
        {
            Random r= new Random();
            int N = (int)r.NextInt64(3,10);
            int[][] Matrix = new int[N][]; //создание зубчатого массива
            for (int i=0;i<N;i++)
            {
                Matrix[i] = new int[N]; //установка длины вторых массивов
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Matrix[i][j] = (int)r.NextInt64(-50, 51); //присвоение значения от -50 до 50
                }
            }
            Console.WriteLine("Квадратная матрица:");
            for (int i = 0; i < N; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < N; j++)
                {
                    Console.Write($"{Matrix[i][j]} "); //вывод элемента
                }
                Console.WriteLine($" | сумма: {Matrix[i].Sum()}");
            }
            int[] sums=new int[N]; //массив сумм строк
            for (int i = 0;i < N; i++)
            {
                sums[i] = Matrix[i].Sum(); //увеличение суммы i-той строки
            }
            GnomeSort(sums); //сортировка сумм
            int[] buf = new int[N]; //буферный массив для перестановки строк
            for (int i = 0; i<N; i++)
            {
                for (int j = 0;j < N; j++)
                {
                    if (Matrix[j].Sum() == sums[i])
                    {
                        buf = Matrix[j];
                        Matrix[j] = Matrix[i];
                        Matrix[i] = buf;
                    }
                }
            }
            Console.WriteLine("Изменённая матрица:");
            for (int i = 0; i < N; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < N; j++)
                {
                    Console.Write($"{Matrix[i][j]} "); //вывод элемента
                }
                Console.WriteLine($" | сумма: {Matrix[i].Sum()}");
            }

        }
    }
}
