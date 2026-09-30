namespace task1_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r=new Random(); //объект для генерации чисел
            double[] a=new double[r.NextInt64(5,16)]; //создание массива
            Console.WriteLine("Изначальный массив:");
            for (byte i = 0; i < a.Length; i++) //заполнение массива случайными элементами
            {
                a[i]= (double)r.NextInt64(-50,51); //присвоение элементу случайного значения от -50 до 50
                Console.WriteLine($"a[{i}]={a[i]}"); //вывод элемента
            }
            double ma = 0; //элемент, на значение которого нужно делить
            for (byte i=0; i < a.Length; i++) //определение наибольшего элемента по модулю
            {
                if (Math.Abs(ma) < Math.Abs(a[i])) //проверка, что текущий элемент больше делителя по модулю
                {
                    ma = a[i]; //текущий элемент - текущий наибольший по модулю
                }
            }
            Console.WriteLine("Изменённый массив:");
            for (byte i = 0;i < a.Length; i++) //изменение массива
            {
                a[i] /= ma; //деление элемента на наибольший в массиве
                Console.WriteLine($"a[{i}]={a[i]}"); //вывод элемента
            }
        }
    }
}
