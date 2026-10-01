namespace task3_5
{
    class Program
    {
        class Sort //сортировки
        {
            delegate void SortDelegate(int[] a); //делегат
            SortDelegate sd;
            public void ChooseSort(int[] a, uint c) //выбор сортировки
            {
                switch (c)
                {
                    case 1:
                        sd = Gnome;
                        sd.Invoke(a);
                        break;
                    case 2:
                        sd = Bubble;
                        sd.Invoke(a);
                        break;
                    case 3:
                        sd = Insertion;
                        sd.Invoke(a);
                        break;
                    default:
                        Console.WriteLine("Неизвестная сортировка");
                        break;
                }
            }
            //гномья
            private void Gnome(int[] a)
            {
                int j = 0;
                int buf; //буферная переменная для перестановок
                while (j < a.Length - 1) //цикл продолжается до тех пор, пока "гном" не дойдёт до конца массива
                {
                    if (a[j] >= a[j + 1]) j++; //Если элементы находятся в правильном порядке, шаг вперёд
                    else
                    {
                        buf = a[j]; //перестановка элементов местами
                        a[j] = a[j + 1];
                        a[j + 1] = buf;
                        if (j != 0) j--; //Если текущий элемент не первый, то шаг назад
                    }
                }
            }
            //пузырьковая
            private void Bubble(int[] a)
            {
                int buf;
                for (int i = 0; i < a.Length; i++)
                {
                    for (int j = 0; j < a.Length - i - 1; j++)
                    {
                        if (a[i] > a[j + 1])
                        {
                            buf = a[i]; //перестановка
                            a[j] = a[j + 1];
                            a[j + 1] = buf;
                        }
                    }
                }
            }
            //сортировка простыми вставками
            private void Insertion(int[] a)
            {
                int j;
                for (int i = 1; i < a.Length; i++) //цикл
                {
                    int num = a[i]; //выбор текущего элемента
                    j = i - 1; //индекс предыдущего элемента для смещения
                    while (j >= 0 && a[j] > num) //выбор места для текущего элемента
                    {
                        a[j + 1] = a[j]; //элемент перемещается на индекс назад
                        j--; //перемещение назад
                    }
                    a[j + 1] = num; //вставка неотсортированного элемента на место
                }
            }
        }
        static void Main(string[] args)
        {
            Random r = new Random();
            int[] a = new int[(int)r.NextInt64(5, 100)]; //создание массива
            Console.WriteLine("Массив до сортировки");
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = (int)r.NextInt64(-100, 101);
                Console.WriteLine(a[i]);
            }
            Sort s = new Sort();
            Console.WriteLine("Выберите сортировку (1 - гномья, 2 - пузырьковая, 3 - простыми вставками)");
            s.ChooseSort(a, Convert.ToUInt32(Console.ReadLine())); //сортировка
            foreach (int i in a) //вывод отсортированного массива
            {
                Console.WriteLine(i);
            }
        }
    }
}